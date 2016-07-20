$(document).ready(function () {
    $('#change-password-block').hide();
    $("#btn-change-password").click(function () {
        if (!$('#change-password-block').is(":visible")) {
            $('#change-password-block').show(500);
        } else {
            $('#change-password-block').hide(500);
        }

    });
});