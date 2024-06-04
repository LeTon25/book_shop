var dataTable
$(document).ready(function () {
    loadDataTables();
})
function loadDataTables() {
    dataTable = $("#tblData").DataTable({
        "ajax": { url: '/Admin/User/GetAll' },
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
            { data: 'name', "width": "20%" },
            { data: 'email', "width": "32%" },
            { data: 'phoneNumber', "width": "16%" },
            { data: 'role', "width": "16%" },
            {
                data: { id :'id',lockoutEnd : 'lockoutEnd'},
                "render": function (data) {
                    var today = new Date().getTime()
                    var lockOut = (data.lockoutEnd) ? new Date(data.lockoutEnd).getTime() : today;
                    if (lockOut > today) {
                        return `
                            <a onclick=LockUnlock('${data.id}') class="w-40 btn btn-group btn-danger" role="group">
                                <i class="bi bi-lock"></i>
                                Khóa
                            </a>
                            <a  href="/Admin/User/RoleManagement?userId=${data.id}" class="w-40 btn btn-group btn-warning mt-1" role="group">
                                <i class="bi bi-pencil-square"></i>
                                Chỉnh sửa
                            </a>
                            `;
                    } else {
                        return `
                            <a onclick=LockUnlock('${data.id}') class="w-40 btn btn-group btn-success" role="group">
                                <i class="bi bi-unlock"></i>
                                Mở Khóa
                            </a>
                            <a  href="/Admin/User/RoleManagement?userId=${data.id}" class="w-40 btn btn-group btn-warning mt-1" role="group">
                                <i class="bi bi-pencil-square"></i>
                                Chỉnh sửa
                            </a>
                            `
                    }
                },
                "width": "16%"  
            }

        ]
    })
}
function LockUnlock(id) {
    $.ajax({
        url: "/Admin/User/LockUnlock",
        data: JSON.stringify(id),
        contentType : 'application/json',
        type: "POST",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message)
                loadDataTables()
            } else {
                toastr.error(data.message)
            }
        },
        error: function (xhr, status, error) {
            alert("Đã xảy ra lỗi khi gửi yêu cầu: " + error); 
        },
    })
}