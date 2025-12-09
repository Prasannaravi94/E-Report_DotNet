<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_View.aspx.cs" Inherits="Reports_DCR_View" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR View</title>
    <style type="text/css">
        #tblDocRpt {
            margin-left: 300px;
        }
    </style>
    <%--    <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, Mode, sf_name) {

            if (Mode.trim() == "View All Remark(s)") {

                //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                popUpObj = window.open("rptRemarks.aspx?sf_code=" + sfcode + "&Month=" + fmon + "&Year=" + fyr + "&Mode=" + Mode + "&sf_name=" + sf_name,
    "_blank"//,
    //"null," +
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
              else if (Mode.trim() == "View All DCR Date(s)" || Mode.trim() == "Detailed View") {
            
                  popUpObj = window.open("Rpt_DCR_View_Tuned.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&Mode=" + Mode + "&sf_name=" + sf_name,
                  "_blank"//,
                  //"null," +
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
            else {

                popUpObj = window.open("Rpt_DCR_View.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&Mode=" + Mode + "&sf_name=" + sf_name,
    "_blank"//,
    //"null," +
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
        }

    </script>

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>

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

                var ddlMRName = $('#<%=ddlMR.ClientID%> :selected').text();
                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select Clear---") { alert("Select FieldForce Name."); $('#ddlFieldForce').focus(); return false; }
                <%--var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }--%>
                if (ddlMRName != '') {

                    var ddlMR = document.getElementById('<%=ddlMR.ClientID%>').value;
                }

                var ddlFieldForceValue = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                <%--var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;

                var ddlYear = document.getElementById('<%=ddlYear.ClientID%>').value;--%>

                var selectedvalue = $('#<%= rbnList.ClientID %> input:checked').val();

                var ToMonYear = document.getElementById('<%=txtMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = ToMonYear[1];

                if (ddlMR != -1 && ddlMR != 0 && ddlMRName != '') {

                    showModalPopUp(ddlMR, Month1, Year1, selectedvalue, ddlMRName)
                }
                else {

                    showModalPopUp(ddlFieldForceValue, Month1, Year1, selectedvalue, FieldForce)
                }



            });
        });
    </script>

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
                        <h2 class="text-center">DCR View</h2>
                        <asp:Label ID="Lblmain" runat="server" SkinID="lblMand" Text="DCR View"></asp:Label>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDivision" Visible="false" runat="server" CssClass="label" Text="Division "></asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="custom-select2 nice-select"
                                    OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="FieldForce Name"></asp:Label>
                                <%--<asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                                 OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged">
                               <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                               <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            
                               </asp:DropDownList>
                                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                               </asp:DropDownList>--%>
                                <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                    ToolTip="Enter Text Here" AutoPostBack="true" OnTextChanged="txtNew_TextChanged"></asp:TextBox>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                    CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:CheckBox ID="chkVacant" Text=" Only Vacant Managers" AutoPostBack="true"
                                    OnCheckedChanged="chkVacant_CheckedChanged" runat="server" />
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblMR" runat="server" Text="Base Level" CssClass="label" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtBase" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                    ToolTip="Enter Text Here"></asp:TextBox>
                                <asp:DropDownList ID="ddlMR" runat="server" Width="100%" CssClass="custom-select2 nice-select" Visible="false">
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <%--              <asp:Label ID="Label2" runat="server" CssClass="label" Text="Year"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select" >
                                </asp:DropDownList>--%>
                                <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                            </div>
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
                            <%--             <div class="single-des clearfix">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Month"></asp:Label>
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
                                </asp:DropDownList>
                            </div>--%>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblmode" runat="server" CssClass="label"
                                    Text="Select the Mode"></asp:Label>
                                <div>
                                    <asp:RadioButtonList ID="rbnList" CellSpacing="5" runat="server"
                                        RepeatDirection="Horizontal" RepeatColumns="2">
                                        <asp:ListItem Text="DCRDoc">  View All DCR Doctor(s)</asp:ListItem>
                                        <asp:ListItem Text="DCRDates">  View All DCR Date(s)</asp:ListItem>
                                        <asp:ListItem Text="Remarks">  View All Remark(s)</asp:ListItem>
                                        <asp:ListItem Selected="True" Text="DetailedView">  Detailed View</asp:ListItem>
                                        <asp:ListItem Text="VALDRemark">  View All Listed Doctor Remark(s)</asp:ListItem>
                                        <asp:ListItem Text="NADDates">  Not Approved DCR Dates</asp:ListItem>
                                        <asp:ListItem Text="TPMyDayPlan">  TP MY Day Plan</asp:ListItem>
                                        <asp:ListItem Text="RCPA">  RCPA View</asp:ListItem>
                                        <asp:ListItem Text="REMINDCall">  Reminder calls</asp:ListItem>
                                        <%-- <asp:ListItem Text="RCPACapture">  RCPA Capture</asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" OnClick="btnSubmit_Click1" />
                        </div>
                        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                            Width="60%">
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


    </form>
</body>
</html>
