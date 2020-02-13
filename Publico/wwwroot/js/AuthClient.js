"use strict";
var app = new Vue({
	el: '#app',
	methods: {
		// Функция регистрации пользователя
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
		}
	}
});