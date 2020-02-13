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
			// Объект с данными для бэка
			var User = {
				Login: login,
				Email: email,
				Password: password
			};
			// Отправляет данные на бэк
			$.ajax({
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				type: 'POST',
				url: 'https://localhost:44323/api/odata/auth/create', 
				data: JSON.stringify(User), 
				dataType: 'json', 
				success: function (data) {
					console.log(data);
				}, 
				error: function (jqXHR, text, error) {
					console.log('Ошибка выполнения');
				}
			});
		},
		// Проверяет существование пользователя в БД
		onSignIn: function () {
			// Получение данных с форм ввода
			var login = document.getElementById("exampleInputLogin").value;
			var password = document.getElementById("exampleInputPassword").value;
			// Объект с данными для бэка
			var UserReg = {
				Login: login,
				Password: password
			};
			// Отправляет данные на бэк
			$.ajax({
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				type: 'POST',
				url: 'https://localhost:44323/api/odata/auth/signin',
				data: JSON.stringify(UserReg),
				dataType: 'json',
				success: function (data) {
					console.log(data);
				},
				error: function (jqXHR, text, error) {
					console.log('Ошибка выполнения');
				}
			});
		}
	}
});