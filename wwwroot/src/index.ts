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

function AllMessage(){
    document.addEventListener("DOMContentLoaded", async () => {
        const response = await fetch("/get-all-messages", {
            method : "GET",
            headers : {"Content-Type" : "application/json"}
        });

        const data = await response.json();

        console.table(data);
        
        data.forEach(msg => {
            worldChat.innerHTML += `<li><strong>${msg.user}:</strong> ${msg.message} - ${msg.date}</li>`;
        });
    });
}

function start(){
    connection.start();
    SendMessageListener();
    AllMessage();
}

start();