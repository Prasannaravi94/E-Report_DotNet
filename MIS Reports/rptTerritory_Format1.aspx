<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTerritory_Format1.aspx.cs" Inherits="MIS_Reports_rptTerritory_Format1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <%--<link type="text/css" rel="stylesheet" href="../css/Report.css" />--%>
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td></td>
                    <td></td>
                    <%-- <td width="80%" align="center">
                        <asp:Label ID="lblProd" runat="server" Text="ListedDr - Product Visit" Font-Underline="true" SkinID="lblMand"></asp:Label>
                    </td>--%>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>

                                <td style="padding-right: 40px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" OnClick="btnClose_Click">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />

        <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <center>
                        <%--<asp:Label ID="lblProd" runat="server" Text="Product Exposure" SkinID="lblMand" ></asp:Label>--%>
                    </center>

                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <div align="center">
                            <asp:Label ID="lblProd" runat="server" Text="ListedDr - Product Visit" CssClass="reportheader"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="lblfieldname" runat="server" Font-Size="18px" CssClass="label" Text="Fieldforce Name:"></asp:Label>
                            <asp:Label ID="lblname" runat="server" Font-Size="18px" CssClass="label"></asp:Label>
                            <br />
                            <br />
                        </div>

                        <div class="display-Approvaltable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                <asp:GridView ID="grdDr" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found" OnRowCreated="grdDr_RowCreated"
                                    AutoGenerateColumns="false" GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdDr_RowDataBound"
                                    AllowSorting="True" ShowHeader="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ListedDrCode" ItemStyle-HorizontalAlign="Left" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrCode" runat="server" Text='<%#Eval("Listeddr_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhq" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhq" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField  HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblforce" runat="server" Width="120px" Text='<%#Eval("Fieldforce_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Tagged Product" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrd_Tagg" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>



                                        <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblquali" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpec" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCat" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product_Priority1" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrd_Tagg1" runat="server" Text='<%# Bind("Product_Priority1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product_Priority2" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrd_Tagg2" runat="server" Text='<%# Bind("Product_Priority2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product_Priority3" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrd_Tagg3" runat="server" Text='<%# Bind("Product_Priority3") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>

                                <%--<asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both">
                </asp:Table>--%>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <br />
        <br />
    </form>
</body>
</html>

