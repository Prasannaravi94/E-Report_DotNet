<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Questionnaire_View_Zoom.aspx.cs"
    Inherits="MasterFiles_Quesionaire_rpt_Questionnaire_View_Zoom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
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
        .rptCellBorder
        {
            border: 1px solid;
            border-color: #999999;
        }
        
        .remove
        {
            text-decoration: none;
        }
            .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            font-size: 11px;
            font-family: Calibri;
            -webkit-appearance: none;
            width: 300px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td width="20%">
                        </td>
                        <td width="80%" align="center">
                            <asp:Label ID="lblHead" Text="" ForeColor="DarkOrange" Font-Size="16px" Font-Bold="true"
                                Font-Underline="true" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                                Font-Size="9pt"></asp:Label>
                        </td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="return PrintPanel();" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="RefreshParent()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <br />
        <center>
            <table>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFilter" runat="server" SkinID="lblMand">Filter By</asp:Label>
                        <%--<asp:DropDownList ID="ddlFilter" runat="server" SkinID="ddlRequired"></asp:DropDownList>--%>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:ListBox ID="lstQues" runat="server" SelectionMode="Multiple" CssClass="ddl">
                        </asp:ListBox>
                        <asp:Button ID="btnGo" runat="server" Text="Go" Width="30px" Height="25px" BackColor="LightBlue" OnClick="btnGo_Click" />
                    </td>
                </tr>
            </table>
        </center>
        <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <div>
                <div align="center">
                    <%--<asp:Label ID="lblHead" Text="Product Exposure Analysis" SkinID="lblMand" Font-Underline="true"
                runat="server"></asp:Label>--%>
                </div>
                <div>
                    <%--  <table width="100%" align="center">
                    <tr>
                    <td width="2.5%"></td>
                        <td align="left">
                            <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblRegionName" runat="server" Font-Size="14px" ForeColor="DarkGoldenrod" Font-Bold="true" ></asp:Label>
                        </td>
                       
                        <td align="left">
                            <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        
                        
                    </tr>
                </table>--%>
                </div>
                <br />
                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="Panel1" runat="server">
                                    <table width="95%">
                                        <caption>
                                            <asp:GridView ID="Grdprd" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="true"
                                                CssClass="mGrids" EmptyDataText="No Records Found" GridLines="Both" HorizontalAlign="Center"
                                                BorderWidth="1" OnRowCreated="Grdprd_RowCreated" OnRowDataBound="Grdprd_RowDataBound"
                                                ShowHeader="False" Width="90%" Font-Names="calibri" Font-Size="Small">
                                                <HeaderStyle Font-Bold="False" />
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="BurlyWood" />
                                                <AlternatingRowStyle CssClass="alt" />
                                                <RowStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="11px" Font-Names="calibri" />
                                                <Columns>
                                                </Columns>
                                                <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                    VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </caption>
                                    </table>
                                </asp:Panel>
                                <%-- <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="95%">
                            </asp:Table>--%>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
    </div>
    <link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />
    <script type="text/javascript" src="../../JsFiles/multiple-select_2.js"></script>
    <script type="text/javascript">

        $('[id*=lstQues]').multipleSelect();
    </script>
    </form>
</body>
</html>
