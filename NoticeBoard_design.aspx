<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoticeBoard_design.aspx.cs"
    Inherits="NoticeBoard_design" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/style.css" />
    <link rel="stylesheet" href="assets/css/nice-select.css" />
    <link rel="stylesheet" href="assets/css/responsive.css" />
    <style type="text/css">
        .post-it {
            background-image: url('http://alternatewrites.com/wp-content/uploads/2012/06/post-it-note-with-a-pin.jpg');
            background-repeat: no-repeat;
            width: 500px;
            height: 300px;
        }

        .note {
            position: absolute;
            top: 90px;
            right: 30px;
            bottom: 30px;
            left: 60px;
            overflow: auto;
        }

        #pnlmar {
            background-repeat: no-repeat;
            height: 800px;
        }

        .word_wrap {
            word-wrap: break-word;
        }

        body {
            text-align: center;
            margin: 0 auto;
        }

        #box {
            position: absolute;
            width: 90%;
            height: 40%;
            left: 35%;
            /*top: 30%;*/
            border: #000;
        }

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
        hr {
            border-top:none;
        }
    </style>
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <table width="100%" id="tblclose" runat="server" visible="false">
            <tr>
                <td></td>
                <td align="right">
                    <table>
                        <tr>
                            <td style="padding-right: 50px">
                                <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                </asp:LinkButton>
                                <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <div class="container home-section-main-body position-relative clearfix" style="min-height:800px">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <asp:Panel ID="pnlnot" runat="server">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize; font-size: 14px;"
                                        ForeColor="#0077FF" Font-Bold="True"> </asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbldiv" runat="server" Text="User" Style="text-transform: capitalize; font-size: 14px;"
                                        ForeColor="#0077FF" Font-Bold="True"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnNext" runat="server" Width="150px" Height="30px" Text="Next to Home Page"
                                        OnClick="btnHome_Click"  CssClass="savebutton" />
                                    &nbsp;&nbsp;
                    <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" CssClass="savebutton"
                        Text="Direct to Home Page" OnClick="btnHomepage_Click"  />
                                    &nbsp;&nbsp;
                    <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" CssClass="resetbutton"
                        Text="Logout" OnClick="btnLogout_Click"  />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <center>
                            <asp:Label ID="lblHead" CssClass="h2"
                                runat="server">Notice Board</asp:Label>
                        </center>
                        <br />
                        <br />
                        <center>
                            <asp:Panel ID="pnldivi" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblDivision" runat="server" Text="Division Name " CssClass="label"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" AutoPostBack="true">
                                            </asp:DropDownList>
                                            &nbsp;
                            <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Width="30px" Height="25px"
                                Text="Go" OnClick="btnGo_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </center>
                        <br />
                        <br />
                        <br />
                      
                            <div id="box" >
                                <asp:Panel ID="pnlmar" runat="server" BackImageUrl="Images/Notice1.jpg" HorizontalAlign="Center" >
                                    <font style="font-family: Verdana; font-style: inherit; font-size: 13px; color: Red; vertical-align: middle">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <marquee onmouseover="this.setAttribute('scrollamount', 0, 0);" onmouseout="this.setAttribute('scrollamount', 6, 0);"
                                            scrolldelay="200" direction="up" style="width: 70%">
<asp:Label ID="lblCon1" runat="Server" Text='<%# Eval("NB_Cont1") %>' Font-Italic="true" ForeColor="Red" Width="220px"></asp:Label>
<br />    
<span style="text-align:center">***</span>
<br />
     <asp:Label ID="lblCon2" runat="Server" Text='<%# Eval("NB_Cont2") %>' width="220px"  Font-Italic="true" ForeColor="Red"></asp:Label>
     <br />
     <span style="text-align:center" >***</span>
     <br />
      <asp:Label ID="lblCon3" runat="Server" Text='<%# Eval("NB_Cont3") %>' Width="220px" Font-Italic="true" ForeColor="Red"></asp:Label>     
     
<asp:Label ID = "lblnorecords" runat="Server" Visible="false">No Data Found </asp:Label>

</marquee>
                                    </font>
                                </asp:Panel>
                            </div>
                      
                    </asp:Panel>
                </div>
            </div>
        </div>
        <br />
        <br />
    </form>
    <script src="assets/js/jQuery.min.js" type="text/javascript"></script>
    <script src="assets/js/popper.min.js" type="text/javascript"></script>
    <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/js/jquery.nice-select.min.js" type="text/javascript"></script>
    <script src="assets/js/main.js" type="text/javascript"></script>
</body>
</html>
