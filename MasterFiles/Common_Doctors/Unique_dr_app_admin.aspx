<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unique_dr_app_admin.aspx.cs" Inherits="MasterFiles_Common_Doctors_Unique_dr_app_admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menumas" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Unique Doctor Approval</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <ucl:Menumas ID="menumas" runat="server" /> 
     <br />
     <center>
            <table width="100%" align="center">
                <tbody>
                 
                    <tr style="height: 25px">
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdUnique" runat="server" Width="65%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Data found for Approval's" GridLines="None"
                                CssClass="mGridImg" PagerStyle-CssClass="pgr" RowStyle-Font-Size="Smaller" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" Font-Size="Smaller" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SF Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldForce Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Designation" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Dr Cnt">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldrcnt" runat="server" Text='<%#Eval("dr_cnt")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                              
                                     <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="UniqueDR_Add_App_admin.aspx?sfcode={0}"
                               DataNavigateUrlFields="SF_Code" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>   
                                </Columns>
                                  <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
            </center>
    </div>
    </form>
</body>
</html>
