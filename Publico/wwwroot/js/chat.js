"use strict";
$(function () {
    app.onInit();
});
var userName = localStorage.getItem("user");
var elem = document.getElementById("username");
elem.textContent = userName;

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.on("ReceiveMessage", function (userName, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = userName + ": " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

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
                    console.log(response);
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
        onSendMessage: (event) => {
            var message = document.getElementById("messageInput").value;
            connection.invoke("SendMessage", userName, message).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
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
                .then((response) => {
                    console.log(response);
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
    }
});