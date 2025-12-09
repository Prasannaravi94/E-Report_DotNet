<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Assesment_MGR.aspx.cs" Inherits="Assesment_MGR" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Interview Assesment MGR</title>
    <%--<link type="text/css" rel="stylesheet" href="css/style.css" />--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/style.css" />
    <link rel="stylesheet" href="assets/css/responsive.css" />
    <link rel="stylesheet" href="assets/css/Calender_CheckBox.css" />
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
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .TEXTAREA {
        }

        .display-reporttable #GrdSale th:first-child {
            border-radius: 8px 0 0 8px;
            background-color: #F1F5F8;
            color: #636d73;
            font-size: 12px;
            font-weight: 400;
            border-left: 0px solid #F1F5F8;
        }

        .display-reporttable #GrdSale tr:nth-child(2) td:first-child {
            background-color: white;
            color: #636d73;
        }

        #grdDCR input[type="checkbox"] + label, #grdDCR input[type="checkbox"]:checked + label {
            color: white;
        }

        #tblAsses tr td:nth-child(3) {
            min-width: 100px;
        }
          #tblalign .single-des,#tblalign1 .single-des,#tblAsses .single-des{
            margin-bottom:2px;
        }
    </style>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>--%>

    <%--  <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript">
        function checkPoor() {
            var grid = document.getElementById('<%= grdDCR.ClientID %>');

            if (grid != null) {

                var inputList = grid.getElementsByTagName("input");
                //                var chkrejall = document.getElementById('grdDCR_ctl01_chkRejAll');
                //                var chkall = document.getElementById('grdDCR_ctl01_chkAll');
                var cnt = 0;
                var index = '';
                var Count = 0;
                var CountVisi = 0;

                for (i = 2; i < inputList.length; i++) {

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {

                        index = i.toString();
                    }

                    var chkPoor = document.getElementById('grdDCR_ctl' + index + '_chkPoor');
                    var chkAve = document.getElementById('grdDCR_ctl' + index + '_chkAVERAGE');
                    var chkGood = document.getElementById('grdDCR_ctl' + index + '_chkgood');

                    if (chkPoor.checked) {

                        //                        document.getElementById("btnReject").style.visibility = "hidden";
                        //                        document.getElementById("btnApprove").style.visibility = "visible";

                        CountVisi = CountVisi + 1;
                        chkAve.checked = false;
                        chkGood.checked = false;
                    }

                    else {
                        Count = Count + 1;
                    }


                    //chkrejall.checked = false;

                    //                    if (Count > 0) {
                    //                        chkall.checked = false;
                    //                    }
                    //                    else {
                    //                        chkall.checked = true;
                    //                    }
                    if (CountVisi == 0) {
                        // document.getElementById("btnApprove").style.visibility = "hidden";
                    }
                }
            }
        }
        //checGood
        function checkAverage() {
            var grid = document.getElementById('<%= grdDCR.ClientID %>');

            if (grid != null) {


                var inputList = grid.getElementsByTagName("input");
                //                var chkrejall = document.getElementById('grdDCR_ctl01_chkRejAll');
                //                var chkall = document.getElementById('grdDCR_ctl01_chkAll');
                var cnt = 0;
                var index = '';
                var Count = 0;
                var CountVisi = 0;

                for (i = 2; i < inputList.length; i++) {

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {

                        index = i.toString();
                    }

                    var chkPoor = document.getElementById('grdDCR_ctl' + index + '_chkPoor');
                    var chkAve = document.getElementById('grdDCR_ctl' + index + '_chkAVERAGE');
                    var chkGood = document.getElementById('grdDCR_ctl' + index + '_chkgood');
                    if (chkAve.checked) {

                        //                        document.getElementById("btnReject").style.visibility = "visible";
                        //                        document.getElementById("btnApprove").style.visibility = "hidden";

                        CountVisi = CountVisi + 1;
                        chkPoor.checked = false;
                        chkGood.checked = false;
                    }

                    else {

                        Count = Count + 1;
                    }

                    // chkall.checked = false;

                    //                    if (Count > 0) {
                    //                        chkrejall.checked = false;
                    //                    }
                    //                    else {
                    //                        chkrejall.checked = true;
                    //                    }

                    if (CountVisi == 0) {
                        //document.getElementById("btnReject").style.visibility = "hidden";
                    }

                }
            }
        }



        function checGood() {
            var grid = document.getElementById('<%= grdDCR.ClientID %>');

            if (grid != null) {


                var inputList = grid.getElementsByTagName("input");
                //                var chkrejall = document.getElementById('grdDCR_ctl01_chkRejAll');
                //                var chkall = document.getElementById('grdDCR_ctl01_chkAll');
                var cnt = 0;
                var index = '';
                var Count = 0;
                var CountVisi = 0;

                for (i = 2; i < inputList.length; i++) {

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {

                        index = i.toString();
                    }

                    var chkPoor = document.getElementById('grdDCR_ctl' + index + '_chkPoor');
                    var chkAve = document.getElementById('grdDCR_ctl' + index + '_chkAVERAGE');
                    var chkGood = document.getElementById('grdDCR_ctl' + index + '_chkgood');

                    if (chkGood.checked) {

                        //                        document.getElementById("btnReject").style.visibility = "visible";
                        //                        document.getElementById("btnApprove").style.visibility = "hidden";

                        CountVisi = CountVisi + 1;
                        chkPoor.checked = false;
                        chkAve.checked = false;
                    }

                    else {

                        Count = Count + 1;
                    }

                    //chkall.checked = false;

                    //                    if (Count > 0) {
                    //                        chkrejall.checked = false;
                    //                    }
                    //                    else {
                    //                        chkrejall.checked = true;
                    //                    }

                    if (CountVisi == 0) {
                        //document.getElementById("btnReject").style.visibility = "hidden";
                    }

                }
            }
        }
    </script>
    <script type="text/javascript" src="JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="JsFiles/jquery-1.10.1.js"></script>
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
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <%--   <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>

  <%--  <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />--%>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/jquery-1.12.4.js"></script>
    <script src="//code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

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
                //                minDate: new Date(year, 11, 1),
                //                maxDate: new Date(year, 11, 31),

                dateFormat: 'dd/mm/yy'
                //                yearRange: "2010:2017",
            });
            // j('.ui-datepicker').addClass('notranslate');
        });
    </script>


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
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
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });

            $('#btnSubmit').click(function () {

                if ($("#txtName").val() == "") { alert("Enter Name."); $('#txtName').focus(); return false; }
                if ($("#txtEffFrom").val() == "") { alert("Enter From Date."); $('#txtEffFrom').focus(); return false; }
                if ($("#txtToDate").val() == "") { alert("Enter To Date."); $('#txtEffFrom').focus(); return false; }

                var FrmDate = document.getElementById('<%=txtEffFrom.ClientID%>').value;
                var ToDate = document.getElementById('<%=txtToDate.ClientID%>').value;
            });
        });
    </script>

    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>

            <table width="100%" runat="server" id="tblclose" visible="false">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 50px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label29" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">

                        <div align="center">
                            <asp:Label ID="lblhead" runat="server" Text="Interview Assesment MGR" Visible="false" CssClass="reportheader"></asp:Label>
                        </div>
                        <br />
                        <br />
                        <div align="center" style="overflow-x: auto">
                            <table align="center" border="0" id="tblAsses">

                                <tr>
                                    <td align="left" width="200px">
                                        <asp:Label ID="lblName" runat="server" Text="NAME " Width="200px" ForeColor="#414d55" Font-Bold="true" CssClass="label">NAME<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                    </td>
                                    <td align="center" width="2px">
                                        <asp:Label ID="Label18" runat="server" Text=": " Font-Size="6pt" Font-Bold="false" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td align="left" width="178px">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtName" runat="server" MaxLength="50"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                TabIndex="27" Width="178px" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtNamelbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                    <td width="30px"></td>
                                    <td width="30px"></td>
                                    <td align="left" width="200px">
                                        <asp:Label ID="lblPost" runat="server" Text="POST" Width="200px" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="center" width="2px">
                                        <asp:Label ID="Label23" runat="server" Text=": " Font-Size="6pt" Font-Bold="false" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td align="left" width="178px">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtPost" runat="server" MaxLength="50"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                Width="178px" TabIndex="27" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtPostlbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td align="left" width="200px">
                                        <asp:Label ID="lblQual" runat="server" Text="QUALIFICATION" Width="200px" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="center" width="2px">
                                        <asp:Label ID="Label19" runat="server" Text=": " Font-Size="6pt" Font-Bold="false" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td align="left" width="178px">
                                        <div class="single-des">
                                            <asp:TextBox ID="TxtQual" runat="server" MaxLength="50"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                Width="178px" TabIndex="27" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="TxtQuallbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                    <td width="30px"></td>
                                    <td width="30px"></td>
                                    <td align="left" width="200px">
                                        <asp:Label ID="lblExp" runat="server" Text="TOTAL PHARMA EXPERIENCE" Width="200px" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="center" width="2px">
                                        <asp:Label ID="Label24" runat="server" Text=": " Font-Size="6pt" Font-Bold="false" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td align="left" width="178px">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtExp" runat="server" MaxLength="12" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                onblur="this.style.backgroundColor='#f4f8fa'" Width="178px" TabIndex="28" CssClass="input"
                                                onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtExplbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td align="left" width="200px">
                                        <asp:Label ID="lblage" runat="server" Text="AGE" Width="200px" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="center" width="2px">
                                        <asp:Label ID="Label20" runat="server" Text=": " Font-Size="6pt" Font-Bold="false" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td align="left" width="178px">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtage" runat="server" MaxLength="2" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                onblur="this.style.backgroundColor='#f4f8fa'" Width="178px" TabIndex="28" CssClass="input"
                                                onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtagelbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                    <td width="30px"></td>
                                    <td width="30px"></td>
                                    <td align="left" width="200px">
                                        <asp:Label ID="lblmarital" runat="server" Text="MARITAL STATUS" Width="200px" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="center" width="2px">
                                        <asp:Label ID="Label25" runat="server" Text=": " Font-Size="6pt" Font-Bold="false" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td align="left" width="178px">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtmarital" runat="server" MaxLength="50"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                Width="178px" TabIndex="27" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtmaritallbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td align="left" width="200px">
                                        <asp:Label ID="lbltwowheel" runat="server" Text="TWO WHEELER" Width="200px" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="center" width="2px">
                                        <asp:Label ID="Label21" runat="server" Text=": " Font-Size="6pt" Font-Bold="false" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td width="178px" align="left">
                                        <asp:DropDownList ID="ddltwowheel" runat="server" onblur="this.style.backgroundColor='White'"
                                            onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired">
                                            <%--<asp:ListItem Selected="True" Text="---Select---" Value="0"></asp:ListItem>--%>
                                            <asp:ListItem Text="YES" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="NO" Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="ddltwowheellbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                    <td width="30px"></td>
                                    <td width="30px"></td>
                                    <td align="left" width="200px">
                                        <asp:Label ID="lbluan" runat="server" Text="UAN NO" Width="200px" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="center" width="2px">
                                        <asp:Label ID="Label26" runat="server" Text=": " Font-Size="6pt" Font-Bold="false" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td align="left" width="178px">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtuan" runat="server" MaxLength="50"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                Width="178px" TabIndex="27" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtuanlbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td align="left" width="200px">
                                        <asp:Label ID="lblHq" runat="server" Text="HQ" Width="200px" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="center" width="2px">
                                        <asp:Label ID="Label22" runat="server" Text=": " Font-Size="6pt" Font-Bold="false" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td align="left" width="178px">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtHq" runat="server" MaxLength="50"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                Width="178px" TabIndex="27" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtHqlbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                    <td width="30px"></td>
                                    <td width="30px"></td>
                                    <td align="left" width="200px">
                                        <asp:Label ID="lblMob" runat="server" Text="MOB NO" Width="200px" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="center" width="2px">
                                        <asp:Label ID="Label27" runat="server" Text=": " Font-Size="6pt" Font-Bold="false" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td align="left" width="178px">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtMbl" runat="server" MaxLength="12" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                onblur="this.style.backgroundColor='#f4f8fa'" Width="178px" TabIndex="28" CssClass="input"
                                                onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtMbllbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>


                            </table>
                        </div>
                        <br />
                        <center>
                            <div class="display-reporttable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin">

                                    <asp:GridView ID="grdDCR" runat="server" Width="75%" HorizontalAlign="Center"
                                        CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                                        CssClass="table" GridLines="None">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-Width="30"
                                                ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    <asp:Label ID="lblSNolbl" runat="server" Text='<%# Bind("parameter") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PARAMETERS" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle Width="290px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParam" runat="server" Text='<%# Bind("parameter") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="POOR" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle Width="60px" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPoor" runat="server" onclick="checkPoor(this); " Text="." />
                                                    <asp:Label ID="chkPoorlbl" runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AVERAGE" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle Width="60px" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAVERAGE" runat="server" onclick="checkAverage(this);" Text="." />
                                                    <asp:Label ID="chkAVERAGElbl" runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GOOD" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle Width="60px" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkgood" runat="server" onclick="checGood(this);" Text="." />
                                                    <asp:Label ID="chkgoodlbl" runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </center>

                        <div>
                            <table align="left" id="tblalign1">
                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="lblEMail" runat="server" Text="EMAIL ID OF THE PREVIOUS COMPANY " ForeColor="#414d55" Font-Bold="true"
                                            CssClass="label"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtEMail" runat="server" Width="168px" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                onblur="this.style.backgroundColor='#f4f8fa'" TabIndex="20" CssClass="input"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtEMaillbl" runat="server"
                                            CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="lblSale" runat="server" Text="SALE GENERATED IN THE PREVIOUS COMPANY (PER MONTH)"
                                            ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtSale_Pre" runat="server" MaxLength="12"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                Width="163px" TabIndex="28" CssClass="input" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtSale_Prelbl" runat="server" Visible="false" CssClass="label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="lblPerfo" runat="server" Text="PERFORMANCE IN PREVIOUS COMPANY(%)"
                                            ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtPerfo" runat="server" MaxLength="12" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                onblur="this.style.backgroundColor='#f4f8fa'" Width="163px" TabIndex="28" CssClass="input"
                                                onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtPerfolbl" runat="server" Visible="false" CssClass="label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="lblCur_Sale" runat="server" Text="CURRENT AVG.SEC.SALE(TRITON)" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtCur_Sale" runat="server" MaxLength="12"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                Width="163px" TabIndex="28" CssClass="input" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtCur_Salelbl" runat="server" Visible="false" CssClass="label"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div align="left">
                            <table>
                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="PROJECTED SALE "
                                            CssClass="reportheader" Font-Size="16px" Width="403px" Height="18px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="display-reporttable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin">
                                <asp:GridView ID="GrdSale" runat="server" Width="75%" HorizontalAlign="Center"
                                    CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                                    CssClass="table" GridLines="None">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Month" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10px">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSale" runat="server" Text="Sec.Sale"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="1" ItemStyle-HorizontalAlign="center">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="txtmonth1lbl" runat="server" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtmonth1" runat="server" Width="70px" CssClass="input"></asp:TextBox>
                                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                    ValidChars="." TargetControlID="txtmonth1">
                                </asp:FilteredTextBoxExtender>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="2" ItemStyle-HorizontalAlign="center">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="txtmonth2lbl" runat="server" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtmonth2" runat="server" Width="70px" CssClass="input"></asp:TextBox>
                                                <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom"
                                    ValidChars="." TargetControlID="txtmonth2">
                                </asp:FilteredTextBoxExtender>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="3" ItemStyle-HorizontalAlign="center">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="txtmonth3lbl" runat="server" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtmonth3" runat="server" Width="70px" CssClass="input"></asp:TextBox>
                                                <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom"
                                    ValidChars="." TargetControlID="txtmonth3">
                                </asp:FilteredTextBoxExtender>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="4" ItemStyle-HorizontalAlign="center">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="txtmonth4lbl" runat="server" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtmonth4" runat="server" Width="70px" CssClass="input"></asp:TextBox>
                                                <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom"
                                    ValidChars="." TargetControlID="txtmonth4">
                                </asp:FilteredTextBoxExtender>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="5" ItemStyle-HorizontalAlign="center">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="txtmonth5lbl" runat="server" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtmonth5" runat="server" Width="70px" CssClass="input"></asp:TextBox>
                                                <%--  <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom"
                                    ValidChars="." TargetControlID="txtmonth5">
                                </asp:FilteredTextBoxExtender>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="6" ItemStyle-HorizontalAlign="center">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="txtmonth6lbl" runat="server" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtmonth6" runat="server" Width="70px" CssClass="input"></asp:TextBox>
                                                <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, Custom"
                                    ValidChars="." TargetControlID="txtmonth6">
                                </asp:FilteredTextBoxExtender>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
                            </div>
                        </div>


                        <div>
                            <table align="left" id="tblalign">
                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="lblreason" runat="server" Text="REASON FOR CHANGING" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtReason" runat="server" MaxLength="50" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                onblur="this.style.backgroundColor='#f4f8fa'" Width="163px" TabIndex="27" CssClass="input"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtReasonlbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="lblL_Salary" runat="server" Text="LAST SALARY DRAWN (CTC) " ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtL_Salary" runat="server" MaxLength="12"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                Width="163px" TabIndex="28" CssClass="input" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtL_Salarylbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="lblmin_sal" runat="server" Text="MINIMUM SALARY EXPECTED(CTC)" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtmin_sal" runat="server" MaxLength="12"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                Width="163px" TabIndex="28" CssClass="input" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="txtmin_sallbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="lblJdate" runat="server" Text="JOINING DATE " ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                        <asp:TextBox ID="txtJ_Date" runat="server"  CssClass="DOBfROMDate input"
                                            onkeypress="Calendar_enter(event);" onfocus="this.style.backgroundColor='#E0EE9D'"
                                            onblur="this.style.backgroundColor='#f4f8fa'" Width="163px" TabIndex="6"></asp:TextBox>
                                            </div>
                                        <asp:Label ID="txtJ_Datelbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="lblCover" runat="server" Text="Covering Areas" ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                        <asp:TextBox ID="txtCover" runat="server"  MaxLength="50" onfocus="this.style.backgroundColor='LavenderBlush'"
                                            onblur="this.style.backgroundColor='#f4f8fa'" Width="163px" TabIndex="27" CssClass="input"
                                            onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                            </div>
                                        <asp:Label ID="txtCoverlbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="Label7" runat="server" Text="INDUCTION BY " ForeColor="#414d55" Font-Bold="true" CssClass="label"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                        <asp:TextBox ID="txtIndu" runat="server" MaxLength="50" onfocus="this.style.backgroundColor='LavenderBlush'"
                                            onblur="this.style.backgroundColor='#f4f8fa'" Width="163px" TabIndex="27" CssClass="input"
                                            onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                            </div>
                                        <asp:Label ID="txtIndulbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style76"></td>
                                    <td width="80px"></td>
                                    <td width="80px"></td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="lblfrmdate" runat="server" Text="INDUCTION FROM " ForeColor="#414d55" Font-Bold="true" CssClass="label">INDUCTION FROM <span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                        <asp:TextBox ID="txtEffFrom" runat="server" CssClass="DOBfROMDate input"
                                            onkeypress="Calendar_enter(event);" onfocus="this.style.backgroundColor='#E0EE9D'"
                                            onblur="this.style.backgroundColor='#f4f8fa'" Width="163px" TabIndex="6"></asp:TextBox>
                                            </div>
                                        <asp:Label ID="txtEffFromlbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                    <td class="style76"></td>
                                    <td align="left">
                                        <asp:Label ID="lbltodate" runat="server" Text="TO" ForeColor="#414d55" Font-Bold="true" CssClass="label">TO<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                        <asp:TextBox ID="txtToDate" runat="server"  CssClass="DOBfROMDate input"
                                            onkeypress="Calendar_enter(event);" onfocus="this.style.backgroundColor='#E0EE9D'"
                                            onblur="this.style.backgroundColor='#f4f8fa'" Width="163px" TabIndex="6"></asp:TextBox>
                                            </div>
                                        <asp:Label ID="txtToDatelbl" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                   
                                      <td colspan="6" align="right">
                                        <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Save"
                                            CssClass="savebutton" OnClick="btnSubmit_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />


                        <center>
                            <table id="tbl" runat="server" width="90%" visible="false">

                                <table width="70%">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label3" CssClass="label" ForeColor="#414d55" Font-Bold="true" runat="server">Date:</asp:Label>
                                            <asp:Label ID="lblSubmitDate" CssClass="label" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label4" CssClass="label" ForeColor="#414d55" Font-Bold="true" runat="server">(SIGNATURE & Name Of Interviewer)</asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label28" CssClass="label" ForeColor="#414d55" Font-Bold="true" runat="server">Senders Name:</asp:Label>
                                            <asp:Label ID="lblSenderName" CssClass="label" runat="server"></asp:Label>
                                        </td>

                                        <td align="right"></td>

                                    </tr>
                                    <tr>
                                        <td height="20px"></td>
                                    </tr>

                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label15" CssClass="label" ForeColor="#414d55" Font-Bold="true" runat="server">(VICE-PRESIDENT)</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label16" CssClass="label" ForeColor="#414d55" Font-Bold="true" runat="server">(PRESIDENT)</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label17" CssClass="label" ForeColor="#414d55" runat="server" Font-Bold="true">(DIRECTORS)</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label5" CssClass="label" ForeColor="#414d55" runat="server" Font-Bold="true" Font-Underline="true">Note:</asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label10" CssClass="label" ForeColor="#414d55" runat="server" Font-Bold="true">To be attached the following for Appoinment letter</asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label6" CssClass="label" runat="server">1. Resume</asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label8" CssClass="label" runat="server">2. New Joinee Kit Requisition Format</asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label9" CssClass="label" runat="server">3. Latest Salary slip of the previous company</asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label11" CssClass="label" runat="server">4. Appoinment letter of the previous company with salary breakup</asp:Label>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label13" CssClass="label" runat="server">5. Aadhar Copy</asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label14" CssClass="label" runat="server">6. Vehicle RC Book Copy(First Page)</asp:Label>
                                        </td>

                                    </tr>
                                </table>

                            </table>
                        </center>
                    </div>
                </div>
            </div>
            <br />
            <br />

        </div>
    </form>
</body>
</html>
