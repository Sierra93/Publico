"use strict";
$(function() {
    
});
var userName = localStorage.getItem("user");
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
    data: {},
    methods: {
        onSendMessage: function (event) {   
            var message = document.getElementById("messageInput").value;
            var userName = localStorage.getItem("user");
            //connection.invoke("SendMessage", userName, message).catch(function (err) {
            //    return console.error(err.toString());
            //});            
            event.preventDefault();
        }
    }
});