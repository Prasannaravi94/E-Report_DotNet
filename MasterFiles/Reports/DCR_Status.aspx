<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Status.aspx.cs" Inherits="Reports_DCR_Status" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DCR Status</title>
    <style type="text/css">
        #tblDocRpt {
            margin-left: 300px;
        }
    </style>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, sf_name, chkatten, checkVacant) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptDCR_Status_New.aspx?sf_code=" + sfcode + "&cmon=" + fmon + "&cyear=" + fyr + "&sf_name=" + sf_name + "&chkatten=" + chkatten + "&checkVacant=" + checkVacant + "&type=1",
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
            // LoadModalDiv();

            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../Images/loading/loading47.gif";

                var ImgTxT = "http://i.imgur.com/KUJoe.gifhttps://s10.postimg.org/g0v7h43wp/Text_1.gif";

                var ImgSrc = "https://s14.postimg.org/laa2v19dd/loading_DCR_Status.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red;margin-left:40%">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:300px; height: 235px;position: fixed;top: 10%;left: 35%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
        }

        function showDCR_Status_DetailedPopUp(sfcode, fmon, fyr, sf_name, checkVacant) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptDCR_Status_New_Detail.aspx?sf_code=" + sfcode + "&cmon=" + fmon + "&cyear=" + fyr + "&sf_name=" + sf_name + "&checkVacant=" + checkVacant + "&type=1",
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
            // LoadModalDiv();

            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../Images/loading/loading47.gif";

                var ImgTxT = "http://i.imgur.com/KUJoe.gifhttps://s10.postimg.org/g0v7h43wp/Text_1.gif";

                var ImgSrc = "https://s21.postimg.org/oxhgvapef/loading_28_DCR_Status.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red;margin-left:40%">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:300px; height: 235px;position: fixed;top: 10%;left: 35%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
        }

        function showPeriodicRep(sfcode, fdate, todate, sf_name, chkatten, checkVacant) {

            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptDCR_Status_New.aspx?sf_code=" + sfcode + "&fdate=" + fdate + "&todate=" + todate + "&sf_name=" + sf_name + "&chkatten=" + chkatten + "&checkVacant=" + checkVacant + "&type=2",
    "ModalPopUp" //,
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

                var ImgSrc = "https://s11.postimg.org/3mtz3je7n/loading_24_ook.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red;margin-left:40%"></p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:300px; height: 50px;position: fixed;top: 40%;left: 35%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
        }


        function showPeriodicRep_Det(sfcode, fdate, todate, sf_name, checkVacant) {

            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptDCR_Status_New_Detail.aspx?sf_code=" + sfcode + "&fdate=" + fdate + "&todate=" + todate + "&sf_name=" + sf_name + "&checkVacant=" + checkVacant + "&type=2",
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
            // LoadModalDiv();

            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../Images/loading/loading47.gif";

                var ImgTxT = "http://i.imgur.com/KUJoe.gifhttps://s10.postimg.org/g0v7h43wp/Text_1.gif";

                var ImgSrc = "https://s11.postimg.org/3mtz3je7n/loading_24_ook.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red;margin-left:40%"></p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:300px; height: 50px;position: fixed;top: 50%;left: 35%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
        }


    </script>

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>

    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                   function (n) {
                       return exp.test($(n).text());
                   });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                   );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />

    <style type="text/css">
        #chkitem [type="checkbox"]:not(:checked) + label, #chkitem [type="checkbox"]:checked + label {
            padding-left: 1.15em;
        }
    </style>

</head>
<body>
    <script type="text/javascript" id=" ">

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
                if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() == "1") {
                    <%--var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                    if (TMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }--%>
                }
                if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() == "2") {
                    if ($('[id$=txtEffFrom]').val().length == 0)
                    { alert("Select Effective From Date."); $('#txtEffFrom').focus(); return false; }
                }
                var SFName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SFName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var ddlFieldForceValue = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() == "1") {


                    <%--var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;

                    var ddlYear = document.getElementById('<%=ddlYear.ClientID%>').value;--%>

                    var ToMonYear = document.getElementById('<%=txtMonthYear.ClientID%>').value.split('-');
                    var Month1 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var Year1 = ToMonYear[1];


                    var chkDetail = document.getElementById("chkDetail");

                    var chkVacant = document.getElementById("chkVacant");
                    var checkVacant = 1;
                    if (chkVacant.checked) {
                        checkVacant = 0;
                    }

                    if (chkDetail.checked) {
                        //alert(checkVacant);
                        showDCR_Status_DetailedPopUp(ddlFieldForceValue, Month1, Year1, SFName, checkVacant);

                    }
                    else {

                        var chkatten = document.getElementById("chkatten");

                        if (chkatten.checked) {
                            showModalPopUp(ddlFieldForceValue, Month1, Year1, SFName, 1, checkVacant)
                        }
                        else {
                            showModalPopUp(ddlFieldForceValue, Month1, Year1, SFName, 0, checkVacant)
                        }
                    }
                }
                if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() == "2") {
                    var txtEffFrom = document.getElementById('<%=txtEffFrom.ClientID%>').value;
                    var txtEffTo = document.getElementById('<%=txtEffTo.ClientID%>').value;

                    var chkDetail = document.getElementById("chkDetail");

                    var chkVacant = document.getElementById("chkVacant");
                    var checkVacant = 1;
                    if (chkVacant.checked) {
                        checkVacant = 0;
                    }
                    //alert(checkVacant);
                    if (chkDetail.checked) {

                        showPeriodicRep_Det(ddlFieldForceValue, txtEffFrom, txtEffTo, SFName, checkVacant);

                    }
                    else {

                        var chkatten = document.getElementById("chkatten");
                        if (chkatten.checked) {
                            showPeriodicRep(ddlFieldForceValue, txtEffFrom, txtEffTo, SFName, 1, checkVacant)
                        }
                        else {
                            showPeriodicRep(ddlFieldForceValue, txtEffFrom, txtEffTo, SFName, 0, checkVacant)
                        }
                    }
                }
                return false;
            });
            $('[id$=txtEffFrom]').change(function () {
                if ($(this).val().length > 2) {
                    date = $(this).val();
                    var efftodate = '';
                    var emon = 0;
                    var edate = 0;
                    var eyear = 0;
                    var todate = $(this).val().split('/');
                    if (todate[0] == '01') {
                        //if (todate[1] == '02') {
                        //    edate = '28';
                        //}

                        //else if (todate[1] == "01" || todate[1] == "03" || todate[1] == "05" || todate[1] == "07" || todate[1] == "08" || todate[1] == "10" || todate[1] == "12") {
                        //    edate = '31';
                        //}
                        //else {
                        //    edate = '30';
                        //}
                        edate = new Date(parseInt(todate[2]), parseInt(todate[1]), 0).getDate();
                    }
                    else
                        edate = parseInt(todate[0]) - 1;

                    var evardate = edate.toString();

                    if (evardate.length == 1)
                        evardate = '0' + evardate;
                    if (todate[0] != '01') {
                        if (todate[1] != '12') {
                            emon = parseInt(todate[1]) + 1;

                            var evarmon = emon.toString();

                            if (evarmon.length == 1)
                                evarmon = '0' + evarmon;
                              if (evarmon == '02' && evardate > '28') {
                              evardate = new Date(parseInt(todate[2]), parseInt(evarmon), 0).getDate();
                          }

                            efftodate = evardate + '/' + evarmon + '/' + todate[2];
                        }
                        else {
                            emon = 1;
                            var evarmon = emon.toString();

                            if (evarmon.length == 1)
                                evarmon = '0' + evarmon;
                            eyear = parseInt(todate[2]) + 1;
                            efftodate = evardate + '/' + evarmon + '/' + eyear;
                        }
                    }
                    else
                        efftodate = evardate + '/' + todate[1] + '/' + todate[2];
                    $('[id$=txtEffTo]').val(efftodate);

                }
            });
        });
    </script>

    <script type="text/javascript" language="javascript">
        function hideDiv() {

            if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() == "1") {
                var lblmon = document.getElementById('lblmon');
                lblmon.style.display = "block";
                lblmon.style.visibility = "visible";
                var ddlMonth = document.getElementById('ddlMonth');
                ddlMonth.style.display = "none";
                ddlMonth.style.visibility = "hidden";
                var lblYear = document.getElementById('lblYear');
                lblYear.style.display = "block";
                lblYear.style.visibility = "visible";
                var ddlYear = document.getElementById('ddlYear');
                ddlYear.style.display = "none";
                ddlYear.style.visibility = "hidden";

                document.getElementById('divMonthYear').style.visibility = "visible";
                document.getElementById('divMonthYear').style.display = "block";

                var lblfrom = document.getElementById('lblfrom');
                lblfrom.style.display = "none";
                lblfrom.style.visibility = "hidden";
                var txtEffFrom = document.getElementById('txtEffFrom');
                txtEffFrom.style.display = "none";
                txtEffFrom.style.visibility = "hidden";
                var lblto = document.getElementById('lblto');
                lblto.style.display = "none";
                lblto.style.visibility = "hidden";
                var txtEffTo = document.getElementById('txtEffTo');
                txtEffTo.style.display = "none";
                txtEffTo.style.visibility = "hidden";
            }

            if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() == "2") {
                var lblfrom = document.getElementById('lblfrom');
                lblfrom.style.display = "block";
                lblfrom.style.visibility = "visible";
                var txtEffFrom = document.getElementById('txtEffFrom');
                txtEffFrom.style.display = "block";
                txtEffFrom.style.visibility = "visible";
                var lblto = document.getElementById('lblto');
                lblto.style.display = "block";
                lblto.style.visibility = "visible";
                var txtEffTo = document.getElementById('txtEffTo');
                txtEffTo.style.display = "block";
                txtEffTo.style.visibility = "visible";

                document.getElementById('divMonthYear').style.visibility = "hidden";
                document.getElementById('divMonthYear').style.display = "none";

                var lblmon = document.getElementById('lblmon');
                lblmon.style.display = "none";
                lblmon.style.visibility = "hidden";
                var ddlMonth = document.getElementById('ddlMonth');
                ddlMonth.style.display = "none";
                ddlMonth.style.visibility = "hidden";
                var lblYear = document.getElementById('lblYear');
                lblYear.style.display = "none";
                lblYear.style.visibility = "hidden";
                var ddlYear = document.getElementById('ddlYear');
                ddlYear.style.display = "none";
                ddlYear.style.visibility = "hidden";
            }
        }
    </script>

    <%--<script type="text/javascript">
        function checkcheckbox(checkbox) {
//            var chkDetail = document.getElementById("<%= chkDetail.ClientID %>").checked;
//            var chkatten = document.getElementById("<%= chkatten.ClientID %>").checked;

            var chkDetail = document.getElementById("<%chkDetail.ClientID%>")
            //var chkatten = document.getElementById("<%chkatten.ClientID%>")

            if (chkDetail.checked) {
                chkatten = false;
            }
//            else if (chkatten.checked) {
//            chkDetail.checked = false;
         //   }
        }
    </script>--%>

    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">

                        <h2 class="text-center">DCR Status</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDivision" Visible="false" runat="server" CssClass="label" Text="Division "></asp:Label>
                                <asp:DropDownList ID="ddlDivision" Visible="false" runat="server" CssClass="custom-select2 nice-select"
                                    OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Width="350">
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="FieldForce Name"></asp:Label>
                                <%--<asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="State Name"></asp:ListItem>
                                    </asp:DropDownList>--%>
                                <%--   <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                  OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                                  </asp:DropDownList>--%>
                                <%--  <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                    ToolTip="Enter Text Here"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%"
                                    AutoPostBack="false" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblType" runat="server" Text="Type" CssClass="label"></asp:Label>
                                <asp:RadioButtonList ID="rdoType" runat="server" RepeatDirection="Horizontal" onchange="hideDiv()">
                                    <asp:ListItem Value="1" Text="Monthwise" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Periodwise"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div id="divMonthYear">
                                <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
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
                                <%--            <div class="single-des clearfix">
                                    <asp:Label ID="lblmon" runat="server" CssClass="label" Text="Month"></asp:Label>
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
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblYear" runat="server" CssClass="label" Text="Year"></asp:Label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>--%>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblfrom" runat="server" CssClass="label" Text="From Date" Style="display: none"></asp:Label>
                                <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input" onkeypress="Calendar_enter(event);" Style="display: none" Width="100%"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom" CssClass="cal_Theme1"
                                    runat="server" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblto" runat="server" CssClass="label" Text="To Date" Style="display: none"></asp:Label>
                                <asp:TextBox ID="txtEffTo" runat="server" CssClass="input" Style="display: none" Width="100%"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix" id="chkitem">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="With Vacants"></asp:Label>
                                        <asp:CheckBox ID="chkVacant" runat="server" Text="." />
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Detailed"></asp:Label>
                                        <asp:CheckBox ID="chkDetail" runat="server" Text="." />
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Attendance"></asp:Label>
                                        <asp:CheckBox ID="chkatten" runat="server" Text="." />
                                    </div>
                                </div>

                            </div>

                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="View" />
                        </div>

                    </div>
                    <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                        Width="60%">
                    </asp:Table>

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
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>

       
    </form>
</body>
</html>
