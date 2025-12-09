<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Survey_Process_View_Zoom.aspx.cs" Inherits="MasterFiles_Survey_Rpt_Survey_Process_View_Zoom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.4.1.js" integrity="sha256-WpOohJOqMqqyKL9FccASB9O0KwACQJpFTUBLTYOVvVU=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        var pageIndex = 1;
        var pageCount;
        $(function () {
            //Remove the original GridView header
            $("[id$=gvCustomers] tr").eq(0).remove();
        });

        //Load GridView Rows when DIV is scrolled
        $("#dvGrid").on("scroll", function (e) {
            var $o = $(e.currentTarget);
            if ($o[0].scrollHeight - $o.scrollTop() <= $o.outerHeight()) {
                GetRecords();
            }
        });
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />
    <script type="text/javascript" src="../../JsFiles/jquery.effects.core.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery.effects.blind.js"></script>
    <script type="text/javascript" src="../../JsFiles/multiple-select.js"></script>

    <link href="../../css/multiple-select.css" rel="stylesheet" type="text/css" />
    <script src="../../JsFiles/multiple-select.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstFruits]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#pnlNew").hide();

        });

        $(document).keypress(function (e) {

            var key = e.which;
            // alert(key);
            if (key == 116) {
                // if the user pressed 't':
                $("#pnlNew").show();
            }
        });

    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
        type="text/javascript"></script>
    <style type="text/css">
        #pnlNew {
            position: fixed;
            top: 40%;
            left: 30%;
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
        }

        .modalBackgroundNew {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopupNew {
            background-color: #FFFFFF;
            width: 350px;
            border: 3px solid #0DA9D0;
            padding: 0;
        }

            .modalPopupNew .header {
                background-color: #2FBDF1;
                height: 20px;
                color: White;
                line-height: 20px;
                text-align: center;
                font-weight: bold;
                font-size: 14px;
                font-family: Verdana;
            }

            .modalPopupNew .body {
                min-height: 120px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                padding: 5px;
            }

        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            font-size: 11px;
            font-family: Calibri;
            -webkit-appearance: none;
            width: 300px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }
    </style>

    <style>
        .goog-te-banner-frame, .skiptranslate {
            display: none !important;
        }

        .goog-te-gadget {
            display: block !important;
            color: white !important;
        }

        .goog-logo-link {
            display: none;
        }

        .goog-te-combo {
            color: black !important;
        }
    </style>

    <%--<script type="text/javascript">
        // For Translate Franch
        getData = function (key) {
            var temp = window.localStorage.getItem(key);
            var ugData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            return ugData;
        }
        function TranslatePG() {
            trnlat = JSON.parse(getData("Language")) || [];
            lngid = window.localStorage.getItem("tLang") || "";
            if (lngid != "") {
                trCtrls = window.localStorage.getItem("Ctrls") || ""; Ctls = trCtrls.split(',');
                for (infx = 0; infx < Ctls.length; infx++) { $(Ctls[infx]).each(function () { $s = ($(this).val()) ? $(this).val() : $(this).html().replace(/[&]nbsp;/g, ''); $d = trnlat.filter(function (a) { return a["en_ln"].toLowerCase().trim() == $s.toLowerCase().trim() }); $trl = $(this).attr('tr'); if ($d.length > 0 && $trl != 'fr') { if ($(this).val()) $(this).val($d[0][lngid]); else $(this).html($d[0][lngid]); $(this).addClass('notranslate'); $(this).attr('tr', 'fr') } }); }
                new google.translate.TranslateElement({
                    pageLanguage: 'en',
                    includedLanguages: 'en,fr,es',
                    layout: google.translate.TranslateElement.FloatPosition.TOP_LEFT,
                    autoDisplay: false
                }, 'google_translate_element');

                // if any webservice screen then use this 
                setTimeout(function () { TranslatePG(); }, 2000);
            }
        }

        function googleTranslateElementInit() {
            //setTimeout(function () { TranslatePG(); }, 500);

            ////new google.translate.TranslateElement({
            ////    pageLanguage: 'en',
            ////    includedLanguages: 'en,fr,es',
            ////    layout: google.translate.TranslateElement.FloatPosition.TOP_LEFT,
            ////    autoDisplay: false
            ////}, 'google_translate_element');
            //$(document).ready(function () {
                if (window.location.hostname == "www.crm.sanclm.info" || window.location.hostname == "crm.sanclm.info") {
                    setTimeout(function () {
                        TranslatePG();
                    }, 500);
                }
                else {
                    //number filter only no translate by Ferooz
                    arr = JSON.parse(window.localStorage.getItem("NumberNoTranslate"));
                    lngid = window.localStorage.getItem("tLang") || "";
                    trCtrls = window.localStorage.getItem("Ctrls") || ""; Ctls = trCtrls.split(',');
                    for (infx = 0; infx < Ctls.length; infx++) { $(Ctls[infx]).each(function () { $s = ($(this).val()) ? $(this).val() : $(this).html().replace(/[&]nbsp;/g, ''); $dt = arr.filter(function (a) { return a == $s; }); $trl = $(this).attr('tr'); if ($dt.length > 0 && $trl != 'fr') { if ($(this).val()) { $(this).val($dt[0][lngid]); } else { $(this).html($dt[0][lngid]); $(this).addClass('notranslate'); $(this).attr('tr', 'fr') } } }); }
                    new google.translate.TranslateElement({
                        pageLanguage: 'en',
                        includedLanguages: 'en,fr,es',
                        layout: google.translate.TranslateElement.FloatPosition.TOP_LEFT,
                        autoDisplay: false
                    }, 'google_translate_element');
                }
            //});
        }
    </script>

    <script type="text/javascript" src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>--%>
    <script type="text/javascript">
        // For Translate Franch
        getData = function (key) {
            var temp = window.localStorage.getItem(key);
            var ugData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            return ugData;
        }
        function TranslatePG() {
            trnlat = JSON.parse(getData("Language")) || [];
            lngid = window.localStorage.getItem("tLang") || "";
            if (lngid != "") {
                trCtrls = window.localStorage.getItem("Ctrls") || ""; Ctls = trCtrls.split(',');
                for (infx = 0; infx < Ctls.length; infx++) { $(Ctls[infx]).each(function () { $s = ($(this).val()) ? $(this).val() : $(this).html().replace(/[&]nbsp;/g, ''); $d = trnlat.filter(function (a) { return a["en_ln"].toLowerCase().trim() == $s.toLowerCase().trim() }); $trl = $(this).attr('tr'); if ($d.length > 0 && $trl != 'fr') { if ($(this).val()) $(this).val($d[0][lngid]); else $(this).html($d[0][lngid]); $(this).addClass('notranslate'); $(this).attr('tr', 'fr') } }); }
                new google.translate.TranslateElement({
                    pageLanguage: 'en',
                    includedLanguages: 'en,fr,es',
                    layout: google.translate.TranslateElement.FloatPosition.TOP_LEFT,
                    autoDisplay: false
                }, 'google_translate_element');

                // if any webservice screen then use this 
                setTimeout(function () { TranslatePG(); }, 2000);
            }
        }

        function googleTranslateElementInit() {
            //setTimeout(function () { TranslatePG(); }, 500);

            ////new google.translate.TranslateElement({
            ////    pageLanguage: 'en',
            ////    includedLanguages: 'en,fr,es',
            ////    layout: google.translate.TranslateElement.FloatPosition.TOP_LEFT,
            ////    autoDisplay: false
            ////}, 'google_translate_element');
            //$(document).ready(function () {
                if (window.location.hostname == "www.crm.sanclm.info" || window.location.hostname == "crm.sanclm.info") {
                    setTimeout(function () {
                        TranslatePG();
                    }, 500);
                }
                else {
                    //number filter only no translate by Ferooz
                    arr = JSON.parse(window.localStorage.getItem("NumberNoTranslate"));
                    lngid = window.localStorage.getItem("tLang") || "";
                    trCtrls = window.localStorage.getItem("Ctrls") || ""; Ctls = trCtrls.split(',');
                    for (infx = 0; infx < Ctls.length; infx++) { $(Ctls[infx]).each(function () { $s = ($(this).val()) ? $(this).val() : $(this).html().replace(/[&]nbsp;/g, ''); $dt = arr.filter(function (a) { return a == $s; }); $trl = $(this).attr('tr'); if ($dt.length > 0 && $trl != 'fr') { if ($(this).val()) { $(this).val($dt[0][lngid]); } else { $(this).html($dt[0][lngid]); $(this).addClass('notranslate'); $(this).attr('tr', 'fr') } } }); }
                    new google.translate.TranslateElement({
                        pageLanguage: 'en',
                        includedLanguages: 'en,fr,es',
                        layout: google.translate.TranslateElement.FloatPosition.TOP_LEFT,
                        autoDisplay: false
                    }, 'google_translate_element');
                }
            //});
        }
    </script>

    <script type="text/javascript" src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table width="100%">
                <tr>
                    <td width="80%"></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="return PrintPanel();" Visible="false" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />
                                </td>

                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent()" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlContents" runat="server">
                <center>

                    <div align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Coverage Analysis for the month of "
                            Font-Underline="True" Font-Bold="True" Font-Names="Verdana" Font-Size="11pt"></asp:Label>
                        <br />
                        <%--<asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="9pt"></asp:Label>--%>
                    </div>

                </center>
                <br />
                <center>
                    <asp:Table ID="tbl5" BorderStyle="Solid" BorderWidth="0" Width="70%"
                        CellSpacing="10" CellPadding="10" runat="server">

                        <asp:TableRow HorizontalAlign="Left">
                            <asp:TableCell BorderWidth="0" CssClass="padding">
                                <asp:Label ID="lblFF" runat="server" Width="120px" Text="Fieldforce Name " SkinID="lblMand" Font-Bold="true" Font-Size="14px"></asp:Label>
                                <asp:Label ID="FF" runat="server" Font-Bold="true" Font-Size="14px">: </asp:Label>
                                <asp:Label ID="LblForceName" runat="server" SkinID="lblMand" Font-Bold="true" Font-Size="12px" ForeColor="#ff6666"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell BorderWidth="0" CssClass="padding">
                                <asp:Label ID="lblHQ" runat="server" Width="130px" Text="Speciality " SkinID="lblMand" Font-Bold="true" Font-Size="14px"></asp:Label>
                                <asp:Label ID="hq" runat="server" Font-Bold="true" Font-Size="14px">: </asp:Label>
                                <asp:Label ID="txtHQ" runat="server" SkinID="lblMand" Font-Bold="true" Font-Size="12px" ForeColor="#ff6666"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell BorderWidth="0" CssClass="padding">
                                <asp:Label ID="lblDesig" runat="server" Width="120px" Text="Designation " SkinID="lblMand" Font-Bold="true" Font-Size="14px"></asp:Label>
                                <asp:Label ID="desig" runat="server" Font-Bold="true" Font-Size="14px">: </asp:Label>
                                <asp:Label ID="txtDesig" runat="server" SkinID="lblMand" Font-Bold="true" Font-Size="12px" ForeColor="#ff6666"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow HorizontalAlign="Left">
                            <asp:TableCell BorderWidth="0" CssClass="padding" ColumnSpan="3">
                                <asp:Label ID="lblQues" Width="120px" runat="server" Text="Question " SkinID="lblMand" Font-Bold="true" Font-Size="14px"></asp:Label>
                                <asp:Label ID="Ques" runat="server" Font-Bold="true" Font-Size="14px">: </asp:Label>
                                <asp:Label ID="txtQues" runat="server" SkinID="lblMand" Font-Bold="true" Font-Size="12px" ForeColor="#ff6666"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow> 
                         
                    </asp:Table>
                </center>
                <br />
                <center>

                    <asp:Panel ID="Panel2" runat="server">

                        <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                            AutoGenerateColumns="true" CssClass="mGrids" EmptyDataText="No Records Found"
                            GridLines="Both" HorizontalAlign="Center" BorderWidth="1"
                            OnRowCreated="GrdFixation_RowCreated" OnRowDataBound="GrdFixation_RowDataBound"
                            ShowHeader="False" Width="68%" Font-Names="calibri" Font-Size="Small">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt" />
                            <RowStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="Small" Font-Names="calibri" />
                            <Columns>
                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" ForeColor="Black"
                                Height="5px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>

                    </asp:Panel>
                </center>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
