<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu_Rights_Creation.aspx.cs"
    Inherits="MasterFiles_Menu_Rights_Creation" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menu Rights Creation</title>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
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
    <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
</head>
<script type="text/javascript">
    $(document).ready(function () {
        //         $('input:text:first').focus();
        $('input:text').bind("keydown", function (e) {
            var n = $("input:text").length;
            if (e.which == 13) { //Enter key
                e.preventDefault(); //to skip default behavior of the enter key
                var curIndex = $('input:text').index(this);

                if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                    $('input:text')[curIndex].focus();
                }
                else {
                    var nextIndex = $('input:text').index(this) + 1;

                    if (nextIndex < n) {
                        e.preventDefault();
                        $('input:text')[nextIndex].focus();
                    }
                    else {
                        $('input:text')[nextIndex - 1].blur();
                        $('#btnSubmit').focus();
                    }
                }
            }
        });
        $("input:text").on("keypress", function (e) {
            if (e.which === 32 && !this.value.length)
                e.preventDefault();
        });
        $('#btnSubmit').click(function () {


            if ($("#txtName").val() == "") { alert("Please Enter Name."); $('#txtName').focus(); return false; }
            if ($("#txtUserName").val() == "") { alert("Please Enter User Name."); $('#txtUserName').focus(); return false; }
            if ($("#txtPassword").val() == "") { alert("Please Enter Password."); $('#txtPassword').focus(); return false; }
            if ($('#chkDivision input:checked').length > 0) { return true; } else { alert('Please Select Division'); return false; }

        });
    });
</script>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Menu Rights Creation</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblName" runat="server" Text="Name" CssClass="label"></asp:Label><br />
                                <asp:TextBox ID="txtName" runat="server" TabIndex="1" CssClass="input" Width="100%"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblUserName" runat="server" Text="User Name" CssClass="label"></asp:Label><br />
                                <asp:TextBox ID="txtUserName" runat="server" TabIndex="2" CssClass="input" Width="100%"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="label"></asp:Label><br />
                                <asp:TextBox ID="txtPassword" runat="server" TabIndex="3" CssClass="input" MaxLength="15"
                                    TextMode="Password" Width="100%"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDivision" runat="server" Text="Division Name" CssClass="label"></asp:Label>
                                <asp:CheckBoxList ID="chkDivision" TabIndex="5" runat="server" DataTextField="division_name"
                                    DataValueField="division_code" RepeatColumns="2"
                                    RepeatDirection="vertical">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" TabIndex="7"
                                Text="Submit" OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" TabIndex="8"
                                CssClass="resetbutton" Text="Reset" OnClick="btnReset_Click" />
                        </div>

                    </div>
                    <br />


                </div>
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />
                <br />
                <br />
                <div class="row justify-content-center">
                    <div class="display-name-heading text-center clearfix">
                        <div class="d-inline-block division-name">Menu Rights</div>
                        <div class="d-inline-block align-middle">
                            <div class="single-des-option">
                                <asp:DropDownList ID="ddlmenu" runat="server" CssClass="nice-select" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlmenu_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Admin"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="MGR"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="MR"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row " style="overflow-x: auto">
                    <br />

                    <asp:Panel ID="pnladmin" runat="server" Visible="false">

                        <center>
                            <asp:Panel ID="pnlNew" runat="server">
                                <br />

                                <div class="row clearfix ">

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Master </h3>
                                            <asp:CheckBoxList ID="chkaccess" DataTextField="Menu_Name" runat="server" RepeatDirection="Vertical"
                                                RepeatColumns="1" ForeColor="#7F525D" CellPadding="2" CellSpacing="5"
                                                Width="200px" Font-Names=" 'Roboto', sans-serif">
                                                <asp:ListItem Value="LiDiv" Text="Division"></asp:ListItem>
                                                <asp:ListItem Value="Listate" Text="State/Location"></asp:ListItem>
                                                <asp:ListItem Value="lides" Text="Designation"></asp:ListItem>
                                                <asp:ListItem Value="liholi" Text="Holiday Master"></asp:ListItem>
                                                <asp:ListItem Value="lichg" Text="Change Password"></asp:ListItem>
                                                <asp:ListItem Value="lihoid" Text="Ho Id Creation"></asp:ListItem>
                                                <asp:ListItem Value="lish" Text="Statwise Holiday"></asp:ListItem>
                                                <asp:ListItem Value="limenu" Text="Menu Rights"></asp:ListItem>
                                            </asp:CheckBoxList>

                                        </div>
                                    </div>

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Reports </h3>
                                            <asp:CheckBoxList ID="chkReports" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                Font-Names=" 'Roboto', sans-serif">
                                                <asp:ListItem Value="liuser" Text="User List"></asp:ListItem>
                                                <asp:ListItem Value="ullidoc" Text="Doctor Details"></asp:ListItem>
                                                <asp:ListItem Value="liFS" Text="FieldForce Status"></asp:ListItem>
                                                <asp:ListItem Value="liCall" Text="Call Average View"></asp:ListItem>
                                                <asp:ListItem Value="liwt" Text="Work type View Status"></asp:ListItem>
                                                <asp:ListItem Value="lidcr" Text="DCR Count View"></asp:ListItem>
                                                <asp:ListItem Value="li2" Text="App version View"></asp:ListItem>
                                            </asp:CheckBoxList>

                                        </div>
                                    </div>
                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Options </h3>
                                            <p class="text-center">
                                                <asp:CheckBoxList ID="ChkOptions" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                    ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                    Font-Names=" 'Roboto', sans-serif">
                                                    <asp:ListItem Value="Limail" Text="Mail Box"></asp:ListItem>
                                                    <asp:ListItem Value="Li1" Text="Status View"></asp:ListItem>
                                                    <asp:ListItem Value="liND" Text="Not Submitted Status"></asp:ListItem>
                                                    <asp:ListItem Value="lidash" Text="Dash Board"></asp:ListItem>
                                                    <asp:ListItem Value="liStkDmp" Text="Stockist Dump"></asp:ListItem>
                                                    <asp:ListItem Value="li3" Text="Color setting"></asp:ListItem>
                                                    <asp:ListItem Value="li5" Text="Bank Details"></asp:ListItem>
                                                    <asp:ListItem Value="lidashbordAlldiv" Text="Dashboard"></asp:ListItem>
                                                    <asp:ListItem Value="liStatus" Text="Status"></asp:ListItem>
                                                    <asp:ListItem Value="li4" Text="App Usage"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </p>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                        </center>
                        <br />

                        <center>
                            <asp:Panel ID="pnlhome" runat="server">

                                <div class="row clearfix">

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Master </h3>

                                            <asp:CheckBoxList ID="chkmasters" DataTextField="Menu_Name" runat="server" RepeatDirection="Vertical"
                                                RepeatColumns="1" ForeColor="#7F525D" CellPadding="2" CellSpacing="5"
                                                Width="230px" Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="ulliMas">Subdivision</asp:ListItem>
                                                <asp:ListItem Value="lim1" Text="Subdivision Entry"></asp:ListItem>
                                                <asp:ListItem Value="lim2" Text="View - Productwise"></asp:ListItem>
                                                <asp:ListItem Value="lim3">View - Fieldforcewise <hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliProd" Text="Product"></asp:ListItem>
                                                <asp:ListItem Value="lim4" Text="Product Category"></asp:ListItem>
                                                <asp:ListItem Value="lim5" Text="Group"></asp:ListItem>
                                                <asp:ListItem Value="lim6" Text="Brand"></asp:ListItem>
                                                <asp:ListItem Value="lim7" Text="Product Detail"></asp:ListItem>
                                                <asp:ListItem Value="lim8" Text="Statewise - Rate Fixation"></asp:ListItem>
                                                <asp:ListItem Value="lim9">Statewise - Rate View<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="lim10">Field Force<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliDoc" Text="Doctor"></asp:ListItem>
                                                <asp:ListItem Value="lim11" Text="Doctor Category"></asp:ListItem>
                                                <asp:ListItem Value="lim12" Text="Speciality"></asp:ListItem>
                                                <asp:ListItem Value="lim13" Text="Class"></asp:ListItem>
                                                <asp:ListItem Value="lim14" Text="Campaign"></asp:ListItem>
                                                <asp:ListItem Value="lim15" Text="Qualification"></asp:ListItem>
                                                <asp:ListItem Value="lim16">Chemists Category<hr width="40%" /></asp:ListItem>                                                
                                                <asp:ListItem Value="lim17">Input<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliFF">Field Force Entries<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="lim18" Text="Territory"></asp:ListItem>
                                                <asp:ListItem Value="lim19" Text="Listed Doctor View"></asp:ListItem>
                                                <asp:ListItem Value="lim20" Text="Listed Doctor"></asp:ListItem>
                                                <asp:ListItem Value="lim21" Text="Chemist"></asp:ListItem>
                                                <asp:ListItem Value="lim22" Text="Hospital"></asp:ListItem>
                                                <asp:ListItem Value="Li15">Chemists<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="li50" Text="Campaign"></asp:ListItem>
                                                <asp:ListItem Value="lim23">Unlisted Doctor<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliHol">Statewise - Holiday Fixation</asp:ListItem>
                                                <asp:ListItem Value="liHolEn">Entry</asp:ListItem>
                                                <asp:ListItem Value="liHolVw">View</asp:ListItem>
                                                <asp:ListItem Value="ulliStock">Stockist</asp:ListItem>
                                                <asp:ListItem Value="lim25" Text="Stockist Add"></asp:ListItem>
                                                <asp:ListItem Value="lim26" Text="Stockist Map"></asp:ListItem>
                                                <asp:ListItem Value="lim27" Text="Stockist View"></asp:ListItem>
                                                <asp:ListItem Value="lim28" Text="Entry Status"></asp:ListItem>
                                                <asp:ListItem Value="lim29" Text="HQ Creation"></asp:ListItem>
                                                <asp:ListItem Value="lim30">HQ Updation<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="liSSRpt101" Text="At a Glance"></asp:ListItem>
                                                <asp:ListItem Value="ulliExpense">Expense</asp:ListItem>
                                                <asp:ListItem Value="lim31" Text="SFC Updation"></asp:ListItem>
                                                <asp:ListItem Value="lim32" Text="SFC View"></asp:ListItem>
                                                <asp:ListItem Value="lim33" Text="Allowance Fixation Entry"></asp:ListItem>
                                                <asp:ListItem Value="liAllw" Text="Allowance Fixation View"></asp:ListItem>
                                                <asp:ListItem Value="lim34" Text="Wrk Type Wise - Allowance Fix"></asp:ListItem>
                                                <asp:ListItem Value="lim35" Text="Fixed/Variable Expense Parameter"></asp:ListItem>
                                                <asp:ListItem Value="exp_setup" Text="Expense Setup"></asp:ListItem>
                                                <asp:ListItem Value="ulliMGE">Manager Expense</asp:ListItem>
                                                <asp:ListItem Value="limMGR31" Text="Allowance Fixation (Automatic)"></asp:ListItem>
                                                <asp:ListItem Value="liMGRsfc2" Text="SFC Updation"></asp:ListItem>
                                                <asp:ListItem Value="liallSFC" Text="SFC View"></asp:ListItem>
                                                <asp:ListItem Value="liMGRsfcvw" Text="Wrk Type Wise - Allowance Fix"></asp:ListItem>
                                                <asp:ListItem Value="ulliEmp">Employee Personal Details</asp:ListItem>
                                                <asp:ListItem Value="liPersonalEntry" Text="Entry"></asp:ListItem>
                                                <asp:ListItem Value="liPersonalSts" Text="Status"></asp:ListItem>
                                                <asp:ListItem Value="ulliCRM">Doctor - Service CRM</asp:ListItem>
                                                <asp:ListItem Value="limCRM101" Text="Doctor Service CRM - Approval"></asp:ListItem>
                                                <asp:ListItem Value="limCRM102" Text="Doctor Service CRM - Status"></asp:ListItem>
                                                <asp:ListItem Value="limCRM103" Text="Doctor Service CRM - Analysis"></asp:ListItem>
                                                <asp:ListItem Value="li15Comp">Competitor Info</asp:ListItem>
                                            </asp:CheckBoxList>



                                        </div>
                                    </div>

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Activities </h3>

                                            <asp:CheckBoxList ID="ChkActive" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="ulliApp">Approval</asp:ListItem>
                                                <asp:ListItem Value="liA1" Text="Listed dr Addition Approval"></asp:ListItem>
                                                <asp:ListItem Value="liA2" Text="Listed Dr Deactivation Approval"></asp:ListItem>
                                                <asp:ListItem Value="liA3" Text="TP Approval"></asp:ListItem>
                                                <asp:ListItem Value="liA4" Text="DCR Approval"></asp:ListItem>
                                                <asp:ListItem Value="liA5" Text="Leave Approval"></asp:ListItem>
                                                <asp:ListItem Value="liA5" Text="Leave Approval"></asp:ListItem>
                                                <asp:ListItem Value="ulliExApp">Expense</asp:ListItem>
                                                <asp:ListItem Value="liA23">Login Details</asp:ListItem>
                                                <asp:ListItem Value="li1Audit">Audit Report</asp:ListItem>
                                                <asp:ListItem Value="Li600SS_Adm">Secondary Sales Entry</asp:ListItem>
                                                <asp:ListItem Value="LiExpe">Approval(Active)</asp:ListItem>
                                                <asp:ListItem Value="LResig">Expense Approval(Vacant/Resigned)</asp:ListItem>
                                                <asp:ListItem Value="LiConsView">Expense ConSolidated View</asp:ListItem>
                                                <asp:ListItem Value="LiAuditExpense">Audited Expense Data DownLoad</asp:ListItem>
                                                <asp:ListItem Value="lilogin">Login Into Fieldforce</asp:ListItem>

                                                <asp:ListItem Value="ulliDocBus">Doctor Business</asp:ListItem>
                                                <asp:ListItem Value="liA7" Text="Doctor Business Entry"></asp:ListItem>
                                                <asp:ListItem Value="liA8">Doctor Business View<hr width="40%" /></asp:ListItem>

                                                <asp:ListItem Value="LiCamp_bus">Campaign Doctor Business</asp:ListItem>
                                                <asp:ListItem Value="liCamp_bus_Vw">Campaign Doctor Business View<hr width="40%" /></asp:ListItem>

                                                <asp:ListItem Value="ulliSamDes">Sample Despatch</asp:ListItem>
                                                <asp:ListItem Value="liA9" Text="Sample Despatch Entry"></asp:ListItem>
                                                <asp:ListItem Value="liA10" Text="Sample Despatch Edit"></asp:ListItem>
                                                <asp:ListItem Value="liA11" Text="Sample Despatch Delete"></asp:ListItem>
                                                <asp:ListItem Value="liA12" Text="Sample Despatch View"></asp:ListItem>
                                                <asp:ListItem Value="liA13">Sample Despatch Status<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliInDes">Input Despatch</asp:ListItem>
                                                <asp:ListItem Value="liA14" Text="Input Despatch Entry"></asp:ListItem>
                                                <asp:ListItem Value="liA15" Text="Input Despatch Edit"></asp:ListItem>
                                                <asp:ListItem Value="liA16" Text="Input Despatch Delete"></asp:ListItem>
                                                <asp:ListItem Value="liA17" Text="Input Despatch View"></asp:ListItem>
                                                <asp:ListItem Value="liA18">Input Despatch Status</asp:ListItem>
                                                <asp:ListItem Value="ulliTarget">Target Fixation</asp:ListItem>
                                                <asp:ListItem Value="liA19" Text="Target Fixation Entry"></asp:ListItem>
                                                <asp:ListItem Value="liA20">Target Fixation View<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="TarvsSal" Text="Target Vs Sales"></asp:ListItem>
                                                <asp:ListItem Value="Li2">RCPA</asp:ListItem>
                                                <asp:ListItem Value="li11" Text="RCPA Entry"></asp:ListItem>
                                                <asp:ListItem Value="li12">RCPA View<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliLeave">Leave Entitlement</asp:ListItem>
                                                <asp:ListItem Value="liA21" Text="Leave Entitlement Entry"></asp:ListItem>
                                                <asp:ListItem Value="liA22" Text="Leave Entitlement View"></asp:ListItem>
                                                <asp:ListItem Value="LiDocBusVal">Doctor Business Valuewise</asp:ListItem>
                                                <asp:ListItem Value="liDocBusVwEntry" Text="Doctor Business Valuewise Entry"></asp:ListItem>
                                                <asp:ListItem Value="liDocBusVwView" Text="Doctor Business Valuewise View"></asp:ListItem>
                                                <asp:ListItem Value="ulliSFE" Text="SFE - KPI"></asp:ListItem>
                                                <asp:ListItem Value="ulliEval">Evaluation</asp:ListItem>
                                                <asp:ListItem Value="li40" Text="Entry"></asp:ListItem>
                                                <asp:ListItem Value="li41" Text="View"></asp:ListItem>
                                                <asp:ListItem Value="Li34">Payslip</asp:ListItem>
                                                <asp:ListItem Value="liOp44" Text="View"></asp:ListItem>
                                                <asp:ListItem Value="li35" Text="Status"></asp:ListItem>
                                            </asp:CheckBoxList>



                                        </div>
                                    </div>

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Activity Reports </h3>


                                            <asp:CheckBoxList ID="ChkAcReports" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="ulliRoute" Text="Route Plan"></asp:ListItem>
                                                <asp:ListItem Value="liAR1" Text="Route Plan View"></asp:ListItem>
                                                <asp:ListItem Value="liAR2">Route Plan Status<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliTP" Text="TP"></asp:ListItem>
                                                <asp:ListItem Value="liAR3" Text="TP Consolidated View"></asp:ListItem>
                                                <asp:ListItem Value="liAR4" Text="TP View"></asp:ListItem>
                                                <asp:ListItem Value="liAR5">TP Status<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliDCR" Text="DCR"></asp:ListItem>
                                                <asp:ListItem Value="liAR6" Text="DCR View"></asp:ListItem>
                                                <asp:ListItem Value="liAR7" Text="DCR Status"></asp:ListItem>
                                                <asp:ListItem Value="liAR8" Text="DCR Not Approved"></asp:ListItem>
                                                <asp:ListItem Value="liAR9">DCR Not Submitted<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="liAR10" Text="Dash Board"></asp:ListItem>
                                                <asp:ListItem Value="ullisurvey">Survey<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="li74" Text="Question Creation"></asp:ListItem>
                                                <asp:ListItem Value="li75" Text="Updation"></asp:ListItem>
                                                <asp:ListItem Value="li76" Text="View"></asp:ListItem>
                                            </asp:CheckBoxList>


                                        </div>
                                    </div>

                                </div>


                                <div class="row clearfix">
                                    <%-- <div class="col-lg-1">
                                         </div>--%>
                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">MIS Reports </h3>

                                            <asp:CheckBoxList ID="ChkMIS" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="ulliMgrAly" Text="Manager Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liMis1" Text="HQ - Coveragewise"></asp:ListItem>
                                                <asp:ListItem Value="liMis2" Text="Coverage Analysis 1"></asp:ListItem>
                                                <asp:ListItem Value="liMis3" Text="Coverage Analysis 2"></asp:ListItem>
                                                <asp:ListItem Value="liMis4" Text="Joint Workwise"></asp:ListItem>
                                                <asp:ListItem Value="liMis5">FieldWork Manager - Analysis<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliAnaly" Text="Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liMis6" Text="DCR Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liMis7" Text="Visit Analysis"></asp:ListItem>

                                                <asp:ListItem Value="ulliSingle" Text="Single Analysis"></asp:ListItem>
                                                <asp:ListItem Value="li8doc" Text="Doctor - Analysis"></asp:ListItem>
                                                <asp:ListItem Value="li10Rep" Text="Rep Vs Manager"></asp:ListItem>
                                                <asp:ListItem Value="li15Rev" Text="Review Report"></asp:ListItem>
                                                <asp:ListItem Value="li18Ass" Text="Assessment Report"></asp:ListItem>


                                                <asp:ListItem Value="liMis8">POB Wise<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="liMis9">Missed Call Report<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliVisit" Text="Visit Details"></asp:ListItem>
                                                <asp:ListItem Value="liMis10" Text="Cat/Cls/Splty/LstDr Wise"></asp:ListItem>
                                                <asp:ListItem Value="liMis11" Text="Datewise"></asp:ListItem>
                                                <asp:ListItem Value="liMis12" Text="Doctorwise(Periodically)"></asp:ListItem>
                                                <asp:ListItem Value="liMis13" Text="Call Feedbackwise"></asp:ListItem>
                                                <asp:ListItem Value="liMis14" Text="Fixationwise(By Visit)"></asp:ListItem>
                                                <asp:ListItem Value="liMis15" Text="Based on Modewise"></asp:ListItem>
                                                <asp:ListItem Value="liMis16">at a Glance</asp:ListItem>
                                                <asp:ListItem Value="lblvacant" Text="Manager - Visit(Vacant HQ's)"></asp:ListItem>
                                                <asp:ListItem Value="liMis_Chm_Ul">Chemist & UnListed Doctors<hr width="40%" />
                                                </asp:ListItem>
                                                <asp:ListItem Value="ulliSales" Text="Sales Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liMis17" Text="Stock & Sales"></asp:ListItem>
                                                <asp:ListItem Value="liMis18" Text="Consolidated View"></asp:ListItem>
                                                <asp:ListItem Value="liMis19">Secondary Sales Report<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliCamp" Text="Campaign"></asp:ListItem>
                                                <asp:ListItem Value="liMis20" Text="Campaign View"></asp:ListItem>
                                                <asp:ListItem Value="liMis21">Campaign Status<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliProductEx" Text="Product Exposure"></asp:ListItem>
                                                <asp:ListItem Value="liMis22" Text="Product Exposure Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liMis23" Text="Speciality/Category Wise"></asp:ListItem>
                                                <asp:ListItem Value="liMis24">ListedDr - Productwise Visit<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="prdunlisted" Text="Product Exposure Analysis - Unlisted Doctor"></asp:ListItem>

                                                <asp:ListItem Value="ulliSamIn" Text="Sample/Input"></asp:ListItem>
                                                <asp:ListItem Value="liMis25" Text="Sample Issued - Fieldforce Wise"></asp:ListItem>
                                                <asp:ListItem Value="liMis26">Input Issued - Fieldforce Wise<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliStat" Text="Status"></asp:ListItem>
                                                <asp:ListItem Value="liMis27" Text="Delayed Status"></asp:ListItem>
                                                <asp:ListItem Value="activefield">Leave Status<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="Li1perio">Leave Status Periodically</asp:ListItem>
                                                <asp:ListItem Value="ulliliMis29" Text="Exception"></asp:ListItem>
                                                <asp:ListItem Value="tpmr" Text="Tour Plan - Baselevel"></asp:ListItem>
                                                <asp:ListItem Value="tpmgr" Text="Tour Plan - Managers"></asp:ListItem>
                                                <asp:ListItem Value="tpglance" Text="Exception - At a Glance"></asp:ListItem>
                                                <asp:ListItem Value="ulliBill" Text="Primary Bill"></asp:ListItem>
                                                <asp:ListItem Value="liB1" Text="Stockist View"></asp:ListItem>
                                                <asp:ListItem Value="liB2" Text="Product View"></asp:ListItem>

                                                <asp:ListItem Value="ulliOpt" Text="Options"></asp:ListItem>
                                                <asp:ListItem Value="liMisResign" Text="Resigned User Status"></asp:ListItem>
                                                <asp:ListItem Value="liMis30Join" Text="Join/Left Details"></asp:ListItem>
                                                <asp:ListItem Value="liQuizRes" Text="Quiz Test Result"></asp:ListItem>

                                                 <asp:ListItem Value="ulliRprt">Report</asp:ListItem>
                                                <asp:ListItem Value="li24" Text="Attendance"></asp:ListItem>
                                                <asp:ListItem Value="li32" Text="Spclty Wise Coverage Analysis"></asp:ListItem>
                                                <asp:ListItem Value="li37" Text="SS - Inventory Stockistwise"></asp:ListItem>
                                                <asp:ListItem Value="li41" Text="SS - Inventory Productwise"></asp:ListItem>
                                                 <asp:ListItem Value="licomp" Text="Comprehensive Work Analysis"></asp:ListItem>
                                                <asp:ListItem Value="li40" Text="Sales Consolidated"></asp:ListItem>
                                                 <asp:ListItem Value="li44" Text="Sales All Stockist"></asp:ListItem>
                                                <asp:ListItem Value="li47" Text="Primary Sales Consolidated"></asp:ListItem>
                                                <asp:ListItem Value="li5" Text="Secondary Sales Consolidated"></asp:ListItem>
                                                 <asp:ListItem Value="li30_new" Text="Secondary Sales Consolidated(Old)"></asp:ListItem>
                                                <asp:ListItem Value="ulliSecondary" Text="Secondary Sale(2019-2020/2020-2021)"></asp:ListItem>
                                            
                                                <asp:ListItem Value="Li42">Digital Detailing</asp:ListItem>
                                                <asp:ListItem Value="li43" Text="Visit wise"></asp:ListItem>
                                                <asp:ListItem Value="li49" Text="Brand wise Star Rating"></asp:ListItem>
                                                <asp:ListItem Value="li48" Text="Product Slide Analyis"></asp:ListItem>
                                                <asp:ListItem Value="li488" Text="Slide wise - Attendance"></asp:ListItem>
                                                <asp:ListItem Value="ullicore">MVD</asp:ListItem>
                                                 <asp:ListItem Value="li39" Text="Visit View"></asp:ListItem>
                                                <asp:ListItem Value="li28" Text="Status"></asp:ListItem>
                                                <asp:ListItem Value="li29" Text="Coverage"></asp:ListItem>
                                                <asp:ListItem Value="li36" Text="Visit - Excel Dump"></asp:ListItem>
                                                <asp:ListItem Value="ulliDump">Dump</asp:ListItem>
                                                 <asp:ListItem Value="li45" Text="Visit - Drs"></asp:ListItem>
                                                <asp:ListItem Value="li46" Text="Secondary Sale"></asp:ListItem>
                                                <asp:ListItem Value="li20" Text="Secondary Sale - Comparision"></asp:ListItem>
                                                <asp:ListItem Value="li21" Text="Listeddr"></asp:ListItem>
                                                <asp:ListItem Value="li22" Text="Chemist"></asp:ListItem>
                                                <asp:ListItem Value="li17" Text="Campaign Visit - Drs"></asp:ListItem>
                                                <asp:ListItem Value="liLstStk" Text="Listed Stockiest"></asp:ListItem>
                                                 <asp:ListItem Value="ulldump2">Dump-II</asp:ListItem>
                                                 <asp:ListItem Value="lidrBusPro" Text="Doctor Product Business"></asp:ListItem>
                                                <asp:ListItem Value="sample_Dump" Text="Sample-Issued Excel Download"></asp:ListItem>
                                                <asp:ListItem Value="input_Dump" Text="Input-Issued Excel Download"></asp:ListItem>
                                                <asp:ListItem Value="Lidocaddde">Doctor</asp:ListItem>
                                                 <asp:ListItem Value="liaddddd" Text="Add/Deactive Status"></asp:ListItem>
                                            </asp:CheckBoxList>



                                        </div>
                                    </div>

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Option </h3>

                                            <asp:CheckBoxList ID="ChkOpt" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="liOp1">Change Password<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliVacant" Text="Vacant MR Login"></asp:ListItem>
                                                <asp:ListItem Value="liOp2" Text="Vacant MR Login Access"></asp:ListItem>
                                                <asp:ListItem Value="liOp3">Permission for Managers<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliUpdate" Text="Update/Delete"></asp:ListItem>
                                                <asp:ListItem Value="liOp4" Text="TP Update"></asp:ListItem>
                                                <asp:ListItem Value="liOp5" Text="TP Delete"></asp:ListItem>
                                                <asp:ListItem Value="liOp6" Text="DCR Edit"></asp:ListItem>
                                                <asp:ListItem Value="liOp7" Text="SS Edit"></asp:ListItem>
                                                <asp:ListItem Value="liOp8" Text="Mail Delete"></asp:ListItem>
                                                <asp:ListItem Value="leavecancel" Text="Leave Cancellation"></asp:ListItem>
                                                <asp:ListItem Value="Licmp_Rst" Text="Campaig Lock Reset"></asp:ListItem>
                                                <asp:ListItem Value="Licmp_bus_Rst" Text="Campaign Bussiness Lock Reset"></asp:ListItem>

                                                <asp:ListItem Value="liOp9">Screen Access Rights<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliSetup" Text="Basic Setups"></asp:ListItem>
                                                <asp:ListItem Value="liOp10" Text="Base Level"></asp:ListItem>
                                                <asp:ListItem Value="liOp11" Text="Managers"></asp:ListItem>
                                                <asp:ListItem Value="liOp12" Text="Approval Mandatory"></asp:ListItem>
                                                <asp:ListItem Value="liOp13" Text="Managerwise Core Doctor Map"></asp:ListItem>
                                                <asp:ListItem Value="liOp14" Text="Screenwise Access"></asp:ListItem>
                                                <asp:ListItem Value="liOp15">Mail Folder Creation<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="liOther" Text="Other Setup"></asp:ListItem>
                                                <asp:ListItem Value="ulliAppSet" Text="App Setups"></asp:ListItem>
                                                <asp:ListItem Value="liOp16" Text="Call Feedback"></asp:ListItem>
                                                <asp:ListItem Value="liOp17" Text="Call Remarks Templates"></asp:ListItem>
                                                <asp:ListItem Value="liOp18">Notification Message<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliSecSet" Text="SS Setups"></asp:ListItem>
                                                <asp:ListItem Value="liOp19" Text="Seconday Sale Setup 1"></asp:ListItem>
                                                <asp:ListItem Value="liOp20" Text="Seconday Sale Setup 2"></asp:ListItem>
                                                <asp:ListItem Value="liOp21">Seconday Sale Setup 3<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="liOp22">Mail Box<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliInUp" Text="Information Upload"></asp:ListItem>
                                                <asp:ListItem Value="liOp23" Text="Flash News"></asp:ListItem>
                                                <asp:ListItem Value="liOp24" Text="Notice Board"></asp:ListItem>
                                                <asp:ListItem Value="liOp25" Text="Quote"></asp:ListItem>
                                                <asp:ListItem Value="liOp26" Text="Talk to Us"></asp:ListItem>
                                                <asp:ListItem Value="liOp27" Text="File Upload"></asp:ListItem>
                                                <asp:ListItem Value="liOp28">User Manual Upload<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliUpld" Text="Upload"></asp:ListItem>
                                                <asp:ListItem Value="liOp29" Text="Listed Doctor Upload"></asp:ListItem>
                                                <asp:ListItem Value="liOp30" Text="Listed Doctor Bulk"></asp:ListItem>
                                                <asp:ListItem Value="liOp31" Text="Chemist Upload"></asp:ListItem>
                                                <asp:ListItem Value="liOp32" Text="Chemist - Bulk"></asp:ListItem>
                                                <asp:ListItem Value="liOp33" Text="Field Force Upload"></asp:ListItem>
                                                <asp:ListItem Value="liOp34" Text="Stockist Upload"></asp:ListItem>
                                                <asp:ListItem Value="liOp35" Text="Product Upload"></asp:ListItem>
                                                <asp:ListItem Value="liOp36" Text="Primary Upload"></asp:ListItem>
                                                <asp:ListItem Value="ulliTran" Text="Transaction Upload"></asp:ListItem>
                                                <asp:ListItem Value="libill">Primary Bill Upload</asp:ListItem>
                                                <asp:ListItem Value="liOp37">Payslip Upload<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="liPay">Payslip File Upload<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliImgUpld" Text="Image Upload"></asp:ListItem>
                                                <asp:ListItem Value="liOp38" Text="Home Page(Common For All)"></asp:ListItem>
                                                <asp:ListItem Value="liOp39">Home Page(FieldForcewise)<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="liOp40">Leave Status<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="ulliTrans" Text="Transfer"></asp:ListItem>
                                                <asp:ListItem Value="liOp41" Text="Transfer - Master Details"></asp:ListItem>
                                                <asp:ListItem Value="liOp42">Convert Unlisted Drs - Listed Drs<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="liOp43" Text="Release - Missing Dates / Delay"></asp:ListItem>
                                                <asp:ListItem Value="liOp44" Text="PaySlip View"></asp:ListItem>

                                                <asp:ListItem Value="liOp45" Text="Primary View"></asp:ListItem>

                                                <asp:ListItem Value="liQuiz101" Text="Quiz"></asp:ListItem>
                                                <asp:ListItem Value="liQuiz_CA102" Text="Quiz Category"></asp:ListItem>
                                                <asp:ListItem Value="Li_lstdr7">Customer Upload<hr width="40%" /></asp:ListItem>
                                                <asp:ListItem Value="li_lst20" Text="Listed Doctor"></asp:ListItem>
                                                <asp:ListItem Value="liTr4" Text="Target"></asp:ListItem>
                                            </asp:CheckBoxList>

                                        </div>
                                    </div>

                                </div>

                            </asp:Panel>
                        </center>
                        <br />
                        <center>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="false" CssClass="savebutton" />
                        </center>
                    </asp:Panel>

                    <br />
                    <asp:Panel ID="pnlmgr" runat="server" Visible="false">
                        <center>
                            <asp:Panel ID="Panel2" runat="server">


                                <div class="row clearfix">

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Information </h3>

                                            <asp:CheckBoxList ID="chkInf" DataTextField="Menu_Name" runat="server" RepeatDirection="Vertical"
                                                RepeatColumns="1" ForeColor="#7F525D" CellPadding="2" CellSpacing="5"
                                                Width="200px" Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="liIn1" Text="Product Information"></asp:ListItem>
                                                <asp:ListItem Value="liIn2" Text="Subordinate Detail"></asp:ListItem>
                                                <asp:ListItem Value="ulliMas" Text="Master View"></asp:ListItem>
                                                <asp:ListItem Value="liIn3" Text="Listed Doctor View"></asp:ListItem>
                                                <asp:ListItem Value="liIn4" Text="Chemist View"></asp:ListItem>
                                                <asp:ListItem Value="liIn5" Text="Hospital View"></asp:ListItem>
                                                <asp:ListItem Value="liIn6" Text="Unlisted Doctor View"></asp:ListItem>
                                                <asp:ListItem Value="ulliApp" Text="Approvals"></asp:ListItem>
                                                <asp:ListItem Value="liIn7" Text="Listed Dr Addition Approval"></asp:ListItem>
                                                <asp:ListItem Value="liIn8" Text="Listed Dr DeActivation Approval"></asp:ListItem>
                                                <asp:ListItem Value="liIn9" Text="TP Approval"></asp:ListItem>
                                                <asp:ListItem Value="liIn10" Text="DCR Approval"></asp:ListItem>
                                                <asp:ListItem Value="liIn11" Text="Leave Approval"></asp:ListItem>
                                                <asp:ListItem Value="liIn12" Text="Secondary Sales Approval"></asp:ListItem>
                                                <asp:ListItem Value="liIn101" Text="Doctor Service CRM Approval"></asp:ListItem>
                                                <asp:ListItem Value="liIn102" Text="Doctor Service CRM Status"></asp:ListItem>
                                                <asp:ListItem Value="liIn13" Text="Doctor - Speciality List"></asp:ListItem>
                                                <asp:ListItem Value="liIn14" Text="Holiday List"></asp:ListItem>
                                                <asp:ListItem Value="liIn15" Text="Listed Doctor Approval Status"></asp:ListItem>
                                                <asp:ListItem Value="liIn16" Text="Unlisted Dr Convert to Listed Dr"></asp:ListItem>
                                                <asp:ListItem Value="ullimgExp1">Manager Expense</asp:ListItem>
                                                <asp:ListItem Value="liMGRaaa" Text="Allowance Fixation (Automatic)"></asp:ListItem>
                                                <asp:ListItem Value="liMGRsfccc" Text="SFC Updation"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Activities </h3>

                                            <asp:CheckBoxList ID="chkmgrAct" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="ulliRo" Text="Territory"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct1" Text="Territory View"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct2" Text="Territory Status"></asp:ListItem>
                                                <asp:ListItem Value="ulliTP" Text="TP"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct3" Text="Consolidated View"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct4" Text="TP Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct5" Text="TP Edit"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct6" Text="TP View"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct7" Text="TP Status"></asp:ListItem>
                                                <asp:ListItem Value="ulliDCR" Text="DCR"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct8" Text="DCR Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct9" Text="DCR View"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct10" Text="DCR Status"></asp:ListItem>
                                                <asp:ListItem Value="ulliBusMGR" Text="Doctor Business"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct14" Text="Doctor Business Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct15" Text="Doctor Business View"></asp:ListItem>
                                                <asp:ListItem Value="LiMGRDocBusVal" Text="Doctor Business Valuewise"></asp:ListItem>
                                                <asp:ListItem Value="liMGRDocBusVwEntry" Text="Doctor Business Entry Valuewise"></asp:ListItem>
                                                <asp:ListItem Value="liMGRDocBusVwView" Text="Doctor Business View Valuewise"></asp:ListItem>

                                                <asp:ListItem Value="LiMGRCampDocBusVal" Text="Campaign Doctor Business Valuewise"></asp:ListItem>
                                                <asp:ListItem Value="liCamp_bus_en" Text="Campaign Doctor Business Entry Valuewise"></asp:ListItem>
                                                <asp:ListItem Value="liCamp_bus_Vw" Text="Campaign Doctor Business View Valuewise"></asp:ListItem>

                                                <asp:ListItem Value="ulliEX" Text="Expense"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct11" Text="Expense Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMgrAct12" Text="Expense View"></asp:ListItem>
                                            </asp:CheckBoxList>

                                        </div>
                                    </div>

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">MIS Reports </h3>

                                            <asp:CheckBoxList ID="ChkMgrMis" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="liMgrMis1" Text="Doctor Details View"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis2" Text="Fieldforce Status"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis3" Text="Call Average View"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis4" Text="Work Type View Status"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis5" Text="Missed Call Report"></asp:ListItem>
                                                <asp:ListItem Value="ulliVisit" Text="Visit Details"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis6" Text="Cat/Cls/Splty/LstDr Wise"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis7" Text="Visit Detail - Datewise"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis8" Text="Doctorwise(Periodically)"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis9" Text="Call Feedbackwise"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis10" Text="Fixationwise(By Visit)"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis11" Text="Based on Modewise"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis12" Text="at a Glance"></asp:ListItem>
                                                <asp:ListItem Value="ulliSS" Text="Secondary Sale"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis13" Text="Secondary Sales View"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis14" Text="Secondary Sales Entry Status"></asp:ListItem>
                                                <asp:ListItem Value="ulliAnl" Text="Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis15" Text="DCR Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis16" Text="HQ - Coveragewise"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis17" Text="Coverage Analysis 1"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis18" Text="Coverage Analysis 2"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis19" Text="Joint Workwise"></asp:ListItem>

                                                <asp:ListItem Value="liMgrMis20" Text="Visit Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liVstGlnce" Text="Visit At A Glance"></asp:ListItem>
                                                <asp:ListItem Value="ulliProd" Text="Product Exposure"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis21" Text="Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis22" Text="Speciality/Category Wise"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis23" Text="ListedDr - Productwise Visit"></asp:ListItem>
                                                <asp:ListItem Value="ulliCamp" Text="Campaign"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis24" Text="Campaign View"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis25" Text="Campaign Status"></asp:ListItem>

                                                <asp:ListItem Value="ullitpExp" Text="Exception"></asp:ListItem>
                                                <asp:ListItem Value="liMgrMis26" Text="Tour Plan Baselevel"></asp:ListItem>
                                                <asp:ListItem Value="tpmgr" Text="Tour Plan Managers"></asp:ListItem>

                                                <asp:ListItem Value="Target">Target Fixation</asp:ListItem>
                                                <asp:ListItem Value="liA20tarr" Text="View"></asp:ListItem>
                                            </asp:CheckBoxList>

                                        </div>
                                    </div>

                                </div>

                                <div class="row clearfix">

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Option </h3>

                                            <asp:CheckBoxList ID="ChkMgrOpt" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="liMgrOpt1" Text="Change Password"></asp:ListItem>
                                                <asp:ListItem Value="liMgrOpt2" Text="Mail Box"></asp:ListItem>
                                                <asp:ListItem Value="liMgrOpt3" Text="Files Download"></asp:ListItem>
                                                <asp:ListItem Value="liMgrOpt4" Text="User Manual - View"></asp:ListItem>
                                                <asp:ListItem Value="liMgrOpt5" Text="Vacant - MR Login Access"></asp:ListItem>
                                                <asp:ListItem Value="ulliLeave" Text="Leave"></asp:ListItem>
                                                <asp:ListItem Value="liMgrOpt6" Text="Leave Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMgrOpt7" Text="Leave Status"></asp:ListItem>
                                                <asp:ListItem Value="liMgrOpt8" Text="Payslip View"></asp:ListItem>
                                                <asp:ListItem Value="liPV" Text="Payslip File View"></asp:ListItem>
                                                <asp:ListItem Value="liMr_drCamp" Text="Doctor - Campaign Map"></asp:ListItem>
                                            </asp:CheckBoxList>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </center>
                    </asp:Panel>
                    <br />
                    <center>
                        <asp:Button ID="btnMgr" runat="server" Text="Save MGR" CssClass="savebutton" Visible="false"
                            OnClick="btnMgr_Click" />
                    </center>
                    <br />
                    <asp:Panel ID="pnlMR" runat="server" Visible="false">
                        <center>
                            <asp:Panel ID="pnlmrHome" runat="server">


                                <div class="row clearfix">

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Information </h3>

                                            <asp:CheckBoxList ID="chkMrInf" DataTextField="Menu_Name" runat="server" RepeatDirection="Vertical"
                                                RepeatColumns="1" ForeColor="#7F525D" CellPadding="2" CellSpacing="5"
                                                Width="200px" Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="liMRIn1" Text="Product Information"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn2" Text="Territory"></asp:ListItem>
                                                <asp:ListItem Value="ulliDoc" Text="Listed Doctor"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn3" Text="Listed Doctor Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn4" Text="Listed Doctor View"></asp:ListItem>
                                                <asp:ListItem Value="ulliChem" Text="Chemist"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn5" Text="Chemist Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn6" Text="Chemist View"></asp:ListItem>
                                                <asp:ListItem Value="ulliHos" Text="Hospital"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn7" Text="Hospital Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn8" Text="Hospital View"></asp:ListItem>
                                                <asp:ListItem Value="ulliUn" Text="UnListed Doctor"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn9" Text="Unlisted Doctor Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn10" Text="Unlisted Doctor View"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn11" Text="Doctor - Speciality List"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn12" Text="Holiday List"></asp:ListItem>
                                                <asp:ListItem Value="liMRIn13" Text="Listed Doctor Approval Status"></asp:ListItem>
                                            </asp:CheckBoxList>


                                        </div>
                                    </div>

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Activities </h3>
                                            <asp:CheckBoxList ID="ChkMrAct" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="ulliTerr" Text="Territory"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct1" Text="Territory Normal"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct2" Text="Territory Classic (Categorywise)"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct3" Text="Territory View"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct4" Text="Territory Status"></asp:ListItem>
                                                <asp:ListItem Value="ulliTP" Text="TP"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct5" Text="TP Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct6" Text="TP View"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct7" Text="TP Status"></asp:ListItem>
                                                <asp:ListItem Value="ulliDCR" Text="DCR"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct8" Text="DCR Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct9" Text="DCR View"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct10" Text="DCR Status"></asp:ListItem>
                                                <asp:ListItem Value="LiMR4" Text="RCPA"></asp:ListItem>
                                                <asp:ListItem Value="liMR5" Text="RCPA Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMR6" Text="RCPA View"></asp:ListItem>
                                                <asp:ListItem Value="ulliSS" Text="Secondary Sales"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct11" Text="Secondary Sale Entry"></asp:ListItem>
                                                <%--  <asp:ListItem Value="" Text="Expense"></asp:ListItem>--%>
                                                <asp:ListItem Value="liMrAct12" Text="Expense Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct13" Text="Expense View"></asp:ListItem>
                                                <asp:ListItem Value="ulliBus" Text="Doctor Business"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct14" Text="Doctor Business Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMrAct15" Text="Doctor Business View"></asp:ListItem>
                                                <asp:ListItem Value="LiMRDocBusVal" Text="Doctor Business Valuewise"></asp:ListItem>
                                                <asp:ListItem Value="liMRDocBusVwEntry" Text="Doctor Business Valuewise Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMRDocBusVwView" Text="Doctor Business Valuewise View"></asp:ListItem>

                                                <asp:ListItem Value="LiMGRCampDocBusVal" Text="Campaign Doctor Business Valuewise"></asp:ListItem>
                                                <asp:ListItem Value="liCamp_bus_en" Text="Campaign Doctor Business Entry Valuewise"></asp:ListItem>
                                                <asp:ListItem Value="liCamp_bus_Vw" Text="Campaign Doctor Business View Valuewise"></asp:ListItem>

                                                <asp:ListItem Value="ulliCRM" Text="Doctor Service CRM"></asp:ListItem>
                                                <asp:ListItem Value="liMrCRM101" Text="Doctor Service CRM Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMrCRM102" Text="Doctor Service CRM Status"></asp:ListItem>
                                            </asp:CheckBoxList>

                                        </div>
                                    </div>

                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">MIS Reports </h3>

                                            <asp:CheckBoxList ID="ChkMrMis" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="liMrMis1" Text="Doctor Details View"></asp:ListItem>
                                                <asp:ListItem Value="ulliAnly" Text="Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis2" Text="DCR Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis3" Text="Visit Analysis"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis4" Text="Fieldforce Status"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis5" Text="Call Average View"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis6" Text="Work Type View Status"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis7" Text="Missed Call Report"></asp:ListItem>
                                                <asp:ListItem Value="ulliVis" Text="Visit Details"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis8" Text="Cat/Cls/Splty/LstDr Wise"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis9" Text="Visit Detail - Datewise"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis10" Text="Doctorwise(Periodically)"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis11" Text="Call Feedbackwise"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis12" Text="Fixationwise(By Visit)"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis13" Text="Based on Modewise"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis14" Text="at a Glance"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis15" Text="Secondary Sales View"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis16" Text="Campaign View"></asp:ListItem>
                                                <asp:ListItem Value="liMrMis17" Text="Exception TP"></asp:ListItem>
                                            </asp:CheckBoxList>

                                        </div>
                                    </div>
                                </div>

                                <div class="row clearfix">
                                    <div class="col-lg-3" style="margin-left: 60px;">
                                        <div class="single-block-area  clearfix" style="width: 260px; min-height: 300px;">
                                            <h3 style="padding-bottom: 5px;">Option</h3>

                                            <asp:CheckBoxList ID="ChkMrOpt" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                                ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="200px"
                                                Font-Names="'Roboto', sans-serif">
                                                <asp:ListItem Value="liMrOpt1" Text="Change Password"></asp:ListItem>
                                                <asp:ListItem Value="liMrOpt2" Text="Mail Box"></asp:ListItem>
                                                <asp:ListItem Value="liMrOpt3" Text="Files Download"></asp:ListItem>
                                                <asp:ListItem Value="liMrOpt4" Text="User Manual - View"></asp:ListItem>
                                                <asp:ListItem Value="ulliLve" Text="Leave"></asp:ListItem>
                                                <asp:ListItem Value="liMrOpt5" Text="Leave Entry"></asp:ListItem>
                                                <asp:ListItem Value="liMrOpt6" Text="Leave Status"></asp:ListItem>
                                                <asp:ListItem Value="liMrOpt7" Text="Doctor - Campaign Map"></asp:ListItem>
                                                <asp:ListItem Value="liMrOpt8" Text="Payslip View"></asp:ListItem>
                                                <asp:ListItem Value="liPV" Text="Payslip File View"></asp:ListItem>
                                                <asp:ListItem Value="liMr_drCamp" Text="Doctor - Campaign Map"></asp:ListItem>
                                                <asp:ListItem Value="LiMrSV008" Text="Status View"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                        </center>
                    </asp:Panel>
                    <br />
                    <center>
                        <asp:Button ID="btnMR" runat="server" Text="Save MR" CssClass="savebutton" Visible="false"
                            OnClick="btnMR_Click" />
                    </center>
                    <div class="loading" align="center">
                        Loading. Please wait.<br />
                        <br />
                        <img src="../Images/loader.gif" alt="" />
                    </div>
                </div>
            </div>


            <br />
            <br />
        </div>
    </form>
</body>
</html>
