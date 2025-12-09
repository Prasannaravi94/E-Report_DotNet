<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product_Reactivate.aspx.cs" Inherits="MasterFiles_Product_Reactivate" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Details Reactivation</title>
    <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <br />
                        <h2 class="text-center">Product Details Reactivation</h2>
                        <div class="display-table clearfix">
                            <div class="table-responsive">

                                <asp:GridView ID="grdProduct" runat="server" Width="100%" HorizontalAlign="Center"
                                    AutoGenerateColumns="false" AllowPaging="True" PageSize="10"
                                    OnPageIndexChanging="grdProduct_PageIndexChanging" EmptyDataText="No Records Found"
                                    OnRowCommand="grdProduct_RowCommand"
                                    GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                    AllowSorting="True">
                                   
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProdCode" runat="server" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProName" runat="server" MaxLength="150" Text='<%# Bind("Product_Detail_Name") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProDesc" runat="server"  MaxLength="250" Text='<%# Bind("Product_Description") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProDesc" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sale Unit" ItemStyle-HorizontalAlign="Left">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtSaleUn"  runat="server" MaxLength="3" Text='<%# Bind("Product_Sale_Unit") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSaleUn" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Product_Cat_Name" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Category" ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Product_Grp_Name" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Group" ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Product_Brd_Name" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Brand" ItemStyle-Width="10%" />
                                        <asp:TemplateField HeaderText="Reactivate" ItemStyle-HorizontalAlign="Center">
                                           
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbutReactivate" runat="server" CommandArgument='<%# Eval("Product_Detail_Code") %>'
                                                    CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Product');">Reactivate
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
                </div>
            </div>
            <br />
            <br />

        </div>
    </form>
</body>
</html>
