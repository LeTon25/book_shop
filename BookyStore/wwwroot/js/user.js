var dataTable
$(document).ready(function () {
    loadDataTables();
})
function loadDataTables() {
    dataTable = $("#tblData").DataTable({
        "ajax": { url: '/Admin/User/GetAll' },
        "columns": [
            { data: 'name', "width": "20%" },
            { data: 'email', "width": "16%" },
            { data: 'phoneNumber', "width": "16%" },
            { data: 'company.name', "width": "16%" },
            { data: 'role', "width": "16%" },
            {
                data: { id :'id',lockoutEnd : 'lockoutEnd'},
                "render": function (data) {
                    var today = new Date().getTime()
                    var lockOut = (data.lockoutEnd) ? new Date(data.lockoutEnd).getTime() : today;
                    if (lockOut > today) {
                        return `
                            <a onclick=LockUnlock(this,'${data.id}') href=""  class="w-40 btn btn-group btn-danger" role="group">
                                <i class="bi bi-lock"></i>
                                Khóa
                            </a>
                            <a  href="/Admin/User/RoleManagement?userId=${data.id}" class="w-40 btn btn-group btn-warning mt-1" role="group">
                                <i class="bi bi-pencil-square"></i>
                                Chỉnh sửa
                            </a>
                            `

                    } else {
                        return `
                            <a onclick=LockUnlock(this,'${data.id}')  href="" class="w-40 btn btn-group btn-success" role="group">
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
        susscess: function (data) {
            if (data.success)
                dataTable.ajax.reload()
            // alert(data.message) thay bằng custom notification 
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText); // Log thông tin lỗi
            alert("Đã xảy ra lỗi khi gửi yêu cầu: " + error); // Hiển thị thông báo lỗi cho người dùng
        }
    })
}