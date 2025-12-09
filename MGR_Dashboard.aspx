<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MGR_Dashboard.aspx.cs" Inherits="MGR_Dashboard" %>

<%@ Register Src="~/UserControl/MGR_TP_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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

        .chartCont {
            padding: 0px;
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
            border: 0px solid black;
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
            border-collapse: collapse;
            width: 280px;
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
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script>
    <script src="DashBoard/JS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="DashBoard/js1/fusioncharts.js" type="text/javascript"></script>
    <script src="DashBoard/js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>



    <script type="text/javascript">
        function TabletsIANs() {
            window.open('http://www.tors.torssfa.info/Tabletiancorner/home.asp', null, '');
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
                window.open('http://www.tors.torssfa.info/index.asp', null, '');
            }
            else if (div_code == '3') {
                window.open('http://www.vibranz.torssfa.info/index.asp', null, '');
            }
            else if (div_code == '4') {
                window.open('http://www.tabzen.torssfa.info/index.asp', null, '');
            }
            else if (div_code == '5') {
                window.open('http://www.parazen.torssfa.info/index.asp', null, '');
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
        $(document).ready(function () {
            Pie();
        });
    </script>
    <script type="text/javascript">

        function Pie() {

            var Month = $("#ddlFMonth").val();
            var Year = $("#ddlFYear").val();

            var Field = $("#ddlFieldForce").val();

            var mode = 0;
            var type = 1;
            var modeName = "";


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();

            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();

            var Data = Month + "^" + Year + "^" + Field + "^" + SsfName + "^" + FMName;
            $.ajax({
                type: 'POST',
                url: "MGR_Dashboard.aspx/SingleFW",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',
                success: function (data) {
                    var chartData = eval("(" + data.d + ')');
                    var fusioncharts = new FusionCharts({
                        type: 'doughnut2d',
                        renderAt: 'chart-container',
                        width: "300",
                        dataFormat: 'json',
                        dataSource: {
                            "chart": {
                                "caption": "",
                                "formatnumberscale": "0",
                                "showLegend": "1",
                                "showValues": "0",
                                "showLabels": "0",
                                "theme": "fint",
                                "showPercentValues": "0",
                                "paletteColors": "#007bff,#71e3e0,#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                "legendScrollBgColor": "#cccccc",
                                "legendScrollBarColor": "#999999",
                            },
                            "data": chartData
                        }
                    });
                    fusioncharts.render();
                },
                error: function (xhr, ErrorText, thrownError) {
                }
            });

            $.ajax({
                type: 'POST',
                url: "MGR_Dashboard.aspx/SingleCV",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',
                success: function (data) {
                    var chartData = eval("(" + data.d + ')');
                    var fusioncharts = new FusionCharts({
                        "type": "Column2d",
                        "renderAt": "chart-container2",
                        "width": "300",
                        "dataFormat": "json",
                        "dataSource": {
                            "chart": {
                                "caption": "",
                                "xaxisname": "Designation",
                                "yaxisname": "",
                                "showLegend": "1",
                                "showValues": "0",
                                "palette": "1",
                                "formatNumber": "0",
                                "formatNumberScale": "0",
                                "theme": "fint",
                                "maxColWidth": "20",
                                "useRoundEdges": '0',
                                "paletteColors": "#007bff,#71e3e0,#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                            },
                            "categories": [{
                                "category": chartData
                            }],
                            "dataset": [{
                                "data": chartData
                            }]
                        }
                    });
                    fusioncharts.render();
                },
                error: function (xhr, ErrorText, thrownError) {
                }
            });

            $.ajax({
                type: 'POST',
                url: "MGR_Dashboard.aspx/Visit",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',
                success: function (data) {
                    var chartData = eval("(" + data.d + ')');
                    var fusioncharts = new FusionCharts({
                        "type": "bar2d",
                        "renderAt": "chart-container3",
                        "width": "300",
                        "dataFormat": "json",
                        "dataSource": {
                            "chart": {
                                "caption": "",
                                "subcaption": "",
                                "xaxisname": "Category",
                                "yaxisname": "Percentage(%)",
                                "showLegend": "1",
                                "showValues": "0",
                                "palette": "1",
                                "formatNumber": "0",
                                "formatNumberScale": "0",
                                "theme": "fint",
                                "maxBarHeight": "20",
                                "paletteColors": "#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                            },
                            "categories": [{
                                "category": chartData
                            }],
                            "dataset": [{
                                "data": chartData
                            }]
                        }
                    });
                    fusioncharts.render();
                },
                error: function (xhr, ErrorText, thrownError) {
                }
            });

            $.ajax({
                type: 'POST',
                url: "MGR_Dashboard.aspx/SingleCall",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',
                success: function (data) {
                    var chartData = eval("(" + data.d + ')');
                    var fusioncharts = new FusionCharts({
                        "type": "line",
                        "renderAt": "chart-container4",
                        "width": "300",
                        "dataFormat": "json",
                        "dataSource": {
                            "chart": {
                                "caption": "",
                                "subCaption": "",
                                "yAxisName": "Days",
                                "showValues": "1",
                                "numberPrefix": "",
                                "showLegend": "1",
                                "theme": "fint",
                                "bgColor": "#ffffff",
                                "paletteColors": "#0496FF,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                "showAlternateHGridColor": "0",
                                "flatScrollBars": "1",
                                "scrollheight": "10",
                                "numVisiblePlot": "12",
                                "showHoverEffect": "1",
                                "showLabels": "1",
                                "showLegend": "1",
                                "showValues": "1",
                                "showPercentValues": "0",
                                "showPercentInTooltip": "0",
                                "plotTooltext": "Detail : $label<br>Value : $value",
                                "labelDisplay": "rotate",
                                "formatNumber": "0",
                                "formatNumberScale": "0"
                            },
                            "categories": [{
                                "category": chartData
                            }],
                            "dataset": [{
                                "data": chartData
                            }]
                        }
                    });
                    fusioncharts.render();
                },
                error: function (xhr, ErrorText, thrownError) {
                }
            });

            $.ajax({
                type: 'POST',
                url: "MGR_Dashboard.aspx/Visit_Days",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',
                success: function (data) {
                    var chartData = eval("(" + data.d + ')');
                    var chartData = eval("(" + data.d + ')');
                    $("#visit_one").html(chartData[0].Value);
                    $("#visit_two").html(chartData[1].Value);
                    $("#visit_three").html(chartData[2].Value);
                    $("#visit_four").html(chartData[3].Value);
                }
            });

            var pData = [];
            pData[0] = $("#ddlFMonth").val();
            pData[1] = $("#ddlFYear").val();
            pData[2] = $("#ddlFieldForce").val();
            //  pData[3] = today.getDate();

            var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
            var function_name = "getChartVal";
            $.ajax({
                type: "POST",
                url: "MGR_Dashboard.aspx/" + function_name,
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: OnErrorCall_
            });

            function OnSuccess_(response) {
                var aData = response.d;
                var arr = [];
                var sf_code, val, met, seen;
                var sf_name = $("#ddlFieldForce").text();
                $.map(aData, function (item, index) {
                    sf_code = item.Sf_Code; val = item.Coverage; met = item.Met; seen = item.Seen;


                });
                var myJsonString = JSON.stringify(arr);
                var jsonArray = JSON.parse(JSON.stringify(arr));

                document.getElementById("lblMet").textContent = met
                document.getElementById("lblSeen").textContent = seen
                document.getElementById("lblcall").textContent = val
            }
            function OnErrorCall_(response) {
                alert("Error: No Data Found!");
            }
            e.preventDefault();
            //            var pData = [];


            //            pData[0] = $("#ddlFMonth").val();
            //            pData[1] = $("#ddlFYear").val();
            //            pData[2] = $("#ddlFieldForce").val();
            //            //  pData[3] = today.getDate();

            //            var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
            //            var function_name = "getChartVal";
            //            $.ajax({
            //                type: "POST",
            //                url: "MGR_Dashboard.aspx/" + function_name,
            //                data: jsonData,
            //                contentType: "application/json; charset=utf-8",
            //                dataType: "json",
            //                success: OnSuccess_,
            //                error: OnErrorCall_
            //            });

            //            function OnSuccess_(response) {
            //                var aData = response.d;
            //                var arr = [];
            //                var sf_code, val, met, seen;
            //                var sf_name = $("#ddlFieldForce").text();
            //                $.map(aData, function (item, index) {
            //                    sf_code = item.Sf_Code; val = item.Coverage; met = item.Met; seen = item.Seen;


            //                });
            //                var myJsonString = JSON.stringify(arr);
            //                var jsonArray = JSON.parse(JSON.stringify(arr));

            //                meter(val);
            //                chrtClock(met, seen);
            //            }
            //            function OnErrorCall_(response) {
            //                alert("Error: No Data Found!");
            //            }
            //            e.preventDefault();

        }
    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
        });
    </script>





</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0px;">
            <ucl:Menu ID="menu1" runat="server" />
        </div>
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
                                                 <asp:TableCell Width="20%" HorizontalAlign="Left">
                                                <a id="btnTabletians" href="#">
                                                    <img src="assets/images/TABLETIAN_Corner.jpg" alt="" />
                                                </a>
                                                </asp:TableCell>

                                                <asp:TableCell HorizontalAlign="Center" Width="60%" VerticalAlign="Top">
                                                    <asp:Label Text="Dashboard" ID="lblHeadTxt" ForeColor="Black" runat="server" />
                                                </asp:TableCell>
                                                <asp:TableCell Width="10%" HorizontalAlign="Right">
                                                </asp:TableCell>
                                                <asp:TableCell Width="10%" HorizontalAlign="Center" VerticalAlign="Top">
                                                    <a id="btnTabletsOldTors" href="#">
                                                        <asp:Label Text="Old Tors" ID="Label6" runat="server" Style="font-size: 16px;" />
                                                    </a>
                                                </asp:TableCell>
                                               
                                            </asp:TableRow>
                                        </asp:Table>
                                    </h2>
                                    <asp:LinkButton ID="btn_shrtcut" Text="Shortcut Menus" CssClass="buttonlabel label" runat="server" OnClick="btn_shrtcut_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="chrt" runat="server">
                        <div class="row clearfix" style="align-items: center; text-align: center;">
                            <div class="col-lg-12" style="margin-top: -60px; margin-bottom: -30px;">
                                <div class="search-area clearfix">
                                    <div class="row clearfix">
                                        <div class="col-lg-5">
                                            <div class="row clearfix">
                                                <div class="col-lg-5">
                                                    <asp:Label ID="lblSF" runat="server" Text="FieldForce Name"></asp:Label>
                                                </div>
                                                <div class="col-lg-7 p-0">
                                                    <div class="single-option">
                                                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="row">
                                                <div class="col-lg-4">
                                                    <asp:Label Text="Month" ID="lblChrtMnth" runat="server" />
                                                </div>
                                                <div class="col-lg-8 p-0">
                                                    <div class="single-option">
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
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="row">
                                                <div class="col-lg-4">
                                                    <asp:Label Text="Year" ID="lblyear" runat="server" />
                                                </div>
                                                <div class="col-lg-8 p-0">
                                                    <div class="single-option">
                                                        <asp:DropDownList runat="server" CssClass="valign" ID="ddlFYear">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:Button Text="View" CssClass="savebutton" ID="btnViewChart" runat="server" Width="70" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="row justify-content-center clearfix">
                                <div class="col-lg-4">
                                    <div class="single-chart single-top-chat-min-height">
                                        <h3>Field Work Days</h3>
                                        <div class="pie-chat-list position-relative">
                                            <div id="chart-container"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="single-chart single-top-chat-min-height">
                                        <h3>Call Average</h3>
                                        <div class="call-average clearfix">
                                            <div id="chart-container2"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="single-chart single-top-chat-min-height">
                                        <h3>Call Adherence</h3>
                                        <div id="chart-container3" style="margin-top: 20px; margin-left: -10px;"></div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="single-chart single-top-chat-min-height">
                                        <h3>Self Details</h3>
                                        <div class='chartCont border-right' style="width: 280px; height: 150px; margin-top: 20px;">
                                            <table class="tableStyle14">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" ForeColor="#9D1309" runat="server"> Met</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMet" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" ForeColor="#9D1309" runat="server"> Seen</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSeen" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" ForeColor="#9D1309" runat="server"> Call Average</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblcall" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" ForeColor="#9D1309" runat="server"> Coverage</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCov" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" ForeColor="#9D1309" runat="server"> Joint Calls</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblJoint" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="single-chart single-top-chat-min-height">
                                        <h3>Self Work Days</h3>
                                        <div id="chart-container4"></div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="single-chart single-top-chat-min-height">
                                        <h3>Call Adherence</h3>
                                        <div id="chart-container5">
                                            <div class="view-chat position-relative text-right clearfix">
                                                <div class="visit-one" id="visit_one"></div>
                                                <div class="visit-two" id="visit_two"></div>
                                                <div class="visit-three" id="visit_three"></div>
                                                <div class="visit-four" id="visit_four"></div>
                                            </div>
                                            <div class="view-list clearfix">
                                                <ul>
                                                    <li>1 Visit</li>
                                                    <li>2 Visit</li>
                                                    <li>3 Visit</li>
                                                    <li>3+ Visit</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>
