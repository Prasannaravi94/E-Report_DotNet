<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProdCat_SlNo_Gen.aspx.cs"
    Inherits="MasterFiles_ProdCat_SlNo_Gen" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Category Serial No - Generation</title>

    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <br />
                        <h2 class="text-center">Product Category Serial No - Generation</h2>
                        <div class="display-table clearfix">
                            <div class="table-responsive">

                                <asp:GridView ID="grdProCat" runat="server" Width="80%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="false" AllowSorting="True" OnSorting="grdProCat_Sorting"
                                    GridLines="None" CssClass="table">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNum" runat="server" Text='<%# (grdProCat.PageIndex * grdProCat.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product_Cat_Code" Visible="false">                                          
                                            <ItemTemplate>
                                                <asp:Label ID="Product_Cat_Code" runat="server" Text='<%#   Bind("Product_Cat_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:BoundField DataField="Doc_Cat_Code" ShowHeader="true" HeaderText="DR Cat Code"  ItemStyle-Width="7%"  />--%>
                                        <asp:TemplateField SortExpression="Product_Cat_SName" HeaderText="Short Name"
                                            ItemStyle-HorizontalAlign="Left">                                         
                                            <ItemTemplate>
                                                <asp:Label ID="lblProduct_Cat_SName" runat="server" Text='<%# Bind("Product_Cat_SName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField SortExpression="Product_Cat_Name" HeaderText="Category Name" ItemStyle-HorizontalAlign="Left" >                                         
                                            <ItemTemplate>
                                                <asp:Label ID="lblProCatName" runat="server" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField DataField="Product_Cat_SName" SortExpression="Product_Cat_SName" ShowHeader="true" HeaderText="Short Name" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Product_Cat_Name" SortExpression="Product_Cat_Name" ShowHeader="true" HeaderText="Category Name"  ItemStyle-Width="20%"/>--%>
                                        <asp:TemplateField HeaderText="Existing S.No" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="New S.No"  ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSlNo" onkeypress="CheckNumeric(event);" runat="server" MaxLength="3"  CssClass="input" ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
                            </div>
                            <br />
                            <center>
                                <asp:Button ID="btnSubmit" runat="server" Text="Generate - Sl No" width=" 130px" CssClass="savebutton" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="resetbutton" OnClick="btnClear_Click" />
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
