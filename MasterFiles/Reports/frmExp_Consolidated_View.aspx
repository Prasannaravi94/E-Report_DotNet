<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmExp_Consolidated_View.aspx.cs" Inherits="MasterFiles_frmExp_Consolidated_View" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <title>Expense Consolidate View</title>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp1(sfcode, fmon, fyr, fmon1, fyr1, sf_name) {
            popUpObj = window.open("rptExp_Consolidated_View_Multiple.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&cur_month1=" + fmon1 + "&cur_year1=" + fyr1 + "&sf_name=" + sf_name,
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
        function showModalPopUp(sfcode, fmon, fyr, sf_name) {
            popUpObj = window.open("rptExp_Consolidated_View.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&sf_name=" + sf_name,
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

        function showModalPopUp(sfcode, fmon, fyr, StrVac, sf_name) {
            popUpObj = window.open("rptExp_Consolidated_View.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&StrVac=" + StrVac + "&sf_name=" + sf_name,
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

                var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (sf_name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                var Month = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#ddlFrmMonth').focus(); return false; }
                var year = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (year == "---Select---") { alert("Select Year."); $('#ddlFrmYear').focus(); return false; }
                var ddlMonth = document.getElementById('<%=ddlFrmMonth.ClientID%>').value;

                var ddlYear = document.getElementById('<%=ddlFrmYear.ClientID%>').value;


                var sf_code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                var chkProcessed = document.getElementById('<%=chkProcessed.ClientID%>');
                var chkVacant = document.getElementById("chkVacant");

                var strValue = 1;
                if (chkVacant.checked) {
                    var strValue = 0;
                }
                if (chkProcessed.checked) {
                    var ddlMonth1 = document.getElementById('<%=ddlToMonth.ClientID%>').value;

                    var ddlYear1 = document.getElementById('<%=ddlToYear.ClientID%>').value;

                    var Month1 = $('#<%=ddlToMonth.ClientID%> :selected').text();
                    if (Month1 == "---Select---") { alert("Select Month."); $('#ddlToMonth').focus(); return false; }
                    var Year1 = $('#<%=ddlToYear.ClientID%> :selected').text();
                    if (Year1 == "---Select---") { alert("Select Month."); $('#ddlToYear').focus(); return false; }

                    showModalPopUp1(sf_code, ddlMonth, ddlYear, ddlMonth1, ddlYear1, sf_name);

                }
                else {
                    //showModalPopUp(sf_code, ddlMonth, ddlYear, sf_name);
                    showModalPopUp(sf_code, ddlMonth, ddlYear, strValue, sf_name);
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
    <style type="text/css">
        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

    </style>
   
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div>
                <div class="container home-section-main-body position-relative clearfix">
                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <h2 class="text-center" style="border-bottom: none">Expense Consolidate View</h2>
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblState" runat="server" Text="Field Force Name" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" Visible="true" CssClass="custom-select2 nice-select" Width="100%">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                    </asp:DropDownList>
                                </div>
                                <div class="single-des clearfix">
                                    <div style="float: left; width: 45%;">
                                        <asp:Label ID="lblFrmMoth" runat="server" Text="From Month" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlFrmMonth" runat="server" SkinID="ddlRequired">
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
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                        <asp:DropDownList ID="ddlFrmYear" runat="server" SkinID="ddlRequired">
                                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="single-des clearfix">
                                    <div style="float: left; width: 45%;">
                                        <asp:Label ID="Label4" runat="server" Text="To Month" CssClass="label" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlToMonth" runat="server" SkinID="ddlRequired" Visible="false">
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
                                        <asp:Label ID="lblToYear" runat="server" Text="To Year" CssClass="label" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlToYear" runat="server" SkinID="ddlRequired" Visible="false">
                                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="single-des clearfix">
                                    <asp:CheckBox ID="chkVacant" Checked="true" runat="server" Text="Including Vacant" />
                                    <%--<asp:Label ID="lblVacant" Text="Including Vacant" runat="server" CssClass="label"></asp:Label>--%>
                                </div>
                                <div class="single-des clearfix">
                                    <asp:CheckBox ID="chkProcessed" Text=" At a Glance" runat="server" AutoPostBack="true" OnCheckedChanged="monthdisable" />
                                </div>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnSubmit" runat="server" class="savebutton" Text="View" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <center>
                </center>
                <div class="loading" align="center">
                    Loading. Please wait.<br />
                    <br />
                    <img src="../../Images/loader.gif" alt="" />
                </div>
            </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>
