<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheCamp_React.aspx.cs" Inherits="MasterFiles_Chemist_Campaign_CheCamp_React" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Chemist Campaign Reactivation</title>
<%--       <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center" id="H1" runat="server"></h2>
                        <div class="designation-reactivation-table-area clearfix">

                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin">
                        <asp:GridView ID="grdCheSpe" runat="server" Width="85%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                            OnPageIndexChanging="grdCheSpe_PageIndexChanging" OnRowCommand="grdCheSpe_RowCommand" GridLines="None"
                             CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Campaign Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCheSpeCode" runat="server" Text='<%#Eval("chm_campaign_code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="Black" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                
                                    <ItemTemplate>
                                        <asp:Label ID="lblChe_Spe_SName" runat="server" Text='<%# Bind("chm_campaign_SName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="Black" HeaderText="Campaign Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCheSpeName" runat="server" Text='<%# Bind("chm_campaign_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reactivate" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutReactivate" runat="server" CommandArgument='<%# Eval("chm_campaign_code") %>'
                                            CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Chemist Campaign');">Reactivate
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
  <EmptyDataRowStyle CssClass="no-result-area"/>                       
                                         </asp:GridView>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
                <%--<asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />--%>
            </div>
                  
    </div>
    </form>
</body>
</html>

   