<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task2.aspx.cs" Inherits="MasterFiles_Task_Management_Task2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 
<meta charset="utf-8">
		<meta http-equiv="x-ua-compatible" content="ie=edge">
		<title>Task Management</title>
		<meta name="description" content="">
		<meta name="viewport" content="width=device-width, initial-scale=1">
		<!-- Demo CSS file not needed for the plugin -->
		<link href="https://fonts.googleapis.com/css?family=Lato:400,700" rel="stylesheet">
		<link rel="stylesheet" href="css-demo-page.css">

		<!-- GRT Youtube Plugin CSS -->
		<link rel="stylesheet" href="grt-responsive-menu.css">
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
  </style>
  <meta name="viewport" content="width=device-width, initial-scale=1">
<!-- Add icon library -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
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
</style>
   <script src="../../DashBoard/JS/jquery-1.7.2.min.js" type="text/javascript"></script>
      <script src="../../DashBoard/js1/fusioncharts.js" type="text/javascript"></script>
     
    <script src="../../DashBoard/js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>
     
    
    <script type="text/javascript">
        $(document).ready(function () {

            var sf_type = '<%=Session["sf_type"] %>';
            if (sf_type == 1 || sf_type == 2) {
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

                            "type": "pie3d",
                            "renderAt": "chart-container",
                            "width": "360",
                            "height": "280",
                            "dataFormat": "json",
                            "dataSource": {
                                "chart": {
                                    "caption": "My Task",
                                    "subcaption": "",
                                    "xaxisname": "",
                                    "yaxisname": "",
                                    "placeValuesInside": "0",
                                    "showLegend": "1",
                                    "palette": "5",
                                    //Configure scrollbar
                                    "formatNumber": "0",
                                    "formatNumberScale": "0",
                                    "useRoundEdges": "1",
                                    "theme": "fint",
                                    "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                    "animation": "1",
                                    "labelDisplay": "rotate",
                                    "legendScrollBgColor": "#cccccc",
                                    "legendScrollBarColor": "#999999"
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


                            type: 'doughnut2d',
                            renderAt: 'chart-container2',

                            width: '340',
                            height: '280',
                            dataFormat: 'json',

                            dataSource: {

                                "chart": {
                                    "caption": "Priority",
                                    "formatnumberscale": "0",
                                    "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB",
                                    "showLegend": "1",
                                    "theme": "fint",
                                    "showPercentValues": "0",
                                    "showPercentInToolTip": "0",
                                    //Setting legend to appear on right side
                                    // "legendPosition": "right",
                                    //Caption for legend
                                    // "legendCaption": "Status: ",
                                    //Customization for legend scroll bar cosmetics
                                    "legendScrollBgColor": "#cccccc",
                                    "legendScrollBarColor": "#999999"
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

                            "type": "Column3d",
                            "renderAt": "chart-container3",
                            "width": "340",
                            "height": "280",
                            "dataFormat": "json",
                            "dataSource": {
                                "chart": {

                                    "caption": caption,
                                    "subcaption": "",
                                    "xaxisname": "",
                                    "yaxisname": "",
                                    "placeValuesInside": "0",
                                    "showLegend": "1",
                                    "palette": "5",
                                    //Configure scrollbar
                                    "formatNumber": "0",
                                    "formatNumberScale": "0",
                                    "useRoundEdges": "1",
                                    //  "theme": "fint",
                                    "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
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




            }
            else {
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
                            "width": "360",
                            "height": "280",
                            "dataFormat": "json",
                            "dataSource": {
                                "chart": {
                                    "caption": "My Team Task",
                                    "subcaption": "",
                                    "xaxisname": "",
                                    "yaxisname": "",
                                    "placeValuesInside": "0",
                                    "showLegend": "1",
                                    "palette": "5",
                                    //Configure scrollbar
                                    "formatNumber": "0",
                                    "formatNumberScale": "0",
                                    "useRoundEdges": "1",
                                    "theme": "fint",
                                    "paletteColors": "#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
                                    "animation": "1",
                                    "labelDisplay": "rotate",
                                    "legendScrollBgColor": "#cccccc",
                                    "legendScrollBarColor": "#999999"
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

                            width: '340',
                            height: '280',
                            dataFormat: 'json',

                            dataSource: {

                                "chart": {
                                    "caption": "Priority",
                                    "formatnumberscale": "0",
                                    "paletteColors": "#FF0000,#ffff00,#008000,#9FB6CD,#A4DCD1,#A74CAB",
                                    "showLegend": "1",
                                    "theme": "fint",
                                    "showPercentValues": "0",
                                    "showPercentInToolTip": "0",
                                    //Setting legend to appear on right side
                                    // "legendPosition": "right",
                                    //Caption for legend
                                    // "legendCaption": "Status: ",
                                    //Customization for legend scroll bar cosmetics
                                    "legendScrollBgColor": "#cccccc",
                                    "legendScrollBarColor": "#999999"
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

                            "type": "line",
                            "renderAt": "chart-container3",
                            "width": "340",
                            "height": "280",
                            "dataFormat": "json",
                            "dataSource": {
                                "chart": {

                                    "caption": "Team (Assigned by Me)",
                                    "subcaption": "",
                                    "xaxisname": "",
                                    "yaxisname": "",
                                    "placeValuesInside": "0",
                                    "showLegend": "1",
                                    "palette": "5",
                                    //Configure scrollbar
                                    "formatNumber": "0",
                                    "formatNumberScale": "0",
                                    "useRoundEdges": "1",
                                    "theme": "fint",
                                    "paletteColors": "#9C6B98,#9D1309,#9DB68C,#9FB6CD,#A4DCD1,#A74CAB,#AADD00,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50",
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
            }
        });
    </script>


<script type="text/javascript">
    function BulbDue(Due) {
        FusionCharts.ready(function () {
            var chartObj = new FusionCharts({
                type: 'bulb',
                renderAt: 'char2',
                width: '220',
                height: '200',
                dataFormat: 'json',
                dataSource: {
                    "chart": {
                        "caption": "Overdue Days",
                        "showshadow": "0",
                        "showvalue": "1",
                        "captionPadding": "30",
                        "useColorNameAsValue": "1",
                        "placeValuesInside": "0",
                        "valueFontSize": "12",
                        "showBorder": "0",
                        "bgColor": "#ffffff",
                        "color": "#ffffff",
                     
                        "valuePadding": "20",
                        "plottooltext": "OverDue Task:$value"

                    },
                    "colorrange": {
                        "color": [{
                            "minvalue": "1",
                            "maxvalue": "1000",
                            "label": "Overdue Task - " +Due,
                            "code": "#ff0000"
                        }, {
                            "minvalue": "0",
                            "maxvalue": "0",
                            "label": "No Overdue Task",
                            "code": "#00ff00"
                        }]
                    },
                    "value": Due
                }
            });


            chartObj.render();
        });
    }
	</script>

</head>
<body>
  
    <form id="form1" runat="server">
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


</div>
<br />
<br />
  <div class="page2">
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
                                   
                                                                </asp:Panel>

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
   
    </form>
      <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img id="Img1" src="~/Images/loader.gif" runat="server" alt="" />
    </div>
</body>
</html>
