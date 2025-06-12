document.addEventListener('DOMContentLoaded',function() { //wait until the entire page is loaded before running script
    const toggleButton = document.getElementById('darkModeToggle');  //grabs the dark mode toggle from from the layout using its ID
    const darkModeIcon = document.getElementById('darkModeIcon'); //Grabs the icon inside the button 
    const body = document.body; //shortcut for body tag

    function updateIcon(isDark){ //helper function to return 
        darkModeIcon.textContent = isDark ? '☀️' : '🌙'; //sun if dark mode is on, and moon if light mode is on
    }

    const savedTheme = localStorage.getItem('theme'); //checks if the user has previously selected theme and stores
    const isDark = savedTheme === 'dark' //set isDark to true if the saved theme is dark

    if (isDark) { //if isDark is true
        body.classList.add('dark-mode') //apply dark mode immediately by adding the class
    }

    if (localStorage.getItem('theme') === 'dark') {  //checks browsers localStorage looking for theme, and checks if is 'dark
        body.classList.add('dark-mode'); //if true, adds dark-mode to the <body> element
    }
    updateIcon(isDark); //updates icon

    toggleButton?.addEventListener('click', ()=>{ //when the class is clicked, ?. ensures the button exists before trying to use it
        const enabled = body.classList.toggle('dark-mode'); //toggles the class on/off(if on, turns off. if off, turns on)
        localStorage.setItem('Theme', enabled ? 'dark' : 'light'); //saves user preference after refresh
        updateIcon(enabled); //updates the icon after toggle
    });
});