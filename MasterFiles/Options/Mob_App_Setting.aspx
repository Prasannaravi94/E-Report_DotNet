 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mob_App_Setting.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="MasterFiles_Options_Mob_App_Setting" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Apps Settings</title>
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
            <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css">
<link rel="stylesheet" href="../../../assets/css/nice-select.css">
<link rel="stylesheet" href="../../../assets/css/bootstrap.min.css">
<link rel="stylesheet" href="../../../assets/css/style.css">
<link rel="stylesheet" href="../../../assets/css/responsive.css">
    <style type="text/css">
        .spc {
            /*padding-left: 5%;*/
            padding-left: 30px !important;
        }

        .spc1 {
            padding-left: 10%;
        }

        .box {
            background: #FFFFFF;
            border: 5px solid #427BD6;
            border-radius: 8px;
        }

        .tableHead {
            background: #e0f3ff;
            color: black;
            border-style: solid;
            border-width: 1px;
            border-color: #427BD6;
        }

        .break {
            height: 10px;
        }

        .style1 {
            padding-left: 5%;
            height: 26px;
        }

        .style2 {
            height: 26px;
        }


        .modalBackground {
            /* background-color: #999999;*/
            filter: alpha(opacity=80);
            opacity: 0.5;
            z-index: 10000;
            display: block;
            cursor: default;
            color: #000000;
            pointer-events: none;
        }

        #menu1 {
            display: none;
        }

        .TextFont {
            text-align: center;
            margin-top: 6px;
        }

        .modalPopupNew {
            background-color: #FFFFFF;
            width: 350px;
            border: 3px solid #0DA9D0;
            padding: 0;
        }

        .modalBackgroundNew {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        #Table4 {
            height: 592px;
            width: 941px;
        }

        .display-reporttable .table tr:first-child td:first-child {
            border-radius: 8px 0 0 8px;
            background-color: #414d55;
            color: #ffffff;
            font-size: 14px;
            font-weight: 400;
            border-left: 0px solid #F1F5F8;
            border-bottom: 10px solid #fff;
        }

        .display-reporttable .table tr:first-child td {
            padding: 20px 10px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            background-color: #F1F5F8;
        }

        .display-reporttable .table tr td table tr:first-child td:first-child, .display-reporttable .table tr td table tr:first-child td {
            background-color: white;
            color: #636d73;
            border-left: none;
        }

        .display-reporttable .table tr td table tr:first-child td {
            padding: 0px;
        }

        .display-reporttable tr td, table tr .single-des .input {
            font-size: 12px;
            margin-bottom: 5px;
        }

        .display-reporttable .table tr td table tr:first-child td, .display-reporttable .table tr td table tr:first-child td:first-child {
            border-bottom: none;
        }

        tbody {
            color: #696d6e;
        }

        .display-table #grdgps th:first-child {
            font-size: 12px;
            text-transform: none;
            background-color: #F1F5F8;
            color: #636d73;
        }

        #UpdatePanel3 .display-table {
            text-transform: none;
            font-size: 11px;
        }

        .display-table #grdgps tr:nth-child(2) td:first-child, .display-table #grdgps tr td:first-child {
            background-color: white;
            color: #636d73;
            padding: 5px 0px;
            border-top: 1px solid #dee2e6;
        }

        .display-table #grdgps th [type="checkbox"]:not(:checked) + label, .display-table #grdgps th [type="checkbox"]:checked + label {
            padding-left: 1.75em;
        }

        .display-table #grdgps tr td [type="checkbox"]:not(:checked) + label, .display-table #grdgps tr td [type="checkbox"]:checked + label {
            color: white;
        }

        .single-block-area td {
            padding: 1px !important;
            width: 66% !important;
        }

        .single-des .input {
            height: 31px !important;
        }
    </style>
    <script type="text/javascript">
        function HidePanel() {
            var pnlpopup = document.getElementById('pnlpopup');
            pnlpopup.style.display = "none";
            pnlpopup.style.visibility = "hidden";
            return false;
        }
    </script>

    <script type="text/javascript">
        function SelectAllCheckboxes(chk, selector) {
            $('#<%=grdgps.ClientID%>').find(selector + " input:checkbox").each(function () {
                $(this).prop("checked", $(chk).prop("checked"));
            });
            SelectSubCheckboxesAll(chk, ".GPS");
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#collapsibleNavbar').removeClass('collapse');
        });
    </script>

    <script type="text/javascript">
        function CheckNumeric(evt) {
            // Only ASCII character in that range allowed
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57)) {
                return false;
            }
            return true;
        }
        function CheckNumericMin(evt) {
            // Only ASCII character in that range allowed
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if (ASCIICode > 31 && (ASCIICode < 49 || ASCIICode > 57)) {
                return false;
            }
            return true;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <asp:HiddenField ID="hdnGetDynamicUpdate" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <h2 class="text-center" id="hHeading" runat="server"></h2>
                        <center>
                            <%--  <asp:Button ID="btnTest" runat="server" CssClass="savebutton"
                                Text="Test" OnClientClick="TestFun()" />--%>

                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton"
                                Text="Save" OnClick="btnSubmitNew_Click" />
                            &nbsp;&nbsp;
                          <asp:Button ID="btnClear" runat="server" CssClass="resetbutton"
                              Text="Clear" OnClick="btnClear_Click" />
                        </center>
                        <br />

                        <div class="row justify-content-center">
                            <div class="col-lg-6" id="Tbl_setup">
                                <div class="single-block-area">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <asp:Label ID="lblloca" runat="server" Text="Location" Width="100%"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton ID="linkgps" Text="GEO Tagging & Fencing" runat="server"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblmandat" runat="server" Text="Location Mandatory" Font-Size="12px" ForeColor="#f82b00"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdomandt" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:HiddenField ID="hdnmandt" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbldevice" runat="server" Text="Device Id Needed" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdodevice" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblcover" runat="server" Text="Coverage {km}" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="style2 single-des">
                                                <asp:TextBox ID="txtcover" runat="server" Width="90" CssClass="input" Height="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Territory based GEO Fencing" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoTerr_based_Tag" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lblgeoTagImg" runat="server" Text="GeoTag - Image Capture Needed" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="RadiogeoTagImg" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">

                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span1" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="lblcap" runat="server" Text="Visit Type" Font-Bold="True"
                                                        Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblvist1" runat="server" Text="Visit Type 1 - Doctor  Label" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtvisit1" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblvist2" runat="server" Text="Visit Type 2 - Chemist  Label" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtvisit2" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblvist3" runat="server" Text="Visit Type 3 - Stockist Label" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtvisit3" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblvist4" runat="server" Text="Visit Type 4 - Unlisted Doctor Label" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtvisit4" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span2" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="lblHalfday" runat="server" Text="HALFDAY WORK"
                                                        Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>

                                            <td align="center">
                                                <asp:Label ID="lblmr" runat="server" Text="Base Level" ForeColor="Magenta" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lblmgr" runat="server" Text="Managers" ForeColor="Magenta" Font-Bold="true"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <div style="padding: 2px; overflow: auto;">
                                                    <asp:CheckBoxList class="bor" ID="chkhaf_work" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>

                                            </td>
                                            <td align="center">
                                                <div style="padding: 2px; overflow: auto;">
                                                    <asp:CheckBoxList class="bor" ID="chkhaf_work_mgr" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">

                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <asp:Label ID="lblChem" runat="server" Text="Chemist Entry"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNeed_chem" runat="server" Text="Needed" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoNeed_chem" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label98" Style="font-size: 12px;" runat="server" Text="POB Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio76" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="llb_16" Style="font-size: 12px;" runat="server" Text="POB Entry Option as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio16" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label110" Style="font-size: 12px;" runat="server" Text="Joint Work Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio82" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label118" Style="font-size: 12px;" runat="server" Text="Joint Work Selection Option as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio86" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblProduct_entr_chem" runat="server" Text="Product Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoProduct_entr_chem" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label126" Style="font-size: 12px;" runat="server" Text="Product Caption"></asp:Label>
                                            </td>
                                            <td class="single-des" align="left">
                                                <asp:TextBox ID="txtCPC" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label34" runat="server" Text="Product - Sample Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtChmSmpCap" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label141" Style="font-size: 12px;" runat="server" Text="Product - Sample Qty Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadiochmsamQty_need" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblqty_Cap_chem" runat="server" Text="Product - Rx Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtqty_Cap_chem" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label167" Style="font-size: 12px;" runat="server" Text="Product - Rx Qty Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioChmRxNd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label153" Style="font-size: 12px;" runat="server" Text="Product - Additional Qty Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiochm_ad_qty" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblinpu_entry_chem" runat="server" Text="Input Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoinpu_entry_chem" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label161" Style="font-size: 12px;" runat="server" Text="Input Caption"></asp:Label>
                                            </td>
                                            <td class="single-des" align="left">
                                                <asp:TextBox ID="txtChm_Input_caption" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label40" Style="font-size: 12px;" runat="server" Text="EventCapture Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio64" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label26" Style="font-size: 12px;" runat="server" Text="EventCapture Option Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioChmEvent_Md" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label14" Style="font-size: 12px;" runat="server" Text="Feedback Entry Options Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio52" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label7" Style="font-size: 12px;" runat="server" Text="Hospital based Chemit option Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiohosp_filter" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label9" Style="font-size: 12px;" runat="server" Text="Detailing Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioDetailing_chem" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <asp:Label ID="lblUnlisr_Dr" runat="server" Text="Unlisted Dr. Entry"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblneed_unlistDr" runat="server" Text="Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoneed_unlistDr" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label102" Style="font-size: 12px;" runat="server" Text="POB Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio78" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label106" Style="font-size: 12px;" runat="server" Text="Pob Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio80" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label114" Style="font-size: 12px;" runat="server" Text="Joint Work Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio84" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label122" Style="font-size: 12px;" runat="server" Text="Joint Work Selection Option as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio88" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label130" Style="font-size: 12px;" runat="server" Text="Product Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtUPC" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"
                                                    ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblprdentry_unlistDr" runat="server" Text="Product Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoprdentry_unlistDr" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSamQty_Cap_unlistDr" runat="server" Text="Product - Sample Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtSamQty_Cap_unlistDr" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblRxQty_Cap_unlistDr" runat="server" Text="Product - Rx Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtRxQty_Cap_unlistDr" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label165" Style="font-size: 12px;" runat="server" Text="Input Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtUl_Input_caption" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"
                                                    ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblinpuEnt_Need_unlistDr" runat="server" Text="Input Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoinpuEnt_Need_unlistDr" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>



                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label72" Style="font-size: 12px;" runat="server" Text="EventCapture Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio66" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label35" Style="font-size: 12px;" runat="server" Text="EventCapture Option Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioUlDrEvent_Md" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label20" Font-Size="12px" runat="server" Text="Feedback Entry Options Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio55" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label37" Style="font-size: 12px;" runat="server" Text="DETAILING Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioDetailing_undr" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span15" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="Label210" runat="server" Text="Tour Plan"
                                                        Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label187" Font-Size="12px" runat="server" Text="Tour Plan Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                             
                                                <asp:RadioButtonList ID="Radiotp_need" runat="server" RepeatDirection="Horizontal"  OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label42" Font-Size="12px" runat="server" Text="Tour Plan Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio32" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTpDrNeed" Font-Size="12px" runat="server" Text="TP Doctor Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioDrNeed" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTpChmNeed" Font-Size="12px" runat="server" Text="TP Chemist Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTpChmNeed" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTpStkNeed" Font-Size="12px" runat="server" Text="TP Stockist Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTpStkNeed" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTpJWNeed" Font-Size="12px" runat="server" Text="TP JointWork Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTpJWNeed" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTpHospNeed" Font-Size="12px" runat="server" Text="TP Hospital Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTpHospNeed" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTpCip_Need" Font-Size="12px" runat="server" Text="TP CIP Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTpCip_Need" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label172" Font-Size="12px" runat="server" Text="Dr/Che/Stk/Unl/Cip/Hos/Jw Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTpFW_meetup_mandatory" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTpClusterNeed" Font-Size="12px" runat="server" Text="TP Cluster Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTpClusterNeed" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label29" Font-Size="12px" runat="server" Text="TP Multiple Cluster Selection {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radioclustertype" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTpAddsessionNeed" Font-Size="12px" runat="server" Text="ADD Session Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTpAddsessionNeed" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTpAddsessionCount" Font-Size="12px" runat="server" Text="TP Session Count"></asp:Label>
                                            </td>
                                            <%--<td align="left">
                                                <asp:RadioButtonList ID="RadioTpAddsessionCount" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>--%>
                                            <td align="left" class="single-des">
                                                <asp:DropDownList ID="ddlTpAddsessionCount" runat="server" CssClass="input">
                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label46" Font-Size="12px" runat="server" Text="TP Based My Day Plan"></asp:Label>
                                            </td>
                                            
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio33" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label78" Font-Size="12px" runat="server" Text="TP Based DCR - Deviation"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio31" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label93" Font-Size="12px" runat="server" Text="TP Deviation Approval Needed"></asp:Label>
                                            </td>
                                            <td align="left">

                                                <asp:RadioButtonList ID="Radio92" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label80" Font-Size="12px" runat="server" Text="TP Start date Range"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:DropDownList runat="server" ID="txt_srtdate" CssClass="input" Style="height: 27px; width: 120px;" AutoPostBack="True" OnSelectedIndexChanged="txt_srtdateSelectedChanged">
                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="6">6</asp:ListItem>
                                                    <asp:ListItem Value="7">7</asp:ListItem>
                                                    <asp:ListItem Value="8">8</asp:ListItem>
                                                    <asp:ListItem Value="9">9</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>

                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                    <asp:ListItem Value="13">13</asp:ListItem>
                                                    <asp:ListItem Value="14">14</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="16">16</asp:ListItem>
                                                    <asp:ListItem Value="17">17</asp:ListItem>
                                                    <asp:ListItem Value="18">18</asp:ListItem>
                                                    <asp:ListItem Value="19">19</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>

                                                    <asp:ListItem Value="21">21</asp:ListItem>
                                                    <asp:ListItem Value="22">22</asp:ListItem>
                                                    <asp:ListItem Value="23">23</asp:ListItem>
                                                    <asp:ListItem Value="24">24</asp:ListItem>
                                                    <asp:ListItem Value="25">25</asp:ListItem>
                                                    <asp:ListItem Value="26">26</asp:ListItem>
                                                    <asp:ListItem Value="27">27</asp:ListItem>
                                                    <asp:ListItem Value="28">28</asp:ListItem>
                                                    <asp:ListItem Value="29">29</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>

                                                    <asp:ListItem Value="31">31</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label82" Font-Size="12px" runat="server" Text="TP End date Range"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:DropDownList runat="server" ID="txt_enddate" CssClass="input" Style="height: 27px; width: 120px;" Enabled="false">
                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="6">6</asp:ListItem>
                                                    <asp:ListItem Value="7">7</asp:ListItem>
                                                    <asp:ListItem Value="8">8</asp:ListItem>
                                                    <asp:ListItem Value="9">9</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>

                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                    <asp:ListItem Value="13">13</asp:ListItem>
                                                    <asp:ListItem Value="14">14</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="16">16</asp:ListItem>
                                                    <asp:ListItem Value="17">17</asp:ListItem>
                                                    <asp:ListItem Value="18">18</asp:ListItem>
                                                    <asp:ListItem Value="19">19</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>

                                                    <asp:ListItem Value="21">21</asp:ListItem>
                                                    <asp:ListItem Value="22">22</asp:ListItem>
                                                    <asp:ListItem Value="23">23</asp:ListItem>
                                                    <asp:ListItem Value="24">24</asp:ListItem>
                                                    <asp:ListItem Value="25">25</asp:ListItem>
                                                    <asp:ListItem Value="26">26</asp:ListItem>
                                                    <asp:ListItem Value="27">27</asp:ListItem>
                                                    <asp:ListItem Value="28">28</asp:ListItem>
                                                    <asp:ListItem Value="29">29</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>

                                                    <asp:ListItem Value="31">31</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label31" Font-Size="12px" runat="server" Text="TP Autopost Holiday Edit option Needed"></asp:Label>
                                            </td>
                                            <td align="left">

                                                <asp:RadioButtonList ID="Radioedit_holiday" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label33" Font-Size="12px" runat="server" Text="TP Autopost Week-off Edit option Needed"></asp:Label>
                                            </td>
                                            <td align="left">

                                                <asp:RadioButtonList ID="Radioedit_weeklyoff" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span22" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="Label230" runat="server" Text="Missed Date / Leave"
                                                        Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl2" Style="font-size: 12px;" runat="server" Text="'Missed Date' Entry Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio2" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label132" Style="font-size: 12px;" runat="server" Text="Missed Date Entry Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioBtnList1" EnableViewState="true" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td align="left">
                                                <asp:Label ID="Label75" Style="font-size: 12px;" runat="server" Text="Dcr Edit / Rejection Re Entry should be Allowed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiodcr_edit_rej" EnableViewState="true" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>--%>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label49" Style="font-size: 12px;" runat="server" Text="Sequential DCR Option needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiosequential_dcr" runat="server" RepeatDirection="Horizontal" OnClick="Showsequential_dcr()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label50" runat="server" Text="DCR Delayed / Lock Days" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtDcrLockDays" runat="server" Width="120" MaxLength="100" onkeypress="return CheckNumeric(event);" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label8" Style="font-size: 12px;" runat="server" Text="DCR Total Lock for Delayed Days Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio49" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label51" Style="font-size: 12px;" runat="server" Text="My Day Plan Needed"></asp:Label>
                                            </td>
                                            
            
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiomydayplan_need" runat="server" RepeatDirection="Horizontal" OnClick="Showmydayplan_need()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <%--   <tr>
                                            <td align="left">
                                                <asp:Label ID="Label24" Style="font-size: 12px;" runat="server" Text="Day Wise My Day Plan Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioCurrentday_TPplanned" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label195" Style="font-size: 12px;" runat="server" Text="My Day Plan Remark Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadiomyplnRmrksMand" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label53" Style="font-size: 12px;" runat="server" Text="Leave Application based on Leave Policy Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioLeave_entitlement_need" runat="server" RepeatDirection="Horizontal" OnClick="ShowLeaveAppOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label150" Style="font-size: 12px;" runat="server" Text="Leave Balance Status without Policy restriction"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radioleavestatus" runat="server" RepeatDirection="Horizontal" OnClick="ShowLeaveStatusOption();">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label193" Style="font-size: 12px;" runat="server" Text="Past Day Leave Apply Option is Not Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiopast_leave_post" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span3" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="Label212" runat="server" Text="Manager Options"
                                                        Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label157" Style="font-size: 12px;" runat="server" Text="APPROVAL Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioApproveneed" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label6" Font-Size="12px" runat="server" Text="Approval Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio48" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label55" Font-Size="12px" runat="server" Text="DCR Approval Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioDcrapprvNd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>

                                    <br />

                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span4" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="Label30" runat="server" Text="Check In - Check Out Options"
                                                        Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_14" Style="font-size: 12px;" runat="server" Text="Day Check IN / OUT Needed" ForeColor="#f82b00"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio14" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label71" Style="font-size: 12px;" runat="server" Text="Customerwise {Dr} Check IN / OUT Needed" ForeColor="#f82b00"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioCustSrtNd" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label87" Style="font-size: 12px;" runat="server" Text="CIP Check IN /OUT Needed" ForeColor="#f82b00"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioCipSrtNd" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span25" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="Label244" runat="server" Text="Additional DCR Options"
                                                        Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label54" Style="font-size: 12px;" runat="server" Text="Cluster name Changes"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtCluster" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label88" Style="font-size: 12px;" runat="server" Text="Add New Chemist/ Unlisted for MR"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioentryFormNeed" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label89" Style="font-size: 12px;" runat="server" Text="Add New Chemist/ Unlisted for Manager"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioentryFormMgr" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label57" Style="font-size: 12px;" runat="server" Text="Add New Chemist {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioaddChm" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label59" Style="font-size: 12px;" runat="server" Text="Add New Unlisted Doctor {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioaddDr" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label61" Style="font-size: 12px;" runat="server" Text="Add New Unlisted based on Hospital {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radioundr_hs_nd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label62" Style="font-size: 12px;" runat="server" Text="MultiCluster Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiomulti_cluster" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label67" Style="font-size: 12px;" runat="server" Text="Call DELETE option Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioshowDelete" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label191" Style="font-size: 12px;" runat="server" Text="DCR Feedback Remark Length"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <%-- <asp:RadioButtonList ID="RadiocntRemarks" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                                <asp:TextBox ID="RadiocntRemarks" runat="server" CssClass="input" min="1" Style="height: 27px; width: 120px;" MaxLength="10" TextMode="number" onkeypress="return CheckNumericMin(event);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label199" Font-Size="12px" runat="server" Text="DCR Product Remark Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radioprod_remark" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label201" Font-Size="12px" runat="server" Text="DCR Product Remark Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radioprod_remark_md" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label117" Style="font-size: 12px;" runat="server" Text="DCR Call Summary before submission"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioDcr_summary_need" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label181" Style="font-size: 12px;" runat="server" Text="Referal Doctor Option Need"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadiorefDoc" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label69" Style="font-size: 12px;" runat="server" Text="Remainder Call Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioRmdrNeed" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label91" Style="font-size: 12px;" runat="server" Text="Remainder Call Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="TxtRemainder_call_cap" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label92" Style="font-size: 12px;" runat="server" Text="Remainder Call Product Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioRemainder_prd_Md" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label205" Style="font-size: 12px;" runat="server" Text="Geo-fencing based Doctor name shows in Remaider call screen"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioRemainder_geo" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label79" Font-Size="12px" runat="server" Text="Presentation Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioPresentNd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label77" Font-Size="12px" runat="server" Text="Slides Customisation Option Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioCustNd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label81" Font-Size="12px" runat="server" Text="Therapatic Option Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiotheraptic" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label83" Font-Size="12px" runat="server" Text="Offline Calls are not Allowed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radioyetrdy_call_del_Nd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <%--<tr>
                                            <td align="left">
                                                <asp:Label ID="Label56" Font-Size="12px" runat="server" Text="All Product display {Hybrid}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio40" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>--%>
                                    </table>

                                    <br />
                                </div>
                            </div>
                            <br />
                            <div class="col-lg-6">
                                <div class="single-block-area">
                                    <table width="100%">

                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <asp:Label ID="lbldr_ent" runat="server" Text="Doctor Entry"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label96" Style="font-size: 12px;" runat="server" Text="Pob Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio75" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label2" Style="font-size: 12px;" runat="server" Text="POB Entry Option as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio11" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label237" Style="font-size: 12px;" runat="server" Text="Minimum Pob Value"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtpob_minvalue" runat="server" TextMode="number" min="1" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"
                                                    onkeypress="return CheckNumericMin(event);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label108" Style="font-size: 12px;" runat="server" Text="Joint Work Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio81" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label116" Style="font-size: 12px;" runat="server" Text="Joint Work Selection Option as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio85" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label124" Style="font-size: 12px;" runat="server" Text="Product Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtDpc" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"
                                                    ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblprd_entry_doc" runat="server" Text="Product Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoprd_entry_doc" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_7" Style="font-size: 12px;" runat="server" Text="Product Entry as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio7" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSamQty_Cap_doc" runat="server" Text="Product - Sample Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtSamQty_Cap_doc" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label175" Font-Size="12px" runat="server" Text="Product - Sample Qty Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioDrSampNd" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_9" Style="font-size: 12px;" runat="server" Text="Product - Sample Quantity Entry as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio9" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblRx_Cap_doc" runat="server" Text="Product - Rx Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtRx_Cap_doc" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_17" Style="font-size: 12px;" runat="server" Text="Product - Rx Entry Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio17" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_10" Style="font-size: 12px;" runat="server" Text="Product - Rx Qty Entry as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio10" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label56" Style="font-size: 12px;" runat="server" Text="Product - RCPA Qty Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioRCPAQty_Need" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label43" Style="font-size: 12px;" runat="server" Text="Product - RCPA Qty  mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioDrRcpaQMd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label229" Font-Size="12px" runat="server" Text="Product wise Feedback Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radioprdfdback" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label65" Style="font-size: 12px;" runat="server" Text="Product wise Feedback Selection Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio89" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label3" Style="font-size: 12px;" runat="server" Text="Product wise Stockist Selection Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioProduct_Stockist" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblProd_Stk_Need" Style="font-size: 12px;" runat="server" Text="Product wise - Common Stockist Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioProd_Stk_Need" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label159" Style="font-size: 12px;" runat="server" Text="Input Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtDoc_Input_caption" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"
                                                   ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblinput_Ent_doc" runat="server" Text="Input Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoinput_Ent_doc" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_8" Style="font-size: 12px;" runat="server" Text="Input Entry as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio8" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label44" Style="font-size: 12px;" runat="server" Text="Next visit Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio34" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label39" Style="font-size: 12px;" runat="server" Text="Next visit Entry Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio35" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label36" Style="font-size: 12px;" runat="server" Text="EventCapture Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio63" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblDrEvent_Md" Style="font-size: 12px;" runat="server" Text="EventCapture Option Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioDrEvent_Md" runat="server" RepeatDirection="Horizontal"  OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label12" Style="font-size: 12px;" runat="server" Text="Feedback Entry Options Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio51" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_6" Style="font-size: 12px;" runat="server" Text="Feed Back Entry as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio6" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label64" Style="font-size: 12px;" runat="server" Text="Remark as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTempNd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label4" Style="font-size: 12px;" runat="server" Text="DOB & DOW Details Needed in Notification"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio47" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label52" Style="font-size: 12px;" runat="server" Text="Multiple Doctor selection"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio38" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label58" Style="font-size: 12px;" runat="server" Text="Speciality wise ALL Brands details {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioSpecFilter" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label60" Style="font-size: 12px;" runat="server" Text="Dr wise - Focused Product mapping {Hybrid}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio42" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblExpenceNd" Style="font-size: 12px;" runat="server" Text="Dr wise - Expense Needed {Hybrid}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rdoExpenceNd" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblExpenceNd_mandatory" Style="font-size: 12px;" runat="server" Text="Dr wise - Expense Mandatory {Hybrid}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rdoExpenceNd_mandatory" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label5" Style="font-size: 12px;" runat="server" Text="Profiling Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioMCLDet" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_3" Style="font-size: 12px;" runat="server" Text="Category Wise Visit Control Needed"></asp:Label>
                                            </td>

                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio3" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label27" Style="font-size: 12px;" runat="server" Text="Territory Wise Visit Control Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTerritory_VstNd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label28" Style="font-size: 12px;" runat="server" Text="First call selfie needed (FDC)"></asp:Label>
                                            </td>

                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioDcr_firstselfie" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />

                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <asp:Label ID="lblstock" runat="server" Text="Stockist Entry"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNeed_stock" runat="server" Text="Needed" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoNeed_stock" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label100" Font-Size="12px" runat="server" Text="POB Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio77" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label104" Font-Size="12px" runat="server" Text="POB Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio79" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label112" Font-Size="12px" runat="server" Text="Joint Work Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio83" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label120" Font-Size="12px" runat="server" Text="Joint Work Selection Option as Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio87" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label128" Font-Size="12px" runat="server" Text="Product Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtSPC" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"
                                                    ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblprdentry_stock" runat="server" Text="Product Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoprdentry_stock" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblQty_Cap_stock" runat="server" Text="Rx Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtQty_Cap_stock" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label163" Font-Size="12px" runat="server" Text="Input Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtStk_Input_caption" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"
                                                    ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblinpu_entry_stock" runat="server" Text="Input Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoinpu_entry_stock" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label70" Font-Size="12px" runat="server" Text="EventCapture Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio65" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label11" Font-Size="12px" runat="server" Text="EventCapture Option Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioStkEvent_Md" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label16" Font-Size="12px" runat="server" Text="Feedback Entry Options Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio53" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label13" Font-Size="12px" runat="server" Text="Detailing Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioDetailing_stk" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                    </table>
                                    <br />

                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <asp:Label ID="Label131" runat="server" Text="CIP Entry"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label10" Font-Size="12px" runat="server" Text="Entry Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio50" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblvist6" runat="server" Text="CIP Caption" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtcip_caption" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label15" runat="server" Text="POB Needed" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="RadioCIPPOBNd" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label17" runat="server" Text="POB Mandatory" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="RadioCIPPOBMd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label133" runat="server" Text="Product Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="Radio90" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="Label47" runat="server" Text="Input Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="Radio91" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label32" runat="server" Text="CIP JointWork Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="RadioCIP_jointwork_Need" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label74" Font-Size="12px" runat="server" Text="EventCapture Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio67" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label19" Font-Size="12px" runat="server" Text="EventCapture Option Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioCipEvent_Md" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label18" Style="font-size: 12px;" runat="server" Text="Feedback Entry Options Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio54" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span16" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="Label211" runat="server" Text="Hospital Entry"
                                                        Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label225" Style="font-size: 12px;" runat="server" Text="Entry Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiohosp_need" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblhosp_caption" runat="server" Text="Hospital Caption" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txthosp_caption" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label21" Style="font-size: 12px;" runat="server" Text="POB Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioHosPOBNd" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label23" Style="font-size: 12px;" runat="server" Text="POB Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioHosPOBMd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label227" Style="font-size: 12px;" runat="server" Text="Product Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioHPNeed" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label223" Style="font-size: 12px;" runat="server" Text="Input Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioHINeed" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label76" Style="font-size: 12px;" runat="server" Text="EventCapture Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio68" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label25" Style="font-size: 12px;" runat="server" Text="EventCapture Option Madatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioHospEvent_Md" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label22" Style="font-size: 12px;" runat="server" Text="Feedback Entry Options Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio56" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span14" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="Label207" runat="server" Text="RCPA"
                                                        Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label68" Style="font-size: 12px;" runat="server" Text="Separate RCPA Entry Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio46" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label151" Style="font-size: 12px;" runat="server" Text="Dr. RCPA Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioRcpaNd" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label239" Style="font-size: 12px;" runat="server" Text="Dr. RCPA MR Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioRcpaMd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label38" Style="font-size: 12px;" runat="server" Text="Dr. RCPA MGR Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioRcpaMd_Mgr" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label41" Style="font-size: 12px;" runat="server" Text="Dr. RCPA Competitor Brand Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioDrRCPA_competitor_Need" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label235" Style="font-size: 12px;" runat="server" Text="Che - RCPA Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioChm_RCPA_Need" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label45" Style="font-size: 12px;" runat="server" Text="Che - RCPA MR Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioChmRcpaMd" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label48" Style="font-size: 12px;" runat="server" Text="Che. RCPA MGR Mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioChmRcpaMd_Mgr" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label85" Style="font-size: 12px;" runat="server" Text="Che. RCPA Competitor Brand Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioChmRCPA_competitor_Need" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label113" Style="font-size: 12px;" runat="server" Text="Che Product RCPA Qty Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioCPRCPAQtyNd" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label115" Style="font-size: 12px;" runat="server" Text="Che Product RCPA Qty mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioCPRCPAQtyMd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label241" Style="font-size: 12px;" runat="server" Text="For ALL - Rcpa Extra Filed for Competitor Product Details (Reason,Rate,Remark)"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioRcpa_Competitor_extra" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                    </table>

                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span10" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="Label171" runat="server" Text="Order Management" Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblOrdMgr" Style="font-size: 12px;" runat="server" Text="Order Management Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rdoOrder_management" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblOrdCap" Style="font-size: 12px;" runat="server" Text="Order Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtOrder_caption" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblOrdPriCap" Style="font-size: 12px;" runat="server" Text="Primary Order Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtPrimary_order_caption" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblOrdPriNeed" Style="font-size: 12px;" runat="server" Text="Primary Order Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rdoPrimary_order" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblOrdSecCap" Style="font-size: 12px;" runat="server" Text="Secondary Order Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtSecondary_order_caption" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblOrdSecNeed" Style="font-size: 12px;" runat="server" Text="Secondary Order Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rdoSecondary_order" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblGstOptNeed" Style="font-size: 12px;" runat="server" Text="GST Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rdoGst_option" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTaxNameCap" Style="font-size: 12px;" runat="server" Text="Tax Name Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtTaxname_caption" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblMasRateEdit" Style="font-size: 12px;" runat="server" Text="Master Product Rate Editable"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rdoProduct_Rate_Editable" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblSecOrdDis" Style="font-size: 12px;" runat="server" Text="Secondary- Order Discount"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rdosecondary_order_discount" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <%--       <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet">
                                    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.js"></script>
                                    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>--%>
                                    <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet">
                                    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.js"></script>
                                    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment.min.js"></script>
                                    <link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.37/css/bootstrap-datetimepicker.min.css" rel="stylesheet">
                                    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.37/js/bootstrap-datetimepicker.min.js"></script>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span5" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="Label95" runat="server" Text="Tracking Options"
                                                        Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label97" Font-Size="12px" runat="server" Text="Location Track Need" ForeColor="#f82b00"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioLocation_track" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label24" Font-Size="12px" runat="server" Text="Location Tracking Shift Time"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txttracking_time1" runat="server" CssClass="input" Style="height: 27px; width: 60px;" MaxLength="10"></asp:TextBox>
                                                <asp:TextBox ID="txttracking_time2" runat="server" CssClass="input" Style="height: 27px; width: 60px;" MaxLength="10"></asp:TextBox>

                                                <script type="text/javascript">

                                                    function TimePickerCtrl($) {
                                                        var startTime = $('#txttracking_time1').datetimepicker({
                                                            format: 'HH'
                                                        });

                                                        var endTime = $('#txttracking_time2').datetimepicker({
                                                            format: 'HH',
                                                            minDate: startTime.data("DateTimePicker").date(),
                                                            maxDate: moment({ hour: 23 }),
                                                        });

                                                        function setMinDate() {
                                                            return endTime
                                                                .data("DateTimePicker").minDate(
                                                                    startTime.data("DateTimePicker").date()
                                                                );
                                                        }

                                                        var bound = false;
                                                        function bindMinEndTimeToStartTime() {
                                                            return bound || startTime.on('dp.change', setMinDate);
                                                        }

                                                        endTime.on('dp.change', () => {
                                                            bindMinEndTimeToStartTime();
                                                            bound = true;
                                                            setMinDate();
                                                        });
                                                    }

                                                    $(document).ready(TimePickerCtrl);
                                                </script>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label99" Style="font-size: 12px;" runat="server" Text="Location Tracking  - Interval Time Range {min}"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="Txttracking_interval" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label101" Font-Size="12px" runat="server" Text="Travelling Distance Entry Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadiotravelDistance_Need" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <span runat="server" id="Span24" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="Label246" runat="server" Text="Other"
                                                        Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label155" Style="font-size: 12px;" runat="server" Text="Camp Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioCampneed" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label177" Style="font-size: 12px;" runat="server" Text="Campaign Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioCmpgnNeed" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label173" Style="font-size: 12px;" runat="server" Text="Survey Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiosurveynd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label103" Style="font-size: 12px;" runat="server" Text="Activity Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="txtActivityCap" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label105" Style="font-size: 12px;" runat="server" Text="Activity Needed "></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioActivityNd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                       <%-- <tr>
                                            <td align="left">
                                                <asp:Label ID="Label63" Style="font-size: 12px;" runat="server" Text="Activity Needed {E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioaddAct" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label206" Style="font-size: 12px;" runat="server" Text="Dashboard Report Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiodashboard" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label94" Style="font-size: 12px;" runat="server" Text="Quiz Caption"></asp:Label>
                                            </td>
                                            <td align="left" class="single-des">
                                                <asp:TextBox ID="Txtquiz_heading" runat="server" CssClass="input" Style="height: 27px; width: 120px;" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label84" Style="font-size: 12px;" runat="server" Text="Quiz Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio69" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label107" Style="font-size: 12px;" runat="server" Text="Quiz mandatory"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radioquiz_need_mandt" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_4" Style="font-size: 12px;" runat="server" Text="Mail Communication Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio4" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label90" Style="font-size: 12px;" runat="server" Text="Media Transfer (sharing Audio & Video)"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio72" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_5" Style="font-size: 12px;" runat="server" Text="Circular Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio5" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label66" Style="font-size: 12px;" runat="server" Text="Frequently Asked Question Option Neeed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiofaq" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label143" Style="font-size: 12px;" runat="server" Text="Change Password Option Needed{E-Detailing}"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioPwdsetup" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label86" Font-Size="12px" runat="server" Text="Product Detailing Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radio70" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label109" Font-Size="12px" runat="server" Text="Primary / Secondary Sales Stockist Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radioprimarysec_need" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label73" Font-Size="12px" runat="server" Text="Target Vs Sales & SFE View - Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTarget_report_Nd" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label111" Font-Size="12px" runat="server" Text="Sales Report Mandatory in Home Screen"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioTarget_report_md" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label145" Font-Size="12px" runat="server" Text="Expence View Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radioexpenseneed" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label168" Font-Size="12px" runat="server" Text="Miscellaneous Expense Entry Option Needed"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Radiomisc_expense_need" runat="server" RepeatDirection="Horizontal" OnClick="ShowMobOption()">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                    </table>
                                    <br />

                                </div>
                            </div>
                        </div>
                        <br />


                        <center>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button2" runat="server" CssClass="savebutton"
                                            Text="Save" OnClick="btnSubmitNew_Click" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <script type="text/javascript">
                            function SelectSubCheckboxes(chk, selector) {
                                let CountNearMe = 0;

                                let GPS = $($(chk).parent().parent().parent().find('td:eq(1)').find("[type='checkbox']")[0]);
                                let GEO = $($(chk).parent().parent().parent().find('td:eq(2)').find("[type='checkbox']")[0]);
                                let chem = $($(chk).parent().parent().parent().find('td:eq(3)').find("[type='checkbox']")[0]);
                                let stock = $($(chk).parent().parent().parent().find('td:eq(4)').find("[type='checkbox']")[0]);
                                let unlisted = $($(chk).parent().parent().parent().find('td:eq(5)').find("[type='checkbox']")[0]);
                                let CIP = $($(chk).parent().parent().parent().find('td:eq(6)').find("[type='checkbox']")[0]);

                                if (selector != ".GPS") {
                                    if (GEO[0].checked && chem[0].checked && stock[0].checked && unlisted[0].checked && CIP[0].checked) {
                                        //$(GPS).prop("checked", false);
                                        //$(GPS).prop('disabled', false);
                                    } else {
                                        $(GPS).prop("checked", true);
                                        //$(GPS).prop('disabled', true);
                                    }
                                } else {
                                    if (!$(GPS)[0].checked && !GEO[0].checked && !chem[0].checked && !stock[0].checked && !unlisted[0].checked && !CIP[0].checked)
                                        $(GPS).prop("checked", false);
                                    else
                                        $(GPS).prop("checked", true);
                                }

                            <%--    $('#<%=grdgps.ClientID%>').find(".GPS" + " input:checkbox").each(function () {
                                    if ($(this)[0].checked) { CountNearMe += 1; }
                                });

                                if (document.getElementById("Radio14_0").checked || document.getElementById("rdoTerr_based_Tag_0").checked || document.getElementById("RadioCustSrtNd_0").checked || document.getElementById("RadioCipSrtNd_0").checked || document.getElementById("RadioLocation_track_0").checked || CountNearMe > 0) {
                                    $('#rdomandt').find("input[value='0']").prop("checked", true);
                                    $("#rdomandt").find('input').prop('disabled', true);
                                    //document.getElementById("hdnmandt").value = "0";
                                } else {
                                   // $('#rdomandt').find("input[value='1']").prop("checked", true);
                                    $("#rdomandt").find('input').prop('disabled', false);
                                }--%>
                            }
                            function SelectSubCheckboxesAll(chk, selector) {
                                $('#<%=grdgps.ClientID%>').find(selector + " input:checkbox").each(function () {
                                    let GPS = $($(this).closest('tr').find('td')[1]).find("[type='checkbox']")[0]
                                    let GEO = $($(this).closest('tr').find('td')[2]).find("[type='checkbox']")[0]
                                    let chem = $($(this).closest('tr').find('td')[3]).find("[type='checkbox']")[0]
                                    let stock = $($(this).closest('tr').find('td')[4]).find("[type='checkbox']")[0];
                                    let unlisted = $($(this).closest('tr').find('td')[5]).find("[type='checkbox']")[0]
                                    let CIP = $($(this).closest('tr').find('td')[6]).find("[type='checkbox']")[0]

                                    //if (selector == ".GPS") {
                                        if (!$(GPS)[0].checked && !GEO.checked && !chem.checked && !stock.checked && !unlisted.checked && !CIP.checked) {
                                             $(GPS).prop("checked", false);
                                        }
                                        else {
                                            $(GPS).prop("checked", true);
                                            // 
                                        }
                                    //}
                                    //$(this).prop("checked", $(chk).prop("checked")); 
                                });
                                
                            }
                        </script>
                        <div>
                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel"
                                PopupControlID="pnlpopup" TargetControlID="linkgps" BackgroundCssClass="modalBackgroundNew">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlpopup" runat="server" BackColor="white"
                                class="ontop" Style="position: absolute; display: none;">
                                <%--Height="480px" Width="470px"--%>
                                <table width="100%" style="border: Solid 3px #4682B4;" cellpadding="0"
                                    cellspacing="0">
                                    <tr style="background-color: #4682B4;">
                                        <td style="height: 10%; color: White; font-weight: bold; font-size: larger;" align="center">
                                            <asp:Label ID="lblHead" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10%; color: White; font-weight: bold; font-size: larger" align="center">&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="height: 50%; color: black; font-weight: bold; font-size: 20pt">
                                            <asp:Label ID="lblgps" Font-Bold="true" Font-Size="20px" runat="server" Text="GEO Tagging & Fencing"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <div align="center" style="width: 750px; height: 400px; overflow-x: auto;">

                                                                    <div class="display-table clearfix">
                                                                        <div class="table-responsive">
                                                                            <asp:GridView ID="grdgps" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center"
                                                                                GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-Width="300px">

                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label></a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Sf_Code" Visible="false">

                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText=" Near me {Tag Needed}" HeaderStyle-ForeColor="#f82b00">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll" runat="server" Text=" Near me {Tag Needed}" onclick="SelectAllCheckboxes(this, '.GPS')" HeaderStyle-ForeColor="#f82b00" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkId" runat="server" Font-Bold="true" CssClass="GPS" Text="."
                                                                                                onclick="SelectSubCheckboxes(this, '.GPS')" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="Listed Dr GEO Fencing">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll_G" runat="server" Text="Listed Dr GEO Fencing"
                                                                                                onclick="SelectAllCheckboxes(this, '.GEO')" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkfencing" runat="server" Font-Bold="true" CssClass="GEO" Text="." onclick="SelectSubCheckboxes(this, '.GEO')" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="Chemist GEO Fencing">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll_chem" runat="server" Text="Chemist GEO Fencing" onclick="SelectAllCheckboxes(this, '.chem')" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkfencingche" runat="server" Font-Bold="true" CssClass="chem" Text="." onclick="SelectSubCheckboxes(this, '.chem')" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="Stockist GEO Fencing">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll_stock" runat="server" Text="Stockist GEO Fencing" onclick="SelectAllCheckboxes(this, '.stock')" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkfencingstock" runat="server" Font-Bold="true" CssClass="stock" Text="." onclick="SelectSubCheckboxes(this, '.stock')" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="Unlisted GEO Fencing">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll_Unlisted" runat="server" Text="Unlisted GEO Fencing" onclick="SelectAllCheckboxes(this, '.unlisted')" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkfencingUnlisted" runat="server" Font-Bold="true" CssClass="unlisted" Text="." onclick="SelectSubCheckboxes(this, '.unlisted')" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="CIP GEO Fencing">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll_CIP" runat="server" Text="CIP GEO Fencing" onclick="SelectAllCheckboxes(this, '.CIP')" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkfencingCIP" runat="server" Font-Bold="true" CssClass="CIP" Text="." onclick="SelectSubCheckboxes(this, '.CIP')" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="padding-top: 5px; padding-bottom: 5px;">
                                            <asp:Button ID="btnUpdate" CommandName="Update" runat="server"
                                                CssClass="savebutton" Text="Save Setting" OnClick="btnUpdate_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                                CssClass="resetbutton" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>


            <br />
            <br />

        </div>
        <script type="text/javascript">
            $(document).ready(function () {
                ShowMobOption();
                ShowLeaveAppOption();
                Showsequential_dcr();
            });
            function ShowMobOption() {

                //Doctor Entry
                if (document.getElementById("rdoprd_entry_doc_0").checked) {
                    $("#<%=Radio7.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=RadioDrSampNd.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=Radio17.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=Radio17.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=RadioRCPAQty_Need.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=Radioprdfdback.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=RadioProd_Stk_Need.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio7.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio7.ClientID %>').find("input[value='0']").prop("checked", true);
                    $("#<%=RadioDrSampNd.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioDrSampNd.ClientID %>').find("input[value='0']").prop("checked", true);
                    $("#<%=Radio17.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio17.ClientID %>').find("input[value='0']").prop("checked", true);
                    $("#<%=RadioRCPAQty_Need.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioRCPAQty_Need.ClientID %>').find("input[value='0']").prop("checked", true);
                    $("#<%=Radioprdfdback.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radioprdfdback.ClientID %>').find("input[value='1']").prop("checked", true);
                    $("#<%=RadioProd_Stk_Need.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioProd_Stk_Need.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio17_0").checked) {
                    $("#<%=Radio10.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio10.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio10.ClientID %>').find("input[value='0']").prop("checked", true);
                }

                if (document.getElementById("rdoinput_Ent_doc_0").checked) {
                    $("#<%=Radio8.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio8.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio8.ClientID %>').find("input[value='0']").prop("checked", true);
                }

                if (document.getElementById("Radio75_0").checked) {
                    $("#<%=Radio11.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio11.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio11.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio81_0").checked) {
                    $("#<%=Radio85.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio85.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio85.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio51_0").checked) {
                    $("#<%=Radio6.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio6.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio6.ClientID %>').find("input[value='0']").prop("checked", true);
                }

                if (document.getElementById("Radioprdfdback_0").checked) {
                    $("#<%=Radio89.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio89.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio89.ClientID %>').find("input[value='1']").prop("checked", true);

                }

                if (document.getElementById("Radio34_0").checked) {
                    $("#<%=Radio35.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio35.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio35.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("rdoExpenceNd_0").checked) {
                    $("#<%=rdoExpenceNd_mandatory.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=rdoExpenceNd_mandatory.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=rdoExpenceNd_mandatory.ClientID %>').find("input[value='1']").prop("checked", true);
                }


                if (document.getElementById("RadioDrSampNd_0").checked) {
                    $("#<%=Radio9.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio9.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio9.ClientID %>').find("input[value='0']").prop("checked", true);
                }

                if (document.getElementById("RadioDcr_firstselfie_0").checked) {
                    $('#<%=RadioDrEvent_Md.ClientID %>').find("input[value='0']").prop("checked", true);
                    $('#<%=Radio63.ClientID %>').find("input[value='0']").prop("checked", true);
                    document.getElementById("<%=Radio63.ClientID %>").disabled = true;
                    document.getElementById("<%=RadioDrEvent_Md.ClientID %>").disabled = true;
                }
                else {
                    document.getElementById("<%=RadioDrEvent_Md.ClientID %>").disabled = false;
                    //$("#<%=RadioDrEvent_Md.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=Radio63.ClientID %>").find('input').prop('disabled', false);
                    if (document.getElementById("Radio63_0").checked) {
                        $("#<%=RadioDrEvent_Md.ClientID %>").find('input').prop('disabled', false);
                    }
                    else {
                        $("#<%=RadioDrEvent_Md.ClientID %>").find('input').prop('disabled', true);
                        $('#<%=RadioDrEvent_Md.ClientID %>').find("input[value='1']").prop("checked", true);
                    }
                }
                //end Doctor Entry

                //Chemist Entry
                if (document.getElementById("Radio76_0").checked) {
                    $("#<%=Radio16.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio16.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio16.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio82_0").checked) {
                    $("#<%=Radio86.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio86.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio86.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio64_0").checked) {
                    $("#<%=RadioChmEvent_Md.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=RadioChmEvent_Md.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioChmEvent_Md.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("rdoProduct_entr_chem_0").checked) {
                    $("#<%=RadiochmsamQty_need.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=RadioChmRxNd.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=RadioCPRCPAQtyNd.ClientID %>").find('input').prop('disabled', false);

                }
                else {
                    $("#<%=RadiochmsamQty_need.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadiochmsamQty_need.ClientID %>').find("input[value='1']").prop("checked", true);
                    $("#<%=RadioChmRxNd.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioChmRxNd.ClientID %>').find("input[value='1']").prop("checked", true);
                    $("#<%=RadioCPRCPAQtyNd.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioCPRCPAQtyNd.ClientID %>').find("input[value='1']").prop("checked", true);
                }
                //End Chemist Entry

                //Stocist Entry
                if (document.getElementById("Radio77_1").checked) {
                    $("#<%=Radio79.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio79.ClientID %>').find("input[value='0']").prop("checked", true);

                }
                else {
                    $("#<%=Radio79.ClientID %>").find('input').prop('disabled', false);
                }

                if (document.getElementById("Radio83_0").checked) {
                    $("#<%=Radio87.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio87.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio87.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio77_0").checked) {
                    $("#<%=Radio79.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio79.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio79.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio65_0").checked) {
                    $("#<%=RadioStkEvent_Md.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=RadioStkEvent_Md.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioStkEvent_Md.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("RadioCIPPOBNd_0").checked) {
                    $("#<%=RadioCIPPOBMd.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=RadioCIPPOBMd.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioCIPPOBMd.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio67_0").checked) {
                    $("#<%=RadioCipEvent_Md.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=RadioCipEvent_Md.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioCipEvent_Md.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                //Hospital Entry
                if (document.getElementById("RadioHosPOBNd_0").checked) {
                    $("#<%=RadioHosPOBMd.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=RadioHosPOBMd.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioHosPOBMd.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio68_0").checked) {
                    $("#<%=RadioHospEvent_Md.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=RadioHospEvent_Md.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioHospEvent_Md.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                //Unlisted Dr. Entry
                if (document.getElementById("Radio78_0").checked) {
                    $("#<%=Radio80.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio80.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio80.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio84_0").checked) {
                    $("#<%=Radio88.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio88.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio88.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio66_0").checked) {
                    $("#<%=RadioUlDrEvent_Md.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=RadioUlDrEvent_Md.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioUlDrEvent_Md.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                //RCPA
                if (document.getElementById("RadioRcpaNd_0").checked) {
                    $("#<%=RadioRcpaMd.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=RadioRcpaMd_Mgr.ClientID %>").find('input').prop('disabled', false);

                }
                else {
                    $("#<%=RadioRcpaMd.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioRcpaMd.ClientID %>').find("input[value='1']").prop("checked", true);
                    $("#<%=RadioRcpaMd_Mgr.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioRcpaMd_Mgr.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("RadioChm_RCPA_Need_0").checked) {
                    $("#<%=RadioChmRcpaMd.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=RadioChmRcpaMd_Mgr.ClientID %>").find('input').prop('disabled', false);

                }
                else {
                    $("#<%=RadioChmRcpaMd.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioChmRcpaMd.ClientID %>').find("input[value='1']").prop("checked", true);
                    $("#<%=RadioChmRcpaMd_Mgr.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioChmRcpaMd_Mgr.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("RadioRCPAQty_Need_0").checked) {
                    $("#<%=RadioDrRcpaQMd.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=RadioDrRcpaQMd.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioDrRcpaQMd.ClientID %>').find("input[value='0']").prop("checked", true);
                }



                //Manager Options
                if (document.getElementById("RadioApproveneed_0").checked) {
                    $("#<%=Radio48.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radio48.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio48.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                //Additional Options
                if (document.getElementById("Radioprod_remark_0").checked) {
                    $("#<%=Radioprod_remark_md.ClientID %>").find('input').prop('disabled', false);
                }
                else {
                    $("#<%=Radioprod_remark_md.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radioprod_remark_md.ClientID %>').find("input[value='1']").prop("checked", true);
                }

                if (document.getElementById("Radio33_1").checked) {
                    $('#<%=Radio31.ClientID %>').find("input[value='1']").prop("checked", true);
                    $("#<%=Radio31.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=Radio92.ClientID %>').find("input[value='1']").prop("checked", true);
                    $("#<%=Radio92.ClientID %>").find('input').prop('disabled', true);
                    $("#<%=Radiotp_need.ClientID %>").find('input').prop('disabled', false);
                    <%--document.getElementById("<%=Radio32.ClientID %>").disabled = true;
                    $('#<%=Radio32.ClientID %>').find("input[value='0']").prop("checked", true);--%>
                    if (document.getElementById("Radiotp_need_0").checked) {
                        $("#<%=Radio32.ClientID %>").find('input').prop('disabled', false);
                    }
                    else {
						document.getElementById("<%=Radio32.ClientID %>").disabled = true;
                        $('#<%=Radio32.ClientID %>').find("input[value='1']").prop("checked", true);
                    }
                }
                else {
                    $("#<%=Radio92.ClientID %>").find('input').prop('disabled', false);
                    $("#<%=Radio31.ClientID %>").find('input').prop('disabled', false);
                    $('#<%=Radiotp_need.ClientID %>').find("input[value='0']").prop("checked", true);
                    document.getElementById("<%=Radiotp_need.ClientID %>").disabled = true;
                    $('#<%=Radio32.ClientID %>').find("input[value='0']").prop("checked", true);
                    document.getElementById("<%=Radio32.ClientID %>").disabled = true;
                }

                if (document.getElementById("Radio31_1").checked) {
                    $('#Radio92').find("input[value='1']").prop("checked", true);
                    $("#Radio92").find('input').prop('disabled', true);
                }
                else {
                    $("#Radio92").find('input').prop('disabled', false);
                }

                if (document.getElementById("RadioDrNeed_1").checked && document.getElementById("RadioTpChmNeed_1").checked &&
                    document.getElementById("RadioTpStkNeed_1").checked && document.getElementById("RadioTpJWNeed_1").checked &&
                    document.getElementById("RadioTpHospNeed_1").checked && document.getElementById("RadioTpCip_Need_1").checked) {
                    $("#<%=RadioTpFW_meetup_mandatory.ClientID %>").find('input').prop('disabled', true);
                    $('#<%=RadioTpFW_meetup_mandatory.ClientID %>').find("input[value='1']").prop("checked", true);

                }
                else {
                    $("#<%=RadioTpFW_meetup_mandatory.ClientID %>").find('input').prop('disabled', false);
                }

                if (document.getElementById("RadioCPRCPAQtyNd_0").checked) {
                    $("#RadioCPRCPAQtyMd").find('input').prop('disabled', false);
                }
                else {
                    $('#RadioCPRCPAQtyMd').find("input[value='1']").prop("checked", true);
                    $("#RadioCPRCPAQtyMd").find('input').prop('disabled', true);
                }

                

                

                if (document.getElementById("RadioRmdrNeed_0").checked) {
                    $("#RadioRemainder_prd_Md").find('input').prop('disabled', false);
                }
                else {
                    $('#RadioRemainder_prd_Md').find("input[value='1']").prop("checked", true);
                    $("#RadioRemainder_prd_Md").find('input').prop('disabled', true);
                }

                if (document.getElementById("Radio69_0").checked) {
                    $("#Radioquiz_need_mandt").find('input').prop('disabled', false);
                }
                else {
                    $('#Radioquiz_need_mandt').find("input[value='1']").prop("checked", true);
                    $("#Radioquiz_need_mandt").find('input').prop('disabled', true);
                }

                if (document.getElementById("Radio69_0").checked) {
                    $("#Radioquiz_need_mandt").find('input').prop('disabled', false);
                }
                else {
                    $('#Radioquiz_need_mandt').find("input[value='1']").prop("checked", true);
                    $("#Radioquiz_need_mandt").find('input').prop('disabled', true);
                }

                if (document.getElementById("rdoOrder_management_0").checked) {
                    $("#rdoPrimary_order").find('input').prop('disabled', false);
                    $("#rdoSecondary_order").find('input').prop('disabled', false);

                }
                else {
                    $('#rdoPrimary_order').find("input[value='1']").prop("checked", true);
                    $("#rdoPrimary_order").find('input').prop('disabled', true);
                    $('#rdoSecondary_order').find("input[value='1']").prop("checked", true);
                    $("#rdoSecondary_order").find('input').prop('disabled', true);
                }

                if (document.getElementById("Radio49_1").checked)
                {
                    $("#Radio2").find('input').prop('disabled', false);
                    document.getElementById("<%=RadioBtnList1.ClientID %>").disabled = false;
                    //$("#RadioBtnList1").find('input').prop('disabled', false);

                    if (document.getElementById("Radio2_0").checked) {
                        //$("#<%=RadioBtnList1.ClientID %>").find('input').prop('disabled', false);
                        document.getElementById("<%=RadioBtnList1.ClientID %>").disabled = false;
                    }
                    else {
                        document.getElementById("<%=RadioBtnList1.ClientID %>").disabled = true;
                        //$("#<%=RadioBtnList1.ClientID %>").find('input').prop('disabled', true);
                        $('#<%=RadioBtnList1.ClientID %>').find("input[value='1']").prop("checked", true);
                        
                    }
                }
                else
                {
                    document.getElementById("<%=Radio2.ClientID %>").disabled = true;
                    $('#Radio2').find("input[value='0']").prop("checked", true);
                    //$("#Radio2").find('input').prop('disabled', true);
                    $('#RadioBtnList1').find("input[value='0']").prop("checked", true);
                    //$("#RadioBtnList1").find('input').prop('disabled', true);
                    document.getElementById("<%=RadioBtnList1.ClientID %>").disabled = true;
                }

                if (document.getElementById("Radio14_0").checked || document.getElementById("rdoTerr_based_Tag_0").checked || document.getElementById("RadioCustSrtNd_0").checked || document.getElementById("RadioCipSrtNd_0").checked || document.getElementById("RadioLocation_track_0").checked ||
                    document.getElementById("hdnmandt").value == "1") {
                    $('#rdomandt').find("input[value='0']").prop("checked", true);
                    $("#rdomandt").find('input').prop('disabled', true);
                } else {
                    //$('#rdomandt').find("input[value='1']").prop("checked", true);
                    $("#rdomandt").find('input').prop('disabled', false);
                }
            }
            
            


            function ShowLeaveAppOption() {
                if (document.getElementById("RadioLeave_entitlement_need_0").checked) {
                    $('#Radioleavestatus').find("input[value='1']").prop("checked", true);
                }
            }

            function ShowLeaveStatusOption() {
                if (document.getElementById("Radioleavestatus_0").checked) {
                    $('#RadioLeave_entitlement_need').find("input[value='1']").prop("checked", true);
                }
            }

            function Showsequential_dcr() {
                if (document.getElementById("Radiosequential_dcr_0").checked) {
                    $('#Radiomydayplan_need').find("input[value='1']").prop("checked", true);
                    $('#RadiomyplnRmrksMand').find("input[value='1']").prop("checked", true);
                    $("#RadiomyplnRmrksMand").find('input').prop('disabled', true);
                }
                else {
                    $("#Radiomydayplan_need").find('input').prop('disabled', false);
                    //$("#RadiomyplnRmrksMand").find('input').prop('disabled', false);
                    //$('#Radiomydayplan_need').find("input[value='0']").prop("checked", true);
                    //$('#RadiomyplnRmrksMand').find("input[value='0']").prop("checked", true);
                }
            }
            function Showmydayplan_need() {
                if (document.getElementById("Radiomydayplan_need_0").checked) {
                    $("#RadiomyplnRmrksMand").find('input').prop('disabled', false);
                    $('#Radiosequential_dcr').find("input[value='1']").prop("checked", true);
                }
                else {
                    $('#RadiomyplnRmrksMand').find("input[value='1']").prop("checked", true);
                    $("#RadiomyplnRmrksMand").find('input').prop('disabled', true);
                    //$('#Radiosequential_dcr').find("input[value='0']").prop("checked", true);
                }
            }

        </script>
    </form>
</body>
</html>


