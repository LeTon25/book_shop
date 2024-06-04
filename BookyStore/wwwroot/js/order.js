var dataTable
(function () {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "2000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
})();
$(document).ready(function () {
    var url = document.location.search;
    console.log(url)
    if (url.includes("inprocess")) {
        loadDataTables("inprocess")
    } else {
        if (url.includes("shipped")) {
            loadDataTables("shipped")
        } else {
            if (url.includes("paymentapproved")) {
                loadDataTables("paymentapproved")
            } else {
                loadDataTables("all")
            }
        }
    }
})
function loadDataTables(status) {
    console.log(status);
    dataTable = $("#tblData").DataTable({
        "ajax": {
            url: '/Admin/Order/GetAll',
            data: function (d) {
                d.status = status
            }
        },

        "columns": [
            { data: 'id', "width": "10%" },
            { data: 'name', "width": "25%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'applicationUser.email', "width": "20%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `
                            <a  href="/Admin/Order/ViewDetail?orderID=${data}" class="w-40 btn btn-group btn-warning" role="group">
                                <i class="bi bi-pencil-square"></i>
                                Xem chi tiết
                            </a>
                    `
                },
                "width": "15%"
            }

        ]
    })
}
