//----------Post IDs are pushed to postIds array inside Index, Explore, Replies & UserProfile pages----------//

const likeButtons = document.querySelectorAll("#like-button");
const likeDisplays = document.querySelectorAll("#like-display");

if(likeButtons) {
    for(let i = 0; i < likeButtons.length; i++) {
        likeButtons[i].style.cursor = "Pointer";

        likeButtons[i].addEventListener("click", function() {
            let index = i;
            postLikesAction(index, likeButtons[i], likeDisplays[i]);
        });
    }
}

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