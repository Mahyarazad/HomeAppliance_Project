
function getHtml(val) {
    $.get(val,
        function (htmlPage) {
            $("#ModalConetnt").html(htmlPage);
        });
}

function showModal() {
    $("#MainModal").modal("show");
    $("#MainModal").on("shown.bs.modal",
        function () {
            window.location.hash = "##";
        });
}


$(window).on('hashchange', function () {
    var url = window.location.hash;
    if (url === "##") {
        return
    } else {
        url = url.split("showmodal=")[1];
        getHtml(url);
        showModal();
    }
});



$(document).on("submit",
    'form[data-ajax="true"]',
    function (e) {
        e.preventDefault();
        var form = $(this);
        const method = form.attr("method").toLocaleLowerCase();
        const url = form.attr("action");
        var action = form.attr("data-action");

        if (method === "get") {
            const data = form.serializeArray();
            $.get(url,
                data,
                function (data) {
                    CallBackHandler(data, action, form);
                });
        } else {
            var formData = new FormData(this);
            $.ajax({
                url: url,
                type: "post",
                data: formData,
                enctype: "multipart/form-data",
                dataType: "json",
                processData: false,
                contentType: false,
                success: function (data) {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Your work has been saved',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    setInterval(function () {
                        CallBackHandler(data, action, form);;
                    }, 1500);

                },
                error: function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                        footer: '<a href="">Why do I have this issue?</a>'
                    })
                    //                    alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
                }
            });
        }
        return false;
    });


function CallBackHandler(data, action, form) {
    switch (action) {
        case "Message":
            alert(data.Message);
            break;
        case "Refresh":
            if (data.isSucceeded) {

                window.location.reload();
            } else {
                alert(data.message);
            }
            break;
        case "RefereshList":
            {
                hideModal();
                const refereshUrl = form.attr("data-refereshurl");
                const refereshDiv = form.attr("data-refereshdiv");
                get(refereshUrl, refereshDiv);
            }
            break;
        case "setValue":
            {
                const element = form.data("element");
                $(`#${element}`).html(data);
            }
            break;
        default:
    }
}

function get(url, refereshDiv) {
    const searchModel = window.location.search;
    $.get(url,
        searchModel,
        function (result) {
            $("#" + refereshDiv).html(result);
        });
}

function makeSlug(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(convertToSlug(value));
}

var convertToSlug = function (str) {
    var $slug = '';
    const trimmed = $.trim(str);
    $slug = trimmed.replace(/[^a-z0-9-آ-ی-]/gi, '-').replace(/-+/g, '-').replace(/^-|-$/g, '');
    return $slug.toLowerCase();
};

function checkSlugDuplication(url, dist) {
    const slug = $('#' + dist).val();
    const id = convertToSlug(slug);
    $.get({
        url: url + '/' + id,
        success: function (data) {
            if (data) {
                sendNotification('error', 'top right', "خطا", "اسلاگ نمی تواند تکراری باشد");
            }
        }
    });
}

function fillField(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(value);
}

$(document).on("click",
    'button[data-ajax="true"]',
    function () {
        const button = $(this);
        const form = button.data("request-form");
        const data = $(`#${form}`).serialize();
        let url = button.data("request-url");
        const method = button.data("request-method");
        const field = button.data("request-field-id");
        if (field !== undefined) {
            const fieldValue = $(`#${field}`).val();
            url = url + "/" + fieldValue;
        }
        if (button.data("request-confirm") == true) {
            if (confirm("آیا از انجام این عملیات اطمینان دارید؟")) {
                handleAjaxCall(method, url, data);
            }
        } else {
            handleAjaxCall(method, url, data);
        }
    });

function handleAjaxCall(method, url, data) {
    if (method === "post") {
        $.post(url,
            data,
            "application/json; charset=utf-8",
            "json",
            function (data) {

            }).fail(function (error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!',
                    footer: '<a href="">Why do I have this issue?</a>'
                })
                alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
            });
    }
}

//jQuery.validator.addMethod("maxFileSize",
//    function (value, element, params) {
//        var size = element.files[0].size;
//        var maxSize = 3 * 1024 * 1024;
//        if (size > maxSize)
//            return false;
//        else {
//            return true;
//        }
//    });
//jQuery.validator.unobtrusive.adapters.addBool("maxFileSize");