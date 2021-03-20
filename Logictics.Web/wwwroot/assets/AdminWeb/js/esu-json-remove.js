function remove(url, id) {
    swal.fire({
        title: "Thông Báo",
        text: "Bạn có muốn xóa nội dung này?",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: 'Đồng ý!',
        cancelButtonText: 'Hủy',
        cancelButtonColor: '#d33',
        dangerMode: true,
    }).then(willDelete => {
        if (willDelete.value) {
            $.ajax({
                type: "POST",
                url: url,
                data: {
                    id: id
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
                    swal.fire({
                        title: "Thông Báo",
                        text: xhr.status,
                        icon: "warning",
                        button: false,
                        //timer: 2000,
                    });
                },
            });
        }
    });
}