<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CallMonitor.aspx.cs" Inherits="MasterFiles_AnalysisReports_CallMonitor" %>

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
        #dtdoctor tr td, #tbl tr td, #tblhq tr td {
            border: 1px solid #DCE2E8;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row justify-content-center">
                    <div class="col-lg-9">
                                   <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="Missed Doctor List for the Month of "
                                    ForeColor="#0077FF" CssClass="reportheader"></asp:Label>
                                <br />
                                <asp:Label ID="lblsubhead" runat="server" Visible="false" CssClass="reportheader" Font-Size="16px" ForeColor="#696d6e"></asp:Label>
                            </div>
                    </div>
                    <div class="col-lg-3">
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
            <br />

            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">
                            <div style="overflow-x: auto; scrollbar-width: thin;">
                                <center>
                                    <asp:DataList ID="dtdoctor" runat="server" BackColor="#f1f5f8" BorderColor="#DCE2E8"
                                        BorderStyle="None" CellPadding="3" CellSpacing="2"
                                        GridLines="Both" RepeatColumns="10" RepeatDirection="Horizontal">
                                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                        <HeaderStyle BackColor="#f1f5f8" Font-Bold="True" Font-Size="Large" ForeColor="#636d73" BorderColor="#DCE2E8"
                                            HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderTemplate>
                                            Visit Based
                                        </HeaderTemplate>
                                        <ItemStyle BackColor="White" ForeColor="#636d73" Width="100px" Height="80px" HorizontalAlign="Center" BorderColor="#DCE2E8"
                                            BorderWidth="1px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDName" runat="server" Font-Size="9pt" Text='<%# Eval("ListedDr_Name") %>'> </asp:Label>
                                            <br />
                                            <asp:Label ID="lblcat" runat="server" Font-Size="9pt" ForeColor="Red" Font-Bold="true"
                                                Text='<%# Eval("Doc_Cat_ShortName") %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="lbldate" runat="server" Font-Size="9pt" ForeColor="BlueViolet" Font-Bold="true"
                                                Text='<%# Eval("date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </center>
                            </div>
                            <br />
                            <br />
                        </asp:Panel>

                        <div class="row justify-content-center">
                            <div class="col-lg-3">
                                <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" style="background-color:white;">
                                </asp:Table>
                            </div>
                            <div class="col-lg-3">
                                <asp:Table ID="tblhq" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" style="background-color:white;">
                                </asp:Table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        <%--<script type="text/javascript">
            document.body.style.backgroundColor = '<%= Session["Div_color"].ToString() %>'
        </script>--%>
    </form>
</body>
</html>
