var reEnteredPassword = document.getElementById("reEnterPassword"),
enteredPassword = document.getElementById("enterPassword"),
confirmMessage = document.getElementById("confirmMessage"),
registerButton = document.getElementById("registerButton");

function checkRePassword() {
    strings = {"confirmMessage": ["Matches Password.", "Does not match Password!"]};

    if(enteredPassword.value === reEnteredPassword.value && (enteredPassword.value + reEnteredPassword.value) !== "") {
        confirmMessage.innerHTML = strings["confirmMessage"][0];
        confirmMessage.style.color = "green";
        registerButton.style.backgroundColor = "";
        registerButton.style.borderColor = "";
        registerButton.style.color = "";
        registerButton.disabled = false;
    }
    else if(enteredPassword.value !== reEnteredPassword.value && (enteredPassword.value + reEnteredPassword.value) !== "") {
        confirmMessage.innerHTML = strings["confirmMessage"][1];
        confirmMessage.style.color = "red";
        confirmMessage.style.fontWeight = "bold";
        registerButton.style.backgroundColor = "grey";
        registerButton.style.borderColor = "grey";
        registerButton.style.color = "white";
        registerButton.disabled = true;
    }
    else {
        message.innerHTML = "";	
    }
}

reEnteredPassword.addEventListener("input", checkRePassword);
enteredPassword.addEventListener("input", checkRePassword);