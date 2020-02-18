"use strict";
var app = new Vue({
	el: '#app',
	methods: {
		// Регистрирует пользователя
		onRegister: function () {
			// Получение данных с форм ввода
			var login = document.getElementById("exampleInputLogin").value;
			var email = document.getElementById("exampleInputEmail").value;
			var password = document.getElementById("exampleInputPassword").value;
			var url = "https://localhost:44323/api/odata/auth/create";
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
				})
				.catch((XMLHttpRequest, textStatus, errorThrown) => {
					console.log("request send error", XMLHttpRequest.response.data);
				});
		},
		// Проверяет существование пользователя в БД
		onSignIn: function () {
			// Получение данных с форм ввода
			var login = document.getElementById("exampleInputLogin").value;
			var password = document.getElementById("exampleInputPassword").value;
			var url = "https://localhost:44323/api/odata/auth/signin";
			// Объект с данными для бэка
			var UserReg = {
				Login: login,
				Password: password
			};
			// Отправляет данные на бэк
			axios.post(url, UserReg)
				.then((response) => {
					console.log(response);
					localStorage.setItem("token", response.data.access_token);
					//window.location.href = "https://localhost:44323/Home/GoToChat";
					//// Проверяет есть ли у пользователя токен
					var token = localStorage.getItem("token");
					if (token !== "" && token !== undefined) {
						window.location.href = "https://localhost:44323/Home/GoToChat";
					}
					// Иначе выбросит на главную
					else {
						window.location.href = "https://localhost:44323/";
					}
				})
				.catch((XMLHttpRequest, textStatus, errorThrown) => {
					console.log("request send error", XMLHttpRequest.response.data);
				});
		}
	}
});