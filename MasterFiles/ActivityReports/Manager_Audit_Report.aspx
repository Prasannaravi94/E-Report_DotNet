<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Manager_Audit_Report.aspx.cs" Inherits="Manager_Audit_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manager - Audit Report</title>
    <%-- <link type="text/css" rel="stylesheet" href="../css/Report.css" />
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
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
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
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
        .mGrid {
            background: white;
        }

            .mGrid td {
                padding: 2px;
                border: solid 1px Black;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                font-size: small;
                font-family: Calibri;
            }


            .mGrid th {
                padding: 4px 2px;
                color: white;
                background: #0097AC;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                border-bottom: solid 1px Black;
                font-weight: normal;
                font-size: small;
                font-family: Calibri;
            }

            .mGrid .pgr {
                background: #A6A6D2;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    text-align: left;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: White;
                    line-height: 12px;
                }

                .mGrid .pgr th {
                    background: #A6A6D2;
                }

                .mGrid .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .display-table .table th {
            font-size: 12px;
        }
    </style>
</head>
<body style="background: white">
    <form id="form1" runat="server">
        <asp:Panel ID="pnlbutton" runat="server">
                     <br />
            <div class="row justify-center">
                <div class="col-lg-12">
                    <div class="row justify-center">
                        <div class="col-lg-9">
                            <center>
                                <div align="center">
                                    <asp:Label ID="lblHead" runat="server" Text="Manager Audit Report" Font-Underline="True" Font-Bold="True" Font-Size="16px"></asp:Label>
                                </div>
                                <br />
                                <div align="center">
                                    <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Size="14px"></asp:Label>
                                </div>
                            </center>
                        </div>
                        <div class="col-lg-3">
                            <table width="100%" id="tbl5" runat="server">
                                <tr>
                                    <td width="80%"></td>
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
                                                <td style="padding-right: 50px">
                                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
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
        </asp:Panel>
        <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-center">
                    <div class="col-lg-12">

                        <div class="designation-reactivation-table-area clearfix">
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                    <asp:GridView ID="grdReport" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                        GridLines="None" HorizontalAlign="Center" BorderWidth="0" Width="100%" OnRowDataBound="grdReport_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumber" runat="server" Text='<%#   Bind("RowNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Login - FieldForce Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#   Bind("Manager_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSFid" runat="server" Text='<%#   Bind("sf_emp_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mode of Report">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMode" runat="server" Text='<%# Bind("Mode_of_Repeat") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Selected Month">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljoin" runat="server" Text='<%# Bind("MonthData") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Viewed On">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Login") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Selected Fieldforce">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("To_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
