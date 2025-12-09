<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fw_Analysis_SFE.aspx.cs"
    Inherits="DashBoard_Fw_Analysis_SFE" %>

<%@ Register Src="~/UserControl/DashBoard.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        body
        {
            margin: 0;
            padding: 0;
            width: 100%;
            font-family: Tahoma, Helvetica, Arial, sans-serif;
        }
        h1, h2, h3, h4, h5
        {
            margin: 0;
            padding: 0;
            font-weight: bold;
        }
        .chartCont
        {
            padding: 0px 12px;
        }
        .border-bottom
        {
            border-bottom: 1px dashed rgba(0, 117, 194, 0.2);
        }
        .border-right
        {
            border-right: 1px dashed rgba(0, 117, 194, 0.2);
        }
        #container
        {
            width: 1200px;
            margin: 0 auto;
            position: relative;
        }
        #container > div
        {
            width: 100%;
            background-color: #ffffff;
        }
        #logoContainer
        {
            float: left;
        }
        #logoContainer img
        {
            padding: 0 10px;
        }
        #logoContainer div
        {
            position: absolute;
            top: 8px;
            left: 95px;
        }
        #logoContainer div h2
        {
            color: #0075c2;
        }
        #logoContainer div h4
        {
            color: #0e948c;
        }
        #logoContainer div p
        {
            color: #719146;
            font-size: 12px;
            padding: 5px 0;
        }
        
        #userDetail
        {
            float: right;
        }
        #userDetail img
        {
            position: absolute;
            top: 16px;
            right: 130px;
        }
        #userDetail div
        {
            position: absolute;
            top: 15px;
            right: 20px;
            font-size: 14px;
            font-weight: bold;
            color: #0075c2;
        }
        #userDetail div p
        {
            margin: 0;
        }
        #userDetail div p:nth-child(2)
        {
            color: #0e948c;
        }
        #header div:nth-child(3)
        {
            clear: both;
            border-bottom: 1px solid #0075c2;
        }
        #content div
        {
            display: inline-block;
        }
        #content > div
        {
            margin: 0px 20px;
        }
        #content > div:nth-child(1) > div
        {
            margin: 20px 0 0;
        }
        #content > div:nth-child(2) > div
        {
            margin: 0 0 20px;
        }
        #footer p
        {
            margin: 0;
            font-size: 9pt;
            color: black;
            padding: 5px 0;
            text-align: center;
        }
    </style>
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
    <script type="text/javascript">
        function showDrop(select) {
            if (select.value == 1) {
                document.getElementById('ddlTMonth').style.display = "block";
                document.getElementById('ddlTYear').style.display = "block";
                document.getElementById('lbltmon').style.display = "block";
                document.getElementById('btnGo').style.display = "block";
                document.getElementById('btnSubmit').style.display = "none";
                document.getElementById('ddltype').style.display = "block";
                document.getElementById('ddltypeSingle').style.display = "none";
                document.getElementById('ddlmode').style.display = "block";
                document.getElementById('ddlmodeSingle').style.display = "none";
            } else {
                document.getElementById('ddlTMonth').style.display = "none";
                document.getElementById('ddlTYear').style.display = "none";
                document.getElementById('lbltmon').style.display = "none";
                document.getElementById('btnGo').style.display = "none";
                document.getElementById('btnSubmit').style.display = "block";
                document.getElementById('ddltype').style.display = "none";
                document.getElementById('ddltypeSingle').style.display = "block";
                document.getElementById('ddlmode').style.display = "none";
                document.getElementById('ddlmodeSingle').style.display = "block";
            }
        } 
    </script>
    <script src="JS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/fusioncharts.js"></script>
<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/themes/fusioncharts.theme.fint.js?cacheBust=56"></script>--%>
    <script src="js1/fusioncharts.js" type="text/javascript"></script>
    <script src="js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>
  
    <script type="text/javascript">

        $(document).ready(function () {


            $("#btnGo").click(function () {

                var ddlType = $("#ddltype").val();
                var ddlmode = $("#ddlmode").val();
                var HideSin = document.getElementById("pnlSingle");
                var Hide = document.getElementById("pnlAll");
                if (ddlType == "0" && ddlmode == "0") {
                    
                    HideSin.style.display = "none";
                    Hide.style.display = "block";
                    MsField();
                }
                else if (ddlType == "1" && ddlmode == "0") {
                 
                    HideSin.style.display = "none";
                    Hide.style.display = "block";
                    AllMsseries();
                }
                else if (ddlType == "2" && ddlmode == "0") {
                 
                    HideSin.style.display = "none";
                    Hide.style.display = "block";
                    AllMsarea();
                }
                else if (ddlType == "3" && ddlmode == "0") {
                   
                    HideSin.style.display = "none";
                    Hide.style.display = "block";
                    AllMsLine();
                }
                else if (ddlType == "4" && ddlmode == "0") {
                   
                    HideSin.style.display = "none";
                    Hide.style.display = "block";
                    AllVertical();
                }
                else if (ddlType == "5" && ddlmode == "0") {
                   
                    HideSin.style.display = "none";
                    Hide.style.display = "block";
                    AllMsstack();
                }
                else if (ddlType == "1") {
                  
                    Hide.style.display = "none";
                    HideSin.style.display = "block";
                    FW();
                }
                else if (ddlType == "2") {
                    
                    Hide.style.display = "none";
                    HideSin.style.display = "block";
                    area();
                }
                else if (ddlType == "3") {
                   
                    Hide.style.display = "none";
                    HideSin.style.display = "block";
                    Line_Multiple();
                }
                else if (ddlType == "4") {
                
                    Hide.style.display = "none";
                    HideSin.style.display = "block";
                    MsBar();
                }
                else if (ddlType == "5") {
               
                    Hide.style.display = "none";
                    HideSin.style.display = "block";
                    Stack();
                }
                else if (ddlType == "6") {
                  
                    Hide.style.display = "none";
                    HideSin.style.display = "block";
                    MultiFreq();
                }
            });

        });
    </script>
    <script type="text/javascript">
        function AllMsseries() {
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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var ddltype = $("#ddltype").val();


            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName + "^" + ddltype;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/All",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'ScrollCombi2D',
                        renderAt: 'sales-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#sales-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CovArea",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'ScrollCombi2D',
                        renderAt: 'cs-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#cs-chart-container").html(xhr.responseText);

                }


            });


            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/MissLine",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'ScrollCombi2D',
                        renderAt: 'footfall-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#footfall-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CallAvgStack",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'ScrollCombi2D',
                        renderAt: 'trans-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#trans-chart-container").html(xhr.responseText);

                }


            });

        }
    </script>
    <script type="text/javascript">
        function AllMsarea() {
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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var ddltype = $("#ddltype").val();


            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName + "^" + ddltype;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/All",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msArea',
                        renderAt: 'sales-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#sales-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CovArea",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msArea',
                        renderAt: 'cs-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#cs-chart-container").html(xhr.responseText);

                }


            });


            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/MissLine",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msArea',
                        renderAt: 'footfall-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#footfall-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CallAvgStack",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msArea',
                        renderAt: 'trans-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#trans-chart-container").html(xhr.responseText);

                }


            });

        }
    </script>
    <script type="text/javascript">
        function AllMsLine() {
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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var ddltype = $("#ddltype").val();


            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName + "^" + ddltype;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/All",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msLine',
                        renderAt: 'sales-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#sales-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CovArea",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msLine',
                        renderAt: 'cs-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#cs-chart-container").html(xhr.responseText);

                }


            });


            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/MissLine",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msLine',
                        renderAt: 'footfall-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#footfall-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CallAvgStack",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msLine',
                        renderAt: 'trans-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#trans-chart-container").html(xhr.responseText);

                }


            });

        }
    </script>
    <script type="text/javascript">
        function AllMsstack() {
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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var ddltype = $("#ddltype").val();


            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName + "^" + ddltype;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/All",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'scrollstackedcolumn2d',
                        renderAt: 'sales-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#sales-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CovArea",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'scrollstackedcolumn2d',
                        renderAt: 'cs-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#cs-chart-container").html(xhr.responseText);

                }


            });


            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/MissLine",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'scrollstackedcolumn2d',
                        renderAt: 'footfall-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#footfall-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CallAvgStack",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'scrollstackedcolumn2d',
                        renderAt: 'trans-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#trans-chart-container").html(xhr.responseText);

                }


            });

        }
    </script>
    <script type="text/javascript">
        function MsField() {
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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/All",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'ScrollCombi2D',
                        renderAt: 'sales-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#sales-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CovArea",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'scrollarea2d',
                        renderAt: 'cs-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#cs-chart-container").html(xhr.responseText);

                }


            });


            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/MissLine",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msline',
                        renderAt: 'footfall-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#footfall-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CallAvgStack",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'scrollstackedcolumn2d',
                        renderAt: 'trans-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#trans-chart-container").html(xhr.responseText);

                }


            });

        }
    </script>
    <script type="text/javascript">
        function AllVertical() {
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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var ddltype = $("#ddltype").val();


            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName + "^" + ddltype;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/All",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msbar3d',
                        renderAt: 'sales-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#sales-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CovArea",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msbar3d',
                        renderAt: 'cs-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#cs-chart-container").html(xhr.responseText);

                }


            });


            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/MissLine",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msbar3d',
                        renderAt: 'footfall-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#footfall-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/CallAvgStack",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msbar3d',
                        renderAt: 'trans-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#trans-chart-container").html(xhr.responseText);

                }


            });

        }
    </script>
    <script type="text/javascript">
        function all() {
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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/all",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'ScrollCombi2D',
                        renderAt: 'sales-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#sales-chart-container").html(xhr.responseText);

                }


            });
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/all",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'scrollarea2d',
                        renderAt: 'trans-chart-container',
                        width: '500',
                        height: '400',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#trans-chart-container").html(xhr.responseText);

                }


            });

        }
    </script>
    <script type="text/javascript">

        function FW() {
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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/Input",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'ScrollCombi2D',
                        renderAt: 'chart-container',
                        width: '800',
                        height: '700',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-Container").html(xhr.responseText);

                }


            });
        }


        function area() {

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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();

            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/Area",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msArea',
                        renderAt: 'chart-container',
                        width: '600',
                        height: '500',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-Container").html(xhr.responseText);

                }


            });
        }

        function Line_Multiple() {

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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();

            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/Line",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msline',
                        renderAt: 'chart-container',
                        width: '600',
                        height: '500',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-Container").html(xhr.responseText);

                }


            });
        }

        function MsBar() {
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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();

            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/MBar",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msbar3d',
                        renderAt: 'chart-container',
                        width: '800',
                        height: '700',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");
                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-Container").html(xhr.responseText);

                }


            });
        }
        function Drag() {
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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();

            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/Drag",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'msstepline',
                        renderAt: 'chart-container',
                        width: '550',
                        height: '350',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");
                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-Container").html(xhr.responseText);

                }


            });
        }
      
       
        function Stack() {
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

            var TMonth = $("#ddlTMonth").val();
            var TYear = $("#ddlTYear").val();

            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName;
            $.ajax({

                type: 'POST',

                url: "Fw_Analysis_SFE.aspx/Stacked",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({
                        type: 'scrollstackedcolumn2d',
                        renderAt: 'chart-container',
                        width: '800',
                        height: '700',
                        dataFormat: 'json',
                        "dataSource": chartData
                    }
            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-Container").html(xhr.responseText);

                }


            });
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {



            $("#btnSubmit").click(function () {
                //  HideDet.style.display = "none";
                var ddlType = $("#ddltypeSingle").val();

                if (ddlType == "4") {
                    Doughnut3D();
                }
                else if (ddlType == "2") {
                    Pie();
                }
                else if (ddlType == "1") {
                    Bar();
                }
                else if (ddlType == "5") {
                    Pyramid3D();
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
                    Doughnut();
                }
                else if (ddlType == "9") {
                    Pyramid();
                }
                else if (ddlType == "10") {
                    Funnel();
                }
                else if (ddlType == "11") {
                    BarFreq();
                }
                else if (ddlType == "12") {
                    BarFreqArea();
                }
            });

        });
</script>
<script type="text/javascript">

    function Bar() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var mode = $("#ddlmodeSingle").val();
        var modename = $('#ddlmodeSingle option:selected').text();

        
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;
        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/SingleFW",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');
                var fusioncharts = new FusionCharts({

                    "type": "Column3d",
                    "renderAt": "chart-container",
                    "width": "600px",
                    "height": "500px",
                    "dataFormat": "json",
                    "dataSource": {
                        "chart": {
                            "caption": modename,
                            "subcaption": "",
                            "xaxisname": "Sf Name",
                            "yaxisname": "",
                            "placeValuesInside": "0",

                            "palette": "5",
                            //Configure scrollbar
                            "formatNumber": "0",
                            "formatNumberScale": "0",
                            "useRoundEdges": "1",
                            //  "theme": "fint",
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",

                            "labelDisplay": "rotate"
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

        var mode = $("#ddlmodeSingle").val();
        var modename = $('#ddlmodeSingle option:selected').text();

        var Data = Month + "^" + Year + "^" + Field + "^" + mode;
        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/SingleFW",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    "type": "scrollarea2d",
                    "renderAt": "chart-container",
                    "width": "600",
                    "height": "500",
                    "dataFormat": "json",
                    "dataSource": {
                        "chart": {
                            "caption": modename,
                            "subCaption": "",
                            "captionFontSize": "14",
                            "subcaptionFontSize": "14",
                            "subcaptionFontBold": "0",
                            "xAxisname": "Sf Name",
                            "pYAxisName": "",

                           
                            "numberPrefix": "",
                            "paletteColors": "#0075c2",
                            "bgcolor": "#ffffff",
                            "showBorder": "1",
                            "showCanvasBorder": "0",
                            "usePlotGradientColor": "0",
                            "plotBorderAlpha": "10",
                            "showAxisLines": "0",
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

        var mode = $("#ddlmodeSingle").val();
        var modename = $('#ddlmodeSingle option:selected').text();
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;

        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/SingleFW",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'doughnut2d',

                    renderAt: 'chart-container',

                    width: '800',

                    height: '600',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": modename,
                            "subCaption": "",
                            "numberPrefix": "",
                            "startingAngle": "310",
                            "decimals": "0",
                            "defaultCenterLabel": "",
                            "showValues": "1",
                            "centerLabel": "SfName from $label: $value",
                            "plotTooltext": "SfName : $label<br> : $value",
                            "showLabels": "1",
                            "showLegend": "1",
                            "showPercentValues": "0",
                            "theme": "fint"
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

        var mode = $("#ddlmodeSingle").val();
        var modename = $('#ddlmodeSingle option:selected').text();
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;

        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/SingleFW",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'doughnut3d',

                    renderAt: 'chart-container',

                    width: '800',

                    height: '600',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": modename,
                            "subCaption": "",
                            "numberPrefix": "",
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                            "bgColor": "#ffffff",
                       
                            "use3DLighting": "0",
                            "showShadow": "0",
                            "enableSmartLabels": "0",
                            "startingAngle": "310",
                            "showLabels": "1",
                            "showPercentValues": "0",
                            "showLegend": "1",
                            "legendShadow": "0",
                            "legendBorderAlpha": "0",
                            "decimals": "0",
                            "captionFontSize": "14",
                            "subcaptionFontSize": "14",
                            "subcaptionFontBold": "0",
                            "toolTipColor": "#ffffff",
                            "toolTipBorderThickness": "0",
                            "toolTipBgColor": "#000000",
                            "toolTipBgAlpha": "80",
                            "toolTipBorderRadius": "2",
                            "toolTipPadding": "5",
                            "plotTooltext": "SfName : $label<br> : $value"
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
        var mode = $("#ddlmodeSingle").val();
        var modename = $('#ddlmodeSingle option:selected').text();
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;
        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/SingleFW",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',
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
                            "caption": modename,
                            "formatnumberscale": "0",
                         
                            "showLegend": "1",
                            "theme": "fint",
                            "showPercentValues": "0",
                            "showPercentInToolTip": "0",
                            //Setting legend to appear on right side
                           // "legendPosition": "right",
                            //Caption for legend
                            "legendCaption": "SfName: ",
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

        var mode = $("#ddlmodeSingle").val();
        var modename = $('#ddlmodeSingle option:selected').text();
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;
        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/SingleFW",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',
            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'pie3d',

                    renderAt: 'chart-container',

                    width: '800',

                    height: '600',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": modename,
                            "subCaption": "",
                            "startingAngle": "120",

                            "showLegend": "1",
                            "enableMultiSlicing": "0",
                            "slicingDistance": "15",
                            //To show the values in percentage
                            "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                           
                            "showPercentValues": "0",
                            "showPercentInTooltip": "0",
                            "plotTooltext": "SfName : $label",
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

        var mode = $("#ddlmodeSingle").val();
        var modename = $('#ddlmodeSingle option:selected').text();
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;

        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/SingleFW",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'pyramid',

                    renderAt: 'chart-container',

                    id: 'wealth-pyramid-chart',

                    width: '800',

                    height: '600',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "theme": "fint",
                            "caption":modename,
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
                            "showPercentValues": "0",
                            "chartLeftMargin": "40",
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

        var mode = $("#ddlmodeSingle").val();
        var modename = $('#ddlmodeSingle option:selected').text();
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;

        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/SingleFW",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'pyramid',

                    renderAt: 'chart-container',

                    id: 'wealth-pyramid-chart',

                    width: '800',

                    height: '600',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "theme": "fint",
                            "caption": modename,
                            "captionOnTop": "0",
                            "captionPadding": "25",
                            "alignCaptionWithCanvas": "1",
                            "subcaption": "",
                            "subCaptionFontSize": "12",
                            "borderAlpha": "20",
                            "is2D": "0",
                            "bgColor": "#ffffff",
                            "showValues": "1",
                     
                            "plotTooltext": "$label SfName $value ",
                            "showPercentValues": "0",
                            "chartLeftMargin": "40",
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

        var mode = $("#ddlmodeSingle").val();

        var modename = $('#ddlmodeSingle option:selected').text();
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;

        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/SingleFW",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    "type": "scrollline2d",
                    "renderAt": "chart-container",
                    "width": "800",
                    "height": "600",
                    "dataFormat": "json",
                    "dataSource": {
                        "chart": {
                            "caption": modename,
                            "subCaption": "",
                            "xAxisName": "Sf_Name",
                            "yAxisName": "",
                            "showValues": "1",
                            "numberPrefix": "",
                            "showBorder": "1",
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
                            "showPercentValues": "0",
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
        var mode = $("#ddlmodeSingle").val();
        var modename = $('#ddlmodeSingle option:selected').text();
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;

        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/SingleFW",

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    type: 'funnel',

                    renderAt: 'chart-container',



                    width: '800',

                    height: '600',

                    dataFormat: 'json',

                    dataSource: {

                        "chart": {
                            "caption": modename,
                            "subcaption": "",
                            "numberprefix": "",
                            "is2D": "1",
                            "streamlinedData": "0",

                            "showLegend": "1",
                            "showLabels": "1",
                            "showValues": "1",
                            "theme": "fint",
                            "plotTooltext": "Sf_Name : $label<br> : $value",
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

    function BarFreq() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var mode = $("#ddlmodeSingle").val();
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;
        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/Freq",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    "type": "scrollcombidy2d",
                    "renderAt": "chart-container",
                    "width": "600",
                    "height": "500",
                    "dataFormat": "json",
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

    function BarFreqArea() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var mode = $("#ddlmodeSingle").val();
        var Data = Month + "^" + Year + "^" + Field + "^" + mode;
        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/FreqArea",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    "type": "scrollcombidy2d",
                    "renderAt": "chart-container",
                    "width": "600",
                    "height": "500",
                    "dataFormat": "json",
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

    function MultiFreq() {

        var Month = $("#ddlFMonth").val();
        var Year = $("#ddlFYear").val();

        var Field = $("#ddlFieldForce").val();

        var mode = $("#ddlmode").val();

        var TMonth = $("#ddlTMonth").val();
        var TYear = $("#ddlTYear").val();


        var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
        var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
        var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();
        var TNName = $('#<%=ddlTMonth.ClientID%> :selected').text();
        var ddltype = $("#ddltype").val();


        var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + SsfName + "^" + modeName + "^" + FMName + "^" + TNName + "^" + ddltype;
        $.ajax({

            type: 'POST',

            url: "Fw_Analysis_SFE.aspx/MultiFreq",

            contentType: "application/json; charset=utf-8",

            dataType: "json",
            data: '{objData:' + JSON.stringify(Data) + '}',

            success: function (data) {

                var chartData = eval("(" + data.d + ')');

                var fusioncharts = new FusionCharts({

                    "type": "scrollcombidy2d",
                    "renderAt": "chart-container",
                    "width": "600",
                    "height": "500",
                    "dataFormat": "json",
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
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnl" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
    </asp:Panel>
    <div style="display: block; float: right;">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/menu2.png" OnClientClick="HideMenu_Det(); return false;" />
    </div>
    <div>
        <asp:Panel ID="pnlchart" runat="server" Style="vertical-align: top">
            <div class="roundbox boxshadow" style="height: 800px; border: solid 2px steelblue;">
                <div class="gridheaderleft">
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="btnHide" runat="server" ImageUrl="~/Images/12_eye.png" OnClientClick="DisplayDetail(); return false;" />
                            </td>
                             <td>
                               <select id="ddlwise" name="form_select" style="FONT-SIZE: 8pt;COLOR: black;FONT-FAMILY: Verdana;" onchange="showDrop(this)">
                                    <option value="0">Geographywise</option>
                                    <option value="1" selected="selected" >Divisionwise</option>
                                </select>
                             </td>
                            <td>
                                <asp:Label ID="lblSF" runat="server" Text="SFName" SkinID="lblMand"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="250px" SkinID="ddlRequired">
                                </asp:DropDownList>
                            </td>
                          
                            <%--  <asp:DropDownList ID="ddlwise" runat="server" SkinID="ddlRequired">
                       <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Single"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Multiple"></asp:ListItem>
                    </asp:DropDownList>--%>
                         
                            <td>
                                <asp:Label ID="lblFMonth" runat="server" Text="From Mnth/Yr " SkinID="lblMand"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" Width="70px">
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
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" Width="70px">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lbltmon" runat="server" Text="To Mnth/Yr " SkinID="lblMand"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTMonth" runat="server" Width="70px" SkinID="ddlRequired">
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
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTYear" runat="server" SkinID="ddlRequired" Width="70px">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lbltyp" runat="server" Text="Type" SkinID="lblMand"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddltype" runat="server" Width="100px" SkinID="ddlRequired">
                                    <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                                    <%--  <asp:ListItem Value="0" Text="ALL"></asp:ListItem>--%>
                                    <asp:ListItem Value="1" Text="MS Series"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Area"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Line"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="MS Series Vertical"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Stacked"></asp:ListItem>
                                    <%-- <asp:ListItem Value="6" Text="Bar Norms"></asp:ListItem>--%>
                                </asp:DropDownList>
                                 <select id="ddltypeSingle" name="form_select" style="FONT-SIZE: 8pt;COLOR: black;FONT-FAMILY: Verdana;display:none" >
                                    <option value="0">--Select--</option>
                                     <option value="1" >Bar Chart</option>
                                    <option value="2" >Pie Chart</option>
                                       <option value="7" >Pie 3D Chart</option>
                                     <option value="3" >Line Chart</option>
                                     <option value="4" >Doughnut Chart</option>
                                     <option value="5" >Pyramid</option>
                                     <option value="10" >Funnel</option>
                                     <option value="11" >Norms Line</option>
                                      <option value="12" >Norms Area</option>
                                </select>
                            </td>
                            <td>
                                <asp:Label ID="lblmode" runat="server" Text="Mode" SkinID="lblMand"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlmode" runat="server" Width="100px" SkinID="ddlRequired">
                                    <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="ALL"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Fieldworking Days"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Call Average"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Coverage"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Missed Calls"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Frequency"></asp:ListItem>
                                </asp:DropDownList>
                               <select ID="ddlmodeSingle"  name="form_select" style="FONT-SIZE: 8pt;COLOR: black;FONT-FAMILY: Verdana;display:none" >
                                    <option value="0">--Select--</option>
                                    <option value="1">Fieldworking Days</option>
                                    <option value="2">Call Average</option>
                                 
                              </select>
                                  <%-- <asp:DropDownList ID="ddlmodeSingle" runat="server" SkinID="ddlRequired" >
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
                        </asp:DropDownList>--%>
                            </td>
                            <td>
                                <input type="button" id="btnGo" class="button5" value="Go" />
                            </td>
                              <td>
                                <input type="button" id="btnSubmit" class="button5" style="display:none;background-color:Yellow" value="Go"  />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="boxcontenttext" style="background: White;">
                    <div id="pnlPreviewSurveyData">
                        <center>
                            <table border="0" cellpadding="3" class="" cellspacing="3" style="border-style: none">
                                <tr>
                                    <td id="HideChart" runat="server" style="border: none;">
                                        <table>
                                            <tr>
                                                <td style="border: none;">
                                                    <asp:Panel ID="pnlSingle" runat="server">
                                                    <center>
                                                        <div id="chart-container">
                                                        </div>
                                                        </center>
                                                    </asp:Panel>
                                                    <div>
                                                        <asp:Literal ID="FCLiteral1" runat="server"></asp:Literal>
                                                    </div>
                                                    <asp:Panel ID="pnlAll" runat="server">
                                                        <center>
                                                            <div class='border-bottom' id='content'>
                                                                <div class='border-bottom'>
                                                                    <div class='chartCont border-right' id='sales-chart-container'>
                                                                    </div>
                                                                    <div class='chartCont' id='trans-chart-container'>
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class='chartCont border-right' id='footfall-chart-container'>
                                                                    </div>
                                                                    <div class='chartCont' id='cs-chart-container'>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </center>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </center>
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
    </form>
</body>
</html>
