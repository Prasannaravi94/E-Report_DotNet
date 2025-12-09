<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSalesReport.aspx.cs" Inherits="MasterFiles_Reports_SecSalesReport" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sales & Stock statement </title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>

    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        var popUpObj;


        function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name, Stok_code, sk_Name,div_code) {
            popUpObj = window.open("rpt_SecSale_Report.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + " &Stok_code=" + Stok_code + " &sk_Name=" + sk_Name+ " &div_code=" + div_code,
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

        function showModalPopUp_Console(sfcode, fmon, fyr, tyear, tmonth, sf_name, Stok_code, sk_Name) {
            popUpObj = window.open("rpt_SecSale_Report_Console.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + " &Stok_code=" + Stok_code + " &sk_Name=" + sk_Name,
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

        function showModalPopUp_Stockist(sfcode, fmon, fyr, tyear, tmonth, sf_name, Stok_code, sk_Name) {
            popUpObj = window.open("rpt_SecSale_Report_Stockistwise.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + " &Stok_code=" + Stok_code + " &sk_Name=" + sk_Name,
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
                <%--var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "--Select--") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
                var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "--Select--") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }

                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                if (TYear == "--Select--") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }
                var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "--Select--") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }--%>

                var Stok = $('#<%=ddlStockiest.ClientID%> :selected').text();
                if (Stok == "---Select---") { alert("Select Stockiest."); $('#ddlStockiest').focus(); return false; }

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                <%--var Year1 = document.getElementById('<%=ddlFYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlFMonth.ClientID%>').value;


                var Year2 = document.getElementById('<%=ddlTYear.ClientID%>').value;
                var Month2 = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>
                var Stok_code = document.getElementById('<%=ddlStockiest.ClientID%>').value;
                var stk_Name = $('#<%=ddlStockiest.ClientID%> :selected').text();

                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = frmMonYear[1];

                var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var Year2 = ToMonYear[1];

                var rdolstrpt = $('#<%=rdolstrpt.ClientID %> input:checked').val();
                if (rdolstrpt == 2) {
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
                    showModalPopUp(sf_Code, Month1, Year1, Year2, Month2, Name, Stok_code, stk_Name, div_code);
                }
                else if (rdolstrpt == 1) {
                    showModalPopUp_Console(sf_Code, Month1, Year1, Year2, Month2, Name, Stok_code, stk_Name);
                }
                else if (rdolstrpt == 3) {
                    showModalPopUp_Stockist(sf_Code, Month1, Year1, Year2, Month2, Name, Stok_code, stk_Name);
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
        <div id="Divid" runat="server">
        </div>

        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <h2 class="text-center" style="border-bottom: none">Sales & Stock statement</h2>

                    <asp:Label ID="Lblmain" runat="server" Text="Secondary Sale" SkinID="lblMand"></asp:Label>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblSF" runat="server" Text="Field Force Name " CssClass="label"></asp:Label>
                            <%--    <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                ToolTip="Enter Text Here"></asp:TextBox>
                            <asp:LinkButton ID="linkcheck" runat="server"
                                OnClick="linkcheck_Click">
                          <img src="../../Images/Selective_Mgr.png" />
                            </asp:LinkButton>--%>

                            <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" Width="100%" CssClass="custom-select2 nice-select"
                                OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                            </asp:DropDownList>

                            <%--     <div id="testImg">
                                <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;" runat="server" /><span
                                    style="font-family: Verdana; color: Red; font-weight: bold;">Loading Please Wait...</span>
                            </div>--%>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblStockiest" runat="server" Text="Stockiest" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlStockiest" runat="server" CssClass="nice-select">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <div style="float: left; width: 45%;">
                                <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div style="float: right; width: 45%;">
                                <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                            </div>
                            <%--<div style="float: left; width: 45%;">
                                <asp:Label ID="lblFMonth" runat="server" Text="From Month " CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlFMonth" runat="server" CssClass="nice-select">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                                <asp:Label ID="lblFYear" runat="server" Text="From Year " CssClass="label"></asp:Label>

                                <asp:DropDownList ID="ddlFYear" runat="server" CssClass="nice-select">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                </asp:DropDownList>
                            </div>--%>
                        </div>
                        <%--              <div class="single-des clearfix">
                            <div style="float: left; width: 45%;">
                                <asp:Label ID="lblTMonth" runat="server" Text="To Month " CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlTMonth" runat="server" CssClass="nice-select">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                                <asp:Label ID="lblTYear" runat="server" Text="To Year " CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlTYear" runat="server" CssClass="nice-select">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblrptType" runat="server" Text="Report Type" CssClass="label"></asp:Label>
                            <asp:RadioButtonList ID="rdolstrpt" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Text="Consolidated&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Monthwise&nbsp;&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Stockistwise"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Go" CssClass="savebutton"
                            OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
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
