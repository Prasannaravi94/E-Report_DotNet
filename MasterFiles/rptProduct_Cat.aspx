<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptProduct_Cat.aspx.cs" Inherits="MasterFiles_rptProduct_Cat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Product Details</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" type="image/png" href="../../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../../assets/css/responsive.css" />
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->

    <script src="../../assets/js/jQuery.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../assets/js/main.js"></script>

    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <br />
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%" style="padding-right: 100px;">
                    <tr>
                        <td style="padding-right: 50px" align="right">
                            <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                <asp:Image ID="Image4" runat="server" ImageUrl="../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                            </asp:LinkButton>
                            <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                        </td>
                    </tr>
                </table>

            </asp:Panel>
            <br />

            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div align="center">
                            <asp:Label ID="lblProd" runat="server" Text="Product Details" CssClass="reportheader"></asp:Label>
                        </div>
                        <br />
                        <div class="display-reporttable clearfix">
                            <div class="table-responsive" style="max-height: 700px; scrollbar-width: thin;">

                                <asp:GridView ID="grdProduct" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="false" GridLines="None" CssClass="table"
                                    AllowSorting="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProdCode" runat="server" Width="120px" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProName" runat="server" Width="150px" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Description" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProDes" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sale Unit" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSaleUn" Width="80px" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--   <asp:TemplateField HeaderText="Sample Unit" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="200px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblsam1" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_One") %>'></asp:Label>--%>
                                        <%-- </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample Unit2">
                                <ItemTemplate>--%>
                                        <%--   <asp:Label ID="lblsam2" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_Two") %>'></asp:Label>--%>
                                        <%--</ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample Unit3">
                                <ItemTemplate>--%>
                                        <%--   <asp:Label ID="lblsam3" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_Three") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Product Group" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgr" runat="server" Width="130px" Text='<%# Bind("Product_Grp_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Category" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcar" runat="server" Width="130px" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product Brand" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbrd" runat="server" Width="130px" Text='<%# Bind("Product_Brd_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField  HeaderText="Product Brand" ItemStyle-HorizontalAlign="Left" 
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblbrd" runat="server" Width="130px" Text='<%# Bind("Product_Brd_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <%-- <asp:TemplateField HeaderText="Sub Division" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsd" runat="server" Width="130px" Text='<%# Bind("subdivision_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
