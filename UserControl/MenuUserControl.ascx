<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuUserControl.ascx.cs"
    Inherits="UserControl_MenuUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<link href="/css/Master.css" rel='stylesheet' type='text/css' />--%>
<!-- IE6-8 support of HTML5 elements -->
<!-- [if lt IE 9]>
	<script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif] -->
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<title>Welcome Corporate – HQ</title>
<meta name="description" content="">
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="shortcut icon" type="image/png" href="../../../assets/images/logo.png" />
<link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
    rel="stylesheet">
<link rel="stylesheet" href="../../../assets/css/font-awesome.min.css">
<link rel="stylesheet" href="../../../assets/css/nice-select.css">
<link rel="stylesheet" href="../../../assets/css/bootstrap.min.css">
<link rel="stylesheet" href="../../../assets/css/style.css">
<link rel="stylesheet" href="../../../assets/css/responsive.css">

<link rel="stylesheet" href="../../../assets/css/Calender_CheckBox.css">

<!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->

<style type="text/css">
    .menu {
        margin-top: 0px;
    }

    .BUTTON : hover {
        background-color: #A6A6D2;
    }

    .style3 {
        width: 99px;
    }

    .under {
        margin-top: 2px;
        text-decoration: underline;
    }

    .modal {
        position: fixed;
        top: 0;
        left: 0;
        background-color: gray;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }

    .loading {
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

    .size {
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 50px;
    }

    body {
        /*font-family: Arial;
        font-size: 10pt;*/
    }

    .spc {
        padding-top: 5px;
        padding-bottom: 5px;
        padding-left: 5px;
    }

    .modalBackground {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }

    .modalPopup {
        background-color: #FFFFFF;
        width: 300px;
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding: 0;
    }

        .modalPopup .header {
            background-color: #2FBDF1;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
            border-top-left-radius: 6px;
            border-top-right-radius: 6px;
        }

        .modalPopup .body {
            padding: 10px;
            min-height: 50px;
            text-align: center;
            font-weight: bold;
        }

        .modalPopup .footer {
            padding: 6px;
        }

        .modalPopup .yes, .modalPopup .no {
            height: 23px;
            color: White;
            line-height: 23px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            border-radius: 4px;
        }

        .modalPopup .yes {
            background-color: #2FBDF1;
            border: 1px solid #0DA9D0;
        }

        .modalPopup .no {
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }

    .WebContainer {
        width: 100%;
        height: auto;
    }

    .dropdown-menu {
        float: right;
    }

    .navbar-brand {
        max-width: 125px !important;
    }
</style>
<script type="text/javascript">

    //    $(document).ready(function () {


    //        window.$zopim || (function (d, s) {
    //            var z = $zopim = function (c) {
    //                z._.push(c)
    //            }, $ = z.s =
    //    d.createElement(s), e = d.getElementsByTagName(s)[0]; z.set = function (o) {
    //        z.set.
    //    _.push(o)
    //    }; z._ = []; z.set._ = []; $.async = !0; $.setAttribute('charset', 'utf-8');
    //            $.src = 'https://v2.zopim.com/?5DURYZFDFeE3izx6HWO5i5IteKcQeGaU'; z.t = +new Date; $.
    //type = 'text/javascript'; e.parentNode.insertBefore($, e)
    //        })(document, 'script');

    //    });
    function initFreshChat() {
        window.fcWidget.init({
            token: "65d3efbd-8e34-4db8-bd0b-dbcd2d844694",
            host: "https://wchat.freshchat.com"
        });
    }
    function initialize(i, t) { var e; i.getElementById(t) ? initFreshChat() : ((e = i.createElement("script")).id = t, e.async = !0, e.src = "https://wchat.freshchat.com/js/widget.js", e.onload = initFreshChat, i.head.appendChild(e)) } function initiateCall() { initialize(document, "freshchat-js-sdk") } window.addEventListener ? window.addEventListener("load", initiateCall, !1) : window.attachEvent("load", initiateCall, !1);
</script>

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
</script>
<%--<asp:LinkButton ID="lnkFake" runat="server" />--%>
<%--<asp:ModalPopupExtender ID="mpeTimeout" BehaviorID="mpeTimeout" runat="server" PopupControlID="pnlPopup"
    TargetControlID="lnkFake" OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground"
    OnOkScript="ResetSession()" OnCancelScript="ExSession()">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
    <div class="header">
        Session Expiring!
    </div>
    <div class="body">
        Your Session will expire in&nbsp;<span id="seconds"></span>&nbsp;seconds.<br />
        Do you want to Continue? <span id="secondsIdle" style="visibility: hidden"></span>
    </div>
    <div class="footer" align="right">
        <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="yes" />
        <asp:Button ID="btnNo" runat="server" Text="No" CssClass="no" />
    </div>
</asp:Panel>
<script type="text/javascript">
    function SessionExpireAlert(timeout) {
        var seconds = timeout / 1000;
        document.getElementsByName("secondsIdle").innerHTML = seconds;
        document.getElementsByName("seconds").innerHTML = seconds;
        setInterval(function () {
            seconds--;
            document.getElementById("seconds").innerHTML = seconds;
            document.getElementById("secondsIdle").innerHTML = seconds;
        }, 1000);
        setTimeout(function () {
            //Show Popup before 20 seconds of timeout.
            $find("mpeTimeout").show();
        }, timeout - 120 * 1000);
        setTimeout(function () {
            window.location.replace("http://torssfa.info/");
        }, timeout);
    };
    function ResetSession() {

        //Redirect to refresh Session.
        window.location = window.location.href;
    }
    function ExSession() {

        window.location.replace("http://torssfa.info/");
    }
</script>--%>

<div class="Container">
    <div id="wrapper">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
        </asp:ToolkitScriptManager>

        <%-- <asp:Panel ID="pnldiv" runat="server" CssClass="spc">
            <table width="100%" border="0">
                <tr>
                    <td>
                        <asp:Label ID="lblSessionTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 35%">
                        <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize; font-size: 14px; text-align: left; margin-top: 0px"
                            ForeColor="#8A2EE6" Font-Bold="True"
                            Font-Names="Verdana">
                        </asp:Label>
                    </td>
                    <td align="center" style="width: 30%">
                        <asp:Label ID="LblDiv" runat="server" Text="DivName" Style="text-transform: capitalize;
                        font-size: 14px; margin-top: 0px" ForeColor="#8A2EE6" Font-Bold="True" Font-Names="Verdana">
                    </asp:Label>
                    </td>
                    <td align="right">
                     
                    </td>
                </tr>
            </table>
        </asp:Panel>--%>

        <asp:Panel ID="pnlControl" runat="server">
            <header class="header-area clearfix">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!---- Menu area start ---->
                            <nav class="navbar navbar-expand-md navbar-light p-0">
                                <a class="navbar-brand col-2" href="#">
                                    <img id="img" runat="server" alt="" />
                                </a>
                                <%--<div style="width: 27%;">
                                <asp:Label ID="LblUser" runat="server" Text="">  </asp:Label>
                            </div>--%>


                                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
                                    <span class="navbar-toggler-icon"></span>
                                </button>
                                <div class="collapse navbar-collapse justify-content-end main-menu" id="collapsibleNavbar">


                                    <ul id="menu" class="navbar-nav">
                                        <li class="nav-item"><a href="../../../../Default.aspx" class="nav-link active" onclick="ShowProgress();">Home</a></li>
                                        <%--class="first"--%>

                                        <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" href="#" id="A1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Master </a>
                                            <ul id="ulMas" runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">

                                                <li id="ulliMas" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">SubDivision </a>
                                                    <ul id="ululMas" class="dropdown-menu" runat="server">
                                                        <li id="lim1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/SubDivisionList.aspx"
                                                            onclick="ShowProgress();">Entry</a></li>
                                                        <li id="lim2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Subdiv_Productwise.aspx"
                                                            onclick="ShowProgress();">View - Productwise</a></li>
                                                        <li id="lim3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Subdiv_Salesforcewise.aspx"
                                                            onclick="ShowProgress();">View - Field Forcewise</a></li>
                                                        <li id="limact" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityMasterList.aspx"
                                                            onclick="ShowProgress();">Activity - Master</a></li>
                                                        <li id="li3000" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Processing_Activity.aspx"
                                                            onclick="ShowProgress();">Processing Activity</a></li>
                                                        <li id="li3111" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReport.aspx"
                                                            onclick="ShowProgress();">Activity Report</a></li>
                                                           <li id="li30act" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Activity_Upload.aspx"
                                                            onclick="ShowProgress();">Activity Upload</a></li>
                                                    </ul>
                                                </li>


                                                <li id="ulliProd" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Product </a>
                                                    <ul id="ululProd" class="dropdown-menu" runat="server">
                                                        <li id="lim4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ProductCategoryList.aspx"
                                                            onclick="ShowProgress();">Category</a></li>
                                                        <li id="lim5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ProductGroupList.aspx"
                                                            onclick="ShowProgress();">Group</a></li>
                                                        <li id="lim6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ProductBrandList.aspx"
                                                            onclick="ShowProgress();">Brand</a></li>
                                                        <li id="lim7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ProductList.aspx" onclick="ShowProgress();">Product Detail</a></li>
                                                        <li id="lim8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ProductRate.aspx" onclick="ShowProgress();">Statewise - Rate Fixation</a></li>
                                                        <!-- <li id="lim9" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ProductRate.aspx"
                                                            onclick="ShowProgress();">Statewise - Product Rate - View</a></li>
                                                        <li id="li30s" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ProductRate_Mnthwise.aspx"
                                                            onclick="ShowProgress();">Month/State/Productwise - Rate View</a></li>
                                                        <li id="liSchem" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/ProductSchemeEntry.aspx"
                                                            onclick="ShowProgress();">Scheme Master</a></li>-->


                                                        <li id="liScheme" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/Product_Scheme_Entry.aspx"
                                                            onclick="ShowProgress();">Scheme</a></li>
                                                    </ul>
                                                </li>
                                                <li id="Li15" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Chemist </a>
                                                    <ul id="ul4" class="dropdown-menu" runat="server">



                                                        <li id="li50" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Chemist_Campaign/ChemistCampaignList.aspx"
                                                            onclick="ShowProgress();">Campaign</a></li>
                                                    </ul>
                                                </li>
                                                <%--   <li id="lim600"><a href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_New.aspx"
                        onclick="ShowProgress();">Doctor - Service CRM</a></li>--%>
                                                <li id="lim10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/SalesForceList.aspx"
                                                    onclick="ShowProgress();">Field Force</a></li>

                                                <li id="ulliDoc" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Doctor</a>
                                                    <ul id="ululDoc" class="dropdown-menu" runat="server">
                                                        <li id="lim11" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/DoctorCategoryList.aspx"
                                                            onclick="ShowProgress();">Category</a></li>
                                                        <li id="lim12" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/DoctorSpecialityList.aspx"
                                                            onclick="ShowProgress();">Speciality</a></li>
                                                        <li id="lim13" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/DoctorClassList.aspx"
                                                            onclick="ShowProgress();">Class</a></li>
                                                        <li id="lim14" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/DoctorCampaignList.aspx"
                                                            onclick="ShowProgress();">Campaign</a></li>
                                                        <li id="lim15" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/DoctorQualificationList.aspx"
                                                            onclick="ShowProgress();">Qualification</a></li>
                                                        <li id="lim16" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ChemistCategoryList.aspx"
                                                            onclick="ShowProgress();">Chemists Category</a></li>
                                                        <li id="liChClassList" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ChemistClassList.aspx"
                                                            onclick="ShowProgress();">Chemists Class</a></li>
                                                        <li id="liBusiness" runat="server"><a href="../../../../MasterFiles/BusinessRange.aspx"
                                onclick="ShowProgress();">Business Range</a></li>
                                                    </ul>
                                                </li>

                                                <li id="lim17" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ProductReminderList.aspx"
                                                    onclick="ShowProgress();">Input</a></li>

                                                <li id="ulliFF" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Field Force Entries </a>
                                                    <ul id="ululFF" class="dropdown-menu" runat="server">
                                                        <%--  <li><a href="../../../../MasterFiles/MR/Territory/Territory.aspx"
                                onclick="ShowProgress();">Territory</a></li>--%>
                                                        <li id="liTown" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MR/Territory/Town_City_Master.aspx"
                                                            onclick="ShowProgress();">
                                                            <asp:Label ID="Label2" Text="Town/City Master" runat="server"></asp:Label></a></li>
                                                        <li id="lim18" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Territory/Territory.aspx"
                                                            onclick="ShowProgress();">
                                                            <asp:Label ID="lblTerritory" Text="Territory" runat="server"></asp:Label></a></li>
                                                        <li id="lim19" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorView.aspx"
                                                            onclick="ShowProgress();">
                                                            <asp:Label ID="lblTerr" Text="Territory" runat="server"></asp:Label></a></li>
                                                        <li id="lim20" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorList.aspx"
                                                            onclick="ShowProgress();">Listed Doctor</a></li>
                                                        <li id="lim21" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Chemist/ChemistList.aspx"
                                                            onclick="ShowProgress();">Chemist</a></li>
                                                        <li id="lim22" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Hospital/HospitalList.aspx"
                                                            onclick="ShowProgress();">Hospital</a></li>
                                                        <li id="lim23" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/UnListedDoctor/UnLstDoctorList.aspx"
                                                            onclick="ShowProgress();">Unlisted Doctor</a></li>
                                                        <li id="literrmapp" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MR/Territory/Territory_Weekly_Mapping.aspx"
                                                            onclick="ShowProgress();">Territorywise-Week Mapping</a></li>

                                                        <!-- <li id="listarea" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/AreaWise_Lat_Long_Updation.aspx" onclick="ShowProgress();">Area Cluster-lat long</a></li>-->
                                                    </ul>
                                                </li>

                                                <li id="ulliHol" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Statewise - Holiday Fixation </a>
                                                    <ul id="ululHol" class="dropdown-menu" runat="server">
                                                        <li id="liHolEn" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/HolidayList.aspx"
                                                            onclick="ShowProgress();">Entry</a></li>
                                                        <li id="liHolVw" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Calendar_Consolidated.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                    </ul>
                                                </li>

                                                <li id="ulliStock" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Stockist Details </a>
                                                    <ul id="ululStock" class="dropdown-menu" runat="server">
                                                        <li id="lim25" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/StockistList.aspx"
                                                            onclick="ShowProgress();">Add/Edit/DeActivate</a></li>
                                                        <li id="lim26" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Stockist_Sale.aspx"
                                                            onclick="ShowProgress();">FieldForce Stockist Entry</a></li>
                                                        <li id="lim27" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Stockist_View.aspx"
                                                            onclick="ShowProgress();">Stockist View</a></li>
                                                        <li id="limStock" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Sec_Sales_Stockiest.aspx"
                                                            onclick="ShowProgress();">Entry Status I</a></li>
                                                        <li id="lim28" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/SecSale_Stockist_Entry_Status.aspx"
                                                            onclick="ShowProgress();">Entry Status II</a></li>
                                                        <%--   <li><a href="../../../../MasterFiles/Stockist_HQ_Map.aspx" onclick="ShowProgress();">
                                HQ Map</a></li>--%>
                                                        <li id="lim29" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/PoolName_List.aspx"
                                                            onclick="ShowProgress();">Stockist - HQ Creation</a></li>
                                                        <li id="lim30" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Stock_HQ_Updation.aspx"
                                                            onclick="ShowProgress();">Stockist - HQ Updation</a></li>
                                                        <li id="lisprstk" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Super_Stockist_Map.aspx"
                                                            onclick="ShowProgress();">Super Stockist - Create & Map</a></li>

                                                        <%--<li id="limStE" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Statewise_Stockist_Report.aspx"
                                                        onclick="ShowProgress();">Statewise - Entry Status </a></li>--%>

                                                        <%--    <li id="liSSRpt101" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SecSale_Report_Free.aspx"
                                                            onclick="ShowProgress();">At a Glance</a></li>--%>
                                                    </ul>
                                                </li>

                                                <li id="ulliExpense" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Expense </a>
                                                    <ul id="ululExpense" class="dropdown-menu" runat="server">
                                                        <li id="lim31" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/password.aspx"
                                                            onclick="ShowProgress();">SFC Updation</a></li>
                                                        <li id="lim32" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Distance_fixation_view.aspx"
                                                            onclick="ShowProgress();">SFC View</a></li>
                                                        <li id="li26" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Territory_SFC_Download.aspx"
                                                            onclick="ShowProgress();">SFC Download(MR)</a></li>
                                                        <li id="li25" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Territory_SFC_Download_mgr.aspx"
                                                            onclick="ShowProgress();">SFC Download(MGR)</a></li>
                                                        <li id="lim33" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/password2.aspx"
                                                            onclick="ShowProgress();">Allowance Fixation</a></li>
                                                        <li id="liAllw" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AllowanceFixation_View.aspx"
                                                            onclick="ShowProgress();">Allowance Fixation View</a></li>
                                                        <li id="lim34" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/WrkTypeWise_Allowance.aspx"
                                                            onclick="ShowProgress();">Work Type Wise - Allowance Fix</a></li>
                                                        <li id="lim35" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/FVExpense_Parameter.aspx"
                                                            onclick="ShowProgress();">Fixed/Variable Expense Parameter</a></li>
                                                        <li id="exp_setup" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Expense_Setup.aspx"
                                                            onclick="ShowProgress();">Expense Setup</a></li>
                                                        <%--
                           <li id="ex_setup"><a href="../../../../MasterFiles/Options/Expense_Setup.aspx" onclick="ShowProgress();">
                                Expense Setup</a></li>--%>
                                                    </ul>
                                                </li>

                                                <li id="ulliMGE" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Manager Expense </a>
                                                    <ul id="ululMGE" class="dropdown-menu" runat="server">
                                                        <li id="limMGR31" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Mgr_Auto_Allowance_Fixation.aspx"
                                                            onclick="ShowProgress();">Allowance Fixation (Automatic)</a></li>
                                                        <li id="limMGR32" runat="server" visible="true"><a class="dropdown-item" href="../../../../MasterFiles/MGR/Mgr_SFC_Updation.aspx"
                                                            onclick="ShowProgress();">Customized Allowance type</a></li>

                                                        <%-- <li id="liallSFC" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Distance_fixation_view_Allsfc.aspx" onclick="ShowProgress();">SFC View</a></li>
                                                    <li id="liMGRsfcvw" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/WrkTypeWise_Allowance.aspx"
                                                        onclick="ShowProgress();">Work Type Wise - Allowance Fix</a></li>--%>
                                                    </ul>
                                                </li>

                                                <%-- <li id="ulliEmp" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Employee Personal Details</a>
                                                <ul id="ululEmp" class="dropdown-menu" runat="server">
                                                    <li id="liPersonalEntry" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Personal_Details.aspx"
                                                        onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liPersonalSts" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Personal_Details_Status.aspx"
                                                        onclick="ShowProgress();">Status</a></li>
                                                </ul>
                                            </li>--%>

                                                <li id="ulliCRM" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Doctor - Service CRM </a>
                                                    <ul id="ululCRM" class="dropdown-menu" runat="server">
                                                        <li id="limCRM101" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_Admin_Approve.aspx"
                                                            onclick="ShowProgress();">Approval</a></li>
                                                        <li id="limCRM102" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_Status.aspx"
                                                            onclick="ShowProgress();">Status</a></li>
                                                        <li id="limCRM103" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_Analysis.aspx"
                                                            onclick="ShowProgress();">CRM - Analysis</a></li>
                                                        <%--       <li id="limCRMEntry" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_Staus_MGR.aspx"
                            onclick="ShowProgress();">CRM - Approval - Status</a></li>   --%>
                                                        <li id="limCRMAdmin" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_New.aspx"
                                                            onclick="ShowProgress();">CRM - Service Entry</a></li>
                                                    </ul>
                                                </li>

                                                <li id="li15Comp" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Competitor_Master.aspx"
                                                    onclick="ShowProgress();">Competitor Info</a></li>



                                            </ul>
                                        </li>
                                        <li id="Act" class="nav-item dropdown" runat="server"><a class="nav-link dropdown-toggle" href="#" id="A2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Activities </a>
                                            <ul id="ulAct" runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                                <%-- <li><a href="#">Std. Daywise Plan &raquo;</a>
                    <ul>
                        <li><a href="../../../../MasterFiles/MR_StdDayPlan.aspx" onclick="ShowProgress();">
                            Entry</a></li>
                        <li><a href="#">View</a></li>
                    </ul>
                </li>--%>
                                                <li id="ulliApp" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Approvals </a>
                                                    <ul id="ululApp" class="dropdown-menu" runat="server">
                                                        <li id="liA1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/Listeddr_admin_Approve.aspx"
                                                            onclick="ShowProgress();">Listed Dr Addition</a></li>
                                                        <li id="liA2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/Listeddr_adm_deAct_Approve.aspx"
                                                            onclick="ShowProgress();">Listed Dr Deactivation</a></li>
                                                        <li id="liNewUni" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Common_Doctors/Unique_dr_app_admin.aspx"
                                                            onclick="ShowProgress();">New Unique Dr Approval</a></li>
                                                        <li id="liA3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/TP_Calendar_Approve.aspx"
                                                            onclick="ShowProgress();">TP</a></li>
                                                        <li id="liA4" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/DCR_Admin_Approval.aspx"
                                                            onclick="ShowProgress();">DCR</a></li>
                                                        <li id="liA5" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Leave_Admin_Approval.aspx"
                                                            onclick="ShowProgress();">Leave</a></li>
                                                        <%--<li runat="server"><a href="../../../../MasterFiles/Reports/rptAutoexpense_Approve.aspx" onclick="ShowProgress();">
                                Expense</a></li>--%>
                                                    </ul>
                                                </li>

                                                <li id="ulliExApp" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Expense </a>
                                                    <ul id="ululExApp" class="dropdown-menu" runat="server">
                                                        <li id="Licom" visible="false" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/rptAutoexpense_Approve.aspx"
                                                            onclick="ShowProgress();">Approval(Active)</a></li>
                                                          <li id="LiRe" visible="false" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/rptAutoexpense_Approve_Reprocess.aspx"
                                                            onclick="ShowProgress();">Approval(Active) Reprocess</a></li>
                                                        <li id="LiExpCib" visible="false" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/rptAutoexpense_Approve_Cibeles.aspx"
                                                            onclick="ShowProgress();">Approval(Active)</a></li>
                                                        <li id="Liicar" visible="false" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/rptAutoexpense_Approve_Icarus.aspx"
                                                            onclick="ShowProgress();">Approval(Active)</a></li>
                                                        <li id="LResig" visible="false" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/rptAutoexpense_Approve_Resigned.aspx"
                                                            onclick="ShowProgress();">Approval(Vacant/Resigned)</a></li>
                                                        <li id="Licibles" visible="false" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/rptAutoexpense_View_Resigned_Cibles.aspx"
                                                            onclick="ShowProgress();">Approval(Vacant/Resigned)</a></li>
                                                        <li id="Liexpcov" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Coverage_Analysis_2.aspx"
                                                            onclick="ShowProgress();">Analysis</a></li>
                                                        <li id="LiConsView" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/frmExp_Consolidated_View.aspx"
                                                            onclick="ShowProgress();">Consolidated View</a></li>
                                                        <li id="LiExpdwn" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Exp_cons.aspx"
                                                            onclick="ShowProgress();">Download (All India)</a></li>

                                                    </ul>
                                                </li>
                                                <%--<li><a href="#">Expense &raquo;</a>
                        <ul>
                             <li id="liA5"><a href="../../../../MasterFiles/Reports/rptAutoexpense_Approve.aspx" onclick="ShowProgress();">
                                Expense</a></li>
                            <li><a href="../../../../MasterFiles/Reports/rptExp_Consolidated_View.aspx"
                                onclick="ShowProgress();">Consolidated View</a></li>
                           
                        </ul>
                    </li>--%>
                                                <li id="LiCPBE13" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Chemist-Productwise Business</a>
                                                    <ul id="ulCPBE3" class="dropdown-menu" runat="server">
                                                        <li id="liCPBE14" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/ChemistProductwiseBusinessEntry.aspx"
                                                            onclick="ShowProgress();">Entry</a></li>
                                                        <li id="liCPBE15" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/ChemistProductwiseBusinessView.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                    </ul>
                                                </li>

                                                <li id="ulliDocBus" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Doctor Business Productwise </a>
                                                    <ul id="ululDocBus" class="dropdown-menu" runat="server">
                                                        <%--  <li><a href="../../../../MasterFiles/ActivityReports/DoctorBusinessEntry.aspx" onclick="ShowProgress();">
                                Entry</a></li>--%>
                                                       <%-- <li id="liA7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorProductBusinessEntry.aspx"
                                                            onclick="ShowProgress();">Entry</a></li>--%>

                                                        <%--   <li><a href="../../../../MasterFiles/ActivityReports/DoctorBusinessView.aspx" onclick="ShowProgress();">
                                View</a></li>--%>
                                                        <li id="liA7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Doctor_Business_Entry.aspx" onclick="ShowProgress();">Entry</a></li>
                                                        <li id="liA8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorProductBusinessView.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                        <li id="li3110" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Prescriber_report.aspx"
                                                            onclick="ShowProgress();">Prescriber Detail</a></li>
                                                        <%-- <li><a href="../../../../MasterFiles/ActivityReports/DoctorBusinessStatus.aspx"
                                onclick="ShowProgress();">Status</a></li>--%>
                                                    </ul>
                                                </li>

                                                <%--  <li><a href="#">Sample Despatch &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/ActivityReports/SampleDespatchHQ.aspx" onclick="ShowProgress();">
                                From HQ</a></li>
                            <li><a href="../../../../MasterFiles/ActivityReports/SampleDespatchHQView.aspx" onclick="ShowProgress();">
                                View</a> </li>
                            <li><a href="../../../../MasterFiles/ActivityReports/SampleDespatchHQStatus.aspx"
                                onclick="ShowProgress();">Status</a> </li>
                        </ul>
                    </li>--%>
                                                <li id="ulliSamDes" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Sample Despatch </a>
                                                    <ul id="ululSamDes" class="dropdown-menu" runat="server">
                                                        <li id="liA9" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/Samm.aspx"
                                                            onclick="ShowProgress();">Entry</a></li>
                                                        <li id="liA10" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/edit.aspx"
                                                            onclick="ShowProgress();">Edit</a></li>
                                                        <li id="liA11" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/deletesam.aspx"
                                                            onclick="ShowProgress();">Delete</a> </li>
                                                        <li id="liA12" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/sampleproduct.aspx"
                                                            onclick="ShowProgress();">View</a> </li>
                                                        <%-- <li id="liA13" runat="server"><a href="../../../../MIS Reports/samplestatus.aspx" onclick="ShowProgress();">Status</a>
                            </li>--%>
                                                        <li id="liA13" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/samplestatus_new.aspx"
                                                            onclick="ShowProgress();">Status</a> </li>
                                                        <li id="li30trans" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Sample_Stock_Transfer.aspx"
                                                        onclick="ShowProgress();">Transfer</a> </li>
                                                        <li id="lismf" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Sample_Status_CalendarYearWise.aspx"
                                                            onclick="ShowProgress();">status(Financial YearWise)</a> </li>
                                                        <li id="lidssd" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Sample_Status_Upload_Status.aspx"
                                                            onclick="ShowProgress();">MonthWise</a> </li>
                                                    </ul>
                                                </li>
                                                <li id="ulliInDes" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Input Despatch </a>
                                                    <ul id="ululInDes" class="dropdown-menu" runat="server">
                                                        <%--<li><a href="../../../../MasterFiles/ActivityReports/InputDespatchHQ.aspx" onclick="ShowProgress();">
                                From HQ</a></li>
                            <li><a href="../../../../MasterFiles/ActivityReports/InputDespatchHQView.aspx" onclick="ShowProgress();">
                                View</a> </li>
                            <li><a href="../../../../MasterFiles/ActivityReports/InputDespatchHQStatus.aspx"
                                onclick="ShowProgress();">Status</a> </li>--%>
                                                        <li id="liA14" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/inpu.aspx"
                                                            onclick="ShowProgress();">Entry </a></li>
                                                        <li id="liA15" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/inputedit.aspx"
                                                            onclick="ShowProgress();">Edit </a></li>
                                                        <li id="liA16" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/inputdelete.aspx"
                                                            onclick="ShowProgress();">Delete </a></li>
                                                        <li id="liA17" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/inputproduct.aspx"
                                                            onclick="ShowProgress();">View</a> </li>
                                                        <%--   <li id="liA18" runat="server"><a href="../../../../MIS Reports/inputstatus.aspx" onclick="ShowProgress();">Status</a>
                            </li>--%>
                                                        <li id="liA18" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/inputstatus_new.aspx"
                                                            onclick="ShowProgress();">Status</a> </li>
                                                        <li id="li31trans" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Input_Stock_Transfer.aspx"
                                                        onclick="ShowProgress();">Transfer</a> </li>
                                                        <li id="liincal" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/input_Status_CalendarYearWise.aspx"
                                                            onclick="ShowProgress();">status(Financial YearWise)</a> </li>
                                                    </ul>
                                                </li>



                                                <li id="Li77" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Input Issued</a>
                                                    <ul id="ul66" class="dropdown-menu" runat="server">
                                                        <li id="li200" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/gift_issued.aspx"
                                                            onclick="ShowProgress();">
                                                            <asp:Label ID="Label3" runat="server" Text="Field Force Wise"></asp:Label>
                                                        </a></li>
                                                        <li id="li300" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/HQ_Wise_Inpute_Det.aspx"
                                                            onclick="ShowProgress();">Utilization</a></li>
                                                         <li id="Li7" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Input_Issued_Details.aspx"
                                                               onclick="ShowProgress();">Input Periodical-Status</a></li>
                                                    </ul>
                                                </li>

                                                <%--  <li id="LiGiftIss" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/gift_issued.aspx"
                                                    onclick="ShowProgress();">Gift Issued - Regionwise</a></li>--%>

                                                <!--<li id="ulliTarget" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Target Fixation </a>
                                                    <ul id="ululTarget" class="dropdown-menu" runat="server">
                                                        <li id="liA19" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/TargetFixationProduct.aspx"
                                                            onclick="ShowProgress();">Productwise Entry</a></li>
                                                        <li id="liA20" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/TargetFixationView.aspx"
                                                            onclick="ShowProgress();">Productwise View</a></li>
                                                        <li id="liTFV11" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/TargetFixationValue.aspx"
                                                            onclick="ShowProgress();">Valuewise Entry</a></li>
                                                        <li id="liTFV12" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/TargetFixationValueView.aspx"
                                                            onclick="ShowProgress();">Valuewise View</a></li>
                                                        <li id="TarvsSal" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/TargetVsSales.aspx"
                                                            onclick="ShowProgress();">Target Vs Sales</a></li>
                                                    </ul>
                                                </li>-->
                                                <li id="ulliRetail" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Retail Chemist Prescription Audit </a>
                                                    <ul id="ululRetail" runat="server" class="dropdown-menu">
                                                        <li id="li6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/RCPAList.aspx"
                                                            onclick="ShowProgress();">Entry</a></li>
                                                        <li id="li12" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/RCPA_View.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                        <li id="li199" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/RCPA_Status.aspx"
                                                            onclick="ShowProgress();">Analysis</a></li>
                                                    </ul>
                                                </li>

                                                <li id="ulliLeave" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Leave Entitlement </a>
                                                    <ul id="ululLeave" runat="server" class="dropdown-menu">
                                                        <li id="liA21" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/Leave_Entitlement.aspx"
                                                            onclick="ShowProgress();">Entry</a></li>
                                                        <li id="liA22" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/Leave_Entitleent_view.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                    </ul>
                                                </li>
                                                <li id="liA23" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/Login_Details.aspx"
                                                    onclick="ShowProgress();">Login Details</a></li>

                                                <li id="li1Audit" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/Audit_View.aspx" onclick="ShowProgress();">Audit Report</a></li>
                                                <li id="lilogin" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/LoginFieldforce.aspx" onclick="ShowProgress();">Login Into Fieldforce</a></li>

                                                <li id="LiDocBusVal" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Doctor Business Valuewise</a>
                                                    <ul id="ulDocBusVal" class="dropdown-menu" runat="server">
                                                        <li id="liDocBusVwEntry" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorBusinessValuewise_Entry.aspx"
                                                            onclick="ShowProgress();">Entry</a></li>
                                                        <li id="liDocBusVwView" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorBusinessValuewise_View.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                    </ul>
                                                </li>
                                                <li id="LiSPS28" runat="server" class="dropdown-submenu" visible="false"><a class="dropdown-item dropdown-toggle" href="#">Stockistwise Primary Sales</a>
                                                    <ul id="ulSPS4" runat="server" class="dropdown-menu">
                                                        <li id="liSPS29" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Primary/Primary_Sales_Entry.aspx"
                                                            onclick="ShowProgress();">Entry</a></li>
                                                    </ul>
                                                </li>
                                                <li id="Li600SS_Adm" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/SSale/SS_Entry_Primary.aspx"
                                                    onclick="ShowProgress();">Secondary Sales Entry</a></li>

                                                <li id="ulliSFE" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">KPI </a>
                                                    <ul id="ululSFE" class="dropdown-menu" runat="server">
                                                        <li id="lisfeKPI" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DashboardSFE.aspx"
                                                            onclick="ShowProgress();">SFE</a></li>

                                                    </ul>
                                                </li>

                                                <%--    PS Pasted by PS on 10/10/2019 at 1.45 p.m     --%>
                                                <!--<li id="liTask" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Task Management </a>
                                                    <ul id="ulliTask" class="dropdown-menu" runat="server">
                                                        <li id="liTaskM" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Task_Management_New/TaskMode.aspx"
                                                            onclick="ShowProgress();">Mode Creation</a></li>
                                                        <li id="li31" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Task_Management_New/Task2.aspx"
                                                            onclick="ShowProgress();">Task Assign</a></li>
                                                    </ul>
                                                </li>-->
                                                <li id="Li34" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Payslip</a>
                                                    <ul id="ul1" class="dropdown-menu" runat="server">
                                                        <li id="liOp44" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Payslip.aspx"
                                                            onclick="ShowProgress();">
                                                            <asp:Label ID="lblpay" runat="server" Text="View"></asp:Label>
                                                        </a></li>
                                                        <li id="li35" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Payslip_Status.aspx"
                                                            onclick="ShowProgress();">Status</a></li>
                                                    </ul>
                                                </li>
                                                <li id="Liisedit" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Sample/Input</a>
                                                    <ul id="ulsampleedit" class="dropdown-menu" runat="server">
                                                       
                                                       
                                                         <li id="Lisaedit" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Sample_Input_Modify_MRWise.aspx"
                                                               onclick="ShowProgress();">Sample- Edit</a></li>
                                                        <li id="Liiedit" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Input_Modify_MRWise.aspx"
                                                               onclick="ShowProgress();">Input- Edit</a></li>
                                                        <li id="Liinoutsampleedit" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Sample_Input_Split_MrWise.aspx"
                                                               onclick="ShowProgress();">Sample/Input- Process</a></li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </li>
                                        <li id="ActRpt" class="nav-item dropdown" runat="server"><a class="nav-link dropdown-toggle" href="#" id="A3" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Activity Reports</a>
                                            <ul id="ulActR" class="dropdown-menu" runat="server" aria-labelledby="navbarDropdownMenuLink">
                                                <%-- <li><a href="../../../../MasterFiles/User_List.aspx" onclick="ShowProgress();">
                    User List</a></li>--%>
                                                <li id="ulliRoute" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">
                                                    <asp:Label ID="lblRoute" Text="Route Plan" runat="server"></asp:Label>
                                                </a>
                                                    <ul id="ululRoute" class="dropdown-menu" runat="server">
                                                        <li id="liAR1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/rptRoutePlan.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                        <li id="liAR2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/RoutePlan_Status.aspx"
                                                            onclick="ShowProgress();">Status</a></li>
                                                    </ul>
                                                </li>
                                                <li id="ulliTP" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Tour Plan </a>
                                                    <ul id="ululTP" class="dropdown-menu" runat="server">
                                                        <li id="liAR3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Report/TP_Consolidated_View.aspx"
                                                            onclick="ShowProgress();">Consolidated View</a></li>
                                                        <li id="liAR4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Report/TP_View_Report.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                        <li id="liAR5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Report/TP_Status_Report.aspx"
                                                            onclick="ShowProgress();">Status</a></li>
                                                        <li id="li14" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/TourPlan_Datewise.aspx"
                                                            onclick="ShowProgress();">Datewise</a></li>
                                                    </ul>
                                                </li>

                                                <li id="ulliDCR" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Daily Call Report</a>
                                                    <ul id="ululDCR" class="dropdown-menu" runat="server">
                                                        <li id="liAR6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCR_View.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                        <li id="liAR7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCR_Status.aspx"
                                                            onclick="ShowProgress();">Status</a></li>
                                                        <li id="liAR8" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCR_NotApprove.aspx"
                                                            onclick="ShowProgress();">Not Approved</a></li>
                                                        <li id="liAR9" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCR_NotSubmit.aspx"
                                                            onclick="ShowProgress();">Not Submitted</a></li>
                                                        <li id="liModCnt" runat="server" visible="true"><a class="dropdown-item" href="../../../../MasterFiles/Options/DCR_Entry_Mode.aspx"
                                                            onclick="ShowProgress();">Count - ModeWise</a></li>
                                                        <li id="liARDCR10" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCR_Approve_Reject.aspx"
                                                            onclick="ShowProgress();">Approve/Reject</a></li>
                                                        <li id="liTimest" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/MGR_Working_Hrs_View.aspx"
                                                        onclick="ShowProgress();">Time Status</a></li>
                                                        <li id="liTimestatus" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/MGR_Working_Hrs_View2.aspx"
                                                            onclick="ShowProgress();">Time Status II</a></li>

                                                    </ul>
                                                </li>
                                                <!--<li id="Li2book" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Order Booking</a>
                                                    <ul id="ul2book" class="dropdown-menu" runat="server">
                                                        <%--<li id="li3book" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Order_booking.aspx" onclick="ShowProgress();">Entry</a></li> --%>
                                                        <li id="li3bookStatus" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Order_BookingStatus.aspx" onclick="ShowProgress();">Status</a></li>
                                                        <li id="li2bookview" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Order_Booking_View.aspx" onclick="ShowProgress();">View</a></li>
                                                    </ul>
                                                </li>-->

                                                <li id="li16" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/DashBoard/TreeView.aspx"
                                                    onclick="ShowProgress();">Dash Board</a></li>
                                                <li id="liAR10" runat="server"><a class="dropdown-item" href="../../../../DashBoard/Dash_Board.aspx" onclick="ShowProgress();">Dash Board</a></li>
                                                <li id="liSaleDash" runat="server"><a class="dropdown-item" href="../../../../Sale_Dashboard_admin.aspx"
                                                    onclick="ShowProgress();">Sale Dashboard</a></li>
                                                <li id="ullisurvey" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Survey </a>
                                                    <ul id="ul13" class="dropdown-menu" runat="server">
                                                        <li id="li74" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Survey/Survey_Ques_Creation.aspx"
                                                            onclick="ShowProgress();">Question Creation</a></li>
                                                        <li id="li75" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Survey/Survey_Ques_Process.aspx"
                                                            onclick="ShowProgress();">Updation</a></li>
                                                        <li id="li76" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Survey/Survey_Process_View.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                    </ul>
                                                </li>
                                                <%--<li><a href="#">Expense Statement &raquo;</a>
                    <ul>
                        <li><a href="#">View/Approval</a></li>
                        <li><a href="#">Status</a></li>
                    </ul>
                </li>--%>
                                                <%-- <li><a href="#">Doctor Details &raquo;</a>
                    <ul>
                        <li><a href="../../../../MasterFiles/Reports/DoctorList_Reportaspx.aspx" onclick="ShowProgress();">
                            View</a></li>
                    </ul>
                </li>
                <li><a href="../../../../MasterFiles/Reports/MR_Status_Report.aspx" onclick="ShowProgress();">
                    Fieldforce Status</a></li>--%>
                                            </ul>
                                        </li>
                                        <li id="Mis" class="nav-item dropdown" runat="server"><a class="nav-link dropdown-toggle" href="#" id="A5" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">MIS Reports</a>
                                            <ul id="ulmis" runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                                <li id="ulliMgrAly" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Manager Analysis </a>
                                                    <ul id="ululMgrAly" class="dropdown-menu" runat="server">
                                                        <li id="liMis1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Mgr_Coverage.aspx"
                                                            onclick="ShowProgress();">HQ - Coveragewise</a></li>
                                                        <li id="liMis2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Coverage_Analysis.aspx"
                                                            onclick="ShowProgress();">Doctor Coverage Analysis Reports</a></li>
                                                        <li id="liMis3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Coverage_Analysis_2.aspx"
                                                            onclick="ShowProgress();">Coverage Analysis 2</a></li>
                                                        <li id="liMis4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/JointWk_Analysis.aspx"
                                                            onclick="ShowProgress();">Joint Workwise</a></li>
                                                        <li id="liMis5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/FieldWork_Analysis.aspx"
                                                            onclick="ShowProgress();">FieldWork Manager - Analysis</a></li>
                                                        <li id="li11" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/ManagerCovrg_Analysis.aspx"
                                                            onclick="ShowProgress();">Coverage Analysis(Mgr)</a></li>
                                                        <li id="li60" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Manager_Analysis.aspx"
                                                            onclick="ShowProgress();">HQ wise Visit</a></li>
                                                        <li id="lidcov" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Doctors_Coverage_Detail.aspx"
                                                            onclick="ShowProgress();">Doctors Coverage Detail with Specialtywise</a></li>
                                                    </ul>
                                                </li>
                                                <li id="ulliRprt" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Report </a>
                                                    <ul id="ululRprt" class="dropdown-menu" runat="server">
                                                        <li id="li24" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Leave_Status_Datewise.aspx"
                                                            onclick="ShowProgress();">Attendance </a></li>
                                                        <li id="li2" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Fully_Operative_Report.aspx"
                                                            onclick="ShowProgress();">Fully Operative</a> </li>
                                                        <li id="li32" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Spcltywise_Coverage_Analysis.aspx"
                                                            onclick="ShowProgress();">Spclty Wise Coverage Analysis </a></li>
                                                        <li id="li37" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SS_Inventory_Prodwise.aspx"
                                                            onclick="ShowProgress();">SS - Inventory Stockistwise</a> </li>
                                                        <li id="li41" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SS_Inventory_Prodwise_Prod.aspx"
                                                            onclick="ShowProgress();">SS - Inventory Productwise</a> </li>

                                                        <li id="licomp" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/EffortVsSales_Wok_Analysis.aspx"
                                                            onclick="ShowProgress();">Comprehensive Work Analysis</a> </li>
                                                        <li id="li40" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SS_ALL_Dump_Multi_Mnth.aspx"
                                                            onclick="ShowProgress();">Sales Consolidated </a></li>
                                                        <li id="li44" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SS_Stockist_HQWise.aspx"
                                                            onclick="ShowProgress();">Sales All Stockist</a> </li>
                                                        <li id="li47" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Primary Sales Consolidated </a></li>
                                                        <li id="li5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SecondarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Secondary Sales Consolidated </a></li>
                                                         <li id="li30_new" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SecondarySale_Multi_Mnth_Dump_New.aspx"
                                                            onclick="ShowProgress();">Secondary Sales Consolidated(Old) </a></li>
                                                        <li id="ulliSecondary" runat="server"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Secondary_StkProd_Bill_View.aspx"
                                                            onclick="ShowProgress();">Secondary Sale(2019-2020/2020-2021)</a></li>
                                                    </ul>
                                                </li>
                                                <li id="Li42" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Digital Detailing </a>
                                                    <ul id="ul2" class="dropdown-menu" runat="server">
                                                        <li id="li43" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/Detaildrs_Visit.aspx"
                                                            onclick="ShowProgress();">Visit wise </a></li>
                                                        <li id="li49" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/Brdwise_Star_Rating.aspx"
                                                            onclick="ShowProgress();">Brand wise Star Rating</a></li>
                                                        <li id="li48" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/Dashboard_DDSlide.aspx"
                                                            onclick="ShowProgress();">Product Slide Analyis</a></li>
                                                        <li id="li488" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Joint_work_detailing.aspx"
                                                            onclick="ShowProgress();">Slide wise - Attendance</a></li>

                                                         <li id="liprdSKU" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/ProductExpBrandWiseAnalysis.aspx"
                                                        onclick="showprogress();">Product Exposure [SKU]</a></li>

                                                    </ul>
                                                </li>
                                                <li id="ulliAnaly" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Analysis </a>
                                                    <ul id="ululAnaly" class="dropdown-menu" runat="server">
                                                        <li id="liMis6" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/DCR_Analysis.aspx"
                                                            onclick="ShowProgress();">Daily Call Report</a> </li>
                                                        <li id="liMis7" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Month_Vertical_Wise.aspx"
                                                            onclick="ShowProgress();">Visit Analysis</a></li>
                                                        <%--  <li id="liVstGlnce" runat="server"><a href="../../../../MIS Reports/Visit_Analysis_At_A_Glance.aspx"
                                onclick="ShowProgress();">Visit At A Glance</a></li>--%>
                                                        <li id="liMis8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Analysis_Pob_count.aspx"
                                                            onclick="ShowProgress();">Doctors/Chemists POB Report</a></li>

                                                        <li id="li3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Analysis_Pob_count_Periodically.aspx"
                                                            onclick="ShowProgress();">Product wise Rx Report</a></li>
                                                        <li id="liMisCon" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Consolidated_Report.aspx"
                                                            onclick="ShowProgress();">Work Hygeine Report</a></li>
                                                        <li id="li18" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Discipline_Coverage_Report.aspx"
                                                            onclick="ShowProgress();">Discipline Coverage Report</a></li>
                                                        <li id="li1DCRAnal" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/DCR_Analysis_Dump_New.aspx"
                                                            onclick="ShowProgress();">DCR Analysis - Dump</a></li>


                                                        <%-- <li id="li15" runat="server"><a href="../../../../MIS Reports/Sales_Details.aspx"
                                onclick="ShowProgress();">Sales Details</a></li>--%>
                                                    </ul>
                                                </li>
                                                <li id="ulliSingle" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Single Analysis </a>
                                                    <ul id="ululSAnaly" class="dropdown-menu" runat="server">
                                                        <li id="li8doc" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Single_Dr_Analysis.aspx"
                                                            onclick="ShowProgress();">Doctor - Analysis</a></li>
                                                        <%--<li id="li10Rep" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Rep_Vs_Mgr.aspx"
                                                            onclick="ShowProgress();">Rep Vs Manager</a></li>
                                                        <li id="li15Rev" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Review_Report.aspx"
                                                            onclick="ShowProgress();">Review Report</a></li>
                                                        <li id="li18Ass" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/MR_Assessment.aspx"
                                                            onclick="ShowProgress();">Assessment Report</a></li>--%>
                                                    </ul>
                                                </li>
                                                <li id="ulliHeat" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Heat Analysis </a>
                                                    <ul id="ululHeat" class="dropdown-menu" runat="server">
                                                        <li id="li51" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Not_At_All_Visit_Drs.aspx"
                                                            onclick="ShowProgress();">Not at all Visited Drs</a></li>
                                                        <li id="li52" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Not_At_All_Prom_Prod.aspx"
                                                            onclick="ShowProgress();">Not at all Promoted Products</a></li>
                                                        <li id="li53" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Not_At_All_Visit_HQs.aspx"
                                                            onclick="ShowProgress();">Not at all Visited HQs</a></li>

                                                    </ul>
                                                </li>
                                                 <li id="liMis9" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#"
                                                onclick="ShowProgress();">Missed Call Report</a>
                                                     <ul id="ul16" class="dropdown-menu" runat="server">
                                                           <li id="li30" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Missed_Call.aspx"
                                                        onclick="ShowProgress();">Missed Call</a></li>

                                                  <li id="limissedDrChm" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Missed_DrChem_Dump.aspx"
                                                        onclick="ShowProgress();">Missed Dr/Chem Dump</a></li>
                                                   
                                                </ul>
                                                </li>
                                                <%-- <li><a href="../../../../MIS Reports/MissedCallReport.aspx" onclick="ShowProgress();">
                        Missed Call</a></li>--%>
                                                <%--<ul>
                        <li><a href="#">Listed Doctorwise</a></li>
                    </ul>--%>
                                                <%--  <li id="Li15" runat="server"><a  class="dropdown-item" href="#">Heat Analysis </a></li>--%>
                                                <li id="ulliVisit" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Visit Details </a>
                                                    <ul id="ululVisit" class="dropdown-menu" runat="server">
                                                        <%--  <li><a href="../../../../MIS Reports/Visit_Details_Report.aspx" onclick="ShowProgress();">
                                Listed Doctorwise</a></li>--%>
                                                        <li id="liMis10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Visit_Details_Cat_Cls_Spclty_LstDr_Wise.aspx"
                                                            onclick="ShowProgress();">Cat/Cls/Splty/LstDr Wise</a></li>
                                                        <li id="liMis11" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/VisitDetail_Datewise.aspx"
                                                            onclick="ShowProgress();">Datewise</a></li>
                                                        <li id="liMis12" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Listeddr_View_Period.aspx"
                                                            onclick="ShowProgress();">Doctorwise(Periodically)</a></li>
                                                        <li id="liMis13" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Callfeedback_Pobwise.aspx"
                                                            onclick="ShowProgress();">Call Feedbackwise</a></li>
                                                        <li id="liMis14" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_Fixation.aspx"
                                                            onclick="ShowProgress();">Fixationwise(By Visit)</a></li>
                                                        <li id="liMis15" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_Basedon_ModeWise.aspx"
                                                            onclick="ShowProgress();">Based on Modewise</a></li>
                                                        <li id="liMis16" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_At_a_Glance.aspx"
                                                            onclick="ShowProgress();">at a Glance</a></li>
                                                        <li id="lblvacant" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/MangerVisit_VacantMR.aspx"
                                                            onclick="ShowProgress();">Manager - Visit(Vacant HQ's)</a></li>
                                                        <li id="liMis_Chm_Ul" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_Chmst_UnLstDr.aspx"
                                                            onclick="ShowProgress();">Chemist & UnListed Doctors</a></li>
                                                        <li id="terrDrvisit" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/TerrTypeWise_DrVisit.aspx"
                                                            onclick="ShowProgress();">Territorywise - Listed Doctor Visit</a></li>
                                                        <%--   <li><a href="#">Modewise</a></li>
                        <li><a href="#">Productwise</a></li>--%>
                                                    </ul>
                                                </li>
                                                <li id="ullicore" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">MVD </a>
                                                    <ul id="ul5" class="dropdown-menu" runat="server">
                                                        <li id="li39" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Core_Drs.aspx"
                                                            onclick="ShowProgress();">Visit View</a></li>
                                                        <li id="li28" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Core_drs_Status.aspx"
                                                            onclick="ShowProgress();">Status</a></li>
                                                        <li id="li29" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Core_drs_Speclty_wise.aspx"
                                                            onclick="ShowProgress();">Coverage</a></li>
                                                        <li id="li36" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/CoreDrsMap_Visit_Dump.aspx"
                                                            onclick="ShowProgress();">Visit - Excel Dump</a></li>
                                                    </ul>
                                                </li>
                                                <li id="ulliSales" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Sales Analysis </a>
                                                    <ul id="ululSales" class="dropdown-menu" runat="server">
                                                        <li id="liMis17" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Reports/SecSalesReport.aspx"
                                                            onclick="ShowProgress();">Stock & Sales Statement</a></li>
                                                        <li id="li9" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/SecSaleReport/SecSalesReport.aspx"
                                                            onclick="ShowProgress();">Stock & Sales Consolidated</a></li>
                                                        <%-- <li id="li5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/SecSaleReport/SalesAnalysis.aspx"
                                                            onclick="ShowProgress();">Mode wise</a></li>--%>
                                                        <li id="liMis18" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Sale_Analysis.aspx"
                                                            onclick="ShowProgress();">Consolidated View</a></li>
                                                        <%--  <li id="liMis19" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/SecSaleReport/SecSalesReport_All_MR.aspx"
                                                        onclick="ShowProgress();">Excel Download</a></li>--%>
                                                    </ul>
                                                </li>
                                                <%--<li><a href="#">Product Exposure &raquo;</a>
                    <ul>
                        <li><a href="#">Detailed View</a></li>
                        <li><a href="#">Speciality/Category Wise</a></li>
                    </ul>
                </li>--%>
                                                <li id="ulliCamp" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Campaign</a>
                                                    <ul id="ululCamp" class="dropdown-menu" runat="server">
                                                        <li id="liMis20" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/ListeddrCamp_View.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                        <li id="limgrcc20" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/ChemistCamp_View.aspx" onclick="ShowProgress();">Chemist Campaign View</a></li>

                                                        <li id="liMis21" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Campaign_View.aspx"
                                                            onclick="ShowProgress();">Status</a></li>
                                                    </ul>
                                                </li>
                                                <li id="ulliProductEx" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Product Exposure </a>
                                                    <ul id="ululProductEx" class="dropdown-menu" runat="server">
                                                        <li id="liMis22" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Product_Exp_Detail.aspx"
                                                            onclick="ShowProgress();">Analysis</a></li>
                                                        <li id="liMis23" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Product_Exp_specat.aspx"
                                                            onclick="ShowProgress();">Speciality/Category Wise</a></li>
                                                        <li id="liMis24" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Territory_Format.aspx"
                                                            onclick="ShowProgress();">ListedDr - Productwise Visit</a></li>
                                                        <li id="prdunlisted" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Product_Exp_Detail_Unlisted.aspx"
                                                            onclick="ShowProgress();">Analysis - Unlisted Doctor</a></li>
                                                        <li id="Li13" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Product_Priority_Wise.aspx"
                                                            onclick="ShowProgress();">Priority Wise</a></li>
                                                        <li id="Li19" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_Chmst_Product_Wise_Sale.aspx"
                                                            onclick="ShowProgress();">Chemist Product Qty Wise Sale</a></li>
                                                    </ul>
                                                </li>
                                                <li id="ulliSamIn" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Sample / Input </a>
                                                    <ul id="ululSamIn" class="dropdown-menu" runat="server">
                                                        <li id="liMis25" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/sampleproduct_details.aspx"
                                                            onclick="ShowProgress();">Sample Issued - Fieldforce Wise</a></li>
                                                        <li id="liMis26" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/SampleGift_details.aspx"
                                                            onclick="ShowProgress();">Input Issued - Fieldforce Wise</a></li>
                                                        <li id="li1" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/SamplePrd_Rxqty.aspx"
                                                            onclick="ShowProgress();">Sample Rx Quantity</a></li>
                                                    </ul>
                                                </li>
                                                <%-- <li><a href="#">Input Details &raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/SampleGift_details.aspx" onclick="ShowProgress();">
                                Fieldforce Wise</a></li>
                        </ul>
                    </li>--%>
                                                <%-- <li><a href="#">ListedDr - Productwise Visit&raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/Territory_Format.aspx" onclick="ShowProgress();">
                                View</a></li>
                        </ul>
                    </li>--%>
                                                <li id="ulliStat" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Status</a>
                                                    <ul id="ululStat" class="dropdown-menu" runat="server">
                                                        <li id="liMis27" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Delayed_DCR_Status.aspx"
                                                            onclick="ShowProgress();">Delayed</a></li>
                                                        <%--        <li id="liMis28" runat="server"><a href="../../../../MIS Reports/Leave_DCR_Status.aspx"
                                onclick="ShowProgress();">Leave</a></li>--%>
                                                        <li id="activefield" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Leave_DCR_Status_active.aspx"
                                                            onclick="ShowProgress();">Leave - Active Fieldforce</a></li>
                                                        <li id="Li1perio" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Leave_DCR_Status_Periodically.aspx"
                                                            onclick="ShowProgress();">Leave - Periodically</a></li>
                                                        <li id="liMis68" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Mail_Status.aspx"
                                                            onclick="ShowProgress();">Mail</a></li>
                                                        <li id="li7new" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Missed_DCR_Status.aspx"
                                                            onclick="ShowProgress();">Missed</a></li>
                                                    </ul>
                                                </li>
                                                <li id="ulliliMis29" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Exception </a>
                                                    <ul id="ululliMis29" class="dropdown-menu" runat="server">
                                                        <li id="tpmr" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/TP_Deviation.aspx"
                                                            onclick="ShowProgress();">Tour Plan - Baselevel</a> </li>
                                                        <li id="tpmgr" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/TP_DeviationMGR.aspx"
                                                            onclick="ShowProgress();">Tour Plan - Managers</a> </li>
                                                        <li id="tpglance" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/TP_Deviation_At_Glance.aspx"
                                                            onclick="ShowProgress();">At a Glance</a></li>
                                                    </ul>
                                                </li>
                                                <li id="ulliOpt" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Options </a>
                                                    <ul id="ululOpt" class="dropdown-menu" runat="server">
                                                        <li id="liMisResign" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Resigned_User_Status.aspx" onclick="ShowProgress();">Resigned User Status </a></li>
                                                        <li id="liMis30Join" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/frmJoin_LeftDetails.aspx"
                                                            onclick="ShowProgress();">Join/Left Details </a></li>
                                                    </ul>
                                                </li>
                                                <li id="ulliDump" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Dump</a>
                                                    <ul id="ululDump" class="dropdown-menu" runat="server">
                                                        <li id="li45" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/ListeddrPeriod_ALL_Dump.aspx"
                                                            onclick="ShowProgress();">Visit - Drs</a></li>
                                                        <li id="li46" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SS_ALL_Dump.aspx"
                                                            onclick="ShowProgress();">Secondary Sale</a></li>
                                                        <li id="li20" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SS_ALL_Dump_New_NRV.aspx"
                                                            onclick="ShowProgress();">Secondary Sale - Comparision</a></li>
                                                        <li id="li21" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Listeddr_Dump.aspx"
                                                            onclick="ShowProgress();">Listeddr</a></li>
                                                        <li id="li22" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Chemists_Dump.aspx"
                                                            onclick="ShowProgress();">Chemist</a></li>
                                                        <li id="li17" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Campaign_Dump.aspx"
                                                            onclick="ShowProgress();">Campaign Visit - Drs</a></li>
                                                        <li id="liLstStk" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Stockist_HQFF_Dump.aspx"
                                                            onclick="ShowProgress();">Listed Stockiest</a></li>
                                                    </ul>
                                                </li>
                                              <li id="ullidump2" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Dump-II</a>
                                                    <ul id="ululDump2" class="dropdown-menu" runat="server">
                                                        <li id="lidrBusPro" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/DrBusinessProductDump.aspx"
                                                            onclick="ShowProgress();">Doctor Product Business </a></li>
                                                     <li id="sample_Dump" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/sampleproduct_details_exel.aspx"
                                                        onclick="ShowProgress();">Sample-Issued Excel Download</a></li>
                                                       <li id="input_Dump" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/SampleGift_details_exel.aspx"
                                                        onclick="ShowProgress();">Input-Issued Excel Download</a></li>
                                                       <li id="acknowledge" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/sample_input.aspx"
                                                        onclick="ShowProgress();">Sample Acknowledgment</a></li>
                                                        <%--  <li id="liSampleNonDes" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/DCR_NonDespatch_Sample.aspx"
                                                        onclick="ShowProgress();">DCR-NonDespatch-Sample Excel Download</a></li>--%>
                                                  </ul>
                                               </li>

                                                <li id="ulliPrimary" runat="server"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Primary_Upload_Bill.aspx"
                                                    onclick="ShowProgress();">Primary</a></li>

                                                <li id="ulliBill" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Primary Bill </a>
                                                    <ul id="ululBil" class="dropdown-menu" runat="server">
                                                        <li id="lib1" runat="server"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Primary_Stk_Bill_View.aspx"
                                                            onclick="ShowProgress();">Stockist View</a></li>
                                                        <li id="lib2" runat="server"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Primary_Product_Bill_View.aspx"
                                                            onclick="ShowProgress();">Product View</a></li>
                                                        <li id="liStck" runat="server" visible="false"><a class="dropdown-item" href="../../../MasterFiles/Stock_Product.aspx"
                                                            onclick="ShowProgress();">Sale - Stockist Wise</a></li>

                                                    </ul>
                                                </li>
                                                <li id="Lidocaddde" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Doctor</a>
                                                       <ul id="uladddedd" class="dropdown-menu" runat="server">
                                                   <li id="liaddddd" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Drs_Add_Del_at_a_glance.aspx" onclick="ShowProgress();">Add/Deactive Status</a></li>
                                              </ul>
                                       </li>
                                                
                                                <li id="liQuizRes" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Quiz_Status.aspx" onclick="ShowProgress();">Quiz Test Result</a></li>

                                                <li id="Li1sum" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Summary</a>
                                                    <ul id="ul3" class="dropdown-menu" runat="server">
                                                        <li id="li23" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/DCR_Dump.aspx"
                                                            onclick="ShowProgress();">Day Wise Report</a></li>
                                                        <li id="li27" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/DCRDump_New.aspx"
                                                            onclick="ShowProgress();">Call Report Dump</a></li>
                                                    </ul>
                                                </li>
                                                <li id="liviwbillS" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/SalesView.aspx"
                                                    onclick="ShowProgress();">Sales Bill View</a></li>

                                            </ul>
                                        </li>
                                          <li class="nav-item"><a class="nav-link" href="../../../../MasterFiles/DynamicDashboard/Dashboard.aspx">Dashboard</a></li>
                                        <li id="Option" class="nav-item dropdown" runat="server"><a class="nav-link dropdown-toggle" href="#" id="A4" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Options</a>
                                            <ul id="ulopt" runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">

                                                <li id="liOp1" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Options/ChangePassword.aspx"
                                                    onclick="ShowProgress();">Change Password</a></li>

                                                <li id="ulliVacant" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Vacant MR Login </a>
                                                    <ul id="ululVacant" class="dropdown-menu" style="left: -100%; width: 100%" runat="server">
                                                        <li id="liOp2" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Options/Vacant_MR_Access.aspx"
                                                            onclick="ShowProgress();">Access</a></li>
                                                        <li id="liOp3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Permission_MR.aspx"
                                                            onclick="ShowProgress();">Permission for Managers </a></li>
                                                    </ul>
                                                </li>
                                                <%-- <li><a href="#">Vacant MR Login - Access</a></li>--%>
                                                <li id="ulliUpdate" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Update/Delete </a>
                                                    <ul id="ululUpdate" class="dropdown-menu" style="left: -105%; width: 105%" runat="server">
                                                        <%--<li id="liOp4" runat="server"><a href="../../../MasterFiles/Options/TPEdit.aspx" onclick="ShowProgress();">TP
                                Edit</a></li>--%>
                                                        <%--<li id="liOp5" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Options/TPDelete.aspx"
                                                            onclick="ShowProgress();">TP Delete</a></li>--%>
                                                        <%--<li id="liOp5" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Options/TP_Delete.aspx"
                                                            onclick="ShowProgress();">TP Delete</a></li>--%>
                                                        <li id="liOp5" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Options/TP_Delete_New.aspx"
                                                            onclick="ShowProgress();">TP Delete</a></li>
                                                        <li id="liOp6" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Options/DCREdit.aspx"
                                                            onclick="ShowProgress();">DCR Edit</a></li>
                                                        <li id="liOp7" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Options/SecSales_Stockist_Edit_FinYr.aspx"
                                                            onclick="ShowProgress();">SS Delete</a></li>
                                                        <%-- <li id="liOp7" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Options/SecSales_Edit.aspx"
                                                            onclick="ShowProgress();">SS Edit</a></li>--%>
                                                        <%-- <li><a href="#">DCR Delete</a></li>--%>
                                                        <li id="liOp8" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Options/MailView.aspx"
                                                            onclick="ShowProgress();">Mail Delete</a></li>
                                                        <li id="leavecancel" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Options/Leave_Cancel.aspx"
                                                            onclick="ShowProgress();">Leave Cancellation</a></li>
                                                        <li id="Li1app_device" runat="server" visible="false"><a class="dropdown-item" href="../../../MasterFiles/Options/DeviceIdDeletion.aspx"
                                                            onclick="ShowProgress();">Mob App - Device Id&nbsp; Deletion</a></li>
                                                        <li id="Lisession" runat="server" visible="false"><a class="dropdown-item" href="../../../MasterFiles/Options/Session_Release.aspx"
                                                            onclick="ShowProgress();">Start/End Time Release</a></li>
                                                        <li id="tp_devrelease" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Options/TP_Deviation_Release.aspx"
                                                            onclick="ShowProgress();">TP Deviation - Release</a></li>
                                                        <li id="lisaminputedit" runat="server" visible="false"><a class="dropdown-item" href="../../../MasterFiles/Options/Sample_Input_Qty_Edit.aspx"
                                                            onclick="ShowProgress();">Sample Input QtyEdit</a></li>
                                                        <%--<li id="Licmp_Rst" runat="server"><a class="dropdown-item" href="../../../MasterFiles/MR/Campaign_Doc_Reset.aspx"
                                                        onclick="ShowProgress();">Campaign&nbsp; Lock Reset</a></li>
                                                    <li id="Licmp_bus_Rst" runat="server"><a class="dropdown-item" href="../../../MasterFiles/MR/Campaign_Doc__Buss_Reset.aspx"
                                                        onclick="ShowProgress();">Campaign&nbsp; Bussiness Lock Reset</a></li>--%>
                                                    </ul>
                                                </li>
                                                <li id="ulliSetup" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Basic Setup </a>
                                                    <ul id="ululSetup" class="dropdown-menu" style="left: -100%; width: 100%" runat="server">
                                                        <%-- <li><a href="../../../MasterFiles/AdminSetup.aspx" onclick="ShowProgress();">
                            TP/DCR</a></li>--%>
                                                        <li id="liOp9" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/SetupScreen.aspx"
                                                            onclick="ShowProgress();">Screen Access Rights</a></li>
                                                        <li id="liOp10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/AdminSetup.aspx"
                                                            onclick="ShowProgress();">Base Level</a></li>
                                                        <li id="liOp11" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/AdminSetupMGR.aspx"
                                                            onclick="ShowProgress();">Managers</a></li>
                                                        <li id="liOp12" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/HomePageRest.aspx"
                                                            onclick="ShowProgress();">Approval Mandatory</a></li>
                                                        <li id="liOp13" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Mgrwise_Core_Doc_Map.aspx"
                                                            onclick="ShowProgress();">Managerwise Core Doctor Map</a></li>
                                                        <li id="liOp14" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Screenwise_Lock.aspx"
                                                            onclick="ShowProgress();">Screenwise Access</a></li>
                                                        <li id="liOp15" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Mail_Folder_Creation.aspx"
                                                            onclick="ShowProgress();">Mail Folder Creation</a></li>
                                                        <li id="liOther" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Other_Setup.aspx"
                                                            onclick="ShowProgress();">Other Setup</a></li>
                                                        <li id="li8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Leave_Setup.aspx"
                                                            onclick="ShowProgress();">Leave Setup</a></li>
                                                        <li id="lidelock" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Device_Lock.aspx"
                                                             onclick="ShowProgress();">Device Lock</a></li>
                                                    </ul>
                                                </li>
                                                <li id="ulliAppSet" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">App Setup </a>
                                                    <ul id="ululAppSet" class="dropdown-menu" style="left: -100%; width: 100%" runat="server">
                                                        <li id="liOp16" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/App_CallFeedback.aspx"
                                                            onclick="ShowProgress();">Call Feedback</a></li>
                                                        <li id="liOp17" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/App_CallRemarks.aspx"
                                                            onclick="ShowProgress();">Call Remarks Templates</a></li>
                                                        <li id="liOp18" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Notfication_Msg.aspx"
                                                            onclick="ShowProgress();">Notification Message</a></li>
                                                        <li id="li10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/GPS_GEOFence.aspx"
                                                            onclick="ShowProgress();">Gps/Geofence & Tagg Deletion</a></li>
                                                         <li id="liMblLink" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Menu_Creation.aspx"
                                                        onclick="ShowProgress();">Dynamic App Link</a></li>
                                                        <%-- <li><a href="../../../../MasterFiles/Options/Mob_App_Setting.aspx" onclick="ShowProgress();">
                                Mobile Apps Setting</a></li>--%>
                                                    </ul>
                                                </li>
                                                <li id="ulliSecSet" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Seconday Sale Setup </a>
                                                    <ul id="ululSecSet" class="dropdown-menu" style="left: -100%; width: 100%" runat="server">
                                                        <li id="liOp19" runat="server"><a class="dropdown-item" href="../../../../SecondarySales/SetUp.aspx" onclick="ShowProgress();">SS Entry - Setup 1</a></li>
                                                        <li id="liOp20" runat="server"><a class="dropdown-item" href="../../../../SecondarySales/SecSalesSetUp.aspx"
                                                            onclick="ShowProgress();">SS Entry - Setup 2</a></li>
                                                        <li id="liOp21" runat="server"><a class="dropdown-item" href="../../../../SecondarySales/CustomizedColumn.aspx"
                                                            onclick="ShowProgress();">SS Entry - Setup 3</a></li>
                                                    </ul>
                                                </li>
                                                <li id="liOp22" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Mails/Mail_Head.aspx"
                                                    onclick="ShowProgress();">Mail Box</a></li>
                                                <li id="Li_lstdr7" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Customer Upload</a>
                                                    <ul id="ul_lstdr6" class="dropdown-menu" style="left: -100%; width: 100%" runat="server">
                                                        <li id="li_lst20" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Listeddr_BulkUpload_Dynamic_Bk.aspx"
                                                            onclick="ShowProgress();">Listed Doctor</a></li>
                                                        <li id="liTr4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Target_Upload.aspx"
                                                            onclick="ShowProgress();">Target</a></li>
                                                         <li id="lisecupl" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Seconday_Sale_Upload.aspx"
                                                            onclick="ShowProgress();">Secondary Sale</a></li>
                                                    </ul>
                                                </li>
                                                <li id="ulliInUp" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Information Upload</a>
                                                    <ul id="ululInUp" class="dropdown-menu" style="left: -100%; width: 100%" runat="server">
                                                        <li id="liOp23" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/FlashNews.aspx"
                                                            onclick="ShowProgress();">Flash News</a></li>
                                                        <li id="liOp24" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/NoticeBoard.aspx"
                                                            onclick="ShowProgress();">Notice Board</a></li>
                                                        <li id="liOp25" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Quote.aspx"
                                                            onclick="ShowProgress();">Quote for the Week</a></li>
                                                        <li id="liOp26" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/TalktoUs.aspx"
                                                            onclick="ShowProgress();">Talk to Us</a></li>
                                                        <li id="liOp27" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/FileUpload.aspx"
                                                            onclick="ShowProgress();">File/Circular (Desig.Wise)</a></li>
                                                        <li id="liOp28" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Usermanual_Upload.aspx"
                                                            onclick="ShowProgress();">User Manual Upload</a></li>
                                                    </ul>
                                                </li>

                                                <li id="ulliUpld" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Upload </a>
                                                    <ul id="ululUpld" class="dropdown-menu" style="left: -100%; width: 100%" runat="server">
                                                        <li id="liOp29" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/ListedDr_Upload.aspx"
                                                            onclick="ShowProgress();">Listed Doctor</a></li>
                                                        <li id="liOp30" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Listeddr_BulkUpload.aspx"
                                                            onclick="ShowProgress();">Listed Doctor - Bulk</a></li>
                                                        <li id="liOp31" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Chemists_Upload.aspx"
                                                            onclick="ShowProgress();">Chemist</a></li>
                                                        <li id="liOp32" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Chemists_BulkUpload.aspx"
                                                            onclick="ShowProgress();">Chemist - Bulk</a></li>
                                                        <li id="liOp33" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Salesforce_Upload.aspx"
                                                            onclick="ShowProgress();">Field Force</a></li>
                                                        <li id="liOp34" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Stockiest_Upload.aspx"
                                                            onclick="ShowProgress();">Stockist</a></li>
                                                        <li id="liOp35" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Product_Upload.aspx"
                                                            onclick="ShowProgress();">Product</a></li>
                                                        <li id="li4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Product_Rate_Upload.aspx"
                                                            onclick="ShowProgress();">Product Rate</a></li>
                                                        <li id="liOp36" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Options/Primary_Upload.aspx"
                                                            onclick="ShowProgress();">
                                                            <asp:Label ID="lblp_upl" runat="server" Text="Primary Upload"></asp:Label></a></li>
                                                        <li id="li33" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Gift_Upload.aspx"
                                                            onclick="ShowProgress();">Gift</a></li>
                                                        <li id="liOp37" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/PaySlip_Upload.aspx"
                                                            onclick="ShowProgress();">
                                                            <asp:Label ID="lblChange" runat="server" Text="Payslip"></asp:Label></a></li>
                                                        <%--   <li><a href="#">Unlisted Doctor</a></li>--%>
                                                        <li id="liPay" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Options/Payslip_Files_Upload.aspx"
                                                            onclick="ShowProgress();">Payslip Files Upload</a></li>
                                                        <li id="lisfcupl" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Territory_SFC_Upload.aspx"
                                                            onclick="ShowProgress();">SFC Upload</a></li>
                                                        <li id="li24DDSlide" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/DD_Slide_Upload.aspx"
                                                            onclick="ShowProgress();">
                                                            <asp:Label ID="Label1" runat="server" Text="Slides Upload"></asp:Label></a></li>
                                                        <li id="li33Holoday" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Holiday_Upload.aspx"
                                                            onclick="ShowProgress();">
                                                            <asp:Label ID="lblHolidayU" runat="server" Text="Holiday Fixation"></asp:Label></a></li>
                                                        <li id="lisbill" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Options/Secondary_Bill_Upload.aspx"
                                                            onclick="ShowProgress();">Sales Bill Upload</a></li>

                                                    </ul>
                                                </li>
                                                <li id="ulliTran" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Transaction Upload </a>
                                                    <ul id="ululTran" class="dropdown-menu" style="left: -100%; width: 100%" runat="server">
                                                        <li id="liTr1" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Options/Primary_Upload_Screen.aspx"
                                                            onclick="ShowProgress();">Primary</a></li>
                                                        <li id="libill" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Primary_Upload_Bill.aspx"
                                                            onclick="ShowProgress();">Primary Bill</a></li>
                                                        <li id="liPHQUpld" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Primary_Upload_HQwise.aspx"
                                                            onclick="ShowProgress();">Primary Bill HQ Wise Upload</a></li>
                                                        <li id="liTr2" runat="server" ><a class="dropdown-item" href="../../../../MasterFiles/MR/Sample_Despatch_Upload.aspx"
                                                            onclick="ShowProgress();">Sample</a></li>
                                                        <li id="liTr3" runat="server" ><a class="dropdown-item" href="../../../../MasterFiles/MR/Input_Despatch_Upload.aspx"
                                                            onclick="ShowProgress();">Input</a></li>
                                                        
                                                    </ul>
                                                </li>
                                                <li id="ulliImgUpld" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Image Upload </a>
                                                    <ul id="ululImgUpld" class="dropdown-menu" style="left: -100%; width: 100%" runat="server">
                                                        <%--     <li><a href="../../../../MasterFiles/Options/Loginpage_ImgUpload.aspx"
                            onclick="ShowProgress();">Login Page</a></li> --%>
                                                      <%--  <li id="liOp38" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Homepage_ImgUpload.aspx"
                                                            onclick="ShowProgress();">Home Page(Common For All)</a></li>--%>
                                                         <li id="liOp38" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/HomePage_ImageUpload_Common.aspx"
                                                            onclick="ShowProgress();">Home Page(Common For All)</a></li>


                                                        
                                                        <li id="liOp39" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/HomePage_FieldForcewise.aspx">Home Page(FieldForcewise)</a></li>
                                                    </ul>
                                                </li>

                                                <li id="liOp40" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Leave_Status.aspx"
                                                    onclick="ShowProgress();">Leave Status</a></li>
                                                <%--   <li><a href="../MasterFiles/Quote_Design.aspx">Design</a></li>--%>

                                                <li id="ulliTrans" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Transfers </a>
                                                    <ul id="ululTrans" class="dropdown-menu" style="left: -105%; width: 105%" runat="server">
                                                        <li id="liOp41" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/MR_MR_Transfer.aspx"
                                                            onclick="ShowProgress();">Transfer - Master Details</a></li>
                                                        <%-- <li><a href="../../../MasterFiles/Options/testing.aspx"
                            onclick="ShowProgrss();">Transfer - Master Details</a></li>--%>
                                                        <%-- <li><a href="#">Stockist With Sale</a></li>--%>
                                                        <li id="liOp42" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/Convert_Unlistto_Listeddr.aspx"
                                                            onclick="ShowProgress();">Convert Unlisted Drs - Listed Drs</a></li>
                                                        <li id="li1Hq_transfer" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/HQ_Tranfer.aspx"
                                                            onclick="ShowProgress();">Transfer - HQ( MGR )</a></li>
                                                    </ul>
                                                </li>

                                                <li id="liOp43" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Delayed_Release.aspx"
                                                    onclick="ShowProgress();">Release - Missing Dates / Delay </a></li>

                                                <li id="liOp45" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Primary_View.aspx"
                                                    onclick="ShowProgress();">
                                                    <asp:Label ID="lblprim" runat="server" Text="Primary View"></asp:Label></a></li>

                                                <li id="LocFin" runat="server" visible="false"><a class="dropdown-item" href="../../../../Location_Finder.aspx" onclick="ShowProgress();">Location Finder</a></li>
                                                <li id="liQuiz101" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Options/Quiz_List.aspx" onclick="ShowProgress();">Quiz</a></li>
                                                <li id="liQuiz_CA102" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Options/Quiz_Category_List.aspx" onclick="ShowProgress();">Quiz Category </a></li>

                                                <%-- <li><a href="#">Release &raquo;</a>
                    <ul>
                        <li><a href="#">DCR Lock</a></li>
                        <li><a href="#">TP Lock</a></li>
                        <li><a href="#">Std.Daywise Lock</a></li>
                    </ul>
                </li>--%>
                                                <%--  <li><a href="#">Setup</a></li>--%>
                                            </ul>
                                        </li>
                                        <li class="nav-item"><a class="nav-link logout" href="../../../../Index.aspx" onclick="ShowProgress();">Logout</a> <%--class="first"--%>
                                        </li>
                                    </ul>




                                </div>
                            </nav>
                        </div>
                    </div>
                </div>
            </header>
        </asp:Panel>
        <div style="text-align: center; padding-top: 20px; padding-bottom: 20px;">
            <asp:Label ID="lbldivision" runat="server" ForeColor="#0077ff">   </asp:Label>
        </div>

        <asp:Panel ID="pnlHeader" runat="server" Width="100%" align="center">
            <table width="95%" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td>
                        <table id="Table1" runat="server" width="100%">
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" ForeColor="Black" Style="font-size: 13px; text-align: center;"
                                        Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                                </td>
                                <td align="center" style="width: 30%">
                                    <asp:Label ID="lblHeading" runat="server" Style="text-transform: capitalize; font-size: 14px; text-align: center;"
                                        ForeColor="#8A2EE6" Font-Bold="True" Font-Names="Verdana"
                                        CssClass="under">
                                    </asp:Label>
                                </td>
                                <td align="right" class="style3" style="width: 35%">
                                    <asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Height="25px" Width="70px"
                                        Text="Back" OnClick="btnBack_Click" />
                                </td>
                                <%--<td align="center" style="width: 30%">--%>
                                <%-- <asp:Label ID="lblHeading" runat="server" Style="text-transform: capitalize; font-size: 14px; text-align: center;"
                                        ForeColor="#8A2EE6" Font-Bold="True" Font-Names="Verdana"
                                        CssClass="under">
                                    </asp:Label>--%>
                                <%-- </td>--%>
                                <%-- <td align="right" class="style3" style="width: 35%">--%>
                                <%--  <asp:Button ID="btnBack" runat="server" CssClass="savebutton" Height="25px" Width="70px"
                                        Text="Back" OnClick="btnBack_Click" />--%>
                                <%-- </td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>



        <%--<asp:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" BorderColor="DarkSlateGray"
        Color="DarkSlateGray" Radius="2" TargetControlID="pnlHeader">
    </asp:RoundedCornersExtender>--%>
    </div>
    <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="../Images/loader.gif" runat="server" alt="" />
    </div>


    <script>
        if ('<%# Session["Div_color"]!= null%>' == 'False') {
            document.body.style.backgroundColor = '#e8ebec';
        } else {
            document.body.style.backgroundColor = '<%# Session["Div_color"] %>'
        }
    </script>
    <script src="../../../assets/js/jQuery.min.js"></script>
    <script src="../../../assets/js/popper.min.js"></script>
    <script src="../../../assets/js/bootstrap.min.js"></script>
    <script src="../../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../../assets/js/main.js"></script>
</div>
