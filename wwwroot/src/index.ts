import * as signalR from "@microsoft/signalr";

// items

// main connection
const connection = new signalR.HubConnectionBuilder()
    .withUrl("mini-chat-connection")
    .withAutomaticReconnect()
    .build();

// listeners

connection.on("user-count-changed", (userCount) => {

});


connection.on("message-received", (messageData) => {

})

connection.onreconnected(userId => {

});

connection.onreconnecting(error => {

});

// invoke