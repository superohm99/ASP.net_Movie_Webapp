/*

https://api.imgbb.com/1/upload

key (required)
The API key.
image (required)
A binary file, base64 data, or a URL for an image. (up to 32 MB)
name (optional)
The name of the file, this is automatically detected if uploading a file with a POST and multipart / form-data
expiration (optional)
Enable this if you want to force uploads to be auto deleted after certain time (in seconds 60-15552000)

{
  "data": {
    "id": "2ndCYJK",
    "title": "c1f64245afb2",
    "url_viewer": "https://ibb.co/2ndCYJK",
    "url": "https://i.ibb.co/w04Prt6/c1f64245afb2.gif",
    "display_url": "https://i.ibb.co/98W13PY/c1f64245afb2.gif",
    "width":"1",
    "height":"1",
    "size": "42",
    "time": "1552042565",
    "expiration":"0",
    "image": {
      "filename": "c1f64245afb2.gif",
      "name": "c1f64245afb2",
      "mime": "image/gif",
      "extension": "gif",
      "url": "https://i.ibb.co/w04Prt6/c1f64245afb2.gif",
    },
    "thumb": {
      "filename": "c1f64245afb2.gif",
      "name": "c1f64245afb2",
      "mime": "image/gif",
      "extension": "gif",
      "url": "https://i.ibb.co/2ndCYJK/c1f64245afb2.gif",
    },
    "medium": {
      "filename": "c1f64245afb2.gif",
      "name": "c1f64245afb2",
      "mime": "image/gif",
      "extension": "gif",
      "url": "https://i.ibb.co/98W13PY/c1f64245afb2.gif",
    },
    "delete_url": "https://ibb.co/2ndCYJK/670a7e48ddcb85ac340c717a41047e5c"
  },
  "success": true,
  "status": 200
}

*/

const endpoint = "https://api.imgbb.com/1/upload";
const apiKey = "62c18fb1790c4f52bb1a031c0332b453";
const fileInput = document.querySelector('input[type="file"]');
const form = document.querySelector('form');
const avatar = document.querySelector('.profile-image img');
async function postData(formData) {
    try {
        const response = await fetch(endpoint, {
            method: 'POST',
            body: formData
        });
        const result = await response.json();
        console.log(result.data.url); // Image URL
        avatar.src = result.data.url;
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
    // formData.append('expiration', 600);
    postData(formData);
});

const cancelButton = document.querySelector('.submit-btn .btn-danger');
const updateButton = document.querySelector('.submit-btn .btn-success');

cancelButton.addEventListener('click', () => {
    form.dataset.submitButton = 'cancel';
});

updateButton.addEventListener('click', () => {
    form.dataset.submitButton = 'update';
});

form.addEventListener('submit', (e) => {
    e.preventDefault();
    if (form.dataset.submitButton === 'update') {
        alert('Profile updated successfully');
        // do something with updated data
    }
    location.href = '/profile';
});