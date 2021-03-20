function initEsuDatatables(tableElement, pageLength = 20, sort = "asc", indexSort = 0) {
    return tableElement.DataTable({
        "paging": paging,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "order": [indexSort, sort],
        "info": false,
        "autoWidth": false,
        "responsive": true,
        "autoWidth": false,
        "pageLength": pageLength,
        "stateSave": true,
        "columnDefs": [{
            "targets": 'nosort',
            "orderable": false
        }],
        "language": {
            search: "Tìm kiếm:",
            paginate: {
                "previous": "Trang Trước",
                "next": "Trang Sau",
                "first": "Đầu Trang",
                "last": "Cuối Trang"
            }
        },
    });
}

function initEsuDatatables(tableElement, pageLength = 20, sort = "asc", indexSort = 0, paging = true, search = true) {
    return tableElement.DataTable({
        "paging": paging,
        "lengthChange": false,
        "searching": search,
        "ordering": true,
        "order": [indexSort, sort],
        "info": false,
        "autoWidth": false,
        "responsive": true,
        "autoWidth": false,
        "pageLength": pageLength,
        "stateSave": true,
        "columnDefs": [{
            "targets": 'nosort',
            "orderable": false
        }],
        "language": {
            search: "Tìm kiếm:",
            paginate: {
                "previous": "Trang Trước",
                "next": "Trang Sau",
                "first": "Đầu Trang",
                "last": "Cuối Trang"
            }
        },
    });
}