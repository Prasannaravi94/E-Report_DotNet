<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptDrs_Add_Del_at_a_glance_Zoom.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_RptDrs_Add_Del_at_a_glance_Zoom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SAN eReport</title>
  
</head>

<style type="text/css">
   
   
     .tblCellFont
        {
            font-size:9pt;
            font-family:Calibri;
        }
        #loading {
    display: block;
    position: absolute;
    top: 0;
    left: 0;
    z-index: 100;
    width: 100vw;
    height: 100vh;
    background-color: rgba(192, 192, 192, 0.5);
    background-image: url("../../Images/loader.gif");
    background-repeat: no-repeat;
    background-position: center;
}
#page {
    display: none;
}
</style>

 

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

<body>

    <form id="form1" runat="server">


    <div >
        <br />
        <center>
            <table width="100%">
          
                <tr>
                    <td width="80%">
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
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        />
                                </td>
                                <td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPDF_Click" Visible="false" />
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
            <center>
                <asp:Panel ID="pnlContents" runat="server">
                    <div align="center">
                    
                        <asp:Label ID="lblHead" runat="server" Text="Manager - HQ - Coverage from " Font-Underline="True"
                            Font-Bold="True" Font-Names="Verdana" Font-Size="11pt"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="9pt"></asp:Label>
                    </div>
                    <br />
                    <table width="100%" align="center">                      
                    
                        <tr>
                            <td>
                                <%--<asp:Table ID="tblhq" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                    Width="95%">
                                </asp:Table>--%>
                                <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="true" CssClass="mGrids" EmptyDataText="No Records Found"
                                        GridLines="Both" HorizontalAlign="Center" BorderWidth="1" OnRowCreated="GrdFixation_RowCreated"
                                         ShowHeader="False" Width="95%" Font-Names="calibri" OnRowDataBound="GrdFixation_RowDataBound" 
                                        Font-Size="Small" >
                                        <HeaderStyle Font-Bold="False" />
                                        <PagerStyle CssClass="pgr" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt" />
                                        <RowStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="Small" Font-Names="calibri" />
                                        <Columns>
                                        </Columns>
                                        <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                            BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </center>
         
        </center>
    </div>
     
    </form>
</body>
</html>
