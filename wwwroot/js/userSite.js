const imagesOBJ = {
    1: {
        imageURL: 'https://uppic.cloud/ib/pqyRSqblvf.jpg',
        imageAlt: 'Ethernal'
    },
    2: {
        imageURL: 'https://uppic.cloud/ib/yKEwr3XQ6P.jpg',
        imageAlt: 'Avatar'
    },
    3: {
        imageURL: 'https://neramitnungfilm.com/wp-content/uploads/2023/11/Skybanner-300x100-cm.jpg?v=1.0',
        imageAlt: '4Kings2'
    },
    4: {
        imageURL: 'https://i.pinimg.com/originals/60/a9/52/60a9521099dea9cf38fe1526f9e9ce5b.jpg',
        imageAlt: 'GozillaVsKong'
    },
    5: {
        imageURL: 'https://i0.wp.com/www.piratesandprincesses.net/wp-content/uploads/2023/12/Kung-Fu-Panda-4-Banner.jpg?fit=1080%2C745&ssl=1',
        imageAlt: 'KungFuPanda4'
    }
}
const preview = document.querySelector('.preview');
let imageIndex = 0;

preview.innerHTML = `
        <li><img src="${imagesOBJ[1].imageURL}" alt="${imagesOBJ[1].imageAlt}"></li>
        <li><img src="${imagesOBJ[2].imageURL}" alt="${imagesOBJ[2].imageAlt}"></li>
        <li><img src="${imagesOBJ[3].imageURL}" alt="${imagesOBJ[3].imageAlt}"></li>
        <li><img src="${imagesOBJ[4].imageURL}" alt="${imagesOBJ[4].imageAlt}"></li>
        <li><img src="${imagesOBJ[5].imageURL}" alt="${imagesOBJ[5].imageAlt}"></li>
`;

let previewImage = document.querySelector(`[alt="${imagesOBJ[imageIndex%5+1].imageAlt}"]`);
preview.children[imageIndex%5].classList.add('active');

const banner = document.querySelector('.banner');
const bannerImage = document.createElement('img');
bannerImage.src = imagesOBJ[imageIndex%5+1].imageURL;
bannerImage.alt = imagesOBJ[imageIndex%5+1].imageAlt;
banner.appendChild(preview);
banner.appendChild(bannerImage);

const currentImageFocus = () => {
    for(let i = 0; i < preview.children.length; i++){
        if(preview.children[i].firstElementChild.alt === bannerImage.alt){
            preview.children[i].classList.add('active');
        }
        else{
            preview.children[i].classList.remove('active');
        }
    }
}

const slider = () =>{
    imageIndex++;
    bannerImage.src = imagesOBJ[imageIndex%5+1].imageURL;
    bannerImage.alt = imagesOBJ[imageIndex%5+1].imageAlt;
    // console.log(bannerImage.alt);
    previewImage = document.querySelector(`[alt="${imagesOBJ[imageIndex%5+1].imageAlt}"]`);
    for(let i = 0; i < preview.children.length; i++){
        if(preview.children[i].firstElementChild.alt === previewImage.alt){
            preview.children[i].classList.add('active');
        }
        else{
            preview.children[i].classList.remove('active');
        }
    }
}

setInterval(slider, 3000);

preview.addEventListener('click', (e) => {
    if(e.target.tagName === 'IMG'){
        banner.children[1].src = e.target.src;
        banner.appendChild(preview);
        imageIndex = e.target.alt === 'Ethernal' ? 0 : e.target.alt === 'Avatar' ? 1 : e.target.alt === '4Kings2' ? 2 : e.target.alt === 'GozillaVsKong' ? 3 : 4;
        
        for(let i = 0; i < preview.children.length; i++){
            if(preview.children[i].firstElementChild.alt === e.target.alt){
                preview.children[i].classList.add('active');
            }
            else{
                preview.children[i].classList.remove('active');
            }
        }
    }
});


document.querySelector('.caret-down > i.fa-caret-down').addEventListener('click', function () {
    document.querySelector('.navlist-responsive').classList.toggle('show');
});