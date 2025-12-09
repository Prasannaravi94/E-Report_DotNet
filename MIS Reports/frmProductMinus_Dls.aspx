<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmProductMinus_Dls.aspx.cs" Inherits="MIS_Reports_frmProductMinus_Dls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
    <div>
         <asp:Label ID="lblName" runat="server" Font-Bold="true" Font-Underline="true"></asp:Label>
        <br />       
        <asp:Label ID="lblFieldForce" runat="server" Font-Bold="true" Font-Underline="true"></asp:Label>
             <br />       
             <br />   
     <asp:GridView ID="GvDcrCount" runat="server" AutoGenerateColumns="false" CssClass="mGrid" 
                          OnRowDataBound="OnRowDataBound_GvDcrCount" ShowFooter="true">
                    <HeaderStyle BorderWidth="1" Font-Names="Verdana" Font-Size="8pt" />
                    <FooterStyle HorizontalAlign="Right" />
                    <RowStyle BorderWidth="1" Font-Names="Verdana" Font-Size="8pt" />
                    <Columns>
                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Brand Name" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="220px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSf_Name" runat="server" Text='<%# Bind("Product_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblTotName" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Minutes Spent for Detailing" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="220px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSf_Hq" runat="server" Text='<%# Bind("Product_Count") %>'></asp:Label>
                            </ItemTemplate>
                              <FooterTemplate>
                                 <asp:Label ID="lblPrdCount" Font-Bold="true" runat="server"></asp:Label>
                             </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No.Of.Drs" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="220px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblReporting" Text='<%# Bind("DrCount") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID ="lblTotalDr" Font-Bold="true" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Average in seconds" ItemStyle-HorizontalAlign="Left" Visible="true"
                            ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="220px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblAverage" runat="server"></asp:Label>
                            </ItemTemplate>                           
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
    </div>
            </center>
    </form>
</body>
</html>
