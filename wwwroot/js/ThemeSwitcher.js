var rootElement = document.documentElement;
var themeSwitcher = document.getElementById("theme-switcher-button");
var lightThemeSelector = document.getElementById("light-theme");
var darkThemeSelector = document.getElementById("dark-theme");

document.addEventListener("DOMContentLoaded", () => {
    if(rootElement.getAttribute("data-bs-theme") == "light") {
        themeSwitcher.innerHTML = "<i class='bi bi-brightness-high-fill mx-2'></i>";
    }
    else if(rootElement.getAttribute("data-bs-theme") == "dark") {
        themeSwitcher.innerHTML = "<i class='bi bi-moon-stars-fill mx-2'></i>";
    }
});

lightThemeSelector.addEventListener("click", () => {
    rootElement.setAttribute("data-bs-theme", "light");
    themeSwitcher.innerHTML = "<i class='bi bi-brightness-high-fill mx-2'></i>";
});

darkThemeSelector.addEventListener("click", () => {
    rootElement.setAttribute("data-bs-theme", "dark");
    themeSwitcher.innerHTML = "<i class='bi bi-moon-stars-fill mx-2'></i>";
});