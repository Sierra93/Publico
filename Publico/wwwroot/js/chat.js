"use strict";
$(function () {
    app.onInit();
});
var userName = localStorage.getItem("user");
var elem = document.getElementById("username");
elem.textContent = userName;

var app = new Vue({
    el: '#app',
    data: {
        friends: localStorage.getItem("friends").split(","),
        nameFriend: localStorage.getItem("indFriend")
    },
    methods: {
        onInit: () => {
            let userId = localStorage.getItem("user_id");
            let url = "https://localhost:44323/api/odata/data/getfriends?id=" + userId;
            let friendArr = [];
            axios.get(url)
                .then((response) => {
                    // Перебор результирующего массива и добавления имен друзей в отдельный массив
                    response.data.forEach(function (el) {
                        friendArr.push(el.friendLogin);
                    });
                    // Записывает список друзей в кэш
                    localStorage.setItem("friends", friendArr);
                })
                .catch((XMLHttpRequest, textStatus, errorThrown) => {
                    console.log("request send error", XMLHttpRequest.response.data);
                });
        },
        // Отправляет сообщение
        onSendMessage: () => {     
            //let numChat = null;
            let sUrl = "https://localhost:44323/api/odata/data/sendmessage";
            //let chatId = localStorage.getItem("chat_id");            
            // Получает введенное сообщение
            var sMessage = document.getElementById("messageInput").value;
            let numChat = app.getRandomNumber(1000);
            // Проверяет, писали ли мы уже пользователю(был ли уже создан ID чата)
            //if (chatId === null) {
            //    // Получает случайное число для ID чата
            //    numChat = app.getRandomNumber(1000);
            //}
            let oData = {
                MessageUserId: localStorage.getItem("user_id"),
                MessageBody: sMessage,
                ChatId: numChat.toString()
            };  
            // Очищает поле сразу после отправки
            document.getElementById("messageInput").value = "";                      
            axios.post(sUrl, oData)
                .then((response) => {
                    console.log(response);
                })
                .catch((XMLHttpRequest, textStatus, errorThrown) => {
                    console.log("request send error", XMLHttpRequest.response.data);
                });
        },
        // Создает случайное число, которое будет ID чата
        getRandomNumber: (max) => {
            return Math.floor(Math.random() * Math.floor(max));
        },
        // Добавляет друга
        onAddFriend: () => {
            let url = "https://localhost:44323/api/odata/data/addfriend";
            let oData = {
                UserId: localStorage.getItem("user_id").toString(),
                FriendLogin: document.getElementById("add-friend").value,
                Status: 0
            };
            axios.post(url, oData)
                .then(() => {
                })
                .catch((XMLHttpRequest, textStatus, errorThrown) => {
                    console.log("request send error", XMLHttpRequest.response.data);
                });
        },
        // Передает имя друга, которому хотим написать
        onSelectFriend: () => {
            // Получает имя друга, которому пишем
            let indFriend = event.target.parentElement.childNodes[0].data;
            localStorage.setItem("indFriend", indFriend);
        }
        // Получает все сообщения выбранного чата
        //onGetMessages: () => {

        //}
    }
});