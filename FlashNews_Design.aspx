<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlashNews_Design.aspx.cs"
    Inherits="FlashNews_Design" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/style.css" />

    <style type="text/css">
        .roundCorner {
            border-radius: 25px;
            background-color: #4F81BD;
            text-align: center;
            font-family: arial, helvetica, sans-serif;
            padding: 5px 10px 10px 10px;
            font-weight: bold;
            width: 150px;
            height: 30px;
        }

        .leaveformtable {
            padding: 0px;
            width: 100%;
        }

        .leaveformth {
            color: White;
            background-color: #179BED;
            padding: 10px;
            border-radius: 10px;
            border-bottom-right-radius: 10px;
            border-bottom-left-radius: 10px;
            border: 0px solid black;
            text-align: center;
            border-bottom-left-radius: 0px;
            border-bottom-right-radius: 0px;
        }

        .leaveformtable .dheading span {
            width: 100%;
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize; font-size: 16px;"
                                       Font-Bold="True"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lbldiv" runat="server" Text="User" Style="text-transform: capitalize; font-size: 16px;"
                                       Font-Bold="True"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <%--  <hr />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2" style="padding-top: 15px;">
                                    <asp:Button ID="btnNext" runat="server" Width="150px" Height="30px" Text="Next to Home Page"
                                        OnClick="btnHome_Click" CssClass="savebutton" />
                                    &nbsp;&nbsp;
                    <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" Text="Goto Home Page" CssClass="savebutton"
                        OnClick="btnHomepage_Click" />
                                    &nbsp;&nbsp;
                    <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" Text="Logout" CssClass="resetbutton"
                        OnClick="btnLogout_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <%--  <hr />--%>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <div class="row justify-content-center">
                            <div class="col-lg-8">
                                <div class="leaveformtable">
                                    <center>
                                        <div class="dheading">
                                            <asp:Label ID="lblHome" CssClass="leaveformth"
                                                runat="server">Flash News</asp:Label>
                                        </div>
                                    </center>
                                    <br />
                                    <br />
                                    <center>
                                        <asp:Panel ID="Panel2" runat="server" ForeColor="Red" Font-Size="Medium"
                                            Width="95%" Height="80px">
                                            <marquee onmouseover="this.setAttribute('scrollamount', 0, 0);" onmouseout="this.setAttribute('scrollamount', 6, 0);"><asp:Label ID="lblFlash" runat="Server" Text='<%# Eval("FN_Cont1") %>' /></marquee>
                                        </asp:Panel>
                                    </center>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
