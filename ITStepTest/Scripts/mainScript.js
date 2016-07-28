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

    $("#search-element").on("change paste keyup", function () {
        var searchText = $(this).val();
        $(".base-items-list>li").each(function (index) {
            var text = $(this).find(".main-content-item-list").html();
            if (text.toUpperCase().indexOf(searchText.toUpperCase()) + 1) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });

    var userSearchArray = ['Администратор', 'Студент', 'Преподаватель'];

    let prepareUserArray = function (element, value) {
        var check = userSearchArray.some(function (item, index, array) {
            return item == value;
        });
        if ($(element).prop("checked")) {
            if (!check) { userSearchArray.push(value); }
        } else {
            if (check) {
                for (var i = 0; i < userSearchArray.length; i++) {
                    if (userSearchArray[i] == value) {
                        userSearchArray.splice(i, 1);
                        break;
                    }
                }
            }
        }
    };

    var searchUser = function () {
        $(".user-base-list>li").each(function (index) {

            var text = $(this).find(".main-content-help-info>span").html();
            var checkRole = userSearchArray.some(function (element, index, array) {
                return element == text;
            });
            if (checkRole) {
                var checkActive = userSearchArray.some(function (element, index, array) {
                    return element == 'graphicon-red';
                });
                if (checkActive) {
                    var notActive = $(this).find(".glyphicon-tag").hasClass("graphicon-red");
                    if (notActive) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                } else {
                $(this).show();
                }
               
            } else {
                $(this).hide();
            }            
        });
    };

    $("#activate").on("change", function () {
        prepareUserArray(this, 'graphicon-red');        
        searchUser();
    });

    $("#admins").on("change", function () {
        prepareUserArray(this, 'Администратор');
        searchUser();
    });

    $("#teachers").on("change", function () {
        prepareUserArray(this, 'Преподаватель');
        searchUser();
    });

    $("#users").on("change", function () {
        prepareUserArray(this, 'Студент');
        searchUser();
    })

});