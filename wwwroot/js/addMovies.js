const addDateContainer = document.querySelector('.add-date-container');
const addDateBTN = document.querySelector('button.add-date')
const dateList = {}

// get image url from api
const endpoint = "https://api.imgbb.com/1/upload";
const apiKey = "62c18fb1790c4f52bb1a031c0332b453";
const fileInput = document.querySelector('input[type="file"]');
const imagePreview = document.querySelector('.preview-image');

fileInput.addEventListener('change', async (e) => {
    const formData = new FormData();
    formData.append('image', e.target.files[0]);
    formData.append('key', apiKey);
    const response = await fetch(endpoint, {
        method: 'POST',
        body: formData
    });
    const data = await response.json();
    imagePreview.style.backgroundImage = `url(${data.data.url})`;
});


// Add New Date
addDateBTN.addEventListener('click', () => {
    const newDateContainer = document.createElement('div');
    newDateContainer.classList.add('new-date');
    const newDateLabel = document.createElement('label');
    newDateLabel.setAttribute('for', 'date');
    newDateLabel.textContent = 'Select a date';
    newDateContainer.appendChild(newDateLabel);

    const newDateInput = document.createElement('input');
    newDateInput.setAttribute('type', 'date');
    newDateInput.setAttribute('id', `date-${newDateContainer.children.length}`);
    // date format: 10 March 2024

    newDateInput.setAttribute('value', new Date().toISOString().split('T')[0]);
    newDateContainer.appendChild(newDateInput);

    // disable button
    addDateBTN.disabled = true;

    addDateContainer.appendChild(newDateContainer);

    // submit button

    const submitDateContainer = document.createElement('div');
    submitDateContainer.classList.add('submit-date-container');

    const cancelBTN = document.createElement('button');
    cancelBTN.textContent = 'Cancel';
    cancelBTN.classList.add('cancel-date');
    submitDateContainer.appendChild(cancelBTN);
    cancelBTN.addEventListener('click', () => {
        addDateBTN.disabled = false;
        newDateContainer.remove();
    });

    const submitBTN = document.createElement('button');
    submitBTN.textContent = 'Add';
    submitBTN.classList.add('submit-date');
    submitDateContainer.appendChild(submitBTN);
    submitBTN.addEventListener('click', () => {
        // add date to database
        // do something with the date
        setTimeout(() => {
            // date format: 10 March 2024
            const date = new Date(newDateInput.value);
            const formattedDate = date.toLocaleString('default', { month: 'long' }) + ' ' + date.getDate() + ' ' + date.getFullYear();

            if (dateList[formattedDate] === undefined) {
                dateList[formattedDate] = [];
            }
            // check if date already exists
            if (dateList[formattedDate].includes(formattedDate)) {
                alert('Date already exists');
                return;
            }

            dateList[formattedDate].push(formattedDate);
            displayDates(dateList[formattedDate]);
            addDateBTN.disabled = false;
            newDateContainer.remove();
        }, 500);

    });

    newDateContainer.appendChild(submitDateContainer);
    isDisplayArrow();

});

// Display Movie Date
const dateHorizontal = document.querySelector('.date-horizontal');

const displayDates = (date) => {
    const dateContainer = document.createElement('div');
    dateContainer.classList.add('date-container');
    const dateP = document.createElement('p');
    dateP.textContent = date;
    const dayOfWeek = document.createElement('p');
    const curDate = new Date(date);
    dayOfWeek.textContent = curDate.toLocaleString('default', { weekday: 'short' });
    dateContainer.appendChild(dayOfWeek);
    dateContainer.appendChild(dateP);

    const ellipsis = document.createElement('div');
    ellipsis.classList.add('ellipsis-container');
    const ellipsisIcon = document.createElement('i');
    ellipsisIcon.classList.add('fa-solid', 'fa-ellipsis-vertical');
    ellipsis.appendChild(ellipsisIcon);

    const ellipsisContent = document.createElement('div');
    ellipsisContent.classList.add('ellipsis-content');

    const deleteDateBTN = document.createElement('button');
    deleteDateBTN.textContent = 'Delete';
    deleteDateBTN.classList.add('delete-date');
    deleteDateBTN.addEventListener('click', () => {
        dateContainer.remove();
        dateList[date].pop(date);
        isDisplayArrow();
    });
    
    const editDateBTN = document.createElement('button');
    editDateBTN.textContent = 'Edit';
    editDateBTN.classList.add('edit-date');
    editDateBTN.addEventListener('click', () => {
        // edit date
    });

    ellipsisContent.appendChild(editDateBTN);
    ellipsisContent.appendChild(deleteDateBTN);
    ellipsisContent.style.display = 'none';
    ellipsis.appendChild(ellipsisContent);

    dateContainer.appendChild(ellipsis);

    ellipsisIcon.addEventListener('click', () => {
        ellipsisContent.style.display = ellipsisContent.style.display === 'none' ? 'block' : 'none';
        // editDateBTN.style.display = editDateBTN.style.display === 'none' ? 'block' : 'none';
        // deleteDateBTN.style.display = deleteDateBTN.style.display === 'none' ? 'block' : 'none';
    });


    dateHorizontal.appendChild(dateContainer);
    let children = dateHorizontal.children;
    let currentDatePick = dateContainer;
    // add active class to date that clicked
    for (let i = 0; i < children.length; i++) {
        children[i].addEventListener('click', () => {
            for (let j = 0; j < children.length; j++) {
                children[j].classList.remove('active');
            }
            children[i].classList.add('active');
            currentDatePick = children[i];
            console.log(currentDatePick);
        });
    }
    isDisplayArrow();
}

// select cinema and add showtime
const cinemaList = [
    {"name": "Cinema 1", "type": "SF Cinema"},
    {"name": "Cinema 2", "type": "Major Cineplex"},
    {"name": "Cinema 3", "type": "Major Cineplex"},
    {"name": "Cinema 4", "type": "SF Cinema"},
    {"name": "Cinema 5", "type": "Major Cineplex"},
    {"name": "Cinema 6", "type": "SF Cinema"},
    {"name": "Cinema 7", "type": "Major Cineplex"},
    {"name": "Cinema 8", "type": "SF Cinema"},
    {"name": "Cinema 9", "type": "Major Cineplex"},
    {"name": "Cinema 10", "type": "SF Cinema"},
    {"name": "Cinema 11", "type": "Major Cineplex"},
    {"name": "Cinema 12", "type": "SF Cinema"},
    {"name": "Cinema 13", "type": "Major Cineplex"},
    {"name": "Cinema 14", "type": "SF Cinema"},
    {"name": "Cinema 15", "type": "Major Cineplex"},
    {"name": "Cinema 16", "type": "SF Cinema"},
    {"name": "Cinema 17", "type": "Major Cineplex"},
    {"name": "Cinema 18", "type": "SF Cinema"},
    {"name": "Cinema 19", "type": "Major Cineplex"},
    {"name": "Cinema 20", "type": "SF Cinema"},
    {"name": "Cinema 21", "type": "Major Cineplex"},
    {"name": "Cinema 22", "type": "SF Cinema"},
    {"name": "Cinema 23", "type": "Major Cineplex"},
    {"name": "Cinema 24", "type": "SF Cinema"},
    {"name": "Cinema 25", "type": "Major Cineplex"},
    {"name": "Cinema 26", "type": "SF Cinema"},
    {"name": "Cinema 27", "type": "Major Cineplex"},
    {"name": "Cinema 28", "type": "SF Cinema"},
    {"name": "Cinema 29", "type": "Major Cineplex"},
    {"name": "Cinema 30", "type": "SF Cinema"},
]

{/* <div class="cinema-showtime">
        <div class="cinema-container">
            <div class="cinema-name">
                <h2>Mega Cineplex</h2>
            </div>
            <div class="showtime">
                <div class="add-showtime-container">
                    <div class="add-showtime-title">
                        <p>Add Showtime</p>
                        <button type="button" title="add-showtime-btn" class="add-showtime"><i
                                class="fa-solid fa-plus"></i></button>
                    </div>
                    <div class="add-showtime-input-container">
                        <div class="add-showtime-input">
                            <label>Select a Time</label>
                            <input type="time" id="showtime" value="@DateTime.Now.ToString("HH:mm")">
                        </div>
                        <div class="add-showtime-submit">
                            <button class="cancel-showtime">Cancel</button>
                            <button class="submit-showtime">Submit</button>
                        </div>
                    </div>
                </div>
                <div class="showtime-container">
                    <span>10:00</span>
                    <span>10:00</span>
                    <span>10:00</span>
                    <span>10:00</span>
                    <span>10:00</span>
                    <span>10:00</span>
                    <span>10:00</span>
                    <span>10:00</span>
                    <span>10:00</span>

                </div>
            </div>
        </div>
    </div> */}


const cinemaShowtimeContainer = document.querySelector('.cinema-showtime');
cinemaList.forEach(cinema => {
    const cinemaContainer = document.createElement('div');
    cinemaContainer.classList.add('cinema-container');

    const cinemaName = document.createElement('div');
    cinemaName.classList.add('cinema-name');
    const h2 = document.createElement('h2');
    h2.textContent = cinema.name;
    cinemaName.appendChild(h2);

    cinemaContainer.appendChild(cinemaName);

    ///

    const showtime = document.createElement('div');
    showtime.classList.add('showtime');

    const addShowtimeContainer = document.createElement('div');
    addShowtimeContainer.classList.add('add-showtime-container');
    
    const addShowtimeTitle = document.createElement('div');
    addShowtimeTitle.classList.add('add-showtime-title');
    const p = document.createElement('p');
    p.textContent = 'Add Showtime';
    const addShowtimeBTN = document.createElement('button');
    addShowtimeBTN.setAttribute('type', 'button');
    addShowtimeBTN.classList.add('add-showtime');
    const i = document.createElement('i');
    i.classList.add('fa-solid', 'fa-plus');
    addShowtimeBTN.appendChild(i);
    addShowtimeTitle.appendChild(p);
    addShowtimeTitle.appendChild(addShowtimeBTN);
    addShowtimeContainer.appendChild(addShowtimeTitle);
    const showtimeContainer = document.createElement('div');
    showtimeContainer.classList.add('showtime-container');
    
    addShowtimeBTN.addEventListener('click', () => {

        // disable add button
        addShowtimeBTN.disabled = true;

        const addShowtimeInputContainer = document.createElement('div');
        addShowtimeInputContainer.classList.add('add-showtime-input-container');

        const addShowtimeInput = document.createElement('div');
        addShowtimeInput.classList.add('add-showtime-input');
        const label = document.createElement('label');
        label.textContent = 'Select a Time';
        const input = document.createElement('input');
        input.setAttribute('type', 'time');

    
        addShowtimeInput.appendChild(label);
        addShowtimeInput.appendChild(input);
        addShowtimeInputContainer.appendChild(addShowtimeInput);

        const addShowtimeSubmit = document.createElement('div');
        addShowtimeSubmit.classList.add('add-showtime-submit');
        const cancelBTN = document.createElement('button');
        cancelBTN.textContent = 'Cancel';
        cancelBTN.classList.add('cancel-showtime');
        const submitBTN = document.createElement('button');
        submitBTN.textContent = 'Submit';
        submitBTN.classList.add('submit-showtime');
        addShowtimeSubmit.appendChild(cancelBTN);
        addShowtimeSubmit.appendChild(submitBTN);

        cancelBTN.addEventListener('click', () => {
            addShowtimeBTN.disabled = false;
            addShowtimeInputContainer.remove();
        });


        submitBTN.addEventListener('click', () => {
            // add showtime to database
            // do something with the showtime
            setTimeout(() => {
                // add showtime to database
                addShowtimeBTN.disabled = false;
                
                const showtime = input.value;
                if (showtime === '') {
                    alert('Showtime cannot be empty');
                    return;
                }


                /////

                // save showtime to database here

                /////

                const span = document.createElement('span');
                span.textContent = showtime;
                showtimeContainer.appendChild(span);
                addShowtimeInputContainer.remove();
                
            }, 500);
        });

        addShowtimeInputContainer.appendChild(addShowtimeSubmit);
        addShowtimeContainer.appendChild(addShowtimeInputContainer);
    });


    showtime.appendChild(addShowtimeContainer);
    showtime.appendChild(showtimeContainer);

    cinemaShowtimeContainer.appendChild(cinemaContainer);
    cinemaContainer.appendChild(showtime);
});




// JavaScript code to observe container size changes and apply corresponding classes
const container = document.querySelector('.date-horizontal');
const leftArrow = document.querySelector('i.fa-chevron-left');
const rightArrow = document.querySelector('i.fa-chevron-right');

leftArrow.addEventListener('click', () => {
    container.scrollLeft -= container.clientWidth/currentItems;
    startOffset -= 1;
    if (startOffset < 0) {
        startOffset = 0;
    }
    isDisplayArrow();
});

rightArrow.addEventListener('click', () => {
    container.scrollLeft += container.clientWidth/currentItems;
    startOffset += 1;
    if (startOffset > container.children.length) {
        startOffset = container.children.length;
    }
    isDisplayArrow();
});

let startOffset = 0;
let currentItems = 5;
const observer = new ResizeObserver(entries => {
    for (const entry of entries) {
        const width = entry.contentRect.width;
        if (width > 870) {
            container.classList.add('large');
            container.classList.remove('small');
            container.classList.remove('medium');
            container.classList.remove('xsmall');
            container.classList.remove('xxsmall');
            currentItems = 5;
        } else if (width > 650) {
            container.classList.add('medium');
            container.classList.remove('small');
            container.classList.remove('large');
            container.classList.remove('xsmall');
            container.classList.remove('xxsmall');
            currentItems = 4;


        } else if (width > 360) {
            container.classList.add('small');
            container.classList.remove('medium');
            container.classList.remove('large');
            container.classList.remove('xsmall');
            container.classList.remove('xxsmall');
            currentItems = 3;
        }
        else if (width > 200) {
            container.classList.add('xsmall');
            container.classList.remove('medium');
            container.classList.remove('large');
            container.classList.remove('small');
            container.classList.remove('xxsmall');
            currentItems = 2;
        }
        else {
            container.classList.add('xxsmall');
            container.classList.remove('medium');
            container.classList.remove('large');
            container.classList.remove('small');
            container.classList.remove('xsmall');
            currentItems = 1;
        }

        isDisplayArrow();
    }
});

observer.observe(container);


// left and right arrow
const isDisplayArrow = () => {
    const dateContainer = document.querySelectorAll('.date-container');
    if (dateContainer.length === 0) {
        leftArrow.style.display = 'none';
        rightArrow.style.display = 'none';
        return;
    }
    if (dateContainer.length <= currentItems) {
        leftArrow.style.display = 'none';
        rightArrow.style.display = 'none';
        return;
    }
    if (startOffset === 0) {
        leftArrow.style.display = 'none';
        rightArrow.style.display = 'block';
        return;
    }
    if (dateContainer.length - startOffset <= currentItems) {
        leftArrow.style.display = 'block';
        rightArrow.style.display = 'none';
        return;
    }
    leftArrow.style.display = 'block';  
    rightArrow.style.display = 'block';
}
