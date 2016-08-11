﻿(function ($) {
    $.fn.serializeFormJSON = function () {

        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };
})(jQuery);

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
    });

    $("#search-comment").on("change paste keyup", function () {
        var searchText = $(this).val();
        $(".comments-items-list>li").each(function (index) {
            var text = $(this).find(".text-comment-item-list").html();
            if (text.toUpperCase().indexOf(searchText.toUpperCase()) + 1) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });

    /**/ 

});

$("#createQuestionBtn").click(function () {
    var frm = $("#createQuestionForm");
    var data = frm.serializeFormJSON();
    $.ajax({
        url: "/Question/Create",
        type: "POST",
        data: data,
        success: function () {
            console.log("done");
            window.location.href = "/Question/List/" + data.test;
        }
    });
});

$(".addVariantBtn").click(function (e) {
   // e.preventDefault();
    var dataid = $(this).attr("data-id");
    var testId = $(this).attr("data-test");
    loadCreateModal(dataid, testId);
});


function loadCreateModal(questionId, testId) {
    $.ajax({
        type: "POST",
        url: "/Question/CreateModal",
        data: {
            questionId: questionId,
            testId: testId
        },
        success: function (data) {
            $("#createVariantForm").html(data);
        }
    });
}

$("#requestVariantBtn").click(function () {
    var frm = $("#createVariantForm");
    var data = frm.serializeFormJSON();
    var test = data.test;
    delete data.test;
    $.ajax({
        url: "/Variant/Create",
        type: "POST",
        data: data,
        success: function () {
            window.location.href = "/Question/List/" + test;
        }
    });
});

$(".deleteVariantBtn").click(function (e) {
    // e.preventDefault();
    var dataid = $(this).attr("data-id");
    var link = $(this).attr("data-link");
    loadDeleteModal(dataid, link);
});

function loadDeleteModal(Id, link) {
    $.ajax({
        type: "POST",
        url: "/Question/DeleteVariantModal",
        data: {
            id: Id,
            link: link
        },
        success: function (data) {
            $("#deleteVariantForm").html(data);
        }
    });
}

$("#requestVariantDeleteBtn").click(function () {
    var frm = $("#deleteVariantForm");
    var data = frm.serializeFormJSON();
    var link = data.link;
    delete data.link;
    $.ajax({
        url: "/Variant/Delete",
        type: "POST",
        data: data,
        success: function () {
            window.location.href = link;
        }
    });
});

/*edit*/

$(".editVariantBtn").click(function (e) {
    // e.preventDefault();
    var dataid = $(this).attr("data-id");
    var link = $(this).attr("data-link");
    loadEditModal(dataid, link);
});

function loadEditModal(Id, link) {
    $.ajax({
        type: "POST",
        url: "/Question/EditVariantModal",
        data: {
            id: Id,
            link: link
        },
        success: function (data) {
            $("#editVariantForm").html(data);
        }
    });
}

$("#requestVariantEditBtn").click(function () {
    var frm = $("#editVariantForm");
    var data = frm.serializeFormJSON();
    var link = data.link;
    delete data.link;
    $.ajax({
        url: "/Variant/Edit",
        type: "POST",
        data: data,
        success: function () {
            window.location.href = link;
        }
    });
});

function leftQuestionStart(item) {
    console.log("left", item);
}

function rightQuestionStart(item) {
    console.log("right", item);
}