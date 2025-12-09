<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoard_RCPA_Analysis.aspx.cs" Inherits="MasterFiles_DashBoard_RCPA_Analysis" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"
        integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css"
        integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/css/bootstrap-select.min.css" />
    <link href="https://cdn.datatables.net/1.10.16/css/dataTables.bootstrap.min.css"
        rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.datatables.net/responsive/2.2.3/css/responsive.dataTables.min.css" />
    <link href="../css/pace/themes/blue/pace-theme-flash.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />

    <style>
        .blockUI.blockMsg.blockElement {
            border: none !important;
        }

        #loader {
            position: fixed;
            top: 50%;
            right: 50%;
            margin: auto;
        }

            #loader .dot {
                bottom: 0;
                height: 100%;
                left: 0;
                margin: auto;
                position: absolute;
                right: 0;
                top: 0;
                width: 87.5px;
            }

                #loader .dot::before {
                    border-radius: 100%;
                    content: "";
                    height: 87.5px;
                    left: 0;
                    position: absolute;
                    right: 0;
                    top: 0;
                    transform: scale(0);
                    width: 87.5px;
                }

                #loader .dot:nth-child(7n+1) {
                    transform: rotate(45deg);
                }

                    #loader .dot:nth-child(7n+1)::before {
                        animation: 0.8s linear 0.1s normal none infinite running load;
                        background: #00ff80 none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+2) {
                    transform: rotate(90deg);
                }

                    #loader .dot:nth-child(7n+2)::before {
                        animation: 0.8s linear 0.2s normal none infinite running load;
                        background: #00ffea none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+3) {
                    transform: rotate(135deg);
                }

                    #loader .dot:nth-child(7n+3)::before {
                        animation: 0.8s linear 0.3s normal none infinite running load;
                        background: #00aaff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+4) {
                    transform: rotate(180deg);
                }

                    #loader .dot:nth-child(7n+4)::before {
                        animation: 0.8s linear 0.4s normal none infinite running load;
                        background: #0040ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+5) {
                    transform: rotate(225deg);
                }

                    #loader .dot:nth-child(7n+5)::before {
                        animation: 0.8s linear 0.5s normal none infinite running load;
                        background: #2a00ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+6) {
                    transform: rotate(270deg);
                }

                    #loader .dot:nth-child(7n+6)::before {
                        animation: 0.8s linear 0.6s normal none infinite running load;
                        background: #9500ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+7) {
                    transform: rotate(315deg);
                }

                    #loader .dot:nth-child(7n+7)::before {
                        animation: 0.8s linear 0.7s normal none infinite running load;
                        background: magenta none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+8) {
                    transform: rotate(360deg);
                }

                    #loader .dot:nth-child(7n+8)::before {
                        animation: 0.8s linear 0.8s normal none infinite running load;
                        background: #ff0095 none repeat scroll 0 0;
                    }

            #loader .lading {
                background-image: url("../images/loading.gif");
                background-position: 50% 50%;
                background-repeat: no-repeat;
                bottom: -40px;
                height: 20px;
                left: 0;
                position: absolute;
                right: 0;
                width: 180px;
            }

        @keyframes load {
            100% {
                opacity: 0;
                transform: scale(1);
            }
        }

        @keyframes load {
            100% {
                opacity: 0;
                transform: scale(1);
            }
        }

        #tblMsgInfo > tbody > tr > td {
            border: 1px solid #aba3a3;
        }
        /* Default Header Styles */
        .dynamic-header {
            background-color: #4cb6c4;
            color: #fff;
            text-align: center;
            padding: 10px;
            font-size: 16px;
        }

        /* Responsive Header for Tablets */
        @media (max-width: 768px) {
            .dynamic-header {
                font-size: 14px;
                padding: 8px;
            }
        }

        /* Responsive Header for Mobile */
        @media (max-width: 480px) {
            .dynamic-header {
                font-size: 12px;
                padding: 6px;
            }
        }

        table.data-table {
            width: 80%;
            border-collapse: collapse;
            margin: 20px 0;
            font-size: 12px;
            text-align: left;
        }

            table.data-table th, table.data-table td {
                padding: 10px;
                border: 1px solid #ddd;
                font-size: 12px;
                font-family: Verdana;
            }


            table.data-table th {
                background-color: #f4f4f4;
                font-size: 12px;
                font-family: Verdana;
            }

            table.data-table td {
                background-color: #f9f9f9;
                font-size: 11px;
                font-family: Verdana;
            }

        /* Mobile responsiveness */
        @media only screen and (max-width: 600px) {
            table.data-table {
                font-size: 12px;
                font-family: Verdana;
            }

                table.data-table th, table.data-table td {
                    padding: 8px;
                    font-size: 11px;
                }
        }

        btn2 {
            display: inline-block;
            font-weight: 400;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            border: 1px solid transparent;
            padding: .375rem .75rem;
            font-size: 1rem;
            line-height: 1.5;
            border-radius: .25rem;
            transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .btn2:focus, .btn2:hover {
            text-decoration: none;
        }

        .btn2.focus, .btn2:focus {
            outline: 0;
            box-shadow: 0 0 0 .2rem rgba(0,123,255,.25);
        }

        .btn2.disabled, .btn2:disabled {
            opacity: .65;
        }

        .btn2:not(:disabled):not(.disabled) {
            cursor: pointer;
        }

            .btn2:not(:disabled):not(.disabled).active, .btn2:not(:disabled):not(.disabled):active {
                background-image: none;
            }

        a.btn2.disabled, fieldset:disabled a.btn2 {
            pointer-events: none;
        }

        .btn-primary2 {
            color: #fff;
            background-color: #0069d9;
            border-color: #0069d9;
        }

            .btn-primary2:hover {
                color: #fff;
                background-color: #0069d9;
                border-color: #0069d9;
            }

            .btn-primary2.focus, .btn-primary2:focus {
                box-shadow: 0 0 0 .2rem rgba(0,123,255,.5);
            }

            .btn-primary2.disabled, .btn-primary2:disabled {
                color: #fff;
                background-color: #0069d9;
                border-color: #0069d9;
            }

            .btn-primary2:not(:disabled):not(.disabled).active, .btn-primary2:not(:disabled):not(.disabled):active, .show > .btn-primary2.dropdown-toggle {
                color: #fff;
                background-color: #0069d9;
                border-color: #0069d9;
            }

                .btn-primary2:not(:disabled):not(.disabled).active:focus, .btn-primary2:not(:disabled):not(.disabled):active:focus, .show > .btn-primary2.dropdown-toggle:focus {
                    box-shadow: 0 0 0 .2rem rgba(0,123,255,.5);
                }

        .mobile-container {
            max-width: 480px;
            margin: auto;
            background-color: #0069d9;
            height: 500px;
            color: white;
            border-radius: 10px;
        }

        .topnav {
            overflow: hidden;
            background-color: #333;
            position: relative;
        }

            .topnav #myLinks {
                display: none;
            }

            .topnav a {
                color: white;
                padding: 14px 16px;
                text-decoration: none;
                font-size: 17px;
                display: block;
            }

                .topnav a.icon {
                    background: black;
                    display: block;
                    position: absolute;
                    right: 0;
                    top: 0;
                }

                .topnav a:hover {
                    background-color: #ddd;
                    color: black;
                }

        .active {
            background-color: #0069d9;
            color: white;
        }

        .loader-wrapper {
            /*top: 0;
            left: 0;*/
            width: 100vw;
            height: 100vh;
            z-index: 1000;
            background-color: rgba(0, 0, 0, 0.3);
            position: absolute;
        }

        .loaders {
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            z-index: 1000; /* Ensure it's above other elements */
            background-color: rgba(255, 255, 255, 0.8); /* Optional semi-transparent background */
            padding: 20px;
            border-radius: 10px;
            text-align: center;
        }

        .custom-dropdown {
            width: 80% !important;
            margin-bottom: 15px; /* Add space below the dropdown */
        }

        /* Ensure dropdown is displayed properly in all screen sizes */
        .form-group-sm {
            padding: 5px 10px;
        }

        /* Style the labels and inputs to be aligned nicely */
        /*label {
    font-size: 14px;
    font-weight: bold;
}*/

        /* Improve spacing on smaller screens */
        @media (max-width: 768px) {
            .custom-dropdown {
                width: 100% !important; /* Full width on smaller screens */
            }
        }

        @media only screen and (max-width: 768px) {
            #chart-containert {
                width: 100% !important;
                height: 350px !important;
                overflow-x: auto; /* Enable horizontal scroll if needed */
            }
        }

        @media only screen and (max-width: 768px) {
            #chart-Container {
                width: 100% !important;
                height: 350px !important;
                overflow-x: auto; /* Enable horizontal scroll if needed */
            }
        }

        .selectpicker > ul {
            display: none;
        }

        .selectpicker > span {
            display: none;
        }
        #detailsView:focus {
    outline: 2px solid #007BFF; /* Optional: Blue outline when focused */
}
        #modalContainer {
	background-color:rgba(0, 0, 0, 0.3);
	position:absolute;
	width:100%;
	height:100%;
	top:0px;
	left:0px;
	z-index:10000;
	background-image:url(tp.png); /* required by MSIE to prevent actions on lower z-index elements */
}

#alertBox {
	position:relative;
	width:300px;
	min-height:100px;
	/*margin-top:50px;*/
	border:1px solid #666;
	background-color:#fff;
	background-repeat:no-repeat;
	background-position:20px 30px;
	margin-top:20%;
}

#modalContainer > #alertBox 
{
	position:fixed;
}

#alertBox h1 {
	margin:0;
	font:bold 0.9em verdana,arial;
	background-color:#3073BB;
	color:#FFF;
	border-bottom:1px solid #000;
	padding:2px 0 2px 5px;
}

#alertBox p {
	font:0.9em verdana,arial;
	height:50px;
	padding-left:5px;
	margin-left:55px;
	/*margin-top:25px;*/
	padding-top:25px;
}

#alertBox #closeBtn {
	display:block;
	position:relative;
	margin:5px auto;
	padding:7px;
	border:0 none;
	width:70px;
	font:0.7em verdana,arial;
	text-transform:uppercase;
	text-align:center;
	color:#FFF;
	background-color:#357EBD;
	border-radius: 3px;
	text-decoration:none;
}

    </style>

    <script type="text/javascript" src="/JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="/JsFiles/jquery-1.10.1.js"></script>
    <script>
        function createCustomAlert(txt) {

    var ALERT_TITLE = "Alert!";
    var ALERT_BUTTON_TEXT = "Ok";
    d = document;

    if (d.getElementById("modalContainer")) return;

    mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
    mObj.id = "modalContainer";

    //mObj.style.height = "100%";

    mObj.style.height = d.documentElement.scrollHeight + "px";

    alertObj = mObj.appendChild(d.createElement("div"));
    alertObj.id = "alertBox";
    if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
    alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
    alertObj.style.visiblity = "visible";

    h1 = alertObj.appendChild(d.createElement("h1"));
    h1.appendChild(d.createTextNode(ALERT_TITLE));

    msg = alertObj.appendChild(d.createElement("p"));

    //msg.appendChild(d.createTextNode(txt));

    msg.innerHTML = txt;

    btn = alertObj.appendChild(d.createElement("a"));
    btn.id = "closeBtn";
    btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
    btn.href = "#";
    btn.focus();
    btn.onclick = function () { removeCustomAlert(); return false; }

    alertObj.style.display = "block";

}

function removeCustomAlert() {
    document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainer"));
}
function ful() {
    alert('Alert this pages');
}


    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#btnGo').click(function () {
                var SF = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SF == "---Select---") { alert("Select FieldForce Name."); $('#ddlFieldForce').focus(); $('#loader').hide(); return false; }
                var selected = $('#<%= lstProd.ClientID %> option:selected').length;

                if (selected === 0) {
                  //  alert("Please select at least one product.");
                        createCustomAlert("Please select at least one product.");
                     $('#loader').hide();
                    return false;
                }
                var mnth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (mnth == "--Select--") { alert("Select Month."); $('#ddlMonth').focus(); $('#loader').hide(); return false; }
                //  Product();
                //   GetDropdown();
                //   Compet();

            });
            $(window).on("resize", function () {
                if (window.innerWidth < 768) {
                    setTimeout(Product, 500); // Redraw chart after screen resize
                }
            });
            $('#<%= ddlprod.ClientID %>').on('change', function () {
                //var selectedValue = $(this).val();
                //var selectedText = $(this).find("option:selected").text();
                Compet();
                // console.log("Selected Value:", selectedValue);
                //  console.log("Selected Text:", selectedText);


            });
        });
    </script>
    <script>
        function scrollToTop() {
            window.scrollTo({ top: 0, behavior: 'smooth' });
        }
    </script>
    <script>
        function myFunction() {
            var x = document.getElementById("myLinks");
            if (x.style.display === "block") {
                x.style.display = "none";
            } else {
                x.style.display = "block";
            }
        }
        function closeNavbar() {
            document.getElementById("myLinks").style.display = "none";
        }
        function updateNetworkStatus() {
            if (navigator.onLine) {
                var alrt = 'You are back Online..'; showAndHideAlertBox_On(alrt); return false;
            } else {
                var alrt = 'Check Network Connection..'; showAndHideAlertBox(alrt); return false;
            }
        }
        function updateNetworkStatusev() {
            if (navigator.onLine) {
            } else {
                var alrt = 'Check Network Connection..'; showAndHideAlertBox(alrt); return;
            }
        }
        window.addEventListener('online', updateNetworkStatus);
        window.addEventListener('offline', updateNetworkStatus);
        //document.addEventListener('click', updateNetworkStatusev);
        //document.addEventListener('keydown', updateNetworkStatusev);
    </script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script>
    <script src="../DashBoard/JS/jquery-1.7.2.min.js" type="text/javascript"></script>

    <script src="../DashBoard/js1/fusioncharts.js" type="text/javascript"></script>
    <script src="../DashBoard/js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script type="text/javascript">


        //$('#detailGoBack').on('click', function (e) {
        //    alert('check');
        //    e.preventDefault();
        //    document.getElementById("detailsView").style.display = "none";
        //    document.getElementById("main").style.display = "block";
        //})
      <%--  document.addEventListener('DOMContentLoaded', function () {
            const backButton = document.getElementById('detailGoBack');
            if (backButton) {
                backButton.addEventListener('click', function (event) {
                    event.preventDefault();

                    document.getElementById('detailsView').style.display = 'none';
                    document.getElementById('main').style.display = 'block';
            document.getElementById('<%= divtrend.ClientID %>').style.display = 'block';
                });
            }
        });--%>

        function showMainView() {
            document.getElementById("main").style.display = "block";
            document.getElementById('divtrend').style.display = 'block';

            document.getElementById("detailsView").style.display = "none";
        }
        function getQueryParam(param) {
            var urlParams = new URLSearchParams(window.location.search);
            return urlParams.get(param);
        }
        function Product() {
            document.getElementById('<%= main.ClientID %>').style.display = 'block';
            var sfcode = $("#ddlFieldForce").val();
            var Month = $("#ddlMonth").val();
            var div_code = getQueryParam("div_code");
            var Year = $("#ddlYear").val();
            var map_prod = "";
            var lstProd = document.getElementById("lstProd"); // Ensure the correct ID

            for (var i = 0; i < lstProd.options.length; i++) {
                if (lstProd.options[i].selected) {
                    map_prod += lstProd.options[i].value + ",";
                }
            }

            if (map_prod.length > 0) {
                map_prod = map_prod.slice(0, -1);
            }
            var Data = Month + "^" + Year + "^" + map_prod + "^" + sfcode + "^" + div_code;
            $.ajax({

                type: 'POST',

                url: "DashBoard_RCPA_Analysis.aspx/RCPA",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData: ' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');
                    var width = window.innerWidth < 768 ? "100%" : "500"; // Adjust width for mobile
                    var height = window.innerWidth < 768 ? "350" : "400"; // Adjust height for mobile

                    var fusioncharts = new FusionCharts({

                        "type": "scrollcombidy2d",
                        "renderAt": "chart-containert",
                        "width": width,
                        "height": height,

                        "dataFormat": "json",
                        "dataSource": chartData
                    }

                    );

                    fusioncharts.render();


                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-containert").html(xhr.responseText);

                }

            });
        }

        function Compet() {

            var Month = $("#ddlMonth").val();
            var sfcode = $("#ddlFieldForce").val();
            var Year = $("#ddlYear").val();
             var div_code = getQueryParam("div_code");
            var map_prod = $("#ddlprod").val();
            var Data = Month + "^" + Year + "^" + map_prod + "^" + sfcode + "^" + div_code;
            $.ajax({

                type: 'POST',

                url: "DashBoard_RCPA_Analysis.aspx/Competitor",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');
                    var width = window.innerWidth < 768 ? "100%" : "500"; // Adjust width for mobile
                    var height = window.innerWidth < 768 ? "350" : "400"; // Adjust height for mobile
                    var fusioncharts = new FusionCharts({

                        "type": "scrollcolumn2d",
                        "renderAt": "chart-Container",
                          "width": width,
                        "height": height,
                        "dataFormat": "json",
                        "dataSource": {
                            "chart": {
                                "caption": "",

                                //"subcaption": "",
                                "xaxisname": "Competitor Products",
                                "yaxisname": "Contributed Qty",
                                //"placeValuesInside": "0",

                                //"palette": "5",
                                ////Configure scrollbar
                                //"formatNumber": "0",
                                //"formatNumberScale": "0",
                                //"useRoundEdges": "1",
                                ////  "theme": "fint",
                                //"paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",

                                //"labelDisplay": "rotate"

                                "bgcolor": "FFFFFF",
                                "showHoverEffect": "1",
                                "plotgradientcolor": "",
                                "plotBorderDashed": "0",

                                "showalternatehgridcolor": "0",
                                "showplotborder": "1",
                                "divlinecolor": "CCCCCC",
                                "showvalues": "1",

                                "syaxisname": "Comp(%)",
                                "palettecolors": "#80C4B7,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",

                                "slantlabels": "0",
                                "canvasborderalpha": "0",
                                "legendshadow": "1",
                                "legendborderalpha": "0",
                                "labelDisplay": "rotate",


                                "formatNumber": "0",
                                "formatNumberScale": "0",
                                "useRoundEdges": "1",
                                "placeValuesInside": "0",
                                "rotateValues": "1",

                                "showborder": "0"


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

        function GetDropdown() {
            var Month = $("#ddlMonth").val();
            var sfcode = $("#ddlFieldForce").val();
                         var div_code = getQueryParam("div_code");

            var Year = $("#ddlYear").val();
            var map_prod = "";
            var lstProd = document.getElementById("lstProd"); // Ensure the correct ID

            for (var i = 0; i < lstProd.options.length; i++) {
                if (lstProd.options[i].selected) {
                    map_prod += lstProd.options[i].value + ",";
                }
            }

            if (map_prod.length > 0) {
                map_prod = map_prod.slice(0, -1);
            }
            var Data = Month + "^" + Year + "^" + map_prod + "^" + sfcode + "^" + div_code;
            // AJAX call to WebMethod
            $.ajax({
                type: "POST",
                url: "DashBoard_RCPA_Analysis.aspx/GetDropdownProd",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ objData: Data }), // This is the better way
                success: function (response) {
                    var ddl = $('#<%= ddlprod.ClientID %>');

                    ddl.empty();
                    //ddl.append($('<option>', { value: "", text: "--Select--" }));

                    var data = response.d;
                    console.log("Dropdown data received:", data); // helpful for debugging

                    $.each(data, function (i, item) {
                        ddl.append($('<option>', {
                            value: item.Code,
                            text: item.Name
                        }));
                    });
                    Compet();
                },
                error: function (xhr, status, error) {
                    console.error("AJAX Error:", error);
                }
            });
        }
        function DoctorGraph() {
            document.getElementById('<%= divdoctor.ClientID %>').style.display = 'block';
            var sfcode = $("#ddlFieldForce").val();
            var Month = $("#ddlMonth").val();
                                     var div_code = getQueryParam("div_code");

            var Year = $("#ddlYear").val();
            var map_prod = "";
            var lstProd = document.getElementById("lstProd"); // Ensure the correct ID

            for (var i = 0; i < lstProd.options.length; i++) {
                if (lstProd.options[i].selected) {
                    map_prod += lstProd.options[i].value + ",";
                }
            }

            if (map_prod.length > 0) {
                map_prod = map_prod.slice(0, -1);
            }
            var Data = Month + "^" + Year + "^" + map_prod + "^" + sfcode + "^" + div_code;
            $.ajax({

                type: 'POST',

                url: "DashBoard_RCPA_Analysis.aspx/RCPA_Doctorwise",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData: ' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');
                    var width = window.innerWidth < 768 ? "100%" : "500"; // Adjust width for mobile
                    var height = window.innerWidth < 768 ? "350" : "400"; // Adjust height for mobile

                    var fusioncharts = new FusionCharts({

                        "type": "scrollcombidy2d",
                        "renderAt": "chart-containerd",
                        "width": width,
                        "height": height,

                        "dataFormat": "json",
                        "dataSource": chartData
                    }

                    );

                    fusioncharts.render();


                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-containerd").html(xhr.responseText);

                }

            });
        }
        function Trend() {
            document.getElementById('<%= divtrend.ClientID %>').style.display = 'block';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
       
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="upAdmDashboard" runat="server">

            <ContentTemplate>
                <div id="alertbox" class="loader-wrapper" style="display: none;">
                    <div class="loaders d-block d-sm-none" style="background-color: darkgoldenrod; top: 90%; padding: 5px 12px 5px 14px;">
                        <label style="font-weight: bolder;" class="alertmsg"></label>
                    </div>
                    <div class="loaders d-none d-sm-block" style="background-color: darkgoldenrod; top: 90%; padding: 5px 12px 5px 14px;">
                        <label style="font-weight: bolder;" class="alertmsg"></label>
                    </div>
                </div>
                <nav class="navbar navbar-default" id="docview" runat="server">

                    <div class="container" style="margin-top: 5px;">
                        <center><h2>RCPA Reports</h2></center>
                        <center>
                           <asp:RadioButtonList ID="rdorcpa" runat="server" RepeatDirection="Horizontal" Font-Names="Verdana" Font-Bold="true" Font-Size="12px">
                               <asp:ListItem Value="0" Selected="True" >Product Qty Wise&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Product Doctor Wise&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                 <asp:ListItem Value="2">Yield Trend</asp:ListItem>
                           </asp:RadioButtonList>
                       </center>
                        <div class="row">
                            <!-- First Column: FieldForce Name -->
                            <div class="col-md-3 col-sm-3">
                                <div class="form-group form-group-sm">
                                    <asp:Label ID="Label2" runat="server" Text="FieldForce Name:"></asp:Label>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="selectpicker form-control custom-dropdown" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <!-- Second Column: Type -->
                            <div class="col-md-2 col-sm-2">
                                <div class="form-group form-group-sm">
                                    <asp:Label ID="lblType" runat="server" Text="Type:"></asp:Label>
                                    <asp:DropDownList ID="ddltype" Enabled="false" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True" Text="Product"></asp:ListItem>
                                        <%--                                        <asp:ListItem Value="2" Text="Brand"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <!-- Third Column: Product -->
                            <div class="col-md-3 col-sm-3">
                                <div class="form-group form-group-sm">
                                    <div>
                                        <asp:Label ID="Label3" runat="server" Text="Product:"></asp:Label>
                                    </div>
                                    <select id="lstProd" class="selectpicker" runat="server" placeholder="Select Product"
                                        data-live-search="true" multiple data-actions-box="true"
                                        style="font-weight: bold; padding-top: 3px; width: 100%;">
                                    </select>
                                </div>
                            </div>

                            <!-- Fourth Column: Month -->
                            <div class="col-md-2 col-sm-2">
                                <div class="form-group form-group-sm">
                                    <asp:Label ID="Label1" runat="server" Text="Month:"></asp:Label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
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

                            <!-- Fifth Column: Year -->
                            <div class="col-md-2 col-sm-2">
                                <div class="form-group form-group-sm">
                                    <asp:Label ID="Label4" runat="server" Text="Year:"></asp:Label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>
                    </div>
</div>
                        <div class="row">
                            <div class="col-sm-12 text-center">
                                <div class="row">
                                    <%-- <div class="form-group form-group-sm">
                                        <asp:Label ID="lblMsgValidation" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div>--%>
                                    <div class="form-group form-group-sm">
                                        <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" CssClass="btn2 btn-primary" Text="Go"
                                            OnClientClick="showLoader('Search1')" />
                                                  <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" CssClass="btn2 btn-primary" Text="Clear"
                                            OnClientClick="showLoader('Search1')" />
                                        <asp:Button ID="btnback" runat="server" OnClick="btnback_Click" CssClass="btn2 btn-primary" Text="Back"
                                            OnClientClick="showLoader('Search1')" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </nav>

                <div id="main" class="container" runat="server" style="display: none;">
                    <center>
                    <h3 style="color:blue;font-weight:bold">Product Qty Wise</h3>
                        </center>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">

                                            <asp:Literal ID="ltrcpa" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <br />
                                    <center>
                                    <div class="row">
                                        <div class="col-sm-12">
                                                                                                <h5 style="background-color: #f2f2f2; color: black; font-family: Verdana; padding: 3px; font-weight: bold">TOP 5 PRODUCTS</h5>

                                            <div id="chart-containert">
                                            </div>
                                        

                                  <br />
                                            <div class="row">
                                        <div class="col-sm-12">
                                    
                                                                                                                                        <h5 style="background-color: #f2f2f2; color: black; font-family: Verdana; padding: 3px; font-weight: bold">COMPETITOR PRODUCTS</h5>

                                          <asp:DropDownList ID="ddlprod" runat="server" Width="150px" CssClass="form-control">
                                             </asp:DropDownList>
                                            </div>
                                                </div>
                                        <br />
                                                <div class="row">
                                        <div class="col-sm-12">
                                            <div id="chart-Container">
                                            </div>
                                            </div>
                                                    </div>
                                         </center>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                </div>
                </div>
               
                       <div id="detailsView" class="container" runat="server" style="display: none;">
                           <div class="row">
                               <div class="col-sm-12">
                                   <div class="panel panel-primary">
                                       <div class="panel-body">
                                           <div class="row">
                                               <div class="col-sm-12" style="text-align: right;">
                                                   <a href="javascript:__doPostBack('goBack','');" id="detailGoBack">Back</a>
                                               </div>
                                               <div class="col-sm-12">


                                                   <asp:Literal ID="ltdet" runat="server"></asp:Literal>
                                                   <br />

                                                      <asp:Literal ID="ltchem" runat="server"></asp:Literal>
                                               </div>
                                           </div>
                                       </div>
                                   </div>
                               </div>
                           </div>
                       </div>
                <div id="divdoctor" class="container" runat="server" style="display: none;">
                    <center>
                    <h3 style="color:blue;font-weight:bold">Product Doctor Wise</h3>
                        </center>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">

                                            <asp:Literal ID="ltdrrcpa" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <br />
                                    <center>
                                    <div class="row">
                                        <div class="col-sm-12">
                                                                                                <h5 style="background-color: #f2f2f2; color: black; font-family: Verdana; padding: 3px; font-weight: bold">TOP 5 PRODUCTS</h5>
                                             <div id="chart-containerd">
                                            </div>
                                        </div>

                                    </div>
                                        </center>




                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divtrend" class="container" runat="server" style="display: none;">
                    <center>
                    <h3 style="color:blue;font-weight:bold">Yield Trend
</h3>
                        </center>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <h5 style="color: blue; font-family: Verdana; padding: 3px; font-weight: bold">HIGH POTENTIAL - LOW YIELD</h5>

                                            <asp:Literal ID="lthightrend" runat="server"></asp:Literal>
                                        </div>
                                        <br />
                                        <br />
                                        <div class="col-sm-12">
                                            <h5 style="color: blue; font-family: Verdana; padding: 3px; font-weight: bold">LOW POTENTIAL - HIGH YIELD</h5>

                                            <asp:Literal ID="ltlowtrend" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <br />




                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ddlFieldForce" />
                <%--  <asp:PostBackTrigger ControlID="ddlMonth" />
                <asp:PostBackTrigger ControlID="ddlYear" />>--%>
                <%--      <asp:PostBackTrigger ControlID="ddlTerritory" />
                  <asp:PostBackTrigger ControlID="ddldr" />--%>
                <asp:PostBackTrigger ControlID="btnGo" />
                <asp:PostBackTrigger ControlID="btnback" />
                <asp:PostBackTrigger ControlID="btnClear" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="
        crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"
        integrity="sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"
        integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa"
        crossorigin="anonymous"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/i18n/defaults-*.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/responsive/2.2.3/js/dataTables.responsive.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script type="text/javascript" src="http://malsup.github.io/jquery.blockUI.js"></script>
    <script src="../css/pace/pace.js"></script>
    <script>
        $(document).ready(function () {
            $('.selectpicker').selectpicker();

            $('#tbSSBFS').DataTable({
                dom: 'Bfrtip',
                paging: false,
                searching: false,
                ordering: false,
                processing: true,
                info: false,
                buttons: []
            });
        });

        // Tooltips Initialization
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
    </script>
    <script>
        function showLoader(loaderType) {
            if (navigator.onLine) {
                if (loaderType == "Search1") {
                    document.getElementById("loader").style.display = '';
                    $('html').block({
                        message: $('#loader'),
                        centerX: true,
                        centerY: true
                    });
                }
            }
            else {
                var alrt = 'Check Network Connection..'; showAndHideAlertBox(alrt); return false;
            }
        }
    </script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/js/bootstrap-select.min.js"></script>

</body>
<div class="container">
    <div class="row">
        <div id="loader" style="display: none;">
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="lading">
            </div>
        </div>
    </div>
</div>

</html>
