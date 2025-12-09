<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Primary_Product_Bill_View.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Primary_Product_Bill_View" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Primary Bill Product View</title>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(fmon, fyr, tyear, tmonth, Sf_Code, div_code) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            //  if (Vmode.trim() == "0") {

            popUpObj = window.open("rpt_Primary_Product_Bill_View.aspx?Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + "&Sf_Code=" + Sf_Code + "&div_code=" + div_code,
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

                var ImgSrc = "https://s10.postimg.org/te70y4ctl/loading_12_ook.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:100px; height: 100px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


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


                <%--var FYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
                var FMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }

                var TYear = $('#<%=ddlToYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select From Year."); $('#ddlToYear').focus(); return false; }
                var TMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select From Month."); $('#ddlToMonth').focus(); return false; }


                var Year1 = document.getElementById('<%=ddlFrmYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlFrmMonth.ClientID%>').value;
                var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
                var Sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = frmMonYear[1];

                var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var Year2 = ToMonYear[1];

                if (Month1 > Month2 && Year1 == Year2) {

                    alert("To Month must be greater than From Month"); return false;
                }
                else if (Year1 > Year2) {
                    alert("To Year must be greater than From Year"); return false;
                }
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
                showModalPopUp(Month1, Year1, Year2, Month2, Sf_Code,div_code);


            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <%-- <ucl:Menu ID="menu1" runat="server" />--%>
             <div id="Divid" runat="server">
        </div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center" id="dheading" runat="server"></h2>
                         <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                <%--  <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                    ToolTip="Enter Text Here"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                    <div style="float: left; width: 45%">
                                        <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div style="float: right; width: 45%">
                                        <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
              <%--                  <div style="float: left; width: 45%">
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
         <%--                   <div class="single-des clearfix">
                                <div style="float: left; width: 45%">
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
                                <div style="float: right; width: 45%">
                                    <asp:Label ID="lblToYear" runat="server" Text="To Year" CssClass="label"></asp:Label>
                                    
                                    <asp:DropDownList ID="ddlToYear" runat="server" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
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
