<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Exp_Cons_View.aspx.cs" Inherits="MIS_Reports_Visit_Summary_AM_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>Expense Consolidated View</title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
        <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script language="Javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
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
          var oldgridcolor;
          function SetMouseOver(element) {
              oldgridcolor = element.style.backgroundColor;
              element.style.backgroundColor = '#ffeb95';
              element.style.cursor = 'pointer';
              element.style.textDecoration = 'underline';
          }
          function SetMouseOut(element) {
              element.style.backgroundColor = oldgridcolor;
              element.style.textDecoration = 'none';

          }
</script>
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
      $('form').live("submit", function () {
          ShowProgress();
      });
    </script>
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color :#999999;
        }
        
        .remove  
  {
    text-decoration:none;
  }
  
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                <td width="20%"></td>
                    <td width="80%" align="center" >
                   
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
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

       <asp:Panel ID="pnlContents" runat="server" Width="100%">
    <div>
        <div align="center">
                <asp:Label ID="lblHead" Text="Expense Consolidated View" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
        </div>
        <div>
                <table width="100%" align="center">
                    <tr>
                    <td width="2.5%"></td><td width="2.5%"></td><td width="2.5%"></td><td width="2.5%"></td>
                        <td align="left">
                            <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblRegionName" runat="server" SkinID="lblMand" Font-Bold="true" ></asp:Label>
                        </td>
                       
                        <td align="left">
                            <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        
                    </tr>
                </table>
           </div>
            <br />
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                          <%--  <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="95%">
                            </asp:Table>--%>
                                <asp:Panel ID="Panel1" runat="server">
                                <table width="100%">
                                    <caption>
                                        <asp:GridView ID="Grdprd" runat="server" AlternatingRowStyle-CssClass="alt"
                                            AutoGenerateColumns="true" CssClass="mGrid" EmptyDataText="No Records Found"
                                            GridLines="Both" HorizontalAlign="Center" BorderWidth="1" OnRowCreated="Grdprd_RowCreated" OnRowDataBound="Grdprd_RowDataBound"
                                            ShowHeader="False" Width="90%" Font-Names="Verdana"
                                            Font-Size="10">
                                            <HeaderStyle Font-Bold="False" />
                                            <PagerStyle CssClass="pgr" />
                                            <SelectedRowStyle BackColor="BurlyWood" />
                                            <AlternatingRowStyle CssClass="alt" />
                                            <RowStyle HorizontalAlign="center" VerticalAlign="Middle" Font-Size="11px" Font-Names="calibri" />
                                            <Columns>
                                            </Columns>
                                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </caption>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </tbody>
            </table>  
            </div>      
    </asp:Panel>
    </form>
</body>
</html>

