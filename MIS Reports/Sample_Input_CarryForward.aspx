<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sample_Input_CarryForward.aspx.cs" Inherits="MIS_Reports_Sample_Input_CarryForward" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sample-Input Carryover</title>
    <link type="text/css" rel="Stylesheet" href="../../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <style>
        .NoRecord {
            font-size: 10pt;
            font-weight: bold;
            color: Black;
            background-color: White;
        }

        .break {
            height: 2px;
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
            $('#btnGo').click(function () {

             <%--     var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }--%>
                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
                <%--var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }--%>
                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }



        <%--      -var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                var FMonth = document.getElementById('<%=ddlFMonth.ClientID%>').value;--%>
            <%--    var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                var TMonth = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>

<%--                var frmYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                var frmMonth = $('#ddlFMonth').find(":selected").index();
                var toYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                var toMonth = $('#ddlTMonth').find(":selected").index();--%>


<%--                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var frmMonth = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var frmYear = frmMonYear[1];

                var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var toMonth = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var toYear = ToMonYear[1];--%>

                var mnth = frmMonth, yr = parseInt(frmYear), validate = '', tmp = '';
                if ((frmMonth <= toMonth && parseInt(frmYear) === parseInt(toYear)) || (parseInt(frmYear) < parseInt(toYear))) {
                    //showModalPopUp(frmMonth, frmYear, toMonth, toYear);
                }
                else {
                    alert("Select Valid Month & Year...");
                    $('#ddlFMonth').focus(); return false;
                }

            });
        });
    </script>
        <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Sample-Input Carryover</h2>
                        <%--   <div class="single-des clearfix">
                            <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                            <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </div>--%>
                                                            <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">SAMPLE/INPUT CarryForward</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label18" runat="server" CssClass="label">Sample OB Carry Forward Needed</asp:Label>
                                                <asp:RadioButtonList ID="RadioSample" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label19" runat="server" CssClass="label">Input OB Carry Forward Needed</asp:Label>
                                                <asp:RadioButtonList ID="RadioInput" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>

                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                            <div class="w-100 designation-submit-button text-center clearfix">
                        <br />
                        <asp:Button ID="btnclick" runat="server" CssClass="savebutton" Text="Reset" OnClick="btnclick_Click" />

                        <%--  <asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="View" CssClass="savebutton"
                OnClick="btnGo_Click" />--%>
                    </div>
                        <br />
                        <br />
<%--                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false" CssClass="ddl">
                                </asp:DropDownList>
                            </div>
                        </div>--%>

                         <div class="card border-primary">
                             <div class="card-header">
                                        <h6 class="card-title">SAMPLE/INPUT Reset Opening Balance</h6>
                                    </div>
                             <div class="card-body">
                         <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblmode" runat="server" CssClass="label" Text="Mode"></asp:Label>
                                <asp:DropDownList ID="ddlmode" runat="server" CssClass="nice-select" Width="70%">
                                    <asp:ListItem Value="0" Text="Sample"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Input"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="single-des clearfix">
                            <div style="width: 45%">
                                <asp:Label ID="lblFYear" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                <asp:DropDownList ID="ddlFYear" runat="server" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="single-des clearfix">
                            <div style="width: 45%">
                                <asp:Label ID="lblTYear" runat="server" CssClass="label" Text="To Year"></asp:Label>
                                <asp:DropDownList ID="ddlTYear" runat="server" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                             </div>
                             </div>
                    </div>

                    <div class="w-100 designation-submit-button text-center clearfix">
                        <br />
                        <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Text="Process" OnClick="btnGo_Click" />

                        <%--  <asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="View" CssClass="savebutton"
                OnClick="btnGo_Click" />--%>
                    </div>
                </div>
            </div>

            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
        
         <!-- Bootstrap Datepicker -->
        <script type="text/javascript" src="../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap-datepicker.js"></script>
                <script type="text/javascript">
                    $(function () {
                        $('[id*=txtFromMonthYear]').datepicker({
                            changeMonth: true,
                            changeYear: true,
                            format: "M-yyyy",
                            viewMode: "months",
                            minViewMode: "months",
                            language: "tr"
                        });

                        $('[id*=txtToMonthYear]').datepicker({
                            changeMonth: true,
                            changeYear: true,
                            format: "M-yyyy",
                            viewMode: "months",
                            minViewMode: "months",
                            language: "tr"
                        });
                    });
        </script>
    </form>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>
