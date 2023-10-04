const sharePostButtons = document.querySelectorAll("#share-post");
const shareReplyButtons = document.querySelectorAll("#share-reply");
const shareUserButtons = document.querySelectorAll("#share-user");
const shareReplyFields = document.querySelectorAll("#share-reply-field");
const shareReplyCopyButtons = document.querySelectorAll("#share-reply-copy-button");
const sharePostFields = document.querySelectorAll("#share-post-field");
const sharePostCopyButtons = document.querySelectorAll("#share-post-copy-button");

if(sharePostButtons) {
    for(let i = 0; i < sharePostButtons.length; i++) {
        sharePostButtons[i].style.cursor = "pointer";
        sharePostFields[i].value = `${window.location.href}`;
        if(!sharePostFields[i].value.includes("?PostId=")) {
            sharePostFields[i].value += `&PostId=${postIds[i]}`
        }

        sharePostCopyButtons[i].addEventListener("click", function() {
            sharePostFields[i].select();
            sharePostFields[i].setSelectionRange(0, 99999);
            navigator.clipboard.writeText(sharePostFields[i].value);
            console.log("copied!!");
        });
    }
}

if(shareReplyButtons) {
    for(let i = 0; i < shareReplyButtons.length; i++) {
        shareReplyButtons[i].style.cursor = "pointer";
        shareReplyFields[i].value = `${window.location.href}`;
        if(!shareReplyFields[i].value.includes("&ReplyId=")) {
            shareReplyFields[i].value += `&ReplyId=${replyIds[i]}`;
        }
        
        shareReplyCopyButtons[i].addEventListener("click", function() {
            shareReplyFields[i].select();
            shareReplyFields[i].setSelectionRange(0, 99999);
            navigator.clipboard.writeText(shareReplyFields[i].value);
            console.log("copied!!");
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