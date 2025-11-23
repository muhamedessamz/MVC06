$(function () {
    $("#sidebarToggle").on("click", function (e) {
        e.preventDefault();
        $("#sidebar").toggleClass("open");
    });
    $(window).on("resize", function () {
        if ($(window).width() > 991) {
            $("#sidebar").removeClass("open");
        }
    });
});
