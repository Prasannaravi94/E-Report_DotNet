<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptPrescriber_report.aspx.cs" Inherits="MIS_Reports_rptPrescriber_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
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
        <div>
        </div>
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td width="20%"></td>
                    <td></td>
                    <td width="80%" align="center">
                        <asp:Label ID="lblProd" runat="server" Text="Prescriber Detail " ForeColor="#794044" Font-Names="Verdana" Font-Size="14px" Font-Underline="true" Font-Bold="true"></asp:Label>
                        <br /><br />
                        <asp:Label ID="lblFieldForce" runat="server" Font-Size="Medium" ></asp:Label>
                        <br />
                        <asp:Label ID="lblDesignSelected" runat="server" Font-Size="Medium" ></asp:Label>
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPrint_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <center>
        <asp:Panel ID="pnlContents" runat="server">
            <asp:GridView ID="GrdPrescriber" runat="server" OnRowCreated="GrdPrescriber_RowCreated" ShowHeader="False" Width="95%" AlternatingRowStyle-CssClass="alt"
               OnRowDataBound="GrdPrescriber_RowDataBound" AutoGenerateColumns="true" CssClass="mGrids" >
                <Columns>
                </Columns>
                <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                    BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
            </asp:GridView>
        </asp:Panel>
            </center>
    </form>
</body>
</html>
