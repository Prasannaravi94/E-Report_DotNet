<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mob_App_Setting.aspx.cs"
    Inherits="MasterFiles_Options_Mob_App_Setting" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Apps Settings</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/Grid.css" />--%>
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <style type="text/css">
        .spc {
            padding-left: 5%;
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
            font-size:11px;
        }

        .display-table #grdgps tr:nth-child(2) td:first-child ,.display-table #grdgps tr td:first-child{
            background-color: white;
            color: #636d73;
            padding: 5px 0px;
            border-top: 1px solid #dee2e6;
        }
        .display-table #grdgps th [type="checkbox"]:not(:checked) + label,.display-table #grdgps  th [type="checkbox"]:checked + label
        {
            padding-left: 1.05em;
        }
         .display-table #grdgps tr td [type="checkbox"]:not(:checked) + label,.display-table #grdgps tr td [type="checkbox"]:checked + label
         {
             color:white;
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
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />



            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <h2 class="text-center" id="hHeading" runat="server"></h2>
                        <center>
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton"
                                Text="Save" OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;
                          <asp:Button ID="btnClear" runat="server" CssClass="resetbutton"
                              Text="Clear" OnClick="btnClear_Click" />
                        </center>
                        <br />

                        <div class="row justify-content-center">
                            <div class="col-lg-6">
                                <div class="single-block-area">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblloca" runat="server" Text="LOCATION" Width="100%"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                            <td>
                                                <%--  <asp:UpdatePanel ID="updateP" runat="server">
                            <ContentTemplate>
                            <table >
                          <tr>
                          <td>--%>
                                                <asp:LinkButton ID="linkgps" Text="GPS Setting" runat="server"></asp:LinkButton>
                                                <%-- </td>
                                </tr>
                                  </table>
                                   </ContentTemplate>
                                         <Triggers>
                         
                           <asp:PostBackTrigger ControlID="linkgps" />
    
               </Triggers>
                        </asp:UpdatePanel>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblmandat" runat="server" Text="Mandatory" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdomandt" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblgeo" runat="server" Text="GEO Tag" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdogeo" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
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
                                            <td class="spc">
                                                <asp:Label ID="lblcover" runat="server" Text="Coverage" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="style2 single-des">
                                                <asp:TextBox ID="txtcover" runat="server" Width="90" CssClass="input" Height="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span runat="server" id="Span1" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="lblcap" runat="server" Text="CAPTIONS" Font-Bold="True"
                                                        Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblvist1" runat="server" Text="Visit Type 1" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtvisit1" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblvist2" runat="server" Text="Visit Type 2" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtvisit2" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblvist3" runat="server" Text="Visit Type 3" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtvisit3" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblvist4" runat="server" Text="Visit Type 4" Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtvisit4" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span runat="server" id="Span2" style="font-weight: bold; border-color: #FF0000; color: MediumVioletRed">
                                                    <asp:Label ID="lblHalfday" runat="server" Text="HALFDAY WORK"
                                                        Font-Bold="True" Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
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
                                            <td class="spc" align="center">
                                                <div style="padding: 2px; overflow: auto;">
                                                    <asp:CheckBoxList class="bor" ID="chkhaf_work" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>
                                                <%--   <asp:CheckBoxList ID="chkhaf_work" style="OVERFLOW-Y:scroll; WIDTH:200px; HEIGHT:200px" CssClass="chkboxLocation" RepeatDirection="vertical" RepeatColumns="1"
                                    runat="server">
                                </asp:CheckBoxList>--%>
                                            </td>
                                            <td class="spc" align="center">
                                                <div style="padding: 2px; overflow: auto;">
                                                    <asp:CheckBoxList class="bor" ID="chkhaf_work_mgr" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>

                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="single-block-area">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <span runat="server" id="Span5" style="font-weight: bold; color: MediumVioletRed">
                                                    <asp:Label ID="lblslide" runat="server" Text="SLIDES" Font-Bold="true"
                                                        Font-Size="14px"> </asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbldr_ent" runat="server" Text="Doctor Entry"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblprd_entry_doc" runat="server" Text="Product Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoprd_entry_doc" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblRx_Cap_doc" runat="server" Text="Rx Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtRx_Cap_doc" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblSamQty_Cap_doc" runat="server" Text="Sample Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtSamQty_Cap_doc" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblinput_Ent_doc" runat="server" Text="Input Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoinput_Ent_doc" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblChem" runat="server" Text="Chemist Entry"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblNeed_chem" runat="server" Text="Needed" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoNeed_chem" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblProduct_entr_chem" runat="server" Text="Product Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoProduct_entr_chem" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblqty_Cap_chem" runat="server" Text="Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtqty_Cap_chem" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblinpu_entry_chem" runat="server" Text="Input Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoinpu_entry_chem" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblstock" runat="server" Text="Stockist Entry"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
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
                                            <td class="spc">
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
                                            <td class="spc">
                                                <asp:Label ID="lblQty_Cap_stock" runat="server" Text="Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtQty_Cap_stock" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
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
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblUnlisr_Dr" runat="server" Text="Unlisted Dr. Entry"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
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
                                            <td class="spc">
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
                                            <td class="spc">
                                                <asp:Label ID="lblRxQty_Cap_unlistDr" runat="server" Text="Rx Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtRxQty_Cap_unlistDr" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="lblSamQty_Cap_unlistDr" runat="server" Text="Sample Qty Caption"
                                                    Font-Size="12px"> </asp:Label>
                                            </td>
                                            <td class="single-des">
                                                <asp:TextBox ID="txtSamQty_Cap_unlistDr" runat="server" Width="120" CssClass="input" Height="27px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="spc">
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

                                        <%--kkkk--%>
                                        <tr>
                                            <td class="break"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label131" runat="server" Text="CIP Entry"
                                                    ForeColor="MediumVioletRed" Font-Bold="True"
                                                    Font-Size="14px"> </asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="spc">
                                                <asp:Label ID="Label133" runat="server" Text="Product Entry Needed"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="Radio90" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="spc">
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

                                    </table>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row justify-content-center">
                            <div class="col-lg-12">

                                <div class="display-reporttable clearfix">
                                    <div class="table-responsive">
                                        <table width="100%" align="center" class="table">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label1" runat="server" Text="#" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblhead2" runat="server" Text="OBJECTIVE" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblhead3" runat="server" Text="YES/NO" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sno" runat="server" Text="1"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl1" runat="server" Text="RCPA Entry Option as Mandatory"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sno1" runat="server" Text="2"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl2" runat="server" Text="'Missed Date' Entry Option Needed in Mobile App"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio2" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sno2" runat="server" Text="3"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_3" runat="server" Text="Doctor Categorywise Visist Control Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio3" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sno3" runat="server" Text="4"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_4" runat="server" Text="Internal Communication Option Needed"></asp:Label>
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
                                                    <asp:Label ID="lbl_sno4" runat="server" Text="5"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_5" runat="server" Text="Circular Option Needed"></asp:Label>
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
                                                    <asp:Label ID="lbl_sno5" runat="server" Text="6"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_6" runat="server" Text="Dr-Call Feed Back Entry as Mandatory"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio6" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sno6" runat="server" Text="7"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_7" runat="server" Text="Dr-Call Product Entry as Mandatory"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio7" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sno7" runat="server" Text="8"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_8" runat="server" Text="Dr-Call Input Entry as Mandatory"></asp:Label>
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
                                                    <asp:Label ID="lbl_sno8" runat="server" Text="9"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_9" runat="server" Text="Dr-Call Sample Quantity Entry as Mandatory"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio9" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sno9" runat="server" Text="10"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_10" runat="server" Text="Dr-Call-Rx Quantity Entry Needed"></asp:Label>
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
                                                    <asp:Label ID="lbl_sno17" runat="server" Text="11"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_17" runat="server" Text="Dr-Call-Rx Entry as Needed/Not"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio17" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <%--<tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno10" runat="server" Text="12"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_11" runat="server" Text="Dr-Call-Rx Quantity Caption"></asp:Label>
          
      </td>
              <td align="left">
                 <asp:TextBox ID="txtrxqty" runat="server" MaxLength="20" Text="Rx Qty"></asp:TextBox>
             </td>
         </tr>
          <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>--%>
                                            <%-- <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno11" runat="server" Text="13"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_12" runat="server" Text="Dr-Call-Sample Quantity Caption"></asp:Label>
          
      </td>
              <td align="left">
                 <asp:TextBox ID="txtsampqty" runat="server" MaxLength="20" Text="Sample Qty"></asp:TextBox>
             </td>
         </tr>--%>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sno12" runat="server" Text="12"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label2" runat="server" Text="Dr-POB Entry Option as Mandatory"></asp:Label>
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
                                                    <asp:Label ID="lbl_sno13" runat="server" Text="13"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_14" runat="server" Text="'Start' & 'Stop' (for Attendance)button display"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio14" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sno14" runat="server" Text="14"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_15" runat="server" Text="MCL Details updation Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio15" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sno15" runat="server" Text="15"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="llb_16" runat="server" Text="Chemist-POB Entry Option as Mandatory"></asp:Label>
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
                                                    <asp:Label ID="Label77" runat="server" Text="16"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label78" runat="server" Text="TP Based DCR  - Deviation"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio31" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label41" runat="server" Text="17"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label42" runat="server" Text="TP Entry mandatory"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio32" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label79" runat="server" Text="18"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label80" runat="server" Text="TP Start date Range"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_srtdate" runat="server" CssClass="input" Width="80px" MaxLength="10"
                                                        onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label81" runat="server" Text="19"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label82" runat="server" Text="TP End date Range"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_enddate" runat="server" CssClass="input" Width="80px" MaxLength="10"
                                                        onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label45" runat="server" Text="20"></asp:Label>
                                                </td>

                                                <td align="left">
                                                    <asp:Label ID="Label46" runat="server" Text="TP based my day plan"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio33" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label43" runat="server" Text="21"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label44" runat="server" Text="Dr next visit needed /Not Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio34" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label38" runat="server" Text="22"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label39" runat="server" Text="Dr next visit Entry Mandatory"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio35" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>



                                            <%-- <tr>
             <td align="left">
                  <asp:Label ID="Label47" runat="server" Text="25"  Height="30px"></asp:Label>
             </td>



      <td align="left">
          <asp:Label ID="Label48" runat="server" Text="All Approval  Mandatory"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio36" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
     
               <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>--%>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label49" runat="server" Text="23"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label50" runat="server" Text="RCPA Qty Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio37" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label51" runat="server" Text="24"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label52" runat="server" Text="Multiple  Doctor selection"></asp:Label>
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
                                                    <asp:Label ID="Label53" runat="server" Text="25"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label54" runat="server" Text="Cluster name Changes"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio39" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label55" runat="server" Text="26"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label56" runat="server" Text="All Product display"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio40" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>



                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label57" runat="server" Text="27"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label58" runat="server" Text="Dr speciality wise product details"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio41" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>






                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label59" runat="server" Text="28"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label60" runat="server" Text="Focused Product - Dr wise product mapping"></asp:Label>
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
                                                    <asp:Label ID="Label61" runat="server" Text="29"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label62" runat="server" Text="Product - screen Stockist needed/not needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio43" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>



                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label63" runat="server" Text="30"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label64" runat="server" Text="Other Text box needed in DCR entry page"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio44" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>



                                            <%-- <tr>
             <td align="left">
                  <asp:Label ID="Label65" runat="server" Text="34"  Height="30px"></asp:Label>
             </td>



      <td align="left">
          <asp:Label ID="Label66" runat="server" Text="Missed Date Having > 48hrs(2days) the Current Date DCR has beed Locked"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio45" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>

    

          <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>--%>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label67" runat="server" Text="31"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label68" runat="server" Text="Separate RCPA entry Needed /Not"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio46" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <%--addes new here--%>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label3" runat="server" Text="32"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label4" runat="server" Text="Doctor DOB & DOW Needed"></asp:Label>
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
                                                    <asp:Label ID="Label5" runat="server" Text="33"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label6" runat="server" Text="Tour Plan Approval Mandatory"></asp:Label>
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
                                                    <asp:Label ID="Label7" runat="server" Text="34"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label8" runat="server" Text="Delayed Days Control Option Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio49" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label9" runat="server" Text="35"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label10" runat="server" Text="CIP Entry Option Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio50" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>





                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label11" runat="server" Text="36"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label12" runat="server" Text="Doctor Feedback Entry Options Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio51" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>



                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label13" runat="server" Text="37"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label14" runat="server" Text="Chemist Feedback Entry Options Needed"></asp:Label>
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
                                                    <asp:Label ID="Label15" runat="server" Text="38"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label16" runat="server" Text="Stockist Feedback Entry Options Needed"></asp:Label>
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
                                                    <asp:Label ID="Label17" runat="server" Text="39"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label18" runat="server" Text="Unlisted Feedback Entry Options Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio54" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label19" runat="server" Text="40"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label20" runat="server" Text="CIP Feedback Entry Options Needed"></asp:Label>
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
                                                    <asp:Label ID="Label21" runat="server" Text="41"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label22" runat="server" Text="Hospital Feedback Entry Options Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio56" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label23" runat="server" Text="42"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label24" runat="server" Text="Doctorwise Questionaire Updation Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio57" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label25" runat="server" Text="43"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label26" runat="server" Text="Chemist Questionaire Updation Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio58" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label27" runat="server" Text="44"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label28" runat="server" Text="Stockist Questionaire Updation Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio59" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label29" runat="server" Text="45"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label30" runat="server" Text="Unlisted Questionaire Updation Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio60" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label31" runat="server" Text="46"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label32" runat="server" Text="CIP Questionaire Updation Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio61" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label33" runat="server" Text="47"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label34" runat="server" Text="Hospital Questionaire Updation Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio62" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label35" runat="server" Text="48"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label36" runat="server" Text="Doctor EventCapture Option Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio63" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label37" runat="server" Text="49"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label40" runat="server" Text="Chemist EventCapture Option Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio64" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label69" runat="server" Text="50"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label70" runat="server" Text="Stockist EventCapture Option Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio65" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label71" runat="server" Text="51"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label72" runat="server" Text="Unlisted EventCapture Option Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio66" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label73" runat="server" Text="52"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label74" runat="server" Text="CIP EventCapture Option Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio67" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label75" runat="server" Text="53"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label76" runat="server" Text="Hospital EventCapture Option Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio68" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label83" runat="server" Text="54"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label84" runat="server" Text="Online  Quiz Entry Option Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio69" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label85" runat="server" Text="55"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label86" runat="server" Text="Product Detailing Option Needed"></asp:Label>
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
                                                    <asp:Label ID="Label87" runat="server" Text="56"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label88" runat="server" Text="Product wise Feedback Selection while Selection the Doctor"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio71" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <%---------%>



                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label48" runat="server" Text="57"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label65" runat="server" Text="Product wise Feedback Selection Mandatory"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio89" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <%--------%>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label89" runat="server" Text="58"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label90" runat="server" Text="Media Transfer (sharing Audio & Vedio)"></asp:Label>
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
                                                    <asp:Label ID="Label91" runat="server" Text="59"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label92" runat="server" Text="Speciality wise Slide Filter"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio73" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label95" runat="server" Text="60"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label96" runat="server" Text="Doctor Pob Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio75" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label97" runat="server" Text="61"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label98" runat="server" Text="Chemist Pob Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio76" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label99" runat="server" Text="62"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label100" runat="server" Text="Stockist Pob Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio77" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label101" runat="server" Text="63"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label102" runat="server" Text="Unlisted Pob Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio78" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label103" runat="server" Text="64"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label104" runat="server" Text="Stockist wise Pob Mandatory"></asp:Label>
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
                                                    <asp:Label ID="Label105" runat="server" Text="65"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label106" runat="server" Text="Unlisted dr wise Pob Mandatory"></asp:Label>
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
                                                    <asp:Label ID="Label107" runat="server" Text="66"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label108" runat="server" Text="Doctorwise Joint Work Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio81" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label109" runat="server" Text="67"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label110" runat="server" Text="Chemist Joint Work Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio82" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label111" runat="server" Text="68"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label112" runat="server" Text="Stockist Joint Work Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio83" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label113" runat="server" Text="69"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label114" runat="server" Text="Unlisted dr wise Joint Work Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio84" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label115" runat="server" Text="70"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label116" runat="server" Text="Doctor Joint Work Selection Option as Mandatory"></asp:Label>
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
                                                    <asp:Label ID="Label117" runat="server" Text="71"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label118" runat="server" Text="Chemist Joint Work Selection Option as Mandatory"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="Radio86" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>



                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label119" runat="server" Text="72"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label120" runat="server" Text="Stockist Joint Work Selection Option as Mandatory"></asp:Label>
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
                                                    <asp:Label ID="Label121" runat="server" Text="73"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label122" runat="server" Text="Unlisted Joint Work Selection Option as Mandatory"></asp:Label>
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
                                                    <asp:Label ID="Label123" runat="server" Text="74"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label124" runat="server" Text="Doctor wise Product Caption"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDpc" runat="server" CssClass="input" Width="80px" MaxLength="10"
                                                        onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label125" runat="server" Text="75"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label126" runat="server" Text="Chemist Product Caption"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCPC" runat="server" CssClass="input" Width="80px" MaxLength="10"
                                                        onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label127" runat="server" Text="76"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label128" runat="server" Text="Stockist Product Caption"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSPC" runat="server" CssClass="input" Width="80px" MaxLength="10"
                                                        onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label129" runat="server" Text="77"></asp:Label>
                                                </td>



                                                <td align="left">
                                                    <asp:Label ID="Label130" runat="server" Text="Unlisted Product Caption"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtUPC" runat="server" CssClass="input" Width="80px" MaxLength="10"
                                                        onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label66" runat="server" Text="78"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label93" runat="server" Text="Tp Deviation Approval Needed"></asp:Label>
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
                                                    <asp:Label ID="Label94" runat="server" Text="79"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label132" runat="server" Text="Missed Date Entry Mandatory"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="RadioBtnList1" EnableViewState="true" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label134" runat="server" Text="80"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label135" runat="server" Text="Reminder Calls Entry Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="RadioBtnList2" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label136" runat="server" Text="81"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label137" runat="server" Text="Template Needed"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="RadioBtnList3" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <center>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" CssClass="savebutton"
                                            Text="Save" OnClick="btnSubmit_Click" />

                                    </td>
                                </tr>
                            </table>
                        </center>
                        <div>
                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel"
                                PopupControlID="pnlpopup" TargetControlID="linkgps" BackgroundCssClass="modalBackgroundNew">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlpopup" runat="server" BackColor="white" 
                                class="ontop" Style=" position: absolute; display: none;">  <%--Height="480px" Width="470px"--%>
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
                                            <asp:Label ID="lblgps" Font-Bold="true" Font-Size="20px" runat="server" Text="GPS Settings"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <div align="center" style="width: 430px; height: 400px; overflow-x: auto;">

                                                                    <div class="display-table clearfix">
                                                                        <div class="table-responsive">
                                                                            <asp:GridView ID="grdgps" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center"
                                                                                GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                                                <Columns>
                                                                                    <%--  <asp:TemplateField HeaderText="#">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                                                                    <%--   <asp:TemplateField HeaderText="Color" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBackColor" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
                                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="GPS">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll" runat="server" Text="GPS" onclick="SelectAllCheckboxes(this, '.GPS')" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkId" runat="server" Font-Bold="true" CssClass="GPS" Text="." />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="GEO Fencing">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll_G" runat="server" Text="GEO Fencing Doctors" onclick="SelectAllCheckboxes(this, '.GEO')" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkfencing" runat="server" Font-Bold="true" CssClass="GEO" Text="." />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="GEO Fencing Chemist">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll_chem" runat="server" Text="GEO Fencing Chemist" onclick="SelectAllCheckboxes(this, '.chem')" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkfencingche" runat="server" Font-Bold="true" CssClass="chem" Text="." />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="GEO Fencing Stock">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll_stock" runat="server" Text="GEO Fencing Stock" onclick="SelectAllCheckboxes(this, '.stock')" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkfencingstock" runat="server" Font-Bold="true" CssClass="stock" Text="." />
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
                                        <td align="center" style="padding-top:5px;padding-bottom:5px;">
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
    </form>
</body>
</html>
