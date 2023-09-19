let rootElement = document.documentElement;
let themeSwitcher = document.getElementById("theme-switcher-button");
let lightThemeSelector = document.getElementById("light-theme");
let darkThemeSelector = document.getElementById("dark-theme");

document.addEventListener("readystatechange", () => {
    if(localStorage.getItem("theme") == "light" || localStorage.getItem("theme") == undefined || localStorage.getItem("theme") == "") {
        themeSwitcher.innerHTML = "<i class='bi bi-brightness-high-fill mx-2'></i>";
        rootElement.setAttribute("data-bs-theme", "light");
    }
    else if(localStorage.getItem("theme") == "dark") {
        themeSwitcher.innerHTML = "<i class='bi bi-moon-stars-fill mx-2'></i>";
        rootElement.setAttribute("data-bs-theme", "dark");
    }
});

lightThemeSelector.addEventListener("click", () => {
    rootElement.setAttribute("data-bs-theme", "light");
    themeSwitcher.innerHTML = "<i class='bi bi-brightness-high-fill mx-2'></i>";
    localStorage.setItem("theme", "light");
});

darkThemeSelector.addEventListener("click", () => {
    rootElement.setAttribute("data-bs-theme", "dark");
    themeSwitcher.innerHTML = "<i class='bi bi-moon-stars-fill mx-2'></i>";
    localStorage.setItem("theme", "dark");
});