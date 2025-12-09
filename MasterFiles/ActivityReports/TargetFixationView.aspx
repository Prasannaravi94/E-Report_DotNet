<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="TargetFixationView.aspx.cs" Inherits="TargetFixationView" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <style type="text/css">
        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .dd {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .ddl1 {
            border: 1px solid #1E90FF;
            border-radius: 5px;
            width: 190px;
            height: 21px;
            font: bold;
            background-image: url('Images/arrow_sort_d.gif');
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow:;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        body {
            font-size: 62.5%;
            background: none !important;
            background-color: #fafdff !important;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#effect").hide();
            //run the currently selected effect
            function runEffect() {
                //get effect type from
                if (!($('#effect').is(":visible"))) {
                    //run the effect
                    $("#effect").show('blind', 200);
                }
                else {
                    $("#effect").hide('blind', 200);
                }
            };

            //set effect from select menu value
            $("#ddlArrow").click(function () {
                runEffect();
                return false;
            });


            $(document).click(function (e) { if (($('#effect').is(":visible"))) { $("#effect").hide('blind', 1000); } });

            $('#effect').click(function (e) {
                e.stopPropagation();
            });
        });

        function autoCompleteEx_ItemSelected(sender, args) {
            __doPostBack(sender.get_element().name, "");
        }

        function OpenNewWindow() {
            var elemField = document.getElementById("<%= ddlFieldForce.ClientID %>");
            <%--var ddlFromMonth = document.getElementById("<%= ddlFromMonth.ClientID %>");
            var ddlFromYear = document.getElementById("<%= ddlFromYear.ClientID %>");
            var ddlToMonth = document.getElementById("<%= ddlToMonth.ClientID %>");
            var ddlToYear = document.getElementById("<%= ddlToYear.ClientID %>");--%>

            var sf = elemField.selectedIndex;
            <%--var fmSI = ddlFromMonth.selectedIndex;
            var fySI = ddlFromYear.selectedIndex;
            var tmSI = ddlToMonth.selectedIndex;
            var tySI = ddlToYear.selectedIndex;--%>

            if (sf == 0) {
                alert("Please select FieldForce!!");
                elemField.focus();
                return false;
            }
            //else if (fmSI == 0) {
            //    alert("Please select From Month!!");
            //    ddlFromMonth.focus();
            //    return false;
            //}
                //            if (fySI == 0) {
                //                alert("Please select From Year!!");
                //                ddlFromYear.focus();
                //                return false;
                //            }
            //else if (tmSI == 0) {
            //    alert("Please select To Month!!");
            //    ddlToMonth.focus();
            //    return false;
            //}
            //            else if (tySI == 0) {
            //                alert("Please select To Year!!");
            //                ddlToYear.focus();
            //                return false;
            //            }
            var sfCode = elemField.options[sf].value;
            <%--var fmCode = ddlFromMonth.options[fmSI].value;
            var fyCode = ddlFromYear.options[fySI].value;
            var tmCode = ddlToMonth.options[tmSI].value;
            var tyCode = ddlToYear.options[tySI].value;--%>

            var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
            var FMont = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
            var FYea = frmMonYear[1];

            var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var TMont = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var TYea = ToMonYear[1];

            //window.open('TargetFixationView_Report_New.aspx?sf_code=' + sfCode + '&sf_name=' + elemField.options[sf].text + '&fromMonthName=' + ddlFromMonth.options[fmSI].text + '&Frm_Month=' + fmCode + '&Frm_year=' + fyCode + '&toMonthName=' + ddlToMonth.options[tmSI].text + '&To_Month=' + tmCode + '&To_year=' + tyCode + '',
            //    'ModalPopUp'//, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no'
            //        );
                window.open('TargetFixationView_Report_New.aspx?sf_code=' + sfCode + '&sf_name=' + elemField.options[sf].text + '&fromMonthName=' + frmMonYear[0] + '&Frm_Month=' + FMont + '&Frm_year=' + FYea + '&toMonthName=' + ToMonYear[0] + '&To_Month=' + TMont + '&To_year=' + TYea + '',
                 'ModalPopUp'//, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no'
                     );
                return false;
            }

    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
        });
    </script>
    <div id="Divid" runat="server">
    </div>
    <div class="container home-section-main-body position-relative clearfix">
        <div class="row justify-content-center">
            <div class="col-lg-5">
                <center>
                    <table>
                        <tr>
                            <td align="center">
                                <h2 class="text-center">Target Productwise View</h2>
                            </td>
                        </tr>
                    </table>
                </center>
                <div class="designation-area clearfix">
                    <div class="single-des clearfix">
                        <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false" CssClass="ddl">
                        </asp:DropDownList>
                    </div>
                    <div class="single-des clearfix">
                        <div style="float: left; width: 45%;">
                            <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                            <input id="txtFromMonthYear" type="text" runat="server" class="nice-select" readonly="true" />
                        </div>
                        <div style="float: right; width: 45%;">
                            <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                            <input id="txtToMonthYear" type="text" runat="server" class="nice-select" readonly="true" />
                        </div>
                        <%--    <div style="float: left; width: 45%;">
                            <asp:Label ID="lblFromMonth" runat="server" CssClass="label" Text="From Month"></asp:Label>
                            <asp:DropDownList ID="ddlFromMonth" runat="server" SkinID="ddlRequired" CssClass="ddl">
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
                        <div style="float: right; width: 45%;">
                            <asp:Label ID="lblFromYear" runat="server" CssClass="label" Text="From Year" Width="60"></asp:Label>
                            <asp:DropDownList ID="ddlFromYear" runat="server" SkinID="ddlRequired" CssClass="ddl"
                                Width="60">
                            </asp:DropDownList>
                        </div>--%>
                    </div>
                    <%--  <div class="single-des clearfix">
                        <div style="float: left; width: 45%;">
                            <asp:Label ID="lblToMonth" runat="server" CssClass="label" Text="To Month"></asp:Label>
                            <asp:DropDownList ID="ddlToMonth" runat="server" SkinID="ddlRequired" CssClass="ddl">
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
                        <div style="float: right; width: 45%;">
                            <asp:Label ID="lblToYear" runat="server" CssClass="label" Text="To Year" Width="60"></asp:Label>
                            <asp:DropDownList ID="ddlToYear" runat="server" SkinID="ddlRequired" CssClass="ddl"
                                Width="60">
                            </asp:DropDownList>
                        </div>
                    </div>--%>
                </div>
                <div class="w-100 designation-submit-button text-center clearfix">
                    <asp:Button ID="CmdView" runat="server" Text="View" CssClass="savebutton" OnClientClick="OpenNewWindow();return false;" />
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

    <!-- Bootstrap Datepicker -->
    <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
    <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
    <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
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
</asp:Content>
