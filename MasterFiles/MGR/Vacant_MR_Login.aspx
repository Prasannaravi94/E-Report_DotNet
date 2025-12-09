<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vacant_MR_Login.aspx.cs" Inherits="MasterFiles_MGR_Vacant_MR_Login" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login To Vacant ID's</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <style type="text/css">
        table.gridtable {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }

            table.gridtable th {
                border-width: 1px;
                padding: 5px;
                border-style: solid;
                border-color: 1px #666666;
                background-color: #99B7B7;
            }

            table.gridtable td {
                border-width: 1px;
                padding: 5px;
                border-style: solid;
                border-color: #666666;
                background-color: #ffffff;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblmr" runat="server">Vacant ID's</asp:Label><br />
                                <asp:ListBox ID="lstVacant" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstVacant_SelectedIndexChanged"></asp:ListBox>
                            </div>
                            <asp:Panel ID="pnlSf" runat="server" Visible="false">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblPassword" runat="server" CssClass="label">Password (Type your own Password)</asp:Label>
                                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="15" TextMode="Password" CssClass="input" Width="100%"></asp:TextBox>
                                </div>
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblLoginto" runat="server" Text="Login To:" CssClass="label"></asp:Label>
                                    <asp:Label ID="lblLogin" runat="server"></asp:Label>
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <br />
                                    <asp:Button ID="btnLogin" runat="server" CssClass="savebutton" Text="Login" OnClick="btnLogin_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
