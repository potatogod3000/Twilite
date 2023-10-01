const alertPlaceholder = document.getElementById('alert-placeholder');
const replyLikeButtons = document.querySelectorAll("#reply-like-button");
const replyLikesCounts = document.querySelectorAll("#reply-likes-count");

for(let i = 0; i < replyLikeButtons.length; i++) {
    replyLikeButtons[i].style.cursor = "pointer";
    replyLikeButtons[i].addEventListener("click", () => likeReply(i, replyLikeButtons[i], replyLikesCounts[i]));
}

function ReplyPost(replyString, postId) {
    const params = "ReplyString="+replyString+"&PostId="+postId;

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
        if(response.status >= 400) {
            let err = new Error();
            if(response.status === 400) {
                err.message = "Your reply is empty! A reply should contain atleast one character";
            }
            else if(response.status === 500) {
                err.message = "Internal server error :(";
            }
            err.response = response;
            err.status = response.status;
            throw err;
        }
        // Handle successful response
        else {
            appendAlert("Reply posted!", "success");
            window.location.reload();
        }
    })

    // Handle network/auth errors
    .catch(function(reject) {
        appendAlert(reject.message, "danger");
    })
}

// Create a new wrapper div and add a bootstrap alert on-demand
function appendAlert(message, type) {
    const wrapper = document.createElement('div');

    wrapper.innerHTML = [
    `<div class="mt-3 mb-0 mx-3 alert alert-${type} alert-dismissible fade show" role="alert">`,
    `   <div class="text-center">${message}</div>`,
    `   <button type="button" class="btn-close" id="alert-close" data-bs-dismiss="alert" aria-label="Close"></button>`,
    `</div>`
    ].join('');

    alertPlaceholder.append(wrapper);
}

// Reply Likes
function likeReply(index, replyLikeButton, replyLikesCount) {
    params = "PostId=" + postId + "&ReplyId=" + replyIds[index];

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
        }
        else if(response.status === 406) {
            appendAlert("You cannot like your own reply", "info");
        }
        else {
            appendAlert("Internal Server error :(", "danger");
        }

        // Change title based on class name
        if(replyLikeButton.classList.contains("bi-heart-fill")) {
            replyLikeButton.title = "Remove Like"
            replyLikesCount.innerText++;
        }
        else {
            replyLikeButton.title = "Like Reply"
            replyLikesCount.innerText--;
        }
    })

    // Handle network errors
    .catch(function(reject) {
        console.log(reject);
        appendAlert(reject.message, "danger");
    });
}