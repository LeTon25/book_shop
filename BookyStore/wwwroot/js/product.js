var dataTable
$(document).ready(function () {
    loadDataTables();
})
function loadDataTables() {
    $.ajax({
        url: '/Admin/Product/GetAll',
        type: "GET",
        susscess: function (data) {
            console.log(data)
        },
        error: function (data) {
            console.log(data)
        }
    })
    dataTable = $("#tblData").DataTable({
        "ajax": { url: '/Admin/Product/GetAll' },
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'isbn', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            { data: 'author', "width": "20%" },
            { data: 'price', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `
                            <a  href="/Admin/Product/Upsert?id=${data}" class="w-40 btn btn-group btn-warning" role="group">
                                <i class="bi bi-pencil-square"></i>
                                Chỉnh sửa
                            </a>
                            <a  onClick=Delete('/Admin/Product/Delete/id=${data}') class="w-40 btn btn-group btn-danger" role="group">
                                <i class="bi bi-trash3"></i>
                                Xóa
                            </a>
                    `
                },
                "width" :"25%"
            }

        ]
    })
}

function Delete(url) {
    Swal.fire({
        title: "Bạn có chắc muốn xóa sách này",
        text: "Dữ liệu sẽ không thể hồi phục khi xóa",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Xác nhận xóa",
        cancelButtonText:"Xóa"
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