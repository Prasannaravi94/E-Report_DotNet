<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Hq_Multiple_St_ALL.aspx.cs" Inherits="MasterFiles_SecSaleReport_rpt_Hq_Multiple_St_ALL" %>

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
        .display-table3rowspan .table tr:first-child td:first-child {
            width: 4%;
        }
    </style>
  
</head>
<body>
    <form id="form1" runat="server">
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
        <br />

        <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <asp:Panel ID="pnlContents" runat="server">
                        <div align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Secondary Sale Analysis from " CssClass="reportheader"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" Font-Size="18px" ForeColor="#696d6e"></asp:Label>
                            <br />
                            <asp:Label ID="lblstock" runat="server" CssClass="reportheader" Font-Size="18px" ForeColor="#696d6e"></asp:Label>
                        </div>
                        <br />

                        <div class="display-table3rowspan clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                <asp:Panel ID="Panel1" runat="server">

                                    <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                        GridLines="None" HorizontalAlign="Center" OnRowCreated="GrdFixation_RowCreated"
                                        ShowHeader="False" Width="100%" OnRowDataBound="GrdFixation_RowDataBound">
                                        <Columns>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                </asp:Panel>
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
