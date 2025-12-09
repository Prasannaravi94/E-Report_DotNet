<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_Reactivation.aspx.cs" Inherits="MasterFiles_MR_Territory_Territory_Reactivation" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reactivation</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div id="Divid" runat="server">
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <br />
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">Reactivation</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:Panel ID="pnlsf" runat="server" style="text-align: center;font-size: 18px;" HorizontalAlign="Right" CssClass="marRight">
                                    <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                                </asp:Panel>
                                <br /><br />
                                <center>
                                    <asp:GridView ID="grdTerr" runat="server" Width="50%" HorizontalAlign="Center" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt" OnRowCommand="grdTerr_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Territory Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Territory_Name" ShowHeader="true" ItemStyle-HorizontalAlign="Left" SortExpression="Territory_Name"
                                                HeaderText="Name"/>
                                            <asp:BoundField DataField="Territory_Cat" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Type" ItemStyle-Width="15%" />
                                            <asp:TemplateField HeaderText="Reactivate" ItemStyle-HorizontalAlign="Center">
                                                <%--<ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="true" />
                                                <ItemStyle ForeColor="DarkBlue" Font-Bold="false" />--%>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Territory_Code") %>'
                                                        CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate');">Reactivate 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area"/>
                                    </asp:GridView>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
        <%--<table width="90%">
        <tr> 
          <td align="right" width="30%">--%>
                 <%--   <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" SkinID="lblMand" Visible="true"></asp:Label>--%>
                <%--</td>
                </tr>
                <tr>
                <td align="right">
                    <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server" 
                    onclick="btnBack_Click" />
                    </td>                    
     </tr>
     </table>--%>
  
    
    </div>
    </form>
</body>
</html>
