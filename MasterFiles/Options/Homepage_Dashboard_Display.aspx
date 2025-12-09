<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Homepage_Dashboard_Display.aspx.cs" Inherits="MasterFiles_Options_SetupScreen" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Homepage Dashboard Display</title>
    <style type="text/css">
    </style>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <style type="text/css">
        .chkNew label
        {
            padding-left: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <center>
                <table cellpadding="4" cellspacing="4" align="center" style="border: solid 1px #347C17; border-collapse: collapse">
                    <tr>
                        <td align="left">
                            <asp:CheckBoxList ID="chkNew" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" CssClass="chkNew"
                                BackColor="#FEFCFF" ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="600px"
                                Font-Names="Calibri">
                                <asp:ListItem Value="0" Text="DOB / DOW (Listed Doctor)"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="savebutton" Width="80px"
                    Height="25px" OnClick="btnSave_Click" />
            </center>
        </div>
    </form>
</body>
</html>
