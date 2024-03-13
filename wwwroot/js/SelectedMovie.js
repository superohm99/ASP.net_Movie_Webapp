var url = window.location.href;

// Split the URL by '/'
var urlParts = url.split('/');

// Get the last part of the URL which contains the "2"
var movieId = urlParts[urlParts.length - 1];

const getMovieByID = async (id) => {
    let response = await fetch("https://localhost:7290/Chat/getMovieByID/"+id);
    let data = await response.json();
    console.log(data);
};
getMovieByID(movieId);
console.log(movieId);
