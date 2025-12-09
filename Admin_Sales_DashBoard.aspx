<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Sales_DashBoard.aspx.cs" Inherits="Admin_Sales_DashBoard" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menumas" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
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

        .chart-section-main-body {
            margin-top: 0px !important;
        }

        .view-list ul li:first-child::before {
            background-color: #84EAF1 !important;
            width: 12px;
            height: 12px;
            border-radius: 0% !important;
        }

        .view-list ul li:nth-child(2)::before {
            background-color: #34D1BF !important;
            width: 12px;
            height: 12px;
            border-radius: 0% !important;
        }

        .view-list ul li:nth-child(3)::before {
            background-color: #6610F2 !important;
            width: 12px;
            height: 12px;
            border-radius: 0% !important;
        }

        .view-list ul li:nth-child(4)::before {
            background-color: #FDCA40 !important;
            width: 12px;
            height: 12px;
            border-radius: 0% !important;
        }

        .view-list ul li {
            font-size: 12px !important;
        }

        .visit-one {
            font-size: 12px !important;
        }

        .visit-two {
            font-size: 12px !important;
        }

        .visit-three {
            font-size: 12px !important;
        }

        .visit-four {
            font-size: 12px !important;
        }

        .chartCont {
            padding: 0px 12px;
        }

        .border-bottom {
            border-bottom: 1px dashed rgba(0, 117, 194, 0.2);
        }

        .border-right {
            border-right: 1px dashed rgba(0, 117, 194, 0.2);
        }

        #container {
            width: 1200px;
            margin: 0 auto;
            position: relative;
        }

            #container > div {
                width: 100%;
                background-color: #ffffff;
            }

        #logoContainer {
            float: left;
        }

            #logoContainer img {
                padding: 0 10px;
            }

            #logoContainer div {
                position: absolute;
                top: 8px;
                left: 95px;
            }

                #logoContainer div h2 {
                    color: #0075c2;
                }

                #logoContainer div h4 {
                    color: #0e948c;
                }

                #logoContainer div p {
                    color: #719146;
                    font-size: 12px;
                    padding: 5px 0;
                }

        #userDetail {
            float: right;
        }

            #userDetail img {
                position: absolute;
                top: 16px;
                right: 130px;
            }

            #userDetail div {
                position: absolute;
                top: 15px;
                right: 20px;
                font-size: 14px;
                font-weight: bold;
                color: #0075c2;
            }

                #userDetail div p {
                    margin: 0;
                }

                    #userDetail div p:nth-child(2) {
                        color: #0e948c;
                    }

        #header div:nth-child(3) {
            clear: both;
            border-bottom: 1px solid #0075c2;
        }

        #content div {
            display: inline-block;
        }

        #content > div {
            margin: 0px 20px;
        }

            #content > div:nth-child(1) > div {
                margin: 20px 0 0;
            }

            #content > div:nth-child(2) > div {
                margin: 0 0 20px;
            }

        #footer p {
            margin: 0;
            font-size: 9pt;
            color: black;
            padding: 5px 0;
            text-align: center;
        }
    </style>
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

        /*div {
            padding: 20px;
        }*/

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
    </style>
    <style type="text/css">
        .boxshadow {
            -moz-box-shadow: 3px 3px 5px #535353;
            -webkit-box-shadow: 3px 3px 5px #535353;
            box-shadow: 3px 3px 5px #535353;
        }

        .roundbox {
            -moz-border-radius: 6px 6px 6px 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px 6px 6px 6px;
        }

        .grd {
            border: 1;
            border-color: Black;
        }

        .roundbox-top {
            -moz-border-radius: 6px 6px 0 0;
            -webkit-border-radius: 6px 6px 0 0;
            border-radius: 6px 6px 0 0;
        }

        .roundbox-bottom {
            -moz-border-radius: 0 0 6px 6px;
            -webkit-border-radius: 0 0 6px 6px;
            border-radius: 0 0 6px 6px;
        }

        .gridheader, .gridheaderbig, .gridheaderleft, .gridheaderright {
            padding: 6px 6px 6px 6px;
            background: #F7DCB4 url(images/vertgradient.png) repeat-x;
            text-align: center;
            font-weight: bold;
            text-decoration: none;
            color: Blue;
        }

        .gridheaderleft {
            text-align: left;
        }

        .gridheaderright {
            text-align: right;
        }

        .gridheaderbig {
            font-size: 135%;
        }
    </style>
    <style type="text/css">
        .tableStyle14 {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 300px;
            height: 150px;
        }
            /*Style for Table Head - TH*/
            .tableStyle14 th {
                text-align: left;
                background-color: #9C6B98;
                color: #fff;
                text-align: left;
                padding: 20px;
                font-size: 14px;
                font-weight: bold;
            }
            /*TD and TH Style*/
            .tableStyle14 td, .tableStyle14 th {
                border: 1px solid #dedede; /*Border color*/
                padding: 10px;
            }
                /*Table Even Columns Styles*/
                .tableStyle14 td:nth-child(even) {
                    background-color: #afafaf;
                }
                /*Table ODD Columns Styles*/
                .tableStyle14 td:nth-child(odd) {
                    background-color: #cfcfcf;
                }
                /*Table Column(Data) HOver Style*/
                .tableStyle14 td:hover {
                    background-color: #e5423f;
                }

        .nice-select {
            color: #90a1ac;
            font-size: 14px;
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
        }
    </style>




    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script>
    <script src="DashBoard/JS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/fusioncharts.js"></script>
<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/themes/fusioncharts.theme.fint.js?cacheBust=56"></script>--%>
    <script src="DashBoard/js1/fusioncharts.js" type="text/javascript"></script>
    <script src="DashBoard/js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var ddlmode = $("#ddlmode").val();
            if (ddlmode == "0") {
                var Hide = document.getElementById("Target");

                Hide.style.display = "none";
                Primary();
                Secondary();
            }
            else if (ddlmode == "1") {
                var Hide2 = document.getElementById("Primary");

                Hide2.style.display = "none";
                Target_Cum();
                Target_Cum_Sec();
            }
        });
    </script>
    <script type="text/javascript">

        function Target_Cum() {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
            var Month = $("#ddlFMonth").val();
            var TMonth = $("#ddlTMonth").val();
            var Year = $("#ddlFYear").val();
            var TYear = $("#ddlTYear").val();
            var Field = $("#ddlFieldForce").val();

            var mode = $("#ddlmode").val();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear;
            $.ajax({

                type: 'POST',

                url: "Admin_Sales_DashBoard.aspx/Target_Cum",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({

                        "type": "scrollcombidy2d",
                        "renderAt": "chart-containert",
                        "width": "500",

                        "dataFormat": "json",
                        "dataSource": chartData
                    }

            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-containert").html(xhr.responseText);

                }

            });
        }


    </script>
    <script type="text/javascript">

        function Target_Cum_Sec() {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
            var Month = $("#ddlFMonth").val();
            var TMonth = $("#ddlTMonth").val();
            var Year = $("#ddlFYear").val();
            var TYear = $("#ddlTYear").val();
            var Field = $("#ddlFieldForce").val();

            var mode = $("#ddlmode").val();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear;
            $.ajax({

                type: 'POST',

                url: "Admin_Sales_DashBoard.aspx/Target_Cum_Sec",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({

                        "type": "scrollcombidy2d",
                        "renderAt": "chart-containers",
                        "width": "500",

                        "dataFormat": "json",
                        "dataSource": chartData
                    }

            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-containers").html(xhr.responseText);

                }

            });
        }


    </script>
    <script type="text/javascript">

        function Primary() {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
            var Month = $("#ddlFMonth").val();
            var TMonth = $("#ddlTMonth").val();
            var Year = $("#ddlFYear").val();
            var TYear = $("#ddlTYear").val();
            var Field = $("#ddlFieldForce").val();

            var mode = $("#ddlmode").val();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear;
            $.ajax({

                type: 'POST',

                url: "Admin_Sales_DashBoard.aspx/Primary",

                contentType: "application/json; charset=utf-8",

                dataType: "json",

                data: '{objData:' + JSON.stringify(Data) + '}',
                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({

                        type: 'pie2d',

                        renderAt: 'chart-container',

                        width: '500',



                        dataFormat: 'json',

                        dataSource: {

                            "chart": {
                                "caption": "",
                                "formatnumberscale": "0",
                                "showBorder": "0",
                                "showLegend": "1",
                                "theme": "fint",
                                "showPercentValues": "1",
                                "showPercentInToolTip": "0",
                                //Setting legend to appear on right side
                                "paletteColors": "#007bff,#71e3e0,#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                "legendPosition": "bottom",
                                //Caption for legend

                                //Customization for legend scroll bar cosmetics
                                "legendScrollBgColor": "#cccccc",
                                "legendScrollBarColor": "#999999"

                            },


                            "data": chartData

                        }

                    }

                );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-container").html(xhr.responseText);

                }

            });
        }
    </script>
    <script type="text/javascript">

        function Secondary() {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
            var Month = $("#ddlFMonth").val();
            var TMonth = $("#ddlTMonth").val();
            var Year = $("#ddlFYear").val();
            var TYear = $("#ddlTYear").val();
            var Field = $("#ddlFieldForce").val();

            var mode = $("#ddlmode").val();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear;
            $.ajax({

                type: 'POST',

                url: "Admin_Sales_DashBoard.aspx/Secondary",

                contentType: "application/json; charset=utf-8",

                dataType: "json",

                data: '{objData:' + JSON.stringify(Data) + '}',
                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({

                        type: 'pie2d',

                        renderAt: 'chart-container2',

                        width: '500',



                        dataFormat: 'json',

                        dataSource: {

                            "chart": {
                                "caption": "",
                                "formatnumberscale": "0",
                                "showBorder": "0",
                                "showLegend": "1",
                                "theme": "fint",
                                "showPercentValues": "1",
                                "showPercentInToolTip": "0",
                                //Setting legend to appear on right side
                                "paletteColors": "#007bff,#71e3e0,#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                "legendPosition": "bottom",
                                //Caption for legend
                                // "legendCaption": "Product: ",

                                //Customization for legend scroll bar cosmetics
                                "legendScrollBgColor": "#cccccc",
                                "legendScrollBarColor": "#999999"

                            },


                            "data": chartData

                        }

                    }

                );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-container2").html(xhr.responseText);

                }

            });
        }
    </script>
    <link href="assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0px;">
            <ucl:Menumas ID="menumas" runat="server" />
        </div>
        <asp:Panel ID="pnlchart" runat="server" Style="vertical-align: top">
            <div class="charts-area clearfix">
                <div class="container home-section-main-body">

                    <div class="row clearfix" style="align-items: center; text-align: center;">
                        <div class="col-lg-12">
                            <div class="row clearfix">
                                <div class="col-lg-10" style="text-align: center; padding-top: 10px;">
                                    <h2 class="text-center" style="color: #292a34; font-size: 24px; font-weight: 700;">
                                        <asp:Label Text="Sales Dashboard" ForeColor="Black" ID="Label1" runat="server" />
                                    </h2>

                                </div>
                                <div class="col-lg-2">
                                    <div style="float: left; width: 30%;margin-top:17px;">
                                        <asp:Label ID="lblMode" runat="server" Text="Mode"></asp:Label>
                                    </div>
                                    <div style="float: right; width: 70%">
                                        <div class="search-area clearfix" style="margin-top: -50px; margin-bottom: -20px;">
                                            <div class="single-option">
                                                <asp:DropDownList ID="ddlDBMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDBMode_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="SFE"></asp:ListItem>
                                                  <%--  <asp:ListItem Value="1" Text="Sales" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Primary vs Secondary"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>


                    </div>
                    <div id="chrt" runat="server">
                        <div class="row clearfix" style="align-items: center;">
                            <%--text-align: center;--%>
                            <div class="col-lg-12" style="margin-top: -60px; margin-bottom: -30px;">
                                <div class="search-area clearfix">
                                    <div class="row justify-content-center">
                                        <div class="col-lg-3">

                                            <div class="designation-area clearfix">
                                                <div class="single-des clearfix">
                                                    <asp:Label ID="lblSF" runat="server" Text="FieldForce Name"></asp:Label>
                                                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="designation-area clearfix">
                                                <div class="single-des clearfix">

                                                    <div style="float: left; width: 50%">
                                                        <asp:Label Text="From Mnth/Yr" ID="lblChrtMnth" runat="server" />
                                                        <asp:DropDownList ID="ddlFMonth" runat="server">
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
                                                    <div style="float: right; width: 50%; padding-top: 28px;">
                                                        <asp:DropDownList runat="server" CssClass="valign" ID="ddlFYear">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="designation-area clearfix">
                                                <div class="single-des clearfix">

                                                    <div style="float: left; width: 50%">
                                                        <asp:Label Text="To Mnth/Yr" ID="lblyear" runat="server" />
                                                        <asp:DropDownList ID="ddlTMonth" runat="server">
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
                                                    <div style="float: right; width: 50%; padding-top: 28px;">
                                                        <asp:DropDownList runat="server" CssClass="valign" ID="ddlTYear">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="designation-area clearfix">
                                                <div class="single-des clearfix">

                                                    <%-- <div style="float: left; width: 50%">--%>
                                                    <asp:Label Text="Mode" ID="Label2" runat="server" />
                                                    <asp:DropDownList ID="ddlmode" runat="server">
                                                        <asp:ListItem Value="0" Text="Product Sales"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Target Vs Sales"></asp:ListItem>

                                                    </asp:DropDownList>
                                                    <%-- </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-1" style="padding-top: 28px;">
                                            <asp:Button Text="View" CssClass="savebutton" Width="60px"
                                                ID="btnViewChart" runat="server" />
                                        </div>
                                    </div>
                                    <%--  <div class="row clearfix">
                                        <div class="col-lg-5">
                                            <div class="row clearfix">
                                                <div class="col-lg-5">
                                                    <asp:Label ID="lblSF" runat="server" Text="FieldForce Name"></asp:Label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="single-option">
                                                        <asp:DropDownList ID="ddlFieldForce" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="row clearfix">
                                                <div class="col-lg-3">
                                                    <asp:Label Text="From Mnth/Yr" ID="lblChrtMnth" runat="server" />
                                                </div>
                                                <div class="col-lg-2 p-0">
                                                   
                                                        <asp:DropDownList ID="ddlFMonth" runat="server">
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
                                                       <div class="col-lg-2 p-0">
                                                         <asp:DropDownList runat="server" CssClass="valign" ID="ddlFYear">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="row clearfix">
                                                <div class="col-lg-3">
                                                    <asp:Label Text="To Mnth/Yr" ID="lblyear" runat="server" />
                                                </div>
                                                <div class="col-lg-2 p-0">
                                                    
                                                         <asp:DropDownList ID="ddlTMonth" runat="server">
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
                                                        <div class="col-lg-2 p-0">
                                                        <asp:DropDownList runat="server" CssClass="valign" ID="ddlTYear">
                                                        </asp:DropDownList>
                                                            </div>
                                                  
                                              
                                            </div>
                                        </div>
                                    
                                        <div class="col-lg-1">
                                            <asp:Button Text="View" CssClass="savebutton" Width="60px"
                                                ID="btnViewChart" runat="server" />
                                        </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="chrt1" runat="server">
                        <div class="col-lg-12" id="Primary">
                            <div class="row justify-content-center clearfix">
                                <div class="col-lg-6">
                                    <div class="single-chart single-top-chat-min-height">
                                        <h3>Top 10 Product Sales in Primary</h3>
                                        <div id="chart-container">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="single-chart single-top-chat-min-height">
                                        <h3>Top 10 Product Sales in Secondary</h3>
                                        <div id="chart-container2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12" id="Target">
                            <div class="row justify-content-center clearfix">
                                <div class="col-lg-6">
                                    <div class="single-chart single-top-chat-min-height">
                                        <h3>Target Vs Sales Vs Ach (Primary)</h3>
                                        <div id="chart-containert">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="single-chart single-top-chat-min-height">
                                        <h3>Target Vs Sales Vs Ach (Secondary)</h3>
                                        <div id="chart-containers">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </asp:Panel>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
