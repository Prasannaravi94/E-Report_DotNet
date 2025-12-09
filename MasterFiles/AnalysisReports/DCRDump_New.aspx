<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCRDump_New.aspx.cs" Inherits="MasterFiles_AnalysisReports_DCRDump_New" %>

<!DOCTYPE html>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Call Report Dump</title>
    <link type="text/css" rel="Stylesheet" href="/css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="/css/Grid.css" />

    <style type="text/css">
        .box {
            background: #FFFFFF;
            border: 2px solid #7E8D29;
            border-radius: 8px;
        }
    </style>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <script type="text/javascript" src="/JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="/JsFiles/jquery-1.10.1.js"></script>
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
            $('#btnCSV').click(function () {

                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }


                if ($("[id$=chkdate]").find("input:checked").length > 0) {
                    var cmon = document.getElementById('<%=ddlMonth.ClientID%>').value;
                    var cyear = document.getElementById('<%=ddlYear.ClientID%>').value;
                    var SF_code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                    var cbValue = "";
                    var checked_checkboxes = $("[id*=chkdate] input:checked");

                    checked_checkboxes.each(function () {
                        cbValue += $(this).parent().attr('cbValue') + ",";
                    });
                    document.getElementById('<%=hdnDate.ClientID%>').value = cbValue;
                }
                else {
                    if (document.getElementById("chkAllDate").checked) {
                        alert('Select atleast 1 day.');
                        return false;
                    }
                }
            });
            $('#btnExcel').click(function () {

                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }


                if ($("[id$=chkdate]").find("input:checked").length > 0) {
                    var cmon = document.getElementById('<%=ddlMonth.ClientID%>').value;
                    var cyear = document.getElementById('<%=ddlYear.ClientID%>').value;
                    var SF_code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                    var cbValue = "";
                    var checked_checkboxes = $("[id*=chkdate] input:checked");

                    checked_checkboxes.each(function () {
                        cbValue += $(this).parent().attr('cbValue') + ",";
                    });
                    document.getElementById('<%=hdnDate.ClientID%>').value = cbValue;
                }
                else {
                    if (document.getElementById("chkAllDate").checked) {
                        alert('Select atleast 1 day.');
                        return false;
                    }
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });
    </script>


    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="/assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Call Report Dump</h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>

                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select"
                                    Visible="false">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                                </asp:DropDownList>

                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Month"></asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Year"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">

                                <asp:CheckBox ID="chkAllDate" Text="DateWise" runat="server" Width="100%" AutoPostBack="true" OnCheckedChanged="chkAllDate_ChckedChanged"></asp:CheckBox>
                            </div>
                            <asp:Panel ID="pnlDateWise" runat="server" Visible="false">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lbldate" runat="server" CssClass="label" Text="Day">
                                    </asp:Label>
                                    <asp:CheckBoxList ID="chkdate" RepeatColumns="7" RepeatDirection="Horizontal" runat="server" Width="100%">
                                    </asp:CheckBoxList>
                                    <asp:HiddenField ID="hdnDate" runat="server" />
                                </div>
                            </asp:Panel>
                            <div class="single-des clearfix">

                                <asp:CheckBox ID="chkWOVacant" Text="Vacant" runat="server" Width="100%"></asp:CheckBox>
                            </div>

                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:LinkButton ID="btnCSV" runat="server" Font-Size="Medium" Font-Bold="true" Text="Download CSV" OnClick="btnCSV_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btnExcel" runat="server" Font-Size="Medium" Font-Bold="true" Text="Download Excel" OnClick="btnExcel_Click" />
                        </div>
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
        <script src="//cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
