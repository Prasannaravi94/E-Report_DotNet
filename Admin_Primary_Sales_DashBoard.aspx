<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Primary_Sales_DashBoard.aspx.cs" Inherits="Admin_Primary_Sales_DashBoard" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menumas" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Welcome Corporate – HQ</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/png" href="assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="assets/css/nice-select.css">
    <link rel="stylesheet" href="assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/css/style.css">
    <link rel="stylesheet" href="assets/css/responsive.css">
    <link rel="stylesheet" href="assets/css/mr_dashboard_style.css" />
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
            width: 1200px;
            margin: 0 auto;
            position: relative;
        }

            #container > div {
                width: 100%;
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
            width: 450px;
            vertical-align: central;
        }

            .single-block-area th {
                text-align: center;
                color: #fff;
                text-align: center;
                padding: 15px;
                border-radius: 10px 10px 0px 0px;
            }

            .single-block-area td {
                padding: 15px;
            }

            .single-block-area tr td {
                border-bottom: 1px solid #DCE2E8;
            }
    </style>

    <style type="text/css">
        .boxshadow {
            -moz-box-shadow: 3px 3px 5px #535353;
            -webkit-box-shadow: 3px 3px 5px #535353;
            box-shadow: 3px 3px 5px #535353;
        }

        .roundbox {
            -moz-border-radius: 6px 6px 6px 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px 6px 6px 6px;
        }

        .grd {
            border: 1;
            border-color: Black;
        }

        .roundbox-top {
            -moz-border-radius: 6px 6px 0 0;
            -webkit-border-radius: 6px 6px 0 0;
            border-radius: 6px 6px 0 0;
        }

        .roundbox-bottom {
            -moz-border-radius: 0 0 6px 6px;
            -webkit-border-radius: 0 0 6px 6px;
            border-radius: 0 0 6px 6px;
        }

        .gridheader, .gridheaderbig, .gridheaderleft, .gridheaderright {
            padding: 6px 6px 6px 6px;
            background: #F7DCB4 url(images/vertgradient.png) repeat-x;
            text-align: center;
            font-weight: bold;
            text-decoration: none;
            color: Blue;
        }

        .gridheaderleft {
            text-align: left;
        }

        .gridheaderright {
            text-align: right;
        }

        .gridheaderbig {
            font-size: 135%;
        }
    </style>
    <style type="text/css">
        .tableStyle14 {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 300px;
            height: 150px;
        }
            /*Style for Table Head - TH*/
            .tableStyle14 th {
                text-align: left;
                background-color: #9C6B98;
                color: #fff;
                text-align: left;
                padding: 20px;
                font-size: 14px;
                font-weight: bold;
            }
            /*TD and TH Style*/
            .tableStyle14 td, .tableStyle14 th {
                border: 1px solid #dedede; /*Border color*/
                padding: 10px;
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
                    background-color: #e5423f;
                }

        .nice-select {
            color: #90a1ac;
            font-size: 14px;
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
        }
    </style>




    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script>
    <script src="DashBoard/JS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/fusioncharts.js"></script>
<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/themes/fusioncharts.theme.fint.js?cacheBust=56"></script>--%>
    <script src="DashBoard/js1/fusioncharts.js" type="text/javascript"></script>
    <script src="DashBoard/js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var ddlmode = $("#ddlmode").val();
            if (ddlmode == "0") {

            }
            else if (ddlmode == "1") {

            }
        });
    </script>

    <link href="assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            Primary();
            Secondary();
            PrimaryYTD();
            SecondaryYTD();
        });
        //*
    </script>
    <script type="text/javascript">
        function GetMonthName(monthNumber) {
            var months = ['Jan', 'Feb', 'Mar', 'April', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
            return months[monthNumber - 1];
        }
    </script>
    <script type="text/javascript">
        function SecondaryYTD() {
            var pData = [];
            var today = new Date();

            pData[0] = $("#ddlFMonth").val();
            pData[1] = $("#ddlFYear").val();
            pData[2] = $("#ddlFieldForce").val();
            pData[3] = $("#ddlsearch").val();
            var mnth = $("#ddlFMonth option:selected").text();
            var yr = $("#ddlFYear option:selected").text();
            var pyr = $("#ddlFYear").val() - 1;
            var ppyr = $("#ddlFYear").val() - 2;
            if (pData[0] < 4) {
                document.getElementById("lbltarsaleYS").textContent = 'Secondary Sales YTD ( Apr ' + pyr + ' to ' + mnth + '  ' + yr + ' )'
                document.getElementById("lblpslys").textContent = 'LYS ( Apr ' + ppyr + ' to ' + mnth + '  ' + pyr + ' )'
            }
            else {
                document.getElementById("lbltarsaleYS").textContent = 'Secondary Sales YTD (  Apr ' + yr + ' to ' + mnth + '  ' + yr + ' )'
                document.getElementById("lblpslys").textContent = 'LYS ( Apr ' + pyr + ' to ' + mnth + '  ' + pyr + ' )'
            }

            var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
            var function_name = "getSecondary_Sale_YTD";
            $.ajax({
                type: "POST",
                url: "Admin_Primary_Sales_DashBoard.aspx/" + function_name,
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: OnErrorCall_
            });

            function OnSuccess_(response) {
                var aData = response.d;
                var arr = [];
                var sf_code, tar, sal, ach, psal, grow, pc;
                var sf_name = $("#ddlFieldForce").text();

                $.map(aData, function (item, index) {
                    sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC;



                });
                var myJsonString = JSON.stringify(arr);
                var jsonArray = JSON.parse(JSON.stringify(arr));
                document.getElementById("lbltarYS").textContent = tar
                document.getElementById("lblsalYS").textContent = sal
                document.getElementById("lblachYS").textContent = ach
                document.getElementById("lblLSYS").textContent = psal
                document.getElementById("lblgrYS").textContent = grow

                if (grow > 0) {
                    var Hide = document.getElementById("up");

                    Hide.style.display = "inline";
                }
                else {
                    var Hide = document.getElementById("down");

                    Hide.style.display = "inline";
                }
                document.getElementById("lblpcYS").textContent = pc

            }
            function OnErrorCall_(response) {
                alert("Error: No Data Found!");
            }
            //  e.preventDefault();


        }
    </script>
    <script type="text/javascript">
        function PrimaryYTD() {
            var pData = [];
            var today = new Date();

            pData[0] = $("#ddlFMonth").val();
            pData[1] = $("#ddlFYear").val();
            pData[2] = $("#ddlFieldForce").val();
            pData[3] = $("#ddlsearch").val();
            var mnth = $("#ddlFMonth option:selected").text();
            var yr = $("#ddlFYear option:selected").text();
            var pyr = $("#ddlFYear").val() - 1;
            var ppyr = $("#ddlFYear").val() - 2;
            if (pData[0] < 4) {
                document.getElementById("lbltarsaleY").textContent = 'Primary Sales YTD ( Apr ' + pyr + ' to ' + mnth + '  ' + yr + ' )'
                document.getElementById("lblsslys").textContent = 'LYS ( Apr ' + ppyr + ' to ' + mnth + '  ' + pyr + ' )'

            }
            else {
                document.getElementById("lbltarsaleY").textContent = 'Primary Sales YTD (  Apr ' + yr + ' to ' + mnth + '  ' + yr + ' )'
                document.getElementById("lblsslys").textContent = 'LYS ( Apr ' + pyr + ' to ' + mnth + '  ' + pyr + ' )'


            }

            var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
            var function_name = "getChartVal_YTD";
            $.ajax({
                type: "POST",
                url: "Admin_Primary_Sales_DashBoard.aspx/" + function_name,
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: OnErrorCall_
            });

            function OnSuccess_(response) {
                var aData = response.d;
                var arr = [];
                var sf_code, tar, sal, ach, psal, grow, pc;
                var sf_name = $("#ddlFieldForce").text();

                $.map(aData, function (item, index) {
                    sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC;



                });
                var myJsonString = JSON.stringify(arr);
                var jsonArray = JSON.parse(JSON.stringify(arr));
                document.getElementById("lbltarY").textContent = tar
                document.getElementById("lblsalY").textContent = sal
                document.getElementById("lblachY").textContent = ach
                document.getElementById("lblLSY").textContent = psal
                document.getElementById("lblgrY").textContent = grow
                if (grow > 0) {
                    var Hide = document.getElementById("yup");

                    Hide.style.display = "inline";
                }
                else {
                    var Hide = document.getElementById("ydown");

                    Hide.style.display = "inline";
                }
                document.getElementById("lblpcY").textContent = pc

            }
            function OnErrorCall_(response) {
                alert("Error: No Data Found!");
            }
            //  e.preventDefault();


        }
    </script>
    <script type="text/javascript">

        function Primary() {
            var pData = [];
            var today = new Date();

            pData[0] = $("#ddlFMonth").val();
            pData[1] = $("#ddlFYear").val();
            pData[2] = $("#ddlFieldForce").val();
            pData[3] = $("#ddlsearch").val();
            var mnth = $("#ddlFMonth option:selected").text();
            var yr = $("#ddlFYear option:selected").text();
            var py = $("#ddlFYear").val() - 1;
            // var pmnth = GetMonthName(data[pm].monthName);
            document.getElementById("lbltarsale").textContent = 'Primary Sales ( ' + mnth + '  ' + yr + ' )'
            document.getElementById("lbllys").textContent = 'LYS ( ' + mnth + '  ' + py + ' )'

            var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
            var function_name = "getChartVal";
            $.ajax({
                type: "POST",
                url: "Admin_Primary_Sales_DashBoard.aspx/" + function_name,
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: OnErrorCall_
            });

            function OnSuccess_(response) {
                var aData = response.d;
                var arr = [];
                var sf_code, tar, sal, ach, psal, grow, pc;
                var sf_name = $("#ddlFieldForce").text();

                $.map(aData, function (item, index) {
                    sf_code = item.Sf_Code; tar = item.Target; sal = item.Sale; ach = item.achie; psal = item.PSale; grow = item.Growth; pc = item.PC;



                });
                var myJsonString = JSON.stringify(arr);
                var jsonArray = JSON.parse(JSON.stringify(arr));
                document.getElementById("lbltar").textContent = tar
                document.getElementById("lblsal").textContent = sal
                document.getElementById("lblach").textContent = ach
                document.getElementById("lblLS").textContent = psal
                document.getElementById("lblgr").textContent = grow

                if (grow > 0) {
                    var Hide = document.getElementById("pup");

                    Hide.style.display = "inline";
                }
                else {
                    var Hide = document.getElementById("pdown");

                    Hide.style.display = "inline";
                }
                document.getElementById("lblpc").textContent = pc

            }
            function OnErrorCall_(response) {
                alert("Error: No Data Found!");
            }
            //  e.preventDefault();


        }
    </script>
    <script type="text/javascript">

        function Secondary() {

            var pData = [];
            var today = new Date();

            pData[0] = $("#ddlFMonth").val();
            pData[1] = $("#ddlFYear").val();
            pData[2] = $("#ddlFieldForce").val();
            pData[3] = $("#ddlsearch").val();
            var mnth = $("#ddlFMonth option:selected").text();
            var yr = $("#ddlFYear option:selected").text();
            var py = $("#ddlFYear").val() - 1;
            document.getElementById("SecSal").textContent = 'Secondary Sales ( ' + mnth + '  ' + yr + ' )'
            document.getElementById("lblslys").textContent = 'LYS ( ' + mnth + '  ' + py + ' )'

            var jsonData = JSON.stringify({ lst_Input_Mn_Yr: pData });
            var function_name = "getSecondary_Sale";
            $.ajax({
                type: "POST",
                url: "Admin_Primary_Sales_DashBoard.aspx/" + function_name,
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: OnErrorCall_
            });

            function OnSuccess_(response) {
                var aData = response.d;
                var arr = [];
                var sf_code, tar, sal, ach, psal, grow, pc;
                var sf_name = $("#ddlFieldForce").text();

                $.map(aData, function (item, index) {
                    sf_code = item.P_Sf_Code; tar = item.P_Target; sal = item.P_Sale; ach = item.P_achie; psal = item.P_PSale; grow = item.P_Growth; pc = item.P_PC;



                });
                var myJsonString = JSON.stringify(arr);
                var jsonArray = JSON.parse(JSON.stringify(arr));
                document.getElementById("lblsecT").textContent = tar
                document.getElementById("lblsecS").textContent = sal
                document.getElementById("lblsecA").textContent = ach
                document.getElementById("lblsecL").textContent = psal
                document.getElementById("lblsecG").textContent = grow
                if (grow > 0) {
                    var Hide = document.getElementById("sup");

                    Hide.style.display = "inline";
                }
                else {
                    var Hide = document.getElementById("sdown");

                    Hide.style.display = "inline";
                }
                document.getElementById("lblsecP").textContent = pc

            }
            function OnErrorCall_(response) {
                alert("Error: No Data Found!");
            }
            //  e.preventDefault();
        }

        //*
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0px;">
            <ucl:Menumas ID="menumas" runat="server" />
        </div>
        <asp:Panel ID="pnlchart" runat="server" Style="vertical-align: top">
            <div class="charts-area clearfix">
                <div class="container home-section-main-body">

                    <div class="row clearfix" style="align-items: center; text-align: center;">
                        <div class="col-lg-12">
                            <div class="row clearfix">
                                <div class="col-lg-10" style="text-align: center; padding: 20px;">
                                    <h2 class="text-center" style="color: #292a34; font-size: 24px; font-weight: 700;">
                                        <asp:Label Text="Primary / Secondary Sales" ForeColor="Black" ID="Label1" runat="server" />
                                    </h2>

                                </div>
                                <div class="col-lg-2">
                                    <div style="float: left; width: 30%; margin-top: 17px;">
                                        <asp:Label ID="lblMode" runat="server" Text="Mode"></asp:Label>
                                    </div>
                                    <div style="float: right; width: 70%">
                                        <div class="search-area clearfix" style="margin-top: -50px; margin-bottom: -20px;">
                                            <div class="single-option">
                                                <asp:DropDownList ID="ddlDBMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDBMode_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="SFE"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Sales"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Primary vs Secondary" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                          
                        </div>
                    </div>
                    <div id="chrt" runat="server">
                        <div class="row clearfix" style="align-items: center;">
                            <%--text-align: center;--%>
                            <div class="col-lg-12" style="margin-top: -60px; margin-bottom: -30px;">
                                <div class="search-area clearfix">
                                    <div class="row justify-content-center">
                                        <div class="col-lg-3">

                                            <div class="designation-area clearfix">
                                                <div class="single-des clearfix">
                                                    <asp:Label ID="lblSF" runat="server" Text="FieldForce Name"></asp:Label>
                                                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="designation-area clearfix">
                                                <div class="single-des clearfix">

                                                    <div style="float: left; width: 50%">
                                                        <asp:Label Text="Mnth/Yr" ID="lblChrtMnth" runat="server" />
                                                        <asp:DropDownList ID="ddlFMonth" runat="server">
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
                                                    <div style="float: right; width: 50%; padding-top: 28px;">
                                                        <asp:DropDownList runat="server" CssClass="valign" ID="ddlFYear">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- <div class="col-lg-3">
                                            <div class="designation-area clearfix">
                                                <div class="single-des clearfix">

                                                    <div style="float: left; width: 50%">
                                                        <asp:Label Text="To Mnth/Yr" ID="lblyear" runat="server" />
                                                        <asp:DropDownList ID="ddlTMonth" runat="server">
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
                                                    <div style="float: right; width: 50%; padding-top: 28px;">
                                                        <asp:DropDownList runat="server" CssClass="valign" ID="ddlTYear">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="col-lg-3">
                                            <div class="designation-area clearfix">
                                                <div class="single-des clearfix">

                                                    <div style="float: left; width: 50%">
                                                        <asp:Label Text="Mode" ID="Label2" runat="server" />
                                                        <asp:DropDownList ID="ddlmode" runat="server">
                                                            <asp:ListItem Value="0" Text="Product"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="HQ"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="State"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div style="float: right; width: 50%; padding-top: 28px;">
                                                        <asp:DropDownList ID="ddlsearch" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                                            data-live-search="true" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-1" style="padding-top: 28px;">
                                            <asp:Button Text="View" CssClass="savebutton" Width="60px"
                                                ID="btnViewChart" runat="server" />
                                        </div>
                                    </div>
                                    <%--  <div class="row clearfix">
                                        <div class="col-lg-5">
                                            <div class="row clearfix">
                                                <div class="col-lg-5">
                                                    <asp:Label ID="lblSF" runat="server" Text="FieldForce Name"></asp:Label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="single-option">
                                                        <asp:DropDownList ID="ddlFieldForce" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="row clearfix">
                                                <div class="col-lg-3">
                                                    <asp:Label Text="From Mnth/Yr" ID="lblChrtMnth" runat="server" />
                                                </div>
                                                <div class="col-lg-2 p-0">
                                                   
                                                        <asp:DropDownList ID="ddlFMonth" runat="server">
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
                                                       <div class="col-lg-2 p-0">
                                                         <asp:DropDownList runat="server" CssClass="valign" ID="ddlFYear">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="row clearfix">
                                                <div class="col-lg-3">
                                                    <asp:Label Text="To Mnth/Yr" ID="lblyear" runat="server" />
                                                </div>
                                                <div class="col-lg-2 p-0">
                                                    
                                                         <asp:DropDownList ID="ddlTMonth" runat="server">
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
                                                        <div class="col-lg-2 p-0">
                                                        <asp:DropDownList runat="server" CssClass="valign" ID="ddlTYear">
                                                        </asp:DropDownList>
                                                            </div>
                                                  
                                              
                                            </div>
                                        </div>
                                    
                                        <div class="col-lg-1">
                                            <asp:Button Text="View" CssClass="savebutton" Width="60px"
                                                ID="btnViewChart" runat="server" />
                                        </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="chrt1" runat="server">
                        <div class="col-lg-12" id="Primary">
                            <div class="row justify-content-center clearfix">
                                <div class="col-lg-6">
                                    <div class="single-chart single-top-chat-min-height">
                                        <div class="row" align="center">
                                            <div class="col-lg-4">
                                                <center>
                                                    <table class="single-block-area" align="center">
                                                        <thead>
                                                            <tr>
                                                                <th colspan="2" style="background-color: #4b64e3"><span id="lbltarsale" runat="server"></span>
                                                                </th>
                                                            </tr>
                                                        </thead>

                                                        <tbody>
                                                            <tr>
                                                                <td>Target
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbltar" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Sales
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblsal" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Achievement (%)
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblach" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>PC PM
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblpc" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbllys" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblLS" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>Growth
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblgr" runat="server"></asp:Label>
                                                                    <img id="pup" src="Images/Uparrow.png" width="20px" height="15px" style="display: none">
                                                                    <img id="pdown" src="Images/Downarrow.png" width="20px" height="15px" style="display: none">
                                                                </td>
                                                            </tr>

                                                        </tbody>
                                                    </table>
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="single-chart single-top-chat-min-height">
                                        <div class="row" align="center">
                                            <div class="col-lg-4">
                                                <center>
                                                    <table class="single-block-area" align="center">
                                                        <thead>
                                                            <tr>
                                                                <th colspan="2" style="background-color: #4b64e3"><span id="lbltarsaleY" runat="server"></span>
                                                                </th>
                                                            </tr>
                                                        </thead>

                                                        <tbody>
                                                            <tr>
                                                                <td>Target
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbltarY" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Sales
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblsalY" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Achievement (%)
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblachY" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>PC PM
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblpcY" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblsslys" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblLSY" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>Growth
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblgrY" runat="server"></asp:Label>
                                                                    <img id="yup" src="Images/Uparrow.png" width="20px" height="15px" style="display: none">
                                                                    <img id="ydown" src="Images/Downarrow.png" width="20px" height="15px" style="display: none">
                                                                </td>
                                                            </tr>

                                                        </tbody>
                                                    </table>
                                                </center>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12" id="Target">
                        <div class="row justify-content-center clearfix">
                            <div class="col-lg-6">
                                <div class="single-chart single-top-chat-min-height">
                                    <div class="row" align="center">
                                        <div class="col-lg-4">
                                            <center>
                                                <table class="single-block-area" align="center">
                                                    <thead>
                                                        <tr>
                                                            <th colspan="2" style="background-color: #4b64e3"><span id="SecSal" runat="server"></span>
                                                            </th>
                                                        </tr>
                                                    </thead>

                                                    <tbody>
                                                        <tr>
                                                            <td>Target
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsecT" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Sales
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsecS" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Achievement (%)
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsecA" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>PC PM
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsecP" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblslys" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsecL" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td>Growth
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsecG" runat="server"></asp:Label>
                                                                <img id="sup" src="Images/Uparrow.png" width="20px" height="15px" style="display: none">
                                                                <img id="sdown" src="Images/Downarrow.png" width="20px" height="15px" style="display: none">
                                                            </td>
                                                        </tr>

                                                    </tbody>
                                                </table>
                                            </center>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-6">
                                <div class="single-chart single-top-chat-min-height">
                                    <div class="row" align="center">
                                        <div class="col-lg-4">
                                            <center>
                                                <table class="single-block-area" align="center">
                                                    <thead>
                                                        <tr>
                                                            <th colspan="2" style="background-color: #4b64e3"><span id="lbltarsaleYS" runat="server"></span>
                                                            </th>
                                                        </tr>
                                                    </thead>

                                                    <tbody>
                                                        <tr>
                                                            <td>Target
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbltarYS" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Sales
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsalYS" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Achievement (%)
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblachYS" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>PC PM
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblpcYS" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblpslys" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblLSYS" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td>Growth
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblgrYS" runat="server"></asp:Label>
                                                                <img id="up" src="Images/Uparrow.png" width="20px" height="15px" style="display: none">
                                                                <img id="down" src="Images/Downarrow.png" width="20px" height="15px" style="display: none">
                                                            </td>
                                                        </tr>

                                                    </tbody>
                                                </table>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </asp:Panel>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>


    </form>
</body>
</html>
