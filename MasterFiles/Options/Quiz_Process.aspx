<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quiz_Process.aspx.cs" Inherits="MasterFiles_Options_Quiz_Process" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Online Quiz - Processing Zone</title>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <link href="../../JScript/DateJs/dist/jquery-clockpicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../../JScript/DateJs/assets/css/github.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        //var j = jQuery.noConflict();
        //j(document).ready(function () {
        //    j('#datepicker').datepicker({
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: '2017:' + new Date().getFullYear().toString(),
        //        dateFormat: 'mm/dd/yy'
        //    });

        //    j("#datepicker").datepicker("setDate", new Date());

        //});

    </script>
    <script type="text/javascript">

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('#datepicker').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: '2018:2020',
                dateFormat: 'mm/dd/yy'
            });

            j("#datepicker").datepicker("setDate", new Date());

        });

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('#datepickerFrom').datepicker({
                changeMonth: true,
                changeYear: true,
                //    yearRange: '2017:' + new Date().getFullYear().toString(),
                yearRange: '2018:2020',
                dateFormat: 'mm/dd/yy'
            });

            j("#datepickerFrom").datepicker("setDate", new Date());

        });
        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('#datepickerTo').datepicker({
                changeMonth: true,
                changeYear: true,
                //   yearRange: '2017:' + new Date().getFullYear().toString(),
                yearRange: '2018:2020',
                dateFormat: 'mm/dd/yy'
            });

            j("#datepickerTo").datepicker("setDate", new Date());

        });

    </script>
    <script type="text/javascript">
      

        function showDrop(select) {

            if (select.value == 0) {
                document.getElementById('ddlFrom').style.display = "none";
                document.getElementById('ddlTo').style.display = "none";
                document.getElementById('lbljoin').style.display = "none";
                document.getElementById('lblSt').style.display = "none";
                document.getElementById('ddlst').style.display = "none";
                document.getElementById('lblDesig').style.display = "none";
                document.getElementById('ddlDesig').style.display = "none";
                document.getElementById('lblsub').style.display = "none";
                document.getElementById('ddlsubdiv').style.display = "none";
                document.getElementById('EnableAll').style.display = "none";

                //  document.getElementById('btngo').style.display = "none";
            }
            else if (select.value == 1) {
                document.getElementById('ddlFrom').style.display = "none";
                document.getElementById('ddlTo').style.display = "none";
                document.getElementById('lbljoin').style.display = "block";
                document.getElementById('lblSt').style.display = "block";
                document.getElementById('ddlst').style.display = "none";
                document.getElementById('lblDesig').style.display = "none";
                document.getElementById('ddlDesig').style.display = "none";
                document.getElementById('lblsub').style.display = "none";
                document.getElementById('ddlsubdiv').style.display = "none";
                //  document.getElementById('btngo').style.display = "block";
                document.getElementById('EnableDOJ').style.display = "block";
                document.getElementById('EnableDesig').style.display = "none";
                document.getElementById('EnableSubdiv').style.display = "none";

            }

            else if (select.value == 2) {
                document.getElementById('ddlFrom').style.display = "none";
                document.getElementById('ddlTo').style.display = "none";
                document.getElementById('lbljoin').style.display = "none";
                document.getElementById('lblSt').style.display = "none";
                document.getElementById('ddlst').style.display = "none";
                document.getElementById('lblDesig').style.display = "block";
                document.getElementById('ddlDesig').style.display = "none";
                document.getElementById('lblsub').style.display = "none";
                document.getElementById('ddlsubdiv').style.display = "none";

                document.getElementById('EnableDOJ').style.display = "none";
                document.getElementById('EnableDesig').style.display = "block";
                document.getElementById('EnableSubdiv').style.display = "none";

            }
            else if (select.value == 3) {
                document.getElementById('ddlFrom').style.display = "none";
                document.getElementById('ddlTo').style.display = "none";
                document.getElementById('lbljoin').style.display = "none";
                document.getElementById('lblSt').style.display = "none";
                document.getElementById('ddlst').style.display = "none";
                document.getElementById('lblDesig').style.display = "none";
                document.getElementById('ddlDesig').style.display = "none";
                document.getElementById('lblsub').style.display = "block";
                document.getElementById('ddlsubdiv').style.display = "none";

                document.getElementById('EnableDOJ').style.display = "none";
                document.getElementById('EnableDesig').style.display = "none";
                document.getElementById('EnableSubdiv').style.display = "block";

            }
        }
    </script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script src="../../JScript/jquery.min.js" type="text/javascript"></script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>

    <script src="../../JScript/Service_CRM/Quiz_JS/AddQuiz_ProcessingJS.js" type="text/javascript"></script>
    <link href="../../JScript/Service_CRM/Crm_Dr_Css_Ob/Quiz_ProcessCSS.css" rel="stylesheet" type="text/css" />

    <%--  <script src="../../JScript/Add_QuizProcessing_JS.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
    <script type="text/javascript">

        //         function preventMultipleSubmissions() 
        //         {
        //             $('#btnProcess').prop('disabled', true);
        //         }
        //         window.onbeforeunload = preventMultipleSubmissions;
    </script>
    <style>
        .tableFixHead {
            overflow-y: auto;
            height: 500px;
        }

            .tableFixHead thead th {
                position: sticky;
                top: 0;
            }

        /* Just common table stuff. Really. */
        table {
            border-collapse: collapse;
            width: 100%;
        }

        th, td {
            padding: 8px 16px;
        }

        th {
            background: #eee;
        }

            th:first-child {
                z-index: 1;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />

        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <br />
                    <h2 class="text-center" style="border-bottom: none !important;" id="hHeading" runat="server"></h2>

                    <div class="row justify-content-center">
                        <div class="col-lg-4">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblFilter" runat="server" CssClass="label">Filter By</asp:Label>
                                       <select id="ddlwise" name="form_select" style="FONT-SIZE: xx-small; COLOR: #000000; padding: 1px 3px  0.2em; Height: 24px; BORDER-TOP-STYLE: groove; FONT-FAMILY: Verdana; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove; width: 80px;"
                                        onchange="showDrop(this)">
                                        <option value="0">ALL</option>
                                        <option value="1">DOJ</option>
                                        <option value="2">Desig.</option>
                                        <option value="3">Subdiv.</option>
                                    </select>
                             
                                </div>
                                <div id="EnableAll">
                                    <div id="EnableDOJ">
                                        <div class="single-des clearfix">
                                            <asp:Label ID="lblSt" runat="server" CssClass="label">State</asp:Label>
                                            <asp:DropDownList ID="ddlst" runat="server" CssClass="nice-select">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="single-des clearfix">
                                            <asp:Label ID="lbljoin" runat="server" CssClass="label">DOJ Mnth & Yr</asp:Label>
                                            <div style="float: left; width: 45%">
                                                <asp:DropDownList ID="ddlFrom" runat="server" CssClass="nice-select">
                                                    <asp:ListItem Value="0" Text="ALL"></asp:ListItem>
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
                                            <div style="float: right; width: 45%">
                                                <asp:DropDownList ID="ddlTo" runat="server" CssClass="nice-select">
                                                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="EnableDesig">
                                        <div class="single-des clearfix">
                                            <asp:Label ID="lblDesig" runat="server" CssClass="label">Desig</asp:Label>
                                            <asp:DropDownList ID="ddlDesig" runat="server" CssClass="nice-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="EnableSubdiv">
                                        <div class="single-des clearfix">
                                            <asp:Label ID="lblsub" runat="server" CssClass="label">Subdivision</asp:Label>
                                            <asp:DropDownList ID="ddlsubdiv" runat="server" CssClass="nice-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <br />
                                    <input type="button" id="btngo" value="Go" class="savebutton" style="width: 60px;" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />

                        <div class="row justify-content-center">
                            <div class="col-lg-6">
                                <div class="designation-reactivation-table-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblUser" runat="server" Font-Bold="true" CssClass="label">User List</asp:Label>
                                    </div>
                                    <div class="display-table clearfix">
                                        <div class="table-responsive tableFixHead" style="scrollbar-width: thin;">
                                            <table id="tblUserList" class="table">
                                            </table>
                                            <div id="loading" class="bar">
                                                <p>loading</p>
                                            </div>
                                            <input type="hidden" id="hdnSfCode" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblProcess" runat="server" Font-Bold="true" CssClass="label">Process</asp:Label>
                                    </div>
                                    <div class="single-des clearfix clockpicker">
                                        <span id="lblTime" class="label">Time Limit</span>
                                        <input type="text" class="input" style="width: 100%" id="txtTime" value="00:10" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                                    </div>
                                    <div class="single-des clearfix">
                                        <span id="lblProcessDate" class="label">Date</span>
                                        <asp:TextBox ID="datepicker" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalStartDate" Format="MM/dd/yyyy" TargetControlID="datepicker" CssClass="cal_Theme1"
                                            runat="server" />
                                    </div>
                                    <div class="single-des clearfix">
                                        <span id="lblType" class="label">Type</span>
                                        <select id="ddlType" class="dropDown">
                                            <option>Suffle</option>
                                            <option selected="selected">No Suffle</option>
                                        </select>
                                    </div>
                                    <div class="single-des clearfix">
                                        <span id="lblNoOfAttempt" class="label">No Of Attempts</span>
                                        <select id="ddlNoOfAttempt" class="dropDown">
                                            <option>1</option>
                                            <option selected="selected">2</option>
                                            <%-- <option>3</option>--%>
                                        </select>
                                    </div>
                                    <div class="single-des clearfix">
                                        <span id="Span1" class="label">Process From Date</span>
                                        <asp:TextBox ID="datepickerFrom" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" Format="MM/dd/yyyy" TargetControlID="datepickerFrom" CssClass="cal_Theme1"
                                            runat="server" />
                                    </div>
                                    <div class="single-des clearfix">
                                        <span id="Span2" class="label">Process To Date</span>
                                        <asp:TextBox ID="datepickerTo" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" Format="MM/dd/yyyy" TargetControlID="datepickerTo" CssClass="cal_Theme1"
                                            runat="server" />
                                    </div>
                                    <div class="w-100 designation-submit-button text-center clearfix">
                                        <br />
                                        <input type="submit" id="btnProcess" value="Process" class="savebutton" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
                </div>
                <div class="loading" align="center">
                    Loading. Please wait.<br />
                    <br />
                    <img src="../../Images/loader.gif" alt="" />
                </div>
            </div>
        </div>
        <br />
        <br />
    </form>
    <script src="../../JScript/DateJs/assets/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../JScript/DateJs/dist/jquery-clockpicker.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var k = jQuery.noConflict();
        k('.clockpicker').clockpicker()
	    .find('input').change(function () {
	        console.log(this.value);
	    });
    </script>
</body>
</html>
