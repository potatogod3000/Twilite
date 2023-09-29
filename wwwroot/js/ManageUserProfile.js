const uploadArea = document.getElementById("upload-area");
const errorArea = document.getElementById("error-area");
const saveButton = document.getElementById("save-button");
const imagePreview = document.getElementById("image-preview");
const hiddenCroppedImage = document.getElementById("base64-image-area");

// Create cropper
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

saveButton.addEventListener("click", sendCroppedData);

// Replace with new image when a new image file is selected
function getImageData(cropper) {
    const files = uploadArea.files;
    const imageFilesLength = files.length;

    if(imageFilesLength > 0) {
        const imageSrc = URL.createObjectURL(files[0]);
        imagePreview.removeAttribute("src");
        
        cropper.replace(imageSrc);
    }
}

// Send cropped data to backend on Save Button click
function sendCroppedData() {
    const croppedArea = cropper.getCroppedCanvas();
    const roundedCroppedData = getRoundedCanvas(croppedArea).toDataURL("image/png");
    hiddenCroppedImage.value = roundedCroppedData;

    /* const formData = new FormData();
    formData.append("profilePicture", roundedCroppedArea);
    const token = document.querySelector('input[name="__RequestVerificationToken"]').getAttribute("value");

    fetch('/Identity/Account/Manage', {
        formData,
        headers: {
            "RequestVerificationToken": token,
            "Content-Type": "multipart/form-data"
        }
    }); */
}

// Get rounded canvas for the given rectangular/square one
function getRoundedCanvas(sourceCanvas) {
    var canvas = document.createElement('canvas');
    var context = canvas.getContext('2d');
    var width = sourceCanvas.width;
    var height = sourceCanvas.height;

    canvas.width = width;
    canvas.height = height;
    context.imageSmoothingEnabled = true;
    context.drawImage(sourceCanvas, 0, 0, width, height);
    context.globalCompositeOperation = 'destination-in';
    context.beginPath();
    context.arc(width / 2, height / 2, Math.min(width, height) / 2, 0, 2 * Math.PI, true);
    context.fill();
    return canvas;
}