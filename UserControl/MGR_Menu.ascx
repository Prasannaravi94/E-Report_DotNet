<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MGR_Menu.ascx.cs" Inherits="UserControl_MGR_Menu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<link href='<%=ResolveClientUrl("../css/MGR.css")%>' rel="stylesheet" type="text/css" />--%>
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

<!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->

<style type="text/css">
    .style1 {
        width: 46%;
    }

    .menu {
        margin-top: 0px;
    }

    .BUTTON {
    }

    .style3 {
        width: 99px;
    }

    .style4 {
        width: 47%;
    }

    .style5 {
        width: 45%;
    }

    .under {
        margin-top: 2px;
        text-decoration: underline;
    }

    .spc {
        padding-top: 5px;
        padding-bottom: 5px;
        padding-left: 5px;
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

    .mar_right {
        margin-right: 10px;
    }

    .modalBackgroundNew {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }

    .modalPopupNew {
        background-color: #FFFFFF;
        width: 350px;
        border: 3px solid #0DA9D0;
        padding: 0;
    }

        .modalPopupNew .header {
            background-color: #2FBDF1;
            height: 20px;
            color: White;
            line-height: 20px;
            text-align: center;
            font-weight: bold;
            font-size: 14px;
            font-family: Verdana;
        }

        .modalPopupNew .body {
            min-height: 120px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
            padding: 5px;
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

    .label1 {
        font-size: 15px !important;
        font-weight: 401 !important;
        color: black !important;
    }
</style>
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
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <%--<asp:Panel ID="pnldiv" runat="server" CssClass="spc">
        <table width="100%" border="0">
            <tr>
                <td class="style1">
                    <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize; font-size: 14px; text-align: left;"
                        ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana">
                    </asp:Label>
                </td>
                <td style="margin-left: 20%">
                    <asp:Label ID="LblDiv" runat="server" Style="text-transform: capitalize; font-size: 14px; text-align: left; margin-top: 0px"
                        ForeColor="BlueViolet" Font-Bold="True" Font-Names="Verdana"></asp:Label>
                </td>
                <td align="right" style="vertical-align: top">
                    <asp:Label ID="lbllogo" runat="server" Visible="false">
                        <img id="img" runat="server" alt="" /></asp:Label>
                </td>
                <td>
                    <asp:Panel ID="pnlQueries" runat="server" HorizontalAlign="Right" CssClass="mar_right">
                        <asp:LinkButton ID="lnkQueries" runat="server" Text="Queries" ForeColor="Red" Font-Size="Medium"
                            Font-Bold="true"></asp:LinkButton>
                        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel"
                            PopupControlID="Panel2" TargetControlID="lnkQueries" BackgroundCssClass="modalBackgroundNew">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopupNew" Style="display: none">
                            <div class="header">
                                Queries
                                <asp:ImageButton ID="imgbtnclosegift" runat="server" ImageUrl="~/Images/Close.gif"
                                    ImageAlign="Right" />
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
                                            <asp:Button ID="btnSend" runat="server" BackColor="LightBlue" Text="Send" OnClick="btnSend_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" BackColor="LightBlue" Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>--%>
        <header class="header-area clearfix">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <!---- Menu area start ---->
                        <nav class="navbar navbar-expand-md navbar-light p-0">
                            <a class="navbar-brand col-2" href="#">
                                <img id="img" runat="server" alt="" />
                            </a>
                            <div class="col-2">
                                <asp:Label ID="LblUser" CssClass="label1" ForeColor="Black" runat="server" Text="User">  </asp:Label>
                            </div>
                            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
                                <span class="navbar-toggler-icon"></span>
                            </button>
                            <div class="collapse navbar-collapse justify-content-end main-menu" id="collapsibleNavbar">
                                <ul id="menu" class="navbar-nav">
                                    <li class="nav-item"><a href="../../../../MGR_Home.aspx" class="nav-link active" onclick="ShowProgress();">Home</a></li>
                                    <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" href="#" id="A1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Information </a>
                                        <ul runat="server" id="ul_MGRInf" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                            <li id="liIn1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ProductRate.aspx" onclick="ShowProgress();">Product Information</a></li>
                                            <%--<li><a href="../../../../MasterFiles/MGR/Territory/Territory.aspx">Territory</a></li>
                                            <li><a href="../../../../MasterFiles/MGR/ListedDoctor/LstDoctorList.aspx">Listed Doctor Details</a></li>
                                            <li><a href="../../../../MasterFiles/MGR/Chemist/ChemistList.aspx">Chemist Details</a></li>
                                            <li><a href="../../../../MasterFiles/MGR/Hospital/HospitalList.aspx">Hospital Details</a></li>
                                            <li><a href="../../../../MasterFiles/MGR/UnListedDoctor/UnLstDoctorList.aspx">Unlisted Doctor</a></li>--%>
                                            <li id="liIn2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/User_List.aspx" onclick="ShowProgress();">Subordinate Detail</a></li>
                                            <li id="ulliMas" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Master View </a>
                                                <ul id="ululMas" runat="server" class="dropdown-menu">
                                                    <li id="liIn3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorView.aspx" onclick="ShowProgress();">Listed Doctor</a></li>

                                                    <li id="li21" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/CommDr_Pend_Mrwise.aspx"
                                                        onclick="ShowProgress();">Dr Approval - Pending Count</a></li>

                                                    <li id="liIn4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Chemist/Chemist_Terr_View.aspx" onclick="ShowProgress();">Chemist</a></li>
                                                    <li id="liIn5" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MR/Hospital/Hospital_Terr_View.aspx" onclick="ShowProgress();">Hospital</a></li>
                                                    <li id="liIn6" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MR/UnListedDoctor/Unlisteddr_Terr_View.aspx" onclick="ShowProgress();">Unlisted Doctor</a></li>


                                                </ul>
                                            </li>
                                            <li id="Li9" runat="server" visible="false" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Field Force Entries </a>
                                                <ul id="ul2" runat="server" class="dropdown-menu">
                                                    <%--                            <li id="liMRIn2" runat="server"><a href="../../../../MasterFiles/MR/Territory/Territory.aspx" onclick="ShowProgress();">
                        <asp:Label ID="lblTerritory" Text="Territory" runat="server"></asp:Label></a></li>--%>

                                                    <li id="liMRIn3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorList.aspx" onclick="ShowProgress();">Listed Doctor</a></li>

                                                </ul>
                                            </li>
                                            <li id="ulliApp" runat="server" visible="false" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Approvals </a>
                                                <ul id="ululApp" runat="server" class="dropdown-menu">
                                                    <li id="liIn7" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MGR/ListedDR_Add_Approve.aspx" onclick="ShowProgress();">Listed Dr Addition</a></li>
                                                    <li id="liIn8" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MGR/ListedDR_DeActivate_Approve.aspx" onclick="ShowProgress();">Listed Dr DeActivation</a></li>
                                                    <li id="liIn9" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MGR/TP_Approve.aspx" onclick="ShowProgress();">TP Approval</a></li>
                                                    <%--<li id="liIn10" runat="server"><a href="../../../../MasterFiles/MGR/DCR_Approval.aspx" onclick="ShowProgress();">DCR Approval</a></li>--%>
                                                    <%--<li id="liIn11" runat="server"><a href="../../../../MasterFiles/MGR/Leave_Approval.aspx" onclick="ShowProgress();">Leave Approval</a></li>--%>
                                                    <li id="liIn12" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MGR/SecSale_Approval.aspx" onclick="ShowProgress();">Secondary Sales Approval</a></li>
                                                    <li id="liIn101" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_Approve_MGR.aspx" onclick="ShowProgress();">Doctor Service CRM Approval</a></li>
                                                    <li id="liIn102" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_Status.aspx" onclick="ShowProgress();">Doctor Service CRM Status</a></li>


                                                </ul>
                                            </li>
                                            <li id="liIn13" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Doctor_Spec_List.aspx" onclick="ShowProgress();">Doctor - Speciality List</a></li>
                                            <li id="liIn14" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/HolidayList_MGR.aspx" onclick="ShowProgress();">Holiday List</a></li>
                                            <li id="liIn15" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/Listeddr_App_Status.aspx" onclick="ShowProgress();">Listed Doctor Approval Status</a></li>
                                            <li id="liIn16" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/Convert_Unlistto_Listeddr.aspx" onclick="ShowProgress();">Unlisted Dr Convert to Listed Dr</a></li>
                                            <li id="liSFCVw1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Distance_fixation_view.aspx" onclick="ShowProgress();">SFC View</a></li>
                                           <%-- <li id="ulliMGRStock" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Stockist Details </a>
                                                <ul id="ululMGRStock" runat="server" class="dropdown-menu">
                                                    <li id="liSSMGRRpt101" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SecSale_Report_Free.aspx"
                                                        onclick="ShowProgress();">At a Glance</a></li>

                                                </ul>
                                            </li>--%>
                                            <li id="Li1" runat="server" visible="false" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Personal Details </a>
                                                <ul id="ul3" runat="server" class="dropdown-menu">
                                                    <li id="li5" runat="server" visible="false"><a class="dropdown-item" href="../../MIS%20Reports/Employee_Application.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="li6" runat="server" visible="false"><a class="dropdown-item" href="../../MIS%20Reports/Employee_Application_Zoom.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </li>
                                    <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" href="#" id="A2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Activities </a>
                                        <ul runat="server" id="ul_Act" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                            <li id="ulliRo" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">
                                                <asp:Label ID="lblRoute" Text="Route Plan" runat="server"></asp:Label>
                                            </a>
                                                <ul id="ululRo" runat="server" class="dropdown-menu">

                                                    <li id="liMgrAct1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/rptRoutePlan.aspx" onclick="ShowProgress();">View</a></li>
                                                    <li id="liMgrAct2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/RoutePlan_Status.aspx" onclick="ShowProgress();">Status</a></li>

                                                </ul>
                                            </li>
                                            <li id="ulliTP" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Tour Plan </a>
                                                <ul id="ululTP" runat="server" class="dropdown-menu">
                                                    <li id="liMgrAct3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Report/TP_Consolidated_View.aspx" onclick="ShowProgress();">Consolidated View</a></li>
                                                    <li id="liMgrAct4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/TourPlan_Calen.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <%--<li id="liMgrAct5" runat="server"><a href="../../../../MasterFiles/MGR/TP_Calendar_Edit.aspx" onclick="ShowProgress();">Edit</a></li>--%>
                                                    <li id="liMgrAct6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Report/TP_View_Report.aspx" onclick="ShowProgress();">View</a></li>
                                                    <li id="liMgrAct7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Report/TP_Status_Report.aspx" onclick="ShowProgress();">Status</a></li>
                                                    <%--     <li id="liccp" runat="server"><a href="../../../../MIS Reports/TPCCP_View.aspx"
                                onclick="ShowProgress();">CCP View</a></li>--%>


                                                    <%-- <li><a href="../../../../MasterFiles/MGR/TP_Approval.aspx">Calendar View - Approval</a></li>--%>
                                                </ul>
                                            </li>
                                            <li id="ulliDCR" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">DCR </a>
                                                <ul id="ululDCR" runat="server" class="dropdown-menu">
                                                    <li id="liMgrAct8" runat="server" ><a class="dropdown-item" href="../../../../DCR/DCR_Entry.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMgrAct9" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCR_View.aspx" onclick="ShowProgress();">View</a></li>
                                                    <li id="liMgrAct10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCR_Status.aspx" onclick="ShowProgress();">Status</a></li>
                                                    <li id="li4" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCRCount/frmDcrCount.aspx" onclick="ShowProgress();">Approval Status</a></li>
                                                    <li id="liModCnt" runat="server" visible="true"><a class="dropdown-item" href="../../../../MasterFiles/Options/DCR_Entry_Mode.aspx"
                                                            onclick="ShowProgress();">Count - ModeWise</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliTarget" runat="server" visible="false" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Target  </a>
                                                <ul id="ululTarget" runat="server" class="dropdown-menu">
                                                    <li id="liA20" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/TargetFixationView.aspx"
                                                        onclick="ShowProgress();">View</a></li>
                                                    <li id="TarvsSal" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/TargetVsSales.aspx"
                                                        onclick="ShowProgress();">Target Vs Sales</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliExpMgr" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Expense </a>
                                                <ul id="ululExpMgr" runat="server" class="dropdown-menu">
                                                    <li id="liaimil123" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MGR/RptAutoExpense_Mgr_New.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMGRRWTxt" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MGR/RptAutoExpense_MGR_RowTextbox.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liSemiAuto" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MGR/RptAutoExpense_MGR_RM_Script.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="lioths" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/MGR/RptAutoExpense_MGR_old.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="li2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/RptAutoExpense_Approve_View.aspx" onclick="ShowProgress();">Actual Expense View</a></li>
                                                    <%-- <li id="li3" runat="server"><a href="../../../../MasterFiles/Reports/RptAutoExpense_Approve_Mgr.aspx" onclick="ShowProgress();">
                           Approval</a></li>--%>
                                                    <li id="li3" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Reports/rptAutoexpense_Approve.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                            <%--  PS Pasted on 09/10/2019 at 1.43 p.m  --%>
                                            <li id="liTaskMgr" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Task_Management_New/Task2.aspx"
                                                onclick="ShowProgress();">Task Management</a></li>
                                           <%-- <li id="ulliDocBus" class="dropdown-submenu" runat="server"  visible="false"><a class="dropdown-item dropdown-toggle" href="#">Doctor Business Productwise </a>
                                                    <ul id="ululDocBus" class="dropdown-menu" runat="server">
                                                       
                                                        <li id="liA7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorProductBusinessEntry.aspx"
                                                            onclick="ShowProgress();">Entry</a></li>
                                                         <li id="liA8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorProductBusinessView.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                        
                                                    </ul>
                                                </li>
                                             <li id="LiDrPotienal" class="dropdown-submenu" runat="server" visible="false"><a class="dropdown-item dropdown-toggle" href="#">Doctor Potential Productwise </a>
                                                    <ul id="ul6" class="dropdown-menu" runat="server">
                                                        
                                                        <li id="li10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorPotientalBusinessEntry.aspx"
                                                            onclick="ShowProgress();">Entry</a></li>
                                                        
                                                        <li id="li14" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorPotentialBusinessView.aspx"
                                                            onclick="ShowProgress();">View</a></li>
                                                        
                                                    </ul>
                                                </li>--%>
                                               <li id="Lis77" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Input Issued</a>
                                                    <ul id="uls66" class="dropdown-menu" runat="server">
                                                        <li id="lis200" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/gift_issued.aspx"
                                                            onclick="ShowProgress();">
                                                            <asp:Label ID="Label3" runat="server" Text="Field Force Wise"></asp:Label>
                                                        </a></li>
                                                       
                                                         <li id="Lif10" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Input_Issued_Details.aspx"
                                                               onclick="ShowProgress();">Input Periodical-Status</a></li>
                                                    </ul>
                                                </li>
                                             <li id="ulliBus" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Doctor Business Productwise</a>
                                                <ul id="ululBus" class="dropdown-menu" runat="server">
                                                    <li id="liA7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Doctor_Business_Entry.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMrAct15" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/DoctorProductBusinessView.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                            <li id="ulliSamDes" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Sample Despatch </a>
                                                    <ul id="ululSamDes" class="dropdown-menu" runat="server"> 
                                                        <li id="liA12" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/sampleproduct.aspx"
                                                            onclick="ShowProgress();">View</a> </li>
                                                           <li id="liincalDes" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Sample_Status_CalendarYearWise.aspx"
                                                            onclick="ShowProgress();">status(Financial YearWise)</a> </li>

                                                    </ul>
                                                </li>
                                            <li id="ulliInDes" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Input Despatch </a>
                                                    <ul id="ululInDes" class="dropdown-menu" runat="server">
                                                        <li id="liA17" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/inputproduct.aspx"
                                                            onclick="ShowProgress();">View</a> </li>
                                                           <li id="liincal1" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/input_Status_CalendarYearWise.aspx"
                                                            onclick="ShowProgress();">status(Financial YearWise)</a> </li>
                                                    </ul>
                                                </li>
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
                                 
                                    <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" href="#" id="A3" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">MIS Reports </a>
                                        <ul runat="server" id="ul_Mis" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                            <%--              <li><a href="../MasterFiles/MR_DCRAnalysis.aspx">DCR Analysis</a></li>--%>
                                            <li id="ulliDoc" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Field Force Master </a>
                                                <ul id="ululDoc" runat="server" class="dropdown-menu">
                                                    <li id="liMgrMis1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DoctorList_Reportaspx.aspx" onclick="ShowProgress();">Doctors Summary Count View</a></li>
                                                    <li id="liMgrMis2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/MR_Status_Report.aspx" onclick="ShowProgress();">Doctors and Chemists Master</a></li>
                                                </ul>
                                            </li>
                                            
                                            <li id="ulliAnl" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Field Work Analysis </a>
                                                <ul id="ululAnl" runat="server" class="dropdown-menu">
                                                    <li id="liMgrMis15" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/DCR_Analysis.aspx" onclick="ShowProgress();">Daily Work Report Summary</a>
                                                    </li>
                                                    <li id="liMgrMis3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/CallAverage.aspx" onclick="ShowProgress();">Drs Met/Visit and Call Average Report</a></li>
                                                    <li id="liMis8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Analysis_Pob_count.aspx"
                                                            onclick="ShowProgress();">Doctors/Chemists POB Report</a></li>

                                                        <li id="li8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Analysis_Pob_count_Periodically.aspx"
                                                            onclick="ShowProgress();">Product wise Rx Report</a></li>
                                            <li id="liMgrMis4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/frmWorkTypeStatusView.aspx" onclick="ShowProgress();">Work Type View Report </a></li>
                                            <li id="liMgrMis5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Missed_Call.aspx" onclick="ShowProgress();">Missed Call Report</a></li>
                                                    <li id="liMgrMis16" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Mgr_Coverage.aspx"
                                                        onclick="ShowProgress();">Manager - HQ Wise Visit Coverage Analysis</a></li>
                                                    <li id="liMgrMis17" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Coverage_Analysis.aspx"
                                                        onclick="ShowProgress();">Coverage Analysis 1</a></li>
                                                    <li id="liMgrMis18" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Coverage_Analysis_2.aspx" onclick="ShowProgress();">Coverage Analysis 2</a></li>
                                                    <li id="liMgrMis19" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/JointWk_Analysis.aspx"
                                                        onclick="ShowProgress();">Joint Work Analysis</a></li>
                                                    <li id="liMgrMis20" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Month_Vertical_Wise.aspx" onclick="ShowProgress();">Visit Analysis</a></li>          
                                                      <li id="lidcov" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Doctors_Coverage_Detail.aspx"
                                                            onclick="ShowProgress();">Doctors Coverage Detail</a></li> 
                                                     <li id="li14" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Discipline_Coverage_Report.aspx"
                                                            onclick="ShowProgress();">Discipline Coverage Report</a></li>                                       
                                                </ul>
                                            </li>
                                               <li id="ulliRprt" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Report </a>
                                                    <ul id="ululRprt" class="dropdown-menu" runat="server">
                                    <li id="lis10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SecondarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Secondary Sales Consolidated </a></li>
                                                        </ul>
                                        </li>
                                             <li id="Li19" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Review Slides(as on Date)</a>
                                                <ul id="ul7" class="dropdown-menu" runat="server">
                                                     <li id="li38" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/EffortVsSales_Wok_Analysis.aspx" onclick="ShowProgress();">Comprehensive Work Analysis</a> </li>
                                                     <li id="li15" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Fully_Operative_Report.aspx"
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
                                            <li id="ulliVisit" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Visit Details </a>
                                                <ul id="ululVisit" runat="server" class="dropdown-menu">
                                               
                                                    <li id="liMgrMis6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Visit_Details_Cat_Cls_Spclty_LstDr_Wise.aspx" onclick="ShowProgress();">Category/Speciality Listed Doctorwise Report</a></li>
                                                    <li id="liMgrMis7" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/VisitDetail_Datewise.aspx" onclick="ShowProgress();">Datewise</a></li>
                                                    <li id="liMgrMis8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Listeddr_View_Period.aspx"
                                                        onclick="ShowProgress();">Doctorwise Periodic Visit Details</a></li>
                                                    <li id="liMgrMis9" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Callfeedback_Pobwise.aspx" onclick="ShowProgress();">Call Feedbackwise</a></li>
                                                    <li id="liMgrMis10" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_Fixation.aspx" onclick="ShowProgress();">Fixationwise(By Visit)</a></li>
                                                    <li id="liMgrMis11" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_Basedon_ModeWise.aspx" onclick="ShowProgress();">Category and Specialitywise Coverage Report Summary</a></li>
                                                    <li id="liMgrMis12" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_At_a_Glance.aspx" onclick="ShowProgress();">At a Glance</a></li>
                                                    <li id="liMis_Chm_Ul" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Details_Chmst_UnLstDr.aspx" onclick="ShowProgress();">Chemist & UnListed Doctors</a></li>                  
                                                </ul>
                                            </li>
                                             <li id="ullicore" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">MVD </a>
                                                    <ul id="ul8" class="dropdown-menu" runat="server">
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
                                            <li id="ulliMgrSS" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Sales Analysis </a>
                                                <ul id="ululSS" runat="server" class="dropdown-menu">
                                                    <li id="liMgrMis13" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Reports/SecSalesReport.aspx" onclick="ShowProgress();">View</a></li>
                                                    <li id="liMgrMis14" runat="server" visible="false"><a href="../../../../MasterFiles/Reports/Sec_Sales_Stockiest.aspx" onclick="ShowProgress();">Entry Status</a></li>
                                                     <li id="li47" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Primary Sales Consolidated </a></li>
                                                    <li id="limgrCon" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/SecSaleReport/SecSalesReport.aspx"
                                                        onclick="ShowProgress();">Secondary Sales Consolidate</a></li>
                                                    <li id="limgrSR" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/SecSaleReport/SalesAnalysis.aspx" onclick="ShowProgress();">Mode wise</a></li>
                                                   <li id="lib1sst" runat="server" visible="true"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Primary_Stk_Bill_View.aspx"
                                                        onclick="ShowProgress();">Primary Sales Stockistswise</a></li>
                                                    <li id="lib2ppt" runat="server" visible="true"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Primary_Product_Bill_View.aspx"
                                                        onclick="ShowProgress();">Primary Sales Productwise</a></li>
                                                </ul>
                                            </li>
                                           

                                            <li id="ulliSamIn" runat="server" visible="false" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Samples / Inputs </a>
                                                <ul id="ululSamIn" runat="server" class="dropdown-menu">
                                                    <li id="liMis25" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/sampleproduct_details.aspx"
                                                        onclick="ShowProgress();">Sample Issued - Doctorwise</a></li>
                                                    <li id="liMis26" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/SampleGift_details.aspx"
                                                        onclick="ShowProgress();">Input Issued - Doctorwise</a></li>

                                                </ul>
                                            </li>


                                            <li id="ulliProd" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Product Exposure </a>
                                                <ul id="ululProd" runat="server" class="dropdown-menu">
                                                    <li id="liMgrMis21" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Product_Exp_Detail.aspx" onclick="ShowProgress();">Analysis</a></li>
                                                    <li id="liMgrMis22" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Product_Exp_specat.aspx" onclick="ShowProgress();">Speciality/Category Wise</a></li>
                                                    <li id="liMgrMis23" runat="server" visible="false"><a class="dropdown-item" href="../../../../MIS Reports/Territory_Format.aspx" onclick="ShowProgress();">ListedDr - Productwise Visit</a></li>
                                                    <li id="liprdSKU" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/ProductExpBrandWiseAnalysis.aspx"
                                                        onclick="showprogress();">Product Exposure [SKU]</a></li>
                                                </ul>
                                            </li>

                                            <li id="ulliCamp" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Campaign </a>
                                                <ul id="ululCamp" runat="server" class="dropdown-menu">
                                                    <li id="liMgrMis24" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/ListeddrCamp_View.aspx" onclick="ShowProgress();">View</a></li>
                                                    <li id="limgrcc20" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/ChemistCamp_View.aspx" onclick="ShowProgress();">Chemist Campaign View</a></li>
                                                    <li id="liMgrMis25" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Campaign_View.aspx" onclick="ShowProgress();">Status</a></li>
                                                </ul>
                                            </li>


                                            <li id="ullitpExp" runat="server" visible="false" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Exception </a>
                                                <ul id="ulultpExp" runat="server" class="dropdown-menu">
                                                    <li id="liMgrMis26" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/TP_Deviation.aspx" onclick="ShowProgress();">Tour Plan -Baselevel</a> </li>
                                                    <li id="tpmgr" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/TP_DeviationMGR.aspx" onclick="ShowProgress();">Tour Plan -Managers</a>
                                                    </li>
                                                </ul>
                                            </li>

                                            <li id="ulliBill" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Primary Bill </a>
                                                <ul id="ululBil" runat="server" class="dropdown-menu">
                                                    
                                                    <li id="liStck" runat="server" visible="false"><a class="dropdown-item" href="../../../MasterFiles/Stock_Product.aspx"
                                                        onclick="ShowProgress();">Sale - Stockist Wise</a></li>
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
                                                       <li id="input_Dump" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/SampleGift_details_exel.aspx"
                                                        onclick="ShowProgress();">Input-Issued Excel Download</a></li>
                                                                  <li id="li22" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Listeddr_Dump.aspx"
                                                            onclick="ShowProgress();">Listeddr</a></li>
                                                         <li id="lidrBusPro" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/DrBusinessProductDump.aspx"
                                                            onclick="ShowProgress();">Doctor Product Business </a></li>
                                                        <li id="li45" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/ListeddrPeriod_ALL_Dump.aspx"
                                                            onclick="ShowProgress();">Visit - Drs</a></li>
                                                    </ul>
                                                </li>

                                            <li id="Likpikra" class="dropdown-submenu" runat="server" visible="false"><a class="dropdown-item dropdown-toggle" href="#">KRA/KPI </a>
                                                    <ul id="ulkpikra" class="dropdown-menu" runat="server">
                                                      
                                                          <li id="liKKRa" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Regional Primary target achievement </a></li>
                                                         <li id="liKKRa1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SecondarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Secondary Sales growth </a></li>
                                                         <li id="liKKRa2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">PCPM Growth</a></li>
                                                         <li id="liKKRa3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Focus brand PCPM (B Range)</a></li>
                                                        <li id="liKKRa4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Focus brand PCPM (A Range) </a></li>
                                                        <li id="liKKRa5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">New prod.performance PCPM </a></li>
                                                        <li id="liKKRa6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">New prod.performance-Bifilac </a></li>
                                                         <li id="liKKRa7" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Fully_Operative_Report.aspx"
                                                            onclick="ShowProgress();">Dr Coverage of the MPO's</a> </li>
                                                       <li id="li10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Core_drs_Speclty_wise.aspx"
                                                            onclick="ShowProgress();">MVD Coverage - Self</a></li>
                                                            
                                                    </ul>
                                                </li>
                                               <li id="Likpikradbm" class="dropdown-submenu" runat="server" visible="false"><a class="dropdown-item dropdown-toggle" href="#">KRA/KPI </a>
                                                    <ul id="ulkpikradbm" class="dropdown-menu" runat="server">
                                                      
                                                          <li id="liKKRadbm" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Divisonal Primary target achievement. </a></li>
                                                         
                                                        <li id="liKKRadbm1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">PCPM Growth</a></li>
                                                         <li id="liKKRadbm2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Focus brand PCPM (B Range)</a></li>
                                                       
                                                         <li id="liKKRadbm3" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Fully_Operative_Report.aspx"
                                                            onclick="ShowProgress();">Dr Coverage of the Team</a> </li>
                                                       <li id="liKKRadbm4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Core_drs_Speclty_wise.aspx"
                                                            onclick="ShowProgress();">MVD Coverage - self</a></li>
                                                        <li id="liKKRadbm5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Core_drs_Speclty_wise.aspx"
                                                            onclick="ShowProgress();">MVD Coverage of the RBM</a></li>
                                                         <li id="liKKRadbm6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SS_Inventory_Prodwise_Prod.aspx"
                                                            onclick="ShowProgress();">Closing Stock Inventory Days</a> </li>
                                                         <li id="liKKRadbm7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/EffortVsSales_Wok_Analysis.aspx"
                                                            onclick="ShowProgress();">Free Goods Usage</a> </li>
                                                            
                                                    </ul>
                                                </li>
                    <li id="Likpikrasm" class="dropdown-submenu" runat="server" visible="false"><a class="dropdown-item dropdown-toggle" href="#">KRA/KPI </a>
                                                    <ul id="ulkpikrasm" class="dropdown-menu" runat="server">                                                      
                <li id="liKKRasm" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Zonal Primary Target Achievement </a></li>
               <li id="liKKRasm1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SecondarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Zonal Secondary Target Achievement </a></li>
               <li id="liKKRasm2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">PCPM Growth </a></li>
              <li id="liKKRasm3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Focus Brand PCPM Growth(Bififlac)</a></li>
            <li id="liKKRasm4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">Focus Brand PCPM Growth(Ambrolite)</a></li>
            <li id="liKKRasm5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">New Product Performance PCPM</a></li>
            <li id="liKKRasm6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx"
                                                            onclick="ShowProgress();">New Product Performance PCPM(Bififlac)</a></li>
             <li id="liKKRasm7" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Fully_Operative_Report.aspx"
                                                            onclick="ShowProgress();">Dr Coverage of the Team </a></li>
             <li id="liKKRasm8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Core_drs_Speclty_wise.aspx"
                                                            onclick="ShowProgress();">MVD Coverage - Self </a></li>
             <li id="liKKRasm9" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Core_drs_Speclty_wise.aspx"
                                                            onclick="ShowProgress();">MVD Coverage of the DBM's</a></li>
            <li id="liKKRasm10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Core_drs_Speclty_wise.aspx"
                                                            onclick="ShowProgress();">MVD Coverage of the RBM's</a></li>
           <li id="liKKRasm11" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/SS_Inventory_Prodwise_Prod.aspx"
                                                            onclick="ShowProgress();">Closing Stock Inventory Days</a></li>
           <li id="liKKRasm12" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/EffortVsSales_Wok_Analysis.aspx"
                                                            onclick="ShowProgress();">Free Goods Usage</a></li>
           <li id="liKKRasm13" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/EffortVsSales_Wok_Analysis.aspx"
                                                            onclick="ShowProgress();">CN Value</a></li>
           <li id="liKKRasm14" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/EffortVsSales_Wok_Analysis.aspx"
                                                            onclick="ShowProgress();">People Retention</a></li>
</ul>
</li>
                                            
                                            <%-- <li id="Target" runat="server"><a href="#">Target Fixation </a>
                        <ul>
               
                            <li id="liA20" runat="server"><a href="../../../../MasterFiles/ActivityReports/TargetFixationView.aspx" onclick="ShowProgress();">
                                View</a></li>
                        </ul>
                    </li>--%>
                                        </ul>
                                    </li>
                                    <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" href="#" id="A4" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Options </a>
                                        <ul runat="server" id="ul_Opt" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                            <li id="liMgrOpt1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/ChangePassword.aspx" onclick="ShowProgress();">Change Password</a></li>
                                            <li id="liMgrOpt2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Mails/Mail_Head.aspx" onclick="ShowProgress();">Mail Box</a></li>
                                            <li id="liMgrOpt3" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/FileUpload_MR.aspx">Files Download</a></li>
                                            <li id="liMgrOpt4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/UserManual_View.aspx" onclick="ShowProgress();">User Manual - View</a></li>
                                            <li id="liMgrOpt5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/Vacant_MR_Login.aspx">Vacant - MR Login Access</a></li>
                                            <li id="ulliLeave" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Leave </a>
                                                <ul id="ululLeave" runat="server" class="dropdown-menu">
                                                    <li id="liMgrOpt6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/Leave_Form_Mgr.aspx" onclick="ShowProgress();">Entry</a></li>
                                                    <li id="liMgrOpt7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MR/Leave_Status.aspx" onclick="ShowProgress();">Status</a></li>

                                                </ul>
                                            </li>

                                            <li id="li8tpdev" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/TP_Deviation_Release.aspx" onclick="ShowProgress();">TP Deviation -Release</a></li>
                                            <li id="liMgrOpt8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Payslip.aspx" onclick="ShowProgress();">
                                                <asp:Label ID="lblpay" runat="server" Text="PaySlip View"></asp:Label>
                                            </a></li>
                                            <li id="lipayF" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Payslip_File_View.aspx" onclick="ShowProgress();">PaySlip Files View</a></li>
                                            <li id="Li7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Status_View.aspx" onclick="ShowProgress();">Status View</a></li>
                                            <li id="Lispecial" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Special Format </a>
                                                <ul id="ul1" runat="server" class="dropdown-menu">
                                                    <li id="lijoin" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/Joinee_Kit.aspx" onclick="ShowProgress();">New Joinee Kit Requisition</a></li>
                                                    <li id="lirecomm" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/Recommendation_For_Confirmation.aspx" onclick="ShowProgress();">Recommendation For Confirmation</a></li>
                                                    <li id="licar" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/MGR/Carservice.aspx" onclick="ShowProgress();">Car Service</a></li>
                                                    <li id="liAss_MR" runat="server"><a class="dropdown-item" href="../../../../Assesment_MR.aspx" onclick="ShowProgress();">Interview Assessment - MR</a></li>
                                                    <li id="liAss_MGR" runat="server"><a class="dropdown-item" href="../../../../Assesment_MGR.aspx" onclick="ShowProgress();">Interview Assessment - MGR</a></li>
                                                </ul>
                                            </li>
                                            <%--  <li id="liOp13" runat="server"><a href="../../../../MasterFiles/Options/Mgrwise_Core_Doc_Map.aspx"
                                onclick="ShowProgress();">Managerwise Core Doctor Map</a></li>--%>
                                            <%--    
                        <li id="liOp43" runat="server"><a href="../../../../MasterFiles/Options/Delayed_Release.aspx"
                        onclick="ShowProgress();">Release - Missing Dates / Delay </a></li>   --%>
                                            <li id="li11" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Managerwise Core Doctor</a>
                                                <ul id="ul5" class="dropdown-menu" runat="server">
                                                    <li id="li12" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Mgrwise_Core_Doc_Map.aspx" onclick="ShowProgress();">Mapping</a></li>
                                                    <li id="li13" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Core_Drs.aspx" onclick="ShowProgress();">View</a></li>
                                                </ul>
                                            </li>
                                            <li id="Li34" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Payslip</a>
                                                <ul id="ul4" class="dropdown-menu" runat="server">
                                                    <li id="liOp44" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Payslip.aspx"
                                                        onclick="ShowProgress();">
                                                        <asp:Label ID="Label1" runat="server" Text="View"></asp:Label>
                                                    </a></li>
                                                    <li id="li35" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Options/Payslip_Status.aspx"
                                                        onclick="ShowProgress();">Status</a></li>
                                                </ul>
                                            </li>
                                            <li id="Li20" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReport.aspx" onclick="ShowProgress();">Activity Report</a></li>
                                        </ul>
                                    </li>
                                    <li class="nav-item"><a class="nav-link logout" href="../../../../Index.aspx" onclick="ShowProgress();">Logout</a></li>
                                </ul>
                            </div>
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
        <%--<asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" BorderColor="DarkSlateGray" 
            Color="DarkSlateGray"       Radius="2" TargetControlID="pnlHeader">
        </asp:RoundedCornersExtender>--%>
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
</div>
<!--end wrapper-->
