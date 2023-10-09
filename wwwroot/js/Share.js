//----------All post usernames are pushed to userNamesPost array inside the ShowPosts.cshtml page----------//

const sharePostButtons = document.querySelectorAll("#share-post");
const shareReplyButtons = document.querySelectorAll("#share-reply");
const shareUserButtons = document.querySelectorAll("#share-user");
const shareReplyFields = document.querySelectorAll("#share-reply-field");
const shareReplyCopyButtons = document.querySelectorAll("#share-reply-copy-button");
const sharePostFields = document.querySelectorAll("#share-post-field");
const sharePostCopyButtons = document.querySelectorAll("#share-post-copy-button");
const shareReplyDropdownContent = document.querySelectorAll("#share-reply-dropdown div");
const sharePostDropdownContent = document.querySelectorAll("#share-post-dropdown div");


// Stop propagation of dropdown menu and prevent closing dropdown after clicking on <div> element
shareReplyDropdownContent.forEach(function(dropdownContent) {
    dropdownContent.addEventListener("click", function(event) {
        event.stopPropagation();
    })
})

sharePostDropdownContent.forEach(function(dropdownContent) {
    dropdownContent.addEventListener("click", function(event) {
        event.stopPropagation();
    })
})

// If the buttons dont return null value, assign correct link to Posts and Replies
// Perform copy action when clicked on copy button
if(sharePostButtons) {
    for(let i = 0; i < sharePostButtons.length; i++) {
        sharePostButtons[i].style.cursor = "pointer";
        sharePostButtons[i].addEventListener("click", function() {
            sharePostFields[i].value = `${window.location.origin}/Post/Replies?PostId=${postIds[i]}`;
        });

        sharePostCopyButtons[i].addEventListener("click", function() {
            sharePostFields[i].select();
            sharePostFields[i].setSelectionRange(0, 99999);
            navigator.clipboard.writeText(sharePostFields[i].value);
            showToast("Link Copied", "normal");
        });
    }
}

if(shareReplyButtons) {
    for(let i = 0; i < shareReplyButtons.length; i++) {
        shareReplyButtons[i].style.cursor = "pointer";
        shareReplyButtons[i].addEventListener("click", function() {
            shareReplyFields[i].value = `${window.location.origin}/Post/Replies?PostId=${replyPostIds[i]}&ReplyId=${replyIds[i]}`;
        });
        
        shareReplyCopyButtons[i].addEventListener("click", function() {
            shareReplyFields[i].select();
            shareReplyFields[i].setSelectionRange(0, 99999);
            navigator.clipboard.writeText(shareReplyFields[i].value);
            showToast("Link Copied", "normal");
        });
        
    }
}

if(shareUserButtons) {
    for(let i = 0; i < shareUserButtons.length; i++) {
        shareUserButtons[i].style.cursor = "pointer";
        shareUserButtons[i].addEventListener("click", function() {
            const link = `${window.location.origin}/Profile/UserProfile?UserName=${userNamesPost[i]}`
            navigator.clipboard.writeText(link);
            showToast("Link Copied", "normal");
        });
    }
}