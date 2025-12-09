<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default_MR.aspx.cs" Inherits="Default_MR" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>E-Reporting Sales & Analysis</title>
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
    <style type="text/css">
        .bodyBgclr {
            /*background:url(https://www.highcharts.com/samples/graphics/skies.jpg) 0% 0%;*/
            background: url(Images/bgclr.jpg) 0% 0%;
            background-size: cover;
        }

        #divChrtRow1 {
            background: url(http://www.psdgraphics.com/file/colorful-triangles-background.jpg) 0% 0%;
            background-size: cover;
        }

        #divChrtRow1 {
            background: url(http://www.highcharts.com/images/stories/logohighcharts.png) 0% 0%;
            background-size: cover;
        }

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

        .imgalign {
            text-align: center;
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

        .spanactive {
            background: #fff;
            border-radius: 10px;
        }

        .TextFont {
            text-align: center;
            margin-top: 6px;
            height: 50px;
            font-size: 14px;
            font-weight: 400;
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

        .modalBackgroundNew {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopupNew {
            background-color: #FFFFFF;
            width: 650px;
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

        .modalPopup .header {
            background-color: #2FBDF1;
            height: 40px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
            border-top-left-radius: 6px;
            border-top-right-radius: 6px;
        }

        .modalPopup .body {
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
            padding: 20px;
        }

        .btn {
            background: blue;
            color: #fff;
            border: 1px solid #900;
            padding: 4px 8px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            text-transform: uppercase;
            text-decoration: none;
            font: bold 12px Verdana, sans-serif;
            text-shadow: 0 0 1px #000;
            box-shadow: 0 2px 2px #aaa;
            -moz-box-shadow: 0 2px 2px #aaa;
            -webkit-box-shadow: 0 2px 2px #aaa;
            background-image: linear-gradient(to top, #0077ff 0%, #28b5e0 100%);
            cursor: pointer;
            border: 0px;
            color: #ffffff;
            font-size: 14px;
            font-weight: 600;
            margin: 0 3px;
            margin-top: 0px;
            margin-bottom: 0px;
            padding: 0px;
            margin-top: 5px;
            margin-bottom: 5px;
        }
    </style>
    <style type="text/css" xml:space="preserve" class="blink">
        div.blink {
            text-decoration: blink;
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

        .loader {
            background-color: rgba(0,0,0,0.95);
            height: 100%;
            width: 100%;
            position: fixed;
            z-index: 999;
            margin-top: 0px;
            top: 0px;
        }

        .loader-centered {
            position: absolute;
            left: 50%;
            top: 50%;
            height: 200px;
            width: 200px;
            margin-top: -100px;
            margin-left: -100px;
        }

        .object {
            width: 50px;
            height: 50px;
            background-color: rgba(255,255,255,0);
            margin-right: auto;
            margin-left: auto;
            border: 4px solid #FFF;
            left: 73px;
            top: 73px;
            position: absolute;
        }

        .square-one {
            -webkit-animation: first_object_animate 1s infinite ease-in-out;
            animation: first_object_animate 1s infinite ease-in-out;
        }

        .square-two {
            -webkit-animation: second_object 1s forwards, second_object_animate 1s infinite ease-in-out;
            animation: second_object 1s forwards, second_object_animate 1s infinite ease-in-out;
        }

        .square-three {
            -webkit-animation: third_object 1s forwards, third_object_animate 1s infinite ease-in-out;
            animation: third_object 1s forwards, third_object_animate 1s infinite ease-in-out;
        }

        @-webkit-keyframes second_object {
            100% {
                width: 100px;
                height: 100px;
                left: 48px;
                top: 48px;
            }
        }

        @keyframes second_object {
            100% {
                width: 100px;
                height: 100px;
                left: 48px;
                top: 48px;
            }
        }

        @-webkit-keyframes third_object {
            100% {
                width: 150px;
                height: 150px;
                left: 23px;
                top: 23px;
            }
        }

        @keyframes third_object {
            100% {
                width: 150px;
                height: 150px;
                left: 23px;
                top: 23px;
            }
        }

        @-webkit-keyframes first_object_animate {
            0% {
                -webkit-transform: perspective(100px);
            }

            50% {
                -webkit-transform: perspective(100px) rotateY(-180deg);
            }

            100% {
                -webkit-transform: perspective(100px) rotateY(-180deg) rotateX(-180deg);
            }
        }

        @keyframes first_object_animate {
            0% {
                transform: perspective(100px) rotateX(0deg) rotateY(0deg);
                -webkit-transform: perspective(100px) rotateX(0deg) rotateY(0deg);
            }

            50% {
                transform: perspective(100px) rotateX(-180deg) rotateY(0deg);
                -webkit-transform: perspective(100px) rotateX(-180deg) rotateY(0deg);
            }

            100% {
                transform: perspective(100px) rotateX(-180deg) rotateY(-180deg);
                -webkit-transform: perspective(100px) rotateX(-180deg) rotateY(-180deg);
            }
        }

        @-webkit-keyframes second_object_animate {
            0% {
                -webkit-transform: perspective(200px);
            }

            50% {
                -webkit-transform: perspective(200px) rotateY(180deg);
            }

            100% {
                -webkit-transform: perspective(200px) rotateY(180deg) rotateX(180deg);
            }
        }


        @keyframes second_object_animate {
            0% {
                transform: perspective(200px) rotateX(0deg) rotateY(0deg);
                -webkit-transform: perspective(200px) rotateX(0deg) rotateY(0deg);
            }

            50% {
                transform: perspective(200px) rotateX(180deg) rotateY(0deg);
                -webkit-transform: perspective(200px) rotateX(180deg) rotateY(0deg);
            }

            100% {
                transform: perspective(200px) rotateX(180deg) rotateY(180deg);
                -webkit-transform: perspective(200px) rotateX(180deg) rotateY(180deg);
            }
        }

        @-webkit-keyframes third_object_animate {
            0% {
                -webkit-transform: perspective(300px);
            }

            50% {
                -webkit-transform: perspective(300px) rotateY(-180deg);
            }

            100% {
                -webkit-transform: perspective(300px) rotateY(-180deg) rotateX(-180deg);
            }
        }

        @keyframes third_object_animate {
            0% {
                transform: perspective(300px) rotateX(0deg) rotateY(0deg);
                -webkit-transform: perspective(300px) rotateX(0deg) rotateY(0deg);
            }

            50% {
                transform: perspective(300px) rotateX(-180deg) rotateY(0deg);
                -webkit-transform: perspective(300px) rotateX(-180deg) rotateY(0deg);
            }

            100% {
                transform: perspective(300px) rotateX(-180deg) rotateY(-180deg);
                -webkit-transform: perspective(300px) rotateX(-180deg) rotateY(-180deg);
            }
        }
    </style>
    <script type="text/javascript">

        function OpenNewWindow_delay() {

            //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

            window.open('Delayed_Status_Multiple.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }

    </script>
    <script type="text/javascript">
        var timer;
        jQuery(function ($) {
            timer = setTimeout(blnk, 0);
        });


        function blnk() {
            $(".blink2").css({ opacity: 0 }).
        animate({ opacity: 1 }, 500, "linear").
        animate({ opacity: 0 }, 300, "linear",
        function () {
            timer = setTimeout(blnk, 0);
        });
        }
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $(".loader").delay(2000).fadeOut("slow");
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#btn_shrtcut').text() === 'Show Chart2') {
                var pData = [];
                pData[0] = $("#ddlChrtMn").val();
                pData[1] = $("#ddlChrtYr").val();

                var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
                var function_name = "getChartVal";
                $.ajax({
                    type: "POST",
                    url: "Default_MR.aspx/" + function_name,
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_,
                    error: OnErrorCall_
                });

                function OnSuccess_(response) {
                    var aData = response.d;
                    var arr = [];
                    var sf_code, ttl_dr, dr_mt, dr_sn, dr_rpt, dr_rpt_sn, fw_dys, covrg, rpt_covg, call_avg, rpt_mt_covrg;

                    $.map(aData, function (item, index) {
                        console.log(item);
                        sf_code = item.Sf_Code; ttl_dr = item.Total_Drs; dr_mt = item.Drs_Met; dr_sn = item.Drs_Seen; dr_rpt = item.Drs_Rpt; dr_rpt_sn = item.Drs_Rpt_sn;
                        fw_dys = item.FW_Dys; covrg = item.Coverage; rpt_covg = item.Rpt_Coverage; call_avg = item.Call_Avg; rpt_mt_covrg = item.Rpt_Mt_coverage;

                        var obj = {};
                        obj.name = 'Total Drs';
                        obj.data = [item.Total_Drs];
                        arr.push(obj);
                        var obj = {};
                        obj.name = 'Joint Days';
                        obj.data = [item.Jnt_Dys];
                        arr.push(obj);
                        var obj = {};
                        obj.name = 'Joint Met';
                        obj.data = [item.Jnt_Mt];
                        arr.push(obj);
                        var obj = {};
                        obj.name = 'Joint Seen';
                        obj.data = [item.Jnt_Sn];
                        arr.push(obj);
                        var obj = {};
                        obj.name = 'Joint Call Avg';
                        obj.data = [item.Jnt_Call_Avg];
                        arr.push(obj);
                        var obj = {};
                        obj.name = 'Joint Coverage';
                        obj.data = [item.Jnt_Coverage];
                        arr.push(obj);
                    });
                    var myJsonString = JSON.stringify(arr);
                    var jsonArray = JSON.parse(JSON.stringify(arr));

                    for (var j = 0; j < 3; j++) {
                        if (j == 1)
                            draw(jsonArray);
                        else if (j == 2)
                            product();
                        else {
                            meter(call_avg);
                            drawGaugeChart(covrg);
                            SolidGauge(ttl_dr, dr_rpt, dr_rpt_sn, rpt_mt_covrg);
                            DualGauge(ttl_dr, dr_mt);
                            chrtClock(ttl_dr, dr_mt, dr_sn, fw_dys);
                        }
                    }
                }
                function OnErrorCall_(response) {
                    alert("Error: No Data Found!");
                }
                e.preventDefault();
            }
        });
        //*
    </script>
    <%-- --%>
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
    <script language="C#" runat="server">

        void DayRender(Object source, DayRenderEventArgs e)
        {

            if (!e.Day.IsOtherMonth && !e.Day.IsWeekend)
                e.Cell.BackColor = System.Drawing.Color.Yellow;

        }

    </script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function TabletsIANs() {
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
                window.open('http://www.tors.sfacrm.info/index.asp', null, '');
            }
            else if (div_code == '3') {
                window.open('http://www.vibranz.sfacrm.info/index.asp', null, '');
            }
            else if (div_code == '4') {
                window.open('http://www.tabzen.sfacrm.info/index.asp', null, '');
            }
            else if (div_code == '5') {
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



    <script type="text/javascript">

        function OpenNewWindow() {

            //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

            window.open('DoctorBirthday_View.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }

    </script>
    <script type="text/javascript">

        function OpenWindow() {

            //          //  window.open("NoticeBoard_design.aspx");
            //            window.open('NoticeBoard_design.aspx', null, 'height=800, width=700,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');

            //            return false;
            var paramVal = "MRLink";
            window.open("NoticeBoard_design.aspx?id=" + paramVal,
              "ModalPopUp"//,
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=700," +
    //"height=800," +
    //"left = 0," +
    //"top=0"
    );
            return false;

            //window.open('NoticeBoard_design.aspx', null, 'height=500, width=900, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
        }

    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0px;">
            <ucl:Menu ID="menu1" runat="server" />
        </div>
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
                                                <asp:Label Text="Dashboard" ID="lblHeadTxt" ForeColor="Black" runat="server" />
                                                  </asp:TableCell><asp:TableCell Width="20%" HorizontalAlign="Right">
                                                <a id="btnTabletians" href="#">
                                                    <img src="assets/images/TABLETIAN_Corner.jpg" alt="" />
                                                </a>
                                            </asp:TableCell><asp:TableCell Width="10%" HorizontalAlign="Center">
                                              

                                            </asp:TableCell><asp:TableCell Width="10%" HorizontalAlign="Right">
                                            </asp:TableCell></asp:TableRow></asp:Table></h2><asp:LinkButton ID="btn_shrtcut" Text="Show Chart" Visible="false" CssClass="buttonlabel label" runat="server" OnClick="btn_shrtcut_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlContents" runat="server" Width="100%">
                    <div id="chrt" runat="server">
                        <div class="row clearfix" style="align-items: center; text-align: center;">
                            <div class="col-lg-12" style="margin-top: -40px; margin-bottom: -30px;">
                                <div class="search-area clearfix">
                                    <form action="" method="get">
                                        <div class="row clearfix">
                                            <div class="col-lg-3">
                                                <div class="single-option">
                                                    <asp:Label ID="lblFMonth" runat="server" CssClass="label" Text="Select Month & Year"></asp:Label></div></div><div class="col-lg-3">
                                                <div class="single-option">
                                                    <asp:DropDownList runat="server" CssClass="nice-select" ID="ddlChrtMn">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="single-option">
                                                    <asp:DropDownList runat="server" CssClass="nice-select" ID="ddlChrtYr">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="single-option">
                                                    <asp:Label Text="" ID="lblChrtMnth" Font-Bold="true" runat="server" />
                                                    <asp:Button Text="View" CssClass="savebutton" ID="btnViewChart" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                        <div class="row justify-content-center clearfix">
                            <div class="col-lg-3">
                                <div class="single-chart single-top-chat-min-height">
                                    <h3>Call Average</h3><div id="divChrtClAvg"></div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="single-chart single-top-chat-min-height">
                                    <h3>Coverage</h3><div id="divChrtRptCls"></div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="single-chart single-top-chat-min-height">
                                    <h3>Repeated Call Details</h3><div class="single-time-chat p-0">
                                        <div id="divChrtSolidGg"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="single-chart single-top-chat-min-height">
                                    <h3>Missed Calls & Percentage</h3><div class="miss">
                                        <div class="mdh-per">
                                            <span id="mdh-per"></span>
                                            <br />
                                            Msd </div><div class="mdh-data">
                                            <span id="mdh-data"></span>
                                            <br />
                                            Msd </div></div></div></div></div><div class="row justify-content-between clearfix">
                            <div class="col-lg-4">
                                <div class="single-chart single-bottom-chat-min-height">
                                    <h3>Joint Works Details</h3><div class="jone-chat clearfix">
                                        <div id="divChrtClmn"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="single-chart single-bottom-chat-min-height">
                                    <h3>Call Details</h3><div id="divChrtClk"></div>
                                    <div id="lblChrtClk"></div>
                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="single-chart single-bottom-chat-min-height">
                                    <h3>Product Vs Promotion</h3><div class="promotionchart fix">
                                        <div id="divChrClmn3d"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div id="shrtct_div" runat="server">
                    <div class="row clearfix" style="align-items: center; text-align: center;">
                        <div class="col-lg-12">
                            <table width="100%" align="center">
                                <tbody>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:GridView ID="grdProduct" runat="server" Width="100%" HorizontalAlign="Center"
                                                AutoGenerateColumns="false" GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Short Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProdCode" runat="server" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Product Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProDes" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Sale Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSaleUn" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Sample Unit1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsam1" runat="server" Text='<%# Bind("Product_Sample_Unit_One") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Sample Unit2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsam2" runat="server" Text='<%# Bind("Product_Sample_Unit_Two") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Sample Unit3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsam3" runat="server" Text='<%# Bind("Product_Sample_Unit_Three") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Product Group">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgr" runat="server" Text='<%# Bind("Product_Grp_Name") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Product Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcar" runat="server" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label></ItemTemplate></asp:TemplateField></Columns></asp:GridView></td></tr></tbody></table><table width="100%" style="margin-top: 0px; margin-bottom: -50px;">
                                <tr>
                                    <td style="width: 48%" align="center">
                                        <div style="width: 100%">
                                            <div style="float: left; width: 50%">
                                                <h3 id="lblActivity" style="text-align: right;">Quick View</h3><br />
                                            </div>
                                            <div style="float: right; margin-top: 5px; margin-right: 110px;">
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
                                                        <asp:Calendar ID="CalMgrDet" runat="server" Height="400px" Width="84%"
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
                                        <asp:Calendar ID="Calendar1" runat="server"
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
                                        <h3 id="lblSC" class="text-center" style="margin-top: -128px;">Short Cut</h3><br />
                                        <asp:Table ID="Table1" runat="server" BackColor="White" BorderStyle="Solid" Width="95%" Height="200px"
                                            BorderWidth="0">
                                            <asp:TableRow>
                                                <asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">

                                                    <asp:Button ID="btntp" runat="server" CssClass="savebutton" BackColor="Chocolate" ForeColor="White" Width="200px"
                                                        Text="TP Entry" OnClick="btntp_Click" />
                                                </asp:TableCell><asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnview" Width="200px" CssClass="savebutton" runat="server" BackColor="Chocolate" ForeColor="White"
                                                        Text="TP View" OnClick="btnview_Click" />
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnDCR"  runat="server" CssClass="savebutton" Style="background: #fed426 !important" Width="200px" Text="DCR Entry"
                                                        OnClick="btndcr_Click" />
                                                    <br />
                                                    <asp:Button ID="btnNDCR" runat="server" CssClass="savebutton" Visible="false" Style="background: #fed426 !important" Width="200px" Text="DCR Entry ( Classic )"
                                                        OnClick="btnNDCR_Click" />
                                                </asp:TableCell><asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnDCRView" runat="server" CssClass="savebutton" Style="background: #fed426 !important" Width="200px"
                                                        Text="DCR View" OnClick="btndcrview_Click" />
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnTerr" runat="server" CssClass="savebutton" Visible="false" Style="background: #3bb913 !important" Width="200px" Text="Territory Entry"
                                                        OnClick="btnTerr_Click" />
                                                </asp:TableCell><asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnlisteddr" runat="server" CssClass="savebutton" Visible="false" Style="background: #3bb913 !important" Width="200px" Text="Listed Dr Entry"
                                                        OnClick="btnlisteddr_Click" />
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center" Visible="false">
                                                    <asp:Button ID="btnEx_entry" runat="server" CssClass="savebutton" Style="background: #3bb913 !important" Width="200px" Text="Expense Entry" Visible="false" OnClick="btnEx_entry_Click" />
                                                </asp:TableCell><asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center">
                                                    <asp:Button ID="btnEx_view" runat="server" CssClass="savebutton" Style="background: #3bb913 !important" Width="200px" Text="Expense View" OnClick="btnEx_view_Click" />
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center" Visible="false">
                                                    <asp:Button ID="btnSS_entry" runat="server" CssClass="savebutton" Style="background: #6e46e2 !important" Width="200px" Text="SS Entry" OnClick="btnSS_entry_Click" />
                                                </asp:TableCell><asp:TableCell Width="200px" BorderWidth="0" HorizontalAlign="Center" Visible="false">
                                                    <asp:Button ID="btnSS_view" runat="server" CssClass="savebutton" Style="background: #6e46e2 !important" Width="200px" Text="SS View" OnClick="btnSS_view_Click" />
                                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell Width="200px" HorizontalAlign="Center">
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
                                <asp:Panel ID="pnlhome" runat="server" BorderWidth="0" Width="90%" CssClass="calendarshadow" Style="margin-left: 0px !important;">
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
                                                </asp:Label></td></tr></table><br /></asp:Panel></center></div></div></div></div></div><script src="assets/js/jQuery.min.js"></script><script src="assets/js/popper.min.js"></script><script src="assets/js/bootstrap.min.js"></script><script src="assets/js/jquery.nice-select.min.js"></script><script src="assets/js/ApexChart.js"></script><script src="https://code.highcharts.com/highcharts.js"></script><script src="https://code.highcharts.com/highcharts-more.js"></script><script src="https://code.highcharts.com/modules/solid-gauge.js"></script><div>
            <div id="chrt1" runat="server">
                <script type="text/javascript">

                    $("#all").click(function () {
                        $("#all").addClass("spanactive");
                        $("#unread").removeClass("spanactive");
                        $("#new").addCremoveClasslass("spanactive");
                    });
                    $("#unread").click(function () {
                        $("#all").removeClass("spanactive");
                        $("#unread").addClass("spanactive");
                        $("#new").removeClass("spanactive");
                    });
                    $("#new").click(function () {
                        $("#all").removeClass("spanactive");
                        $("#unread").removeClass("spanactive");
                        $("#new").addClass("spanactive");
                    });

                    function meter(result) {
                        var endPoint = Math.round(result + 5);
                        var chart = {
                            type: 'gauge',
                            plotBackgroundColor: null,
                            plotBackgroundImage: null,
                            plotBorderWidth: 0,
                            plotShadow: false,
                            height: 250,
                        };
                        var credits = {
                            enabled: false
                        };

                        var title = {
                            text: ''
                        };
                        var exporting = {
                            enabled: false
                        };
                        var pane = [{
                            startAngle: -150,
                            endAngle: 150,
                            background: null,
                        }];

                        var yAxis = {
                            min: 0,
                            max: endPoint,
                            plotBands: [{
                                from: 0,
                                to: Math.round(endPoint / 3),
                                color: '#C02316',
                                outerRadius: '105%',
                                thickness: '10%'
                            }, {
                                from: Math.round(endPoint / 3),
                                to: Math.round(endPoint / 3) + Math.round(endPoint / 3),
                                color: '#DDDF0D',
                                outerRadius: '105%',
                                thickness: '10%'
                            }, {
                                from: Math.round(endPoint / 3) + Math.round(endPoint / 3),
                                to: endPoint,
                                color: '#55BF3B',
                                outerRadius: '105%',
                                thickness: '10%'
                            }],
                            pane: 0,
                            title: {
                                text: 'Call Average',
                                y: 25
                            }
                        };

                        //var plotOptions = {
                        //    gauge: {
                        //        dataLabels: {
                        //            enabled: true,
                        //            y: -65
                        //        },
                        //        dial: {
                        //            radius: '100%',
                        //            backgroundColor: '#000000',
                        //            topWidth: 1,
                        //            baseWidth: 4,
                        //            rearLength: '-4%'
                        //        },
                        //        pivot: {
                        //            radius: 7,
                        //            borderWidth: 7,
                        //            borderColor: 'white',
                        //            backgroundColor: 'transparent'
                        //        }
                        //    }
                        //};
                        var series = [{
                            name: 'Call Average',
                            data: [result],
                            dataLabels: {
                                formatter: function () {
                                    return '<span style="color:#339">' + result + ' </span>';
                                }
                            },
                            //yAxis: 0,
                            tooltip: {
                                valueSuffix: ' '
                                //y: 40
                            }
                        }];

                        var json = {};
                        json.chart = chart;
                        json.credits = credits;
                        json.title = title;
                        json.pane = pane;
                        json.yAxis = yAxis;
                        //json.plotOptions = plotOptions;
                        json.series = series;
                        json.exporting = exporting;

                        $('#divChrtClAvg').highcharts(json);
                    }
                </script>
                <script type="text/javascript">
                    function drawGaugeChart(result) {
                        console.log(result);
                        var chart = {
                            type: 'gauge',
                            plotBackgroundColor: null,
                            plotBackgroundImage: null,
                            plotBorderWidth: 0,
                            plotShadow: false,
                            height: 250,
                        };
                        var title = {
                            text: '',
                            y: 20
                        };

                        var pane = {
                            startAngle: -150,
                            endAngle: 150,
                            background: null,
                        };

                        // the value axis
                        var yAxis = {
                            min: 0,
                            max: 100,

                            minorTickInterval: 'auto',
                            minorTickWidth: 0,
                            minorTickLength: 10,
                            minorTickPosition: 'inside',
                            minorTickColor: '#000',

                            tickPixelInterval: 30,
                            tickWidth: 2,
                            tickPosition: 'inside',
                            tickLength: 10,
                            tickColor: '#fff',
                            labels: {
                                step: 2,
                                rotation: 'auto'
                            },
                            title: {
                                text: 'Coverage',
                                y: 25
                            },
                            plotBands: [{
                                from: 0,
                                to: 40,
                                color: '#6BFADA' // green
                            }, {
                                from: 40,
                                to: 80,
                                color: '#F4CB45' // yellow
                            }, {
                                from: 80,
                                to: 100,
                                color: '#FF7C7A' // red
                            }]
                        };
                        var exporting = {
                            enabled: false
                        };
                        var series = [{
                            name: 'Coverage',
                            data: [result],
                            dataLabels: {
                                formatter: function () {
                                    return '<span style="color:#339">' + result + ' %</span>';
                                }
                            },
                            tooltip: {
                                valueSuffix: ' %'
                            }
                        }];

                        //var plotOptions = {
                        //    gauge: {
                        //        dataLabels: {
                        //            enabled: true,
                        //            y: 20
                        //        },
                        //        dial: {
                        //            radius: '100%',
                        //            backgroundColor: '#FF0000',
                        //            topWidth: 1,
                        //            baseWidth: 4
                        //        },
                        //        pivot: {
                        //            radius: 7,
                        //            borderWidth: 4,
                        //            borderColor: 'blue',
                        //            backgroundColor: 'blue'
                        //        }
                        //    }
                        //};

                        var json = {};
                        json.chart = chart;
                        json.title = title;
                        json.pane = pane;
                        json.yAxis = yAxis;
                        json.series = series;
                        json.exporting = exporting;
                        //json.plotOptions = plotOptions;

                        // Add some life
                        var chartFunction = function (chart) {
                            if (!chart.renderer.forExport) {
                                setInterval(function () {
                                    var point = chart.series[0].points[0],
                                newVal,
                                inc = Math.round((Math.random() - 0.5) * 20);

                                    newVal = point.y + inc;
                                    if (newVal < 0 || newVal > 100) {
                                        newVal = point.y - inc;
                                    }
                                    point.update(newVal);
                                }, 3000);
                            }
                        };
                        $('#divChrtRptCls').highcharts(json); //, chartFunction);
                    }
                </script>
                <script type="text/javascript">
                    function SolidGauge(ttldr, rpt, rpt_sn, rptcvg) {
                        var iTtl = ttldr;
                        if (ttldr === 0)
                            iTtl = 100;

                        var gaugeOptions = {
                            chart: {
                                type: 'solidgauge',
                                height: 250,
                                backgroundColor: 'rgba(0,0,0,0)',
                                width: 235
                            },
                            title: {
                                text: ''
                            },
                            pane: {
                                center: ['50%', '75%'],
                                size: '100%',
                                startAngle: -90,
                                endAngle: 90,
                                background: {
                                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || '#EEE',
                                    innerRadius: '60%',
                                    outerRadius: '100%',
                                    shape: 'arc'
                                }
                            },

                            tooltip: {
                                enabled: false
                            },

                            // the value axis
                            yAxis: {
                                stops: [
                                [0.1, '#55BF3B'], // green
                                [0.4, '#DDDF0D'], // yellow
                                [0.9, '#DF5353'] // red
                                ],
                                lineWidth: 0,
                                minorTickInterval: null,
                                //tickAmount: 2,
                                tickInterval: 20,
                                title: {
                                    text: '<b><i>Calls Met</i></b>',
                                    y: 15
                                },
                                labels: {
                                    y: -5
                                }
                            },

                            plotOptions: {
                                solidgauge: {
                                    dataLabels: {
                                        y: 5,
                                        borderWidth: 0,
                                        useHTML: true
                                    }
                                }
                            }
                        };

                        // The speed gauge
                        $('#divChrtSolidGg').highcharts(Highcharts.merge(gaugeOptions, {
                            yAxis: {
                                min: 0,
                                max: iTtl
                            },
                            exporting: {
                                enabled: false
                            },
                            credits: {
                                enabled: false
                            },
                            series: [{
                                name: 'Calls',
                                data: [rpt],
                                dataLabels: {
                                    formatter: function () {
                                        return '<br /><span style="color:#339; position: absolute;top: 15px;left: -50px">Coverage   : ' + rptcvg + '%</span><br/>' +
                                            '<span style="color:#933; position: absolute;top: 30px;left: -50px">Calls Met  : ' + rpt + '</span><br/>' +
                                            '<span style="color:#000; position: absolute;top: 45px;left: -50px">Calls Seen: ' + rpt_sn + '</span>';
                                    }
                                }
                            }]

                        }));
                    }
                </script>
                <script type="text/javascript">
                    function DualGauge(ttldr, drmt) {
                        var iTtl = ttldr, iTmpTtl = ttldr;
                        var msddr = 0;
                        var msd_prcnt = 0;

                        if (ttldr === 0) {
                            iTtl = 1;
                            iTmpTtl = 100;
                        }
                        var drmsd = ttldr - drmt;

                        var chart = {
                            type: 'gauge',
                            backgroundColor: 'rgba(0,0,0,0)',
                            plotBorderWidth: 0,
                            plotShadow: false,
                            height: 250
                        };
                        var credits = {
                            enabled: false
                        };

                        var title = {
                            text: ''
                        };

                        var pane = {
                            startAngle: -150,
                            endAngle: 150
                        };

                        // the value axis
                        var yAxis = [{
                            min: 0,
                            max: iTmpTtl,
                            lineColor: '#339',
                            tickColor: '#339',
                            minorTickColor: '#339',
                            offset: -25,
                            lineWidth: 2,
                            labels: {
                                distance: -20,
                                rotation: 'auto'
                            },
                            tickLength: 5,
                            minorTickLength: 5,
                            endOnTick: false
                        }, {
                            min: 0,
                            max: 100,
                            tickPosition: 'outside',
                            lineColor: '#933',
                            lineWidth: 2,
                            minorTickPosition: 'outside',
                            tickColor: '#933',
                            minorTickColor: '#933',
                            tickLength: 5,
                            minorTickLength: 5,
                            labels: {
                                distance: 12,
                                rotation: 'auto'
                            },
                            offset: -20,
                            endOnTick: false
                        }];

                        var series = [{
                            name: 'Missed Calls',
                            data: [drmsd],
                            dataLabels: {
                                formatter: function () {
                                    msddr = [drmsd][0],
                                    msd_prcnt = ((msddr / iTtl) * 100).toFixed(1);
                                    return '<span style="color:#933">Msd:' + msd_prcnt + '%</span><br/>' +
                                        '<span style="color:#339"> Msd:' + msddr + '</span>';
                                },
                                backgroundColor: {
                                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                                    stops: [
                                   [0, '#DDD'],
                                   [1, '#FFF']
                                    ]
                                },
                                y: -18
                            },
                            tooltip: {
                                valueSuffix: ''
                            }
                        }];
                        //console.log(series);

                        var exporting = {
                            enabled: false
                        };
                        var plotOptions = {
                            gauge: {
                                dataLabels: {
                                    enabled: true,
                                    y: -65
                                },
                                dial: {
                                    radius: '78%',
                                    backgroundColor: '#FFF00',
                                    topWidth: 1,
                                    baseWidth: 4,
                                    rearLength: '10%'
                                },
                                pivot: {
                                    radius: 3,
                                    borderWidth: 3,
                                    borderColor: 'green',
                                    backgroundColor: 'transparent'
                                }
                            }
                        };
                        var json = {};
                        json.chart = chart;
                        json.credits = credits;
                        json.title = title;
                        json.pane = pane;
                        json.yAxis = yAxis;
                        json.series = series;
                        json.exporting = exporting;
                        json.plotOptions = plotOptions;

                        $("#mdh-per").html((([drmsd][0] / iTtl) * 100).toFixed(1) + " %");
                        $("#mdh-data").html([drmsd][0]);

                        $('#divChrtCvg').highcharts(json);
                    }
                </script>
                <script type="text/javascript">
                    function chrtClock(ttldr, drmt, drsn, fwdys) {
                        function getNow() {
                            var now = new Date();
                            return {
                                hours: now.getHours() + now.getMinutes() / 60,
                                minutes: now.getMinutes() * 12 / 60 + now.getSeconds() * 12 / 3600,
                                seconds: now.getSeconds() * 12 / 60
                            };
                        }

                        function pad(number, length) {
                            return new Array((length || 2) + 1 - String(number).length).join(0) + number;
                        }

                        var now = getNow();

                        var chart = {
                            type: 'gauge',
                            plotBackgroundColor: null,
                            plotBackgroundImage: null,
                            plotBorderWidth: 0,
                            plotShadow: false,
                            height: 250,
                        };
                        var credits = {
                            enabled: false
                        };

                        var title = {
                            text: ''
                        };

                        var exporting = {
                            enabled: false
                        };

                        var pane = {
                            startAngle: -150,
                            endAngle: 150,
                            background: null,
                        };

                        // the value axis
                        var yAxis = {
                            min: 0,
                            max: 400,
                            minorTickInterval: 'auto',
                            minorTickWidth: 0,
                            minorTickLength: 10,
                            minorTickPosition: 'inside',
                            minorTickColor: '#000',

                            tickPixelInterval: 30,
                            tickWidth: 2,
                            tickPosition: 'inside',
                            tickLength: 10,
                            tickColor: '#fff',
                            labels: {
                                step: 2,
                                rotation: 'auto'
                            },
                            plotBands: [{
                                from: 0,
                                to: 400,
                                color: '#29AEFF',
                            }]
                        };

                        var tooltip = {
                            enabled: true,
                            pointFormat: '<b>{point.y}</b><br/>'
                            //pointFormat: '{series.name}: <b>{point.y}</b><br/>'
                        };
                        var series = [{
                            data: [{
                                name: 'Total Drs',
                                y: ttldr,
                                dial: {
                                    radius: '60%',
                                    baseWidth: 6,
                                    baseLength: '95%',
                                    rearLength: 0,
                                    backgroundColor: 'red'
                                }
                            }, {
                                name: 'Drs Met',
                                y: drmt,
                                dial: {
                                    baseLength: '95%',
                                    baseWidth: 3,
                                    rearLength: 0,
                                    backgroundColor: 'green'
                                }
                            }, {
                                name: 'Drs Seen',
                                y: drsn,
                                dial: {
                                    radius: '100%',
                                    baseWidth: 1,
                                    rearLength: '20%',
                                    backgroundColor: 'blue'
                                }
                            }],
                            //animation: false,
                            dataLabels: {
                                formatter: function () {
                                    return '<span style="color:red">Ttl Drs: ' + ttldr + '</span><br/>' +
                                            '<span style="color:green">Drs Met: ' + drmt + '</span><br/>' +
                                            '<span style="color:blue">Drs Seen: ' + drsn + '</span><br/>' +
                                            '<span style="color:black">FW Days: ' + fwdys + '</span>';
                                }
                            }
                        }];

                        $("#lblChrtClk").html('<span style="color:red;font-size: 12px;font-weight: bold;width: 75%;position: absolute;text-align: center;">Totol Doctors: ' + ttldr + '</span><br />' +
                                            '<span style="color:green;font-size: 12px;font-weight: bold;">Doctors Met: ' + drmt + '</span> &nbsp&nbsp&nbsp&nbsp&nbsp' +
                                            '<span style="color:blue;font-size: 12px;font-weight: bold;">Doctors Seen: ' + drsn + '</span>');
                        var json = {};
                        json.chart = chart;
                        json.credits = credits;
                        json.title = title;
                        json.pane = pane;
                        json.yAxis = yAxis;
                        json.tooltip = tooltip;
                        json.series = series;
                        json.exporting = exporting;

                        $('#divChrtClk').highcharts(json);
                    }

                    // Extend jQuery with some easing (copied from jQuery UI)
                    $.extend($.easing, {
                        easeOutElastic: function (x, t, b, c, d) {
                            var s = 1.70158; var p = 0; var a = c;
                            if (t == 0) return b;
                            if ((t /= d) == 1) return b + c;
                            if (!p) p = d * .3;
                            if (a < Math.abs(c)) { a = c; var s = p / 4; }
                            else
                                var s = p / (2 * Math.PI) * Math.asin(c / a);
                            return a * Math.pow(2, -10 * t) * Math.sin((t * d - s) * (2 * Math.PI) / p) + c + b;
                        }
                    });
                </script>
                <script type="text/javascript">
                    function draw(datas) {
                        var colors = ['#84EAF1', '#34D1BF', '#6610F2', '#0496FF', '#91D260', '#FDCA40'];
                        var chartdata = {
                            chart: {
                                renderTo: 'divChrtClmn',
                                type: 'column',
                                height: 300
                            },
                            title: {
                                text: ''
                            },
                            subtitle: {
                                //text: ''
                            },
                            xAxis: {
                                categories: [$("#ddlChrtMn").find("option:selected").text()]
                            },
                            yAxis: {
                                title: {
                                    text: ''
                                },
                                labels: {
                                    formatter: function () {
                                        return this.value;
                                    }
                                }
                            },
                            series: datas,
                            colors: colors,
                            plotOptions: {
                                column: {
                                    dataLabels: {
                                        enabled: false,
                                        style: {
                                            fontSize: '10px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }/*,
                                    rotation: -90
                                    y: 10*/
                                    }
                                }
                            }
                        };
                        var chart = new Highcharts.Chart(chartdata);
                    }
                </script>
                <script type="text/javascript">
                    function product() {
                        $(document).ready(function () {
                            var pData = [];
                            pData[0] = $("#ddlChrtMn").val();
                            pData[1] = $("#ddlChrtYr").val();

                            var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
                            var function_name = "getChartProduct";
                            $.ajax({
                                type: "POST",
                                url: "Default_MR.aspx/" + function_name,
                                data: jsonData,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: OnSuccess_,
                                error: OnErrorCall_
                            });

                            var arr_prd = [], arr_ttl = [], arr_sSub = [], arr_mt = [];
                            function OnSuccess_(response) {
                                var aData = response.d;
                                var iMin = 0, iMax = 0, product, drlist, drmet, prd_id;

                                $.map(aData, function (item, index) {
                                    prd_id = item.prd_Code; product = item.prd_Name; drlist = item.Ttl_Drs; drmet = item.Ttl_Mt;
                                    if (iMin === 0) {
                                        iMin++;
                                    }
                                    iMax++;
                                    var obj = {};
                                    obj.data = [item.Ttl_Drs];
                                    arr_ttl.push(item.Ttl_Drs);
                                    var obj = {};
                                    obj.data = [item.Ttl_Mt];
                                    arr_mt.push(item.Ttl_Mt);
                                    var obj = {};
                                    obj.prd = [item.prd_Name];
                                    arr_prd.push(item.prd_Name);
                                    var obj = {};
                                    obj.prd = [item.prd_Name];
                                    obj.ttl = [item.Ttl_Drs];
                                    obj.mt = [item.Ttl_Mt];
                                    arr_sSub.push(obj);
                                });
                                //alert(iMin + ':' + iMax);
                                //var myJsonString = JSON.stringify(arr_main);
                                //var jsonArray = JSON.parse(JSON.stringify(arr_main));                        
                                drawchart(arr_prd, arr_ttl, arr_mt, arr_sSub);
                            }

                            function OnErrorCall_(response) {
                                //alert("Error: No Data Found!");
                            }
                            //
                            function drawchart(arr_prd, arr_ttl, arr_mt, arr_sSub) {
                                Highcharts.setOptions({
                                    global: {
                                        useUTC: false
                                    }
                                });
                                var arr_prd_tmp = $.extend(true, [], arr_prd);
                                arr_prd_tmp.unshift(' ');
                                $('#divChrClmn3d').highcharts({
                                    chart: {
                                        type: 'spline',
                                        backgroundColor: 'rgba(0,0,0,0)',
                                        animation: Highcharts.svg,
                                        marginRight: 10,
                                        height: 300,
                                        events: {
                                            load: function () {
                                                var series = this.series[0];
                                                var secondseries = this.series[1];
                                                var categories = this.xAxis[0];
                                                var inx = 0, x, y, y1, yinfo, indx = 1;
                                                var category = $.extend(true, [], arr_prd);

                                                setInterval(function () {
                                                    $.map(arr_sSub, function (value, index) {
                                                        if (index === inx) {
                                                            x = indx;
                                                            y = parseInt(value.ttl);
                                                            yinfo = value.prd;
                                                            y1 = parseInt(value.mt);
                                                        }
                                                    });

                                                    series.addPoint({ x: x, y: y, yinfo: yinfo }, false, true);
                                                    secondseries.addPoint({ x: x, y: y1, yinfo: yinfo }, true, true);
                                                    inx++; indx++;
                                                    if (inx > arr_prd.length) {
                                                        inx = 0;
                                                        drawchart(arr_prd, arr_ttl, arr_mt, arr_sSub);
                                                    }
                                                }, 3000);
                                            }
                                        }
                                    },
                                    title: {
                                        text: ''
                                    },
                                    xAxis: {
                                        type: 'datetime',
                                        categories: arr_prd_tmp,
                                        tickPixelInterval: 1
                                    },
                                    yAxis: {
                                        title: {
                                            text: 'Listed Doctors'
                                        },
                                        plotLines: [{
                                            value: 0,
                                            width: 2,
                                            color: '#808080'
                                        }]
                                    },
                                    tooltip: {
                                        crosshairs: true,
                                        //shared: true,
                                        formatter: function () {
                                            return '<b>Product: ' + this.point.yinfo + '</b><br/>' +
                                                Highcharts.dateFormat(this.x) + '<br/>' + this.series.name + '<br/>' + this.y;
                                        }
                                    },
                                    plotOptions: {
                                        series: {
                                            marker: {
                                                enabled: true
                                            },
                                            dataLabels: {
                                                formatter: function () { return this.y; },
                                                enabled: true,
                                                align: 'top',
                                                //color: '#294469',
                                                shadow: false,
                                                x: -10,
                                                style: { "fontSize": "10px", "textShadow": "0px" }
                                            },
                                            pointPadding: 0.1,
                                            groupPadding: 0
                                        }
                                    },
                                    legend: {
                                        enabled: true
                                    },
                                    exporting: {
                                        enabled: false
                                    },
                                    series: [{
                                        name: 'Doctors Tagged',
                                        color: 'red',
                                        data: (function () {
                                            // generate an array of random data                                
                                            var data = [], i, j = 1;
                                            for (i = 0; i < 5; i++) {
                                                data.push({
                                                    x: j,
                                                    y: parseInt(arr_ttl[i], 10),
                                                    yinfo: arr_prd[i]
                                                });
                                                j++;
                                            }
                                            return data;
                                        })()
                                    }, {
                                        name: 'Doctors Promoted',
                                        color: '#0496FF',
                                        data: (function () {
                                            // generate an array of random data                                    
                                            var data = [], i, j = 1;
                                            for (i = 0; i < 5; i++) {
                                                data.push({
                                                    x: j,
                                                    y: parseInt(arr_mt[i], 10),
                                                    yinfo: arr_prd[i]
                                                });
                                                j++;
                                            }
                                            return data;
                                        })()
                                    }]
                                });
                            }
                        });
                    }
                </script>
            </div>

            <div class="loading" align="center">
                Loading. Please wait.enter">
                Loading. Please wait.<br />
                <br />
                <img src="Images/loader.gif" alt="" /> </div></div></form><%-- <div class="loader" runat="server" id="divLoading">
	    <div class="loader-centered">
		    <div class="object square-one"></div>
		    <div class="object square-two"></div>
		    <div class="object square-three"></div>
	    </div>
    </div>--%></body></html>