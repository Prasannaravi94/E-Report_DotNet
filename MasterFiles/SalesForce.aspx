<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesForce.aspx.cs" Inherits="MasterFiles_SalesForce" MaintainScrollPositionOnPostback="true"%>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Force Master</title>
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <style type="text/css">
        .div_fixed {
            position: fixed;
            top: 400px;
            right: 5px;
        }

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

        .chkboxLocation label {
            padding-left: 5px;
        }

        .height {
            height: 20px;
        }
    </style>
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
    <script type="text/javascript" language="javascript">
        window.onload = function () {
            noBack();
        }
        function noBack() {
            window.history.forward();
        }
    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">

        function Vacant() {
            if (confirm('Do you want to Vacant this user?')) {
                if (confirm('Are you sure?')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        function Promote() {
            if (confirm('Do you want to Promote this user?')) {
                if (confirm('Are you sure?')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        function BaseLevelPromote() {
            if (confirm('Do you want to Promote this user?')) {
                return true;
            }

            else {
                return false;
            }
        }
        function DePromote() {
            if (confirm('Do you want to De-Promote this user?')) {
                if (confirm('Are you sure?')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function Block() {
            if (confirm('Do you want to Block this user?')) {
                if (confirm('Are you sure?')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

    </script>
    <style type="text/css">
        .TEXTAREA {
            margin-left: 0px;
        }

        .style9 {
            height: 27px;
            width: 173px;
        }

        .style26 {
            width: 173px;
        }

        .style38 {
            width: 173px;
            height: 31px;
        }

        #tblpanel {
            height: 14px;
            width: 77%;
            margin-left: 0px;
        }

        .style57 {
            width: 620px;
        }

        .Radio {
            margin-left: 0px;
        }

        #Table1 {
            width: 78%;
            margin-left: 0px;
        }

        .style65 {
            width: 236px;
            height: 45px;
        }

        .style66 {
            height: 31px;
            width: 219px;
        }

        .style67 {
            width: 219px;
        }

        .style68 {
            height: 27px;
            width: 219px;
        }

        .style70 {
            width: 350px;
        }

        .style71 {
            width: 270px;
        }

        .style72 {
            height: 31px;
            width: 1367px;
        }

        .style73 {
            width: 1367px;
        }

        .style75 {
            width: 169px;
        }

        .style76 {
            height: 27px;
            width: 1588px;
        }

        .style77 {
            height: 27px;
            width: 1367px;
        }

        .style78 {
            height: 19px;
        }

        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
            margin-left: 10px;
            overflow-y: scroll;
            height: 200px;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Black;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }

        #divwidth {
            width: 150px !important;
        }

            #divwidth div {
                width: 150px !important;
            }
    </style>

          <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />
</head>
<body>
   <script type="text/javascript">
       $(document).ready(function () {
           $("#btnSubmit").click(function () {
               var hdnrejoin_mode = $("#hdnrejoin_mode").val();

               if (hdnrejoin_mode == "active_mode") {
                   var str1 = $("#hdnname_check").val();
                   var str2 = document.getElementById("txtFieldForceName").value;

                   if (str2.indexOf(str1) != -1) {
                       alert("Same Employee colud not be activate..kindly rejoin");
                       return false;
                   }
               }
           });
       });
</script>


<script type="text/javascript">
    $(document).ready(function () {
        $("#btnSave").click(function () {
            var hdnrejoin_mode = $("#hdnrejoin_mode").val();

            if (hdnrejoin_mode == "active_mode") {
                var str1 = $("#hdnname_check").val();
                var str2 = document.getElementById("txtFieldForceName").value;

                if (str2.indexOf(str1) != -1) {
                    alert("Same Employee colud not be activate..kindly rejoin");
                    return false;
                }
            }
        });
    });
</script>

<script type = "text/javascript">
    function validateRadio() {
        var RB1 = document.getElementById("<%=rdoMode.ClientID%>");
        var radio = RB1.getElementsByTagName("input");
        var isChecked = false;
        for (var i = 0; i < radio.length; i++) {
            if (radio[i].checked) {
                isChecked = true;
                break;
            }
        }
        if (!isChecked) {
            alert("Select Mode");
        }
        return isChecked;
    }
</script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$('input:text:first').focus();
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
            $('#btnSubmit').click(function () {

                if ($("#txtFieldForceName").val() == "") { alert("Enter FieldForce Name."); $('#txtFieldForceName').focus(); return false; }
                var type = $('#<%=ddlFieldForceType.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select Type."); $('#ddlFieldForceType').focus(); return false; }
                var sta = $('#<%=ddlState.ClientID%> :selected').text();
                if (sta == "---Select---") { alert("Select State."); $('#ddlState').focus(); return false; }
                if ($("#txtHQ").val() == "") { alert("Enter HQ."); $('#txtHQ').focus(); return false; }
                var type = $('#<%=ddlReporting.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Reporting To."); $('#ddlReporting').focus(); return false; }
                var Des = $('#<%=txtDesignation.ClientID%> :selected').text();
                if (Des == "---Select---") { alert("Select Designation."); $('#txtDesignation').focus(); return false; }
                if ($("#txtUserName").val() == "") { alert("Enter User Name."); $('#txtUserName').focus(); return false; }
                if ($("#txtPassword").val() == "") { alert("Enter Password."); $('#txtPassword').focus(); return false; }
                if ($("#txtJoingingDate").val() == "") { alert("Enter Joining Date."); $('#txtJoingingDate').focus(); return false; }
                if ($("#txtTPDCRStartDate").val() == "") { alert("Enter Reporting Start Date."); $('#txtTPDCRStartDate').focus(); return false; }
                if ($("#txtEmployeeID").val() == "") { alert("Enter Employee ID."); $('#txtEmployeeID').focus(); return false; }
                if ($("#txtReason").val() == "") { alert("Enter Reason for Blocking/Holding the ID."); $('#txtReason').focus(); return false; }
                if ($("#txteffe").val() == "") { alert("Enter Effective Date."); $('#txteffe').focus(); return false; }
                if ($("#UsrDfd_UserName").val() == "") { alert("Enter UserDefined UserName."); $('#UsrDfd_UserName').focus(); return false; }
                var category = $('#<%=ddlcatg.ClientID%> :selected').text();
                if (category == "---Select---") { alert("Select Category."); $('#ddlcatg').focus(); return false; }


                var ddlfieldtype = $('#<%=ddlfieldtype.ClientID%> :selected').text();
                if (ddlfieldtype == "---Select---") { alert("Select Fieldforce Type."); $('#ddlfieldtype').focus(); return false; }



                if ($('#chkboxLocation input:checked').length > 0) { return true; } else { alert('Select Subdivision'); return false; }

           });
            $('#btnSave').click(function () {

                if ($("#txtFieldForceName").val() == "") { alert("Enter FieldForce Name."); $('#txtFieldForceName').focus(); return false; }
                var type = $('#<%=ddlFieldForceType.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select Type."); $('#ddlFieldForceType').focus(); return false; }
                var sta = $('#<%=ddlState.ClientID%> :selected').text();
                if (sta == "---Select---") { alert("Select State."); $('#ddlState').focus(); return false; }
                if ($("#txtHQ").val() == "") { alert("Enter HQ."); $('#txtHQ').focus(); return false; }
                var type = $('#<%=ddlReporting.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Reporting To."); $('#ddlReporting').focus(); return false; }
                var Des = $('#<%=txtDesignation.ClientID%> :selected').text();
                if (Des == "---Select---") { alert("Select Designation."); $('#txtDesignation').focus(); return false; }
                if ($("#txtUserName").val() == "") { alert("Enter User Name."); $('#txtUserName').focus(); return false; }
                if ($("#txtPassword").val() == "") { alert("Enter Password."); $('#txtPassword').focus(); return false; }
                if ($("#txtJoingingDate").val() == "") { alert("Enter Joining Date."); $('#txtJoingingDate').focus(); return false; }
                if ($("#txtTPDCRStartDate").val() == "") { alert("Enter Reporting Start Date."); $('#txtTPDCRStartDate').focus(); return false; }
                if ($("#txtEmployeeID").val() == "") { alert("Enter Employee ID."); $('#txtEmployeeID').focus(); return false; }
                if ($("#txtReason").val() == "") { alert("Enter Reason for Blocking/Holding the ID."); $('#txtReason').focus(); return false; }
                if ($("#txteffe").val() == "") { alert("Enter Effective Date."); $('#txteffe').focus(); return false; }
                if ($("#UsrDfd_UserName").val() == "") { alert("Enter UserDefined UserName."); $('#UsrDfd_UserName').focus(); return false; }
                var category = $('#<%=ddlcatg.ClientID%> :selected').text();
                if (category == "---Select---") { alert("Select Category."); $('#ddlcatg').focus(); return false; }

                var ddlfieldtype = $('#<%=ddlfieldtype.ClientID%> :selected').text();
                if (ddlfieldtype == "---Select---") { alert("Select Fieldforce Type."); $('#ddlfieldtype').focus(); return false; }



                if ($('#chkboxLocation input:checked').length > 0) { return true; } else { alert('Select Subdivision'); return false; }

            });
        });
    </script>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">

                    <div class="col-lg-11">

                        <h2 class="text-center">FieldForce Master</h2>

                        <table cellpadding="0" cellspacing="0" id="Table2" align="center" style="width: 100%;">
                            <tr>
                                <td rowspan="" class="style65" align="left" style="background-color: #f4f8fa; color: #696d6e">&nbsp; Login & Joining Details&nbsp;
                                </td>
                            </tr>
                        </table>
                        <br />

                        <%--      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                        <div class="row ">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <%--<asp:Label ID="lblFieldForceName" runat="server" SkinID="lblMand"
                                                         Width="120px"><span style="Color:Red">*</span>FieldForce Name</asp:Label>--%>
                                        <span runat="server" id="levelset" class="label">FieldForce N<asp:LinkButton ID="lnk" Font-Underline="false" runat="server" class="label"
                                            Text="a" OnClick="lnk_Click"></asp:LinkButton>me</span>  <span style="color: Red; font-size: 12px">*</span>
                                        <asp:TextBox ID="txtFieldForceName" runat="server" MaxLength="50" Width="100%"
                                            TabIndex="1" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                        <asp:HiddenField ID="hdnname_check" runat="server" />
                                        <asp:HiddenField ID="hdnrejoin_mode" runat="server" />
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblStateName" runat="server" CssClass="label">State Name<span style="color: Red;padding-left:5px;">*</span></asp:Label><br />
                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" MaxLength="15" Width="100%"
                                            CssClass="nice-select width custom-select2" TabIndex="3" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblDesignation" runat="server" CssClass="label">Designation<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                        <%--      <asp:TextBox ID="txtDesignation" runat="server" SkinID="TxtBxNumOnly" MaxLength="100"
                                                         onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'" 
                                                            Width="162px" TabIndex="6" CssClass="TEXTAREA"  onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>--%>
                                        <asp:DropDownList ID="txtDesignation" runat="server" AutoPostBack="true" MaxLength="15" Width="100%"
                                            CssClass="nice-select custom-select2" TabIndex="5" OnSelectedIndexChanged="txtDesignation_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblUserName" runat="server" CssClass="label">Unique Code<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                        <asp:TextBox ID="txtUserName" runat="server" MaxLength="100" Width="100%"
                                            TabIndex="7" CssClass="input " onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblJoingingDate" runat="server" CssClass="label">Joining Date<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                        <asp:TextBox ID="txtJoingingDate" runat="server" CssClass="input"
                                            onkeypress="Calendar_enterBa(event);" TabIndex="9" Width="100%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtJoingingDate" CssClass=" cal_Theme1" />
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblEmployeeID" runat="server" CssClass="label">Employee ID<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                        <asp:TextBox ID="txtEmployeeID" runat="server" CssClass="input" MaxLength="10"
                                            Width="100%" TabIndex="11" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblfieldtype" runat="server" CssClass="label"><span style="Color:Red"></span>Fieldforce Type</asp:Label>
                                        <asp:DropDownList ID="ddlfieldtype" runat="server"
                                            CssClass="nice-select">
                                            <asp:ListItem Selected="True" Text="---Select---" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Trainee" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Probation" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Confirmed" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblLastDCRDate" runat="server" Visible="False"
                                            CssClass="label" ForeColor="#0033CC">Last DCR Date<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                        <%--      <asp:TextBox ID="txtDCRDate" runat="server" Height="18px" onblur="this.style.backgroundColor='White'"
                                                            onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="MandTxtBox" Visible="false"
                                                            Width="125px"></asp:TextBox>
                                                           <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDCRDate" />  --%>
                                        <br />
                                        <asp:Label ID="txtDCRDate" runat="server" CssClass="label" Font-Bold="true" Visible="false"></asp:Label>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblReason" runat="server" Text="Reason for Blocking the ID:" CssClass="label"
                                            Visible="False" ForeColor="Red"></asp:Label>
                                        <asp:TextBox ID="txtReason" runat="server" Visible="false" MaxLength="300" TabIndex="14"
                                            Placeholder="Type Here"
                                            TextMode="MultiLine" Width="100%" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lbleffective" runat="server" Visible="false" CssClass="label">Effective Date<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                        <asp:TextBox ID="txteffe" runat="server" Visible="false"
                                            onkeypress="Calendar_enterBa(event);" CssClass="input" TabIndex="9"
                                            Width="100%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" runat="server" TargetControlID="txteffe" CssClass="cal_Theme1" />
                                    </div>


                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblmode" runat="server" CssClass="label" Visible="false" ForeColor="Red" Text="Mode "></asp:Label>
                                        <asp:RadioButtonList ID="rdoMode" runat="server" RepeatDirection="Horizontal" Visible="false"
                                            AutoPostBack="true" OnSelectedIndexChanged="rdoMode_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="New Id&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Replacement"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                </div>

                            </div>

                            <div class="col-lg-5" style="padding-top: 10px">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <div class="single-des-option">
                                            <asp:Label ID="lblFieldForceType" runat="server" CssClass="label">Type<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlFieldForceType" runat="server" AutoPostBack="true" CssClass="nice-select "
                                                TabIndex="2" OnSelectedIndexChanged="ddlFieldForceType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Text="--Select--" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Base Level" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Manager" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblHQ" runat="server" CssClass="label">HQ<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                        <asp:TextBox ID="txtHQ" runat="server" Width="100%" MaxLength="30"
                                            TabIndex="4" CssClass="input" onkeypress="AlphaNumeric_NoSpecialCharshq(event);"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="AutoCompleteAjaxRequest"
                                            ServicePath="~/MasterFiles/MR/ListedDoctor/Webservice/SalesHQ.asmx" MinimumPrefixLength="1"
                                            CompletionInterval="100" EnableCaching="false"
                                            CompletionSetCount="10" CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList" CompletionListElementID="divwidth"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                            TargetControlID="txtHQ" FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>
                                    </div>

                                    <div class="single-des clearfix">
                                        <%--<asp:Label ID="lblReporting" runat="server" CssClass="label">Reporting To<span style="color: Red;padding-left:5px;">*</span></asp:Label>--%>
                                          <span runat="server" id="lblReporting" class="label" >Rep<asp:LinkButton ID="LinkButton1" ForeColor="#696d6e" Font-Underline="false" runat="server"
                                            Text="o" OnClick="Reportlnk_Click"></asp:LinkButton>rting To</span><span style="color: Red;padding-left:5px;font-size:12px;">*</span>
                                        <asp:DropDownList ID="ddlReporting" runat="server" AutoPostBack="true"
                                             CssClass="nice-select custom-select2" TabIndex="6" Width="100%">
                                            <%--  <asp:ListItem Selected="true" Value="">---Select Mgr---</asp:ListItem> --%>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblPassword" runat="server" CssClass="label">Password<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="15" TextMode="Password"
                                            Width="100%" TabIndex="8" CssClass="input" onkeypress="AlphaNumeric(event);"></asp:TextBox>
                                    </div>

                                    <div class="single-des clearfix">
                                        <%--<asp:Label ID="lblTPDCRStartDate" runat="server" SkinID="lblMand"
                                                            Width="140px"><span style="Color:Red">*</span>Report Starting Date</asp:Label>--%>
                                        <asp:Label ID="lblTPDCRStartDate" runat="server" CssClass="label">Report St<asp:LinkButton CssClass="label"
                                            ID="lnkdate" runat="server"
                                            Text="a" OnClick="lnkdate_Click"></asp:LinkButton>rt Date<span style="color: Red; font-size: 12px; padding-left: 5px;">*</span></asp:Label>
                                        <div style="margin-top: -5px;">
                                            <asp:TextBox ID="txtTPDCRStartDate" runat="server"
                                                onkeypress="Calendar_enterBa(event);" TabIndex="10" CssClass="input"
                                                Width="100%"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" runat="server" TargetControlID="txtTPDCRStartDate" CssClass=" cal_Theme1" />
                                        </div>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblUI" runat="server" CssClass="label">UserName:<span style="Color:Red; padding-left: 5px;">*</span></asp:Label>
                                        <asp:TextBox ID="UsrDfd_UserName" runat="server" MaxLength="100" Width="100%"
                                            TabIndex="15" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblcategory" runat="server" CssClass="label"><span style="Color:Red"></span>Category</asp:Label>

                                        <asp:DropDownList ID="ddlcatg" runat="server" CssClass="nice-select">
                                            <asp:ListItem Selected="True" Text="---Select---" Value="0"></asp:ListItem>
                                            <%--<asp:ListItem Text="Metro" Value="M"></asp:ListItem>--%>
                                             <asp:ListItem Text="Mega-Metro" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Non-Metro" Value="N"></asp:ListItem>
                                           <%-- <asp:ListItem Text="Semi-Metro" Value="S"></asp:ListItem>--%>
                                             <asp:ListItem Text="Metro" Value="S"></asp:ListItem>

                                        </asp:DropDownList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="Label7" runat="server" CssClass="label">Status<span style="color: Red;padding-left:5px;">*</span></asp:Label><br />
                                        <asp:RadioButtonList ID="RblSta" runat="server" RepeatColumns="3" TabIndex="12">
                                            <asp:ListItem Value="0" Selected="True">&nbsp;Active </asp:ListItem>
                                            <asp:ListItem Value="1">&nbsp;Vacant</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;Block</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>



                                    <div class="single-des clearfix" style="padding-top: 13px">
                                        <asp:Label ID="lblVacantBlock" runat="server" Text="Vacant Block" Visible="false" CssClass="label"></asp:Label>
                                        <asp:RadioButtonList ID="rblVacantBlock" runat="server" Visible="false" Enabled="false" RepeatColumns="2" TabIndex="13">
                                            <asp:ListItem Value="R" Selected="True">Resigned</asp:ListItem>
                                            <asp:ListItem Value="H">Hold</asp:ListItem>
                                            <%--     <asp:ListItem Value="P">Promotion</asp:ListItem>--%>
                                            <%--    <asp:ListItem Value="T">Transfered</asp:ListItem> --%>
                                            <%--     <asp:ListItem Value="D">Depromotion</asp:ListItem> --%>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lbloldusername" runat="server" Text="&nbsp;Old ID" CssClass="label" Visible="false"></asp:Label>
                                        <asp:Label ID="lbluserdefi" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </div>
                                    <div class="single-des clearfix">
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblreplace" runat="server" Text="Replacement For (Only Vacant Manager ID's)" Font-Bold="true" CssClass="label" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlrepla" runat="server" Visible="false"
                                            CssClass="nice-select" TabIndex="6">
                                            <%--  <asp:ListItem Selected="true" Value="">---Select Mgr---</asp:ListItem> --%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                        </div>



                        <%--      </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <br />
                        <div class="row ">
                            <table border="0" cellpadding="0" cellspacing="0" id="Table1" align="center" style="width: 100%;">
                                <tr>
                                    <td rowspan="" class="style65" align="left" style="background-color: #f4f8fa; color: #696d6e">&nbsp; Sub Division&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 54%; margin-bottom: 0px; margin-right: 0px; margin-top: 15px;">
                                <tr>
                                    <td>
                                        <asp:CheckBoxList ID="chkboxLocation" runat="server" DataTextField="subdivision_name"
                                            DataValueField="subdivision_code" RepeatColumns="4"
                                            RepeatDirection="vertical" Width="753px" TabIndex="29">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <br />
                        <div class="row">
                            <table cellpadding="0" cellspacing="0" id="Table3" align="center" style="width: 100%;">
                                <tr>
                                    <td rowspan="" class="style65" align="left" style="background-color: #f4f8fa; color: #696d6e">&nbsp; Contact Details&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div class="row ">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblDOB" runat="server" Text="DOB" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtDOB" runat="server"
                                            onkeypress="Calendar_enter(event);" CssClass="input" TabIndex="16"
                                            Width="100%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDOB" CssClass="cal_Theme1" />
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblPhone1" runat="server" Text="Phone No" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtPhone1" runat="server" MaxLength="12"
                                            Width="100%" TabIndex="18" CssClass="input"
                                            onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblEMail" runat="server" Text="EMail" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtEMail" runat="server" Width="100%"
                                            TabIndex="20" CssClass="input"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblDOW" runat="server" Text="DOW" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtDOW" runat="server" CssClass="input" Width="100%"
                                            onkeypress="Calendar_enter(event);" TabIndex="17"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDOW" CssClass="cal_Theme1" />
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblMobile" runat="server" Text="Mobile No" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtMobile" runat="server" Width="100%" TabIndex="19" MaxLength="12"
                                            CssClass="input" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <br />
                        <div class="row justify-content-center">
                            <asp:Label ID="Label3" runat="server" Text="Present Address" ForeColor="#696d6e"></asp:Label>
                        </div>
                        <br />
                        <div class="row ">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblAddress1" runat="server" Text="Address1" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtAddress1" runat="server" MaxLength="150"
                                            Width="100%" TabIndex="21" CssClass="input" onkeypress="AlphaNumeric(event);"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblCityPin" runat="server" Text="City / Pincode" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtCityPin" runat="server" MaxLength="50"
                                            Width="100%" TabIndex="23" CssClass="input"
                                            onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblAddress2" runat="server" Text="Address2" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtAddress2" runat="server" MaxLength="150"
                                            Width="100%" TabIndex="22" CssClass="input" onkeypress="AlphaNumeric(event);"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblPhone2" runat="server" Text="Phone No" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtPhone2" runat="server" Width="100%" TabIndex="24" MaxLength="12"
                                            CssClass="input" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <br />
                        <div class="row justify-content-center">
                            <asp:Label ID="Label2" runat="server" Text="Permanent Address"
                                ForeColor="#696d6e"></asp:Label>
                        </div>
                        <br />
                        <div class="row ">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblPerAddress1" runat="server" Text="Address1" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtPerAddress1" runat="server" MaxLength="150"
                                            Width="100%" TabIndex="25" CssClass="input" onkeypress="AlphaNumeric(event);"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblPerCityPin" runat="server" Text="City / PinCode" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtPerCityPin" runat="server" MaxLength="50"
                                            Width="100%" TabIndex="27" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblPerAddress2" runat="server" Text="Address2" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtPerAddress2" runat="server" MaxLength="150"
                                            Width="100%" TabIndex="26" CssClass="input" onkeypress="AlphaNumeric(event);"></asp:TextBox>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblPhone" runat="server" Text="Phone No" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtPhone" runat="server" MaxLength="12"
                                            Width="100%" TabIndex="28" CssClass="input"
                                            onkeypress="CheckNumeric(event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>



                         <br />
                        <div class="row justify-content-center">
                            <asp:Label ID="Label1" runat="server" Text="Bank Details"
                                ForeColor="#696d6e"></asp:Label>
                        </div>
                        <br />
                        <div class="row ">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblBankName" runat="server" Text="Bank Name" CssClass="label"></asp:Label>
                                       <%-- <asp:TextBox ID="txtBankName" runat="server" MaxLength="150"
                                            Width="100%" TabIndex="29" CssClass="input" onkeypress="AlphaNumeric(event);"></asp:TextBox>--%>
                                          <asp:DropDownList ID="ddlBankName" runat="server"  CssClass="nice-select"
                                                TabIndex="29" >
                                               <asp:ListItem  Text="--Select--" Value=""></asp:ListItem>
                                               <asp:ListItem Selected="True" Text="Cheque" Value="Cheque"></asp:ListItem>
                                                <asp:ListItem Text="SBI Bank" Value="SBI"></asp:ListItem>
                                                <asp:ListItem Text="ICICI Bank" Value="ICICI"></asp:ListItem>
                                               <asp:ListItem Text="HDFC Bank" Value="HDFC"></asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblIFSC" runat="server" Text="IFSC Code" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtIFSC" runat="server" MaxLength="50"
                                            Width="100%" TabIndex="31" CssClass="input" onkeypress="AlphaNumeric(event);"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblAcountNo" runat="server" Text="Bank Account No" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtAcountNo" runat="server" MaxLength="50"
                                            Width="100%" TabIndex="30" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>

                                        
                                    </div>
                                   <%-- <div class="single-des clearfix">
                                        <asp:Label ID="Label8" runat="server" Text="Phone No" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="TextBox4" runat="server" MaxLength="12"
                                            Width="100%" TabIndex="28" CssClass="input"
                                            onkeypress="CheckNumeric(event);"></asp:TextBox>
                                    </div>--%>
                                </div>
                            </div>
                        </div>


                        <%--    <table border="0" cellpadding="0" cellspacing="0" id="Table1" align="center" style="width: 67%;
           ">
            <tr>
                <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white;">
                    &nbsp; Sub Division&nbsp;
                </td>
            </tr>
        </table>
       
        <table align="center" border="1" cellpadding="0" cellspacing="0" style="width: 54%;
            margin-bottom: 0px; margin-right: 0px; margin-top:15px;">
            <tr>
                <td class="style71" align="left">
                    <asp:CheckBoxList ID="chkboxLocation" runat="server" DataTextField="subdivision_name" CssClass="chkboxLocation"  
                        DataValueField="subdivision_code" Font-Names="Verdana" Font-Bold ="true" ForeColor ="BlueViolet"    Font-Size="X-Small" RepeatColumns="4"
                        RepeatDirection="vertical" Width="753px" TabIndex="29">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>--%>
                        <br />
                        <center>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validateRadio()"
                                TabIndex="26" CssClass="savebutton" />
                        </center>

                    </div>

                    <asp:Button ID="btnback" runat="server" CssClass="backbutton" Text="Back" OnClick="btnback_Click" />
                    <div class="div_fixed">
                        <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Text="Submit" OnClick="btnSave_Click" OnClientClick="return validateRadio()" />
                    </div>
                </div>
            </div>
            <br />
            <br />

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
         <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>
