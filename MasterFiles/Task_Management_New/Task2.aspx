<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task2.aspx.cs" Inherits="MasterFiles_Task_Management_New_Task2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <%-- <style type="text/css">
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
.flex-container {
  display: flex;
  flex-wrap: wrap;


}


.flex-container > div {
  background-color: #f1f1f1;
 
  width: 160px;
  margin: 15px;
  text-align: center;
  line-height: 40px;
  font-size: 20px;
  font-weight: bolder;
}

.notification {
  background-color: lightblue;
  color: white;
  text-decoration: none;
  padding: 30px 41px;
  position: relative;
  display: inline-block;
  border-radius: 2px;
}

.notification:hover {
  background: blue;
}

.notification .badge {
  position: absolute;
  top: -10px;
  right: -10px;
  padding: 5px 10px;
  border-radius: 50%;
  background: red;
  color: white;
}

.notification .badge :hover
{
      background: green;
}
#box {
	float: left;
	height: 80px;
	width: 250px;
	margin: 20px;
	padding: 10px;
	border: solid;
	border-radius: 10px;
	background: #000;
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
  </style>--%>
    <!-- Add icon library -->
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
<style>
.btn {
  background-color: DodgerBlue;
  border: none;
  color: white;
  padding: 30px 20px;
  font-size: 16px;
  cursor: pointer;
}

/* Darker background on mouse-over */
.btn:hover {
  background-color: RoyalBlue;
}



.header2
{
    position: relative;

    background: teal;
    width: 100%;
}

.header2 h1
{
    font-weight: 300;
  
    padding: 0px 0px 0px 20px;
    color: #f9f9f9;
    border: none;
    line-height: 1.5em;
    font-size: 1.5em;
}
   .chartCont
        {
            padding: 0px 4px;
        }
        .border-bottom
        {
            border-bottom: 1px dashed rgba(0, 117, 194, 0.2);
        }
        .border-right
        {
            border-right: 1px dashed rgba(0, 117, 194, 0.2);
        }
           #content div
        {
            display: inline-block;
        }
        #content > div
        {
            margin: 0px 5px;
        }
        #content > div:nth-child(1) > div
        {
            margin: 5px 0 0;
        }
        #content > div:nth-child(2) > div
        {
            margin: 0 0 5px;
        }
</style>--%>
    <script src="../../DashBoard/JS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../DashBoard/js1/fusioncharts.js" type="text/javascript"></script>
    <script src="../../DashBoard/js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var sf_type = '<%=Session["sf_type"] %>';
          
                var caption = "My Task Detail";
                if (sf_type == 1) {
                    caption = "My Task Detail";
                }
                else {
                    caption = "Team (Assigned by Me)";
                }

              
              
                var Data = "";
                $.ajax({

                    type: 'POST',

                    url: "Task2.aspx/TaskDet",

                    contentType: "application/json; charset=utf-8",

                    dataType: "json",
                    data: '{objData:' + JSON.stringify(Data) + '}',

                    success: function (data) {

                        var chartData = eval("(" + data.d + ')');
                        var fusioncharts = new FusionCharts({

                            "type": "radar",
                            "renderAt": "chart-container",

                            "dataFormat": "json",
                            "dataSource": {
                                "chart": {
                                    "caption": "My Team Task",
                                    "subcaption": "",


                                    "paletteColors": "#0077ff",
                                    "theme": "fint",
                                    "divLineDashed": "0",

                                    "paletteColors": "#0077ff",
                                    "showBorder": "0",
                                    "bgColor": "FFFFFF",
                                    "animation": "1",
                                    "radarfillcolor": "#ffffff",
                                    "labelDisplay": "rotate"

                                },


                                "categories": [{
                                    "category": chartData
                                }],
                                "dataset": [{
                                    "data": chartData
                                }]


                            }

                        }

            );

                        fusioncharts.render();

                    },

                    error: function (xhr, ErrorText, thrownError) {

                        alert("Error: No Data Found!");

                    }

                });

                var Data = "";
                $.ajax({

                    type: 'POST',

                    url: "Task2.aspx/TaskPrior",

                    contentType: "application/json; charset=utf-8",

                    dataType: "json",

                    data: '{objData:' + JSON.stringify(Data) + '}',
                    success: function (data) {

                        var chartData = eval("(" + data.d + ')');

                        var fusioncharts = new FusionCharts({


                            type: 'bar3d',
                            renderAt: 'chart-container2',

                            width:'350',
                            dataFormat: 'json',

                            dataSource: {

                                "chart": {
                                    "caption": "Priority",
                                    "theme": "fint",
                                    "maxBarHeight": "20",
                                    "paletteColors": "#0077ff",
                                    "showBorder": "0",
                                    "bgColor": "FFFFFF",
                                    "animation": "1",
                                    "divLineDashed": "0",
                                    "xAxisPosition": "top",
                                    
                                    "alignCaptionWithCanvas": "0"
                                
                                },


                                "data": chartData

                            }

                        }

            );

                        fusioncharts.render();

                    },

                    error: function (xhr, ErrorText, thrownError) {

                        $("#chart-container2").html(xhr.responseText);

                    }

                });

                $.ajax({

                    type: 'POST',

                    url: "Task2.aspx/TaskDetTeam",

                    contentType: "application/json; charset=utf-8",

                    dataType: "json",
                    data: '{objData:' + JSON.stringify(Data) + '}',

                    success: function (data) {

                        var chartData = eval("(" + data.d + ')');
                        var fusioncharts = new FusionCharts({

                            "type": "spline",
                            "renderAt": "chart-container3",

                            "dataFormat": "json",
                            "dataSource": {
                                "chart": {

                                    "caption": caption,
                                    "subcaption": "",

                                    "placeValuesInside": "0",


                                    //Configure scrollbar
                                    "theme": "fint",
                                    "divLineDashed": "0",
                                    "divLineColor": "#FFFFFF",
                                    "paletteColors": "#0077ff",
                                    "showBorder": "0",
                                    "bgColor": "FFFFFF",
                                    "animation": "1",
                                    "labelDisplay": "rotate"
                                },


                                "categories": [{
                                    "category": chartData
                                }],
                                "dataset": [{
                                    "data": chartData
                                }]


                            }

                        }

            );

                        fusioncharts.render();

                    },

                    error: function (xhr, ErrorText, thrownError) {

                        alert("Error: No Data Found!");

                    }

                });

                var jsonData = '';
                $.ajax({
                    type: "POST",
                    url: "Task2.aspx/getTaskVal",
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_,
                    error: OnErrorCall_
                });

                function OnSuccess_(response) {
                    var aData = response.d;
                    var arr = [];
                    var Total, Open, New, Completed, Closed, ReOpen, Hold, Cancel, Due;

                    $.map(aData, function (item, index) {
                        Total = item.Total; Open = item.Open; New = item.New; Completed = item.Completed; Closed = item.Closed; ReOpen = item.Reopen; Hold = item.Hold; Cancel = item.Cancel; Due = item.Due;



                    });
                    var myJsonString = JSON.stringify(arr);
                    var jsonArray = JSON.parse(JSON.stringify(arr));
                    document.getElementById("lblNew").textContent = New
                    document.getElementById("lblopen").textContent = Open
                    document.getElementById("lblComp").textContent = Completed
                    document.getElementById("lblClosed").textContent = Closed
                    document.getElementById("lblReopen").textContent = ReOpen
                    document.getElementById("lblHold").textContent = Hold
                    document.getElementById("lblTot").textContent = Total

                    BulbDue(Due);

                }
                function OnErrorCall_(response) {
                    alert("Error: No Data Found!");
                }
                e.preventDefault();
            
        });
    </script>
    <script type="text/javascript">
        function BulbDue(Due) {
            FusionCharts.ready(function () {
                var chartObj = new FusionCharts({
                    type: 'angulargauge',
                    renderAt: 'char2',
                    width: '300',
                    dataFormat: 'json',
                    dataSource: {
                        "chart": {
                            "caption": "Overdue Days",
                            captionpadding: "0",
                            origw: "320",
                            origh: "300",
                            gaugeouterradius: "105",
                            gaugestartangle: "250",
                            gaugeendangle: "-25",
                            showvalue: "1",
                            valuefontsize: "30",
                            majortmnumber: "13",
                            majortmthickness: "2",
                            majortmheight: "13",
                            minortmheight: "7",
                            minortmthickness: "1",
                            minortmnumber: "1",
                            showgaugeborder: "0",
                            showBorder: "0",
                            bgColor: "FFFFFF",
                            theme: "fint"

                        },
                        colorrange: {
                            color: [
      {
          minvalue: "0",
          maxvalue: "110",
          code: "#0077ff"
      },
      {
          minvalue: Due,
          maxvalue: "280",
          code: "#F6F6F6"
      }
    ]
                        },
                        dials: {
                            dial: [
      {
          value: Due,
          bgcolor: "#F20F2F",
          basewidth: "8"
      }
    ]
                        }
                    }
                });


                chartObj.render();
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <%--    <div>
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
								<li class="active"><a href="Task2.aspx" onclick="ShowProgress();">Home</a></li>
								<li id="liassign" runat="server"><a href="Task_Assign.aspx" onclick="ShowProgress();">Assign</a></li>
								<li><a href="Task_Status.aspx" onclick="ShowProgress();">Status</a></li>
								<li><a href="Task_Tracking.aspx" onclick="ShowProgress();">Track</a></li>
								<li><a id="back" href="#" runat="server"  onserverclick="Back_Click" onclick="ShowProgress();" >Back</a>
									</li>
								
													
							</ul>
						</nav>
					</div>
				</div>
			</div>
		</header>
		
	

		
		</div>
        <br />
        <br />
        <br />
        <br />
        <br />
     
        <div class="flex-container">
  <div><span style="font-size:30px;color:Green" ><i class="fa fa-suitcase";></i></span> <br /> <span style="color:BlueViolet"  > Total -</span><asp:Label ID="lblTot" runat="server" style="color:Green"></asp:Label></div>
  <div><span style="font-size:30px;color:Green" ><i class="fa fa-plus-square";></i></span>  <br /><span style="color:BlueViolet"  > New -</span><asp:Label ID="lblNew" runat="server" style="color:Green"></asp:Label></div>
   <div><span style="font-size:30px;color:Green" ><i class="fa fa-folder-open";></i></span> <br /><span style="color:BlueViolet"  > Pending -</span><asp:Label ID="lblopen" runat="server" style="color:Green"></asp:Label></div>
    <div><span style="font-size:30px;color:Green" ><i class="fa fa-thumbs-up";></i></span> <br /><span style="color:BlueViolet"  > Completed -</span><asp:Label ID="lblComp" runat="server" style="color:Green"></asp:Label></div>
    <div><span style="font-size:30px;color:Green" ><i class="fa fa-folder";></i></span> <br /><span style="color:BlueViolet"  > Closed -</span><asp:Label ID="lblClosed" runat="server" style="color:Green"></asp:Label></div>
      <div><span style="font-size:30px;color:Green" ><i class="fa fa-refresh";></i></span> <br /><span style="color:BlueViolet"  > Re Open -</span><asp:Label ID="lblReopen" runat="server" style="color:Green"></asp:Label></div>
            <div><span style="font-size:30px;color:Green" ><i class="fa fa-stop-circle";></i></span> <br /><span style="color:BlueViolet"  > On Hold -</span><asp:Label ID="lblHold" runat="server" style="color:Green"></asp:Label></div>


</div>--%>
    <header class="header-area clearfix">
			<div class="container-fluid">
				<div class="row">
					<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
						<!---- Menu area start ---->
						<nav class="navbar navbar-expand-md navbar-light p-0">
							<a class="navbar-brand" href="#"><img src="assets/images/logo.png" alt="" /></a>
							<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
								<span class="navbar-toggler-icon"></span>
							</button>
							<div class="collapse navbar-collapse justify-content-end main-menu" id="collapsibleNavbar">
								<ul class="navbar-nav">
									<li class="nav-item">
										<a class="nav-link active" href="Task2.aspx" onclick="ShowProgress();">Home</a>
									</li>
									<li class="nav-item" id="liassign" runat="server" onclick="ShowProgress();">
										<a class="nav-link" href="Task_Assign.aspx">Assign</a>
									</li>
									<li class="nav-item">
										<a class="nav-link" href="Task_Status.aspx" onclick="ShowProgress();">Status</a>
									</li>
									<li class="nav-item">
										<a class="nav-link" href="Task_Tracking.aspx" onclick="ShowProgress();">Track</a>
									</li>
									<li class="nav-item">
										<a class="nav-link" onclick="ShowProgress();" href="#" runat="server" onserverclick="Back_Click">Exit</a>
									</li>
								<%--	
									<li class="nav-item">
										<a class="nav-link login" onclick="ShowProgress();" href="../../Index.aspx">Log Out</a>
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
    <!----- Catagory area start ----->
    <section class="cat-area text-center clearfix">
			<div class="container-fluid">
				<div class="row">
					<div class="col">
						<div class="single-cat-area clearfix">
							<p>Total</p>
							<div class="single-cat-icon clearfix">
								<img src="assets/images/icon-1.png" alt="" />
							</div>
							<div class="single-cat-nubber clearfix">
                          <a href="Task_Status.aspx" style="color:White">
							<asp:Label ID="lblTot" runat="server"></asp:Label></a>
							</div>
						</div>
					</div>
					<div class="col">
						<div class="single-cat-area clearfix">
							<p>New</p>
							<div class="single-cat-icon clearfix">
								<img src="assets/images/icon-2.png" alt="" />
							</div>
							<div class="single-cat-nubber clearfix">
                             <a href="Task_Status.aspx?type=New" style="color:White">
								<asp:Label ID="lblNew" runat="server"></asp:Label></a>
							</div>
						</div>
					</div>
					<div class="col">
						<div class="single-cat-area clearfix">
							<p>Pending</p>
							<div class="single-cat-icon clearfix">
								<img src="assets/images/icon-3.png" alt="" />
							</div>
							<div class="single-cat-nubber clearfix">
                                 <a href="Task_Status.aspx?type=Open" style="color:White">
								<asp:Label ID="lblopen" runat="server"></asp:Label></a>
							</div>
						</div>
					</div>
					<div class="col">
						<div class="single-cat-area clearfix">
							<p>Completed</p>
							<div class="single-cat-icon clearfix">
								<img src="assets/images/icon-4.png" alt="" />
							</div>
							<div class="single-cat-nubber clearfix">
                                 <a href="Task_Status.aspx?type=Com" style="color:White">
								<asp:Label ID="lblComp" runat="server"></asp:Label></a>
							</div>
						</div>
					</div>
					<div class="col">
						<div class="single-cat-area clearfix">
							<p>Closed</p>
							<div class="single-cat-icon clearfix">
								<img src="assets/images/icon-5.png" alt="" />
							</div>
							<div class="single-cat-nubber clearfix">
                              <a href="Task_Status.aspx?type=Close" style="color:White">
							<asp:Label ID="lblClosed" runat="server"></asp:Label>
                            </a>
							</div>
						</div>
					</div>
					<div class="col">
						<div class="single-cat-area clearfix">
							<p>ReOpen</p>
							<div class="single-cat-icon clearfix">
								<img src="assets/images/icon-6.png" alt="" />
							</div>
							<div class="single-cat-nubber clearfix">
                              <a href="Task_Status.aspx?type=Reopen" style="color:White">
								<asp:Label ID="lblReopen" runat="server"></asp:Label></a>
							</div>
						</div>
					</div>
					<div class="col">
						<div class="single-cat-area clearfix">
							<p>On Hold</p>
							<div class="single-cat-icon clearfix">
								<img src="assets/images/icon-7.png" alt="" />
							</div>
							<div class="single-cat-nubber clearfix">
                             <a href="Task_Status.aspx?type=Hold" style="color:White">
							<asp:Label ID="lblHold" runat="server"></asp:Label></a>
							</div>
						</div>
					</div>
				</div>
			</div>
		</section>
    <section class="task-list-area clearfix">
			<div class="container-fluid">
				<%--<div class="row clearfix">
					<div class="col-lg-12">
						<h2>Task Details (Priority)</h2>
					</div>
				</div>--%>
                <br />
				<div class="row clearfix">
					<div class="col-lg-3">
					
							<!--<h3>Team (Assigned by Me)</h3>
							<p><img src="assets/images/chat.png" alt="" /></p>
							<a href="#">Projects</a>-->
                               <div id='chart-container3'>
                                                                    </div>
						
					</div>
					<div class="col-lg-3">
						
						 <div id='chart-container'>
                                                                    </div>
					
					</div>
					<div class="col-lg-3">
						
                          <div id='chart-container2'>
                                                                    </div>
							<!--<h3>Priority</h3>
							<h1>87%</h1>
							<p>of projects have Medium priority type</p>
							<div class="single-rate-area clearfix">
								<p><strong>High</strong></p>
								<div class="rate-sing-space check-color"></div>
								<div class="rate-sing-space check-color"></div>
								<div class="rate-sing-space check-color"></div>
								<div class="rate-sing-space check-color"></div>
								<div class="rate-sing-space"></div>
								<div class="clearfix">
									<div class="left-rate float-left">0</div>
									<div class="right-rate float-right">5</div>
								</div>
							</div>
							<div class="single-rate-area clearfix">
								<p><strong>Medium</strong></p>
								<div class="rate-sing-space check-color"></div>
								<div class="rate-sing-space check-color"></div>
								<div class="rate-sing-space check-color"></div>
								<div class="rate-sing-space"></div>
								<div class="rate-sing-space"></div>
								<div class="clearfix">
									<div class="left-rate float-left">0</div>
									<div class="right-rate float-right">5</div>
								</div>
							</div>
							<div class="single-rate-area clearfix">
								<p><strong>Low</strong></p>
								<div class="rate-sing-space check-color"></div>
								<div class="rate-sing-space"></div>
								<div class="rate-sing-space"></div>
								<div class="rate-sing-space"></div>
								<div class="rate-sing-space"></div>
								<div class="clearfix">
									<div class="left-rate float-left">0</div>
									<div class="right-rate float-right">5</div>
								</div>
							</div>-->
						
					</div>
					<div class="col-lg-3">
						
                      <div  id="char2"></div>    
						
						
					</div>
				</div>
			</div>
		</section>
    <%-- <div class="page2">
        <div class="header2">
         
                <h1>
                 Task Details (Priority)
                </h1>
          
            </div>
</div>
 <asp:Panel ID="pnlAll" runat="server" BackColor="white" Width="100%">
                                                       
                                                            <div class='border-bottom' id='content'>
                                                              <div class='border-bottom'>
                                                                     <div class='chartCont border-right' id='chart-container3'>
                                                                    </div>
                                                                </div>
                                                                <div class='border-bottom'>
                                                                    <div class='chartCont border-right' id='chart-container'>
                                                                    </div>
                                                                   
                                                                    
                                                                </div>
                                                                   <div class='chartCont border-right'>
                                                                     <div class='chartCont' id='chart-container2'>
                                                                    </div>
                                                                </div>
                                                                  <div  style="vertical-align:top;padding-top:5px" >
                                                                <div class='chartCont' id="char2"></div>
                                                                  </div>
                                                                 
                                   </div>
                                   
                                                                </asp:Panel>--%>
    <%--
<a href="#" class="notification">
  <span>Total</span>
  <span class="badge">25</span>
</a>
&nbsp;&nbsp;

<a href="#" class="notification">
  <span>Pending</span>
  <span class="badge">12</span>
</a>
<br />
<div id="box">

 <div id="picture">

    <img src="images/meni1.png" />

 </div>

 <div id="text">

    Title 1

 </div>

</div>--%>
    <%--<button class="btn"><i class="fa fa-suitcase";style="width:30px"></i> New</button>
<button class="btn"><i class="fa fa-folder-open"></i> Open</button>
<button class="btn"><i class="fa fa-folder"></i> Close</button>

<button class="btn"><i class="fa fa-refresh"></i> Re-Open</button>
<button class="btn"><i class="fa fa-stop"></i> Hold</button>--%>
    <!-- Jquery -->
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <!-- Github button for demo page -->
    <script async defer src="https://buttons.github.io/buttons.js"></script>
    <!-- GRT Youtube Popup -->
    <script src="grt-responsive-menu.js"></script>
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
     
    </form>
      <div class="loading" align="center">
     
        <img id="Img1" src="~/Images/loading/source2.gif" runat="server" alt="" />
    </div>
    <script src="assets/js/jQuery.min.js"></script>
    <script src="assets/js/popper.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/main.js"></script>
    
</body>
</html>
