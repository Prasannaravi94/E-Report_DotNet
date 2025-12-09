<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_Details.aspx.cs" Inherits="MasterFiles_ActivityReports_Login_Details" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Details</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" href="../../css/Report.css" rel="Stylesheet" />
    <%--<link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />--%>
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <style type="text/css">
        .padding {
            padding: 3px;
        }

        .chkboxLocation label {
            padding-left: 5px;
        }

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
            text-overflow:;
        }

        #effect {
            width: 180px;
            height: 160px;
            padding: 0.4em;
            position: relative;
            overflow: auto;
        }

        .textbox {
            width: 185px;
            height: 14px;
        }

        body {
            font-size: 62.5%;
        }

        td.stylespc {
            padding-bottom: 20px;
            padding-right: 10px;
        }

        .style1 {
            width: 195px;
        }

        .style2 {
            width: 232px;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .divlabel {
            width: 45%;
            float: right;
            position: relative;
            padding-left: 2.15em;
            cursor: pointer;
            vertical-align: top;
            line-height: 20px;
            margin: 2px 0;
            display: block;
            font-size: 14px;
        }

        .divpadding {
            padding-right: 10px;
        }

        .calendarcontrol {
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            color: #90a1ac;
            font-size: 14px;
            padding-left: 10px;
            height: 35px;
        }
    </style>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <script type="text/javascript">
            var popUpObj;

            function showModalPopUp_1(sfcode, fmon, fyr, sf_name, strValue) {
                //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                popUpObj = window.open("rpt_Login_Details.aspx?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&sf_name=" + sf_name + "&strValue=" + strValue,
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
                });
                // LoadModalDiv();
            }

            function showModalPopUp(sfcode, From, To, sf_name, strValue) {
                //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                popUpObj = window.open("rpt_NotLogin_Datewise.aspx?sf_code=" + sfcode + "&From=" + From + "&To=" + To + "&sf_name=" + sf_name + "&strValue=" + strValue,
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

                    var ImgSrc = "https://s18.postimg.org/fissk5ve1/chipmunk.gif"



                    $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');
                });
                // LoadModalDiv();
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

                    var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                    if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                    <%--var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                    if (Year == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                    var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                    if (Month == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }--%>

                    var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                    if ($('#chklog').prop('checked')) {
                        var From = ($("#txtEffFrom").val());
                        var To = ($("#txtEffTo").val());
                        var chkDetail = document.getElementById("chkvacant");

                        var strValue = 1;
                        if (chkDetail.checked) {
                            var strValue = 0;
                        }

                        showModalPopUp(sf_Code, From, To, Name, strValue);
                    }
                    else {
                        <%--var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                        var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;--%>
                        var ToMonYear = document.getElementById('<%=txtMonthYear.ClientID%>').value.split('-');
                        var Month1 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        var Year1 = ToMonYear[1];
                        var chkDetail = document.getElementById("chkvacant");

                        var strValue = 1;
                        if (chkDetail.checked) {
                            var strValue = 0;
                        }

                        showModalPopUp_1(sf_Code, Month1, Year1, Name, strValue);
                    }
                });
            });
        </script>
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
        <link href="../../assets/css/select2.min.css" rel="stylesheet" />
        <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
        <script type="text/javascript">
            $(document).ready(function () {
                $("[id*=ddlFieldForce]").select2();
            });
        </script>
        <div>
            <div id="Divid" runat="server">
            </div>
            <ucl:Menu ID="Menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <center>
                            <table>
                                <tr>
                                    <td align="center">
                                        <h2 class="text-center">Login Details - View</h2>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false" CssClass="ddl">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMonthYear" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                <%--       <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblMoth" runat="server" Text="Month" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
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
                                    <asp:Label ID="lblToYear" runat="server" Text="Year" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                            </div>
                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblEffFrom" runat="server" CssClass="label" Visible="false"><span style="Color:Red">*</span>From</asp:Label><br />
                                    <asp:TextBox ID="txtEffFrom" Visible="false" runat="server" CssClass="calendarcontrol" onkeypress="Calendar_enter(event);"
                                        AutoPostBack="true" OnTextChanged="txtEffFrom_TextChanged" TabIndex="6"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom"
                                        runat="server" />
                                </div>
                                <div style="float: right; width: 45%;">
                                    <asp:Label ID="lblEffTo" runat="server" CssClass="label" Visible="false"><span style="Color:Red">*</span>To</asp:Label><br />
                                    <asp:TextBox ID="txtEffTo" runat="server" Visible="false" CssClass="calendarcontrol" onkeypress="Calendar_enter(event);"
                                        OnTextChanged="txtEffTo_TextChanged" AutoPostBack="true" TabIndex="7" />
                                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEffTo"
                                        runat="server" />
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:CheckBox ID="chkvacant" runat="server" Text="Without Vacant" />
                            </div>
                            <div class="single-des clearfix">
                                <div style="float: left; width: 50%;">
                                    <asp:CheckBox ID="chklog" runat="server" Text="Whoever Not login more than...." AutoPostBack="true" OnCheckedChanged="chklog_CheckedChanged" />
                                </div>
                                <div class="divlabel">
                                    <asp:Label ID="lblday" ForeColor="Red" Font-Bold="true" CssClass="divpadding" runat="server"></asp:Label>Days.
                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

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
</body>
</html>
