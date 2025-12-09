<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visit_Analysis_At_A_Glance_Report.aspx.cs"
    Inherits="MIS_Reports_Visit_Details_Basedonfield_Level1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Visit Details Field Report</title>
    <link type="text/css" rel="stylesheet" href="../css/repstyle.css" />
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, cmode, Type) {
            popUpObj = window.open("Visit_Details_Basedonfield_Level2.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&cMode=" + cmode + "&Type=" + Type,
            "_blank",
        "ModalPopUp_Level1," +
         "0," +
        "toolbar=no," +
        "scrollbars=yes," +
        "location=no," +
        "statusbar=no," +
        "menubar=no," +
        "addressbar=no," +
        "resizable=yes," +
        "width=800," +
        "height=600," +
        "left = 0," +
        "top=0"
        );
            popUpObj.focus();
            //LoadModalDiv();
        }

    </script>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $("form1").live("submit", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        $(document).live('load', function () {
            $('#GrdDoctor tr').each(function () {
                var vara = $(this).find('td').html();
                if (vara != null)
                    alert($(this).find('td').html());
            });
        });
    </script>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <style type="text/css">
        .gvHeader th
        {
            padding: 3px;
            background-color: #DDEECC;
            color: maroon;
            border: 1px solid #bbb;
        }
        .gvRow td
        {
            padding: 3px;
            background-color: #ffffff;
            border: 1px solid #bbb;
        }
        .gvAltRow td
        {
            padding: 3px;
            background-color: #f1f1f1;
            border: 1px solid #bbb;
        }
        .gvHeader th:first-child
        {
            display: none;
        }
        .gvRow td:first-child
        {
            display: none;
        }
        .gvAltRow td:first-child
        {
            display: none;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <center>
            <br />
            <table width="100%">
                <tr>
                    <td width="80%">
                    </td>
                    <td align="right">
                        <table>
                            <tr style="float: right; margin-right: 10px;">
                                <td>
                                    <asp:ImageButton ID="btnPrint" ImageUrl="~/Images/printer.png" runat="server" Width="35px"
                                        Height="30px" OnClientClick="return PrintPanel();" />&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnExcel" ImageUrl="~/Images/Excels.png" runat="server" Height="30px"
                                        Width="35px" />&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnClose" ImageUrl="~/Images/closebtn.png" runat="server" Height="30px"
                                        Width="35px" OnClientClick="RefreshParent()" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Panel ID="pnlContents" runat="server">
                <center>
                    <div align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Visit Details for the month of " Font-Underline="True"
                            Font-Bold="True" Font-Names="Verdana" Font-Size="11pt"></asp:Label>
                        <br />
                        <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="9pt"></asp:Label>
                    </div>
                </center>
                <br />
                <left>
                  <asp:Panel ID="Panel1" runat="server">
                    <table width="95%"> 
                        <caption>
                            <asp:GridView ID="GrdFixationMode" runat="server" AlternatingRowStyle-CssClass="alt" 
                                AutoGenerateColumns="true" CssClass="mGrids" EmptyDataText="No Records Found" 
                                GridLines="Both" HorizontalAlign="Center"  BorderWidth="1"
                                OnRowCreated="GrdFixationMode_RowCreated" onrowdatabound="GrdFixationMode_RowDataBound" 
                                ShowHeader="False" Width="90%">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                </Columns>
                                <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" ForeColor="Black" 
                                    Height="5px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView>
                        </caption>
                    </table>
                </asp:Panel>
            </left>
        </asp:Panel>
        </center>
    </div>
    </form>
</body>
</html>
