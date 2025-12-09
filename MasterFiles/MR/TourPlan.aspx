<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TourPlan.aspx.cs" Inherits="MasterFiles_MR_TourPlan" EnableEventValidation="false" %>

<%--<%@ Register Src="~/UserControl/MGR_TP_Menu.ascx" TagName="Menu1" TagPrefix="m1" %>
<%@ Register Src="~/UserControl/MR_TP_Menu.ascx" TagName="Menu2" TagPrefix="m2" %>--%>
<%--<%@ Register Src="~/UserControl/MenuUserControl_TP.ascx" TagName="Menu3" TagPrefix="m3" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP Entry</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/Grid.css" />--%>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <link type="text/css" rel="stylesheet" href="../../css/MR.css" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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

        .c1 {
            width: 280px;
            height: 240px;
        }

        .c1 {
            width: 280px;
            height: 240px;
        }

        .modalDialog {
            position: fixed;
            font-family: Arial, Helvetica, sans-serif;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            background: rgba(0, 0, 0, 0.8);
            z-index: 99999;
            opacity: 0;
            -webkit-transition: opacity 400ms ease-in;
            -moz-transition: opacity 400ms ease-in;
            transition: opacity 400ms ease-in;
            pointer-events: none;
        }

            .modalDialog:target {
                opacity: 1;
                pointer-events: auto;
            }

            .modalDialog > div {
                width: 400px;
                position: relative;
                margin: 10% auto;
                padding: 5px 20px 13px 20px;
                border-radius: 10px;
                background: #fff;
                background: -moz-linear-gradient(#fff, #999);
                background: -webkit-linear-gradient(#fff, #999);
                background: -o-linear-gradient(#fff, #999);
            }

        .close {
            background: #606061;
            color: #FFFFFF;
            line-height: 25px;
            position: absolute;
            right: -12px;
            text-align: center;
            top: -10px;
            width: 24px;
            text-decoration: none;
            font-weight: bold;
            -webkit-border-radius: 12px;
            -moz-border-radius: 12px;
            border-radius: 12px;
            -moz-box-shadow: 1px 1px 3px #000;
            -webkit-box-shadow: 1px 1px 3px #000;
            box-shadow: 1px 1px 3px #000;
        }

            .close:hover {
                background: #00d9ff;
            }

        .p {
            font-family: Calibri;
            font-size: 14px;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .gridheight {
            overflow-y: auto !important;
            height: 500px !important;
        }

        .dropdownlist1 {
            background-position: 184px;
        }

        .DropDown {
            background-color: yellow;
        }

        .display-table .table td {
            padding: 5px 20px;
        }

        .home-section-main-body {
            padding: 5px;
        }

        #grdTP .dropdownlist {
            background-position: 250px;
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

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <link href="../../css/stylesheet.css" rel="stylesheet" type="text/css" />
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>


</head>
<body class="bodycolor">

    <script type="text/javascript">
        function ValidateEmptyValue() {
            var grid = document.getElementById('<%= grdTP.ClientID %>');
            if (grid != null) {

                var isEmpty = false;
                var Inputs = grid.getElementsByTagName("input");
                var Incre = Inputs.length;
                var cnt = 0;
                var index = '';

                for (i = 2; i < Incre; i++) {
                    if (Inputs[i].type != '') {

                        if (Inputs[i].type == 'text') {
                            if (i.toString().length == 1) {
                                index = cnt.toString() + i.toString();
                            }
                            else {
                                index = i.toString();
                            }

                            var ddlWT = document.getElementById('grdTP_ctl' + index + '_ddlWT');
                            var ddlTerr = document.getElementById('grdTP_ctl' + index + '_ddlTerr');
                            var drpDownListValue = ddlWT.options[ddlWT.selectedIndex].innerHTML;

                            if (ddlWT.value == '0') {
                                //isEmpty = true;
                                alert('Select Work Type')
                                ddlWT.focus();
                                return false;
                            }
                            if (drpDownListValue == 'Field Work') {
                                //if (ddlTerr.value == '0') {
                                if (ddlTerr.value == '-1') {
                                    alert('Select Territory')
                                    ddlTerr.focus();
                                    return false;
                                }
                            }

                        }
                    }
                }

                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do You want to Submit?")) {
                    confirm_value.value = "Yes";
                }
                else {
                    confirm_value.value = "No";
                    return false;
                }
                document.forms[0].appendChild(confirm_value);

            }
        }

        function ValidateEmptyValueApprove() {
            var grid = document.getElementById('<%= grdTP.ClientID %>');
            if (grid != null) {

                var isEmpty = false;
                var Inputs = grid.getElementsByTagName("input");
                var Incre = Inputs.length;
                var cnt = 0;
                var index = '';

                for (i = 2; i < Incre; i++) {
                    if (Inputs[i].type != '') {

                        if (Inputs[i].type == 'text') {
                            if (i.toString().length == 1) {
                                index = cnt.toString() + i.toString();
                            }
                            else {
                                index = i.toString();
                            }

                            var ddlWT = document.getElementById('grdTP_ctl' + index + '_ddlWT');
                            var ddlTerr = document.getElementById('grdTP_ctl' + index + '_ddlTerr');
                            var drpDownListValue = ddlWT.options[ddlWT.selectedIndex].innerHTML;

                            if (ddlWT.value == '0') {
                                //isEmpty = true;
                                alert('Select Work Type')
                                ddlWT.focus();
                                return false;
                            }
                            if (drpDownListValue == 'Field Work') {
                                if (ddlTerr.value == '0') {
                                    alert('Select Territory')
                                    ddlTerr.focus();
                                    return false;
                                }
                            }

                        }
                    }
                }

                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to Approve?")) {
                    confirm_value.value = "Yes";
                }
                else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);

            }
        }
    </script>

    <script type="text/javascript">
        function ClearValidate() {

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Clear?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
                return false;
            }
            document.forms[0].appendChild(confirm_value);
        }
        function ToggleOnOff(ddonoff) {

            var grid = document.getElementById('<%=grdWorkType.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (i = 2; i < grid.rows.length + 1; i++) {
                        var cnt = 0;
                        var index = '';

                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }

                        var code = document.getElementById('grdWorkType_ctl' + index + '_lblCode');
                        var place = document.getElementById('grdWorkType_ctl' + index + '_lblplace');
                        var fwd = document.getElementById('grdWorkType_ctl' + index + '_lblFWInd');
                        if ($(ddonoff).val() == code.innerHTML) {
                            if (place.innerHTML == "N") {
                                $(ddonoff)
                                    .parent()//td
                                    .parent()//tr
                                    .find('select[id*="ddlTerr"]')//select by id
                                    .css('backgroundColor', 'LightGray')
                                    .val('0')
                                    .attr('title', "Disabled!!")
                                    .attr('disabled', true); //disable-enable
                                break;
                            }
                            else if (fwd.innerHTML != "F") {
                                $(ddonoff)
                                    .parent()//td
                                    .parent()//tr
                                    .find('select[id*="ddlTerr"]')//select by id
                                    .css('backgroundColor', 'white')
                                    .attr('title', "Select")
                                    .attr('disabled', false);  //disable-enable     
                            }
                            else {
                                $(ddonoff)
                                    .parent()//td
                                    .parent()//tr
                                    .find('select[id*="ddlTerr"]')//select by id
                                    .css('backgroundColor', 'white')
                                    .attr('title', "Select")
                                    .attr('disabled', false)  //disable-enable  
                                    .empty();
                                $('#ddlAllTerr option').clone().appendTo($(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr"]'));
                            }
                        }
                    }
                }
            }

        }
        function ToggleOnOff1(ddonoff) {
            var grid = document.getElementById('<%=grdWorkType.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (i = 2; i < grid.rows.length + 1; i++) {
                        var cnt = 0;
                        var index = '';

                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }

                        var code = document.getElementById('grdWorkType_ctl' + index + '_lblCode');
                        var place = document.getElementById('grdWorkType_ctl' + index + '_lblplace');
                        var fwd = document.getElementById('grdWorkType_ctl' + index + '_lblFWInd');
                        if ($(ddonoff).val() == code.innerHTML) {
                            if (place.innerHTML == "N") {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr1"]')//select by id
                                .css('backgroundColor', 'LightGray')
                                .val('0')
                                .attr('title', "Disabled!!")
                                .attr('disabled', true); //disable-enable
                                break;
                            }
                            else if (fwd.innerHTML != "F") {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr1"]')//select by id
                                .css('backgroundColor', 'white')
                                .attr('title', "Select")
                                .attr('disabled', false);  //disable-enable     
                            }
                            else {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr1"]')//select by id
                                .css('backgroundColor', 'white')
                                .attr('title', "Select")
                                .attr('disabled', false)  //disable-enable  
                                .empty();
                                $('#ddlAllTerr option').clone().appendTo($(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr1"]'));
                            }
                        }
                    }
                }
            }

        }
        function ToggleOnOff2(ddonoff) {
            var grid = document.getElementById('<%=grdWorkType.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (i = 2; i < grid.rows.length + 1; i++) {
                        var cnt = 0;
                        var index = '';

                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }

                        var code = document.getElementById('grdWorkType_ctl' + index + '_lblCode');
                        var place = document.getElementById('grdWorkType_ctl' + index + '_lblplace');
                        var fwd = document.getElementById('grdWorkType_ctl' + index + '_lblFWInd');
                        if ($(ddonoff).val() == code.innerHTML) {
                            if (place.innerHTML == "N") {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr2"]')//select by id
                                .css('backgroundColor', 'LightGray')
                                .val('0')
                                .attr('title', "Disabled!!")
                                .attr('disabled', true); //disable-enable
                                break;
                            }
                            else if (fwd.innerHTML != "F") {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr2"]')//select by id
                                .css('backgroundColor', 'white')
                                .attr('title', "Select")
                                .attr('disabled', false);  //disable-enable     
                            }
                            else {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr2"]')//select by id
                                .css('backgroundColor', 'white')
                                .attr('title', "Select")
                                .attr('disabled', false)  //disable-enable  
                                .empty();
                                $('#ddlAllTerr option').clone().appendTo($(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr2"]'));
                            }
                        }
                    }
                }
            }

        }
    </script>


    <script type="text/javascript">
        function DraftValidateEmptyValue() {
            var grid = document.getElementById('<%= grdTP.ClientID %>');
            if (grid != null) {

                var isEmpty = false;
                var Inputs = grid.getElementsByTagName("input");
                var Incre = Inputs.length;
                var cnt = 0;
                var index = '';


                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to Save as a Draft ?")) {
                    confirm_value.value = "Yes";
                }
                else {
                    confirm_value.value = "No";
                    return false;

                }
                document.forms[0].appendChild(confirm_value);

            }
        }
    </script>
    <script type="text/javascript">
        function getInternetExplorerVersion() {
            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE");
            var rv = -1;

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer, return version number
            {

                if (isNaN(parseInt(ua.substring(msie + 5, ua.indexOf(".", msie))))) {
                    //For IE 11 >
                    if (navigator.appName == 'Netscape') {
                        var ua = navigator.userAgent;
                        var re = new RegExp("Trident/.*rv:([0-9]{1,}[\.0-9]{0,})");
                        if (re.exec(ua) != null) {
                            rv = parseFloat(RegExp.$1);
                            // alert(rv);

                        }
                    }
                    else {
                        // alert('otherbrowser');
                    }
                }
                else {
                    //For < IE11
                    // alert(parseInt(ua.substring(msie + 5, ua.indexOf(".", msie))));
                    //alert("IE");
                    $("#LnkClose").css("background-color", "White");
                }
                return false;
            }
            else {
                // alert("chrome");

                $("#LnkClose").css("right", 473);
                $("#LnkClose").css("top", 120);

            }
        }
    </script>
    <form id="form1" runat="server">
        <br />
        <asp:Panel ID="tpmsg" runat="server" Visible="false">
            <div class="container home-section-main-body position-relative clearfix" style="width: 1330px; max-width: 1350px !important;">
                <div class="row justify-content-center" style="height: 500px">
                    <div class="col-lg-12">
                        <asp:Panel runat="server"  Width="100%">
                        <asp:Label runat="server" Text="Tour Plan has been locked...contact Admin " Font-Size="20pt"
                            ForeColor="Red" Font-Names="Verdana"></asp:Label>
                        <asp:Button ID="Button1" Width="80px" style="margin-left:500px;" runat="server" Text="Back" Font-Bold="true" ForeColor="Red" Font-Size="15px" PostBackUrl="~/Sales_DashBoard_Admin_Brand.aspx" />

                        </asp:Panel>      
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="tppnl">
            <div class="container home-section-main-body position-relative clearfix" style="width: 1330px; max-width: 1350px !important;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div style="margin-left: 90%">
                            <asp:Button ID="btnBack" runat="server" CssClass="savebutton" Text="Back" OnClick="btnBack_Click" />
                        </div>
                        <div>
                            <%--    <m1:Menu1 ID = "menu1" runat = "server" />
    <m2:Menu2 ID ="menu2" runat ="server" />--%>
                            <%--<m3:Menu3 ID ="menu3" runat ="server" />--%>
                            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                            </asp:ToolkitScriptManager>
                            <center>
                                <table id="tblMargin" runat="server" width="100%">
                                    <tr>
                                        <td>
                                            <div>
                                                <section style="display: block">
                                                            <ol id="ExampleList">   
                                                                <li><a href="#NCP_Lightbox" onclick="getInternetExplorerVersion()"><img src="../../Images/help_animated.gif" alt="" /></a></li>  
                                                            </ol>
                                                            <div class="ncp-popup ncp-popup-overlay" id="NCP_Lightbox">
	                                                            <div class="ncp-popup-spacer">
		                                                            <a href="#" id="LnkClose" class="ncp-popup-close">X</a>
		                                                            <div class="ncp-popup-container">			
			                                                            <div class="ncp-popup-content">
				                                                            <h2 style="color: Red; font-weight: bold;font-family: Arial, Helvetica, sans-serif;">TP - Entry / Edit</h2>
                                                                            <p class="p">
                                                                                1. Fill Your "TP" for all days and Press "Send to Manager Approval" Button for Manager
                                                                                Approval.
                                                                            </p>
                                                                            <p>
                                                                                2. After Approval From Your Manager, then next Month "TP" will open.
                                                                            </p>
                                                                            <p>
                                                                                3. After Selecting the "Field Work" , the Area will appear for Selection for the
                                                                                Particular Day. Whichever having the Doctors, those area only will appear.
                                                                            </p>
                                                                            <p>
                                                                                4. "Without Doctor Areas" - will not reflect in your TP- Entry.
                                                                            </p>
                                                                            <p>
                                                                                5. For Other Worktypes, not Possible to Select the Areas. The "Selection box" will
                                                                                be in "Disable" Mode.
                                                                            </p>
                                                                            <p>
                                                                                6. Before Approval from your Manager, You can Edit your TP for the Particular Month.
                                                                            </p>
                                                                            <p>
                                                                                7. After Approval from your Manager, the Fieldforce cannot Edit their TP. Get the
                                                                                Permission from "Admin", then the Fiedlforce can Edit their "TP" for the required
                                                                                month.
                                                                            </p>
			                                                            </div>
		                                                            </div>
	                                                            </div>
                                                            </div>
                                                        </section>
                                            </div>
                                        </td>
                                        <%-- </tr>
                                <tr valign="bottom">--%>
                                        <td colspan="5" align="center">
                                            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                                                <center>
                                                    <h2 class="text-center">
                                                        <asp:Label ID="lblHead" runat="server" Text="Tour Plan for the Month of "></asp:Label>
                                                        <asp:Label ID="lblmon" runat="server"></asp:Label>
                                                    </h2>
                                                </center>
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnLogin" Width="80px" runat="server" Text="LogOut" Font-Bold="true" ForeColor="Red" Font-Size="15px" PostBackUrl="~/Index.aspx" />
                                            <asp:Button ID="btnBack1" Width="80px" runat="server" Text="Back" Font-Bold="true" ForeColor="Red" Font-Size="15px" OnClick="btnBack_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" align="center">
                                            <asp:Label ID="lblLink" runat="server" Font-Size="Small" Font-Names="Verdana"
                                                ForeColor="Black"></asp:Label>
                                            <asp:HyperLink ID="hylEdit" runat="server" NavigateUrl="~/MasterFiles/MR/TourPlan.aspx?Edit=E"
                                                Font-Size="Small" Font-Names="Verdana" ForeColor="Blue"></asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDeactivate" Font-Bold="true" ForeColor="Red" Font-Names="Verdana" Visible="false" Font-Size="8" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <%-- <br />--%>
                                <table align="center">
                                    <asp:Panel ID="Panel1" runat="server" Style="text-align: left;">
                                        <asp:Label ID="lblReason" runat="server" Style="text-align: left" Font-Size="Small"
                                            Font-Names="Verdana" Visible="false"></asp:Label>
                                    </asp:Panel>
                                    <asp:BalloonPopupExtender ID="BalloonPopupExtender2" TargetControlID="lblNote" BalloonPopupControlID="Panel1"
                                        runat="server" Position="TopLeft" DisplayOnMouseOver="true" BalloonSize="Small">
                                    </asp:BalloonPopupExtender>
                                    <tr>

                                        <td align="right">
                                            <asp:Label ID="lblStatingDate" Visible="false" Font-Names="Verdana" runat="server"></asp:Label>
                                            &nbsp;&nbsp
                        <asp:Label ID="lblNote" runat="server" Style="text-decoration: underline;" ForeColor="Red"
                            Font-Size="Small" Font-Names="Verdana" Text="Rejection Reason" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblFieldForce" SkinID="lblMand" runat="server" Font-Size="Small" Font-Names="Verdana" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlAllTerr" runat="server" Style="display: none;"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <div class="designation-reactivation-table-area clearfix" style="width: 1300px">
                                                <div class="display-table clearfix">
                                                    <div class="table-responsive ">
                                                        <%--gridheight--%>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="grdTP" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                                    GridLines="None" CssClass="table" OnRowDataBound="grdTP_RowDataBound" AlternatingRowStyle-CssClass="alt">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("TP_Date") %>'></asp:Label>
                                                                                <%--Width="50px"--%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDay" runat="server" Text='<%#  Eval("TP_Day") %>'></asp:Label>
                                                                                <%--Width="60px"--%>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>

                                                                                <asp:DropDownList ID="ddlWT" runat="server" CssClass="dropdownlist" DataSource="<%# FillWorkType() %>"
                                                                                    DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" onchange="ToggleOnOff(this)">
                                                                                    <%--Width="100px"--%>
                                                                                </asp:DropDownList>

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Route Plan 1" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlTerr" Width="200px" runat="server" CssClass="dropdownlist1" DataSource="<%# FillTerritory() %>"
                                                                                    DataTextField="Territory_Name" DataValueField="Territory_Code" Enabled="false">
                                                                                </asp:DropDownList>

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlWT1" runat="server" CssClass="dropdownlist" Width="100px" DataSource="<%# FillWorkType() %>"
                                                                                    DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" onchange="ToggleOnOff1(this)">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Route Plan 2" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlTerr1" Width="200px" runat="server" CssClass="dropdownlist1" DataSource="<%# FillTerritory() %>"
                                                                                    DataTextField="Territory_Name" DataValueField="Territory_Code" Enabled="false">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlWT2" runat="server" CssClass="dropdownlist" Width="100px" DataSource="<%# FillWorkType() %>"
                                                                                    DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" onchange="ToggleOnOff2(this)">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Route Plan 3" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlTerr2" Width="200px" runat="server" CssClass="dropdownlist1" DataSource="<%# FillTerritory() %>"
                                                                                    DataTextField="Territory_Name" DataValueField="Territory_Code" Enabled="false">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Objective" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtObjective" runat="server" CssClass="textbox1" Width="200" Height="27">                                           
                                                                                </asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnClear" CssClass="savebutton" runat="server" Text="Clear"
                                                OnClick="btnClear_Click" OnClientClick="return ClearValidate()" Visible="false" />
                                            &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSave" CssClass="savebutton" runat="server" Text="Draft Save" Visible="false"
                            OnClick="btnSave_Click" OnClientClick="return DraftValidateEmptyValue()" />
                                            &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" CssClass="savebutton" Width="200px" runat="server" Text="Send to Manager Approval"
                            OnClick="btnSubmit_Click" OnClientClick="return ValidateEmptyValue()" />


                                        </td>
                                    </tr>
                                </table>
                            </center>

                            <div style="margin-left: 40%">
                                <asp:Button ID="btnApprove" CssClass="savebutton" runat="server" Visible="false" Text="Approve TP" OnClick="btnApprove_Click"
                                    OnClientClick="return ValidateEmptyValueApprove()" />
                                &nbsp
            
            <asp:Button ID="btnReject" CssClass="savebutton" runat="server" Visible="false" Text="Reject TP" OnClick="btnReject_Click" />

                                &nbsp
            <asp:Label ID="lblRejectReason" Text="Reject Reason : " Visible="false" SkinID="lblMand" runat="server"></asp:Label>
                                &nbsp
            <asp:TextBox ID="txtReason" Width="400" Height="45" Visible="false" TextMode="MultiLine"
                runat="server"></asp:TextBox>
                                &nbsp
            <asp:Button ID="btnSendBack" CssClass="savebutton" Width="140px" runat="server" Visible="false"
                Text="Send for ReEntry" OnClick="btnSendBack_Click" />
                            </div>
                            <asp:GridView ID="grdWorkType" runat="server" Width="100%" HorizontalAlign="Center"
                                AlternatingRowStyle-CssClass="alt" GridLines="None" CssClass="mGridDCR" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="WorkType_Code" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"WorkType_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="But_Access" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblplace" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"Place_Involved") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="fw " HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFWInd" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"FieldWork_Indicator") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <div class="loading" align="center">
                                Loading. Please wait.<br />
                                <br />
                                <img src="../../Images/loader.gif" alt="" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
