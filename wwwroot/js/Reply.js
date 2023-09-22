function ReplyPost(replyString, postId) {
    let xhr = new XMLHttpRequest();
    let params = "ReplyString="+replyString+"&PostId="+postId;
    
    xhr.open("POST", "/Post/Replies", true);
    xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    
    xhr.onload = function() {
        if(this.status ==200) {
            console.log(this.responseText);
        }
    }

    xhr.send(params);
}