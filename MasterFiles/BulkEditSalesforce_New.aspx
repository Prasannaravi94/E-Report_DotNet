<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEditSalesforce_New.aspx.cs"
    Inherits="MasterFiles_BulkEditSalesforce_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FieldForce - Bulk Edit</title>

    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <script src="../JsFiles/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../JsFiles/ScrollableGridPlugin.js" type="text/javascript"></script>
    <style type="text/css">
        .modal {
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

        .closeLoginPanel {
            font-family: Verdana, Helvetica, Arial, sans-serif;
            height: 14px;
            font-size: 11px;
            font-weight: bold;
            position: absolute;
            top: -2px;
            right: 1px;
        }

            .closeLoginPanel a {
                /*background-color: Yellow;*/
                cursor: pointer;
                color: Black;
                text-align: center;
                text-decoration: none;
                padding: 3px;
            }

        .display-table .table td {
            padding: 5px 5px !important;
        }
    </style>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />
</head>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

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
                function myDropDown() {
                    var myDropDown = document.getElementById("ddlFilter");
                    var length = myDropDown.options.length;
                    //open dropdown
                    myDropDown.size = length;
                    //close dropdown
                    myDropDown.size = 0;
                }
            </script>
            <script type="text/javascript">
                 function checkAllSample() {
                    var grid = document.getElementById('<%= grdSalesForce.ClientID %>');
                     if (grid != null) {
                         var inputList = grid.getElementsByTagName("input");
                         var cnt = 0;
                         var index = '';
                         var chkallSample = document.getElementById('grdSalesForce_ctl01_chkAllSample');
                         for (i = 2; i < inputList.length; i++) {
                             if (i.toString().length == 1) {
                                 index = cnt.toString() + i.toString();
                             }
                             else {
                                 index = i.toString();
                             }
                             var chkSample = document.getElementById('grdSalesForce_ctl' + index + '_chkSample');

                             if (chkallSample.checked) {

                                 chkSample.checked = true;
                             }
                             else {
                                 chkSample.checked = false;

                             }
                         }
                     }
                }

                 function checkAllInput() {
                    var grid = document.getElementById('<%= grdSalesForce.ClientID %>');
                    if (grid != null) {
                        var inputList = grid.getElementsByTagName("input");
                        var cnt = 0;
                        var index = '';
                        var chkallInput = document.getElementById('grdSalesForce_ctl01_chkAllInput');
                        for (i = 2; i < inputList.length; i++) {
                            if (i.toString().length == 1) {
                                index = cnt.toString() + i.toString();
                            }
                            else {
                                index = i.toString();
                            }
                            var chkInput = document.getElementById('grdSalesForce_ctl' + index + '_chkInput');
                
                            if (chkallInput.checked) {
                                chkInput.checked = true;
                            }
                            else {
                                chkInput.checked = false;
                            }
                        }
                    }
                }
                </script>
            <script type="text/javascript">
                function HidePopup() {

                    var popup = $find('TextBox1_PopupControlExtender');
                    popup.hide();
                }
            </script>

            <div class=" home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">FieldForce - Bulk Edit</h2>

                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <div class="row clearfix">
                                    <div class="col-lg-4">

                                        <asp:Label ID="Label1" runat="server" Text="Select Field To Edit" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlselectfield" runat="server" CssClass="nice-select">
                                            <asp:ListItem Value="1">All</asp:ListItem>
                                            <asp:ListItem Value="5">User Name</asp:ListItem>
                                            <asp:ListItem Value="6">Password</asp:ListItem>
                                            <asp:ListItem Value="7">HQ</asp:ListItem>
                                            <asp:ListItem Value="8">State</asp:ListItem>
                                            <asp:ListItem Value="9">Employee ID</asp:ListItem>
                                            <%--  <asp:ListItem Value="Designation_Code">Designation</asp:ListItem>--%>
                                            <asp:ListItem Value="10">Joining Date</asp:ListItem>
                                            <asp:ListItem Value="11">Sub Division</asp:ListItem>
                                            <asp:ListItem Value="12">Fieldforce Type</asp:ListItem>
                                            <asp:ListItem Value="13">Mobile No</asp:ListItem>
                                            <asp:ListItem Value="14">Total Lock(Delayed Days)</asp:ListItem>
                                            <asp:ListItem Value="15">HQ Code & HQ Name</asp:ListItem>
                                            <%--<asp:ListItem Value="16">HQ Name</asp:ListItem>--%>
                                            <asp:ListItem Value="17">Confirmation Date</asp:ListItem>
                                            <asp:ListItem Value="18">Category</asp:ListItem>
                                            <asp:ListItem Value="19">Bank Details</asp:ListItem>
                                            <asp:ListItem Value="20">DCR Sample/Input Qty Validation</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>


                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="btngoo" runat="server" Text="Go" CssClass="savebutton" Width="50px"
                                            OnClick="btngoo_Click" />
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label ID="lblFilter" runat="server" Text="Filter By Manager" Visible="false" CssClass="label"></asp:Label>

                                        <asp:DropDownList ID="ddlFilter" runat="server" CssClass="custom-select2 nice-select" Visible="false">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-lg-1" style="padding-top: 17px; padding-left: 0px">
                                        <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" Width="50px"
                                            OnClick="btnGo_Click" Visible="false" />
                                    </div>
                                </div>

                            </div>

                            <br />
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                    <div runat="server" id="tblSalesForce" visible="false" width="100%">

                                        <asp:GridView ID="grdSalesForce" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false"
                                            OnRowCreated="grdSalesForce_RowCreated" OnRowDataBound="grdSalesForce_RowDataBound"
                                            GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" Style="background-color: white"
                                            EmptyDataText="No Records Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SF_Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSf_Name" runat="server" widht="220px" MaxLength="150"
                                                            Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Design" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="160px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSf_HQ" runat="server" MaxLength="150" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="160px"></ItemStyle>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="User Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="UsrDfd_UserName" runat="server" CssClass="input" MaxLength="70" Height="40px"
                                                            Text='<%# Bind("UsrDfd_UserName") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Password" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Sf_Password" runat="server" CssClass="input" Width="100px" Height="40px"
                                                            MaxLength="15" Text='<%# Bind("Sf_Password") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Sf_HQ" runat="server" CssClass="input" Width="100px" MaxLength="150" Height="40px"
                                                            Text='<%# Bind("Sf_HQ") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="State_Code" runat="server" Width="180px" DataSource="<%# FillState() %>"
                                                            DataTextField="StateName" DataValueField="State_Code" CssClass="nice-select">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="sf_emp_id" runat="server" CssClass="input" MaxLength="10" Height="40px"
                                                            Width="80px" Text='<%# Bind("sf_emp_id") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Joining Date">
                                                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Sf_Joining_Date" onkeypress="Calendar_enter(event);" runat="server" Height="40px"
                                                            Text='<%# Bind("Sf_Joining_Date") %>' CssClass="input"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="Sf_Joining_Date" CssClass="cal_Theme1"
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Division" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="updatepanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="input" Width="145px" Height="40px"></asp:TextBox>
                                                                <asp:HiddenField ID="hdnSubDivisionId" runat="server"></asp:HiddenField>
                                                                <asp:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" Enabled="True"
                                                                    ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1" OffsetY="2" Position="Bottom">
                                                                </asp:PopupControlExtender>
                                                                <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" BorderColor="#d1e2ea" Direction="LeftToRight" BackColor="#f4f8fa" Style="display: none; border-radius: 8px; overflow-x: auto; height: 250px; scrollbar-width: thin">
                                                                    <%--  <div style="height:15px; position:relative; background-color: #4682B4; 
                                        text-transform: capitalize; width:100%; float: left" align="right">
                                        <asp:Button ID="btnsubdiv" Style="font-family: Verdana; font-size: 7pt; font-weight:bold; width: 25px; background-color: Yellow; 
                                            Color: Black; margin-top: -1px;" Text="X" runat="server" OnClick="btnClose_Click"  OnClientClick="HidePopup();" />
                                        
                                            </div>--%>
                                                                    <div style="height: 17px; position: relative; text-transform: capitalize; width: 100%; float: left"
                                                                        align="right">
                                                                        <div class="closeLoginPanel">
                                                                            <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                                                title="Close">X</a>
                                                                        </div>
                                                                    </div>
                                                                    <asp:CheckBoxList ID="subdivision_code" runat="server" Width="155px" CssClass="gridcheckbox"
                                                                        DataSource="<%# FillCheckBoxList() %>" DataTextField="subdivision_name" DataValueField="subdivision_code"
                                                                        AutoPostBack="True" OnSelectedIndexChanged="subdivision_code_SelectedIndexChanged"
                                                                        onclick="checkAll1(this);">
                                                                    </asp:CheckBoxList>
                                                                    <%--   <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
                                                        SelectCommand="SELECT [subdivision_code],[subdivision_name] FROM [mas_subdivision]"></asp:SqlDataSource>--%>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fieldforce Type" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="Fieldforce_Type" runat="server" Width="180px" CssClass="nice-select">
                                                            <asp:ListItem Selected="True" Text="---Select---" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Trainee" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Probation" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Confirmed" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="SF_Mobile" runat="server" CssClass="input" MaxLength="10" onkeypress="CheckNumeric(event);" Height="40px"
                                                            Width="100px" Text='<%# Bind("SF_Mobile") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Total Lock(Delayed Days)" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="sf_short_name" runat="server" CssClass="input" MaxLength="25" onkeypress="CheckNumeric(event);" Height="40px"
                                                            Width="80px" Text='<%# Bind("sf_short_name") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HQ Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="SF_Cat_Code" runat="server" CssClass="input" Width="90px" MaxLength="150" Height="40px"
                                                            Text='<%# Bind("SF_Cat_Code") %>'></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HQ Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Approved_By" runat="server" Width="130px" MaxLength="200" CssClass="input" Height="40px"
                                                            Text='<%# Bind("Approved_By") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="130px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Confirmation Date">
                                                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="SF_DOW" onkeypress="Calendar_enter(event);" runat="server"
                                                            Text='<%# Bind("SF_DOW") %>' CssClass="input" Height="40px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="SF_DOW" CssClass="cal_Theme1"
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="Category" runat="server">
                                                            <asp:ListItem Selected="True" Text="---Select---" Value="0"></asp:ListItem>
                                                           <%-- <asp:ListItem Text="Metro" Value="M"></asp:ListItem>--%>
                                                             <asp:ListItem Text="Mega-Metro" Value="M"></asp:ListItem>
                                                            <asp:ListItem Text="Non-Metro" Value="N"></asp:ListItem>
                                                           <%-- <asp:ListItem Text="Semi-Metro" Value="S"></asp:ListItem>--%>
                                                             <asp:ListItem Text="Metro" Value="S"></asp:ListItem>

                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Employee ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSf_emp_id" runat="server" MaxLength="150" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="160px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bank Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%--  <asp:TextBox ID="Bank_Name" runat="server" CssClass="input" Width="120px" MaxLength="150" Height="40px"
                                                                    Text='<%# Bind("Bank_Name") %>'></asp:TextBox>--%>
                                                        <asp:DropDownList ID="ddlBankName" runat="server" CssClass="nice-select"
                                                            TabIndex="29">
                                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                            <asp:ListItem  Text="Cheque" Value="Cheque"></asp:ListItem>
                                                            <asp:ListItem Text="SBI Bank" Value="SBI"></asp:ListItem>
                                                            <asp:ListItem Text="ICICI Bank" Value="ICICI"></asp:ListItem>
                                                            <asp:ListItem Text="HDFC Bank" Value="HDFC"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Account No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Bank_AcNo" runat="server" CssClass="input" Width="150px" MaxLength="150" Height="40px"
                                                            Text='<%# Bind("Bank_AcNo") %>'></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IFSC Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="IFS_Code" runat="server" CssClass="input" Width="150px" MaxLength="150" Height="40px"
                                                            Text='<%# Bind("IFS_Code") %>'></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DCR_Sample" ItemStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDCR_Sample" runat="server" Text='<%#Eval("Sf_DCRSample_Valid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllSample" Text="DCR Sample Validation Needed" runat="server" onclick="checkAllSample(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSample" Text=" " runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="DCR_Input" ItemStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDCR_Input" runat="server" Text='<%#Eval("Sf_DCRInput_Valid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllInput" Text="DCR Input Validation Needed" runat="server" onclick="checkAllInput(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkInput" Text=" " runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row justify-content-center">
                                <asp:Button ID="btnUpdate" CssClass="savebutton" runat="server" Visible="false"
                                    Text="Update" OnClick="btnUpdate_Click" />
                            </div>
                        </div>
                    </div>

                </div>

                <asp:Button ID="btnback" runat="server" CssClass="backbutton" Text="Back" OnClick="btnback_Click" />

            </div>

            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>
