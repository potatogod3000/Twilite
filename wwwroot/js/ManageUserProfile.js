const uploadArea = document.getElementById("upload-area");
const errorArea = document.getElementById("error-area");
const saveButton = document.getElementById("save-button");
const imagePreview = document.getElementById("image-preview");

// Create cropper div
const cropper = new Cropper(imagePreview, {
    aspectRatio: 1/1,
    viewMode: 2,
    zoomOnWheel: false,
    restore: false,
    movable: false,
    zoomable: false,
    rotatable: false,
    scalable: false,
    background: false
});

// Validate file to be only of png, jpg or jpeg format
uploadArea.addEventListener("change", () => {
    let fileName = uploadArea.value;
    if(!(fileName.includes(".jpg") || fileName.includes(".jpeg") || fileName.includes(".png"))) {
        errorArea.textContent = "Unsupported file detected! Supported formats: jpg, jpeg and png"
        saveButton.setAttribute("disabled", true);
    }
    else {
        errorArea.textContent = "";
        getImageData(cropper);
        saveButton.removeAttribute("disabled");
    }
});

function getImageData(cropper) {
    const files = uploadArea.files;
    const imageFilesLength = files.length;

    if(imageFilesLength > 0) {
        const imageSrc = URL.createObjectURL(files[0]);
        imagePreview.removeAttribute("src");
        
        cropper.replace(imageSrc);
    }
}