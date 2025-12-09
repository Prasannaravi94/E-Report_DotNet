<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sales_DashBoard_Admin_Brand.aspx.cs" Inherits="Sales_DashBoard_Admin_Brand" %>

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
    <link href="assets/css/style_New.css" rel="stylesheet" />

    <!-- Responsive CSS -->
    <%--<link rel="stylesheet" href="css/responsive.css" />--%>

 <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
 <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>

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


   <%-- <script type="text/javascript">
        var popUpObj;

        function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name) {
            popUpObj = window.open("rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + "&selectedValue=" + "4" + "&Stok_code=" + "-2" + " &sk_Name=" + "",
    "ModalPopUp"//,
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=800," +
    //"height=600," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"

                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');
            });
        }
    </script>--%>
     <script type="text/javascript">
     var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name) {
            popUpObj = window.open("rpt_PrimarySale_Multi_Mnth_HQWise.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + "&To_year=" + tyear + "&To_Month=" + tmonth + "&selectedValue=" + "4" + "&sf_name=" + sf_name + "&Stok_code=" + "-2" + " &sk_Name=" + "",
    "ModalPopUp"//,
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=800," +
    //"height=600," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"
                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');

            });
        }


</script>



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


        //function GotoHomepage() {
        //    window.location.href = "http://www.sansfa.in/default.aspx";
        //    // window.open('Default.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
        //    return false;
        //}

    </script>
     <script type="text/javascript">
        function getprimary() {
            var pData = [];           

            pData[0] = $("#lstDiv").val();
          

            var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
            var function_name = "getprimary";
            $.ajax({
                type: "POST",
                url: "Sales_DashBoard_Admin_Brand.aspx/" + function_name,
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: OnErrorCall_
            });

            function OnSuccess_(response) {
                var aData = response.d;
                var arr = [];
                var sf_code;

                $.map(aData, function (item, index) {
                    sf_code = item.Sf_Code;



                });
                var myJsonString = JSON.stringify(arr);
                var jsonArray = JSON.parse(JSON.stringify(arr));

                document.getElementById("lblldate").textContent = sf_code





            }
            function OnErrorCall_(response) {
                alert("Error: No Data Found!");
            }
            e.preventDefault();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server"></div>
        <link rel="stylesheet"
            href="https://netdna.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css">
       


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
            $(function () {

                $('[id*=lstDiv]').multiselect({
                    enableFiltering: true,
                    maxHeight: 250,
                    buttonWidth: '180px'
                });
                $('[id*=lstFieldForce]').multiselect({
                    enableFiltering: true,
                    maxHeight: 400,
                    buttonWidth: '370px'
                });
                $('[id*=lstMode]').multiselect({
                    enableFiltering: true,
                    maxHeight: 250,
                    buttonWidth: '150px'
                });
                $('[id*=lstSale]').multiselect({
                    enableFiltering: true,
                    maxHeight: 250,
                    buttonWidth: '180px'
                });
                $('[id*=lstFinacyr]').multiselect({
                    enableFiltering: true,
                    maxHeight: 250,
                    buttonWidth: '230px'
                });

            });
        </script>



        <script type="text/javascript">
            $(document).ready(function () {
                document.getElementById("chrt1").style.visibility = "hidden";
                var Dashpgload = '<%=Session["Dashpgload"] %>';
                $("#lstDiv").change(function () {
                });

                $("#btnGo").click(function () {
                    document.getElementById("chrt1").style.visibility = "visible";
           
         //getprimary();
                    
                //  alert(common);
                    var rdoSale = $("#lstSale").val();
                    var NofDiv = '<%=Session["NofDiv"] %>';
                    if (rdoSale == 1) { //primary
                        var ddlType = $("#lstMode").val();
                        if (ddlType == null) {
                            alert("Select Mode."); $('#lstMode').focus(); return false;
                        }
                        else if (ddlType == "0") {
                            //MTD
                            PrimaryYTD();  //QTD
                            Primary();
                            getprimary();
                        }
                        else if (ddlType == "1") {
                            PrimaryYTD();  //QTD
                            Primary();
                            getprimary();
                        }
                        else if (ddlType == "2") {
                            //YTD
                            PrimaryYTD();  //QTD
                            Primary();
                            getprimary();
                        }
                    }

                    else if (rdoSale == 2) {  //secondary

                        var ddlType = $("#lstMode").val();
                        if (ddlType == null) {
                            alert("Select Mode."); $('#lstMode').focus(); return false;
                        }
                        else if (ddlType == "0") {
                            //MTD
                            SecondaryYTD();  //QTD
                            Secondary();
                        }
                        else if (ddlType == "1") {
                            SecondaryYTD();  //QTD
                            Secondary();
                        }
                        else if (ddlType == "2") {
                            //YTD
                            SecondaryYTD();  //QTD
                            Secondary();
                        }
                    }
                });

                $("#btnGoBrand").click(function () {
                    document.getElementById("chrt1").style.visibility = "visible";
                    var rdoSale = $("#lstSale").val();
                    if (rdoSale == 1) { //primary

                        var ddlType = $("#lstMode").val();
                        if (ddlType == null) {
                            alert("Select Mode."); $('#lstMode').focus(); return false;
                        }
                        else if (ddlType == "0") {
                            //MTD
                            //PrimaryYTD();  //QTD
                            Primary();
                        }
                        else if (ddlType == "1") {
                            // PrimaryYTD();  //QTD
                            Primary();
                        }
                        else if (ddlType == "2") {
                            //YTD
                            // PrimaryYTD();  //QTD
                            Primary();
                        }
                    }

                    else if (rdoSale == 2) {  //secondary

                        var ddlType = $("#lstMode").val();
                        if (ddlType == null) {
                            alert("Select Mode."); $('#lstMode').focus(); return false;
                        }
                        else if (ddlType == "0") {
                            //MTD
                            // SecondaryYTD();  //QTD
                            Secondary();
                        }
                        else if (ddlType == "1") {
                            // SecondaryYTD();  //QTD
                            Secondary();
                        }
                        else if (ddlType == "2") {
                            //YTD
                            //SecondaryYTD();  //QTD
                            Secondary();
                        }
                    }
                });

                //$("#btnback").click(function () {

                //    GotoHomepage();

                //});
            });

        </script>


        <script type="text/javascript">
            function PrimaryYTD() {
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

                var ddlType = $("#lstMode").val();

                var FinanceYr = $("#lstFinacyr").val();
                var FinanceYr_Name = $('#<%=lstFinacyr.ClientID%> :selected').text();

                if (ddlType == 1) {  //QTD

                    var FinanceYr_Splt = FinanceYr.split(' to ');
                    var FinanceYr_Splt_1 = FinanceYr_Splt[0].split('  - ');
                    var FinanceYr_Splt_2 = FinanceYr_Splt[1].split(' - ');

                    Fmnth = FinanceYr_Splt_1[0];
                    FYear = FinanceYr_Splt_1[1];
                    Tmnth = FinanceYr_Splt_2[0];
                    TYear = FinanceYr_Splt_2[1];
                    Month_Name = FinanceYr_Name;
                }

                else if (ddlType == 0) {  //MTD

                    var FinanceYr_Splt = FinanceYr.split(' - ');


                    Fmnth = FinanceYr_Splt[0];
                    Tmnth = FinanceYr_Splt[0];
                    FYear = FinanceYr_Splt[1];
                    TYear = FinanceYr_Splt[1];

                    Month_Name = FinanceYr_Name;
                }

                else if (ddlType == 2) {  //YTD

                    var FinanceYr_Splt1 = FinanceYr.split('  to ');

                    var From_Yr = FinanceYr_Splt1[0];

                    var FinanceYr_Splt = FinanceYr_Splt1[1].split(' - ');
                    var div_code = FinanceYr_Splt[1];

                    Fmnth = 4;
                    Tmnth = FinanceYr_Splt[0];
                    TYear = FinanceYr_Splt[1];
                    FYear = From_Yr;
                    Month_Name = FinanceYr_Name;
                }

                var FF_Name = $('#<%=lstFieldForce.ClientID%> :selected').text();

                pData[0] = Fmnth;
                pData[1] = FYear;
                pData[2] = $("#lstFieldForce").val();
                pData[3] = $("#lstMode").val();
                pData[4] = Tmnth;
                pData[5] = TYear;
                pData[6] = $("#lstDiv").val();
                pData[7] = $('#<%=lstDiv.ClientID%> :selected').text();
                var Sale = $('#<%=lstSale.ClientID%> :selected').text();
                   var common = '<%=Session["common"] %>';
                if ($("#lstFieldForce").val() == 'MGR0140') {
                  $("#lblivf").show();
                }
                if (common != '')
                {
                    $("#lblcs").show();
                }

                var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
                var function_name = "getChartVal_YTD";
                $.ajax({
                    type: "POST",
                    url: "Sales_DashBoard_Admin_Brand.aspx/" + function_name,
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_,
                    error: OnErrorCall_
                });

                function OnSuccess_(response) {
                    var aData = response.d;
                    var arr = [];
                    var sf_code, tar, sal, ach, psal, grow, pc, Div_Name, Div_Code, tar1, sal1, ach1, psal1, grow1, pc1, Div_Name1, Div_Code1, tar2, sal2, ach2, psal2, grow2, pc2, Div_Name2, Div_Code2, tar3, sal3, ach3, psal3, grow3, pc3, Div_Name3, Div_Code3, tar4, sal4, ach4, psal4, grow4, pc4, Div_Name4, Div_Code4, tar5, sal5, ach5, psal5, grow5, pc5, Div_Name5, Div_Code5, div_cnt;
                    var sf_name = $("#lstFieldForce").text();
                   
                    $.map(aData, function (item, index) {
                        div_cnt = item.Div_cnt;
                        if (div_cnt == 1) {
                            sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC; Div_Name = item.Div_Name; Div_Code = item.Div_Code;
                        }
                        else if (div_cnt == 2) {

                            sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC; Div_Name = item.Div_Name; Div_Code = item.Div_Code;
                            tar1 = item.Target1; sal1 = item.Sale1; ach1 = item.achie1; grow1 = item.Growth1; pc1 = item.PC1; Div_Name1 = item.Div_Name1; Div_Code1 = item.Div_Code1;
                            tar2 = item.Target2; sal2 = item.Sale2; ach2 = item.achie2; grow2 = item.Growth2; pc2 = item.PC2; Div_Name2 = item.Div_Name2; Div_Code2 = item.Div_Code2;
                        }
                        else if (div_cnt == 3) {

                            sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC; Div_Name = item.Div_Name; Div_Code = item.Div_Code;
                            tar1 = item.Target1; sal1 = item.Sale1; ach1 = item.achie1; grow1 = item.Growth1; pc1 = item.PC1; Div_Name1 = item.Div_Name1; Div_Code1 = item.Div_Code1;
                            tar2 = item.Target2; sal2 = item.Sale2; ach2 = item.achie2; grow2 = item.Growth2; pc2 = item.PC2; Div_Name2 = item.Div_Name2; Div_Code2 = item.Div_Code2;
                            tar3 = item.Target3; sal3 = item.Sale3; ach3 = item.achie3; grow3 = item.Growth3; pc3 = item.PC3; Div_Name3 = item.Div_Name3; Div_Code3 = item.Div_Code3;
                        }
                        else if (div_cnt == 4) {

                            sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC; Div_Name = item.Div_Name; Div_Code = item.Div_Code;
                            tar1 = item.Target1; sal1 = item.Sale1; ach1 = item.achie1; grow1 = item.Growth1; pc1 = item.PC1; Div_Name1 = item.Div_Name1; Div_Code1 = item.Div_Code1;
                            tar2 = item.Target2; sal2 = item.Sale2; ach2 = item.achie2; grow2 = item.Growth2; pc2 = item.PC2; Div_Name2 = item.Div_Name2; Div_Code2 = item.Div_Code2;
                            tar3 = item.Target3; sal3 = item.Sale3; ach3 = item.achie3; grow3 = item.Growth3; pc3 = item.PC3; Div_Name3 = item.Div_Name3; Div_Code3 = item.Div_Code3;
                            tar4 = item.Target4; sal4 = item.Sale4; ach4 = item.achie4; grow4 = item.Growth4; pc4 = item.PC4; Div_Name4 = item.Div_Name4; Div_Code4 = item.Div_Code4;
                        }
                        else if (div_cnt == 5) {

                            sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC; Div_Name = item.Div_Name; Div_Code = item.Div_Code;
                            tar1 = item.Target1; sal1 = item.Sale1; ach1 = item.achie1; grow1 = item.Growth1; pc1 = item.PC1; Div_Name1 = item.Div_Name1; Div_Code1 = item.Div_Code1;
                            tar2 = item.Target2; sal2 = item.Sale2; ach2 = item.achie2; grow2 = item.Growth2; pc2 = item.PC2; Div_Name2 = item.Div_Name2; Div_Code2 = item.Div_Code2;
                            tar3 = item.Target3; sal3 = item.Sale3; ach3 = item.achie3; grow3 = item.Growth3; pc3 = item.PC3; Div_Name3 = item.Div_Name3; Div_Code3 = item.Div_Code3;
                            tar4 = item.Target4; sal4 = item.Sale4; ach4 = item.achie4; grow4 = item.Growth4; pc4 = item.PC4; Div_Name4 = item.Div_Name4; Div_Code4 = item.Div_Code4;
                            tar5 = item.Target5; sal5 = item.Sale5; ach5 = item.achie5; grow5 = item.Growth5; pc5 = item.PC5; Div_Name5 = item.Div_Name5; Div_Code5 = item.Div_Code5;
                        }
                    });
                    var myJsonString = JSON.stringify(arr);
                    var jsonArray = JSON.parse(JSON.stringify(arr));
                    if (div_cnt == 1) {
                        document.getElementById("lbltarY").textContent = tar
                        document.getElementById("lblsalY").textContent = sal
                        document.getElementById("lblachY").textContent = ach
                        document.getElementById("lblDivY").textContent = Div_Name
                        document.getElementById("lblGrwthY").textContent = grow
                        document.getElementById("lblPCPMY").textContent = pc
                       
                        $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_PrimarySale_Multi_Mnth_HQWise.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Stok_code=" + "-2" + "&selectedValue=" + "4" + "&Div_New=" + "ALL" + "&sk_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                       // $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblivf").wrapInner("<a target=_blank href=/Sales_Dashboard_IVF.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblcs").wrapInner("<a target=_blank href=/Sales_Dashboard_CS.aspx?Sf_Code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&HQ_Code=" + "COMKOL" + "&HQ_Name=" + "" + "&Mode=" + "2" + "&sf_name=" + FF_Name + "></a>");
                    }

                    else if (div_cnt == 2) {
                        document.getElementById("lbltarY").textContent = tar
                        document.getElementById("lblsalY").textContent = sal
                        document.getElementById("lblachY").textContent = ach
                        document.getElementById("lblDivY").textContent = Div_Name
                        document.getElementById("lblGrwthY").textContent = grow
                        document.getElementById("lblPCPMY").textContent = pc

                        document.getElementById("lbltarY1").textContent = tar1
                        document.getElementById("lblsalY1").textContent = sal1
                        document.getElementById("lblachY1").textContent = ach1
                        document.getElementById("lblDivY1").textContent = Div_Name1
                        document.getElementById("lblGrwthY1").textContent = grow1
                        document.getElementById("lblPCPMY1").textContent = pc1

                        document.getElementById("lbltarY2").textContent = tar2
                        document.getElementById("lblsalY2").textContent = sal2
                        document.getElementById("lblachY2").textContent = ach2
                        document.getElementById("lblDivY2").textContent = Div_Name2
                        document.getElementById("lblGrwthY2").textContent = grow2
                        document.getElementById("lblPCPMY2").textContent = pc2

                        $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY1").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code1 + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");

                    }

                    else if (div_cnt == 3) {
                        document.getElementById("lbltarY").textContent = tar
                        document.getElementById("lblsalY").textContent = sal
                        document.getElementById("lblachY").textContent = ach
                        document.getElementById("lblDivY").textContent = Div_Name
                        document.getElementById("lblGrwthY").textContent = grow
                        document.getElementById("lblPCPMY").textContent = pc

                        document.getElementById("lbltarY1").textContent = tar1
                        document.getElementById("lblsalY1").textContent = sal1
                        document.getElementById("lblachY1").textContent = ach1
                        document.getElementById("lblDivY1").textContent = Div_Name1
                        document.getElementById("lblGrwthY1").textContent = grow1
                        document.getElementById("lblPCPMY1").textContent = pc1

                        document.getElementById("lbltarY2").textContent = tar2
                        document.getElementById("lblsalY2").textContent = sal2
                        document.getElementById("lblachY2").textContent = ach2
                        document.getElementById("lblDivY2").textContent = Div_Name2
                        document.getElementById("lblGrwthY2").textContent = grow2
                        document.getElementById("lblPCPMY2").textContent = pc2

                        document.getElementById("lbltarY3").textContent = tar3
                        document.getElementById("lblsalY3").textContent = sal3
                        document.getElementById("lblachY3").textContent = ach3
                        document.getElementById("lblDivY3").textContent = Div_Name3
                        document.getElementById("lblGrwthY3").textContent = grow3
                        document.getElementById("lblPCPMY3").textContent = pc3

                        $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY1").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code1 + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY2").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code3 + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                    }

                    else if (div_cnt == 4) {
                        document.getElementById("lbltarY").textContent = tar
                        document.getElementById("lblsalY").textContent = sal
                        document.getElementById("lblachY").textContent = ach
                        document.getElementById("lblDivY").textContent = Div_Name
                        document.getElementById("lblGrwthY").textContent = grow
                        document.getElementById("lblPCPMY").textContent = pc

                        if (ach >= 100) {
                            $("#lblachY").css("color", "Green");
                        }
                        else {
                            $("#lblachY").css("color", "Red");
                        }

                        if (grow < 0) {
                            $("#lblGrwthY").css("color", "Red");
                        }
                        else {
                            $("#lblGrwthY").css("color", "Green");
                        }

                        document.getElementById("lbltarY1").textContent = tar1
                        document.getElementById("lblsalY1").textContent = sal1
                        document.getElementById("lblachY1").textContent = ach1
                        document.getElementById("lblDivY1").textContent = Div_Name1
                        document.getElementById("lblGrwthY1").textContent = grow1
                        document.getElementById("lblPCPMY1").textContent = pc1
                        if (ach1 >= 100) {
                            $("#lblachY1").css("color", "Green");
                        }
                        else {
                            $("#lblachY1").css("color", "Red");
                        }

                        if (grow1 < 0) {
                            $("#lblGrwthY1").css("color", "Red");
                        }
                        else {
                            $("#lblGrwthY1").css("color", "Green");
                        }



                        document.getElementById("lbltarY2").textContent = tar2
                        document.getElementById("lblsalY2").textContent = sal2
                        document.getElementById("lblachY2").textContent = ach2
                        document.getElementById("lblDivY2").textContent = Div_Name2
                        document.getElementById("lblGrwthY2").textContent = grow2
                        document.getElementById("lblPCPMY2").textContent = pc2

                        if (ach2 >= 100) {
                            $("#lblachY2").css("color", "Green");
                        }
                        else {
                            $("#lblachY2").css("color", "Red");
                        }

                        if (grow2 < 0) {
                            $("#lblGrwthY2").css("color", "Red");
                        }
                        else {
                            $("#lblGrwthY2").css("color", "Green");
                        }


                        document.getElementById("lbltarY3").textContent = tar3
                        document.getElementById("lblsalY3").textContent = sal3
                        document.getElementById("lblachY3").textContent = ach3
                        document.getElementById("lblDivY3").textContent = Div_Name3
                        document.getElementById("lblGrwthY3").textContent = grow3
                        document.getElementById("lblPCPMY3").textContent = pc3

                        if (ach3 >= 100) {
                            $("#lblachY3").css("color", "Green");
                        }
                        else {
                            $("#lblachY3").css("color", "Red");
                        }

                        if (grow3 < 0) {
                            $("#lblGrwthY3").css("color", "Red");
                        }
                        else {
                            $("#lblGrwthY3").css("color", "Green");
                        }


                        document.getElementById("lbltarY4").textContent = tar4
                        document.getElementById("lblsalY4").textContent = sal4
                        document.getElementById("lblachY4").textContent = ach4
                        document.getElementById("lblDivY4").textContent = Div_Name4
                        document.getElementById("lblGrwthY4").textContent = grow4
                        document.getElementById("lblPCPMY4").textContent = pc4

                        if (ach4 >= 100) {
                            $("#lblachY4").css("color", "Green");
                        }
                        else {
                            $("#lblachY4").css("color", "Red");
                        }

                        if (grow4 < 0) {
                            $("#lblGrwthY4").css("color", "Red");
                        }
                        else {
                            $("#lblGrwthY4").css("color", "Green");
                        }

                        $("#lbltarY4").css("font-weight", "bold");
                        $("#lblsalY4").css("font-weight", "bold");
                        $("#lblachY4").css("font-weight", "bold");
                        $("#lblDivY4").css("font-weight", "bold");
                        $("#lblGrwthY4").css("font-weight", "bold");
                        $("#lblPCPMY4").css("font-weight", "bold");

                        $("#tdtarY4").css("background-color", "#CCFFFF");
                        $("#tdsalY4").css("background-color", "#CCFFFF");
                        $("#tdachY4").css("background-color", "#CCFFFF");
                        $("#tdDivY4").css("background-color", "#CCFFFF");
                        $("#tdGrwthY4").css("background-color", "#CCFFFF");
                        $("#tdPCPMY4").css("background-color", "#CCFFFF");


                        $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY1").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code1 + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY2").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code3 + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY3").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code4 + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                    }

                    else if (div_cnt == 5) {
                        document.getElementById("lbltarY").textContent = tar
                        document.getElementById("lblsalY").textContent = sal
                        document.getElementById("lblachY").textContent = ach
                        document.getElementById("lblDivY").textContent = Div_Name
                        document.getElementById("lblGrwthY").textContent = grow
                        document.getElementById("lblPCPMY").textContent = pc

                        document.getElementById("lbltarY1").textContent = tar1
                        document.getElementById("lblsalY1").textContent = sal1
                        document.getElementById("lblachY1").textContent = ach1
                        document.getElementById("lblDivY1").textContent = Div_Name1
                        document.getElementById("lblGrwthY1").textContent = grow1
                        document.getElementById("lblPCPMY1").textContent = pc1

                        document.getElementById("lbltarY2").textContent = tar2
                        document.getElementById("lblsalY2").textContent = sal2
                        document.getElementById("lblachY2").textContent = ach2
                        document.getElementById("lblDivY2").textContent = Div_Name2
                        document.getElementById("lblGrwthY2").textContent = grow2
                        document.getElementById("lblPCPMY2").textContent = pc2

                        document.getElementById("lbltarY3").textContent = tar3
                        document.getElementById("lblsalY3").textContent = sal3
                        document.getElementById("lblachY3").textContent = ach3
                        document.getElementById("lblDivY3").textContent = Div_Name3
                        document.getElementById("lblGrwthY3").textContent = grow3
                        document.getElementById("lblPCPMY3").textContent = pc3

                        document.getElementById("lbltarY4").textContent = tar4
                        document.getElementById("lblsalY4").textContent = sal4
                        document.getElementById("lblachY4").textContent = ach4
                        document.getElementById("lblDivY4").textContent = Div_Name4
                        document.getElementById("lblGrwthY4").textContent = grow4
                        document.getElementById("lblPCPMY4").textContent = pc4

                        document.getElementById("lbltarY5").textContent = tar5
                        document.getElementById("lblsalY5").textContent = sal5
                        document.getElementById("lblachY5").textContent = ach5
                        document.getElementById("lblDivY5").textContent = Div_Name5
                        document.getElementById("lblGrwthY5").textContent = grow5
                        document.getElementById("lblPCPMY5").textContent = pc5

                        $("#lbltarY5").css("color", "Red");
                        $("#lblsalY5").css("color", "Red");
                        $("#lblachY5").css("color", "Red");
                        $("#lblDivY5").css("color", "Red");
                        $("#lblGrwthY5").css("color", "Red");
                        $("#lblPCPMY5").css("color", "Red");

                        $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY1").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code1 + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY2").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code3 + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY3").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code4 + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY4").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code5 + "&Brand_Code=" + "-2" + "&selectedValue=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                    }
                }
                function OnErrorCall_(response) {
                    alert("Error: No Data Found!");
                }
                //  e.preventDefault();
            }

            function Primary() {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });


                var Fmnth = '';
                var Tmnth = '';
                var FYear = '';
                var TYear = '';

                var Month_Name = '';

                var monthShortNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
                ];

                var d = new Date();

                var FinanceYr = $("#lstFinacyr").val();
                var FinanceYr_Name = $('#<%=lstFinacyr.ClientID%> :selected').text();

                var ddlType = $("#lstMode").val();
                if (ddlType == 1) {
                    var FinanceYr_Splt = FinanceYr.split(' to ');
                    var FinanceYr_Splt_1 = FinanceYr_Splt[0].split('  - ');
                    var FinanceYr_Splt_2 = FinanceYr_Splt[1].split(' - ');

                    Fmnth = FinanceYr_Splt_1[0];
                    FYear = FinanceYr_Splt_1[1];

                    Tmnth = FinanceYr_Splt_2[0];
                    TYear = FinanceYr_Splt_2[1];

                    Month_Name = FinanceYr_Name;
                }

                else if (ddlType == 0) {  //MTD

                    var FinanceYr_Splt = FinanceYr.split(' - ');


                    Fmnth = FinanceYr_Splt[0];
                    Tmnth = FinanceYr_Splt[0];
                    FYear = FinanceYr_Splt[1];
                    TYear = FinanceYr_Splt[1];

                    //Month_Name = monthShortNames[0] + ' - ' + d.getFullYear() + ' to ' + monthShortNames[2] + ' - ' + d.getFullYear()
                    Month_Name = FinanceYr_Name;

                }

                else if (ddlType == 2) {  //YTD

                    var FinanceYr_Splt1 = FinanceYr.split('  to ');

                    var From_Yr = FinanceYr_Splt1[0];

                    var FinanceYr_Splt = FinanceYr_Splt1[1].split(' - ');
                    var div_code = FinanceYr_Splt[1];

                    Fmnth = 4;
                    Tmnth = FinanceYr_Splt[0];
                    TYear = FinanceYr_Splt[1];
                    FYear = From_Yr;
                    Month_Name = FinanceYr_Name;
                }


                var Month = Fmnth;
                var TMonth = Tmnth;
                var Year = FYear;
                var TYear = TYear;
                var Field = $("#lstFieldForce").val();
                var FF_Name = $('#<%=lstFieldForce.ClientID%> :selected').text();
                var mode = $("#lstMode").val();
                var Div = $("#lstDiv").val();
                var Div_Name = $('#<%=lstDiv.ClientID%> :selected').text();
                var Div_New = $("#lstDiv_new").val();
                var Div_Name_New = $('#<%=lstDiv_new.ClientID%> :selected').text();

                var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + Div + "^" + Div_Name + "^" + Div_New + "^" + Div_Name_New;
                $.ajax({

                    type: 'POST',

                    url: "Sales_DashBoard_Admin_Brand.aspx/Primary",

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
                                link: "n-/MasterFiles/AnalysisReports/rpt_Primary_Sale_StockistWise_Product.aspx?sf_code=" + Field + "&Frm_Month=" + Month + "&Frm_year=" + Year + "&To_year=" + TYear + "&To_Month=" + TMonth + "&div_Code=" + Div + "&Brand_Code=" + chartData[i].Code + "&Brand_Name=" + chartData[i].Label + "&sf_name=" + FF_Name + "&selectedValue=" + "5" + "&Div_New=" + Div_New

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
            }

            function SecondaryYTD() {

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

                var d = new Date();

                var FinanceYr = $("#lstFinacyr").val();
                var FinanceYr_Name = $('#<%=lstFinacyr.ClientID%> :selected').text();
                var ddlType = $("#lstMode").val();
                if (ddlType == 1) {
                    var FinanceYr_Splt = FinanceYr.split(' to ');
                    var FinanceYr_Splt_1 = FinanceYr_Splt[0].split('  - ');
                    var FinanceYr_Splt_2 = FinanceYr_Splt[1].split(' - ');

                    Fmnth = FinanceYr_Splt_1[0];
                    FYear = FinanceYr_Splt_1[1];

                    Tmnth = FinanceYr_Splt_2[0];
                    TYear = FinanceYr_Splt_2[1];

                    Month_Name = FinanceYr_Name;

                }

                else if (ddlType == 0) {  //MTD

                    var FinanceYr_Splt = FinanceYr.split(' - ');


                    Fmnth = FinanceYr_Splt[0];
                    Tmnth = FinanceYr_Splt[0];
                    FYear = FinanceYr_Splt[1];
                    TYear = FinanceYr_Splt[1];

                    //Month_Name = monthShortNames[0] + ' - ' + d.getFullYear() + ' to ' + monthShortNames[2] + ' - ' + d.getFullYear()
                    Month_Name = FinanceYr_Name;

                }

                else if (ddlType == 2) {  //YTD

                    var FinanceYr_Splt1 = FinanceYr.split('  to ');

                    var From_Yr = FinanceYr_Splt1[0];

                    var FinanceYr_Splt = FinanceYr_Splt1[1].split(' - ');
                    var div_code = FinanceYr_Splt[1];

                    Fmnth = 4;
                    Tmnth = FinanceYr_Splt[0];
                    TYear = FinanceYr_Splt[1];
                    FYear = From_Yr;

                    //if (d.getMonth() == 0 || d.getMonth() == 1 || d.getMonth() == 2) {
                    //    FYear = (d.getFullYear()) - 1;
                    //}
                    //else {
                    //    FYear = d.getFullYear();
                    //}
                    //Month_Name = monthShortNames[0] + ' - ' + d.getFullYear() + ' to ' + monthShortNames[2] + ' - ' + d.getFullYear()
                    Month_Name = FinanceYr_Name;

                }

                var FF_Name = $('#<%=lstFieldForce.ClientID%> :selected').text();
                pData[0] = Fmnth;
                pData[1] = FYear;
                pData[2] = $("#lstFieldForce").val();
                pData[3] = $("#lstMode").val();
                pData[4] = Tmnth;
                pData[5] = TYear;
                pData[6] = $("#lstDiv").val();
                pData[7] = $('#<%=lstDiv.ClientID%> :selected').text();
                var Sale = $('#<%=lstSale.ClientID%> :selected').text();
                //document.getElementById("lblmnthyr").textContent = FF_Name + ' - ' + Month_Name + '(' + Sale + ')'
                //document.getElementById("lblmnthyr").textContent = Month_Name
                //document.getElementById("lbltarsaleY").textContent = FF_Name

                var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
                var function_name = "getSecondary_Sale_YTD";
                $.ajax({
                    type: "POST",
                    url: "Sales_DashBoard_Admin_Brand.aspx/" + function_name,
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_,
                    error: OnErrorCall_
                });

                function OnSuccess_(response) {
                    var aData = response.d;
                    var arr = [];
                    var sf_code, tar, sal, ach, psal, grow, pc, Div_Name, Div_Code, tar1, sal1, ach1, psal1, grow1, pc1, Div_Name1, Div_Code1, tar2, sal2, ach2, psal2, grow2, pc2, Div_Name2, Div_Code2, tar3, sal3, ach3, psal3, grow3, pc3, Div_Name3, Div_Code3, tar4, sal4, ach4, psal4, grow4, pc4, Div_Name4, Div_Code4, tar5, sal5, ach5, psal5, grow5, pc5, Div_Name5, Div_Code5, div_cnt;
                    var sf_name = $("#lstFieldForce").text();

                    $.map(aData, function (item, index) {
                        div_cnt = item.Div_cnt;
                        if (div_cnt == 1) {
                            sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC; Div_Name = item.Div_Name; Div_Code = item.Div_Code;
                        }

                        else if (div_cnt == 2) {

                            sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC; Div_Name = item.Div_Name; Div_Code = item.Div_Code;
                            tar1 = item.Target1; sal1 = item.Sale1; ach1 = item.achie1; grow1 = item.Growth1; pc1 = item.PC1; Div_Name1 = item.Div_Name1; Div_Code1 = item.Div_Code1;
                            tar2 = item.Target2; sal2 = item.Sale2; ach2 = item.achie2; grow2 = item.Growth2; pc2 = item.PC2; Div_Name2 = item.Div_Name2; Div_Code2 = item.Div_Code2;
                        }
                        else if (div_cnt == 3) {

                            sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC; Div_Name = item.Div_Name; Div_Code = item.Div_Code;
                            tar1 = item.Target1; sal1 = item.Sale1; ach1 = item.achie1; grow1 = item.Growth1; pc1 = item.PC1; Div_Name1 = item.Div_Name1; Div_Code1 = item.Div_Code1;
                            tar2 = item.Target2; sal2 = item.Sale2; ach2 = item.achie2; grow2 = item.Growth2; pc2 = item.PC2; Div_Name2 = item.Div_Name2; Div_Code2 = item.Div_Code2;
                            tar3 = item.Target3; sal3 = item.Sale3; ach3 = item.achie3; grow3 = item.Growth3; pc3 = item.PC3; Div_Name3 = item.Div_Name3; Div_Code3 = item.Div_Code3;
                        }
                        else if (div_cnt == 4) {

                            sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC; Div_Name = item.Div_Name; Div_Code = item.Div_Code;
                            tar1 = item.Target1; sal1 = item.Sale1; ach1 = item.achie1; grow1 = item.Growth1; pc1 = item.PC1; Div_Name1 = item.Div_Name1; Div_Code1 = item.Div_Code1;
                            tar2 = item.Target2; sal2 = item.Sale2; ach2 = item.achie2; grow2 = item.Growth2; pc2 = item.PC2; Div_Name2 = item.Div_Name2; Div_Code2 = item.Div_Code2;
                            tar3 = item.Target3; sal3 = item.Sale3; ach3 = item.achie3; grow3 = item.Growth3; pc3 = item.PC3; Div_Name3 = item.Div_Name3; Div_Code3 = item.Div_Code3;
                            tar4 = item.Target4; sal4 = item.Sale4; ach4 = item.achie4; grow4 = item.Growth4; pc4 = item.PC4; Div_Name4 = item.Div_Name4; Div_Code4 = item.Div_Code4;
                        }
                        else if (div_cnt == 5) {

                            sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC; Div_Name = item.Div_Name; Div_Code = item.Div_Code;
                            tar1 = item.Target1; sal1 = item.Sale1; ach1 = item.achie1; grow1 = item.Growth1; pc1 = item.PC1; Div_Name1 = item.Div_Name1; Div_Code1 = item.Div_Code1;
                            tar2 = item.Target2; sal2 = item.Sale2; ach2 = item.achie2; grow2 = item.Growth2; pc2 = item.PC2; Div_Name2 = item.Div_Name2; Div_Code2 = item.Div_Code2;
                            tar3 = item.Target3; sal3 = item.Sale3; ach3 = item.achie3; grow3 = item.Growth3; pc3 = item.PC3; Div_Name3 = item.Div_Name3; Div_Code3 = item.Div_Code3;
                            tar4 = item.Target4; sal4 = item.Sale4; ach4 = item.achie4; grow4 = item.Growth4; pc4 = item.PC4; Div_Name4 = item.Div_Name4; Div_Code4 = item.Div_Code4;
                            tar5 = item.Target5; sal5 = item.Sale5; ach5 = item.achie5; grow5 = item.Growth5; pc5 = item.PC5; Div_Name5 = item.Div_Name5; Div_Code5 = item.Div_Code5;
                        }
                    });
                    var myJsonString = JSON.stringify(arr);
                    var jsonArray = JSON.parse(JSON.stringify(arr));
                    if (div_cnt == 1) {
                        document.getElementById("lbltarY").textContent = tar
                        document.getElementById("lblsalY").textContent = sal
                        document.getElementById("lblachY").textContent = ach
                        document.getElementById("lblDivY").textContent = Div_Name
                        document.getElementById("lblGrwthY").textContent = grow
                        document.getElementById("lblPCPMY").textContent = pc

                        $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                    }

                    else if (div_cnt == 2) {
                        document.getElementById("lbltarY").textContent = tar
                        document.getElementById("lblsalY").textContent = sal
                        document.getElementById("lblachY").textContent = ach
                        document.getElementById("lblDivY").textContent = Div_Name
                        document.getElementById("lblGrwthY").textContent = grow
                        document.getElementById("lblPCPMY").textContent = pc

                        document.getElementById("lbltarY1").textContent = tar1
                        document.getElementById("lblsalY1").textContent = sal1
                        document.getElementById("lblachY1").textContent = ach1
                        document.getElementById("lblDivY1").textContent = Div_Name1
                        document.getElementById("lblGrwthY1").textContent = grow1
                        document.getElementById("lblPCPMY1").textContent = pc1

                        document.getElementById("lbltarY2").textContent = tar2
                        document.getElementById("lblsalY2").textContent = sal2
                        document.getElementById("lblachY2").textContent = ach2
                        document.getElementById("lblDivY2").textContent = Div_Name2
                        document.getElementById("lblGrwthY2").textContent = grow2
                        document.getElementById("lblPCPMY2").textContent = pc2

                        $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY1").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code1 + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");

                    }

                    else if (div_cnt == 3) {
                        document.getElementById("lbltarY").textContent = tar
                        document.getElementById("lblsalY").textContent = sal
                        document.getElementById("lblachY").textContent = ach
                        document.getElementById("lblDivY").textContent = Div_Name
                        document.getElementById("lblGrwthY").textContent = grow
                        document.getElementById("lblPCPMY").textContent = pc

                        document.getElementById("lbltarY1").textContent = tar1
                        document.getElementById("lblsalY1").textContent = sal1
                        document.getElementById("lblachY1").textContent = ach1
                        document.getElementById("lblDivY1").textContent = Div_Name1
                        document.getElementById("lblGrwthY1").textContent = grow1
                        document.getElementById("lblPCPMY1").textContent = pc1

                        document.getElementById("lbltarY2").textContent = tar2
                        document.getElementById("lblsalY2").textContent = sal2
                        document.getElementById("lblachY2").textContent = ach2
                        document.getElementById("lblDivY2").textContent = Div_Name2
                        document.getElementById("lblGrwthY2").textContent = grow2
                        document.getElementById("lblPCPMY2").textContent = pc2

                        document.getElementById("lbltarY3").textContent = tar3
                        document.getElementById("lblsalY3").textContent = sal3
                        document.getElementById("lblachY3").textContent = ach3
                        document.getElementById("lblDivY3").textContent = Div_Name3
                        document.getElementById("lblGrwthY3").textContent = grow3
                        document.getElementById("lblPCPMY3").textContent = pc3

                        $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY1").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code1 + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY2").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code3 + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                    }

                    else if (div_cnt == 4) {
                        document.getElementById("lbltarY").textContent = tar
                        document.getElementById("lblsalY").textContent = sal
                        document.getElementById("lblachY").textContent = ach
                        document.getElementById("lblDivY").textContent = Div_Name
                        document.getElementById("lblGrwthY").textContent = grow
                        document.getElementById("lblPCPMY").textContent = pc

                        document.getElementById("lbltarY1").textContent = tar1
                        document.getElementById("lblsalY1").textContent = sal1
                        document.getElementById("lblachY1").textContent = ach1
                        document.getElementById("lblDivY1").textContent = Div_Name1
                        document.getElementById("lblGrwthY1").textContent = grow1
                        document.getElementById("lblPCPMY1").textContent = pc1

                        document.getElementById("lbltarY2").textContent = tar2
                        document.getElementById("lblsalY2").textContent = sal2
                        document.getElementById("lblachY2").textContent = ach2
                        document.getElementById("lblDivY2").textContent = Div_Name2
                        document.getElementById("lblGrwthY2").textContent = grow2
                        document.getElementById("lblPCPMY2").textContent = pc2

                        document.getElementById("lbltarY3").textContent = tar3
                        document.getElementById("lblsalY3").textContent = sal3
                        document.getElementById("lblachY3").textContent = ach3
                        document.getElementById("lblDivY3").textContent = Div_Name3
                        document.getElementById("lblGrwthY3").textContent = grow3
                        document.getElementById("lblPCPMY3").textContent = pc3

                        document.getElementById("lbltarY4").textContent = tar4
                        document.getElementById("lblsalY4").textContent = sal4
                        document.getElementById("lblachY4").textContent = ach4
                        document.getElementById("lblDivY4").textContent = Div_Name4
                        document.getElementById("lblGrwthY4").textContent = grow4
                        document.getElementById("lblPCPMY4").textContent = pc4

                        $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY1").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code1 + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY2").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code3 + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY3").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code4 + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                    }

                    else if (div_cnt == 5) {
                        document.getElementById("lbltarY").textContent = tar
                        document.getElementById("lblsalY").textContent = sal
                        document.getElementById("lblachY").textContent = ach
                        document.getElementById("lblDivY").textContent = Div_Name
                        document.getElementById("lblGrwthY").textContent = grow
                        document.getElementById("lblPCPMY").textContent = pc

                        document.getElementById("lbltarY1").textContent = tar1
                        document.getElementById("lblsalY1").textContent = sal1
                        document.getElementById("lblachY1").textContent = ach1
                        document.getElementById("lblDivY1").textContent = Div_Name1
                        document.getElementById("lblGrwthY1").textContent = grow1
                        document.getElementById("lblPCPMY1").textContent = pc1

                        document.getElementById("lbltarY2").textContent = tar2
                        document.getElementById("lblsalY2").textContent = sal2
                        document.getElementById("lblachY2").textContent = ach2
                        document.getElementById("lblDivY2").textContent = Div_Name2
                        document.getElementById("lblGrwthY2").textContent = grow2
                        document.getElementById("lblPCPMY2").textContent = pc2

                        document.getElementById("lbltarY3").textContent = tar3
                        document.getElementById("lblsalY3").textContent = sal3
                        document.getElementById("lblachY3").textContent = ach3
                        document.getElementById("lblDivY3").textContent = Div_Name3
                        document.getElementById("lblGrwthY3").textContent = grow3
                        document.getElementById("lblPCPMY3").textContent = pc3

                        document.getElementById("lbltarY4").textContent = tar4
                        document.getElementById("lblsalY4").textContent = sal4
                        document.getElementById("lblachY4").textContent = ach4
                        document.getElementById("lblDivY4").textContent = Div_Name4
                        document.getElementById("lblGrwthY4").textContent = grow4
                        document.getElementById("lblPCPMY4").textContent = pc4

                        document.getElementById("lbltarY5").textContent = tar5
                        document.getElementById("lblsalY5").textContent = sal5
                        document.getElementById("lblachY5").textContent = ach5
                        document.getElementById("lblDivY5").textContent = Div_Name5
                        document.getElementById("lblGrwthY5").textContent = grow5
                        document.getElementById("lblPCPMY5").textContent = pc5

                        $("#lblDivY").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY1").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code1 + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY2").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code3 + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY3").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code4 + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                        $("#lblDivY4").wrapInner("<a target=_blank href=/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?sf_code=" + sf_code + "&Frm_Month=" + pData[0] + "&Frm_year=" + pData[1] + "&To_year=" + pData[5] + "&To_Month=" + pData[4] + "&div_Code=" + Div_Code5 + "&Brand_Code=" + "-2" + "&Mode=" + "5" + "&Div_New=" + "ALL" + "&Brand_Name=" + "ALL" + "&sf_name=" + FF_Name + "></a>");
                    }
                }
                function OnErrorCall_(response) {
                    alert("Error: No Data Found!");
                }
                //  e.preventDefault();


            }



            function Secondary() {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });


                var Fmnth = '';
                var Tmnth = '';
                var FYear = '';
                var TYear = '';

                var Month_Name = '';

                var monthShortNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
                ];

                var d = new Date();


                var FinanceYr = $("#lstFinacyr").val();

                var FinanceYr_Name = $('#<%=lstFinacyr.ClientID%> :selected').text();
                var ddlType = $("#lstMode").val();


                if (ddlType == 1) {

                    var FinanceYr_Splt = FinanceYr.split(' to ');
                    var FinanceYr_Splt_1 = FinanceYr_Splt[0].split('  - ');
                    var FinanceYr_Splt_2 = FinanceYr_Splt[1].split(' - ');

                    Fmnth = FinanceYr_Splt_1[0];
                    FYear = FinanceYr_Splt_1[1];

                    Tmnth = FinanceYr_Splt_2[0];
                    TYear = FinanceYr_Splt_2[1];

                    Month_Name = FinanceYr_Name;
                }

                else if (ddlType == 0) {  //MTD

                    var FinanceYr_Splt = FinanceYr.split(' - ');


                    Fmnth = FinanceYr_Splt[0];
                    Tmnth = FinanceYr_Splt[0];
                    FYear = FinanceYr_Splt[1];
                    TYear = FinanceYr_Splt[1];

                    //Month_Name = monthShortNames[0] + ' - ' + d.getFullYear() + ' to ' + monthShortNames[2] + ' - ' + d.getFullYear()
                    Month_Name = FinanceYr_Name;

                }

                else if (ddlType == 2) {  //YTD

                    var FinanceYr_Splt1 = FinanceYr.split('  to ');

                    var From_Yr = FinanceYr_Splt1[0];

                    var FinanceYr_Splt = FinanceYr_Splt1[1].split(' - ');
                    var div_code = FinanceYr_Splt[1];

                    Fmnth = 4;
                    Tmnth = FinanceYr_Splt[0];
                    TYear = FinanceYr_Splt[1];
                    FYear = From_Yr;

                    Month_Name = FinanceYr_Name;
                }


                var Month = Fmnth;
                var TMonth = Tmnth;
                var Year = FYear;
                var TYear = TYear;
                var Field = $("#lstFieldForce").val();
                var FF_Name = $('#<%=lstFieldForce.ClientID%> :selected').text();
                var mode = $("#lstMode").val();
                var Div = $("#lstDiv").val();
                var Div_Name = $('#<%=lstDiv.ClientID%> :selected').text();
                var Div_New = $("#lstDiv_new").val();
                var Div_Name_New = $('#<%=lstDiv_new.ClientID%> :selected').text();

                var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + TMonth + "^" + TYear + "^" + Div + "^" + Div_Name + "^" + Div_New + "^" + Div_Name_New;
                $.ajax({

                    type: 'POST',

                    url: "Sales_DashBoard_Admin_Brand.aspx/Secondary",

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
                                //"plotTooltext": "Brand : $label<br>Value : $value",
                                "tooltext": "Brand : " + chartData[i].Label + "<br>Target : " + chartData[i].Target_Val + "<br>Sales : " + chartData[i].Value + "<br>Ach (%) : " + chartData[i].achie + "<br>Growth (%) : " + chartData[i].Growth + "<br>PCPM : " + chartData[i].PC + "<br>Division Name : " + chartData[i].Division_Name,
                                //link: "JavaScript: showModalPopUp(" + $("#lstFieldForce").val() + "," + Month + "," + Year + "," + TMonth + "," + TYear + "," + $("#lstFieldForce").text + ")"
                                //link: "n-Sales_DashBoard_Admin_Brand.aspx?Brand : " + chartData[i].Code + "&Drs_Cnt :" + chartData[i].Value
                                link: "n-/MasterFiles/AnalysisReports/rpt_Secondary_StkProd_Bill_View.aspx?Sf_Code=" + Field + "&Frm_Month=" + Month + "&Frm_year=" + Year + "&To_year=" + TYear + "&To_Month=" + TMonth + "&div_Code=" + Div + "&Brand_Code=" + chartData[i].Code + "&Brand_Name=" + chartData[i].Label + "&sf_name=" + FF_Name + "&Mode=" + "5" + "&Div_New=" + Div_New
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
                                "showPercentValues": "0",
                                "showPercentInToolTip": "0",
                                //Setting legend to appear on right side
                                // "paletteColors": "#007bff,#71e3e0,#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                "paletteColors": "#33558B,#77A033,#7F6084,#F79647,#4AACC5,#8064A1,#23BFAA,#9BBB58,#C0504E,#4F81BC   ,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                "legendPosition": "bottom",
                                //Caption for legend

                                //Customization for legend scroll bar cosmetics
                                "legendScrollBgColor": "#cccccc",
                                "legendScrollBarColor": "#999999"
                                //,"link": "http://google.com/",
                                //"plotTooltext": "Brand : $label<br>Value : $value",
                                //"link": "F-drill-https://en.wikipedia.org/wiki/Cranberry"
                                //adding ClickURL: to make chart a hotspot    
                                //"clickURL": "n-http://google.com"
                                //"clickURL": "n-Sales_DashBoard_Admin_Brand.aspx?Brand : $label&Drs Cnt : $value"
                                //"clickURL": "j-showModalPopUp('','','','')"
                                //"width": "550",
                                //"height": "350" 
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
        </script>
      <%--  <asp:HiddenField ID="hdncommon" runat="server"  Visible="false">   </asp:HiddenField>--%>

        <header class="header-area">
            <div class="row align-items-center">
                <div class="col-lg-6">
                    <div class="header-left">
                        <a href="#">
                            <img src="images/logo-1.png" alt="">
                        </a>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="header-right text-right ">
                        <ul>
                            <li id="liback" runat="server"><a href="#"><i class="fas fa-chevron-left"></i>
                                <input type="button" id="btnback" class="Viewbutton" value="Back" runat="server" onserverclick="btnback_click" />
                            </a></li>

                            <li id="liSFE" runat="server"><a href="#"><i class="far fa-lightbulb"></i>
                                <asp:Button ID="btnSFE" runat="server" CssClass="Viewbutton"
                                    Text="SFE" OnClick="btnSFE_Click" /></a></li>

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
                            <asp:ListBox ID="lstDiv" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstDiv_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="col-xl col-lg-4 col-md-6">
                        <div>
                            <asp:ListBox ID="lstFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstFieldForce_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="col-xl col-lg-4 col-md-6">
                        <div>
                            <asp:ListBox ID="lstMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstMode_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="MTD" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="QTD"></asp:ListItem>
                                <asp:ListItem Value="2" Text="YTD"></asp:ListItem>
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="col-xl col-lg-4 col-md-6">
                        <div>
                            <asp:ListBox ID="lstFinacyr" runat="server" AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="col-xl col-lg-4 col-md-6">
                        <div>
                            <ul>
                                <li class="lst-slc">
                                    <asp:ListBox ID="lstSale" runat="server" AutoPostBack="true">
                                        <asp:ListItem Value="1" Text="Primary" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Secondary"></asp:ListItem>
                                    </asp:ListBox>
                                </li>



                            </ul>
                        </div>
                    </div>
                    <div class="col-xl col-lg-4 col-md-6">
                        <div class="drop-item">
                            <ul>
                                <li>
<%--<a href="#"><i id="btnGo" class="fas fa-arrow-right"></i></a>--%>
<input type="button" id="btnGo" class="fas fa-arrow-right" style="background:#33B1FF" value="Go" runat="server" />
</li>
                            </ul>
                            <%--<input type="button" id="Button1" class="fas fa-arrow-right"  value="->" runat="server" />--%>
                        </div>
                    </div>
                </div>
            </div>
          
        </div>


        <div id="chrt1" runat="server" class="main-wrapper-area">
              <asp:Label ID="lbllast" runat="server" ForeColor="Black" Font-Bold="true" >Last Uploaded Date (Primary) :</asp:Label>
             <asp:Label ID="lblldate" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>
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
                                          <span id="lblivf"  style= "display:none;color:Black; font-family:Verdana;font-size:small;">IVF</span>
                                                <span id="lblcs"  style= "display:none;color:red; font-family:Verdana;font-size:small;">    CS</span>
                                        </td>
                                        
                                    </tr>
                                    <tr class="mar-row"></tr>
                                    <tr id="trDashRow1" runat="server">
                                        <td>
                                            <asp:Label ID="lblDivY1" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="9"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbltarY1" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblsalY1" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblachY1" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblGrwthY1" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblPCPMY1" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label></td>
                                    </tr>
                                    <tr class="mar-row"></tr>
                                    <tr id="trDashRow2" runat="server">
                                        <td>
                                            <asp:Label ID="lblDivY2" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="9"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lbltarY2" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblsalY2" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblachY2" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblGrwthY2" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPCPMY2" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="mar-row"></tr>
                                    <tr id="trDashRow3" runat="server">
                                        <td>
                                            <asp:Label ID="lblDivY3" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="9"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lbltarY3" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblsalY3" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblachY3" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblGrwthY3" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPCPMY3" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="mar-row"></tr>
                                    <tr id="trDashRow4" runat="server">
                                        <td id="tdDivY4" runat="server">
                                            <asp:Label ID="lblDivY4" runat="server" Font-Size="9"></asp:Label>
                                        </td>

                                        <td id="tdtarY4" runat="server">
                                            <asp:Label ID="lbltarY4" runat="server" Font-Size="8"></asp:Label>
                                        </td>

                                        <td id="tdsalY4" runat="server">
                                            <asp:Label ID="lblsalY4" runat="server" Font-Size="8"></asp:Label>
                                        </td>

                                        <td id="tdachY4" runat="server">
                                            <asp:Label ID="lblachY4" runat="server" Font-Size="8"></asp:Label>
                                        </td>

                                        <td id="tdGrwthY4" runat="server">
                                            <asp:Label ID="lblGrwthY4" runat="server" Font-Size="8"></asp:Label>
                                        </td>
                                        <td id="tdPCPMY4" runat="server">
                                            <asp:Label ID="lblPCPMY4" runat="server" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr id="trDashRow5" runat="server">
                                        <td>
                                            <span id="lblDivY5" style="color: Red; font-family: Verdana; font-size: 11pt;">Total</span>

                                        </td>

                                        <td>
                                            <asp:Label ID="lbltarY5" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="10"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblsalY5" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="10"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblachY5" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="10"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblGrwthY5" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="10"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPCPMY5" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="8"></asp:Label>
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
                                <div>

                                    <asp:ListBox ID="lstDiv_new" runat="server" Width="50px">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:ListBox>

                                </div>
                                <div class="rttop-right">
                                    <ul>
                                        <li>
<%--<a href="#"><i id="btnGoBrand" class="fas fa-arrow-right"></i></a>--%>
<input type="button" id="btnGoBrand" class="fas fa-arrow-right" style="background:#33B1FF" value="Go" runat="server" />
</li>
                                    </ul>
                                    <%--<input type="button" id="Button1" class="fas fa-arrow-right"  value="->" runat="server" />--%>
                                </div>
                            </div>
                            <div class="chart" id="chart-container" style="height: 370px; width: 100%; margin: 0 auto; align-items: flex-start;"></div>

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

        <!-- Bootstrap Propper jQuery -->
        <script src="js/Popper.js"></script>

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
