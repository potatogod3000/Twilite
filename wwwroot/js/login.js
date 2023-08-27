var userNameElement = document.getElementById("username-input");
var passwordElement = document.getElementById("password-input");
var userName;

console.log(userNameElement);
console.log(passwordElement);

function getText() {
    userName = userNameElement.textContent;
    console.log(userName);
}

document.getElementById("login-button").onclick = function() {getText()};