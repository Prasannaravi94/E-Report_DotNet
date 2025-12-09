<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Met.aspx.cs" Inherits="DashBoard_Doctor_Met" %>

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
        text-align: center;
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

<script src="js1/fusioncharts.js" type="text/javascript"></script>
<script src="js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>
    <script src="js1/fusioncharts.maps.js" type="text/javascript"></script>
    <script src="maps/fusioncharts.india.js" type="text/javascript"></script>
<script type="text/javascript">
    function showDrop(select) {
        if (select.value == 0) {
            document.getElementById('lblSF').style.display = "none";
            document.getElementById('ddlFieldForce').style.display = "none";
            document.getElementById('lblmode').style.display = "block";
            document.getElementById('ddlMode').style.display = "block";
        }
        else if (select.value == 1) {
            document.getElementById('lblSF').style.display = "block";
            document.getElementById('ddlFieldForce').style.display = "block";
            document.getElementById('lblmode').style.display = "none";
            document.getElementById('ddlMode').style.display = "none";
        }
    } 
</script>
<script type="text/javascript">

    $(document).ready(function () {

        document.getElementById('lblSF').style.display = "none";
        document.getElementById('ddlFieldForce').style.display = "none";
        var HideSin = document.getElementById("pnlSingle");
        var Hide = document.getElementById("pnlgrid");
      
        $("#btnGo").click(function () {

            var ddlMode = $("#ddlMode").val();
            var ddlwise = $("#ddlwise").val();
            if (ddlwise == "0") {
                if (ddlMode == "1") {
                   
                    Hide.style.display = "none";
                    Prod_Cat();
                }
                else if (ddlMode == "2") {
                    Hide.style.display = "none";
                    Prod_Grp();
                }
                else if (ddlMode == "3") {
                    Hide.style.display = "none";
                    Prod_Brand();
                }
                else if (ddlMode == "4") {
                    Hide.style.display = "none";
                    Doc_Cat();
                }
                else if (ddlMode == "5") {
                    Hide.style.display = "none";
                    Doc_Spec();
                }
                else if (ddlMode == "6") {
                    Hide.style.display = "none";
                    Doc_Cls();
                }
                else if (ddlMode == "7") {
                    Hide.style.display = "none";
                    Doc_Camp();
                }
                else if (ddlMode == "8") {
                    Hide.style.display = "block";
                    Stok_St();
                }
            }
            else if (ddlwise == "1") {
                Hide.style.display = "none";

                Stack();
            }
        });

    });
</script>
<%--<script type="text/javascript">

    function Stack() {

        var Field = $("#ddlFieldForce").val();
          var Data = Field;

       
        $.ajax({
            type: 'POST',
            url: "Doctor_Met.aspx/Input",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var chartData = eval("(" + data.d + ')');
                FusionCharts.ready(function () {
                    var fusioncharts = new FusionCharts({
                        type: 'scrollstackedcolumn2d',
                        renderAt: 'chart-container',
                        width: '70%',
                        height: '70%',
                        dataFormat: 'json',
                        dataSource: chartData
                        //dataSource: {
                        //    "chart": {
                        //        "caption": "Revenue split by product category",
                        //        "subCaption": "For current year",
                        //        "xAxisname": "Quarter",
                        //        "yAxisName": "Revenues (In USD)",
                        //        "showSum": "1",
                        //        "numberPrefix": "$",
                        //        "theme": "fint"
                        //    },
                        //    "categories": [{
                        //        "category": [{
                        //            "label": "Q1"
                        //        }, {
                        //            "label": "Q2"
                        //        }, {
                        //            "label": "Q3"
                        //        }, {
                        //            "label": "Q4"
                        //        }]
                        //    }],

                        //    "dataset": [{
                        //        "seriesname": "Food Products",
                        //        "data": [{
                        //            "value": "11000"
                        //        }, {
                        //            "value": "15000"
                        //        }, {
                        //            "value": "13500"
                        //        }, {
                        //            "value": "15000"
                        //        }]
                        //    }, {
                        //        "seriesname": "Non-Food Products",
                        //        "data": [{
                        //            "value": "11400"
                        //        }, {
                        //            "value": "14800"
                        //        }, {
                        //            "value": "8300"
                        //        }, {
                        //            "value": "11800"
                        //        }]
                        //    }]
                        //}
                    });
                    fusioncharts.render();
                });
            }
      
</script>--%>
<%--<script type="text/javascript">
    $(document).ready(function () {

        $.ajax({
            type: 'POST',
            url: "Doctor_Met.aspx/Input",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var chartData = eval("(" + data.d + ')');
                FusionCharts.ready(function () {
                    var fusioncharts = new FusionCharts({
                        type: 'scrollstackedcolumn2d',
                        renderAt: 'chart-container',
                        width: '70%',
                        height: '70%',
                        dataFormat: 'json',
                        dataSource: chartData
                        //dataSource: {
                        //    "chart": {
                        //        "caption": "Revenue split by product category",
                        //        "subCaption": "For current year",
                        //        "xAxisname": "Quarter",
                        //        "yAxisName": "Revenues (In USD)",
                        //        "showSum": "1",
                        //        "numberPrefix": "$",
                        //        "theme": "fint"
                        //    },
                        //    "categories": [{
                        //        "category": [{
                        //            "label": "Q1"
                        //        }, {
                        //            "label": "Q2"
                        //        }, {
                        //            "label": "Q3"
                        //        }, {
                        //            "label": "Q4"
                        //        }]
                        //    }],

                        //    "dataset": [{
                        //        "seriesname": "Food Products",
                        //        "data": [{
                        //            "value": "11000"
                        //        }, {
                        //            "value": "15000"
                        //        }, {
                        //            "value": "13500"
                        //        }, {
                        //            "value": "15000"
                        //        }]
                        //    }, {
                        //        "seriesname": "Non-Food Products",
                        //        "data": [{
                        //            "value": "11400"
                        //        }, {
                        //            "value": "14800"
                        //        }, {
                        //            "value": "8300"
                        //        }, {
                        //            "value": "11800"
                        //        }]
                        //    }]
                        //}
                    });
                    fusioncharts.render();
                });
            }
        });
    });
    </script>--%>
<script type="text/javascript">

    function Prod_Cat() {
        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });

        $.ajax({

            type: 'POST',

            url: "Doctor_Met.aspx/ProdCat",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'pie3d',

                    renderAt: 'chart-container',

                    width: '600',

                    height: '400',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": "Product Category - SKU wise",
                            "subCaption": "",
                            "startingAngle": "120",

                            "showLegend": "1",
                            "enableMultiSlicing": "0",

                            //To show the values in percentage
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",

                            "showPercentValues": "0",
                            "showPercentInTooltip": "0",
                            "plotTooltext": "Category : $label<br>Product Count: $value",
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

    function Prod_Grp() {
        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });

        $.ajax({

            type: 'POST',

            url: "Doctor_Met.aspx/ProdGroup",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'doughnut2d',

                    renderAt: 'chart-container',

                    width: '600',

                    height: '400',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": "Product Group - SKU wise",
                            "subCaption": "",
                            "startingAngle": "120",

                            "showLegend": "1",
                            "enableMultiSlicing": "0",

                            //To show the values in percentage
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",

                            "showPercentValues": "0",
                            "showPercentInTooltip": "0",
                            "plotTooltext": "Group : $label<br>Product Count: $value",
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

    function Prod_Brand() {
        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });

        $.ajax({

            type: 'POST',

            url: "Doctor_Met.aspx/ProdBrand",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'pie2d',

                    renderAt: 'chart-container',

                    width: '800',

                    height: '600',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": "Product Brand - SKU Wise",
                            "subCaption": "",
                            "startingAngle": "120",

                            "showLegend": "1",
                            "enableMultiSlicing": "0",

                            //To show the values in percentage
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",

                            "showPercentValues": "0",
                            "showPercentInTooltip": "0",
                            "plotTooltext": "Brand : $label<br>Product Count: $value",
                            "theme": "fint",
                            "legendPosition": "right",
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

    function Doc_Cat() {

        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
        $.ajax({

            type: 'POST',

            url: "Doctor_Met.aspx/DocCat",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'pyramid',

                    renderAt: 'chart-container',

                    width: '600',

                    height: '400',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": "Doctor Category - Dr. Count Wise",
                            "subCaption": "",
                            "startingAngle": "120",

                            "showLegend": "1",
                            "enableMultiSlicing": "0",

                            //To show the values in percentage
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",

                            "showPercentValues": "0",
                            "showPercentInTooltip": "0",
                            "plotTooltext": "Category : $label<br>Doctor Count: $value",
                            "theme": "fint",
                            "formatNumber": "0",
                            "formatNumberScale": "0",
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

    function Doc_Spec() {
        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });

        $.ajax({

            type: 'POST',

            url: "Doctor_Met.aspx/DocSpec",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'radar',

                    renderAt: 'chart-container',

                    width: '800',

                    height: '600',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": "Doctor Speciality - Dr. Count wise",
                            "subCaption": "",


                            "showLegend": "1",
                            "enableMultiSlicing": "0",

                            //To show the values in percentage
                            "paletteColors": "#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",

                            "showPercentValues": "0",
                            "showPercentInTooltip": "0",
                            "plotTooltext": "Speciality : $label<br>Doctor Count: $value",
                            "theme": "fint",
                            "formatNumber": "0",
                            "formatNumberScale": "0",
                            "legendPosition": "right",
                            "legendScrollBgColor": "#cccccc",
                            "legendScrollBarColor": "#999999",
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

    function Doc_Cls() {
        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });

        $.ajax({

            type: 'POST',

            url: "Doctor_Met.aspx/DocCls",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'doughnut3d',

                    renderAt: 'chart-container',

                    width: '600',

                    height: '400',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": "Doctor Class - Dr.Count wise",
                            "subCaption": "",
                            "startingAngle": "120",

                            "showLegend": "1",
                            "enableMultiSlicing": "0",

                            //To show the values in percentage
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",

                            "showPercentValues": "0",
                            "showPercentInTooltip": "0",
                            "plotTooltext": "Class : $label<br>Doctor Count: $value",
                            "theme": "fint",
                            "formatNumber": "0",
                            "formatNumberScale": "0",
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

    function Doc_Camp() {

        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
        $.ajax({

            type: 'POST',

            url: "Doctor_Met.aspx/Doc_Camp",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'scrollline2d',

                    renderAt: 'chart-container',

                    width: '600',

                    height: '400',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": "Doctor Campaign - Dr.Count wise",
                            "xAxisName": "Campaign",
                            "yAxisName": "No of Drs",
                            "showValues": "1",
                            "numberPrefix": "",
                            "showLegend": "1",
                            "theme": "fint",
                            //   "showBorder": "1",
                            // "showShadow": "1",
                            "bgColor": "#ffffff",
                            "paletteColors": "#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                            // "showCanvasBorder": "1",
                            //   "showAxisLines": "1",
                            "showAlternateHGridColor": "0",
                            //   "divlineAlpha": "100",
                            //   "divlineThickness": "1",
                            //  "divLineIsDashed": "1",
                            //   "divLineDashLen": "1",
                            //   "divLineGapLen": "1",
                            //   "lineThickness": "3",
                            "flatScrollBars": "1",
                            "scrollheight": "10",
                            "numVisiblePlot": "12",
                            "showHoverEffect": "1",

                            "showLabels": "1",
                            "showLegend": "1",
                            "showValues": "1",
                            "showPercentValues": "0",
                            "showPercentInTooltip": "0",
                            "plotTooltext": "Campaign : $label<br>Drs Cnt : $value",
                            "labelDisplay": "rotate",
                            "rotateValues": "1",
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

    function Stack() {
        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
        var Field = $("#ddlFieldForce").val();

        var Data = Field;

        $.ajax({

            type: 'POST',

            url: "Doctor_Met.aspx/Input",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    "type": "scrollstackedcolumn2d",
                    "renderAt": "chart-container",
                    "width": "600",
                    "height": "500",
                    "dataFormat": "json",
                    "dataSource": chartData
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
    function Stok_St() {
     

        $.ajax({

            type: 'POST',

            url: "Doctor_Met.aspx/StockSt",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'maps/india',

                    renderAt: 'chart-container',

                    width: '600',

                    height: '400',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "hovercolor": "CCCCCC",
                            "fillalpha": "80",
                            "connectorcolor": "000000",
                            "caption": "State wise - Stockist Count",

                            "legendposition": "BOTTOM",
                            "showshadow": "0",
                            "showlegend": "1",
                            "bordercolor": "FFFFFF",
                            "canvasbordercolor": "FFFFFF",
                            "usehovercolor": "1",
                            "showbevel": "0",
                            "animation": "1",


                            "nullEntityColor": "#85929E",
                            "entityFillColor": "#D35400",
                            "entityFillHoverColor": "#F9E79F",
                            "nullEntityAlpha": "50",
                            "formatNumber": "0",
                            "formatnumberscale": "0",


                            "useSNameInLabels": "1",
                            "showborder": "1"

                        },

                        "data": chartData

                    }

                }

            );

                fusioncharts.render();
               
            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-container").html(xhr.responseText);

            }

        });
        $.ajax({

            type: 'POST',

            url: "Doctor_Met.aspx/ssgrid",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'ssgrid',
                    renderAt: 'chart-container-St',

                    width: '300',

                    height: '300',

                    dataFormat: 'json',

                    dataSource: {

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

            },

            error: function (xhr, ErrorText, thrownError) {

                $("#chart-container-St").html(xhr.responseText);

            }

        });
    }
   
</script>
<script type="text/javascript">

    function Muliti() {

        var Field = $("#ddlFieldForce").val();

        var Data = Field;

        $.ajax({

            type: 'POST',

            url: "Doctor_Met.aspx/Multi",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({
                    type: 'ScrollCombi2D',
                    renderAt: 'chart-container',
                    width: '600',
                    height: '500',
                    dataFormat: 'json',
                    "dataSource": chartData
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
</style>
<body>
    <form id="form1" runat="server">
    <ucl:Menu ID="menu1" runat="server" />
    <br />
    <center>
        <div>
            <asp:Panel ID="pnlchart" runat="server" Style="vertical-align: top">
                <div class="roundbox boxshadow" style="width: 90%; height: 700px; border: solid 2px steelblue;">
                    <div class="gridheaderleft">
                        <center>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblView" runat="server" Text="View " SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td>
                                        <select id="ddlwise" name="form_select" style="font-size: 8pt; color: black; font-family: Verdana;"
                                            onchange="showDrop(this)">
                                            <option value="0" selected="selected">ALL</option>
                                            <option value="1">Team Wise</option>
                                        </select>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSF" runat="server" Text="Field Force Name " SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Mode" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMode" runat="server" SkinID="ddlRequired">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Product Category - SKU wise"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Product Group - SKU wise"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Product Brand - SKU Wise"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Doctor Category - Dr. Count Wise"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="Doctor Speciality - Dr. Count wise"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="Doctor Class - Dr.Count wise"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="Doctor Campaign - Dr.Count wise"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="State wise - Stockist Count"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <input type="button" id="btnGo" style="background-color: LightBlue" value="Go" />
                                    </td>
                                </tr>
                            </table>
                        </center>
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
                                                    <asp:Panel ID="pnlSingle" runat="server">
                                                    <div id="chart-container">
                                                    </div>
                                                    </asp:Panel>
                                                </td>
                                                <td style="border: none;">
                                                  <asp:Panel ID="pnlgrid" runat="server">
                                                    <div class='chartCont' id='chart-container-St'>
                                                    </div>
                                                    </asp:Panel>
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
                <%--<img src="../Images/loading/tenor.gif" width="350px" height="250px" alt="" />--%>
                  <img src="../Images/loading/Graph_Loading.gif" width="350px" height="250px" alt="" />
            </div>
        </div>
    </center>
    </form>
</body>
</html>
