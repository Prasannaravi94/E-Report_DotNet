<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Super_Stockist_Map.aspx.cs"
    Inherits="MasterFiles_Super_Stockist_Map" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Super Stockist Create & Map</title>
  <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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

        .height {
            height: 20px;
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
    <style type="text/css">
        .web_dialog_overlay {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            background: #000000;
            opacity: .15;
            filter: alpha(opacity=15);
            -moz-opacity: .15;
            z-index: 101;
            display: none;
        }

        .web_dialog {
            display: none;
            position: fixed;
            width: 380px;
            min-height: 180px;
            max-height: auto;
            top: 50%;
            left: 50%;
            margin-left: -190px;
            margin-top: -100px;
            background-color: #ffffff;
            border: 2px solid #336699;
            padding: 0px;
            z-index: 102;
            font-family: Verdana;
            font-size: 10pt;
        }

        .web_dialog_title {
            border-bottom: solid 2px Teal;
            background-color: Teal;
            padding: 4px;
            color: White;
            font-weight: bold;
        }

            .web_dialog_title a {
                color: White;
                text-decoration: none;
            }

        .align_right {
            text-align: right;
        }

        .Formatrbtn label {
            margin-right: 30px;
        }


        /* hover style just for information */
        label:hover:before {
            border: 1px solid #4778d9 !important;
        }


        .btnReAct {
            display: inline-block;
            padding: 3px 9px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            margin-top: 25px;
        }

        .btnReActivation {
            color: #fff;
            background-color: #158263;
            border-color: #158263;
        }

            .btnReActivation:hover {
                color: #fff;
                background-color: #2b9a7b;
                border-color: #2b9a7b;
            }

            .btnReActivation:focus, .btnReActivation.focus {
                color: #fff;
                background-color: #2b9a7b;
                border-color: #2b9a7b;
            }

            .btnReActivation:active, .btnReActivation.active {
                color: #fff;
                background-color: #158263;
                border-color: #158263;
                background-image: none;
            }


        #btnClose_Plus:focus {
            outline-offset: -2px;
        }

        #btnClose_Plus:hover, #btnClose_Plus:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btnClose_Plus:hover, #btnClose_Plus:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btnClose_Plus:active, #btnClose_Plus:hover {
            outline: 0px none currentColor;
        }
    </style>
    <link href="../JScript/BootStrap/dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript">


        function HQ_Validation() {

            var ShortName = $("#txt_Sname").val();
            var State = $("#ddlState").val();

            if (ShortName == "") {
                var txt = "Please Enter Name";
                createCustomAlert(txt);
                return false;
            }
            else if (State == "---Select---") {
                var txt = "Please Select State";
                createCustomAlert(txt);
                return false;
                //createCustomAlert(txt);
            }
            else {
                return true;
            }
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            function blinker() {
                $('.blink_me').fadeOut(500);
                $('.blink_me').fadeIn(500);
            }

            setInterval(blinker, 1000);
        });
    </script>
     <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnGo').click(function () {
                 var ss = $('#<%=ddlst.ClientID%> :selected').text();
                if (ss == "---Select---") { createCustomAlert("Select Super Stockist."); $('#ddlst').focus(); return false; }
            });
            $('#btnSubmit').click(function () {
                if (!CheckBoxSelectionValidation()) {
                    return false;
                }
            });
        });
    </script>
     <script type="text/javascript" language="javascript">
        function CheckBoxSelectionValidation() {
            var count = 0;
            var objgridview = document.getElementById('<%= ChkStock.ClientID %>');

            for (var i = 0; i < objgridview.getElementsByTagName("input").length; i++) {

                var chknode = objgridview.getElementsByTagName("input")[i];

                if (chknode != null && chknode.type == "checkbox" && chknode.checked) {
                    count = count + 1;
                }
            }

            if (count == 0) {
                var txt = "Please Select Stockist";
                createCustomAlert(txt);

                return false;
            }
            else {
                return true;
            }
        }

    </script>
    <style type="text/css">
        #tblStockistDetails {
            margin-left: 330px;
        }

        #tblSalesforceDtls {
            margin-left: 330px;
        }

        .div_fixed {
            position: fixed;
            top: 400px;
            right: 5px;
        }

        #table-wrapper {
            position: relative;
        }

        #table-scroll {
            height: 850px;
            overflow: auto;
            margin-top: 20px;
        }

        #table-wrapper table {
            width: 90%;
        }

            #table-wrapper table * {
                background: white;
                color: black;
            }

            #table-wrapper table thead th .text {
                text-align: center;
                font-weight: bold;
                font-size: 14px;
                color: Blue;
                position: absolute;
                top: -20px;
                z-index: 2;
                height: 30px;
                width: 80%;
                border: 1px solid red;
            }
    </style>
    <script type="text/javascript">


        $(document).ready(function () {

            $("#btnReActivate_Plus").click(function (e) {
                ShowDialog_Plus(false);
                e.preventDefault();
            });

            $("#btnClose_Plus").click(function (e) {
                HideDialog_Plus();
                e.preventDefault();
            });
        });

        function ShowDialog_Plus(modal) {
            $("#overlay_Plus").show();
            $("#dialog_Plus").fadeIn(300);

            if (modal) {
                $("#overlay_Plus").unbind("click");
            }
            else {
                $("#overlay_Plus").click(function (e) {
                    HideDialog_Plus();
                });
            }
        }

        function HideDialog_Plus() {
            $("#overlay_Plus").hide();
            $("#dialog_Plus").fadeOut(300);
        }


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
                        <h2 class="text-center" style="border-style: none;">Super Stockiest </h2>
                        <div class="designation-area clearfix">
                            <center>
                            <div class="single-des clearfix">

                            <div class="single-des clearfix">
                                <asp:Label ID="lblSt" runat="server" CssClass="label" Text="Super Stockiest" ForeColor="Black"></asp:Label>
                                <div id="dvState" class="row-fluid">
                                    <asp:DropDownList ID="ddlst" CssClass="nice-select" runat="server" Width="100%" >
                                    </asp:DropDownList>
                                </div>
                         <a href="#" id="btnReActivate_Plus" style="color: Blue; font-weight: bold; font-family: @NSimSun; font-size: 14px"
                    shape="circle">Add Super Stockist</a>
                                  </div>
                                </div>
                               
                                </center>
                            </div>
                        
                        </div>
                    </div>
                </div>
            
            <div>
                <br />
                <div id="output_Plus">
                </div>
                <div id="overlay_Plus" class="web_dialog_overlay">
                </div>
                <div id="dialog_Plus" class="web_dialog">
                    <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                        <tr>
                            <td class="web_dialog_title">Super Stockist Creation
                            </td>
                            <td class="web_dialog_title align_right">
                                <a href="#" id="btnClose_Plus">Close</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <center>
                                    <table border="0" cellpadding="3" cellspacing="3" id="tblDivisionDtls" align="center">
                                        <tr>
                                            <td align="left" class="stylespc">
                                                <asp:Label ID="lblShortName" runat="server" SkinID="lblMand" Height="19px" Width="150px"><span style="color:Red">*</span>Super Stockist Name</asp:Label>
                                            </td>
                                            <td align="left" class="stylespc">
                                                <asp:TextBox ID="txt_Sname" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                    onblur="this.style.backgroundColor='White'" TabIndex="1" runat="server" MaxLength="100"
                                                    onkeypress="CharactersOnly(event);">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="stylespc">
                                                <asp:Label ID="lblState" runat="server" SkinID="lblMand" Height="19px" Width="120px"><span style="color:Red">*</span>State</asp:Label>
                                            </td>
                                            <td align="left" class="stylespc">
                                                <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="stylespc">
                                                <asp:Label ID="LblEmail" runat="server" SkinID="lblMand" Height="19px" Width="150px"><span style="color:Red">*</span>Email</asp:Label>
                                            </td>
                                            <td align="left" class="stylespc">
                                                <asp:TextBox ID="txtEmail" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                    onblur="this.style.backgroundColor='White'" runat="server" MaxLength="100">
                                                    
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnHq" runat="server" Text="Save" CssClass="btnReAct btnReActivation"
                                    OnClick="btnHq_Click" OnClientClick="if(!HQ_Validation()) return false;" />
                                <%--<asp:Button ID="btnActive_Plus" runat="server" Text="Activate" OnClick="btnActive_Plus_Click"
                                            CssClass="btn btnReActivation" />--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <center>
                   <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Width="50px" Text="Go" OnClick="btnGo_Click" />
                        
                        </div>
            </center>
            <br />
            <asp:Panel ID="pnlStk" runat="server" Visible="false">
                <center>
                    <asp:Label ID="lblS" runat="server" Font-Size="15px" Font-Bold="true" ForeColor="Red"
                        Font-Italic="true">Stockist - Super Stockist Map</asp:Label>
                    <div style="height: 400px; width: 1000px; border: solid 2px orange; overflow: scroll; overflow-x: scroll; overflow-y: scroll; background: lightpink">
                        <asp:CheckBoxList ID="ChkStock" runat="server" DataTextField="Stockist_Name" CssClass="chkboxLocation"
                            DataValueField="stockist_code" RepeatDirection="Vertical" RepeatColumns="3" Width="950px"
                            TabIndex="7" Style="font-size: x-small; color: black; font-family: Verdana;">
                        </asp:CheckBoxList>
                    </div>
                    <%-- <div id="table-wrapper">
                <div id="table-scroll">
                    <table>
                        <thead>
                            <tr>
                                <th>
                                    <span class="text">Stockist Map</span>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="ChkStock" runat="server" DataTextField="Stockist_Name" CssClass="chkboxLocation"
                                        DataValueField="stockist_code" RepeatDirection="Vertical" RepeatColumns="3" 
                                        TabIndex="7" Style="font-size: x-small; color: black; font-family: Verdana;">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>--%>
                    <br />
                </center>
            </asp:Panel>
            <br />
            <center>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="75px" Height="25px" Visible="false"  CssClass="BUTTON"
                     OnClick="btnSubmit_Click" />
            </center>
        </div>
    </form>
</body>
</html>
