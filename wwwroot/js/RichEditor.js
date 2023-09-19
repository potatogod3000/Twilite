// Button and Element Variables
const undo = document.getElementById("undo");
const redo = document.getElementById("redo");
const bold = document.getElementById("bold");
const italic = document.getElementById("italic");
const underline = document.getElementById("underline");
const strikethrough = document.getElementById("strikethrough");
const fsSelector = document.querySelector("#fs-selector")

const textLeft = document.getElementById("text-left");
const textCenter = document.getElementById("text-center");
const textRight = document.getElementById("text-right");
const textJustify = document.getElementById("text-justify");

const ul = document.getElementById("ul");
const ol = document.getElementById("ol");

const linkDropdown = document.getElementById("link-dropdown");
const link = document.getElementById("add-link");
const unlink = document.getElementById("remove-link");
const picture = document.getElementById("add-picture");
const video = document.getElementById("add-video");

const postArea = document.getElementById("post-area-div");
const hiddenPostArea = document.getElementById("post-area");
const characterCounter = document.getElementById("character-counter");

// Event Listeners
document.addEventListener("DOMContentLoaded", onGet);

undo.addEventListener("click", () => actionToPerform("undo"));
redo.addEventListener("click", () => actionToPerform("redo"));
bold.addEventListener("click", () => actionToPerform("bold"));
italic.addEventListener("click", () => actionToPerform("italic"));
underline.addEventListener("click", () => actionToPerform("underline"));
strikethrough.addEventListener("click", () => actionToPerform("strikeThrough"));
fsSelector.addEventListener("change", fontSelection);

textLeft.addEventListener("click", () => actionToPerform("justifyLeft"));
textCenter.addEventListener("click", () => actionToPerform("justifyCenter"));
textRight.addEventListener("click", () => actionToPerform("justifyRight"));
textJustify.addEventListener("click", () => actionToPerform("justifyFull"));

ul.addEventListener("click", () => actionToPerform("insertUnorderedList"));
ol.addEventListener("click", () => actionToPerform("insertOrderedList"));

link.addEventListener("click", addUrl);
unlink.addEventListener("click", () => actionToPerform("unLink"));
picture.addEventListener("click", pictureAction);
video.addEventListener("click", videoAction);

postArea.addEventListener("input", countCharacters);

// Functions

//Main action function
function actionToPerform(cmd, value = null) {
    console.log(cmd + " " + value);
    if(value) {
        document.execCommand(cmd, false, value);
    }
    else {
        document.execCommand(cmd);
    }
    
}

// Select font size
function fontSelection() {
    let selectedValue = fsSelector.value;
    actionToPerform("fontSize", value = selectedValue);
}

// Add URL
function addUrl() {
    let url = prompt("Enter URL");

    while(url == "") {
        url = prompt("Enter URL");
    }

    if(url !== "" && !url.includes("https://")) {
        url = "https://" + url;
    }

    actionToPerform("createLink", url);
}

function pictureAction () {

}

function videoAction() {

}

// Count characters entered in Div
function countCharacters() {
    let count = postArea.textContent.length;
    characterCounter.innerText = count;
    
    if(count >= 450) {
        characterCounter.style.color = "red";
        characterCounter.append(" (Limit Reached)");
        if(count > 450) {
            button().setAttribute("disabled", "true");
        } else {
            button().removeAttribute("disabled");
        }
    }
    else if(count >= 430) {
        characterCounter.style.color = "";
        button().removeAttribute("disabled");
    }
    else if(count >= 350 ) {
        characterCounter.style.color = "";
        button().removeAttribute("disabled");
    }
    else {
        characterCounter.style.color = "";
        button().removeAttribute("disabled");
    }
}

// Assign hidden post area's value to post area on page load
function onGet() {
    postArea.innerHTML = hiddenPostArea.innerText;
}

// Assign post area content to hidden post area to be sent to the server side
function onPost() {
    hiddenPostArea.innerText = postArea.innerHTML;
}