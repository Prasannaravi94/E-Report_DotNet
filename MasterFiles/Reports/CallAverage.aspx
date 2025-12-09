<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CallAverage.aspx.cs" Inherits="Reports_CallAverage" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Call Average</title>

    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <link rel="stylesheet" href="../../assets/css/Calender_CheckBox.css" type="text/css" />
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    	<link href="../../../assets/css/select2.min.css" rel="stylesheet" />
	<link href="../../assets/css/select2.min.css" rel="stylesheet" />
	<link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
       
    
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
     <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css"/>
        <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="//cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#ddlFieldForce").select2();
        });
        </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, To_Month, To_Year, sf_name, div_Code, ChkMgr, checkVacant, Mode) {

            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptCallAverage.aspx?sf_code=" + sfcode + "&frm_month=" + fmon + "&frm_year=" + fyr + "&To_Month=" + To_Month + "&To_Year=" + To_Year + "&sf_name=" + sf_name + "&div_Code=" + div_Code + "&ChkMgr=" + ChkMgr + "&chkVacant=" + checkVacant + "&Mode=" + Mode,
                "ModalPopUp"
                //"toolbar=no," +
                //"scrollbars=yes," +
                //"location=no," +
                //"statusbar=no," +
                //"menubar=no," +
                //"addressbar=no," +
                //"resizable=yes," +
                //"fullscreen=yes,"+
                ////"width=800," +
                ////"height=600," +
                //"left = 0," +
                //"top=0"
            );
            popUpObj.focus();
            // LoadModalDiv();

        }


    </script>
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
    <%--    <script type="text/javascript">

        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>--%>
</head>
<body>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp1(sfcode, fmon, fyr, To_Month, To_Year, sf_name, div_Code, ChkMgr, checkVacant, Mode) {
            //alert('popup1');
            var cbTxt = "";
            var cbValue = "";
            var CHK = document.getElementById("<%=cbSpeciality.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    //alert("Selected = " + label[i].innerHTML);                    
                    cbTxt += label[i].innerHTML + ",";
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
            //             //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            //         popUpObj = window.open("rptCallAverage.aspx?sf_code=" + sfcode + "&frm_month=" + fmon + "&frm_year=" + fyr + "&To_Month=" + To_Month + "&To_Year=" + To_Year + "&sf_name=" + sf_name + "&div_Code=" + div_Code + "&ChkMgr=" + ChkMgr + "&chkVacant=" + checkVacant + "&cbtxt=" + cbValue + "&Mode=" + Mode,
            //"ModalPopUp"
            ////"toolbar=no," +
            ////"scrollbars=yes," +
            ////"location=no," +
            ////"statusbar=no," +
            ////"menubar=no," +
            ////"addressbar=no," +
            ////"resizable=yes," +
            ////"fullscreen=yes,"+
            //////"width=800," +
            //////"height=600," +
            ////"left = 0," +
            ////"top=0"
            //);
            //         popUpObj.focus();
            //         // LoadModalDiv();
            //         return false;

            var tURL = 'rptCallAverage.aspx?sf_code=' + sfcode + '&frm_month=' + fmon + '&frm_year=' + fyr + '&To_Month=' + To_Month + '&To_Year=' + To_Year + '&sf_name=' + sf_name + '&div_Code=' + div_Code + '&ChkMgr=' + ChkMgr + '&chkVacant=' + checkVacant + '&cbtxt=' + cbValue + '&Mode=' + Mode;
            var x = screen.width;
            var y = screen.height;
            x = x - 250;
            y = y - 200;
            newsarticle = eval('window.open (tURL,"Result","location=no,memubar=no,Height=' + y + ',Width=' + x + ',resizable=no,scrollbars=yes,titlebar=no,toolbar=no,Left=0,top=0")');
            newsarticle.focus();
            return false;



        }
        </script>
  <script type="text/javascript">
      var popUpObj;
      function showModalPopUp_Half(sfcode, fmon, fyr, To_Month, To_Year, sf_name, div_Code, ChkMgr, checkVacant, Mode) {

          //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
          popUpObj = window.open("rptCallAverage_Halfday.aspx?sf_code=" + sfcode + "&frm_month=" + fmon + "&frm_year=" + fyr + "&To_Month=" + To_Month + "&To_Year=" + To_Year + "&sf_name=" + sf_name + "&div_Code=" + div_Code + "&ChkMgr=" + ChkMgr + "&chkVacant=" + checkVacant + "&Mode=" + Mode,
              "ModalPopUp"
              //"toolbar=no," +
              //"scrollbars=yes," +
              //"location=no," +
              //"statusbar=no," +
              //"menubar=no," +
              //"addressbar=no," +
              //"resizable=yes," +
              //"fullscreen=yes,"+
              ////"width=800," +
              ////"height=600," +
              //"left = 0," +
              //"top=0"
          );
          popUpObj.focus();
          // LoadModalDiv();

      }


    </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp1_Half(sfcode, fmon, fyr, To_Month, To_Year, sf_name, div_Code, ChkMgr, checkVacant, Mode) {
            //alert('popup1');
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptCallAverage_Halfday.aspx?sf_code=" + sfcode + "&frm_month=" + fmon + "&frm_year=" + fyr + "&To_Month=" + To_Month + "&To_Year=" + To_Year + "&sf_name=" + sf_name + "&div_Code=" + div_Code + "&ChkMgr=" + ChkMgr + "&chkVacant=" + checkVacant + "&Mode=" + Mode,
                "ModalPopUp"
                //"toolbar=no," +
                //"scrollbars=yes," +
                //"location=no," +
                //"statusbar=no," +
                //"menubar=no," +
                //"addressbar=no," +
                //"resizable=yes," +
                //"fullscreen=yes,"+
                ////"width=800," +
                ////"height=600," +
                //"left = 0," +
                //"top=0"
            );
            popUpObj.focus();
            // LoadModalDiv();

        }
        </script>
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

                var ddlDivisionName = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (ddlDivisionName == "--Select--") { alert("Select Division."); $('#ddlDivision').focus(); return false; }
                var grp = $('#<%=ddlOption.ClientID%> :selected').text();
                if (grp == "---Select---") { alert("Select Mode."); $('#ddlOption').focus(); return false; }
                var grpFieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (grpFieldForce == "---Select---" || grpFieldForce == "---Select Clear---") { alert("Select Field Force."); $('#ddlFieldForce').focus(); return false; }
            <%--var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
            if (Month == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
            var Year = $('#<%=ddlYear.ClientID%> :selected').text();
            if (Year == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                var FrmMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                if (FrmMonth == "---Select---") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }
                var FrmYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
            if (FrmYear == "---Select---") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
            var ToMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
            if (ToMonth == "---Select---") { alert("Select To Month."); $('#ddlToMonth').focus(); return false; }
            var ToMonth = $('#<%=ddlToYear.ClientID%> :selected').text();
            if (ToMonth == "---Select---") { alert("Select To Year."); $('#ddlToYear').focus(); return false; }--%>
                if (grp == "MonthWise") {
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
                    if (iCount > 3) {
                        //alert("Select maximum of upto 3 Specialities...");
                        //return false;
                    }
                }
                if (grp == "Periodically All Field Force") {
                    var chkMgr = document.getElementById("chkMgr");
                    var checkMgr = 1;
                    if (chkMgr.checked) {
                        checkMgr = 0;
                    }
                }
                if (grp == 'Date Wise' || grp == 'Date Wise(With out Approval)') {
                    if ($('[id$=txtEffFrom]').val().length == 0) { alert("Select Effective From Date."); $('#txtEffFrom').focus(); return false; }
                }

                var chkVacant = document.getElementById("chkWOVacant");
                var checkVacant = 0;
                if (chkVacant.checked) {
                    checkVacant = 1;
                }
                var chkHalf = document.getElementById("ChkHalfDay");
                var checkHalf = 0;
                if (chkHalf.checked) {
                    checkHalf = 1;
                }
                if (grpFieldForce != '') {
                    var ddlFieldForceValue = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                }

                <%--if (Month != '') {
                    var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;
                }
                if (Year != '') {
                    var ddlYear = document.getElementById('<%=ddlYear.ClientID%>').value;
                }
                if (FrmMonth != '') {
                    var ddlFrmMonth = document.getElementById('<%=ddlFrmMonth.ClientID%>').value;
                }
                if (FrmYear != '') {
                    var ddlFrmYear = document.getElementById('<%=ddlFrmYear.ClientID%>').value;
                }
                if (ToMonth != '') {
                    var ddlToMonth = document.getElementById('<%=ddlToMonth.ClientID%>').value;
                }
            
                if (FrmYear != '') {
                    var ddlToYear = document.getElementById('<%=ddlToYear.ClientID%>').value;
                }--%>


                var ddlOption = document.getElementById('<%=ddlOption.ClientID%>').value;

                if (ddlDivisionName != '') {
                    var ddlDivision = document.getElementById('<%=ddlDivision.ClientID%>').value;
                }

                if (grp == 'Date Wise' || grp == 'Date Wise(for App Approval)') {
                    if (txtEffFrom != '') {
                        var txtEffFrom = document.getElementById('<%=txtEffFrom.ClientID%>').value;
                        var txtEffTo = document.getElementById('<%=txtEffTo.ClientID%>').value;
                    }
                }



                if (grp == "MonthWise") {

                    var ToMonYear = document.getElementById('<%=txtMonthYear.ClientID%>').value.split('-');
                    var MonthVal = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var YearVal = ToMonYear[1];

                    if (checkHalf == 0) {
                        showModalPopUp1(ddlFieldForceValue, MonthVal, YearVal, 0, 0, grpFieldForce, ddlDivision, 1, checkVacant, grp)
                        return false;
                    }
                    else {
                        showModalPopUp1_Half(ddlFieldForceValue, MonthVal, YearVal, 0, 0, grpFieldForce, ddlDivision, 1, checkVacant, grp)
                        return false;
                    }

                }
                else if (grp == "Periodically") {
                    var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                    var fromMon = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                    var fromYear = frmMonYear[1];

                    var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                    var ToMon = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var ToYear = ToMonYear[1];
                    if (checkHalf == 0) {
                        showModalPopUp(ddlFieldForceValue, fromMon, fromYear, ToMon, ToYear, grpFieldForce, ddlDivision, 1, checkVacant, grp)
                    }
                    else {
                        showModalPopUp_Half(ddlFieldForceValue, fromMon, fromYear, ToMon, ToYear, grpFieldForce, ddlDivision, 1, checkVacant, grp)
                    }

                }
                else if (grp == "Periodically All Field Force") {
                    var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                    var fromMon = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                    var fromYear = frmMonYear[1];

                    var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                    var ToMon = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var ToYear = ToMonYear[1];
                    if (checkHalf == 0) {
                        showModalPopUp(ddlFieldForceValue, fromMon, fromYear, ToMon, ToYear, grpFieldForce, ddlDivision, checkMgr, checkVacant, grp)
                    }
                    else {
                        showModalPopUp_Half(ddlFieldForceValue, fromMon, fromYear, ToMon, ToYear, grpFieldForce, ddlDivision, checkMgr, checkVacant, grp)
                    }

                }
                else if (grp == "Date Wise" || grp == 'Date Wise(for App Approval)') {
                    if (checkHalf == 0) {
                        showModalPopUp(ddlFieldForceValue, txtEffFrom, txtEffTo, 0, 0, grpFieldForce, ddlDivision, 1, checkVacant, grp)
                    }
                    else {
                        showModalPopUp_Half(ddlFieldForceValue, txtEffFrom, txtEffTo, 0, 0, grpFieldForce, ddlDivision, 1, checkVacant, grp)
                    }



                }
            });
            //        $('[id$=txtEffFrom]').change(function () {

            //            if ($(this).val().length > 2) {
            //                date = $(this).val();
            //                var efftodate = '';
            //                var emon = 0;
            //                var edate = 0;
            //                var eyear = 0;
            //                var todate = $(this).val().split('/');
            //                if (todate[0] == '01') {
            //                    if (todate[1] == '02')
            //                        edate = '28';
            //                    else
            //                        edate = '30';
            //                }
            //                else
            //                    edate = parseInt(todate[0]) - 1;

            //                var evardate = edate.toString();

            //                if (evardate.length == 1)
            //                    evardate = '0' + evardate;
            //                if (todate[0] != '01') {
            //                    if (todate[1] != '12') {
            //                        emon = parseInt(todate[1]) + 1;

            //                        var evarmon = emon.toString();

            //                        if (evarmon.length == 1)
            //                            evarmon = '0' + evarmon;
            //                        efftodate = evardate + '/' + evarmon + '/' + todate[2];
            //                    }
            //                    else {
            //                        emon = 1;
            //                        var evarmon = emon.toString();

            //                        if (evarmon.length == 1)
            //                            evarmon = '0' + evarmon;
            //                        eyear = parseInt(todate[2]) + 1;
            //                        efftodate = evardate + '/' + evarmon + '/' + eyear;
            //                    }
            //                }
            //                else
            //                    efftodate = evardate + '/' + todate[1] + '/' + todate[2];
            //                $('[id$=txtEffTo]').val(efftodate);

            //            }
            //        });

        });
    </script>
    <form id="form1" runat="server">

        <div>
            <div>
                <div id="Divid" runat="server"></div>
                <%--<ucl:Menu ID="Menu1" runat="server" />--%>
            </div>
            <br />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">

                    <div class="col-lg-5">
                        <h2 class="text-center">Drs Met/Visit and Call Average</h2>
                        <asp:Label ID="Lblmain" runat="server" Text="Call Average" SkinID="lblMand"></asp:Label>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblDivision" runat="server" Text="Division Name " CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="Label1" runat="server" Text="Mode" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlOption" runat="server" AutoPostBack="true" CssClass="nice-select"
                                        OnSelectedIndexChanged="ddlOption_SelectedIndexChanged1">
                                        <asp:ListItem>---Select---</asp:ListItem>
                                        <asp:ListItem>MonthWise</asp:ListItem>
                                        <asp:ListItem>Periodically</asp:ListItem>
                                        <asp:ListItem>Periodically All Field Force</asp:ListItem>
                                        <asp:ListItem>Date Wise</asp:ListItem>
                                        <asp:ListItem>Date Wise(for App Approval)</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:CheckBox ID="chkMgr" Text="Managers" AutoPostBack="true"
                                        runat="server" />
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name " CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select">
                                        <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                    </asp:DropDownList>
                                    <!--<asp:DropDownList ID="ddlFieldForce1" CssClass="drop" runat="server" Width="350"></asp:DropDownList>-->
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Panel ID="pnlMonthly" Visible="false" runat="server">
                                        <%--<div class="single-des">
                                            <asp:Label ID="lblMonth" runat="server" Text="Month" CssClass="label"></asp:Label>

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
                                        <br />
                                        <div class="single-des" style="padding-top: 12px;">
                                            <asp:Label ID="lblYear" runat="server" CssClass="label" Text="Year"></asp:Label>

                                            <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="nice-select">
                                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>
                                        <asp:Label ID="lblMonth" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </asp:Panel>
                                </div>
                                <div>
                                    <asp:Label ID="lblMode" CssClass="label" runat="server" Text="Category" />
                                    <%--<asp:CheckBox ID="CheckState" runat="server" Text="All" onclick="CheckAllState();" />--%>
                                 <asp:CheckBoxList runat="server" ID="cbSpeciality" Visible="false" RepeatDirection="Horizontal" CellSpacing="10"
                                RepeatColumns="7">
                            </asp:CheckBoxList>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Panel ID="pnlPeriodically" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                                <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                                <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <%--        <div class="col-lg-6" style="padding-bottom: 20px;">
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
                                            <div class="col-lg-6" style="padding-bottom: 20px;">
                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                                <asp:DropDownList ID="ddlFrmYear" runat="server" CssClass="nice-select">
                                                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>--%>
                                        </div>
                                        <div class="row">
                                   <%--         <div class="col-lg-6" style="padding-bottom: 20px;">
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
                                            <div class="col-lg-6" style="padding-bottom: 20px;">
                                                <asp:Label ID="lblToYear" runat="server" Text="To Year" CssClass="label"></asp:Label>
                                                <asp:DropDownList ID="ddlToYear" runat="server" CssClass="nice-select">
                                                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>--%>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>


                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Panel ID="pnlDateWise" Visible="false" runat="server">

                                        <div class="single-des clearfix">
                                            <asp:Label ID="lblfrom" runat="server" CssClass="label" Text="From Date"></asp:Label><br />
                                            <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input" onkeypress="Calendar_enter(event);" Width="100%"></asp:TextBox>
                                            <%--<asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtEffFrom" CssClass=" cal_Theme1" />--%>
                                        </div>

                                        <div class="single-des clearfix">
                                            <asp:Label ID="lblto" runat="server" CssClass="label" Text="To Date"></asp:Label><br />
                                            <asp:TextBox ID="txtEffTo" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                            <%--<asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" runat="server" TargetControlID="txtEffTo" CssClass=" cal_Theme1" />--%>
                                        </div>

                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:CheckBox ID="chkWOVacant" runat="server" Text="With Vacants" />
                            </div>
                            <div class="single-des clearfix">
                               <asp:CheckBox ID="ChkHalfDay" runat="server" Text="Half Day Work" />
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">

                            <asp:Button ID="btnSubmit" runat="server" Text="View"
                                CssClass="savebutton" OnClick="btnSubmit_Click1" />
                            <!--btnSubmit_Click1-->

                        </div>
                    </div>


                    <div class="loading" align="center">
                        Loading. Please wait.<br />
                        <br />
                        <img src="../../Images/loader.gif" alt="" />
                    </div>
                </div>
            </div>
        </div>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js" type="text/javascript"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>

        <!--<script src="//ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>-->
        <script src="//cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

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
