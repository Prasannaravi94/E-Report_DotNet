<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task_Status.aspx.cs" Inherits="MasterFiles_Task_Management_Task_Status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
            padding-bottom: 10px;
            padding-right: 10px;
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
        
        
        .mGrid
        {
            width: 100%;
            background-color: white;
            margin: 5px 0 10px 0;
            border: solid 1px black;
            border-collapse: collapse;
        }
        .mGrid td
        {
            padding: 2px;
            border: solid 1px black;
            color: black;
            border-color: black;
            border-left: solid 1px black;
            font-size: 12px;
            font-family: Calibri;
        }
        .mGrid th
        {
            padding: 4px 2px;
            color: black;
            background: teal;
            border-color: black;
            border-left: solid 1px black;
            font-weight: bold;
            font-size: 13px;
            font-family: Calibri;
        }
        
        .mGrid .pgr
        {
            background: white;
        }
        .mGrid .pgr table
        {
            margin: 5px 0;
        }
        .mGrid .pgr td
        {
            border-width: 0;
            padding: 0 6px;
            border-left: solid 1px black;
            font-weight: bold;
            color: black;
            line-height: 12px;
        }
        .mGrid .pgr a
        {
            color: black;
            text-decoration: none;
        }
        .mGrid .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
        
        .normal
        {
            background-color: white;
        }
        .highlight
        {
            background-color: white;
        }
        .web_dialog_overlay
        {
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
        
        .web_dialog
        {
            display: none;
            position: fixed;
            width: 450px;
            min-height: 150px;
            max-height: auto;
            top: 50%;
            left: 50%;
            margin-left: -190px;
            margin-top: -100px;
            background-color: #ffffff;
            border: 2px solid #336699;
            padding: 0px;
            z-index: 102;
            font-family: Verdana;
            font-size: 10pt;
        }
        
        .web_dialog_title
        {
            border-bottom: solid 2px Teal;
            background-color: Teal;
            padding: 4px;
            color: White;
            font-weight: bold;
            width: 450px;
        }
        
        .web_dialog_title a
        {
            color: White;
            text-decoration: none;
        }
        
        .align_right
        {
            text-align: right;
        }
        
        .Formatrbtn label
        {
            margin-right: 30px;
        }
        
        
        /* hover style just for information */
        label:hover:before
        {
            border: 1px solid #4778d9 !important;
        }
        
        
        .btnReAct
        {
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
        
        .btnReActivation
        {
            color: #fff;
            background-color: #158263;
            border-color: #158263;
        }
        
        .btnReActivation:hover
        {
            color: #fff;
            background-color: #2b9a7b;
            border-color: #2b9a7b;
        }
        
        .btnReActivation:focus, .btnReActivation.focus
        {
            color: #fff;
            background-color: #2b9a7b;
            border-color: #2b9a7b;
        }
        
        .btnReActivation:active, .btnReActivation.active
        {
            color: #fff;
            background-color: #158263;
            border-color: #158263;
            background-image: none;
        }
        
        
        #btnClose_Plus:focus
        {
            outline-offset: -2px;
        }
        
        #btnClose_Plus:hover, #btnClose_Plus:focus
        {
            color: #fff;
            text-decoration: underline;
        }
        
        #btnClose_Plus:hover, #btnClose_Plus:focus
        {
            color: #fff;
            text-decoration: underline;
        }
        
        #btnClose_Plus:active, #btnClose_Plus:hover
        {
            outline: 0px none currentColor;
        }
    </style>
    <style type="text/css">
.blink {
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
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}

@-webkit-keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}

@keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}

</style>
    <style type="text/css">
        .fancy-green .ajax__tab_header
        {
            background: url(green_bg_Tab.gif) repeat-x;
            cursor: pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
            background: url(green_left_Tab.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
            background: url(green_right_Tab.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
            font-size: 16px;
            font-weight: bold;
            color: #000;
            font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
            height: 46px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
            height: 46px;
            margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
            margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
            color: #fff;
        }
        .fancy .ajax__tab_body
        {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
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
    <%--<script type="text/javascript">
        $(document).on('click', function (e) {
            if (!e) e = event;
            elm = (e.target) ? e.target : e.srcElement;
            elm1 = $('.ajax__tab_active');
            elm2 = $(elm1).find('.ajax__tab_tab');

            if (elm2[0].id == elm.id || elm2[0].id == elm.parentNode.id) {
                drawChart(elm2[0].id.replace(/__tab_TabContainer1_TabPanel/g, ''));
            }
        });

            google.setOnLoadCallback(drawChart);
            function drawChart(r) {

                var options = {

                };
                alert(r);
            }
    </script>--%>
        <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="css/bootstrap-select.min.css" />
    <script type="text/javascript">

        $(document).ready(function () {

            $("#btnApproveLink").click(function (e) {
                ShowDialog_Plus(false);
                e.preventDefault();
            });

            $("#btnClose_Plus").click(function (e) {
                HideDialog_Plus();
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
 <%--     <link href="../../css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />--%>
       
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="scriptmanager1" runat="server">
    </asp:ToolkitScriptManager>
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
        <h2 style="color: White">
            Status
        </h2>
        <table>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblsfName" runat="server" Text="FieldForce" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                   <div class="row-fluid">
                    <asp:DropDownList ID="ddlSF" data-live-search="true" class="selectpicker" runat="server" Width="400px">
                    </asp:DropDownList>
                    </div>
                </td>
                &nbsp;&nbsp;
                <td align="left" class="stylespc">
                    <asp:Label ID="Label1" runat="server" Text="Priority" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlPri" runat="server" CssClass="mydropdownlist">
                        <asp:ListItem Value="0" Text="ALL"></asp:ListItem>
                        <asp:ListItem Value="H" Text="High"></asp:ListItem>
                        <asp:ListItem Value="M" Text="Medium"></asp:ListItem>
                        <asp:ListItem Value="L" Text="Low"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                &nbsp;&nbsp;
                <td align="left" class="stylespc">
                    <asp:Label ID="Label2" runat="server" Text="Mode of Task" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlmode" runat="server" CssClass="mydropdownlist">
                    </asp:DropDownList>
                    &nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnGo" runat="server" Text="Go" Width="50px" Height="25px" OnClick="btnGo_Click" />
                </td>
            </tr>
        </table>
        <asp:TabContainer ID="TabContainer1" runat="server" Width="95%" Height="420px" ScrollBars="Both"
            CssClass="fancy fancy-green">
            <asp:TabPanel ID="TabPanel1" runat="server" ScrollBars="Both">
                <HeaderTemplate>
                    New <span id="New" style="color: Red; font-size: 16px; font-weight: bolder" runat="server">
                    </span>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="grdNew" runat="server" Width="95%" HorizontalAlign="Center" AutoGenerateColumns="false"
                        EmptyDataText="No Records Found" GridLines="Both"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdNew_RowDataBound">
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="sf Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbltaskid" runat="server" Text='<%#Eval("Task_ID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="sf Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSfCode" runat="server" Text='<%#Eval("Task_To_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mgr Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblmgrcode" runat="server" Text='<%#Eval("Task_From_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned By" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned To" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--  <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Description" ItemStyle-Width="280px">
                        
                                 <div style="overflow: auto; height: 80px;">
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                    </div>
                                        <ItemTemplate>
                                        <a href="#" id="btndesc">Click to View Description</a>
                                    </ItemTemplate>
                               
                            </asp:TemplateField>--%>
                            <asp:HyperLinkField HeaderText="Description" HeaderStyle-ForeColor="white" Text="Click Here to View"
                                DataNavigateUrlFormatString="Task_Edit.aspx?type=1&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                          <asp:HyperLinkField HeaderText="Update" HeaderStyle-ForeColor="white" Text="Update Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=1&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkGreen" BackColor="Yellow" Font-Size="Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel2" runat="server">
                <HeaderTemplate>
                    Pending <span id="pen" style="color: Red" runat="server"></span>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="grdOpen" runat="server" Width="95%" HorizontalAlign="Center" AutoGenerateColumns="false"
                        EmptyDataText="No Records Found" GridLines="Both"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdOpen_RowDataBound">
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="sf Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSfCode" runat="server" Text='<%#Eval("Task_To_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mgr Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblmgrcode" runat="server" Text='<%#Eval("Task_From_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned By" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned To" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                    <div style="overflow: auto; height: 80px;">
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                           <asp:HyperLinkField HeaderText="Update" HeaderStyle-ForeColor="white" Text="Update Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=2&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkGreen" BackColor="Yellow" Font-Size="Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                    <div id="output_Plus">
                    </div>
                    <div id="overlay_Plus" class="web_dialog_overlay">
                    </div>
                    <div id="dialog_Plus" class="web_dialog">
                        <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                            <tr>
                                <td class="web_dialog_title">
                                    Stockist - HQ Creation
                                </td>
                                <td class="web_dialog_title align_right">
                                    <a href="#" id="btnClose_Plus">Close</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel3" runat="server">
                <HeaderTemplate>
                    Completed <span id="Com" style="color: Red" runat="server"></span>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="grdComp" runat="server" Width="95%" HorizontalAlign="Center" AutoGenerateColumns="false"
                        EmptyDataText="No Records Found" GridLines="Both"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdComp_RowDataBound">
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="sf Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSfCode" runat="server" Text='<%#Eval("Task_To_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mgr Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblmgrcode" runat="server" Text='<%#Eval("Task_From_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned By" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned To" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                    <div style="overflow: auto; height: 80px;">
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                             <asp:HyperLinkField HeaderText="Update" HeaderStyle-ForeColor="white" Text="Update Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=3&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkGreen" BackColor="Yellow" Font-Size="Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel4" runat="server">
                <HeaderTemplate>
                    Closed <span id="close" style="color: Red" runat="server"></span>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="grdClose" runat="server" Width="95%" HorizontalAlign="Center" AutoGenerateColumns="false"
                         EmptyDataText="No Records Found" GridLines="Both"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdClose_RowDataBound">
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="sf Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSfCode" runat="server" Text='<%#Eval("Task_To_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mgr Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblmgrcode" runat="server" Text='<%#Eval("Task_From_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned By" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned To" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                    <div style="overflow: auto; height: 80px;">
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                           <asp:HyperLinkField HeaderText="Update" HeaderStyle-ForeColor="white" Text="Update Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=4&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkGreen" BackColor="Yellow" Font-Size="Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel5" runat="server">
                <HeaderTemplate>
                    Reopen <span id="Reopen" style="color: Red" runat="server"></span>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="grdReopen" runat="server" Width="95%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" EmptyDataText="No Records Found"
                        GridLines="Both" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdReopen_RowDataBound">
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="sf Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSfCode" runat="server" Text='<%#Eval("Task_To_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mgr Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblmgrcode" runat="server" Text='<%#Eval("Task_From_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned By" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned To" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                    <div style="overflow: auto; height: 80px;">
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:HyperLinkField HeaderText="Update" HeaderStyle-ForeColor="white" Text="Update Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=5&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkGreen" BackColor="Yellow" Font-Size="Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel6" runat="server">
                <HeaderTemplate>
                    Hold <span id="Hold" style="color: Red" runat="server"></span>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="grdHold" runat="server" Width="95%" HorizontalAlign="Center" AutoGenerateColumns="false"
                        EmptyDataText="No Records Found" GridLines="Both"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdHold_RowDataBound">
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="sf Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSfCode" runat="server" Text='<%#Eval("Task_To_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mgr Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblmgrcode" runat="server" Text='<%#Eval("Task_From_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned By" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned To" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                    <div style="overflow: auto; height: 80px;">
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:HyperLinkField HeaderText="Update" HeaderStyle-ForeColor="white" Text="Update Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=6&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkGreen" BackColor="Yellow" Font-Size="Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel7" runat="server">
                <HeaderTemplate>
                    Cancel <span id="Cancel" style="color: Red" runat="server"></span>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="grdCancel" runat="server" Width="95%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" EmptyDataText="No Records Found"
                        GridLines="Both" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdCancel_RowDataBound">
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="sf Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSfCode" runat="server" Text='<%#Eval("Task_To_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mgr Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblmgrcode" runat="server" Text='<%#Eval("Task_From_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned By" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned To" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                    <div style="overflow: auto; height: 80px;">
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                           <asp:HyperLinkField HeaderText="Update" HeaderStyle-ForeColor="white" Text="Update Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=7&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkGreen" BackColor="Yellow" Font-Size="Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
     
    </center>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
          <script src="grt-responsive-menu.js"></script>
  <script src="js/bootstrap.min.js" type="text/javascript"></script>
  <script src="js/bootstrap-select.min.js" type="text/javascript"></script>
    </form>
        <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img id="Img1" src="~/Images/loader.gif" runat="server" alt="" />
    </div>
</body>
</html>
