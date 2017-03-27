// on page load, assign handler functions
$(document).ready(function () {
    $('#left-menu').sidr();

    // menu icon handler
    var menuFlag = false;
    $('#left-menu').bind('touchstart click', function () {
        var iconObj = $('#menu-icon');
        const hamburger = 'glyphicon-menu-hamburger';
        const close = 'glyphicon-arrow-left';

        // ensure touchstart & click don't cancel each other
        if (!menuFlag) {
            menuFlag = true;
            setTimeout(function () { menuFlag = false }, 100);
        }

        // swap icon accordingly
        if (iconObj.hasClass(hamburger)) {
            iconObj.removeClass(hamburger);
            iconObj.addClass(close);
        } else if (iconObj.hasClass(close)) {
            iconObj.removeClass(close);
            iconObj.addClass(hamburger);
        }
    });

});