<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocClass_SlNo.aspx.cs" Inherits="MasterFiles_DocClass_SlNo" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor Class Serial No Generation</title>
     <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
                    <h2 class="text-center">Doctor Class Serial No Generation</h2>
                    <div class="display-table clearfix">
                        <div class="table-responsive">
                            <asp:GridView ID="grdDocCat" runat="server" Width="80%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                AutoGenerateColumns="false" AllowSorting="True" OnSorting="grdDocCls_Sorting"
                                GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt">
                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNum" runat="server" Text='<%# (grdDocCat.PageIndex * grdDocCat.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="Doc_ClsCode" ShowHeader="true" HeaderText="Class Code"  ItemStyle-Width="7%"  Visible="false"/>--%>
                                    <asp:TemplateField HeaderText="Doc_ClsCode" Visible="false">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="Doc_Cat_Code" runat="server" Text='<%#   Bind("Doc_ClsCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Doc_ClsSName" SortExpression="Doc_ClsSName" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Short Name"/>
                                    <asp:BoundField DataField="Doc_ClsName" SortExpression="Doc_ClsName" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Class Name"/>
                                    <asp:TemplateField HeaderText="Existing S.No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="New S.No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSlNo" onkeypress="CheckNumeric(event);" CssClass="input" MaxLength="3" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                  <EmptyDataRowStyle CssClass="no-result-area" />
                            </asp:GridView>
                        </div>
                        <br />
                        <center>
                            <asp:Button ID="btnSubmit" runat="server" Width="120px" Text="Generate - Sl No"
                                OnClick="btnSubmit_Click" CssClass="savebutton" />

                            <asp:Button ID="btnClear" runat="server" Width="60px" Text="Clear"
                                OnClick="btnClear_Click" CssClass="resetbutton" />
                        </center>
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
