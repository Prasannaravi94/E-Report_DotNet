<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard_Payslip_View.aspx.cs" Inherits="MasterFiles_Dashboard_Payslip_View"
    EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"
        integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css"
        integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/css/bootstrap-select.min.css" />
    <link href="../css/pace/themes/blue/pace-theme-flash.css" rel="stylesheet" />
    <style>
        #ifmReviewRep
        {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
        }
    </style>
    <style>
        .blockUI.blockMsg.blockElement
        {
            border: none !important;
        }

        #loader
        {
            position: fixed;
            top: 50%;
            right: 50%;
            margin: auto;
        }

            #loader .dot
            {
                bottom: 0;
                height: 100%;
                left: 0;
                margin: auto;
                position: absolute;
                right: 0;
                top: 0;
                width: 87.5px;
            }

                #loader .dot::before
                {
                    border-radius: 100%;
                    content: "";
                    height: 87.5px;
                    left: 0;
                    position: absolute;
                    right: 0;
                    top: 0;
                    transform: scale(0);
                    width: 87.5px;
                }

                #loader .dot:nth-child(7n+1)
                {
                    transform: rotate(45deg);
                }

                    #loader .dot:nth-child(7n+1)::before
                    {
                        animation: 0.8s linear 0.1s normal none infinite running load;
                        background: #00ff80 none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+2)
                {
                    transform: rotate(90deg);
                }

                    #loader .dot:nth-child(7n+2)::before
                    {
                        animation: 0.8s linear 0.2s normal none infinite running load;
                        background: #00ffea none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+3)
                {
                    transform: rotate(135deg);
                }

                    #loader .dot:nth-child(7n+3)::before
                    {
                        animation: 0.8s linear 0.3s normal none infinite running load;
                        background: #00aaff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+4)
                {
                    transform: rotate(180deg);
                }

                    #loader .dot:nth-child(7n+4)::before
                    {
                        animation: 0.8s linear 0.4s normal none infinite running load;
                        background: #0040ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+5)
                {
                    transform: rotate(225deg);
                }

                    #loader .dot:nth-child(7n+5)::before
                    {
                        animation: 0.8s linear 0.5s normal none infinite running load;
                        background: #2a00ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+6)
                {
                    transform: rotate(270deg);
                }

                    #loader .dot:nth-child(7n+6)::before
                    {
                        animation: 0.8s linear 0.6s normal none infinite running load;
                        background: #9500ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+7)
                {
                    transform: rotate(315deg);
                }

                    #loader .dot:nth-child(7n+7)::before
                    {
                        animation: 0.8s linear 0.7s normal none infinite running load;
                        background: magenta none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+8)
                {
                    transform: rotate(360deg);
                }

                    #loader .dot:nth-child(7n+8)::before
                    {
                        animation: 0.8s linear 0.8s normal none infinite running load;
                        background: #ff0095 none repeat scroll 0 0;
                    }

            #loader .lading
            {
                background-image: url("../images/loading.gif");
                background-position: 50% 50%;
                background-repeat: no-repeat;
                bottom: -40px;
                height: 20px;
                left: 0;
                position: absolute;
                right: 0;
                width: 180px;
            }

        @keyframes load
        {
            100%
            {
                opacity: 0;
                transform: scale(1);
            }
        }

        @keyframes load
        {
            100%
            {
                opacity: 0;
                transform: scale(1);
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upAdmDashboard" runat="server">
            <ContentTemplate>
                <nav class="navbar navbar-default">
                    <div class="container" style="margin-top: 12px;">
                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group form-group-sm">
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="selectpicker" data-live-search="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group form-group-sm">
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group form-group-sm">
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 text-center">
                                    <div class="row">
                                        <div class="form-group form-group-sm">
                                            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" CssClass="btn btn-sm btn-primary" Text="Go"
                                                OnClientClick="showLoader('Search1')" />
                                            <asp:Button ID="btnback" runat="server" OnClick="btnback_Click" CssClass="btn btn-sm btn-primary" Text="Back"
                                                OnClientClick="showLoader('Search1')" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </nav>
                <div id="main" class="container">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-primary" style="border-color:#FFF !important;">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div id="navbar">
                                                <ul class="nav navbar-nav navbar-right">
                                                    <li>
                                                        <li></li>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="container-fluid">
                                            <div class="table-responsive">
                                                <iframe id="ifmReviewRep" runat="server" style='border: 0;'></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ddlFieldForce" />
                <asp:PostBackTrigger ControlID="ddlMonth" />
                <asp:PostBackTrigger ControlID="ddlYear" />
                <asp:PostBackTrigger ControlID="btnback" />
                <asp:PostBackTrigger ControlID="btnGo" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="
        crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"
        integrity="sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"
        integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa"
        crossorigin="anonymous"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/i18n/defaults-*.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script type="text/javascript" src="http://malsup.github.io/jquery.blockUI.js"></script>
    <script src="../css/pace/pace.js"></script>
    <script>
        $(document).ready(function () {
            $('.selectpicker').selectpicker();
        });

        // Tooltips Initialization
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
    </script>
    <script>
        function showLoader(loaderType) {
            if (loaderType == "Search1") {
                //$('#loader').show();
                document.getElementById("loader").style.display = '';
                $('html').block({
                    message: $('#loader'),
                    centerX: true,
                    centerY: true
                });
            }
        }

        $(document).ready(function () {
            $("#ifmReviewRep").on('load', function () {
                var h = $(this).contents().find("body").height();
                $(this).height(h);
            });
        });
    </script>
</body>
<div class="container">
    <div class="row">
        <div id="loader" style="display: none;">
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="lading">
            </div>
        </div>
    </div>
</div>
</html>
