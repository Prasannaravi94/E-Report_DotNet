<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fieldwork_Analysis.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Fieldwork_Analysis" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FieldWork Manager - Analysis</title>
    <style type="text/css">
        .padding {
            padding: 3px;
        }

        .chkboxLocation label {
            padding-left: 5px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Fieldwork_Pivot.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name,
 "ModalPopUp"//,
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

                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                <%--var FYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
                var FMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }

                var TYear = $('#<%=ddlToYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select From Year."); $('#ddlToYear').focus(); return false; }
                var TMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select From Month."); $('#ddlToMonth').focus(); return false; }--%>


                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                <%--var Year1 = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                var Month1 = $('#ddlFrmMonth').find(":selected").index();
                var Year2 = $('#<%=ddlToYear.ClientID%> :selected').text();
                var Month2 = $('#ddlToMonth').find(":selected").index();--%>

                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var fromMon = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var fromYear = frmMonYear[1];

                var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var ToMon = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var ToYear = ToMonYear[1];

                var mnth = fromMon, yr = parseInt(fromYear), validate = '', tmp = '';
                if ((fromMon <= ToMon && parseInt(fromYear) === parseInt(ToYear)) || (parseInt(fromYear) < parseInt(ToYear) && (fromMon <= ToMon || fromMon >= ToMon))) {
                    showModalPopUp(sf_Code, fromMon, fromYear, ToYear, ToMon, Name);
                }
                else {
                    alert("Select Valid Month & Year...");
                    return false;
                }
            });
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



    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.on('keyup', function () {
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

    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">FieldWork Manager - Analysis</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>

                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <%--<div class="col-lg-6">
                                        <asp:Label ID="lblFrmMoth" runat="server" Text="From Month" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlFrmMonth" runat="server" CssClass="nice-select">
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
                                    <div class="col-lg-6">
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                        <asp:DropDownList ID="ddlFrmYear" runat="server" CssClass="nice-select">
                                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>--%>
                                </div>
                            </div>
                            <%--  <div class="single-des clearfix">
                                <div class="row">
                                  <div class="col-lg-6">
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="To Month"></asp:Label>
                                        <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="nice-select">
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
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblToYear" runat="server" Text="To Year" CssClass="label"></asp:Label>

                                        <asp:DropDownList ID="ddlToYear" runat="server" CssClass="nice-select">
                                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>

                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />

                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
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
    </form>
</body>
</html>
