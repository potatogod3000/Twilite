const option = {
    animation: true,
    autohide: true,
    delay : 4000
};

const toastAreas = document.querySelectorAll("#toast-area");
const toastArea = document.getElementById("js-toast-area");
const toastAreaBody = document.getElementById("js-toast-area-body");

addEventListener("DOMContentLoaded", function() {
    let toastElList = [].slice.call(toastAreas)
    let toastList = toastElList.map(function(toastEl) {
        return new bootstrap.Toast(toastEl, option);
    });
    toastList.forEach(toast => toast.show());
});

function showToast(message, status) {
    toastAreaBody.className = "toast-body";
    toastAreaBody.innerText = message;
    toastAreaBody.classList.add(`text-${status}`);
    
    const toast = new bootstrap.Toast(toastArea, option);
    toast.show();
}