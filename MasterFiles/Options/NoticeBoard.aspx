<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoticeBoard.aspx.cs" Inherits="MasterFiles_Options_NoticeBoard" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notice Board</title>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
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


                if ($("#txtNotice1").val() == "") { alert("Enter Content1."); $('#txtNotice1').focus(); return false; }
                if ($("#txtStartDate").val() == "") { alert("Enter Start Date."); $('#txtStartDate').focus(); return false; }
                if ($("#txtEndDate").val() == "") { alert("Enter End Date."); $('#txtEndDate').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblNotice1" runat="server" CssClass="label">Content1</asp:Label>
                                        <asp:TextBox ID="txtNotice1" runat="server" CssClass="input" TextMode="MultiLine" Width="100%" Style="height: 100px !important;"></asp:TextBox>

                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblNotice2" runat="server" CssClass="label">Content2</asp:Label>
                                        <asp:TextBox ID="txtNotice2" runat="server" CssClass="input" TextMode="MultiLine" Width="100%" Style="height: 100px !important;"></asp:TextBox>

                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblNotice3" runat="server" CssClass="label">Content3</asp:Label>
                                        <asp:TextBox ID="txtNotice3" runat="server" CssClass="input" TextMode="MultiLine" Width="100%" Style="height: 100px !important;"></asp:TextBox>

                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Width="100%">Start Date</asp:Label>
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="input" Width="100%" onkeypress="Calendar_enter(event);"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalStartDate" Format="dd/MM/yyyy" TargetControlID="txtStartDate" CssClass="cal_Theme1"
                                            runat="server" />
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Width="100%">End Date</asp:Label>
                                        <asp:TextBox ID="txtEndDate" CssClass="input" onkeypress="Calendar_enter(event);" Width="100%" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalEndDate" Format="dd/MM/yyyy" TargetControlID="txtEndDate" CssClass="cal_Theme1"
                                            runat="server" />
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:CheckBox ID="chkback" runat="server" Text="Set as Home Page" Font-Names="Verdana" Font-Size="12px" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" Font-Size="Medium" OnClick="lnkEdit_Click"></asp:LinkButton>
                                        <asp:DropDownList ID="ddlEdit" runat="server"
                                            Visible="false" OnSelectedIndexChanged="ddlEdit_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" OnClick="btnGo_Click" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="w-100 designation-submit-button text-center clearfix">
                                        <br />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="savebutton" OnClick="btnSubmit_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="savebutton" OnClick="btnClear_Click" />
                                    </div>
                                </div>
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
