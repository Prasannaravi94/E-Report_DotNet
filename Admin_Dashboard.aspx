<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Dashboard.aspx.cs"
    Inherits="Admin_Dashboard" EnableEventValidation="false" %>

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
    </style>




    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script>
    <script src="DashBoard/JS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/fusioncharts.js"></script>
<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/themes/fusioncharts.theme.fint.js?cacheBust=56"></script>--%>
    <script src="DashBoard/js1/fusioncharts.js" type="text/javascript"></script>
    <script src="DashBoard/js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            Pie();




        });
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript">

        var popUpObj;


        function showModalPopUp(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("Missed_Call_Zoom.aspx?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&sf_name=" + sf_name,
    "ModalPopUp"//,
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=800," +
    //"height=600," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            // LoadModalDiv();
            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s10.postimg.org/b9kmgkw55/triangle_square_animation.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:500px; height: 500px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });

        }

        function showModalPopUp1(sfcode, fmon, fyr, sf_name) {
            popUpObj = window.open("Visit_Call_Zoom.aspx?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&sf_name=" + sf_name, "ModalPopUp");
            popUpObj.focus();
            // LoadModalDiv();
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s10.postimg.org/b9kmgkw55/triangle_square_animation.gif"
                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:500px; height: 500px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');
            });
        }


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
                url: "Admin_Dashboard.aspx/SingleFW",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',
                success: function (data) {
                    var chartData = eval("(" + data.d + ')');
                    console.log(chartData);
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
                                "clickURL": "F-drill-Fw_Zoom.aspx?sf_code=" + Field + "&FMonth=" + Month + "&Fyear=" + Year + "&sf_name=" + SsfName + ""
                            },
                            "data": chartData
                        }
                    });
                    fusioncharts.render();
                },
                error: function (xhr, ErrorText, thrownError) {
                    //  alert("Error: No Data Found!");
                }
            });
            $.ajax({
                type: 'POST',
                url: "Admin_Dashboard.aspx/SingleCV",
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
                                "clickURL": "F-drill-CallAvg_Zoom.aspx?sf_code=" + Field + "&FMonth=" + Month + "&Fyear=" + Year + "&sf_name=" + SsfName + ""
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
                    //  alert("Error: No Data Found!");
                }
            });
            $.ajax({
                type: 'POST',
                url: "Admin_Dashboard.aspx/Visit",
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
                                "clickURL": "F-drill-Call_Adh_Zoom.aspx?sf_code=" + Field + "&FMonth=" + Month + "&Fyear=" + Year + "&sf_name=" + SsfName + ""
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
                    //     $("#chart-container3").html(xhr.responseText);
                    //  alert("Error: No Data Found!");
                }
            });
            $.ajax({

                type: 'POST',

                url: "Admin_Dashboard.aspx/Prod_Det",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');
                    var markers = {
                        size: 4,
                        opacity: 0.9,
                        colors: ["#289fff"],
                        strokeColor: "rgba(35, 165, 239, 0.1)",
                        strokeWidth: 10,
                        hover: {
                            size: 7,
                        }
                    };
                    var fusioncharts = new FusionCharts({

                        "type": "scrollline2d",
                        "renderAt": "chart-container4",
                        "width": "300",
                        "height": "300",
                        "dataFormat": "json",
                        "dataSource": {
                            "chart": {
                                "caption": "",
                                "subCaption": "",
                                //"xAxisName": "Products",
                                //"yAxisName": "No of Doctors",
                                "flatScrollBars": "1",
                                "scrollheight": "10",
                                "showValues": "0",
                                "numberPrefix": "",
                                "showLegend": "0",
                                "theme": "fint",
                                "bgColor": "#ffffff",
                                "markers": "markers",
                                "paletteColors": "#0496FF",
                                "scrollColor": "#007bff",
                                "showAlternateHGridColor": "0",
                                "lineThickness": "2",
                                "numVisiblePlot": "12",
                                "showPercentValues": "0",
                                "showPercentInTooltip": "0",
                                "plotTooltext": "Product : $label<br>Drs Cnt : $value",
                                "formatNumber": "0",
                                "formatNumberScale": "0",
                                "clickURL": "F-drill-Product_Detail_Zoom.aspx?sf_code=" + Field + "&FMonth=" + Month + "&Fyear=" + Year + "&sf_name=" + SsfName + ""
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
                    //  alert("Error: No Data Found!");
                }
            });
            $.ajax({
                type: 'POST',
                url: "Admin_Dashboard.aspx/Visit_Days",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',
                success: function (data) {
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
            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            //  pData[3] = today.getDate();
            var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
            var function_name = "getChartVal";
            $.ajax({
                type: "POST",
                url: "Admin_Dashboard.aspx/" + function_name,
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: OnErrorCall_
            });

            function OnSuccess_(response) {
                var aData = response.d;
                var arr = [];
                var totdrs, met, miss;

                $.map(aData, function (item, index) {
                    totdrs = item.totdrs; met = item.Met; miss = item.Miss;


                });
                var myJsonString = JSON.stringify(arr);
                var jsonArray = JSON.parse(JSON.stringify(arr));

                //  meter(val);

                $('#chart-container6').click(function () {
                    // window.open('Missed_Call_Zoom.aspx?sf_code=' + pData[2] + '&FMonth=' + pData[0] + '&Fyear=' + pData[1] + '&sf_name=' + SsfName + ','null', 'height=500, width=400,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no'');
                    showModalPopUp(pData[2], pData[0], pData[1], SsfName);
                });

                $('#chart-container5').click(function () {
                    showModalPopUp1(pData[2], pData[0], pData[1], SsfName);
                });

                chrtClock(totdrs, met, miss);
            }
            function OnErrorCall_(response) {
                //  alert("Error: No Data Found!");
            }
            e.preventDefault();

            $("#btnDownload").click(function () {
                Pie();
                fusioncharts.batchExport({
                    charts: [
              {
                  id: "chart-container"
              },
              {
                  id: "chart-container2"
              },
              {
                  id: "chart-container3"
              }
                    ],
                    exportFileName: "batchExport",
                    exportFormat: "jpg",
                    exportAtClientSide: "1"
                });
            });
        }
    </script>
    <script type="text/javascript">
        function OpenNewWindow_delay() {
            window.open('Delayed_Status_Multiple.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }
    </script>
    <script type="text/javascript" src="JScript/js/jspdf.min.js"></script>
    <script type="text/javascript">



        //function printPDF() {
        //    var doc = new jsPDF('p', 'pt', 'a4', true);
        //    var specialElementHandlers = {
        //        '#editor': function (element, renderer) {
        //            return true;
        //        }
        //    };
        //    debugger;
        //    console.log($('#chrt1').html());
        //    doc.fromHTML($('#chrt1').html(), 15, 15, {
        //        //'width': 170,
        //        'elementHandlers': specialElementHandlers
        //    });
        //    doc.save('sample-file.pdf');
        //}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0px;">
            <ucl:Menumas ID="menumas" runat="server" />
        </div>
        <%--<div style="margin: 0px;">
            <asp:Table ID="tbl5" runat="server">
                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableCell BorderWidth="0" CssClass="padding">
                        <ucl:Menumas ID="menumas" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>--%>

        <asp:Panel ID="pnlchart" runat="server" Style="vertical-align: top">
            <div class="charts-area clearfix">
                <div class="container chart-section-main-body">
                    <a href="MasterFiles/Options/Bulk_Deactivation_Dr_Chem.aspx" style="float: left; color: white;">.</a>
                    <div class="row clearfix" style="align-items: center; text-align: center;">
                        <div class="col-lg-12">
                            <div class="row clearfix">
                                <div class="col-lg-10" style="text-align: center; padding-top: 20px;">

                                    <h2 class="text-center" style="color: #292a34; font-size: 24px; font-weight: 700;">
                                        <asp:Label Text="Dashboard" ForeColor="Black" ID="Label1" runat="server" />
                                    </h2>

                                    <%-- <asp:LinkButton ID="btnDownload" ToolTip="PDF" runat="server" Style="margin-left: 700px; z-index: 2; position: sticky;">
                                        OnClick="btnDownload_Click
                                        <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>--%>
                                </div>
                                <div class="col-lg-2">
                                    <div style="float: left; width: 30%; margin-top: 17px;">
                                        <asp:Label ID="lblMode" runat="server" Text="Mode" CssClass="label"></asp:Label>
                                    </div>
                                    <div style="float: right; width: 70%">
                                        <div class="search-area clearfix" style="margin-top: -50px; margin-bottom: -20px;">
                                            <div class="single-option">
                                                <asp:DropDownList ID="ddlMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="SFE" Selected="True"></asp:ListItem>
                                                   <%-- <asp:ListItem Value="1" Text="Sales"></asp:ListItem>
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
                                                        <asp:DropDownList ID="ddlFieldForce" runat="server">
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
                                            <asp:Button Text="View" CssClass="savebutton" Width="60px"
                                                ID="btnViewChart" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="chrt1" runat="server">
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
                                            <h3>Missed Call</h3>
                                            <div id='chart-container6'>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="single-chart single-top-chat-min-height">
                                            <h3>Product Detailed Doctors</h3>
                                            <div id='chart-container4' style="margin-top: 25px; margin-left: -10px;"></div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="single-chart single-top-chat-min-height">
                                            <h3>Visit Calls (Team)</h3>
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
            </div>
        </asp:Panel>
        <a href="MasterFiles/Options/Bulk_Deactivation_Dr_Chem.aspx">.</a>
        <script type="text/javascript" src="https://code.highcharts.com/highcharts.js"></script>
        <script type="text/javascript" src="https://code.highcharts.com/highcharts-more.js"></script>
        <script type="text/javascript" src="https://code.highcharts.com/modules/solid-gauge.js"></script>
        <script type="text/javascript">
            function chrtClock(totdrs, met, miss) {
                function renderIcons() {
                    // Move icon
                    if (!this.series[0].icon) {
                        this.series[0].icon = this.renderer.path(['M', -4, 0, 'L', 4, 0, 'M', 0, -4, 'L', 4, 0, 0, 4])
                        .attr({
                            'stroke': '#ffffff',
                            'stroke-linecap': 'round',
                            'stroke-linejoin': 'round',
                            'stroke-width': 2,
                            'zIndex': 10
                        })
                        .add(this.series[2].group);
                    }
                    this.series[0].icon.translate(
                        this.chartWidth / 2 - 10,
                        this.plotHeight / 2 - this.series[0].points[0].shapeArgs.innerR -
                        (this.series[0].points[0].shapeArgs.r - this.series[0].points[0].shapeArgs.innerR) / 2
                    );

                    // Exercise icon
                    if (!this.series[1].icon) {
                        this.series[1].icon = this.renderer.path(['M', -4, 0, 'L', 4, 0, 'M', 0, -4, 'L', 4, 0, 0, 4])
                            .attr({
                                'stroke': '#ffffff',
                                'stroke-linecap': 'round',
                                'stroke-linejoin': 'round',
                                'stroke-width': 2,
                                'zIndex': 10
                            })
                            .add(this.series[2].group);
                    }
                    this.series[1].icon.translate(
                        this.chartWidth / 2 - 10,
                        this.plotHeight / 2 - this.series[1].points[0].shapeArgs.innerR -
                        (this.series[1].points[0].shapeArgs.r - this.series[1].points[0].shapeArgs.innerR) / 2
                    );

                    // Stand icon
                    if (!this.series[2].icon) {
                        this.series[2].icon = this.renderer.path(['M', 0, 4, 'L', 0, -4, 'M', -4, 0, 'L', 0, -4, 4, 0])
                        .attr({
                            'stroke': '#ffffff',
                            'stroke-linecap': 'round',
                            'stroke-linejoin': 'round',
                            'stroke-width': 2,
                            'zIndex': 10
                        })
                        .add(this.series[2].group);
                    }

                    this.series[2].icon.translate(
                        this.chartWidth / 2 - 10,
                        this.plotHeight / 2 - this.series[2].points[0].shapeArgs.innerR -
                        (this.series[2].points[0].shapeArgs.r - this.series[2].points[0].shapeArgs.innerR) / 2
                    );
                }

                Highcharts.chart('chart-container6', {
                    credits: {
                        enabled: false
                    },
                    chart: {
                        type: 'solidgauge',
                        height: '110%',
                        width: "300",
                        events: {
                            render: renderIcons
                        }
                    },
                    title: {
                        text: '',
                    },
                    tooltip: {
                        borderWidth: 0,
                        backgroundColor: 'none',
                        shadow: false,
                        style: {
                            fontSize: '14px'
                        },
                        pointFormat: '{series.name}<br><span style="font-size:0.8em; color: {point.color}; font-weight: bold">{point.y}</span>',
                        positioner: function (labelWidth) {
                            return {
                                x: (this.chart.chartWidth - labelWidth) / 2,
                                y: (this.chart.plotHeight / 2) + 15
                            };
                        }
                    },
                    pane: {
                        startAngle: 0,
                        endAngle: 360,
                        background: [{
                            outerRadius: '112%',
                            innerRadius: '88%',
                            backgroundColor: Highcharts.Color('#007bff').setOpacity(0.3).get(),
                            borderWidth: 0
                        },
                        {
                            outerRadius: '87%',
                            innerRadius: '63%',
                            backgroundColor: Highcharts.Color('#e0810d').setOpacity(0.3).get(),
                            borderWidth: 0
                        }, {
                            outerRadius: '62%',
                            innerRadius: '38%',
                            backgroundColor: Highcharts.Color('#49c47a').setOpacity(0.3).get(),
                            borderWidth: 0
                        }]
                    },
                    yAxis: {
                        min: 0,
                        max: 10000,
                        lineWidth: 0,
                        tickPositions: []
                    },
                    plotOptions: {
                        solidgauge: {
                            dataLabels: {
                                enabled: false
                            },
                            linecap: 'round',
                            stickyTracking: false,
                            rounded: true
                        }
                    },
                    legend: {
                        labelFormatter: function () {
                            return '<span style="text-weight:bold;color:' + this.userOptions.color + '">' + this.name + '</span>';
                        }
                    },
                    series: [{
                        name: 'Total Doctors',
                        borderColor: '#007bff',
                        color: '#007bff',
                        data: [{
                            color: '#007bff',
                            radius: '112%',
                            innerRadius: '88%',
                            y: totdrs
                        }],
                        showInLegend: true
                    }, {
                        name: 'Met',
                        borderColor: '#e0810d',
                        color: '#e0810d',
                        data: [{
                            color: '#e0810d',
                            radius: '87%',
                            innerRadius: '63%',
                            y: met
                        }],
                        showInLegend: true
                    }, {
                        name: 'Missed',
                        borderColor: '#49c47a',
                        color: '#49c47a',
                        data: [{
                            color: '#49c47a',
                            radius: '62%',
                            innerRadius: '38%',
                            y: miss
                        }],
                        showInLegend: true
                    }]
                });

            }
        </script>
    </form>
</body>
</html>
