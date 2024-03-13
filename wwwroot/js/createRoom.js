// ------------------------------
// DECLARATION AND INITIALIZATION
// ------------------------------

//Movies instance for testing
const movies = [
    {
        picture: "",
        name: "Hum Noi",
        theaters: ["A", "B", "C"],
        showTime: ["10.00", "10.30", "11.00", "12.00"]
    },
    {
        picture: "",
        name: "Hum Yai",
        theaters: ["A", "C"],
        showTimes: ["10.00", "10.30"]
    },
    {
        picture: "",
        name: "Ultraman",
        theaters: ["A", "B", "C", "D"],
        showTimes: ["10.00", "10.30", "11.00", "12.00"]
    },
    {
        picture: "",
        name: "Jujutsu Kaisen",
        theaters: ["A", "B", "C"],
        showTimes: ["10.30", "11.00", "12.00"]
    },
    {
        picture: "",
        name: "Attack on Titan",
        theaters: ["A", "B", "C"],
        showTimes: ["10.00", "10.30", "11.00", "12.00", "13.00", "14.00"]
    },
    {
        picture: "",
        name: "Spy X Family",
        theaters: ["A", "B", "C", "F", "G"],
        showTimes: ["10.00", "10.30", "11.00", "12.00", "12.30"]
    },
    {
        picture: "",
        name: "Fullmetal Alchemist",
        theaters: ["AAA", "AB", "AAC", "D", "E", "F", "G", "H", "I", "J", "K"],
        showTimes: ["10.00", "10.30", "11.00", "12.00", "13.00", "13.30", "15.00", , "16.00", "16.30", "17.00", "17.20", "18.00"]
    },
    {
        picture: "",
        name: "Deathnote",
        theaters: ["A"],
        showTimes: ["10.00", "10.30"]
    },
    {
        picture: "",
        name: "Doraemon",
        theaters: ["A", "C", "D"],
        showTimes: ["10.00", "10.30", "12.00"]
    },
    {
        picture: "",
        name: "HEE YAI KHAI LEK HEY",
        theaters: ["A", "C", "D"],
        showTimes: ["10.00", "12.00"]
    },
    {
        picture: "",
        name: "AYOOOOOO ",
        theaters: ["A", "C", "D"],
        showTimes: ["10.00", "10.30", "12.00"]
    },
];

// WINDOW VARIABLE
let windowWidth = window.screen.width;

// SELECT MOVIE SECTION
let movieGridContainer = document.querySelector(".movie-grid-container");
addMovie();
let movieCards = document.querySelectorAll(".movie-card");
let movieGrid = document.querySelector(".movie-card-grid");
let movieContainerWidth = document.querySelector(".movie-grid-container").getBoundingClientRect().width;
let movieScrollLeftButton = document.querySelector(".movie-content .button-container:first-child");
let movieScrollRightButton = document.querySelector(".movie-content .button-container:last-child");

//SELECT DATE SECTION
let dateContainer = document.querySelector(".date-grid-container");
const datesName = ["SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT"];
const monthsName = ["JAN", "FEB", "MAR", "APR", "MAY", "JUNE", "JULY", "AUG", "SEP", "OCT", "NOV", "DEC"];
addDate();
const dateScrollLeftButton = document.getElementById("select-date-left-button");
const dateScrollRightButton = document.getElementById("select-date-right-button");
const dateCards = document.querySelectorAll(".date-card");

//SELECT SIZE SECTION
const upArrow = document.getElementById("up-arrow");
const downArrow = document.getElementById("down-arrow");
const sizeElm = document.getElementById("size");
const MAX_SIZE = 4;
const MIN_SIZE = 2;
let roomSize = 2;
sizeElm.innerHTML = MIN_SIZE;

//SELECT THEATER SECTION
const selectTheaterSection = document.querySelector(".select-theater-section");
const selectTheaterWrapper = document.querySelector("#select-theater-wrapper");
let searchTheaterInp = document.querySelector("#search-theater-input");
let theaterList = document.getElementById("theater-list");
let theaterName = document.getElementById("theater-name");
addTheater(6);

//SELECT SHOWTIME SECTION
const showTimeContainer = document.getElementById("showtime-card-container");
addShowTime(6);
const showTimeCards = document.querySelectorAll('.showtime-card');
document.querySelector('.showtime-card')?.classList.add('clicked');

// -------
// FUNCTION
// -------

// SELECT MOVIE SECTION
function addMovie() {
    let movieCardGrid = document.createElement("div");
    let N = 0;
    if (windowWidth > 768) {
        N = movies.length + (5 - movies.length % 5);
    } else {
        N = movies.length + (2 - movies.length % 2);
    }
    console.log(N);

    movieCardGrid.setAttribute("class", "movie-card-grid");

    for (let i = 0; i < N; i++) {
        let movieCard = document.createElement("div");
        let movieImageContainer = document.createElement("div");
        let movieImage = document.createElement("img");
        let movieName = document.createElement("p");

        if (i < movies.length) {
            movieImage.setAttribute("src", "https://www.alleycat.org/wp-content/uploads/2019/03/FELV-cat.jpg")
            movieImage.setAttribute("alt", "Loading Picture Failed")
            movieName.innerText = `${movies[i].name}`;
            if (windowWidth > 768) {
                if (i % 5 === 0) {
                    movieCard.style.transformOrigin = "left";
                } else if ((i + 1) % 5 === 0) {
                    movieCard.style.transformOrigin = "right";
                }
            } else {
                if (i % 2 === 0) {
                    movieCard.style.transformOrigin = "left";
                } else if ((i + 1) % 2 === 0) {
                    movieCard.style.transformOrigin = "right";
                }
            }
        }

        movieImageContainer.setAttribute("class", "img");
        movieImageContainer.appendChild(movieImage);
        movieCard.setAttribute("class", "movie-card");
        movieCard.setAttribute("id", `movie-card-${i}`);
        movieCard.appendChild(movieImageContainer);
        movieCard.appendChild(movieName);
        movieCardGrid.appendChild(movieCard);
    }
    movieGridContainer.appendChild(movieCardGrid);
}

// SELECT DATE SECTION 
function addDate() {
    let date = new Date();

    let newDateGrid = document.createElement("div");
    newDateGrid.setAttribute("class", "date-grid");
    for (let i = 0; i < 14; i++) {
        let newDateCard = document.createElement("div");
        let newDateElm = document.createElement("div");
        let newDayElm = document.createElement("div");
        let tempDate = new Date(date);
        tempDate.setDate(date.getDate() + i);

        newDateCard.setAttribute("class", "date-card");
        newDateElm.setAttribute("class", "date");
        newDayElm.setAttribute("class", "day");
        if (i === 0) {
            newDateElm.innerText = "TODAY";
            newDateCard.classList.add('clicked');
        } else {
            newDateElm.innerText = `${datesName[tempDate.getDay()]}`;
        }
        newDayElm.innerText = `${tempDate.getDate()} ${monthsName[tempDate.getMonth()]} ${tempDate.getFullYear() % 100}`;

        newDateCard.appendChild(newDateElm);
        newDateCard.appendChild(newDayElm);
        newDateGrid.appendChild(newDateCard);
    }
    dateContainer.appendChild(newDateGrid);
}

// SELECT THEATER SECTION
function addTheater(selectedTheater) {
    //Initialize movieID for testing
    let movieID = 6;

    theaterList.innerHTML = "";
    movies[movieID].theaters.forEach(theater => {
        let isSelected = theater == selectedTheater ? "clicked" : "";

        let li = document.createElement("li");
        li.setAttribute("onclick", "updateTheaterName(this)");
        li.setAttribute("class", `${isSelected}`);
        li.innerText = `${theater}`;
        theaterList.appendChild(li);
    });
}

function updateTheaterName(selectedTheater) {
    searchTheaterInp.value = "";
    addTheater(selectedTheater.innerText);
    selectTheaterSection.classList.remove("active");
    selectTheaterSection.classList.add("selected");
    theaterName.innerText = selectedTheater.innerText;
}

// SELECT SHOWTIME SECTION
function addShowTime(movieID) {
    movies[movieID].showTimes.forEach(showTime => {
        let li = `<li class="showtime-card">${showTime}</li>`;
        showTimeContainer.insertAdjacentHTML("beforeend", li);
    })
}

// ------------------
// ADD EVENT LISTENER
// ------------------

// SELECT MOVIE SECTION
movieCards.forEach(movieCard => {
    movieCard.addEventListener('click', () => {
        if (movieCard === document.querySelector('.movie-card.clicked')) {
            movieCard.classList.remove("clicked");
        } else {
            document.querySelector('.movie-card.clicked')?.classList.remove('clicked');
            movieCard.classList.add('clicked');
        }
    });
});
movieScrollLeftButton.addEventListener("click", () => {
    movieGrid.scrollLeft -= movieContainerWidth;
});

movieScrollRightButton.addEventListener("click", () => {
    movieGrid.scrollLeft += movieContainerWidth;
});

// SELECT DATE SECTION 
dateCards.forEach(dateCard => {
    dateCard.addEventListener('click', () => {
        document.querySelector('.date-card.clicked')?.classList.remove("clicked");
        dateCard.classList.add('clicked');
    });
});

dateScrollLeftButton.addEventListener("click", () => {
    let dateGrid = document.querySelector(".date-grid");
    let dateCardWidth = Math.ceil(document.querySelector(".date-card").getBoundingClientRect().width);
    dateGrid.scrollLeft -= dateCardWidth;
});

dateScrollRightButton.addEventListener("click", () => {
    let dateGrid = document.querySelector(".date-grid");
    let dateCardWidth = Math.ceil(document.querySelector(".date-card").getBoundingClientRect().width);
    dateGrid.scrollLeft += dateCardWidth;
});

// SELECT THEATER SECTION
selectTheaterWrapper.addEventListener("click", () => {
    selectTheaterSection.classList.toggle("active");
});

searchTheaterInp.addEventListener("keyup", () => {
    //Initialize movieID for testing
    let movieID = 6;

    let arr = [];
    let searchedValue = searchTheaterInp.value.toLowerCase();
    arr = movies[movieID].theaters.filter(data => {
        return data.toLowerCase().startsWith(searchedValue);
    }).map(data => `<li onclick="updateTheaterName(this)">${data}</li>`).join("");
    theaterList.innerHTML = arr ? arr : "NOT FOUND";
});

// SELECT SHOWTIME SECTION 
showTimeCards.forEach(showTimeCard => {
    showTimeCard.addEventListener('click', () => {
        document.querySelector('.showtime-card.clicked')?.classList.remove("clicked");
        showTimeCard.classList.add('clicked');
    });
});

// SELECT SIZE SECTION
upArrow.addEventListener("click", () => {
    if (roomSize < MAX_SIZE) {
        roomSize += 1;
    }
    sizeElm.innerText = roomSize;
});

downArrow.addEventListener("click", () => {
    if (roomSize > MIN_SIZE) {
        roomSize -= 1;
    }
    sizeElm.innerHTML = roomSize;
});

// ----
// Test
// ----