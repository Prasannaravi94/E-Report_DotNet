<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDCRViewDeletedDetails.aspx.cs" Inherits="MasterFiles_Reports_rptDCRViewDeletedDetails" %>

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

    <style type="text/css">
        .tblHead {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            height: 25px;
            border: 1px solid;
            border-color: #999999;
        }

        .tblRow {
            font-size: 8pt;
            border: 1px solid;
            border-color: #999999;
            font-family: Verdana;
        }

        .tbldetail_main {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }

        .table {
            margin-top: 45px;
        }
    </style>

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
            <center>
                <br />
                <asp:Panel ID="pnlbutton" runat="server">
                    <table width="100%">
                        <tr>
                            <td></td>
                            <td align="right">
                                <table>
                                    <tr>
                                        <td style="padding-right: 30px">
                                            <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                            </asp:LinkButton>
                                            <asp:Label ID="lblPrint" runat="server" Text="Print" Font-Size="14px"></asp:Label>

                                        </td>
                                        <td style="padding-right: 15px">
                                            <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClick="btnExcel_Click">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                            </asp:LinkButton>
                                            <asp:Label ID="lblExcel" runat="server" Text="Excel" Font-Size="14px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnPDF" ToolTip="PDF" runat="server" Visible="false">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                            </asp:LinkButton>
                                            <asp:Label ID="Label3" runat="server" Text="PDF" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>

                                        </td>
                                        <td style="padding-right: 50px">
                                            <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                            </asp:LinkButton>
                                            <asp:Label ID="lblClose" runat="server" Text="Close" Font-Size="14px"></asp:Label>

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
                            <asp:Panel ID="pnlContents" runat="server" Width="100%">

                                <asp:Label ID="lblHead" runat="server" Text="Daily Call Report for " Visible="true"
                                    CssClass="reportheader"></asp:Label>
                                <br />
                                <br />
                                <br />
                                <div class="row" align="left">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblFieldForceName" runat="server" Text="Daily Call Report for " ForeColor="#696d6e" Font-Size="16px"
                                            Visible="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblSubArea" runat="server" Text="Daily Call Report for " Visible="true"
                                            ForeColor="#696d6e" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblHQ" runat="server" Text="Daily Call Report for " Visible="true"
                                            ForeColor="#696d6e" Font-Size="16px"></asp:Label>
                                    </div>
                                </div>

                                  <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="grdDCRTrans" runat="server" AutoGenerateColumns="true"
                                            CellPadding="2" CssClass="table" EmptyDataText="" GridLines="None"
                                            HorizontalAlign="Center" Width="100%">

                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="grdLstDr" runat="server" Width="100%" HorizontalAlign="Center"
                                            CellPadding="2" EmptyDataText="" AutoGenerateColumns="true"
                                             CssClass="table" GridLines="None">
                                            <%-- <HeaderStyle Font-Bold="False" Wrap="false" />--%>

                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="GVChemist" runat="server" Width="100%" HorizontalAlign="Center" GridLines="None"
                                            CellPadding="2"  EmptyDataText="" AutoGenerateColumns="true"
                                            CssClass="table">
                                            <%-- <HeaderStyle Font-Bold="False" Wrap="false" />--%>

                                           
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="GVUnlstDr" runat="server" AutoGenerateColumns="true"
                                            CellPadding="2" CssClass="table" EmptyDataText="" GridLines="None"
                                            HorizontalAlign="Center" Width="100%">
                                            <%--  <HeaderStyle Font-Bold="False" Wrap="false" />--%>

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
            </center>
        </div>
    </form>
</body>
</html>
