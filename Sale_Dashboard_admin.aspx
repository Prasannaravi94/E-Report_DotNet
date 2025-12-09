<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sale_Dashboard_admin.aspx.cs" Inherits="Sale_Dashboard_admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <style type="text/css">
        .tableStyle1
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 300px;
        }
        /*Style for Table Head - TH*/
        .tableStyle1 th
        {
            text-align: left;
            background-color: #006699;
            color: #fff;
            text-align: left;
            padding: 25px;
        }
        /*TD and TH Style*/
        .tableStyle1 td, .tableStyle1 th
        {
            border: 1px solid #dedede; /*Border color*/
            padding: 15px;
        }
        /*Table Even Columns Styles*/
        .tableStyle1 td:nth-child(even)
        {
            background-color: #afafaf;
        }
        /*Table ODD Columns Styles*/
        .tableStyle1 td:nth-child(odd)
        {
            background-color: #cfcfcf;
        }
        /*Table Column(Data) HOver Style*/
        .tableStyle1 td:hover
        {
            background-color: #e5423f;
        }
        /* Table14 */
        
        .tableStyle14
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 300px;
        }
        /*Style for Table Head - TH*/
        .tableStyle14 th
        {
            text-align: left;
            background-color: #008B8B;
            color: #fff;
            text-align: left;
            padding: 25px;
        }
        /*TD and TH Style*/
        .tableStyle14 td, .tableStyle14 th
        {
            border: 1px solid #dedede; /*Border color*/
            padding: 15px;
        }
        /*Table Even Columns Styles*/
        .tableStyle14 td:nth-child(even)
        {
            background-color: #afafaf;
        }
        /*Table ODD Columns Styles*/
        .tableStyle14 td:nth-child(odd)
        {
            background-color: #cfcfcf;
        }
        /*Table Column(Data) HOver Style*/
        .tableStyle14 td:hover
        {
            background-color: #e5423f;
        }
        /* Table15 */
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
            background: #003399 url(images/vertgradient.png) repeat-x;
            text-align: center;
            font-weight: bold;
            text-decoration: none;
            color: khaki;
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
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $(".loader").delay(2000).fadeOut("slow");
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            var pData = [];
            var today = new Date();

            pData[0] = today.getMonth() + 1;
            pData[1] = today.getFullYear();
            pData[2] = $("#ddlFieldForce").val();
            pData[3] = today.getDate();
            
            var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
            var function_name = "getChartVal";
            $.ajax({
                type: "POST",
                url: "Sale_Dashboard_admin.aspx/" + function_name,
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: OnErrorCall_
            });

            function OnSuccess_(response) {
                var aData = response.d;
                var arr = [];
                var sf_code, tar, sal, ach, psal, grow;
                var sf_name = $("#ddlFieldForce").text();
                $.map(aData, function (item, index) {
                    sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; 



                });
                var myJsonString = JSON.stringify(arr);
                var jsonArray = JSON.parse(JSON.stringify(arr));
                document.getElementById("lbltar").textContent = tar
                document.getElementById("lblsal").textContent = sal
                document.getElementById("lblach").textContent = ach
                document.getElementById("lblLS").textContent = psal
                document.getElementById("lblgr").textContent = grow
                chrtClock(tar, sal, ach, sf_code);
                chrtTar(ach);
            }
            function OnErrorCall_(response) {
                alert("Error: No Data Found!");
            }
            e.preventDefault();



        });
        //*
    </script>
    <script src="JsFiles/highcharts.js" type="text/javascript"></script>
    <%-- This for Config --%>
    <script src="JsFiles/exporting.js" type="text/javascript"></script>
    <%-- This for Exporting --%>
    <script src="JsFiles/drilldown.js" type="text/javascript"></script>
    <%-- This for Drilldown --%>
    <script src="JsFiles/highcharts-more.js" type="text/javascript"></script>
    <%-- This for Gauge --%>
    <script src="JsFiles/data.js" type="text/javascript"></script>
    <%-- This for data --%>
    <script src="JsFiles/solid-gauge.js" type="text/javascript"></script>
    <%-- This for Gauge --%>
    <script src="JsFiles/highcharts-3d.js" type="text/javascript"></script>
    <%-- This for 3d --%>
    <script type="text/javascript">
        function chrtClock(target, sale, ach, sf_code) {
            Highcharts.setOptions({
                lang: {
                    numericSymbols: null //otherwise by default ['k', 'M', 'G', 'T', 'P', 'E']
                }
            });
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
               backgroundColor: 'lightpink',
                plotBorderWidth: 0,
                plotShadow: false,
                height: 450
            };
            var credits = {
                enabled: false
            };

            var title = {
                text: "Target Vs Sales",
                style: {

                    color: '#FF00FF',
                    fontWeight: 'bold',
                    textDecoration: 'underline'
                }
            };
           
           
            var exporting = {
                enabled: true
            };

            var pane = {
                background: [{
                    // default background
                }, {
                    backgroundColor: Highcharts.svg ? {
                        radialGradient: {
                            cx: 0.5,
                            cy: -0.4,
                            r: 1.9
                        },
                        stops: [
                               [0.5, 'rgba(255, 255, 255, 0.2)'],
                               [0.5, 'rgba(200, 200, 200, 0.2)']
                            ]
                    } : null
                }]
            };
            var s = sf_code.indexOf('MGR') >= 0
            if (s == true) {
                var mma = 100000000;
                var mmi = 10000000;
            }
            else {
                var mma = 1000000;
                var mmi = 100000;
            }

            // the value axis
            var yAxis = {
                labels: {

                    distance: -20

                },
                min: 0,


                max: mma,

                lineWidth: 0,

                showFirstLabel: false,
                minorTickInterval: 'auto',
                minorTickWidth: 1,
                minorTickLength: 5,
                minorTickPosition: 'inside',
                minorGridLineWidth: 0,
                minorTickColor: '#666',

                tickInterval: mmi,
                tickWidth: 2,
                tickPosition: 'inside',
                tickLength: 10,
                tickColor: '#666',
                title: {
                    text: 'Target vs Sales',
                    style: {
                        color: '#BBB',
                        fontWeight: 'normal',
                        fontSize: '10px',
                        lineHeight: '10px'
                    },
                    
                    y: 10
                }
            };

            var tooltip = {
                enabled: true,
                pointFormat: '<b>{point.y}</b><br/>'
                //pointFormat: '{series.name}: <b>{point.y}</b><br/>'
            };
            var series = [{
                data: [{
                    name: 'Target',
                    y: target,
                    dial: {
                        radius: '60%',
                        baseWidth: 6,
                        baseLength: '95%',
                        rearLength: 0,
                        backgroundColor: 'red'
                    }
                }, {
                    name: 'Sale',
                    y: sale,
                    dial: {
                        baseLength: '95%',
                        baseWidth: 3,
                        rearLength: 0,
                        backgroundColor: 'green'
                    }
                }, {
                    name: 'Ach(%)',
                    y: ach,
                    dial: {
                        radius: '30%',
                        baseWidth: 1,
                        rearLength: '5%',
                        backgroundColor: 'blue'
                    }
                }],
                animation: false,
                dataLabels: {
                    formatter: function () {

                        return '<span style="color:red">Target: ' + target + '</span><br/>' +

                                    '<span style="color:green">Sale: ' + sale + '</span><br/>' +
                                    '<span style="color:Blue">Ach(%): ' + ach + '</span>';
                    },
                    backgroundColor: {
                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                        stops: [
                               [0, '#DDD'],
                               [1, '#FFF']
                            ]
                    },
                    y: 22
                }
            }];

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
        function chrtTar(ach) {

            $('#container').highcharts({

                chart: {
                    type: 'gauge'
                },

                title: {
                    text: "Achievement(%)",

                    style: {

                        color: '#FF00FF',
                        fontWeight: 'bold',
                        textDecoration: 'underline'
                    }
                },
               
                exporting: {
                    enabled: false
                },

                pane: {
                    startAngle: -90,
                    endAngle: 90,
                    background: {
                        backgroundColor: "#ffffff",
                        borderWidth: 0,
                        shape: 'arc'
                    }
                },

                yAxis: {
                    min: 0,
                    max: 100,

                    minorTickInterval: 'auto',
                    minorTickWidth: 0,
                    minorTickLength: 10,
                    minorTickPosition: 'outside',
                    minorTickColor: '#666',

                    tickPixelInterval: 30,
                    tickWidth: 2,
                    tickPosition: 'outside',
                    tickLength: 0,
                    tickColor: '#666',
                  
                    lineWidth: 0,
                    labels: {
                           step: 2,
                        rotation: 'auto'
                      
                    },
                  
                   
                    minorTickWidth: 1,
                    tickWidth: 0,
                    plotBands: [{
                        from: 0,
                        to: 33,
                        innerRadius: '60%',
                        outerRadius: '102%',
                        color: '#DF5353' // red
                    }, {
                        from: 34,
                        to: 66,
                        innerRadius: '60%',
                        outerRadius: '102%',
                        color: '#DDDF0D' // yellow
                    }, {
                        from: 67,
                        to: 100,
                        innerRadius: '60%',
                        outerRadius: '102%',
                        color: '#55BF3B' // green
                    }]
                },

                plotOptions: {
                    gauge: {
                        dial: {
                            radius: '110%',
                            backgroundColor: '#464A4F',
                            borderColor: '#464A4F',
                            borderWidth: 0,
                            baseWidth: 15,
                            topWidth: 1,
                            baseLength: '10%', // of radius
                            rearLength: '0%'
                        },
                         animation: {
                duration: 2000
            },
                        pivot: {
                            backgroundColor: "#464A4F",
                            radius: 7.5
                        }
                    },

                    series: {
                        dataLabels: {
                            formatter: function () {
                                return '<span style="color:red">Achievement(%): ' + ach + '</span>';


                            },
                            enabled: false
                        }
                    }
                },


                series: [{
                    name: 'Achievement(%)',
                    data: [ach],
                    dataLabels: {
                        enabled: true,
                       
                        style: {
                            fontWeight: 'bold',
                            fontSize: '12px'
                        }
                      
                       

                    }
                }]

            });

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

      <br />
  
          <div style="margin-left:90%">
    <asp:ImageButton ID="btnBack" ImageUrl="Images/back3.jpg" runat="server" OnClick="btnBack_Click" /> 
   
     </div>  
    
    <center>
    <h2 style="color:Blue;font-weight:bold">Sale Dashboard</h2>
    </center>
    <br />
        <center>
            <div class="roundbox boxshadow" style="width: 90%; border: solid 2px steelblue;">
                <div class="gridheaderleft">
                    <asp:Label ID="LblUser" runat="server"> </asp:Label>
                     <center>
                    <asp:Label ID="lblsf" runat="server" ForeColor="White" >Team</asp:Label>
                   
                      <asp:DropDownList ID="ddlFieldForce" runat="server" Width="300px" SkinID="ddlRequired">
                                </asp:DropDownList>
                                 
                                <button id="submit_btn" style="background-color:Teal;color:White">Go</button>
                    </center>
                </div>
                <div class="boxcontenttext" style="background: White;">
                    <div id="pnlPreviewSurveyData">
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <div id="divChrtClk" class="col-md-4" style="min-width: 400px; height: 450px; margin: 0 auto;
                                        padding-top: 15px;">
                                    </div>
                                </td>
                                <td>
                                    <div id="container" style="width: 450px; height: 300px; margin: 0 auto">
                                    </div>
                                </td>
                                <td>
                                    <table class="tableStyle14" style="vertical-align: top">
                                        <thead>
                                            <tr>
                                                <th colspan="2">
                                                    <asp:Label ID="lbltarsale" runat="server"></asp:Label>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    Target
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbltar" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                 Primary Sale
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsal" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Achievement (%)
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblach" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Last Year Sale
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLS" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Growth
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblgr" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </center>
        <br />
    </div>
    </form>
</body>
</html>
