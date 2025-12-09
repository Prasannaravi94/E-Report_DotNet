<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaleAnalysis_Graph.aspx.cs"
    Inherits="DashBoard_SaleAnalysis_Graph" %>

<%@ Register Src="~/UserControl/DashBoard.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<style type="text/css">
    .aclass
    {
        border: 1px solid lighgray;
    }
    .aclass
    {
        width: 50%;
    }
    .aclass tr td
    {
        background: White;
        font-weight: bold;
        color: Black;
        border: 1px solid black;
        border-collapse: collapse;
    }
    .aclass th
    {
        border: 1px solid black;
        border-collapse: collapse;
        background: LightBlue;
    }
    .lbl
    {
        color: Red;
    }
    
    
    .space
    {
        padding: 3px 3px;
    }
    .sp
    {
        padding-left: 11px;
    }
    
    .style6
    {
        padding: 3px 3px;
        height: 28px;
    }
    .marRight
    {
        margin-right: 35px;
    }
    
    .boxshadow
    {
        -moz-box-shadow: 3px 3px 5px #535353;
        -webkit-box-shadow: 3px 3px 5px #535353;
        box-shadow: 3px 3px 5px #535353;
    }
    .roundbox
    {
        -moz-border-radius: 6px 6px 6px 6px;
        -webkit-border-radius: 6px;
        border-radius: 6px 6px 6px 6px;
    }
    .grd
    {
        border: 1;
        border-color: Black;
    }
    .roundbox-top
    {
        -moz-border-radius: 6px 6px 0 0;
        -webkit-border-radius: 6px 6px 0 0;
        border-radius: 6px 6px 0 0;
    }
    .roundbox-bottom
    {
        -moz-border-radius: 0 0 6px 6px;
        -webkit-border-radius: 0 0 6px 6px;
        border-radius: 0 0 6px 6px;
    }
    .gridheader, .gridheaderbig, .gridheaderleft, .gridheaderright
    {
        padding: 6px 6px 6px 6px;
        background: Pink url(images/vertgradient.png) repeat-x;
        text-align: center;
        font-weight: bold;
        text-decoration: none;
        color: Blue;
    }
    .gridheaderleft
    {
        text-align: left;
    }
    .gridheaderright
    {
        text-align: right;
    }
    .gridheaderbig
    {
        font-size: 135%;
    }
     .modal
        {
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
        .loading
        {
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
</style>
<script src="JS/jquery-1.7.2.min.js" type="text/javascript"></script>
<%--<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/fusioncharts.js"></script>
<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/themes/fusioncharts.theme.fint.js?cacheBust=56"></script>--%>
<script src="js1/fusioncharts.js" type="text/javascript"></script>
<script src="js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>
    <script src="js1/fusioncharts.maps.js" type="text/javascript"></script>
<script src="maps/fusioncharts.india.js" type="text/javascript"></script>
<script type="text/javascript">

    $(document).ready(function () {


        //        $("#ddltype").change(function () {

        //            var ddlType = $("#ddltype").val();
        //            if (ddlType == "4") {
        //                Doughnut();
        //            }
        //            else if (ddlType == "2") {
        //                Pie();
        //            }
        //            else if (ddlType == "1") {
        //                Bar();
        //            }
        //            else if (ddlType == "5") {
        //                Pyramid();
        //            }
        //            else if (ddlType == "3") {
        //                Line();
        //            }
        //            else if (ddlType == "6") {
        //                Area();
        //            }
        //            else if (ddlType == "7") {
        //                Funnel();
        //            }
        //        });


        $("#btnGo").click(function () {
            HideDet.style.display = "none";
            var ddlType = $("#ddltype").val();

            if (ddlType == "4") {
                Doughnut();
            }
            else if (ddlType == "2") {
                Pie();
            }
            else if (ddlType == "1") {
                Bar();
            }
            else if (ddlType == "5") {
                Pyramid();
            }
            else if (ddlType == "3") {
                Line();
            }
            else if (ddlType == "6") {
                Area();
            }
            else if (ddlType == "7") {
                Pie3D();
            }
            else if (ddlType == "8") {
                Doughnut3D();
            }
            else if (ddlType == "9") {
                Pyramid3D();
            }
            else if (ddlType == "10") {
                Funnel();
            }
            else if (ddlType == "11") {
                Map();
            }
        });

    });
</script>
<script type="text/javascript">

    function Map() {
        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();
        var mode = $("#ddlmode").val();
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;

        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/Map",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'maps/india',

                    renderAt: 'chart-container',


                    width: '700',

                    height: '500',

                    dataFormat: 'json',

                    dataSource: {


                        "chart": {
                            "hovercolor": "CCCCCC",
                            "fillalpha": "80",
                            "connectorcolor": "000000",
                            "caption": "Sales Analysis",

                            "legendposition": "BOTTOM",
                            "showshadow": "0",
                            "showlegend": "1",
                            "bordercolor": "FFFFFF",
                            "canvasbordercolor": "FFFFFF",
                            "usehovercolor": "1",
                            "showbevel": "0",
                            "animation": "1",

                            "bgColor": "#00FFFF",
                            "nullEntityColor": "#C2C2D6",
                            "entityFillHoverColor": "CCCCCC",
                            "nullEntityAlpha": "50",
                            "formatnumberscale": "0",
                            "formatNumber": "0",

                            "useSNameInLabels": "1",
                            "showborder": "1"

                        },
                        "colorrange": {

                            "color": [
                    {
                        "minvalue": "0",
                        "maxvalue": "0",
                        "displayvalue": "Low (<0)",
                        "code": "#e44a00"
                    },
                    {
                        "minvalue": "100000",
                        "maxvalue": "1000000",
                        "displayvalue": "Moderate (100000 - 1000000)",
                        "code": "#f8bd19"
                    },
                    {
                        "minvalue": "1000000",
                        "maxvalue": "10000000",
                        "displayvalue": "High(10000000+)",
                        "code": "#6baa01"
                    }
                ]
                        },

                        "data": [
        {
            "data": chartData

        }



                    ]
                    }

                }

            );


                fusioncharts.render();



            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-Container").html(xhr.responseText);

            }

        });
        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/ssgrid",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    "type": "ssgrid",
                    "renderAt": "chart-container-St",
                    "width": "400",
                    "height": "300",
                    "dataFormat": "json",
                    "dataSource": {
                        "chart": {


                            "xAxisName": "State",
                            "yAxisName": "Value",

                            //"paletteColors": "#0075c2",
                            "bgColor": "#ffffff",
                            "showBorder": "1",
                            "showCanvasBorder": "1",
                            "plotBorderAlpha": "10",
                            "usePlotGradientColor": "0",
                            "plotFillAlpha": "50",
                            "showXAxisLine": "1",
                            "axisLineAlpha": "25",
                            "divLineAlpha": "10",
                            "showValues": "1",
                            "showAlternateHGridColor": "0",
                            "captionFontSize": "14",
                            "subcaptionFontSize": "14",
                            "subcaptionFontBold": "0",
                            "toolTipColor": "#ffffff",
                            "toolTipBorderThickness": "0",
                            "toolTipBgColor": "#000000",
                            "toolTipBgAlpha": "80",
                            "toolTipBorderRadius": "2",
                            "formatNumber": "0",
                            "formatNumberScale": "0",
                            "toolTipPadding": "5"
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

                $("#chart-container-St").html(xhr.responseText);

            }

        });
    }

</script>
<script type="text/javascript">

    function Bar() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var Data = Month + "^" + Year + "^" + Field;

        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/Input",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    "type": "Column3d",
                    "renderAt": "chart-container",
                    "width": "70%",
                    "height": "70%",
                    "dataFormat": "json",
                    "dataSource": {
                        "chart": {
                            "caption": "Sale Analysis",
                            "subcaption": "",
                            "xaxisname": "Product",
                            "yaxisname": "Value",
                            "placeValuesInside": "0",
                            "rotatevalues": "1",
                            "palette": "5",
                            //Configure scrollbar
                            "formatNumber": "0",
                            "formatNumberScale": "0",
                            "useRoundEdges": "1",
                            //  "theme": "fint",
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",

                            "labelDisplay": "rotate",
                            "exportEnabled": "1",
                            "exportAtClient": "1",
                            "exportHandler": "http://export.api3.fusioncharts.com",
                            "html5ExportHandler": "http://export.api3.fusioncharts.com"
                        },


                        "categories": [{
                            "category": chartData
                        }],
                        "dataset": [{
                            "data": chartData
                        }]


                    }

                }

            );

                fusioncharts.render();

            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-Container").html(xhr.responseText);

            }

        });
    }

</script>
<script type="text/javascript">

    function Area() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var Data = Month + "^" + Year + "^" + Field;

        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/Input",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    "type": "scrollarea2d",
                    "renderAt": "chart-container",
                    "width": "70%",
                    "height": "70%",
                    "dataFormat": "json",
                    "dataSource": {
                        "chart": {
                            "caption": "Sales Analysis",
                            "subCaption": "",
                            "captionFontSize": "14",
                            "subcaptionFontSize": "14",
                            "subcaptionFontBold": "0",
                            "xAxisname": "Product",
                            "pYAxisName": "Value",
                            "rotatevalues": "1",
                            "sYAxisName": "Employees",
                            "numberPrefix": "",
                            "paletteColors": "#0075c2",
                            "bgcolor": "#ffffff",
                            "showBorder": "1",
                            "showCanvasBorder": "0",
                            "usePlotGradientColor": "0",
                            "plotBorderAlpha": "10",
                            "showAxisLines": "1",
                            "valueBgColor": "#FFFFFF",
                            "valueBgAlpha": "50",
                            "showAlternateHGridColor": "0",
                            "divlineThickness": "1",
                            "divLineIsDashed": "1",
                            "divLineDashLen": "1",
                            "divLineGapLen": "1",
                            "numVisiblePlot": "8",
                            "flatScrollBars": "1",
                            "scrollheight": "10",
                            "labelDisplay": "rotate",
                            "formatNumber": "0",
                            "formatNumberScale": "0",
                            "exportEnabled": "1",
                            "exportAtClient": "1",
                            "exportHandler": "http://export.api3.fusioncharts.com",
                            "html5ExportHandler": "http://export.api3.fusioncharts.com"
                        },


                        "categories": [{
                            "category": chartData
                        }],
                        "dataset": [{
                            "data": chartData
                        }]

                    }

                }

            );

                fusioncharts.render();

            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-Container").html(xhr.responseText);

            }

        });
    }

</script>
<script type="text/javascript">

    function Doughnut() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var Data = Month + "^" + Year + "^" + Field;

        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/Input",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'doughnut2d',

                    renderAt: 'chart-container',

                    width: '70%',

                    height: '70%',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": "Sale Analysis",
                            "subCaption": "",
                            "numberPrefix": "",
                            "startingAngle": "310",
                            "decimals": "0",
                            "defaultCenterLabel": "",
                            "showValues": "1",
                            "centerLabel": "$label",
                            "plotTooltext": "Product : $label<br>Value : $value",
                            "showLabels": "1",
                            "showLegend": "1",
                            "legendScrollBgColor": "#cccccc",
                            "legendScrollBarColor": "#999999",
                            "legendPosition": "right",
                            "showPercentValues": "1",
                            "theme": "fint",
                            "exportEnabled": "1",
                            "exportAtClient": "1",
                            "exportHandler": "http://export.api3.fusioncharts.com",
                            "html5ExportHandler": "http://export.api3.fusioncharts.com"
                        },



                        "data": chartData

                    }

                }

            );

                fusioncharts.render();

            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-Container").html(xhr.responseText);

            }

        });
    }

</script>
<script type="text/javascript">

    function Doughnut3D() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var Data = Month + "^" + Year + "^" + Field;

        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/Input",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'doughnut3d',

                    renderAt: 'chart-container',

                    width: '70%',

                    height: '70%',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                            "bgColor": "#ffffff",
                            "showBorder": "1",
                            "use3DLighting": "0",
                            "showShadow": "0",
                            "enableSmartLabels": "0",

                            "caption": "Sale Analysis",
                            "subCaption": "",
                            "numberPrefix": "",
                            "startingAngle": "310",
                            "decimals": "0",
                            "defaultCenterLabel": "",
                            "showValues": "1",
                            "centerLabel": "$label",
                            "plotTooltext": "Product : $label<br>Value : $value",
                            "showLabels": "1",
                            "showLegend": "1",
                            "legendScrollBgColor": "#cccccc",
                            "legendScrollBarColor": "#999999",
                            "legendPosition": "right",
                            "showPercentValues": "1",
                            "theme": "fint",
                            "smartLineColor": "#d11b2d",
                            "smartLineThickness": "2",
                            "smartLineAlpha": "75",
                            "isSmartLineSlanted": "0",
                            "exportEnabled": "1",
                            "exportAtClient": "1",
                            "exportHandler": "http://export.api3.fusioncharts.com",
                            "html5ExportHandler": "http://export.api3.fusioncharts.com"
                        },



                        "data": chartData

                    }

                }

            );

                fusioncharts.render();

            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-Container").html(xhr.responseText);

            }

        });
    }

</script>
<script type="text/javascript">

    function Pie() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var Data = Month + "^" + Year + "^" + Field;
        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/Input",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',
            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'pie2d',

                    renderAt: 'chart-container',

                    width: '80%',

                    height: '80%',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": "Sales Analysis",
                            "formatnumberscale": "0",
                            "showBorder": "1",
                            "showLegend": "1",
                            "theme": "fint",
                            "showPercentValues": "1",
                            "showPercentInToolTip": "0",
                            //Setting legend to appear on right side
                            "legendPosition": "right",
                            //Caption for legend
                            "legendCaption": "Product: ",
                            //Customization for legend scroll bar cosmetics
                            "legendScrollBgColor": "#cccccc",
                            "legendScrollBarColor": "#999999",
                            "exportEnabled": "1",
                            "exportAtClient": "1",
                            "exportHandler": "http://export.api3.fusioncharts.com",
                            "html5ExportHandler": "http://export.api3.fusioncharts.com"
                        },


                        "data": chartData

                    }

                }

            );

                fusioncharts.render();

            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-Container").html(xhr.responseText);

            }

        });
    }
</script>
<script type="text/javascript">

    function Pie3D() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var Data = Month + "^" + Year + "^" + Field;
        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/Input",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',
            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'pie3d',

                    renderAt: 'chart-container',

                    width: '70%',

                    height: '70%',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": "Sale Analysis",
                            "subCaption": "",
                            "startingAngle": "120",

                            "showLegend": "1",
                            "legendScrollBgColor": "#cccccc",
                            "legendScrollBarColor": "#999999",
                            "legendPosition": "right",
                            "enableMultiSlicing": "0",
                            "slicingDistance": "15",
                            //To show the values in percentage
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                            "showBorder": "1",
                            "showPercentValues": "1",
                            "showPercentInTooltip": "0",
                            "plotTooltext": "Product : $label<br>Value : $value",
                            "theme": "fint",
                            "exportEnabled": "1",
                            "exportAtClient": "1",
                            "exportHandler": "http://export.api3.fusioncharts.com",
                            "html5ExportHandler": "http://export.api3.fusioncharts.com"
                        },


                        "data": chartData

                    }

                }

            );

                fusioncharts.render();

            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-Container").html(xhr.responseText);

            }

        });
    }
</script>
<script type="text/javascript">

    function Pyramid() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var Data = Month + "^" + Year + "^" + Field;

        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/Input",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'pyramid',

                    renderAt: 'chart-container',

                    id: 'wealth-pyramid-chart',

                    width: '70%',

                    height: '70%',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "theme": "fint",
                            "caption": "",
                            "captionOnTop": "0",
                            "captionPadding": "25",
                            "alignCaptionWithCanvas": "1",
                            "subcaption": "",
                            "subCaptionFontSize": "12",
                            "borderAlpha": "20",
                            "is2D": "1",
                            "bgColor": "#ffffff",
                            "showValues": "1",
                            "numberPrefix": "",
                            "numberSuffix": "",
                            "plotTooltext": "$label $value",
                            "showPercentValues": "1",
                            "chartLeftMargin": "40",
                            "showLegend": "1",
                            "legendScrollBgColor": "#cccccc",
                            "legendScrollBarColor": "#999999",
                            "legendPosition": "right",
                            "exportEnabled": "1",
                            "exportAtClient": "1",
                            "exportHandler": "http://export.api3.fusioncharts.com",
                            "html5ExportHandler": "http://export.api3.fusioncharts.com"
                        },



                        "data": chartData

                    }

                }

            );

                fusioncharts.render();

            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-Container").html(xhr.responseText);

            }

        });
    }

</script>
<script type="text/javascript">

    function Pyramid3D() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var Data = Month + "^" + Year + "^" + Field;

        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/Input",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'pyramid',

                    renderAt: 'chart-container',

                    id: 'wealth-pyramid-chart',

                    width: '70%',

                    height: '70%',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "theme": "fint",
                            "caption": "Sale Analysis",
                            "captionOnTop": "0",
                            "captionPadding": "25",
                            "alignCaptionWithCanvas": "1",
                            "subcaption": "",
                            "subCaptionFontSize": "12",
                            "borderAlpha": "20",
                            "is2D": "0",
                            "bgColor": "#ffffff",
                            "showValues": "1",
                            "numberPrefix": "$",
                            "numberSuffix": "M",
                            "plotTooltext": "$label Product $value Value ",
                            "showPercentValues": "1",
                            "chartLeftMargin": "40",
                            "showLegend": "1",
                            "legendScrollBgColor": "#cccccc",
                            "legendScrollBarColor": "#999999",
                            "legendPosition": "right",
                            "exportEnabled": "1",
                            "exportAtClient": "1",
                            "exportHandler": "http://export.api3.fusioncharts.com",
                            "html5ExportHandler": "http://export.api3.fusioncharts.com"
                        },



                        "data": chartData

                    }

                }

            );

                fusioncharts.render();

            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-Container").html(xhr.responseText);

            }

        });
    }

</script>
<script type="text/javascript">

    function Line() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var Data = Month + "^" + Year + "^" + Field;

        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/Input",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    "type": "scrollline2d",
                    "renderAt": "chart-container",
                    "width": "600",
                    "height": "500",
                    "dataFormat": "json",
                    "dataSource": {
                        "chart": {
                            "caption": "Sales Analysis",
                            "subCaption": "",
                            "xAxisName": "Product",
                            "yAxisName": "Value",
                            "showValues": "1",
                            "numberPrefix": "",
                            "showBorder": "1",
                            "rotatevalues": "1",
                            "showShadow": "1",
                            "bgColor": "#ffffff",
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                            "showCanvasBorder": "1",
                            "showAxisLines": "1",
                            "showAlternateHGridColor": "0",
                            "divlineAlpha": "100",
                            "divlineThickness": "1",
                            "divLineIsDashed": "1",
                            "divLineDashLen": "1",
                            "divLineGapLen": "1",
                            "lineThickness": "3",
                            "flatScrollBars": "1",
                            "scrollheight": "10",
                            "numVisiblePlot": "12",
                            "showHoverEffect": "1",

                            "showLabels": "1",
                            "showLegend": "1",
                            "showValues": "1",
                            "showPercentValues": "1",
                            "showPercentInTooltip": "0",
                            "plotTooltext": "Product : $label<br>Value : $value",
                            "labelDisplay": "rotate",
                            "formatNumber": "0",
                            "formatNumberScale": "0",
                            "exportEnabled": "1",
                            "exportAtClient": "1",
                            "exportHandler": "http://export.api3.fusioncharts.com",
                            "html5ExportHandler": "http://export.api3.fusioncharts.com"

                        },

                        "categories": [{
                            "category": chartData
                        }],
                        "dataset": [{
                            "data": chartData
                        }]

                    }

                }

            );

                fusioncharts.render();

            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-Container").html(xhr.responseText);

            }

        });
    }

</script>
<script type="text/javascript">

    function Funnel() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var Data = Month + "^" + Year + "^" + Field;

        $.ajax({

            type: 'POST',

            url: "SaleAnalysis_Graph.aspx/Input",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'funnel',

                    renderAt: 'chart-container',



                    width: '70%',

                    height: '70%',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": "Sale Analysis",
                            "subcaption": "",
                            "numberprefix": "",
                            "is2D": "1",
                            "streamlinedData": "0",
                            "showPercentValues": "1",
                            "showLegend": "1",
                            "showLabels": "1",
                            "showValues": "1",
                            "theme": "fint",
                            "showLegend": "1",
                            "legendScrollBgColor": "#cccccc",
                            "legendScrollBarColor": "#999999",
                            "legendPosition": "right",
                            "plotTooltext": "Product : $label<br>Value : $value",
                            "exportAtClient": "1",
                            "exportHandler": "http://export.api3.fusioncharts.com",
                            "html5ExportHandler": "http://export.api3.fusioncharts.com"
                        },



                        "data": chartData

                    }

                }

            );

                fusioncharts.render();

            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-Container").html(xhr.responseText);

            }

        });
    }

</script>
<script type="text/javascript">
    function DisplayDetail() {

        var HideDet = document.getElementById("HideDet");
        if (HideDet.style.display == "none")
            HideDet.style.display = "block";
        else
            HideDet.style.display = "none";
        return;

    }

</script>
<script type="text/javascript">
    function HideMenu_Det() {

        var HideMenu = document.getElementById("pnl");
        if (HideMenu.style.display == "none")
            HideMenu.style.display = "block";
        else
            HideMenu.style.display = "none";
        return;

    }

</script>
<style type="text/css">
    table.gridtable
    {
        font-family: verdana,arial,sans-serif;
        font-size: 11px;
        color: #333333;
        border-width: 1px;
        border-color: #666666;
        border-collapse: collapse;
    }
    table.gridtable th
    {
        padding: 5px;
    }
    table.gridtable td
    {
        border-width: 1px;
        padding: 5px;
        border-style: solid;
        border-color: #666666;
    }
       .button5 {border-radius: 50%;
             background-color:Blue;
             color:White;
             } 
</style>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnl" runat="server">
    <ucl:Menu ID="menu1" runat="server" />
    </asp:Panel>
       
    <br />
    <center>
        <div>
            <asp:Panel ID="pnlchart" runat="server" Style="vertical-align: top">
                <div class="roundbox boxshadow" style="width: 100%; height: 800px; border: solid 2px steelblue;">
                    <div class="gridheaderleft">
                       Secondary - Sales Analysis
                        <asp:ImageButton ID="btnHide" runat="server" ImageUrl="~/Images/12_eye.png" ToolTip="Show/Hide" OnClientClick="DisplayDetail(); return false;" />
                        <asp:Label ID="lblSF" runat="server" Text="Field Force Name " SkinID="lblMand"></asp:Label>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:Label ID="lblFMonth" runat="server" Text="From Month " SkinID="lblMand"></asp:Label>
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired">
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
                        <asp:Label ID="lblFYear" runat="server" Text="From Year " SkinID="lblMand"></asp:Label>
                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label1" runat="server" Text="Type" SkinID="lblMand"></asp:Label>
                        <asp:DropDownList ID="ddltype" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Pie Chart"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Pie 3D Chart"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Line Chart"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Doughnut Chart"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Doughnut 3D Chart"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Pyramid"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Pyramid 3D"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Bar Chart"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Area Chart"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Funnel"></asp:ListItem>
                                   <asp:ListItem Value="11" Text="Map"></asp:ListItem>
                        </asp:DropDownList>
                        <input type="button" id="btnGo" class="button5"  value="Go" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Show/Hide" ImageUrl="~/Images/menu2.png" OnClientClick="HideMenu_Det(); return false;" />
                    </div>
                    <div class="boxcontenttext" style="background: White;">
                        <div id="pnlPreviewSurveyData">
                            <table border="0" cellpadding="3" class="gridtable" cellspacing="3">
                                <tr>
                                    <%-- <td align="left">
                                        <table style="vertical-align:top;width:200px">
                                            <tr>
                                                <td>
                                                    <asp:TreeView runat="server" ID="TreeView1" Visible="false">
                                                        <Nodes>
                                                            <asp:TreeNode Text="Secondary Sale" NavigateUrl="">
                                                                <asp:TreeNode Text="Stockisewise" NavigateUrl="" />
                                                                <asp:TreeNode Text="Productwise" NavigateUrl="" />
                                                                <asp:TreeNode Text="Fieldforcewise" NavigateUrl="" />
                                                            </asp:TreeNode>
                                                            <%--<asp:TreeNode Text="" NavigateUrl="">
                                                                <asp:TreeNode Text="" NavigateUrl="" Target="" />
                                                                <asp:TreeNode Text="" NavigateUrl="" Target="" />
                                                            </asp:TreeNode>--%>
                                    <%--    </Nodes>
                                                    </asp:TreeView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>--%>
                                    <td id="HideDet" runat="server" style="border: none;">
                                        <table style="vertical-align: top;">
                                            <tr>
                                                <td style="border: none;" align="left" class="stylespc">
                                                </td>
                                                <td style="border: none;" align="left" class="stylespc">
                                                    <%-- <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                                        ToolTip="Enter Text Here"></asp:TextBox>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: none;" align="left" class="stylespc">
                                                </td>
                                                <td style="border: none;" align="left" class="stylespc">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: none;" align="left" class="stylespc">
                                                </td>
                                                <td style="border: none;" align="left" class="stylespc">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: none;" align="left" class="stylespc">
                                                </td>
                                                <td style="border: none;" align="left" class="stylespc">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <%--  <table>
                    <tr>
                        <td>
                            <div>
                    <div id="chart-container">
                    </div>--%>
                                    <td style="border: none;">
                                        <table>
                                            <tr>
                                                <td style="border: none;">
                                                    <div id="chart-container">
                                                    </div>
                                                     <br />
                                                                 <div id="chart-container-St">
                                                                </div>
                                                    <div>
                                                        <asp:Literal ID="FCLiteral1" runat="server"></asp:Literal>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>
              <div class="loading" align="center">
            <br />
            <%--<img src="../Images/loading/loadingScreen.gif" width="350px"  height="250px" alt="" />--%>
            <img src="../Images/loading/Graph_Loading.gif" width="350px" height="250px" alt="" />
        </div>
        </div>
    </center>
    </form>
</body>
</html>
