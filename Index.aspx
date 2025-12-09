<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <title>Login</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link rel="icon" type="image/png" href="https://colorlib.com/etc/lf/Login_v19/images/icons/favicon.ico">
    <link rel="stylesheet" type="text/css" href="Login/bootstrap.css">
    <link rel="stylesheet" type="text/css" href="Login/font-awesome.css">
    <link rel="stylesheet" type="text/css" href="Login/icon-font.css">
    <link rel="stylesheet" type="text/css" href="Login/animate.css">
    <link rel="stylesheet" type="text/css" href="Login/hamburgers.css">
    <link rel="stylesheet" type="text/css" href="Login/animsition.css">
    <link rel="stylesheet" type="text/css" href="Login/select2.css">
    <link rel="stylesheet" type="text/css" href="Login/daterangepicker.css">
    <link rel="stylesheet" type="text/css" href="Login/util.css">
    <link rel="stylesheet" type="text/css" href="Login/main.css">


    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
    <script type="text/javascript">

        function validate() {

            var txtUserName = document.getElementById('<%=txtUserName.ClientID %>').value;
            if (txtUserName == "") {

                document.getElementById('<%=txtUserName.ClientID %>').focus();
                return false;
            }
            var txtPassWord = document.getElementById('<%=txtPassWord.ClientID %>').value;
            if (txtPassWord == "") {

                document.getElementById('<%=txtPassWord.ClientID %>').focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#close').click(function () {
                $('#float_bottom_left').remove();
            });
        });
    </script>
    <script type="text/javascript">

        function ShowCurrentTime() {

            var dt = new Date();

            document.getElementById("lblCurTime").innerHTML = dt.toLocaleTimeString();

            window.setTimeout("ShowCurrentTime()", 1000); // Here 1000(milliseconds) means one 1 Sec  

        }

    </script>
</head>
<body>
    <div class="limiter">
        <div class="container-login100">
            <div class="wrap-login100 p-l-20 p-r-20 p-t-65 p-b-50">
                <form class="login100-form" id="dd" runat="server">
                    <span class="login100-form-title p-b-33"><b>TORS</b>
                    </span>
                    <div class="wrap-input100">
                        <input type="text" placeholder="Username" class="input100" id="txtUserName" runat="server" />
                        <span class="focus-input100-1"></span>
                        <span class="focus-input100-2"></span>
                    </div>
                    <div class="wrap-input100 rs1 " data-validate="Password is required">
                        <%--<input class="input100" type="password" name="pass" placeholder="Password">--%>
                        <input class="input100" type="password" placeholder="Password" runat="server" id="txtPassWord" />
                        <span class="focus-input100-1"></span>
                        <span class="focus-input100-2"></span>
                    </div>
                    <div style="padding-top: 10px; text-align: center;">
                        <asp:Label ID="msg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </div>
                    <div class="container-login100-form-btn m-t-20">
                        <button type="submit" name="btnLogin" value="Sign In" onclick="return validate();" id="btnLogin" class="login100-form-btn" runat="server" onserverclick="btnLogin_Click">SIGN IN</button>
                    </div>


                </form>
            </div>
        </div>
    </div>

    <script type="text/javascript" async="" src="Login/analytics.js"></script>
    <script src="Login/jquery-3.js"></script>

    <script src="Login/animsition.js"></script>

    <script src="Login/popper.js"></script>
    <script src="Login/bootstrap.js"></script>

    <script src="Login/select2.js"></script>

    <script src="Login/moment.js"></script>
    <script src="Login/daterangepicker.js"></script>

    <script src="Login/countdowntime.js"></script>

    <script src="Login/main.js"></script>

    <script async="" src="Login/js"></script>


</body>

</html>
