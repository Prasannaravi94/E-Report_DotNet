<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrimarySale_Multi_Mnth_Dump.aspx.cs" Inherits="MasterFiles_AnalysisReports_PrimarySale_Multi_Mnth_Dump" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Primary Sales - Dump </title>

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


    <%--    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>

    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
       
      
        function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name, selectedValue,div_code) {
            popUpObj = window.open("rpt_PrimarySale_Multi_Mnth_HQWise.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + "&To_year=" + tyear + "&To_Month=" + tmonth + "&selectedValue=" + selectedValue + "&sf_name=" + sf_name + "&div_code=" + div_code + "&Stok_code=" + "-2" + " &sk_Name=" + " ",
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
        }

            
        var popUpObj1;
        function showModalPopUp_HQ(sfcode, fmon, fyr, tyear, tmonth, sf_name, selectedValue, div_code) {
            popUpObj = window.open('/MasterFiles/ActivityReports/rptTargetVsSales_FF.aspx?&mode=' + selectedValue + '&sf_code=' + sfcode + '&sf_name=' + sf_name + '&fromMonthName=' + "" + '&Frm_Month=' + fmon + '&Frm_year=' + fyr + '&toMonthName=' + "" + '&To_Month=' + tmonth + '&To_year=' + tyear + '&div_code=' + div_code,
                'ModalPopUp'//, 'height=600, width=900,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no'
                );
            popUpObj1.focus();
            $(popUpObj1.document.body).ready(function () {
                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"
                $(popUpObj1.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');

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
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                <%--var FYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (FYear == "--Select--") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
                var FMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                if (FMonth == "--Select--") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }

                var TYear = $('#<%=ddlToYear.ClientID%> :selected').text();
                if (TYear == "--Select--") { alert("Select To Year."); $('#ddlToYear').focus(); return false; }
                var TMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
                if (TMonth == "--Select--") { alert("Select To Month."); $('#ddlToMonth').focus(); return false; }--%>

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                <%--var Year1 = document.getElementById('<%=ddlFrmYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlFrmMonth.ClientID%>').value;
                var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
                var selectedValue = $("#<%= rdolstrpt.ClientID %> input:checked").val();

                <%--var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');--%>
                var frmMonYear = document.getElementById('txtFromMonthYear').value.split('-');
                var Month1 = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = frmMonYear[1];

                <%--var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');--%>
                var ToMonYear = document.getElementById('txtToMonthYear').value.split('-');
                var Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var Year2 = ToMonYear[1];
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
                if (selectedValue == '6' ) {
                    showModalPopUp_HQ(sf_Code, Month1, Year1, Year2, Month2, Name, "6",div_code);
                }
                else if (selectedValue == '5') {
                    showModalPopUp_HQ(sf_Code, Month1, Year1, Year2, Month2, Name, "2",div_code);
                }
                else {
                    showModalPopUp(sf_Code, Month1, Year1, Year2, Month2, Name, selectedValue,div_code);
                }
            });
        });
    </script>


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var selectedValue = $("#<%= rdolstrpt.ClientID %> input:checked").val();
            //var selectedValue = $("#rdolstrpt").val();
            //var selectedText = $(this).next().html();
            if (selectedValue == '1' || selectedValue == '2' || selectedValue == '7') {
                $("#btnSubmit").css("display", "none");
                $("#btnDump").css("display", "block");
            }
            else {
                $("#btnSubmit").css("display", "block");
                $("#btnDump").css("display", "none");
            }

            $("[id*=rdolstrpt] input").on("click", function () {
                var selectedValue = $(this).val();
                var selectedText = $(this).next().html();
                if (selectedValue == '3' || selectedValue == '4' || selectedValue == '6' || selectedValue == '5') {
                    $("#btnSubmit").css("display", "block");
                    $("#btnDump").css("display", "none");
                }
                else {
                    $("#btnSubmit").css("display", "none");
                    $("#btnDump").css("display", "block");

                }
            });
        });
    </script>

    <%--  <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var ddlVal = $("#rdolstrpt").val();

            if (ddlVal == "3") {
                $("#btnSubmit").css("display", "block");
            }
            else {
                $("#btnSubmit").css("display", "none");
            }

            $("#rdolstrpt").change(function () {
                var ddlVal = $("#rdolstrpt").val();

                if (ddlVal == "3") {
                    $("#btnSubmit").css("display", "block");
                }
                else {
                    $("#btnSubmit").css("display", "none");
                }
            });
        });

    </script>--%>

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
                        <h2 class="text-center">Primary Sales - Dump </h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                    Visible="false" ToolTip="Enter Text Here"></asp:TextBox>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                    <%--     <asp:ListItem Selected="True">---Select---</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>


                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%">
                                    <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                    <%--<asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>--%>
                                    <input type="text" id="txtFromMonthYear" runat="server" class="nice-select" readonly="true" />
                                </div>
                                <div style="float: right; width: 45%">
                                    <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                    <%--<asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>--%>
                                    <input type="text" id="txtToMonthYear" runat="server" class="nice-select" readonly="true" />
                                </div>
                                <%--                 <div style="float: left; width: 45%">
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
                                <div style="float: right; width: 45%">
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                    <asp:DropDownList ID="ddlFrmYear" runat="server" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                            </div>

                            <%--                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%">
                                    <asp:Label ID="Label1" runat="server" Text="To Month" CssClass="label"></asp:Label>
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
                                <div style="float: right; width: 45%">
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="To Year"></asp:Label>
                                    <asp:DropDownList ID="ddlToYear" runat="server" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>--%>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblrptType" runat="server" Text="Report Type" CssClass="label"></asp:Label>
                                <asp:RadioButtonList ID="rdolstrpt" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                                    <%--   <asp:ListItem Value="1" Text="Fieldforce Wise&nbsp;&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Product Wise&nbsp;&nbsp;&nbsp;"></asp:ListItem>--%>
                                    <asp:ListItem Value="3" Text="Primary Sale HQ Wise(Bifurcation)&nbsp;&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Primary Sale Product Wise&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Primary Sale HQ Wise&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Primary Sale FF Wise&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Primary Sale FF HQ Wise&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <center>
                                <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />
                                <asp:LinkButton ID="btnDump" runat="server" Font-Size="Medium" Font-Bold="true"
                                    Text="Download Excel" OnClick="btnSubmit_Click" />
                            </center>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

        <!-- Bootstrap Datepicker -->
        <%--<script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>--%>
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

