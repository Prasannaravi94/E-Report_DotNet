<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doc_Spec_React.aspx.cs" Inherits="MasterFiles_Doc_Spec_React" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor Speciality Reactivation</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-11">
                    <br />
                    <h2 class="text-center">Doctor Speciality Reactivation</h2>
                    <div class="display-table clearfix">
                        <div class="table-responsive">
                            <asp:GridView ID="grdDocSpe" runat="server" Width="85%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                            OnPageIndexChanging="grdDocSpe_PageIndexChanging" OnRowCommand="grdDocSpe_RowCommand"
                            GridLines="None" CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speciality Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocSpeCode" runat="server" Text='<%#Eval("Doc_Special_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDoc_Spe_SName" runat="server" CssClass="input" Text='<%# Bind("Doc_Special_SName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDoc_Spe_SName" runat="server" Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speciality Name" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDocSpeName" CssClass="input" runat="server" MaxLength="100"
                                            Text='<%# Bind("Doc_Special_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocSpeName" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reactivate" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutReactivate" runat="server" CommandArgument='<%# Eval("Doc_Special_Code") %>'
                                            CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Doctor Speciality');">Reactivate
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="no-result-area"/>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
