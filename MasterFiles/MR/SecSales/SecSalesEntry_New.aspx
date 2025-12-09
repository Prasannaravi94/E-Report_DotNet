<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSalesEntry_New.aspx.cs"
    Inherits="MasterFiles_MR_SecSales_SecSalesEntry_New" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secondary Sales Entry</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <link type="text/css" href="../../css/Report.css" rel="Stylesheet" />
    <link href="../../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <%--   <link href="../../../JScript/Process_CSS.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="../../../JScript/DateJs/AlertJSBox.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function confirm_Submit() {
            if (confirm('Do you want to Submit?')) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <link href="../../../JScript/Service_CRM/Crm_Dr_Css_Ob/SecondaryCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JScript/Service_CRM/SecSale/SecSale_AllLevel_JS.js" type="text/javascript"></script>
    <style type="text/css">
        h1, h2 {
            border-bottom: none !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div1" style="font-weight: bold; font-size: 16px; text-align: center; color: Purple; text-decoration: underline;"
            runat="server">
        </div>
        <div id="Divid" runat="server">
        </div>
        <asp:Button ID="btnBack" runat="server" Text="Back" Style="float: right" class="savebutton" OnClick="btnBack_Click" />
        <br />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <input id="SS_SfCode" type="hidden" value='<%= Session["sf_code"] %>' />
                    <input id="SS_SfType" type="hidden" value='<%= Session["sf_type"] %>' />
                    <input id="SS_DivCode" type="hidden" value='<%= Session["div_code"] %>' />
                    <center>
                        <table>
                            <tr>
                                <td align="center">
                                    <h2 class="text-center">Secondary Sales - Entry</h2>
                                </td>
                            </tr>
                        </table>
                    </center>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblFieldForceName" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                            <select id="ddlFieldForce" style="width: 250px; font-size: 11px"></select>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblStockiest" runat="server" CssClass="label" Text="Stockiest"></asp:Label>
                            <select id="ddlStockiest" style="width: 250px; font-size: 11px">
                                <option value="0">--Select--</option>
                            </select>
                        </div>
                        <div class="single-des clearfix">
                            <div style="float: left; width: 45%;">
                                <asp:Label ID="lblMonth" runat="server" CssClass="label" Text="Month"></asp:Label>
                                <select id="ddlMonth" style="font-size: 11px">
                                    <option value="0">--Select--</option>
                                    <option value="1">Jan</option>
                                    <option value="2">Feb</option>
                                    <option value="3">Mar</option>
                                    <option value="4">Apr</option>
                                    <option value="5">May</option>
                                    <option value="6">Jun</option>
                                    <option value="7">Jul</option>
                                    <option value="8">Aug</option>
                                    <option value="9">Sep</option>
                                    <option value="10">Oct</option>
                                    <option value="11">Nov</option>
                                    <option value="12">Dec</option>
                                </select>
                            </div>
                            <div style="float: right; width: 45%;">
                                <asp:Label ID="lblYear" runat="server" CssClass="label" Text="Year"></asp:Label>
                                <select id="ddlYear" style="font-size: 11px">
                                    <option value="0">--Select--</option>
                                </select>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <input type="button" id="btnGo" value="Go" class="savebutton" />
                            <input type="button" id="btnClear" value="Clear" class="savebutton" />
                        </div>
                        <br />
                        <span id="lblStatus" style="color: Red; font-weight: bold; font-size: medium; display: none">Edit Restricted </span>
                        <br />
                    </div>
                </div>
            </div>
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <div class="display-table clearfix">
                        <div class="table-responsive" style="scrollbar-width: thin;">
                            <table id="tblSecSale" border="1px solid" cellpadding="5" cellspacing="5" style="border-collapse: collapse; border-width: 1; padding-right: 30px; width: 80%"
                                class="tblSecSaleCss">
                                <tr>
                                    <table id="rptSecSaleHeader" style="width: 80%">
                                    </table>
                                </tr>
                            </table>

                            <input type="hidden" id="hidClBal" />
                            <input type="hidden" id="hidprdcnt" />
                            <input type="hidden" id="hidCalc" />
                            <input type="hidden" id="hidPlus" />
                            <input type="hidden" id="hidMinus" />
                            <input type="hidden" id="hidCust" />
                            <input type="hidden" id="hidCust1" />
                            <input type="hidden" id="hid_Calc_Rate" />
                            <input type="hidden" id="hid_Calc_Field" />
                            <input type="hidden" id="hid_Sec_Sale_Order" />
                            <input type="hidden" id="hid_Order_Cnt" />
                            <input type="hidden" id="hid_Param_Cnt" />
                            <input type="hidden" id="hid_Receipt" />

                            <input type="hidden" id="hdnClsId" />
                            <input type="hidden" id="hdnOptionMonth" />
                            <input type="hidden" id="hidNegative" />
                            <input type="hidden" id="hidDivCode" />

                            <input type="hidden" id="hidPre_MonthCnt" />
                            <input type="hidden" id="hdn_Tot" />
                            <input type="hidden" id="hdn_txtID" />
                            <input type="hidden" id="hdnPrimaryBill" />
                            <input type="hidden" id="hdnBillSecSale" />
                            <input type="hidden" id="hdnInvOption" />
                            <br />

                        </div>
                    </div>
                    <br />
                    <center>
                        <input type="button" id="btnDraft" value="Draft Save" class="savebutton" />
                        <input type="button" id="btnSubmit" value="Send to Admin" class="savebutton" />
                    </center>
                    <div class="modal">
                        <div class="timer">
                            <svg class="rotate" viewbox="0 0 250 250">
                                    <path id="loader" transform="translate(125, 125)" />
                                </svg>
                            <div class="dots">
                                <span class="time deg0"></span><span class="time deg45"></span><span class="time deg90"></span><span class="time deg135"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <style>
                .timer {
                    width: 250px;
                    height: 250px;
                    overflow: hidden;
                    margin: 10% auto;
                    position: relative;
                }

                    .timer .rotate {
                        width: 100%;
                        height: 100%;
                        display: block;
                        position: relative;
                        z-index: 10;
                    }

                        .timer .rotate #loader {
                            fill: #ff6d69;
                        }

                er .dos {
                    top: 0;
                    left 0;
                }

                .tmer dots .tim height: 100%; widt: 6%; display boc; -gradient(#ffc8c6 #ffc8c6 16%, rgba(0, 0, 0, 0) 1%, rba(0, 0 0 0 8%, #fc8c6 8%, #fc86); t-linear-gra ient 0, 0 0) c8c6 84%, #ffc8c6); ient(#fc8c6 #ffc8c6 16, 0, 0) 84%, #ffc c6 84%, #ffc8c argin-left: -3; position:
                }

                g moz-tansform: ro a -ms-t ansform: rotat -webk t-transform: r trans orm: rotate(45
                }

                g moz-tansform: rot t -ms-tr nsform: rotate( -webkit transform: rota transfor : rotate(90deg) {

                                  -moz-transform: rotate(135deg);
                                -ms-transform: rotate(135deg);
                                -webkit-transform: rotate(135deg);
                                transform: rotate(135deg);
                            }
            </style>
            <link href="http://www.cssscript.com/wp-includes/css/sticky.css" rel="stylesheet"
                type="text/css">
            <script>
                var seconds = 10;
                var doPlay = true;
                var loader = document.getElementById('loader')
      , α = 0
      , π = Math.PI
      , t = (seconds / 360 * 1000);

                (function draw() {
                    α++;
                    α %= 360;
                    var r = (α * π / 180)
        , x = Math.sin(r) * 125
        , y = Math.cos(r) * -125
        , mid = (α > 180) ? 1 : 0
        , anim = 'M 0 0 v -125 A 125 125 1 '
               + mid + ' 1 '
               + x + ' '
               + y + ' z';
                    //[x,y].forEach(function( d ){
                    //  d = Math.round( d * 1e3 ) / 1e3;
                    //});

                    loader.setAttribute('d', anim);

                    if (doPlay) {
                        setTimeout(draw, t); // Redraw
                    }
                })();

                $(function () {

                    if ($("#SS_SfType").val() == 1) {
                        $("#ddlStockiest").show();
                        $("#ddlMonth").show();
                        $("#ddlYear").show();
                    } else {
                        $("#ddlFieldForce").show();
                        $("#ddlStockiest").show();
                        $("#ddlMonth").show();
                        $("#ddlYear").show();
                    }




                    $(".nice-select").remove();
                });
            </script>
        </div>
    </form>
</body>
</html>
