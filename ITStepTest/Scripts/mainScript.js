(function ($) {
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
    updateAnswer(item, function () {
        let index = parseInt(item, 10);
        $('.question-' + item).addClass('hide');
        if (item == 0) {
            var newIndex = (maxLengthQuestions - 1)
            $('.question-' + newIndex).removeClass('hide');
        } else {
            var leftIndex = index - 1;
            $('.question-' + leftIndex).removeClass('hide');
        }
    });
}

function rightQuestionStart(item) {   
    updateAnswer(item, function () {
        let index = parseInt(item, 10);
        $('.question-' + item).addClass('hide');
        if (item == (maxLengthQuestions - 1)) {
            $('.question-' + 0).removeClass('hide');
        } else {
            var newIndex = index + 1;
            $('.question-' + newIndex).removeClass('hide');
        }
    });
}

function updateAnswer(item, callback) {
     var dataTest = $("#form-answer-" + item).attr("data-test");
    var datQuestion = $("#form-answer-" + item).attr("data-question");
    var frm = $("#form-answer-" + item);
    var data = frm.serializeFormJSON();
    var requestData = {};
    if (data.variantId != undefined) {
        requestData.test = dataTest;
        requestData.question = datQuestion;
        requestData.variantId = data.variantId;
        requestData.update = cookiesCheck(item, dataTest);
    }

    $.ajax({
        url: "/Result/UpdateRadio",
        type: "POST",
        data: requestData,
        success: function () {
            callback();
        }
    });
}

function cookiesCheck(item, test) {
    var store = localStorage.getItem(test);
    if (store === undefined || store == null) {
        return "false";
    } else {
        items = store.split(',');
        for (var i=0; i<items.length; i++) {
            if (item[i] == item) {
                return "true";
            }
        }
        return "false";
    }   
}

function setCookies(item, test) {
    var store = localStorage.getItem(test);
    var items = [];
    if (store === undefined) {
        items.push(item);
        localStorage.setItem(test, items.join())
    } else {
        items = store.split(',');
        var info = false;
        for (var i = 0; i < items.length; i++) {
            if (item[i] == item) {
                info = true;
                break;
            }
        }
        if (!info) {
            items.push(item);
        }
        localStorage.setItem(test, items.join())
    }
}

$("#btn-change-password").ready(function () {
    $.ajax({
        url: "/Result/Information",
        type: "GET",
        success: function (data) {
            console.log("data", data);
            var response = $.parseJSON(data);
            console.log("response", response);
            $(".test-result-list").html(prepareResultTestList(response));
        }
    });
});

function prepareResultTestList(data) {
    let result = "";
    data.forEach(function (item) {
        result += templateResult(item);
    });
    return result;
}

function templateResult(item) {
    var res = "<li>" +
       "<div class='show-marker'><span class='glyphicon glyphicon-tag' aria-hidden='true'></span></div>" +
       "<div class='main-content-item-list'>" +
       item.TestName +
       item.SubjectName +
       " </div>" +
       " <div class='balls-block-item-list'>" +
       convertBalls(item.Balls) +
       "</div>" +
       " <div class='result-block-item-list'>" +
       generateResultImage(item.Balls) +
       "</div>" +
       "</li>";
    return res;
}

function convertBalls(bal) {
    return (parseInt(bal) / 100);
}

function generateResultImage(bal) {
    var img = "star-1";
    var ball = convertBalls(bal);
    if (ball > 6 && ball <= 9) {
        img = "star-2";
    } else if (ball > 9) {
        img = "star-3";
    }
    return ("<img class='star-result' src='/Content/Images/" + img + ".png' />");
}