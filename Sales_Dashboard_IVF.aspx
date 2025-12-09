<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sales_Dashboard_IVF.aspx.cs" Inherits="Sales_Dashboard_IVF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="keywords" content="">
    <meta name="decription" content="">
    <meta name="designer" content="Asad Kabir">

 
    <link rel="icon" href="images/favicon.ico" />

    <!-- Include Bootstrap -->
    <link href="css/bootstrap_New.css" rel="stylesheet" />

    <!-- datepicker  -->
    <link rel="stylesheet" href="css/datepicker.css" />

    <!-- Main StyleSheet -->
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




        .drop-item ListBox, .drop-item ListItem, .drop-item button {
            /*width: 100%;*/
            /*font-size: 16px;*/
            /*font-weight: 400;*/
            /*background: #616870;*/
            /*background-position-x: 0%;*/
            /*background-position-y: 0%;*/
            /*background-repeat: repeat;*/
            /*background-image: none;*/
            /*background-size: auto;*/
            /*color: #FFF;*/
            /*display: inline-block;*/
            /*padding: 13px 15px;*/
            /*border: 1px solid #878D96;*/
            /*border-radius: 7px;*/
            /*-webkit-appearance: none;*/
            /*-moz-appearance: none;*/
            /*appearance: none;*/
            /*background-image: url(images/arrow.png);*/
            /*background-repeat: no-repeat;*/
            /*background-size: 20px;*/
            /*background-position: 94% 50%;*/
            /*outline: none;*/
        }
    </style>



    <style type="text/css">
        .savebutton {
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
            /*font-size: 18px;
background: #697077;
color: #FFF;
display: inline-block;
padding: 7px 18px;
border-radius: 5px;
transition: 0.2s all ease;
-webkit-transition: 0.2s all ease;*/
        }

        .Gobutton {
            /*width: 45px;
            height: 50px;
            border-radius: 3px;
            background-image: url(images/calender.png);
            background-image: linear-gradient(to top, #0077ff 0%, #28b5e0 100%);
            background: #33B1FF;
            cursor: pointer;
            border: 0px;
            color: #FFF;
            font-size: 20px;
            font-weight: 600;
            margin: 0 3px;
            padding: 0px;
            margin-top: 5px;
            margin-bottom: 5px;*/
            font-size: 18px;
            width: 50px;
            height: 50px;
            cursor: pointer;
            line-height: 50px;
            display: inline-block;
            text-align: center;
            background: #33B1FF;
            color: #FFF;
            border-radius: 6px;
            transition: 0.2s all ease;
            -webkit-transition: 0.2s all ease;
            position: absolute;
            top: 0;
        }

        .tableStyle14 {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 900px;
        }
            /*Style for Table Head - TH*/
            .tableStyle14 th {
                text-align: left;
                background-color: #008B8B;
                color: #fff;
                text-align: left;
                padding: 25px;
            }
            /*TD and TH Style*/
            .tableStyle14 td, .tableStyle14 th {
                border: 1px solid #dedede; /*Border color*/
                padding: 15px;
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
                    background-color: white;
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
                color: black;
                text-align: center;
                padding: 5px;
                border-radius: 10px 10px 0px 0px;
            }

            .single-block-area td {
                padding: 5px;
                background-color: #D3D3D3;
            }

            /*.single-block-area tr td {
                border-bottom: 1px solid #DCE2E8;
            }*/


            .single-block-area tr td {
                border-bottom: 1px solid #5b5f63;
                border-top: 1px solid #5b5f63;
                border-left: 1px solid #5b5f63;
                border-right: 1px solid #5b5f63;
            }






        .single-block-area-All {
            border-collapse: collapse;
            width: 100%;
            height: 40%;
            vertical-align: central;
            background-color: #D3D3D3;
        }

            .single-block-area-All th {
                text-align: center;
                color: black;
                text-align: center;
                padding: 10px;
                border-radius: 10px 10px 0px 0px;
            }

            .single-block-area-All td {
                padding: 5px;
            }

            /*.single-block-area tr td {
                border-bottom: 1px solid #DCE2E8;
            }*/


            .single-block-area-All tr td {
                border-bottom: 1px solid #5b5f63;
                border-top: 1px solid #5b5f63;
                border-left: 1px solid #5b5f63;
                border-right: 1px solid #5b5f63;
            }
    </style>

    <title></title>
      
</head>
<body>
    <form id="form1" runat="server">
         <div id="Divid" runat="server"></div>
        <link rel="stylesheet"
            href="https://netdna.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css">
        <script
            src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>


        <script
            src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
        <script
            src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/js/bootstrap-multiselect.js"></script>
        <link rel="stylesheet"
            href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/css/bootstrap-multiselect.css">


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
           $(document).ready(function () {
          
                   var pData = [];
                   var today = new Date();

                   var Fmnth = '';
                   var Tmnth = '';
                   var FYear = '';
                   var TYear = '';

                   var Month_Name = '';

                   var monthShortNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
          "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
                   ];
                   //d.getMonth() =0-- jan , d.getMonth()=1--Feb etc
                   var d = new Date();
                   pData[0] = '1';
                   pData[1] = '2';

                   var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
                   var function_name = "getChartVal_YTD";
                   $.ajax({
                       type: "POST",
                       url: "Sales_Dashboard_IVF.aspx/" + function_name,
                       data: jsonData,
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: OnSuccess_,
                       error: OnErrorCall_
                   });

                   function OnSuccess_(response) {
                       var aData = response.d;
                       var arr = [];
                       var sf_code, tar, sal, ach, psal, grow, pc, Div_Name, Div_Code, div_cnt;


                       $.map(aData, function (item, index) {

                           sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC; Div_Name = item.Div_Name; Div_Code = item.Div_Code;

                       });
                       var myJsonString = JSON.stringify(arr);
                       var jsonArray = JSON.parse(JSON.stringify(arr));

                       document.getElementById("lbltarY").textContent = tar
                       document.getElementById("lblsalY").textContent = sal
                       document.getElementById("lblachY").textContent = ach
                       document.getElementById("lblDivY").textContent = Div_Name
                       document.getElementById("lblGrwthY").textContent = grow
                       document.getElementById("lblPCPMY").textContent = pc


                       //  $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                       //  $("#lblivf").wrapInner("<a target=_blank href=/Sales_Dashboard_IVF.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");





                   }
                   function OnErrorCall_(response) {
                       alert("Error: No Data Found!");
                   }
               //  e.preventDefault();

                   var Data = "1" + "^" + "2";
                   $.ajax({

                       type: 'POST',

                       url: "Sales_Dashboard_IVF.aspx/Primary",

                       contentType: "application/json; charset=utf-8",

                       dataType: "json",

                       data: '{objData:' + JSON.stringify(Data) + '}',
                       success: function (data) {
                           var chartData = eval("(" + data.d + ')');

                           var data_array = [];
                           var count = chartData.length;
                           for (var i = 0; i < count; i++) {
                               var obj = {
                                   label: chartData[i].Label,
                                   value: chartData[i].Value,
                                   Code: chartData[i].Code,
                                   "tooltext": "Brand : " + chartData[i].Label + "<br>Target : " + chartData[i].Target_Val + "<br>Sales : " + chartData[i].Value + "<br>Ach (%) : " + chartData[i].achie + "<br>Growth (%) : " + chartData[i].Growth + "<br>PCPM : " + chartData[i].PC + "<br>Division Name : " + chartData[i].Division_Name,
                                 //  link: "n-/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + Field + "&Frm_Month=" + Month + "&Frm_year=" + Year + "&To_year=" + TYear + "&To_Month=" + TMonth + "&div_Code=" + Div + "&Brand_Code=" + chartData[i].Code + "&Brand_Name=" + chartData[i].Label + "&sf_name=" + FF_Name + "&selectedValue=" + "5" + "&Div_New=" + Div_New

                                   //color: arrcolor
                               }
                               data_array.push(obj);
                           }

                           var objJSON = {
                               chart: {
                                   "caption": "",
                                   "formatnumberscale": "0",
                                   "showBorder": "0",
                                   "showLegend": "1",
                                   "theme": "fint",
                                   //"showPercentValues": "0",   
                                   //"showPercentInToolTip": "1",
                                   //Setting legend to appear on right side
                                   //"paletteColors": "#007bff,#71e3e0,#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                   "paletteColors": "#33558B,#77A033,#7F6084,#F79647,#4AACC5,#8064A1,#23BFAA,#9BBB58,#C0504E,#4F81BC   ,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                   "legendPosition": "bottom",
                                   //Caption for legend

                                   //Customization for legend scroll bar cosmetics
                                   "legendScrollBgColor": "#b8b6b6",
                                   "legendScrollBarColor": "#999999",

                                   //,"link": "http://google.com/",
                                   //"plotTooltext": "Brand : $label<br>Value : $value",
                               }, data: (data_array)
                               //, options: { backgroundColor: '#b8b6b6' }
                           };
                           var newdata = JSON.stringify(objJSON);


                           var fusioncharts = new FusionCharts({
                               type: 'pie2d',
                               renderAt: 'chart-container',
                               width: '660',
                               height: '430',
                               align: 'center',
                               dataFormat: 'json',
                               //backgroundColor: "#F5DEB3",
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
              
           });
           </script>
    <div>
    <div id="chrt1" runat="server" class="main-wrapper-area">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="wrapper-left">
                            <h4>
                                <img src="images/icon.png" alt="">Target vs Sales</h4>
                            <div id="DivSale" class="table-part">
                                <table>
                                    <tr class="head-row">
                                        <th>Division</th>
                                        <th>Target</th>
                                        <th>Sales</th>
                                        <th>Ach (%)</th>
                                        <th>Growth</th>
                                        <th>PCPM</th>
                                    </tr>
                                    <tr class="mar-row"></tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDivY" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="9"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbltarY" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblsalY" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblachY" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblGrwthY" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblPCPMY" runat="server"  ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
                                          <%-- <span id="lblivf"  style= "display:none;color:Black; font-family:Verdana;font-size:small;">IVF</span>--%>
                                        </td>
                                        
                                    </tr>
                                    
                               

                                </table>
                            </div>
                        </div>
                    </div>


                    <div class="col-lg-6">
                        <div class="wrapper-right">
                            <div class="right-top">
                                <div class="rttop-left">
                                    <h4>
                                        <img src="images/graps.png" alt="">Top 10 Brand Contributions</h4>
                                </div>
                               
                               
                            </div>
                            <div class="chart" id="chart-container" style="height: 370px; width: 100%; margin: 0 auto; align-items: flex-start;"></div>

                        </div>
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
        <script type="text/javascript" src="https://code.highcharts.com/highcharts.js"></script>
        <script type="text/javascript" src="https://code.highcharts.com/highcharts-more.js"></script>
        <script type="text/javascript" src="https://code.highcharts.com/modules/solid-gauge.js"></script>


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
