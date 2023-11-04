var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll",
        },
        columns: [
            { "data": "name", "width": "5%" },
            { "data": "address", "width": "5%" },
            { "data": "city", "width": "5%" },
            { "data": "postalCode", "width": "5%" },
            { "data": "phoneNumber", "width": "5%" },
            {
                "data": "isAuthoirzedCompany",
                "render": function (date) {
                    if (date)
                        return `<input type="checkbox" disabled checked />`;
                    else
                        return `<input type="checkbox" disabled/>`;
                },
                "width": "5%"
            },

            {
                "data": "id",
                "render": function (data) {
                    return `
                                <div class="text-center">
                                    <a href="/Admin/Company/Upsert/${data}" class="btn btn-primary"  style="cursor:pointer"><i class="bi bi-pencil-fill"></i></a>
                                    <a onclick=Delete("/Admin/Company/Delete/${data}") class="btn btn-danger"><i class="bi bi-trash"></i></a>
                                </div>
                                `;
                },
                "width": "5%"
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

