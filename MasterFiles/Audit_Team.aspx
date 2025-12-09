<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Audit_Team.aspx.cs" Inherits="MasterFiles_Audit_Team" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Audit-ID Creation</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />


            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <div class="designation-reactivation-table-area clearfix">
                            <h2 class="text-center">Audit-ID Creation</h2>

                            <table width="100%" align="center">
                                <tbody>
                                    <tr align="right">
                                        <td>
                                            <asp:HyperLink ID="href" runat="server" NavigateUrl="Audit_Team_Cancel.aspx" ForeColor="White">.</asp:HyperLink>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                            <div class="display-table clearfix">
                                <div class="table-responsive">

                                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        OnRowCreated="grdSalesForce_RowCreated" OnRowDataBound="grdSalesForce_RowDataBound"
                                        GridLines="None" CssClass="table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Sf_UserName" HeaderText="User Name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtUsrName" runat="server" CssClass="input" Text='<%# Bind("Sf_UserName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUsrName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Sf_Name" HeaderText="FieldForce Name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtsfName" CssClass="input" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--                            <asp:TemplateField HeaderText="Type">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSFType" runat="server" Text='<%#Eval("SF_Type")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                            --%>
                                            <asp:TemplateField SortExpression="Sf_HQ" HeaderText="HQ">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtHQ" CssClass="input" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:HyperLinkField HeaderText="Select Audit Team" Text="Select Audit Team" DataNavigateUrlFormatString="~/MasterFiles/Audit_Team_Selection_New.aspx?sfcode={0}" DataNavigateUrlFields="SF_Code" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <asp:Button ID="btnback" runat="server" Text="Back" CssClass="backbutton" OnClick="btnback_Click" />
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
