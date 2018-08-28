//import { create } from "domain";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

let userName = '';
let direction = "";
let oldImgName;
let id;
let start = false;

connection.on('Send', function (message, userName) {

    let userNameElem = document.createElement("b");
    userNameElem.appendChild(document.createTextNode(userName + ': '));

    let elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message));

    var firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
});

connection.on('Lost', function (ghost, pacman) {
    debugger;
    document.getElementById("score").innerHTML = "You lost!!!" + " Press SPACE for continue!!!";
    start = false;
    Clear(ghost, pacman)
});

connection.on("Win", function () {
    AfterGameMenu("You Win!!!", "Start new game!!!");
})

connection.on('GameOver', function () {
    debugger;
    AfterGameMenu("GameOver!!!", "Start new game!!!");
});

function AfterGameMenu(title, text) {
    var chat = document.getElementById("chat-form");
    while (chat.firstChild)
        chat.firstChild.remove();
    var map = document.getElementById("gameMap");
    while (map.firstChild)
        map.firstChild.remove();
    map.id = "afterGameMenu-form";
    var h1Title = document.createElement("h1");
    h1Title.innerHTML = title;
    var lbText = document.createElement("p");
    lbText.innerHTML = text;
    var fieldset = document.createElement("fieldset");

    var btnOk = document.createElement("input");
    btnOk.setAttribute("type", "submit");
    btnOk.setAttribute("value", "Yes");
    btnOk.setAttribute("onclick", "window.location.reload()");

    var btnCancel = document.createElement("input");
    btnCancel.setAttribute("type", "submit");
    btnCancel.setAttribute("value", "No");
    btnCancel.setAttribute("onclick", "window.history.back()");

    fieldset.appendChild(h1Title);
    fieldset.appendChild(lbText);
    fieldset.appendChild(btnOk);
    fieldset.appendChild(btnCancel);
    map.appendChild(fieldset);
}

function Clear(ghost, pacman) {
    GhostMove(ghost.getPosOldX, ghost.getPosOldY, ghost.startPositionX, ghost.startPositionY, ghost.ghostColor);

    PacManMove(pacman.getPositionOldX, pacman.getPositionOldY, pacman.startPositionX, pacman.startPositionY, "/images/PacMan_right.png");
}

connection.on('Move', function (pacMan, direction, name) {
    document.getElementById("score").innerHTML = "Name: " + name + "; Score: " + pacMan.score + "; Life: " + pacMan.life;
    switch (direction) {
        case "up":
            PacManMove(pacMan.getPositionOldX, pacMan.getPositionOldY, pacMan.getPositionNewX, pacMan.getPositionNewY, "/images/PacMan_up.png");
            break;
        case "down":
            PacManMove(pacMan.getPositionOldX, pacMan.getPositionOldY, pacMan.getPositionNewX, pacMan.getPositionNewY, "/images/PacMan_down.png");
            break;
        case "right":
            PacManMove(pacMan.getPositionOldX, pacMan.getPositionOldY, pacMan.getPositionNewX, pacMan.getPositionNewY, "/images/PacMan_right.png");
            break;
        case "left":
            PacManMove(pacMan.getPositionOldX, pacMan.getPositionOldY, pacMan.getPositionNewX, pacMan.getPositionNewY, "/images/PacMan_left.png");
            break;
    }
    connection.invoke('Move', pacMan.direction, window.location.search);
});

function PacManMove(oldX, oldY, newX, newY, image) {
    var oldPacman = document.getElementById("pacman_" + oldY + "-" + oldX);
    oldPacman.removeChild(oldPacman.childNodes[0]);
    oldPacman.id = "empty_" + oldY + "-" + oldX;
    oldPacman.classList = ["empty"];
    var img = document.createElement("img");
    img.classList.add('pacman_img');
    img.src = "/images/empty.png";
    oldPacman.appendChild(img);

    var newPacman = document.getElementById("dot_" + newY + "-" + newX);
    newPacman = !newPacman ? document.getElementById("empty_" + newY + "-" + newX) : newPacman;
    newPacman.id = "pacman_" + newY + "-" + newX;
    newPacman.classList = ["pacman"];
    newPacman.removeChild(newPacman.childNodes[0]);
    var img = document.createElement("img");
    img.classList.add('pacman_img');
    img.src = image;
    newPacman.appendChild(img);
}

connection.on("MoveGhost", function (ghost) {
    GhostMove(ghost.getPosOldX, ghost.getPosOldY, ghost.getPosNewX, ghost.getPosNewY, ghost.ghostColor);
    connection.invoke('Ghost', window.location.search);
});

function GhostMove(oldX, oldY, newX, newY, color) {
    var ghostStep = document.getElementById("empty_" + newX + "-" + newY);
    if (ghostStep != null) {
        ChangeGhost("empty_", 'empty', ghostStep, 'empty', oldX, oldY, newX, newY, color);
    }
    ghostStep = document.getElementById("dot_" + newX + "-" + newY);
    if (ghostStep != null) {
        ChangeGhost("dot_", 'dot', ghostStep, 'point', oldX, oldY, newX, newY, color);
    }
    ghostStep = document.getElementById("star_" + newX + "-" + newY);
    if (ghostStep != null) {
        ChangeGhost("star_", 'dot', ghostStep, 'Star', oldX, oldY, newX, newY, color);
    }
}

function ChangeGhost(id, className, ghostStep, imgName, oldX, oldY, newX, newY, color) {
    ghostStep.id = "ghost_" + color + "_" + newX + "-" + newY;
    ghostStep.classList = ["ghost"];
    oldImgName = ghostStep.childNodes[0];
    ghostStep.removeChild(ghostStep.childNodes[0]);
    var img = document.createElement("img");
    img.classList.add('ghost_img');
    var image;
    switch (color) {
        case "G":
            image = "/images/ghost_green.png";
            break;
        case "B":
            image = "/images/ghost_black.png";
            break;
        case "R":
            image = "/images/ghost_red.png";
            break;
        case "P":
            image = "/images/ghost_pinck.png";
            break;
    }
    img.src = image;
    ghostStep.appendChild(img);

    var oldGhost = document.getElementById("ghost_" + color + "_" + oldX + "-" + oldY);
    oldGhost.id = id + oldX + "-" + oldY;
    oldGhost.classList = [className];
    oldGhost.removeChild(oldGhost.childNodes[0]);
    var imgNext = document.createElement("img");
    imgNext.classList.add('dot_img');
    imgNext.src = "/images/" + imgName + ".png";
    oldGhost.appendChild(oldImgName);
}

document.addEventListener("keydown", checkKeyPress, false);
//37-left
//38-top
//39-right
//40-bottom

function checkKeyPress(event) {
    switch (event.keyCode) {
        case 32:
            if (!start) {
                direction = "right";
                connection.invoke('Move', direction, window.location.search);
                connection.invoke('Ghost', window.location.search);
                start = true;
            }
            break;
        case 37:
            direction = "left";
            connection.invoke('Move', direction, window.location.search);
            break;
        case 38:
            direction = "up";
            connection.invoke('Move', direction, window.location.search);
            break;
        case 39:
            direction = "right";
            connection.invoke('Move', direction, window.location.search);
            break;
        case 40:
            direction = "down";
            connection.invoke('Move', direction, window.location.search);
            break;
    }
}

// отправка сообщения на сервер
document.getElementById("sendBtn").addEventListener("click", function (e) {
    let message = document.getElementById("message").value;
    connection.invoke('Send', message);
});
connection.start();
