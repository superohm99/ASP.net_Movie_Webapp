const hamburgerBar = document.querySelector('header > nav.header > ul > li > i.fa-bars');
hamburgerBar.addEventListener('click', () => {
    const mediaQuery = window.matchMedia('(max-width: 640px)');
    if (mediaQuery.matches) {
        const section = document.querySelector('section');
        section.classList.add('active');
    }
    else{
        const wrapper = document.querySelector('.wrapper');
        const navText = document.querySelectorAll('aside > nav.sidebar > ul > li > a > span');

        // get grid template columns of wrapper
        const gridTemplateColumns = window.getComputedStyle(wrapper).gridTemplateColumns;
        firstColumn = gridTemplateColumns.split(' ')[0];
        if (firstColumn === '250px') {
            wrapper.style.gridTemplateColumns = '50px 1fr';
            // navText.forEach((text) => {
            //     text.classList.add('hide');
            // });
            
        }
        else{
            wrapper.style.gridTemplateColumns = '250px 1fr';
            // navText.forEach((text) => {
            //     text.classList.remove('hide');
            // });
        }
    }
});

const closeBar = document.querySelector('section > nav.sidebar-responsive > .close-menu > i.fa-times');
closeBar.addEventListener('click', () => {
    const section = document.querySelector('section');
    section.classList.remove('active');
});

// onresize event
window.onresize = () => {
    const section = document.querySelector('section');
    const wrapper = document.querySelector('.wrapper');
    const mediaQuery = window.matchMedia('(max-width: 640px)');
    const gridTemplateColumns = window.getComputedStyle(wrapper).gridTemplateColumns;

    if (mediaQuery.matches) {
        // section.classList.add('active');
        wrapper.style.gridTemplateColumns = '50px 1fr';
    }
    else{
        section.classList.remove('active');
        // wrapper.style.gridTemplateColumns = '250px 1fr';
    }
};