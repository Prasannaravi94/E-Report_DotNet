<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task_Tracking.aspx.cs" Inherits="MasterFiles_Task_Management_Task_Tracking" %>

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
            padding-bottom: 10px;
            padding-right: 10px;
        }
        .mydropdownlist
        {
            font-size: 14px;
            padding: 5px 10px;
            border-radius: 5px;
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
        .gridview1
        {
            background-color: Teal;
            border-style: none;
            padding: 2px;
            margin: 2% auto;
        }
        
        .gridview1 a
        {
            margin: auto 1%;
            border-style: none;
            border-radius: 50%;
            background-color: #444;
            padding: 5px 7px 5px 7px;
            color: #fff;
            text-decoration: none;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
        }
        .gridview1 a:hover
        {
            background-color: #1e8d12;
            color: #fff;
        }
        .gridview1 td
        {
            border-style: none;
        }
        .gridview1 span
        {
            background-color: #ae2676;
            color: #fff;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
            border-radius: 50%;
            padding: 5px 7px 5px 7px;
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
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/bootstrap-select.min.css" />
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
								<li ><a href="Task2.aspx" onclick="ShowProgress();">Home</a></li>
								<li id="liassign" runat="server"><a href="Task_Assign.aspx" onclick="ShowProgress();">Assign</a></li>
								<li><a href="Task_Status.aspx" onclick="ShowProgress();">Status</a></li>
								<li class="active"><a href="Task_Tracking.aspx" onclick="ShowProgress();">Track</a></li>
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
