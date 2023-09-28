const uploadArea = document.getElementById("upload-area");
const errorArea = document.getElementById("error-area");
const saveButton = document.getElementById("save-button");
const imagePreview = document.getElementById("image-preview");
let cropper;

// Create and load cropper div
document.addEventListener("DOMContentLoaded", loadCropper);
function loadCropper() {
    cropper = new Cropper(imagePreview, {
        aspectRatio: 1,
        zoomOnWheel: false,
        viewMode: 2,
    });
}

// Validate file to be only of png, jpg or jpeg format
uploadArea.addEventListener("change", () => {
    let fileName = uploadArea.value;
    if(!(fileName.includes(".jpg") || fileName.includes(".jpeg") || fileName.includes(".png"))) {
        errorArea.textContent = "Unsupported file detected! Supported formats: jpg, jpeg and png"
        saveButton.setAttribute("disabled", true);
    }
    else {
        errorArea.textContent = "";
        getImageData();
        saveButton.removeAttribute("disabled");
    }
});

function getImageData() {
    const files = uploadArea.files;
    const imageFilesLength = files.length;

    if(imageFilesLength > 0) {
        const imageSrc = URL.createObjectURL(files[0]);
        imagePreview.removeAttribute("src");
        
        cropper.destroy();
        imagePreview.src = imageSrc;
        imagePreview.style.display = "block";
        loadCropper();
    }
}