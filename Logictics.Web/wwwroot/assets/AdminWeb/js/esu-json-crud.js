function create(actionUrl, data) {
    $.ajax({
        type: "POST",
        url: actionUrl,
        data: JSON.stringify(data),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $("body").LoadingOverlay("show");
            //prevent back button
            setPreventLeavingPage();
        },
        success: function (result) {
            $("body").LoadingOverlay("hide");
            if (result.statusCode >= 200 && result.statusCode < 400) {
                swal.fire({
                    title: "Thông Báo",
                    text: result.message,
                    icon: "success",
                    button: false,
                    timer: 2000,
                }).then(() => {
                    window.location.href = result.href;
                });
            }
        },
        error: function (xhr) {
            $("body").LoadingOverlay("hide");
            if (xhr.status == 401) {
                swal.fire({
                    title: "Thông Báo",
                    text: "Phiên đăng nhập hết hạn",
                    icon: "error",
                    //timer: 2000,
                }).then(() => {
                    window.location.href = "/Login/Index";
                });
            } else {
                swal.fire({
                    title: "Thông Báo",
                    text: xhr.status,
                    icon: "error",
                    //timer: 2000,
                });
            }
        },
    }).done(() => {
        removePreventLeavingPage();
    });
}

function update(actionUrl, data) {
    $.ajax({
        type: "POST",
        url: actionUrl,
        data: JSON.stringify(data),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $("body").LoadingOverlay("show");
            //prevent back button
            setPreventLeavingPage();
        },
        success: function (result) {
            $("body").LoadingOverlay("hide");
            if (result.statusCode >= 200 && result.statusCode < 400) {
                swal.fire({
                    title: "Thông Báo",
                    text: result.message,
                    icon: "success",
                    button: false,
                    timer: 2000,
                }).then(() => {
                    window.location.href = result.href;
                });
            } else {
                swal.fire({
                    title: "Thông Báo",
                    text: result.message,
                    icon: "warning",
                    button: false,
                    timer: 2000
                }).then(() => {
                    window.location.href = result.href;
                });
            }
        },
        error: function (xhr) {
            $("body").LoadingOverlay("hide");
            if (xhr.status == 401) {
                swal.fire({
                    title: "Thông Báo",
                    text: "Phiên đăng nhập hết hạn",
                    icon: "error",
                    //timer: 2000,
                }).then(() => {
                    window.location.href = "/Login/Index";
                });
            } else {
                swal.fire({
                    title: "Thông Báo",
                    text: xhr.status,
                    icon: "error",
                    //timer: 2000,
                });
            }
        },
    }).done(() => {
        removePreventLeavingPage();
    });
}

function setPreventLeavingPage() {
    window.onbeforeunload = function () {
        return "Vui lòng chờ đến hết tiến trình";
    }
}

function removePreventLeavingPage() {
    window.onbeforeunload = null;
}