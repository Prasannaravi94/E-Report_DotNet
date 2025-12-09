<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_SecSale_Stockist_DeActivate_List.aspx.cs" Inherits="MasterFiles_AnalysisReports_rpt_SecSale_Stockist_DeActivate_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <link href="../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link href="../../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet"
        type="text/css" />
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../../JScript/Service_CRM/Stockist_JS/PrintandExcel_JS.js" type="text/javascript"></script>
   
    <script src="../../JScript/Service_CRM/SecSale/rpt_SecSale_Stockist_DeActivate_JS.js"
        type="text/javascript"></script>

    <style type="text/css">
        .table > thead > tr > th,
        .table > tbody > tr > th,
        .table > tfoot > tr > th,
        .table > thead > tr > td,
        .table > tbody > tr > td,
        .table > tfoot > tr > td {
            font-size: 10px;
            white-space: pre;
        }

        .table-bordered > thead > tr > th,
        .table-bordered > tbody > tr > th {
            border: 1px solid #000;
        }

        .table-bordered > tfoot > tr > th,
        .table-bordered > thead > tr > td,
        .table-bordered > tbody > tr > td,
        .table-bordered > tfoot > tr > td {
            border: 1px solid #696969;
        }


        #lblStock {
            color: #0e50ed;
            font-weight: bold;
            font-size: 14px;
            /*font-family:Cambria;*/
            font-family: 'Comic Sans MS';
        }

        #lblField {
            color: #ff28f6;
            font-weight: bold;
            font-size: 13px;
            font-family: Calibri;
        }

        .Service {
            font-size: 14px;
            font-family: Calibri;
            color: #d10091;
            font-weight: bold;
            /*-webkit-text-stroke: 1px #d10091;
            -webkit-text-fill-color: #d10091;
            -webkit-animation: fillser 0.5s infinite alternate;*/
        }

        /*@-webkit-keyframes fillser {
            from {
                -webkit-text-fill-color: #d10091;
            }

            to {
                -webkit-text-fill-color: #d10091;
            }
        }*/


           
    .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
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

    </style>
    <style type="text/css">
        @media print {
            .noPrnCtrl {
                display: none;
            }
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="SS_DivCode" type="hidden" value='<%= Session["div_code"] %>' />

            <div id="MainDiv">
                <center>
                <span id="lblStock"></span>
                <br />
                <span id="lblField"></span>
                 </center>
                <br />
                <div id="divpnl" style="width:100%">        
                    <div id="divstock" style="width:45%;float:left">

                    </div>      
                    <div id="divPrd" style="width:45%;float:left">

                    </div>        
                </div>
            </div>
            <div class="modal">
              <%--  <img src="../../Images/ICP/Loading_SS_1.gif" style="width: 100px; height: 100px; position: fixed; top: 35%; left: 35%;"
                    alt="" />--%>

                 <img src="../../Images/ICP/loading_3.gif" style="width:100px;
            height: 100px; position: fixed; top: 45%; left: 45%;" alt="" />

            </div>
        </div>
    </form>
</body>
</html>
