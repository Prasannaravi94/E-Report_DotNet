<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payslip_Upload.aspx.cs" Inherits="MasterFiles_Options_Payslip_Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload - Screens</title>
    <style type="text/css">
        table, th
        {
            border: 1px solid black;
            border-collapse: collapse;
        }
        
        .padding
        {
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 3px;
            padding-bottom: 3px;
        }
        td.des
        {
            margin-left: 10px;
            vertical-align: top;
        }
        .bor
        {
            border-style: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <%--   <ucl:Menu ID="menu1" runat="server" />--%>
    <center>
        <h2>
            Upload - Screens
        </h2>
    </center>
    <br />
    <center>
        <asp:Label ID="lbldivi" runat="server" Text="Division Name" Visible="false"></asp:Label>
        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" AutoPostBack="true"
            Visible="false">
        </asp:DropDownList>
    </center>
    <center>
        <asp:Label ID="lblmode" runat="server" Text="(Upload Type)"></asp:Label>
        <asp:DropDownList ID="ddlmode" runat="server" SkinID="ddlRequired" AutoPostBack="true">
            <asp:ListItem Value="-1" Text="Payslip"></asp:ListItem>
        </asp:DropDownList>
    </center>
    <%-- <center>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblmonth" runat="server" Text="Month"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlmonth" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblyear" runat="server" Text="Year"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </center>--%>
    <div>
        <div style="margin-left: 90%">
            <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />
        </div>
        <br />
        <center>
            <asp:Table ID="tblLeave" BorderStyle="Solid" BackColor="White" BorderWidth="1" Width="90%"
                CellSpacing="5" CellPadding="5" runat="server">
                <asp:TableHeaderRow HorizontalAlign="Center" Font-Size="14px" Font-Names="Verdana">
                    <asp:TableHeaderCell BackColor="LightBlue" VerticalAlign="Middle" ColumnSpan="3">PaySlip Upload</asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell BackColor="LightBlue" VerticalAlign="Middle">Excel Creation</asp:TableHeaderCell>
                    <asp:TableHeaderCell BackColor="LightBlue" VerticalAlign="Middle">Generating the Excel</asp:TableHeaderCell>
                    <asp:TableHeaderCell BackColor="LightBlue" VerticalAlign="Middle">Uploading the Excel</asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow CssClass="padding" HorizontalAlign="Left">
                    <asp:TableCell BorderWidth="1" CssClass="des" VerticalAlign="Top" Width="250px">
                        <asp:TextBox ID="txtadd" SkinID="MandTxtBox" runat="server" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" Text="Add" BackColor="LightPink" runat="server" OnClick="AddValues" />
                        <asp:Button ID="btndelete" Text="Delete" BackColor="LightPink" runat="server" OnClick="DeleteValues"
                            Visible="false" />
                    </asp:TableCell>
                    <asp:TableCell BorderWidth="1" CssClass="padding">
                                                <asp:ListBox ID="lstAddColumns" runat="server" Width="250" Height="250">
                           
                        </asp:ListBox>
                    <%--    <div style="width: 250px; height: 300px; padding: 2px; overflow: auto; border: 1px solid Black;">
                            <asp:CheckBoxList class="bor" ID="lstAddColumns" runat="server">
                            </asp:CheckBoxList>
                        </div>--%>
                        <asp:DataGrid ID="dtgrid" runat="server" Visible="false">
                        </asp:DataGrid>
                        <br />
                        <br />
                        <asp:Button ID="btn" runat="server" BackColor="LightPink" Text="Generate" OnClick="btn_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" BackColor="LightPink" Text="Reset" OnClick="btnReset_OnClick"
                            Visible="false" />
                            <br />
                            <br />
                             <asp:RadioButtonList ID="RblSta" CssClass="bor" runat="server" RepeatColumns="2" Enabled="false"
                        Font-Names="Verdana" Font-Size="X-Small">
                        <asp:ListItem Value="0" Selected="True">Monthwise</asp:ListItem>
                        <asp:ListItem Value="1">Financial Year</asp:ListItem>
                        <asp:ListItem Value="2">Calendar Year</asp:ListItem>
                        <asp:ListItem Value="3">None</asp:ListItem>
                    </asp:RadioButtonList>
                    </asp:TableCell>
                    <asp:TableCell BorderWidth="1" CssClass="padding" Width="40%">
                        <table cellpadding="5px" cellspacing="5px" style="border: 1px solid Black; background-color: White">
                            <tr>
                                <td align="center" class="stylespc">
                                    <asp:Label ID="lblMoth" runat="server" Text="Month" SkinID="lblMand"></asp:Label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
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
                                </td>
                            </tr>
                            <tr>
                                <td class="stylespc" align="center">
                                    <asp:Label ID="lblToYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>
                                    <asp:DropDownList ID="ddlYear" runat="server" Width="80px" SkinID="ddlRequired">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                              <tr> <td class="stylespc" align="center">
                                            <asp:RadioButtonList ID="rdolstrpt" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" Font-Size="Small">
                                                <asp:ListItem Value="1" Text="Delete & Insert&nbsp;&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Insert&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                            </asp:RadioButtonList>
                              </td>     </tr>
                            <tr>
                                <td style="padding-left: 80px">
                                    <asp:Label ID="lblExcel" runat="server" SkinID="lblMand" Font-Size="Small" Font-Names="Verdana">Excel file</asp:Label>
                                    <asp:FileUpload ID="FlUploadcsv" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnUpload" runat="server" Width="70px" Height="25px" BackColor="BurlyWood"
                                        Font-Size="Medium" OnClick="btnUpload_Click" Text="Upload" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid Black; padding-left: 80px;">
                                    <asp:Label ID="lblIns" runat="server" Text="Note:" ForeColor="Red"></asp:Label>
                                    &nbsp; &nbsp;
                                    <asp:Label ID="Label1" runat="server" Font-Size="11px" Font-Names="Verdana" Width="280px"
                                        Text="1) Sheet Name Must be 'Upl_Payslip'"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblExc" runat="server" Text="Excel Format File" Font-Size="Medium"></asp:Label>
                                    <asp:LinkButton ID="lnkDownload" runat="server" Font-Size="12px" Font-Names="Verdana"
                                        Text="Download Here" OnClick="lnkDownload_Click"> 
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </center>
    </div>
    </form>
</body>
</html>
