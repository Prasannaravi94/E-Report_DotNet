<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feed_Back_View.aspx.cs" Inherits="Feed_Back_View" %>
<%@ Register Src="~/UserControl/AdminMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FeedBack View</title>
      <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
      <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
  <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
       <asp:GridView ID="grdDivision" runat="server" Width="85%" HorizontalAlign="Center"
                                    AutoGenerateColumns="false"  EmptyDataText="No Records Found"
                                  
                                  
                                  
                                    GridLines="None" CssClass="mGrid" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt"
                                    >
                                    <HeaderStyle Font-Bold="False" />
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="20px" HeaderStyle-ForeColor="white"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Div_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDivCode" runat="server" Text='<%#Eval("Division_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Feed_Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFeed" runat="server" Text='<%#Eval("Feed_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-Width="280px" HeaderStyle-ForeColor="white"
                                            HeaderText="Division Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiv" runat="server" Text='<%# Bind("Div_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Status" HeaderStyle-ForeColor="White" HeaderStyle-Width="120px">
                                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                            </ControlStyle>
                                            <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Division_Code") %>'
                                                    CommandName="Completed" OnClientClick="return confirm('Do you want to Complete the division');">Completed
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:HyperLinkField HeaderText="View" Text="Click Here to View" HeaderStyle-Width="100px" DataNavigateUrlFormatString="Feed_Back_Form.aspx?Div_Code={0}&Feed_ID={1}"
                                            DataNavigateUrlFields="Division_code,Feed_ID" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Center">
                                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                            </ControlStyle>
                                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                        </asp:HyperLinkField>
                                        </Columns>
                                        </asp:GridView>
                                        </center>
    </div>
    </form>
</body>
</html>
