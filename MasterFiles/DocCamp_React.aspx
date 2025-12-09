<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocCamp_React.aspx.cs" Inherits="MasterFiles_DocCamp_React" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor Campaign Reactivation</title>
    <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center" id="Heading" runat="server"></h2>
                        <div class="designation-reactivation-table-area clearfix">

                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin">
                                    <asp:GridView ID="grdDocSpe" runat="server" Width="85%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                                        OnPageIndexChanging="grdDocSpe_PageIndexChanging" OnRowCommand="grdDocSpe_RowCommand"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt"
                                        AllowSorting="True">
                                     
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Campaign Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocSpeCode" runat="server" Text='<%#Eval("Doc_SubCatCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblDoc_Spe_SName" runat="server" Text='<%# Bind("Doc_SubCatSName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Campaign Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocSpeName" runat="server" Text='<%# Bind("Doc_SubCatName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reactivate" ItemStyle-HorizontalAlign="Center">
                                             
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutReactivate" runat="server" CommandArgument='<%# Eval("Doc_SubCatCode") %>'
                                                        CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Doctor Campaign');">Reactivate
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
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
