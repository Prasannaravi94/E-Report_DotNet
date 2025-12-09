<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TargetFixationView_Report_New.aspx.cs" Inherits="MasterFiles_ActivityReports_TargetFixationView_Report_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Target Fixation Report</title>
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
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
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
    <script type="text/javascript">
        function RefreshParent() {
            //window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#ffeb95';
            element.style.cursor = 'pointer';

        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';

        }
    </script>

    <style type="text/css">
        td {
            cursor: pointer;
        }

        .selected_row {
            background-color: #45B39D;
        }

        .display-table .table tr:nth-child(2) td:first-child {
            background-color: #f1f5f8;
            color: #636d73;
        }

        .display-table {
            line-height: 1px !important;
        }

            .display-table .table tr:nth-child(3) td:first-child {
                background-color: #414d55 !important;
                color: #ffffff !important;
            }
    </style>
</head>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <asp:Panel ID="pnlbutton" runat="server">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <br />
                    <div class="row justify-content-center">
                        <div class="col-lg-9">
                            <table width="0%" align="center">
                                <tr>
                                    <td width="100%" align="center">
                                        <asp:Label ID="lblHead" runat="server" Text="Target View from " CssClass="reportheader"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="LblForceName" runat="server" Font-Size="14px" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblstock" runat="server" Font-Size="14px" CssClass="label" ForeColor="Pink"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-lg-3">
                            <table width="100%">
                                <tr>
                                    <td width="80%"></td>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td style="padding-right: 30px">
                                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <p>
                                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                    </p>
                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <p>
                                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                    </p>
                                                </td>
                                                <td style="padding-right: 50px">
                                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <p>
                                                        <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                    </p>
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
        <asp:Panel ID="pnlContents" runat="server">
            <div class="home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">

                        <div class="designation-reactivation-table-area clearfix">
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                    <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                        GridLines="None" HorizontalAlign="Center" OnRowCreated="GrdFixation_RowCreated"
                                        OnRowDataBound="GrdFixation_RowDataBound" ShowHeader="False" Width="100%">
                                        <Columns>
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

