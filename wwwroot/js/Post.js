//----------Post IDs are pushed to postIds array inside Index, Explore, Replies & UserProfile pages----------//

const likeButtons = document.querySelectorAll("#like-button");
const likeDisplays = document.querySelectorAll("#like-display");
const createPostButtons = document.querySelectorAll("#");
const editPostButtons = document.querySelectorAll("#");
const deletePostButtons = document.querySelectorAll("#");

// Assign event listeners to all the buttons (if the page contains Like buttons)
if(likeButtons) {
    for(let i = 0; i < likeButtons.length; i++) {
        likeButtons[i].style.cursor = "Pointer";

        likeButtons[i].addEventListener("click", function() {
            let index = i;
            postLikesAction(index, likeButtons[i], likeDisplays[i]);
        });
    }
}

// Perform Like/Unlike action
function postLikesAction(index, likeButton, likeDisplay) {
    const param = "PostId="+postIds[index];
    
    fetch("/Post/LikePost", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: param
    })
    
    .then(function(response) {

        if (response.status === 200) {
            likeButton.classList.toggle("bi-heart");
            likeButton.classList.toggle("bi-heart-fill");

            if(likeButton.classList.contains("bi-heart-fill")) {
                likeButton.title = "Remove Like";
                likeDisplay.innerText++;
            }
            else {
                likeButton.title = "Like Post";
                likeDisplay.innerText--;
            }
        }
        else if(response.status === 406) {
            showToast("You cannot like your own post", "info");
        }
        else {
            showToast("Internal Server error :(", "warning");
        }

    })

    .catch(function(reject) {
        showToast(reject, "danger");
    });
}

// Create Post
function createPost(currentUserName, postContent) {
    const postModel = {
        UserName: currentUserName,
        PostContent: postContent
    };
    const params = `Post=${postModel}`;

    fetch("/Post/CreatePost", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: params
    })

    .then(function(response) {
        if(response.ok) {
            showToast("Your post has been posted successfully!", "normal");
            window.location.reload();
        }
        else {
            return response.text();
        }
    })

    .then(function(text) {
        console.log(text);
    })

    .catch(function(reject) {

    });
}

// Edit Post
function editPost(postId, postContent) {
    const params = `PostId=${postId}&PostContent=${postContent}`;

    fetch("/Post/EditPost", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: params
    })

    .then(function(response) {

    })

    .catch(function(reject) {

    });
}

// Delete Post
function deletePost(postId) {
    const params = `PostId=${postId}`;

    fetch("/Post/DeletePost", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: params
    })

    .then(function(response) {

    })

    .catch(function(reject) {

    });
}