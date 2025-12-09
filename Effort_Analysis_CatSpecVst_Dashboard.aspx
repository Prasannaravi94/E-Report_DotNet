<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Effort_Analysis_CatSpecVst_Dashboard.aspx.cs" Inherits="Effort_Analysis_CatSpecVst_Dashboard" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="keywords" content="">
    <meta name="decription" content="">
    <meta name="designer" content="Asad Kabir">


    <title></title>

    <link rel="icon" href="images/favicon.ico" />

    <!-- Include Bootstrap -->
    <link href="css/bootstrap_New.css" rel="stylesheet" />

    <!-- datepicker  -->
    <link rel="stylesheet" href="css/datepicker.css" />

    <!-- Main StyleSheet -->
    <%--<link href="assets/css/style_New.css" rel="stylesheet" />--%>
    <link href="assets/css/style_New.css" rel="stylesheet" />

    <!-- Responsive CSS -->
    <link rel="stylesheet" href="css/responsive.css" />

    <script src="DashBoard/js1/fusioncharts.js" type="text/javascript"></script>
    <script src="DashBoard/js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: white;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }



        .loading {
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>

    <style type="text/css">
        .savebutton {
            width: 60px;
            height: 22px;
            border-radius: 8px;
            background-image: linear-gradient(to top, #0077ff 0%, #28b5e0 100%);
            cursor: pointer;
            border: 0px;
            color: #ffffff;
            font-size: 14px;
            font-weight: 600;
            margin: 0 3px;
            padding: 0px;
            margin-top: 5px;
            margin-bottom: 5px;
        }

        .Viewbutton {
            width: 50px;
            height: 22px;
            border-radius: 8px;
            /*background-image: linear-gradient(to top, #0077ff 0%, #28b5e0 100%);*/
            background: #697077;
            cursor: pointer;
            border: 0px;
            color: #FFF;
            font-size: 14px;
            font-weight: 600;
            margin: 0 3px;
            padding: 0px;
            margin-top: 5px;
            margin-bottom: 5px;
        }

        .Gobutton {
            width: 40px;
            height: 26px;
            border-radius: 8px;
            background-image: linear-gradient(to top, #0077ff 0%, #28b5e0 100%);
            cursor: pointer;
            border: 0px;
            color: #ffffff;
            font-size: 14px;
            font-weight: 600;
            margin: 0 3px;
            padding: 0px;
            margin-top: 5px;
            margin-bottom: 5px;
        }
    </style>

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
            /*width: 1200px;*/
            margin: 0 auto;
            position: relative;
        }

            #container > div {
                /*width: 100%;*/
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

        .single-block-area {
            border-collapse: collapse;
            width: 550px;
            height: 360px;
            vertical-align: central;
        }

            .single-block-area th {
                text-align: center;
                color: #fff;
                text-align: center;
                padding: 15px;
                border-radius: 10px 10px 0px 0px;
            }

            .single-block-area td {
                padding: 15px;
            }

            /*.single-block-area tr td {
                border-bottom: 1px solid #DCE2E8;
            }*/


            .single-block-area tr td {
                border-bottom: 1px solid #DCE2E8;
            }




        .single-block-area-All {
            border-collapse: collapse;
            width: 550px;
            height: 240px;
            vertical-align: central;
        }

            .single-block-area-All th {
                text-align: center;
                color: #fff;
                text-align: center;
                padding: 15px;
                border-radius: 10px 10px 0px 0px;
            }

            .single-block-area-All td {
                padding: 15px;
            }

            /*.single-block-area tr td {
                border-bottom: 1px solid #DCE2E8;
            }*/


            .single-block-area-All tr td {
                border-bottom: 1px solid #DCE2E8;
            }
    </style>

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sSf_Code, SubDiv_Code, MGR_Code, Subid_YN) {
            popUpObj = window.open("rpt_Dashboard_Admin_FFTeam_View.aspx?sSf_Code=" + sSf_Code + "&SubDiv_Code=" + SubDiv_Code + "&MGR_Code=" + MGR_Code + "&Subid_YN=" + Subid_YN,
       "_blank",
      "ModalPopUp," +
      "toolbar=no," +
      "scrollbars=yes," +
      "location=no," +
      "statusbar=no," +
      "menubar=no," +
      "addressbar=no," +
      "resizable=yes," +
      "width=800," +
      "height=500," +
      "left = 0," +
      "top=0"
      );
            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s27.postimg.org/ke5a9z0o3/11_8_little_loader.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:650px; height: 300px;position: fixed;top: 20%;left: 10%;"  alt="" /></div>');
            });
        }


        function GotoHomepage() {
            window.location.href = "http://www.sansfa.in/default.aspx";
            // window.open('Default.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server"></div>
        <link rel="stylesheet"
            href="//netdna.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css">
        <script
            src="//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>


        <script
            src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
        <script
            src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/js/bootstrap-multiselect.js"></script>
        <link rel="stylesheet"
            href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/css/bootstrap-multiselect.css">



        <style>
            .dropdown-menu {
                font-size: 14px;
                text-align: left;
                list-style: none;
                background: #616870;
                color: #FFF;
            }

                .dropdown-menu > .active > a, .dropdown-menu > .active > a:focus {
                    color: #fff;
                    background: #616870;
                }

                /*.dropdown-menu:hover {
                color:blue;
                 background: #616870;
            }*/
                .dropdown-menu > a:hover {
                    color: blue;
                    background: blue;
                }

                .dropdown-menu > li > a {
                    font-weight: 400;
                    line-height: 1.42857143;
                    color: #fff;
                    white-space: nowrap;
                }

            .btn-default {
                color: #FFF;
                background-color: #697077;
                border-color: #697077;
            }

                .btn-default.active,
                .btn-default.focus,
                .btn-default:active,
                .btn-default:focus,
                .btn-default:hover,
                .open > .dropdown-toggle.btn-default {
                    color: #FFF;
                    background-color: #697077;
                    border-color: #adadad;
                }

            .caret {
                display: inline-block;
                width: 0px;
                height: 0px;
                margin-left: 2px;
                vertical-align: middle;
                border-top: 4px solid;
                border-right: 4px solid transparent;
                border-left: 4px solid transparent;
                color: #007bff;
            }

            /*.multiselect-container > li > a > label {
                margin: 0;
                height: 100%;
                cursor: pointer;
                font-weight: 400;
                padding: 3px 20px 3px 40px;
                  color: #fff;
                    background: #616870;
            }*/
        </style>


        <script type="text/javascript">
            $(function () {


                $('[id*=lstDiv]').multiselect({
                    enableFiltering: true,
                    maxHeight: 250,
                    buttonWidth: '200px'
                });
                $('[id*=lstFieldForce]').multiselect({
                    enableFiltering: true,
                    maxHeight: 400,
                    buttonWidth: '350px'
                });
                $('[id*=lstMode]').multiselect({
                    enableFiltering: true,
                    maxHeight: 250,
                    buttonWidth: '180px'
                });
                $('[id*=lstSpec]').multiselect({
                    enableFiltering: true,

                    enableCaseInsensitiveFiltering: true,
                    maxHeight: 250,
                    includeSelectAllOption: true,
                    buttonWidth: '150px'
                });
                $('[id*=ddlMonth]').multiselect({
                    maxHeight: 250,
                    buttonWidth: '110px'
                });
                $('[id*=ddlYear]').multiselect({
                    maxHeight: 250,
                    buttonWidth: '100px'
                });
            });
        </script>



        <script type="text/javascript">
            $(document).ready(function () {

                //$("#lstDiv").change(function () {
                //    alert("kdfslfsjd");
                //});

                //$(!window).on("load", function () {       
                //document.getElementById("btnGoBrand").style.visibility = "hidden";
                document.getElementById("chrt1").style.visibility = "hidden";

                var Dashpgload = '<%=Session["Dashpgload"] %>';
                //if (Dashpgload == 'Onload') {
                //    //alert("fdhgfdr");

                //    $(window).bind("load", function () {
                //        // code here
                //        document.getElementById("chrt1").style.visibility = "visible";
                //        var ddlType = $("#lstMode").val();
                //        if (ddlType == null) {
                //            alert("Select Mode."); $('#lstMode').focus(); return false;
                //        }
                //        else if (ddlType == "1") {
                //            CatWise();
                //        }
                //        else if (ddlType == "2") {
                //            SpecltyWise();
                //        }
                //    });
                //}

                $("#btnGo").click(function () {
                    document.getElementById("chrt1").style.visibility = "visible";
                    var ddlType = $("#lstMode").val();
                    if (ddlType == null) {
                        alert("Select Mode."); $('#lstMode').focus(); return false;
                    }
                    else if (ddlType == "1") {
                        CatWise();
                    }
                    else if (ddlType == "2") {
                        SpecltyWise();
                    }
                    else if (ddlType == "3") {
                        CampWise();
                    }
                    else if (ddlType == "4") {
                        MVDWise2();
                    }
                });
                //$("#btnback").click(function () {
                //    GotoHomepage();
                //});
            });
        </script>


        <script type="text/javascript">


            function CatWise() {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });


                var Fmnth = $("#ddlMonth").val();
                var FYear = $("#ddlYear").val();

                var Month_Name = '';
                var d = new Date();

                var FinanceYr = "";
                var FinanceYr_Name = "";


                var Month = Fmnth;
                var Year = FYear;
                var Field = $("#lstFieldForce").val();
                var FF_Name = $('#<%=lstFieldForce.ClientID%> :selected').text();
                var mode = $("#lstMode").val();
                var Div = $("#lstDiv").val();
                var Div_Name = $('#<%=lstDiv.ClientID%> :selected').text();
                var Div_New = "";
                var Div_Name_New = "";
                var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + Div + "^" + Div_Name;
                $.ajax({

                    type: 'POST',

                    url: "Effort_Analysis_CatSpecVst_Dashboard.aspx/CatWise",

                    contentType: "application/json; charset=utf-8",

                    dataType: "json",

                    data: '{objData:' + JSON.stringify(Data) + '}',
                    success: function (data) {

                        var chartData = eval("(" + data.d + ')');

                        //var data = [];

                        //for (var i in chartData) {
                        //    var serie = new Array(chartData[i].Cat_Name, chartData[i].Visit1, chartData[i].Visit2);
                        //    data.push(serie);
                        //}

                        //-----------------

                        document.getElementById("lblHead").textContent = "Listed Doctor Category Wise";
                        FusionCharts.ready(function () {
                            var revenueChart = new FusionCharts({
                                type: 'mscolumn2d',
                                renderAt: 'chart-container',
                                width: '1200',
                                height: '400',
                                dataFormat: 'json',
                                dataSource: {
                                    "chart": {
                                        "theme": "fusion",
                                        //"caption": "Listed Doctor Category Wise",
                                        "formatnumberscale": "0",
                                        "captionFont": "Arial",
                                        "captionFontSize": "18",
                                        "captionFontColor": "#1F51FF",
                                        "captionFontBold": "1",
                                        "showLegend": "0",
                                        "theme": "fint",
                                        "showPercentValues": "0",
                                        "placeValuesInside": "0",
                                        "valueFontColor": "#5d62b5",

                                        "valueFontBold": "1",
                                        "paletteColors": "#08BDBA,#EE5396,#A56EFF,#42BE65,#007bff,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                        //Setting legend to appear on right side
                                        // "legendPosition": "right",
                                        //Caption for legend
                                        // "legendCaption": "SfName: ",
                                        //Customization for legend scroll bar cosmetics
                                        "legendScrollBgColor": "#cccccc",
                                        "legendScrollBarColor": "#999999",

                                        "showLabels": "1",
                                        "showLegend": "1",
                                        "showValues": "1",
                                        "showPercentValues": "0",
                                        "showPercentInTooltip": "0",
                                        //"plotTooltext": "Cat : $label($seriesname)<br>Drs Cnt : $value<br>Avg : $Avrg",
                                        "rotateValues": "1",
                                        "formatNumber": "0",
                                        "formatNumberScale": "0"
                                    },

                                    "categories": [{
                                        "category": [{
                                            "label": chartData[0].Cat_Name
                                        }, {
                                            "label": chartData[1].Cat_Name
                                        }, {
                                            "label": chartData[2].Cat_Name
                                        }]
                                    }],
                                    "dataset": [
                                         {
                                             "seriesname": "0 Visit",
                                             "data": [{
                                                 "value": chartData[0].Visit0,
                                                 link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "8" + "&sf_name=" + FF_Name + "&Division_code=" + chartData[0].Division_code + "&Doc_Cat_Code=" + chartData[0].Doc_Cat_Code + "&Brand_Name=" + chartData[0].Cat_Name
                                                 , "tooltext": "Cat : $label($seriesname)<br>Missed Drs : $value<br>Tot Drs : " + chartData[0].Tot_drs + "<br>Avg : " + chartData[0].Visit0Avg + "(in %)"
                                             }, {
                                                 "value": chartData[1].Visit0
                                                 , link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "8" + "&sf_name=" + FF_Name + "&Division_code=" + chartData[1].Division_code + "&Doc_Cat_Code=" + chartData[1].Doc_Cat_Code + "&Brand_Name=" + chartData[1].Cat_Name
                                                  , "tooltext": "Cat : $label($seriesname)<br>Missed Drs : $value<br>Tot Drs : " + chartData[1].Tot_drs + "<br>Avg : " + chartData[1].Visit0Avg + "(in %)"
                                             }, {
                                                 "value": chartData[2].Visit0
                                                 , link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "8" + "&sf_name=" + FF_Name + "&Division_code=" + chartData[2].Division_code + "&Doc_Cat_Code=" + chartData[2].Doc_Cat_Code + "&Brand_Name=" + chartData[2].Cat_Name
                                                  , "tooltext": "Cat : $label($seriesname)<br>Missed Drs : $value<br>Tot Drs : " + chartData[2].Tot_drs + "<br>Avg : " + chartData[2].Visit0Avg + "(in %)"
                                             }]

                                         },
                                        {
                                            "seriesname": "1 Visit",
                                            "data": [{
                                                "value": chartData[0].Visit1   //SV1
                                                , link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "10" + "&Vst=" + "1" + "&sf_name=" + FF_Name + "&Division_code=" + chartData[0].Division_code + "&Doc_Cat_Code=" + chartData[0].Doc_Cat_Code + "&Brand_Name=" + chartData[0].Cat_Name
                                                , "tooltext": "Cat : $label($seriesname)<br>Drs Met : $value<br>Tot Drs : " + chartData[0].Tot_drs + "<br>Avg : " + chartData[0].Visit1Avg + "(in %)"
                                            }, {
                                                "value": chartData[1].Visit1  //RV2
                                                , link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "10" + "&Vst=" + "1" + "&sf_name=" + FF_Name + "&Division_code=" + chartData[1].Division_code + "&Doc_Cat_Code=" + chartData[1].Doc_Cat_Code + "&Brand_Name=" + chartData[1].Cat_Name
                                                , "tooltext": "Cat : $label($seriesname)<br>Drs Met : $value<br>Tot Drs : " + chartData[1].Tot_drs + "<br>Avg : " + chartData[1].Visit1Avg + "(in %)"
                                            }, {
                                                "value": chartData[2].Visit1   //TV3
                                                , link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "10" + "&Vst=" + "1" + "&sf_name=" + FF_Name + "&Division_code=" + chartData[2].Division_code + "&Doc_Cat_Code=" + chartData[2].Doc_Cat_Code + "&Brand_Name=" + chartData[2].Cat_Name
                                                 , "tooltext": "Cat : $label($seriesname)<br>Drs Met : $value<br>Tot Drs : " + chartData[2].Tot_drs + "<br>Avg : " + chartData[2].Visit1Avg + "(in %)"
                                            }]
                                        }, {
                                            "seriesname": "2 Visit",
                                            "data": [{
                                                "value": "0".visibility = false //SV1

                                            }, {
                                                "value": chartData[1].Visit2//RV2
                                                , link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "10" + "&Vst=" + "2" + "&sf_name=" + FF_Name + "&Division_code=" + chartData[1].Division_code + "&Doc_Cat_Code=" + chartData[1].Doc_Cat_Code + "&Brand_Name=" + chartData[1].Cat_Name
                                                , "tooltext": "Cat : $label($seriesname)<br>Drs Met : $value<br>Tot Drs : " + chartData[1].Tot_drs + "<br>Avg : " + chartData[1].Visit2Avg + "(in %)"
                                            }, {
                                                "value": chartData[2].Visit2 //TV3
                                                , link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "10" + "&Vst=" + "2" + "&sf_name=" + FF_Name + "&Division_code=" + chartData[2].Division_code + "&Doc_Cat_Code=" + chartData[2].Doc_Cat_Code + "&Brand_Name=" + chartData[2].Cat_Name
                                                , "tooltext": "Cat : $label($seriesname)<br>Drs Met : $value<br>Tot Drs : " + chartData[2].Tot_drs + "<br>Avg : " + chartData[2].Visit2Avg + "(in %)"
                                            }]
                                        }
                                    , {
                                        "seriesname": "3 Visit",
                                        "data": [{
                                            "value": "0".visibility = false//SV1
                                        }, {
                                            "value": "0".visibility = false//RV2
                                        }, {
                                            "value": chartData[2].Visit3 //TV3
                                            , link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "10" + "&Vst=" + "3" + "&sf_name=" + FF_Name + "&Division_code=" + chartData[2].Division_code + "&Doc_Cat_Code=" + chartData[2].Doc_Cat_Code + "&Brand_Name=" + chartData[2].Cat_Name
                                             , "tooltext": "Cat : $label($seriesname)<br>Drs Met : $value<br>Tot Drs : " + chartData[2].Tot_drs + "<br>Avg : " + chartData[2].Visit3Avg + "(in %)"
                                        }]
                                    }]

                                    //,"trendlines": [{
                                    //    "line": [{
                                    //        "startvalue": "12250",
                                    //        "color": "#007bff",
                                    //        //"displayvalue": "Previous{br}Average",
                                    //        "valueOnRight": "1",
                                    //        "thickness": "1",
                                    //        "showBelow": "1"
                                    //        //,"tooltext": "Previous year quarterly target  : $13.5K"
                                    //    }, {
                                    //        "startvalue": "25950",
                                    //        "color": "#71e3e0",
                                    //        //"displayvalue": "Current{br}Average",
                                    //        "valueOnRight": "1",
                                    //        "thickness": "1",
                                    //        "showBelow": "1"
                                    //        //,"tooltext": "Current year quarterly target  : $23K"
                                    //    }]
                                    //}]

                                    //"data": chartData

                                }


                            });

                            revenueChart.render();
                        });
                        //--------------------------------------
                        loading.hide();
                        modal.removeClass("modal");
                    },

                    error: function (xhr, ErrorText, thrownError) {
                        $("#chart-container").html(xhr.responseText);
                    }
                });
            }


            function SpecltyWise() {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });


                var Month = $("#ddlMonth").val();
                var Year = $("#ddlYear").val();
                var Field = $("#lstFieldForce").val();
                var FF_Name = $('#<%=lstFieldForce.ClientID%> :selected').text();
                var mode = $("#lstMode").val();
                var Div = $("#lstDiv").val();
                var Div_Name = $('#<%=lstDiv.ClientID%> :selected').text();


                var Svalues = "";
                var SpecMode = "0";
                var spec = $("[id*=lstSpec] option:selected");
                spec.each(function () {
                    Svalues += $(this).val() + ",";
                });
                if (Svalues == "") {
                    SpecMode = "0";
                }
                else {
                    SpecMode = "1";
                }

                var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + Div + "^" + Div_Name + "^" + Svalues + "^" + SpecMode;
                $.ajax({

                    type: 'POST',

                    url: "Effort_Analysis_CatSpecVst_Dashboard.aspx/SpecltyWise",

                    contentType: "application/json; charset=utf-8",

                    dataType: "json",

                    data: '{objData:' + JSON.stringify(Data) + '}',
                    success: function (getdatasval) {

                        var chartData = eval("(" + getdatasval.d + ')');
                        //var dataPoints = [];
                        //for (var i = 0; i < chartData.length; i++) {
                        //    for (var key in chartData[i]) {
                        //        if (!isNaN(chartData[i][key])) {
                        //            dataPoints.push({ indexLabel: key, y: Number(chartData[i][key]) });
                        //        }
                        //    }
                        //}
                        //var techDATA = [];
                        //var jdata = chartData;
                        //var jl = jdata.length;
                        //for (var i = 0; i < jl; i++) {
                        //    techDATA.push(chartData[i].cnt);
                        //}


                        document.getElementById("lblHead").textContent = "Listed Doctor Speciality Wise";
                        var data_array = [];
                        var count = chartData.length;
                        for (var i = 0; i < count; i++) {
                            var obj = {
                                label: chartData[i].Label,
                                value: chartData[i].Value,
                                Code: chartData[i].cnt,
                                link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "9" + "&sf_name=" + FF_Name + "&Division_code=" + chartData[i].Division_Code + "&Doc_Special_Code=" + chartData[i].Doc_Special_Code + "&Brand_Name=" + chartData[i].Label
                                , "tooltext": "Speciality : " + chartData[i].Label + "<br>Tot Drs : " + chartData[i].Totdrs + "<br>Drs Met : " + chartData[i].Value + "<br>Missed : " + chartData[i].miss +"<br>Avg : " + chartData[i].cnt + " %"
                            }
                            data_array.push(obj);
                        }

                        var objJSON = {
                            chart: {
                                //"caption": "Listed Doctor Speciality Wise",
                                //"url": "images/icon.png",
                                "formatnumberscale": "0",
                                "captionFont": "Arial",
                                "captionFontSize": "18",
                                "captionFontColor": "#1F51FF",
                                "captionFontBold": "1",
                                "showLegend": "0",
                                "theme": "fint",
                                "showPercentValues": "0",
                                "placeValuesInside": "0",
                                "valueFontColor": "#5d62b5",

                                "valueFontBold": "1",
                                //"paletteColors": "#007bff,#71e3e0,#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                "paletteColors": "#A6CEE3,#FF832B,#F1C21B,#D0E2FF,#82CFFF,#42BE65,#D12771,#D4BBFF,#009D9A,#4589FF,#33B1FF,#FF7EB6,#9EF0F0,#0043CE,#1192E8,#A2A9B0,#3DDBD9,#A7F0BA,#4589FF,#FF8389,#A56EFF,#EE5396,#A2A9B0,#9F1853,#5A70EA,#6DD400",
                                //Setting legend to appear on right side
                                // "legendPosition": "right",
                                //Caption for legend
                                // "legendCaption": "SfName: ",
                                //Customization for legend scroll bar cosmetics
                                "legendScrollBgColor": "#cccccc",
                                "legendScrollBarColor": "#999999",
                                "fill": "none"

                                //, "plotTooltext": "Speciality : $label<br>Drs Met : $value"
                            }, data: (data_array)
                            //, options: { backgroundColor: '#b8b6b6' }
                        };
                        var newdata = JSON.stringify(objJSON);


                        var fusioncharts = new FusionCharts({
                            type: 'Column2d',
                            renderAt: 'chart-container',
                            width: '1200',
                            height: '450',
                            align: 'center',
                            dataFormat: 'json',

                            //containerBackgroundColor: '#b8b6b6',

                            dataSource: newdata
                        });


                        //    var fusioncharts = new FusionCharts({

                        //        type: 'Column3d',
                        //        width: '800',
                        //        height: '400',
                        //        renderAt: 'chart-container',

                        //        dataFormat: 'json',

                        //        dataSource: {

                        //            "chart": {
                        //                "caption": "Listed Doctor Speciality Wise",
                        //                "formatnumberscale": "0",
                        //                "captionFont": "Arial",
                        //                "captionFontSize": "18",
                        //                "captionFontColor": "#993300",
                        //                "captionFontBold": "1",
                        //                "showLegend": "0",
                        //                "theme": "fint",
                        //                "showPercentValues": "0",
                        //                "placeValuesInside": "0",
                        //                "valueFontColor": "#5d62b5",

                        //                "valueFontBold": "1",
                        //                "paletteColors": "#007bff,#71e3e0,#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                        //                //Setting legend to appear on right side
                        //                // "legendPosition": "right",
                        //                //Caption for legend
                        //                // "legendCaption": "SfName: ",
                        //                //Customization for legend scroll bar cosmetics
                        //                "legendScrollBgColor": "#cccccc",
                        //                "legendScrollBarColor": "#999999"

                        //                //, "plotTooltext": "Speciality : $label<br>Drs Met : $value<br>Drs Met :" + techDATA + " "
                        //                 , "plotTooltext": "Speciality : $label<br>Drs Met : $value"


                        //                //, "dataProvider": techDATA,
                        //                // "valueField": "value",
                        //                // "titleField": "title",
                        //                // "labelText": "[[title]]: [[value]]",
                        //                // "pullOutOnlyOne": true

                        //            },


                        //            //labels: techDATA, datasets: [{
                        //            //    label: 'Method Covered' 
                        //            //}],

                        //            "data": chartData
                        //        }
                        //    }

                        //);

                        fusioncharts.render();
                        loading.hide();
                        modal.removeClass("modal");

                    },

                    error: function (xhr, ErrorText, thrownError) {

                        $("#chart-container").html(xhr.responseText);
                    }

                });
            }

            function CampWise() {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });


                var Month = $("#ddlMonth").val();
                var Year = $("#ddlYear").val();
                var Field = $("#lstFieldForce").val();
                var FF_Name = $('#<%=lstFieldForce.ClientID%> :selected').text();
                var mode = $("#lstMode").val();
                var Div = $("#lstDiv").val();
                var Div_Name = $('#<%=lstDiv.ClientID%> :selected').text();


                var Svalues = "";
                var SpecMode = "0";
                var spec = $("[id*=lstSpec] option:selected");
                spec.each(function () {
                    Svalues += $(this).val() + ",";
                });
                if (Svalues == "") {
                    SpecMode = "0";
                }
                else {
                    SpecMode = "1";
                }

                var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + Div + "^" + Div_Name + "^" + Svalues + "^" + SpecMode;
                $.ajax({

                    type: 'POST',

                    url: "Effort_Analysis_CatSpecVst_Dashboard.aspx/CampWise",

                    contentType: "application/json; charset=utf-8",

                    dataType: "json",

                    data: '{objData:' + JSON.stringify(Data) + '}',
                    success: function (getdatasval) {

                        var chartData = eval("(" + getdatasval.d + ')');
                        //var dataPoints = [];
                        //for (var i = 0; i < chartData.length; i++) {
                        //    for (var key in chartData[i]) {
                        //        if (!isNaN(chartData[i][key])) {
                        //            dataPoints.push({ indexLabel: key, y: Number(chartData[i][key]) });
                        //        }
                        //    }
                        //}
                        //var techDATA = [];
                        //var jdata = chartData;
                        //var jl = jdata.length;
                        //for (var i = 0; i < jl; i++) {
                        //    techDATA.push(chartData[i].cnt);
                        //}


                        document.getElementById("lblHead").textContent = "Listed Doctor Campaign Wise";
                        var data_array = [];
                        var count = chartData.length;
                        for (var i = 0; i < count; i++) {
                            var obj = {
                                label: chartData[i].Label,
                                value: chartData[i].Value,
                                Code: chartData[i].cnt,
                                link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "11" + "&sf_name=" + FF_Name + "&Division_code=" + chartData[i].Division_Code + "&Doc_Special_Code=" + chartData[i].Doc_Special_Code + "&Brand_Name=" + chartData[i].Label
                                , "tooltext": "Campaign : " + chartData[i].Label + "<br>Tot Drs : " + chartData[i].Totdrs + "<br>Drs Met : " + chartData[i].Value + "<br>Missed : " + chartData[i].miss + "<br>Avg : " + chartData[i].cnt + " %"
                            }
                            data_array.push(obj);
                        }

                        var objJSON = {
                            chart: {
                                //"caption": "Listed Doctor Speciality Wise",
                                //"url": "images/icon.png",
                                "formatnumberscale": "0",
                                "captionFont": "Arial",
                                "captionFontSize": "18",
                                "captionFontColor": "#1F51FF",
                                "captionFontBold": "1",
                                "showLegend": "0",
                                "theme": "fint",
                                "showPercentValues": "0",
                                "placeValuesInside": "0",
                                "valueFontColor": "#5d62b5",

                                "valueFontBold": "1",
                                //"paletteColors": "#007bff,#71e3e0,#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                "paletteColors": "#A6CEE3,#FF832B,#F1C21B,#D0E2FF,#82CFFF,#42BE65,#D12771,#D4BBFF,#009D9A,#4589FF,#33B1FF,#FF7EB6,#9EF0F0,#0043CE,#1192E8,#A2A9B0,#3DDBD9,#A7F0BA,#4589FF,#FF8389,#A56EFF,#EE5396,#A2A9B0,#9F1853,#5A70EA,#6DD400",
                                //Setting legend to appear on right side
                                // "legendPosition": "right",
                                //Caption for legend
                                // "legendCaption": "SfName: ",
                                //Customization for legend scroll bar cosmetics
                                "legendScrollBgColor": "#cccccc",
                                "legendScrollBarColor": "#999999",
                                "fill": "none"

                                //, "plotTooltext": "Speciality : $label<br>Drs Met : $value"
                            }, data: (data_array)
                            //, options: { backgroundColor: '#b8b6b6' }
                        };
                        var newdata = JSON.stringify(objJSON);


                        var fusioncharts = new FusionCharts({
                            type: 'Column2d',
                            renderAt: 'chart-container',
                            width: '1200',
                            height: '450',
                            align: 'center',
                            dataFormat: 'json',

                            //containerBackgroundColor: '#b8b6b6',

                            dataSource: newdata
                        });



                        fusioncharts.render();
                        loading.hide();
                        modal.removeClass("modal");

                    },

                    error: function (xhr, ErrorText, thrownError) {

                        $("#chart-container").html(xhr.responseText);
                    }

                });
            }
            function MVDWise() {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });


                var Month = $("#ddlMonth").val();
                var Year = $("#ddlYear").val();
                var Field = $("#lstFieldForce").val();
                var FF_Name = $('#<%=lstFieldForce.ClientID%> :selected').text();
                var mode = $("#lstMode").val();
                var Div = $("#lstDiv").val();
                var Div_Name = $('#<%=lstDiv.ClientID%> :selected').text();


                var Svalues = "";
                var SpecMode = "0";
                //var spec = $("[id*=lstSpec] option:selected");
                //spec.each(function () {
                //    Svalues += $(this).val() + ",";
                //});
                //if (Svalues == "") {
                //    SpecMode = "0";
                //}
                //else {
                //    SpecMode = "1";
                //}

                var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + Div + "^" + Div_Name;
                $.ajax({

                    type: 'POST',

                    url: "Effort_Analysis_CatSpecVst_Dashboard.aspx/MVDWise",

                    contentType: "application/json; charset=utf-8",

                    dataType: "json",

                    data: '{objData:' + JSON.stringify(Data) + '}',
                    success: function (getdatasval) {

                        var chartData = eval("(" + getdatasval.d + ')');
                        //var dataPoints = [];
                        //for (var i = 0; i < chartData.length; i++) {
                        //    for (var key in chartData[i]) {
                        //        if (!isNaN(chartData[i][key])) {
                        //            dataPoints.push({ indexLabel: key, y: Number(chartData[i][key]) });
                        //        }
                        //    }
                        //}
                        //var techDATA = [];
                        //var jdata = chartData;
                        //var jl = jdata.length;
                        //for (var i = 0; i < jl; i++) {
                        //    techDATA.push(chartData[i].cnt);
                        //}


                        document.getElementById("lblHead").textContent = "MVD Doctor Wise";
                        var data_array = [];
                        var count = chartData.length;
                        for (var i = 0; i < count; i++) {
                            var obj = {
                                label: chartData[i].Label,
                                value: chartData[i].Value,
                                Code: chartData[i].cnt,
                                link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "12" + "&sf_name=" + FF_Name + "&Division_code=" + Div + "&sf=" + chartData[i].sfcode + "&Brand_Name=" + chartData[i].Label
                                , "tooltext": "Fieldforce Name : " + chartData[i].Label + "<br>Tot Drs : " + chartData[i].Totdrs + "<br>Drs Met : " + chartData[i].Value + "<br>Missed : " + chartData[i].miss + "<br>Avg : " + chartData[i].cnt + " %"
                            }
                            data_array.push(obj);
                        }

                        var objJSON = {
                            chart: {
                                //"caption": "Listed Doctor Speciality Wise",
                                //"url": "images/icon.png",
                                "formatnumberscale": "0",
                                "captionFont": "Arial",
                                "captionFontSize": "18",
                                "captionFontColor": "#1F51FF",
                                "captionFontBold": "1",
                                "showLegend": "0",
                                "theme": "fint",
                                "showPercentValues": "0",
                                "placeValuesInside": "0",
                                "valueFontColor": "#5d62b5",

                                "valueFontBold": "1",
                                //"paletteColors": "#007bff,#71e3e0,#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                "paletteColors": "#A6CEE3,#FF832B,#F1C21B,#D0E2FF,#82CFFF,#42BE65,#D12771,#D4BBFF,#009D9A,#4589FF,#33B1FF,#FF7EB6,#9EF0F0,#0043CE,#1192E8,#A2A9B0,#3DDBD9,#A7F0BA,#4589FF,#FF8389,#A56EFF,#EE5396,#A2A9B0,#9F1853,#5A70EA,#6DD400",
                                //Setting legend to appear on right side
                                // "legendPosition": "right",
                                //Caption for legend
                                // "legendCaption": "SfName: ",
                                //Customization for legend scroll bar cosmetics
                                "legendScrollBgColor": "#cccccc",
                                "legendScrollBarColor": "#999999",
                                "fill": "none"

                                //, "plotTooltext": "Speciality : $label<br>Drs Met : $value"
                            }, data: (data_array)
                            //, options: { backgroundColor: '#b8b6b6' }
                        };
                        var newdata = JSON.stringify(objJSON);


                        var fusioncharts = new FusionCharts({
                            type: 'Column2d',
                            renderAt: 'chart-container',
                            width: '1200',
                            height: '450',
                            align: 'center',
                            dataFormat: 'json',

                            //containerBackgroundColor: '#b8b6b6',

                            dataSource: newdata
                        });



                        fusioncharts.render();
                        loading.hide();
                        modal.removeClass("modal");

                    },

                    error: function (xhr, ErrorText, thrownError) {

                        $("#chart-container").html(xhr.responseText);
                    }

                });
            }
            function MVDWise2() {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });

                var Month = $("#ddlMonth").val();
                var Year = $("#ddlYear").val();
                var Field = $("#lstFieldForce").val();
                var FF_Name = $('#<%=lstFieldForce.ClientID%> :selected').text();
                var mode = $("#lstMode").val();
                var Div = $("#lstDiv").val();
                var Div_Name = $('#<%=lstDiv.ClientID%> :selected').text();


                var Svalues = "";
                var SpecMode = "0";
                //var spec = $("[id*=lstSpec] option:selected");
                //spec.each(function () {
                //    Svalues += $(this).val() + ",";
                //});
                //if (Svalues == "") {
                //    SpecMode = "0";
                //}
                //else {
                //    SpecMode = "1";
                //}

                var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + Div + "^" + Div_Name;
                $.ajax({

                    type: 'POST',

                    url: "Effort_Analysis_CatSpecVst_Dashboard.aspx/MVDWise",

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
                        document.getElementById("lblHead").textContent = "MVD Doctor Wise";
                        var data_array = [];
                        var count = chartData.length;
                        for (var i = 0; i < count; i++) {
                            var obj = {
                                label: chartData[i].Label,
                                value: chartData[i].Value,
                                Code: chartData[i].cnt,
                                link: "n-/MIS Reports/Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + Field + "&FMnth=" + Month + "&FYear=" + Year + "&TMonth=" + Month + "&TYear=" + Year + "&mode=" + "12" + "&sf_name=" + FF_Name + "&Division_code=" + Div + "&sf=" + chartData[i].sfcode + "&Brand_Name=" + chartData[i].Label
                                , "tooltext": "Fieldforce Name : " + chartData[i].Label + "<br>Tot Drs : " + chartData[i].Totdrs + "<br>Drs Met : " + chartData[i].Value + "<br>Missed : " + chartData[i].miss + "<br>Avg : " + chartData[i].cnt + " %"
                            }
                            data_array.push(obj);
                        }
                        var fusioncharts = new FusionCharts({

                            "type": "scrollColumn2D",
                            "renderAt": "chart-container",
                            width: '1200',
                            height: '450',
                            "dataFormat": "json",
                            "dataSource": {
                                "chart": {
                                    "formatnumberscale": "0",
                                    "captionFont": "Arial",
                                    "captionFontSize": "18",
                                    "captionFontColor": "#1F51FF",
                                    "captionFontBold": "1",
                                    "showLegend": "0",
                                    "theme": "fint",
                                    "showPercentValues": "0",
                                    "placeValuesInside": "0",
                                    "valueFontColor": "#5d62b5",

                                    "valueFontBold": "1",
                                    //"paletteColors": "#007bff,#71e3e0,#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                    "paletteColors": "#A6CEE3,#FF832B,#F1C21B,#D0E2FF,#82CFFF,#42BE65,#D12771,#D4BBFF,#009D9A,#4589FF,#33B1FF,#FF7EB6,#9EF0F0,#0043CE,#1192E8,#A2A9B0,#3DDBD9,#A7F0BA,#4589FF,#FF8389,#A56EFF,#EE5396,#A2A9B0,#9F1853,#5A70EA,#6DD400",
                                    //Setting legend to appear on right side
                                    // "legendPosition": "right",
                                    //Caption for legend
                                    // "legendCaption": "SfName: ",
                                    //Customization for legend scroll bar cosmetics
                                    "legendScrollBgColor": "#cccccc",
                                    "legendScrollBarColor": "#999999",
                                    "labelDisplay": "rotate",
                                    "fill": "none"
                                  //  "clickURL": "F-drill-Product_Detail_Zoom.aspx?sf_code=" + Field + "&FMonth=" + Month + "&Fyear=" + Year + "&sf_name=" + SsfName + ""
                                },
                                "categories": [{
                                    "category": data_array
                                }],
                                "dataset": [{
                                    "data": data_array
                                }]
                            }
                        });
                        fusioncharts.render();
                        loading.hide();
                        modal.removeClass("modal");
                    },
                    error: function (xhr, ErrorText, thrownError) {
                        //  alert("Error: No Data Found!");
                    }
                });
            }
        </script>



        <header class="header-area">
            <div class="row align-items-center">
                <div class="col-lg-6">
                    <div class="header-left">
                        <a href="#">
                            <img src="images/sfe_dash.png" alt="">
                        </a>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="header-right text-right d-none d-lg-block">
                        <ul>
                            <li id="liback" runat="server"><a href="#"><i class="fas fa-chevron-left"></i>
                                <input type="button" id="btnback" class="Viewbutton" value="Back" runat="server"   onserverclick="btnback_click"/></a></li>

                            <li id="libtnHome" runat="server"><a href="#"><i class="fas fa-chevron-left"></i>
                                <asp:Button ID="btnHome" runat="server" Width="150px" Height="22px" CssClass="Viewbutton"
                                    Text="Direct to Home Page" OnClick="btngohome_click" /></a></li>

                            <li id="liLogout" runat="server"><a href="#"><i class="fas fa-lock"></i>
                                <asp:Button ID="btnLogout" runat="server" CssClass="Viewbutton"
                                    Text="Logout" OnClick="btnLogout_Click" /></a></li>

                        </ul>
                    </div>
                </div>
            </div>
        </header>





        <div class="dropdown-area">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-xl col-lg-4 col-md-6">
                        <div>
                            <asp:ListBox ID="lstDiv" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="lstDiv_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="col-xl col-lg-4 col-md-6">
                        <div>
                            <asp:ListBox ID="lstFieldForce" runat="server" Width="330px" AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="col-xl col-lg-4 col-md-6">
                        <div>
                            <asp:ListBox ID="ddlMonth" runat="server" Width="70px" AutoPostBack="true">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="col-xl col-lg-4 col-md-6">
                        <div>
                            <asp:ListBox ID="ddlYear" runat="server" Width="80px" AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="col-xl col-lg-4 col-md-6">
                        <div>
                            <ul>
                                <li class="lst-slc">
                                    <asp:ListBox ID="lstMode" runat="server" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="lstMode_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Text="Category" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Speciality"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Campaign"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="MVD Doctors"></asp:ListItem>
                                    </asp:ListBox>
                                </li>



                            </ul>
                        </div>
                    </div>

                    <div class="col-xl col-lg-4 col-md-6">
                        <div>
                            <asp:ListBox ID="lstSpec" runat="server" SelectionMode="Multiple" Width="80px" Visible="false"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-xl col-lg-4 col-md-6">
                        <div class="drop-item">
                            <ul>
                                <li>
<%--<a href="#"><i id="btnGo" class="fas fa-arrow-right"></i></a>--%>
<input type="button" id="btnGo" class="fas fa-arrow-right" value="Go" style="background:#33B1FF"  />
</li>
                            </ul>
                            <%--<input type="button" id="btnGo" class="savebutton" value="Go" />--%>
                            <%--<input type="button" id="Button1" class="fas fa-arrow-right"  value="->" runat="server" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div id="chrt1" runat="server" class="main-wrapper-area">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="wrapper-left">
                            <%--<h5 id="lblHead" runat="server">--%>
                            <h5>
                                <img src="images/icon.png" width="32" alt="">
                                <asp:Label ID="lblHead" runat="server" Style="vertical-align: middle; font-size: 24px; color: #191919; font-weight: bold;"></asp:Label></h5>
                            <div id="chart-container" style="height: 400px; width: 100%;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div>
            <div class="loading" align="center">
                <br />
                <%--<img src="../Images/loading/loadingScreen.gif" width="350px"  height="250px" alt="" />--%>
                <img src="../Images/loading/Graph_Loading.gif" width="350px" height="250px" alt="" />
            </div>
        </div>



        <script type="text/javascript" src="//code.highcharts.com/highcharts.js"></script>
        <script type="text/javascript" src="//code.highcharts.com/highcharts-more.js"></script>
        <script type="text/javascript" src="//code.highcharts.com/modules/solid-gauge.js"></script>

        <!-- main-wrapper-area end -->

        <!-- Main jQuery -->
        <script src="js/jquery-3.4.1.min.js"></script>

        <!-- Bootstrap Propper jQuery -->
        <script src="js/popper.js"></script>

        <!-- Bootstrap jQuery -->
        <script src="js/bootstrap.js"></script>

        <!-- Fontawesome Script -->
        <script src="https://kit.fontawesome.com/7749c9f08a.js"></script>

        <!-- datepicker js -->
        <script src="js/datepicker.min.js"></script>

        <!-- Custom jQuery -->
        <script src="js/scripts.js"></script>

        <!-- Scroll-Top button -->
        <a href="#" class="scrolltotop"><i class="fas fa-angle-up"></i></a>


    </form>
</body>
</html>
