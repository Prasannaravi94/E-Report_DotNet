<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task_Assign.aspx.cs" Inherits="MasterFiles_Task_Management_New_Task_Assign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Task | Assign</title>
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnAssign').click(function () {
                var mode = $('#<%=ddlmode.ClientID%> :selected').text();
                if (mode == "---Select---") { alert("Select Mode."); $('#ddlmode').focus(); return false; }
                var Prior = $('#<%=ddlPri.ClientID%> :selected').text();
                if (Prior == "--Select--") { alert("Select Priority."); $('#ddlPri').focus(); return false; }

                var sf = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (sf == "---Select---") { alert("Select Task Assign To."); $('#ddlFieldForce').focus(); return false; }

                if ($("#txt_Date").val() == "") { alert("Enter DeadLine From."); $('#txt_Date').focus(); return false; }

                if ($("#to_Date").val() == "") { alert("Enter DeadLine To."); $('#to_Date').focus(); return false; }

                var from = document.getElementById("txt_Date").value;
                var to = document.getElementById("to_Date").value;



                if (Date.parse(to) < Date.parse(from)) {
                    alert("Deadline to must be greater than Deadline From");
                    return false;
                }

                if ($("#txtdes").val() == "") { alert("Enter Description."); $('#txtdes').focus(); return false; }

            });
        });
    </script>
     <script type="text/javascript">

         function preventMultipleSubmissions() {
             $('#<%=btnAssign.ClientID %>').prop('disabled', true);
         }

         window.onbeforeunload = preventMultipleSubmissions;

    </script>
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
    <form id="form1" runat="server" autocomplete="off">
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
									<li class="nav-item">
										<a class="nav-link active" href="Task_Assign.aspx" onclick="ShowProgress();">Assign</a>
									</li>
									<li class="nav-item">
										<a class="nav-link" href="Task_Status.aspx" onclick="ShowProgress();">Status</a>
									</li>
									<li class="nav-item">
										<a class="nav-link" href="Task_Tracking.aspx" onclick="ShowProgress();">Track</a>
									</li>
									<li class="nav-item">
										<a class="nav-link" href="#" runat="server" onserverclick="Back_Click" onclick="ShowProgress();">Exit</a>
									</li>
									
									<%--<li class="nav-item">
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
    <!---- Header end --->
    <!----- Task Assignment Start ----->
    <section class="task-assignment-area clearfix">
			<div class="container">
				<div class="row justify-content-center">
					<div class="col-lg-8">
						<div class="task-assignment-list">
							<h2>Task – Assignment</h2>
							<!---- Form start ---->
							<form action="" method="post">
								<div class="row clearfix">
									<div class="col-lg-7">
										<p>
											<label for="">Mode of Task</label>
										
                                           <asp:DropDownList ID="ddlmode" CssClass="select-wrapper" runat="server" >
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
											<label for="">Task Assign to</label>
										 <asp:DropDownList ID="ddlFieldForce" runat="server" >
                    </asp:DropDownList>
                      <asp:DropDownList ID="ddlSF" runat="server" Visible="false">
                    </asp:DropDownList>
										</p>
									</div>
									<div class="col-lg-5">
										<label for="">Deadline </label>
										<div class="row clearfix">
											<div class="col-lg-6">
											  <asp:TextBox ID="txt_Date" runat="server"  CssClass="DOBDate"></asp:TextBox>
											</div>
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
								<div class="row text-center clearfix">
									<div class="col-lg-12">
										
          
               <asp:Button ID="btnAssign" runat="server" Text="Assign" CssClass="submitBtn"
            OnClick="btnAssign_Click" />
         
						  <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="resetBtn"
            OnClick="btnClear_Click" />			 
                                     
									</div>
								</div>
							</form>
							<!---- Form End ---->
						</div>
					</div>
				</div>
			</div>
		</section>
    <!----- Task Assignment End ----->
    <script src="assets/js/jQuery.min.js"></script>
    <script src="assets/js/popper.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/main.js"></script>
             <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
        <div class="loading" align="center">
       
        <img id="Img1" src="~/Images/loading/Source2.gif" runat="server" alt="" />
    </div>
    </form>
</body>
</html>
