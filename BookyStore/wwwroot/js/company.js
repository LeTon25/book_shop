var dataTable
$(document).ready(function () {
    loadDataTables();
})
function loadDataTables() {
    dataTable = $("#tblData").DataTable({
        "ajax": { url: '/Admin/Company/GetAll' },
        "columns": [
            { data: 'name', "width": "20%" },
            { data: 'streetAddress', "width": "16%" },
            { data: 'state', "width": "16%" },
            { data: 'city', "width": "16%" },
            { data: 'phoneNumber', "width": "16%" },
            {
                data: 'id',
                "render": function (data) {
                    return `
                            <a  href="/Admin/Company/Upsert?id=${data}" class="w-40 btn btn-group btn-warning" role="group">
                                <i class="bi bi-pencil-square"></i>
                                Chỉnh sửa
                            </a>
                            <a  onClick=Delete('/Admin/Company/Delete/id=${data}') class="w-40 btn btn-group btn-danger" role="group">
                                <i class="bi bi-trash3"></i>
                                Xóa
                            </a>
                    `
                },
                "width": "16%"
            }

        ]
    })
}

function Delete(url) {
    Swal.fire({
        title: "Bạn có chắc muốn xóa công ty này",
        text: "Dữ liệu sẽ không thể hồi phục khi xóa",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Xác nhận xóa",
        cancelButtonText: "Xóa"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                susscess: function (data) {
                    dataTable.ajax.reload()
                    // alert(data.message) thay bằng custom notification 
                }
            })
        }
    });
}