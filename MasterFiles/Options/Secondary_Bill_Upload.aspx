<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Secondary_Bill_Upload.aspx.cs" Inherits="MasterFiles_Options_Secondary_Bill_Upload" %>


<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Bill  Upload</title>
     <style type="text/css">
        td.stylespc
        {
            padding-bottom: 5px;
            padding-right: 5px;
        }
         .Grid
        {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
        }
        .Grid td
        {
            padding: 2px;
            border: solid 1px #c1c1c1;
        }
        .Grid th
        {
            padding: 4px 2px;
            color: #fff;
            background: #363670 url(Images/grid-header.png) repeat-x top;
            border-left: solid 1px #525252;
            font-size: 0.9em;
        }
        .Grid .alt
        {
            background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
        }
        .Grid .pgr
        {
            background: #363670 url(Images/grid-pgr.png) repeat-x top;
        }
        .Grid .pgr table
        {
            margin: 3px 0;
        }
        .Grid .pgr td
        {
            border-width: 0;
            padding: 0 6px;
            border-left: solid 1px #666;
            font-weight: bold;
            color: #fff;
            line-height: 12px;
        }
        .Grid .pgr a
        {
            color: Gray;
            text-decoration: none;
        }
        .Grid .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
          </style>
</head>
<body>
    <form id="form1" runat="server">
         <ucl:Menu ID="menu1" runat="server" />
         <br />
    <div>
    <center>
            <asp:Panel ID="pnlSalesForce" Width="90%" runat="server">
                <table cellpadding="5px" cellspacing="5px" style="border: 1px solid Black; background-color: White; height:350px" width="60%"> 
                <tr>
                <td>
                <br />
                </td>
                </tr>
                    <tr>
                  
                        <td align="center" class="stylespc">
                            <table align="center" width="300px">
                               
                                <tr>
                                    <td align="center" class="stylespc">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="stylespc" align="center">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblExcel" runat="server" SkinID="lblMand" Font-Size="Small" Font-Names="Verdana">Excel file</asp:Label>
                                        <asp:FileUpload ID="FlUploadcsv" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td style="padding-left: 80px">
                            <asp:CheckBox ID="chkDeact" runat="server" ForeColor="Red" Font-Size="12px" Font-Names="Verdana"
                                Text="Deactivate Existing Salesforce List ( if Yes then Check this Option )" OnCheckedChanged="chkDeact_CheckedChanged" />
                        </td>--%>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="Button1" runat="server" Width="70px" Height="25px" BackColor="BurlyWood"
                                            Font-Size="Medium" Text="Upload" OnClick="btnUpload_Click" />
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
                                            Text="1) Sheet Name Must be 'UPL_Sales_Bill'"></asp:Label>
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
                                        &nbsp;
                                        <asp:LinkButton ID="lnkDownload" runat="server" Font-Size="12px" Font-Names="Verdana"
                                            Text="Download Here" OnClick="lnkDownload_Click" > 
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </center>
          <br />
    <center>
    </center>
    <br />
    </div>
    </form>
</body>
</html>
