<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDR_DetailAdd.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_ListedDR_DetailAdd" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor Detail Add</title>
    <%--  <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
    <%-- <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />--%>
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }


        .space {
            padding: 3px 3px;
        }

        .sp {
            padding-left: 11px;
        }

        .style6 {
            padding: 3px 3px;
            height: 28px;
        }

        .marRight {
            margin-right: 35px;
        }

        #chkPatientClass {
            width: 300px;
        }

        .blink_me {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 1s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 1s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 1s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <script type="text/javascript">
        $(function () {
            $("#FilUpImage").change(function () {
                $("#dvPreview").html("");
                var maxFileSize = 1048576
                var fileUpload = $('#FilUpImage');
                if (fileUpload[0].files[0].size < maxFileSize) {

                    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                    if (regex.test($(this).val().toLowerCase())) {
                        //if ($.browser.msie && parseFloat(jQuery.browser.version) <= 9.0) {
                        // $("#dvPreview").show();
                        // $("#dvPreview")[0].filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = $(this).val();
                        //}
                        //else {
                        if (typeof (FileReader) != "undefined") {
                            $("#dvPreview").show();
                            $("#dvPreview").append("<img />");
                            var reader = new FileReader();
                            reader.onload = function (e) {
                                $("#dvPreview img").attr("src", e.target.result);
                            }
                            reader.readAsDataURL($(this)[0].files[0]);
                        } else {
                            alert("This browser does not support FileReader.");
                            // }
                        }
                    } else {
                        alert("Please upload a valid image file.");
                        fileUpload.val('');
                        return false;
                    }
                } else {
                    alert('File size should be less than 1 MB')
                    fileUpload.val('');
                    return false;
                }
            });
        });
        </script>
     <script type="text/javascript">
        function ValidatePaste(event) {
        // Prevent pasting if non-numeric characters are included
        var clipboardData = (event.clipboardData || window.clipboardData);
        var pastedData = clipboardData.getData('Text');
        
        if (!/^\d*$/.test(pastedData)) {
            event.preventDefault();
        }
    }
    </script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
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
                            $('#btnSave').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSave').click(function () {
                if ($("#txtName").val() == "") { alert("Enter Doctor Name."); $('#txtName').focus(); return false; }
                var qual = $('#<%=ddlQual.ClientID%> :selected').text();
                if (qual == "---Select---") { alert("Select Qualification."); $('#ddlQual').focus(); return false; }
                var spec = $('#<%=ddlSpec.ClientID%> :selected').text();
                if (spec == "---Select---") { alert("Select Speciality."); $('#ddlSpec').focus(); return false; }
                var cat = $('#<%=ddlCatg.ClientID%> :selected').text();
                if (cat == "---Select---") { alert("Select Category."); $('#ddlCatg').focus(); return false; }
                var clas = $('#<%=ddlClass.ClientID%> :selected').text();
                var area = $('#<%=ddlTerritory.ClientID%> :selected').text();
                if (area == "---Select---") { alert("Select Area Cluster Name."); $('#ddlTerritory').focus(); return false; }
                if (clas == "---Select---") { alert("Select Class."); $('#ddlClass').focus(); return false; }
                if ($("#txtAddress1").val() == "") { alert("Enter Line-1."); $('#txtAddress1').focus(); return false; }
                if ($("#txtCity").val() == "") { alert("Enter City."); $('#txtCity').focus(); return false; }
                if ($("#txtPin").val() == "") { alert("Enter Pincode."); $('#txtPin').focus(); return false; }
                var p = /^\d{10}$/;
                var val = document.getElementById('txtMobile');
                if (val.value.length != 10) {
                    alert("Plz Enter 10 Digit Only!!");
                    $('#txtMobile').focus();
                    return false;
                }
                if ($("#txtMobile").val() == "") { alert("Enter Mobile."); $('#txtMobile').focus(); return false; }
                if ($("#txtAvg").val() == "") { alert("Please Enter Avg Patients."); $('#txtAvg').focus(); return false; }

                if ($("[id$=chkVisit]").find("input:checked").length > 0) {
                    return true;
                } else {
                    alert('Please select at Least One Day.');
                    return false;
                }

                var st = $('#<%=ddlState.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select State."); $('#ddlState').focus(); return false; }

            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>--%>
            <%--<ucl:Menu ID="menu1" runat="server" />--%>


            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <h2 class="text-center">Listed Doctor Detail Add</h2>
                        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="center">
                            <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                        </asp:Panel>
                        <br />
                        <center>
                            <span class="blink_me">
                                <asp:Label ID="lblFN" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="Red">While typing, Kindly avoid Special Character like " ' " \ < > "</asp:Label></span>
                        </center>
                        <br />
                        <table id="Table1" align="center" style="width: 100%;">
                            <tr>
                                <td align="left" style="background-color: #f4f8fa; color: #696d6e; height: 45px;">&nbsp
                                    <asp:Label ID="lblHead" runat="server" Text="Personal Profile"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <div class="row ">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix" style="padding-top: 30px">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblName" CssClass="label" runat="server">Name of Doctor :<span style="color: Red;padding-left:5px;">*</span> </asp:Label>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text=" DR."></asp:Label>

                                        <asp:TextBox ID="txtName" runat="server" Width="100%"
                                            onkeypress="AlphaNumeric_NoSpecialChars(event)" CssClass="input">
                                        </asp:TextBox>
                                    </div>

                                    <div class="single-des clearfix" style="padding-bottom: 15px">
                                        <asp:Label ID="lblGender" runat="server" CssClass="label">Sex<span style="color: Red;padding-left:5px;">*</span></asp:Label><br />
                                        <asp:RadioButtonList ID="rdoGender" runat="server" RepeatColumns="3">
                                            <asp:ListItem Value="M" Text=" Male " Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="F" Text=" Female"></asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblQual" runat="server" CssClass="label">Qualification<span style="color: Red;padding-left:5px;">*</span> </asp:Label>
                                        <asp:DropDownList ID="ddlQual" runat="server" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblSpec" runat="server" CssClass="label">Speciality<span style="color: Red;padding-left:5px;">*</span> </asp:Label>
                                        <asp:DropDownList ID="ddlSpec" runat="server" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblCatg" runat="server" CssClass="label">Category<span style="color: Red;padding-left:5px;">*</span> </asp:Label>
                                        <asp:DropDownList ID="ddlCatg" runat="server" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblCommunication" CssClass="label" runat="server" Text="Communication "></asp:Label><br />
                                        <asp:RadioButtonList ID="rdoCommunication" runat="server" RepeatColumns="3">
                                            <asp:ListItem Value="C" Text=" Clinic " Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="H" Text=" Hospital "></asp:ListItem>
                                            <asp:ListItem Value="R" Text=" Residence"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-5">
                                <div class="designation-area clearfix" style="padding-top: 35px">


                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblDOB" runat="server" Text="Date of Birth " CssClass="label"></asp:Label>
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="ddlDobDate" runat="server" CssClass="nice-select" Width="50">
                                                    <asp:ListItem Value="01" Text="DD"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                                    <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                    <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                    <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                    <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                    <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-lg-4">

                                                <asp:DropDownList ID="ddlDobMonth" runat="server" CssClass="nice-select" Width="50">
                                                    <asp:ListItem Value="01" Text="MM"></asp:ListItem>
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
                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="ddlDobYear" runat="server" CssClass="nice-select" Width="60">
                                                    <asp:ListItem Selected="True" Value="1900" Text="YYYY"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblDOW" runat="server" CssClass="label" Text="DOW "></asp:Label>
                                        <%-- <asp:TextBox ID="txtDOW" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'" ReadOnly="true"
                    onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>   
               <asp:CalendarExtender ID="Caldow" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDOW"> </asp:CalendarExtender>--%>
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="ddlDowDate" runat="server" CssClass="nice-select" Width="50">
                                                    <asp:ListItem Value="01" Text="DD"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                                    <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                    <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                    <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                    <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                    <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="ddlDowMonth" runat="server" CssClass="nice-select" Width="50">
                                                    <asp:ListItem Value="01" Text="MM"></asp:ListItem>
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

                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="ddlDowYear" runat="server" CssClass="nice-select" Width="60">
                                                    <asp:ListItem Selected="True" Value="1900" Text="YYYY"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblRegNo" runat="server" CssClass="label" Text="Registration No "></asp:Label>
                                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblTerritory" CssClass="label" runat="server">Territory<span style="color: Red;padding-left:2px;">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlTerritory" runat="server" CssClass="nice-select">
                                        </asp:DropDownList>
                                        <%--<asp:UpdatePanel ID="updatepanel1" runat="server">
                                            <ContentTemplate>--%>
                                        <asp:TextBox ID="txtTerritory" runat="server" SkinID="TxtBxAllowSymb" Width="200px"></asp:TextBox>
                                        <asp:HiddenField ID="hdnTerritoryId" runat="server"></asp:HiddenField>
                                        <asp:PopupControlExtender ID="txtTerritory_PopupControlExtender" runat="server" Enabled="True"
                                            ExtenderControlID="" TargetControlID="txtTerritory" PopupControlID="Panel2" OffsetY="22">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel2" runat="server" Height="116px" Width="200px" BorderStyle="Solid"
                                            BorderWidth="2px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                            Style="display: none">
                                            <div style="height: 17px; position: relative; background-color: #4682B4; text-transform: capitalize; width: 100%; float: left"
                                                align="right">
                                                <div class="closeLoginPanel">
                                                    <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                        title="Close">X</a>
                                                </div>
                                            </div>
                                            <asp:CheckBoxList ID="ChkTerritory" runat="server" Width="180px" DataTextField="Territory_Name"
                                                DataValueField="Territory_Code" AutoPostBack="True" OnSelectedIndexChanged="ChkTerritory_SelectedIndexChanged"
                                                onclick="checkAll(this);">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblClass" CssClass="label" runat="server">Class <span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlClass" runat="server" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>

                        </div>
                        <br />
                        <table align="center" style="width: 100%;">
                            <tr>
                                <td align="left" style="background-color: #f4f8fa; color: #696d6e; height: 45px;">&nbsp
                                    <asp:Label ID="lblHeadAddress" runat="server" Text=" Address  "></asp:Label>

                                </td>
                            </tr>
                        </table>
                        <br />
                        <div class="row ">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblAddress1" runat="server" CssClass="label">Line-1<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblStreet" runat="server" CssClass="label" Text="Line-2"></asp:Label>
                                        <asp:TextBox ID="txtStreet" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblCity" runat="server" CssClass="label">City<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblState" runat="server" CssClass="label">State<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblpan" runat="server" CssClass="label">Pan Card</asp:Label>
                                        <asp:TextBox ID="txtpancard" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblPin" runat="server" CssClass="label">PIN Code<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                        <asp:TextBox ID="txtPin" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblMobile" runat="server" CssClass="label">Mobile No<span style="Color:Red;padding-left:2px;">*</span><span style="Color:black;padding-left:2px;">(Enter 10 digit Mobile No)</span></asp:Label>
                                        <asp:TextBox ID="txtMobile" runat="server" onkeypress="CheckNumeric(event);"  onpaste="return ValidatePaste(event);" 
                                            CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblTel" runat="server" CssClass="label" Text="Tel No"></asp:Label>
                                        <asp:TextBox ID="txtTel" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblEMail" runat="server" CssClass="label" Text="Email ID "></asp:Label>
                                        <asp:TextBox ID="txtEMail" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <table align="center" style="width: 100%;">
                            <tr>
                                <td align="left" style="background-color: #f4f8fa; color: #696d6e; height: 45px;">&nbsp
                                    <asp:Label ID="lblHeadingPractice" runat="server" Text=" Practice Profile"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div class="row" style="scrollbar-width: thin; overflow-x: auto;">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-11 single-des clearfix">
                                <asp:Label ID="lblwork" runat="server" CssClass="label" Text="Working Place"></asp:Label>

                                <asp:RadioButtonList ID="rdoProfile" runat="server" RepeatDirection="Horizontal"
                                    Width="650px">
                                    <asp:ListItem Value="1" Text=" Private Nursing Home " Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text=" Govt College "></asp:ListItem>
                                    <asp:ListItem Value="3" Text=" Private Institution / Corporate Hospital "></asp:ListItem>
                                    <asp:ListItem Value="4" Text=" Private Practice"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <div class="row" style="scrollbar-width: thin; overflow-x: auto;">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-11 single-des clearfix">
                                <asp:Label ID="lblVisit" runat="server" CssClass="label" Text="Visiting Days "></asp:Label><span style="Color:Red;padding-left:2px;">*</span>
                                <asp:CheckBoxList ID="chkVisit" runat="server" RepeatColumns="7" RepeatDirection="Horizontal"
                                    CellPadding="5" CellSpacing="5">
                                    <asp:ListItem Value="0">&nbsp;Sunday&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="1">&nbsp;Monday&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="2">&nbsp;Tuesday&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="3">&nbsp;Wednesday&nbsp;&nbsp; </asp:ListItem>
                                    <asp:ListItem Value="4">&nbsp;Thursday&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="5">&nbsp;Friday&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="6">&nbsp;Saturday</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </div>

                        <div class="row ">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblIUI" runat="server" CssClass="label" Text="IUI Cycle"></asp:Label>
                                        <asp:TextBox ID="txtIUICycle" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblAvg" runat="server" CssClass="label" Text="Average Number of Patients Per Day (Approx.) "></asp:Label><span style="Color:Red;padding-left:2px;">*</span>
                                        <asp:TextBox ID="txtAvg" runat="server" CssClass="input"
                                            Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblHosAddr" runat="server" CssClass="label" Text="Hospital Address"></asp:Label>
                                        <asp:TextBox ID="txtHosAddress" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblDayTime" runat="server" CssClass="label" Text="Time of meeting"></asp:Label>
                                        <asp:TextBox ID="txtDayTime" runat="server" CssClass="input" Width="100%" TextMode="Time" format="HH:mm"></asp:TextBox>
                                        <%-- <asp:Label ID="lblDayTime" runat="server" CssClass="label" Text="Day & Time of meeting"></asp:Label>
                                        <asp:TextBox ID="txtDayTime" runat="server" CssClass="input" Width="100%"></asp:TextBox>--%>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblHospital" runat="server" CssClass="label" Text="Hospital Name"></asp:Label>
                                        <asp:TextBox ID="txtHospital" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblERp" runat="server" CssClass="label" Text="Doctor ERP Code"></asp:Label>
                                        <asp:TextBox ID="txtERPcode" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="scrollbar-width: thin; overflow-x: auto; padding-bottom: 10px;">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-11 single-des clearfix">
                                <asp:Label ID="lblPatientClass" runat="server" CssClass="label" Text="Class of Patients"></asp:Label>
                                <asp:CheckBoxList ID="chkPatientClass" runat="server" RepeatDirection="Horizontal"
                                    RepeatColumns="4">
                                    <asp:ListItem Value="1">Higher</asp:ListItem>
                                    <asp:ListItem Value="2">Upper Middle</asp:ListItem>
                                    <asp:ListItem Value="3">Middle</asp:ListItem>
                                    <asp:ListItem Value="4">Lower</asp:ListItem>
                                </asp:CheckBoxList>

                            </div>
                        </div>

                        <div class="row" style="scrollbar-width: thin; overflow-x: auto; padding-bottom: 10px;">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-11 single-des clearfix">

                                <asp:Label ID="lblConsultation" runat="server" CssClass="label" Text="Consultation Fees"></asp:Label>
                                <asp:RadioButtonList ID="rdoFees" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Text=" > Rs.500"></asp:ListItem>
                                    <asp:ListItem Value="2" Text=" Rs.200 to 500"></asp:ListItem>
                                    <asp:ListItem Value="3" Text=" < Rs.200"></asp:ListItem>
                                </asp:RadioButtonList>

                            </div>
                        </div>
                        <center>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="savebutton" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="resetbutton" />
                        </center>
                    </div>

                    <asp:Button ID="btnBack" CssClass="backbutton" Text="Back" runat="server" OnClick="btnBack_Click" />

                </div>
                <br />
                <center>
                    <div id="visitid" class="row justify-content-center" visible="false" runat="server" style="scrollbar-width: thin; overflow-x: auto; padding-bottom: 10px;">
                        <div class="roundbox boxshadow" style="width: 600px; border: solid 2px steelblue; display: inline-block">
                            <div class="gridheaderleft" style="color: white; font-weight: bold; font-family: Arial; background-image: linear-gradient(to top, rgb(0, 119, 255) 0%, rgb(40, 181, 224) 100%)">
                                <center>Upload Visiting Card<span style="Color:Red;padding-left:2px;">*</span></center>
                            </div>
                            <div class="boxcontenttext">
                                <div id="pnlPreviewSurveyData">
                                    <br />
                                    <%--    <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" BackColor="LightBlue" OnClick="Upload" />
                                    --%>
                                    <%--   <asp:TemplateField HeaderText="Upload" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>--%>
                                    <asp:FileUpload ID="FilUpImage" runat="server" Font-Size="14px" />
                                    <div id="dvPreview" width="250px"></div>
                                    <asp:Button ID="bt_upload" runat="server" EnableViewState="False"
                                        CssClass="resetbutton" Text="Upload" OnClick="bt_upload_OnClick" />
                                    <asp:DataList ID="DataList1" runat="server" HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Image ID="imgHome" ImageUrl='<%# Eval("Visiting_Card") %>' Width="200px" ImageAlign="Top"
                                                    runat="server" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                        </div>
                    </div>
                </center>

            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
