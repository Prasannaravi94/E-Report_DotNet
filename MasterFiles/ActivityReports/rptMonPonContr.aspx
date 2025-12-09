<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptMonPonContr.aspx.cs" Inherits="MasterFiles_ActivityReports_rptMonPonContr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
</head>
<body style="background-color:lightcyan">
    <form id="form1" runat="server">
        <center>
        <div>
            <div>
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblHead" Font-Size="X-Large" Font-Underline="true" Font-Bold="true" SkinID="lblMand" runat="server"></asp:Label>
                        </td>                         
                    </tr>
                   <tr>
                       <td style="height:20px"></td>
                   </tr>
                    <tr>
                        <td><asp:Label ID="lblMon" SkinID="lblMand" runat="server" Text="Month"></asp:Label> </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired"></asp:DropDownList>
                        </td>
                         <td><asp:Label ID="lblYear" SkinID="lblMand" runat="server" Text="Year"></asp:Label> </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired"></asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                       <td style="height:20px"></td>
                   </tr>
                    <tr align="center">
                        <td colspan="4">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="View" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:GridView ID="gvMnthPon" runat="server" AutoGenerateColumns="false" CssClass="footable" EmptyDataText="No Records Found"
                    AlternatingRowStyle-CssClass="alt" GridLines="Both" HorizontalAlign="Center" BorderWidth="1" RowStyle-Font-Size="8pt"
                    HeaderStyle-Font-Size="9pt">
                    <Columns>
                        <asp:TemplateField
                            HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" HeaderStyle-Height="30px">
                            <ControlStyle Width="90%"></ControlStyle>
                          
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField
                            HeaderText="sf_code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" Visible="false">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNextMonth"  runat="server" Text='<%# Bind("sf_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField
                            HeaderText="Listed Dr Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDrname"  runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField
                            HeaderText="Category" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDrname"  runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField
                            HeaderText="Speciality" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSpec"  runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField
                            HeaderText="Territory Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSpec"  runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField
                            HeaderText="Visit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblVisit"  runat="server" Text='<%# Bind("Visit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField
                            HeaderText="Pontentcial" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPon"  runat="server" Text='<%# Bind("Pontentcial") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField
                            HeaderText="Average" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblAverage"  runat="server" Text='<%# Bind("Average") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField
                            HeaderText="Yield" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblContri"  runat="server" Text='<%# Bind("Contribution") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField
                            HeaderText="Monthly Potential" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblContri"  runat="server" Text='<%# Bind("MnPont") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
        </div>
            </center>
    </form>
</body>
</html>
