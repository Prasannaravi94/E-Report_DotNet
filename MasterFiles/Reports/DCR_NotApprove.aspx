<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_NotApprove.aspx.cs" Inherits="Reports_DCR_NotApprove" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Not Approved DCR Dates</title>
    <style type="text/css">
        #tblDocRpt {
            margin-left: 300px;
        }
    </style>


    <%-- <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(fmon, fyr) {
            popUpObj = window.open("rptDCRNotApprove.aspx?cmon=" + fmon + "&cyear=" + fyr,
    "ModalPopUp"//,
   // "null," //+
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

                //  var ImgSrc = "../Images/loading/loading47.gif";

                var ImgTxT = "http://i.imgur.com/KUJoe.gifhttps://s10.postimg.org/g0v7h43wp/Text_1.gif";

                var ImgSrc = "https://s10.postimg.org/4i4mt6p3t/loading_23_ook.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red;margin-left:40%">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:300px; height: 235px;position: fixed;top: 10%;left: 35%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
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

                <%--var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
                var ddlYear = $('#<%=ddlYear.ClientID%> :selected').text();
                if (ddlYear == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }

                var ddlMonVal = document.getElementById('<%=ddlMonth.ClientID%>').value;
                var ddlYearVal = document.getElementById('<%=ddlYear.ClientID%>').value;--%>

                var ToMonYear = document.getElementById('<%=txtMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = ToMonYear[1];

                showModalPopUp(Month1, ddlYearVal)


            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Not Approved DCR Dates</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <%--<asp:Label ID="Label1" runat="server" CssClass="label" Text="From Range : "></asp:Label>--%>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="From Range : "></asp:Label>
                                        <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <%--                <div class="col-lg-6">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select">
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
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select" Width="100">
                                        </asp:DropDownList>
                                    </div>--%>
                                </div>

                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Go" OnClick="btnSubmit_Click" />
                            <%--     <asp:Button ID="btnClear" runat="server" CssClass="savebutton" Width="60px" Height="25px" Text="Clear"/>--%>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
        
        <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=txtMonthYear]').datepicker({
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
