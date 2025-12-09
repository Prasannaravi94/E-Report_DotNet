<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorCampaign.aspx.cs" Inherits="MasterFiles_DoctorCampaign" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor - Campaign</title>
    <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //     $('input:text:first').focus();
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
                if ($("#txtDoc_SubCat_SName").val() == "") { alert("Enter Short Name."); $('#txtDoc_SubCat_SName').focus(); return false; }
                if ($("#txtDocSubCatName").val() == "") { alert("Enter Campaign Name."); $('#txtDocSubCatName').focus(); return false; }
                if ($("#txtEffFrom").val() == "") { alert("Enter Effective From."); $('#txtEffFrom').focus(); return false; }
                if ($("#txtEffTo").val() == "") { alert("Enter Effective To."); $('#txtEffTo').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />


            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center" id="heading" runat="server"></h2>

                        <div class="designation-area clearfix">

                            <div class="single-des clearfix">
                                <asp:Label ID="lblDoc_SubCat_SName" runat="server" CssClass="label">Short Name<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                <asp:TextBox ID="txtDoc_SubCat_SName" CssClass="input" Width="100%" TabIndex="1"
                                    runat="server" MaxLength="12" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblDocSubCatName" runat="server" CssClass="label">Campaign Name<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                <asp:TextBox ID="txtDocSubCatName" CssClass="input" Width="100%"
                                    TabIndex="2" runat="server"
                                    MaxLength="120" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblEffFrom" runat="server" CssClass="label">Effective From<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                <asp:TextBox ID="txtEffFrom" runat="server" onkeypress="Calendar_enter(event);" CssClass="input" Width="100%"
                                    TabIndex="6"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom" CssClass="cal_Theme1"
                                    runat="server" />
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblEffTo" runat="server" CssClass="label">Effective To<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                <asp:TextBox ID="txtEffTo" runat="server" onkeypress="Calendar_enter(event);"
                                    TabIndex="7" CssClass="input" Width="100%" />
                                <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEffTo" CssClass="cal_Theme1"
                                    runat="server" />
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lbldrtag" runat="server" Text=" No. of Drs Tagged" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtDr_tagg"
                                    TabIndex="5" runat="server" CssClass="input" Width="100%"
                                    MaxLength="3" onkeypress="CheckNumeric(event);">
                                </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblvisit" runat="server" CssClass="label" Text=" No. of Visit"></asp:Label>
                                <asp:TextBox ID="txtvisit" CssClass="input"
                                    TabIndex="6" runat="server" Width="100%"
                                    MaxLength="2" onkeypress="CheckNumeric(event);">
                                </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblinput" runat="server" CssClass="label" Text=" Input"></asp:Label>
                                <asp:UpdatePanel ID="updatepanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtinput" ReadOnly="true" CssClass="input" Width="100%" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="txtinput_PopupControlExtender" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtinput" PopupControlID="Panel1" Position="Bottom"
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel1" runat="server" Height="116px" BorderStyle="Solid" BorderColor="#d1e2ea"
                                            BorderWidth="1px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#f4f8fa"
                                            Style="display: none; scrollbar-width: thin; width: 90%">
                                            <asp:CheckBoxList ID="Chkinput" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Chkinput_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblRs" runat="server" Text=" Expected Business in Rs /-" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtRs" CssClass="input" Width="100%"
                                    TabIndex="6" runat="server"
                                    MaxLength="10" onkeypress="CheckNumeric(event);">
                                </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblstate" runat="server" CssClass="label" Text=" State"></asp:Label>
                                <asp:UpdatePanel ID="updatepanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtstate" ReadOnly="true" CssClass="input" Width="100%" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtstate" PopupControlID="Panel2" Position="bottom"
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel2" runat="server" Height="116px" BorderStyle="Solid" BorderColor="#d1e2ea"
                                            BorderWidth="1px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#f4f8fa"
                                            Style="display: none; scrollbar-width: thin; width: 90%">
                                            <asp:CheckBoxList ID="chkstate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkstate_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>


                            <div class="single-des clearfix">
                                <asp:Label ID="lblsms" runat="server" CssClass="label" Text=" SMS Code"></asp:Label>
                                <asp:TextBox ID="txtsms" CssClass="input"
                                    TabIndex="6" runat="server" Width="100%"
                                    MaxLength="10" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblfor" runat="server" CssClass="label" Text=" For"></asp:Label>
                                <asp:DropDownList ID="ddlfor" runat="server"
                                    CssClass="nice-select" AutoPostBack="true"
                                    TabIndex="7" OnSelectedIndexChanged="ddlfor_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Category" Value="Category"></asp:ListItem>
                                    <asp:ListItem Text="Speciality" Value="Speciality"></asp:ListItem>
                                    <asp:ListItem Text="Class" Value="Class"></asp:ListItem>
                                    <asp:ListItem Text="Brand" Value="Brand"></asp:ListItem>
                                    <asp:ListItem Text="Product" Value="Product"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:CheckBoxList ID="chk_for" runat="server" CssClass="chkboxLocation" CellPadding="5" RepeatColumns="2" RepeatDirection="vertical"></asp:CheckBoxList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:CheckBox ID="chkall_drs" Checked="true" Text="Show All Drs for Campaign Tagging" runat="server" />
                            </div>

                        </div>
                        <p>
                            <br />

                        </p>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Save" OnClick="btnSubmit_Click" />
                        </div>

                    </div>
                </div>


                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
