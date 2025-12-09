

$(document).ready(function () {
    //$('#ddlstock_list').select2();
    //$('#ddlmode_val').select2();
    $('#ddlstock_list').select2({
        dropdownAutoWidth: true
    })

    $('#ddlmode_val').select2({
        dropdownAutoWidth: true
    })

    $('#ddlprd').select2({
        dropdownAutoWidth: true
    })

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = dd + '/' + mm + '/' + yyyy;

    $('#txtdate').val(today);

    var modee = $('#ddlmode').val();
    var sf_code = $('#hdnsf_code').val();
    var div_code = $('#hdndiv_code').val();

    getnames(modee, sf_code, div_code)

    $('#ddlmode').click(function () {
        var mode = $('#ddlmode').val();
        getnames(modee, sf_code, div_code)
    });


    $.ajax({
        type: "POST",
        url: "../MR/webservice/Order.asmx/GetStockist",
        data: JSON.stringify({ "mode": "4", "sf_code": sf_code, "div_code": div_code }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#ddlstock_list').empty();
            $.each(result.d, function (key, value) {
                $('#ddlstock_list').append($("<option></option>").val(value.Stockist_Code).html(value.Stockist_Name));
            });
        },
        error: function () {
            alert("error! try again...");
        }
    });

    var subdiv_code = $('#hdnsub_div').val();

    $.ajax({
        type: "POST",
        url: "../MR/webservice/Order.asmx/Get_prd",
        data: JSON.stringify({ "sf_code": sf_code, "div_code": div_code, "subdiv_code": subdiv_code }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#ddlprd').empty();
            $.each(result.d, function (key, value) {
                // $('#ddlprd').append($("<option></option>").val(value.Product_Code_SlNo).html(value.Product_Detail_Name));

                $('#ddlprd')
                    .append($("<option></option>")
                    .attr("pack", value.Pack)
                    .val(value.Product_Code_SlNo)
                    .text(value.Product_Detail_Name));
            });
        },
        error: function () {
            alert("error! try again...");
        }
    });


    $('#btnGo').click(function () {

        if ($('#ddlstock_list').val() == '-1') {
            alert('Select Stockist Name');
            $('#ddlstock_list').focus();
            return false;
        }

        if ($('#txtdate').val() == '') {
            alert('Select Date');
            $('#txtdate').focus();
            return false;
        }

        if ($('#ddlmode_val').val() == '-1') {

            var mode_name = $("#ddlmode option:selected").text();

            alert('Select ' + mode_name + ' Name');
            $('#ddlmode_val').focus();
            return false;
        }

        $('#div_prd').show();
        $('#tblprd').show();

        $('#fulpnl').show();
        $('#totall').show();

        $('#ddlstock_list').attr("disabled", "disabled");
        $('#ddlmode').attr("disabled", "disabled");
        $('#ddlmode_val').attr("disabled", "disabled");
        $('#txtdate').attr("disabled", "disabled");

        var subdiv_code = $('#hdnsub_div').val();

        var rows = '<tr>';
        rows += '<th></th>';
        rows += '<th>S.No  <div>S.No</div></th>';

        rows += '<th>Product Name <div style="width:30%">Product Name</div></th>';
        rows += '<th>Pack <div>Pack</div></th>';
        rows += '<th>Sale Qty <div>Sale Qty</div></th>';
        rows += '<th>FOC Qty <div>FOC Qty</div></th>';
        rows += '<th>Rate <div>Rate</div></th>';
        rows += '<th>Amount <div>Amount</div></th>';
        rows += '<th>NRV <div>NRV</div></th>';
        rows += '<th>Tot Net Amt <div>Tot Net Amt</div></th>';
        rows += '<th>Delete <div>Delete</div></th>';

        rows += '</tr>';


        var mode = $('#ddlmode').val();
        var sf_code = $('#hdnsf_code').val();
        var div_code = $('#hdndiv_code').val();

        var Stockist_Code = $('#ddlstock_list').val();
        var Order_Date = $('#txtdate').val();
        var DHP_Code = $('#ddlmode_val').val();

        $.ajax({

            type: "POST",
            url: "../MR/webservice/Order.asmx/Get_prd_table",
            data: JSON.stringify({ "sf_code": sf_code, "div_code": div_code, "subdiv_code": subdiv_code, "mode": mode, "Stockist_Code": Stockist_Code, "Order_Date": Order_Date, "DHP_Code": DHP_Code }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (data) {
                $('#tblprd').empty();
                if (data.d.length > 0) {

                    var rows = '<tr>';
                    rows += '<th></th>';
                    rows += '<th>S.No  <div>S.No</div></th>';

                    rows += '<th>Product Name <div style="width:30%">Product Name</div></th>';
                    rows += '<th>Pack <div>Pack</div></th>';
                    rows += '<th>Sale Qty <div>Sale Qty</div></th>';
                    rows += '<th>FOC Qty <div>FOC Qty</div></th>';
                    rows += '<th>Rate <div>Rate</div></th>';
                    rows += '<th>Amount <div>Amount</div></th>';
                    rows += '<th>NRV <div>NRV</div></th>';
                    rows += '<th>Tot Net Amt <div>Tot Net Amt</div></th>';
                    rows += '<th>Delete <div>Delete</div></th>';

                    rows += '</tr>';

                    var Total = 0;
                    var totdr = 0;
                    var row_num = 0;
                    for (var i = 0; i < data.d.length; i++) {
                        row_num += 1;
                        rows += '<tr>';
                        rows += '<td><input type="hidden" id="tblprd_ctl' + row_num + '_hdnprdcode"  class="hdnprdcode" value=' + data.d[i].Product_Code_SlNo + '></td>';
                        rows += '<td class="snoo">' + data.d[i].sno + '</td>';

                        rows += '<td class="ddlprd">' + data.d[i].Product_Detail_Name + '</td>';
                        rows += '<td class="pack">' + data.d[i].Pack + '</td>';
                        // rows += '<td>' + data.d[i].saleqty + '</td>';

                        rows += '<td><input type="text" name="newBox" id="tblprd_ctl' + row_num + '_txtsaleqty" autocomplete="off" style="width:90px;height:25px" maxlength="6" class="txtsaleqty form-control" onkeyup="onkeysale(' + row_num + ');"  value=' + data.d[i].saleqty + '></td>';
                        rows += '<td><input type="text" name="newBox" id="tblprd_ctl' + row_num + '_txtfocqty" autocomplete="off" style="width:90px;height:25px" maxlength="6"  class="txtfocqty form-control" onkeyup="onkeysale(' + row_num + ');"  value=' + data.d[i].Foc_qty + '></td>';
                        rows += '<td><input type="text" name="newBox" id="tblprd_ctl' + row_num + '_txtrateqty" autocomplete="off" style="width:90px;height:25px" maxlength="6"  class="txtrateqty form-control" onkeyup="onkeysale(' + row_num + ');"  value=' + data.d[i].rate + '></td>';
                        rows += '<td><input type="text" name="newBox" id="tblprd_ctl' + row_num + '_txtamtqty" style="width:90px;height:25px"  class="txtamtqty textlabel" ReadOnly="true" onkeyup="onkeysale(' + row_num + ');"  value=' + data.d[i].amt + '></td>';
                        rows += '<td><input type="text" name="newBox" id="tblprd_ctl' + row_num + '_txtnrv" style="width:90px;height:25px"  class="txtnrv textlabel" ReadOnly="true" onkeyup="onkeysale(' + row_num + ');"  value=' + data.d[i].NRV_Value + '></td>';
                        rows += '<td><input type="text" name="newBox" id="tblprd_ctl' + row_num + '_txtnetamt" style="width:90px;height:25px"  class="txtnetamt textlabel" ReadOnly="true" onkeyup="onkeysale(' + row_num + ');"  value=' + data.d[i].TotNet_Amt + '></td>';
                        rows += '<td><input type="button" name="newBox" id="tblprd_ctl' + row_num + '_txtdelete" class="btn btn-warning txtdelete"  value="Delete" onclick="deleteRow(this)"></td>';
                        // rows += '<input type="text" class="rptmar" onkeydown="onkeysale(event);" />';
                        // rows += '<td>' + data.d[i].Foc_qty + '</td>';
                        //rows += '<td>' + data.d[i].rate + '</td>';
                        //rows += '<td>' + data.d[i].amt + '</td>';
                        //  rows += '<input type="text" name="newBox" />'
                        // rows += '<td>' + data.d[i].RemainStock + '</td>';
                        //   rows += '<td><a id="lnkStock_' + i + '" href="#"  title="RemainStock">' + data.d[i].RemainStock + '</a></td>';

                        rows += '</tr>';
                    }



                    $("#tblprd").html(rows);

                    var salee = 0;
                    var focc = 0;
                    var ratee = 0;
                    var amtt = 0;
                    var nrvv = 0;
                    var netamtt = 0;


                    $('#tblprd tr:visible').not(":first").each(function () {


                        salee += Number($(this).find(".txtsaleqty").val());
                        focc += Number($(this).find(".txtfocqty").val());
                        ratee += Number($(this).find(".txtrateqty").val());
                        amtt += Number($(this).find(".txtamtqty").val());
                        nrvv += Number($(this).find(".txtnrv").val());
                        netamtt += Number($(this).find(".txtnetamt").val());


                    });


                    $('#lblsaletot').text('Sale Tot :-' + salee);
                    $('#lblfoctot').text('FOC Tot :-' + focc);
                    $('#lblratetot').text('Rate Tot :-' + ratee);
                    $('#lblamtot').text('Amount Tot :-' + amtt);
                    $('#lblNRV').text('NRV Tot :-' + nrvv);
                    $('#lblnettot').text('Net Amt Tot :-' + netamtt);
                }
                else {
                    var rows = '<tr>';
                    rows += '<th></th>';
                    rows += '<th>S.No  <div>S.No</div></th>';

                    rows += '<th>Product Name <div style="width:30%">Product Name</div></th>';
                    rows += '<th>Pack <div>Pack</div></th>';
                    rows += '<th>Sale Qty <div>Sale Qty</div></th>';
                    rows += '<th>FOC Qty <div>FOC Qty</div></th>';
                    rows += '<th>Rate <div>Rate</div></th>';
                    rows += '<th>Amount <div>Amount</div></th>';
                    rows += '<th>NRV <div>NRV</div></th>';
                    rows += '<th>Tot Net Amt <div>Tot Net Amt</div></th>';
                    rows += '<th>Delete <div>Delete</div></th>';

                    rows += '</tr>';
                    $("#tblprd").html(rows);
                }

            },
            error: function (res) {
            }
        });


        $('#btnsubmit').show();
        return false;



    });



    $('#btnprdGo').click(function () {

        var ddlprd = $("#ddlprd option:selected").text();

        var hdnprdcode = $("#ddlprd option:selected").val();
       


        var pack = $('#ddlprd option:selected').attr('pack');



        var breakOut = false;

        $('#tblprd tr:visible').not(":first").each(function () {
            var prd_name = $(this).find(".ddlprd").html();

            if (ddlprd == prd_name) {
                alert(ddlprd + ' Already Selected');
                $(this).find(".txtsaleqty").focus();
                $(this).addClass('focus');
                breakOut = true;
                return false;
            }
            else {
                $(this).removeClass('focus')

            }

        });



        if (breakOut == false) {

            var table = document.getElementById("tblprd");

            var sno = table.rows.length;

            var markup = "<tr> <td><input type='hidden' id='tblprd_ctl" + sno + "_hdnprdcode'  class='hdnprdcode' value=" + hdnprdcode + "></td>" +
            " <td class='snoo'>" + sno + "</td><td class='ddlprd'>" + ddlprd + "</td><td class='pack'>" + pack + "</td> " +
        " <td><input type='text' id='tblprd_ctl" + sno + "_txtsaleqty' style='width:90px;height:25px' maxlength='6' autocomplete='off' class='txtsaleqty form-control' onkeyup='onkeysale(" + sno + ");' name='newBox'></td>" +
        " <td><input type='text' id='tblprd_ctl" + sno + "_txtfocqty' style='width:90px;height:25px' maxlength='6' autocomplete='off' class='txtfocqty form-control' onkeyup='onkeysale(" + sno + ");' name='newBox'></td>" +
        " <td><input type='text' id='tblprd_ctl" + sno + "_txtrateqty' style='width:90px;height:25px' maxlength='6' autocomplete='off' class='txtrateqty form-control' onkeyup='onkeysale(" + sno + ");' name='newBox'></td>" +
        " <td><input type='text' id='tblprd_ctl" + sno + "_txtamtqty' style='width:90px;height:25px' class='txtamtqty textlabel' ReadOnly='true' onkeyup='onkeysale(" + sno + ");' name='newBox'></td>" +
        " <td><input type='text' id='tblprd_ctl" + sno + "_txtnrv' style='width:90px;height:25px' class='txtnrv textlabel' ReadOnly='true' onkeyup='onkeysale(" + sno + ");' name='newBox'></td>" +
         " <td><input type='text' id='tblprd_ctl" + sno + "_txtnetamt' style='width:90px;height:25px' class='txtnetamt textlabel' ReadOnly='true' onkeyup='onkeysale(" + sno + ");' name='newBox'></td>" +
        " <td><input type='button' id='tblprd_ctl" + sno + "_txtdelete' class='btn btn-warning txtdelete' value='Delete'  name='newBox' onclick='deleteRow(this)'></td>" +
        "</tr>";

            $("#tblprd").append(markup);

            $("#tblprd").find(".txtsaleqty").focus();




            var count = 0;


            $('#tblprd tr:visible').not(":first").each(function () {
                var prd_name = $(this).find(".ddlprd").html();
                count++;
                $(this).find(".snoo").text(count);

            });



        }

        return false;

    });



});

function getnames(mode, sf_code, div_code) {

  


    var mode = $('#ddlmode').val();


    $.ajax({
        type: "POST",
        url: "../MR/webservice/Order.asmx/GetStockist",
        data: JSON.stringify({ "mode": mode,"sf_code":sf_code,"div_code":div_code }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#ddlmode_val').empty();

            $.each(result.d, function (key, value) {

                $('#ddlmode_val').append($("<option></option>").val(value.Stockist_Code).html(value.Stockist_Name));
            });



        },
        error: function () {
            alert("error!...");
        }
    });

}

function onkeysale(x) {

   // var row_no = x.parentNode.parentNode.rowIndex;


  


    var txtsaleqty = document.getElementById('tblprd_ctl' + x + '_txtsaleqty').value;
    var txtfocqty = document.getElementById('tblprd_ctl' + x + '_txtfocqty').value;
    var txtrateqty = document.getElementById('tblprd_ctl' + x + '_txtrateqty').value;

    var txtamtqty = document.getElementById('tblprd_ctl' + x + '_txtamtqty');
    var txtnrv = document.getElementById('tblprd_ctl' + x + '_txtnrv');
    var txtnetamt = document.getElementById('tblprd_ctl' + x + '_txtnetamt');


    var salee = document.getElementById('tblprd_ctl' + x + '_txtsaleqty');
    var foc = document.getElementById('tblprd_ctl' + x + '_txtfocqty');
    var rate = document.getElementById('tblprd_ctl' + x + '_txtrateqty');



    if (isNaN(parseFloat(txtfocqty))) {

        txtfocqty = 0;
    }

    //    var totamt = parseFloat((parseFloat(txtsaleqty) - parseFloat(txtfocqty)) * parseFloat(txtrateqty)).toFixed(2);
    var totamt = parseFloat(parseFloat(txtsaleqty) * parseFloat(txtrateqty)).toFixed(2);



    if (isNaN(totamt)) {
        totamt = 0;
    }


    txtamtqty.value = parseFloat(totamt);

    var nrvamt = parseFloat(parseFloat(totamt) / (parseFloat(txtsaleqty) + parseFloat(txtfocqty))).toFixed(2);


    if (isNaN(nrvamt)) {
        nrvamt = 0;
    }

    txtnrv.value = parseFloat(nrvamt);

    var netamt = parseFloat(parseFloat(nrvamt) * parseFloat(txtsaleqty)).toFixed(2);


    if (isNaN(netamt)) {
        netamt = 0;
    }

    txtnetamt.value = parseFloat(netamt);

    if (parseFloat(txtfocqty) > parseFloat(txtsaleqty)) {
        alert('FOC Qty not less than Sale Qty');
        foc.value = '';
        rate.value = '';
        txtamtqty.value = '';
        txtnrv.value = '';

    }



    // salee.onkeyup = foc.onkeyup = rate.onkeyup = function (e) {



    var val = salee.value;
        if (isNaN(val)) {
            val = val.replace(/[^0-9\.]/g, '');
            if (val.split('.').length > 2)
                val = val.replace(/\.+$/, "");
        }
        salee.value = val;
        // }


        var val2 = foc.value;
        if (isNaN(val2)) {
            val2 = val2.replace(/[^0-9\.]/g, '');
            if (val2.split('.').length > 2)
                val2 = val2.replace(/\.+$/, "");
        }
        foc.value = val2;

        var val3 = rate.value;
        if (isNaN(val3)) {
            val3 = val3.replace(/[^0-9\.]/g, '');
            if (val3.split('.').length > 2)
                val3 = val3.replace(/\.+$/, "");
        }
        rate.value = val3;


        var salee = 0;
        var focc = 0;
        var ratee = 0;
        var amtt = 0;
        var nrvv = 0;
        var netamtt = 0;



        $('#tblprd tr:visible').not(":first").each(function () {

            salee += Number($(this).find(".txtsaleqty").val());
            focc += Number($(this).find(".txtfocqty").val());
            ratee += Number($(this).find(".txtrateqty").val());
            amtt += Number($(this).find(".txtamtqty").val());
            nrvv += Number($(this).find(".txtnrv").val());
            netamtt += Number($(this).find(".txtnetamt").val());


        });


        $('#lblsaletot').text('Sale Tot :-' + salee);
        $('#lblfoctot').text('FOC Tot :-' + focc);
        $('#lblratetot').text('Rate Tot :-' + ratee);
        $('#lblamtot').text('Amount Tot :-' + amtt);
        $('#lblNRV').text('NRV Tot :-' + nrvv);
        $('#lblnettot').text('Net Amt Tot :-' + netamtt);



 

   // alert("Row index is: " + x.parentNode.parentNode.rowIndex);
}

function deleteRow(btn) {

    var row = btn.parentNode.parentNode;
    //row.parentNode.removeChild(row);
    row.style.display = 'none';


    var count = 0;


    $('#tblprd tr:visible').not(":first").each(function () {
        var prd_name = $(this).find(".ddlprd").html();
        count++;
        $(this).find(".snoo").text(count);

    });

//    var table = document.getElementById("tblprd");

//    var sno = table.rows.length;
    //    alert(sno-1);


}

$(document).ready(function () {

    $('#btnclear').click(function () {
        $('#ddlstock_list').removeAttr("disabled");
        $('#ddlmode').removeAttr("disabled");
        $('#ddlmode_val').removeAttr("disabled");
        $('#txtdate').removeAttr("disabled");

        $('#fulpnl').hide();
        $('#totall').hide();

        $('#lblsaletot').text('Sale Tot :-');
        $('#lblfoctot').text('FOC Tot :-');
        $('#lblratetot').text('Rate Tot :-');
        $('#lblamtot').text('Amount Tot :-');
        $('#lblNRV').text('NRV Tot :-');
        $('#lblnettot').text('Net Amt Tot :-');

        return false;

    });

    $('#btnsubmit').click(function () {


        hdncode = [];
        prdname = [];
        packname = [];
        salqty = [];
        focqty = [];
        rate = [];
        amt = [];
        NRV_Value = [];
        TotNet_Amt = [];

        var breakOut = false;


        $('#tblprd tr:visible').not(":first").each(function () {
            var hdnprdcode = $(this).find(".hdnprdcode").val();
            var ddlprd = $(this).find(".ddlprd").text();
            var pack = $(this).find(".pack").text();
            var txtsaleqty = $(this).find(".txtsaleqty").val();
            var txtfocqty = $(this).find(".txtfocqty").val();
            var txtrateqty = $(this).find(".txtrateqty").val();
            var txtamtqty = $(this).find(".txtamtqty").val();

            var txtnrv = $(this).find(".txtnrv").val();
            var txtnetamt = $(this).find(".txtnetamt").val();

       


            if (txtfocqty == '') {
                txtfocqty = '0';
            }

            if (txtsaleqty == '') {
                alert('Enter Sale Qty');
                $(this).find(".txtsaleqty").focus();
                breakOut = true;
                return false;
            }

            if (txtrateqty == '') {
                alert('Enter Rate');
                $(this).find(".txtrateqty").focus();
                breakOut = true;
                return false;
            }

            if (txtsaleqty != '' && txtfocqty != '' && txtrateqty != '' && txtamtqty != '' && txtnrv != '' && txtnetamt != '') {


                hdncode.push(hdnprdcode);
                prdname.push(ddlprd);
                packname.push(pack);
                salqty.push(txtsaleqty);
                focqty.push(txtfocqty);
                rate.push(txtrateqty);
                amt.push(txtamtqty);

                NRV_Value.push(txtnrv);
                TotNet_Amt.push(txtnetamt);
            }

            // $(this).find(".snoo").text(count);

        });

        //        alert(hdncode);
        //        alert(prdname);
        //        alert(packname);
        //        alert(salqty);
        //        alert(focqty);
        //        alert(rate);
        //        alert(amt);

        if (breakOut == false) {

            var tot = hdncode + '~' + prdname + '~' + packname + '~' + salqty + '~' + focqty + '~' + rate + '~' + amt + '~' + NRV_Value + '~' + TotNet_Amt;

            $('#hdntot_values').val(tot);

            var hdntot_values = $('#hdntot_values').val();

            var mode = $('#ddlmode').val();
            var sf_code = $('#hdnsf_code').val();
            var div_code = $('#hdndiv_code').val();

            var Stockist_Code = $('#ddlstock_list').val();
            var Order_Date = $('#txtdate').val();
            var sf_name = $('#lblname').text();

            var Stockist_Name = $("#ddlstock_list option:selected").text();
            var subdiv_code = $('#hdnsub_div').val();

            var DHP_Code = $('#ddlmode_val').val();
            var DHP_Name = $("#ddlmode_val option:selected").text();




            $.ajax({
                type: "POST",
                url: "../MR/webservice/Order.asmx/SaveingOrderbooking",
                data: JSON.stringify({ "mode": mode, "sf_code": sf_code, "div_code": div_code, "hdntot_values": hdntot_values, "Stockist_Code": Stockist_Code, "Order_Date": Order_Date, "sf_name": sf_name, "Stockist_Name": Stockist_Name, "subdiv_code": subdiv_code, "DHP_Code": DHP_Code, "DHP_Name": DHP_Name }),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (result) {
                    alert('Record Saved Successfully');
                    $('#ddlstock_list').removeAttr("disabled");
                    $('#ddlmode').removeAttr("disabled");
                    $('#ddlmode_val').removeAttr("disabled");
                    $('#txtdate').removeAttr("disabled");
                    $('#fulpnl').hide();
                    $('#totall').hide();
                    //  $('#ddlstock_list').empty();



                    //                $.each(result.d, function (key, value) {

                    //                    $('#ddlstock_list').append($("<option></option>").val(value.Stockist_Code).html(value.Stockist_Name));

                    //                });



                },
                error: function () {
                    alert("error! try again...");
                }
            });
        }





        return false;
    });


    (function ($) {

        $.fn.enableCellNavigation = function () {


            var arrow = {
                left: 37,
                up: 38,
                right: 39,
                down: 40
            };

            // select all on focus
            // works for input elements, and will put focus into
            // adjacent input or textarea. once in a textarea,
            // however, it will not attempt to break out because
            // that just seems too messy imho.
            //            this.find('input').keydown(function (e) {
            $(document).on('keyup', '#tblprd input', function (e) {
                //alert('test');


                // shortcut for key other than arrow keys
                if ($.inArray(e.which, [arrow.left, arrow.up, arrow.right, arrow.down]) < 0) {
                    return;
                }

                var input = e.target;
                var td = $(e.target).closest('td');
                var moveTo = null;

                switch (e.which) {

                    case arrow.left:
                        {
                            if (input.selectionStart == 0) {
                                moveTo = td.prev('td:has(input,textarea)');
                            }
                            break;
                        }
                    case arrow.right:
                        {
                            if (input.selectionEnd == input.value.length) {
                                moveTo = td.next('td:has(input,textarea)');
                            }
                            break;
                        }

                    case arrow.up:
                    case arrow.down:
                        {

                            var tr = td.closest('tr');
                            var pos = td[0].cellIndex;

                            var moveToRow = null;
                            if (e.which == arrow.down) {
                                moveToRow = tr.next('tr');
                            } else if (e.which == arrow.up) {
                                moveToRow = tr.prev('tr');
                            }

                            if (moveToRow.length) {
                                moveTo = $(moveToRow[0].cells[pos]);
                            }

                            break;
                        }

                }

                if (moveTo && moveTo.length) {

                    e.preventDefault();

                    moveTo.find('input,textarea').each(function (i, input) {
                        input.focus();
                        input.select();
                    });

                }

            });

        };

    })(jQuery);


    // use the plugin
    $(function () {
        $('#tblprd').enableCellNavigation();
    });


});









