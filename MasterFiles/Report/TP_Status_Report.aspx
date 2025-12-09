<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_Status_Report.aspx.cs"
    Inherits="Reports_TP_Status_Report" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tour Plan Status Report</title>
    <%-- <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, Option, ddlState, strValue, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_TP_Status.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&Option=" + Option + "&state_code=" + ddlState + "&strValue=" + strValue + "&sf_name=" + sf_name,
    "_blank"//,
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=800," +
    //"height=600," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            // LoadModalDiv();
        }
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
            $('#btnSubmit').click(function () {

                var sf_name = $('#<%=ddlMR.ClientID%> :selected').text();
                var ddlMRName = $('#<%=ddlState.ClientID%> :selected').text();
                if (sf_name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }

                <%--var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }--%>

                if (ddlMRName != '') {
                    var ddlState = document.getElementById('<%=ddlState.ClientID%>').value;
                }

                <%--var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;

                var ddlYear = document.getElementById('<%=ddlYear.ClientID%>').value;--%>

                var ToMonYear = document.getElementById('<%=txtMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = ToMonYear[1];

                var radio = document.getElementsByName('rdoMGRState');
                for (var i = 0; i < radio.length; i++) {
                    if (radio[i].checked) {
                        var Option = radio[i].value;
                    }
                }

                var chkDetail = document.getElementById("chkVacant");

                var strValue = 0;
                if (chkDetail.checked) {
                    var strValue = 1;
                }

                if (sf_name != '') {
                    var sf_code = document.getElementById('<%=ddlMR.ClientID%>').value;
                }

                if (ddlMRName != '') {

                    showModalPopUp(0, Month1, Year1, Option, ddlState, strValue, sf_name);
                }
                else {

                    showModalPopUp(sf_code, Month1, Year1, Option, 0, strValue, sf_name);
                }

            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtBase]');
            var $ddl = $('select[id$=ddlMR]');
            var $items = $('select[id$=ddlMR] option');

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
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtstate]');
            var $ddl = $('select[id$=ddlState]');
            var $items = $('select[id$=ddlState] option');

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

    <style type="text/css">
        .single-des [type="checkbox"]:not(:checked) + label, .single-des [type="checkbox"]:checked + label {
            padding-left: 2.15em;
        }
    </style>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">TP Status</h2>
                        <div class="designation-area clearfix">

                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" Text="View By" CssClass="label"></asp:Label>
                                <asp:RadioButtonList ID="rdoMGRState" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rdoMGRState_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="FieldForce-wise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="State-wise"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblFieldforce" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>

                                <asp:TextBox ID="txtBase" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                    ToolTip="Enter Text Here"></asp:TextBox>
                                <asp:DropDownList ID="ddlMR" runat="server" Width="100%" AutoPostBack="false" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select"></asp:DropDownList>


                                <asp:Label ID="lblState" runat="server" CssClass="label" Text="State"></asp:Label>
                                <asp:TextBox ID="txtstate" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                    ToolTip="Enter Text Here"></asp:TextBox>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="Label1" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                <%--                 <asp:Label ID="lblMonth" runat="server" CssClass="label" Text="Month"></asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select">
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
                                </asp:DropDownList>--%>
                            </div>
                            <%--           <div class="single-des clearfix">
                                <asp:Label ID="lblYear" runat="server" CssClass="label" Text="Year"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>--%>
                            <div class="single-des clearfix">
                                <asp:CheckBox ID="chkVacant" runat="server" Text=" With Vacants" />
                            </div>

                        </div>

                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />
                        </div>

                        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                            Width="80%">
                        </asp:Table>

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
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

         <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=txtMonthYear]').datepicker({
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
</body>
</html>
