//----------Post IDs are pushed to postIds array inside Index, Explore, Replies & UserProfile pages----------//
//----------Current user's Post IDs are pushed to currentUserPostIds array inside UserActions pages----------//

const likeButtons = document.querySelectorAll("#like-button");
const likeDisplays = document.querySelectorAll("#like-display");
const editPostButtons = document.querySelectorAll("#edit-post");
const deletePostButtons = document.querySelectorAll("#delete-post");

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

// Assign event listeners to all edit and delete buttons (if the page contains the buttons)
if(editPostButtons && deletePostButtons) {
    for(let i = 0; i < editPostButtons.length; i++) {
        editPostButtons[i].style.cursor = "Pointer";

        editPostButtons[i].addEventListener("click", function() {
            performEdit(currentUserPostIds[i]);
        });
    }

    for(let i = 0; i < deletePostButtons.length; i++) {
        deletePostButtons[i].style.cursor = "Pointer";

        deletePostButtons[i].addEventListener("click", function() {
            deletePost(currentUserPostIds[i]);
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

// Perform DOM manipulations to edit post
async function performEdit(currentPostId) {
    const parentDiv = document.querySelector(`#post-${currentPostId}`);
    const originalPostContent = document.querySelector(`#post-${currentPostId}-content`).innerHTML;
    const postScript = document.getElementById("post-script");
    const innerHtmlStore = parentDiv.innerHTML;
    
    parentDiv.innerHTML = `<div class="d-flex align-items-center justify-content-between mb-3 mx-2">
    <span>Edit Post</span>
    <span><a class="btn btn-sm btn-outline-danger border-0"><i class="bi bi-x-lg" title="Cancel Edit" id="cancel-edit-button"></i></a></span>
    </div>`;

    // Fetch _RichEditor Partial and insert it inside post's card div
    await fetch("/Post/GetRichEditorPartial", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        }
    })

    .then(function(response) {
        if(response.status === 200) {
            return response.text();
        }
        else {
            const err = new Error();
            err.message = "Error loading editor!";
            throw err;
        }
    })

    .then(function(text) {
        // Load RichEditor html page from response body by using DOMParser() to parse the html string
        const richEditor = new DOMParser().parseFromString(
            `${text}
            <button class="btn btn-primary float-end" type="button" id="edit-button">Edit Post</button>`
            , "text/html"
        );

        const richEditorBody = richEditor.querySelector("body");
        richEditorBody.style.width = "100%";
        parentDiv.appendChild(richEditor.firstChild);
        
        // Load the RichEditor.js script
        const script = document.createElement("script");
        script.src = `${window.location.origin}/js/RichEditor.js`;
        parentDiv.appendChild(script);
    })

    .catch(function(reject) {
        showToast(reject.message, "warning");
    });
    
    // Set postArea variable required by RichEditor.js file and copy original PostContent to postArea
    const postArea = document.getElementById("post-area-div");
    postArea.innerHTML = originalPostContent;

    // Action to perform on clicking Cancel Edit button
    const cancelEditButton = document.getElementById("cancel-edit-button");
    cancelEditButton.addEventListener("click", function() {
        parentDiv.innerHTML = innerHtmlStore;
        
        postScript.parentNode.removeChild(postScript);
        const newPostScript = document.createElement("script");
        newPostScript.src = `${window.location.origin}/js/Post.js`;
        parentDiv.append(newPostScript);
    });
}

// Create Post
function createPost(currentUserName, postContent) {
    const params = JSON.stringify({
        UserName: currentUserName,
        PostContent: postContent
    });

    fetch("/Post/CreatePost", {
        method: "POST",
        headers: {
            "Content-Type": "application/json; charset=utf-8"
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
        if(response.status === 200) {
            showToast("Your post has been deleted!", "info");
            window.location.reload();
        }
        else if(response.status === 400) {
            return response.text();
        }
        else {
            const err = new Error();
            err.message = "Server Error :(";
            throw err;
        }
    })

    .then(function(text) {
        showToast(text, "warning");
    })

    .catch(function(reject) {
        showToast(reject.message, "danger");
    });
}