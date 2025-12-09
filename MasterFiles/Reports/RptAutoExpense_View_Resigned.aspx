<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptAutoExpense_view_resigned.aspx.cs" Inherits="MasterFiles_MR_RptAutoExpense" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Statement</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
     
   
     <style type="text/css">
             table {
    border-collapse: collapse;
}
.mainDiv
{
    background-color:White;
}
.removeMainDiv
{
    background-color:White;
}
.tdHead
{
background: #FFEFD5; /* Old browsers */


}
.tblHead
{
background: #FFEFD5;
}
.mainGrid
{
background: #FFEFD5; /* Old browsers */

}
</style>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
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
                var $type = parseFloat($R.children[$rl].cells[2].children[0].value.replace(/,/g, ''));
                var $amount = parseFloat($R.children[$rl].cells[3].children[0].value.replace(/,/g, ''));
                //alert($amount);
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
            grandtotalcalc($temp,$tot);
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
        function clrAmntValue($x)
{
    //alert($x.value);    
}
function clrNRw($r) {
    //alert(Array(16).join("js" - 1));
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
    
   // alert($r.children[3].innerHTML);
    //alert($r.children[3].cells[3].children[0]);
    //$r.children[3].value = "0";
}
 
function DRForOthExp($x, $r, rCnt) {
    var $temp = $r.cells[1].childNodes[0].value.replace(/,/g, '');
    if (isNaN($temp) || $temp == '') $temp = 0;

    var tb = $r.parentNode;
    var Ttb = tb.parentNode
   
    if (Ttb.rows.length-1 > rCnt) {
        tb.removeChild($r);
    }
    else
        clrNRw($r);


    $OthExpTotValEle = document.getElementById("Othtotal");
    $grndTot = document.getElementById("grandTotalName");
    //alert($OthExpTotValEle);
    //alert($grndTot);
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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>

     <script type="text/javascript" language="javascript">
         $(function () {
             $('#btnExcel').click(function () {

                 $("#tableId").html("");
                 //$("#misExp").attr('disable', true);
                 $("#misExp").hide();
                 $("#btnSave").remove();
				 $("#rmks").remove();
                 $("#btnDrftSave").remove();
                 $("#bakfldfrce").remove();
 				 $("#Divmiss").css('Visibility', 'Visible');
                 $("#rmks1").css('Visibility', 'Visible');
                 var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                 location.href = url
                 return false
             })
         })
    </script>
</head>
  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("#btnPrint").live("click", function () {           
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
     <table width="100%">
            <tr>
                <td width="80%">
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    Visible="true" />
                            </td>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                     />
                            </td>
                            <td>
                                <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                     Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClientClick="RefreshParent();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
    <div class="mainDiv" id="mainDiv" runat="server" >
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
     <center><font size="3" face="Verdana, Arial, Helvetica, sans-serif" color="maroon"><b><u>Expense Statement For The month of <span id="mnthtxtId" runat="server"></span>-<span id="yrtxtId" runat="server"></span></u></b></font></center>
 <br /> 
 <div align="center">
  <table align="center" width="80%">
<tr>
<td align="left"  style="font-weight:bold;font-size:small;" id="empid1" runat="server"></td>
<td align="left"  style="font-weight:bold;font-size:small;" id="fieldforceId" runat="server"></td>
<td align="center" style="font-weight:bold;font-size:small;" id="hqId" runat="server"></td>
<td align="right" style="font-weight:bold;font-size:small;" id="empId" runat="server"></td>
</tr>
</table></div>
     <table  width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdExpMain" runat="server" Width="80%" Font-Size="8pt" 
                            HeaderStyle-BackColor="#FFEFD5" HeaderStyle-CssClass="mainGrid" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="black"  HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" cellpadding="4" cellspacing="2">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left"  Visible="false">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdExpMain.PageIndex * grdExpMain.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                         <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ADate" runat="server" Text='<%# Bind("Adate") %>'></asp:Label>
                                            <asp:HiddenField ID="adateHidden" runat="server" Value='<%# Bind("Adate1") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDayName" runat="server" Text='<%# Bind("theDayName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" width="70px" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkType" runat="server" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Place of Work" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTerrName" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Allow.</br>Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCat" runat="server" Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Allow.</br>(in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAllw" runat="server" Text='<%# Bind("Allowance") %>'>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="From&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" width="250px" BorderWidth="1px" BorderColor="#9AA3A9" >
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                     <table>
                                            <tr>
                                           <td width="100px" align="left"><asp:Label ID="lblFrom" runat="server" Text='<%# Bind("From_place") %>'></asp:Label></td>
                                           <td width="150px" align="right"><asp:Label ID="lblTo" runat="server" Text='<%# Bind("To_place") %>'>'></asp:Label></td>
                                            </tr>
                                     </table>
                                     <table id='<%# Eval("Adate") %>' class="disttable"></table>
                                        <button id="dtlBtn" runat="server" style="visibility:hidden" onclick="distTable(this);return false">+</button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Dist. Travelled</br>(in Kms)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistance" runat="server" Text='<%# Bind("Distance") %>'>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Fare</br>(in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFare" runat="server" Text='<%# Bind("Fare") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Total Amt</br>(in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
              <table align="center" id="twdid" width="80%" runat="server" visible="false" style="margin-left:10%">
                    <tr>
                    <td ><font color="red">TWD:-<label id="twd" runat="server"></label></font></td>
                    <td><font color="red">FW:-<label id="fw" runat="server"></label></font></td>
                    <td><font color="red">HQ:-<label id="hhq" runat="server"></label></font></td>
                    <td><font color="red">EX:-<label id="eex" runat="server"></label></font></td>
                    <td><font color="red">OS:-<label id="oos" runat="server"></label></font></td>
                    <td><font color="red">Calls Met:-<label id="met" runat="server"></label></font></td>
                    <td><font color="red">Calls Made:-<label id="made" runat="server"></label></font></td>
                    <td><font color="red">Call Avg:-<label id="cavg" runat="server"></label></font></td>
                    </tr>
                    </table>
            <table width="90%" align="center">
            <tr><td>&nbsp;</td></tr>

<tr>
<td>
<div align="center" id="divAttach" runat="server" style=" border: 1px; border-style: solid; background-color: #ffebcd;
                        color: Black; height: 50px">
                        <br />
                        <a id="aTagAttach" target="_blank" runat="server">
                            <asp:Label ID="lblViewAttach" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                        </a>
                        <br />
                    </div>
                    </td>
<td align="right">

                                <asp:GridView ID="otherExpGrid" runat="server" Width="40%" Font-Name="Verdana"  Font-Size="8pt" 
            HeaderStyle-BackColor="#FFEFD5"
            HeaderStyle-ForeColor="black" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Fixed Exepense" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblSexpName" runat="server" Text='<%# Bind("Expense_Parameter_Name") %>'></asp:Label>
                                            <asp:HiddenField ID="hdnSexpName" runat="server" Value='<%#Eval("Expense_Parameter_Code")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSexpAmnt" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                               </Columns>
                                
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
</td>

<td  align="right">

                                <asp:GridView ID="expGrid" runat="server"  Width="40%" Font-Name="Verdana"  Font-Size="8pt" 
            HeaderStyle-BackColor="#FFEFD5"
            HeaderStyle-ForeColor="black" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Miscellaneous Expense" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblType" runat="server" Text='<%# Bind("Paritulars") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amt") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                               </Columns>
                                
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
</td></tr>

</table>
<div id="misExp" align="center" visible="false" runat="server">
<br />
 <table border="0" width="700px" align="center" id="tableId" runat="server">

                             <tr>                                
                                <td class="tblHead" style="width:200px;"><b>Date</b></td>
                                <td class="tblHead" style="width:200px;"><b>MisExp</b></td>
                                <td class="tblHead" style="width:30px"><b>Type</b></td>
                                <td class="tblHead" style="width:100px"><b>Amount</b></td>
                                <td class="tblHead" style="width:250px;"><b>Remarks</b></td>
                                <td class="tblHead" style="width:20px" colspan="2"><b>+/-</b></td>
	                        
                            </tr>                         
                            <tr>
                              <td>
                              <asp:DropDownList  class="date" ID="date" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                              <asp:ListItem Selected="false" Text="--Select--" Value="0"></asp:ListItem>
                             </asp:DropDownList>
                             </td>
                              <td>
                              <asp:DropDownList  class="misExpList" ID="misExpList" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                              <asp:ListItem Selected="false" Text="--Select--" Value="0"></asp:ListItem>
                             </asp:DropDownList>
                             </td>


                                <td><input type='text' value='' name='tP' size="150" maxlength='50' class='textbox' onkeypress='_fNvALIDeNTRY("-o-!!~~,",50)' onkeydown='if(event.shiftKey==true && event.keyCode==9){this.parentNode.previousSibling.focus();return false}' style="width:90%;height:19px" /></td>
                                <td>
                                    <!--<select name='sTyp' style="width:80px" class='Combovalue' onchange='adminAdjustCalc(this,0);'>
										<option value="-1" >---Select Type---</option>
                                        <option value='1' >+</option>
                                        <option value='0' >-</option>
                                    </select>-->
                          <asp:DropDownList onchange="adminAdjustCalc(this,0)" class="Combovalue" ID="Combovalue" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                        <asp:ListItem Selected="false" Text="---Select---" Value="2"></asp:ListItem>
                        <asp:ListItem Selected="false" Text="+" Value="1"></asp:ListItem>
                        <asp:ListItem Selected="false" Text="-" Value="0"></asp:ListItem>
                    </asp:DropDownList>

                                    
                                </td>
                                <td><input type='text' value='' name='tAmt' maxlength='50' onkeypress='_fNvALIDeNTRY("D",7)' onkeyup='adminAdjustCalc(this,0);' class='textbox' style="width:100%;height:19px;text-align:right" />
							
                                 </td>
								<td><input type="button" value=" + " class='btnSave' onclick="_AdRowByCurrElem(this);clrAmntValue(this);" /></td>
								<td><input type="button" value=" - " class='btnSave' onclick="DRForAdmin(this,this.parentNode.parentNode,1)"/></td>
		                    </tr>
            </table>
  <div id="Divmiss" align="center" style="Visibility:hidden" runat="server">
<br />
            <table id="lblmiss" runat="server" align="center">
            <tr><td  align="right">

                                <asp:GridView ID="adminExpGrid" runat="server"  Width="40%" Font-Name="Verdana"  Font-Size="8pt" 
            HeaderStyle-BackColor="#FFEFD5"
            HeaderStyle-ForeColor="black" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblDt" runat="server" Text='<%# Bind("Paritulars") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MisExp" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblmis" runat="server" Text='<%# Bind("Paritulars") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTyp" runat="server" Text='<%# Bind("typ") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amt") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblrmks" runat="server" Text='<%# Bind("amt") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                                    
                               </Columns>
                                
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
</td></tr></table>
</div>
<table width="80%"  border="0">
 <tr><td  style="padding-top:10px;text-align:right;color:red; font-family:Times New Roman;font-weight:bold;font-size:30px">Grand Total : </td><td style="padding-top:10px;color:red;text-align:right;font-family:Times New Roman;font-weight:bold;font-size:30px" runat="server" id="grandTotalName">0</td></tr>
</table> <asp:HiddenField ID="otherExpValues" runat="server" Value="" /><asp:HiddenField ID="hidtamtval" runat="server" Value="0" /><asp:HiddenField ID="frmTovalues" runat="server" Value="" />
<br/>
<table width="80%" border="1" align="center" id="rmks">
<tr><td align="center"><b class="subheading"><font color='blue'>Remarks : - </font></b>&nbsp;</td>
<td><textarea name="approveTextId" id="approveTextId" runat="server" cols="50" rows="5" ></textarea>
</td></tr>
</table>
<table width="80%" border="0" align="center" id="rmks1" style="Visibility:hidden">
<tr><td ><font color="red">Remarks:-<label id="lblrmks1" runat="server"></label></font></td></tr>
</table>
<br />
<br />
<br />
              


    </div>
     </asp:Panel>
      <center>
        <asp:Button ID="btnDrftSave" runat="server" CssClass="BUTTON" Width="80px" Height="25px"
            Text="Draft Save" Visible="false" OnClientClick="saveOtherExp()" OnClick="btnSaveDraft_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" Width="60px" Height="25px"
            Text="Process" Visible="true" OnClientClick="saveOtherExp()" OnClick="btnSave_Click" />
                    <asp:Button ID="bakfldfrce" runat="server" CssClass="BUTTON" Width="130px" Height="25px"
            Text="BacK To FieldForce" Visible="false" OnClientClick="saveOtherExp()" />
    </center>
    </div>
   
    </form>

</body>
<script>
    function saveOtherExp() {
        //alert(document.getElementById("otherExp"));
        var otherExpRmk = document.getElementsByName("tP");
        //alert(otherExpRmk.length);
        var otherExpVal = document.getElementsByName("tAmt");
        var otherExp = document.getElementsByClassName("Combovalue");
        var misExp = document.getElementsByClassName("misExpList");
        var date = document.getElementsByClassName("date");
        var exp = 0, val = 0, gt = 0;
        var remarks="";
        for (var i = 0; i < otherExpRmk.length; i++) {
            var value = otherExp[i].options[otherExp[i].selectedIndex].value;
            var text = otherExp[i].options[otherExp[i].selectedIndex].text;

            var datevalue = date[i].options[date[i].selectedIndex].value;
            var datetext = date[i].options[date[i].selectedIndex].text;

            var misExpvalue = misExp[i].options[misExp[i].selectedIndex].value;
            var misExptext = misExp[i].options[misExp[i].selectedIndex].text;
            // alert(otherExpRmk[i].value + "::" + otherExpVal[i].value + "::" + otherExp[i].value);
            if (i == 0) {
                dateValues = datevalue + "=" + datetext;
                misExpValues = misExpvalue + "=" + misExptext;
                remarks =  otherExpRmk[i].value;
                val = otherExpVal[i].value;
                exp = value+"="+text;
            }
            else {
                dateValues = dateValues+","+datevalue + "=" + datetext;
                misExpValues = misExpValues+","+misExpvalue + "=" + misExptext;
                remarks = remarks + "," + otherExpRmk[i].value;
                val = val + "," + otherExpVal[i].value;
                exp = exp+","+value + "=" + text;

            }

        }
        $grandTotalEle = document.getElementById("grandTotalName");
        var gt = parseFloat($grandTotalEle.innerHTML.replace(/,/g, ''));
        //alert(remarks + "~" + val + "~" + exp);
        document.getElementById("otherExpValues").value = remarks + "~" + val + "~" + exp+"~"+gt+"~"+dateValues+"~"+misExpValues;

    }
    function distTable($this) {
        //       // alert('tes');
        //        var sVal = document.getElementById("frmTovalues").value;
        //        //alert(sVal);
        //        sVal = sVal + '#';
        //        var $R1 = $this.parentNode.parentNode;
        //        var $cR = $this.parentNode;
        //        var date = $R1.cells[0].children[1].value;

        //        alert(date);
        //        var dateid = date.substring(0, 10);
        //        //var dateid = $R1.cells[0].children[0].value;
        //        var $st = sVal.indexOf(date);
        //        //alert(date);
        //        //alert(sVal);
        //        var placewithdis = "From$To$0".split('$');
        //        //alert($st);
        //        if ($st > -1) {
        //            $st = $st + (date).length;
        //            var $et = sVal.indexOf("#", $st);
        //            //alert($et);
        //            placewithdis = sVal.substring($st, $et).split('$');
        //            //alert(placewithdis[0]);

        //        }
        //        var fareTot = 0;
        //        var distTot = 0;
        //        var distances = new Array();
        //        distances.push(["From", "To", "Distance", "Fare"]);
        //        for (var i = 0; i < placewithdis.length; i++) {
        //            var data = placewithdis[i].split('~');
        //            distances.push([data[0], data[1], data[2] + ' X ' + data[4], data[3]]);
        //            fareTot = fareTot + parseFloat(data[3]);
        //            distTot = distTot + parseFloat(data[2]);
        //        }
        //        distances.push(["Total", "", distTot, fareTot]);
        //        //Create a HTML Table element.
        //        var table = document.getElementById(dateid);
        //        for (var i = 0; i < table.rows.length; ) {
        //            table.deleteRow(i);
        //        }
        //        //alert(table);
        //        //alert("distTable_" + dateid)
        //        //var table = document.createElement("TABLE");
        //        table.border = "2";

        //        //Get the count of columns.
        //        var columnCount = distances[0].length;

        //        //Add the header row.
        //        var row = table.insertRow(-1);
        //        for (var i = 0; i < columnCount; i++) {
        //            var headerCell = document.createElement("TH");
        //            headerCell.innerHTML = distances[0][i];
        //            row.appendChild(headerCell);
        //        }

        //        //Add the data rows.
        //        for (var i = 1; i < distances.length; i++) {
        //            row = table.insertRow(-1);
        //            for (var j = 0; j < columnCount; j++) {
        //                var cell = row.insertCell(-1);
        //                cell.innerHTML = distances[i][j];
        //            }
        //        }
        //        //alert($cR.cells[0].children[1])
        //        $cR.children[2].style.visibility = "hidden";

        //        //alert(date);
        //        // alert(sVal);
        //        //var s = sVal.split('#');

        //        //alert(table);
        //        //tab.innerHTML = "";
        //        //table.innerHtml=table;
        //=====

        var $R1 = $this.parentNode.parentNode;
        var $cR = $this.parentNode;
        var date = $R1.cells[0].children[1].value;
        var dateid = date.substring(0, 10);
        //alert($cR.children[2].value);
        // alert($cR.children[2].innerHtml);
        if ($cR.children[2].value == "+" || $cR.children[2].value == "") {
            // alert('tes');
            var sVal = document.getElementById("frmTovalues").value;
            //alert(sVal);
            sVal = sVal + '#';

            //alert(date);

            //var dateid = $R1.cells[0].children[0].value;
            var $st = sVal.indexOf(date);
            //alert(date);
            //alert(sVal);
            var placewithdis = "From$To$0".split('$');
            //alert($st);
            if ($st > -1) {
                $st = $st + (date).length;
                var $et = sVal.indexOf("#", $st);
                //alert($et);
                placewithdis = sVal.substring($st, $et).split('$');
                //alert(placewithdis[0]);

            }
            if (placewithdis == "") {
                $cR.children[2].style.visibility = "hidden";
                return false;
            }
            var fareTot = 0;
            var distTot = 0;
            var distances = new Array();
            distances.push(["From", "To", "Distance", "Fare"]);
            for (var i = 0; i < placewithdis.length; i++) {
                var data = placewithdis[i].split('~');
                distances.push([data[0], data[1], data[2] + ' X ' + data[4], data[3]]);
                fareTot = fareTot + parseFloat(data[3]);
                distTot = distTot + parseFloat(data[2]);
            }
            distances.push(["Total", "", distTot, fareTot]);
            //Create a HTML Table element.
            var table = document.getElementById(dateid);
            for (var i = 0; i < table.rows.length; ) {
                table.deleteRow(i);
            }
            //alert(table);
            //alert("distTable_" + dateid)
            //var table = document.createElement("TABLE");
            table.border = "2";

            //Get the count of columns.
            var columnCount = distances[0].length;

            //Add the header row.
            var row = table.insertRow(-1);
            for (var i = 0; i < columnCount; i++) {
                var headerCell = document.createElement("TH");
                headerCell.innerHTML = distances[0][i];
                row.appendChild(headerCell);
            }

            //Add the data rows.
            for (var i = 1; i < distances.length; i++) {
                row = table.insertRow(-1);
                for (var j = 0; j < columnCount; j++) {
                    var cell = row.insertCell(-1);
                    cell.innerHTML = distances[i][j];
                }
            }
            //alert($cR.cells[0].children[1])
            $cR.children[2].value = "-";
            $cR.children[2].innerHTML = "-";

        }
        else {
            //alert('tes111');
            var table = document.getElementById(dateid);
            for (var i = 0; i < table.rows.length; ) {
                table.deleteRow(i);
            }
            $cR.children[2].value = "+";
            $cR.children[2].innerHTML = "+";

        }

    }
</script>
</html>
