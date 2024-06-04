var dataTable
$(document).ready(function () {
    loadDataTables();
})
function loadDataTables() {
    dataTable = $("#tblPublisher").DataTable({
        "ajax": { url: '/Admin/Publisher/GetAll' },
        "language": {
            "sProcessing": "Đang xử lý...",
            "sLengthMenu": "Hiển thị _MENU_ dòng",
            "sZeroRecords": "Không tìm thấy dữ liệu",
            "sInfo": "Hiển thị từ _START_ đến _END_ của _TOTAL_ dòng",
            "sInfoEmpty": "Hiển thị từ 0 đến 0 của 0 dòng",
            "sInfoFiltered": "(được lọc từ _MAX_ dòng)",
            "sInfoPostFix": "",
            "sSearch": "Tìm kiếm:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Đầu",
                "sPrevious": "Trước",
                "sNext": "Tiếp",
                "sLast": "Cuối"
            }
        },
        "columns": [
            { data: 'id', "width": "20%" },
            { data: 'name', "width": "20%" },
            { data: 'address', "width": "20%" },
            { data: 'phoneNumber',"width": "20%"},
            {
                data: 'id',
                "render": function (data) {
                    return `
                            <a  href="/Admin/Publisher/Upsert?id=${data}" class="w-40 btn btn-group btn-warning" role="group">
                                <i class="bi bi-pencil-square"></i>
                                Chỉnh sửa
                            </a>
                            <a  onClick=Delete('/Admin/Publisher/Delete?id=${data}') class="w-40 btn btn-group btn-danger" role="group">
                                <i class="bi bi-trash3"></i>
                                Xóa
                            </a>
                    `
                },
                "width": "20%"
            }

        ]
    })
}
function Delete(url) {
    Swal.fire({
        title: "Bạn có chắc muốn xóa NXB này",
        text: "Dữ liệu sẽ không thể hồi phục khi xóa",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Xác nhận xóa",
        cancelButtonText: "Hủy"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message)
                        dataTable.ajax.reload()
                    } else {
                        toastr.error(data.message)
                    }
                },
                error: function (data) {
                    console.log(data);
                }
            })
        }
    });
}
