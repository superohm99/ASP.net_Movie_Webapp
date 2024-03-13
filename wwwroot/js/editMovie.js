// get image url from api
const endpoint = "https://api.imgbb.com/1/upload";
const apiKey = "62c18fb1790c4f52bb1a031c0332b453";
const fileInput = document.querySelector('input[type="file"]');
const imagePreview = document.querySelector('.preview-image');
const movieImage = document.querySelector('.movie-image-text');
console.log(movieImage.value);
////////////////////////////
const getImageUrl = async (file) => {
    const formData = new FormData();
    formData.append('image', file);
    formData.append('key', apiKey);
    const response = await fetch(endpoint, {
        method: 'POST',
        body: formData
    });
    const data = await response.json();
    movieImage.value = data.data.url;
    imagePreview.style.backgroundImage = `url(${data.data.url})`;
};

fileInput.addEventListener('change', async (e) => {
    getImageUrl(e.target.files[0]);
});


const updateMovie = async (movieTitle, movieDescription, movieImage) => {
    try {
        const response = await fetch(`https://localhost:7290/Admin/UpdateMovie/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                title: movieTitle,
                description: movieDescription,
                image: movieImage
            })
        });
        const data = await response.json();
        console.log(data);
    }catch(error){
        console.error(error);
    };

};

/////////////////////////////
const cinemaShowtime = document.querySelector('.cinema-showtime');
// format date to 2024-03-12T00:00:00Z
// get only yyyy-mm-dd
let date = new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getDate() + 'T00:00:00Z';
// add 0 in front of single digit
let currentDateSelected = null;
// console.log(currentDateSelected);
console.log(date);
const dateContainer = document.querySelector('.date-container');



// Initial state /////////////////////////////////////////////////////////////////////////////////////////
const showtimes = [];

// showtimeList
const getAllDates = async (id) => {
    const response = await fetch(`https://localhost:7290/Admin/getDates/${id}`);
    const data = await response.json();
    data.forEach(date => {
        const dateObj = new Date(date);
        // dateformat to dayofweek, day month year
        const options = { weekday: 'short', year: 'numeric', month: 'long', day: 'numeric' };
        const formattedDate = dateObj.toLocaleDateString('en-GB', options);
        // split as dayofweek, day month year
        const dateParts = formattedDate.split(' ');
        let tmpDayOfWeek = dateParts[0].slice(0, 3);
        let tmpDate = `${dateParts[1]} ${dateParts[2]} ${dateParts[3]}`;

        let time = dateObj.toLocaleTimeString('en-GB');
        // timeformat hr:min
        time = time.split(':').slice(0, 2).join(':');
        // console.log({ date: tmpDate, dayOfWeek: tmpDayOfWeek });

        let showtime = { date: tmpDate, dayOfWeek: tmpDayOfWeek, showtimes: [time] };
        showtimes.push(showtime);
        // console.log(showtimes);

        createDateElement({ date: tmpDate, dayOfWeek: tmpDayOfWeek });
    });
}


// get last part of url
const url = window.location.href;
let id = url.split('/').pop();
// getAllDates(id);

////////////////////////////////////////////////////////////////////////////////////////


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


getPlaces(id, date);

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
        getPlaces(id, date);
    }
});

// fetch delete showtime
// const deleteShowtime = async (id) => {
//     const response = await fetch(`https://localhost:7290/Admin/DeleteShowtime/${id}`,
//         {
//             method: 'DELETE'
//         });
//     const data = await response.json();
//     console.log(data);
// };

// // filter placeID and date and movieID to get showtimeID and delete

// const getProgramID = async (movieID, placeID, date) => {
//     const response = await fetch(`https://localhost:7290/Admin/GetProgramID?movieID=${movieID}&placeID=${placeID}&date=${date}`);
//     const data = await response.json();
//     console.log(data);

//     try {
//         deleteShowtime(data[0].id);
//     } catch (error) {
//         console.error(error);
//     };
// };

// const getPlaceID = async (canton, county, datetime) => {
//     console.log(`https://localhost:7290/Admin/GetPlaceID?movieid=${id}&canton=${canton}&county=${county}`);
//     const response = await fetch(`https://localhost:7290/Admin/GetPlaceID?movieid=${id}&canton=${canton}&county=${county}`);
//     const data = await response.json();
//     let newDatetime = currentDateSelected.querySelector('span:nth-child(2)').textContent;
//     newDatetime = new Date(newDatetime).getFullYear() + '-' + (new Date(newDatetime).getMonth() + 1) + '-' + new Date(newDatetime).getDate() + 'T' + datetime + ':00Z';
//     console.log(`movieID: ${id}, placeID: ${data[0].id}, date: ${newDatetime}`);
//     getProgramID(id, data[0].id, newDatetime);
// };

// Fetch delete showtime
const deleteShowtime = async (id) => {
    try {
        const response = await fetch(`https://localhost:7290/Admin/DeleteShowtime/${id}`, {
            method: 'DELETE'
        });
        const data = await response.json();
        console.log(data);
    } catch (error) {
        console.error(error);
    }
    finally {
        console.log('deleted');
        getPlaces(id, date);
    }
};

// Filter placeID and date and movieID to get showtimeID and delete
const getProgramID = async (movieID, placeID, date) => {
    try {
        const response = await fetch(`https://localhost:7290/Admin/GetProgramID?movieID=${movieID}&placeID=${placeID}&date=${date}`);
        const data = await response.json();
        console.log(data);

        if (data.length > 0) {
            await deleteShowtime(data[0].id);
        } else {
            console.log("No showtime found for the specified parameters.");
        }
    } catch (error) {
        console.error(error);
    }
};

const getPlaceID = async (id, canton, county, datetime) => {
    try {
        console.log(`https://localhost:7290/Admin/GetPlaceID?movieid=${id}&canton=${canton}&county=${county}`);
        let response = await fetch(`https://localhost:7290/Admin/GetPlaceID?movieid=${id}&canton=${canton}&county=${county}`);
        let data = await response.json();
        let newDatetime = currentDateSelected.querySelector('span:nth-child(2)').textContent;
        newDatetime = new Date(newDatetime).getFullYear() + '-' + (new Date(newDatetime).getMonth() + 1) + '-' + new Date(newDatetime).getDate() + 'T' + datetime + ':00Z';
        console.log(`movieID: ${id}, placeID: ${data[0].id}, date: ${newDatetime}`);
        await getProgramID(id, data[0].id, newDatetime);
    } catch (error) {
        console.error(error);
    }
};

// cinema showtimes
const createCinemaShowtime = (placeName, showtimes) => {
    let cinemaContainer = document.createElement('div');

    cinemaContainer.classList.add('cinema-container');
    let cinemaName = document.createElement('div');
    cinemaName.classList.add('cinema-name');
    let h2 = document.createElement('h2');
    h2.textContent = placeName;
    cinemaName.appendChild(h2);
    cinemaContainer.appendChild(cinemaName);


    let showtime = document.createElement('div');
    showtime.classList.add('showtime');

    let showtimeContainer = document.createElement('div');
    showtimeContainer.classList.add('showtime-container');

    showtimes.forEach(showtime => {
        let span = document.createElement('span');
        span.textContent = showtime;
        span.addEventListener('click', (e) => {
            let countyCanton = e.target.parentElement.parentElement.previousElementSibling;
            let tmpCounty = countyCanton.querySelector('h2').textContent.split(' ')[0];
            let tmpCanton = countyCanton.querySelector('h2').textContent.split(' ')[1];
            getPlaceID(id, tmpCanton, tmpCounty, showtime);
            console.log(tmpCounty, tmpCanton, showtime, e.target.textContent);
        });
        showtimeContainer.appendChild(span);
    });
    showtime.appendChild(showtimeContainer);
    cinemaContainer.appendChild(showtime);

    cinemaShowtime.appendChild(cinemaContainer);

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


////////////////////////////////////////////////////////////////////////////////////

const sliderContainer = document.querySelector('.date-container');
let maxItemsShow = 6;
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


const leftArrow = document.querySelector('i.fa-chevron-left');
const rightArrow = document.querySelector('i.fa-chevron-right');

let currentOffset = 0;
let currentItemWidth = 0;
let dateLength = document.querySelectorAll('.date').length;
let dates = document.querySelectorAll('.date');
leftArrow.addEventListener('click', () => {
    dates = document.querySelectorAll('.date');
    if (currentOffset > 0) {
        currentOffset -= 1;
        const multiplier = dates[0].offsetWidth;
        sliderContainer.style.transform = `translateX(${-currentOffset * multiplier}px)`;
        // console.log(currentOffset, multiplier, currentOffset * multiplier);
    }
});

rightArrow.addEventListener('click', () => {
    dates = document.querySelectorAll('.date');
    if (currentOffset < dates.length - maxItemsShow) {
        currentOffset += 1;
        const multiplier = dates[0].offsetWidth;
        sliderContainer.style.transform = `translateX(${-currentOffset * multiplier}px)`;
        // console.log(currentOffset, multiplier, currentOffset * multiplier);
    }
});

window.addEventListener('resize', () => {
    sliderContainer.style.transform = `translateX(0px)`;
});