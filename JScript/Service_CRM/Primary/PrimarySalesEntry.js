
var PS_SfCode = "";
var PS_SfType = "";
var PS_DivCode = "";
var PS_CalRate = "";
var arrHead = [];


$(document).ready(function () {
    $(document).ajaxStart(function () {
        $('#loader').css("display", "block")
    })['ajaxStop'](function () {
        $('#loader').css("display", "none")
    });

    PS_SfCode = $('#PS_SfCode').val();
    PS_SfType = $('#PS_SfType').val();
    PS_DivCode = $('#PS_DivCode').val();
    PS_CalRate = $('#PS_CalRate').val();

    $(".invoice").hide();

    if (PS_SfType == '2' || PS_SfType == '3') {
        BindField_Force_DDL();
    } else {
        if (PS_SfType == '1') {
            var usr = $('#ctl13_LblUser').text();
            usr = usr.replace('Welcome ', '');
            $('#ddlFieldForce').empty();
            $('#ddlFieldForce').append('<option value=\'0\'>---Select---</option>');
            $("#ddlFieldForce").append($("<option/>").val(PS_SfCode).text(usr));
            $('#ddlFieldForce').val(PS_SfCode);
            $('#ddlFieldForce').attr('disabled', true);
            $("#ddlFieldForce").selectpicker("refresh");
            $("#ddlFieldForce").trigger("change");
        }
    };

    $(".selectpicker").selectpicker();

    $(".date").datepicker({
        format: "mm-yyyy",
        autoclose: true,
        endDate: '+0d',
        startView: "months",
        minViewMode: "months"
    });

    $(".date1").datepicker({
        format: "yyyy-mm-dd",
        autoclose: true,
        endDate: '+0d',
        todayHighlight: true
    });

    $('#btnEdit').click(function (e) {
        $(document).ajaxStart(function () {
            $('#loader').css("display", "block")
        })['ajaxStop'](function () {
            $('#loader').css("display", "none")
        });
        if (Validation() === true) {
            var sfcode = $('#ddlFieldForce').val();
            $('#btnEdit').prop('disabled', true);
            $('#btnGo').prop('disabled', true);
            BindPrimaryHead();
            $('#SPS_Modal').modal('toggle');
        };
        e.preventDefault();
    });

    $('#btnGo').click(function (e) {
        $(document).ajaxStart(function () {
            $('#loader').css("display", "block")
        })['ajaxStop'](function () {
            $('#loader').css("display", "none")
        });
        if (Validation() === true) {
            var sf_Code, sf_Name, stck_code, stck_name, mnth, yr, Inv_No, Inv_Dt, Ord_No, Ord_Dt = '';

            sf_Code = $('#ddlFieldForce').val();
            sf_Name = $('#ddlFieldForce option:selected').text();
            stck_code = $('#ddlStockiest').val();
            stck_name = $('#ddlStockiest option:selected').text();
            mnth = $('#txtDate').val().substr(0, 2).replace(/^0+/, '');
            yr = $('#txtDate').val().substr($('#txtDate').val().length - 4);

            arrHead = [];
            arrHead.push(sf_Code);
            arrHead.push(stck_code);
            arrHead.push(mnth);
            arrHead.push(yr);
            arrHead.push(Inv_No);
            arrHead.push(Inv_Dt);
            arrHead.push(Ord_No);
            arrHead.push(Ord_Dt);

            $("#txtInvoiceNo").val('');
            $("#txtOrderNo").val('');

            $('#btnEdit').prop('disabled', true);
            $('#btnGo').prop('disabled', true);
            BindPrimary(arrHead.join("^"));
        };
        e.preventDefault();
    });

    $('#btnSave').click(function (e) {
        $(document).ajaxStart(function () {
            $('#loader').css("display", "block")
        })['ajaxStop'](function () {
            $('#loader').css("display", "none")
        });
        if (ValidationSave() === true) {
            var valid = 0;
            $("#grdPrimary").find('input[type=text]').each(function () {
                if ($(this).val() != "") valid += 1;
            });
            if (valid) {
                UpdatePrimary();
            } else {
                $.alert('You must fill in at least one field', 'Alert!');
            }
        };
        e.preventDefault();
    });
});

$('.fa-calendar').click(function (event) {
    event.preventDefault();
    $('#txtDate').click();
});

function BindField_Force_DDL() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/SecSaleWebService.asmx/GetFieldForceName',
        data: '{}',
        dataType: 'json',
        success: function (response) {
            $('#ddlFieldForce').empty();
            $('#ddlFieldForce').append('<option value=\'0\'>---Select---</option>');
            $.each(response.d, function () {
                $("#ddlFieldForce").append($("<option/>").val(this.Field_Sf_Code).text(this.Field_Sf_Name));
            });
            $("#ddlFieldForce").selectpicker("refresh");
        },
        error: function ajaxError(reponse) { }
    })
}

function BindStockist_DDL(sf_Code) {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/SecSaleWebService.asmx/GetStockist',
        data: '{objSfCode:' + JSON['stringify'](sf_Code) + '}',
        dataType: 'json',
        success: function (response) {
            $('#ddlStockiest').empty();
            //$('#ddlStockiest').append('<option value=\'0\'>---Select---</option>');
            $.each(response.d, function () {
                $("#ddlStockiest").append($("<option/>").val(this.Stockist_Code).text(this.Stockist_Name));
            });
            $("#ddlStockiest").selectpicker("refresh");

        },
        error: function ajaxError(response) { }
    })
}

$("#ddlFieldForce").change(function () {
    PS_SfCode = $('#ddlFieldForce').val();
    BindStockist_DDL(PS_SfCode);
});

function BindRCPA(sf_Code) {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/PrimarySalesWebService.asmx/GetRCPA',
        data: '{objPrd_SfCode:' + JSON['stringify'](sf_Code) + '}',
        dataType: 'json',
        success: function (response) {
            var arr = [];
            arr = JSON.parse(response.d);
            // EXTRACT VALUE FOR HTML HEADER. 
            // ('Book ID', 'Book Name', 'Category' and 'Price')
            var col = [];
            for (var i = 0; i < arr.length; i++) {
                for (var key in arr[i]) {
                    if (col.indexOf(key) === -1) {
                        col.push(key);
                    }
                }
            }

            // CREATE DYNAMIC TABLE.
            var table = $("#Table1");
            var thead = $('<thead>');
            // CREATE HTML TABLE HEADER ROW USING THE EXTRACTED HEADERS ABOVE.

            var tr = $('<tr>');                  // TABLE ROW.

            for (var i = 0; i < col.length; i++) {
                var th = document.createElement("th");      // TABLE HEADER.
                th.innerHTML = col[i];
                tr.append(th);
            }

            thead = thead.append(tr);
            table = table.append(thead);


            var tbody = $('<tbody>');
            var trow;

            // ADD JSON DATA TO THE TABLE AS ROWS.
            for (var i = 0; i < arr.length; i++) {
                //trr = document.createElement("tr");
                //tr1 = table.append($('<tr>'));
                trow = document.createElement("tr");

                for (var j = 0; j < col.length; j++) {
                    //var tabCell = tr.append($('<td>'));
                    //tabCell.innerHTML = arr[i][col[j]];
                    //console.log(arr[i][col[j]]);

                    var td = document.createElement("td");
                    td.innerHTML = arr[i][col[j]];
                    trow.append(td);
                }
                tbody = tbody.append(trow);
            }
            table = table.append(tbody);

            $('#Table1').DataTable({
                "dom": 'Bfrtip',
                "destroy": true,
                "paging": false,
                "ordering": false,
                "info": false,
                "searching": false,
                "fixedHeader": true,
                "buttons": [
                    'excel'
                ]
            });
            $('.buttons-excel').hide();
            $('.buttons-excel').click();

            ////Create a HTML Table element.
            //var table = $("#Table1");
            //table[0].border = "1";

            ////Get the count of columns.
            //var columnCount = response.length;

            ////Add the header row.
            //var row = $(table[0].insertRow(-1));
            //for (var i = 0; i < columnCount; i++) {
            //    var headerCell = $("<th />");
            //    headerCell.html(response[0][i]);
            //    row.append(headerCell);
            //}

            ////Add the data rows.
            //for (var i = 1; i < response.length; i++) {
            //    row = $(table[0].insertRow(-1));
            //    for (var j = 0; j < columnCount; j++) {
            //        var cell = $("<td />");
            //        cell.html(response[i][j]);
            //        row.append(cell);
            //    }
            //}
        },
        failure: function (response) { },
        error: function ajaxError(response) { }
    })
}

function BindPrimary(arrHead) {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/PrimarySalesWebService.asmx/GetProductDetail',
        data: '{obj_Head:' + JSON['stringify'](arrHead) + '}',
        dataType: 'json',
        success: function (response) {
            if ($.trim(response.d)) {
                $('#grdPrimary').empty();
                $('#grdPrimary').append("<thead><tr style='background-color:White;'><th scope='col'>#</th><th class='grdHeader' scope='col'>Product Code</th><th scope='col'>Product</th><th scope='col'>Pack</th><th scope='col' class='tdM'>Rate</th><th scope='col' class='tdR'>Rate</th><th scope='col' class='tdD'>Rate</th><th scope='col' class='tdN'>Rate</th><th scope='col' class='tdT'>Rate</th><th scope='col'>Sale Qty</th><th scope='col'>Free Qty</th><th scope='col'>Repl. Qty</th><th scope='col'>Batch No.</th><th scope='col'>Amount</th><th scope='col'>Add</th><th scope='col'>Del</th></tr></thead><tbody>");
                var sno = '';
                for (var i = 0; i < response.d.length; i++) {
                    var sale, free, replace, batch = '';

                    sale = response.d[i].Sale_Qty;
                    free = response.d[i].Free_Qty;
                    replace = response.d[i].Replace_Qty;
                    batch = response.d[i].Batch_No;

                    if (sale == 0) {
                        sale = ''
                    }
                    if (free == 0) {
                        free = ''
                    }
                    if (replace == 0) {
                        replace = ''
                    }
                    if (batch == 0) {
                        batch = ''
                    }

                    var amount = '';

                    if (response.d[0].Cal_Rate == "M") {
                        amount = sale * response.d[i].MRP_Price;
                    }
                    if (response.d[0].Cal_Rate == "R") {
                        amount = sale * response.d[i].Retailor_Price;
                    }
                    if (response.d[0].Cal_Rate == "D") {
                        amount = sale * response.d[i].Distributor_Price;
                    }
                    if (response.d[0].Cal_Rate == "N") {
                        amount = sale * response.d[i].NSR_Price;
                    }
                    if (response.d[0].Cal_Rate == "T") {
                        amount = sale * response.d[i].Target_Price;
                    }

                    if (i == 0) {
                        sno = i + 1;

                        $('#grdPrimary').append("<tr class='head'><td><span id='lblSNo_" + i + "'>" + sno + "</span></td>"
                        + " <td class='grdHeader'><span id='lblProductCode_" + i + "'>" + response.d[i].Product_Detail_Code + "</span></td> "
                        + "<td><span id='lblProductName_" + i + "'>" + response.d[i].Product_Description + "</span></td>"
                        + "<td><span id='lblProduct_Sale_Unit_" + i + "'>" + response.d[i].Product_Sale_Unit + "</span></td>"
                        + "<td class='tdM'><span id='lblMRP_Price_" + i + "'>" + response.d[i].MRP_Price + "</span></td>"
                        + "<td class='tdR'><span id='lblRetailor_Price_" + i + "'>" + response.d[i].Retailor_Price + "</span></td>"
                        + "<td class='tdD'><span id='lblDistributor_Price_" + i + "'>" + response.d[i].Distributor_Price + "</span></td>"
                        + "<td class='tdN'><span id='lblNSR_Price_" + i + "'>" + response.d[i].NSR_Price + "</span></td>"
                        + "<td class='tdT'><span id='lblTarget_Price_" + i + "'>" + response.d[i].Target_Price + "</span></td>"
                        + "<td><input id='txtSaleQty_" + i + "' autocomplete='off' type='text' value='" + sale + "' class='form-control txt' maxlength='5' onkeypress='return isNumberKey(event)' oncopy='return false' onpaste='return false' ondragstart='return false' ondrop='return false' value=''></td>"
                        + "<td><input id='txtFreeQty_" + i + "' autocomplete='off' type='text' value='" + free + "' class='form-control' maxlength='5' onkeypress='return isNumberKey(event)' oncopy='return false' onpaste='return false' ondragstart='return false' ondrop='return false' value=''></td>"
                        + "<td><input id='txtReplaceQty_" + i + "' autocomplete='off' type='text' value='" + replace + "' class='form-control' maxlength='5' onkeypress='return isNumberKey(event)' oncopy='return false' onpaste='return false' ondragstart='return false' ondrop='return false' value=''></td>"
                        + "<td><input id='txtBatchNo_" + i + "' autocomplete='off' type='text' value='" + batch + "' class='form-control' maxlength='200' value=''></td>"
                        + "<td><span id='lblAmount_" + i + "'>" + amount + "</span></td>"
                        + "<td><a class='add' id='btnAdd_" + i + "' title='Add' href='#' data-toggle='tooltip'><i class=\"fa fa-plus\"></i></a></td>"
                        + "<td><a class='delete' style='display:none;' id='btnDelete_" + i + "' title='Delete' href='#' data-toggle='tooltip'><i class=\"fa fa-minus\"></i></a></td>"
                        + "</tr> ");
                    } else if (i > 0) {
                        if (response.d[i].Product_Detail_Code == response.d[i - 1].Product_Detail_Code) {
                            sno = sno;

                            $('#grdPrimary').append("<tr class='child'><td><span id='lblSNo_" + i + "' style='display:none;'>" + sno + "</span></td>"
                            + " <td class='grdHeader'><span id='lblProductCode_" + i + "'>" + response.d[i].Product_Detail_Code + "</span></td> "
                            + "<td><span id='lblProductName_" + i + "' style='display:none;'>" + response.d[i].Product_Description + "</span></td>"
                            + "<td><span id='lblProduct_Sale_Unit_" + i + "' style='display:none;'>" + response.d[i].Product_Sale_Unit + "</span></td>"
                            + "<td class='tdM'><span id='lblMRP_Price_" + i + "' style='display:none;'>" + response.d[i].MRP_Price + "</span></td>"
                            + "<td class='tdR'><span id='lblRetailor_Price_" + i + "' style='display:none;'>" + response.d[i].Retailor_Price + "</span></td>"
                            + "<td class='tdD'><span id='lblDistributor_Price_" + i + "' style='display:none;'>" + response.d[i].Distributor_Price + "</span></td>"
                            + "<td class='tdN'><span id='lblNSR_Price_" + i + "' style='display:none;'>" + response.d[i].NSR_Price + "</span></td>"
                            + "<td class='tdT'><span id='lblTarget_Price_" + i + "' style='display:none;'>" + response.d[i].Target_Price + "</span></td>"
                            + "<td><input id='txtSaleQty_" + i + "' autocomplete='off' type='text' value='" + sale + "' class='form-control txt' maxlength='5' onkeypress='return isNumberKey(event)' oncopy='return false' onpaste='return false' ondragstart='return false' ondrop='return false' value=''></td>"
                            + "<td><input id='txtFreeQty_" + i + "' autocomplete='off' type='text' value='" + free + "' class='form-control' maxlength='5' onkeypress='return isNumberKey(event)' oncopy='return false' onpaste='return false' ondragstart='return false' ondrop='return false' value=''></td>"
                            + "<td><input id='txtReplaceQty_" + i + "' autocomplete='off' type='text' value='" + replace + "' class='form-control' maxlength='5' onkeypress='return isNumberKey(event)' oncopy='return false' onpaste='return false' ondragstart='return false' ondrop='return false' value=''></td>"
                            + "<td><input id='txtBatchNo_" + i + "' autocomplete='off' type='text' value='" + batch + "' class='form-control' maxlength='200' value=''></td>"
                            + "<td><span id='lblAmount_" + i + "'>" + amount + "</span></td>"
                            + "<td><a class='add' style='display:none;' id='btnAdd_" + i + "' title='Add' href='#' data-toggle='tooltip'><i class=\"fa fa-plus\"></i></a></td>"
                            + "<td><a class='delete' id='btnDelete_" + i + "' title='Delete' href='#' data-toggle='tooltip'><i class=\"fa fa-minus\"></i></a></td>"
                            + "</tr> ");

                        } else {
                            sno = sno + 1;

                            $('#grdPrimary').append("<tr class='head'><td><span id='lblSNo_" + i + "'>" + sno + "</span></td>"
                            + " <td class='grdHeader'><span id='lblProductCode_" + i + "'>" + response.d[i].Product_Detail_Code + "</span></td> "
                            + "<td><span id='lblProductName_" + i + "'>" + response.d[i].Product_Description + "</span></td>"
                            + "<td><span id='lblProduct_Sale_Unit_" + i + "'>" + response.d[i].Product_Sale_Unit + "</span></td>"
                            + "<td class='tdM'><span id='lblMRP_Price_" + i + "'>" + response.d[i].MRP_Price + "</span></td>"
                            + "<td class='tdR'><span id='lblRetailor_Price_" + i + "'>" + response.d[i].Retailor_Price + "</span></td>"
                            + "<td class='tdD'><span id='lblDistributor_Price_" + i + "'>" + response.d[i].Distributor_Price + "</span></td>"
                            + "<td class='tdN'><span id='lblNSR_Price_" + i + "'>" + response.d[i].NSR_Price + "</span></td>"
                            + "<td class='tdT'><span id='lblTarget_Price_" + i + "'>" + response.d[i].Target_Price + "</span></td>"
                            + "<td><input id='txtSaleQty_" + i + "' autocomplete='off' type='text' value='" + sale + "' class='form-control txt' maxlength='5' onkeypress='return isNumberKey(event)' oncopy='return false' onpaste='return false' ondragstart='return false' ondrop='return false' value=''></td>"
                            + "<td><input id='txtFreeQty_" + i + "' autocomplete='off' type='text' value='" + free + "' class='form-control' maxlength='5' onkeypress='return isNumberKey(event)' oncopy='return false' onpaste='return false' ondragstart='return false' ondrop='return false' value=''></td>"
                            + "<td><input id='txtReplaceQty_" + i + "' autocomplete='off' type='text' value='" + replace + "' class='form-control' maxlength='5' onkeypress='return isNumberKey(event)' oncopy='return false' onpaste='return false' ondragstart='return false' ondrop='return false' value=''></td>"
                            + "<td><input id='txtBatchNo_" + i + "' autocomplete='off' type='text' value='" + batch + "' class='form-control' maxlength='200' value=''></td>"
                            + "<td><span id='lblAmount_" + i + "'>" + amount + "</span></td>"
                            + "<td><a class='add' id='btnAdd_" + i + "' title='Add' href='#' data-toggle='tooltip'><i class=\"fa fa-plus\"></i></a></td>"
                            + "<td><a class='delete' style='display:none;' id='btnDelete_" + i + "' title='Delete' href='#' data-toggle='tooltip'><i class=\"fa fa-minus\"></i></a></td>"
                            + "</tr> ");
                        }
                    }
                };

                if (response.d[0].Cal_Rate == "M") {
                    $(".tdM").show();
                    $(".tdR").hide();
                    $(".tdD").hide();
                    $(".tdN").hide();
                    $(".tdT").hide();
                }
                if (response.d[0].Cal_Rate == "R") {
                    $(".tdM").hide();
                    $(".tdR").show();
                    $(".tdD").hide();
                    $(".tdN").hide();
                    $(".tdT").hide();
                }
                if (response.d[0].Cal_Rate == "D") {
                    $(".tdM").hide();
                    $(".tdR").hide();
                    $(".tdD").show();
                    $(".tdN").hide();
                    $(".tdT").hide();
                }
                if (response.d[0].Cal_Rate == "N") {
                    $(".tdM").hide();
                    $(".tdR").hide();
                    $(".tdD").hide();
                    $(".tdN").show();
                    $(".tdT").hide();
                }
                if (response.d[0].Cal_Rate == "T") {
                    $(".tdM").hide();
                    $(".tdR").hide();
                    $(".tdD").hide();
                    $(".tdN").hide();
                    $(".tdT").show();
                }

                $('#grdPrimary').append("</tbody>");

                $('#grdPrimary').DataTable({
                    "destroy": true,
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "searching": false,
                    "fixedHeader": true
                });
            }

            $(".invoice").show();
        },
        failure: function (response) { },
        error: function ajaxError(response) { }
    })
}

function BindPrimaryHead() {
    var sf_Code, sf_Name, stck_code, stck_name, mnth, yr = '';

    sf_Code = $('#ddlFieldForce').val();
    sf_Name = $('#ddlFieldForce option:selected').text();
    stck_code = $('#ddlStockiest').val();
    stck_name = $('#ddlStockiest option:selected').text();
    mnth = $('#txtDate').val().substr(0, 2).replace(/^0+/, '');
    yr = $('#txtDate').val().substr($('#txtDate').val().length - 4);

    arrHead = [];
    arrHead.push(sf_Code);
    arrHead.push(stck_code);
    arrHead.push(mnth);
    arrHead.push(yr);

    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/PrimarySalesWebService.asmx/GetPrimaryHead',
        data: '{objPrimary_Head:' + JSON['stringify'](arrHead.join("^")) + '}',
        dataType: 'json',
        success: function (response) {
            $('#mdl_lblSf_Name').text(sf_Name);
            $('#mdl_lblStockist').text(stck_name);
            $('#mdl_MnthYr').text($('#txtDate').val().substr(0, 2) + '-' + yr);

            if ($.trim(response.d)) {
                $('#tblPrimaryData').empty();
                $('#tblPrimaryData').append("<thead><tr style='background-color:White;'><th scope='col'>#</th><th scope='col'>Invoice No.</th><th scope='col'>Invoice Date</th><th scope='col'>Order No.</th><th scope='col'>Order Date</th><th scope='col'>Edit</th></tr></thead><tbody>");
                for (var i = 0; i < response.d.length; i++) {
                    var sno = i + 1;

                    $('#tblPrimaryData').append("<tr><td><span id='lblSNo_" + i + "'>" + sno + "</span></td>"
                        + "<td><span id='lblInv_No_" + i + "'>" + response.d[i].Inv_No + "</span></td>"
                        + "<td><span id='lblInv_Dt_" + i + "'>" + response.d[i].Inv_Dt.substr(0, 10).split("/").reverse().join("-") + "</span></td>"
                        + "<td><span id='lblOrd_No_" + i + "'>" + response.d[i].Ord_No + "</span></td>"
                        + "<td><span id='lblOrd_Dt_" + i + "'>" + response.d[i].Ord_Dt.substr(0, 10).split("/").reverse().join("-") + "</span></td>"
                        + "<td><a class='EditInvoice' id='btnEditInvoice_" + i + "' title='Edit' href='#' data-toggle='tooltip'><i class=\"fa fa-pencil\"></i></a></td>"
                        + "</tr> ");
                };

                $('#tblPrimaryData').DataTable({
                    "destroy": true,
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "searching": false
                });
            }
            else {
                $('#mdl_lblSf_Name').text(sf_Name);
                $('#mdl_lblStockist').text(stck_name);
                $('#mdl_MnthYr').text($('#txtDate').val().substr(0, 2) + '-' + yr);

                $('#tblPrimaryData').empty();
                $('#tblPrimaryData').append("<tbody><tr><td> No Records Found </td></tr></tbody>");

                $('#tblPrimaryData').DataTable({
                    "destroy": true,
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "searching": false
                });
            }
        },
        error: function ajaxError(response) { }
    })
}

$("#grdPrimary").on("keyup", ".txt", function () {
    var row = $(this).parents("tr:first");
    var amount = 0;

    var spanM = row.find('td:eq(4) span');
    var spanR = row.find('td:eq(5) span');
    var spanD = row.find('td:eq(6) span');
    var spanN = row.find('td:eq(7) span');
    var spanT = row.find('td:eq(8) span');

    var txtSale = row.find('input:eq(0)');

    var span = row.find('span:eq(9)');

    if (PS_CalRate == 'M') {
        amount = txtSale.val() * spanM.text().trim();
    }
    if (PS_CalRate == 'R') {
        amount = txtSale.val() * spanR.text().trim();
    }
    if (PS_CalRate == 'D') {
        amount = txtSale.val() * spanD.text().trim();
    }
    if (PS_CalRate == 'N') {
        amount = txtSale.val() * spanN.text().trim();
    }
    if (PS_CalRate == 'T') {
        amount = txtSale.val() * spanT.text().trim();
    }

    console.log(amount);
    span.text(amount.toFixed(2));
});

$(document).on('click', '.add', function (e) {
    e.preventDefault();

    $(document).ajaxStart(function () {
        $('#loader').css("display", "block")
    })['ajaxStop'](function () {
        $('#loader').css("display", "none")
    });

    var row = $(this).closest("tr");

    var $nxttr = $(this).closest('tr').next('tr');

    var $tr = $(this).closest('tr');
    var $clone = $tr.clone();
    $clone.removeClass('head');
    $clone.addClass('child');

    $clone.find('span:eq(0)').css("display", "none");
    $clone.find('span:eq(2)').css("display", "none");
    $clone.find('span:eq(3)').css("display", "none");
    $clone.find('span:eq(4)').css("display", "none");
    $clone.find('span:eq(5)').css("display", "none");
    $clone.find('span:eq(6)').css("display", "none");
    $clone.find('span:eq(7)').css("display", "none");
    $clone.find('span:eq(8)').css("display", "none");
    $clone.find('input:eq(0)').val('');
    $clone.find('input:eq(1)').val('');
    $clone.find('input:eq(2)').val('');
    $clone.find('input:eq(3)').val('');
    $clone.find('span:eq(9)').text('');
    $clone.find('a:eq(0)').css("display", "none");
    $clone.find('a:eq(1)').css("display", "block");

    if ($nxttr.hasClass("child")) {
        var rowIndex = $tr.next('.child');
        $tr.nextAll('tr.head').eq(0).before($clone);
    }
    else {
        $tr.after($clone);
    }
});

$(document).on('click', '.delete', function (e) {
    e.preventDefault();

    $(document).ajaxStart(function () {
        $('#loader').css("display", "block")
    })['ajaxStop'](function () {
        $('#loader').css("display", "none")
    });

    var row = $(this).closest("tr");
    row.remove();
});

$(document).on('click', '.EditInvoice', function (e) {
    e.preventDefault();

    $(document).ajaxStart(function () {
        $('#loader').css("display", "block")
    })['ajaxStop'](function () {
        $('#loader').css("display", "none")
    });

    var sf_Code, sf_Name, stck_code, stck_name, mnth, yr, Inv_No, Inv_Dt, Ord_No, Ord_Dt = '';

    var row = $(this).closest("tr");
    Inv_No = row.find('span:eq(1)').text();
    Inv_Dt = row.find('span:eq(2)').text();
    Ord_No = row.find('span:eq(3)').text();
    Ord_Dt = row.find('span:eq(4)').text();

    sf_Code = $('#ddlFieldForce').val();
    sf_Name = $('#ddlFieldForce option:selected').text();
    stck_code = $('#ddlStockiest').val();
    stck_name = $('#ddlStockiest option:selected').text();
    mnth = $('#txtDate').val().substr(0, 2).replace(/^0+/, '');
    yr = $('#txtDate').val().substr($('#txtDate').val().length - 4);

    $('#txtInvoiceNo').val(Inv_No);
    $('#txtInvoiceDate').val(Inv_Dt);
    $('#txtOrderNo').val(Ord_No);
    $('#txtOrderDate').val(Ord_Dt);

    arrHead = [];
    arrHead.push(sf_Code);
    arrHead.push(stck_code);
    arrHead.push(mnth);
    arrHead.push(yr);
    arrHead.push(Inv_No);
    arrHead.push(Inv_Dt);
    arrHead.push(Ord_No);
    arrHead.push(Ord_Dt);

    $(".invoice").show();

    $('#btnEdit').prop('disabled', true);
    BindPrimary(arrHead.join("^"));

    $('#SPS_Modal').modal('toggle');
});


function UpdatePrimary() {
    var rows = [];
    rows = $("#grdPrimary > tbody > tr").map(function () {
        var row = $(this);
        return [$(this).map(function () {
            var pCode = row.find('span:eq(1)').text();
            var spanM = row.find('span:eq(4)').text();
            var spanR = row.find('span:eq(5)').text();
            var spanD = row.find('span:eq(6)').text();
            var spanN = row.find('span:eq(7)').text();
            var spanT = row.find('span:eq(8)').text();
            var sale = row.find('input:eq(0)').val();
            var free = row.find('input:eq(1)').val();
            var repl = row.find('input:eq(2)').val();
            var batch = row.find('input:eq(3)').val();

            return [{ 'Product_Detail_Code': pCode, 'MRP_Price': spanM, 'Retailer_Price': spanR, 'Distributor_price': spanD, 'NSR_Price': spanN, 'Target_Price': spanT, 'Sale_Qty': sale, 'Free_Qty': free, 'Replace_Qty': repl, 'Batch_No': batch }];

        }).get()];
    }).get();

    var xml = '';
    xml += "<xml>";
    for (var i = 0; i < rows.length; i++) {
        xml += "<PSUpload>";
        $.each(rows[i], function (key, field) {
            var sale = '';
            var free = '';
            var repl = '';

            sale = isNaN(parseFloat(rows[i][0].Sale_Qty).toFixed(2)) ? 0 : parseFloat(rows[i][0].Sale_Qty).toFixed(2);
            free = isNaN(parseFloat(rows[i][0].Free_Qty).toFixed(2)) ? 0 : parseFloat(rows[i][0].Free_Qty).toFixed(2);
            repl = isNaN(parseFloat(rows[i][0].Replace_Qty).toFixed(2)) ? 0 : parseFloat(rows[i][0].Replace_Qty).toFixed(2);

            xml += "<Product_Detail_Code>" + rows[i][0].Product_Detail_Code + "</Product_Detail_Code>";
            xml += "<MRP_Price>" + rows[i][0].MRP_Price + "</MRP_Price>";
            xml += "<Retailer_Price>" + rows[i][0].Retailer_Price + "</Retailer_Price>";
            xml += "<Distributor_price>" + rows[i][0].Distributor_price + "</Distributor_price>";
            xml += "<NSR_Price>" + rows[i][0].NSR_Price + "</NSR_Price>";
            xml += "<Target_Price>" + rows[i][0].Target_Price + "</Target_Price>";
            xml += "<Sale_Qty>" + sale + "</Sale_Qty>";
            xml += "<Free_Qty>" + free + "</Free_Qty>";
            xml += "<Replace_Qty>" + repl + "</Replace_Qty>";
            xml += "<Batch_No>" + rows[i][0].Batch_No + "</Batch_No>";
        });
        xml += "</PSUpload>";
    }
    xml += "</xml>";

    var sf_Code, stck_code, mnth, yr, in_no, in_date, ord_no, ord_date = '';

    sf_Code = $('#ddlFieldForce').val();
    stck_code = $('#ddlStockiest').val();
    mnth = $('#txtDate').val().substr(0, 2).replace(/^0+/, '');
    yr = $('#txtDate').val().substr($('#txtDate').val().length - 4);
    in_no = $('#txtInvoiceNo').val();
    in_date = $('#txtInvoiceDate').val();
    ord_no = $('#txtOrderNo').val();
    ord_date = $('#txtOrderDate').val();

    arrHead = [];
    arrHead.push(sf_Code);
    arrHead.push(stck_code);
    arrHead.push(mnth);
    arrHead.push(yr);
    arrHead.push(in_no);
    arrHead.push(in_date);
    arrHead.push(ord_no);
    arrHead.push(ord_date);

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../webservice/PrimarySalesWebService.asmx/Update",
        data: "{objPS_Head:" + JSON.stringify(arrHead.join("^")) + ", objPS_Detail:" + JSON.stringify(xml) + "}",
        dataType: "json",
        success: function (result) {
            $.confirm({
                title: 'Alert!',
                content: 'Update Success!',
                buttons: {
                    ok: function () {
                        $("#btnClear").click();
                    }
                }
            });
        }
    });
}

// ----------------------------Validation----------------------------
function Validation() {
    if ($("#ddlFieldForce").next().attr('title') == "Nothing selected" || $("#ddlFieldForce").val() == 0) {
        $.alert('Please Select Field-Force!', 'Alert!');
        return false;
    }
    if ($("#ddlStockiest").next().attr('title') == "Nothing selected" || $("#ddlStockiest").val() == -1) {
        $.alert('Please Select Stockist!', 'Alert!');
        return false;
    }
    if ($("#txtDate").val() == "") {
        $.alert('Please Select Month & Date!', 'Alert!');
        return false;
    }
    else {
        return true;
    }
}

function ValidationSave() {
    if ($("#txtInvoiceNo").val().trim() == "") {
        $.alert('Please enter Invoice No!', 'Alert!');
        return false;
    }
    if ($("#txtInvoiceDate").val().trim() == "") {
        $.alert('Please enter Invoice Date!', 'Alert!');
        return false;
    }
    if ($("#txtOrderNo").val().trim() == "") {
        $.alert('Please enter Order No!', 'Alert!');
        return false;
    }
    if ($("#txtOrderDate").val().trim() == "") {
        $.alert('Please enter Order Date!', 'Alert!');
        return false;
    }
    else {
        return true;
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
    && (charCode < 48 || charCode > 57))
        return false;
    return true;
}


function formatDate(date) {
    var d = new Date(Date.parse(date)),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}