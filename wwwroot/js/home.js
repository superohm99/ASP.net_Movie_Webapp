const isLogged = document.querySelector('input[type="hidden"]').value;

const cards = document.querySelector('.cards');

const getAllMovies = async () => {
    try {
        const response = await fetch('https://localhost:7290/admin/getmovies');
        const movies = await response.json();
        console.log(movies);
        movies.forEach(movie => {
            const card = document.createElement('section');
            card.classList.add('card');
            card.innerHTML = `
                <img src="${movie.image}" alt="${movie.title}" class="card-img">
                <h2>${movie.title}</h2>
                <button class="btn btn-transparent">Check</button>
            `;

            // @Model in asp.net core
            const heart = document.createElement('i');
            heart.classList.add('fas', 'fa-heart');
            heart.addEventListener('click', () => {
                heart.classList.toggle('active');
                // do something with movie like add to favorites
            });
            console.log(isLogged);
            if (isLogged !== 'value') {
                heart.style.display = 'none';
            }else{
                heart.style.display = 'block';
            }
            card.appendChild(heart);
            cards.appendChild(card);
        });
    } catch (err) {
        console.error('Error:', err);
    }
};

getAllMovies();