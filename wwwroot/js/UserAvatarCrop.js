const image = document.getElementById('image');
const cropper = new Cropper(image, {
    aspectRatio: 1,
    viewMode: 2,
});