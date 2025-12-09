<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task_Update.aspx.cs" Inherits="MasterFiles_Task_Management_Task_Update" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>Task Management</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Demo CSS file not needed for the plugin -->
    <link href="https://fonts.googleapis.com/css?family=Lato:400,700" rel="stylesheet">
    <link rel="stylesheet" href="css-demo-page.css">
    <!-- GRT Youtube Plugin CSS -->
    <link rel="stylesheet" href="grt-responsive-menu.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        .title
        {
            display: block;
            float: left;
            text-align: left;
            width: auto;
        }
        .header h1
        {
            font-weight: 700;
            margin: 0px;
            padding: 0px 0px 0px 20px;
            color: #f9f9f9;
            border: none;
            line-height: 2em;
            font-size: 2em;
        }
        h1, h2, h3, h4, h5, h6
        {
            font-size: 1.6em;
            color: #666666;
            font-variant: small-caps;
            text-transform: none;
            font-weight: bolder;
            margin-bottom: 0px;
        }
        td.stylespc
        {
            padding-bottom: 15px;
            padding-right: 15px;
        }
        .mydropdownlist
        {
            font-size: 12px;
            padding: 4px 8px;
            border-radius: 4px;
            font-weight: bold;
            font-family: Verdana;
        }
        .txtbox
        {
            font-size: 14px;
            padding: 5px 10px;
            border-radius: 5px;
            font-weight: bold;
            font-family: Verdana;
        }
         .modal
        {
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
    
    <script type="text/javascript">

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.DOBDate').datepicker
            ({
                changeMonth: true,
                changeYear: true,

                yearRange: '1930:' + new Date().getFullYear().toString(),
                //                yearRange: "2010:2017",
                dateFormat: 'dd/mm/yy'
            });
        });
        var j = jQuery.noConflict();
        j(document).ready(function () {
            var date = new Date();
            var currentMonth = date.getMonth();
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();
            j('.CompDate').datepicker
            ({
                changeMonth: true,
                changeYear: true,

                yearRange: '1930:' + new Date().getFullYear().toString(),

                //                yearRange: "2010:2017",
                dateFormat: 'dd/mm/yy',
                minDate: new Date(currentYear, currentMonth, currentDate - 15)
            });
        });
            
    </script>
    <script type="text/javascript">
        function validate() {
            var txtReason = document.getElementById('<%=txtReason.ClientID %>').value;
            if (txtReason == "") {
                alert("Please Enter the Reason");
                document.getElementById('<%=txtReason.ClientID %>').focus();
                return false;
            }

            if (confirm('Do you want to Submit?')) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnComp').click(function () {
                if ($("#txtComment").val() == "") { alert("Enter Comments."); $('#txtComment').focus(); return false; }

                if ($("#txtC_Date").val() == "") { alert("Enter Completed Date."); $('#txtC_Date').focus(); return false; }
            });
            $('#btnUpdate').click(function () {

                if ($("#txt_Date").val() == "") { alert("Enter DeadLine From."); $('#txt_Date').focus(); return false; }
                if ($("#to_Date").val() == "") { alert("Enter DeadLine To."); $('#to_Date').focus(); return false; }
                var from = $("#txt_Date").val();
                var to = $("#to_Date").val();

                if (from > to) {
                    alert("Deadline to must be greater than Deadline From");
                    return false;
                }

                if ($("#txtdes").val() == "") { alert("Enter Description."); $('#txtdes').focus(); return false; }

            });
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
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <header>
			<div class="menu-container">
				<div class="grt-menu-row">
					<div class="title">
					   <h1 style="color:BlueViolet">
                   Task Management System <asp:Label ID="lblsf" Font-Size="16px" Font-Bold="true" ForeColor="Green" runat="server" ></asp:Label>
                </h1>
					</div>
					<div class="grt-menu-right">
						<nav>
							<button class="grt-mobile-button"><span class="line1"></span><span class="line2"></span><span class="line3"></span></button>
							<ul class="grt-menu">
								<li ><a href="Task2.aspx" onclick="ShowProgress();">Home</a></li>
								<li id="liassign" runat="server"><a href="Task_Assign.aspx" onclick="ShowProgress();">Assign</a></li>
								<li class="active"><a href="Task_Status.aspx" onclick="ShowProgress();">Status</a></li>
								<li><a href="Task_Tracking.aspx" onclick="ShowProgress();">Track</a></li>
								<li><a id="back" href="#" runat="server"  onserverclick="Back_Click" onclick="ShowProgress();" >Back</a>
									</li>
								
													
							</ul>
						</nav>
					</div>
				</div>
			</div>
		</header>
        <%--	<div class="demo-page-container">
			<!-- Github Button -->
			<div class="github-position">
				<a class="github-button" href="https://github.com/grt107/grt-responsive-menu/archive/master.zip" data-icon="octicon-cloud-download" data-size="large" aria-label="Download grt107/grt-responsive-menu on GitHub">Download</a>
				<a class="github-button" href="https://github.com/grt107" data-size="large" aria-label="Follow @grt107 on GitHub">Follow @grt107</a>
			</div>--%>
    </div>
    <br />
    <br />
    <br />
    <center>
        <table width="100%" border="0">
            <tr>
                <td align="left" style="width: 35%">
                </td>
                <td align="center" style="width: 30%">
                    <h2 id="head" runat="server" style="color: White">
                        Task - Assignment
                    </h2>
                </td>
                <td align="right" style="padding-right: 10px; padding-top: 10px;">
                    <asp:ImageButton ID="btnBack" ImageUrl="~/Images/Back_T.png" runat="server" Width="60px"
                        OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table style="vertical-align: top">
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label2" runat="server" Text="Mode of Task " Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    :
                </td>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblmode" runat="server" Font-Bold="true" Font-Size="16px" Font-Names="verdana"
                        ForeColor="white"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-bottom: 10px; padding-right: 10px;">
                    <asp:Label ID="Label5" runat="server" Text="Task Assign By " Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    :
                </td>
                <td align="left" style="padding-bottom: 10px; padding-right: 10px;">
                    <asp:Label ID="lblAssignFrom" runat="server" Font-Bold="true" Font-Size="16px" Font-Names="verdana"
                        ForeColor="white"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-bottom: 10px; padding-right: 10px;">
                    <asp:Label ID="lblFilter" runat="server" Text="Task Assign To" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    :
                </td>
                <td align="left" style="padding-bottom: 10px; padding-right: 10px;">
                    <asp:Label ID="lblAssignTo" runat="server" Font-Bold="true" Font-Size="16px" Font-Names="verdana"
                        ForeColor="white"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label1" runat="server" Text="Priority" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    :
                </td>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblprior" runat="server" Font-Bold="true" Font-Size="16px" Font-Names="verdana"
                        ForeColor="white"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label3" runat="server" Text="Dead Line From / To" Font-Bold="true"
                        Font-Size="16px" Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    :
                </td>
                <td align="left" class="stylespc">
                    <asp:TextBox ID="txt_Date" runat="server" Width="150" Height="30px" Font-Names="verdana"
                        Font-Bold="true" Font-Size="14px" CssClass="DOBDate"></asp:TextBox>
                    &nbsp; / &nbsp;
                    <asp:TextBox ID="to_Date" runat="server" Width="150" Height="30px" Font-Names="verdana"
                        Font-Bold="true" Font-Size="14px" CssClass="DOBDate"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label4" runat="server" Text="Task Description" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    :
                </td>
                <td valign="middle" style="height: 25px;">
                    <asp:TextBox ID="txtdes" runat="server" TextMode="MultiLine" Rows="7" Columns="55"></asp:TextBox>
                </td>
            </tr>
            <tr id="trcomm" runat="server" visible="false">
                <td align="left" class="stylespc">
                    <asp:Label ID="lblCom" runat="server" Text="Comments" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <span id="col" runat="server">: </span>
                </td>
                <td valign="middle" style="height: 25px;">
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="7" Columns="55"></asp:TextBox>
                </td>
            </tr>
            <tr id="trcomp" runat="server" visible="false">
                <td align="left" class="stylespc">
                    <asp:Label ID="lblCdate" runat="server" Text="Completed Date" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <span id="sdate" runat="server">: </span>
                </td>
                <td valign="middle" style="height: 25px;">
                    <asp:TextBox ID="txtC_Date" runat="server" Width="150" Height="30px" Font-Names="verdana"
                        Font-Bold="true" Font-Size="14px" CssClass="CompDate"></asp:TextBox>
                </td>
            </tr>
            <tr id="trreason" runat="server" visible="false">
                <td align="left" class="stylespc">
                    <asp:Label ID="lblCReason" runat="server" Text="Reason" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <span id="Span1" runat="server">: </span>
                </td>
                <td valign="middle" style="height: 25px;">
                    <asp:TextBox ID="txtCReason" runat="server" TextMode="MultiLine" Rows="5" Columns="50"></asp:TextBox>
                </td>
            </tr>
            <tr id="trreasonD" runat="server">
                <td align="left" class="stylespc">
                    <asp:Label ID="lblRDate" runat="server" Text="Reason Date" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <span id="Span2" runat="server">: </span>
                </td>
                <td valign="middle" style="height: 25px;">
                    <asp:Label ID="lblReasonDate" runat="server" Font-Bold="true" Font-Size="16px" Font-Names="verdana"
                        ForeColor="white"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" Visible="false"
            Height="25px" OnClick="btnUpdate_Click" />
        &nbsp;
        <asp:Button ID="btnComp" runat="server" Text="Completed" Width="100px" Visible="false"
            Height="25px" OnClick="btnComp_Click" />
        &nbsp;
        <asp:Button ID="btnClose" runat="server" Text="Close" Width="80px" Visible="false"
            Height="25px" OnClick="btnClose_Click" />
        &nbsp;
        <asp:Button ID="btnHold" runat="server" Text="Hold" Width="70px" Visible="false"
            Height="25px" OnClick="btnHold_Click" />
        &nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" Visible="false"
            Height="25px" OnClick="btnCancel_Click" />
        &nbsp;
        <asp:Button ID="btnReopen" runat="server" Text="Re Open" Width="90px" Visible="false"
            Height="25px" OnClick="btnReopen_Click" />
        &nbsp;
        <asp:Label ID="lblReason" Text="Reason : " Visible="false" Font-Bold="true" Font-Size="16px"
            Font-Names="verdana" ForeColor="white" runat="server"></asp:Label>
        &nbsp;
        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" BorderStyle="Solid"
            BorderColor="Gray" Visible="false" Height="70px" Width="350px"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Width="80px" Visible="false"
            OnClientClick="return validate();" Height="25px" OnClick="btnConfirm_Click" />
        <%-- <asp:Button ID="btnClear" runat="server" Text="CLEAR" Width="80px" Height="25px" /> --%>
    </center>
    <!-- Jquery -->
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <!-- Github button for demo page -->
    <script async defer src="https://buttons.github.io/buttons.js"></script>
    <!-- GRT Youtube Popup -->
    <script src="grt-responsive-menu.js"></script>
    <script type="text/javascript" src="jquery-ui-1.11.4/external/jquery/jquery.js"></script>
    <script type="text/javascript" src="jquery-ui-1.11.4/jquery-ui.min.js"></script>
    <link href="jquery-ui-1.11.4/jquery-ui.min.css" rel="Stylesheet" />
    </form>
        <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img id="Img1" src="~/Images/loader.gif" runat="server" alt="" />
    </div>
</body>
</html>
