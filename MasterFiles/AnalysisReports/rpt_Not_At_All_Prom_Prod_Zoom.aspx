<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Not_At_All_Prom_Prod_Zoom.aspx.cs" Inherits="MasterFiles_AnalysisReports_rpt_Not_At_All_Prom_Prod_Zoom" %>

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

    <%--<link type="text/css" rel="stylesheet" href="../../css/Report.css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', '');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
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
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .display-reportMaintable .table th {
            padding: 15px 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row justify-content-center">
                    <div class="col-lg-9">
                        <div align="center">
                            <br />
                            <asp:Label ID="lblHead" runat="server" Text="Not at all Promoted " CssClass="reportheader"></asp:Label>
                            <br />

                            <asp:Label ID="lblsubhead" runat="server" Visible="false" CssClass="reportheader" Font-Size="18px" ForeColor="#696d6e"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-3">

                        <br />
                        <table width="100%">
                            <tr>
                                <td></td>
                                <td align="right">
                                    <table>
                                        <tr>
                                            <td style="padding-right: 30px">
                                                <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
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
                                                <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                                    <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                </asp:LinkButton>
                                                <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="container clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; background-color: white">
                                    <asp:GridView ID="grdDoctor" runat="server" AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                        OnRowDataBound="grdDoctor_RowDataBound" GridLines="None"
                                        HorizontalAlign="Center"
                                        Width="100%">

                                        <Columns>

                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center">
                                                <ControlStyle Width="25%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text="<%#  ((GridViewRow)Container).RowIndex + 1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Code"
                                                ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ControlStyle Width="90%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProdCode" runat="server" Text='<%# Bind("Product_Code_SlNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField
                                                HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="90%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfname" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField
                                                HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="90%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Desig."
                                                ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="60%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesig" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField
                                                HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="40%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProd" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField
                                                HeaderText="Last Promoted Dt" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="90%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisit" runat="server" ForeColor="Red"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </asp:Panel>

                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
<script type="text/javascript">
        if ('<%= Session["Div_color"]!= null%>' == 'False') {
            document.body.style.backgroundColor = '#e8ebec';
        } else {
            document.body.style.backgroundColor = '<%= Session["Div_color"] %>'
        }
    </script>
    </form>
</body>
</html>
