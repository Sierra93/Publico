"use strict";
$(function () {
	if (localStorage.getItem("token") !== null) {
		window.location.href = "https://localhost:44323/Home/GoToChat";
	}
});
var app = new Vue({
	el: '#app',
	methods: {
		// Регистрирует пользователя
		onRegister: () => {
			app.onValidEmail();
			app.onValidLogin();
			app.onValidPassword();
			app.onCheckPasswordFields();
			$(".btn-register").prop('disabled', true);
			// Получение данных с форм ввода
			var login = $("#exampleInputLogin").val();
			var email = $("#exampleInputEmail").val();
			var password = $("#exampleInputPassword").val();
			const url = "https://localhost:44323/api/odata/auth/create";
			const sUrlLogin = "https://localhost:44323/api/odata/auth/checklogin?login=" + login;
			const sUrlEmail = "https://localhost:44323/api/odata/auth/checkemail?email=" + email;
			// Объект с данными для бэка
			var User = {
				Login: login,
				Email: email,
				Password: password
			};
			let promise = new Promise((resolve, reject) => {
				setTimeout(() => {
					// Проверяет существует ли уже такой логин
					axios.get(sUrlLogin)
						.then((response) => {
							$(".btn-register").prop('disabled', false);
							console.log(response);
							resolve();
						})
						.catch((XMLHttpRequest) => {
							$(".btn-register").prop('disabled', false);
							console.log("request send error", XMLHttpRequest.response.data);
							reject();
						});
				}, 1);
			});
			promise.then(() => {
				return new Promise((resolve, reject) => {
					setTimeout(() => {
						// Проверяет существует ли уже такой email
						axios.get(sUrlEmail)
							.then((response) => {
								console.log(response);
								resolve();
							})
							.catch((XMLHttpRequest) => {
								console.log("request send error", XMLHttpRequest.response.data);
								reject();
							});
					}, 2);
				});
			});
			promise.then(() => {
				return new Promise((resolve, reject) => {
					setTimeout(() => {
						// Отправляет данные на бэк
						axios.post(url, User)
							.then((response) => {
								console.log(response);
								window.location.href = "https://localhost:44323/Home/GoToLogin";
							})
							.catch((XMLHttpRequest) => {
								console.log("request send error", XMLHttpRequest.response.data);
							});
					}, 3);
				});
			}).catch(XMLHttpRequest => {
				console.log(XMLHttpRequest);
			});			
		},
		// Проверяет существование пользователя в БД
		onSignIn: () => {
			$(".btn-login").prop('disabled', true);
			// Получение данных с форм ввода
			var login = $("#exampleInputLogin").val();
			var password = $("#exampleInputPassword").val();
			const url = "https://localhost:44323/api/odata/auth/signin";
			// Объект с данными для бэка
			var UserReg = {
				Login: login,
				Password: password
			};
			// Отправляет данные на бэк
			axios.post(url, UserReg)
				.then((response) => {
					$(".btn-login").prop('disabled', false);
					console.log(response);
					localStorage.setItem("user", response.data.userName);
					localStorage.setItem("user_id", response.data.id);
					localStorage.setItem("token", response.data.access_token);
					var userToken = localStorage.getItem("token");
					// Проверяет наличие пользователя и токена
					var name = localStorage.getItem("user");
					if (name !== "" && name !== undefined && userToken !== null) { 
						window.location.href = "https://localhost:44323/Home/GoToChat";
					}
				})
				.catch((XMLHttpRequest) => {
					$(".btn-login").prop('disabled', false);
					$("#idCheckAuthorization").addClass("check-authorization");
					$("#idCheckAuthorization").html("Логин или пароль введены не верно");
					console.log("request send error", XMLHttpRequest.response.data);
				});
		},
		// Валидация почты
		onValidEmail: () => {
			let reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;			
			let checkField = $("#exampleInputEmail").val();
			if (reg.test(checkField) === false) {
				$("#idValidationEmail").html("Введите корректный email");
				$("#idValidationEmail").addClass("validation-email");
				throw new Error("Введите корректный email");
			}
		},
		// Валидация логина
		onValidLogin: () => {
			let fieldLogin = $("#exampleInputLogin").val();
			if (fieldLogin === "") {
				$("#idValidationLogin").html("Поле логина не может быть пустым");
				$("#idValidationLogin").addClass("validation-login");
				throw new Error("Поле логина не может быть пустым");
			}
		},
		// Валидация пароля
		onValidPassword: () => {
			let fieldPassword = $("#exampleInputPassword").val();
			if (fieldPassword === "") {
				$("#idValidationPassword").html("Поле пароля не может быть пустым");
				$("#idValidationPassword").addClass("validation-password");
				throw new Error("Поле пароля не может быть пустым");
			}
		},
		// Проверка на совпадение паролей
		onCheckPasswordFields: () => {
			let sPasswordFirstField = $("#exampleInputPassword").val();
			let sPasswordSecondField = $("#exampleRepeatInputPassword").val();
			if (sPasswordFirstField !== sPasswordSecondField) {
				$("#idValidationPassword").html("Пароли не совпадают");
				$("#idValidationPassword").addClass("validation-fields-password");
				throw new Error("Пароли не совпадают");
			}
		}
	}
});