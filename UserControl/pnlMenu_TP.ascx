<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pnlMenu_TP.ascx.cs" Inherits="UserControl_pnlMenu_TP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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

<!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->

<style type="text/css">
    .style1 {
        width: 381px;
        margin-top: 0%;
    }

    .BUTTON:hover {
        background-color: #336277;
        -webkit-border-radius: 6px;
        -moz-border-radius: 6px;
        border-radius: 6px;
        color: White;
    }

    .under {
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

    .spc {
        padding-top: 5px;
        padding-bottom: 5px;
        padding-left: 5px;
    }

    .flo {
        float: right;
        margin-top: 0px;
    }

    jQuery(document).ready(function(){ 


    if (jQuery(window).width() < 900) { 


        jQuery(".AdminMenuStyle").css("display", "none"); 


    }   


}); jQuery(window).resize(function () { 


        if (jQuery(window).width() < 900) { 


            jQuery(".top-menu").css("display", "none"); 


        } 


});
</style>
<script type="text/javascript">

    $(document).ready(function () {


        window.$zopim || (function (d, s) {
            var z = $zopim = function (c) {
                z._.push(c)
            }, $ = z.s =
		d.createElement(s), e = d.getElementsByTagName(s)[0]; z.set = function (o) {
		    z.set.
		_.push(o)
		}; z._ = []; z.set._ = []; $.async = !0; $.setAttribute('charset', 'utf-8');
            $.src = 'https://v2.zopim.com/?5DURYZFDFeE3izx6HWO5i5IteKcQeGaU'; z.t = +new Date; $.
		type = 'text/javascript'; e.parentNode.insertBefore($, e)
        })(document, 'script');

    });
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
<asp:LinkButton ID="lnkFake" runat="server" />
<asp:ModalPopupExtender ID="mpeTimeout" BehaviorID="mpeTimeout" runat="server" PopupControlID="pnlPopup"
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
            window.location.replace("http://demo.sansfe.info/");
        }, timeout);
    };
    function ResetSession() {
        //Redirect to refresh Session.
        window.location = window.location.href;
    }
    function ExSession() {
        window.location.replace("http://demo.sansfe.info/");
    }
</script>
<div id="wrapper">

    <%--  <asp:Panel ID="pnldiv" runat="server" CssClass="spc">
        <table width="100%" border="0">
            <tr>
                <td class="style1" valign="top">
                   
                                <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                                    font-size: 14px; text-align: left; margin-top: 0px" ForeColor="Maroon" Font-Bold="True"
                                    Font-Names="Verdana">
                                </asp:Label>
                         
                </td>
                <td align="right" valign="top">
                    <table >
                        <tr>
                            <td valign="top">
                              
                                    <a id="A2" href="~/HelpVideo.aspx" target="_blank" class="flo" style="text-decoration: none;
                                        color: Red;font-weight:bold;font-size:17px; height:40px" runat="server">SUPPORT <br /> PORTAL </a>
                            </td>
                            <td valign="top">
                          
                                <a id="A1" href="~/HelpVideo.aspx" target="_blank" class="flo" runat="server">
                                    <img src="Images/icon1.png" alt="" width="70px" height="40px" />
                                </a>
                            </td>
                        </tr>
                    </table>
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
                        <a class="navbar-brand  col-2" href="#">
                            <img src="../../../assets/images/logo.png" alt="" /></a>
                         <div class="col-3">
                                <asp:Label ID="LblUser" CssClass="label1" ForeColor="Black" runat="server" Text="User">  </asp:Label>
                            </div>
                        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
                            <span class="navbar-toggler-icon"></span>
                        </button>
                        <div class="collapse navbar-collapse justify-content-end main-menu" id="collapsibleNavbar">
                            <ul id="menu" class="navbar-nav">
                                <li class="nav-item"><a href="../../../Default.aspx" class="nav-link active" onclick="ShowProgress();">Home</a></li>
                                <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Master</a>
                                    <ul runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink" id="ul_myLst">
                                        <li id="LiDiv" runat="server"><a class="dropdown-item" href="../../../MasterFiles/DivisionList.aspx" onclick="ShowProgress();">Division</a> </li>

                                        <li id="lides" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Designation.aspx" onclick="ShowProgress();">Designation</a></li>
                                        <li id="liholi" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Holiday_List.aspx" onclick="ShowProgress();">Holiday Master</a></li>
                                        <li id="lihoid" runat="server"><a class="dropdown-item" href="../../../MasterFiles/HO_ID_View.aspx" onclick="ShowProgress();">
                                            <asp:Label ID="lblRoute" Text="HO ID Creation" runat="server"></asp:Label>
                                        </a></li>
                                        <li id="lichg" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Sub_HO_ID_View.aspx"
                                            onclick="ShowProgress();">HO ID Creation </a></li>
                                        <li id="lish" runat="server"><a class="dropdown-item" href="../../../MasterFiles/State_HO_View.aspx" onclick="ShowProgress();">Statewise Weekoff Fixation</a></li>
                                        <li id="Lique" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Query_Box_List.aspx"
                                            onclick="ShowProgress();">Query</a> </li>
                                        <!--<li id="limenu" runat="server"><a class="dropdown-item" href="../../../MasterFiles/Menu_Rights_View.aspx"
                                            onclick="ShowProgress();">Menu Rights</a> </li>-->
                                    </ul>
                                </li>
                                <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" href="#" id="A1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Reports</a>

                                    <ul id="lireports" runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                        <li id="liuser" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/User_List.aspx" onclick="ShowProgress();">User List</a></li>
                                        <li id="ullidoc" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Doctor Details</a>
                                            <ul class="dropdown-menu" runat="server">
                                                <li id="lidoc" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DoctorList_Reportaspx.aspx"
                                                    onclick="ShowProgress();">View</a></li>

                                            </ul>
                                        </li>
                                        <li id="liFS" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/MR_Status_Report.aspx"
                                            onclick="ShowProgress();">Fieldforce Status</a></li>
                                        <li id="liCall" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/CallAverage.aspx"
                                            onclick="ShowProgress();">Call Average View</a></li>
                                        <li id="liwt" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/frmWorkTypeStatusView.aspx"
                                            onclick="ShowProgress();">Work Type View Status </a></li>
                                        <!--<li id="lidcr" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCRCount/frmDcrCount.aspx"
                                            onclick="ShowProgress();">DCR Count View </a></li>
                                        <li id="lidcrst" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DCRCount/frmDCRCountStateView.aspx" onclick="ShowProgress();">DCR Count View - State Wise </a></li>
                                        <li id="li2VacantBlockId" runat="server" class="dropdown-submenu"><a class="dropdown-item dropdown-toggle" href="#">Hold / Blocked Id's </a>
                                            <ul class="dropdown-menu">
                                                <li><a class="dropdown-item" href="../../../../MasterFiles/Reports/VacantBlockId.aspx" onclick="ShowProgress();">View</a></li>
                                                 <li><a href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorView.aspx" onclick="ShowProgress();">
                        Territory view</a></li>
                                            </ul>
                                        </li>-->
                                        <li id="li2" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/App_version_View.aspx" onclick="ShowProgress();">App version View </a></li>
                                    </ul>
                                </li>
                                <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" href="#" id="A3" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Options</a>
                                    <ul id="lioption" runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                        <%--<li id="Limail" runat="server"><a href="../../../../MasterFiles/Mails/Mail_Head.aspx"  onclick="ShowProgress();">Mail Box</a></li>   --%>
                                        <li id="Li1" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Statusview_Prd_Camp.aspx" onclick="ShowProgress();">Status View</a></li>
                                        <!--<li id="liND" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/frmDTENotSubmittedDays.aspx" onclick="ShowProgress();">Not Submitted Status</a></li>
                                        <li id="licnt" runat="server" visible="false"><a class="dropdown-item" href="../../../MasterFiles/Common_Doctors/Unique_DR_Tot_View.aspx" onclick="ShowProgress();">Listeddr Count View</a></li>
                                        <li id="liEXPVW" visible="false" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Expense_Status_View.aspx" onclick="ShowProgress();">Expense Status View</a></li>
                                        <li id="li26" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/LstDr_Search.aspx" onclick="ShowProgress();">Listed Dr Search </a></li>-->
                                         <li id="listv" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/State_View.aspx" onclick="ShowProgress();">State View</a></li>
<li id="libill" runat="server" ><a href="javascript:void(0)" onclick="sanbilling_viewInvoices();">Billing Information</a></li>
                                    </ul>
                                </li>
                                <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" href="#" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Dash Board</a>
                                    <ul id="lidashboard" runat="server" class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                        <!--<li id="lidash" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/DashBoard/Doctor_Details.aspx"
                                            onclick="ShowProgress();">Doctor Details</a></li>-->
                                        <li id="lidashbordAlldiv" runat="server"><a class="dropdown-item" href="../../../../Sales_DashBoard_Admin_Brand.aspx"
                                            onclick="ShowProgress();">Dashboard</a></li>
                                          <li id="liStatus" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Status.aspx"
                                            onclick="ShowProgress();">Status</a></li>
                                        <li id="li4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/DashBoard/App_Usage.aspx"
                                            onclick="ShowProgress();">App Usage</a></li>
                                    </ul>
                                </li>
                                <li class="nav-item"><a class="nav-link logout" href="../../../Index.aspx" onclick="ShowProgress();">Logout</a></li>
                            </ul>





                        </div>
                    </nav>



                </div>
                <%-- <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                         <div class="container support clearfix">
                                                <div class="col-lg-12">
                                                    <a class="float-right" runat="server" href="~/HelpVideo.aspx" target="_blank">Support 
                                                        <br /> portal </a>     
                                                       
                                                </div>
                                            </div>
                               </div>--%>
            </div>
        </div>
    </header>

    <script src="../../../assets/js/jQuery.min.js"></script>
    <script src="../../../assets/js/popper.min.js"></script>
    <script src="../../../assets/js/bootstrap.min.js"></script>
    <script src="../../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../../assets/js/main.js"></script>
</div>
