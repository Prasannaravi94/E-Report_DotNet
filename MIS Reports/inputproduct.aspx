<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inputproduct.aspx.cs" Inherits="MIS_Reports_inputproduct" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Input Despatch View</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" href="../../css/Report.css" rel="Stylesheet" />
    <%--<link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />--%>
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
   
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
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }
    </style>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
        });
    </script>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
         <script type="text/javascript">
             var popUpObj;
             function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, SName, Day_Wise) {
                 //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                 popUpObj = window.open("inputdes.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&sf_name=" + SName + "&Day_Wise=" + Day_Wise,
                     "ModalPopUp"//,
                     //"toolbar=no," +
                     //"scrollbars=yes," +
                     //"location=no," +
                     //"statusbar=no," +
                     //"menubar=no," +
                     //"addressbar=no," +
                     //"resizable=yes," +
                     //"width=900," +
                     //"height=600," +
                     //"left = 0," +
                     //"top=0"
                 );
                 popUpObj.focus();
                 //LoadModalDiv();
             }
         </script>
         <script type="text/javascript">
             var popUpObj1;

             function showModalPopUpCheck(sf_Code, FDate, TDate, SName) {
                 
                 popUpObj1 = window.open("InputDatewise_Report.aspx?sfcode=" + sf_Code + "&FDate=" + FDate + "&TDate=" + TDate + "&sf_name=" + SName,
                     "ModalPopUp"//,
                     //"toolbar=no," +
                     //"scrollbars=yes," +
                     //"location=no," +
                     //"statusbar=no," +
                     //"menubar=no," +
                     //"addressbar=no," +
                     //"resizable=yes," +
                     //"width=900," +
                     //"height=600," +
                     //"left = 0," +
                     //"top=0"
                 );
                 popUpObj1.focus();
                 //LoadModalDiv();
             }

         </script>
 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
 <script type="text/javascript">
     function ShowProgress() {
         setTimeout(function () {
             var modal = $('<div />');
             modal.addClass("modal");
             $('body').append(modal);
             var loading = $(".loading");
             loading.show();
             var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
             var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
             loading.css({ top: top, left: left });
         }, 200);
     }
     $('form').live("submit", function () {
         ShowProgress();
     });
 </script>
 <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
 <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
         $('#btnGo').click(function () {
             var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
             if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
             <%--var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
             if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
             var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
             if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
             var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
             if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }
             var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
             if (TYear == "---Select---") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }--%>

             var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
             <%--var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
             var FMonth = document.getElementById('<%=ddlFMonth.ClientID%>').value;
             var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
             var TMonth = document.getElementById('<%=ddlTMonth.ClientID%>').value;

             var frmYear = $('#<%=ddlFYear.ClientID%> :selected').text();
             var frmMonth = $('#ddlFMonth').find(":selected").index();
             var toYear = $('#<%=ddlTYear.ClientID%> :selected').text();
             var toMonth = $('#ddlTMonth').find(":selected").index();--%>

             var Day_Wise = 0;
             // Check Box
             if ($("#ChkDaywise").is(':checked')) {
                 Day_Wise = 1;
                 
             }
             if (Day_Wise == 1) {
                 
                 if ($('[id$=txtEffFrom]').val().length == 0) { alert("Select Effective From Date."); $('#txtEffFrom').focus(); return false; }
                 if ($('[id$=txtEffTo]').val().length == 0) { alert("Select Effective To Date."); $('#txtEffTo').focus(); return false; }

                 var FDate = document.getElementById('<%=txtEffFrom.ClientID%>').value;
                 var TDate = document.getElementById('<%=txtEffTo.ClientID%>').value;
                 showModalPopUpCheck(sf_Code, FDate, TDate, SName);
             }
             else {
                 
                 var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                 var frmMonth = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                     new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                 var frmYear = frmMonYear[1];

                 var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                 var toMonth = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                     new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                 var toYear = ToMonYear[1];

                 var mnth = frmMonth, yr = parseInt(frmYear), validate = '', tmp = '';
                 if ((frmMonth <= toMonth && parseInt(frmYear) === parseInt(toYear)) || (parseInt(frmYear) < parseInt(toYear) && (frmMonth <= toMonth || frmMonth >= toMonth))) {
                     showModalPopUp(sf_Code, frmMonth, frmYear, toMonth, toYear, SName);


                 }
                 else {
                     alert("Select Valid Month & Year...");
                     //$('#ddlFMonth').focus();
                     return false;
                 }
             }

         });
     });
 </script>
         
        <script type="text/javascript">
            // Function to toggle visibility based on checkbox status
            $(document).ready(function () {
                // Bind the change event to the checkbox
                $("#ChkDaywise").change(function () {
                    if ($(this).is(':checked')) {
                        $("#iddaywise").show();  // Show the div
                        $("#idMonyr").hide();
                        idMonyr
                    } else {
                        $("#iddaywise").hide();  // Hide the div
                        $("#idMonyr").show();
                    }
                });
            });
</script>
        <div>
            <div id="Divid" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <center>
                            <table>
                                <tr>
                                    <td align="center">
                                        <h2 class="text-center">Input Despatch - View</h2>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" CssClass="ddl"
                                    SkinID="ddlRequired">
                                    <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false" CssClass="ddl"
                                    OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false" CssClass="ddl">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix" id="idMonyr">
                                        <div style="float: left; width: 45%;">
                                        <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div style="float: right; width: 45%;">
                                        <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
                                <%--<div style="float: left; width: 45%;">
                                    <asp:Label ID="lblFMonth" runat="server" CssClass="label" Text="From Month"></asp:Label>
                                    <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired">
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
                                    <asp:Label ID="lblFYear" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                    <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired">
                                    </asp:DropDownList>
                                </div>--%>
                            </div>
                  <%--          <div class="single-des clearfix">
                                <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblTMonth" runat="server" CssClass="label" Text="To Month"></asp:Label>
                                    <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired">
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
                                    <asp:Label ID="lblTYear" runat="server" CssClass="label" Text="To Year"></asp:Label>
                                    <asp:DropDownList ID="ddlTYear" runat="server" SkinID="ddlRequired">
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
                        </div>
                        <div class="single-des clearfix">
                            <asp:CheckBox ID="ChkDaywise" Text="Datewise" runat="server" />
                        </div>
                        <div id="iddaywise" style="display:none;">
                         <div class="single-des clearfix">
                                    <asp:Label ID="lblEffFrom" runat="server" CssClass="label"> From Date<span style="Color:Red;padding-left:5px;"></span></asp:Label>
                                    <div id="dvEffc_Frm" class="row-fluid">
                                        <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input"
                                            onkeypress="Calendar_enter(event);" Width="100%"
                                            onblur="this.style.backgroundColor='White'" TabIndex="6"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffFrom" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
                  
                                    </div>
                          </div>
                          <div class="single-des clearfix">
                            <asp:Label ID="lblEffTo" runat="server" CssClass="label"> To Date<span style="Color:Red;padding-left:5px;"></span></asp:Label>
                            <div id="dvEffc_To" class="row-fluid">
                                <asp:TextBox ID="txtEffTo" runat="server" CssClass="input"
                                    onkeypress="Calendar_enter(event);" Width="100%"
                                    TabIndex="7" onblur="this.style.backgroundColor='White'" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEffTo" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
          
                                </div>
                            </div>
                            </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnGo" runat="server" Text="View" CssClass="savebutton" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

             <!-- Bootstrap Datepicker -->
        <script type="text/javascript" src="../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap-datepicker.js"></script>
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
</body>
</html>

