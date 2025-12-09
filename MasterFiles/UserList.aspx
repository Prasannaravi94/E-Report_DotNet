<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="MasterFiles_UserList" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User List</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <ucl:Menu ID="menu1" runat="server" /> 
        <table id="Table1" height="50%" align="center" cellSpacing="0" cellPadding="8">
            <tr height="100%">
                <td valign="top">
                    <asp:TreeView ID="trvuser" runat="server" >
                    </asp:TreeView>    
                </td>
            </tr>
        </table> 
    </div>
    </form>
</body>
</html>
