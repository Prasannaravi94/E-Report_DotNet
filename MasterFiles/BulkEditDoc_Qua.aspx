<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEditDoc_Qua.aspx.cs" Inherits="MasterFiles_BulkEditDoc_Qua" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bulk Edit - Doctor Qualification</title>
        <style type="text/css">
            .modal
    {
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
    .loading
    {
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
     <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-11">
                    <br />
                    <h2 class="text-center">Bulk Edit - Doctor Qualification</h2>
                    <div class="display-table clearfix">
                        <div class="table-responsive" style="scrollbar-width: thin;">
                            <asp:GridView ID="grdDocQua" runat="server" Width="55%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                AutoGenerateColumns="false" GridLines="None" CssClass="table" PagerStyle-CssClass="pgr"
                                AlternatingRowStyle-CssClass="alt">
                                <PagerStyle CssClass="gridview1"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <%--<ControlStyle Width="90%"></ControlStyle>
                                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qualification Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocQuaCode" runat="server" Text='<%#Eval("Doc_QuaCode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                                         <ItemTemplate> 
                                                            <asp:TextBox ID="txtDoc_Qua_SName" runat="server" SkinID="TxtBxAllowSymb" onkeypress="AlphaNumeric_NoSpecialChars(event);" Text='<%# Bind("Doc_QuaSName") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Qualification Name" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDocQuaName" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                                runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="no-result-area" />
                            </asp:GridView>
                        </div>
                        <br />
                        <center>
                            <asp:Button ID="btnSubmit" runat="server" Width="70px" Text="Update" CssClass="savebutton"
                                OnClick="btnSubmit_Click" />
                        </center> 
                    </div>
                </div>
                <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
        <br /><br />
     <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="../Images/loader.gif" alt="" />
</div>
    </div>
    </form>
</body>
</html>
