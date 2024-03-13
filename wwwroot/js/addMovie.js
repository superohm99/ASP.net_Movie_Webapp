const imageInput = document.querySelector('#imageUrl');
const previewImage = document.querySelector('.preview-image');


const endpoint = "https://api.imgbb.com/1/upload";
const apiKey = "62c18fb1790c4f52bb1a031c0332b453";
const fileInput = document.querySelector('input[type="file"]');
const form = document.querySelector('form');
async function postData(formData) {
    try {
        const response = await fetch(endpoint, {
            method: 'POST',
            body: formData
        });
        const result = await response.json();
        console.log(result.data.url); // Image URL
        imageInput.value = result.data.url;
        // document.body.style.backgroundImage = `url(${result.data.url})`;
        previewImage.style.backgroundImage = `url(${result.data.url})`;
    } catch (error) {
        console.error('Error:', error);
    }
}

fileInput.addEventListener('change', (e) => {
    e.preventDefault();
    const formData = new FormData();
    formData.append('key', apiKey);
    formData.append('image', fileInput.files[0]);
    formData.append('name', 'my-image');
    console.log(fileInput.files[0]);
    // formData.append('expiration', 600); // unit in second
    postData(formData);
});
