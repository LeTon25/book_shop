var dataTable
$(document).ready(function () {
    var url = document.location.search;
    if (url.includes()) {
        loadDataTables("inprocess")
    } else {
        if (url.includes()) {
            loadDataTables("shipped")
        } else {
            if (url.includes()) {
                loadDataTables("paymentapproved")
            } else {
                loadDataTables("all")
            }
        }
    }
})
function loadDataTables(status) {
    $.ajax({
        url: '/Admin/Product/GetAll?status=' + status,
        type: "GET",
        susscess: function (data) {
            console.log(data)
        },
        error: function (data) {
            console.log(data)
        }
    })
    dataTable = $("#tblData").DataTable({
        "ajax": { url: '/Admin/Order/GetAll' },
        "columns": [
            { data: 'id', "width": "10%" },
            { data: 'name', "width": "25%" },
            { data: 'phonenumber', "width": "10%" },
            { data: 'applicationuser.email', "width": "20%" },
            { data: 'orderstatus', "width": "10%" },
            { data: 'orderstotal', "width": "10%" },

            {
                data: 'id',
                "render": function (data) {
                    return `
                            <a  href="/Admin/Order/ViewDetail?id=${data}" class="w-40 btn btn-group btn-warning" role="group">
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
