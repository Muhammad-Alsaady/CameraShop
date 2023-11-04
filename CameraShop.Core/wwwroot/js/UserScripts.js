var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
   dataTable = $('#tblData').dataTable({
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admin/User/GetAll",
            "type": "POST",
            "dateType": "Json"
        },
        columns: [
            { "data": "name", "title": "Name"},
            { "data": "email", "title": "Email" },
            { "data": "phone", "title": "Phone" },
            { "data": "company", "title": "Company"},
            {
                "data": "roles",
                "title": "Roles",
                "render": function (data) {
                    if (data.length > 0) {
                        return data.join(", ");
                    } else {
                        return "-";
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                                <div class="text-center">
                                    <a href="/Admin/User/Upsert/${data}" class="btn btn-primary"  style="cursor:pointer"><i class="bi bi-pencil-fill"></i></a>
                                    <a onclick=Delete("/Admin/User/Delete/${data}") class="btn btn-danger"><i class="bi bi-trash"></i></a>
                                </div>
                                `;
                },
           }
        ]
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