var option = {
    animation: true,
    autohide: true,
    delay : 4000
};

var toastAreas = document.querySelectorAll("#toast-area");

addEventListener("DOMContentLoaded", function() {
    var toastElList = [].slice.call(toastAreas)
    var toastList = toastElList.map(function(toastEl) {
        return new bootstrap.Toast(toastEl, option)
    });
    toastList.forEach(toast => toast.show());
});