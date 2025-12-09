<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Leave_Status_Datewise.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Leave_Status_Datewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Attendance Status</title>
    

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

        function showModalPopUp(sfcode, FDate, TDate, sf_name, Vacant) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("Leave_Status_Datewise_View.aspx?sf_code=" + sfcode + "&FDate=" + FDate + "&TDate=" + TDate + " &sf_name=" + sf_name + "&Vacant=" + Vacant,
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
                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";
                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";
                var ImgSrc = "http://s11.postimg.org/47q6jab8j/konlang.gif"

                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:500px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
        }
    </script>
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
<body>

   
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

                var ddlFieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (ddlFieldForce == "--Select--") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }

                if ($('[id$=txtEffFrom]').val().length == 0)
                { alert("Select Effective From Date."); $('#txtEffFrom').focus(); return false; }
                if ($('[id$=txtEffTo]').val().length == 0)
                { alert("Select Effective To Date."); $('#txtEffTo').focus(); return false; }


                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var FDate = document.getElementById('<%=txtEffFrom.ClientID%>').value;
                var TDate = document.getElementById('<%=txtEffTo.ClientID%>').value;
                var Checkvacant = document.getElementById("chkvacant");

                if (Checkvacant.checked) {
                    showModalPopUp(sf_Code, FDate, TDate, Name, 1);
                }
                else {
                    showModalPopUp(sf_Code, FDate, TDate, Name, 0);
                }
               // showModalPopUp(sf_Code, FDate, TDate, Name);
            });
        });
    </script>


    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <div class="row justify-content-center ">
                            <div class="col-lg-5">
                                <center>
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <h2 class="text-center">Attendance Status</h2>
                                            </td>
                                        </tr>
                                    </table>
                                </center>

                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                            <%--     <asp:ListItem Selected="True">---Select---</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblEffFrom" runat="server" CssClass="label"> From Date<span style="Color:Red;padding-left:5px;"></span></asp:Label>
                                        <div id="dvEffc_Frm" class="row-fluid">
                                            <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input"
                                                onkeypress="Calendar_enter(event);" Width="100%"
                                                onblur="this.style.backgroundColor='White'" TabIndex="6"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffFrom" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
                                            <%--<asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom"
                                            runat="server" />--%>
                                        </div>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblEffTo" runat="server" CssClass="label"> To Date<span style="Color:Red;padding-left:5px;"></span></asp:Label>
                                        <div id="dvEffc_To" class="row-fluid">
                                            <asp:TextBox ID="txtEffTo" runat="server" CssClass="input"
                                                onkeypress="Calendar_enter(event);" Width="100%"
                                                TabIndex="7" onblur="this.style.backgroundColor='White'" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEffTo" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
                                            <%--   <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEffTo"
                                    runat="server" />--%>
                                        </div>
                                    </div>

                                </div>
                                 <div class="single-des clearfix">
                                    <asp:CheckBox ID="chkvacant" runat="server" Text="With Vacant"></asp:CheckBox>
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>

        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
