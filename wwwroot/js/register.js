var enteredEmail = document.getElementById("enterEmail"),
enteredUserName = document.getElementById("enterUserName"),
enteredPassword = document.getElementById("enterPassword"),
reEnteredPassword = document.getElementById("reEnterPassword"),
confirmMessage = document.getElementById("confirmMessage"),
registerButton = document.getElementById("registerButton");

var passwordMatch = {"confirmMessage": ["Matches Password.", "Does not match Password!"]};

function checkRePassword() {
    if(enteredPassword.value === reEnteredPassword.value && (enteredPassword.value + reEnteredPassword.value) !== "") {
        rePasswordMessage.innerHTML = passwordMatch["confirmMessage"][0];
        rePasswordMessage.style.color = "green";
        registerButtonState(true);
        
    }
    else if(enteredPassword.value !== reEnteredPassword.value && (enteredPassword.value + reEnteredPassword.value) !== "") {
        rePasswordMessage.innerHTML = passwordMatch["confirmMessage"][1];
        rePasswordMessage.style.color = "rgb(220, 53, 69)";
        rePasswordMessage.style.textOpacity = "1";
        registerButtonState(false);
    }
}

function registerButtonState(state) {
    if(state) {
        registerButton.style.backgroundColor = "";
        registerButton.style.borderColor = "";
        registerButton.style.color = "";
        registerButton.disabled = false;
    }
    else {
        registerButton.style.backgroundColor = "grey";
        registerButton.style.borderColor = "grey";
        registerButton.style.color = "white";
        registerButton.disabled = true;
    }
}

reEnteredPassword.addEventListener("input", checkRePassword);
enteredPassword.addEventListener("input", checkRePassword);
registerButton.onload = registerButtonState(false);