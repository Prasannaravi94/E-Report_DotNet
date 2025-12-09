<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task_Status.aspx.cs" Inherits="MasterFiles_Task_Management_New_Task_Status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Task | Status</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/png" href="assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700,900&display=swap"
        rel="stylesheet">
    <link rel="stylesheet" href="assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="assets/css/bootstrap.min.css">
     <link rel="stylesheet" href="assets/css/nice-select.css">
    <link rel="stylesheet" href="assets/css/responsive.css">
    <link rel="stylesheet" href="assets/css/style.css">
       
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
   
    .GridPager span
    {
      font-size:14px;
      font-weight:bold;
        color: Red;
      
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
    <asp:ToolkitScriptManager ID="scriptmanager1" runat="server">
    </asp:ToolkitScriptManager>
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
										<a class="nav-link" href="Task_Assign.aspx" onclick="ShowProgress();">Assign</a>
									</li>
									<li class="nav-item">
										<a class="nav-link active" href="Task_Status.aspx" onclick="ShowProgress();">Status</a>
									</li>
									<li class="nav-item">
										<a class="nav-link" href="Task_Tracking.aspx" onclick="ShowProgress();">Track</a>
									</li>
									<li class="nav-item">
										<a class="nav-link" href="#" runat="server" onclick="ShowProgress();" onserverclick="Back_Click">Exit</a>
									</li>
									
									<%--<li class="nav-item" >
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
    <section class="status-area clearfix">
			<div class="container">
				<div class="row">
					<div class="col-lg-12">
						<div class="status">
							<h2>Status</h2>
							<div class="track-search-area clearfix">
								<form action="" method="get">
									<div class="row clearfix">
									
											<label for="">FieldForce</label>
                                          
                                            <div class="col-lg-3">
											   <asp:DropDownList ID="ddlSF"  runat="server" >
                    </asp:DropDownList>
										</div>

										<div class="col-lg-1">
											<label for="">Priority</label>
                                            </div>
                                            <div class="col-lg-2">
                                            
											  <asp:DropDownList ID="ddlPri" runat="server" >
                        <asp:ListItem Value="0" Text="ALL"></asp:ListItem>
                        <asp:ListItem Value="H" Text="High"></asp:ListItem>
                        <asp:ListItem Value="M" Text="Medium"></asp:ListItem>
                        <asp:ListItem Value="L" Text="Low"></asp:ListItem>
                    </asp:DropDownList>
                    
										</div>
									
											<label for="">Mode of Task</label>
                                           
                                            <div class="col-lg-2">
											   <asp:DropDownList ID="ddlmode" runat="server" >
                    </asp:DropDownList>
										</div>
									<div class="col-lg-1">
											<%--<button type="submit">Go</button>--%>
                                              <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="btngo" OnClick="btnGo_Click" />
										</div>
									</div>
								</form>
							</div>
                            <div class="status-search-link-area clearfix">
								<ul>
									<li><a id="iNew" href="#"  runat="server" onserverclick="New_Click" onclick="ShowProgress();">New <br /><span class="yallow" id="New" runat="server"></span></a></li>
									<li ><a id="ipen" href="#" runat="server" onserverclick="Open_Click" onclick="ShowProgress();">Pending <br /><span  id="pen" runat="server"></span></a></li>
									<li><a id="icom" href="#" runat="server" onserverclick="Com_Click" onclick="ShowProgress();">Completed  <br /><span id="Com" class="green" runat="server"></span></a></li>
									<li><a id="iclose" href="#" runat="server" onserverclick="Close_Click" onclick="ShowProgress();">Closed <br /><span class="red" id="close" runat="server"></span></a></li>
									<li><a id="iRopen" href="#" runat="server" onserverclick="ReOpen_Click" onclick="ShowProgress();">ReOpen <br /><span  id="Reopen" runat="server"></span></a></li>
									<li><a id="iHold" href="#" runat="server" onserverclick="Hold_Click" onclick="ShowProgress();">Hold <br /><span class="red" id="Hold" runat="server"></span></a></li>
									<li><a id="iCancel" href="#" runat="server" onserverclick="Cancel_Click" onclick="ShowProgress();">Cancel <br /><span class="red" id="Cancel" runat="server"></span></a></li>
								</ul>
							</div>
						<div class="track-table-area clearfix">
								<div class="table-responsive">
                         
								   <asp:GridView ID="grdNew" runat="server" BorderStyle="None"  AutoGenerateColumns="false"
                    CssClass="table"  GridLines="None" AllowPaging="true" PageSize="3" OnPageIndexChanging="grdNew_OnPageIndexChanging"
                      
                        OnRowDataBound="grdNew_RowDataBound">
                     
                        <Columns>
                            <asp:TemplateField HeaderText="#" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdNew.PageIndex * grdNew.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Assigned By" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                         
                            <asp:HyperLinkField HeaderText="Description" Text="Click Here to View"
                                    DataNavigateUrlFormatString="Task_Edit.aspx?type=1&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" >
                              
                            </asp:HyperLinkField>
                            <asp:TemplateField  HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                         
                        </Columns>
                      <PagerStyle CssClass = "GridPager" />
                    </asp:GridView>
                  
                       <asp:GridView ID="grdOpen" runat="server" BorderStyle="None"  AutoGenerateColumns="false"
                    CssClass="table" GridLines="None" AllowPaging="true" PageSize="3" OnPageIndexChanging="grdOpen_OnPageIndexChanging"
                        OnRowDataBound="grdOpen_RowDataBound">
                      
                      
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdOpen.PageIndex * grdOpen.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Assigned By">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                  
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                           <asp:HyperLinkField HeaderText="Update" Text="Click Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=2&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" >
                                
                            </asp:HyperLinkField>
                        </Columns>
                      <PagerStyle CssClass = "GridPager" />
                    </asp:GridView>
                      <asp:GridView ID="grdComp" runat="server" BorderStyle="None"  AutoGenerateColumns="false"
                    CssClass="table" GridLines="None" AllowPaging="true" PageSize="3" OnPageIndexChanging="grdComp_OnPageIndexChanging"
                        OnRowDataBound="grdComp_RowDataBound">
                      
                      
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdComp.PageIndex * grdComp.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Assigned By">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                  
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                           <asp:HyperLinkField HeaderText="Update" Text="Click Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=3&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" >
                                
                            </asp:HyperLinkField>
                        </Columns>
                        <PagerStyle CssClass = "GridPager" />
                    </asp:GridView>
                        <asp:GridView ID="grdClose" runat="server" BorderStyle="None"  AutoGenerateColumns="false"
                    CssClass="table" GridLines="None" AllowPaging="true" PageSize="3" OnPageIndexChanging="grdClose_OnPageIndexChanging"
                        OnRowDataBound="grdClose_RowDataBound">
                      
                      
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdClose.PageIndex * grdClose.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Assigned By">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                  
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                           <asp:HyperLinkField HeaderText="Update" Text="Click Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=4&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" >
                                
                            </asp:HyperLinkField>
                        </Columns>
                        <PagerStyle CssClass = "GridPager" />
                    </asp:GridView>
                       <asp:GridView ID="grdReopen" runat="server" BorderStyle="None"  AutoGenerateColumns="false"
                    CssClass="table" GridLines="None" AllowPaging="true" PageSize="3" OnPageIndexChanging="grdReopen_OnPageIndexChanging"
                        OnRowDataBound="grdReopen_RowDataBound">
                      
                      
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdReopen.PageIndex * grdReopen.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Assigned By">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                  
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                           <asp:HyperLinkField HeaderText="Update" Text="Click Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=5&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" >
                                
                            </asp:HyperLinkField>
                        </Columns>
                       <PagerStyle CssClass = "GridPager" />
                    </asp:GridView>
                     <asp:GridView ID="grdHold" runat="server" BorderStyle="None"  AutoGenerateColumns="false"
                    CssClass="table" GridLines="None" AllowPaging="true" PageSize="3" OnPageIndexChanging="grdHold_OnPageIndexChanging"
                        OnRowDataBound="grdHold_RowDataBound">
                      
                      
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdHold.PageIndex * grdHold.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Assigned By">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                  
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                           <asp:HyperLinkField HeaderText="Update" Text="Click Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=6&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" >
                                
                            </asp:HyperLinkField>
                        </Columns>
                        <PagerStyle CssClass = "GridPager" />
                    </asp:GridView>
                      <asp:GridView ID="grdCancel" runat="server" BorderStyle="None"  AutoGenerateColumns="false"
                    CssClass="table" GridLines="None" AllowPaging="true" PageSize="3" OnPageIndexChanging="grdCancel_OnPageIndexChanging"
                        OnRowDataBound="grdCancel_RowDataBound">
                      
                      
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdCancel.PageIndex * grdCancel.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Assigned By">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="280px">
                                <ItemTemplate>
                                  
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                           <asp:HyperLinkField HeaderText="Update" Text="Click Here"
                                DataNavigateUrlFormatString="Task_Update.aspx?type=7&Task_ID={0}&Assign_From={1}&Assign_To={2}"
                                DataNavigateUrlFields="Task_ID,Task_From_Code,Task_To_Code" >
                                
                            </asp:HyperLinkField>
                        </Columns>
                        <PagerStyle CssClass = "GridPager" />
                    </asp:GridView>
                   
								</div>
                   
     </div>
     	<div class="status-result-area clearfix">
								<div class="no-result-area clearfix" id="divid" runat="server" visible="false">
									No Records Found
								</div>
                                </div>
     </div>
     </div>
     </div>
     </div>
     
     </section>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/js/jquery.nice-select.min.js"></script>
    <script src="js/bootstrap-select.min.js" type="text/javascript"></script>
     <div class="loading" align="center">
       
        <img id="Img1" src="~/Images/loading/Source2.gif" runat="server" alt="" />
    </div>
    </form>
   
</body>
</html>
