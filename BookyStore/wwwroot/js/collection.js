var dataTable
$(document).ready(function () {
    loadDataTables();
})
function loadDataTables() {
    dataTable = $("#tblCollection").DataTable({
        "ajax": { url: '/Admin/Collection/GetAll' },
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
            { data: 'id', "width": "25%" },
            { data: 'name', "width": "25%" },
            {
                data: 'imageUrl',
                "render": function (data) {
                    return `
                        <img class="datatable-img" src="${data}" alt="Hình ảnh sản phẩm">
                    `;
                },
                "width": "25%"
            },
            {
                data: 'id',
                "render": function (data) {
                    return `
                            <a  href="/Admin/Collection/Upsert?id=${data}" class="w-40 btn btn-group btn-warning" role="group">
                                <i class="bi bi-pencil-square"></i>
                                Chỉnh sửa
                            </a>
                            <a  onClick=Delete('/Admin/Collection/Delete?id=${data}') class="w-40 btn btn-group btn-danger" role="group">
                                <i class="bi bi-trash3"></i>
                                Xóa
                            </a>
                    `
                },
                "width": "25%"
            }

        ]
    })
}

function Delete(url) {
    Swal.fire({
        title: "Bạn có chắc muốn xóa bộ này",
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
initImageUpload(document.getElementsByClassName("image-box")[0])
function initImageUpload(box) {
    if (box) {
        let uploadField = box.querySelector('.image-upload');

        uploadField.addEventListener('change', getFile);

        function previewImage(file) {
            let thumb = box.querySelector('.js--image-preview'),
                reader = new FileReader();
            reader.onload = function () {
                thumb.style.backgroundImage = 'url(' + reader.result + ')';
            }
            reader.readAsDataURL(file);
            thumb.className += ' js--no-default';
        }
        function getFile(e) {
            let file = e.currentTarget.files[0];
            checkType(file);
        }
        function checkType(file) {
            let imageType = /image.*/;
            if (!file.type.match(imageType)) {
                throw 'Datei ist kein Bild';
            } else if (!file) {
                throw 'File không tồn tại';
            } else {
                previewImage(file);
            }
        }
        return 0;
    }
    
}