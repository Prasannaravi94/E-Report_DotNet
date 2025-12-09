<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MGR_Home.aspx.cs" Inherits="MasterFiles_MGR_MGR_Home" %>

<%@ Register Src="~/UserControl/MGR_TP_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Manager</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Welcome Corporate – HQ</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/png" href="assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="assets/css/nice-select.css">
    <link rel="stylesheet" href="assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/css/style.css">
    <link rel="stylesheet" href="assets/css/responsive.css">
    <link rel="stylesheet" href="assets/css/mr_dashboard_style.css" />
    <style type="text/css" xml:space="preserve" class="blink">
        div.blink {
            text-decoration: blink;
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
    <script runat="server">

    </script>
    <style type="text/css">
        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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

        .TextFont {
            text-align: center;
            margin-top: 6px;
            height: 50px;
            font-size: 14px;
            font-weight: 400;
        }

        .buttonlabel {
            background-color: #f1f5f8;
            border-radius: 10px;
            height: 30px;
            padding: 5px 10px 10px 10px;
            font-size: 14px;
            font-weight: 400;
            display: inline-block;
            margin-left: 900px;
            margin-top: -30px;
            color: #0056b3;
            z-index: 2;
            position: relative;
        }

        .linkviewlabel {
            background-color: #f1f5f8;
            border-radius: 10px;
            height: 30px;
            padding: 5px 30px 10px 30px;
            font-size: 14px;
            font-weight: 400;
            display: inline-block;
            color: #0056b3;
        }

        .chart-section-main-body {
            margin-top: 0px !important;
        }

        .single-top-chat-min-height {
            min-height: 377px !important;
        }

        .calendarshadow {
            box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            -webkit-box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            -moz-box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            -o-box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            border-radius: 8px;
            background-color: #ffffff;
            margin-bottom: 80px;
            margin-top: 15px;
            margin-left: 50px;
        }

        .blink_me {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 1s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 1s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 1s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }



        .exam #lblFN {
            padding: 2em 2em 0 2em;
        }

        #lblFN {
            width: 130px;
            height: 70px;
            background: Red;
            -moz-border-radius: 30px / 30px;
            -webkit-border-radius: 30px / 30px;
            border-radius: 30px / 30px;
            padding: 3px;
        }

        .roundCorner {
            border-radius: 25px;
            background-color: #4F81BD;
            text-align: center;
            font-family: arial, helvetica, sans-serif;
            padding: 5px 10px 10px 10px;
            font-weight: bold;
            width: 100px;
            height: 30px;
        }

        .modalBackgroundNew {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopupNew {
            background-color: #FFFFFF;
            width: 800px;
            border: 3px solid #0DA9D0;
            padding: 0;
        }

            .modalPopupNew .header {
                background-color: #2FBDF1;
                height: 20px;
                color: White;
                line-height: 20px;
                text-align: center;
                font-weight: bold;
                font-size: 14px;
                font-family: Verdana;
            }

            .modalPopupNew .body {
            }

        .modalPopup {
            background-color: #FFFFFF;
            width: 650px;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                padding: 10px;
                min-height: 50px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

        div {
            padding: 0px;
        }

        .btn {
            background: green;
            color: #fff;
            border: 1px solid #900;
            padding: 4px 8px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            margin: 20px;
            text-transform: uppercase;
            text-decoration: none;
            font: bold 12px Verdana, sans-serif;
            text-shadow: 0 0 1px #000;
            box-shadow: 0 2px 2px #aaa;
            -moz-box-shadow: 0 2px 2px #aaa;
            -webkit-box-shadow: 0 2px 2px #aaa;
        }

        .spanactive {
            background: #fff;
            border-radius: 10px;
        }

        table, th, td {
            border: 0px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        var timer;
        jQuery(function ($) {
            timer = setTimeout(blnk, 0);
        });


        function blnk() {
            $(".blink").css({ opacity: 0 }).
         animate({ opacity: 1 }, 500, "linear").
         animate({ opacity: 0 }, 300, "linear",
         function () {
             timer = setTimeout(blnk, 0);
         });
        }
    </script>
    <script type="text/javascript" language="javascript">
        window.onload = function () {
            noBack();
        }
        function noBack() {
            window.history.forward();
        }
    </script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("marquee").hover(function () {
                this.stop();
            }, function () {
                this.start();
            });
        });
    </script>
    <script type="text/javascript">

        function OpenNewWindow() {

            window.open('../../DoctorBirthday_View.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');

            return false;

        }

    </script>
    <script type="text/javascript">

        function OpenNewWindow_delay() {

            //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

            window.open('../../Delayed_Status_Multiple.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }

    </script>
    <script type="text/javascript">

        function OpenWindow() {

            var paramVal = "MRLink";
            window.open("../../NoticeBoard_design.aspx?id=" + paramVal, "ModalPopUp");

            return false;
        }

    </script>
     <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function TabletsIANs() {
          //  window.open('http://www.tors.torssfa.info/Tabletiancorner/home.asp', null, '');
            window.open('http://www.tors.sfacrm.info/Tabletiancorner/home.asp', null, '');
            return false;
        }

        function TabletsOldTors() {

            var div_code = "";
            var sf_type = '<%=Session["sf_type"] %>';

            if (sf_type == 1 || sf_type == 2) {
                div_code = '<%=Session["div_code"] %>';
            }
            else {
                div_code = '<%=Session["div_code"] %>';
                if (div_code.includes(",")) {
                    div_code = div_code.substring(0, div_code.length - 1)
                }
            }
            if (div_code == '2') {
              //  window.open('https://www.tors.torssfa.info/index.asp', null, '');
                window.open('http://www.tors.sfacrm.info/index.asp', null, '');
            }
            else if (div_code == '3') {
              //  window.open('https://www.vibranz.torssfa.info/index.asp', null, '');
                window.open('http://www.vibranz.sfacrm.info/index.asp', null, '');
            }
            else if (div_code == '4') {
                //window.open('https://www.tabzen.torssfa.info/index.asp', null, '');
                window.open('http://www.tabzen.sfacrm.info/index.asp', null, '');
            }
            else if (div_code == '5') {
               // window.open('https://www.parazen.torssfa.info/index.asp', null, '');
                window.open('http://www.parazen.sfacrm.info/index.asp', null, '');
            }
            return false;
        }
    </script>
      <script type="text/javascript">
        $(document).ready(function () {
            $("#btnTabletians").click(function (e) {
                TabletsIANs();
                e.preventDefault();
            });

            $("#btnTabletsOldTors").click(function (e) {
                TabletsOldTors();
                e.preventDefault();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0px; padding: 0px;">
            <ucl:Menu ID="menu1" runat="server" />
        </div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:Panel ID="pnlchart" runat="server" Style="vertical-align: top">
            <div class="charts-area clearfix">
                <div class="container chart-section-main-body">
                    <div class="row clearfix" style="align-items: center; text-align: center;">
                        <div class="col-lg-12">
                            <div class="row clearfix">
                                <div class="col-lg-12" style="text-align: center; padding: 20px;">
                                    <h2 class="text-center" style="color: #292a34; font-size: 24px; font-weight: 700;">
                                        <asp:Table runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Right" Width="10%">
                                                    <a id="btnTabletsOldTors" href="#">
                                                        <asp:Label Text="Old Tors" ID="Label1" runat="server" Style="font-size: 16px;" />
                                                    </a>
                                                </asp:TableCell><asp:TableCell HorizontalAlign="Right" Width="40%">
                                                    <asp:Label Text="Shortcut Menus" ID="lblHeadTxt" ForeColor="Black" runat="server" />
                                                </asp:TableCell><asp:TableCell Width="30%" HorizontalAlign="Right">
                                                <a id="btnTabletians" href="#">
                                                    <img src="../../assets/images/TABLETIAN_Corner.jpg" alt="" />
                                                </a>
                                                </asp:TableCell><asp:TableCell Width="1%" HorizontalAlign="Center">
                                              

                                                </asp:TableCell><asp:TableCell Width="1%" HorizontalAlign="Right">
                                                </asp:TableCell></asp:TableRow></asp:Table></h2><asp:LinkButton ID="btn_shrtcut" Text="Dashboard" CssClass="buttonlabel label" runat="server" OnClick="btn_shrtcut_Click" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix" style="align-items: center; text-align: center;">
                        <div class="col-lg-12">
                            <table width="100%" style="margin-top: 0px; margin-bottom: -50px;">
                                <tr>
                                    <td style="width: 48%" align="center">
                                        <div style="width: 100%">
                                            <div style="float: left; width: 50%">
                                                <h3 id="lblActivity" style="text-align: right;">Quick View</h3><br /></div><div style="float: right; margin-top: 5px; margin-right: 110px;">
                                                <asp:LinkButton ID="btnshow" runat="server" CssClass="linkviewlabel label" Text="Detail View">
                                                </asp:LinkButton></div></div><asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel"
                                            PopupControlID="Panel2" TargetControlID="btnshow" BackgroundCssClass="modalBackgroundNew">
                                        </asp:ModalPopupExtender>
                                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Width="600px" Height="500px" align="center" Style="display: none">
                                            <div class="header">
                                                Calendar Detail <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Close.gif" ImageAlign="Right" Style="margin-top: 4px; margin-right: 7px; width: 20px;" />
                                            </div>
                                            <div class="body">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Calendar ID="CalMgrDet" runat="server" Height="400px" Width="84%" Style="margin-left: 50px;"
                                                            NextPrevFormat="ShortMonth" SelectionMode="Day" ShowGridLines="True" BorderWidth="0" CssClass="calendarshadow">

                                                            <DayHeaderStyle BackColor="#ffffff" ForeColor="Black" CssClass="TextFont" BorderColor="#F3F7FA" Height="50" />
                                                            <DayStyle BackColor="#FFFFFF" Font-Names="'Roboto', sans-serif" BorderColor="SlateGray" BorderWidth="0" Font-Bold="true" ForeColor="#5e686f" CssClass="TextFont" />
                                                            <NextPrevStyle Font-Italic="true" ForeColor="#ccd1d5" Width="10%" CssClass="imgalign" />
                                                            <OtherMonthDayStyle BackColor="#f8fafc" ForeColor="#d1d5d7" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <TitleStyle BackColor="#f0f5f7" ForeColor="Black" Height="50" Font-Size="Large" BorderColor="#FFFFFF" CssClass="TextFont" />
                                                            <TodayDayStyle Font-Size="Small" />
                                                        </asp:Calendar>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </asp:Panel>
                                        <asp:Calendar ID="CalMgr" runat="server"
                                            PrevMonthText="<img src='images/PMonth.ICO' border=0 align=top>"
                                            DayNameFormat="Short" Width="80%"
                                            NextMonthText="<img src='Images/NMonth.ICO' border=0 align=top text-align=center>"
                                            ShowGridLines="True" Height="200px" BorderWidth="0" CssClass="calendarshadow">

                                            <DayHeaderStyle BackColor="#ffffff" ForeColor="Black" CssClass="TextFont" BorderColor="#F3F7FA" Height="50" />
                                            <DayStyle BackColor="#FFFFFF" Font-Names="'Roboto', sans-serif" BorderColor="SlateGray" BorderWidth="0" Font-Bold="true" ForeColor="#5e686f" CssClass="TextFont" />
                                            <NextPrevStyle Font-Italic="true" ForeColor="#ccd1d5" Width="10%" CssClass="imgalign" />
                                            <OtherMonthDayStyle BackColor="#f8fafc" ForeColor="#d1d5d7" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <SelectedDayStyle BackColor="LightBlue" Font-Size="Small" BorderColor="SeaGreen" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="TextFont" />
                                            <SelectorStyle BackColor="DarkSeaGreen" ForeColor="Snow" Font-Names="Times New Roman Greek"
                                                Font-Size="Small" BorderColor="MediumSeaGreen" BorderWidth="1" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="TextFont" />
                                            <TitleStyle BackColor="#f0f5f7" ForeColor="Black" Height="50" Font-Size="Large" BorderColor="#FFFFFF" CssClass="TextFont" />
                                            <TodayDayStyle Font-Size="Small" />
                                        </asp:Calendar>
                                    </td>
                                    <td style="width: 4%;">
                                        <div style="height: 350px; border-right: 2px dashed #d0e6f9;"></div>
                                    </td>
                                    <td style="width: 48%" align="center">
                                        <h3 id="lblSC" class="text-center" style="margin-top: -30px;">Short Cut</h3><br /><asp:Table ID="Table1" runat="server" BackColor="White" BorderStyle="Solid" Width="95%" Height="200px" BorderWidth="0">
                                            <asp:TableRow>
                                                <asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnmain" runat="server" Style="background: HotPink !important" CssClass="savebutton" ForeColor="White" Width="200px"
                                                        Text="Goto Main Page" OnClick="btnmain_Click" />
                                                </asp:TableCell><asp:TableCell BorderWidth="0" Width="200px" HorizontalAlign="Center">
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btntp" runat="server" CssClass="savebutton" Width="200px"
                                                        Text="TP Entry" OnClick="btntp_Click" />
                                                </asp:TableCell><asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnview" Width="200px" CssClass="savebutton" runat="server"
                                                        Text="TP View" OnClick="btnview_Click" />
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnDCR" runat="server" Visible="false" CssClass="savebutton" Style="background: #fed426 !important" Width="200px" Text="DCR Entry"
                                                        OnClick="btndcr_Click" />
                                                </asp:TableCell><asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnDCRView" runat="server" CssClass="savebutton" Style="background: #fed426 !important" Width="200px"
                                                        Text="DCR View" OnClick="btndcrview_Click" />
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center" Visible="false">
                                                    <asp:Button ID="btnTerr" runat="server" Visible="false" CssClass="savebutton" Style="background: #3bb913 !important" Width="200px"
                                                        PostBackUrl="~/MasterFiles/MGR/RptAutoExpense_Mgr_old.aspx" Text="Expense Entry" />
                                                    <asp:Button ID="btnTerr123" runat="server" Visible="false" CssClass="savebutton" Style="background: #3bb913 !important" Width="200px"
                                                        PostBackUrl="~/MasterFiles/MGR/RptAutoExpense_Mgr_New.aspx" Text="Expense Entry" />
                                                    <asp:Button ID="btnmgrrwtxt" runat="server" Visible="false" CssClass="savebutton" Style="background: #3bb913 !important" Width="200px"
                                                        PostBackUrl="~/MasterFiles/MGR/RptAutoExpense_Mgr_RowTextbox.aspx" Text="Expense Entry" />
                                                    <asp:Button ID="btnTerr456" runat="server" Visible="false" CssClass="savebutton" Style="background: #3bb913 !important" Width="200px"
                                                        PostBackUrl="~/MasterFiles/MGR/RptAutoExpense_Mgr_RM_Script.aspx" Text="Expense Entry" />
                                                </asp:TableCell><asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnlisteddr" runat="server" CssClass="savebutton" Style="background: #3bb913 !important"
                                                        PostBackUrl="~/MasterFiles/Reports/rptAutoexpense_Approve_View.aspx" Width="200px" Text="Expense View" />
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnmail" runat="server" CssClass="savebutton" Style="background: #f52533 !important" Width="200px" Text="Internal Mail Box"
                                                        OnClick="btnmail_Click" />
                                                </asp:TableCell><asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <br />
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell BorderWidth="0" ColumnSpan="2" HorizontalAlign="Left" Style="height: 40px; padding-left: 25px;">
                                                    <span style="vertical-align: bottom">
                                                        <img src="assets/images/notice_board.png" alt="" width="28px" /></span>
                                                    <asp:LinkButton ID="LnkNotice" runat="server" Font-Size="16px" ForeColor="#48535b" Font-Bold="true"
                                                        Font-Italic="false" Text="Notice Board" OnClientClick="return OpenWindow() ;"></asp:LinkButton>
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell BorderWidth="0" ColumnSpan="2" HorizontalAlign="Left" Style="height: 40px; padding-left: 25px;">
                                                    <span style="vertical-align: bottom">
                                                        <img src="assets/images/rejection.png" alt="" width="28px" /></span>
                                                    <asp:LinkButton ID="lnkreject" runat="server" Font-Size="16px" ForeColor="#48535b" Font-Bold="true"
                                                        Font-Italic="false" Text="Rejection / ReEntries " OnClick="lnkreject_Click">
                                                    </asp:LinkButton>
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell BorderWidth="0" ColumnSpan="2" HorizontalAlign="Left" Style="height: 40px; padding-left: 25px;">
                                                    <span style="vertical-align: bottom">
                                                        <img src="assets/images/gift.png" alt="" width="28px" /></span>
                                                    <asp:LinkButton ID="LnkDoctor" runat="server" Font-Size="16px" ForeColor="#48535b" Font-Bold="true"
                                                        Font-Italic="false" Text="Doctor's DOB and DOW View" OnClientClick="return OpenNewWindow() ;"></asp:LinkButton>
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell BorderWidth="0" ColumnSpan="2" HorizontalAlign="Left" Style="height: 40px; padding-left: 25px;">
                                                    <span style="vertical-align: bottom;">
                                                        <img src="assets/images/delay.png" alt="" width="28px" /></span>
                                                    <asp:LinkButton ID="lnkdelay" runat="server" Font-Size="16px" ForeColor="#48535b" Font-Bold="true"
                                                        Font-Italic="false" Text="Delay Details " OnClientClick="return OpenNewWindow_delay();">
                                                    </asp:LinkButton>
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell Width="200px" BorderWidth="0" ColumnSpan="2" HorizontalAlign="Center">                     
                                                </asp:TableCell></asp:TableRow></asp:Table></td></tr></table><center>
                                <asp:Panel ID="pnlhome" runat="server" BorderWidth="0" Width="90%" CssClass="calendarshadow" Style="margin-left: 0px !important; margin-top: 60px;">
                                    <table width="100%" border="0" style="border-width: 0">
                                        <tr>
                                            <td align="left" colspan="2" style="padding: 20px 25px 10px 35px">
                                                <asp:Label ID="lblFN" runat="server" Text="Flash News" Font-Bold="true" Font-Size="18px" Font-Italic="true" ForeColor="#636d73" BackColor="White"></asp:Label></td></tr><tr>
                                            <td colspan="2" style="padding: 5px 20px 5px 20px">
                                                <div style="background-color: #f1f5f8; border-radius: 10px; height: 40px; padding: 10px 7px 7px 30px; font-size: 14px; font-weight: 400; color: #5484f2;">
                                                    <span id="all" style="width: 75px; text-align: center; display: inline-block;">All</span> <span id="unread" style="width: 75px; text-align: center; display: inline-block;" class="spanactive">Unread</span> <span id="new" style="width: 75px; text-align: center; display: inline-block;">New</span> </div></td></tr><tr>
                                            <td align="left" style="padding: 0 0 10px 30px" colspan="2">
                                                <div style="width: 33%; float: inline-start; text-align: justify; padding-right: 15px;">
                                                    <div style="height: 125px; border-bottom: 1px dashed #d0e6f9;">
                                                        <asp:Label ID="lblFlash" runat="Server" Style="margin-top: 10px;" Width="100%"
                                                            ForeColor="Black" Font-Size="16px" Text='<%# Eval("FN_Cont1") %>'></asp:Label></div></div><div style="width: 33%; float: inline-start; text-align: justify; padding-right: 15px;">
                                                    <div style="height: 125px; border-bottom: 1px dashed #d0e6f9;">
                                                        <asp:Label ID="lblsup1" runat="server" ForeColor="Black" Font-Bold="false" Font-Size="14px" Text='<%# Eval("TalktoUs_Text") %>'> 
                                                        </asp:Label></div></div><div style="width: 33%; float: inline-start; text-align: justify; padding-right: 10px;">
                                                    <div style="height: 125px; border-bottom: 1px dashed #d0e6f9;">
                                                        <asp:Label ID="lblsup2" runat="server" ForeColor="Black" Font-Bold="false" Font-Size="14px" Text='<%# Eval("TalktoUs_Text") %>'> 
                                                        </asp:Label></div></div></td></tr></table><table width="97%" border="0" style="border-width: 0">
                                        <tr>
                                            <td align="left" style="padding: 15px 5px 16px 25px; background-color: #f1f5f8;">
                                                <img src="assets/images/fieldsupport.png" width="30px;" />
                                            </td>
                                            <td align="left" style="padding: 15px 15px 16px 5px; background-color: #f1f5f8;">
                                                <asp:Label ID="lblsup" runat="server" ForeColor="Brown" Font-Bold="false" Font-Size="10px" Text='<%# Eval("TalktoUs_Text") %>'> 
                                                </asp:Label></td></tr></table><br /></asp:Panel></center></div></div></div></div></asp:Panel><div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="Images/loader.gif" alt="" /> </div></form></body></html>