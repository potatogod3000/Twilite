//----------Eligible to follow usernames are pushed to userNamesFollow array inside the UserActions page----------//
//----------Following status is pushed to isFollowed array inside UserActions pages----------//

const follow = document.querySelectorAll("#follow");
// const replyFollow = document.querySelectorAll("#reply-follow");

// Assign event listener to each follow element to listen for a click event and perform the followAction function
for(let i = 0; i < follow.length; i++) {
    follow[i].addEventListener("click", function() {followAction(i)});
}

// Based on the state of follow data, perform wither Add or Remove follower action using fetch POST method
function followAction(index) {
    const params = `PostUserName=${userNamesFollow[index]}&CurrentUserName=${currentUserName}`;

    if(isFollowed[index]) {
        actionToPerform = "RemoveFollower";
    }
    else {
        actionToPerform = "AddFollower";
    }

    fetch(`/Post/${actionToPerform}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: params
    })

    .then(function(response) {
        if(response.status === 200) {
            
            if(actionToPerform === "AddFollower") {
                modifyState(userNamesFollow[index], isFollowed[index]);
                showToast(`You are now following ${userNamesFollow[index]}`, "normal");
            }
            else {
                modifyState(userNamesFollow[index], isFollowed[index]);
                showToast(`You have unfollowed ${userNamesFollow[index]}`, "normal");
            }
            
        }
        else {
            showToast(response.message, "warning");
        }
    })

    .catch(function(reject) {
        showToast(reject, "danger");
    });
}

// Change the appearance of followElement and its innerHTML
function changeFollowState(followElement, state) {
    let followInnerElement;

    if(state) {
        followInnerElement = `<i class="bi bi-person-dash-fill me-2"></i>Un-Follow`;
        followElement.classList.add("text-danger");
    }
    else {
        followInnerElement = '<i class="bi bi-person-plus me-2"></i>Follow';
        followElement.classList.remove("text-danger");
    }

    followElement.innerHTML = followInnerElement;
}

// Modify state of the Follow dropdown-item
function modifyState(userName, currentState) {
    for(let i = 0; i < userNamesFollow.length; i++) {
        if(userName === userNamesFollow[i]) {
            isFollowed[i] = !currentState;
            changeFollowState(follow[i], isFollowed[i]);
        }
    }
}