function toggleMenu() {
    const sideMenu = document.getElementById('side-menu');
    sideMenu.classList.toggle('active');
}

document.getElementById('hamburger').addEventListener('click', toggleMenu);