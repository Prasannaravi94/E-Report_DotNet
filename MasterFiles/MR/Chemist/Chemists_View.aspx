<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chemists_View.aspx.cs" Inherits="MasterFiles_MR_Chemist_Chemists_View" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chemists View</title>
     <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
       <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
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
           /*.marRight
        {
            margin-right:35px;
        }*/
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="Divid" runat="server">
        </div>
       <%-- <ucl:Menu ID="menu1" runat="server" /> --%>

        <div class="container home-section-main-body position-relative clearfix">
            <br />
            <div class="row justify-content-center ">
                <div class="col-lg-5">
                    <h2 class="text-center">Chemists View</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-table clearfix">
                            <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" style="text-align: center;font-size: 16px;" CssClass="marRight">
                                <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                            </asp:Panel>
                            </div>
                        </div>
                    <br /><br />
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <%--   <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                            <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server"
                                                OnClick="btnBack_Click" />
                            </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblName" runat="server" CssClass="label" Text="Chemists Name"></asp:Label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="input" Width="100%" Enabled="False" Text='<%#Eval("Chemists_Name")%>'></asp:TextBox>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblAddress" runat="server" CssClass="label" Text="Chemists Address"></asp:Label>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="input" Width="100%" Enabled="False" Text='<%#Eval("Chemists_Address1")%>'></asp:TextBox>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblContact" runat="server" CssClass="label" Text="Contact"></asp:Label>
                            <asp:TextBox ID="txtContact" runat="server" CssClass="input" Width="100%" Enabled="false" Text='<%#Eval("Chemists_Contact")%>'></asp:TextBox>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblPhone" runat="server" CssClass="label" Text="Chemists Phone"></asp:Label>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="input" Width="100%" Enabled="false" Text='<%#Eval("Chemists_Phone")%>'></asp:TextBox>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblTerritory" runat="server" CssClass="label" Text="Territory"></asp:Label>
                            <asp:TextBox ID="txtTerritory" runat="server" CssClass="input" Width="100%" Enabled="false" Text='<%#Eval("Territory_Name")%>'></asp:TextBox>
                        </div>
                        </div>
                    </div>
                <asp:Button ID="Button1" runat="server" CssClass="backbutton" Text="Back" OnClick="buttonBack_Click" />
                </div>
            </div>
        <br /><br />
          <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>    
    </div>
    </form>
</body>
</html>
