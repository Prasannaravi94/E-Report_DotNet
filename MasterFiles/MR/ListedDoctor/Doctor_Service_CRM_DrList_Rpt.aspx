<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_CRM_DrList_Rpt.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_DrList_Rpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View</title>
    <link href="../../../css/Report.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <center>
    <h1 style="font-family:Calibri;font-size:14px;color:Purple">Doctor List</h1>
    </center>
    <br />

    <asp:Panel ID="Panel1" runat="server" Width="100%">
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdDrCRM" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Docotor Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDr_Code" runat="server" Width="120px" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" 
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrName" runat="server"  Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpeciality"  runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            
                                <asp:TemplateField  HeaderText="Qualification" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQualif" runat="server"  Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                      
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Service Date" ItemStyle-HorizontalAlign="Left"
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblServicDate" runat="server" Text='<%# Bind("Created_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField  HeaderText="Service Amount" ItemStyle-HorizontalAlign="Left"
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblService" runat="server"  Text='<%# Bind("Service_Amt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Target Amount" ItemStyle-HorizontalAlign="Left"
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblTarget" runat="server"  Text='<%# Bind("Total_Business_Expect") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Business Received (Rs)" ItemStyle-HorizontalAlign="Left"
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotal" runat="server"  Text='<%# Bind("Total") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                               <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
