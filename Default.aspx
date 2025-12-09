<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--  <title>E-Reporting Sales & Analysis</title>--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Welcome Corporate – HQ</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" type="image/png" href="assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/style.css" />
   <%-- <link rel="stylesheet" href="assets/css/nice-select.css" />--%>
    <link rel="stylesheet" href="assets/css/responsive.css" />

    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
    <style type="text/css">
        .exam #lblFN {
            padding: 2em 2em 0 2em;
        }

        #lblFN {
            width: 130px;
            height: 70px;
            background: Red;
            -moz-border-radius: 30px / 30px;
            -webkit-border-radius: 30px / 30px;
            border-radius: 30px / 30px;
            padding: 3px;
        }

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
            width: 600px;
            min-height: 180px;
            max-height: auto;
            top: 30%;
            left: 40%;
            margin-left: -150px;
            margin-top: -100px;
            background-color: #ffffff;
            border: 1px solid #0077FF;
            padding: 0px;
            z-index: 102;
            /*font-family: Verdana;*/
            font-size: 10pt;
            overflow-x:auto;
        }

        .web_dialog_title {
            border-bottom: solid 1px #0077FF;
            background-color: #0077FF;
            padding: 4px;
            color: White;
            font-weight: bold;
            font-size: 15px;
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
            background-color: #158263;
            border-color: #158263;
        }

            .btnReActivation:hover {
                color: #fff;
                background-color: #2b9a7b;
                border-color: #2b9a7b;
            }

            .btnReActivation:focus, .btnReActivation.focus {
                color: #fff;
                background-color: #2b9a7b;
                border-color: #2b9a7b;
            }

            .btnReActivation:active, .btnReActivation.active {
                color: #fff;
                background-color: #158263;
                border-color: #158263;
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





        .modal {
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

        .padding {
            padding-left: 10px;
        }
             .fa-headphones {
            content: "\f025";
            font-size: 30px !important;
            color: #2662f0;
        }
    </style>
    <style type="text/css">
        .blink_me {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 1s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 1s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 1s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }



        .exam #lblFN {
            padding: 2em 2em 0 2em;
        }

        #lblFN {
            width: 130px;
            height: 70px;
            background: Red;
            -moz-border-radius: 30px / 30px;
            -webkit-border-radius: 30px / 30px;
            border-radius: 30px / 30px;
            padding: 3px;
        }
        .single-block-area #MailCount a {
            font-size: 16px;
        }

    </style>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.js"></script>

    <script type="text/javascript">
        function openOnImageClick() {
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

        }
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
    <script type="text/javascript">
        function hideRadioSymbol() {
            var rads = new Array();
            rads = document.getElementsByName('div'); //Whatever ID u have given to ur radiolist.
            for (var i = 0; i < rads.length; i++)
                document.getElementById(rads.item(i).id).style.display = 'none'; //hide
        }
    </script>
    <script type="text/javascript">

        function OpenNewWindow() {

            //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

            window.open('DoctorBirthday_View.aspx', null, '');
            return false;
        }

    </script>
    <script type="text/javascript">

        function OpenWindow() {

            //          //  window.open("NoticeBoard_design.aspx");
            //            window.open('NoticeBoard_design.aspx', null, 'height=800, width=700,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');

            //            return false;
            var paramVal = "MRLink";
            window.open("NoticeBoard_design.aspx?id=" + paramVal,
             "ModalPopUp"//,
   //"toolbar=no," +
   //"scrollbars=yes," +
   //"location=no," +
   //"statusbar=no," +
   //"menubar=no," +
   //"addressbar=no," +
   //"resizable=yes," +
   //"width=700," +
   //"height=800," +
   //"left = 0," +
   //"top=0"
   );

            return false;
            //window.open('NoticeBoard_design.aspx', null, 'height=500, width=900, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
        }

    </script>
    <script type="text/javascript">

        function OpenNewWindow_delay() {

            //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

            window.open('Delayed_Status_Multiple.aspx', null, '');
            return false;
        }

    </script>
    <script type="text/javascript">

        function OpenWindow_Service() {
            window.open('MasterFiles/MGR/Car_Service.aspx', null, '');
            return false;

        }

    </script>

    <script type="text/javascript">

        function OpenNewWindow_Asses_MR() {

            //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

            window.open('Assesment_MR_View.aspx', null, '');
            return false;
        }

    </script>
    <script type="text/javascript">

        function OpenNewWindow_Asses_MGR() {

            //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

            window.open('Assesment_MGR_View.aspx', null, '');
            return false;
        }

    </script>
    <script type="text/javascript">

        function OpenWindow_Joinee() {

            //          //  window.open("NoticeBoard_design.aspx");
            //            window.open('NoticeBoard_design.aspx', null, 'height=800, width=700,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');

            //            return false;

            window.open('MasterFiles/MGR/Joinee_KitView.aspx', null, '');
            return false;

        }

    </script>
    <script type="text/javascript">

        function OpenWindow_Recmd() {

            //          //  window.open("NoticeBoard_design.aspx");
            //            window.open('NoticeBoard_design.aspx', null, 'height=800, width=700,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');

            //            return false;

            window.open('MasterFiles/MGR/Recommendation_View.aspx', null, '');
            return false;

        }

    </script>

    <script type="text/javascript">

        function TabletsIANs() {

            window.open('http://www.tors.sfacrm.info/Tabletiancorner/home.asp', null, '');
            return false;
        }
    </script>

    <script type="text/javascript">


        $(document).ready(function () {

            $("#btnReActivate_Plus").click(function (e) {
                ShowDialog_Plus(false);
                e.preventDefault();
            });

            $("#btnClose_Plus").click(function (e) {
                HideDialog_Plus();
                e.preventDefault();
            });

            $("#btnTabletians").click(function (e) {
                TabletsIANs();
                e.preventDefault();
            });
        });

        function ShowDialog_Plus(modal) {
            $("#overlay_Plus").show();
            $("#dialog_Plus").fadeIn(300);

            if (modal) {
                $("#overlay_Plus").unbind("click");
            }
            else {
                $("#overlay_Plus").click(function (e) {
                    HideDialog_Plus();
                });
            }
        }

        function HideDialog_Plus() {
            $("#overlay_Plus").hide();
            $("#dialog_Plus").fadeOut(300);
        }


    </script>
    <script type="text/javascript">
    function Openlicense() {
        window.open('MasterFiles/LicenceKey_View.aspx', null, '');
        return false;
    }
</script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu" runat="server" />
            <section class="home-section-area clearfix">
		<br />
           	<br />
			<div class="container home-section-main-body clearfix">
				<div class="row clearfix">
					<div class="col-lg-12">
						<div class="row clearfix">
							<div class="col-lg-4">
								<div class="division-selection">
									<h3 class="text-center">Division Selection</h3>
									<span class="select-division d-inline-block w-100 text-center">Select Division then click Enter</span>
									
										<%--   <asp:ListBox ID="dd1division" Width="100%" Height="100%" runat="server" Style="border: solid 1px black;
                                        border-collapse: collapse;" AutoPostBack="false" OnDataBound="ddldivision_DataBound"
                                        OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:ListBox>--%>
                                       <%-- <form action="" method="post" class="clearfix">--%>
										<p >
                                        <asp:RadioButtonList ID="div"  RepeatLayout="Flow"  runat="server" ></asp:RadioButtonList>
                                        </p>
										<%--<button type="submit">Enter</button>--%>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Enter" CssClass="button" OnClick="btnSelect_Click"></asp:Button>
                                  

                                     <asp:Button ID="btndash" runat="server" Text="Enter W/O Dashboard" CssClass="button" OnClick="btndash_Click"></asp:Button>
                                           
                                    <%--    </form>--%>
                                 

								
								</div>
							</div>
							<div class="col-lg-8">
								<div class="row clearfix">
								<div class="col-lg-6">
										<div class="single-block-area  text-center clearfix">
                                            <div style="float:left;width:60%">
                                               <h3 style="text-align:left;padding-top:10px">Recieved Mails</h3>
                                            </div>
                                             <div style="float:right;width:40%">
                                                  <asp:DropDownList ID="ddldivision" runat="server" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged1" AutoPostBack="true" CssClass="nice-select" ></asp:DropDownList>
                                             </div>
											
                                           
										<%--	<a href="#"><img src="../assets/images/link.png" alt="" /> Nelle Pearson</a>
											<p>Beta 2 Microglobulin (B2M) Tumor Marker Test <img src="../assets/images/Oval.png" alt="" /></p>
											<a class="detail-link" href="#">Details</a>
											<a class="detail-link" href="#">Contact </a>--%>
                                            <table width="100%" id="MailCount">
                                            <tr>
                                            <td>
                                            <h3 class="text-center" style="color:Blue;padding-bottom:0px;">
                                                <%--<asp:Label ID="lblunread" runat="server" Text=""></asp:Label>--%>
                                                <asp:LinkButton ID="lblunread" runat="server" OnClick="lblunread_Click" ForeColor="Black" >LinkButton</asp:LinkButton>
                                            </h3>
                                             <h3 class="text-center" style="color:Blue;padding-bottom:0px;">Unread</h3>
                                             </td>
                                             <td>
                                               <h3  class="text-center" style="padding-bottom:0px;">
                                                  <%-- <asp:Label ID="lblInbox" runat="server" Text=""></asp:Label>--%>
                                                    <asp:LinkButton ID="lblInbox" runat="server" OnClick="lblInbox_Click" ForeColor="Black">LinkButton</asp:LinkButton>
                                               </h3>
                                             <h3 class="text-center" style="color:Blue;padding-bottom:0px;">Inbox</h3>
                                             </td>
                                             </tr>
                                                 <tr>
        <td colspan="2" class="text-center">
           
            <h3 onclick="return Openlicense();" style="padding-bottom:0px;color:Blue; font-size:12px;">License Key</h3>
        </td>
    </tr>
                                             </table>
										</div>
									</div>
									<div class="col-lg-6">
										<div class="single-block-area text-center clearfix" style="padding-top:35px">
											<p><img src="../assets/images/icon-1.png" alt="" /></p>
											<h3 runat="server" onclick="return OpenNewWindow();" style="padding-bottom:0px;">Click here to View Doctor’s DOB and DOW</h3>
										</div>
									</div>
									<div class="col-lg-6">
										<div class="single-block-area clearfix">
											<h3 >Delayed report status</h3>
											<p class="text-center" runat="server" onclick="return OpenNewWindow_delay();"><img src="../assets/images/icon-2.png" alt="" /></p>
										</div>
									</div>
									<div class="col-lg-6">
										<div class="single-block-area clearfix">
											<h3 >Notice board</h3>
											<p class="text-center" runat="server" onclick="return OpenWindow();"><img src="../assets/images/icon-3.png" alt="" /></p>
										</div>
									</div>
									<div class="col-lg-6">
										<div class="single-block-area m-0 clearfix">
											<h3>Task</h3>
											<p class="text-center">
                                               <a id="btnReActivate_Plus" href="#">
                                                     <img src="assets/images/icon-4.png" alt="" />
                                               </a>
                                              </p>
										</div>
                                         <div>   
                                        <div id="output_Plus">
                                        </div>
                                        <div id="overlay_Plus" class="web_dialog_overlay">
                                        </div>
                                        <div id="dialog_Plus" class="web_dialog">
                                            <table cellpadding="3" cellspacing="0" style="width: 100%; border: 0px;">
                                                <tr>
                                                    <td class="web_dialog_title">Task Management </td>
                                                    <td class="web_dialog_title align_right"><a id="btnClose_Plus" href="#">Close</a> </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <center>
                                                            <table id="tblDivisionDtls" align="center" border="0" cellpadding="3" cellspacing="3">
                                                                <tr>
                                                                    <td><span style="font-size:14px;font-weight:bold;color:blueviolet">Task Manager Currently in BETA Version.<br /> It will be Available till 31st JAN 2020.<br /> After 1st FEB 2020, Kindly Contact SANeFORCE - Support Team.<br /> </span>
                                                                        <br />
                                                                        <br />
                                                                        <span style="font-size:14px;font-weight:bold">Task Module Description :</span>
                                                                        <br />
                                                                        1. All the Line Managers and Admin has to Assign the Task to their Subordinates. (including MR&#39;s) For this Click the &quot;Task Management&quot; link and Click &quot;Assign&quot; link at Right Side Corner. &quot;MR&quot; Not Possible to Assign the Task.<br />
                                                                        <br>
                                                                        <br>
                                                                       
                                                                      
                                                                        2. After Assigned the Task by their all line Managers or Admin, the User has to Click &quot;Task Management&quot; link and Click &quot;Status&quot; link at Right Side Corner&quot;. Then User has to Read the Task and make it as &quot;Completed&quot;. (If User Read the Task, those Task will be Moved to &quot;Pending&quot; Status. Both &quot;New&quot; and &quot;Pending&quot; Modes, the User can &quot;Complete&quot; their Task.)
                                                                        <br />
                                                                        <br>
                                                                       
                                                                       
                                                                        3. After Completion of Task, the &quot;Assigner&quot; has to Click the &quot;Completed&quot; Mode and &quot;Close&quot; the Task by using the link &quot;Status&quot; at Right Side Corner. After &quot;Closed&quot; the Task
                                                                        <br>
                                                                       
                                                                      
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center;"></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

									</div>


                                   


                                    	<div class="col-lg-6">
												<div class="single-block-area clearfix">
                                      <h3>  Any query please whatsapp</h3>
                                      <div class="fa fa-headphones position-relative clearfix"></div>
                                          <div  style="font-family: sans-serif;line-height: 2;display: contents;">
							            <a href="https://www.sansupport.in" target="_blank" style="padding-left:12px;font-weight: 600;">Support portal</a>
										<h3 style="padding-bottom:0px;padding-left: 44px;">8929222444</h3>
										<p style="padding-left:44px;color:black">24/7 Support</p>
                                                <a id="btnTabletians" href="#">
                                                     <img src="assets/images/TABLETIAN_Corner.jpg" alt="" />
                                               </a>
                                              </div> 
									
                                        </div>
                                        </div>
                                     <div class="col-lg-12" id="newMsg" runat="server" >
										<div class="single-block-area  clearfix">                                          
                                            <h3>Alerts</h3>
                                              <asp:Label ID="linkjoinee" runat="server" Font-Size="14px" 
                                        Text="You Have Received "></asp:Label>&nbsp;
                                     <asp:LinkButton ID="linkjoincnt" runat="server" Font-Size="14px" ForeColor="blue" 
                                        Text="" OnClientClick="return OpenWindow_Joinee();"></asp:LinkButton>&nbsp;
                                    <asp:Label ID="linkjoin" runat="server" Font-Size="14px" 
                                        Text="New Joinee Kit Requisition" ></asp:Label><span class="blink_me"> <asp:Image ID="Image1" runat="server" src="Images/popBrown.png" style="height: 18px; width: 19px" Visible="false"/></span>
                                            <br />
                                               <asp:Label ID="lblrecmd" runat="server" Font-Size="14px" 
                                        Text="You Have Received "></asp:Label>&nbsp;
                                     <asp:LinkButton ID="linkrecommend" runat="server" Font-Size="14px" ForeColor="blue" 
                                        Text="" OnClientClick="return OpenWindow_Recmd();"></asp:LinkButton>&nbsp;
                                    <asp:Label ID="lblre" runat="server" Font-Size="14px" 
                                        Text="Recommendation For Confirmation" ></asp:Label><span class="blink_me"> <asp:Image ID="Image2" runat="server" src="Images/popRed.png" style="height: 18px; width: 19px" Visible="false"/></span>
                                            <br />
                                                   <asp:Label ID="lblService" runat="server" Font-Size="14px" 
                                        Text="You Have Received "></asp:Label>&nbsp;
                                     <asp:LinkButton ID="linkService" runat="server" Font-Size="14px" ForeColor="blue" 
                                        Text="" OnClientClick="return OpenWindow_Service();"></asp:LinkButton>&nbsp;
                                    <asp:Label ID="lblcar" runat="server" Font-Size="14px" 
                                        Text="Car Service Request" ></asp:Label><span class="blink_me"> <asp:Image ID="Image3" runat="server" src="Images/popDarkpink.png" style="height: 18px; width: 19px" Visible="false"/></span>
                                            <br />
                                                                              <asp:Label ID="lblMR" runat="server" Font-Size="14px" 
                                        Text="You Have Received "></asp:Label>&nbsp;
                                     <asp:LinkButton ID="linkAssMRcnt" runat="server" Font-Size="14px" ForeColor="blue" 
                                        Text="" OnClientClick="return OpenNewWindow_Asses_MR();"></asp:LinkButton>&nbsp;
                                    <asp:Label ID="lblMRIn" runat="server" Font-Size="14px" 
                                        Text="Interview Assessment MR" ></asp:Label><span class="blink_me"> <asp:Image ID="Image4" runat="server" src="Images/Popblue.png" style="height: 18px; width: 19px" Visible="false"/></span>
                                            <br />
                                              <asp:Label ID="lblMGR" runat="server" Font-Size="14px" 
                                        Text="You Have Received "></asp:Label>&nbsp;
                                     <asp:LinkButton ID="linkAssMGRcnt" runat="server" Font-Size="14px" ForeColor="blue" 
                                        Text="" OnClientClick="return OpenNewWindow_Asses_MGR();"></asp:LinkButton>&nbsp;
                                    <asp:Label ID="lblMGRIn" runat="server" Font-Size="14px" 
                                        Text="Interview Assessment MGR" ></asp:Label><span class="blink_me"> <asp:Image ID="Image5" runat="server" src="Images/popRose.png" style="height: 18px; width: 19px" Visible="false"/></span>
                                            </div>
                                         </div>

                                        <div class="col-lg-12">
										<div class="single-block-area  clearfix">
                                        	<h3>Flash News</h3>
                                            <p ID="lblFlash" runat="server" style="color:Blue" class="text-center"></p>
                                        </div>
                                        </div>
								</div>
							</div>
						</div>
					</div>
					<%--<div class="margin-30 clearfix"></div>
					<div class="col-lg-12">
						<div class="row">
							<div class="col-lg-4">
								<div class="contact-nubmber">
									<p>Any query please whatsapp</p>
									
									<a href="#">Start chat</a>
									<a href="#">Voice call</a>
								</div>
							</div>
							
						</div>
					</div>
				</div>--%>
			</div>
                </div>
			<div class="margin-30 clearfix"></div>
		</section>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="Images/loader.gif" alt="" />
            </div>
        </div>
        <script type="text/javascript">        hideRadioSymbol()</script>
        <br />

    </form>

    <%--<script>
        function initFreshChat() {
            window.fcWidget.init({
                token: "65d3efbd-8e34-4db8-bd0b-dbcd2d844694",
                host: "https://wchat.freshchat.com"
            });
        }
        function initialize(i, t) { var e; i.getElementById(t) ? initFreshChat() : ((e = i.createElement("script")).id = t, e.async = !0, e.src = "https://wchat.freshchat.com/js/widget.js", e.onload = initFreshChat, i.head.appendChild(e)) } function initiateCall() { initialize(document, "freshchat-js-sdk") } window.addEventListener ? window.addEventListener("load", initiateCall, !1) : window.attachEvent("load", initiateCall, !1);
</script>--%>

    <script src="assets/js/jQuery.min.js" type="text/javascript"></script>
    <script src="assets/js/popper.min.js" type="text/javascript"></script>
    <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>
  <%--  <script src="assets/js/jquery.nice-select.min.js" type="text/javascript"></script>--%>
    <script src="assets/js/main.js" type="text/javascript"></script>
      <% if (Session["sf_type"].ToString() == "3")
        { %>
    <script src="https://sanebilling.info/JScript/sanbilling.js?v=<%= DateTime.Now.Ticks.ToString() %>" data-san-billing-divisions="<%= Session["division_code"].ToString() %>" data-san-billing-tenant-id="sansfaTablets"></script>
    <% } %>
</body>
</html>
