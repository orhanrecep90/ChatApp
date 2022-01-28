
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

function importMessages(user, time, message) {
    var currentUser = document.getElementById("userName").value;

    var divmessageLeft = document.createElement("div");
    divmessageLeft.classList.add('chat-message');
    if (currentUser == user) {
        divmessageLeft.classList.add('left');
    }
    else {

        divmessageLeft.classList.add('right');
    }

    var divmessage = document.createElement("div");
    divmessage.classList.add('message');

    var aAuthor = document.createElement("strong");
    aAuthor.classList.add('message-author');
    aAuthor.innerHTML = user;

    var spanTime = document.createElement("span");
    spanTime.classList.add('message-date');
    spanTime.innerHTML = time;

    var spanContent = document.createElement("span");
    spanContent.classList.add('message-content');
    spanContent.innerHTML = message;

    divmessage.appendChild(aAuthor);
    divmessage.appendChild(spanTime);
    divmessage.appendChild(spanContent);
    divmessageLeft.appendChild(divmessage);
    document.getElementById("messagesArea").appendChild(divmessageLeft);
    scrollBottom();
}


connection.on("ReceiveMessage", function (user, time, message) {
    importMessages(user, time, message);
    

    //var stringToHTML = function () {
    //    var str = `<div class="chat-messageleft"><div class="message"><a class="message-author" href="#">${user}</a><span class="message-date">${time}</span><span class="message-content">${message}</span></div></div>`;
    //    var parser = new DOMParser();
    //    var doc = parser.parseFromString(str, 'text/html');
    //    return doc.body;
    //};
    //console.log(stringToHTML);

    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.`
    //li.textContent = `${user} says ${message}`;



});

connection.on("AddToGroup", function (group) {
    debugger;
    console.log(group);
    var user, time, message;
    group.forEach(obj => {
        Object.entries(obj).forEach(([key, value]) => {
            debugger;
            if (key == 'user') {
                user = value;
            } if (key == 'sentTime') {
                time = value;
            } if (key == 'text') {
                message = value;
            }
            console.log(`${key} ${value}`);
        });
        console.log('-------------------');
        importMessages(user, time, message);
    });
});

function scrollBottom() {

    var focusBottom = document.getElementById("messagesArea");
    focusBottom.scrollTop = focusBottom.scrollHeight;
}

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userName").value;
    var group = document.getElementById("groupName").value;
    var message = document.getElementById("messageInput");


    connection.invoke("SendMessageToGroup", group, user, message.value).catch(function (err) {
        return console.error(err.toString());
    });
    message.value = "";
    event.preventDefault();
});

function fncjoinGroup(group) {
    //var group = this.innerText;
    var extGroup = document.getElementById("groupName").value;
    connection.invoke("RemoveFromGroup", extGroup).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("messagesArea").innerHTML = "";
    document.getElementById("groupName").value = group;    
    connection.invoke("AddToGroup", group).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("chatTex").style.display = "";
}

//document.getElementById("joinGroup").addEventListener("click", function (event) {
//    var group = this.innerText;

//    connection.invoke("AddToGroup", group).catch(function (err) {
//        return console.error(err.toString());
//    });

//    event.preventDefault();
//});


document.getElementById("SaveUserName").addEventListener("click", function (event) {
    var user = document.getElementById("userName").value;
    if (user != "") {
        document.getElementById("chatAppMain").style.display = "";
        document.getElementById("SaveUserName").style.display = "none";
        document.getElementById("userName").readOnly = true;
    }

    event.preventDefault();
});