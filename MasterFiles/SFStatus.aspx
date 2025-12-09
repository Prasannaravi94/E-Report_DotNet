<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SFStatus.aspx.cs" Inherits="MasterFiles_SFStatus" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FieldForce Status</title>
   <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>

    <script language="javascript" type="text/javascript">
        function popUp(sf_type) {
            strOpen = "UserList_NewWindow.aspx?sf_type=" + sf_type
            window.open(strOpen, 'popWindow', ''); //toolbar = 0, scrollbars = 1, location = 0, statusbar = 1, menubar = 0, resizable = 1, width = 600, height = 800, left = 0, top = 0
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">FieldForce Status</h2>
                        <div class="designation-reactivation-table-area clearfix">

                            <div class="display-Approvaltable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center" OnRowDataBound="grdSalesForce_RowDataBound"
                                        AutoGenerateColumns="false" OnPreRender="grdSalesForce_PreRender" OnRowCreated="grdSalesForce_RowCreated"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" ShowFooter="true"
                                        ShowHeader="False">
                                      
                                        <Columns>
                                            <asp:TemplateField>
                                                <ControlStyle Width="90%"></ControlStyle>
                                             
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ControlStyle Width="90%"></ControlStyle>
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblTot" Text="Total" Font-Bold="true" ForeColor="Red" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                       <asp:LinkButton ID="lnksum" runat="server" CausesValidation="False"
                                           OnClientClick='<%# "return popUp(\"" + Eval("sf_type") + "\");" %>'>
                                       </asp:LinkButton>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("MR_Count") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblMR_Count" Font-Bold="true" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSFType" runat="server" Text='<%#Eval("MGR_Count")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblMGR_Count" Font-Bold="true" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActive_count" runat="server" Text='<%# Bind("Active_MR_Count") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblActMR_Count" Font-Bold="true" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmgr_count" runat="server" Text='<%# Bind("Active_MGR_Count") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblActMGR_Count" Font-Bold="true" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                                <ControlStyle Width="90%"></ControlStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeactive_count" runat="server" Text='<%# Bind("DeActive_MR_Count") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblDeActMR_Count" Font-Bold="true" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeact_mgr_count" runat="server" Text='<%# Bind("DeActive_MGR_Count") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblDeActMGR_Count" Font-Bold="true" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                                <ControlStyle Width="90%"></ControlStyle> 
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBlock_count" runat="server" Text='<%# Bind("Block_MR_Count") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblBlockMR_Count" Font-Bold="true" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBlock_mgr" runat="server" Text='<%# Bind("Block_MGR_Count") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblBlockMGR_Count" Font-Bold="true" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                                <ControlStyle Width="90%"></ControlStyle>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="total" Font-Bold="true" runat="server"></asp:Label>
                                                </ItemTemplate>


                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                    </div>
                     <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click"  />
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
