<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptSecSaleStockiest.aspx.cs" Inherits="MasterFiles_Reports_rptSecSaleStockiest" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secondary Sales Entry Status</title>
    <%-- <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }

        .display-reportMaintable .table tr:first-child td:first-child {
            border-radius: 8px 0 0 8px;
            background-color: #414d55;
            color: #ffffff;
            font-size: 14px;
            font-weight: 400;
            border-left: 0px solid #F1F5F8;
        }

        /*.display-reporttable .table tr td:first-child a {
            font-size: 12px;
            font-weight: 400;
        }*/

        .display-reportMaintable .table tr:first-child td {
            padding: 20px 10px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            background-color: #F1F5F8;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:Panel ID="pnlbutton" runat="server">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="row justify-content-center">
                            <div class="col-lg-9">
                                <div align="center">
                                    <asp:Label ID="lblText" runat="server" CssClass="reportheader" Text="All Stockist Secondary Sales Entry Stauts for"></asp:Label>
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
                                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" Visible="false" runat="server">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="lblPrint" runat="server" Text="Print" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                                        <%-- <asp:Button ID="btnPrint" runat="server" Visible="false" Text="Print" Font-Names="Verdana"
                                                Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px"
                                                Width="60px" />--%>
                                                    </td>
                                                    <td style="padding-right: 15px">
                                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClick="btnExcel_Click">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                        <%-- <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                                OnClick="btnExcel_Click" />--%>
                                                    </td>
                                                    <td style="padding-right: 15px">
                                                        <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" Visible="false">
                                                            <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="lblpdf" runat="server" Text="Pdf" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                                        <%-- <asp:Button ID="btnPDF" runat="server" Text="PDF" Visible="false" Font-Names="Verdana"
                                                Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px"
                                                Width="60px" />--%>
                                                    </td>
                                                    <td style="padding-right: 50px">
                                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                                        <%-- <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                                OnClientClick="RefreshParent();" />--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
            </asp:Panel>
            <br />
            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server" Width="100%">

                            <br />
                            <br />
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <table width="100%" align="center">
                                        <asp:Table ID="tbl" runat="server" GridLines="None" Width="100%" CssClass="table">
                                        </asp:Table>
                                    </table>
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
