// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function imageFull(img) {
    if (!img.classList.contains("imgzoom")) {
        img.classList.add("imgzoom");
        window.onwheel = preventDefault; // modern standard
        window.onmousewheel = preventDefault; // older browsers, IE
        window.ontouchmove = preventDefault; // mobile
        document.onkeydown = preventDefaultForScrollKeys;
    }
    else {
        img.classList.remove("imgzoom");
        window.onwheel = null; // modern standard
        window.onmousewheel = null; // older browsers, IE
        window.ontouchmove = null; // mobile
        document.onkeydown = null;
    }
}