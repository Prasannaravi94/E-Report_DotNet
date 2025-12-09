<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doc_Qua_Trans.aspx.cs" Inherits="MasterFiles_Doc_Qua_Trans" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Doctor Qualification</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />
    <link rel="stylesheet" href="../assets/css/responsive.css" />

    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" />

    <script src="../assets/js/jQuery.min.js" type="text/javascript"></script>
    <script src="../assets/js/popper.min.js" type="text/javascript"></script>
    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/js/jquery.nice-select.min.js" type="text/javascript"></script>
    <script src="../assets/js/main.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />

    <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
            $('#btnTransfer').click(function () {

                var To = $('#<%=ddlTrans_To.ClientID%> :selected').text();
                if (To == "--Select--") { alert("Select Transfer To"); $('#ddlTrans_To').focus(); return false; }

            });
        });
    </script>
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

        .clp {
            border-collapse: collapse;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .display-table .table tr:nth-child(2) td:first-child, .display-table .table th:first-child {
            background-color: #f1f5f8;
            color: #636d73;
        }

        .display-table .table tr td:first-child {
            border-top: 1px solid #DCE2E8;
        }

        .selectpicker > ul {
            display: none;
        }

        .selectpicker > span {
            display: none;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .bootstrap-select:not([class*="col-"]):not([class*="form-control"]):not(.input-group-btn) {
            width: 295px !important;
            color: #90a1ac;
            font-size: 14px !important;
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            padding-left: 3px;
            padding-right: 3px;
        }

        .bootstrap-select > .dropdown-toggle.bs-placeholder {
            background-color: #f4f8fa;
        }

        .bootstrap-select .nice-select {
            display: none;
        }

        .dropdown-menu > li > a:hover, .dropdown-menu > li > a:focus {
            background-color: transparent;
            color: white;
            background-image: linear-gradient(to top, #0496ff 0%, #28b5e0 100%);
        }

        #bs-select-1 {
            scrollbar-width: thin;
        }

        .single-des input {
            color: white;
            height: 32px;
            padding-left: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
            rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
        <%--<link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
            rel="stylesheet" type="text/css" />--%>
        <%--<script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
            type="text/javascript"></script>--%>
        <script type="text/javascript">
            $(function () {
                $('[id*=lstQual]').multiselect({
                    includeSelectAllOption: true
                });

                $('.selectpicker').removeClass('nice-select');
            });
        </script>


        <br />
        <br />
        <div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <h2 class="text-center">Transfer Qualification</h2>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblTrans_From" runat="server" CssClass="label">Transfer From<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                            <%--  <asp:DropDownList ID="ddlTrans_From" runat="server"  onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired" AutoPostBack="true"
                            onselectedindexchanged="ddlTrans_From_SelectedIndexChanged" 
                            TabIndex="2" >--%>


                            <div style="float: left; width: 80%;">
                                <asp:ListBox ID="lstQual" runat="server" SelectionMode="Multiple" CssClass="selectpicker" data-live-search="true"></asp:ListBox>
                            </div>
                            <div style="float: right;">
                                <asp:Button ID="btngo" runat="server" Text="Go" CssClass="savebutton"
                                    Width="60px" OnClick="btngo_Click" />
                            </div>
                        </div>


                        <div class="single-des clearfix">
                            <asp:Label ID="lblQual" runat="server" CssClass="label" Visible="false">Selected Qualification</asp:Label>
                            <asp:Label ID="lblselect" runat="server" Visible="false" CssClass="label" Font-Size="14px" Font-Bold="true" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblTrans_To" runat="server" CssClass="label">Transfer To<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                            <asp:DropDownList ID="ddlTrans_To" runat="server" CssClass="nice-select"
                                SkinID="ddlRequired"
                                TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:CheckBox ID="Chkdelete" runat="server"
                                Text=" 'Delete' After Transfer" />
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:HiddenField ID="txtconformmessageValue" runat="server" />
                            <asp:Button ID="btnTransfer" runat="server" CssClass="savebutton" Text="Transfer - Qualification" Width="180px" OnClick="btnTransfer_Click" />

                        </div>
                    </div>

                </div>
                <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
            </div>
            <br />
            <center>
                <asp:Panel ID="pnlCount" runat="server" Visible="false">
                    <div class="display-table clearfix">
                        <div class="table-responsive">
                            <asp:Table ID="Table1" runat="server" Width="300px" CssClass="table"
                                CellSpacing="3" CellPadding="3">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell ColumnSpan="2">
                                        <asp:Label ID="lbltrans" runat="server" Text="Transaction Available" Font-Bold="true"></asp:Label>
                                    </asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="80px" HorizontalAlign="Left">
                                        <asp:Label ID="lblListed" runat="server" Text="Listed Dr Count"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="80px" HorizontalAlign="Center">
                                        <asp:Label ID="lblDrcount" runat="server" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="80px" HorizontalAlign="Left">
                                        <asp:Label ID="lblUnListed" runat="server" Text="UnListed Dr Count"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="80px" HorizontalAlign="Center">
                                        <asp:Label ID="lblUndrcount" runat="server" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                    </div>
                    <br />
                    <asp:Button ID="btnConfirm" runat="server" Width="150px" Text="Confirm to Transfer" CssClass="savebutton" OnClientClick="return confirm('Do you want to Transfer Qualification?') &&  confirm('Are you sure want to Transfer?');"
                        OnClick="btnConfirm_Click" />
                </asp:Panel>
            </center>
        </div>
        <br />
        <br />

        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
        </div>
        <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/js/bootstrap-select.min.js"></script>
    </form>
</body>
</html>
