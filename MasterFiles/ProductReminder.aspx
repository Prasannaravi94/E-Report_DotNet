<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductReminder.aspx.cs"
    Inherits="MasterFiles_ProductReminder" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Details</title>
    <style type="text/css">
        #Table1 {
            margin-left: 370px;
        }

        #tblLocationDtls {
            margin-left: 370px;
        }

        #Submit {
        }

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
        }

        .style1 {
            width: 240px;
        }

        .style2 {
            height: 10px;
            width: 240px;
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
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            //$('input:text:first').focus();
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
                            $('#Submit').focus();
                        }
                    }
                }
            });
            $('#Submit').click(function () {
                var todayDate = new Date();
                var dd = todayDate.getDate();
                var mm = todayDate.getMonth() + 01; //January is 0!
                var yyyy = todayDate.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
                }
                var today = dd + '/' + mm + '/' + yyyy;

                var dtDate = document.getElementById("txtEffTo").value;


                if ($("#txtGift_SName").val() == "") { createCustomAlert("Enter Short Name."); $('#txtGift_SName').focus(); return false; }
                if ($("#txtGiftName").val() == "") { createCustomAlert("Enter Name."); $('#txtGiftName').focus(); return false; }

                var cat = $('#<%=ddlGiftType.ClientID%> :selected').text();
                if (cat == "--Select--") { createCustomAlert("Enter Type."); $('#ddlGiftType').focus(); return false; }
                if ($("#txtGiftValue").val() == "") { createCustomAlert("Enter Gift Value."); $('#txtGiftValue').focus(); return false; }
                if ($("#txtEffFrom").val() == "") { createCustomAlert("Enter Effective From Date."); $('#txtEffFrom').focus(); return false; }
                if ($("#txtEffTo").val() == "") { createCustomAlert("Enter Effective To Date."); $('#txtEffTo').focus(); return false; }

                //  if ($('#chkboxLocation input:checked').length > 0) { return true; } else { createCustomAlert('Select State'); return false; }
                // if ($('#chkSubdiv input:checked').length > 0) { return true; } else { alert('Select Subdivision'); return false; }

                if ($('#chkboxLocation input:checked').length == 0) {
                    createCustomAlert('Please Select State'); return false;
                }
                if (!CheckBoxSelectionValidation()) {
                    return false;
                }
                if ($('#chkBrand input:checked').length == 0) {
                    createCustomAlert('Please Select Brand'); return false;
                }
                //                if (dtDate < today) {
                //                    createCustomAlert('select Valid Effective To Date'); return false;
                //                }
            });
        });
    </script>
    <script type="text/javascript" language="javascript">

        function CheckBoxSelectionValidation() {
            var count = 0;
            var objgridview = document.getElementById('<%= chkSubdiv.ClientID %>');

            for (var i = 0; i < objgridview.getElementsByTagName("input").length; i++) {

                var chknode = objgridview.getElementsByTagName("input")[i];

                if (chknode != null && chknode.type == "checkbox" && chknode.checked) {
                    count = count + 1;
                }
            }

            if (count == 0) {
                var txt = "Please Select Sub Division";
                createCustomAlert(txt);

                return false;
            }
            else {
                return true;
            }
        }

    </script>
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <%--<link type="text/css" rel="Stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        var today = new Date();
        var lastDate = new Date(today.getFullYear(), today.getMonth(0) - 1, 31);
        var year = today.getFullYear() - 1;


        var dd = today.getDate();
        var mm = today.getMonth() + 01; //January is 0!
        var yyyy = today.getFullYear();

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.DOBDate').datepicker
            ({
                minDate: dd + '/' + mm + '/' + yyyy,
                //                minDate: new Date(year, 11, 1),
                //                maxDate: new Date(year, 11, 31),

                dateFormat: 'dd/mm/yy'
                //                yearRange: "2010:2017",
            });

            j('.DOBfROMDate').datepicker
            ({
                //                                minDate: new Date(year, 11, 1),
                //                                maxDate: new Date(year, 11, 31),

                dateFormat: 'dd/mm/yy'
                //                yearRange: "2010:2017",
            });
        });
    </script>
    <script type="text/javascript">
        function CheckAllState() {
            var intIndex = 0;
            var rowCount = document.getElementById('chkboxLocation').getElementsByTagName("input").length;

            for (intIndex = 0; intIndex < rowCount; intIndex++) {
                if (document.getElementById('CheckState').checked == true) {
                    if (document.getElementById("chkboxLocation" + "_" + intIndex)) {
                        if (document.getElementById("chkboxLocation" + "_" + intIndex).disabled != true)
                            document.getElementById("chkboxLocation" + "_" + intIndex).checked = true;
                    }
                }
                else {
                    if (document.getElementById("chkboxLocation" + "_" + intIndex)) {
                        if (document.getElementById("chkboxLocation" + "_" + intIndex).disabled != true)
                            document.getElementById("chkboxLocation" + "_" + intIndex).checked = false;
                    }
                }
            }
        }

        function ClearAllState() {
            var intIndex = 0;
            var flag = 0;

            var rowCount = document.getElementById('chkboxLocation').getElementsByTagName("input").length;
            for (intIndex = 0; intIndex < rowCount; intIndex++) {
                if (document.getElementById("chkboxLocation" + "_" + intIndex)) {
                    if (document.getElementById("chkboxLocation" + "_" + intIndex).checked == true) {
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
        }

        function CheckAllBrand() {
            var intIndex = 0;
            var rowCount = document.getElementById('chkBrand').getElementsByTagName("input").length;

            for (intIndex = 0; intIndex < rowCount; intIndex++) {
                if (document.getElementById('CheckBrand').checked == true) {
                    if (document.getElementById("chkBrand" + "_" + intIndex)) {
                        if (document.getElementById("chkBrand" + "_" + intIndex).disabled != true)
                            document.getElementById("chkBrand" + "_" + intIndex).checked = true;
                    }
                }
                else {
                    if (document.getElementById("chkBrand" + "_" + intIndex)) {
                        if (document.getElementById("chkBrand" + "_" + intIndex).disabled != true)
                            document.getElementById("chkBrand" + "_" + intIndex).checked = false;
                    }
                }
            }
        }


        function ClearAllBrand() {
            var intIndex = 0;
            var flag = 0;

            var rowCount = document.getElementById('chkBrand').getElementsByTagName("input").length;
            for (intIndex = 0; intIndex < rowCount; intIndex++) {
                if (document.getElementById("chkBrand" + "_" + intIndex)) {
                    if (document.getElementById("chkBrand" + "_" + intIndex).checked == true) {
                        flag = 1;
                    }
                    else {
                        flag = 0;
                        break;
                    }
                }
            }
            if (flag == 0)
                document.getElementById('CheckBrand').checked = false;
            else
                document.getElementById('CheckBrand').checked = true;
        }
    </script>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" style="border-style: none;">Input Details</h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblGift_SName" runat="server" CssClass="label">Short Name<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtGift_SName"
                                    onblur="this.style.backgroundColor='White'" TabIndex="1" Width="100%" runat="server" MaxLength="15"
                                    CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                </asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblGiftName" runat="server" CssClass="label">Name<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtGiftName" CssClass="input" Width="100%"
                                    onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server"
                                    onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                </asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblGiftType" runat="server" CssClass="label">Type<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                <asp:DropDownList ID="ddlGiftType" runat="server" CssClass="nice-select" TabIndex="4" Width="100%">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Literature/Lable" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Special Gift" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Doctor Kit" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Ordinary Gift" Value="4"></asp:ListItem>
                                    
                                    <asp:ListItem Text="HVG" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Paper Gift" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Self" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="LVG" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="MVG" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Others" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Gift" Value="11"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblGiftValue" runat="server" CssClass="label">Value(in RS)<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtGiftValue" onkeypress="CheckNumeric(event);"
                                    onblur="this.style.backgroundColor='White'" Width="100%"
                                    TabIndex="5" runat="server" CssClass="input">
                                </asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblEffFrom" runat="server" CssClass="label">Effective From<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input"
                                    onkeypress="Calendar_enter(event);" Width="100%"
                                    onblur="this.style.backgroundColor='White'" TabIndex="6"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffFrom" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
                                <%--<asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom"
                                            runat="server" />--%>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblEffTo" runat="server" CssClass="label">Effective To<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtEffTo" runat="server" CssClass="input"
                                    onkeypress="Calendar_enter(event);" Width="100%"
                                    TabIndex="7" onblur="this.style.backgroundColor='White'" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEffTo" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
                                <%--   <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEffTo"
                                    runat="server" />--%>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back"
                        OnClick="btnBack_Click" />
                </div>
                <div class="row justify-content-center" style="text-align: center;">
                    <asp:Label ID="lblTitle_LocationDtls" runat="server" Text="Select the State/Location"
                        TabIndex="8"></asp:Label>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-7">
                        <asp:CheckBox ID="CheckState" runat="server" Text="All" onclick="CheckAllState();" />
                    </div>
                </div>
                <div class="row justify-content-center" style="overflow-x: auto;">
                    <div class="col-lg-7">
                        <asp:CheckBoxList ID="chkboxLocation" runat="server" DataTextField="State_Name" DataValueField="State_Code"
                            RepeatColumns="4" RepeatDirection="vertical"
                            Width="500px" TabIndex="9" onclick="ClearAllState();">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: center;">
                    <asp:Label ID="lbldivision" runat="server" Width="210px" Text="Select the Sub Division" TabIndex="6"></asp:Label>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-7">
                        <asp:CheckBoxList ID="chkSubdiv" runat="server" DataTextField="subdivision_name"
                            DataValueField="subdivision_code" RepeatDirection="Vertical" RepeatColumns="4"
                            Width="500px">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: center;">
                    <asp:Label ID="Label1" runat="server" Width="210px" Text="Select the Division" TabIndex="6"></asp:Label>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-7">
                        <asp:CheckBoxList ID="chkDivision" runat="server" DataTextField="Division_Name"
                            DataValueField="Division_Code" RepeatDirection="Vertical" RepeatColumns="2"
                            Width="500px">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="row justify-content-center" style="text-align: center;">
                    <asp:Label ID="Label2" runat="server" Width="210px" Text="Select the Brand" TabIndex="6"></asp:Label>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-7">
                        <asp:CheckBox ID="CheckBrand" runat="server" Text="All" onclick="CheckAllBrand();" />
                    </div>
                </div>
                <div class="row justify-content-center" style="overflow-x: auto;">
                    <div class="col-lg-7">
                        <asp:CheckBoxList ID="chkBrand" runat="server" DataTextField="Brand_Name"
                            DataValueField="Brand" RepeatDirection="Vertical" RepeatColumns="2"
                            Width="500px" onclick="ClearAllBrand();">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="row justify-content-center">
                    <asp:Button ID="Submit" runat="server" CssClass="savebutton" Width="60px"
                        Text="Save" OnClick="Submit_Click" />
                </div>
            </div>
            <div class="single-des clearfix">
                <asp:CheckBox ID="chkView" Text="Entry" Checked="true" runat="server" />
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
