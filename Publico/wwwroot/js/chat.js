"use strict";
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
    methods: {
        onSendMessage: function (event) {   
            var message = document.getElementById("messageInput").value;
            connection.invoke("SendMessage", userName, message).catch(function (err) {
                return console.error(err.toString());
            });            
            event.preventDefault();
        },
        // Добавляет друга
        onAddFriend: () => {
            let url = "https://localhost:44323/api/odata/data/addfriend";
            //let whoFriend = document.getElementById("add-friend").value;
            //let userId = localStorage.getItem("user_id").toString();
            //let status = "0";
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
        }
    }
});