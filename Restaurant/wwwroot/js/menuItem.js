$(document).ready(function () {
    $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/menuItem", 
            "type": "GET",
            "datatype": "json"
        },
        "column": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "25%" },
        ],
        "width": "100%"
    });
});