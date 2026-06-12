// Site JavaScript
document.addEventListener('DOMContentLoaded', function() {
    // Hide site intro after 1.5s
    const intro = document.getElementById('site-intro');
    if (intro) {
        setTimeout(() => {
            intro.style.opacity = '0';
            setTimeout(() => intro.remove(), 400);
        }, 1500);
    }
});
