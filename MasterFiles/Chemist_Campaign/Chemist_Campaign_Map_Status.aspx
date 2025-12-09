<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chemist_Campaign_Map_Status.aspx.cs" Inherits="MasterFiles_Chemist_Campaign_Chemist_Campaign_Map_Status" %>

<!DOCTYPE html>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   
    <title id="Title1" runat="server">Chemist - Campaign Map Details</title>
 
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <style type="text/css">
        .tr_det_head
        {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }
    </style>
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .bar
        {
            text-align: center;
        }
    </style>
</head>
<body>
   <form id="form1" runat="server">
    <div>
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
                                        OnClick="btnExcel_Click" />
                                </td>
                                <%--<td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPDF_Click" />
                                </td>--%>
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
            <br />
            <center>
                         
                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <div align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Chemist - Campaign Map Details" Font-Underline="True"
                                Font-Bold="True" Font-Size="9pt"></asp:Label>
                                <br />
                            <br />
                                <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="9pt"></asp:Label>
                                </div>
                       <br />
                      <table width="100%" align="center">  
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grdChemist" runat="server" Width="90%" HorizontalAlign="Center" GridLines="Both" Font-Names="calibri" Font-Size="small"
                                    BorderWidth="1" AutoGenerateColumns="false"  OnRowDataBound="grdChemist_RowDataBound" HeaderStyle-Font-Size="8pt"
                                    CssClass="mGrid">
                                     
                                    <HeaderStyle BorderWidth="1" />
                                    <RowStyle BorderWidth="1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("Chemists_Name") %>'></asp:Label>
                                                <asp:HiddenField ID="drCodeHidden" runat="server" Value='<%#Eval("Chemists_Code")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Chemists_Address1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                             
                                        <asp:TemplateField HeaderText="Root Plan" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                 
                                        <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Chemists_Mobile") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMail" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("Chemists_Email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Campaign" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCampaign" runat="server" Text='<%# Bind("Campaign") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
                    
                    </td>
                    </tr>
                    </asp:Panel>
                  
                    
            </table>
        </center>
    </div>
    </form>
</body>
</html>
