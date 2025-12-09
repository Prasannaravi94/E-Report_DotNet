<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleInput_CountDetail.aspx.cs" Inherits="MIS_Reports_SampleInput_CountDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
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

     <script type="text/javascript" language="javascript">
         $(function () {
             $('#btnExcel').click(function () {
                 var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                 location.href = url
                 return false
             })
         })
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".btnDrSn").mouseover(function () {
                $(this).css("color", "Fuchsia");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "15px");
            });
            $(".btnDrSn").mouseout(function () {
                $(this).css("color", "blue");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "13px");
            });
        });
    </script>  
   
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="pnlbutton" runat="server">
        <table width="100%">
            <tr>
                <td width="80%">
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Visible="false" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                     />
                            </td>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                     />
                            </td>
                            <td>
                                <asp:Button ID="btnPDF" runat="server" Text="PDF" Visible="false" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" 
                                     />
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
   <asp:Panel ID="pnlContents" runat="server">
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="lblHead" runat="server" Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <asp:Label ID="lbldivName"  SkinID="lblMand" Font-Bold="true" runat="server"></asp:Label>
                        </td>
                        
                    <tr>
                        <td align="center" colspan="3">
                            <asp:GridView ID="GrdFFcount" runat="server" AlternatingRowStyle-CssClass="alt" 
                                AutoGenerateColumns="true" CssClass="mGrids" EmptyDataText="No Records Found" 
                                GridLines="Both" HorizontalAlign="Center"  BorderWidth="1"
                                OnRowCreated="GrdFFcount_RowCreated" onrowdatabound="GrdFFcount_RowDataBound" 
                                ShowHeader="False" Width="98%" Font-Names="calibri" Font-Size="Small">
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
                            
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
    </div>
	<%--<script type="text/javascript">
        if ('<%= Session["Div_color"]!= null%>' == 'False') {
            document.body.style.backgroundColor = '#e8ebec';
        } else {
            document.body.style.backgroundColor = '<%= Session["Div_color"] %>'
        }
    </script>--%>
    </form>
</body>
</html>
