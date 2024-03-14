const movieContainer = document.querySelector('.movie-container');
const movieSlider = document.querySelector('.movie-slider');
const getMovies = async () => {
  const response = await fetch('https://localhost:7290/admin/getmovies');
  const movies = await response.json();
    console.log(movies);
  createMovies(movies);
};

getMovies();
let currentMovieID = null;
const createMovies =(movies) => {
    movies.forEach((movie) => {
        if (currentMovieID === null) {
            currentMovieID = movie.id;
        }
        let movieItem = document.createElement('div');
        movieItem.classList.add('movie-item');

        movieItem.style.backgroundImage = `url(${movie.image})`;
        movieItem.addEventListener('click', () => {
            currentMovieID = movie.id;
            console.log('movieID : ', currentMovieID);
        });
        movieSlider.appendChild(movieItem);
    });
};


let maxItemsShow = 4;
const leftArrow = document.querySelector('#back-movie');
const rightArrow = document.querySelector('#next-movie');

let currentOffset = 0;
let dates = document.querySelectorAll('.movie-item');
leftArrow.addEventListener('click', () => {
    dates = document.querySelectorAll('.movie-item');
    if (currentOffset > 0) {
        currentOffset -= 1;
        const multiplier = dates[0].offsetWidth;
        movieSlider.style.transform = `translateX(${-currentOffset * multiplier}px)`;
        // console.log(currentOffset, multiplier, currentOffset * multiplier);
    }
});

rightArrow.addEventListener('click', () => {
    dates = document.querySelectorAll('.movie-item');
    if (currentOffset < dates.length - maxItemsShow) {
        currentOffset += 1;
        const multiplier = dates[0].offsetWidth;
        movieSlider.style.transform = `translateX(${-currentOffset * multiplier}px)`;
        // console.log(currentOffset, multiplier, currentOffset * multiplier);
    }
});


let currentDateSelected = null;
const dateContainer = document.querySelector('.date-container');
const createDateElement = (date) => {
    const dateElement = document.createElement('div');
    dateElement.classList.add('date');
    const dayOfWeek = document.createElement('span');
    dayOfWeek.textContent = date.dayOfWeek;
    const dateChild = document.createElement('span');
    dateChild.textContent = date.date;
    dateElement.appendChild(dayOfWeek);
    dateElement.appendChild(dateChild);
    if (currentDateSelected === null) {
        dateElement.classList.add('active');
        currentDateSelected = dateElement;
    }
    dateContainer.appendChild(dateElement);
};

const initialDate = () => {
    // generate date for the next 21 days
    const today = new Date();
    for (let i = 0; i < 21; i++) {
        const date = new Date(today);
        date.setDate(date.getDate() + i);
        const options = { weekday: 'short', year: 'numeric', month: 'long', day: 'numeric' };
        const formattedDate = date.toLocaleDateString('en-GB', options);
        const dateParts = formattedDate.split(' ');
        let tmpDayOfWeek = dateParts[0].slice(0, 3);
        let tmpDate = `${dateParts[1]} ${dateParts[2]} ${dateParts[3]}`;
        createDateElement({ date: tmpDate, dayOfWeek: tmpDayOfWeek });
    }
};

initialDate();


let dateItems = document.querySelectorAll('.date');


const sliderContainer = document.querySelector('.date-container');
const observer = new ResizeObserver(entries => {
    for (const entry of entries) {
        const width = entry.contentRect.width;
        if (width > 870) {
            dateContainer.classList.add('large');
            dateContainer.classList.remove('small');
            dateContainer.classList.remove('medium');
            dateContainer.classList.remove('xsmall');
            dateContainer.classList.remove('xxsmall');
            maxItemsShow = 5;
        } else if (width > 650) {
            dateContainer.classList.add('medium');
            dateContainer.classList.remove('small');
            dateContainer.classList.remove('large');
            dateContainer.classList.remove('xsmall');
            dateContainer.classList.remove('xxsmall');
            maxItemsShow = 4;

        } else if (width > 360) {
            dateContainer.classList.add('small');
            dateContainer.classList.remove('medium');
            dateContainer.classList.remove('large');
            dateContainer.classList.remove('xsmall');
            dateContainer.classList.remove('xxsmall');
            maxItemsShow = 3;
        }
        else if (width > 200) {
            dateContainer.classList.add('xsmall');
            dateContainer.classList.remove('medium');
            dateContainer.classList.remove('large');
            dateContainer.classList.remove('small');
            dateContainer.classList.remove('xxsmall');
            maxItemsShow = 2;
        }
        else {
            dateContainer.classList.add('xxsmall');
            dateContainer.classList.remove('medium');
            dateContainer.classList.remove('large');
            dateContainer.classList.remove('small');
            dateContainer.classList.remove('xsmall');
            maxItemsShow = 1;
        }
    }
});

observer.observe(dateContainer);


const backDate = document.querySelector('#back-date');
const nextDate = document.querySelector('#next-date');

let currentOffsetDate = 0;
let maxItemsShowDate = 5;
let dates2 = document.querySelectorAll('.date');
backDate.addEventListener('click', () => {
    console.log('back');
    dates2 = document.querySelectorAll('.date');
    if (currentOffsetDate > 0) {
        currentOffsetDate -= 1;
        const multiplier = dates2[0].offsetWidth;
        sliderContainer.style.transform = `translateX(${-currentOffsetDate * multiplier}px)`;
    }
});

nextDate.addEventListener('click', () => {
    console.log('next');
    dates2 = document.querySelectorAll('.date');
    if (currentOffsetDate < dates2.length - maxItemsShowDate) {
        currentOffsetDate += 1;
        const multiplier = dates2[0].offsetWidth;
        sliderContainer.style.transform = `translateX(${-currentOffsetDate * multiplier}px)`;
    }
});

window.addEventListener('resize', () => {
    sliderContainer.style.transform = `translateX(0px)`;
});

let date = new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getDate() + 'T00:00:00Z';


// https://localhost:7290/Admin/getplaces?id=1&date=2024-03-12T00:00:00Z
const getPlaces = async (id, date) => {
    console.log(`https://localhost:7290/Admin/getplaces?id=${id}&date=${date}`)
    const response = await fetch(`https://localhost:7290/Admin/getplaces?id=${id}&date=${date}`);
    const data = await response.json();

    // console.log(data);
    if (data.length === 0) {
        console.log('no showtime');
        cinemaShowtime.innerHTML = '';
        let noShowtime = document.createElement('div');
        noShowtime.classList.add('no-showtime');
        noShowtime.innerHTML = `<h2 style="text-align: center;">No Showtime</h2>`;
        cinemaShowtime.appendChild(noShowtime);
        console.log(cinemaShowtime);
        return;
    }

    // console.log(data);

    data.forEach(place => {
        console.log(place.id, place.canton, place.county, place.showtimes[0]);
        createCinemaShowtime(`${place.county} ${place.canton}`, place.showtimes);
    });
};


getPlaces(currentMovieID, date);


// get today date as format 2024-03-12T00:00:00Z

dateContainer.addEventListener('click', (e) => {
    // child of date-container that is clicked
    console.log(e.target.closest('.date'));
    cinemaShowtime.innerHTML = '';
    currentDateSelected = e.target.closest('.date');
    if (e.target.closest('.date')) {
        const datesAll = document.querySelectorAll('.date');
        for (let i = 0; i < datesAll.length; i++) {
            datesAll[i].classList.remove('active');
        }
        e.target.closest('.date').classList.add('active');
        currentDateSelected = e.target.closest('.date');
        let currentDate = currentDateSelected.querySelector('span:nth-child(2)').textContent;
        // convert to this datetime format 2024-03-12T00:00:00Z as datetime type
        // format date to 2024-03-12T00:00:00Z
        date = new Date(currentDate).getFullYear() + '-' + (new Date(currentDate).getMonth() + 1) + '-' + new Date(currentDate).getDate() + 'T00:00:00Z';
        
        // passing movieID and Datetime here
        // getPlaces(id, date);
    }
});