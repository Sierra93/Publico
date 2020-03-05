﻿"use strict";
$(function () {
    app.onInit();
});
var userName = localStorage.getItem("user");
$("#username").text(userName);

var app = new Vue({
    el: '#app',
    data: {
        friends: localStorage.getItem("friends").split(","),
        nameFriend: localStorage.getItem("indFriend")
    },
    methods: {
        onInit: () => {
            let userId = localStorage.getItem("user_id");
            const url = "https://localhost:44323/api/odata/data/getfriends?id=" + +userId;
            let friendArr = [];
            axios.get(url)
                .then((response) => {
                    // Перебор результирующего массива и добавления имен друзей в отдельный массив
                    response.data.forEach(function (el) {
                        friendArr.push(el.friends);
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
            const sUrl = "https://localhost:44323/api/odata/data/sendmessage";
            const sUrlUser = "https://localhost:44323/api/odata/data/getuser?user=" + userName;
            // Получает введенное сообщение
            var sMessage = $("#messageInput").val();
            var toUserId;
            // Очищает поле сразу после отправки
            $("#messageInput").val("");
            let promise = new Promise((resolve, reject) => {
                setTimeout(() => {
                    // Проверяет существует ли пользователь
                    axios.get(sUrlUser)
                        .then((response) => {
                            console.log(response.data);
                            toUserId = response.data.id;
                            resolve();
                        })
                        .catch((XMLHttpRequest) => {
                            // Если пользователя не существует
                            if (XMLHttpRequest.response.status === 500) {
                                console.log("Пользователя не существует.");
                            }
                            reject();
                        });
                }, 1);
            });
            promise
                .then(() => {
                    return new Promise((resolve, reject) => {
                        setTimeout(() => {
                            let oData = {
                                FromUserId: +localStorage.getItem("user_id"),
                                ToUserId: +toUserId,
                                Message: sMessage
                            };
                            // Отправляет сообщение
                            axios.post(sUrl, oData)
                                .then((response) => {
                                    console.log(response);
                                    resolve();
                                })
                                .catch((XMLHttpRequest, textStatus, errorThrown) => {
                                    console.log("request send error", XMLHttpRequest.response.data);
                                    reject();
                                });
                            resolve();
                        }, 2);
                    });
                }).catch(XMLHttpRequest => {
                    alert(XMLHttpRequest);
                });
        },        
        // Добавляет друга
        onAddFriend: () => {
            const url = "https://localhost:44323/api/odata/data/addfriend";
            let user = $("#add-friend").val();
            const sUrlUser = "https://localhost:44323/api/odata/data/getuser?user=" + user;
            var toUserId;
            // Через call-back's сначала проверяет существует ли пользователь, затем уже добавляет его в друзья
            let promise = new Promise((resolve, reject) => {
                setTimeout(() => {
                    // Проверяет существует ли пользователь
                    axios.get(sUrlUser)
                        .then((response) => {
                            console.log(response.data);
                            toUserId = response.data.id;
                            resolve();
                        })
                        .catch((XMLHttpRequest) => {
                            // Если пользователя не существует
                            if (XMLHttpRequest.response.status === 500) {
                                console.log("Пользователя не существует. Добавление в друзья невозможно.");
                            }
                            reject();
                        });
                }, 1);
            });
            promise
                .then(() => {
                    return new Promise((resolve, reject) => {
                        setTimeout(() => {
                            let oData = {
                                UserId: +localStorage.getItem("user_id"),
                                ToUserId: +toUserId,
                                Type: "1"
                            };
                            // Добавляет пользователя в друзья
                            axios.post(url, oData)
                                .then(() => {
                                    resolve();
                                })
                                .catch((XMLHttpRequest, textStatus, errorThrown) => {
                                    console.log("request send error", XMLHttpRequest.response.data);
                                    reject();
                                });
                            resolve();
                        }, 2);
                    });
                }).catch(XMLHttpRequest => {
                    alert(XMLHttpRequest);
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