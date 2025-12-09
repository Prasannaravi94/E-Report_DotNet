<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_CRM_Print.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_Print_1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../JScript/CRM_Css/ServiceCRM_EditCSS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <link href="../../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet"
        type="text/css" />
    <style type="text/css">
        h3, h4
        {
            color: Black;
            font-weight: bold;
            font-family: Verdana;
            font-size: 11px;
        }
        
        #tblDoc, #tblCurrentSupport, #tblPotentialProduct, #tblConfirm
        {
            font-size: 10px;
            font-family: Verdana;
        }
        #tbl > thead > tr > th, #tbl > tbody > tr > th, #tbl > tfoot > tr > th, #tbl > thead > tr > td, #tbl > tbody > tr > td, .table > tfoot > tr > td
        {
            font-size: 10px;
        }
        .blank_row
        {
            height: 30px !important; /* overwrites any other rules */
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            var Doc_code = GetParameterValues('ListedDrCode');
            var SlNoDr = GetParameterValues('SlNoDr');

            ProductTable();
            GetChemist(Doc_code);
            GetStockist();

            var ArrDrService = {};

            ArrDrService.Sl_No = SlNoDr;
            ArrDrService.DoctorCode = Doc_code;

            var today = new Date();
            var formattedtoday = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();

            $("#txtDate").html(formattedtoday);

            setTimeout(function () {
                ProductTable(ArrDrService);
            }, 200);

        });

        /*---------------------URL Split Function-------------------------*/
        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }
    </script>
    
    <script type="text/javascript">
        /*---------------Product Table-------------------*/

        function ProductTable(ArrDrService) {

            var ArrayPrdID = [];

            setTimeout(function () { 

            var Col1 = "DOCTOR'S CURRENT SUPPORT.PRODUCTS & VALUE";
            var rows = '<tbody>';
            rows += '<tr id="trHead">';
            rows += '<th colspan="4" style="background-color:white;color:black;font-size:10px;">' + Col1 + '</th>';
            rows += '</tr>';
            rows += '<tr>';
            rows += '<th style="background-color:white;color:black;font-size:10px;">Product Name</th>';
            rows += '<th style="background-color:white;color:black;font-size:10px;">Price</th>';
            rows += '<th style="background-color:white;color:black;font-size:10px;">Qty</th>';
            rows += '<th style="background-color:white;color:black;font-size:10px;">Value</th>';
            rows += '</tr>';

            for (var i = 0; i < 6; i++) {
                rows += '<tr id="trPrd" class="blank_row">';

                var PrdID = "ddlProduct_" + i;
                var QtyID_1 = "txtPrice_" + i;
                rows += '<td><select id="ddlProduct_' + i + '" class="textbox"   style="font-size:10px;"></select><span class="textbox"   id="lblProduct_' + i + '" style="font-size:10px;"></span></td>';
                var ID = "ddlProduct_" + i;
                ArrayPrdID.push(ID);
                rows += '<td><span id="txtPrice_' + i + '" style="font-size:10px;" class="textbox" ><span/></td>';
                var Q_txt = "txtQtyS_" + i;
                var Q_Value = "txtValueS_" + i;
                rows += '<td><span id="txtQtyS_' + i + '" style="font-size:10px;" class="textbox" ><span/></td>';
                rows += '<td><span id="txtValueS_' + i + '" style="font-size:10px;" class="textbox" ><span/></td>';
                rows += '</tr>';
            }

            rows += '<tr id="tr_tot">';
            rows += '<td></td>';
            rows += '<td></td>';
            rows += '<td style="font-size:10px;">Total</td>';
            rows += '<td><span id="Total_Prd" style="font-size:10px;" class="textbox" ><span/></td>';

            rows += '</tr>';
            rows += '</tbody>';

            $("#tblCurrentSupport").html(rows);

            // Bind_Product_1(ArrayPrdID);

            var Col2 = "DOCTOR'S POTENTIAL.PRODUCTS & VALUE";
            var Rpoten = '<tbody>';
            Rpoten += '<tr id="trHead_Dr">';
            Rpoten += '<th colspan="4" style="background-color:white;color:black;font-size:10px;">' + Col2 + '</th>';
            Rpoten += '</tr>';
            Rpoten += '<tr>';
            Rpoten += '<th style="background-color:white;color:black;font-size:10px;">Product Name</th>';
            Rpoten += '<th style="background-color:white;color:black;font-size:10px;">Price</th>';
            Rpoten += '<th style="background-color:white;color:black;font-size:10px;">Qty</th>';
            Rpoten += '<th style="background-color:white;color:black;font-size:10px;">Value</th>';
            Rpoten += '</tr>';

            for (var j = 0; j < 6; j++) {
                Rpoten += '<tr id="trPrd_Dr" class="blank_row">';

                var PrdID = "ddlProductDr_" + j;
                var QtyID = "txtPriceDr_" + j;

                Rpoten += '<td><select id="ddlProductDr_' + j + '" style="font-size:10px;" class="textbox" ></select><span class="textbox"  id="lblProductDr_' + j + '" style="font-size:10px;"></span></td>';
                var ID = "ddlProductDr_" + j;
                ArrayPrdID.push(ID);
                Rpoten += '<td><span id="txtPriceDr_' + j + '" style="font-size:10px;" class="textbox" ><span/></td>';

                var Q_txt = "txtQtyDr_" + j;
                var Q_Value = "txtValueDr_" + j;
                Rpoten += '<td><span id="txtQtyDr_' + j + '" style="font-size:10px;" class="textbox" ><span/></td>';
                Rpoten += '<td><span id="txtValueDr_' + j + '" style="font-size:10px;" class="textbox" ><span/></td>';
                Rpoten += '</tr>';
            }

            Rpoten += '<tr id="trTot_Dr">';
            Rpoten += '<td></td>';
            Rpoten += '<td></td>';
            Rpoten += '<td style="font-size:10px;">Total</td>';
            Rpoten += '<td><span id="Total_Dr" style="font-size:10px;" class="textbox"><span/></td>';

            Rpoten += '</tr>';
            Rpoten += '</tbody>';

            $("#tblPotentialProduct").html(Rpoten);
            Bind_Product_1(ArrayPrdID);

        },2000);

        BindGet_CRM(ArrDrService);

        }

        /*---------------DropDown Product -------------------*/

        function Bind_Product_1(ArrayPrdID) {

            var PrdID = [];
            PrdID = ArrayPrdID;

            $.ajax({

                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Doctor_Service_CRM_Print.aspx/GetProductDetail",
                data: "{}",
                dataType: "json",
                success: function (result) {

                    for (var k = 0; k < ArrayPrdID.length; k++) {
                        var Id = ArrayPrdID[k];

                        $('#' + Id + '').empty();
                        $("#" + Id + "").append("<option value='0'>--Select--</option>");
                        $.each(result.d, function (key, value) {
                            $("#" + Id + "").append($("<option></option>").val(value.Product_Detail_Code).html(value.Product_Detail_Name));

                        });
                    }
                },
                error: function ajaxError(result) {
                    alert("Error");
                }

            });
        }

        function GetChemist(Doctor_Code) {

            $.ajax({

                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Doctor_Service_CRM_Print.aspx/GetChemist",
                data: '{DoctorCode:' + JSON.stringify(Doctor_Code) + '}',
                dataType: "json",
                success: function (result) {
                    $("#ddlChemist_1").empty();
                    $("#ddlChemist_2").empty();
                    $("#ddlChemist_3").empty();

                    $("#ddlChemist_1").append("<option value='0'>--Select--</option>");
                    $("#ddlChemist_2").append("<option value='0'>--Select--</option>");
                    $("#ddlChemist_3").append("<option value='0'>--Select--</option>");

                    $.each(result.d, function (key, value) {
                        $("#ddlChemist_1").append($("<option></option>").val(value.Chemists_Code).html(value.Chemists_Name));
                        $("#ddlChemist_2").append($("<option></option>").val(value.Chemists_Code).html(value.Chemists_Name));
                        $("#ddlChemist_3").append($("<option></option>").val(value.Chemists_Code).html(value.Chemists_Name));

                    });

                },
                error: function ajaxError(result) {
                    //alert("Error");
                }
            });
        }

        function GetStockist() {

            $.ajax({

                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Doctor_Service_CRM_Print.aspx/GetStockist",
                data: "{}",
                dataType: "json",
                success: function (result) {
                    $("#ddlStockist_1").empty();
                    $("#ddlStockist_2").empty();
                    $("#ddlStockist_3").empty();

                    $.each(result.d, function (key, value) {
                        $("#ddlStockist_1").append($("<option></option>").val(value.Stockist_Code).html(value.Stockist_Name));
                        $("#ddlStockist_2").append($("<option></option>").val(value.Stockist_Code).html(value.Stockist_Name));
                        $("#ddlStockist_3").append($("<option></option>").val(value.Stockist_Code).html(value.Stockist_Name));

                    });

                },
                error: function (result) {
                    // alert("Error");
                }

            });
        }

    </script>

    <script type="text/javascript">
        function BindGet_CRM(ArrDrService) {

            setTimeout(function () { 

            $.ajax({
                type: "POST",
                url: "Doctor_Service_CRM_Print.aspx/BindDoctorService",
                data: '{objDrDetail:' + JSON.stringify(ArrDrService) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d.length > 0) {

                        // var ArrSfDet = [];

                        for (var i = 0; i < data.d.length; i++) {

                            $("#txtField").html(data.d[i].Applied_By);
                            $("#txtHQ").html(data.d[i].Sf_HQ);
                            $("#txtDesig").html(data.d[i].Sf_Desig);

                            $("#divName").html(data.d[i].Division_Name);

                            var rows = '<tr>';
                            rows += '<td style="font-size:10px;">Doctors Name</td>';
                            rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + data.d[i].DoctorName + '</td>';
                            rows += '<td style="font-size:10px;">Address</td>';
                            rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + data.d[i].Address + '</td>';
                            rows += '</tr>';
                            rows += '<tr>';
                            rows += '<td style="font-size:10px;">Category</td>';
                            rows += '<td style="color:black;font-weight: bold;font-size:10px;" >' + data.d[i].Category + '</td>';
                            rows += '<td style="font-size:10px;">Qualification</td>';
                            rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + data.d[i].Qualification + '</td>';
                            rows += '</tr>';
                            rows += '<tr>';
                            rows += '<td style="font-size:10px;">Speciality</td>';
                            rows += '<td style="color:black;font-weight: bold;font-size:10px;" >' + data.d[i].Speciality + '</td>';
                            rows += '<td style="font-size:10px;">Class</td>';
                            rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + data.d[i].Class + '</td>';
                            rows += '</tr>';
                            rows += '<tr>';
                            rows += '<td style="font-size:10px;">Mobile No</td>';
                            rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + data.d[i].Mobile + '</td>';
                            rows += '<td style="font-size:10px;">Email</td>';
                            rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + data.d[i].Email + '</td>';
                            rows += '</tr>';

                            rows += '<tr>';
                            if (data.d[i].First_Lev_Name != "") {
                                var Data = [];
                                Data = data.d[i].First_Lev_Name.split('-');
                                rows += '<td style="font-size:10px;">Name of ' + Data[2] + '& HQ</td>';
                                rows += '<td style="color:black;font-weight: bold;font-size:10px;" >' + Data[1] + '-' + Data[3] + '</td>';
                            }
                            if (data.d[i].Second_Lev_Name != "") {
                                var FstData = [];
                                FstData = data.d[i].Second_Lev_Name.split('-');
                                rows += '<td style="font-size:10px;">Name of ' + FstData[2] + '& HQ</td>';
                                rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + FstData[1] + '-' + FstData[3] + '</td>';
                            }
                            else {
                                rows += '<td style="font-size:10px;"></td>';
                                rows += '<td style="color:black;font-weight: bold;font-size:10px;"></td>';
                            }
                            rows += '</tr>';

                            rows += '<tr>';
                            if (data.d[i].Third_Lev_Name != "") {
                                var Data = [];
                                Data = data.d[i].Third_Lev_Name.split('-');
                                rows += '<td style="font-size:10px;">Name of ' + Data[2] + '& HQ</td>';
                                rows += '<td style="color:black;font-weight: bold;font-size:10px;" >' + Data[1] + '-' + Data[3] + '</td>';
                            }
                            if (data.d[i].Four_Lev_Name != "") {
                                var FstData = [];
                                FstData = data.d[i].Four_Lev_Name.split('-');
                                rows += '<td style="font-size:10px;">Name of ' + FstData[2] + '& HQ</td>';
                                rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + FstData[1] + '-' + FstData[3] + '</td>';
                            }
                            else {
                                rows += '<td style="font-size:10px;"></td>';
                                rows += '<td style="color:black;font-weight: bold;font-size:10px;"></td>';
                            }
                            rows += '</tr>';

                            rows += '<tr>';
                            if (data.d[i].Fivth_Lev_Name != "") {
                                var Data = [];
                                Data = data.d[i].Fivth_Lev_Name.split('-');
                                rows += '<td style="font-size:10px;">Name of ' + Data[2] + '& HQ</td>';
                                rows += '<td style="color:black;font-weight: bold;font-size:10px;" >' + Data[1] + '-' + Data[3] + '</td>';
                            }
                            if (data.d[i].Six_Lev_Name != "") {
                                var FstData = [];
                                FstData = data.d[i].Six_Lev_Name.split('-');
                                rows += '<td style="font-size:10px;">Name of ' + FstData[2] + '& HQ</td>';
                                rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + FstData[1] + '-' + FstData[3] + '</td>';
                            }
                            else {
                                rows += '<td style="font-size:10px;"></td>';
                                rows += '<td style="color:black;font-weight: bold;font-size:10px;"></td>';
                            }
                            rows += '</tr>';

                            rows += '<tr>';
                            rows += '<td style="font-size:10px;">Applied Date</td>';

                            if (data.d[i].Applied_Date == "01/01/1900") {
                                data.d[i].Applied_Date = "";
                            }
                            if (data.d[i].Approved_Date == "01/01/1900") {
                                data.d[i].Approved_Date = "";
                            }
                            if (data.d[i].Confirmed_Date == "01/01/1900") {
                                data.d[i].Confirmed_Date = "";
                            }

                            rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + data.d[i].Applied_Date + '</td>';
                            rows += '<td style="font-size:10px;">Approved Date</td>';
                            rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + data.d[i].Approved_Date + '</td>';
                            rows += '</tr>';
                            rows += '<tr>';
                            rows += '<td style="font-size:10px;">Confirmed Date</td>';
                            rows += '<td style="color:black;font-weight: bold;font-size:10px;">' + data.d[i].Confirmed_Date + '</td>';
                            rows += '<td style="font-size:10px;"></td>';
                            rows += '<td style="color:black;font-weight: bold;font-size:10px;"></td>';
                            rows += '</tr>';

                            $("#ddlChemist_1 option[value='" + data.d[i].ddlChemist_1 + "']").attr('selected', 'selected');
                            $("#ddlChemist_2 option[value='" + data.d[i].ddlChemist_2 + "']").attr('selected', 'selected');
                            $("#ddlChemist_3 option[value='" + data.d[i].ddlChemist_3 + "']").attr('selected', 'selected');
                            $("#ddlStockist_1 option[value='" + data.d[i].ddlStockist_1 + "']").attr('selected', 'selected');
                            $("#ddlStockist_2 option[value='" + data.d[i].ddlStockist_2 + "']").attr('selected', 'selected');
                            $("#ddlStockist_3 option[value='" + data.d[i].ddlStockist_3 + "']").attr('selected', 'selected');

                            var SerRow = '<tr>';
                            SerRow += '<td style="font-size:10px;">Total Business Return Expected the Doctor in Amt(Rs/-)</br>(Target Amount)</td>';
                            SerRow += '<td style="color:black;font-weight: bold;font-size:10px;" >' + data.d[i].TotalBusReturn_Amt + '</td>';
                            SerRow += '</tr>';
                            SerRow += '<tr>';
                            SerRow += '<td style="font-size:10px;" >ROI Duration Month</td>';
                            SerRow += '<td style="color:black;font-weight: bold;font-size:10px;" >' + data.d[i].ROI_Dur_Month + '</td>';
                            SerRow += '</tr>';
                            SerRow += '<tr>';
                            SerRow += '<td style="font-size:10px;" >Service Required </td>';
                            SerRow += '<td style="color:black;font-weight: bold;font-size:10px;" >' + data.d[i].Service_Req + '</td>';
                            SerRow += '</tr>';
                            SerRow += '<tr>';
                            SerRow += '<td style="font-size:10px;" >Service Amount</td>';
                            SerRow += '<td style="color:black;font-weight: bold;font-size:10px;" >' + data.d[i].Service_Amt + '</td>';
                            SerRow += '</tr>';
                            SerRow += '<tr>';
                            SerRow += '<td style="font-size:10px;" >Specific Activities (Remarks)</td>';
                            SerRow += '<td style="color:black;font-weight: bold;font-size:10px;" >' + data.d[i].Specific_Act + '</td>';
                            SerRow += '</tr>';
                            SerRow += '<tr>';
                            SerRow += '<td style="font-size:10px;" >Prescription Outlets(Chemist)</td>';
                            SerRow += '<td style="color:black;font-weight: bold;font-size:10px;"><select id="ddlChemist_1" class="input-sm" ></select><span id="lblChem1" class="input-sm textbox">' + lblCh1 + '</span><select id="ddlChemist_2" class="input-sm" ></select><span id="lblChem2" class="input-sm textbox">' + lblCh2 + '</span><select id="ddlChemist_3" class="input-sm" ></select><span id="lblChem3" class="input-sm textbox">' + lblCh3 + '</span></td>';
                            SerRow += '</tr>';
                            SerRow += '<tr>';
                            SerRow += '<td style="font-size:10px;" >Stockist </td>';
                            SerRow += '<td style="color:black;font-weight: bold;font-size:10px;"><select id="ddlStockist_1" class="input-sm" ></select><span id="lblStock1" class="input-sm textbox">' + lblStock1 + '</span><select id="ddlStockist_2" class="input-sm" ></select><span id="lblStock2" class="input-sm textbox">' + lblStock2 + '</span><select id="ddlStockist_3" class="input-sm" ></select><span id="lblStock3" class="input-sm textbox">' + lblStock3 + '</span></td>';
                            SerRow += '</tr>';

                            $('#ddlProduct_0 option[value="' + data.d[i].Cur_Prod_Code_1 + '"]').attr("selected", "selected");
                            $('#ddlProduct_1 option[value="' + data.d[i].Cur_Prod_Code_2 + '"]').attr("selected", "selected");
                            $('#ddlProduct_2 option[value="' + data.d[i].Cur_Prod_Code_3 + '"]').attr("selected", "selected");
                            $('#ddlProduct_3 option[value="' + data.d[i].Cur_Prod_Code_4 + '"]').attr("selected", "selected");
                            $('#ddlProduct_4 option[value="' + data.d[i].Cur_Prod_Code_5 + '"]').attr("selected", "selected");
                            $('#ddlProduct_5 option[value="' + data.d[i].Cur_Prod_Code_6 + '"]').attr("selected", "selected");

                            $('#ddlProductDr_0 option[value="' + data.d[i].Potl_Prod_Code_1 + '"]').attr("selected", "selected");
                            $('#ddlProductDr_1 option[value="' + data.d[i].Potl_Prod_Code_2 + '"]').attr("selected", "selected");
                            $('#ddlProductDr_2 option[value="' + data.d[i].Potl_Prod_Code_3 + '"]').attr("selected", "selected");
                            $('#ddlProductDr_3 option[value="' + data.d[i].Potl_Prod_Code_4 + '"]').attr("selected", "selected");
                            $('#ddlProductDr_4 option[value="' + data.d[i].Potl_Prod_Code_5 + '"]').attr("selected", "selected");
                            $('#ddlProductDr_5 option[value="' + data.d[i].Potl_Prod_Code_6 + '"]').attr("selected", "selected");

                            $("#lblProduct_0").html($("#ddlProduct_0 option:selected").text());
                            $("#lblProduct_1").html($("#ddlProduct_1 option:selected").text());
                            $("#lblProduct_2").html($("#ddlProduct_2 option:selected").text());
                            $("#lblProduct_3").html($("#ddlProduct_3 option:selected").text());
                            $("#lblProduct_4").html($("#ddlProduct_4 option:selected").text());
                            $("#lblProduct_5").html($("#ddlProduct_5 option:selected").text());

                            $("#lblProductDr_0").html($("#ddlProductDr_0 option:selected").text());
                            $("#lblProductDr_1").html($("#ddlProductDr_1 option:selected").text());
                            $("#lblProductDr_2").html($("#ddlProductDr_2 option:selected").text());
                            $("#lblProductDr_3").html($("#ddlProductDr_3 option:selected").text());
                            $("#lblProductDr_4").html($("#ddlProductDr_4 option:selected").text());
                            $("#lblProductDr_5").html($("#ddlProductDr_5 option:selected").text());

                            if ($("#lblProduct_0").html() == "--Select--") {
                                $("#lblProduct_0").html("");
                            }
                            if ($("#lblProduct_1").html() == "--Select--") {
                                $("#lblProduct_1").html("");
                            }
                            if ($("#lblProduct_2").html() == "--Select--") {
                                $("#lblProduct_2").html("");
                            }
                            if ($("#lblProduct_3").html() == "--Select--") {
                                $("#lblProduct_3").html("");
                            }
                            if ($("#lblProduct_4").html() == "--Select--") {
                                $("#lblProduct_4").html("");
                            }
                            if ($("#lblProduct_5").html() == "--Select--") {
                                $("#lblProduct_5").html("");
                            }
                            if ($("#lblProductDr_0").html() == "--Select--") {
                                $("#lblProductDr_0").html("");
                            }
                            if ($("#lblProductDr_1").html() == "--Select--") {
                                $("#lblProductDr_1").html("");
                            }
                            if ($("#lblProductDr_2").html() == "--Select--") {
                                $("#lblProductDr_2").html("");
                            }
                            if ($("#lblProductDr_3").html() == "--Select--") {
                                $("#lblProductDr_3").html("");
                            }
                            if ($("#lblProductDr_4").html() == "--Select--") {
                                $("#lblProductDr_4").html("");
                            }
                            if ($("#lblProductDr_5").html() == "--Select--") {
                                $("#lblProductDr_5").html("");
                            }

                            $("#ddlProduct_0").hide();
                            $("#ddlProduct_1").hide();
                            $("#ddlProduct_2").hide();
                            $("#ddlProduct_3").hide();
                            $("#ddlProduct_4").hide();
                            $("#ddlProduct_5").hide();

                            $("#ddlProductDr_0").hide();
                            $("#ddlProductDr_1").hide();
                            $("#ddlProductDr_2").hide();
                            $("#ddlProductDr_3").hide();
                            $("#ddlProductDr_4").hide();
                            $("#ddlProductDr_5").hide();



                            $("#txtPrice_0").html(data.d[i].Cur_Prod_Price_1);
                            $("#txtPrice_1").html(data.d[i].Cur_Prod_Price_2);
                            $("#txtPrice_2").html(data.d[i].Cur_Prod_Price_3);
                            $("#txtPrice_3").html(data.d[i].Cur_Prod_Price_4);
                            $("#txtPrice_4").html(data.d[i].Cur_Prod_Price_5);
                            $("#txtPrice_5").html(data.d[i].Cur_Prod_Price_6);

                            $("#txtPriceDr_0").html(data.d[i].Potl_Prod_Price_1);
                            $("#txtPriceDr_1").html(data.d[i].Potl_Prod_Price_2);
                            $("#txtPriceDr_2").html(data.d[i].Potl_Prod_Price_3);
                            $("#txtPriceDr_3").html(data.d[i].Potl_Prod_Price_4);
                            $("#txtPriceDr_4").html(data.d[i].Potl_Prod_Price_5);
                            $("#txtPriceDr_5").html(data.d[i].Potl_Prod_Price_6);

                            $("#txtQtyS_0").html(data.d[i].Cur_Prod_Qty_1);
                            $("#txtQtyS_1").html(data.d[i].Cur_Prod_Qty_2);
                            $("#txtQtyS_2").html(data.d[i].Cur_Prod_Qty_3);
                            $("#txtQtyS_3").html(data.d[i].Cur_Prod_Qty_4);
                            $("#txtQtyS_4").html(data.d[i].Cur_Prod_Qty_5);
                            $("#txtQtyS_5").html(data.d[i].Cur_Prod_Qty_6);

                            $("#txtQtyDr_0").html(data.d[i].Potl_Prod_Qty_1);
                            $("#txtQtyDr_1").html(data.d[i].Potl_Prod_Qty_2);
                            $("#txtQtyDr_2").html(data.d[i].Potl_Prod_Qty_3);
                            $("#txtQtyDr_3").html(data.d[i].Potl_Prod_Qty_4);
                            $("#txtQtyDr_4").html(data.d[i].Potl_Prod_Qty_5);
                            $("#txtQtyDr_5").html(data.d[i].Potl_Prod_Qty_6);

                            $("#txtValueS_0").html(data.d[i].Cur_Prod_Value_1);
                            $("#txtValueS_1").html(data.d[i].Cur_Prod_Value_2);
                            $("#txtValueS_2").html(data.d[i].Cur_Prod_Value_3);
                            $("#txtValueS_3").html(data.d[i].Cur_Prod_Value_4);
                            $("#txtValueS_4").html(data.d[i].Cur_Prod_Value_5);
                            $("#txtValueS_5").html(data.d[i].Cur_Prod_Value_6);

                            $("#txtValueDr_0").html(data.d[i].Potl_Prod_Value_1);
                            $("#txtValueDr_1").html(data.d[i].Potl_Prod_Value_2);
                            $("#txtValueDr_2").html(data.d[i].Potl_Prod_Value_3);
                            $("#txtValueDr_3").html(data.d[i].Potl_Prod_Value_4);
                            $("#txtValueDr_4").html(data.d[i].Potl_Prod_Value_5);
                            $("#txtValueDr_5").html(data.d[i].Potl_Prod_Value_6);



                            $("#Total_Prd").html(data.d[i].Cur_Total);
                            $("#Total_Dr").html(data.d[i].Potl_Total);

                        }
                        $("#tblDrDetail").html(rows);
                        $("#tblServiceReq").html(SerRow);

                        var lblCh1 = $("#ddlChemist_1 option:selected").text();
                        var lblCh2 = $("#ddlChemist_2 option:selected").text();
                        var lblCh3 = $("#ddlChemist_3 option:selected").text();

                        var lblStock1 = $("#ddlStockist_1 option:selected").text();
                        var lblStock2 = $("#ddlStockist_2 option:selected").text();
                        var lblStock3 = $("#ddlStockist_3 option:selected").text();


                        $("#lblChem1").html(lblCh1);
                        $("#lblChem2").html(lblCh2);
                        $("#lblChem3").html(lblCh3);

                        $("#lblStock1").html(lblStock1);
                        $("#lblStock2").html(lblStock2);
                        $("#lblStock3").html(lblStock3);

                        $("#ddlChemist_1").hide();
                        $("#ddlChemist_2").hide();
                        $("#ddlChemist_3").hide();

                        $("#ddlStockist_1").hide();
                        $("#ddlStockist_2").hide();
                        $("#ddlStockist_3").hide();

                    }
                },
                error: function (res) {

                }
            });

        }, 2500);
        }
    </script>

    <style type="text/css">
        @media print
        {
            .noPrnCtrl
            {
                display: none;
            }
        }
    </style>
    <script type="text/javascript">
        var NeNCtrl = false;
        function cnvtExc($v) {
            if ($v == '_E') {
                lc = window.location.toString();
                var s = ('/' + window.location.pathname).split('/');
                var fn = (s[s.length - 1].substring(0, s[s.length - 1].indexOf('.')));
                with (document.forms[0]) {
                    action = lc + ((lc.indexOf('?') > -1) ? "&" : "?") + "Md=" + $v + "&Fln=" + fn; method = 'post'; submit();
                }
            }
            else if ($v == '_P') {
                print();
            }
            else if ($v == '_C') {
                window.close();
            }
            else if ($v == '_B') {
                window.location.href = '';
            }
        }
        function creCtrl() {
            ReqctlStrs = "hSF$$$TNRM01^^^slM$$$10^^^slY$$$2015^^^hMod$$$_v^^^";
            if (document.forms.length <= 0) {
                var ctlfrmEx = document.createElement("Form");
                document.body.appendChild(ctlfrmEx);
            }
            else {
                var ctlfrmEx = document.forms[0];
            }
            if (ReqctlStrs != '') {
                ReqctlStrs = ReqctlStrs.split('^^^');
                for ($intCtls = 0; $intCtls < ReqctlStrs.length - 1; $intCtls++) {
                    ReqctlStrsSP = ReqctlStrs[$intCtls].split('$$$');
                    if (ReqctlStrsSP[0].indexOf('image') < 0) {
                        if (eval('ctlfrmEx.' + ReqctlStrsSP[0]) == null) {
                            var ctlhidEx = document.createElement("INPUT");
                            with (ctlhidEx) {
                                type = 'hidden';
                                id = ReqctlStrsSP[0];
                                name = ReqctlStrsSP[0];
                                value = ReqctlStrsSP[1];
                                ctlfrmEx.appendChild(ctlhidEx);
                            }
                        }
                    }
                }
            }
            var ctlPrnEx = document.createElement("div");
            with (ctlPrnEx.style) {
                position = "absolute";
                top = "0px";
                right = "0px";
                cursor = "hand";
            }
            var $sH = "<table class='noPrnCtrl' style='border-collapse:collapse;'><tr>"; if (NeNCtrl == false) $sH += "<td id='btnprint' style='border:solid 1px #000000;background-Color:#666633;color:#FFFFFF;cursor:pointer;font-size:13px' onclick=\"cnvtExc('_P')\">Print</td>";
            if (window.opener != null) {
                $sH += "<td style='width:2px'></td><td style='border:solid 1px #000000;background-Color:#666633;color:#FFFFFF;font-size:13px' onclick=\"cnvtExc('_C')\">Close</td>"
            }
            $sH = $sH + "</tr></table>";
            ctlPrnEx.innerHTML = $sH; document.body.appendChild(ctlPrnEx);
        }
        function creFltr() {
            $CMB = document.getElementsByAlphFilter();
            for ($iql = 0; $iql < $CMB.length; $iql++) {
                nCMB = document.createElement('SELECT');
                nSpn = document.createElement('span');
                nSpn.id = "spnSrc_" + $iql;
                nSpn.innerText = getSrcStr($CMB[$iql]);
                nSpn.style.display = 'none';
                $CMB[$iql].insertAdjacentElement('beforeBegin', nCMB);
                nCMB.insertAdjacentElement('beforeBegin', nSpn);
                if ($CMB[$iql].id == '') $CMB[$iql].id = "TSrch_" + $iql;
                nCMB.outerHTML = "<select class='combovalue' onchange='FilterCmb(this.value," + $CMB[$iql].id + ",spnSrc_" + $iql + ")'><option value=''>--ALL--</option>" + getFilterChar($CMB[$iql].innerHTML.replace(/&nbsp;/g, '')) + "</select>";
            }
        }
        function getSrcStr() {
            var $sStr = '';
            $opt = $CMB[$iql].getElementsByTagName("OPTION");
            for ($i = 0; $i < $opt.length; $i++) {
                if ($opt[$i].value != '') $sStr += $opt[$i].text.replace(/ /g, '').substring(0, 1).toLocaleUpperCase() + "#" + $opt[$i].outerHTML + '$';
            }
            return $sStr;
        }
        function FilterCmb($x, $y, $z) {
            var $rss = '';
            $sScr = $z.innerText;
            $spP = $sScr.indexOf($x + '#');
            while ($spP > -1) {
                $epP = $sScr.indexOf('$', $spP);
                $rss += $sScr.substring($spP, $epP);
                $spP = $sScr.indexOf($x + '#', $epP);
            } if ($rss == '') $rss = $sScr;
            if ($y.outerHTML.indexOf('</OPTION>', 0) > -1)
                $y.outerHTML = $y.outerHTML.substring(0, $y.outerHTML.indexOf('</OPTION>', 0)) + '</OPTION>' + $rss + '</SELECT>';
            else
                $y.outerHTML = $y.outerHTML.substring(0, $y.outerHTML.indexOf('</option>', 0)) + '</OPTION>' + $rss + '</SELECT>';
        }
        function getFilterChar(Str) {
            $rstr = '';
            for ($i = 65; $i < 91; $i++) {
                fnStr = '>' + String.fromCharCode($i);
                if (Str.indexOf(fnStr) > -1 || Str.indexOf(fnStr.toLowerCase()) > -1) $rstr += "<option value='" + String.fromCharCode($i) + "'>" + String.fromCharCode($i) + "</option>";
            }
            return $rstr;
        }
        document.getElementsByAlphFilter = function () {
            var retnode = [];
            $CMB = document.getElementsByTagName('SELECT');
            $sCnt = $CMB.length;
            for ($iql = 0; $iql < $sCnt; $iql++)
                if ($CMB[$iql].getAttribute('AlphFilter') != null && $CMB[$iql].getAttribute('AlphFilter').toLowerCase() == 'true') retnode.push($CMB[$iql]);
            return retnode;
        }

        objGetElementsByName = function ($TRElem, $Name) {
            var retnode = [];
            $CMB = $TRElem.getElementsByTagName('SELECT'); $sCnt = $CMB.length; for ($iql = 0; $iql < $sCnt; $iql++) if ($CMB[$iql].name != null && $CMB[$iql].name.toLowerCase() == $Name.toLowerCase()) retnode.push($CMB[$iql]);
            $CMB = $TRElem.getElementsByTagName('INPUT'); $sCnt = $CMB.length; for ($iql = 0; $iql < $sCnt; $iql++) if ($CMB[$iql].name != null && $CMB[$iql].name.toLowerCase() == $Name.toLowerCase()) retnode.push($CMB[$iql]);
            $CMB = $TRElem.getElementsByTagName('TEXTAREA'); $sCnt = $CMB.length; for ($iql = 0; $iql < $sCnt; $iql++) if ($CMB[$iql].name != null && $CMB[$iql].name.toLowerCase() == $Name.toLowerCase()) retnode.push($CMB[$iql]);

            return retnode;
        }
        var NNCtrl = false;
        if (typeof (window.opener) == "undefined") { NNCtrl = true; }
        window.onload = function () {
            if (NNCtrl == false) creCtrl();
            creFltr();
        }

    </script>
    <style type="text/css">
        .grid
        {
            border: 1px solid #CCC;
        }
        
        .grid tr
        {
            height: 30px;
            border-bottom: 1px solid black;
        }
        
        .grid tr:first-child
        {
            border: none;
        }
        
        .grid tr:last-child
        {
            border: none;
        }
        .grid td
        {
            border-right: 1px solid black;
            border-bottom: 1px solid black;
            border-top: 1px solid black;
        }
        .grid td:last-child
        {
            border-right: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="width: 100%">
            <center>
            <div style="min-width: 70%; border: 1px solid #000;max-width:75%;">
                <div style="width: 100%; height: 20px">
                    <h3>CRM REQUEST FORM</h3>
                    <h4 id="divName"></h4>
                </div>
                <br />
                <div>
                    <table id="tblDoc" class="tblDr" style="width: 100%">
                        <tr>
                            <td>
                                <span id="lblSerial">Serial No </span>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <span id="txtSerial" style="font-weight: bold;"></span>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <span id="lblDate">Date </span>
                            </td>
                            <td>
                             :
                            </td>
                            <td>
                                <span id="txtDate" style="font-weight: bold;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="lblFieldForce">Field Force Name </span>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <span id="txtField" style="font-weight: bold;"></span>
                            </td>
                            <td>
                                <span id="lblHQ">HQ </span>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <span id="txtHQ" style="font-weight: bold;"></span>
                            </td>
                            <td>
                                <span id="lblDesig">Designation </span>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <span id="txtDesig" style="font-weight: bold;"></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width:100%;">                  
                 <table id="tblDrDetail" class="table table-bordered table-striped" style="width: 100%">
                 </table>   
                </div>
                <div>
                    <div>
                        <p style="font-family: Verdana; font-weight: bold; font-size: 11px; color: black">
                            Visit - Last Three Months</p>
                    </div>
                    <asp:Table ID="tbl" runat="server" class="table table-bordered table-striped" Width="100%">
                    </asp:Table>
                </div>
                <div style="width: 100%">
                    <table id="Table1" class="table table-bordered table-striped" width="100%">
                        <tr>
                            <td>
                                <div>
                                    <table id="tblCurrentSupport" class="table table-bordered table-striped" style="width:100%;">
                                    </table>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <table id="tblPotentialProduct" class="table table-bordered table-striped" style="width:100%;">
                                    </table>
                                </div>
                            </td>
                        </tr>
                       
                    </table>
                </div>
                <div style="width:100%">
                <table id="tblServiceReq" class="table table-bordered table-striped" style="width: 100%;white-space:pre">
                                </table>
                </div>
                <div>
                <center>
                    <table id="tblConfirm" class="tblDr" style="width: 100%;white-space:pre">
                        <tr>
                            <td>
                                <span id="lblApprovedBy" style="font-weight: bold;">Applied By</span>
                            </td>
                            <td>
                                <span id="txtApproveBy" style="font-weight: bold;"></span>
                            </td>
                            <td>
                            </td>                            
                            <td>
                                <span id="lblConfim" style="font-weight: bold;">Confirm By</span>
                            </td>
                            <td>                              
                            </td>
                            <td>
                                <span id="txtConfirm" style="font-weight: bold;"></span>
                            </td>
                        </tr>
                    </table>
                    </center>
                </div>
            </div>
              </center>
        </div>
    </div>
    </form>
</body>
</html>
