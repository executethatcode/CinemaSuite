$(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#tblData').DataTable({
        "ajax": {
            url: '/admin/movie/getall',
            type: 'GET',
            dataSrc: 'data'
        },
        "columns": [
            { data: 'title', "width": "20%" },
            {
                data: 'releaseDate',
                "width": "15%",
                render: function (data) {
                    return new Date(data).toLocaleDateString('en-GB');
                }
            },
            { data: 'director', "width": "20%" },
            { data: 'category.name', "width": "10%" },
            { data: 'blurayPrice', "width": "15%", render: function (data) { return '₹ ' + data.toFixed(2); } },
            {
                data: 'id',
                render: function (data, type, row) {
                    return `
                        <a class="btn btn-primary btn-sm me-2" href="/Admin/Movie/Upsert/${data}">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a class="btn btn-danger btn-sm" href="#" onclick="DeleteMovie('/Admin/Movie/Delete/${data}')">
                            <i class="bi bi-trash"></i> Delete
                        </a>
                    `;
                },
                "width": "20%"
            }
        ],
        "autoWidth": false
    });
}

function DeleteMovie(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    toastr.success(data.message);
                    $('#tblData').DataTable().ajax.reload(); // Reload the DataTable
                },
                error: function (xhr, status, error) {
                    toastr.error('Failed to delete movie.');
                }
            });
        }
    });
}
