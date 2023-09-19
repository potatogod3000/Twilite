let option = {
    animation: true,
    autohide: true,
    delay : 4000
};

let toastAreas = document.querySelectorAll("#toast-area");

addEventListener("DOMContentLoaded", function() {
    let toastElList = [].slice.call(toastAreas)
    let toastList = toastElList.map(function(toastEl) {
        return new bootstrap.Toast(toastEl, option)
    });
    toastList.forEach(toast => toast.show());
});