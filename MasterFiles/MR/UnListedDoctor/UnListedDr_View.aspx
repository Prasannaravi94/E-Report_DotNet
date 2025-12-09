<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnListedDr_View.aspx.cs" Inherits="MasterFiles_MR_UnListedDoctor_UnListedDr_View" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>
 <%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu1" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UnListed DR View</title>
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
    <div id="Divid" runat="server"></div>
        <div class="container home-section-main-body position-relative clearfix">
            <br />
            <div class="row justify-content-center ">
                <div class="col-lg-5">
                    <h2 class="text-center">UnListed DR View</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-table clearfix">
                            <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" Style="text-align: center; font-size: 18px;" CssClass="marRight">
                                <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                            </asp:Panel>
                        </div>
                    </div>
                    <br /><br />
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <table id="Table1" runat="server" width="90%">
                                <tr>
                                    <td align="right" width="30%">
                                        <%-- <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server"
                                            OnClick="btnBack_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblDRName" runat="server" Text="UnListed DR Name" CssClass="label"></asp:Label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'" Width="100%">
                            </asp:TextBox>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblAddress1" CssClass="label" runat="server" Text="Address "></asp:Label>
                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="input"
                                onfocus="this.style.backgroundColor='#E0EE9D'"
                                onblur="this.style.backgroundColor='White'" Width="100%"></asp:TextBox>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblCatg" runat="server" CssClass="label" Text="Category: "></asp:Label>
                            <asp:DropDownList ID="ddlCatg" runat="server" CssClass="nice-select"></asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblSpec" runat="server" CssClass="label" Text="Speciality: "></asp:Label>
                            <asp:DropDownList ID="ddlSpec" runat="server" CssClass="nice-select"></asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblQual" runat="server" CssClass="label" Text="Qualification: "></asp:Label>
                            <asp:DropDownList ID="ddlQual" runat="server" CssClass="nice-select"></asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblClass" runat="server" CssClass="label" Text="Class: "></asp:Label>
                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="nice-select" Width="225px"></asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblTerritory" runat="server" CssClass="label" Text="Territory: "></asp:Label>
                            <asp:DropDownList ID="ddlTerritory" runat="server" CssClass="nice-select"></asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <center>
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="savebutton" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="resetbutton" />
                            </center>
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
