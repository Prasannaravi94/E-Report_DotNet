<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductView.aspx.cs" Inherits="MasterFiles_ProductView" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Details - View</title>

</head>
<style>
    .display-table .table th {
        font-size: 12px !important;
    }
</style>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class=" home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <br />
                        <h2 class="text-center">Product Details - View</h2>
                        <div class="row justify-content-center">
                            <div class="col-lg-3">
                                <asp:Label ID="lblGiftType" runat="server" Text="Search By" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlSrch" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                                    TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Product Name" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Product Category" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Product Group" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Product Brand" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Sub Division" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="State" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Mode of Product" Value="8"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-3">
                                <div class="single-des clearfix" style="padding-top: 18px">
                                    <asp:TextBox ID="TxtSrch" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                </div>
                                <div style="margin-top: -18px">
                                    <asp:DropDownList ID="ddlProCatGrp" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlProCatGrp_SelectedIndexChanged"
                                        CssClass="nice-select" TabIndex="4" Visible="false">
                                    </asp:DropDownList>

                                </div>

                            </div>
                            <div class="col-lg-6" style="margin-top: 19px">
                                <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="50px"
                                    Text="Go" OnClick="Btnsrc_Click" Visible="false" />
                            </div>
                        </div>
                        <br />

                        <div class="display-table clearfix">
                            <div class="table-responsive" style="overflow: inherit">

                                <asp:GridView ID="grdProduct" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="false" GridLines="None" CssClass="table"
                                    AllowSorting="True" OnSorting="grdProduct_Sorting">

                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Product_Detail_Code" HeaderText="Product Code" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProdCode" runat="server" Width="60px" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Product_Detail_Name" HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProName" runat="server" Width="60px" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Description" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProDes" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sale Unit" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSaleUn" Width="60px" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sample Unit">
                                            <ItemStyle Width="200px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblsam1" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_One") %>'></asp:Label>
                                                <%-- </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample Unit2">
                                <ItemTemplate>--%>
                                                <asp:Label ID="lblsam2" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_Two") %>'></asp:Label>
                                                <%--</ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample Unit3">
                                <ItemTemplate>--%>
                                                <asp:Label ID="lblsam3" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_Three") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Product_Grp_Name" HeaderText="Product Group">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgr" runat="server" Width="130px" Text='<%# Bind("Product_Grp_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Product_Cat_Name" HeaderText="Product Category">

                                            <ItemTemplate>
                                                <asp:Label ID="lblcar" runat="server" Width="130px" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField SortExpression="Product_Brd_Name" HeaderText="Product Brand">

                                            <ItemTemplate>
                                                <asp:Label ID="lblbrd" runat="server" Width="130px" Text='<%# Bind("Product_Brd_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="subdivision name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsubdivision_name" runat="server" Text='<%# Bind("subdivision_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sample ERP Code" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSample_Erp_Code" runat="server" Text='<%# Bind("Sample_Erp_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sale ERP Code" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSale_Erp_Code"  runat="server" Text='<%# Bind("Sale_Erp_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Sub Division" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsd" runat="server" Width="130px" Text='<%# Bind("subdivision_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>

                            </div>
                            <br />
                            <br />

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
