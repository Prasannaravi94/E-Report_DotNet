<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSale_Delete.aspx.cs" Inherits="MasterFiles_MGR_SecSales_SecSale_Delete" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Sales Entry Delete</title>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/MR.css" />--%>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <link href="../../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <%--   <link href="../../../JScript/Process_CSS.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="../../../JScript/DateJs/AlertJSBox.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<link href="../../../JScript/BootStrap/dist/css/bootstrap.css" rel="stylesheet" type="text/css" />--%>
    <script src="../../../JScript/Service_CRM/SecSale/SecSale_Entry_Delete_JS.js" type="text/javascript"></script>

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

        #btn_Close_Prime:focus {
            outline-offset: -2px;
        }

        #btn_Close_Prime:hover, #btn_Close_Prime:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btn_Close_Prime:hover, #btn_Close_Prime:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btn_Close_Prime:active, #btn_Close_Prime:hover {
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
        #tblStatus {
            border-collapse: collapse;
            width: 100%;
        }

            #tblStatus th, #tblStatus td {
                text-align: left;
                padding: 5px;
            }

            #tblStatus tr:nth-child(even) {
                background-color: white;
            }

            #tblStatus th {
                background-color: white;
                color: white;
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
            background: url(../../../Images/loading/ArrowIcon3.png) right / 90% no-repeat #fff;
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
    <script type="text/javascript">
        function confirm_Submit() {
            if (confirm('Do you want to Submit?')) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <input id="SS_SfCode" type="hidden" value='<%= Session["sf_code"] %>' />
            <input id="SS_SfType" type="hidden" value='<%= Session["sf_type"] %>' />
            <input id="SS_DivCode" type="hidden" value='<%= Session["div_code"] %>' />
            <div id="Divid" runat="server">
            </div>
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <br />
                        <h2 class="text-center" style="border-bottom: none !important;" id="hHeading" runat="server"></h2>

                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <span id="lblFieldForceName" class="label">
                                            <span style="color: Red">*</span>Field Force
                                        </span>
                                        <select id="ddlFieldForce" class="custom-select2 nice-select"></select>
                                    </div>
                                    <div class="single-des clearfix">
                                        <span id="lblStockiest" class="label">
                                            <span style="color: Red">*</span>Stockiest
                                        </span>
                                        <select id="ddlStockiest" class="custom-select2 nice-select">
                                            <option value="0">--Select--</option>
                                        </select>
                                    </div>
                                    <div class="single-des clearfix">
                                        <span id="lblMonth" class="label">
                                            <span style="color: Red">*</span>Month
                                        </span>
                                        <select id="ddlMonth" style="font-size: 11px">
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
                                        <span id="lblYear" class="label">
                                            <span style="color: Red">*</span>Year
                                        </span>
                                        <select id="ddlYear" style="font-size: 11px">
                                            <option value="0">--Select--</option>
                                        </select>
                                    </div>
                                    <div class="w-100 designation-submit-button text-center clearfix">
                                        <input type="button" id="btnGo" value="Go" class="savebutton" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--  &nbsp;&nbsp;
            <input type="button" id="btnClear" value="Clear" style="width: 70px" class="BUTTON" />--%>
        </div>
        <div class="modal">
            <img src="../../../Images/ICP/loading_9_k.gif" style="width: 150px; height: 150px; position: fixed; top: 45%; left: 45%;"
                alt="" />
        </div>

    </form>
</body>
</html>
