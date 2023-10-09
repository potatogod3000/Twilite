//----------Reply IDs are pushed to replyIds array inside Replies & UserProfile pages----------//
//----------Post IDs for replies are pushed to replyPostIds array inside Replies & UserProfile pages----------//

const replyLikeButtons = document.querySelectorAll("#reply-like-button");
const replyLikesCounts = document.querySelectorAll("#reply-likes-count");
const loadAllReplies = document.getElementById("load-all-replies");

for(let i = 0; i < replyLikeButtons.length; i++) {
    replyLikeButtons[i].style.cursor = "pointer";
    replyLikeButtons[i].addEventListener("click", () => likeReply(i, replyLikeButtons[i], replyLikesCounts[i]));
}

// Reply function
function ReplyPost(replyString, postId) {
    const params = `ReplyString=${replyString}&PostId=${postId}`;

    // Post replies to backend Controller
    fetch("/Post/Replies", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: params
    })

    // Handle responses
    .then(function(response) {
        
        // Handle error response
        if(response.status === 400) {
            showToast("Your reply is empty! A reply should contain atleast one character", "info");
        }
        else if(response.status === 500) {
            showToast("Internal server error :(", "warning");
        }
        
        // Handle successful response
        else {
            showToast("Reply posted!", "success");
            window.location.reload();
        }
    })

    // Handle network/auth errors
    .catch(function(reject) {
        showToast(reject, "danger");
    })
}

// Reply Likes
function likeReply(index, replyLikeButton, replyLikesCount) {
    params = `PostId=${replyPostIds[index]}&ReplyId=${replyIds[index]}`;

    // Reply likes manipulation
    fetch("/Post/ReplyLikes", {
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: params,
        method: "POST"
    })

    // Handle all responses
    .then(function(response) {
        if(response.status === 200) {
            replyLikeButton.classList.toggle("bi-heart-fill");
            replyLikeButton.classList.toggle("bi-heart");

            // Change title based on class name
            if(replyLikeButton.classList.contains("bi-heart-fill")) {
                replyLikeButton.title = "Remove Like"
                replyLikesCount.innerText++;
            }
            else {
                replyLikeButton.title = "Like Reply"
                replyLikesCount.innerText--;
            }
            
        }
        else if(response.status === 406) {
            showToast("You cannot like your own reply", "info");
        }
        else {
            showToast("Internal Server error :(", "danger");
        }
    })

    // Handle network errors
    .catch(function(reject) {
        console.log(reject);
        appendAlert(reject.message, "danger");
    });
}