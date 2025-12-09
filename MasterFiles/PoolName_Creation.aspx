<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoolName_Creation.aspx.cs"
    Inherits="MasterFiles_PoolName_Creation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stockist - HQ Creation</title>
    <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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
    </style>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {

                if ($("#ddlState").val() == "0") { alert("Please Select State."); $('#ddlState').focus(); return false; }
                if ($("#txtPool_Sname").val() == "") { alert("Enter Short Name."); $('#txtPool_Sname').focus(); return false; }
                if ($("#txtPool_Name").val() == "") { alert("Enter Pool Area Name."); $('#txtPool_Name').focus(); return false; }
            });
        });
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
                        <h2 class="text-center" style="border-bottom: none">Stockist - HQ Creation</h2>

                        <asp:HiddenField ID="hdnpoolName" runat="server" />
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblState" runat="server" CssClass="label">State<span style="color:Red;padding-left:2px">*</span></asp:Label>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblShortName" runat="server" CssClass="label"
                                    Width="120px">Short Name<span style="color:Red;padding-left:2px">*</span></asp:Label>
                                <asp:TextBox ID="txtPool_Sname" CssClass="input" Width="100%"
                                    TabIndex="1" runat="server" MaxLength="10"
                                    onkeypress="CharactersOnly(event);">
                                </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblPoolName" runat="server" CssClass="label">Name<span style="color:Red;padding-left:2px">*</span></asp:Label>
                                <asp:TextBox ID="txtPool_Name"
                                    TabIndex="2" runat="server" CssClass="input" Width="100%"
                                    MaxLength="120" onkeypress="CharactersOnly(event);">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton"
                                Text="Save" OnClick="btnSubmit_Click" />
                        </div>

                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
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
