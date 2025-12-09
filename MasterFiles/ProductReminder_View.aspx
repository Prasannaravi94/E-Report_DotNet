<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductReminder_View.aspx.cs"
    Inherits="MasterFiles_ProductReminder_View" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Details View</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />--%>
    <link href="../assets/css/Calender_CheckBox.css" rel="stylesheet" />

    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
            .PnlDesign;

        {
            border: solid 1px #000000;
            height: 150px;
            width: 330px;
            overflow-y: scroll;
            background-color: #EAEAEA;
            font-size: 15px;
            font-family: Arial;
        }

        .txtbox {
            background-image: url(../images/drpdwn.png);
            background-position: right top;
            background-repeat: no-repeat;
            cursor: pointer;
            cursor: hand;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 200px;
            height: 500px;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }

        .closeLoginPanel {
            font-family: Verdana, Helvetica, Arial, sans-serif;
            height: 14px;
            font-size: 11px;
            font-weight: bold;
            position: absolute;
            top: -2px;
            right: 1px;
        }

            .closeLoginPanel a {
                /*background-color: Yellow;*/
                cursor: pointer;
                color: Black;
                text-align: center;
                text-decoration: none;
                padding: 3px;
            }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSubmit').click(function () {
                var st = $('#<%=ddlState.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select State."); $('ddlState').focus(); return false; }
                var FromYear = $('#<%=ddlFromYear.ClientID%> :selected').text();
                if (FromYear == "---Select---") { alert("Select From Year."); $('#ddlFromYear').focus(); return false; }
                var ToYear = $('#<%=ddlToYear.ClientID%> :selected').text();
                if (ToYear == "---Select---") { alert("Select To Year."); $('#ddlToYear').focus(); return false; }

            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var checked_checkboxes = $("[id*=ChkDiv] input:checked");
            var message = "";
            checked_checkboxes.each(function () {
                var value = $(this).val();
                var text = $(this).closest("td").find("label").html();
                message += text + '/';
            });

            var checked_checkboxes2 = $("[id*=chkState] input:checked");
            var message2 = "";
            checked_checkboxes2.each(function () {
                var value2 = $(this).val();
                var text2 = $(this).closest("td").find("label").html();
                message2 += text2 + '/';
            });

            var checked_checkboxes3 = $("[id*=ChkBrand] input:checked");
            var message3 = "";
            checked_checkboxes3.each(function () {
                var value3 = $(this).val();
                var text3 = $(this).closest("td").find("label").html();
                message3 += text3 + '/';

            });

            document.getElementById("ddlBrandChk").options[0] = new Option(message3, "");
            document.getElementById("ddlStateChk").options[0] = new Option(message2, "");
            document.getElementById("ddlSubDivChk").options[0] = new Option(message, "");
            return false;
        });
    </script>



    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function Validate_Checkbox() {

            var ddlVal = $("#ddlMode").val();
            var chkstate = $("#<%= chkState.ClientID %> input:checkbox");
            var chksDiv = $("#<%= ChkDiv.ClientID %> input:checkbox");
            var chksBrand = $("#<%= ChkBrand.ClientID %> input:checkbox");

            var hasCheckedState = false;
            var hasCheckedDiv = false;
            var hasCheckedBrand = false;
            if (ddlVal == "3") {
                for (var i = 0; i < chkstate.length; i++) {
                    if (chkstate[i].checked) {
                        hasCheckedState = true;
                        break;
                    }
                }
                if (hasCheckedState == false) {
                    alert("Please select at least one State..!");
                    return false;
                }

                for (var i = 0; i < chksDiv.length; i++) {
                    if (chksDiv[i].checked) {
                        hasCheckedDiv = true;
                        break;
                    }
                }
                if (hasCheckedDiv == false) {
                    alert("Please select at least one SubDivision..!");
                    return false;
                }

                for (var i = 0; i < chksBrand.length; i++) {
                    if (chksBrand[i].checked) {
                        hasCheckedBrand = true;
                        break;
                    }
                }
                if (hasCheckedBrand == false) {
                    alert("Please select at least one Brand..!");
                    return false;
                }
            }
        }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript">
        function CheckAll() {
            var intIndex = 0;
            var rowCount = document.getElementById('ChkBrand').getElementsByTagName("input").length;

            var message = "";

            var checkBoxList = document.getElementById("<%=ChkBrand.ClientID%>");
            var checkBoxes = checkBoxList.getElementsByTagName("INPUT");
            for (intIndex = 0; intIndex < rowCount; intIndex++) {
                if (document.getElementById('checkAll').checked == true) {
                    if (document.getElementById("ChkBrand" + "_" + intIndex)) {
                        if (document.getElementById("ChkBrand" + "_" + intIndex).disabled != true)
                            document.getElementById("ChkBrand" + "_" + intIndex).checked = true;
                        var text = checkBoxes[intIndex].parentNode.getElementsByTagName("LABEL")[0].innerHTML;
                        message += text + " / ";

                    }
                }
                else {
                    if (document.getElementById("ChkBrand" + "_" + intIndex)) {
                        if (document.getElementById("ChkBrand" + "_" + intIndex).disabled != true)
                            document.getElementById("ChkBrand" + "_" + intIndex).checked = false;
                    }
                }
            }
            //document.getElementById("ddlBrandChk").options[0] = new Option(message, "");
            document.getElementsByName('ddlBrandChk')[0].value = message;
        }


        function ClearAll() {
            var intIndex = 0;
            var flag = 0;
            var rowCount = document.getElementById('ChkBrand').getElementsByTagName("input").length;
            for (intIndex = 0; intIndex < rowCount; intIndex++) {
                if (document.getElementById("ChkBrand" + "_" + intIndex)) {
                    if (document.getElementById("ChkBrand" + "_" + intIndex).checked == true) {
                        flag = 1;
                    }
                    else {
                        flag = 0;
                        break;
                    }
                }
            }
            if (flag == 0)
                document.getElementById('checkAll').checked = false;
            else
                document.getElementById('checkAll').checked = true;


            var message = "";
            var checkBoxList = document.getElementById("<%=ChkBrand.ClientID%>");
            var checkBoxes = checkBoxList.getElementsByTagName("INPUT");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    var value = checkBoxes[i].value;
                    var text = checkBoxes[i].parentNode.getElementsByTagName("LABEL")[0].innerHTML;
                    message += text + " / ";

                    //             message += "\n";
                }
                //document.getElementById("ddlBrandChk").options[0] = new Option(message, "");
                document.getElementsByName('ddlBrandChk')[0].value = message;
            }
        }


        function CheckAllState() {
            var intIndex = 0;
            var rowCount = document.getElementById('chkState').getElementsByTagName("input").length;
            var message = "";

            var checkBoxList = document.getElementById("<%=chkState.ClientID%>");
            var checkBoxes = checkBoxList.getElementsByTagName("INPUT");
            for (intIndex = 0; intIndex < rowCount; intIndex++) {
                if (document.getElementById('CheckState').checked == true) {
                    if (document.getElementById("chkState" + "_" + intIndex)) {
                        if (document.getElementById("chkState" + "_" + intIndex).disabled != true)
                            document.getElementById("chkState" + "_" + intIndex).checked = true;
                        var text = checkBoxes[intIndex].parentNode.getElementsByTagName("LABEL")[0].innerHTML;
                        message += text + " / ";
                    }
                }
                else {
                    if (document.getElementById("chkState" + "_" + intIndex)) {
                        if (document.getElementById("chkState" + "_" + intIndex).disabled != true)
                            document.getElementById("chkState" + "_" + intIndex).checked = false;
                    }
                }
            }
            //document.getElementById("ddlStateChk").options[0] = new Option(message, "");
            document.getElementsByName('ddlStateChk')[0].value = message;
        }

        function ClearAllState() {
            var intIndex = 0;
            var flag = 0;
            var rowCount = document.getElementById('chkState').getElementsByTagName("input").length;
            for (intIndex = 0; intIndex < rowCount; intIndex++) {
                if (document.getElementById("chkState" + "_" + intIndex)) {
                    if (document.getElementById("chkState" + "_" + intIndex).checked == true) {
                        flag = 1;
                    }
                    else {
                        flag = 0;
                        break;
                    }
                }
            }
            if (flag == 0)
                document.getElementById('CheckState').checked = false;
            else
                document.getElementById('CheckState').checked = true;


            var message = "";
            var checkBoxList = document.getElementById("<%=chkState.ClientID%>");
            var checkBoxes = checkBoxList.getElementsByTagName("INPUT");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    var value = checkBoxes[i].value;
                    var text = checkBoxes[i].parentNode.getElementsByTagName("LABEL")[0].innerHTML;
                    message += text + " / ";

                    //             message += "\n";
                }
                //document.getElementById("ddlStateChk").options[0] = new Option(message, "");
                document.getElementsByName('ddlStateChk')[0].value = message;
            }
        }

        function CheckAllDiv() {
            var intIndex = 0;
            var rowCount = document.getElementById('ChkDiv').getElementsByTagName("input").length;
            var message = "";

            var checkBoxList = document.getElementById("<%=ChkDiv.ClientID%>");
            var checkBoxes = checkBoxList.getElementsByTagName("INPUT");
            for (intIndex = 0; intIndex < rowCount; intIndex++) {
                if (document.getElementById('CheckDiv').checked == true) {
                    if (document.getElementById("ChkDiv" + "_" + intIndex)) {
                        if (document.getElementById("ChkDiv" + "_" + intIndex).disabled != true)
                            document.getElementById("ChkDiv" + "_" + intIndex).checked = true;

                        var text = checkBoxes[intIndex].parentNode.getElementsByTagName("LABEL")[0].innerHTML;
                        message += text + " / ";
                    }
                }
                else {
                    if (document.getElementById("ChkDiv" + "_" + intIndex)) {
                        if (document.getElementById("ChkDiv" + "_" + intIndex).disabled != true)
                            document.getElementById("ChkDiv" + "_" + intIndex).checked = false;
                    }
                }
            }
            //document.getElementById("ddlSubDivChk").options[0] = new Option(message, "");
            document.getElementsByName('ddlSubDivChk')[0].value = message;
        }

        function ClearAllDiv() {
            var intIndex = 0;
            var flag = 0;

            var rowCount = document.getElementById('ChkDiv').getElementsByTagName("input").length;
            for (intIndex = 0; intIndex < rowCount; intIndex++) {
                if (document.getElementById("ChkDiv" + "_" + intIndex)) {
                    if (document.getElementById("ChkDiv" + "_" + intIndex).checked == true) {
                        flag = 1;
                    }
                    else {
                        flag = 0;
                        break;
                    }
                }
            }
            if (flag == 0)
                document.getElementById('CheckDiv').checked = false;
            else
                document.getElementById('CheckDiv').checked = true;



            var message = "";
            var checkBoxList = document.getElementById("<%=ChkDiv.ClientID%>");
            var checkBoxes = checkBoxList.getElementsByTagName("INPUT");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    var value = checkBoxes[i].value;
                    var text = checkBoxes[i].parentNode.getElementsByTagName("LABEL")[0].innerHTML;
                    message += text + " / ";

                    //             message += "\n";
                }
                //document.getElementById("ddlSubDivChk").options[0] = new Option(message, "");
                document.getElementsByName('ddlSubDivChk')[0].value = message;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var ddlVal = $("#ddlMode").val();

            if (ddlVal == "0") {
                $("#ddlState").css("display", "block");
                $("#ddlDiv").css("display", "none");
                $("#ddlBrand").css("display", "none");
                $("#ddlStateChk").css("display", "none");
                $("#ddlSubDivChk").css("display", "none");
                $("#ddlBrandChk").css("display", "none");
                $("#lblState").css("display", "block");
                $("#lblDiv ").css("display", "none");
                $("#lblBrand").css("display", "none");
                $("#lblChkState").css("display", "none");
                $("#lblChkDiv").css("display", "none");
                $("#lblChkBrand").css("display", "none");

                //                 document.getElementById("btnSubmit").style.visibility = "visible";
                //                 $("#btnSubmit").css("display", "block");
                $("#btnProceed").css("display", "none");
                $("#btnClear").css("display", "none");


            }
            else if (ddlVal == "2") {

                $("#ddlState").css("display", "none");
                $("#ddlDiv").css("display", "none");
                $("#ddlBrand").css("display", "block");
                $("#ddlStateChk").css("display", "none");
                $("#ddlSubDivChk").css("display", "none");
                $("#ddlBrandChk").css("display", "none");
                $("#lblState").css("display", "none");
                $("#lblDiv ").css("display", "none");
                $("#lblBrand").css("display", "block");
                $("#lblChkState").css("display", "none");
                $("#lblChkDiv").css("display", "none");
                $("#lblChkBrand").css("display", "none");
            }
            else if (ddlVal == "3") {
                $("#ddlState").css("display", "none");
                $("#ddlDiv").css("display", "none");
                $("#ddlBrand").css("display", "none");
                $("#ddlStateChk").css("display", "block");
                $("#ddlSubDivChk").css("display", "block");
                $("#ddlBrandChk").css("display", "block");
                $("#lblState").css("display", "none");
                $("#lblDiv ").css("display", "none");
                $("#lblBrand").css("display", "none");
                $("#lblChkState").css("display", "block");
                $("#lblChkDiv").css("display", "block");
                $("#lblChkBrand").css("display", "block");
            }
            else if (ddlVal == "1") {
                $("#ddlState").css("display", "none");
                $("#ddlDiv").css("display", "block");
                $("#ddlBrand").css("display", "none");
                $("#ddlStateChk").css("display", "none");
                $("#ddlSubDivChk").css("display", "none");
                $("#ddlBrandChk").css("display", "none");
                $("#lblState").css("display", "none");
                $("#lblDiv ").css("display", "block");
                $("#lblBrand").css("display", "none");
                $("#lblChkState").css("display", "none");
                $("#lblChkDiv").css("display", "none");
                $("#lblChkBrand").css("display", "none");

                var TypeDel = {};
                TypeDel.Type = ddlVal;
                //             GetDropDownVal(TypeDel, ddlVal);

            }

            $("#ddlMode").change(function () {
                var ddlVal = $("#ddlMode").val();

                if (ddlVal == "0") {
                    $("#ddlState").css("display", "block");
                    $("#ddlDiv").css("display", "none");
                    $("#ddlBrand").css("display", "none");
                    $("#ddlStateChk").css("display", "none");
                    $("#ddlSubDivChk").css("display", "none");
                    $("#ddlBrandChk").css("display", "none");
                    $("#lblState").css("display", "block");
                    $("#lblDiv ").css("display", "none");
                    $("#lblBrand").css("display", "none");
                    $("#lblChkState").css("display", "none");
                    $("#lblChkDiv").css("display", "none");
                    $("#lblChkBrand").css("display", "none");


                    //                     $("#btnSubmit").css("display", "block");
                    $("#btnProceed").css("display", "none");
                    $("#btnClear").css("display", "none");

                }
                else if (ddlVal == "2") {

                    $("#ddlState").css("display", "none");
                    $("#ddlDiv").css("display", "none");
                    $("#ddlBrand").css("display", "block");
                    $("#ddlStateChk").css("display", "none");
                    $("#ddlSubDivChk").css("display", "none");
                    $("#ddlBrandChk").css("display", "none");
                    $("#lblState").css("display", "none");
                    $("#lblDiv ").css("display", "none");
                    $("#lblBrand").css("display", "block");
                    $("#lblChkState").css("display", "none");
                    $("#lblChkDiv").css("display", "none");
                    $("#lblChkBrand").css("display", "none");

                    //                     document.getElementById("btnSubmit").style.visibility = "visible";
                    //                     $("#btnSubmit").css("display", "block");
                    $("#btnProceed").css("display", "none");
                    $("#btnClear").css("display", "none");
                }
                else if (ddlVal == "3") {

                    $("#ddlStateChk").css("display", "block");
                    $("#ddlState").css("display", "none");
                    $("#ddlDiv").css("display", "none");
                    $("#ddlBrand").css("display", "none");

                    $("#ddlSubDivChk").css("display", "block");
                    $("#ddlBrandChk").css("display", "block");
                    $("#lblState").css("display", "none");
                    $("#lblDiv ").css("display", "none");
                    $("#lblBrand").css("display", "none");
                    $("#lblChkState").css("display", "block");
                    $("#lblChkDiv").css("display", "block");
                    $("#lblChkBrand").css("display", "block");

                    //                     $("#btnSubmit").css("display", "none");
                    $("#btnProceed").css("display", "block");
                    $("#btnClear").css("display", "block");

                }
                else if (ddlVal == "1") {

                    $("#ddlDiv").css("display", "block");
                    $("#ddlState").css("display", "none");

                    $("#ddlBrand").css("display", "none");
                    $("#ddlStateChk").css("display", "none");
                    $("#ddlSubDivChk").css("display", "none");
                    $("#ddlBrandChk").css("display", "none");
                    $("#lblState").css("display", "none");
                    $("#lblDiv ").css("display", "block");
                    $("#lblBrand").css("display", "none");
                    $("#lblChkState").css("display", "none");
                    $("#lblChkDiv").css("display", "none");
                    $("#lblChkBrand").css("display", "none");

                    //                     document.getElementById("btnSubmit").style.visibility = "visible";
                    //                     $("#btnSubmit").css("display", "block");
                    $("#btnProceed").css("display", "none");
                    $("#btnClear").css("display", "none");

                    var TypeDel = {};
                    TypeDel.Type = ddlVal;
                    //             GetDropDownVal(TypeDel, ddlVal);

                }


            });
        });

        function GetDropDownVal(TypeDel, ddlVal) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "SalesForceList.aspx/GetDropDown",
                data: '{objDDL:' + JSON.stringify(TypeDel) + '}',
                dataType: "json",
                success: function (result) {
                    $('#ddlSrc').empty();
                    //  $('#ddlSrc').append("<option value='0'>--Select--</option>");

                    if (ddlVal == "StateName") {
                        $.each(result.d, function (key, value) {
                            $('#ddlSrc').append($("<option></option>").val(value.State_Code).html(value.StateName));
                        });
                    }
                    else if (ddlVal == "Designation_Name") {
                        $.each(result.d, function (key, value) {
                            $('#ddlSrc').append($("<option></option>").val(value.Designation_Code).html(value.Designation_Name));
                        });
                        //var HQName = $("#ddlSrc").find("select option:selected").text();
                        //$("#ddlSrc option:contains(" + HQName + ")").attr('selected', 'selected');
                    }
                },
                error: function ajaxError(result) {
                    alert("Error");
                }
            });
        }
        function ProcessData() {
            $("#hdnProduct").val($("#ddlSrc option:selected").val());
            return true;
        }
    </script>

    <script type="text/javascript">
        function HidePopup() {

            var popup = $find('PceSelectCustomer');
            popup.hide();
            var popup = $find('PopupControlExtender1');
            popup.hide();
            var popup = $find('PopupControlExtender2');
            popup.hide();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center">Input Details View</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMode" runat="server" CssClass="label" Width="100px">Mode<span style="color:red;padding-left:5px;">*</span></asp:Label>
                                <asp:DropDownList ID="ddlMode" runat="server" CssClass="nice-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged">
                                    <asp:ListItem Text="State" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Subdivision" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Brand" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Advance Search" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblState" runat="server" CssClass="label" Width="100px">State Name<span style="color:red;padding-left:5px;">*</span></asp:Label>
                                <asp:Label ID="lblDiv" runat="server" CssClass="label" Width="100px">Sub Division<span style="color:red;padding-left:5px;">*</span></asp:Label>
                                <asp:Label ID="lblBrand" runat="server" Visible="false" CssClass="label" Width="100px">Brand<span style="color:red;padding-left:5px;">*</span></asp:Label>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="nice-select"
                                    onfocus="this.style.backgroundColor='#E0EE9D'">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlDiv" runat="server" CssClass="nice-select"
                                    onfocus="this.style.backgroundColor='#E0EE9D'">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlBrand" runat="server" Visible="false" CssClass="nice-select"
                                    onfocus="this.style.backgroundColor='#E0EE9D'">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblChkState" runat="server" CssClass="label" Width="100px">State Name<span style="color:red;padding-left:5px;">*</span></asp:Label>

                                <asp:TextBox ID="ddlStateChk" runat="server" Text="" CssClass="input" Width="100%"></asp:TextBox>
                                <%--     <asp:DropDownList ID="ddlStateChk" CssClass="nice-select" Height="25px" Width="145px" onfocus="this.style.backgroundColor='#E0EE9D'" runat="server">
                                <asp:ListItem Text=""></asp:ListItem>
                            </asp:DropDownList>--%>


                                <asp:Panel ID="PnlState" runat="server" Height="185px" Width="365px"
                                    BorderWidth="1px" BorderColor="#d1e2ea" Direction="LeftToRight" BackColor="#f4f8fa" Style="display: none; border-radius: 8px; display: none; width: 420px; height: 185px; overflow: scroll;">
                                    <div style="height: 17px; position: relative; text-transform: capitalize; width: 100%; float: left"
                                        align="right">
                                        <div class="closeLoginPanel">
                                            <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                title="Close">X</a>
                                        </div>
                                    </div>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="CheckState" runat="server" Text="Select/Unselect" ForeColor="Red" SkinID="ddlRequired" onclick="CheckAllState();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList ID="chkState" runat="server" SkinID="ddlRequired" onclick="ClearAllState();">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblChkDiv" runat="server" CssClass="label" Width="100px">Sub Division<span style="color:red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="ddlSubDivChk" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlSubDivChk" Height="25px" onfocus="this.style.backgroundColor='#E0EE9D'" Width="145px" runat="server" CssClass="nice-selet">
                                    <asp:ListItem Text=""></asp:ListItem>
                                </asp:DropDownList>--%>

                                <asp:Panel ID="pnlDiv" runat="server" Height="185px" Width="365px"
                                    BorderWidth="1px" BorderColor="#d1e2ea" Direction="LeftToRight" BackColor="#f4f8fa" Style="display: none; border-radius: 8px; display: none; width: 420px; height: 185px; overflow: scroll;">
                                    <div style="height: 17px; position: relative; text-transform: capitalize; width: 100%; float: left"
                                        align="right">
                                        <div class="closeLoginPanel">
                                            <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                title="Close">X</a>
                                        </div>
                                    </div>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="CheckDiv" runat="server" Text="Select/Unselect" ForeColor="Red" onclick="CheckAllDiv();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList ID="ChkDiv" runat="server" onclick="ClearAllDiv();">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblChkBrand" runat="server" CssClass="label" Width="100px">Brand<span style="color:red;padding-left:5px;">*</span></asp:Label>
                                <%-- <asp:TextBox ID="txtBrandChk" runat="server" SkinID="MandTxtBox" 
                                CssClass="TEXTAREA" Visible="false" Height="22px" Width="108px" ></asp:TextBox>--%>
                                <asp:TextBox ID="ddlBrandChk" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlBrandChk" Height="25px" CssClass="nice-select" onfocus="this.style.backgroundColor='#E0EE9D'" Width="145px" runat="server">
                                    <asp:ListItem Text=""></asp:ListItem>
                                </asp:DropDownList>--%>

                                <asp:Panel ID="pnlBrand" runat="server" Height="185px" Width="365px"
                                    BorderWidth="1px" BorderColor="#d1e2ea" Direction="LeftToRight" BackColor="#f4f8fa" Style="display: none; border-radius: 8px; display: none; width: 420px; height: 185px; overflow: scroll;">
                                    <div style="height: 17px; position: relative; text-transform: capitalize; width: 100%; float: left"
                                        align="right">
                                        <div class="closeLoginPanel">
                                            <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                title="Close">X</a>
                                        </div>
                                    </div>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="checkAll" runat="server" Text="Select/Unselect" ForeColor="Red" onclick="CheckAll();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList ID="ChkBrand" runat="server" onclick="ClearAll();" OnSelectedIndexChanged="ChkBrand_Changed">
                                                    <%-- <asp:ListItem Text="All" Value="0"></asp:ListItem> --%>
                                                </asp:CheckBoxList></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblEff_From" runat="server" CssClass="label"
                                    Width="100px">Effective From<span style="color:red;padding-left:5px;">*</span></asp:Label>
                                <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="nice-select"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblEff_To" runat="server" CssClass="label" Width="100px">Effective To<span style="color:red;padding-left:5px;">*</span></asp:Label>
                                <asp:DropDownList ID="ddlToYear" runat="server" CssClass="nice-select"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:PopupControlExtender ID="PceSelectCustomer" runat="server" TargetControlID="ddlStateChk" OffsetY="2" Enabled="true"
                                PopupControlID="PnlState" Position="Bottom">
                            </asp:PopupControlExtender>
                        </td>
                        <td>
                            <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="ddlSubDivChk"
                                PopupControlID="pnlDiv" Position="Bottom">
                            </asp:PopupControlExtender>
                        </td>
                        <td>
                            <asp:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="ddlBrandChk"
                                PopupControlID="pnlBrand" Position="Bottom">
                            </asp:PopupControlExtender>
                        </td>
                    </tr>
                </table>
                <center>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Width="70px"
                                    Text="View" OnClick="btnSubmit_Onclick" OnClientClick="return Validate_Checkbox();" />
                            </td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnClear" runat="server" CssClass="resetbutton" Width="70px"
                                    Height="25px" Text="Clear" OnClick="btnClear_Onclick" />
                            </td>

                        </tr>
                    </table>
                    <br />
                    <table id="tblState" runat="server" width="100%" align="center" visible="false">
                        <tr style="height: 25px;">

                            <td align="center">
                                <asp:Label ID="lblSt" runat="server" Text="State Name: "></asp:Label>
                                <asp:Label ID="lblStatename" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <table>
                        <tr>
                            <td width="300px"></td>
                            <td width="300px"></td>
                            <td width="300px" align="Right">
                                <%--<asp:Button ID="btnExcel" runat="server" CssClass="savebutton" Width="70px"
                                    Height="25px" Text="Excel" Visible="false" />--%>
                                 <asp:LinkButton ID="btnExcel" ToolTip="Pdf" runat="server" OnClientClick="RefreshParent();"  Visible="false">
                                <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="30px" Style="border-width: 0px;" />
                            </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Panel ID="pnlContents" runat="server">
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">

                                <asp:GridView ID="grdGift" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                    OnRowDataBound="grdGift_RowDataBound" EmptyDataText="No Records Found"
                                    GridLines="None" CssClass="table" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt" AllowSorting="True"
                                    OnRowEditing="grdGift_RowEditing">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Style="text-align: center" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gift_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGiftCode" runat="server" Text='<%#Eval("Gift_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="160px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGiftName" runat="server" Text='<%# Bind("Gift_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="140px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGiftSN" runat="server" Width="60px" Text='<%# Bind("Gift_SName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="140px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGiftType" runat="server" Text='<%# Bind("Gift_Type") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlGiftType" runat="server" CssClass="nice-select">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Literature/Lable" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Special Gift" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Doctor Kit" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Ordinary Gift" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Gift Value">
                                    <ItemStyle Width="70px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGiftVal" runat="server" Text='<%# Bind("Gift_Value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Effective From" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFrom" runat="server" Style="text-align: center" Text='<%# Bind("Gift_Effective_From") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Effective To" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTo" runat="server" Style="text-align: center" Text='<%# Bind("Gift_Effective_To") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mode" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMode" runat="server" Style="text-align: center" ForeColor="Blue" Text='<%# Bind("mode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area"  />
                                </asp:GridView>
                            </div>
                        </div>

                    </asp:Panel>
                </center>
                <div class="row justify-content-center">
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back"
                        OnClick="btnBack_Click" />
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
