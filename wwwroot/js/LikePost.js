var likeButtons = document.querySelectorAll("#like-button");

if(likeButtons != null) {
    likeButtons.forEach(likeButton => { 
        likeButton.style.cursor = "Pointer";
        likeButton.addEventListener("click", function() {
            LikeButton(likeButton);
            //document.onload = DisplayPostLikes();
        });
    });
}

//document.onload = DisplayPostLikes();

function LikeButton(likeButton) {
    if(likeButton.classList.contains("bi-heart")) {
        likeButton.classList.toggle("bi-heart");
        likeButton.classList.toggle("bi-heart-fill");
        likeButton.title = "Remove Like";
        //ToggleLikePost(true);
    }
    else if(likeButton.classList.contains("bi-heart-fill")) {
        likeButton.classList.toggle("bi-heart-fill");
        likeButton.classList.toggle("bi-heart");
        likeButton.title = "Like Post";
        //ToggleLikePost(false)
    }
}

/* function DisplayPostLikes(CurrentPost) {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "Post/PostLikes?CurrentPost="+CurrentPost, true);
    xhr.send();
    console.log(xhr.response);
    const LikeDisplays = document.querySelectorAll("#like-display");
    LikeDisplays.forEach(LikeDisplay => {
        console.log(CurrentPost);
        LikeDisplay.innerHTML = CurrentPost;
    });
}

function ToggleLikePost(Liked) {

} */