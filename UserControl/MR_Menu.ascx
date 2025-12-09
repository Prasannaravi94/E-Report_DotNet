<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MR_Menu.ascx.cs" Inherits="UserControl_MR_Menu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<link href="/css/MR.css" rel="stylesheet" type="text/css" />--%>
<!-- IE6-8 support of HTML5 elements -->
<!--[if lt IE 9]>
	<script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
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

    table, th, td {
        border: 0px;
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

    div {
        padding: 0px;
    }

    .navbar-brand {
        padding-top: .3125rem !important;
        padding-bottom: .3125rem !important;
        margin-right: 0rem;
    }

    .navbar-brand {
        padding: 0px 0px !important;
        padding-top: 0px !important;
    }

    .label1 {
        font-size: 15px !important;
        font-weight: 401 !important;
        color: black !important;
    }

    .justify-content-end {
        -webkit-box-pack: end !important;
        -ms-flex-pack: end !important;
        justify-content: flex-start !important;
    }

    .dropdown-item:focus, .dropdown-item:hover {
        background-color: #3c9be8 !important;
    }

    .navbar-brand {
        max-width: 125px !important;
    }
</style>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
<%--<script type="text/javascript" language="javascript">
    window.onload = function () {
        noBack();
    }
    function noBack() {
        window.history.forward();
    }
</script>--%>
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
        Do you want to Continue?
               <span id="secondsIdle" style="visibility: hidden"></span>
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
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <header class="header-area clearfix">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <!---- Menu area start ---->
                        <nav class="navbar navbar-expand-md navbar-light p-0">
                            <a class="navbar-brand col-2" href="#">
                                <img id="img" runat="server" alt="" />
                            </a>
                            <div class="col-3">
                                <asp:Label ID="LblUser" CssClass="label1" ForeColor="Black" runat="server" Text="User">  </asp:Label>
                            </div>
                            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
                                <span class="navbar-toggler-icon"></span>
                            </button>
                            <div class="collapse navbar-collapse justify-content-end main-menu" id="collapsibleNavbar">
                                <ul id="menu" class="navbar-nav">
                                   <%-- <li class="nav-item"><a href="../../../../Default_MR.aspx" class="nav-link active" onclick="ShowProgress();">Home</a></li>--%>
                                     <li class="nav-item"><a href="../../../../Default_MR_Basic.aspx" class="nav-link active" onclick="ShowProgress();">Home</a></li>
                                    <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" href="#" id="A1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Information </a>
                                        <ul id="ul_Inf" runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                            <li id="liMRIn1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ProductRate.aspx" onclick="ShowProgress();">Product Information</a></li>
                                            <li id="liMRIn2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Territory/Territory.aspx" onclick="ShowProgress();">
                                                <asp:Label ID="lblTerritory" Text="Territory" runat="server"></asp:Label></a></li>
                                            <li id="ulliDoc" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Listed Doctor Details</a>
                                                <ul id="ululDoc" class="dropdown-menu" runat="server">
                                                    <li id="liMRIn3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorList.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMRIn4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorView.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                            <li id="liunique" class="dropdown-submenu" runat="server" visible="false"><a class="dropdown-item dropdown-toggle" href="#">Unique Drs</a>
                                                <ul id="ul2" class="dropdown-menu" runat="server">
                                                    <li id="li3" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Common_Doctors/Common_Doctor_List_FDC.aspx" onclick="ShowProgress();">Creation</a></li>
                                                    <li id="li4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/ListedDrEdit.aspx" onclick="ShowProgress();">Edit</a></li>
                                                    <li id="li6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/ListedDrDeactivate.aspx" onclick="ShowProgress();">Deactivation</a></li>
                                                    <li id="li2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/ListedDrReactivate.aspx" onclick="ShowProgress();">Reactivation</a></li>
                                                    <li id="li7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/Listeddr_Prod_Map_New.aspx" onclick="ShowProgress();">ListedDr-Product Map</a></li>
                                                    <%-- <li id="li6" runat="server"><a href="../../../MasterFiles/Common_Doctors/Unique_ListedDrDeactivate.aspx" onclick="ShowProgress();">Deacitivation</a></li>--%>
                                                    <li id="li5" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Common_Doctors/UniqueDr_Approval_Pending.aspx" onclick="ShowProgress();">Unique DR - Approval Pending View</a></li>
                                                    <li id="li1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorView.aspx" onclick="ShowProgress();">View</a></li>
                                                    <li id="li21" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/CommDr_Pend_Mrwise.aspx"
                                                        onclick="ShowProgress();">Dr Approval - Pending Count</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliChem" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Chemist Details</a>
                                                <ul id="ululChem" class="dropdown-menu" runat="server">
                                                    <li id="liMRIn5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Chemist/ChemistList.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMRIn6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Chemist/Chemist_Terr_View.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliHos" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Hospital Details</a>
                                                <ul id="ululHos" class="dropdown-menu" runat="server">
                                                    <li id="liMRIn7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Hospital/HospitalList.aspx" onclick="ShowProgress();">Entry</a>
                                                    </li>
                                                    <li id="liMRIn8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Hospital/Hospital_Terr_View.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliUn" class="dropdown-submenu" runat="server" visible="false"><a class="dropdown-item dropdown-toggle" href="#">Unlisted Doctor Details</a>
                                                <ul id="ululUn" class="dropdown-menu" runat="server">
                                                    <li id="liMRIn9" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/UnListedDoctor/UnLstDoctorList.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMRIn10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/UnListedDoctor/Unlisteddr_Terr_View.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                            <li id="liMRIn11" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Doctor_Spec_List.aspx">Doctor - Speciality List</a></li>
                                            <li id="liMRIn12" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/HolidayList_MR.aspx" onclick="ShowProgress();">Holiday List</a></li>
                                            <li id="liMRIn14" visible="false" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Stockist_List_MR.aspx" onclick="ShowProgress();">Stockist List</a></li>
                                            <li id="liMRIn13" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDr_Approval_Pending.aspx" onclick="ShowProgress();">Lst.Dr - Approval Pending View</a></li>
                                            <li id="liSFC" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Distance_fixation_view.aspx" onclick="ShowProgress();">SFC View</a></li>
                                            <!--<li id="Li8" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Personal Details</a>
                                                <ul id="ul3" class="dropdown-menu" runat="server">
                                                    <li id="li9" runat="server"><a class="dropdown-item" href="../../MIS%20Reports/Employee_Application.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="li10" runat="server"><a class="dropdown-item" href="../../MIS%20Reports/Employee_Application_Zoom.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>-->
                                        </ul>
                                    </li>
                                    <li id="Act" class="nav-item dropdown" runat="server"><a class="nav-link dropdown-toggle" href="#" id="A2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Activities </a>
                                        <ul id="ul_Act" runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                            <li id="ulliTerr" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">
                                                <asp:Label ID="lblTerritory1" Text="Territory" runat="server"></asp:Label>
                                            </a>
                                                <ul id="ululTerr" class="dropdown-menu" runat="server">
                                                    <li id="liMrAct1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/RoutePlan.aspx" onclick="ShowProgress();">Normal</a></li>
                                                    <li id="liMrAct2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/RoutePlan_Catgwise.aspx">Classic (Categorywise)</a></li>
                                                    <li id="liMrAct3" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Reports/rptRoutePlan.aspx" onclick="ShowProgress();">View</a></li>
                                                    <li id="liMrAct4" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Reports/RoutePlan_Status.aspx" onclick="ShowProgress();">Status</a></li>
                                                </ul>
                                            </li>
                                            <li id="listp" visible="false" runat="server"><a href="../../../../MasterFiles/MR/STP_Creation.aspx" onclick="ShowProgress();">STP Creation</a></li>
                                            <li id="ulliTP" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Tour Plan</a>
                                                <ul id="ululTP" class="dropdown-menu" runat="server">
                                                    <li id="liMrAct5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/TourPlan.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liTpTerr" visible="false" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/TP_Entry_Terr.aspx" onclick="ShowProgress();">Entry New</a></li>
                                                    <li id="listptp" visible="false" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/TP_ENTRY_STP.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="litp_auto" visible="false" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/TP_New.aspx" onclick="ShowProgress();">Entry New</a></li>
                                                    <li id="liMrAct6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Report/TP_View_Report.aspx" onclick="ShowProgress();">View</a></li>
                                                    <li id="liMrAct7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Report/TP_Status_Report.aspx" onclick="ShowProgress();">Status</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliDCR" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">DCR</a>
                                                <ul id="ululDCR" class="dropdown-menu" runat="server">
                                                    <li id="liMrAct8" runat="server"><a class="dropdown-item" href="../../../../DCR/DCR_Entry.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMrAct9" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCR_View.aspx" onclick="ShowProgress();">View</a></li>
                                                    <li id="liMrAct10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCR_Status.aspx" onclick="ShowProgress();">Status</a></li>
                                                </ul>
                                            </li>
                                            <li id="LiMR4" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">RCPA</a>
                                                <ul id="ulMR1" class="dropdown-menu" runat="server">
                                                    <li id="liMR5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/RCPAList.aspx" onclick="ShowProgress();">Entry</a></li>
                                                </ul>
                                            </li>
                                            <li id="LiSPS28" runat="server" class="dropdown-submenu" visible="false"><a class="dropdown-item dropdown-toggle" href="#">Stockistwise Primary Sales</a>
                                                <ul id="ulSPS4" runat="server" class="dropdown-menu">
                                                    <li id="liSPS29" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Primary/Primary_Sales_Entry.aspx"
                                                        onclick="ShowProgress();">Entry</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliSS" class="dropdown-submenu" runat="server">
                                                <a class="dropdown-item dropdown-toggle" href="#">Secondary Sales</a>
                                                <ul id="ululSS" class="dropdown-menu" runat="server">
                                                    <li id="liMrAct11" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/SecSales/SecSalesEntry_New.aspx" onclick="ShowProgress();">Entry</a></li>
                                                </ul>
                                            </li>
                                            <li id="liMrAnthem" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MR/RptAutoExpense_RowWise_Anthem.aspx" onclick="ShowProgress();">Expense Statement</a></li>
                                            <li id="liMRRwExp" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MR/RptAutoExpense_RowWise.aspx" onclick="ShowProgress();">Expense Statement</a></li>
                                            <li id="liMRRwTxt" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MR/RptAutoExpense_RowWise_Textbox.aspx" onclick="ShowProgress();">Expense Statement</a></li>
                                            <li id="liINDSWFT" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MGR/RptAutoExpense_RowWise.aspx" onclick="ShowProgress();">Expense Statement</a></li>
                                            <li id="liMrAct13" runat="server" visible="true"><a class="dropdown-item" href="../../../../MasterFiles/Reports/RptAutoExpense_Approve_View.aspx"  onclick="ShowProgress();">Actual Expense View</a></li>
                                            <li id="LiMrCPBE13" class="dropdown-submenu" runat="server" ><a class="dropdown-item dropdown-toggle" href="#">Chemist-Productwise Business&raquo;</a>
                                                <ul id="ulMrCPBE3" class="dropdown-menu" runat="server">
                                                    <li id="liMrCPBE14" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/ChemistProductwiseBusinessEntry.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMrCPBE15" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/ChemistProductwiseBusinessView.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliBus" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Doctor Business Productwise</a>
                                                <ul id="ululBus" class="dropdown-menu" runat="server">
                                                    <%--<li id="liMrAct14" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorProductBusinessEntry.aspx" onclick="ShowProgress();">Entry</a></li>--%>

                                                    <li id="liA7" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Doctor_Business_Entry.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMrAct15" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorProductBusinessView.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                            <li id="LiMRDocBusVal" class="dropdown-submenu" runat="server" ><a class="dropdown-item dropdown-toggle" href="#">Doctor Business Valuewise</a>
                                                <ul id="ulMRDocBusVal" class="dropdown-menu" runat="server">
                                                    <li id="liMRDocBusVwEntry" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorBusinessValuewise_Entry.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMRDocBusVwView" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorBusinessValuewise_View.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliSamDes" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Sample Despatch </a>
                                                    <ul id="ululSamDes" class="dropdown-menu" runat="server"> 
                                                        <li id="liA12" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/sampleproduct.aspx"
                                                            onclick="ShowProgress();">View</a> </li>

                                                        <li id="liincalDes1" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Sample_Status_CalendarYearWise.aspx"
                                                            onclick="ShowProgress();">status(Financial YearWise)</a> </li>
                                                    </ul>
                                                </li>
                                            <li id="Li8inpsamp" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Input Despatch </a>
                                                    <ul id="ul3input" class="dropdown-menu" runat="server">
                                                        <li id="liA17" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/inputproduct.aspx"
                                                            onclick="ShowProgress();">View</a> </li>

                                                         <li id="liincalW" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/input_Status_CalendarYearWise.aspx"
                                                            onclick="ShowProgress();">status(Financial YearWise)</a> </li>
                                                    </ul>
                                                </li>
 
                                            <li id="ulliTarget" class="dropdown-submenu" runat="server" visible="false"><a class="dropdown-item dropdown-toggle" href="#">Target </a>
                                                <ul id="ululTarget" class="dropdown-menu" runat="server">
                                                    <li id="liA20" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/TargetFixationView.aspx" onclick="ShowProgress();">View</a></li>
                                                    <li id="TarvsSal" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/TargetVsSales.aspx"
                                                        onclick="ShowProgress();">Target Vs Sales</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliCRM" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Doctor Service CRM</a>
                                                <ul id="ululCRM" class="dropdown-menu" runat="server">
                                                    <li id="liMrCRM101" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_New.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMrCRM102" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_Status.aspx" onclick="ShowProgress();">Status</a></li>
                                                </ul>
                                            </li>
                                            <li id="liTaskMr" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Task_Management_New/Task2.aspx" onclick="ShowProgress();">Task Management</a></li>
                                            <li id="Li2book" class="dropdown-submenu" runat="server" visible="false"><a class="dropdown-item dropdown-toggle" href="#">Order Booking</a>
                                                <ul id="ul2book" class="dropdown-menu" runat="server">
                                                    <li id="li3book" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Order_booking.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="li2bookview" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Order_Booking_View.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                          <%--   <li id="Lis77" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Input Issued</a>
                                                    <ul id="uls66" class="dropdown-menu" runat="server">
                                                       
                                                       
                                                         <li id="Lif10" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Input_Issued_Details.aspx"
                                                               onclick="ShowProgress();">Input Periodical-Status</a></li>
                                                    </ul>
                                                </li>--%>
                              <li id="Liisedit" class="dropdown-submenu" runat="server" visible="false"><a class="dropdown-item dropdown-toggle" href="#">Sample/Input</a>
                                                    <ul id="ulsampleedit" class="dropdown-menu" runat="server">
                                                       
                                                       
                                                         <li id="Lisaedit" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Sample_Input_Modify_MRWise.aspx"
                                                               onclick="ShowProgress();">Sample- Edit</a></li>
                                                        <li id="Liiedit" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Input_Modify_MRWise.aspx"
                                                               onclick="ShowProgress();">Input- Edit</a></li>
                                                    </ul>
                                                </li>
                                        </ul>
                                    </li>
                                    <li id="Mis" class="nav-item dropdown" runat="server"><a class="nav-link dropdown-toggle" href="#" id="A3" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">MIS Reports</a>
                                        <ul id="ul_Mis" runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                            <li id="ulliDD" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Field Force Master</a>
                                                <ul id="ululDD" class="dropdown-menu" runat="server">
                                                    <li id="liMrMis1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DoctorList_Reportaspx.aspx" onclick="ShowProgress();">Doctor Summary Count View</a></li>
                                                    <li id="liMrMis4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/MR_Status_Report.aspx" onclick="ShowProgress();">Listed Doctor and Chemist Master</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliAnly" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Field Work Analysis</a>
                                                <ul id="ululAnly" class="dropdown-menu" runat="server">
                                                    <li id="liMrMis2" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/DCR_Analysis.aspx" onclick="ShowProgress();">Daily Work Report Summary</a></li>
                                                    <!--<li id="liMrMis3" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Month_Vertical_Wise.aspx" onclick="ShowProgress();">Visit Analysis</a></li>-->
                                                    <li id="liPob" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Analysis_Pob_count.aspx" onclick="ShowProgress();">Doctors/Chemists POB Report</a></li>
                                                       <li id="lir8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Analysis_Pob_count_Periodically.aspx"
                                                            onclick="ShowProgress();">Product wise Rx Report</a></li>

                                                    <li id="liMrMis5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/CallAverage.aspx" onclick="ShowProgress();">Drs Met/Visit and Call average Report</a></li>
                                                    <li id="liMrMis6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/frmWorkTypeStatusView.aspx" onclick="ShowProgress();">Work Type View Report</a></li>
                                                     <li id="liMrMis7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Missed_Call.aspx" onclick="ShowProgress();">
                                                                
                        Missed Call Report</a></li>
                                                     <li id="liMis20" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/ListeddrCamp_View.aspx"
                                                            onclick="ShowProgress();">Doctor Campaign View</a></li>
                                                    <li id="limgrcc20" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/ChemistCamp_View.aspx"
                                                            onclick="ShowProgress();">Chemist Campaign View</a></li>
                                                </ul>
                                            </li>

                                             <!--<li id="Li15" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Status</a>
                                                <ul id="ul4" class="dropdown-menu" runat="server">
                                                    
                                                     
                                                </ul>
                                            </li>-->
                                             <li id="Li19" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Review Slides(as on Date)</a>
                                                <ul id="ul6" class="dropdown-menu" runat="server">
                                                     <li id="li38" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/EffortVsSales_Wok_Analysis.aspx" onclick="ShowProgress();">Comprehensive Work Analysis</a> </li>
                                                      <li id="li1522" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Fully_Operative_Report.aspx"
                                                            onclick="ShowProgress();">Fully Operative</a> </li>
                                                    <li id="li16" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Primary_Sale_StockistWise_Product.aspx" onclick="ShowProgress();">Primary Sale</a> </li>
                                                    <li id="li17" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/samplestatus_new.aspx" onclick="ShowProgress();">Samples Status Report for a Month</a> </li>
                                                    <li id="li18" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/inputstatus_new.aspx" onclick="ShowProgress();">Inputs Status Report for a Month</a> </li>
                                                        <li id="li37" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SS_Inventory_Prodwise.aspx"
                                                            onclick="ShowProgress();">SS - Inventory Stockistwise</a> </li>
                                                        <li id="li41" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SS_Inventory_Prodwise_Prod.aspx"
                                                            onclick="ShowProgress();">SS - Inventory Productwise</a> </li>
                                                </ul>
                                            </li>
                                         
                                            <li id="ulliVis" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Visit Details</a>
                                                <ul id="ululVis" class="dropdown-menu" runat="server">
                                                    <li id="liMrMis8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Visit_Details_Cat_Cls_Spclty_LstDr_Wise.aspx" onclick="ShowProgress();">Category/Speciality/Listed Doctorwise</a></li>
                                                   <!-- <li id="liMrMis9" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/VisitDetail_Datewise.aspx" onclick="ShowProgress();">Datewise</a></li>-->
                                                    <li id="liMrMis10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Listeddr_View_Period.aspx" onclick="ShowProgress();">Doctor wise - Periodic Visit Details</a></li>
                                                    <!--<li id="liMrMis11" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Callfeedback_Pobwise.aspx" onclick="ShowProgress();">Call Feedbackwise</a></li>
                                                    <li id="liMrMis12" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_Fixation.aspx" onclick="ShowProgress();">Fixationwise(By Visit)</a></li>-->
                                                  <%--  <li id="liMrMis13" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_Basedon_ModeWise.aspx" onclick="ShowProgress();">Category/Specialitywise Coverage</a></li>--%>
                                                    <li id="liMrMis14" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_At_a_Glance.aspx" onclick="ShowProgress();">At a Glance</a></li>
                                                    <!--<li id="liMis_Chm_Ul" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_Chmst_UnLstDr.aspx" onclick="ShowProgress();">Chemist & UnListed Doctors</a></li>-->
                                                </ul>
                                            </li>
                                               
                                            <li id="ulliMrSV" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Sales Analysis</a>
                                                <ul id="ululSS2" class="dropdown-menu" runat="server">
                                                    <!--<li id="liMrMis15" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Reports/SecSalesReport.aspx" onclick="ShowProgress();">View</a></li>-->
                                                    <li id="li14" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx" onclick="ShowProgress();">Primary Sales Consolidate</a></li>
                                                    <li id="limrCon" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/SecSaleReport/SecSalesReport.aspx" onclick="ShowProgress();">Secondary Sales Consolidate</a></li>
                                                     <li id="lib1sst" runat="server" visible="true"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Primary_Stk_Bill_View.aspx" 
                                                        onclick="ShowProgress();">Primary Sales Stockistwise View</a></li>
                                                    <li id="lib2ppt" runat="server" visible="true"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Primary_Product_Bill_View.aspx"
                                                        onclick="ShowProgress();">Primary Sales Productwise View</a></li>

                                                    <li id="limrSR" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/SecSaleReport/SalesAnalysis.aspx" onclick="ShowProgress();">Mode wise</a></li>
                                                </ul>
                                            </li>
                                           <%-- <li id="ulliSamIn" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Sample / Input</a>
                                                <ul id="ululSamIn" class="dropdown-menu" runat="server">
                                                    <li id="liMis25" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/sampleproduct_details.aspx" onclick="ShowProgress();">Sample Issued - Fieldforce Wise</a></li>
                                                    <li id="liMis26" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/SampleGift_details.aspx" onclick="ShowProgress();">Input Issued - Fieldforce Wise</a></li>
                                                </ul>
                                            </li>--%>
                                       <%--   <li id="ulliCamp" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Campaign</a>
                                                    <ul id="ululCamp" class="dropdown-menu" runat="server">
                                                        <li id="liMis20" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/ListeddrCamp_View.aspx"
                                                            onclick="ShowProgress();">View</a></li>--%>
                                                   <%--     <li id="liMis21" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Campaign_View.aspx"
                                                            onclick="ShowProgress();">Status</a></li>--%>
                                                  <%--  </ul>
                                                </li>--%>
                                            <li id="ulliEx" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Exception</a>
                                                <ul id="ululEx" class="dropdown-menu" runat="server">
                                                    <li id="liMrMis17" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/TP_Deviation.aspx" onclick="ShowProgress();">Tour Plan</a>
                                                    </li>
                                                </ul>
                                            </li>
                                              <li id="ulliDump" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Dump </a>
                                                    <ul id="ululDump" class="dropdown-menu" runat="server">
                                                      
                                                        <li id="li23" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Campaign_Dump.aspx"
                                                            onclick="ShowProgress();">Campaign Visit - Drs</a></li>
                                                         <li id="lichemdump" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Chemists_Dump.aspx"
                                                            onclick="ShowProgress();">Chemist Dump</a></li>
                                                        <li id="sample_Dump" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/sampleproduct_details_exel.aspx"
                                                        onclick="ShowProgress();">Sample-Issued Excel Download</a></li>
                                                         <li id="li89" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Listeddr_Dump.aspx"
                                                            onclick="ShowProgress();">Listeddr</a></li>
                                                       <li id="input_Dump" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/SampleGift_details_exel.aspx"
                                                        onclick="ShowProgress();">Input-Issued Excel Download</a></li>
                                                          <li id="lidrBusPro" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/DrBusinessProductDump.aspx"
                                                            onclick="ShowProgress();">Doctor Product Business </a></li>   
                                                    </ul>
                                                </li>
                                             <li id="Likpikra" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">KRA/KPI </a>
                                                    <ul id="ulkpikra" class="dropdown-menu" runat="server">
                                                      
                                                          <li id="liKKRa" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Primary target achievement </a></li>
                                                         <li id="liKKRa1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SecondarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">SS Growth</a></li>
                                                         <li id="liKKRa2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">PCPM Growth </a></li>
                                                         <li id="liKKRa3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Focus brand PCPM </a></li>
                                                        <li id="liKKRa4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Focus brand PCPM (A Range) </a></li>
                                                        <li id="liKKRa5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">New prod.performance </a></li>
                                                        <li id="liKKRa6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">New prod.performance-Bifilac </a></li>
                                                         <li id="liKKRa7" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Fully_Operative_Report.aspx"
                                                            onclick="ShowProgress();">Dr Coverage</a> </li>
                                                        <li id="liKKRa8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Visit_Details_Cat_Cls_Spclty_LstDr_Wise.aspx"
                                                            onclick="ShowProgress();">Key Splty coverage(PED)</a></li>
                                                            
                                                    </ul>
                                                </li>
                                            <!--<li id="ulliBill" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Primary Bill</a>
                                                <ul id="ululBil" class="dropdown-menu" runat="server">
                                                   
                                                    <li id="liStck" runat="server" visible="false"><a class="dropdown-item" href="../../../MasterFiles/Stock_Product.aspx" onclick="ShowProgress();">Sale - Stockist Wise</a></li>
                                                </ul>
                                            </li>-->
                                        </ul>
                                    </li>
                                    <li id="Option" class="nav-item dropdown" runat="server"><a class="nav-link dropdown-toggle" href="#" id="A4" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Options</a>
                                        <ul id="ul_Opt" runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                            <li id="liMrOpt1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/ChangePassword.aspx" onclick="ShowProgress();">
                                                <asp:Label ID="lblChange" runat="server" Text="Change Password"></asp:Label></a></li>
                                            <li id="liMrOpt2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Mails/Mail_Head.aspx" onclick="ShowProgress();">Mail Box</a></li>
                                            <li id="liMrOpt3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/FileUpload_MR.aspx" onclick="ShowProgress();">Files Download</a></li>
                                            <li id="liMrOpt4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/UserManual_View.aspx" onclick="ShowProgress();">User Manual - View</a></li>
                                            <li id="ulliLve" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Leave</a>
                                                <ul id="ululLve" class="dropdown-menu" runat="server">
                                                    <li id="liMrOpt5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/LeaveForm.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMrOpt6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Leave_Status.aspx" onclick="ShowProgress();">Status</a></li>
                                                </ul>
                                            </li>
                                          <%--  <li id="li11" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Managerwise Core Doctor</a>
                                                <ul id="ul5" class="dropdown-menu" runat="server">
                                                    <li id="li12" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Mgrwise_Core_Doc_Map.aspx" onclick="ShowProgress();">Mapping</a></li>
                                                    <li id="li13" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Core_Drs.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>--%>
                                            <li id="Li34" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Payslip</a>
                                                <ul id="ul1" class="dropdown-menu" runat="server">
                                                    <li id="liOp44" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Payslip.aspx"
                                                        onclick="ShowProgress();">
                                                        <asp:Label ID="Label1" runat="server" Text="View"></asp:Label>
                                                    </a></li>
                                                    <li id="li35" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Payslip_Status.aspx"
                                                        onclick="ShowProgress();">Status</a></li>
                                                </ul>
                                            </li>

                                            <li id="liMrOpt7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Doctor-SubCategory-Map.aspx" onclick="ShowProgress();">Doctor - Campaign Map</a></li>
                                             <li id="liChemCam" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Chemist_Campaign/Chemist-SubCategory-Map.aspx" onclick="ShowProgress();">Chemist - Campaign Map</a></li>               

                                            <li id="liMrOpt8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Payslip.aspx" onclick="ShowProgress();">
                                                <asp:Label ID="lblpay" runat="server" Text="PaySlip View"></asp:Label>
                                            </a></li>
                                            <li id="lipayF" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Payslip_File_View.aspx" onclick="ShowProgress();">PaySlip Files View</a></li>
                                            <li id="LiMrSV008" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Status_View.aspx" onclick="ShowProgress();">Status View</a></li>
                                             <li id="Li888" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReport.aspx" onclick="ShowProgress();">Activity Report</a></li>
                                        </ul>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link logout" href="../../../../Index.aspx" onclick="ShowProgress();">Logout</a>
                                    </li>
                                </ul>
                            </div>
                            <%--<div style="width: 5%;">
                                <asp:Panel ID="pnlQueries" runat="server" HorizontalAlign="Right" CssClass="mar_right">
                                    <asp:LinkButton ID="lnkQueries" runat="server" Text="Queries" ForeColor="Red" Font-Size="Medium" Font-Bold="true"></asp:LinkButton>
                                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel"
                                        PopupControlID="Panel2" TargetControlID="lnkQueries" BackgroundCssClass="modalBackgroundNew">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopupNew" Style="display: none">
                                        <div class="header">
                                            Queries
                                            <asp:ImageButton ID="imgbtnclosegift" runat="server" ImageUrl="~/Images/Close.gif" ImageAlign="Right" />
                                        </div>
                                        <div class="body">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblprob" runat="server" SkinID="lblMand" Text="Queries File"></asp:Label>
                                                        <asp:DropDownList ID="ddlProb" runat="server" SkinID="ddlRequired">
                                                            <asp:ListItem Text="--Select the Problem--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="DCR" Value="0">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="TP" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Others" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtQuery" runat="server" Width="330px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Button ID="btnSend" runat="server" CssClass="savebutton" Text="Send" OnClick="btnSend_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="savebutton" Text="Cancel" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>--%>
                        </nav>
                    </div>
                </div>
            </div>
        </header>
        <asp:Panel ID="pnlHeader" runat="server" Width="100%" align="center">
            <table width="95%" cellpadding="0" cellspacing="0" align="center" border="0">
                <tr>
                    <td>
                        <table id="tblpanel" runat="server" width="100%" border="0">
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl"
                                        ForeColor="Black" Style="font-size: 13px; text-align: center;"
                                        Font-Bold="True"></asp:Label>
                                </td>
                                <td align="center" class="style5" style="width: 30%; padding-top: 20px; padding-bottom: 20px;">
                                    <asp:Label ID="lblHeading" runat="server" Style="text-transform: capitalize; font-size: 18px; text-align: center;" ForeColor="#0077FF">
                                    </asp:Label>
                                </td>
                                <td align="right" class="style3" style="width: 35%">
                                    <asp:Button ID="btnBack" runat="server" CssClass="savebutton" Text="Back" Visible="false" OnClick="btnBack_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <script src="../../../assets/js/jQuery.min.js"></script>
    <script src="../../../assets/js/popper.min.js"></script>
    <script src="../../../assets/js/bootstrap.min.js"></script>
    <script src="../../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../../assets/js/main.js"></script>
</div>
<div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="../Images/loader.gif" alt="" runat="server" />
</div>
<!--end wrapper-->
