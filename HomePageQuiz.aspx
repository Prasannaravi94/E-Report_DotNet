<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePageQuiz.aspx.cs" Inherits="HomePageQuiz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
      
                .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
     

    </style>
      <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:Panel ID="pnlnot" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
            <tr>
                <td>
                    <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                        font-size: 14px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lbldiv" runat="server" Text="User" Style="text-transform: capitalize;
                        font-size: 14px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                </td>
            </tr>
             <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            </table>
            </asp:Panel>
    <div>
   <%-- <center>
    <img src="Images/COVER PAGE.jpg" width="500px" height="500px" alt="" />
    </center>
    <br />--%>
    <br />
    <center>
    <img src="Images/test1.gif" alt="" />
   

    </center>
    <br />
    <center>
    <img src="Images/good_luck.gif" alt="" />
    </center>
    <br />
    <center>
    <asp:ImageButton ID="btnimg" runat="server" Width="120px" 
            ImageUrl="~/Images/click.gif" onclick="btnimg_Click" />
    </center>
     <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="Images/loader.gif" alt="" />

    </div>
    </form>
</body>
</html>
