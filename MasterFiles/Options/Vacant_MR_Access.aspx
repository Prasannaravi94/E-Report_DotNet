<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vacant_MR_Access.aspx.cs"
    Inherits="MasterFiles_Options_Vacant_MR_Access" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
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
                background-color: #A6A6D2;
            }

            table.gridtable td {
                border-width: 1px;
                padding: 5px;
                border-style: solid;
                border-color: #666666;
                background-color: #ffffff;
            }
    </style>
    <!-- Table goes in the document BODY -->
    <style type="text/css">
        .modal {
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

        .loading {
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
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
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
                                <asp:Label ID="lblmr" runat="server" CssClass="label">Vacant ID's<span style="color: Red;padding-left:5px;">*</span></asp:Label><br />
                                <asp:ListBox ID="lstVacant" Font-Size="Small" Font-Names="Verdana" CssClass="custom-select2 nice-select" runat="server"
                                    Style="border: solid 1px black; border-collapse: collapse;" AutoPostBack="true" OnSelectedIndexChanged="lstVacant_SelectedIndexChanged"></asp:ListBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblPassword" runat="server" CssClass="label">Password (Type your own Password)</asp:Label>
                                <asp:TextBox ID="txtPassword" runat="server" MaxLength="15" TextMode="Password" CssClass="input" Width="100%"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblLoginto" runat="server" Text="Login To:" CssClass="label" Width="100%"></asp:Label>
                                <asp:Label ID="lblLogin" ForeColor="Blue" runat="server" CssClass="label" Width="100%"></asp:Label>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnLogin" runat="server" CssClass="savebutton" Text="Login" OnClick="btnLogin_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <center>
                <asp:Label ID="lblrecord" ForeColor="Black" CssClass="label" Width="100%" BackColor="AliceBlue" Height="20px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" runat="server" Text="No Records Found" Visible="false"></asp:Label>
            </center>

            <%--  <center>
        <asp:Label ID="lblnorecord" Width="90%" runat="server" Visible="false" ForeColor="Black" Text="No Records Found" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></asp:Label>
        </center>--%>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
