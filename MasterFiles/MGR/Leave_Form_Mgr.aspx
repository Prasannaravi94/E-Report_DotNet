<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Leave_Form_Mgr.aspx.cs" Inherits="MasterFiles_MGR_Leave_Form_Mgr" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Leave Application Form</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/png" href="../../images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="../../assets/css/nice-select.css">
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/css/style.css">
    <link rel="stylesheet" href="../../assets/css/responsive.css">
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" type="text/css" />
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <style type="text/css">
        .padding {
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 3px;
            padding-bottom: 3px;
        }

        .chkboxLocation label {
            padding-right: 5px;
        }

        .Cal_Theme .ajax__calendar_container {
            background-color: #EDCF81;
            border: solid 1px #cccccc;
        }

        .Cal_Theme .ajax__calendar_header {
            background-color: #FFFFEA;
            margin-bottom: 4px;
        }

        .Cal_Theme .ajax__calendar_title,
        .Cal_Theme .ajax__calendar_next,
        .Cal_Theme .ajax__calendar_prev {
            color: #004080;
            padding-top: 3px;
        }

        .Cal_Theme .ajax__calendar_body {
            background-color: #FFFFEA;
            border: solid 1px #cccccc;
        }

        .Cal_Theme .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
        }

        .Cal_Theme .ajax__calendar_day {
            text-align: center;
        }

        .Cal_Theme .ajax__calendar_hover .ajax__calendar_day,
        .Cal_Theme .ajax__calendar_hover .ajax__calendar_month,
        .Cal_Theme .ajax__calendar_hover .ajax__calendar_year,
        .Cal_Theme .ajax__calendar_active {
            color: #FFFFFF;
            font-weight: bold;
            background-color: #4A89B9;
        }

        .Cal_Theme .ajax__calendar_today {
            font-weight: bold;
        }

        .Cal_Theme .ajax__calendar_other,
        .Cal_Theme .ajax__calendar_hover .ajax__calendar_today,
        .Cal_Theme .ajax__calendar_hover .ajax__calendar_title {
            color: #000000;
        }

        #btnBack {
            margin-left: 0px;
        }

        .nice-select {
            width: 150px !important;
        }

        .leaveformtable {
            width: 100% !important;
        }

            .leaveformtable th {
                background-color: #25b2e2 !important;
            }

        .leaveformselect {
            width: 20%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    
        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                //  $('input:text:first').focus();
                $('input:text').bind("keydown", function (e) {
                    var n = $("input:text").length;
                    if (e.which == 13) { //Enter key
                        e.preventDefault(); //to skip default behavior of the enter key
                        var curIndex = $('input:text').index(this);

                        if ($('input:text')[curIndex].value == '') {
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
                                $('#btnApprove').focus();
                            }
                        }
                    }
                });
                $("input:text").on("keypress", function (e) {
                    if (e.which === 32 && !this.value.length)
                        e.preventDefault();
                });
                $('#btnApprove').click(function () {
                    var divi = $('#<%=ddltype.ClientID%> :selected').text();
                        if (divi == "--Select--") { alert("Select Type of Leave."); $('#ddltype').focus(); return false; }
                        if ($("#txtLeave").val() == "") { alert("Enter Leave From Date."); $('#txtLeave').focus(); return false; }
                        if ($("#txtLeaveto").val() == "") { alert("Enter Leave To Date."); $('#txtLeaveto').focus(); return false; }
                        if ($("#txtreason").val() == "") { alert("Enter Reason for Leave."); $('#txtreason').focus(); return false; }
                        if (confirm('Do you want to Submit?')) {
                            return true;
                        }
                        else {
                            return false;
                        }

                    });
                });
        </script>
        <script src="../../JScript/js/jquery-1.9.1.min.js" type="text/javascript"></script>
        <script src="../../JScript/js/bootstrap-datepicker.js" type="text/javascript"></script>
        <script type="text/javascript">

            var j = jQuery.noConflict();
            j(document).ready(function () {



                j('#date1').datepicker({

                    format: 'dd/mm/yyyy'

                });

                j('.date').datepicker({

                    startView: 1,
                    format: 'dd/mm/yyyy'
                });




            });
        </script>
        <script type="text/javascript">

            function preventMultipleSubmissions() {
                $('#<%=btnApprove.ClientID %>').prop('disabled', true);
                }

                window.onbeforeunload = preventMultipleSubmissions;

        </script>
        <div>
            <ucl:Menu ID="menu1" runat="server" />
                
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <br />
                        <h2 class="text-center" style="border-bottom: none !important;" id="hHeading" runat="server"></h2>

                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <center>
                                        <asp:Panel ID="pnltit" runat="server" Visible="false">
                                            <h2 class="text-center" style="border-bottom: none">Leave Application Form</h2>
                                            <%--<asp:Label ID="lbltit" runat="server" Text="Leave Application Form" Style="text-transform: capitalize; font-size: 15px; text-align: center;"
                                                ForeColor="BlueViolet" Font-Bold="True"
                                                Font-Names="Verdana" CssClass="under"></asp:Label>--%>
                                        </asp:Panel>
                                    </center>
                                    <center>
                                        <asp:Panel ID="pnlHead" runat="server" Visible="false">
                                            <h2 class="text-center" style="border-bottom: none">Leave Approval</h2>
                                            <%--<asp:Label ID="lblhead" runat="server" Font-Underline="true" Text="Leave Approval" Font-Bold="true" ForeColor="Green" Font-Size="Medium" Font-Names="Verdana"></asp:Label>--%>
                                        </asp:Panel>
                                        <br />
                                    </center>
                                    <center>
                                        <asp:Table ID="tblLeave" CssClass="leaveformtable"
                                            CellSpacing="5" CellPadding="5" runat="server">

                                            <asp:TableHeaderRow HorizontalAlign="Center" Font-Size="14px" Font-Names="Verdana">

                                                <asp:TableHeaderCell ColumnSpan="2">
                                                    Leave Form
                        <br />
                                                    <asp:Panel ID="Pnlent" runat="server" Visible="false">
                                                        <table style="border: 0px;" width="100%">
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblEle" runat="server" Text="Eligiblity:-" Font-Size="11.5px" Font-Bold="true" ForeColor="Green"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblCL" runat="server" Text="CL:" SkinID="lblMand"></asp:Label>
                                                                    <asp:Label ID="lblCLL" runat="server" SkinID="lblMand"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblPL" runat="server" Text="PL:" SkinID="lblMand"></asp:Label>
                                                                    <asp:Label ID="lblPLL" runat="server" SkinID="lblMand"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblSL" runat="server" Text="SL:" SkinID="lblMand"></asp:Label>
                                                                    <asp:Label ID="lblSLL" runat="server" SkinID="lblMand"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblLL" runat="server" Text="LOP:" SkinID="lblMand"></asp:Label>
                                                                    <asp:Label ID="lblLLL" runat="server" SkinID="lblMand"></asp:Label>

                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="Label11" runat="server" Text="Balance:-" Font-Bold="true" Font-Size="11.5px" ForeColor="Red"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label12" runat="server" SkinID="lblMand" Text="CL:"></asp:Label>
                                                                    <asp:Label ID="lblBCL" runat="server" SkinID="lblMand"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblB" runat="server" Text="PL:" SkinID="lblMand"></asp:Label>
                                                                    <asp:Label ID="lblBPL" runat="server" SkinID="lblMand"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label16" runat="server" Text="SL:" SkinID="lblMand"></asp:Label>
                                                                    <asp:Label ID="lblBSL" runat="server" SkinID="lblMand"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label15" runat="server" Text="LOP:" SkinID="lblMand"></asp:Label>
                                                                    <asp:Label ID="lblBLL" runat="server" SkinID="lblMand"></asp:Label>

                                                                </td>
                                                                <td style="text-align: right">

                                                                    <asp:Label ID="Label13" runat="server" Text="Approval Pending:-" Font-Bold="true" Font-Size="11.5px" ForeColor="Red"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label14" runat="server" SkinID="lblMand" Text="CL:"></asp:Label>
                                                                    <asp:Label ID="lblACL" runat="server" SkinID="lblMand"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label17" runat="server" Text="PL:" SkinID="lblMand"></asp:Label>
                                                                    <asp:Label ID="lblAPL" runat="server" SkinID="lblMand"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label19" runat="server" Text="SL:" SkinID="lblMand"></asp:Label>
                                                                    <asp:Label ID="lblASL" runat="server" SkinID="lblMand"></asp:Label>

                                                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label18" runat="server" Text="LOP:" SkinID="lblMand"></asp:Label>
                                                                    <asp:Label ID="lblALL" runat="server" SkinID="lblMand"></asp:Label>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </asp:TableHeaderCell>
                                            </asp:TableHeaderRow>

                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableCell CssClass="padding" VerticalAlign="Middle">
                                                    <asp:Label ID="lblName" Width="141px" runat="server" Text=" Name Of the Employee  "
                                                        CssClass="label"></asp:Label>
                                                    <asp:Label ID="lblcol" runat="server">:</asp:Label>
                                                    <asp:Label ID="lblemp" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="padding" VerticalAlign="Middle">
                                                    <asp:Label ID="lblHq" runat="server" Text="HQ " CssClass="label"></asp:Label>
                                                    <asp:Label ID="Label8" runat="server">: </asp:Label>
                                                    <asp:Label ID="lblSfhq" runat="server" CssClass="Sfhqleftalign"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableCell CssClass="padding">
                                                    <asp:Label ID="lblCode" runat="server" Text="Emp Code " CssClass="label"></asp:Label>
                                                    <asp:Label ID="Label1" runat="server">: </asp:Label>
                                                    <asp:Label ID="lblempcode" runat="server" CssClass="empcodeleftalign"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="padding">
                                                    <asp:Label ID="lblDesign" runat="server" Text="Designation " CssClass="label"></asp:Label>
                                                    <asp:Label ID="Label7" runat="server">: </asp:Label>
                                                    <asp:Label ID="lbldesig" runat="server" SkinID="lblMand"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow HorizontalAlign="Left" CssClass="leaveformtabletrsplit">
                                                <asp:TableCell CssClass="padding">
                                                    <asp:Label ID="lblDivision" CssClass="label" runat="server" Text="Division Name "></asp:Label>
                                                    <asp:Label ID="Label2" runat="server">: </asp:Label>
                                                    <asp:Label ID="lbldivi" runat="server" CssClass="divileftalign"></asp:Label>
                                                    <br />
                                                    <br />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow HorizontalAlign="Left" CssClass="leaveformtabletrsplit">
                                                <asp:TableCell CssClass="padding">
												<table>
												<tr>
												<td>    <asp:Label ID="lbltype" runat="server" Text="Type of Leave " CssClass="label"></asp:Label>
                                                    <asp:Label ID="Label5" runat="server">: </asp:Label></td>
												<td>
												 <asp:DropDownList ID="ddltype" runat="server" CssClass="txtLeaveFrom">
                                                    </asp:DropDownList></td>
												</tr>
                                                </table>
                                                    <br />
                                                    <asp:Label ID="lblLeave" runat="server" CssClass="label" Text="Leave From Date "></asp:Label>
                                                    <asp:Label ID="Label3" runat="server">: </asp:Label>
                                                    <asp:TextBox ID="txtLeave" runat="server" Width="150px" onkeypress="Calendar_enter(event);"
                                                        CssClass="leaveformselect txtLeaveFrom" AutoPostBack="true" OnTextChanged="txtLeave_TextChanged"></asp:TextBox>
                                                    <%--<asp:ImageButton ID="imgPopup" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom"
                                                        runat="server" />--%>
                                                    <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtLeave"
                                                        CssClass="cal_Theme1" runat="server" />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblLeaveto" runat="server" CssClass="label" Text="Leave To Date "></asp:Label>
                                                    <asp:Label ID="Label4" runat="server">: </asp:Label>
                                                    <asp:TextBox ID="txtLeaveto" runat="server" onkeypress="Calendar_enter(event);" CssClass="leaveformselect"
                                                        AutoPostBack="true" Width="150px" OnTextChanged="txtLeaveto_TextChanged"></asp:TextBox>
                                                    <%--<asp:ImageButton ID="imgPop" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom"
                                                        runat="server" />--%>
                                                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtLeaveto"
                                                        CssClass="cal_Theme1" runat="server" />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblDays" runat="server" CssClass="label" Text="Number Of Days "></asp:Label>
                                                    <asp:Label ID="Label6" runat="server">: </asp:Label>
                                                    <asp:Label ID="lblDaysCount" runat="server" Font-Bold="true"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="padding">
                                                    <asp:Label ID="lblreason" runat="server" CssClass="label" Text="Reason For Leave "></asp:Label>
                                                    <asp:Label ID="Label9" runat="server">: </asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine" onkeypress="AlphaNumeric_NoSpecialChars_New(event);"
                                                        CssClass="leaveformtextarea"></asp:TextBox>
                                                    <br />
                                                    <asp:Label ID="lblAddr" runat="server" Text="Address On Leave " CssClass="label"></asp:Label>
                                                    <asp:Label ID="Label10" runat="server">: </asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtAddr" runat="server" TextMode="MultiLine" onkeypress="AlphaNumeric_NoSpecialChars_New(event);"
                                                        CssClass="leaveformtextarea"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow CssClass="leaveformtabletrsplit">
                                                <asp:TableCell CssClass="padding" HorizontalAlign="Left">
                                                    <asp:Label ID="lblInform" runat="server" CssClass="label" Text="Informed Manager : "></asp:Label>
                                                    <asp:CheckBoxList ID="chkmanager" BorderStyle="None"
                                                        CssClass="single-des clearfix" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1"> Phone</asp:ListItem>
                                                        <asp:ListItem Value="2"> Whatsapp</asp:ListItem>
                                                        <asp:ListItem Value="3"> SMS</asp:ListItem>
                                                        <asp:ListItem Value="4"> E-Mail</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="padding" HorizontalAlign="Left">
                                                    <asp:Label ID="lblho" runat="server" CssClass="label" Text="Informed HO : "></asp:Label>
                                                    <asp:CheckBoxList ID="chkho" BorderStyle="None"
                                                        CssClass="single-des clearfix" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="4"> E-Mail</asp:ListItem>
                                                        <%--<asp:ListItem Value="1"> Phone</asp:ListItem>--%>
                                                       <%-- <asp:ListItem Value="2"> Whatsapp</asp:ListItem>--%>
                                                       <%-- <asp:ListItem Value="3"> SMS</asp:ListItem>--%>
                                                    </asp:CheckBoxList>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                           <%-- <asp:TableRow CssClass="leaveformtabletrsplit">
                                                <asp:TableCell CssClass="padding" HorizontalAlign="Left" VerticalAlign="Middle" ColumnSpan="2">
                                                    <span style="vertical-align: top">
                                                        <asp:Label ID="lblValid" runat="server" CssClass="label" Text="If no Phone / E-Mail /SMS ,Valid Reason : "></asp:Label></span>
                                                    <asp:TextBox ID="lblValidreason" CssClass="leaveforminput" runat="server" TextMode="MultiLine" SkinID="lblMand"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>--%>
                                        </asp:Table>
                                    </center>
                                    <br />
                                    <center>
                                        <asp:Panel ID="pnlmr" runat="server">
                                            <asp:Button ID="btnApprove" runat="server" Text="Submit for Approval" Width="200px" CssClass="savebutton"
                                                OnClick="btnApprove_Click" />
                                        </asp:Panel>
                                    </center>
                                </div>
                            </div>

                            <div class="loading" align="center">
                                Loading. Please wait.<br />
                                <br />
                                <img src="../../Images/loader.gif" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
