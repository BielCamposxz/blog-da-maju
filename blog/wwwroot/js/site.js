console.log("Eu te amo maria julia!");
console.log("Um dia nos vamos nos casar");
console.log("se vc esta lendo isso tudo deu certo, e contruimos uma familia juntos");
console.log("escrito dia: 4/10/2025 16:26")

window.addEventListener('scroll', function () {
    const navbar = document.querySelector('.navbar');
    if (window.scrollY > 50) {
        navbar.classList.add('scrolled');
    } else {
        navbar.classList.remove('scrolled');
    }
});

    document.addEventListener("DOMContentLoaded", function() {
    const navbar = document.getElementById("scrollNavbar");
    const heroHeight = document.querySelector(".hero-refined").offsetHeight;

    // Navbar inicia invisível
    navbar.style.opacity = 0;
    navbar.style.transition = "opacity 0.4s";

    window.addEventListener("scroll", function() {
        if (window.scrollY > heroHeight - 80) { // 80px para ajustar altura do navbar
        navbar.style.opacity = 1;
    navbar.classList.add("bg-white", "shadow");
        } else {
        navbar.style.opacity = 0;
    navbar.classList.remove("bg-white", "shadow");
        }
    });
});

document.querySelectorAll('a.nav-link[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        const offset = 80; // ajuste conforme altura da navbar
        const bodyRect = document.body.getBoundingClientRect().top;
        const targetRect = target.getBoundingClientRect().top;
        const targetPosition = targetRect - bodyRect - offset;

        window.scrollTo({
            top: targetPosition,
            behavior: 'smooth'
        });
    });
});

function previewPostImage(event) {
    const input = event.target;
    if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const previewWrapper = document.getElementById('postPreviewWrapper');
            const previewImg = document.getElementById('postPreview');
            previewImg.src = e.target.result;
            previewWrapper.style.display = 'block'; // mostra a div
        }
        reader.readAsDataURL(input.files[0]);
    }
}
