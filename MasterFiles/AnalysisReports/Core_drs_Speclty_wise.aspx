<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Core_drs_Speclty_wise.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Core_drs_Speclty_wise" %>


<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MVD Coverage</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <link href="../../css/Font-Awesome-4.7.0/css/font-awesome.css" rel="stylesheet" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script language="javascript" type="text/javascript">
        function showModalPopUp(sfcode, sf_name, txtEffFrom, txtEffTo, cbTxt, cbValue) {

            popUpObj = window.open("Rpt_Core_drs_Speclty_wise.aspx?sf_code=" + sfcode + "&sf_name=" + sf_name + "&cbVal=" + cbValue + "&cbTxt=" + cbTxt + "&txtEffFrom=" + txtEffFrom + "&txtEffTo=" + txtEffTo,
                   "ModalPopUp",
                   "toolbar=no," +
                   "scrollbars=yes," +
                   "location=no," +
                   "statusbar=no," +
                   "menubar=no," +
                   "addressbar=no," +
                   "resizable=yes," +
                   "width=900," +
                   "height=600," +
                   "left = 0," +
                   "top=0"
                   );
            popUpObj.focus();

            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s4.postimg.org/j1heaetwt/loading16.gif"
                $(popUpObj.document.body).append('<div><p style="color:blue;margin-top:10%;margin-left:40%;">Loading Please Wait....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style="width:310px;height:300px;position:fixed;top:20%;left:30%;"  alt="" /></div>');
            });
        }
    </script>

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
                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                if ($('[id$=txtEffFrom]').val().length == 0)
                { alert("Select Effective From Date."); $('#txtEffFrom').focus(); return false; }

                if ($('[id$=txtEffTo]').val().length == 0)
                { alert("Select Effective To Date."); $('#txtEffTo').focus(); return false; }

                var txtEffFrom = document.getElementById('<%=txtEffFrom.ClientID%>').value;
                var txtEffTo = document.getElementById('<%=txtEffTo.ClientID%>').value;

                var iCount = 0;
                var CHK = document.getElementById("<%=cbSpeciality.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");
                var label = CHK.getElementsByTagName("label");
                for (var i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].checked) {
                        iCount++;
                    }
                }
                if (iCount === 0) {
                    alert("Select valid Checkbox...");
                    return false;
                }

                var cbValue = "";
                var cbTxt = "";
                
                var CHK = document.getElementById("<%=cbSpeciality.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    //alert("Selected = " + label[i].innerHTML); 
                    var value = checkbox[i].value;
                    cbTxt += label[i].innerHTML + ",";
                    //cbValue += value + ",";
                    //alert(cbValue);
                }
            }
            var checked_checkboxes = $("[id*=cbSpeciality] input:checked");
            var message = "";
            checked_checkboxes.each(function () {
                //var value = $(this).parent().attr('cbValue');
                //var text = $(this).closest("td").find("label").html();
                //message += "Text: " + text + " Value: " + value;
                //message += "\n";
                cbValue += $(this).parent().attr('cbValue') + ",";
            });
                //alert(message);
            showModalPopUp(sf_Code, Name, txtEffFrom, txtEffTo, cbTxt, cbValue);
            });
        });
    </script>

    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">

                    <div class="col-lg-5">

                        <h2 class="text-center">MVD Coverage</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false" CssClass="ddl">
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

                            <div>
                                <asp:Label ID="lblMode" CssClass="label" Text="Select Speciality : " runat="server" />
                                <asp:CheckBoxList runat="server" ID="cbSpeciality" RepeatDirection="Horizontal" CellSpacing="20"
                                    RepeatColumns="7">
                                </asp:CheckBoxList>
                            </div>

                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Width="50px" Text="View" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>--%>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
