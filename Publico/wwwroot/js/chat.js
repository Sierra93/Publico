"use strict";
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
            let toUserName = localStorage.getItem("indFriend");
            const sUrl = "https://localhost:44323/api/odata/data/sendmessage";
            const sUrlUser = "https://localhost:44323/api/odata/data/getuser?user=" + toUserName;
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
                    console.log(XMLHttpRequest);
                });
        },
        // Передает имя друга, которому хотим написать
        onSelectFriend: () => {
            event.preventDefault();
            //let encodedMsg;
            //let li;
            let messages = localStorage.getItem("messages").split(",");
            //let msg = messages.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
            messages.forEach((el) => {
                let encodedMsg = userName + ": " + el;
                let li = document.createElement("li");
                li.textContent = encodedMsg;
                document.getElementById("messagesList").appendChild(li);
            });
            // Получает имя друга, которому пишем
            let indFriend = event.target.parentElement.childNodes[0].data;
            localStorage.setItem("indFriend", indFriend);
            app.onGetMessages();
        },
        // Получает все сообщения выбранного чата
        onGetMessages: () => {
            event.preventDefault();
            let userIdTo;
            let arrMessages = [];
            let userNameTo = localStorage.getItem("indFriend");
            let userIdFrom = +localStorage.getItem("user_id");
            const sUrl = "https://localhost:44323/api/odata/data/getmessages";
            //const sUrlCheckUser = "https://localhost:44323/api/odata/data/getuser?user=" + userNameTo;
            let urlUser = "https://localhost:44323/api/odata/data/getuserpost";
            let promise = new Promise((resolve, reject) => {
                setTimeout(() => {
                    let oLayout = {
                        UserIdFrom: userIdFrom,
                        UserNameTo: userNameTo
                    };
                    // Проверяет существует ли пользователь
                    axios.post(urlUser, oLayout)
                        .then((response) => {
                            console.log(response.data);
                            userIdTo = +response.data.id;
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
                                FromUserId: userIdFrom,
                                ToUserId: userIdTo
                            };
                            // Получает список сообщений пользователя с которым идет общение в данный момент
                            axios.post(sUrl, oData)
                                .then((response) => {
                                    response.data.forEach((el) => {
                                        arrMessages.push(el.messages);
                                    });
                                    localStorage.setItem("messages", arrMessages);
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
                    console.log(XMLHttpRequest);
                });
        }
    }
});