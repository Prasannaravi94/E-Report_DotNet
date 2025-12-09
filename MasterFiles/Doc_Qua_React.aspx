<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doc_Qua_React.aspx.cs" Inherits="MasterFiles_Doc_Qua_React" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor Qualification Reactivation</title>

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
                    <h2 class="text-center">Doctor Qualification Reactivation</h2>
                    <div class="display-table clearfix">
                        <div class="table-responsive"  style=" scrollbar-width: thin;">
                            <asp:GridView ID="grdDocQua" runat="server" Width="500px" HorizontalAlign="Center" 
                             AutoGenerateColumns="false" AllowPaging="True" PageSize="10" 
                             onrowcommand="grdDocQua_RowCommand" EmptyDataText="No Records Found"  
                             onpageindexchanging="grdDocQua_PageIndexChanging" GridLines="None" CssClass="table" PagerStyle-CssClass="pgr" 
                               AlternatingRowStyle-CssClass="alt" AllowSorting="True" >
                             <PagerStyle CssClass="GridView1"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="#"  ItemStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" Width="25px" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qualification Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocQuaCode" runat="server" Text='<%#Eval("Doc_QuaCode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate> 
                                            <asp:TextBox ID="txtDoc_Qua_SName" runat="server" SkinID="TxtBxAllowSymb"  Text='<%# Bind("Doc_QuaSName") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoc_Qua_SName" runat="server" Text='<%# Bind("Doc_QuaSName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Qualification Name" ItemStyle-Width="235px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDocQuaName" CssClass="input" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocQuaName" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reactivate" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbutReactivate" runat="server" CommandArgument='<%# Eval("Doc_QuaCode") %>'
                                                CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Doctor Qualification');">Reactivate
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
