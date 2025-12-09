<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task_Edit.aspx.cs" Inherits="MasterFiles_Task_Management_Task_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>Task Management</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/png" href="assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700,900&display=swap"
        rel="stylesheet">
    <link rel="stylesheet" href="assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/css/style.css">
    <link rel="stylesheet" href="assets/css/responsive.css">
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
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
            
    </script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
      <style type="text/css">
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
       
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
     
        z-index: 999;
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
</head>
<body>
    <form id="form1" runat="server">
    <header class="header-area clearfix">
			<div class="container-fluid">
				<div class="row">
					<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
						<!---- Menu area start ---->
						<nav class="navbar navbar-expand-md navbar-light p-0">
							<a class="navbar-brand" href="Task2.aspx"><img src="assets/images/logo.png" alt="" /></a>
							<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
								<span class="navbar-toggler-icon"></span>
							</button>
							<div class="collapse navbar-collapse justify-content-end main-menu" id="collapsibleNavbar">
								<ul class="navbar-nav">
									<li class="nav-item">
										<a class="nav-link" href="Task2.aspx" onclick="ShowProgress();">Home</a>
									</li>
									<li class="nav-item" id="liassign" runat="server">
										<a class="nav-link " href="Task_Assign.aspx" onclick="ShowProgress();">Assign</a>
									</li>
									<li class="nav-item">
										<a class="nav-link active" href="Task_Status.aspx" onclick="ShowProgress();">Status</a>
									</li>
									<li class="nav-item">
										<a class="nav-link" href="Task_Tracking.aspx" onclick="ShowProgress();">Track</a>
									</li>
									<li class="nav-item">
										<a class="nav-link" href="#" runat="server" onserverclick="Back_Click" onclick="ShowProgress();">Exit</a>
									</li>
									
								<%--	<li class="nav-item">
										<a class="nav-link login" href="../../Index.aspx" onclick="ShowProgress();">Log Out</a>
									</li>--%>
								</ul>
							</div>
						</nav> 
						<!---- Menu area end ---->
					</div>
				</div>
			</div>
		</header>
    <%--  <center>
        <table width="100%" border="0">
            <tr>
                <td align="left" style="width: 45%">
                </td>
                <td>
                    <h2 id="head" runat="server" style="color: White">
                        Task - Assignment
                    </h2>
               </td>
                <td align="right" style="padding-right:10px;padding-top:10px;">
                    <asp:ImageButton ID="btnBack" ImageUrl="~/Images/Back_T.png" runat="server" Width="60px"
                        OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table style="vertical-align: top">
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label2" runat="server" Text="Mode of Task" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlmode" runat="server" CssClass="mydropdownlist">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label5" runat="server" Text="Task Assign By" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
              
                        <asp:Label ID="lblAssignfrom" runat="server" Font-Bold="true" Font-Size="16px" Font-Names="verdana"
                        ForeColor="white"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFilter" runat="server" Text="Task Assign To" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
               
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
                    <asp:DropDownList ID="ddlPri" runat="server" CssClass="mydropdownlist">
                        <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="H" Text="High"></asp:ListItem>
                        <asp:ListItem Value="M" Text="Medium"></asp:ListItem>
                        <asp:ListItem Value="L" Text="Low"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label3" runat="server" Text="Dead Line From / To" Font-Bold="true"
                        Font-Size="16px" Font-Names="verdana" ForeColor="white"></asp:Label>
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
                <td valign="middle" style="height: 30px;">
                    <asp:TextBox ID="txtdes" runat="server" TextMode="MultiLine" Rows="15" Columns="80"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
     
    </center>--%>
    <section class="task-assignment-area clearfix">
  <a href="Task_Status.aspx" class="btn btn-light float-right">Back</a>
 
			<div class="container">     
				<div class="row justify-content-center">
					<div class="col-lg-8">
						<div class="task-assignment-list">
							<h2>Task – View</h2>
                            
							<!---- Form start ---->
                      
							<form action="" method="post">
								<div class="row clearfix">
									<div class="col-lg-7">
										<p>
											<label for="">Mode of Task</label>
											
                                           <asp:DropDownList ID="ddlmode" runat="server" >
                    </asp:DropDownList>
										</p>
									</div>
									<div class="col-lg-5">
										<p>
											<label for="">Priority</label>
										  <asp:DropDownList ID="ddlPri" runat="server">
                        <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="H" Text="High"></asp:ListItem>
                        <asp:ListItem Value="M" Text="Medium"></asp:ListItem>
                        <asp:ListItem Value="L" Text="Low"></asp:ListItem>
                    </asp:DropDownList>
										</p>
									</div>
								</div>
								<div class="row clearfix">
									<div class="col-lg-7">
										<p>
											<label for="">Task Assign from</label>
										  <asp:Label ID="lblAssignfrom" runat="server" 
                        ></asp:Label>
										</p>
									</div>
									<div class="col-lg-5">
										<label for="">Deadline From</label>
										<div class="row clearfix">
											<div class="col-lg-6">
											  <asp:TextBox ID="txt_Date" runat="server"  CssClass="DOBDate"></asp:TextBox>
											</div>
											
										</div>
									</div>
								</div>
                                	<div class="row clearfix">
									<div class="col-lg-7">
										<p>
											<label for="">Task Assign to</label>
										  <asp:Label ID="lblAssignTo" runat="server" 
                        ></asp:Label>
										</p>
									</div>
                                 
                                    <div class="col-lg-5">
										<label for="">Deadline To</label>
										<div class="row clearfix">
											<div class="col-lg-6">
												 <asp:TextBox ID="to_Date" runat="server" CssClass="DOBDate"></asp:TextBox>
											</div>
                                            </div>
                                    </div>
                                       </div>
								<div class="row clearfix">
									<div class="col-lg-12">
										<p>
											<label for="">Task Description</label>
											
                                            <asp:TextBox id="txtdes" TextMode="multiline" Columns="30" Rows="10" runat="server" />
										</p>
									</div>
								</div>
								
							</form>
							<!---- Form End ---->
						</div>
					</div>
				</div>
			</div>
		</section>
    <!-- Jquery -->
    <script src="assets/js/jQuery.min.js"></script>
    <script src="assets/js/popper.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/main.js"></script>
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
     <div class="loading" align="center">
     
        <img id="Img1" src="~/Images/loading/source2.gif" runat="server" alt="" />
    </div>
    </form>
</body>
</html>
