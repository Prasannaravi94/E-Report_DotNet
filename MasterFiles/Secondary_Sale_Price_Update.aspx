<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Secondary_Sale_Price_Update.aspx.cs"
    Inherits="MasterFiles_Secondary_Sale_Price_Update" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Sale Price Update </title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="../css/MR.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <link href="../JScript/Process_CSS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link href="../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .divrpt {
            margin: 20px;
            font-family: Verdana;
            color: Maroon;
            font-size: 20px;
        }

        .rpt {
            border: 1px solid #A6A6D2;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            -khtml-border-radius: 8px;
            color: #071019;
            padding: 0px;
        }

        .rpttr {
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            -khtml-border-radius: 8px;
            background-color: #F0F8FF;
            padding: 0px;
        }

        .rptSpan {
            color: Black;
        }

        .rptmar {
            margin: 8px;
            color: Maroon;
            font-family: Verdana;
        }

        .rpta {
            color: Maroon;
            width: 200px;
            height: auto;
            text-decoration: underline;
        }

            .rpta:hover {
                color: #b70b6e;
                text-decoration: underline;
            }

        .rpttdWidth {
            width: 100px;
        }

        .rptTr {
            border-bottom: dashed 1px maroon;
            background-color: #F5FAEA;
        }
    </style>
    <style type="text/css">
        .div_fixed {
            position: fixed;
            top: 400px;
            right: 5px;
        }
    </style>
    <style type="text/css">
        .web_dialog_overlay {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            background: #000000;
            opacity: .15;
            filter: alpha(opacity=15);
            -moz-opacity: .15;
            z-index: 101;
            display: none;
        }

        .web_dialog {
            display: none;
            position: fixed;
            width: 380px;
            min-height: 200px;
            max-height: auto;
            top: 50%;
            left: 50%;
            margin-left: -190px;
            margin-top: -100px;
            background-color: #ffffff;
            border: 2px solid #336699;
            padding: 0px;
            z-index: 102;
            font-family: Verdana;
            font-size: 10pt;
        }

        .web_dialog_title {
            border-bottom: solid 2px #336699;
            background-color: #336699;
            padding: 4px;
            color: White;
            font-weight: bold;
        }

            .web_dialog_title a {
                color: White;
                text-decoration: none;
            }

        .align_right {
            text-align: right;
        }

        .Formatrbtn label {
            margin-right: 30px;
        }

        /* hover style just for information */
        label:hover:before {
            border: 1px solid #4778d9 !important;
        }

        .btnReAct {
            display: inline-block;
            padding: 3px 9px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            margin-top: 25px;
        }

        .btnReActivation {
            color: #fff;
            background-color: #457fb9;
            border-color: #457fb9;
        }

            .btnReActivation:hover {
                color: #fff;
                background-color: #104982;
                border-color: #104982;
            }

            .btnReActivation:focus, .btnReActivation.focus {
                color: #fff;
                background-color: #104982;
                border-color: #104982;
            }

            .btnReActivation:active, .btnReActivation.active {
                color: #fff;
                background-color: #457fb9;
                border-color: #457fb9;
                background-image: none;
            }

        #btnClose_Plus:focus {
            outline-offset: -2px;
        }

        #btnClose_Plus:hover, #btnClose_Plus:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btnClose_Plus:hover, #btnClose_Plus:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btnClose_Plus:active, #btnClose_Plus:hover {
            outline: 0px none currentColor;
        }
    </style>
    <style type="text/css">
        body {
            font: normal 12px "Segoe UI", Arial, "Helvetica Neue", Helvetica, sans-serif;
        }

        a {
            cursor: pointer;
            text-decoration: none;
        }

        /*------------Container styles --------------*/

        #container {
            width: 100%;
            height: 100%;
        }

        /*------------Popup styles --------------*/

        #popup_box {
            display: none;
            position: fixed;
            height: 250px;
            width: 300px;
            background: #fff;
            left: 50%;
            top: 50%;
            margin-left: -150px;
            margin-top: -150px;
            z-index: 100;
            padding: 15px;
            font-size: 15px;
            -moz-box-shadow: 0 0 5px;
            -webkit-box-shadow: 0 0 5px;
            box-shadow: 0 0 5px;
        }

        #popupBoxClose {
            background: #369;
            color: #fff;
            padding: 15px;
            margin: -15px -15px 10px -15px;
            display: block;
            position: relative;
            text-align: right;
        }

            #popupBoxClose #CntDown {
                position: absolute;
                top: 6px;
                left: 10px;
                width: 20px;
                height: 20px;
                background: #fff;
                color: #369;
                text-align: center;
                -webkit-border-radius: 50%;
                border-radius: 50%;
                font-size: 14px;
                font-weight: bold;
            }
    </style>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .modal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            left: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 128px;
                width: 128px;
            }
    </style>
    <style type="text/css">
        .notIE {
            position: relative;
            display: inline-block;
        }

        select {
            display: inline-block;
            height: 30px;
            width: 150px;
            padding: 2px 10px 2px 2px;
            outline: none;
            color: #74646e;
            border: 1px solid #C8BFC4;
            border-radius: 4px;
            box-shadow: inset 1px 1px 2px #ddd8dc;
            background: #fff;
        }

        /* Select arrow styling */
        .notIE .fancyArrow {
            width: 23px;
            height: 28px;
            position: absolute;
            display: inline-block;
            top: 1px;
            right: 3px;
            background: url(../../Images/loading/ArrowIcon3.png) right / 90% no-repeat #fff;
            pointer-events: none;
        }
        /*target Internet Explorer 9 and Internet Explorer 10:*/
        @media screen and (min-width:0\0) {
            .notIE .fancyArrow {
                display: none;
            }
        }

        .label {
            display: inline-block;
            height: 19px;
            width: 100px;
            font-size: 12px;
            color: black;
            font-family: Verdana;
        }
    </style>
    <style type="text/css">
        .btn {
            background: #3498db;
            background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
            background-image: -moz-linear-gradient(top, #3498db, #2980b9);
            background-image: -ms-linear-gradient(top, #3498db, #2980b9);
            background-image: -o-linear-gradient(top, #3498db, #2980b9);
            background-image: linear-gradient(to bottom, #3498db, #2980b9); /*  -webkit-border-radius: 28;
            -moz-border-radius: 28;
            border-radius: 28px;*/
            font-family: Verdana;
            color: #ffffff;
            font-size: 15px;
            padding: 5px 10px 5px 10px;
            text-decoration: none;
        }

            .btn:hover {
                background: #3cb0fd; /* background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
            background-image: linear-gradient(to bottom, #3cb0fd, #3498db);*/
                text-decoration: none;
                color: Black;
            }
    </style>
    <style type="text/css">
        .scene {
            width: 300px;
            height: 300px;
            margin: 250px auto 0;
        }

        .cube {
            position: relative;
            width: 300px;
            height: 300px;
            transform: perspective(120px) translateZ(-150px);
        }

        .side {
            position: absolute;
            width: 300px;
            height: 300px;
            box-sizing: border-box;
            background-color: #999;
            background-size: 100% 100%;
            background-repeat: no-repeat;
            padding: 120px 0;
            font: 50px/1 'Trebuchet MS', sans-serif;
            color: #fff;
            text-transform: uppercase;
            text-align: center;
            transform-origin: 50% 50% -150px;
            backface-visibility: hidden;
        }

            .side::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background: rgba(0, 0, 0, 0.15);
            }

            .side span {
                position: relative;
            }

        .guides {
            position: absolute;
            top: 0;
            left: 50px;
            width: 200px;
            height: 100%;
            border-style: dotted;
            border-width: 0 1px;
            color: rgba(255, 255, 255, 0.6);
        }

            .guides::before {
                content: '';
                position: absolute;
                top: 0;
                left: 50%;
                width: 0;
                height: 100%;
                border-left: 1px dotted;
            }

        .back {
            transform: perspective(1200px) rotateX(0deg) rotateY(180deg);
            animation: back 3s infinite linear;
        }

        .top {
            transform: perspective(1200px) rotateX(90deg);
            animation: top 3s infinite linear;
        }

        .bottom {
            transform: perspective(1200px) rotateX(-90deg);
            animation: bottom 3s infinite linear;
        }

        .front {
            transform: perspective(1200px) rotateX(0deg);
            animation: front 3s infinite linear;
        }

        .back {
            background-image: url(../../Images/Tablet2.jpg);
        }

        .top {
            background-image: url(../../Images/Tablet3.jpg);
        }

        .bottom {
            background-image: url(../../Images/TabletImg.jpg);
        }

        .front {
            background-image: url(../../Images/Tablet2.jpg);
        }

        @keyframes top {
            0% {
                transform: perspective(1200px) rotateX(90deg);
            }

            15% {
                transform: perspective(1200px) rotateX(180deg);
            }

            25% {
                transform: perspective(1200px) rotateX(180deg);
            }

            40% {
                transform: perspective(1200px) rotateX(270deg);
            }

            50% {
                transform: perspective(1200px) rotateX(270deg);
            }

            65% {
                transform: perspective(1200px) rotateX(360deg);
            }

            75% {
                transform: perspective(1200px) rotateX(360deg);
            }

            90% {
                transform: perspective(1200px) rotateX(450deg);
            }

            100% {
                transform: perspective(1200px) rotateX(450deg);
            }
        }

        @keyframes bottom {
            0% {
                transform: perspective(1200px) rotateX(-90deg);
            }

            15% {
                transform: perspective(1200px) rotateX(0deg);
            }

            25% {
                transform: perspective(1200px) rotateX(0deg);
            }

            40% {
                transform: perspective(1200px) rotateX(90deg);
            }

            50% {
                transform: perspective(1200px) rotateX(90deg);
            }

            65% {
                transform: perspective(1200px) rotateX(180deg);
            }

            75% {
                transform: perspective(1200px) rotateX(180deg);
            }

            90% {
                transform: perspective(1200px) rotateX(270deg);
            }

            100% {
                transform: perspective(1200px) rotateX(270deg);
            }
        }

        @keyframes front {
            0% {
                transform: perspective(1200px) rotateX(0deg);
            }

            15% {
                transform: perspective(1200px) rotateX(90deg);
            }

            25% {
                transform: perspective(1200px) rotateX(90deg);
            }

            40% {
                transform: perspective(1200px) rotateX(180deg);
            }

            50% {
                transform: perspective(1200px) rotateX(180deg);
            }

            65% {
                transform: perspective(1200px) rotateX(270deg);
            }

            75% {
                transform: perspective(1200px) rotateX(270deg);
            }

            90% {
                transform: perspective(1200px) rotateX(360deg);
            }

            100% {
                transform: perspective(1200px) rotateX(360deg);
            }
        }

        @keyframes back {
            0% {
                transform: perspective(1200px) rotateX(0deg) rotateY(180deg) rotateZ(180deg);
            }

            15% {
                transform: perspective(1200px) rotateX(90deg) rotateY(180deg) rotateZ(180deg);
            }

            25% {
                transform: perspective(1200px) rotateX(90deg) rotateY(180deg) rotateZ(180deg);
            }

            40% {
                transform: perspective(1200px) rotateX(180deg) rotateY(180deg) rotateZ(180deg);
            }

            50% {
                transform: perspective(1200px) rotateX(180deg) rotateY(180deg) rotateZ(180deg);
            }

            65% {
                transform: perspective(1200px) rotateX(270deg) rotateY(180deg) rotateZ(180deg);
            }

            75% {
                transform: perspective(1200px) rotateX(270deg) rotateY(180deg) rotateZ(180deg);
            }

            90% {
                transform: perspective(1200px) rotateX(360deg) rotateY(180deg) rotateZ(180deg);
            }

            100% {
                transform: perspective(1200px) rotateX(360deg) rotateY(180deg) rotateZ(180deg);
            }
        }
    </style>
    <script type="text/javascript">


        /*--------------------State DDL-------------------*/
        function BindState() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Secondary_Sale_Price_Update.aspx/BindState",
                data: '{}',
                dataType: "json",
                success: function (result) {
                    $("#ddlState").empty();
                    $('#ddlState').append("<option value='0'>--All--</option>");
                    $.each(result.d, function (key, value) {
                        $("#ddlState").append($("<option></option>").val(value.StateCode).html(value.StateName));
                    });
                },
                error: function ajaxError(result) {
                    createCustomAlert("Error");
                }
            });
        }
        /*--------------------Year DDL-------------------*/
        function BindYear_DDL() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Secondary_Sale_Price_Update.aspx/FillYear",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    $("#ddlYear").empty();

                    var Year = data.d[0].Year;
                    var Cur_Year = new Date().getFullYear();
                    $('#ddlYear').append("<option value='0'>--Select--</option>");
                    for (var i = parseInt(Year) ; i <= Cur_Year; i++) {
                        $("#ddlYear").append($("<option></option>").val(i).html(i));
                    }

                    $("#ddlYear option:contains('" + Cur_Year + "')").attr('selected', 'selected');
                },
                error: function ajaxError(result) {
                    createCustomAlert("Error");
                }

            });
        }

    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            var d = new Date(),
            n = d.getMonth(),
            y = d.getFullYear();

            $('#ddlMonth option:eq(' + n + ')').prop('selected', true);

            BindState();
            BindYear_DDL();

            $("#btnGo").click(function () {

                var State = $("#ddlState").val();
                var Price = $("#ddlPrice").val();
                var Month = $("#ddlMonth").val();
                var Year = $("#ddlYear").val();

                var SS_Data = {};

                SS_Data.StateCode = State;
                SS_Data.PriceID = Price;
                SS_Data.Month = Month;
                SS_Data.Year = Year;

                if (Month > 0 && Year > 0) {

                    $.ajax({
                        type: "POST",
                        url: "Secondary_Sale_Price_Update.aspx/Update_SecSale_Price",
                        data: '{objDataDet:' + JSON.stringify(SS_Data) + ' }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {

                            //  if (data.d > 0) {

                            createCustomAlert("Price Updated Successfully");

                            $("#ddlState").val("0");
                            $('#ddlPrice').val("0");
                            $('#ddlMonth').val("0");
                            $('#ddlYear').val("0");


                            // }

                        },
                        error: function (res) {

                            alert("Error");
                        }
                    });
                }

            });

        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <br />
                    <h2 class="text-center" style="border-bottom:none !important;" id="hHeading" runat="server"></h2>

                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <span id="lblState" class="label"><span style="color: Red">*</span>State </span>
                                    <select id="ddlState" style="width: 250px">
                                    </select>
                                </div>
                                <div class="single-des clearfix">
                                    <span id="lblPrice" class="label"><span style="color: Red">*</span>Price </span>
                                    <select id="ddlPrice" style="width: 250px">
                                        <option value="0">--Select--</option>
                                        <option value="M">MRP Price</option>
                                        <option value="R">Retailor Price</option>
                                        <option value="D">Distributor Price</option>
                                        <option value="T">Target Price</option>
                                        <option value="N">NSR Price</option>
                                    </select>
                                </div>
                                <div class="single-des clearfix">
                                    <span id="lblMonth" class="label"><span style="color: Red">*</span>Month </span>
                                    <select id="ddlMonth">
                                        <option value="0">--Select--</option>
                                        <option value="1">Jan</option>
                                        <option value="2">Feb</option>
                                        <option value="3">Mar</option>
                                        <option value="4">Apr</option>
                                        <option value="5">May</option>
                                        <option value="6">Jun</option>
                                        <option value="7">Jul</option>
                                        <option value="8">Aug</option>
                                        <option value="9">Sep</option>
                                        <option value="10">Oct</option>
                                        <option value="11">Nov</option>
                                        <option value="12">Dec</option>
                                    </select>
                                </div>
                                <div class="single-des clearfix">
                                    <span id="lblYear" class="label"><span style="color: Red">*</span>Year </span>
                                    <select id="ddlYear">
                                        <option value="0">--Select--</option>
                                    </select>
                                </div>
                                <div class="single-des clearfix">
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <input type="button" id="btnGo" value="Go" class="savebutton" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
