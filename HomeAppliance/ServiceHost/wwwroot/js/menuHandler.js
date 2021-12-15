$(".dropdown").on('mouseleave', function (e) {
    e.preventDefault;
    $("#admin-dropdwon").removeClass('open');
});
$(".user-info").on('mouseenter', function (e) {
    e.preventDefault;
    $("#admin-dropdwon").addClass('open');
});


