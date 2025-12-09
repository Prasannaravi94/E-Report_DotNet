<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptAutoExpense_Zoom.aspx.cs" Inherits="MasterFiles_MR_RptAutoExpense" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Expense Statement</title>
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <style type="text/css">
        table {
            border-collapse: collapse;
        }

        .mainDiv {
            background-color: White;
        }

        .removeMainDiv {
            background-color: White;
        }

        .tdHead {
            background: #FFEFD5; /* Old browsers */
        }

        .tblHead {
            background: #FFEFD5;
        }

        .mainGrid {
            background: #FFEFD5; /* Old browsers */
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .display-table1 {
            color: #636d73;
            font-size: 12px;
            font-weight: 400;
            text-transform: uppercase;
        }

            .display-table1 .table {
                margin-bottom: 0rem;
            }

                .display-table1 .table th {
                    padding: 5px 5px;
                    border-bottom: 5px solid #fff;
                    border-top: 0px;
                    font-weight: 400;
                    text-align: center;
                    border-left: 1px solid #DCE2E8;
                    vertical-align: inherit;
                }

                    .display-table1 .table th:first-child {
                        font-weight: 500;
                        text-align: left;
                    }

                    .display-table1 .table th:last-child {
                        border-radius: 0px 8px 8px 0px;
                    }

                .display-table1 .table th {
                    background-color: #FFEFD5;
                }

                .display-table1 .table tr:nth-child(2) td:first-child {
                    background-color: #f1f5f8;
                }


                .display-table1 .table td {
                    border-left: 1px solid #DCE2E8;
                    vertical-align: inherit;
                }

                .display-table1 .table tr td:first-child {
                    background-color: #f1f5f8;
                    text-align: center;
                    border: 0px;
                    padding: 5px 5px;
                }

            .display-table1 tr td a {
                color: #1584fb;
                font-size: 12px;
                font-weight: 500;
                padding-right: 4px;
            }

            .display-table1 #expGrid {
                margin-bottom: 1.5rem !important;
            }

        .display-table1-no-result-area {
            border: solid 1px #9aa3a9;
            text-align: center;
            padding: 10px;
            color: #696d6e;
        }

        .gridheight {
            overflow-y: auto !important;
            height: 500px !important;
        }

        .textbox {
            height: 25px !important;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            });
        });
    </script>
    <script type="" language="javascript">
        function _fNvALIDeNTRY(_tYPE, _MaxL) { var _cTRL = event.srcElement; var _v = _cTRL.value; if (_tYPE == 'N' || _tYPE == 'n' || _tYPE == 'D' || _tYPE == 'd') { if ((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 46) { var _sTi = _v.indexOf('.'); if ((_tYPE == 'D' || _tYPE == 'd') && _sTi <= -1) { if (_v.length < _MaxL - 2 || event.keyCode == 46) { event.returnValue = true; return true; } } else { if ((_v.substring(_sTi + 1, _v.length).length != 2 || _tYPE == 'N' || _tYPE == 'n') && (event.keyCode != 46)) { if (_v.length < _MaxL || _sTi > -1) { event.returnValue = true; return true; } } } } } else if (_tYPE == 'C' || _tYPE == 'c') { if (_v.length < _MaxL) { event.returnValue = true; return true; } } else if (_tYPE.substring(0, 3) == '-O-' || _tYPE.substring(0, 3) == '-o-') { _tYPE = _tYPE.replace(/-o-/, ''); if (_v.length < _MaxL) { var _C = String.fromCharCode(event.keyCode); if (_C == '"') _tYPE = _tYPE.replace("!!", '"'); if (_C == "'") _tYPE = _tYPE.replace("~~", "'"); if (_tYPE.indexOf(_C) == -1) { event.returnValue = true; return true; } } } event.returnValue = false; }

        function DRForAdmin($x, $r, rCnt) {
            var tb = $r.parentNode;
            var Ttb = tb.parentNode
            if (Ttb.rows.length > rCnt + 1) {
                tb.removeChild($r);
            }
            else
                clrNRw($r);
            adminAdjustCalc(tb, 1);
        }
        function adminAdjustCalc($x, isDelete) {
            $grandTotalEle = document.getElementById("grandTotalName");
            var grandTotalVal = parseFloat($grandTotalEle.innerHTML.replace(/,/g, ''));
            var $R;
            if (isDelete == 1) {
                //alert('ddd');
                $R = $x;
            }
            else {
                $R = $x.parentNode.parentNode.parentNode;
            }
            var $tot = 0;

            $totEle = document.getElementById("hidtamtval");
            var $tot = parseFloat($totEle.value.replace(/,/g, ''));

            var $plus = 0;
            var $minus = 0;
            var $temp = 0;
            for (var $rl = 1; $rl < $R.children.length; $rl++) {
                var $type = parseFloat($R.children[$rl].cells[1].children[0].value.replace(/,/g, ''));
                var $amount = parseFloat($R.children[$rl].cells[2].children[0].value.replace(/,/g, ''));
                if (isNaN($amount)) $amount = 0;

                if ($type == 1) {
                    $plus = $plus + $amount;
                }
                if ($type == 0) {
                    $minus = $minus + $amount;
                }

            }
            $temp = $plus - $minus;
            //alert($temp+"::"+$tot);
            if ($tot < 0) {
                //$tot=-$tot;
            }
            //alert($tot);
            //$tot = $temp + $tot;
            grandtotalcalc($temp, $tot);
            $totEle.value = $temp;

        }
        function grandtotalcalc1(addVal) {
            $grndTot = document.getElementById("grandTotalName");
            var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
            alert(grndVal + " kkk " + addVal);
            $grndTot.innerHTML = grndVal + addVal;
        }
        function _AdRowByCurrElem($x) {

            _tR = $x.parentNode.parentNode;
            _nTR = _tR.cloneNode(true);
            _tR.parentNode.appendChild(_nTR);
            //newRow.appendChild(_nTR);
            //_tR.parentNode.appendChild(newRow);
            clrNRw(_nTR)
        }
        function clrNRw($r) {
            for (var $rl = 0; $rl < $r.childNodes.length; $rl++) {
                $c = $r.childNodes[$rl];
                for (var $i = 0; $i < $c.childNodes.length; $i++) {
                    $o = $c.childNodes[$i];

                    if ($o.id != '' && $o.id != null) {
                        $s = $o.id.split('_');
                        $o.id = $s[0] + '_' + $r.rowIndex
                    }
                    if ($o.type == "checkbox") {
                        $o.checked = false;
                    }
                    else if ($o.tagName == 'SELECT') {
                        $o.selectedIndex = 0;
                    }
                    else if ($o.tagName == 'SPAN') {
                        $o.innerText = "";
                    }
                    else if ($o.value != null && $o.type != "button" && $o.type != "hidden") {
                        $o.value = "";

                    }
                    if ($o.pv != null) $o.pv = '';
                    if ($o.Pval != null) $o.Pval = '';
                }
            }
        }
        function DRForOthExp($x, $r, rCnt) {
            var $temp = $r.cells[1].childNodes[0].value.replace(/,/g, '');
            if (isNaN($temp) || $temp == '') $temp = 0;

            var tb = $r.parentNode;
            var Ttb = tb.parentNode

            if (Ttb.rows.length - 1 > rCnt) {
                tb.removeChild($r);
            }
            else
                clrNRw($r);


            $OthExpTotValEle = document.getElementById("Othtotal");
            $grndTot = document.getElementById("grandTotalName");
            alert($OthExpTotValEle);
            alert($grndTot);
            var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
            var othExpVal = parseFloat($OthExpTotValEle.value);

            $grndTot.innerHTML = parseFloat(grndVal) - parseFloat($temp);
            $OthExpTotValEle.value = parseFloat(othExpVal) - parseFloat($temp);

        }
        function getMaxMisLmtVal($x) {
            var $limit = 0;
            var limit = "50";
            //alert(limit);
            var $R = $x.parentNode.parentNode;
            var $Fr = $R.cells[0].children[0].value;
            //alert($Fr);
            if ($Fr == '') {
                $limit = 0;

            }
            else {
                var $st = limit.indexOf($Fr + "#");
                if ($st > -1) {
                    $st = $st + ($Fr + "#").length;
                    var $et = limit.indexOf("$", $st);
                    $limit = limit.substring($st, $et);
                }
            }
            return $limit;
        }

        function OthExpCalc($OthExpVal) {
            $OthExpTotValEle = document.getElementById("Othtotal");
            var othExpVal = parseFloat($OthExpTotValEle.value);
            //alert(othExpVal);

            var $maxLimit = parseFloat(getMaxMisLmtVal($OthExpVal));
            var amt = parseFloat($OthExpVal.parentNode.parentNode.cells[1].children[0].value);
            //alert($maxLimit+" ff "+amt);
            //alert(amt>=$maxLimit);
            if ($maxLimit > 0 && amt > $maxLimit) {
                alert("Amount should be less than equal to " + $maxLimit);
                $OthExpVal.parentNode.parentNode.cells[1].children[0].value = 0;
                //othExpVal=0;
            }

            var $R = $OthExpVal.parentNode.parentNode.parentNode;
            var $Tot = 0;
            var $temp = 0;
            for (var $rl = 1; $rl < $R.children.length; $rl++) {
                $temp = $R.children[$rl].cells[1].childNodes[0].value.replace(/,/g, '');
                if (isNaN($temp) || $temp == '') $temp = 0;
                $Tot = parseFloat($Tot) + parseFloat($temp);

            }
            $OthExpTotValEle.value = $Tot;
            //alert($Tot);
            //alert(othExpVal);
            grandtotalcalc($Tot, othExpVal);

        }
        function totalAllowCalc(newvalue, oldvalue) {
            var $tot = document.getElementById("AllowTotal");
            var totval = parseFloat($tot.innerHTML.replace(/,/g, ''));
            $tot.innerHTML = totval - oldvalue + newvalue;
        }

        function totalDistCalc(newvalue, oldvalue) {
            var $tot = document.getElementById("DistTotal");
            var totval = parseFloat($tot.innerHTML.replace(/,/g, ''));
            $tot.innerHTML = totval - oldvalue + newvalue;
        }

        function totalFareCalc(newvalue, oldvalue) {
            var $tot = document.getElementById("FareTotal");
            var totval = parseFloat($tot.innerHTML.replace(/,/g, ''));
            $tot.innerHTML = totval - oldvalue + newvalue;
        }

        function totalcalc(newvalue, oldvalue) {
            $tot = document.getElementById("TotalVal");
            var totval = parseFloat($tot.innerHTML.replace(/,/g, ''));
            $tot.innerHTML = totval - oldvalue + newvalue;
        }
        function grandtotalcalc(newvalue, oldvalue) {
            $grndTot = document.getElementById("grandTotalName");
            var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
            $grndTot.innerHTML = grndVal - oldvalue + newvalue;
        }

    </script>
</head>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $("#btn").live("click", function () {
        var divContents = $("#pnlContents").html();
        var printWindow = window.open('', '', 'height=400,width=900');
        printWindow.document.write('<html><head><title>Actual Expense Statement</title>');
        printWindow.document.write('</head><body style="font-size:5pt">');
        //printWindow.document.write('<span style="font-size:8px">' + divContents + '<span>');           
        printWindow.document.write(divContents);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    });
</script>
<body class="bodycss">
    <form id="form1" runat="server">
        <div class="mainDiv" id="mainDiv" runat="server" visible="true">
            <br />
            <table width="100%">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btn" ToolTip="Print" runat="server">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server" Width="100%">
                            <center>
                                <asp:Label Font-Bold="true" ForeColor="red" runat="server" Visible="false" ID="messageId" Text=""></asp:Label>
                                <h2 class="text-center">Expense Statement For The month of <span id="mnthtxtId" runat="server"></span>-<span id="yrtxtId" runat="server"></span></h2>
                            </center>
                            <div id="msgId" runat="server" visible="false">
                                <center><font size="3" face="Verdana, Arial, Helvetica, sans-serif" color="Red"><b>Your Expense Not Yet Approved...</b></font></center>
                                <br />
                            </div>
                        </asp:Panel>
                        <div id="MainId" runat="server" visible="true">
                            <table width="100%" align="center">
                                <tbody>
                                    <tr>
                                        <td align="left" style="font-weight: bold; font-size: small;" id="fieldforceId" runat="server"></td>
                                        <td align="left" style="font-weight: bold; font-size: small;" id="hqId" runat="server"></td>
                                        <td align="right" style="font-weight: bold; font-size: small;" id="empId" runat="server"></td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-table clearfix">
                                    <div class="table-responsive gridheight">
                                        <center>
                                            <asp:GridView ID="grdExpMain" runat="server" Width="100%" HorizontalAlign="Center"
                                                AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                                CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdExpMain.PageIndex * grdExpMain.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ADate" runat="server" Text='<%# Bind("Adate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDayName" runat="server" Text='<%# Bind("theDayName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Work Type" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWorkType" runat="server" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Place of Work" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTerrName" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Allowance Type" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCat" runat="server" Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Allowance (in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAllw" runat="server" Text='<%# Bind("Allowance") %>'>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From and To" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td width="100px" align="left">
                                                                        <asp:Label ID="lblFrom" runat="server" Text='<%# Bind("From_place") %>'></asp:Label></td>
                                                                    <td width="200px" align="right">
                                                                        <asp:Label ID="lblTo" runat="server" Text='<%# Bind("To_place") %>'>'></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("exp_remarks") %>'>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Distance Travelled</br>(in Kms)" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDistance" runat="server" Text='<%# Bind("Distance") %>'>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fare</br>(in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFare" runat="server" Text='<%# Bind("Fare") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Additional Expense</br>(in Rs/-)" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbaddExp" runat="server" Text='<%# Bind("rw_amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbaddExp" runat="server" Text='<%# Bind("rw_rmks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amt</br>(in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </center>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <table width="100%" align="right">
                                <tr>
                                    <td align="right">
                                        <div class="designation-reactivation-table-area clearfix">
                                            <div class="display-table1 clearfix">
                                                <div class="table-responsive" style="float: right;">
                                                    <asp:GridView ID="otherExpGrid" runat="server" Width="51%" HorizontalAlign="right" CellPadding="0"
                                                        AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                                        CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Expense Name" ItemStyle-HorizontalAlign="Left">
                                                                <ControlStyle></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSexpName" runat="server" Text='<%# Bind("Expense_Parameter_Name") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnSexpName" runat="server" Value='<%#Eval("Expense_Parameter_Code")%>' />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotal" Style="font-weight: bold" runat="server" Text="Total"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                                                <ControlStyle></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="right"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSexpAmnt" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <div style="text-align: right;">
                                                                        <asp:Label ID="ftlblSexpAmnt" Style="font-weight: bold" runat="server"></asp:Label>
                                                                    </div>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="display-table1-no-result-area" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <div class="designation-reactivation-table-area clearfix">
                                            <div class="display-table1 clearfix">
                                                <div class="table-responsive" style="float: right;">
                                                    <br />
                                                    <asp:GridView ID="expGrid" runat="server" Width="51%" HorizontalAlign="right" CellPadding="0"
                                                        AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                                        CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                                                <ControlStyle></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblType" runat="server" Text='<%# Bind("Paritulars") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                                                <ControlStyle></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotal" Style="font-weight: bold" runat="server" Text="Total"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                                                <ControlStyle></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="right"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amt") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <div style="text-align: right;">
                                                                        <asp:Label ID="ftlblAmount" Style="font-weight: bold" runat="server"></asp:Label>
                                                                    </div>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="display-table1-no-result-area" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <div class="designation-reactivation-table-area clearfix">
                                            <div class="display-table1 clearfix">
                                                <div class="table-responsive" style="float: right;">
                                                    <asp:GridView ID="adminExpGrid" runat="server" Width="51%" HorizontalAlign="right" CellPadding="0"
                                                        AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                                        CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left">
                                                                <ControlStyle></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldt" runat="server" Text='<%# Bind("adminAdjDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MisExp" ItemStyle-HorizontalAlign="Left">
                                                                <ControlStyle></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMis" runat="server" Text='<%# Bind("Expense_Parameter_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                                                <ControlStyle></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTyp" runat="server" Text='<%# Bind("Typ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                                                <ControlStyle></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Paritulars") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotal" Style="font-weight: bold" runat="server" Text="Total"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                                                <ControlStyle></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="right"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amt") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <div style="text-align: right;">
                                                                        <asp:Label ID="ftlblAmount" Style="font-weight: bold" runat="server"></asp:Label>
                                                                    </div>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="display-table1-no-result-area" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="misExp" visible="false" runat="server">
                                            <br />
                                            <table width="100%" border="1" align="center" id="rmks" style="margin-right: 0px; border-color: #9aa3a9;">
                                                <tr width="100%">
                                                    <td align="center" style="width: 12%;">
                                                        <b class="subheading"><font color='blue' style="font-size:14px;">Admin Remarks : - </font></b>&nbsp;
                                                    </td>
                                                    <td align="center" style="width: 37%;">
                                                        <textarea name="approveTextId" id="approveTextId" runat="server" cols="46" rows="5"></textarea>
                                                    </td>
                                                    <td align="center" style="width: 12%;">
                                                        <b class="subheading"><font color='blue' style="font-size:14px;">Manager Remarks : - </font></b>&nbsp;
                                                    </td>
                                                    <td align="center" style="width: 37%;">
                                                        <textarea name="MgrTextId" id="MgrTextId" runat="server" cols="46" rows="5"></textarea>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td style="padding-top: 5px; text-align: right; color: red; font-family: Times New Roman; font-weight: bold; font-size: 20px; width: 90%;">Grand Total : </td>
                                                    <td style="padding-top: 5px; color: red; text-align: right; font-family: Times New Roman; font-weight: bold; font-size: 20px" runat="server" id="grandTotalName">0</td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="otherExpValues" runat="server" Value="" />
                                            <asp:HiddenField ID="hidtamtval" runat="server" Value="0" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
		<script type="text/javascript">
        if ('<%= Session["Div_color"]!= null%>' == 'False') {
            document.body.style.backgroundColor = '#e8ebec';
        } else {
            document.body.style.backgroundColor = '<%= Session["Div_color"] %>'
        }
    </script>
    </form>
</body>
<script>
    function saveOtherExp() {
        //alert(document.getElementById("otherExp"));
        var otherExpRmk = document.getElementsByName("tP");
        //alert(otherExpRmk.length);
        var otherExpVal = document.getElementsByName("tAmt");
        var otherExp = document.getElementsByClassName("Combovalue");
        var exp = 0, val = 0, gt = 0;
        var remarks = "";
        for (var i = 0; i < otherExpRmk.length; i++) {
            var value = otherExp[i].options[otherExp[i].selectedIndex].value;
            var text = otherExp[i].options[otherExp[i].selectedIndex].text;

            alert(otherExpRmk[i].value + "::" + otherExpVal[i].value + "::" + otherExp[i].value);
            if (i == 0) {
                remarks = otherExpRmk[i].value;
                val = otherExpVal[i].value;
                exp = value + "=" + text;
            }
            else {
                remarks = remarks + "," + otherExpRmk[i].value;
                val = val + "," + otherExpVal[i].value;
                exp = exp + "," + value + "=" + text;

            }

        }
        $grandTotalEle = document.getElementById("grandTotalName");
        var gt = parseFloat($grandTotalEle.innerHTML.replace(/,/g, ''));
        alert(remarks + "~" + val + "~" + exp);
        document.getElementById("otherExpValues").value = remarks + "~" + val + "~" + exp + "~" + gt;

    }
</script>
</html>
