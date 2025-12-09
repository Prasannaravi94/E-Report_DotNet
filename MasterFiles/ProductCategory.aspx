<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductCategory.aspx.cs"
    Inherits="MasterFiles_ProductCategory" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product-Category</title>
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script> 
    <script type="text/javascript">
        $(document).ready(function () {
            //  $('input:text:first').focus();
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
                if ($("#txtProduct_Cat_SName").val() == "") { alert("Enter Short Name."); $('#txtProduct_Cat_SName').focus(); return false; }
                if ($("#txtProCatName").val() == "") { alert("Enter Category Name."); $('#txtProCatName').focus(); return false; }
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
                        <h2 class="text-center">Product-Category</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblProduct_Cat_SName" runat="server" CssClass="label">Short Name<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtProduct_Cat_SName" CssClass="input" Width="100%" TabIndex="1" runat="server" MaxLength="12" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblProCatName" runat="server" CssClass="label">Category Name<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtProCatName" CssClass="input" Width="100%"  TabIndex="2" runat="server" MaxLength="120" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                  </asp:TextBox>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Save" OnClick="btnSubmit_Click" />
                        </div>

                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />

                </div>
            </div>

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
