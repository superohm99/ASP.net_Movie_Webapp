// const mainContainer = document.querySelector('.main-container');

// // get movies list /////////////////////////////////
// const getMovies = async () => {
//     let response = await fetch("https://localhost:7290/Chat/getMovies");
//     let data = await response.json();
//     // console.log(data);
//     generateMovieItem(data);
// };




// const generateMovieItem = (data) => {
//     // console.log(data);

//     data.forEach((movieItem) => {
//         console.log(movieItem);
//         const movieItemCon = document.createElement('a');
//         movieItemCon.classList.add('movie-item');
//         const movieImage = document.createElement('img');
//         movieImage.src = movieItem.image;
//         movieItemCon.appendChild(movieImage);
//         mainContainer.appendChild(movieItemCon);
//         movieItemCon.setAttribute('asp-action', 'SelectedMovie');
//         movieItemCon.addEventListener('click', () => {
//             // console.log(movieItem.id);
//             window.location.href = 'SelectedMovie/'+movieItem.id;
//         });
//     });
// }

// getMovies(); 

const mainContainer = document.querySelector('.slides');
const backBtn = document.getElementById('backBtn');
const nextBtn = document.getElementById('nextBtn');

let slideIndex = 0;
const imagesPerSlide = 5; // Number of images to show per slide

const createImage = (src, id) => {
    const img = document.createElement('img');
    img.src = src;
    img.setAttribute('data-id', id); // Set data-id attribute with the movie ID

    img.addEventListener('click', (event) => {
        event.preventDefault(); // Prevent the default behavior of the click event

        const movieId = img.getAttribute('data-id'); // Get the movie ID
        document.getElementById('movieSelect').value = movieId; // Update the input field with the movie ID
        console.log('Clicked image Id:', movieId);

        $('#movieSelect').trigger('change'); // Trigger the change event on #movieSelect
    });

    return img;
}

const getMovies = async () => {
    const response = await fetch('https://localhost:7290/admin/getmovies');
    const data = await response.json();
    data.forEach(movie => {
        const img = createImage(movie.image, movie.id);
        mainContainer.appendChild(img);
    });

    showSlide(); // Show the initial slide
}

getMovies();

const showSlide = () => {
    const startIndex = slideIndex * imagesPerSlide;
    const endIndex = Math.min(startIndex + imagesPerSlide, mainContainer.children.length);
    
    for (let i = 0; i < mainContainer.children.length; i++) {
        if (i >= startIndex && i < endIndex) {
            mainContainer.children[i].style.display = 'inline-block';
        } else {
            mainContainer.children[i].style.display = 'none';
        }
    }
}

nextBtn.addEventListener('click', () => {
    slideIndex++;
    if (slideIndex >= Math.ceil(mainContainer.children.length / imagesPerSlide)) {
        slideIndex = 0;
    }
    showSlide();
});

backBtn.addEventListener('click', () => {
    slideIndex--;
    if (slideIndex < 0) {
        slideIndex = Math.ceil(mainContainer.children.length / imagesPerSlide) - 1;
    }
    showSlide();
});

////////////////////////////////////////////