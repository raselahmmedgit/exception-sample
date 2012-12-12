//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add Product Success Function
function AddProductSuccess() {
    alert("AddProductSuccess");
    if ($("#addProductMess").html() == "True") {

        //now we can close the dialog
        $('#addProductDialog').dialog('close');
        //twitter type notification
        $('#commonMessage').html("Product Added.");
        $('#commonMessage').delay(400).slideDown(400).delay(3000).slideUp(400);

        catObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#addProductMess").show();
    }
}

// Edit Product Success Function
function EditProductSuccess() {
    if ($("#editProductMess").html() == "True") {

        //now we can close the dialog
        $('#editProductDialog').dialog('close');
        //twitter type notification
        $('#commonMessage').html("Product Updated.");
        $('#commonMessage').delay(400).slideDown(400).delay(3000).slideUp(400);

        catObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#editProductMess").show();
    }
}

// Delete Product Success Function
function DeleteProductSuccess() {
    if ($("#deleteProductMess").html() == "True") {

        //now we can close the dialog
        $('#deleteProductDialog').dialog('close');
        //twitter type notification
        $('#commonMessage').html("Task deleted");
        $('#commonMessage').delay(400).slideDown(400).delay(3000).slideUp(400);

        catObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#deleteProductMess").show();
    }
}
//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

//-----------------------------------------------------
//start Add, Edit, Delete - Success Common Funtion
function AjaxSuccess(updateTargetId, dailogId, commonMessageId, commonMessage) {

    var _updateTargetId = "#" + updateTargetId;
    var _dailogID = "#" + dailogId;
    var _commonMessageId = "#" + commonMessageId;
    var _commonMessage = commonMessage;

    if ($(_updateTargetId).html() == "True") {

        //now we can close the dialog
        $(_dailogID).dialog('close');
        //twitter type notification
        $(_commonMessageId).html(_commonMessage);
        $(_commonMessageId).delay(400).slideDown(400).delay(3000).slideUp(400);

        catObjData.fnDraw();

    }
    else {
        //show message in popup
        $(_updateTargetId).show();
    }
}
//end Add, Edit, Delete - Success Common Funtion
//-----------------------------------------------------

var proObjData;

$(function () {
    //start DataTable Script

    //for display more collapse data from category
    $('#productDataTable tbody td img.proCatg').live('click', function () {

        if ($(this).attr('class').match('proCatg')) {
            var nTr = this.parentNode.parentNode;
            if (this.src.match('details_close')) {
                this.src = "/Content/Images/App/details_open.png";
                proObjData.fnClose(nTr);
            }
            else {
                this.src = "/Content/Images/App/details_close.png";
                var proid = $(this).attr("rel");
                $.get("/Product/GetCategory?proId=" + proid, function (pro) {
                    proObjData.fnOpen(nTr, pro, 'details');
                });
            }
        }

    });

    proObjData = $('#productDataTable').dataTable({
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
        "sAjaxSource": "/Product/GetProducts",
        "aoColumns": [{ "sName": "ID",
            "bSearchable": false,
            "bSortable": false,
            "fnRender": function (oObj) {
                return '<img class="proCatg img-expand-collapse" src="/Content/Images/App/details_open.png" title="Category" alt="expand/collapse" rel="' +
                                oObj.aData[0] + '"/>' +
                                '<a class="lnkDetailsProduct" href=\"/Product/Details/' +
                                oObj.aData[0] + '\" ><img src="/Content/Images/App/detail.png" title="Details" class="tb-space" alt="Detail"></a>' +
                                '<a class="lnkEditProduct" href=\"/Product/Edit/' +
                                oObj.aData[0] + '\" ><img src="/Content/Images/App/edit.png" title="Edit" class="tb-space" alt="Edit"></a>' +
                                '<a class="lnkDeleteProduct" href=\"/Product/Delete/' +
                                oObj.aData[0] + '\" ><img src="/Content/Images/App/delete.png" title="Delete" class="tb-space" alt="Delete"></a>' +
                                '<a class="lnkProDelete" href=\"/Product/Delete/' +
                                oObj.aData[0] + '\" ><img src="/Content/Images/App/delete.png" title="Delete" class="tb-space" alt="Delete"></a>';

            }

        },
                          { "sName": "PRODUCTNAME" },
                          { "sName": "PRICE" },
                          { "sName": "CATEGORYID" },
                          { "sName": "CATEGORYNAME" }
            ]
    });

    //end DataTable Script

    //product create
    $('#proCreate').live('click', function () {

        var product = {};

        var name = $("#Name").val();
        var price = $("#Price").val();
        var categoryId = $("#CategoryId").val();

        product.Name = name;
        product.Price = price;
        product.CategoryId = categoryId;

        var passData = JSON.stringify({ product: product });

        $.ajax({
            url: '/Product/Create',
            type: 'POST',
            dataType: 'json',
            data: passData,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                //JQDialogAlert mass, status
                JQDialogAlert(data.msg, data.status);
            },
            error: function (objAjaxRequest, strError) {
                var resptext = objAjaxRequest.responseText;
            }


        });

        return false;
    });

    //product edit
    $('#proEdit').live('click', function () {

        var product = {};

        var id = $("#ProductId").val();
        var name = $("#Name").val();
        var price = $("#Price").val();
        var categoryId = $("#CategoryId").val();

        product.ProductId = id;
        product.Name = name;
        product.Price = price;
        product.CategoryId = categoryId;

        var passData = JSON.stringify({ product: product });

        $.ajax({
            url: '/Product/Edit',
            type: 'POST',
            dataType: 'json',
            data: passData,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                //JQDialogAlert mass, status
                JQDialogAlert(data.msg, data.status);
            },
            error: function (objAjaxRequest, strError) {
                var resptext = objAjaxRequest.responseText;
            }


        });

        return false;
    });

    //product delete
    $('#proDelete').live('click', function () {

        var productId = $("#ProductId").val();

        var passData = JSON.stringify({ id: productId });

        $.ajax({
            url: '/Product/Delete',
            type: 'POST',
            dataType: 'json',
            data: passData,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                //JQDialogAlert mass, status
                JQDialogAlert(data.msg, data.status);
            },
            error: function (objAjaxRequest, strError) {
                var resptext = objAjaxRequest.responseText;
            }


        });

        return false;
    });

    //product delete
    $('#productDataTable tbody td a.lnkProDelete').live('click', function () {

        var linkObj = $(this);
        var viewUrl = linkObj.attr('href');

        var message = "Do you want delete this product?";
        var status = "warning";

        JQDialogConfirm(message, status, function () {

            $.ajax({
                url: viewUrl,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    //JQDialogAlert mass, status
                    JQDialogAlert(data.msg, data.status);
                    proObjData.fnDraw();
                },
                error: function (objAjaxRequest, strError) {
                    var resptext = objAjaxRequest.responseText;
                }
            });

        }, function () {


        });

        return false;
    });

});