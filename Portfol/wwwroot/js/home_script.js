document.addEventListener('DOMContentLoaded', function () {
    let currentSlide = 0;
    const cardWrapper = document.querySelector('.card-wrapper');
    const cards = document.querySelectorAll('.card');
    const cardsPerRow = 3;
    const totalSlides = Math.ceil(cards.length / cardsPerRow);
    const rightArrow = document.querySelector('.arrow.right');
    const leftArrow = document.querySelector('.arrow.left');

    if (rightArrow) {
        rightArrow.addEventListener('click', next);
    }

    if (leftArrow) {
        leftArrow.addEventListener('click', prev);
    }

    function next() {
        if (currentSlide < totalSlides - 1) {
            currentSlide++;
            cardWrapper.style.transform = `translateX(-${currentSlide * 100}%)`;
        }
    }

    function prev() {
        if (currentSlide > 0) {
            currentSlide--;
            cardWrapper.style.transform = `translateX(-${currentSlide * 100}%)`;
        }
    }

    const right = document.querySelector('.arrow.right');

    const left = document.querySelector('.arrow.left');

    if (right && left) {

        right.addEventListener('click', next);
        left.addEventListener('click', prev);
    }
});