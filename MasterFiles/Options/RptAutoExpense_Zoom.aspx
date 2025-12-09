<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptAutoExpense_Zoom.aspx.cs" Inherits="MasterFiles_MR_RptAutoExpense" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

    <script type="" language="javascript">


        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }


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
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
                <td width="100%">
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
     <center> 
         

           <table  width="100%" align="center" style="margin-left:5%">
                <tbody>
                    <tr>
                    
                        <td align="center">
               
         <font size="3" face="Verdana, Arial, Helvetica, sans-serif"><b>TABLETS INDIA LIMITED <p> 
             <span id="divid" runat="server"></span>  DIVISION <p> TRAVEL EXPENSES STATEMENT  FOR THE MONTH OF 
                 <span id="mnthtxtId" runat="server"></span>-<span id="yrtxtId" runat="server"></span>
             <span id="iddNtTot" runat="server" style="position:absolute;padding:10px;border:solid 1px #000000;text-align:right;top:33px;right:5%;font-family:Times New Roman;font-weight:bold;font-size:50px;">
             </span></b></font>
        </td>
        </tr>
     

        </tbody>
               </table>
             
<br /> 
  <div id="msgId" runat="server" visible="false">
 
  <center><font size="3" face="Verdana, Arial, Helvetica, sans-serif" color="Red"><b>Your Expense Not Yet Approved...</b></font></center>
  <br/>
 </div>
 <div id="MainId1" runat="server" visible="true" style="margin-left:0.5px">
  <table align="center" width="100%" style="margin-left:0.5px">
<tr>
<td align="left"   style="font-weight:bold;font-size:12px" id="fieldforceId" runat="server"></td>
    <td>&nbsp;</td>
<td align="center" style="font-weight:bold;font-size:12px" id="empId" runat="server"></td>
    <td>&nbsp;</td>
    <td align="center" style="font-weight:bold;font-size:12px" id="hqdesig" runat="server"></td>
    <td>&nbsp;</td>
    <td align="center" style="font-weight:bold;font-size:12px" id="hqid" runat="server"></td>
    <td>&nbsp;</td>
    <td align="center" style="font-weight:bold;font-size:12px" id="mgrName" runat="server"></td>
    <td>&nbsp;</td>
    </tr>
      <tr height="10px"></tr>
     
</table>
     <table  width="100%" align="center" style="margin-left:0.5px">
                <tbody>
                    <tr>
                    
                        <td align="right">
                            <asp:GridView ID="grdExpMain" runat="server" Width="100%" Font-Size="8pt" 
                            HeaderStyle-BackColor="#FFEFD5" HeaderStyle-CssClass="mainGrid" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="black"  HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" cellpadding="10" cellspacing="5">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" Visible="false">
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
                                    
                                    <asp:TemplateField HeaderText="Work Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkType" runat="server" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Place of Work" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTerrName" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" >
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                     <table>
                                            <tr>
                                             <td width="100px" align="left"><asp:Label ID="lblFrom" runat="server" Text='<%# Bind("From_place") %>'></asp:Label></td>
                                             <td width="150px" align="right"><asp:Label ID="lblTo" runat="server" Text='<%# Bind("To_place") %>'>'></asp:Label></td>
                                            </tr>
                                     </table>
                                     <table id="MT_table" class="disttable" runat="server"></table>
                                        <button id="dtlBtn" runat="server" style="visibility:hidden" onclick="distTable(this);return false">+</button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Allowance Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCat" runat="server" Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distance Travelled</br>(in Kms)" ItemStyle-HorizontalAlign="Left">
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
                                     <asp:TemplateField HeaderText="DA (in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAllw" runat="server" Text='<%# Bind("Allowance") %>'>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                     
                                    <asp:TemplateField HeaderText="Additional Expense</br>(in Rs/-)" Visible="false" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbaddExpamt" runat="server" Text='<%# Bind("rw_amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Remarks" Visible="true" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbaddExp" runat="server" Text='<%# Bind("rw_rmks") %>'></asp:Label>
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
           <table width="100%" align="center" style="margin-left:0.5px">
            <tr><td>&nbsp;</td></tr>
            <tr>
<td align="left">
<div align="center" id="divAttach" runat="server" style=" border: 1px; border-style: solid; background-color: #ffebcd;
                        color: Black; height: 50px">
                        <br />
                        <a id="aTagAttach" target="_blank" runat="server">
                            <asp:Label ID="lblViewAttach" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                        </a>
                        <br />
                    </div>
                    </td>
                    
                    </tr>
                    <tr><td colspan="3" valign="top">&nbsp;
					
                  
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
					
					
                    <td></td><td></td>
					</tr>
    				<tr><td colspan="3" valign="top">&nbsp;
					
                  
<asp:GridView ID="GridViewExcessfare" runat="server" Width="40%" Font-Name="Verdana"  Font-Size="8pt" 
            HeaderStyle-BackColor="#FFEFD5" ShowHeader="false"
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
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" Wrap="false" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="excesremks" runat="server" Text='<%# Bind("Exp_param_name") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="excesallwrmks" Width="20px" runat="server" Text='<%# Bind("Amt") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                               </Columns>
                                
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
</td>
					
					
                    <td></td><td></td>
					</tr>

                
						<tr ><td align="left" id="fixtot" runat="server" ><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b> </td>
                            
                            
                            

						</tr>
					 
	                    
<tr>
	<td colspan='5' id="admnexp1" runat="server" visible="true">
                        
        <asp:GridView ID="adminExpGrid" runat="server"  Width="60%" Font-Name="Verdana"  Font-Size="8pt"  ShowFooter="true"
            HeaderStyle-BackColor="#FFEFD5"  OnRowDataBound="grdExpense_RowDataBound"
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
                                           <asp:Label ID="lbldt" runat="server" Text='<%# Bind("adminAdjDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MisExp" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblMis" runat="server" Text='<%# Bind("Expense_Parameter_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTyp" runat="server" Text='<%# Bind("Typ") %>'></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lblTotal" style="color:Red;font-weight:bold" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmt" runat="server" Text='<%# Bind("amt") %>'></asp:Label>
                                        </ItemTemplate>
                                          <FooterTemplate>
                                       <asp:Label ID="ftlblzoomtotal" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                         </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Paritulars") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                               </Columns>
                                
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
        <br><br>
						<table border="1"  cellpadding="10" id="wtydet" runat="server" visible="true">
							
                            <tr>
                            <td><asp:Label align="center" ID="noHQ" runat="server">Total No HQ.</asp:Label></td><td ID="valnoHQ" width="20px" align="center" runat="server"></td>
        <td><asp:Label ID="noEX" align="center" runat="server">Total No EX.</asp:Label></td><td ID="valnoEX" width="20px" align="center" runat="server"></td>
        <td><asp:Label ID="noOS" align="center" runat="server">Total No OS.</asp:Label></td><td ID="valOS" width="20px" align="center" runat="server"></td>
         <td><asp:Label ID="totleave" align="center" runat="server">Total No Leave.</asp:Label></td><td ID="totleaveval" runat="server" align="center" width="20px"></td></tr>
        <tr><td><asp:Label ID="noholi" align="center" runat="server">Total No Holiday.</asp:Label></td><td ID="totholiday" align="center" runat="server"></td>
        <td><asp:Label ID="nomeet" align="center" runat="server">Total No Meeting.</asp:Label></td><td ID="totmeet" align="center" runat="server"></td>
        <td><asp:Label ID="notans" align="center" runat="server">Total No transit</asp:Label></td><td ID="tottrans" align="center" runat="server"></td>
        <td><asp:Label ID="totsunday" align="center" runat="server">Total No sunday</asp:Label></td><td ID="totsundayval" align="center" runat="server"></td></tr>
        <tr><td><asp:Label ID="totnotrng" align="center" runat="server">Total No Training</asp:Label></td><td ID="tottraining" align="center" runat="server"></td>
         <td><asp:Label ID="totsundy" align="center" runat="server">Total No Other works</asp:Label></td><td ID="totothr" align="center" runat="server"></td>
            <td align="center">Total No Missed</td><td align="center" runat="server" id="totmissed"></td>
        </tr>
               <tr><td colspan="8" align="center" id="tottypcnt" runat="server"><b></b></td></tr>
						</table>
                    </td>
   <td colspan="4"></td>
<td colspan="4" rowspan="2" valign="top" nowrap style="padding-top:10px;">
						<br><br>
						
    <table border="1" style="width:100%" cellpadding="5">
                             <tr>
<td align="center" style="white-space:pre">Fare Total (Current Month + Previous Month):</td>
  <td style="text-align:right;font-family:Times New Roman;font-weight:bold;font-size:10px"><asp:Label  runat="server" ID="lblfaretot">0</asp:Label></td>

 </tr>
                                <tr>
<td align="center" style="white-space:pre">Allowance Total(Current Month + Previous Month):</td>
 <td style="text-align:right;font-family:Times New Roman;font-weight:bold;font-size:10px"><asp:Label  runat="server" ID="lblallwtot">0</asp:Label></td>


 </tr>
                                <tr>
<td align="center" style="white-space:pre">Addition/Deletion Expenses Total:</td>
<td style="text-align:right;font-family:Times New Roman;font-weight:bold;font-size:10px"><asp:Label  runat="server" ID="lbladddele">0</asp:Label></td>

 </tr>
                              <tr>
<td align="center" style="white-space:pre">Miscellaneous Total:</td>
 <td style="text-align:right;font-family:Times New Roman;font-weight:bold;font-size:10px"><asp:Label  runat="server" ID="lblmisc">0</asp:Label></td>


 </tr>
 <tr>
<td align="center" style="white-space:pre">Total Expense:</td>
 <td style="text-align:right;font-family:Times New Roman;font-weight:bold;font-size:10px"><asp:Label  runat="server" ID="totexp1">0</asp:Label></td>

 </tr>
                              <tr>
<td align="center" style="white-space:pre">LOP(for admin use):</td>
<td style="text-align:right;font-family:Times New Roman;font-weight:bold;font-size:10px"><asp:Label  runat="server" ID="lbllop">0</asp:Label></td>

 </tr>
        <tr><td align="center" nowrap style="padding-top:10px;">Net Payable</td>
                                <td valign="middle" id='grandTotalName' runat="server" style="text-align:right;font-family:Times New Roman;font-weight:bold;font-size:50px">0.00</td></tr>
    </table>
					</td>					
                    <td colspan="2" style="display:none" valign="top" style="padding-top:10px;">
						
					</td>
</tr> 


</table>

<br />
 


<div id="misExp" visible="false" runat="server">
<br />
<table width="90%" border="0" align="center" id="rmks">
<tr><td align="center"><b class="subheading"><font color='blue'>Admin Remarks : - </font></b>&nbsp;<textarea name="approveTextId" id="approveTextId" runat="server" cols="50" rows="5" ></textarea>
</td>
</tr>
</table>

    <asp:HiddenField ID="otherExpValues" runat="server" Value="" /><asp:HiddenField ID="hidtamtval" runat="server" Value="0" /><asp:HiddenField ID="frmTovalues" runat="server" Value="" />
<br />
<br />
<br />
    </div>
    </div>
    </asp:Panel>
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
                if (data[4] == '' || data[4] == 'undefined')
                    data[4] = 0;
                distances.push([data[0], data[1], data[2] + ' X ' + data[4], data[3]]);
                if (data[2] == '' || data[2] == 'undefined')
                    data[2] = 0;
                if (data[3] == '' || data[3] == 'undefined')
                    data[3] = 0;
                fareTot = fareTot + parseFloat(data[3]);
                distTot = distTot + parseFloat(data[2]);
                if (fareTot == NaN || fareTot == '')
                    fareTot = 0;
                if (distTot == NaN || distTot == '')
                    distTot = 0;
            }
            distances.push(["Total", "", distTot, fareTot]);
            //Create a HTML Table element.
            var table = document.getElementById(dateid);
            for (var i = 0; i < table.rows.length;) {
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
            for (var i = 0; i < table.rows.length;) {
                table.deleteRow(i);
            }
            $cR.children[2].value = "+";
            $cR.children[2].innerHTML = "+";

        }

    }
</script>
</html>
