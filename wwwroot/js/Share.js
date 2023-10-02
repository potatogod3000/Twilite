const sharePostButtons = document.querySelectorAll("#share-post");
const shareReplyButtons = document.querySelectorAll("#share-reply");
const shareUserButtons = document.querySelectorAll("#share-user");

if(sharePostButtons) {
    for(let i = 0; i < sharePostButtons.length; i++) {
        sharePostButtons[i].style.cursor = "pointer";
        sharePostButtons[i].addEventListener("click", function() {

        });
    }
}

if(shareReplyButtons) {
    for(let i = 0; i < shareReplyButtons.length; i++) {
        shareReplyButtons[i].style.cursor = "pointer";
        
        shareReplyButtons[i].addEventListener("click", function() {
            console.log(`${window.location.href}&ReplyId=${replyIds[i]}`);
        });
    }
}

if(shareUserButtons) {
    for(let i = 0; i < shareUserButtons.length; i++) {
        shareUserButtons[i].style.cursor = "pointer";
        shareUserButtons[i].addEventListener("click", function() {

        });
    }
}