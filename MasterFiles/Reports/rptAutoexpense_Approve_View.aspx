<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptAutoexpense_Approve_View.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Statement Approval View</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" href="../../css/Report.css" rel="Stylesheet" />
    <%--<link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />--%>
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid;
            prtGrid.border = 1;
            var prtwin = window.open('', 'ModalPopUp');
        }

    </script>
     <script type="text/javascript">
        var popUpObj;

        function showModalPopUp(sfcode, divcode,fmon, fyr) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("RptAutoexpense_zoom.aspx?sf_code=" + sfcode + "&divCode=" + divcode + "&month=" + fmon + "&year=" + fyr + "&flg=" + 'MR',
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=800," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            // LoadModalDiv();
        }
    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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
            <%--$('#btnSF').click(function () {
                var Prod = $('#<%=ddlSubdiv.ClientID%> :selected').text();
                if (Prod == "---Select---") { alert("Select Salesforce Name."); $('#ddlSubdiv').focus(); return false; }
            });--%>
             $('#btnSF').click(function () {
      
            var Name = $('#<%=ddlSubdiv.ClientID%> :selected').text();
            if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlSubdiv').focus(); return false; }
            var Year = $('#<%=yearID.ClientID%> :selected').text();
            if (Year == "---Select---") { alert("Select Year."); $('#yearID').focus(); return false; }
            var Month = $('#<%=monthId.ClientID%> :selected').text();
            if (Month == "---Select---") { alert("Select Month."); $('#monthId').focus(); return false; }

            var sf_Code = document.getElementById('<%=ddlSubdiv.ClientID%>').value;
            var Year1 = document.getElementById('<%=yearID.ClientID%>').value;
            var Month1 = document.getElementById('<%=monthId.ClientID%>').value;

                var divcode ='<%=Session["div_code"]%>';
           // showModalPopUp(sf_Code,divcode, Month1, Year1);
		       var d = new Date();
                 var Curyear = d.getFullYear();
                 // Set it to one month ago               
                //var  perviousdate = d.setMonth(d.getMonth() - 1);               
                 var month = d.getMonth() + 1;
                 var day = d.getDate();
                 var dayNumb = day;              
                 var Currmonth = (month < 10 ? '' : '') + month;
                 if (Month1 == 10 || Month1 == 11 || Month1 == 12) { Currmonth = '0' + Currmonth }
                 //if (Currmonth == 01) { Currmonth = 13 }
               
                // if (Month1 == 12) { Curyear = Curyear - 1 } else { Curyear }
                 if (Year1 == Curyear) { //||((Month1 == 12) && Year1 == Curyear-1)
                   
                     if (Currmonth - 1 == Month1 && dayNumb <= 20) {
                         alert("Option Will Open On Every Month 21st"); return false;
                        
                     } else {
                        
                         if (Month1 <= Currmonth - 1) { showModalPopUp(sf_Code, divcode, Month1, Year1); }
                         else { alert("Option Will Open On Every Month 21st"); return false; }
                     }
                 } else { showModalPopUp(sf_Code, divcode, Month1, Year1); }




                 //if (Year1 == Curyear) {
              
                     //if (Currmonth - 1 == Month1 && dayNumb <= 20) {
                     //    alert("Option Will Open On Every Month 21st"); return false;

                     //} else {
                     //    if (Month1 <= Currmonth - 1) { showModalPopUp(sf_Code, divcode, Month1, Year1); }
                     //    else { alert("Option Will Open On Every Month 21st"); return false; }
                     //}
             //} 
             //else { showModalPopUp(sf_Code, divcode, Month1, Year1); }
        });

        });
    </script>
    <style type="text/css">
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
        <div>
            <div id="Divid" runat="server"></div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <center>
                            <table>
                                <tr>
                                    <td align="center">
                                        <h2 class="text-center">Auto Expense Statement</h2>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblSubdiv" runat="server" visible="true" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <asp:DropDownList ID="ddlSubdiv" runat="server" CssClass="custom-select2 nice-select" Width="100%"
                                    OnSelectedIndexChanged="ddlSubdiv_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMonth" runat="server" Text="Month " CssClass="label"></asp:Label>
                                <asp:DropDownList ID="monthId" runat="server" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblYr" runat="server" Text="Year " CssClass="label"></asp:Label>
                                <asp:DropDownList ID="yearID" runat="server" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnSF" runat="server" Text="Go" CssClass="savebutton" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <table align="right" style="margin-right: 5%">
                <tr>
                    <td align="right">
                        <asp:Panel ID="pnlprint" runat="server" Visible="false">
                            <input type="button" id="btnPrint" value="Print" style="width: 60px; height: 25px"
                                onclick="PrintGridData()" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
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
