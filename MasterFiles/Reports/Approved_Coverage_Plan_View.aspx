<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approved_Coverage_Plan_View.aspx.cs"
    Inherits="MIS_Reports_Approved_Coverage_Plan_View" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="Title1" runat="server">APPROVED COVERAGE PLAN</title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <style type="text/css">
        .tr_det_head
        {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }
    </style>
    <style type="text/css">
        .bar
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="100%">
            <tr>
                <td width="80%">
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" Visible="false"
                                    OnClick="btnExcel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        
            
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="APPROVED COVERAGE PLAN" Font-Underline="True"
                        Font-Bold="True" Font-Size="9pt"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                        Font-Size="9pt"></asp:Label>
                </div>
                <br />
                <asp:Panel ID="pnlContents" runat="server" Width="100%">
                <table width="90%" align="center">
                    <tr>
                        <td align="center" width="90%" colspan="2">
                            <asp:GridView ID="grdTerrPlan" runat="server" Width="100%" HorizontalAlign="Center"
                                GridLines="Both" Font-Names="calibri" Font-Size="small" BorderWidth="1" AutoGenerateColumns="false"
                                HeaderStyle-Font-Size="8pt" CssClass="mGrid">
                                <HeaderStyle BorderWidth="1" />
                                <RowStyle BorderWidth="1" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                        HeaderStyle-ForeColor="White">
                                        <ControlStyle></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MARKET/s NAME" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                        HeaderStyle-ForeColor="White">
                                        <ControlStyle></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTName" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            <asp:HiddenField ID="drCodeHidden" runat="server" Value='<%#Eval("Territory_Code")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ/SUB/OS" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                        HeaderStyle-ForeColor="White">
                                        <ControlStyle></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTerr" runat="server" Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dist.from one way(KM)" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDist" runat="server" Text='<%# Bind("Distance_in_kms")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Allow" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                        HeaderStyle-ForeColor="White">
                                        <ControlStyle></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblallow" runat="server" Text='<%# Bind("Allow")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mode of Transfer" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="MOT" runat="server" Text='BIKE'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To and fro fare" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                        <ControlStyle></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="ToFro" runat="server" Text='<%# Bind("fare")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="** Drs" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                        HeaderStyle-ForeColor="White">
                                        <ControlStyle></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="MultiDrs" runat="server" Text='<%# Bind("MultiDrs")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="* Drs" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                        HeaderStyle-ForeColor="White">
                                        <ControlStyle></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="SingleDrs" runat="server" Text='<%# Bind("SingleDrs")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Drs" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                        HeaderStyle-ForeColor="White">
                                        <ControlStyle></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="tbldrs" runat="server" Text='<%# Bind("tbldrs")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Chm" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                        HeaderStyle-ForeColor="White">
                                        <ControlStyle></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="tblChm" runat="server" Text=''></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
             
                    <tr>
                        <td width="40%" align="center">
                            <table width="100%">
                                   <tr>
                                    <td >
                                        <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" GridLines="Both"
                                            Font-Names="calibri" Font-Size="small" BorderWidth="1" AutoGenerateColumns="false"
                                            HeaderStyle-Font-Size="8pt" Width="100%" CssClass="mGrid">
                                            <HeaderStyle BorderWidth="1" />
                                            <RowStyle BorderWidth="1" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                    HeaderStyle-ForeColor="White">
                                                    <ControlStyle></ControlStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblhq" runat="server" Text='<%# Bind("HQ")%>'></asp:Label>
                                                        <asp:HiddenField ID="drCodeHidden" runat="server" Value='' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EX" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                    HeaderStyle-ForeColor="White">
                                                    <ControlStyle></ControlStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblex" runat="server" Text='<%# Bind("EX")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OS" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                    HeaderStyle-ForeColor="White">
                                                    <ControlStyle></ControlStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblos" runat="server" Text='<%# Bind("OS")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" align="center">
                            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                width="100%">
                            </asp:Table>
                            <asp:Label ID="lblNoRecord" runat="server" ForeColor="Black" BackColor="AliceBlue"
                                Visible="false" Width="100%" Height="20px" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2" Font-Bold="True">No Records Found</asp:Label>
                        </td>
                    </tr>
             
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Table ID="tbldrs" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                Width="100%">
                            </asp:Table>
                            <asp:Label ID="lblrcd" runat="server" ForeColor="Black" BackColor="AliceBlue" Visible="false"
                                Height="20px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True">No Records Found</asp:Label>
                        </td>
                    </tr>
                </table>
               
            </asp:Panel>
        
    </div>
    </form>
</body>
</html>
