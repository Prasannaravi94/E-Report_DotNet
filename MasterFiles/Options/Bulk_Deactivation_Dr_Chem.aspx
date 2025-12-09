<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bulk_Deactivation_Dr_Chem.aspx.cs"
    Inherits="MasterFiles_Options_Bulk_Deactivation_Dr_Chem" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listeddr Deactivation - Multiple Fieldforce</title>
     <link type="text/css" rel="stylesheet" href="../../css/style.css" />
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
        td.stylespc
        {
            padding-bottom: 5px;
            padding-right: 5px;
        }
        .space
        {
            padding: 3px 3px;
        }
          .aclass
        {
            border: 1px solid lighgray;
        }
        .aclass
        {
            width: 50%;
        }
        .aclass tr td
        {
            background: White;
            font-weight: bold;
            color: Black;
            border: 1px solid black;
            border-collapse: collapse;
        }
        .aclass th
        {
            border: 1px solid black;
            border-collapse: collapse;
            background: LightBlue;
        }
        .lbl
        {
            color: Red;
        }
        
        
        .space
        {
            padding: 3px 3px;
        }
        .sp
        {
            padding-left: 11px;
        }
        
        .style6
        {
            padding: 3px 3px;
            height: 28px;
        }
        .marRight
        {
            margin-right: 35px;
        }
        
        .boxshadow
        {
            -moz-box-shadow: 3px 3px 5px #535353;
            -webkit-box-shadow: 3px 3px 5px #535353;
            box-shadow: 3px 3px 5px #535353;
        }
        .roundbox
        {
            -moz-border-radius: 6px 6px 6px 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px 6px 6px 6px;
        }
        .grd
        {
            border: 1;
            border-color: Black;
        }
        .roundbox-top
        {
            -moz-border-radius: 6px 6px 0 0;
            -webkit-border-radius: 6px 6px 0 0;
            border-radius: 6px 6px 0 0;
        }
        .roundbox-bottom
        {
            -moz-border-radius: 0 0 6px 6px;
            -webkit-border-radius: 0 0 6px 6px;
            border-radius: 0 0 6px 6px;
        }
        .gridheader, .gridheaderbig, .gridheaderleft, .gridheaderright
        {
            padding: 6px 6px 6px 6px;
            background: #003399 url(images/vertgradient.png) repeat-x;
            text-align: center;
            font-weight: bold;
            text-decoration: none;
            color: khaki;
        }
        .gridheaderleft
        {
            text-align: left;
        }
        .gridheaderright
        {
            text-align: right;
        }
        .gridheaderbig
        {
            font-size: 135%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ucl:Menu ID="menu1" runat="server" />
    <br />
    <div>
        <center>
            <table>
                <tr>
                    <td class="stylespc" align="left">
                        <asp:Label ID="lblmode" runat="server" SkinID="lblMand" Text="Mode"></asp:Label>
                    </td>
                    <td class="stylespc" align="left">
                        <asp:DropDownList ID="ddlmode" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            onselectedindexchanged="ddlmode_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Doctor"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Chemist"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </center>
          <br />
        <center>
            <div class="roundbox boxshadow" style="width: 60%; border: solid 2px steelblue;">
                <div class="gridheaderleft">
                  Paste UserName
                </div>
                <div class="boxcontenttext" style="background: White;">
                    <div id="pnlPreviewSurveyData">
                        <br />
                        <asp:Table ID="tblLeave" BorderStyle="Solid" BackColor="White" BorderWidth="1" Width="50%"
                            CellSpacing="5" CellPadding="5" runat="server">
                            <%-- <asp:TableHeaderRow>
                        <asp:TableHeaderCell BackColor="LightBlue" VerticalAlign="Middle">Uni Number</asp:TableHeaderCell>
                        <asp:TableHeaderCell BackColor="LightBlue" VerticalAlign="Middle"></asp:TableHeaderCell>
                        <asp:TableHeaderCell BackColor="LightBlue" VerticalAlign="Middle">Get Uni Number with Status</asp:TableHeaderCell>
                    
                    </asp:TableHeaderRow>--%>
                            <asp:TableRow CssClass="padding" HorizontalAlign="Left">
                                <asp:TableCell BorderWidth="1" CssClass="des" VerticalAlign="Top" Width="250px">
                                    <asp:TextBox ID="txtadd" TextMode="MultiLine" Height="250" Width="250" runat="server" />
                                    <%--<asp:Button ID="btndelete" Text="Delete" BackColor="LightPink" runat="server" OnClick="DeleteValues"
                                Visible="false" />--%>
                                </asp:TableCell><asp:TableCell BorderWidth="1" VerticalAlign="Top">
                                    <asp:Button ID="Button1" Text="Move" BackColor="LightPink" Width="50px" runat="server"
                                        OnClick="AddValues" />
                                </asp:TableCell><asp:TableCell BorderWidth="1" CssClass="padding">
                                    <asp:ListBox ID="lstAddColumns" runat="server" Width="250" Height="250"></asp:ListBox>
                                    <%--    <div style="width: 250px; height: 300px; padding: 2px; overflow: auto; border: 1px solid Black;">
                            <asp:CheckBoxList class="bor" ID="lstAddColumns" runat="server">
                            </asp:CheckBoxList>
                        </div>--%>
                                    <asp:DataGrid ID="dtgrid" runat="server" Visible="false">
                                    </asp:DataGrid>
                                    <br />
                                    <br />
                                    <%--<asp:Button ID="btn" runat="server" BackColor="LightPink" Text="Generate" OnClick="btn_Click" />&nbsp;&nbsp;--%>
                                    <%--  <asp:Button ID="btnReset" runat="server" BackColor="LightPink" Text="Reset" OnClick="btnReset_OnClick"
                                Visible="false" />--%>
                                </asp:TableCell></asp:TableRow></asp:Table></div></div></div></center></div><br />
    <center>
    <asp:Button ID="btnCreate" runat="server" BackColor="LightBlue" 
            Text="Create" Width="120px" Height="25px" onclick="btnCreate_Click" /></center>
           
    
    </form>
</body>
</html>
