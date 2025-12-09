<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuizTitleCreation.aspx.cs"
    Inherits="MasterFiles_Options_QuizTitleCreation" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Online Quiz - Title Creation</title>
    <%--<style type="text/css">
        .Textbox
        {
            width: 120px;
            height: 20px;
        }
        
        #txttitle
        {
            width: 250px;
            height: 28px;
        }
        
        .label
        {
            width: 150px;
            height: 20px;
        }
        
        .dropDown
        {
            height: 24px;
            width: 80px;
        }
        
        #ddlCategory
        {
            height: 24px;
            width: 180px;
        }
        
        #tblTitleCreate td
        {
            padding-top: 5px;
            padding-bottom: 5px;
        }
        
        .file
        {
            color: Blue;
            font-family: Verdana;
            font-weight: 600;
        }
    </style>--%>
    <%--<link href="../../css/style.css" rel="stylesheet" type="text/css" />--%>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <%--<link href="../../JScript/DateJs/dist/jquery-clockpicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../JScript/DateJs/assets/css/github.min.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">

        var j = jQuery.noConflict();
        j(document).ready(function () {
            //j('#txtEffFrom').datepicker({
            //    changeMonth: true,
            //    changeYear: true,
            //    //yearRange: "2010:2017",
            //    yearRange: '2017:' + new Date().getFullYear().toString(),
            //    dateFormat: 'mm/dd/yy'
            //});

            //j("#txtEffFrom").datepicker("setDate", new Date());

            var currentMonth = (new Date).getMonth() + 1;

            $("#ddlMonth option[value='" + currentMonth + "']").prop('selected', true);

            // var currentYear = (new Date).getFullYear();

            //            $("#ddlMonth option[value='" + currentMonth + "']").prop('selected', true);
            // $("#ddlYear option[value='" + currentYear + "']").prop('selected', true);

            //var noofyears = 2; // Change to whatever you want
            //var thisYear = (new Date()).getFullYear();
            //for (var i = 0; i <= noofyears; i++)
            //{
            //    var year = thisYear - i;
            //    $('<option>', { value: year, text: year }).appendTo("#ddlYear");
            //}

            var currentYear = (new Date).getFullYear();
            var PreviousYear = currentYear - 1;
            $("#ddlYear").append($("<option></option>").val(PreviousYear).html(PreviousYear));
            $("#ddlYear").append($("<option></option>").val(currentYear).html(currentYear));
            $("#ddlYear option[value='" + currentYear + "']").prop('selected', true);

            $("#hidYear").val($("#ddlYear").val());

            $("#hidYear").val();

            $("#txttitle").val("");

            $("#ddlYear").change(function () {
                $("#hidYear").val($("#ddlYear").val());
                console.log($("#ddlYear").val());
            });

        });

    </script>

    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript">

        function Validation() {

            //alert($("#ddlCategory").val());

            if ($("#txttitle").val() == "") {

                createCustomAlert("Please Enter Quiz Title");
                return false;
            }
            if ($("#ddlCategory").val() == 0) {
                createCustomAlert("Please Select Category");
                return false;
            }
            if ($("#txtEffFrom").val() == "") {
                createCustomAlert("Please Enter Effective Date");
                return false;
            }
            if ($("#ddlMonth").val() == "") {
                createCustomAlert("Please Select Month");
                return false;
            }
            if ($("#ddlYear").val() == "") {
                createCustomAlert("Please Select Year");
                return false;
            }
            else {
                return true;
            }

        }

    </script>
    <script type="text/javascript">

        function preventMultipleSubmissions() {
            $('#btnSurvey').prop('disabled', true);
        }
        window.onbeforeunload = preventMultipleSubmissions;
    </script>
    <link href="../../JScript/Process_CSS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <div>
            <asp:HiddenField ID="hidSurveyID" runat="server" />
            <asp:HiddenField ID="hidYear" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" style="border-bottom:none !important;" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lbltitle" runat="server" CssClass="label">Quiz Title</asp:Label>
                                <asp:TextBox ID="txttitle" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblcategory" runat="server" CssClass="label">Quiz Category</asp:Label>
                                <asp:DropDownList ID="ddlCategory" CssClass="dropDown" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Effective Date</asp:Label>
                                <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input" Width="100%" TabIndex="6"></asp:TextBox>
                                <asp:CalendarExtender ID="CalStartDate" Format="MM/dd/yyyy" TargetControlID="txtEffFrom" CssClass="cal_Theme1"
                                            runat="server" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMonth" runat="server" CssClass="label">Month</asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropDown">
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
                            <div class="single-des clearfix">
                                <asp:Label ID="lblYear" runat="server" CssClass="label">Year</asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropDown">
                                    <%--  <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                                <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                                <asp:ListItem Value="2017" Text="2017"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:FileUpload ID="fileUpload1" runat="server" CssClass="input" Width="100%" />
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnSurvey" runat="server" Text="Save" OnClick="btnadd_Click" CssClass="savebutton"
                                    OnClientClick="if (!Validation()) return false;" />
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
