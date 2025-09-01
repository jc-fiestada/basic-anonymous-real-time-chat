import * as signalR from "@microsoft/signalr";

// items
const userForm = <HTMLFormElement> document.getElementById("user-form");
const worldChat = <HTMLLIElement> document.getElementById("world-chat-list");
const counter = <HTMLSpanElement> document.getElementById("counter");



// main connection
const connection = new signalR.HubConnectionBuilder()
    .withUrl("mini-chat-connection")
    .withAutomaticReconnect()
    .build();

// listeners

connection.on("user-count-changed", (userCount: number) => {
    counter.innerText = `${userCount}`;
});


connection.on("message-received", (messageData: any) => {
    worldChat.innerHTML += `<li><strong>${messageData.user}:</strong> ${messageData.message} - ${messageData.date}}</li>`;
})

connection.onreconnected(userId => {
    alert(`Successfully Reconnected - UserId(${userId})`);
});

connection.onreconnecting(error => {
    alert(`Error: ${error}`)
});

// invoke

function SendMessageListener()
{   
    userForm.addEventListener("submit", async (e) => {
        e.preventDefault();
        const message = <HTMLInputElement>document.getElementById("message");
        const user = <HTMLInputElement>document.getElementById("user");

        await connection.invoke("SendMessage", message.value, user.value);
    })
    
}

function start(){
    connection.start();
    SendMessageListener();
}

start();