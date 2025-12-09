<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptMangerVisit_VacantMR.aspx.cs" Inherits="MIS_Reports_rptMangerVisit_VacantMR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table width="100%">
                <tr>
                    <td width="80%">
                    </td>
                    <td style="float:right;">
                        <table>
                            <tr style="float:right; margin-right:10px;">
                               <td>
                                   
                                    <asp:ImageButton ID="btnPrint" runat="server" ToolTip="Print" ImageUrl="~/Images/print_image2.jpg" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid"  Height="45px" Width="60px"
                                       OnClientClick="return PrintPanel();"
                                         />
                                </td>&nbsp;&nbsp;&nbsp;
                                <td>
                                   
                                   <asp:ImageButton  ID="btnExcel" runat="server" ToolTip="Excel"  ImageUrl="~/Images/excel_image.png" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid"  Height="25px" Width="60px"
                                        />
                                         
                                </td>&nbsp;&nbsp;&nbsp;
                                
                                <td>
                                    
                                    <asp:ImageButton ID="btnClose" runat="server" ToolTip="Close" ImageUrl="~/Images/closebtn.png" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid"  Height="25px" Width="40px"
                                        OnClientClick="RefreshParent()"
                                        />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>      
            <asp:Panel ID="pnlContents" runat="server">
            <center>
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="Manager - Visit (Vacant HQ's)  for the month of " Font-Underline="True" ForeColor="#794044"
                        Font-Bold="True" Font-Names="Verdana" Font-Size="11pt"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                        Font-Size="9pt"></asp:Label>
                </div>
                  <br />

                <div align="left">
                  <asp:Label ID="lblvacant" Text ="Whether the Managers are covered the Vacant HQ's or not ..." ForeColor="#9400D3" Font-Bold="true" runat="server" ></asp:Label>
                </div>
        </center>
        <br />
        <center>
                       <asp:Panel ID="Panel1" runat="server" Width="100%">
                 <table width="95%"> 
                        <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt" 
                            AutoGenerateColumns="true" CssClass="mGrids" EmptyDataText="No Records Found" 
                            GridLines="Both" HorizontalAlign="Center"  BorderWidth="1"
                            OnRowCreated="GrdFixation_RowCreated" onrowdatabound="GrdFixation_RowDataBound" 
                            ShowHeader="False" Width="80%" Font-Names="calibri" Font-Size="Small">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt" />
                            <RowStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="Small" Font-Names="calibri" />
                            <Columns>
                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" ForeColor="Black" 
                                Height="5px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                     </table>
            </asp:Panel>
       </center>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
