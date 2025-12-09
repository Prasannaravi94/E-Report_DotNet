<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MR_TP_Menu.ascx.cs" Inherits="UserControl_MR_TP_Menu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href='<%=ResolveClientUrl("../css/MR.css")%>' rel="stylesheet" type="text/css" />
<!-- IE6-8 support of HTML5 elements -->
<!--[if lt IE 9]>
	<script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
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

    body {
        font-family: Arial;
        font-size: 10pt;
    }

    .modalBackground {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
        position: absolute;
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

    .modalBackground1 {
        background-color: #3399FF;
        filter: alpha(opacity=45);
        opacity: 0.5;
    }

    .popup tr {
        background-color: LightBlue;
    }

    .popup {
        background-color: #6699FF;
        position: absolute;
        top: 0px;
        border: Gray 2px inset;
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
</style>


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
<%--<script type="text/javascript" language="javascript">
    window.onload = function () {
        noBack();
    }
    function noBack() {
        window.history.forward();
    }
</script>--%>
<%-- <asp:LinkButton ID="lnkFake" runat="server" />--%>
  <%--  <asp:ModalPopupExtender ID="mpeTimeout" BehaviorID="mpeTimeout" runat="server" PopupControlID="pnlPopup"
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
               <span id="secondsIdle" style="visibility:hidden"></span>
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
                 window.location.replace("http://sanffe.info/");
             }, timeout);
         };
         function ResetSession() {
             //Redirect to refresh Session.
             window.location = window.location.href;
         }
         function ExSession() {
             window.location.replace("http://sanffe.info/");
         }
    </script>--%>
<div id="wrapper">

      <asp:Panel ID="pnldiv" runat="server" CssClass="spc">
    <table width="100%" >
        <tr>
            <td class="style1">
                <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                    font-size: 14px; text-align: left; margin-top:0px" ForeColor="Blue" Font-Bold="True" Font-Names="Verdana" >
                </asp:Label>
            </td>
            <td>
            <asp:Panel ID="pnlQueries" runat="server" HorizontalAlign="Right"  CssClass="mar_right" >
 <asp:LinkButton ID="lnkQueries" runat="server" Text="Queries" ForeColor="Red" Font-Size="Medium" Font-Bold="true" ></asp:LinkButton>

    <asp:ModalPopupExtender ID="ModalPopupExtender1"  runat="server" CancelControlID="btnCancel"
        PopupControlID="Panel2" TargetControlID="lnkQueries" BackgroundCssClass="modalBackgroundNew"></asp:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopupNew"  Style="display: none">
        <div class="header">
           Queries
            <asp:ImageButton ID="imgbtnclosegift" runat="server" ImageUrl="~/Images/Close.gif" ImageAlign="Right" />
        </div>
        <div class="body" >
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
    
            <td >
                <asp:TextBox ID="txtQuery" runat="server" Width="330px" TextMode="MultiLine"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td align="right">
            <asp:Button ID="btnSend" runat="server" BackColor="LightBlue" Text="Send" OnClick="btnSend_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" BackColor="LightBlue"   Text="Cancel" />
            </td>
            </tr>
            </table>
        </div>
    </asp:Panel>

         </asp:Panel>
            </td>
        </tr>
    </table>
   </asp:Panel>

 	<ul id="menu">
		<li><a href="../../../../Default_MR.aspx" onclick="ShowProgress()";>Home</a></li>
		<li><a href="#">Information &raquo;</a>
			<ul runat="server" id="ul_Inf">
				<li id="liMRIn1" runat="server"><a href="../../../../MasterFiles/MR/ProductRate.aspx" onclick="ShowProgress();">Product Information</a></li>                
                <li id="liMRIn2" runat="server"><a href="../../../../MasterFiles/MR/Territory/Territory.aspx" onclick="ShowProgress();"><asp:Label ID="lblTerritory" Text="Territory" runat="server"></asp:Label></a></li>
                <li id="ulliDoc" runat="server"><a href="#">Listed Doctor Details &raquo;</a>
                <ul id="ululDoc" runat="server">                
                <li id="liMRIn3" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorList.aspx" onclick="ShowProgress();">Entry</a></li>
                <li id="liMRIn4" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorView.aspx" onclick="ShowProgress();">View</a></li>
                 
                </ul>
                </li>
                  <li id="liunique" runat="server" visible="false"><a href="#">Unique Drs &raquo;</a>
                <ul id="ul2" runat="server">                
                <li id="li3" runat="server"><a href="../../../MasterFiles/Common_Doctors/Common_Doctor_List_FDC.aspx" onclick="ShowProgress();">Creation</a></li>
                <li id="li4" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/ListedDrEdit.aspx" onclick="ShowProgress();">Edit</a></li>  
                <li id="li6" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/ListedDrDeactivate.aspx" onclick="ShowProgress();">Deactivation</a></li> 
                <li id="li2" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/ListedDrReactivate.aspx" onclick="ShowProgress();">Reactivation</a></li>  
                <li id="li7" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/Listeddr_Prod_Map_New.aspx" onclick="ShowProgress();">ListedDr-Product Map</a></li>  
                <%-- <li id="li6" runat="server"><a href="../../../MasterFiles/Common_Doctors/Unique_ListedDrDeactivate.aspx" onclick="ShowProgress();">Deacitivation</a></li>--%>
                  <li id="li5" runat="server"><a href="../../../MasterFiles/Common_Doctors/UniqueDr_Approval_Pending.aspx" onclick="ShowProgress();">Unique DR - Approval Pending View</a></li>
                  <li id="li1" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorView.aspx" onclick="ShowProgress();">View</a></li>
                     <li id="li21" runat="server"><a href="../../../../MasterFiles/CommDr_Pend_Mrwise.aspx"
                                onclick="ShowProgress();">Dr Approval - Pending Count</a></li>
               </ul>
               </li>
                <li id="ulliChem" runat="server"><a href="#">Chemist Details &raquo;</a>
                <ul id="ululChem" runat="server">
                <li id="liMRIn5" runat="server"><a href="../../../../MasterFiles/MR/Chemist/ChemistList.aspx" onclick="ShowProgress();">Entry</a></li>
                 <li id="liMRIn6" runat="server"><a href="../../../../MasterFiles/MR/Chemist/Chemist_Terr_View.aspx" onclick="ShowProgress();">View</a></li>
                </ul>
                </li>
                <li id="ulliHos" runat="server"><a href="#">Hospital Details &raquo;</a>
                <ul id="ululHos" runat="server">
                <li id="liMRIn7" runat="server"><a href="../../../../MasterFiles/MR/Hospital/HospitalList.aspx" onclick="ShowProgress();">Entry</a>               
                </li>
                 <li id="liMRIn8" runat="server"><a href="../../../../MasterFiles/MR/Hospital/Hospital_Terr_View.aspx" onclick="ShowProgress();">View</a></li>
                </ul>                
                </li>
                <li id="ulliUn" runat="server" visible="false"><a href="#">Unlisted Doctor Details &raquo;</a>
                <ul id="ululUn" runat="server">
                <li id="liMRIn9" runat="server"><a href="../../../../MasterFiles/MR/UnListedDoctor/UnLstDoctorList.aspx" onclick="ShowProgress();">Entry</a></li>
                 <li id="liMRIn10" runat="server"><a href="../../../../MasterFiles/MR/UnListedDoctor/Unlisteddr_Terr_View.aspx" onclick="ShowProgress();">View</a></li>
                </ul>
                </li>
                  <li id="liMRIn11" runat="server"><a href="../../../../MasterFiles/MR/Doctor_Spec_List.aspx">Doctor - Speciality List</a></li>
                <li id="liMRIn12" runat="server"><a href="../../../../MasterFiles/MR/HolidayList_MR.aspx" onclick="ShowProgress();">Holiday List</a></li>
                <li id="liMRIn14" visible="false" runat="server"><a href="../../../../MasterFiles/MR/Stockist_List_MR.aspx" onclick="ShowProgress();">Stockist List</a></li>
                <li id="liMRIn13" runat="server"><a href="../../../../MasterFiles/MR/ListedDr_Approval_Pending.aspx" onclick="ShowProgress();">Lst.Dr - Approval Pending View</a></li>
                <li id="liSFC" runat="server"><a href="../../../../MasterFiles/Reports/Distance_fixation_view.aspx" onclick="ShowProgress();">SFC View</a></li>

 <!--<li id="Li8" runat="server"><a href="#">Personal Details &raquo;</a>
					<ul id="ul3" runat="server">
						<li id="li9" runat="server"><a href="../../MIS%20Reports/Employee_Application.aspx" onclick="ShowProgress();">Entry</a></li>
                        <li id="li10" runat="server"><a href="../../MIS%20Reports/Employee_Application_Zoom.aspx" onclick="ShowProgress();">View</a></li>
                        </ul>
                        </li>  -->  		

			</ul>
		</li>
		<li><a href="#">Activities &raquo;</a>
			<ul runat="server" id="ul_Act">
            <li id="ulliTerr" runat="server"><a href="#"><asp:Label ID="lblTerritory1" Text="Territory" runat="server"></asp:Label>  &raquo;</a>
					<ul id="ululTerr" runat="server">
                <%--<li id="liMrAct1" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/RoutePlan.aspx" onclick="ShowProgress();">Normal</a></li>--%>
                <%--<li id="liMrAct2" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/RoutePlan_Catgwise.aspx">Classic (Categorywise)</a></li>--%>
                  <li id="liMrAct3" runat="server"><a href="../../../MasterFiles/Reports/rptRoutePlan.aspx" onclick="ShowProgress();">View</a></li>
                 <li id="liMrAct4" runat="server"><a href="../../../MasterFiles/Reports/RoutePlan_Status.aspx" onclick="ShowProgress();">Status</a></li>
                
                </ul>
                </li>
                 <li id="listp" visible="false" runat="server"><a href="../../../../MasterFiles/MR/STP_Creation.aspx" onclick="ShowProgress();">STP Creation</a></li>
 				<li id="ulliTP" runat="server"><a href="#">Tour Plan &raquo;</a>
					<ul id="ululTP" runat="server">
                          <li id="liMrAct5" runat="server"><a href="../../../../MasterFiles/MR/TourPlan.aspx" onclick="ShowProgress();">Entry</a></li>
                         <li id="liTpTerr" visible="false" runat="server"><a href="../../../../MasterFiles/MR/TP_Entry_Terr.aspx" onclick="ShowProgress();">Entry</a></li>
                         <li id="listptp" visible="false" runat="server"><a href="../../../../MasterFiles/MR/TP_ENTRY_STP.aspx" onclick="ShowProgress();">Entry New</a></li>
                         <li id="litp_auto" visible="false" runat="server"><a href="../../../../MasterFiles/MR/TP_New.aspx"
                            onclick="ShowProgress();">Entry New</a></li>
<%--                        <li id="liMrAct5" runat="server"><a href="../../../../MasterFiles/MR/TP_Entry_daywise.aspx" onclick="ShowProgress();">Entry</a></li>--%>
						<%-- <li><a href="../../../../MasterFiles/MR/TPEdit.aspx" onclick="ShowProgress();">Edit</a></li>--%>
						<li id="liMrAct6" runat="server"><a href="../../../../MasterFiles/Report/TP_View_Report.aspx" onclick="ShowProgress();">View</a></li>
                        <li id="liMrAct7" runat="server"><a href="../../../../MasterFiles/Report/TP_Status_Report.aspx" onclick="ShowProgress();">Status</a></li>
            <%--                 <li id="liccp" runat="server"><a href="../../../../MIS Reports/TPCCP_View.aspx"
                                onclick="ShowProgress();">CCP View</a></li>--%>
                        	<%--<li><a href="#">Status</a></li>--%>
                            <%--<li><a href="#">Deviation</a></li>--%>
                        
					</ul>						
				</li>
                <li id="ulliDCR" runat="server"><a href="#">DCR &raquo;</a>
					<ul id="ululDCR" runat="server">
                <li id="liMrAct8" runat="server" ><a href="../../../../DCR/DCR_Entry.aspx" onclick="ShowProgress();">Entry</a></li>
                
                   <li id="liMrAct9" runat="server"><a href="../../../../MasterFiles/Reports/DCR_View.aspx" onclick="ShowProgress();">
                            View</a></li>
                        <li id="liMrAct10" runat="server"><a href="../../../../MasterFiles/Reports/DCR_Status.aspx" onclick="ShowProgress();">
                            Status</a></li>
                </ul>
                </li>
            <li id="ulliSS" runat="server">
                    <a href="#">Secondary Sales&raquo;</a>
					<ul id="ululSS" runat="server">
                        <li id="liMrAct11" runat="server">
                            <%--<a href="../../../../MasterFiles/MR/SecSales/SecSalesEntry.aspx" onclick="ShowProgress();">Entry</a>--%>

                            <a href="../../../../MasterFiles/MR/SecSales/SecSalesEntry_New.aspx" onclick="ShowProgress();">Entry</a>

                        </li>                
                    </ul>
                </li>
            
                 <li id="liMrAnthem" runat="server" visible="false"><a href="../../../../MasterFiles/MR/RptAutoExpense_RowWise_Anthem.aspx" onclick="ShowProgress();">Expense Statement</a></li>
                 <li id="liMRRwExp" runat="server" visible="false"><a href="../../../../MasterFiles/MR/RptAutoExpense_RowWise.aspx" onclick="ShowProgress();">Expense Statement</a></li>
                <li id="liMRRwTxt" runat="server" visible="false"><a href="../../../../MasterFiles/MR/RptAutoExpense_RowWise_Textbox.aspx" onclick="ShowProgress();">Expense Statement</a></li>
                <li id="liINDSWFT" runat="server" visible="false"><a href="../../../../MasterFiles/MGR/RptAutoExpense_RowWise.aspx" onclick="ShowProgress();">Expense Statement</a></li>
                 <li id="liMrAct13" runat="server"><a href="../../../../MasterFiles/Reports/RptAutoExpense_Approve_View.aspx" onclick="ShowProgress();">Actual Expense View</a></li>
                 <li id="LiMrCPBE13" runat="server"><a href="#">Chemist-Productwise Business&raquo;</a>
                        <ul id="ulMrCPBE3" runat="server">
                            <li id="liMrCPBE14" runat="server"><a href="../../../../MasterFiles/ActivityReports/ChemistProductwiseBusinessEntry.aspx" onclick="ShowProgress();">
                                Entry</a></li>
                            <li id="liMrCPBE15" runat="server"><a href="../../../../MasterFiles/ActivityReports/ChemistProductwiseBusinessView.aspx" onclick="ShowProgress();">
                                View</a></li>
                        </ul>
                    </li>
                 <li id="ulliBus" runat="server"><a href="#">Doctor Business &raquo;</a>
                        <ul id="ululBus" runat="server">
                               <%--<li id="liMrAct14" runat="server"><a href="../../../../MasterFiles/ActivityReports/DoctorProductBusinessEntry.aspx" onclick="ShowProgress();">
                                Entry</a></li>--%>
                            <li id="liA7" runat="server" visible="false" ><a class="dropdown-item" href="../../../../MasterFiles/Doctor_Business_Entry.aspx" onclick="ShowProgress();">Entry</a></li>
                                <li id="liMrAct15" runat="server"><a href="../../../../MasterFiles/ActivityReports/DoctorProductBusinessView.aspx" onclick="ShowProgress();">
                                View</a></li>
                      </ul>
                    </li>
                    <li id="LiMRDocBusVal" runat="server"><a href="#">Doctor Business Valuewise&raquo;</a>
                        <ul id="ulMRDocBusVal" runat="server">
                            <li id="liMRDocBusVwEntry" runat="server"><a href="../../../../MasterFiles/ActivityReports/DoctorBusinessValuewise_Entry.aspx" onclick="ShowProgress();">
                                Entry</a></li>
                            <li id="liMRDocBusVwView" runat="server"><a href="../../../../MasterFiles/ActivityReports/DoctorBusinessValuewise_View.aspx" onclick="ShowProgress();">View</a></li>
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
                                            <li id="ulliInDes" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Input Despatch </a>
                                                    <ul id="ululInDes" class="dropdown-menu" runat="server">
                                                        <li id="liA17" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/inputproduct.aspx"
                                                            onclick="ShowProgress();">View</a> </li>

                                                         <li id="liincalW" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/input_Status_CalendarYearWise.aspx"
                                                            onclick="ShowProgress();">status(Financial YearWise)</a> </li>
                                                    </ul>
                                                </li>

                             <li id="ulliCRM" runat="server">
              <a href="#">Doctor Service CRM &raquo;</a>
              <ul id="ululCRM" runat="server">
                <li id="liMrCRM101" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_New.aspx" onclick="ShowProgress();">
                        Entry</a></li>
                <li id="liMrCRM102" runat="server"><a href="../../../../MasterFiles/MR/ListedDoctor/Doctor_Service_CRM_Status.aspx" onclick="ShowProgress();">
                Status</a></li>  
              </ul>
             </li>
                 <li id="ulliTarget" runat="server" visible="false"><a href="#">Target  &raquo;</a>
                        <ul id="ululTarget" runat="server">
                            <li id="liA20" runat="server"><a href="../../../../MasterFiles/ActivityReports/TargetFixationView.aspx"
                                onclick="ShowProgress();"> View</a></li>
                            <li id="TarvsSal" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReports/TargetVsSales.aspx"
                                onclick="ShowProgress();">Target Vs Sales</a></li>
                        </ul>
                    </li>

                <%--  PS Pasted on 09/10/2019 at 1.43 p.m  --%> 
                <li id="liTaskMr" runat="server" visible="false"><a href="../../../../MasterFiles/Task_Management_New/Task2.aspx"
                                onclick="ShowProgress();">Task Management</a></li>

                 <li id="Li2book" runat="server" visible="false">
              <a href="#">Order Booking &raquo;</a>
              <ul id="ul2book" runat="server">
                <li id="li3book" runat="server"><a href="../../../../MasterFiles/MR/Order_booking.aspx" onclick="ShowProgress();">
                        Entry</a></li>
                   <li id="li2bookview" runat="server"><a href="../../../../MasterFiles/MR/Order_Booking_View.aspx" onclick="ShowProgress();">
                        View</a></li>
              
              </ul>

             </li>
<%--                <li id="Lis77" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Input Issued</a>
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
		<li><a href="#">MIS Reports &raquo;</a>
            <ul runat="server" id="ul_Mis">
            <%--    <li><a href="../MasterFiles/MR_DCRAnalysis.aspx" onclick="ShowProgress();">DCR Analysis</a></li>--%>
                     <li id="ulliDD" runat="server"><a href="#">Field Force Master</a>
                    <ul id="ululDD" class="dropdown-menu" runat="server">
                       <li id="liMrMis1" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/DoctorList_Reportaspx.aspx" onclick="ShowProgress();">Doctor Summary Count View</a></li>
                       <li id="li10" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/MR_Status_Report.aspx" onclick="ShowProgress();">Listed Doctor and Chemist Master</a></li>
                    </ul>
                </li>
                <li id="ulliAnly" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Field Work Analysis</a>
                                                <ul id="ululAnly" class="dropdown-menu" runat="server">
                                                    <li id="liMrMis2" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/DCR_Analysis.aspx" onclick="ShowProgress();">Daily Work Report Summary</a></li>
                                                    <!--<li id="liMrMis3" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/Visit_Month_Vertical_Wise.aspx" onclick="ShowProgress();">Visit Analysis</a></li>-->
                                                    <li id="liPob" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Analysis_Pob_count.aspx" onclick="ShowProgress();">Doctors/Chemists POB Report</a></li>
                                                    <li id="lir8" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Analysis_Pob_count_Periodically.aspx"
                                                            onclick="ShowProgress();">Product wise Rx Report</a></li>
                                                    <li id="li20" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/CallAverage.aspx" onclick="ShowProgress();">Drs Met/Visit and Call average Report</a></li>
                                                    <li id="li22" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/frmWorkTypeStatusView.aspx" onclick="ShowProgress();">Work Type View Report</a></li>
                                                         <li id="liMrMis7" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/Missed_Call.aspx" onclick="ShowProgress();">
                                                                
                        Missed Call Report</a></li>
                                                     <li id="liMis20" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/ListeddrCamp_View.aspx"
                                                            onclick="ShowProgress();">Campaign View</a></li>
                                                </ul>
                                            </li>

                 
                 <!--<li id="Li15" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Status</a>
                                                <ul id="ul4" class="dropdown-menu" runat="server">
                                                    <li id="liMrMis4" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/MR_Status_Report.aspx" onclick="ShowProgress();">Fieldforce</a></li>
                                                     <li id="liMrMis5" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/CallAverage.aspx" onclick="ShowProgress();">Call Average</a></li>
                                                    <li id="liMrMis6" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/frmWorkTypeStatusView.aspx" onclick="ShowProgress();">Work Type View</a></li>
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

                
          <%--             <li><a href="../../../../MasterFiles/AnalysisReports/Listeddr_View_Period.aspx" 
                                onclick="ShowProgress();">Listed Doctor Visit - Periodically</a></li>--%>

    <%--         <li id="liMrMis8" runat="server"><a href="../../../../MIS Reports/VisitDetail_Datewise.aspx" onclick="ShowProgress();">
                        Visit Detail - Datewise</a></li>--%>
                 <li id="ulliVis" runat="server"><a href="#">Visit Details &raquo;</a>
                    <ul id="ululVis" runat="server">
                           <%--  <li><a href="../../../../MIS Reports/Visit_Details_Report.aspx" onclick="ShowProgress();">
                                Listed Doctorwise</a></li>--%>
                                  <li id="liMrMis8" runat="server"><a href="../../../../MasterFiles/AnalysisReports/Visit_Details_Cat_Cls_Spclty_LstDr_Wise.aspx" onclick="ShowProgress();"> Category/Speciality/Listed Doctorwise</a></li>
                                <!-- <li id="liMrMis9" runat="server"><a href="../../../../MIS Reports/VisitDetail_Datewise.aspx" onclick="ShowProgress();">
                        Datewise</a></li>-->
                          <li id="liMrMis10" runat="server"><a href="../../../../MasterFiles/AnalysisReports/Listeddr_View_Period.aspx" 
                                onclick="ShowProgress();">Doctor wise - Periodic Visit Details</a></li>                            
                                
                           <!--<li id="liMrMis11" runat="server"><a href="../../../../MasterFiles/AnalysisReports/Callfeedback_Pobwise.aspx" onclick="ShowProgress();">
                        Call Feedbackwise</a></li>
                            <li id="liMrMis12" runat="server"><a href="../../../../MIS Reports/Visit_Details_Fixation.aspx" onclick="ShowProgress();">
                                Fixationwise(By Visit)</a></li>-->
                          <%--  <li id="liMrMis13" runat="server"><a href="../../../../MIS Reports/Visit_Details_Basedon_ModeWise.aspx" onclick="ShowProgress();">
                                Category/Specialitywise Coverage</a></li>--%>
                            <li id="liMrMis14" runat="server" visible="false"><a href="../../../../MIS Reports/Visit_Details_At_a_Glance.aspx" onclick="ShowProgress();">
                                At a Glance</a></li>    
                           <!-- <li id="liMis_Chm_Ul" runat="server"><a href="../../../../MIS Reports/Visit_Details_Chmst_UnLstDr.aspx" onclick="ShowProgress();">
                                Chemist & UnListed Doctors</a></li>   -->                     
                            <%--   <li><a href="#">Modewise</a></li>
                        <li><a href="#">Productwise</a></li>--%>
                        </ul>
                </li>
                
               <li id="ulliMrSV" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Sales Analysis</a>
                                                <ul id="ululSS2" class="dropdown-menu" runat="server">
                                                    <!--<li id="liMrMis15" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/Reports/SecSalesReport.aspx" onclick="ShowProgress();">View</a></li>-->
                                                    <li id="li14" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/AnalysisReports/PrimarySale_Multi_Mnth_Dump.aspx" onclick="ShowProgress();">Primary Sales Consolidate</a></li>
                                                    <li id="limrCon" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/SecSaleReport/SecSalesReport.aspx" onclick="ShowProgress();">Secondary Sales Consolidate</a></li>
                                                     <li id="li8" runat="server" visible="true"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Primary_Stk_Bill_View.aspx" 
                                                        onclick="ShowProgress();">Primary Sales Stockistwise View</a></li>
                                                    <li id="li9" runat="server" visible="true"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Primary_Product_Bill_View.aspx"
                                                        onclick="ShowProgress();">Primary Sales Productwise View</a></li>

                                                    <li id="limrSR" runat="server" visible="false"><a class="dropdown-item" href="../../../../MasterFiles/SecSaleReport/SalesAnalysis.aspx" onclick="ShowProgress();">Mode wise</a></li>
                                                </ul>
                                            </li>

               <%--  <li id="ulliSamIn" runat="server"><a href="#">Sample / Input &raquo;</a>
                        <ul id="ululSamIn" runat="server">
                            <li id="liMis25" runat="server"><a href="../../../../MIS Reports/sampleproduct_details.aspx"
                                onclick="ShowProgress();">Sample Issued - Fieldforce Wise</a></li>
                            <li id="liMis26" runat="server"><a href="../../../../MIS Reports/SampleGift_details.aspx"
                                onclick="ShowProgress();">Input Issued - Fieldforce Wise</a></li>
                           
                        </ul>
                    </li>--%>

               <%-- <li id="ulliCamp" class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Campaign</a>
                                                    <ul id="ululCamp" class="dropdown-menu" runat="server">
                                                        <li id="liMis20" runat="server"><a class="dropdown-item" href="../../../../MIS Reports/ListeddrCamp_View.aspx"
                                                            onclick="ShowProgress();">View</a></li>--%>
                                                        <%--<li id="liMis21" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/Reports/Campaign_View.aspx"
                                                            onclick="ShowProgress();">Status</a></li>--%>
                                                   <%-- </ul>
                                                </li>--%>

                     <li id="ulliEx" runat="server"><a href="#">Exception &raquo;</a>
                        <ul id="ululEx" runat="server">
                            <li id="liMrMis17" runat="server"><a href="../../../../MIS Reports/TP_Deviation.aspx" onclick="ShowProgress();">Tour Plan</a>
                            </li>
                        </ul>
                    </li>

                     <li id="ulliBill" runat="server"><a href="#">Primary Bill &raquo;</a>
                        <ul id="ululBil" runat="server">  
                             <li id="lib1sst" runat="server" visible="true"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Primary_Stk_Bill_View.aspx"
                                                        onclick="ShowProgress();">Stockist View</a></li>
                                                    <li id="lib2ppt" runat="server" visible="true"><a class="dropdown-item" href="../../../MasterFiles/AnalysisReports/Primary_Product_Bill_View.aspx"
                                                        onclick="ShowProgress();">Product View</a></li>                         
                            <li id="liStck" runat="server" visible="false"><a href="../../../MasterFiles/Stock_Product.aspx"
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
                 <%--     <li id="li1088" runat="server"><a href="../../../../MasterFiles/Reports/Approved_Coverage_Plan.aspx" onclick="ShowProgress();">
                    Approved Coverage Plan</a></li>--%>
            </ul>

        </li>
		<li><a href="#">Options &raquo;</a>
            <ul runat="server" id="ul_Opt">
                <li id="liMrOpt1" runat="server"><a href="../../../../MasterFiles/Options/ChangePassword.aspx" onclick="ShowProgress();">
               <asp:Label ID="lblChange" runat="server" Text="Change Password" ></asp:Label></a></li>
               
                <li id="liMrOpt2" runat="server"><a href="../../../../MasterFiles/Mails/Mail_Head.aspx" onclick="ShowProgress();">Mail Box</a></li>   
                  <li id="liMrOpt3" runat="server"><a href="../../../../MasterFiles/MR/FileUpload_MR.aspx" onclick="ShowProgress();">Files Download</a></li>               
    <li id="liMrOpt4" runat="server"><a href="../../../../MasterFiles/MR/UserManual_View.aspx" onclick="ShowProgress();">User Manual - View</a></li>               
                  <li id="ulliLve" runat="server"><a href="#">Leave &raquo;</a>
                  <ul id="ululLve" runat="server">
                  <li id="liMrOpt5" runat="server"><a href="../../../../MasterFiles/MR/LeaveForm.aspx" onclick="ShowProgress();">Entry</a></li>               
                  <li id="liMrOpt6" runat="server"><a href="../../../../MasterFiles/MR/Leave_Status.aspx" onclick="ShowProgress();">Status</a></li>               
                         </ul>
                  </li>
                <%--<li id="li11"  class="dropdown-submenu" runat="server"><a class="dropdown-item dropdown-toggle" href="#">Managerwise Core Doctor</a>
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
                    <li id="liMrOpt7" runat="server"><a href="../../../../MasterFiles/MR/Doctor-SubCategory-Map.aspx" onclick="ShowProgress();">Doctor - Campaign Map</a></li>     
                                <li id="liChemCam" runat="server"><a href="../../../../MasterFiles/Chemist_Campaign/Chemist-SubCategory-Map.aspx" onclick="ShowProgress();">Chemist - Campaign Map</a></li>               
          
                        <li id="liMrOpt8" runat="server"><a href="../../../../MasterFiles/Options/Payslip.aspx" onclick="ShowProgress();">
                        <asp:Label ID="lblpay" runat="server" Text="PaySlip View" ></asp:Label> </a></li>
                        <li id="lipayF" runat="server"><a href="../../../../MasterFiles/Options/Payslip_File_View.aspx" onclick="ShowProgress();">
                       PaySlip Files View</a></li>
                 <li id="LiMrSV008" runat="server"><a href="../../../../MasterFiles/Options/Status_View.aspx" onclick="ShowProgress();">Status View</a></li>  
                <li id="Li11" runat="server"><a class="dropdown-item" href="../../../../MasterFiles/ActivityReport.aspx" onclick="ShowProgress();">Activity Report</a></li>          
            </ul>
        </li>
		<li><a href="../../../../Index.aspx" class="first" onclick="ShowProgress();">Logout</a></li>
	</ul>

 
         <asp:Panel ID="pnlHeader" runat="server" Width="100%" align="center">
        <table width="95%" cellpadding ="0" cellspacing ="0" align="center">
            <tr>
                <td>                   
                    <table id="tblpanel" runat="server" width="100%" border="0">
                        <tr>                 
                                                     
                             <td  style="width:35%" >
                                 <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" 
                                  ForeColor="Black" style="font-size:13px; text-align: center;" 
                                Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                            </td>
                            <td align="center" class="style5" style="width:30%">
                            <asp:Label ID="lblHeading" runat="server" style="text-transform: capitalize;
                                font-size:15px; text-align: center;" ForeColor="Blue" 
                                Font-Bold="True" Font-Names="Verdana" CssClass="under">
                            </asp:Label>
                        </td>
                        
                        
                        
                            <td align ="right" class="style3" style="width:35%"  >                            
                                <asp:Button ID="btnBack" runat="server" CssClass="savebutton" Height="25px" Width="70px" Text="Back" 
                                    onClick="btnBack_Click" />
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

           <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
           <img id="Img1" src="../Images/loader.gif" alt="" runat="server" />
        </div>   
</div>
<!--end wrapper-->
