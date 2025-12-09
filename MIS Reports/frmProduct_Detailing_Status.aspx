<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmProduct_Detailing_Status.aspx.cs" Inherits="MIS_Reports_frmProduct_Detailing_Status" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <center>
                <table cellpadding="0" cellspacing="5">
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblDivision" Visible="false" runat="server" SkinID="lblMand" Text="Division "></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlDivision" runat="server" Visible="false" SkinID="ddlRequired"
                                Width="350" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true">
                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                SkinID="ddlRequired">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" 
                                SkinID="ddlRequired">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                            </asp:DropDownList>
                            <%--<asp:CheckBox ID="chkVacant" Text=" Only Vacant Managers" SkinID="ddlRequired" AutoPostBack="true" 
                       OnCheckedChanged="chkVacant_CheckedChanged" runat="server" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblFrmMoth" runat="server" Text="From Month" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlFrmMonth" runat="server" SkinID="ddlRequired">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="From Year"></asp:Label>
                            <asp:DropDownList ID="ddlFrmYear" runat="server" Width="80px" SkinID="ddlRequired">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td align="left" class="stylespc">
                            <asp:Label ID="Label4" runat="server" SkinID="lblMand" Text="To Month"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlToMonth" runat="server" SkinID="ddlRequired">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblToYear" runat="server" Text="To Year" SkinID="lblMand"></asp:Label>
                            <span style="margin-left: 14px"></span>
                            <asp:DropDownList ID="ddlToYear" runat="server" Width="80px" SkinID="ddlRequired">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>

                          
                        </td>
                    </tr>
                    <tr align="center"><td colspan="2">  <asp:Button ID="btnSubmit" runat="server" Width="60px" Height="25px"
                                Text="View" CssClass="savebutton" OnClick="btnSubmit_Click" /></td></tr>
                    <tr align="center">
                        <td colspan="2">
                            <asp:Label ID="lblHead" runat="server" Font-Bold="true" Font-Underline="true"></asp:Label>
                        </td>
                        
                    </tr>
                   
                   
                   
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>

                        <td colspan="2">
                            <asp:GridView ID="GvDcrCount" runat="server" AutoGenerateColumns="false" CssClass="mGrid" OnRowDataBound="GvDcrCount_OnRowDataBound" >
                                <HeaderStyle BorderWidth="1" Font-Names="Verdana" Font-Size="8pt" />
                                <RowStyle BorderWidth="1" Font-Names="Verdana" Font-Size="8pt" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                                        HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40"
                                        Visible="false" HeaderStyle-CssClass="" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSfcode" runat="server" Text='<%# Bind("sf_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Field Force Name" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle Width="220px"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSf_Name" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle Width="220px"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSf_Hq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="designation" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesig_color" Text='<%# Bind("sf_Designation_Short_Name") %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Code" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsf_emp_id" Text='<%# Bind("sf_emp_id") %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month" ItemStyle-HorizontalAlign="Left" Visible="false"
                                        ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblmon" Text='<%# Bind("mon") %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Left" Visible="false"
                                        ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" Text='<%# Bind("Year") %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="div_Code" ItemStyle-HorizontalAlign="Left" Visible="false"
                                        ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle Width="100"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldiv_Code" Text='<%# Bind("division_code") %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Minutes Spent for Detailing" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle Width="200px"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hypDCRCount" Target="_blank" runat="server" Text='<%# Eval("Minutes_Spent") %>'
                                                NavigateUrl='<%# String.Format("frmProductMinus_Dls.aspx?sfcode={0}&Mon={1}&Year={2}&div_code={3}&Sf_Name={4}&Sf_HQ={5}&sf_Designation_Short_Name={6}&sf_emp_id={7}",
                                                Eval("sf_Code"), Eval("Mon"), Eval("Year"),Eval("division_code"),Eval("Sf_Name"),Eval("Sf_HQ"),Eval("sf_Designation_Short_Name"),Eval("sf_emp_id")) %>'> 
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <br />

                <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                    Width="60%">
                </asp:Table>
            </center>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
