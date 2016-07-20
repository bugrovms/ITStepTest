$(document).ready(function () {
    $('#change-password-block').hide();
    $("#btn-change-password").click(function () {
        if (!$('#change-password-block').is(":visible")) {
            $('#change-password-block').show(500);
        } else {
            $('#change-password-block').hide(500);
        }

    });

    $('#register-role-select').on('change', function () {
        var selected = $(this).find("option:selected").val();
        if (selected == 1 || selected == 2) {
            $('#register-group-select').hide();
        } else {
            $('#register-group-select').show();
        }
    });

});