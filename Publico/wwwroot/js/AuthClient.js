"use strict";
var app = new Vue({
	el: '#app',
	methods: {
		// Регистрирует пользователя
		onRegister: () => {
			app.onValidEmail();
			app.onValidLogin();
			app.onValidPassword();
			app.onCheckPasswordFields();
			// Получение данных с форм ввода
			var login = $("#exampleInputLogin").val();
			var email = $("#exampleInputEmail").val();
			var password = $("#exampleInputPassword").val();
			const url = "https://localhost:44323/api/odata/auth/create";
			// Объект с данными для бэка
			var User = {
				Login: login,
				Email: email,
				Password: password
			};
			// Отправляет данные на бэк
			axios.post(url, User)
				.then((response) => {
					console.log(response);
					window.location.href = "https://localhost:44323/Home/GoToLogin";
				})
				.catch((XMLHttpRequest, textStatus, errorThrown) => {
					console.log("request send error", XMLHttpRequest.response.data);
				});
		},
		// Проверяет существование пользователя в БД
		onSignIn: () => {
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
					console.log(response);
					localStorage.setItem("user", response.data.userName);
					localStorage.setItem("user_id", response.data.id);
					// Проверяет есть ли у пользователя токен
					var name = localStorage.getItem("user");
					if (name !== "" && name !== undefined) { 
						window.location.href = "https://localhost:44323/Home/GoToChat";
					}
				})
				.catch((XMLHttpRequest, textStatus, errorThrown) => {
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