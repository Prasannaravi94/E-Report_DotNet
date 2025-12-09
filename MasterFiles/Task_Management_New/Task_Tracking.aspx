<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task_Tracking.aspx.cs" Inherits="MasterFiles_Task_Management_New_Task_Tracking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Task | Track</title>
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
										<a class="nav-link" href="Task_Status.aspx" onclick="ShowProgress();">Status</a>
									</li>
									<li class="nav-item">
										<a class="nav-link active" href="Task_Tracking.aspx" onclick="ShowProgress();">Track</a>
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
    <%--  <center>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <h2 style="color: White">
            Status - Tracking
        </h2>
        <br />
        <table>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblsfName" runat="server" Text="FieldForce" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <div class="row-fluid">
                        <asp:DropDownList ID="ddlSF" data-live-search="true" class="selectpicker" runat="server"
                            Width="500px">
                        </asp:DropDownList>
                    </div>
                </td>
                &nbsp;&nbsp;
                <td align="left" class="stylespc">
                    <asp:Label ID="Label1" runat="server" Text="Month" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="mydropdownlist">
                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                &nbsp;&nbsp;
                <td align="left" class="stylespc">
                    <asp:Label ID="Label2" runat="server" Text="Year" Font-Bold="true" Font-Size="16px"
                        Font-Names="verdana" ForeColor="white"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="mydropdownlist">
                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnGo" runat="server" Text="Go" Width="50px" Height="25px" OnClick="btnGo_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdTask" runat="server" Width="95%" HorizontalAlign="Center" AutoGenerateColumns="false"
                    CssClass="mGrid" EmptyDataText="No Records Found" GridLines="Both" PagerStyle-CssClass="pgr"
                    AllowPaging="true" PageSize="5" OnPageIndexChanging="OnPageIndexChanging" AlternatingRowStyle-CssClass="alt">
                    <PagerStyle CssClass="gridview1"></PagerStyle>
                    <SelectedRowStyle BackColor="BurlyWood" />
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdTask.PageIndex * grdTask.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
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
                        <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned By" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAssignBy" runat="server" Text='<%# Bind("Task_By_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Assigned To" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAssignTo" runat="server" Text='<%# Bind("Task_To_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Description" ItemStyle-Width="280px"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <div style="overflow: auto; height: 70px;">
                                    <asp:Label ID="lbldes" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Dead Line" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Priority" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbldead" runat="server" Text='<%# Bind("Task_Status_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Comments" ItemStyle-Width="220px"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <div style="overflow: auto; height: 70px;">
                                    <asp:Label ID="lblcom" runat="server" Text='<%# Bind("comments") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Updated Date" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblupd" runat="server" Text='<%# Bind("comments_dt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                        VerticalAlign="Middle" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>--%>
    <section class="track-area clearfix">
			<div class="container">
				<div class="row">
					<div class="col-lg-12">
						<div class="track">
							<h2>Status - Tracking</h2>
							<div class="track-search-area clearfix">
								<form action="" method="get">
                             
									<div class="row clearfix">
										<div class="col-lg-2">
											<label for="">FieldForce</label>
										</div>
										<div class="col-lg-4">
											   <asp:DropDownList ID="ddlSF" runat="server">
                        </asp:DropDownList>
										</div>
                                        	<div class="col-lg-1">
											<label for="">Mnth/Yr</label>
										</div>
											<div class="col-lg-2">
										 <asp:DropDownList ID="ddlMonth" runat="server" >
                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                   </div>
                  	<div class="col-lg-2">
                          <asp:DropDownList ID="ddlYear" runat="server" >
                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                    </asp:DropDownList>
                    </div>
									
										<div class="col-lg-1">
											
                                              <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="btngo" OnClick="btnGo_Click" />
										</div>
									</div>
                                    
								</form>
							</div>
							<div class="track-table-area clearfix">
								<div class="table-responsive">
								
									 <asp:GridView ID="grdTask" runat="server" BorderStyle="None"  AutoGenerateColumns="false"
                    CssClass="table"   GridLines="None"
                    AllowPaging="true" PageSize="3" OnPageIndexChanging="OnPageIndexChanging">
             <HeaderStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdTask.PageIndex * grdTask.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                             
                                    <asp:Label ID="lbldes" runat="server" Text='<%# Bind("Task_Desc") %>'></asp:Label>
                            
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dead Line" >
                            <ItemTemplate>
                                <asp:Label ID="lbldead" runat="server" Text='<%# Bind("DeadLine_To") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Priority">
                            <ItemTemplate>
                                <asp:Label ID="lblPri" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lbldead" runat="server" Text='<%# Bind("Task_Status_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments">
                            <ItemTemplate>
                                <div style="overflow: auto; height: 70px;">
                                    <asp:Label ID="lblcom" runat="server" Text='<%# Bind("comments") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Updated Date">
                            <ItemTemplate>
                                <asp:Label ID="lblupd" runat="server" Text='<%# Bind("comments_dt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
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
