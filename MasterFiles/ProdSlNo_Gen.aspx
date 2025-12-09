<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProdSlNo_Gen.aspx.cs" Inherits="MasterFiles_ProdSlNo_Gen" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Serial No Generation</title>
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
                        <h2 class="text-center">Product Serial No Generation</h2>
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">

                                <asp:GridView ID="grdProduct" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="false" AllowSorting="True" OnSorting="grdProduct_Sorting"
                                    GridLines="None" CssClass="table" >
                                 
                                    <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNum" runat="server" Text='<%# (grdProduct.PageIndex * grdProduct.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Product_Detail_Code" ItemStyle-HorizontalAlign="Left" SortExpression="Product_Detail_Code" ShowHeader="true" HeaderText="Short Name"  ItemStyle-Width="7%" />
                                        <asp:BoundField DataField="Product_Detail_Name" ItemStyle-HorizontalAlign="Left" SortExpression="Product_Detail_Name" ShowHeader="true" HeaderText="Product Name"  ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Product_Description" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Product Description" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="Product_Sale_Unit" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Sale Unit" ItemStyle-Width="8%" />
                                        <asp:BoundField DataField="Product_Type_Name" ItemStyle-HorizontalAlign="Left" SortExpression="Product_Type_Name" ShowHeader="true" HeaderText="Type" ItemStyle-Width="8%"  />
                                        <asp:BoundField DataField="Product_Cat_Name" ItemStyle-HorizontalAlign="Left" SortExpression="Product_Cat_Name" ShowHeader="true" HeaderText="Category" ItemStyle-Width="10%"  />
                                        <asp:BoundField DataField="Product_Grp_Name" ItemStyle-HorizontalAlign="Left" SortExpression="Product_Grp_Name" ShowHeader="true" HeaderText="Group" ItemStyle-Width="10%"  />
                                        <asp:BoundField DataField="Product_Brd_Name" ItemStyle-HorizontalAlign="Left" SortExpression="Product_Brd_Name" ShowHeader="true" HeaderText="Brand" ItemStyle-Width="10%"  />
                                        <asp:TemplateField HeaderText="Existing S.No" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New S.No"  ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSlNo" onkeypress="CheckNumeric(event);" MaxLength="3" runat="server" Width="70px" CssClass="input"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle  CssClass="no-result-area" />
                                </asp:GridView>

                            </div>
                            <br />
                              <br />
                            <center>
                                <asp:Button ID="btnSubmit" runat="server" Text="Generate - Sl No"  Width="130px"
                                    OnClick="btnSubmit_Click" CssClass="savebutton" />

                                <asp:Button ID="btnClear" runat="server" Text="Clear"
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
