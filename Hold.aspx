<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hold.aspx.cs" Inherits="Hold" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <div align="right">
            <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" Text="Logout"
                OnClick="btnLogout_Click" BackColor="Red" ForeColor="White" />
        </div>
        <br />
        <br />
        <center>
             <img src="Images/hold_id.png" alt="" />
        </center>
        <center>
        </center>
        <center>
            <h2>
                <span style="color: Blue">Your Id is on Hold... Kindly Contact Your Line Manager...</span></h2>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblimg" runat="server"><img src="Images/hand.gif" alt=""  /></asp:Label>
                    </td>
                    <td >
                        <asp:Label ID="lblblo" runat="server" Text="Hold Reason:" Font-Bold="true" Font-Size="18px"></asp:Label>
                        <asp:Label ID="lblreason" runat="Server" Style="margin-top: 10px;" Width="100%" ForeColor="Blue"
                            Font-Bold="true" Font-Size="16px" Font-Names="Tahoma" Text='<%# Eval("sf_blkreason") %>' />
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <br />
        <br />
        <%--  <asp:Label ID="lblreason" runat="Server"  style="margin-top:10px;" Width="100%" ForeColor="Blue" Font-Bold="true" Font-Size="16px" Font-Names="Tahoma"  Text='<%# Eval("sf_blkreason") %>' />--%>
    </div>
    </form>
</body>
</html>
