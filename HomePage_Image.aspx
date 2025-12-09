<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage_Image.aspx.cs" Inherits="HomePage_Image" %>

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
    </style>
    <%--   <link type="text/css" rel="stylesheet" href="../css/Grid.css" />--%>
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
                                <td align="center">
                                    <asp:Label ID="LblUser" runat="server" Text="User" CssClass="reportheader"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                             
                                    <td align="right" style="padding-top:10px;padding-bottom:10px;">
                                    <asp:Button ID="Button1" runat="server" Width="150px" Height="30px" Text="Next to Home Page"
                                        OnClick="btnHome_Click" CssClass="savebutton" Visible="false" />
                                    &nbsp;&nbsp;
           
                                
                    <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" CssClass="savebutton"
                        Text="Direct to Home Page" OnClick="btnHomepage_Click" Visible="false" />
                                    &nbsp;&nbsp;
                    <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" CssClass="resetbutton"
                        Text="Logout" OnClick="btnLogout_Click" />
                                </td>
                            </tr>
                        </table>
                        <center>
                            <table>
                                <tr>
                                    <td>
                                        <asp:DataList ID="DataList1" runat="server" HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblsub" runat="server" Text='<%# Eval("subject") %>' Font-Bold="true" CssClass="reportheader"></asp:Label>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Image ID="imgHome" ImageUrl='<%# Eval("FilePath") %>' ImageAlign="Top" runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                        </center>
                         <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
                 <tr>
            <td align="right" style="padding-top:10px;padding-bottom:10px;">
                                    <asp:Button ID="btnNext" runat="server" Width="150px" Height="30px" Text="Next to Home Page"
                                        OnClick="btnHome_Click" CssClass="savebutton" />
                                    &nbsp;&nbsp;
                </td></tr>
                                </table>
                    </div>
                </div>
            </div>
            <br />
            <br />
           
        </div>
    </form>
</body>
</html>
