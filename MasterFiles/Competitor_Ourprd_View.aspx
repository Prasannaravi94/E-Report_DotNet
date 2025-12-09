<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Competitor_Ourprd_View.aspx.cs" Inherits="MasterFiles_Competitor_Ourprd_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <link type="text/css" rel="stylesheet" href="../css/Report.css" />
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
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

</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlbutton" runat="server">
        <table width="100%">
            <tr>
            <td width="20%"></td><td></td>
                <td width="80%" align="center" >
                <asp:Label ID="lblProd" runat="server" Text="Our Product - Competitor Product View" Font-Size="16px" ForeColor="Maroon"  Font-Bold="true" ></asp:Label>
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
     <br />
    <br />
    
    <center >
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <table width="100%" align="center">
            <tbody>
        <%--    <tr>
           
              <td align="center" >
               <asp:Label ID="lblfieldname" runat="server" Font-Size="14px" Text="Fieldforce Name:" ></asp:Label>
               <asp:Label ID="lblname" runat="server" SkinID="lblMand"></asp:Label>
              </td>
              
            </tr>--%>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdComp" runat="server" Width="60%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                        
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" RowStyle-Wrap="false"  
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField  HeaderText="Sl_No" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblSl_No" runat="server" Text='<%#Eval("Sl_No")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Our Product" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblOur_prd_name" runat="server"  Text='<%#Eval("Our_prd_name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                
                                 <asp:TemplateField  HeaderText="Competitor Name" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompetitor_name" runat="server"  Text='<%#Eval("Competitor_name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                
                                <asp:TemplateField  HeaderText="Competitor Product - Tagged" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lbltagged" runat="server"  Text='<%#Eval("Competitor_prd_name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                           

                               
                                                                                                                    
                            </Columns>
                               <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
         </asp:Panel>
       <%-- <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="70%" >
                            </asp:Table>
                        </td>
                    </tr>
                </tbody>
            </table>  --%>
       
        </center>
    </div>
    </form>
</body>
</html>

