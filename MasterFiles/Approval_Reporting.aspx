<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approval_Reporting.aspx.cs" Inherits="MasterFiles_Approval_Reporting" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subordinate Details</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <center>
    <div>
    <asp:Label ID="lblHead" Text="Subordinate Details" ForeColor="Chocolate" Font-Bold="true" Font-Underline="true" Font-Size="16px"  
                runat="server"></asp:Label>
                </div>
                </center>
                <br />
    <br />

      <div>
                <table width="90%" align="center">
                    <tr>
                    <td width="17%"></td>
                        <td align="left">
                            <asp:Label ID="Label1" Text="Filed Force Name :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblRegionName" runat="server" Font-Bold="true" ForeColor="#ff0066"></asp:Label>
                        </td>
                       
                        <td align="left">
                            <asp:Label ID="lblreprt" Text="Reporting :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblRept_name" runat="server" Font-Bold="true" ForeColor="#660066"></asp:Label>
                        </td>
                      
                    </tr>
                </table>
           </div>
    <br />

 

           <br />


        <table width="100%" align="center">
                        <tbody>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:GridView ID="grdSalesForce" runat="server" Width="70%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                        AutoGenerateColumns="False"  GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                          OnRowCreated="grdSalesForce_RowCreated"
                                        ShowHeader="False">
                                        <HeaderStyle Font-Bold="False" />
                                        <PagerStyle CssClass="gridview1"></PagerStyle>
                                        <RowStyle Wrap="true" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px"  HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%# Bind("SF_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForce Name" >
                                               
                                                <ItemStyle BorderStyle="Solid" Width="200px" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" >
                                               
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="HQ">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                       
                                            <asp:TemplateField HeaderText="DCR_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDCRReporting" runat="server" Text='<%# Bind("DCR_AM") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TP_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("TP_AM") %>'></asp:Label>
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LstDr_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLstReporting" runat="server" Text='<%# Bind("LstDr_AM") %>'></asp:Label>
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLeaveReporting" runat="server" Text='<%# Bind("Leave_AM") %>'></asp:Label>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Expense_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpReporting" runat="server" Text='<%# Bind("Expense_AM") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                           
                                        </Columns>
                                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                                    </asp:GridView>
                                </td>
                            </tr>


                        </tbody>
                    </table>
    
    </div>
    </form>
</body>
</html>
