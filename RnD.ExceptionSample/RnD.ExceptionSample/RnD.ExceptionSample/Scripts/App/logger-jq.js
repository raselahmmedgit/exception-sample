
var logObjData;

$(function () {
    //start DataTable Script


    logObjData = $('#loggerDataTable').dataTable({
        "bJQueryUI": true,
        "bAutoWidth": false,
        "sPaginationType": "full_numbers",
        "bSort": false,
        "oLanguage": {
            "sLengthMenu": "Display _MENU_ records per page",
            "sZeroRecords": "Nothing found - Sorry",
            "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
            "sInfoEmpty": "Showing 0 to 0 of 0 records",
            "sInfoFiltered": "(filtered from _MAX_ total records)"
        },
        "bProcessing": true,
        "bServerSide": true,
        "sAjaxSource": "/Logger/GetLoggers",
        "aoColumns": [{ "sName": "Id",
            "bSearchable": false,
            "bSortable": false
        },
                          { "sName": "Summery" },
                          { "sName": "Details" },
                          { "sName": "FilePath" },
                          { "sName": "Url" },
                          { "sName": "Type" }
            ]
    });

    //end DataTable Script

});