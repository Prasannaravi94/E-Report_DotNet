<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TargetVsSales.aspx.cs" Inherits="MasterFiles_ActivityReports_TargetVsSales" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Target Vs Sales</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" href="../../css/Report.css" rel="Stylesheet" />
    <%--<link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />--%>
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />

    <script type="text/javascript">
        function OpenNewWindow() {
            var elemField = document.getElementById("<%= ddlFieldForce.ClientID %>");
            var ddlFromMonth = document.getElementById("<%= ddlFromMonth.ClientID %>");
            var ddlFromYear = document.getElementById("<%= ddlFromYear.ClientID %>");
            var ddlToMonth = document.getElementById("<%= ddlToMonth.ClientID %>");
            var ddlToYear = document.getElementById("<%= ddlToYear.ClientID %>");
            var mdfld = document.getElementById("<%= Drpmode.ClientID %>");

            var sf = elemField.selectedIndex;
            var fmSI = ddlFromMonth.selectedIndex;
            var fySI = ddlFromYear.selectedIndex;
            var tmSI = ddlToMonth.selectedIndex;
            var tySI = ddlToYear.selectedIndex;
            var mdf = mdfld.selectedIndex;

            //              if (sf == 0) {
            //                  alert("Select FieldForce!!");
            //                  elemField.focus();
            //                  return false;
            //              }
            if (fmSI == 0) {
                alert("Select From Month!!");
                ddlFromMonth.focus();
                return false;
            }
            //            if (fySI == 0) {
            //                alert("Please select From Year!!");
            //                ddlFromYear.focus();
            //                return false;
            //            }
            if (tmSI == 0) {
                alert("Select To Month!!");
                ddlToMonth.focus();
                return false;
            }
            if (mdf == 0) {
                alert("Select the Mode!!");
                mdfld.focus();
                return false;
            }
            //            else if (tySI == 0) {
            //                alert("Please select To Year!!");
            //                ddlToYear.focus();
            //                return false;
            //            }
            var sfCode = elemField.options[sf].value;
            var fmCode = ddlFromMonth.options[fmSI].value;
            var fyCode = ddlFromYear.options[fySI].value;
            var tmCode = ddlToMonth.options[tmSI].value;
            var tyCode = ddlToYear.options[tySI].value;
            var tmd = mdfld.options[mdf].value;

            var div_code = "";
            var sf_type = '<%=Session["sf_type"] %>';

            if (sf_type == 1 || sf_type == 2) {
                div_code = '<%=Session["div_code"] %>';
            }
            else {
                div_code = '<%=Session["div_code"] %>';
                if (div_code.includes(",")) {
                    div_code = div_code.substring(0, div_code.length - 1)
                }
            }

            var popUpObj;
            if (tmd == "TSC") {
                popUpObj = window.open('rptTargetVsSales_Cummulative.aspx?sf_code=' + sfCode + '&sf_name=' + elemField.options[sf].text + '&fromMonthName=' + ddlFromMonth.options[fmSI].text + '&Frm_Month=' + fmCode + '&Frm_year=' + fyCode + '&toMonthName=' + ddlToMonth.options[tmSI].text + '&To_Month=' + tmCode + '&To_year=' + tyCode + '&div_code=' + div_code +
                    '',
                    'ModalPopUp'//, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no'
                    );
            }
            else if (tmd == "TSP") {
                popUpObj = window.open('rptTargetVsSales_Cummulative_Performance.aspx?sf_code=' + sfCode + '&sf_name=' + elemField.options[sf].text + '&fromMonthName=' + ddlFromMonth.options[fmSI].text + '&Frm_Month=' + fmCode + '&Frm_year=' + fyCode + '&div_code=' + div_code + '',
                    'ModalPopUp'//, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no'
                    );
            }
            else if (tmd == "TVSP") {
                popUpObj = window.open('rptTargetVsSales_New_Primary.aspx?sf_code=' + sfCode + '&sf_name=' + elemField.options[sf].text + '&fromMonthName=' + ddlFromMonth.options[fmSI].text + '&Frm_Month=' + fmCode + '&Frm_year=' + fyCode + '&toMonthName=' + ddlToMonth.options[tmSI].text + '&To_Month=' + tmCode + '&To_year=' + tyCode + '&div_code=' + div_code + '',
                    'ModalPopUp'//, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no'
                    );
                    //null, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            }
            else if (tmd == "TVSPF") {
                popUpObj = window.open('rptTargetVsSales_FF.aspx?&mode=2&sf_code=' + sfCode + '&sf_name=' + elemField.options[sf].text + '&fromMonthName=' + ddlFromMonth.options[fmSI].text + '&Frm_Month=' + fmCode + '&Frm_year=' + fyCode + '&toMonthName=' + ddlToMonth.options[tmSI].text + '&To_Month=' + tmCode + '&To_year=' + tyCode + '&div_code=' + div_code + '',
                    'ModalPopUp'//, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no'
                    );
                //null, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            }
            else if (tmd == "TVSPHQ") {
                popUpObj = window.open('rptTargetVsSales_FF.aspx?&mode=6&sf_code=' + sfCode + '&sf_name=' + elemField.options[sf].text + '&fromMonthName=' + ddlFromMonth.options[fmSI].text + '&Frm_Month=' + fmCode + '&Frm_year=' + fyCode + '&toMonthName=' + ddlToMonth.options[tmSI].text + '&To_Month=' + tmCode + '&To_year=' + tyCode + '&div_code=' + div_code + '',
                    'ModalPopUp'//, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no'
                    );
                //null, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            }
            else {
                popUpObj = window.open('rptTargetVsSales.aspx?sf_code=' + sfCode + '&sf_name=' + elemField.options[sf].text + '&fromMonthName=' + ddlFromMonth.options[fmSI].text + '&Frm_Month=' + fmCode + '&Frm_year=' + fyCode + '&toMonthName=' + ddlToMonth.options[tmSI].text + '&To_Month=' + tmCode + '&To_year=' + tyCode + '&div_code=' + div_code + '',
                    'ModalPopUp'//, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no'
                    );
            }

            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s3.postimg.org/h5u7rfuvn/08_spinner.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red; width:180px; margin:0 auto;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:600px; height: 400px;position: fixed;top: 10%;left: 15%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
            return false;
        }
        
        function OpenNewWindow1() {
            var elemField = document.getElementById("<%= ddlFieldForce.ClientID %>");
            var ddlFromMonth = document.getElementById("<%= ddlFromMonth.ClientID %>");
            var ddlFromYear = document.getElementById("<%= ddlFromYear.ClientID %>");
            var mdfld = document.getElementById("<%= Drpmode.ClientID %>");

            var sf = elemField.selectedIndex;
            var fmSI = ddlFromMonth.selectedIndex;
            var fySI = ddlFromYear.selectedIndex;

            var mdf = mdfld.selectedIndex;

            //              if (sf == 0) {
            //                  alert("Select FieldForce!!");
            //                  elemField.focus();
            //                  return false;
            //              }
            if (fmSI == 0) {
                alert("Select From Month!!");
                ddlFromMonth.focus();
                return false;
            }
            //            if (fySI == 0) {
            //                alert("Please select From Year!!");
            //                ddlFromYear.focus();
            //                return false;
            //            }

            if (mdf == 0) {
                alert("Select the Mode!!");
                mdfld.focus();
                return false;
            }
            //            else if (tySI == 0) {
            //                alert("Please select To Year!!");
            //                ddlToYear.focus();
            //                return false;
            //            }
            var sfCode = elemField.options[sf].value;
            var fmCode = ddlFromMonth.options[fmSI].value;
            var fyCode = ddlFromYear.options[fySI].value;
            var tmd = mdfld.options[mdf].value;

            var div_code = "";
            var sf_type = '<%=Session["sf_type"] %>';

            if (sf_type == 1 || sf_type == 2) {
                div_code = '<%=Session["div_code"] %>';
            }
            else {
                div_code = '<%=Session["div_code"] %>';
                if (div_code.includes(",")) {
                    div_code = div_code.substring(0, div_code.length - 1)
                }
            }

            var popUpObj;

            popUpObj = window.open('rptTargetVsSales_Cummulative_Performance.aspx?sf_code=' + sfCode + '&sf_name=' + elemField.options[sf].text + '&fromMonthName=' + ddlFromMonth.options[fmSI].text + '&Frm_Month=' + fmCode + '&Frm_year=' + fyCode + '&div_code=' + div_code + '',
                'ModalPopUp'//, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no'
                    );


            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s3.postimg.org/h5u7rfuvn/08_spinner.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red; width:180px; margin:0 auto;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:600px; height: 400px;position: fixed;top: 10%;left: 15%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
            return false;
        }
    </script>

    <script type="text/javascript">
        $(function () {
            //var $txt = $('input[id$=txtNew]');
            //var $ddl = $('select[id$=ddlFieldForce]');
            //var $items = $('select[id$=ddlFieldForce] option');

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
        function mnthdisable($x) {
            alert($x.value);
            var ddlToMonth = document.getElementById("<%= ddlToMonth.ClientID %>");
            var ddlToYear = document.getElementById("<%= ddlToYear.ClientID %>");
            alert(ddlToMonth);
            var val = $x.value;
            if (val = "TSC") {
                ddlToMonth.style.display = none;
                ddlToYear.style.display = none;
            }
        }
    </script>
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
    </style>
         <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>

    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
   <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <center>
                        <table>
                            <tr>
                                <td align="center">
                                    <h2 class="text-center">Target vs Sales</h2>
                                </td>
                            </tr>
                        </table>
                    </center>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblFilter" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%" >
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false" CssClass="ddl">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="Mde" runat="server" Text="Mode" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="Drpmode" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="onselected_mnthdisable" AutoPostBack="true">
                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="TVSS" Text="Target Vs Sales(Secondary)"></asp:ListItem>
                                <asp:ListItem Value="TVSP" Text="Target Vs Sales(Primary)"></asp:ListItem>
                                <%--<asp:ListItem Value="TVSPF" Text="Target Vs Sales(Primary-Fieldforcewise)"></asp:ListItem>--%>
                                <%--<asp:ListItem Value="TVSPHQ" Text="Target Vs Sales(Primary-Only HQwise)"></asp:ListItem>--%>
                              <%--  <asp:ListItem Value="TSC" Text="Target/sales Cumulative"></asp:ListItem>
                                <asp:ListItem Value="TSP" Text="Sales Performance"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </div> 
                        <div class="single-des clearfix">
                            <div style="float: left; width: 45%;">
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
                            </div>
                        </div>
                        <div class="single-des clearfix">
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
                        </div>
                        <center>
                            <asp:Button ID="CmdView" runat="server" Text="View" OnClientClick="OpenNewWindow();return false;" CssClass="savebutton" />
                            <asp:Button ID="CmdpView" runat="server" Text="View" OnClientClick="OpenNewWindow1();return false;" Visible="false" CssClass="savebutton" />
                        </center>
                    </div>
                </div>
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
