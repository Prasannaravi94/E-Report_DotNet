<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptinputstatus_New2.aspx.cs" Inherits="MIS_Reports_rptinputstatus_New2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link type="text/css" rel="stylesheet" href="../css/Report.css" />
     <script language="Javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:Panel ID="pnlbutton" runat="server">
        <table width="100%">
            <tr>
            <td width="20%"></td><td></td>
                <td width="80%" align="center" >
                <asp:Label ID="lblProd" runat="server" Text="Input Details" Font-Underline="true" ForeColor="#794044" Font-Size="14px" ></asp:Label>
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
                                    OnClick="btnExcel_Click" />
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
    </center>
    <center >
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <table width="100%" align="center">
            <tbody>
            <tr>
            <%--<td ></td>--%>
              <td align="center" >
               <asp:Label ID="lblfieldname" runat="server" Font-Size="14px" Text="Fieldforce Name:" ></asp:Label>
               <asp:Label ID="lblname" runat="server" Font-Bold="true" ForeColor="Magenta"></asp:Label>
              </td>
              
            </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdprd" runat="server" Width="70%" HorizontalAlign="Center" EmptyDataText="No Records Found" OnRowDataBound="grdDr_RowDataBound" 
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"  
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField  HeaderText="gift_code" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblprd_code" runat="server" Width="120px" Text='<%#Eval("gift_code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblforce" runat="server" Width="120px" Text='<%#Eval("FeildforceName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Designation" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesig" runat="server" Width="80px" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                 <asp:TemplateField  HeaderText="HQ" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblhq" runat="server" Width="100px" Text='<%#Eval("sf_HQ")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>        
                                
                                <asp:TemplateField  HeaderText="Input Name" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblprd_name" runat="server" Width="120px" Text='<%#Eval("Gift_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                
                              
                                  <asp:TemplateField HeaderText="OB" ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="70px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblopening" runat="server" Font-Size="10px" Text='<%# Bind("opening") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Despatch Quantity" ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="70px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblsamqty" runat="server" Font-Size="10px" Text='<%# Bind("Input_qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Issued Quantity" ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="80px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblissued" runat="server" Font-Size="10px" Text='<%# Bind("Issed_qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="CB" ItemStyle-HorizontalAlign="Left"  
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="150px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblclosing" runat="server" Font-Size="10px" Text='<%# Bind("Closing") %>'></asp:Label>
                        </ItemTemplate>
                       
                    </asp:TemplateField>
                                
                                                                                                                    
                            </Columns>
                               <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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


</html>
