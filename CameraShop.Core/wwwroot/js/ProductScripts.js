var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll",
        },
        columns: [
            { "data": "name", "width": "20%" },
            { "data": "price", "width": "20%" },
            { "data": "category.name", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                                <div class="text-center">
                                    <a href="/Admin/Product/Upsert/${data}" class="btn btn-primary"  style="cursor:pointer"><i class="bi bi-pencil-fill"></i></a>
                                    <a onclick=Delete("/Admin/Product/Delete/${data}") class="btn btn-danger"><i class="bi bi-trash"></i></a>
                                </div>
                                `;
                },
                "width": "40%"
            }]
    });

}
function Delete(url) {
    Swal.fire({
        title: "Are you sure ?",
        text: "You will not be able to restore the data !",
        icon: "warning",
        buttons: true,
        showCancelButton: true,
        dangerMode: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                async: false,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        setTimeout(function () {
                            location.reload();
                        }, 1000);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}

//tinymce.init({
//    selector: 'textare#content',
//    plugins: 'lists',
//    menubar: 'file edit format'
//});

//function validateInput() {
//    if (document.getElementById('uploadBox').value == "") {
//        swal("Error", "Select Image", "error");
//        return false
//    }
//    return true;
//}

