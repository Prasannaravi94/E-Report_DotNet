<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminSetup.aspx.cs" Inherits="MasterFiles_AdminSetup" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Base Level Setup</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <style type="text/css">
        .hover {
            cursor: text;
        }


        /*.tableHead {
            background: #e0f3ff;
            color: black;
            border-style: solid;
            border-width: 1px;
            border-color: #a2cd5a;
        }*/

        .blink_me {
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
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        .blink {
            animation: blink-animation 1s steps(5, start) infinite;
            -webkit-animation: blink-animation 1s steps(5, start) infinite;
        }

        @keyframes blink-animation {
            to {
                visibility: hidden;
            }
        }

        @-webkit-keyframes blink-animation {
            to {
                visibility: hidden;
            }
        }

        .table-responsive {
            display: block;
            width: 100%;
            overflow-x: inherit !important;
        }

        .display-table .table td {
            padding: 14px 10px !important;
        }

        .table .input {
            width: 85%;
        }

        .display-table .table th {
            font-size: 14px !important;
        }
    </style>

    <script type="text/javascript">

        function OpenWindow_TpEntry() {
            window.open('TpEntry-Selection.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }
    </script>
    <script type="text/javascript">
        function validate() {
            var flag = true;
            var dropdowns = new Array(); //Create array to hold all the dropdown lists.
            var gridview = document.getElementById('<%=grdtp_setup.ClientID %>'); //GridView1 is the id of ur gridview.
            dropdowns = gridview.getElementsByTagName('select'); //Get all dropdown lists contained in GridView1.
            var RB1 = document.getElementById("<%=rdoDCRTP.ClientID%>");

            if (!RB1.checked) {
            }
            else {
                for (var i = 0; i < dropdowns.length; i++) {
                    if (dropdowns.item(i).value == '-1') //If dropdown has no selected value
                    {
                        flag = false;
                        break; //break the loop as there is no need to check further.
                    }
                }
                if (!flag) {
                    alert('Select Tour Plan between days');
                }
                return flag;
            }
        }
    </script>

    <script type="text/javascript">
        function HidePopup1() {

            var mpu = $find('txtDesig_PopupControlExtender');
            mpu.hide();
        }
    </script>
    <script type="text/javascript">
        function HidePopup1() {

            var mpu = $find('txtDesig_v_PopupControlExtender');
            mpu.hide();
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id*=btnSubmit]").click(function () {
                var checked_radio = $("[id*=rdoprd_priority] input:checked");
                var value = checked_radio.val();
                if (value == '1') {
                    if ($("#txtprdrange").val() == '') {
                        createCustomAlert('Enter No. of Priorities ');
                        $('#txtprdrange').focus();
                        return false;
                    }

                }
            });
        });
    </script>
    <script type="text/javascript">
        function FFWiseFunction() {
            var checkBox = document.getElementById("chkFFWiseDly");
            var text = document.getElementById("divFFWiseDly");
            if (checkBox.checked == true) {
                text.style.display = "block";
            } else {
                text.style.display = "none";
            }
        }

        $(document).ready(function () {
            var checkBox = document.getElementById("chkFFWiseDly");
            var text = document.getElementById("divFFWiseDly");
            if (checkBox.checked == true) {
                text.style.display = "block";
            } else {
                text.style.display = "none";
            }
        })
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <br />
                        <h2 class="text-center" style="border-bottom: none !important;" runat="server">Base Level S<asp:LinkButton ID="lnk" ForeColor="#292a34" runat="server" Text="e" OnClick="lnk_Click"></asp:LinkButton>tup</h2>
                        <div class="row justify-content-center">
                            <div class="col-lg-6">
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">Plan Setup</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label2" runat="server" CssClass="label">Doctor with Multiple Plan(s) Allowed</asp:Label>
                                                <asp:RadioButton ID="rdoMultiDRNo" Enabled="false" runat="server" CssClass="pull-right" Text="No" GroupName="MultiDR" />
                                                <asp:RadioButton ID="rdoMultiDRYes" Enabled="false" runat="server" CssClass="pull-right" Text="Yes" GroupName="MultiDR" />

                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblWorkAreaName" runat="server" CssClass="label">Working Area Name</asp:Label>
                                                <asp:DropDownList ID="ddlWorkingAreaList" runat="server" CssClass="pull-right">
                                                    <%--      <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        <asp:ListItem Value="1">Std. Daywise Plan</asp:ListItem>
                                                        <asp:ListItem Value="2">Std. Work Plan</asp:ListItem>
                                                        <asp:ListItem Value="3">Route Plan</asp:ListItem>
                                                        <asp:ListItem Value="4">Patches</asp:ListItem>
                                                        <asp:ListItem Value="5">Territory</asp:ListItem>
                                                        <asp:ListItem Value="6">Clusters</asp:ListItem>
                                                        <asp:ListItem Value="7">Sub Area</asp:ListItem>
                                                        <asp:ListItem Value="8">Permanent Journey Plan</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblNoofTourPlan" runat="server" CssClass="label">No.of Tour Plan view</asp:Label>
                                                <asp:TextBox ID="txtNoofTourPlan" runat="server" CssClass="input" Width="100%" MaxLength="3"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">DCR Setup</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label4" runat="server" CssClass="label">No.of Listed Doctors Allowed For DCR Entry </asp:Label>
                                                <asp:TextBox ID="txtDRAllow" runat="server" CssClass="input" Width="100%" MaxLength="3"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label5" runat="server" CssClass="label">No.of Chemists Allowed For DCR Entry </asp:Label>
                                                <asp:TextBox ID="txtChemAllow" runat="server" CssClass="input" Width="100%" MaxLength="3"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label24" runat="server" CssClass="label">No.of Stockists Allowed For DCR Entry </asp:Label>
                                                <asp:TextBox ID="txtStkAllow" runat="server" CssClass="input" Width="100%" MaxLength="3"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label6" runat="server" CssClass="label">No.of UnListed Doctors Allowed For DCR Entry </asp:Label>
                                                <asp:TextBox ID="txtUNLAllow" runat="server" CssClass="input" Width="100%" MaxLength="3"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label15" runat="server" CssClass="label">No.of Hospitals Allowed For DCR Entry </asp:Label>
                                                <asp:TextBox ID="txtHosAllow" runat="server" CssClass="input" Width="100%" MaxLength="3"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label7" runat="server" CssClass="label">Doctors listing display in DCR Entry </asp:Label>
                                                <asp:RadioButton ID="rdoDCRNone" runat="server" Text="None" GroupName="DCREntry" Checked="true" />
                                                <asp:RadioButton ID="rdoDCRSVLNo" runat="server" Text="SVL No" GroupName="DCREntry" />
                                                <asp:RadioButton ID="rdoDCRSpeciality" runat="server" Text="Speciality" GroupName="DCREntry" />
                                                <asp:RadioButton ID="rdoDCRCategory" runat="server" Text="Category" GroupName="DCREntry" />
                                                <asp:RadioButton ID="rdoClass" runat="server" Text="Class" GroupName="DCREntry" />
                                                <asp:RadioButton ID="rdoCampaign" runat="server" Visible="false" Text="Campaign" GroupName="DCREntry" />
                                            </div>
                                            <div class="single-des clearfix">

                                                <asp:Label ID="lblDrpatches" runat="server" CssClass="label">Display Patchwise Doctors in DCR</asp:Label>
                                                <asp:RadioButtonList ID="rdoDrPatch" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">

                                                <asp:Label ID="Label20" runat="server" CssClass="label">Selection in DCR - Listed Doctor as Mandatory</asp:Label>
                                                <asp:RadioButtonList ID="rdodcrDr" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>



                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label1" runat="server" CssClass="label">DCR Approval System</asp:Label>
                                                <asp:RadioButton ID="rdoAprYes" runat="server" CssClass="pull-right" Text="Needed" GroupName="APRVL" />
                                                <asp:RadioButton ID="rdoAprNo" runat="server" CssClass="pull-right" Text="Not Needed" GroupName="APRVL" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <span runat="server" id="Span3" class="label">DCR Del<asp:LinkButton ID="LinkButton1" ForeColor="#696d6e" runat="server" Text="a" OnClick="LinkButton1_Click"></asp:LinkButton>yed System</span>
                                                <asp:RadioButton ID="rdoDlyYes" runat="server" CssClass="pull-right" Text="Needed" GroupName="DLY" />
                                                <asp:RadioButton ID="rdoDlyNo" runat="server" CssClass="pull-right" Text="Not Needed" GroupName="DLY" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label29" runat="server" CssClass="label">Week Off / Holiday Calculated in Delayed </asp:Label>
                                                <asp:RadioButton ID="rdoDlyHolidayNo" runat="server" CssClass="pull-right" Text="No" GroupName="DLYHLD" />
                                                <asp:RadioButton ID="rdoDlyHoliday" runat="server" CssClass="pull-right" Text="Yes" GroupName="DLYHLD" />

                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label28" runat="server" CssClass="label">No.of Days Allowed For Delay</asp:Label>
                                                <asp:TextBox ID="txtNoDaysDly" runat="server" CssClass="input" Width="100%" MaxLength="3"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <div class="row">
                                                    <div class="col-lg-7">
                                                        <asp:CheckBox ID="chkFFWiseDly" runat="server" Text="FieldForceWise" onclick="FFWiseFunction()" />
                                                    </div>
                                                    <div class="col-lg-5">
                                                        <div id="divFFWiseDly" style="display: none;">
                                                            <div style="float: left; width: 40%">
                                                                <asp:TextBox ID="txtFFWiseDly" runat="server" CssClass="input pull-right" Width="100%" MaxLength="3" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                            </div>
                                                            <div style="float: right; width: 40%">
                                                                <asp:Button ID="btnFFWiseDly" runat="server" Text="Save" CssClass="savebutton pull-right" OnClick="btnlockdays_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label38" runat="server" CssClass="label" Visible="false">Total Lock System Needed</asp:Label>
                                                <asp:RadioButton ID="rdolock_no" runat="server" CssClass="pull-right" Text="No" Visible="false" GroupName="DLYT" />
                                                <asp:RadioButton ID="rdolock_yes" runat="server" CssClass="pull-right" Text="Yes" Visible="false" GroupName="DLYT" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lbllock_timelimit" runat="server" CssClass="label" Visible="false">Days Limit for Submitting their DCR Entry</asp:Label>
                                                <asp:TextBox ID="txtlock_timelimit" runat="server" CssClass="input" Width="100%" MaxLength="3" onkeypress="CheckNumeric(event);" Visible="false"></asp:TextBox>
                                                <asp:Button ID="btnlockdays" runat="server" Text="Save" CssClass="savebutton" OnClick="btnlockdays_Click" Visible="false" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label31" runat="server" CssClass="label">DCR Auto Post Holiday</asp:Label>
                                                <asp:RadioButton ID="rdoAutoHldNo" runat="server" CssClass="pull-right" Text="No" GroupName="APHLD" />
                                                <asp:RadioButton ID="rdoAutoHldYes" runat="server" CssClass="pull-right" Text="Yes" GroupName="APHLD" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label32" runat="server" CssClass="label">DCR Auto Post Week Off</asp:Label>
                                                <asp:RadioButton ID="rdoAutoSunNo" runat="server" CssClass="pull-right" Text="No" GroupName="APSun" />
                                                <asp:RadioButton ID="rdoAutoSunYes" runat="server" CssClass="pull-right" Text="Yes" GroupName="APSun" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lbltpbas" runat="server" CssClass="label">TP Based DCR</asp:Label>
                                                <asp:RadioButtonList ID="rdoTpBas" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label44" runat="server" CssClass="label">TP Based DCR with Deviation Reason</asp:Label>
                                                <asp:RadioButtonList ID="rdoTPdevia" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label43" runat="server" CssClass="label">TP Based DCR with Manager Approval Needed</asp:Label>
                                                <asp:RadioButtonList ID="rdotpdcr_appr" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h5 class="card-title">Chemist Setup</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label21" runat="server" CssClass="label">No.of Chemists Allowed For Entry</asp:Label>
                                                <asp:TextBox ID="txtChemAllowed" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">DCR Entry Setup</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label9" runat="server" CssClass="label">No.of Characters Allowed For Remarks </asp:Label>
                                                <asp:TextBox ID="txtFFRemarks" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label10" runat="server" CssClass="label">Maximum Product Selection Allowed For DCR Entry </asp:Label>
                                                <asp:TextBox ID="txtFFProd" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label26" runat="server" CssClass="label">No. of Products Selection Mandatory For DCR Entry </asp:Label>
                                                <asp:TextBox ID="txtMaxProd" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label11" runat="server" CssClass="label" Visible="false">Remove Session in DCR Entry </asp:Label>
                                                <asp:RadioButton ID="rdoFFDCRNo" runat="server" CssClass="pull-right" Text="No" Visible="false" GroupName="DCREntryFF" />
                                                <asp:RadioButton ID="rdoFFDCRYes" runat="server" CssClass="pull-right" Text="Yes" Visible="false" GroupName="DCREntryFF" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label12" runat="server" CssClass="label" Visible="false">Remove Time in DCR Entry </asp:Label>
                                                <asp:RadioButton ID="rdoFFDCRTimeNo" runat="server" CssClass="pull-right" Visible="false" Text="No" GroupName="DCREntryFFTime" />
                                                <asp:RadioButton ID="rdoFFDCRTimeYes" runat="server" CssClass="pull-right" Visible="false" Text="Yes" GroupName="DCREntryFFTime" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label17" runat="server" CssClass="label">Is Session Mandatory in DCR Entry </asp:Label>
                                                <asp:RadioButton ID="rdoSessMNo" runat="server" CssClass="pull-right" Text="No" GroupName="DCREntrySessM" />
                                                <asp:RadioButton ID="rdoSessMYes" runat="server" CssClass="pull-right" Text="Yes" GroupName="DCREntrySessM" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label25" runat="server" CssClass="label">Is Time Mandatory in DCR Entry </asp:Label>
                                                <asp:RadioButton ID="rdoTimeMNo" runat="server" CssClass="pull-right" Text="No" GroupName="DCREntryTimeM" />
                                                <asp:RadioButton ID="rdoTimeMYes" runat="server" CssClass="pull-right" Text="Yes" GroupName="DCREntryTimeM" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label13" runat="server" CssClass="label">After Product Selection Qty Value 0(Listed Dr Sample Qty)</asp:Label>
                                                <asp:RadioButton ID="rdoFFDCRQtyNo" CssClass="pull-right" runat="server" Text="No" GroupName="DCREntryFFQty" />
                                                <asp:RadioButton ID="rdoFFDCRQtyYes" CssClass="pull-right" runat="server" Text="Yes" GroupName="DCREntryFFQty" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label3" runat="server" CssClass="label">After Product Selection Qty Value 0(Listed Dr POB Qty)</asp:Label>
                                                <asp:RadioButtonList ID="rdoDrPOBQty0" runat="server" RepeatDirection="Horizontal" CssClass="pull-right"
                                                    Font-Bold="true">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label8" runat="server" CssClass="label">After Product Selection Qty Value 0(Chem Sample Qty)</asp:Label>
                                                <asp:RadioButtonList ID="rdoChemSampleQty0" runat="server" RepeatDirection="Horizontal" CssClass="pull-right"
                                                    Font-Bold="true">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label18" runat="server" CssClass="label">After Product Selection Qty Value 0(Chem POB Qty)</asp:Label>
                                                <asp:RadioButtonList ID="rdoChemPOBQty0" runat="server" RepeatDirection="Horizontal" CssClass="pull-right"
                                                    Font-Bold="true">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblprdfeed" runat="server" CssClass="label">Productwise Feedback Selection in DCR Entry Needed</asp:Label>
                                                <asp:RadioButtonList ID="rdoprdfeedYes_No" runat="server" CssClass="pull-right" AutoPostBack="true" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="rdoprdfeedYes_No_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:DropDownList ID="ddlprdfeed" runat="server" Visible="false"></asp:DropDownList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblRxqnty" runat="server" CssClass="label">Productwise Updation</asp:Label>
                                                <asp:RadioButtonList ID="rdoRxqntyYes_No" runat="server" RepeatDirection="Horizontal" CssClass="pull-right">
                                                    <%--<asp:ListItem Value="1">Rx</asp:ListItem>
                                                    <asp:ListItem Value="2">POB</asp:ListItem>
                                                    <asp:ListItem Value="0">None</asp:ListItem>--%>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblRxqntyYes_No_Caption" runat="server" Text="Caption" CssClass="label"></asp:Label>
                                                <asp:TextBox ID="txtRxqntyYes_No" runat="server" CssClass="input" Width="100%" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblChemistQty" runat="server" Text="Chemistwise/Productwise Order Qty Updation" CssClass="label"></asp:Label>
                                                <asp:RadioButtonList ID="rdoChemistQty" runat="server" RepeatDirection="Horizontal" CssClass="pull-right">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblChemistQty_Caption" runat="server" Text="Caption" CssClass="label"></asp:Label>
                                                <asp:TextBox ID="txtChemistQty" runat="server" CssClass="input" Width="100%" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblDr_SampleQty_Needed" runat="server" Text="Listed Doctorwise Sample Qty Needed" CssClass="label"></asp:Label>
                                                <asp:RadioButtonList ID="rdoDr_SampleQty_Needed" runat="server" RepeatDirection="Horizontal" CssClass="pull-right">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblDr_SampleQty_Caption" runat="server" Text="Caption" CssClass="label"></asp:Label>
                                                <asp:TextBox ID="txtDr_SampleQty_Caption" runat="server" MaxLength="15" CssClass="input" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblChem_SampleQty_Needed" runat="server" Text="Chemistwise Sample Qty Needed" CssClass="label"></asp:Label>
                                                <asp:RadioButtonList ID="rdoChem_SampleQty_Needed" runat="server" RepeatDirection="Horizontal" CssClass="pull-right">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblChem_SampleQty_Caption" runat="server" Text="Caption" CssClass="label"></asp:Label>
                                                <asp:TextBox ID="txtChem_SampleQty_Caption" runat="server" MaxLength="15" CssClass="input" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label14" runat="server" CssClass="label" Visible="false">UnListed Doctor Entry Needed </asp:Label>
                                                <asp:RadioButton ID="rdoFFUNLNo" runat="server" CssClass="pull-right" Text="No" Visible="false" GroupName="DCREntryFFUNL" />
                                                <asp:RadioButton ID="rdoFFUNLYes" runat="server" CssClass="pull-right" Text="Yes" Visible="false" GroupName="DCREntryFFUNL" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblInput_Mand" runat="server" Text="Input Selection as Mandatory" CssClass="label"></asp:Label>
                                                <asp:RadioButtonList ID="rdoInput_Mand" runat="server" RepeatDirection="Horizontal" CssClass="pull-right">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblpboLst" runat="server" CssClass="label">Listed Doctorwise POB Updation is Mandatory in DCR</asp:Label>
                                                <asp:RadioButtonList ID="rdopobLstYes_No" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblpboChem" runat="server" CssClass="label">Chemistwise POB Updation is Mandatory in DCR</asp:Label>
                                                <asp:RadioButtonList ID="rdopobChemYes_No" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label34" runat="server" CssClass="label">Is Doctor-wise Remarks Mandatory </asp:Label>
                                                <asp:RadioButton ID="rdoDRNo" runat="server" CssClass="pull-right" Text="No" GroupName="DCREntryDRYes" />
                                                <asp:RadioButton ID="rdoDRYes" runat="server" CssClass="pull-right" Text="Yes" GroupName="DCREntryDRYes" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label35" runat="server" CssClass="label">Allow to Enter New Chemist in DCR Entry </asp:Label>
                                                <asp:RadioButton ID="rdoCheNo" runat="server" CssClass="pull-right" Text="No" GroupName="DCREntryCheYes" />
                                                <asp:RadioButton ID="rdoCheYes" runat="server" CssClass="pull-right" Text="Yes" GroupName="DCREntryCheYes" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label36" runat="server" CssClass="label">Allow to Enter New Un-Listed Dr in DCR Entry </asp:Label>
                                                <asp:RadioButton ID="rdoUnNo" runat="server" CssClass="pull-right" Text="No" GroupName="DCREntryUnYes" />
                                                <asp:RadioButton ID="rdoUnYes" runat="server" CssClass="pull-right" Text="Yes" GroupName="DCREntryUnYes" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label151" runat="server" CssClass="label" Visible="false">Productwise POB Entry Needed </asp:Label>
                                                <asp:RadioButton ID="rdoFFPOBYes" runat="server" CssClass="pull-right" Text="Yes" Visible="false" GroupName="DCREntryFFPOB" />
                                                <asp:RadioButton ID="rdoFFPOBNo" runat="server" CssClass="pull-right" Text="No" Visible="false" GroupName="DCREntryFFPOB" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:RadioButton ID="rdopobprod" runat="server" Text="Productwise  POB" Visible="false" GroupName="grpPOB" />
                                                <asp:RadioButton ID="rdopobdoc" runat="server" Text="Doctorwise  POB" Visible="false" GroupName="grpPOB" />
                                                <asp:RadioButton ID="rdopobdocrx" runat="server" Text="Productwise  Rx" Visible="false" GroupName="grpPOB" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label16" runat="server" CssClass="label">Half day Work Entry Needed </asp:Label>
                                                <asp:RadioButtonList ID="rdoFFDayYes_No" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label22" runat="server" CssClass="label">Product Sample Qty Validation Needed</asp:Label>
                                                <asp:RadioButtonList ID="rdoProductSample" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label30" runat="server" CssClass="label">Input Qty Validation Needed</asp:Label>
                                                <asp:RadioButtonList ID="rdoInputSample" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h5 class="card-title">Doctor Setup</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label19" runat="server" CssClass="label">No.of Listed Doctors Allowed For Entry in Master </asp:Label>
                                                <asp:TextBox ID="txtDRAllowed" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblunlist" runat="server" CssClass="label">No.of Unlisted Doctors Allowed For Entry in Master </asp:Label>
                                                <asp:TextBox ID="txtUnDRAllowed" runat="server" CssClass="input" Width="100%" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label37" runat="server" CssClass="label">Listed Doctor Approval Needed </asp:Label>
                                                <asp:RadioButton ID="rdodocNo" runat="server" CssClass="pull-right" Text="No" GroupName="DocApp" />
                                                <asp:RadioButton ID="rdodocYes" runat="server" CssClass="pull-right" Text="Yes" GroupName="DocApp" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lbldeact" runat="server" CssClass="label">Listed Doctor Deactivation Approval Needed</asp:Label>
                                                <asp:RadioButton ID="rdodeactNo" runat="server" CssClass="pull-right" Text="No" GroupName="DocDeactApp" />
                                                <asp:RadioButton ID="rdodeactYes" runat="server" CssClass="pull-right" Text="Yes" GroupName="DocDeactApp" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lbladddeact" runat="server" CssClass="label">Listed Doctor Add against Deact Approval Needed</asp:Label>
                                                <asp:RadioButton ID="rdoadddeaNo" runat="server" CssClass="pull-right" Text="No" GroupName="DocAddDeact" />
                                                <asp:RadioButton ID="rdoadddeaYes" runat="server" CssClass="pull-right" Text="Yes" GroupName="DocAddDeact" />
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblprdprio" runat="server" CssClass="label">Listed Doctor Product Tag-Prioritywise Needed </asp:Label>
                                                <asp:RadioButtonList ID="rdoprd_priority" runat="server" CssClass="pull-right" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="rdoprd_priority_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label27" runat="server" Visible="false" CssClass="label blink_me">No.of Priorities</asp:Label>
                                                <asp:TextBox ID="txtprdrange" runat="server" Visible="false" CssClass="input" Width="100%" onkeypress="CheckNumeric(event);"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h5 class="card-title">Stockist Setup</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label23" runat="server" CssClass="label">No.of Stockists Allowed For Entry</asp:Label>
                                                <asp:TextBox ID="txtStkAllowed" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row justify-content-center">
                            <div class="col-lg-12">
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">Tour Plan Setup</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="row justify-content-center">
                                            <div class="col-lg-6">
                                                <div class="designation-reactivation-table-area clearfix">
                                                    <div class="single-des clearfix">
                                                        <asp:Label ID="Label40" runat="server" CssClass="label">TP has to be submitted in between days</asp:Label>
                                                    </div>
                                                    <div class="display-table clearfix">
                                                        <div class="table-responsive">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="grdtp_setup" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table"
                                                                        GridLines="None" PagerStyle-CssClass="gridview1" Style="overflow-y: hidden !important;">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdtp_setup.PageIndex * grdtp_setup.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesignation" runat="server" ForeColor="#8B0000"
                                                                                        Text='<% #Eval("Designation_Code")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Design">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesignation_shortname" runat="server" ForeColor="#8B0000"
                                                                                        Text='<% #Eval("Designation_Short_Name")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="center" HeaderText="Tp Start Date">
                                                                                <ItemTemplate>

                                                                                    <asp:DropDownList ID="ddlstart_date" runat="server">
                                                                                        <asp:ListItem Value="-1">---Select---</asp:ListItem>
                                                                                        <asp:ListItem Value="0">0</asp:ListItem>
                                                                                         <asp:ListItem Value ="1">1</asp:ListItem>
                                                    <asp:ListItem Value ="2">2</asp:ListItem>
                                                    <asp:ListItem Value ="3">3</asp:ListItem>
                                                    <asp:ListItem Value ="4">4</asp:ListItem>
                                                    <asp:ListItem Value ="5">5</asp:ListItem>
                                                    <asp:ListItem Value ="6">6</asp:ListItem>
                                                    <asp:ListItem Value ="7">7</asp:ListItem>
                                                    <asp:ListItem Value ="8">8</asp:ListItem>
                                                    <asp:ListItem Value ="9">9</asp:ListItem>
                                                    <asp:ListItem Value ="10">10</asp:ListItem>
                                                    <asp:ListItem Value ="11">11</asp:ListItem>
                                                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                                                        <asp:ListItem Value="16">16</asp:ListItem>
                                                                                        <asp:ListItem Value="17">17</asp:ListItem>
                                                                                        <asp:ListItem Value="18">18</asp:ListItem>
                                                                                        <asp:ListItem Value="19">19</asp:ListItem>
                                                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                                                        <asp:ListItem Value="21">21</asp:ListItem>
                                                                                        <asp:ListItem Value="22">22</asp:ListItem>
                                                                                        <asp:ListItem Value="23">23</asp:ListItem>
                                                                                        <asp:ListItem Value="24">24</asp:ListItem>
                                                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                                                        <asp:ListItem Value="26">26</asp:ListItem>
                                                                                        <asp:ListItem Value="27">27</asp:ListItem>
                                                                                        <asp:ListItem Value="28">28</asp:ListItem>
                                                                                        <asp:ListItem Value="29">29</asp:ListItem>
                                                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                                                        <asp:ListItem Value="31">31</asp:ListItem>

                                                                                    </asp:DropDownList>
                                                                                    <%-- <asp:TextBox ID="txtstart_date" runat="server" Height="25px" onblur="this.style.backgroundColor='White'" CssClass="DOBDate"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onkeypress="Calendar_enterBa(event);"
                                     TabIndex="9" Width="118px"></asp:TextBox>--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Tp End Date">
                                                                                <ItemTemplate>

                                                                                    <asp:DropDownList ID="ddlend_date" runat="server">
                                                                                        <asp:ListItem Value="-1">---Select---</asp:ListItem>
                                                                                        <asp:ListItem Value="0">0</asp:ListItem>
                                                                                          <asp:ListItem Value ="1">1</asp:ListItem>
                                                    <asp:ListItem Value ="2">2</asp:ListItem>
                                                    <asp:ListItem Value ="3">3</asp:ListItem>
                                                    <asp:ListItem Value ="4">4</asp:ListItem>
                                                    <asp:ListItem Value ="5">5</asp:ListItem>
                                                    <asp:ListItem Value ="6">6</asp:ListItem>
                                                    <asp:ListItem Value ="7">7</asp:ListItem>
                                                    <asp:ListItem Value ="8">8</asp:ListItem>
                                                    <asp:ListItem Value ="9">9</asp:ListItem>
                                                    <asp:ListItem Value ="10">10</asp:ListItem>
                                                    <asp:ListItem Value ="11">11</asp:ListItem>
                                                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                                                        <asp:ListItem Value="16">16</asp:ListItem>
                                                                                        <asp:ListItem Value="17">17</asp:ListItem>
                                                                                        <asp:ListItem Value="18">18</asp:ListItem>
                                                                                        <asp:ListItem Value="19">19</asp:ListItem>
                                                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                                                        <asp:ListItem Value="21">21</asp:ListItem>
                                                                                        <asp:ListItem Value="22">22</asp:ListItem>
                                                                                        <asp:ListItem Value="23">23</asp:ListItem>
                                                                                        <asp:ListItem Value="24">24</asp:ListItem>
                                                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                                                        <asp:ListItem Value="26">26</asp:ListItem>
                                                                                        <asp:ListItem Value="27">27</asp:ListItem>
                                                                                        <asp:ListItem Value="28">28</asp:ListItem>
                                                                                        <asp:ListItem Value="29">29</asp:ListItem>
                                                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                                                        <asp:ListItem Value="31">31</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <%--             <asp:TextBox ID="txtend_date" runat="server" Height="25px" onblur="this.style.backgroundColor='White'" CssClass="DOBDate"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onkeypress="Calendar_enterBa(event);"
                                     TabIndex="9" Width="118px"></asp:TextBox>--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="designation-reactivation-table-area clearfix">
                                                    <div class="single-des clearfix">
                                                        <asp:Label ID="Label41" runat="server" CssClass="label">T</asp:Label><asp:LinkButton ID="LinkButton5" class="hover label" runat="server" Text="o" OnClientClick="return OpenWindow_TpEntry()"></asp:LinkButton><asp:Label ID="Label45" runat="server" CssClass="label">ur Plan</asp:Label>
                                                    </div>
                                                    <div class="single-des clearfix">
                                                        <asp:RadioButton ID="rdoDCRTP" runat="server" Text="Tour Plan Based System(Approval Mandatory)" AutoPostBack="true" GroupName="TP" />
                                                    </div>
                                                    <div class="display-table clearfix">
                                                        <div class="table-responsive">
                                                            <%--<asp:UpdatePanel ID="updpnlDesign" runat="server">
                                                                <ContentTemplate>--%>
                                                            <asp:GridView ID="gvDesignation" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                CellPadding="2" CellSpacing="2" GridLines="None" CssClass="table" ShowHeader="false">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="appr" runat="server" Text="Approval Needed for"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDesignation" runat="server" ForeColor="#8B0000"
                                                                                Text='<% #Eval("Designation_Short_Name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkId" Text="  No" runat="server" Width="100%"
                                                                                AutoPostBack="true" OnCheckedChanged="chkId_OnCheckedChanged" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkNo" Text="  Yes" runat="server" Width="100%"
                                                                                AutoPostBack="true" OnCheckedChanged="chkNo_OnCheckedChanged" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <%--</ContentTemplate>
                                                            </asp:UpdatePanel>--%>
                                                        </div>
                                                    </div>
                                                    <div class="single-des clearfix">
                                                        <asp:RadioButton ID="rdoDCRWTP" runat="server" AutoPostBack="true" Text="Without Tour Plan Based System" GroupName="TP" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row justify-content-center">
                            <div class="col-lg-12">
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">Additional Setup (Baselevel & Managers)</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="row justify-content-center">
                                            <div class="col-lg-6">
                                                <div class="designation-reactivation-table-area clearfix">
                                                    <div class="single-des clearfix">
                                                        <asp:Label ID="lbladdi_entry" runat="server" CssClass="label">DCR Setup - Entry Mode</asp:Label>
                                                    </div>
                                                    <div class="display-table clearfix">
                                                        <div class="table-responsive">
                                                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="grdentrymode" runat="server" Width="100%" GridLines="None"
                                                                        CssClass="table" PagerStyle-CssClass="gridview1" AutoGenerateColumns="false" OnRowDataBound="grdentrymode_RowDataBound"
                                                                        EmptyDataText="No Records Found">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Mode" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmode" runat="server" Visible="false" Text='<%# Bind("typ") %>'></asp:Label>
                                                                                    <asp:Label ID="lblmode2" runat="server" Visible="false" Text='<%# Bind("typ") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="List Dr" ItemStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chklisteddr" runat="server" Text="." />
                                                                                    <%-- <asp:Label ID="lbllisteddr" runat="server" Text='<%# Bind("Msl") %>'></asp:Label>--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Chem" ItemStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkchemis" runat="server" Text="." />
                                                                                    <%-- <asp:Label ID="lblchemis" runat="server" Text='<%# Bind("Chm") %>'></asp:Label>--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Stk" ItemStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkstockist" runat="server" Text="." />
                                                                                    <%-- <asp:Label ID="lblstockist" runat="server" Text='<%# Bind("Stk") %>'></asp:Label>--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Unlist Dr" ItemStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkunlisted" runat="server" Text="." />
                                                                                    <%--                                <asp:Label ID="lblunlisted" runat="server" Text='<%# Bind("Unl") %>'></asp:Label>--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Hsptl" ItemStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkhospi" runat="server" Text="." />
                                                                                    <%--        <asp:Label ID="lblhospi" runat="server" Text='<%# Bind("Hos") %>'></asp:Label>--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Design" HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:UpdatePanel ID="updatepanel3" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:TextBox ID="txtDesig" runat="server" Text="ALL" CssClass="input"></asp:TextBox>
                                                                                            <asp:HiddenField ID="hdnDesigId" runat="server"></asp:HiddenField>
                                                                                            <asp:PopupControlExtender ID="txtDesig_PopupControlExtender" runat="server" Enabled="True"
                                                                                                ExtenderControlID="" TargetControlID="txtDesig" PopupControlID="Panel3" OffsetY="22">
                                                                                            </asp:PopupControlExtender>
                                                                                            <asp:Panel ID="Panel3" runat="server" BorderStyle="Solid"
                                                                                                BorderWidth="1px" CssClass="collp" Direction="LeftToRight" ScrollBars="Auto"
                                                                                                Style="display: none">
                                                                                                <div style="height: 15px; position: relative; background-color: #4682B4; text-transform: capitalize; width: 100%; float: left"
                                                                                                    align="right">
                                                                                                </div>
                                                                                                <asp:CheckBoxList ID="ChkDesig" runat="server" BorderStyle="None" CssClass="collp"
                                                                                                    DataTextField="Designation_Short_Name" DataValueField="Designation_Code" AutoPostBack="True"
                                                                                                    OnSelectedIndexChanged="ChkDesig_SelectedIndexChanged" onclick="checkAll(this);">
                                                                                                </asp:CheckBoxList>
                                                                                                <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
                                                        SelectCommand="SELECT [State_Code],[StateName] FROM [Mas_State]"></asp:SqlDataSource>--%>
                                                                                            </asp:Panel>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="designation-reactivation-table-area clearfix">
                                                    <div class="single-des clearfix">
                                                        <asp:Label ID="lbldisplay" runat="server" CssClass="label">DCR Setup - Display Mode</asp:Label>
                                                    </div>
                                                    <div class="display-table clearfix">
                                                        <div class="table-responsive">
                                                            <asp:UpdatePanel ID="updatepnl2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="grddisplaymode" runat="server" Width="100%" GridLines="None"
                                                                        CssClass="table" PagerStyle-CssClass="gridview1" AutoGenerateColumns="false" OnRowDataBound="grddisplaymode_RowDataBound"
                                                                        EmptyDataText="No Records Found">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Mode" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmode_v" runat="server" Visible="false" Text='<%# Bind("typ") %>'></asp:Label>
                                                                                    <asp:Label ID="lblmode_v2" runat="server" Visible="false" Text='<%# Bind("typ") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="List Dr" ItemStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chklisteddr_v" runat="server" Text="." />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Chem" ItemStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkchemis_v" runat="server" Text="." />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Stk" ItemStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkstockist_v" runat="server" Text="." />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Unlist Dr" ItemStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkunlisted_v" runat="server" Text="." />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Hsptl" ItemStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkhospi_v" runat="server" Text="." />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Design" HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:UpdatePanel ID="updatepanel4" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:TextBox ID="txtDesig_v" runat="server" Text="ALL" CssClass="input"></asp:TextBox>
                                                                                            <asp:HiddenField ID="hdnDesigId_v" runat="server"></asp:HiddenField>
                                                                                            <asp:PopupControlExtender ID="txtDesig_v_PopupControlExtender" runat="server" Enabled="True"
                                                                                                ExtenderControlID="" TargetControlID="txtDesig_v" PopupControlID="Panel4" OffsetY="22">
                                                                                            </asp:PopupControlExtender>
                                                                                            <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid"
                                                                                                BorderWidth="1px" CssClass="collp" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                                                                                Style="display: none">
                                                                                                <div style="height: 15px; position: relative; background-color: #4682B4; text-transform: capitalize; width: 100%; float: left"
                                                                                                    align="right">
                                                                                                </div>
                                                                                                <asp:CheckBoxList ID="ChkDesig_v" runat="server" BorderStyle="None" CssClass="collp"
                                                                                                    DataTextField="Designation_Short_Name" DataValueField="Designation_Code" AutoPostBack="True"
                                                                                                    OnSelectedIndexChanged="ChkDesig_v_SelectedIndexChanged" onclick="checkAll(this);">
                                                                                                </asp:CheckBoxList>
                                                                                                <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
                                                        SelectCommand="SELECT [State_Code],[StateName] FROM [Mas_State]"></asp:SqlDataSource>--%>
                                                                                            </asp:Panel>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" OnClientClick="return validate()" Text="Save" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="savebutton" Text="Clear" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
