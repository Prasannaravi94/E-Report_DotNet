<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quiz_Questions.aspx.cs" Inherits="MasterFiles_Options_Quiz_Questions" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Question Entry</title>
    <script language="javascript" type="text/javascript">
        function QuestionTypeChange(dropdown) {
            var myindex = dropdown.selectedIndex
            var SelValue = dropdown.options[myindex].value
            if (SelValue == 3) {
                document.getElementById("trAnswer").style.display = 'none';
                document.getElementById("trCorrectAns").style.display = 'none';
                document.getElementById("trNoOfOption").style.display = 'none';
            }
            else {
                document.getElementById("trAnswer").style.display = '';
                document.getElementById("trCorrectAns").style.display = '';
                document.getElementById("trNoOfOption").style.display = '';
            }
        }
    </script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
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
    </style>
    <%--<style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
            font-size: 9pt;
        }

        .label {
            display: inline-block;
            font-size: 9pt;
            color: black;
            font-family: Verdana;
        }

        .dropDown {
            height: 27px;
            width: 182px;
            font-size: 8pt;
            color: #000000;
            padding: 1px 3px 0.2em;
            height: 25px;
            border-top-style: groove;
            font-family: Verdana;
            border-right-style: groove;
            border-left-style: groove;
            border-bottom-style: groove;
        }

        .Textbox {
            font-size: 8pt;
            color: black;
            border-top-style: groove;
            border-right-style: groove;
            border-left-style: groove;
            height: 22px;
            padding-left: 4px;
            background-color: white;
            border-bottom-style: groove;
        }
    </style>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />

    <link href="../../JScript/css/RadioBtnCSS.css" rel="stylesheet" type="text/css" />--%>
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
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../../JScript/Service_CRM/Quiz_JS/Quiz_QusCreate_JS.js" type="text/javascript"></script>
    <script type="text/javascript">

        function preventMultipleSubmissions() {

            $('#<%=btnAddQuestion.ClientID %>').prop('disabled', true);

        }

        window.onbeforeunload = preventMultipleSubmissions;

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <div>
            <asp:HiddenField ID="hidSurveyId" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" style="border-bottom: none !important;" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblQuestion_Type" runat="server" CssClass="label">Question Type</asp:Label>
                                <asp:DropDownList ID="ddlQuestionType" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblQuestionText" runat="server" CssClass="label">Question Text</asp:Label>
                                <asp:TextBox ID="txtQuestionText" runat="server" CssClass="input" Width="100%" Style="height: 100px !important;" TextMode="MultiLine">                                        
                                </asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblNoofOption" runat="server" CssClass="label">No Of Option</asp:Label>
                                <asp:TextBox ID="txtNoOfOption" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblAnswer" runat="server" CssClass="label">Answer Choices</asp:Label>
                                <asp:TextBox ID="txtInputOptions" runat="server" CssClass="input" Width="100%" Style="height: 100px !important;" TextMode="MultiLine" Visible="false"></asp:TextBox>
                            </div>
                            <div class="OptionDiv">

                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblCrctAns" runat="server" CssClass="label">Correct Answer</asp:Label>
                                <asp:TextBox ID="txtAns" runat="server" CssClass="input" ReadOnly="true" Width="100%"></asp:TextBox>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnAddQuestion" runat="server" Text="Add Question" CssClass="savebutton" />
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
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
