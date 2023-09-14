var likeButtons = document.querySelectorAll("#like-button");
var likeDisplays = document.querySelectorAll("#like-display");

if(likeButtons != null) {
    for(let i = 0; i < likeButtons.length; i++) {
        likeButtons[i].style.cursor = "Pointer";

        likeButtons[i].addEventListener("click", function() {
            var index = i;
            likeButtonAction(likeButtons[i], likeDisplays[i]);
            postLikesAction(index);
        });
    }
}

function likeButtonAction(likeButton, likeDisplay) {
    var liked;

    // Action performed when Liked    
    if(likeButton.classList.contains("bi-heart")) {
        likeButton.classList.toggle("bi-heart");
        likeButton.classList.toggle("bi-heart-fill");
        likeButton.title = "Remove Like";
        liked = true;
    }

    // Action performed when Removing Like
    else if(likeButton.classList.contains("bi-heart-fill")) {
        likeButton.classList.toggle("bi-heart-fill");
        likeButton.classList.toggle("bi-heart");
        likeButton.title = "Like Post";
        liked = false;
    }

    // Toggle the Likes number
    toggleLikeNumbers(liked, likeDisplay);
}

function toggleLikeNumbers(liked, likeDisplay) {
    if(liked) {
        likeDisplay.innerText++;
    }

    else if(!liked && likeDisplay.innerText !== 0) {
        likeDisplay.innerText--;
    }
}

function postLikesAction(index) {
    for(let i = 0; i < postIdArr.length; i++) {
        if(postIdArr[i] === postIdArr[index]) {
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "/Post/LikePost?PostId="+postIdArr[i], true);
            xhr.send();
        }
    }
}