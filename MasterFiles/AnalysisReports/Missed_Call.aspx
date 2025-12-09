<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Missed_Call.aspx.cs" Inherits="MasterFiles_AnalysisReports_Missed_Call" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Missed Call</title>
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
        /*body {
            background-color: #e8ebec !important;
        }*/
    </style>
    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp_Miss(sfcode, fmon, fyr, tyear, tmonth, sf_name, Vmode, div_code) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            //  if (Vmode.trim() == "0") {

            popUpObj = window.open("rptMissed_Call.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + "&div_code=" + div_code,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });

            // LoadModalDiv();
        }


        function showModalPopUp(sfcode, fmon, fyr, sf_name, Vmode) {


            popUpObj = window.open("CallMonitor.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &sf_name=" + sf_name,
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
            //}

            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s10.postimg.org/te70y4ctl/loading_12_ook.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:100px; height: 100px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });

        }
        function showModalPopUp_Camp(sfcode, fmon, fyr, tyear, tmonth, sf_name, Vmode) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            //  if (Vmode.trim() == "0") {

            popUpObj = window.open("rptMissed_Call_Camp.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name,
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
            $(popUpObj.document.body).ready(function () {


                var ImgSrc = "https://s21.postimg.org/kd2btsn5j/loading.gif"





                $(popUpObj.document.body).append('<div class="preload"> <img src="' + ImgSrc + '"  style=" width:300px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
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
                if (Name == "--Select--") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                <%--var FYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
               var FMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                //if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }--%>

                <%-- var TYear = $('#<%=ddlToYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select From Year."); $('#ddlToYear').focus(); return false; }
               var TMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select From Month."); $('#ddlToMonth').focus(); return false; }--%>


                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                <%--var Year1 = document.getElementById('<%=ddlFrmYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlFrmMonth.ClientID%>').value;--%>

                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var fromMon = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var fromYear = frmMonYear[1];

                var mode = $('#<%=ddlmode.ClientID%> :selected').text();
                var Vmode = document.getElementById('<%=ddlmode.ClientID%>').value;

                var ToMonYear = "", ToMon = "", ToYear = "";
                if (Vmode.trim() == "0" || Vmode.trim() == "2") {
                    ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                    ToMon = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    ToYear = ToMonYear[1];
                }
                if (Vmode.trim() != "1") {
                    if (fromMon > ToMon && fromYear == ToYear) {

                        alert("To Month must be greater than From Month"); return false;
                    }
                    else if (fromYear > ToYear) {
                        alert("To Year must be greater than From Year"); return false;
                    }
                }

                if (Vmode.trim() == "0") {

                    var div_code;
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

                    //showModalPopUp_Miss(sf_Code, Month1, Year1, Year2, Month2, Name, Vmode, div_code);
                    showModalPopUp_Miss(sf_Code, fromMon, fromYear, ToYear, ToMon, Name, Vmode, div_code);
                }
                else if (Vmode.trim() == "1") {
                    showModalPopUp(sf_Code, fromMon, fromYear, Name, Vmode);
                }
                else if (Vmode.trim() == "2") {
                    <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                    var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>

                    //showModalPopUp_Camp(sf_Code, Month1, Year1, Year2, Month2, Name, Vmode);
                    showModalPopUp_Camp(sf_Code, fromMon, fromYear, ToYear, ToMon, Name, Vmode);
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
                        <h2 class="text-center">Missed Call</h2>

                        <asp:Label ID="Lblmain" SkinID="lblMand" runat="server" Text="Missed Call"></asp:Label>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lbMode" CssClass="label" runat="server" Text="Mode"></asp:Label>
                                <asp:DropDownList ID="ddlmode" runat="server" CssClass="nice-select" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlmode_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True" Text="Listed Doctor"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Call Monitor(Detailed)"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Campaign Doctor"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                <%--  <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                    ToolTip="Enter Text Here"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">

                                <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>


                                    <%--<asp:DropDownList ID="ddlFrmMonth" runat="server" CssClass="nice-select">
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
                                <div style="float: left; width: 45%; margin-left: 10%">
                                    <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                </div>
                                <%--         <div style="float: right; width: 45%;">
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                    <asp:DropDownList ID="ddlFrmYear" runat="server" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                            </div>
                            <%--<div class="single-des clearfix">
                                    <asp:Label ID="lbltomon" runat="server" CssClass="label" Text="To Month"></asp:Label>
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
                        <%--        <div style="float: right; width: 45%;">
                                    <asp:Label ID="lblToYear" runat="server" Text="To Year" CssClass="label"></asp:Label>
                                
                                    <asp:DropDownList ID="ddlToYear" runat="server" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                        </div>
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton"
                            OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        </div>
        <!-- Bootstrap -->
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
