"use strict";
var app = new Vue({
	el: '#app',
	methods: {
		// Регистрирует пользователя
		onRegister: () => {
			app.onCheckEmail();
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
					console.log("request send error", XMLHttpRequest.response.data);
				});
		},
		// Валидация почты
		onCheckEmail: () => {
			let reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;			
			let checkField = $("#exampleInputEmail").val();
			if (reg.test(checkField) === false) {
				$("#idValidationEmail").html("Введите корректный email");
				$("#idValidationEmail").addClass("validation-email");
				return false;
			}
		}
	}
});