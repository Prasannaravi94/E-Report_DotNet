<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptAutoExpense_Mgr_New.aspx.cs" Inherits="MasterFiles_MR_RptAutoExpense" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
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
}
.removeMainDiv
{
    background-color:White;
}sf
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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="" language="javascript">
        function TerritoryView(sfCode, divCode) {
            window.open("Territory_View.aspx?sfCode=" + sfCode + "&divCode=" + divCode, 'TerritoryView', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
        }
        function sfcView(sfCode, divCode) {
            // alert("sss");
            window.open("/../masterfiles/Reports/Distance_fixation_view.aspx?sfCode=" + sfCode + "&divCode=" + divCode, 'sfcView', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
        }

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

            $totEle1 = document.getElementById("adddeducttot");
            var $tot1 = parseFloat($totEle1.value.replace(/,/g, ''));
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
            grandtotalcalc($temp, $tot);
            grandtotalcalcTop($temp, $tot);
            grandtotalcalc123($temp, $tot);
            grandtotalcalcadddeduct($temp, $tot);
            $totEle.value = $temp;
            $totEle1.value = $temp;

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
            var $Fr = $R.cells[0].children[0].value.split("##")[1];
            //alert($Fr);
            if ($Fr == '') {
                $limit = 0;

            }
            else {
                $limit = $Fr;
            }
            return $limit;
        }




        function OthExpCalcFare($OthVal) {
            $OthTotValEle = document.getElementById("fartot");
            var othVal = parseFloat($OthTotValEle.value);
           // alert(othVal);
            var newval = parseFloat($OthVal.value);
           // alert(newval);
            $grndTot = document.getElementById("grandTotalName");
            var grndVal = parseFloat($grndTot.innerHTML);
            if (isNaN(grndVal)) grndVal = 0;
            if (isNaN(newval)) newval = 0;
            if (isNaN(othVal)) othVal = 0;
           // alert(grndVal + ":" + othVal + ":" + newval)
            var newtotval = grndVal - othVal + newval;
           // alert(newtotval);
            $OthTotValEle.value = newval;
            grandtotalcalc(newtotval, grndVal);
            grandtotalcalcTop(newtotval, grndVal);
            grandtotalcalcfaare(newtotval, grndVal);
            grandtotalcalc123(newtotval, grndVal);
        }

        function OthExpCalcAllw($OthVal) {
            $OthTotValEle = document.getElementById("allwtot");
            var othVal = parseFloat($OthTotValEle.value);
            // alert(othVal);
            var newval = parseFloat($OthVal.value);
            // alert(newval);
            $grndTot = document.getElementById("grandTotalName");
            var grndVal = parseFloat($grndTot.innerHTML);
            if (isNaN(grndVal)) grndVal = 0;
            if (isNaN(newval)) newval = 0;
            if (isNaN(othVal)) othVal = 0;
            //alert(grndVal + ":" + othVal + ":" + newval)
            var newtotval = grndVal - othVal + newval;
            //alert(newtotval);
            $OthTotValEle.value = newval;
            grandtotalcalc(newtotval, grndVal);
            grandtotalcalcTop(newtotval, grndVal);
            grandtotalcalcallw(newtotval, grndVal);
            grandtotalcalc123(newtotval, grndVal);
        }

        function OthExpCalc($OthExpVal) {
            $OthExpTotValEle = document.getElementById("Othtotal");
            var othExpVal = parseFloat($OthExpTotValEle.value);
            //alert(othExpVal);

            var $maxLimit = parseFloat(getMaxMisLmtVal($OthExpVal));
            var amt = parseFloat($OthExpVal.parentNode.parentNode.cells[1].children[0].value);
            // alert($maxLimit+" ff "+amt);
            //alert(amt>=$maxLimit);
            if ($maxLimit > 0 && amt > $maxLimit) {
                alert("Amount should be less than equal to " + $maxLimit);
                $OthExpVal.parentNode.parentNode.cells[1].children[0].value = 0;
                //othExpVal=0;
            }

            var $R = $OthExpVal.parentNode.parentNode.parentNode;
            var $Tot = 0;
            var $temp = 0;
            var paramval1 = "";
            for (var $rl = 1; $rl < $R.children.length; $rl++) {
                $temp = $R.children[$rl].cells[1].childNodes[0].value.replace(/,/g, '');
                paramval1 = $R.children[$rl].cells[0].children[0].options[$R.children[$rl].cells[0].children[0].selectedIndex].value
                if (paramval1 == 0) {
                    alert("Please select Type");
                    $R.children[$rl].cells[1].childNodes[0].value = 0;
                    return;
                }
                if (isNaN($temp) || $temp == '') $temp = 0;
                $Tot = parseFloat($Tot) + parseFloat($temp);

            }
            $OthExpTotValEle.value = $Tot;
            //alert($Tot);
            //alert(othExpVal);
            grandtotalcalc($Tot, othExpVal);

        }
        function rmksval($OthExpVal) {

            var $R = $OthExpVal.parentNode.parentNode.parentNode;
            var $Tot = 0;
            var $temp = 0;
            var paramval1 = "";
            for (var $rl = 1; $rl < $R.children.length; $rl++) {
                $temp = $R.children[$rl].cells[1].childNodes[0].value.replace(/,/g, '');
                paramval1 = $R.children[$rl].cells[0].children[0].options[$R.children[$rl].cells[0].children[0].selectedIndex].value
                if (paramval1 == 0) {
                    alert("Please select Type");
                    $R.children[$rl].cells[2].childNodes[0].value = "";
                    return;
                }


            }


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

        function totalcalc(newvalue, oldvalue, $tot) {
            //$tot = document.getElementById("TotalVal");
            var totval = parseFloat($tot.innerHTML.replace(/,/g, ''));
            $tot.innerHTML = totval - oldvalue + newvalue;
        }
        function grandtotalcalc(newvalue, oldvalue) {
            $grndTot = document.getElementById("grandTotalName");
            var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
            //$grndTot.innerHTML = grndVal - oldvalue + newvalue;
            $grndTot.innerHTML = AmtFormat((grndVal - oldvalue + newvalue), false);
        }
        function grandtotalcalcTop(newvalue, oldvalue) {
            $grndTot = document.getElementById("iddNtTot");
            var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
            $grndTot.innerHTML = AmtFormat((grndVal - oldvalue + newvalue), false);
        }
        function grandtotalcalcfaare(newvalue, oldvalue) {
            
            $grndTot = document.getElementById("lblfaretot");
            var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
            
            
            $grndTot.innerHTML = AmtFormat((grndVal - oldvalue + newvalue), false);
        }
        function grandtotalcalcallw(newvalue, oldvalue) {
            $grndTot = document.getElementById("lblallwtot");
            var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
            $grndTot.innerHTML = AmtFormat((grndVal - oldvalue + newvalue), false);
        }
        function grandtotalcalcadddeduct(newvalue, oldvalue) {
            $grndTot = document.getElementById("lbladddele");
            var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
            $grndTot.innerHTML = AmtFormat((grndVal - oldvalue + newvalue), false);
        }
        function grandtotalcalc123(newvalue, oldvalue) {
            $grndTot = document.getElementById("totexp1");
            // $grndTot1 = document.getElementById("totexp");
            // var grndVal1 = parseFloat($grndTot1.innerHTML.replace(/,/g, ''));
            var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
            // $totEle1 = document.getElementById("totexp1");
            //var $tot1 = parseFloat($totEle1.value.replace(/,/g, ''));
            $grndTot.innerHTML = AmtFormat((grndVal - oldvalue + newvalue), false);
            // $grndTot1. = grndVal1 - oldvalue + newvalue;
            // $totEle1.value = grndVal1 - oldvalue + newvalue;
        }
        document.querySelector("input").addEventListener("change", function () {
            alert("Input Changed");
        })


        function validate() {
            if (document.getElementById("monthId").value == "0") {
                alert('Select the Month');
                document.getElementById("monthId").focus();
                return false;
            }
            else if (document.getElementById("yearID").value == "0") {
                alert('Select the Year');
                document.getElementById("yearID").focus();
                return false;
            }
        }
        function AmtFormat(Amt, $f) { var Amt = new String(Amt); Amt = Amt.replace(/,/g, ''); if (Amt.indexOf(".", 0) > -1) { var Amtpart = Amt.split("."); if (Amtpart[1].length == 1) Amt = Amt + "0"; else Amt = Amt; } else { var Amt = Amt + ".00"; } if ($f == false) { var Amtpart = Amt.split("."); var tmpAmt = ""; var $i = 1; var j = 0; for (IntLoop = Amtpart[0].length - 1; IntLoop >= 0; IntLoop--) { tmpAmt = tmpAmt + Amtpart[0].substring(IntLoop, (Amtpart[0].length - (tmpAmt.length - j))); $i++; if ($i == 4 || $i == 6 || $i == 8) if (IntLoop > 0) { tmpAmt = tmpAmt + ","; j++; } } Amtpart[0] = tmpAmt; tmpAmt = ""; for (IntLoop = Amtpart[0].length - 1; IntLoop >= 0; IntLoop--) { tmpAmt = tmpAmt + Amtpart[0].substring(IntLoop, (Amtpart[0].length - tmpAmt.length)); } var Amt = tmpAmt + "." + Amtpart[1]; } return (Amt); }
    </script>
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
</head>
<body class="bodycss">
    <form id="form1" runat="server">
     <div class="mainDiv" id="mainDiv" runat="server" >
    <div id="menuId" runat="server">
    <ucl:Menu ID="menu1" runat="server" /></div>
     <div id="monthyearDiv" runat="server" >
    <center>
    <table align="center">
    <tr><td><u><font color="#800000" size=4>Expense Statement</font></u><br/><br/></td></tr>
     
        <tr><td><asp:dropdownlist ID="monthId" runat="server"></asp:dropdownlist></td> 
   <td><asp:DropDownList ID="yearID" runat="server"></asp:DropDownList></td>
        <td><asp:Button ID="btnSubmit" runat ="server" Text ="Go" CssClass="savebutton" OnClientClick="return validate();"
            onclick="btnSubmit_Click" /></td></tr>
  </table></center></div>
  <br />
  <div id="heading" runat="server" visible="false">
  <asp:HiddenField ID="sfCode" runat="server" Value="" /><asp:HiddenField ID="divCode" runat="server" Value="" />
      <center>
    
                    <font size="3" face="Verdana, Arial, Helvetica, sans-serif"><b>TABLETS INDIA LIMITED <p> <span id="divid" runat="server"></span>  DIVISION <p> TRAVEL EXPENSES STATEMENT  FOR THE MONTH OF <span id="mnthtxtId" runat="server"></span>-<span id="yrtxtId" runat="server"></span><span id="iddNtTot" runat="server" style="position:absolute;padding:10px;border:solid 1px #000000;text-align:right;top:33px;right:5%;font-family:Times New Roman;font-weight:bold;font-size:50px;"></span></b></font><br /><br />


</center>
   <table align="center" width="100%">
<tr>
<td align="left" width="25%"><asp:label Font-Bold="true" ForeColor="red" runat="server" Visible="false" id="messageId" Text=""></asp:label></td>
<td align="center" style="font-weight:bold" width="50%" ><font size="3"  face="Verdana, Arial, Helvetica, sans-serif" color="maroon"><b><u>
    
<td align="right" width="25%">&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="savebutton" Height="25px" Width="60px" Text="Back" onClick="btnBack_Click" /></td>
</tr>
</table>
<br/>
 <table align="center" width="100%">
<tr>
<td align="left"  style="font-weight:bold;font-size:small;" id="fieldforceId" runat="server"></td>
<td align="center" style="font-weight:bold;font-size:small;" id="empId" runat="server"></td>
    <td align="center" style="font-weight:bold;font-size:small;" id="hqdesig" runat="server"></td>
    <td align="right" style="font-weight:bold;font-size:small;" id="hqid" runat="server"></td>
    <td align="right" style="font-weight:bold;font-size:small;" id="mgrName" runat="server"></td>
</tr>
</table></div>
     <table  width="100%" align="center">
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
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkType" runat="server" Text='<%#Eval("Worktype_Name_M")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Place of Work" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" Width="300px" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           <asp:Label ID="lblTerrName" runat="server" Visible="true" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            <asp:TextBox ID="txtterr" Visible="false" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:TextBox>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Allowance Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:Label ID="lblCat" runat="server" Text='<%# Bind("Type_Code") %>'></asp:Label>
                                        <asp:DropDownList  onchange="onAllowTypeComboChange(this,'false','')" class="from" ID="AllowType" Visible="false" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                                                <asp:ListItem Selected="false" Text="---Select---" Value=""></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="HQ" Value="HQ"></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="EX" Value="EX"></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="OS" Value="OS"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="allowTypeHidden" runat="server" Value='<%# Bind("Type_Code") %>' />
                                            <asp:HiddenField ID="allowTypeHidden1" runat="server" Value='<%# Bind("Type_Code") %>' />
                                            <asp:HiddenField ID="allowTypeHidden2" runat="server" Value='<%# Bind("temtyp") %>' />
                                            <asp:HiddenField ID="Hiddenpls" runat="server" Value='<%# Bind("TerrPlaces") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Allowance (in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:Label ID="lblAllw" style="display:none"  Visible="true" runat="server" Text='<%# Bind("Allowance") %>'></asp:Label>
                                            <asp:TextBox ID="txtAllow" width="50" style="display:block;width:50px" visible="true" ReadOnly="true" runat="server" Text='<%# Bind("Allowance") %>' onkeyup="onAllowChange(this)"></asp:TextBox><asp:HiddenField ID="allowHidden" runat="server" Value='<%# Bind("Allowance") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Local Conveyance" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           <asp:Label ID="lbllocalconvey" runat="server" Text='<%# Bind("Local_convey") %>'></asp:Label>
                                            <asp:HiddenField ID="hdnlocal" runat="server"  Value='<%# Bind("Local_convey") %>'/>
                                            
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Total Allowance" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           <asp:Label ID="lbltotalconvey" runat="server" Text='<%# Bind("Total_allowance") %>'></asp:Label>
                                            <asp:HiddenField ID="hdntotAllw" runat="server"  Value='<%# Bind("Total_allowance") %>'/>
                                            
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Bill Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                         <asp:DropDownList onchange="onHotelTypeChange(this,'false','')"  ID="hotelType" runat="server" style="display:none" AutoPostBack="false" SkinID="ddlRequired">
                                                
                                             <asp:ListItem Selected="false" Text="Without Bill" Value="Without Bill"></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="With Bill" Value="With Bill"></asp:ListItem>
                                                
                                           </asp:DropDownList>
                                           <asp:HiddenField ID="hotelTypeHidden" runat="server" Value='' />
                                           </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From and To" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" >
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                     <table id="frmToTableId" runat="server">
                                           <tr>
                                           <td align="left"><asp:Label ID="lblFrom" runat="server" Text='<%# Bind("From_place") %>'> </asp:Label>
                                             <asp:HiddenField ID="fromHidden" runat="server" Value='<%# Bind("From_place") %>' /></td>
                                           <td align="left"><asp:Label ID="lblTo" runat="server" Text='<%# Bind("To_place") %>'></asp:Label><asp:DropDownList onchange="_CalDist(this,'sss','rrr');" class="to" ID="toPlace" Visible="false" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                                                <asp:ListItem Selected="false" Text="---Select---" Value=""></asp:ListItem>
                                            </asp:DropDownList><asp:HiddenField ID="toHidden" runat="server" Value='<%# Bind("To_place") %>' /></td>
                                            <td><asp:HiddenField ID="distPrev" runat="server" Value="0" /></td>
                                            </tr>
                                     </table>
                                        <table id='<%# Eval("Adate") %>' class="disttable"></table>
                                        <button id="dtlBtn" runat="server" style="visibility:hidden" onclick="distTable(this);return false">+</button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Distance Travelled</br>(in Kms)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:Label ID="lblDistance" runat="server" Text='<%# Bind("Distance") %>'></asp:Label><asp:HiddenField ID="distHidden" runat="server" Value='<%# Bind("Distance") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Fare</br>(in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:TextBox ID="txtFare" onkeyup="onFareChange(this);" visible="false" width="30" runat="server" Text='<%# Bind("Fare") %>'></asp:TextBox><asp:Label ID="lblFare" runat="server" Text='<%# Bind("Fare") %>'></asp:Label><asp:HiddenField ID="fareHidden" runat="server" Value='<%# Bind("Fare") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                     <asp:TemplateField HeaderText="Total Amt</br>(in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total") %>'></asp:Label><asp:HiddenField ID="totHidden" runat="server" Value='<%# Bind("Total") %>' /></ItemTemplate>
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
             <table align="center" id="twdid" width="100%" class="tblHead" runat="server" visible="false">
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
           <table width="100%" align="right">
            <tr><td colspan="10">&nbsp;</td></tr>
            <tr><td colspan="10" align="right""><div id="twdys1" runat="server" Visible="false" ><font color="red">Total working Days:-<label id="Tworkdys" runat="server"></label></font></div></td></tr>
             <tr>
            <td colspan="2">
                <div id="dvPage" runat="server" visible="false">
                    <span style="font-family: Verdana; font-size: 8pt; color: Red; font-weight: bold">Note:
                        If you have more Images for Upload, Put all the Images is one Folder and  <br /> right                       
                        click the Folder--> click "Send to" --> again click "Compressed (Zip folder)"
                        <br />
                        then all the Images are Converted to "Zip Folder"</span>
                </div>
            </td>
            </tr>

<tr>
<td width="50%" valign="top">
                  <div id="divAttach" visible="false" style="border: 1px; border-style: solid; background-color: #ffebcd;
                     color: Black" runat="server">
                     <p style="text-decoration: underline; font-family: Verdana; text-align: center; font-weight: bold">
                         Scan Bill Upload
                     </p>
                     <br />
                     <p style="font-family: Verdana; text-align: center">
                         <asp:LinkButton ID="lnkAttachment" runat="server" style="display:none" Text="Click here to Upload the Bills"></asp:LinkButton>
                     </p>
                     <p style="font-family: Verdana; text-align: center">
                         <asp:Label ID="lblAttachment" runat="server" style="display:none" Font-Bold="true"></asp:Label>
                         <asp:LinkButton CssClass="print" Style="font-family: Verdana;display:none" SkinID="dfs" ID="lbFileDel"
                             runat="server" Text="Remove" OnClick="lbFileDel_Onclick"></asp:LinkButton>
                             <asp:FileUpload ID="FileUpload2" runat="server" /></p>
                     <br />
                 </div>
                   <div align="center" id="divLinkattach" visible="false" runat="server" style=" border: 1px; border-style: solid; background-color: #ffebcd;
                        color: Black; height: 50px">
                        <br />
                        <a id="aTagAttach" target="_blank" runat="server">
                            <asp:Label ID="lblViewAttach" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                        </a>
                        <br />
                    </div>
                    </td>
<td colspan="8" align="right">

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
                                     <asp:TemplateField HeaderText="Fixed Expenses</br>(Per day in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center" Wrap="false">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblSexpName" runat="server" Text='<%# Bind("Expense_Parameter_Name") %>'></asp:Label>
                                            <asp:HiddenField ID="hdnSexpName" runat="server" Value='<%#Eval("Expense_Parameter_Code")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fixed Amount" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSexpAmnt" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Actual Amount(Pro Rate)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblProAmnt" runat="server" Text='<%# Bind("Pro_rate") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                               </Columns>
                                
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
</td></tr>
                
                <tr runat="server" id="excesid" visible="false">
                    <td width="50%"></td>
                    <td colspan="8" align="right">
                        <table border="1" width="40%" align="right">
                             <tr>
<td align="center" style="white-space:pre">Excess Fare of Previous Month</td>

<td style="width:50px"><asp:TextBox runat="server"  name="excesfare" id="excesfare"  value="" size='6' maxlength=4 onkeyup="OthExpCalcFare(this)"></asp:TextBox></td>
<td><asp:TextBox  name="OthExpRmk" id="excesremks" runat="server" value=""  maxlength="150" onkeyup="rmksval(this)"></asp:TextBox></td>
 </tr>
                            <tr>
                
<td align="center" style="white-space:pre">Excess Allowance of Previous Month</td>

<td style="width:50px"><asp:TextBox  runat="server"  name="excesallw" id="excesallw"  value="" size='6' maxlength=4 onkeyup="OthExpCalcAllw(this)"></asp:TextBox></td>
<td><asp:TextBox  name="OthExpRmk" runat="server" id="excesallwrmks" value="" maxlength="150" onkeyup="rmksval(this)"></asp:TextBox></td>
 </tr>
                            </table>
                </td>
                    </tr>
                <tr height="20px"></tr>
           <%-- 
</table>
        

            </table>--%>
</table>
<br />
<div id="misExp" visible="false" runat="server">
<br />
<br />
<br />
    <table width="100%"  border="0">
        <tr>
            <td align="left" width="50% ">
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
                                <td><input type="button" value=" + " class='btnSave' onclick="_AdRowByCurrElem(this); clrAmntValue(this);" /></td>
                                <td><input type="button" value=" - " class='btnSave' onclick="DRForAdmin(this, this.parentNode.parentNode, 1)"/></td>
                            </tr>
            </table>

                </td>
            <td align="right" width="50%">
<table border="1" width="100%" align="right">
                             <tr>
<td align="center" style="white-space:pre">Fare Total (Current Month + Previous Month):</td>
  <td align="right"><asp:Label  runat="server" ID="lblfaretot">0.00</asp:Label></td>

 </tr>
                                <tr>
<td align="center" style="white-space:pre">Allowance Total(Current Month + Previous Month):</td>
 <td align="right"><asp:Label  runat="server" ID="lblallwtot">0.00</asp:Label></td>


 </tr>
                                <tr>
<td align="center" style="white-space:pre">Addition/Deletion Expenses Total:</td>
<td align="right"><asp:Label  runat="server" ID="lbladddele">0.00</asp:Label></td>

 </tr>
                              <tr>
<td align="center" style="white-space:pre">Miscellaneous Total:</td>
 <td align="right"><asp:Label  runat="server" ID="lblmisc">0.00</asp:Label></td>


 </tr>
 <tr>
<td align="center" style="white-space:pre">Total Expense:</td>
 <td align="right"><asp:Label  runat="server" ID="totexp1">0.00</asp:Label></td>

 </tr>
                              <tr>
<td align="center" style="white-space:pre">LOP(for admin use):</td>
<td align="right"><asp:Label  runat="server" ID="lbllop">0.00</asp:Label></td>

 </tr>
    </table>
            </td>
            </tr>
            </table>
<table width="100%"  border="0">
 <tr><td  style="padding-top:10px;text-align:right;color:red; font-family:Times New Roman;font-weight:bold;font-size:30px">Grand Total : </td>
     <td style="padding-top:10px;color:red;text-align:right;font-family:Times New Roman;font-weight:bold;font-size:30px" runat="server" id="grandTotalName">0</td></tr></table> <asp:HiddenField ID="otherExpValues" runat="server" Value="" /><asp:HiddenField ID="Othtotal" runat="server" Value="0" /><asp:HiddenField ID="distString" runat="server" Value="" /><asp:HiddenField ID="allowString" runat="server" Value="" /><asp:HiddenField ID="allowString1" runat="server" Value="" /><asp:HiddenField ID="allowString2" runat="server" Value="" /><asp:HiddenField ID="frmTovalues" runat="server" Value="" /><asp:HiddenField ID="hidtamtval" runat="server" Value="" />
    <asp:HiddenField ID="fartot" runat="server" Value="0" /><asp:HiddenField ID="allwtot" runat="server" Value="0" /><asp:HiddenField ID="adddeducttot" runat="server" Value="0" /><asp:HiddenField ID="misstot" runat="server" Value="0" /><asp:HiddenField ID="totexp" runat="server" Value="0" />
<br />
<br />
               <center>
        <asp:Button ID="btnDrftSave" runat="server"  CssClass="savebutton" Width="80px" Height="25px"
            Text="Draft Save" Visible="false" OnClientClick="saveOtherExp()" OnClick="btnSaveDraft_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Width="80px" Height="25px"
            Text="Send To HO" Visible="true" OnClientClick="saveOtherExp()" OnClick="btnSave_Click" />
    </center>
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
        var misExp = document.getElementsByClassName("misExpList");
        var date = document.getElementsByClassName("date");
        var exp = 0, val = 0, gt = 0;
        var remarks = "";
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
                remarks = otherExpRmk[i].value;
                val = otherExpVal[i].value;
                exp = value + "=" + text;
            }
            else {
                dateValues = dateValues + "," + datevalue + "=" + datetext;
                misExpValues = misExpValues + "," + misExpvalue + "=" + misExptext;
                remarks = remarks + "," + otherExpRmk[i].value;
                val = val + "," + otherExpVal[i].value;
                exp = exp + "," + value + "=" + text;

            }

        }
        $grandTotalEle = document.getElementById("grandTotalName");
        var gt = parseFloat($grandTotalEle.innerHTML.replace(/,/g, ''));
        //alert(remarks + "~" + val + "~" + exp);
        document.getElementById("otherExpValues").value = remarks + "~" + val + "~" + exp + "~" + gt + "~" + dateValues + "~" + misExpValues;
    }
    function distTable($this) {
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
                //distances.push([data[0].replace(/,/g, ''), data[1].replace(/,/g, ''), data[2].replace(/,/g, '') + ' X ' + data[4.replace(/,/g, '') data[3].replace(/,/g, '')]);
                //fareTot = fareTot + parseFloat(data[3].replace(/,/g, ''));
                //distTot = distTot + parseFloat(data[2].replace(/,/g, ''));

            }
            distances.push(["Total", "", distTot, Math.round(fareTot*100)/100]);
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
            //alert('tes121');
            var table = document.getElementById(dateid);
            for (var i = 0; i < table.rows.length; ) {
                table.deleteRow(i);
            }
            $cR.children[2].value = "+";
            $cR.children[2].innerHTML = "+";

        }

    }
    function _CalDist($x, TotDist, fare) {
        var sVal = document.getElementById("allowString").value;
        //alert(sVal);
        var allowArr = sVal.split('@');
        fare = allowArr[3];
        //alert("fare :: " + fare);
        var idString = $x.id;
        var $i1 = idString.indexOf("_");
        $i1 = $i1 + 4;
        var $i2 = idString.indexOf("_", $i1);
        var prevValue = "";
        var PprevValue = "";
        var currValue = "";
        var nextValue = "";
        var pretyp = "";
        var pretyp1 = "";
        var curtyp = "";
        var curtyp1 = "";
        var nextEl;
        var prevEl;
        if ((idString.substring($i1, $i2) - 1) < 10) {

            var cIdprev = parseInt(idString.substring($i1, $i2)) - 1;
            prevEl = document.getElementById("grdExpMain_ctl0" + cIdprev + "_toPlace");
            if (prevEl != "" && prevEl != null)
                prevValue = document.getElementById("grdExpMain_ctl0" + cIdprev + "_toPlace").value;

            currValue = document.getElementById("grdExpMain_ctl" + (idString.substring($i1, $i2)) + "_toPlace").value;



            var cId = parseInt(idString.substring($i1, $i2)) + 1;

            if (cId < 10) {

                nextEl = document.getElementById("grdExpMain_ctl0" + cId + "_toPlace");
                if (nextEl != "" && nextEl != null)
                    nextValue = document.getElementById("grdExpMain_ctl0" + cId + "_toPlace").value;
            }
            else {
                nextEl = document.getElementById("grdExpMain_ctl" + cId + "_toPlace");
                if (nextEl != "" && nextEl != null)
                    nextValue = document.getElementById("grdExpMain_ctl" + cId + "_toPlace").value;
            }

            pretyp = prevValue.split('~~');
            pretyp1 = pretyp[1];

            curtyp = currValue.split('~~');
            curtyp1 = curtyp[1];

        }
        else {

            var cIdprev = parseInt(idString.substring($i1, $i2)) - 1;
            prevEl = document.getElementById("grdExpMain_ctl" + cIdprev + "_toPlace");
            if (prevEl != "" && prevEl != null)
                prevValue = document.getElementById("grdExpMain_ctl" + cIdprev + "_toPlace").value;

            currValue = document.getElementById("grdExpMain_ctl" + (idString.substring($i1, $i2)) + "_toPlace").value;
            var cId = parseInt(idString.substring($i1, $i2)) + 1;
            nextEl = document.getElementById("grdExpMain_ctl" + cId + "_toPlace");
            if (nextEl != "" && nextEl != null)
                nextValue = document.getElementById("grdExpMain_ctl" + cId + "_toPlace").value;

            pretyp = prevValue.split('~~');
            pretyp1 = pretyp[1];

            curtyp = currValue.split('~~');
            curtyp1 = curtyp[1];

        }
        var $Dis = 0;
        var $sDist = 0;
        var $Dis11 = "0@0".split('&');
        $distEle = document.getElementById("distString");

        SDis = $distEle.value;

        var selValue = $x.parentNode.children[1].selectedOptions[0].innerHTML;

        $x.parentNode.children[2].value = selValue;
        var $R = $x.parentNode.parentNode;
        //var $Fr = $R.cells[0].childNodes[1].value.split("~~");
        var $To = $R.cells[1].childNodes[1].value.split("~");
        //alert($To[0]);
        var $R1 = $x.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
        var type = $R1.cells[4].children[2].value;
        var typ2 = $R1.cells[4].children[3].value;
        if ("AE-FE" == type) {
            if ($To[0] != '') {
                $R1.cells[4].children[0].innerHTML = $To[1];
                $R1.cells[4].children[1].value = $To[1];
            }
            else {
                $R1.cells[4].children[0].innerHTML = "";
                $R1.cells[4].children[1].value = "";

            }
            return;
        }
        if ($To[0] != '') {
            if ("AS-FA" != type && "AS-FE" != type) {
                $R1.cells[4].children[0].innerHTML = $To[1];
                $R1.cells[4].children[1].value = $To[1];
            }
        }
        else {
            if ("AS-FA" != type && "AS-FE" != type) {
                $R1.cells[4].children[0].innerHTML = "";
                $R1.cells[4].children[1].value = "";
            }
            $To[1] = '';
        }
        //alert("type :: "+ type);
        if ("NA-FA" != type && "AS-FA" != type && "AS-FE" != type && "AE-FA" != type) {

        //    onAllowTypeChange($R1, 'true', $To[1]);
        if ("FA" == type || "FF" == type || "FD" == type) {

            onAllowTypeChange($R1, 'true', $To[1]);
        }
         else if("AA-FA" == type) {

            onAllowTypeComboChange($R1, 'true', $To[1]);
        }

        }
       
         
        else {
            if ("AS-FA" != type && "AS-FE" != type) {
                $R1.cells[4].children[0].innerHTML = "";
                $R1.cells[4].children[1].value = "";
            }
        }
        if ("AE-FE" == type || "AA-FE" == type || "AA-NF" == type || "AS-FE" == type) {
            return;
        }
        if ($To[0] == '') {

            $Dis = 0;
            $To[1] = "";
            $To[3] = "";
        }
        else {

            //var $st = SDis.indexOf($Fr[0] + "#" + $To[0] + "#");
            var $st = SDis.indexOf($To[0] + "#");
            if ($st > -1) {
                // $st = $st + ($Fr[0] + "#" + $To[0] + "#").length;
                $st = $st + ($To[0] + "#").length;
                var $et = SDis.indexOf("$", $st);

                var $et = SDis.indexOf("$", $st);
                $Dis11 = SDis.substring($st, $et).split('@');
                $Dis = $Dis11[0];
                $sDist = $Dis;

                if ($R1.cells[4].children.length > 0) {
                    if ($R1.cells[4].children[0].value == 'AS-FA' || $R1.cells[4].children[0].value == 'FA' || $R1.cells[4].children[0].value == 'FF' || $R1.cells[4].children[0].value == 'FD' || $R1.cells[4].children[0].value == 'FM' || $R1.cells[4].children[0].value == 'AE-FA' || $R1.cells[4].children[0].value == 'AA-FA' || $R1.cells[4].children[0].value == 'AA-FE'
            || $R1.cells[4].children[0].value == 'AA-NF' || $R1.cells[4].children[0].value == 'NA-FA' || $R1.cells[4].children[0].value == 'AE-FE') {
                        $Dis = $Dis;
                    }
                }
            }
        }
        var ffare = $Dis11[1];
        //if (typ2 == "FD" || $To[1] == "EX") {
            $Dis = $Dis * 2;
            ffare = ffare * 2;
//        }
//        else if (typ2 == "FM" && ($To[1] == "OS" || $To[1] == "OS-EX")) {
//            $Dis = 0;
//            ffare = 0;
//        }
//        else {
//            $Dis = $Dis;

//        }
        if (currValue == prevValue && $To[3] == "OS") {
            $Dis = 0;
            ffare = 0;

        }
        // $Dis = $Dis;
        //alert($Dis);
        var pValue = $R1.cells[7].children[1].value;
        pValue = parseFloat(pValue.replace(/,/g, ''));
        //alert($R1.cells[4].children[0].value);
        if ($R1.cells[4].children.length > 0) {
            if ($R1.cells[4].children.length > 0 && $R1.cells[4].children[0].value == '  ' || $R1.cells[4].children[0].value == 'Act  ' || $R1.cells[4].children[0].value == 'OS  ' || $R1.cells[4].children[0].value == 'EX  ' || $R1.cells[4].children[0].value == 'HQ  '
        || $R1.cells[4].children[0].value == 'FAct' || $R1.cells[4].children[0].value == 'EAAct') {
                pValue = pValue;
            }
        }
        //alert(pValue);
        if (isNaN($Dis)) $Dis = 0;
        if (isNaN(pValue)) pValue = 0;
        $R1.cells[7].children[1].value = $Dis;
        $R1.cells[7].children[0].innerHTML = $Dis;
        //if (isNaN(TotDist.value)) TotDist.value=0;
        //var totDist=parseFloat($R1.cells[7].children[0].innerHTML.replace(/,/g, ''));
        //var tDis = ((totDist + parseFloat($Dis)) - parseFloat(pValue));
        //$R1.cells[9].children[0].innerHTML=((totDist+parseFloat($Dis))-parseFloat(pValue));
        //alert("dist "+tDis);
        var oldFare = parseFloat($R1.cells[8].children[1].innerHTML.replace(/,/g, ''));
        if (isNaN(ffare)) ffare = 0;
        $R1.cells[8].children[0].innerHTML = parseFloat(parseFloat(ffare));
        $R1.cells[8].children[1].value = parseFloat(parseFloat(ffare));

        //        if ($Dis > 120) {
        //            $R1.cells[8].children[0].innerHTML = parseFloat(parseFloat($Dis) * parseFloat(fare));
        //            $R1.cells[8].children[1].value = parseFloat(parseFloat($Dis) * parseFloat(fare));
        //        }
        //        else {
        //            $R1.cells[8].children[0].innerHTML = parseFloat(parseFloat($Dis) * parseFloat(fare));
        //            $R1.cells[8].children[1].value = parseFloat(parseFloat($Dis) * parseFloat(fare));
        //        }
        var newFare = parseFloat($R1.cells[8].children[0].innerHTML.replace(/,/g, ''));
        //alert(oldFare + " f::f " + newFare);
        var allw = 0;
        if (type == "AE-FA") {
            allw = parseFloat($R1.cells[5].children[0].value.replace(/,/g, ''));
            $R1.cells[4].children[0].innerHTML = $To[1];
            $R1.cells[4].children[1].value = $To[1];
        }
        else {
            allw = parseFloat($R1.cells[5].children[0].innerHTML.replace(/,/g, ''));
        }
        //alert("ddd"+$R1.cells[4].children[0].children.length);
        if ($R1.cells[4].children[0].children.length == 4) {
            //allw = 0;
        }

        var oldTot = parseFloat($R1.cells[9].childNodes[0].innerHTML.replace(/,/g, ''));
        if (isNaN(allw)) allw = 0;
        if (isNaN(newFare)) newFare = 0;
        if (isNaN(oldTot)) oldTot = 0;
        var newTot = parseFloat(newFare) + parseFloat(allw);
        $R1.cells[9].childNodes[0].innerHTML = parseFloat(newTot);
        $R1.cells[9].childNodes[1].value = parseFloat(newTot);
        //alert($R1.parentNode.children.length - 1);
        $tot = $R1.parentNode.children[$R1.parentNode.children.length - 1].cells[9].children[0];
        $totF = $R1.parentNode.children[$R1.parentNode.children.length - 1].cells[8].children[0];
        //$tot=document.getElementById("TotalVal");
        $grndTot = document.getElementById("grandTotalName");

        var totFareVal = parseFloat($totF.innerHTML.replace(/,/g, ''));
        var totval = parseFloat($tot.innerHTML.replace(/,/g, ''));
        var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
        if (isNaN(totval)) totval = 0;
        if (isNaN(grndVal)) grndVal = 0;
        if (isNaN(oldFare)) oldFare = 0;
        //alert(totval + " :: " + grndVal + " :: " + totFareVal);
        var newtotval = grndVal - oldTot + newTot;
        $tot.innerHTML = totval - oldTot + newTot;
        //totalcalc(newTot,oldTot,$tot);
        grandtotalcalc(newtotval, grndVal);
        $totF.innerHTML = totFareVal - oldFare + newFare;
        //totalFareCalc(newFare,oldFare);
        if (TotDist != "next" && nextEl != null) {
            _CalDist(nextEl, 'next', 'dd');
        }
    }
    function onAllowTypeChange($e, flag, type) {
        var $R;
        var allType = $e.value;
        //alert(flag+" :: "+$e);
        if (flag == 'false') {
            $R = $e.parentNode.parentNode;
        }
        else {
            $R = $e;
            allType = type;
        }
        $R.cells[4].children[1].value = allType;
        var workType = $R.cells[2].children[0].innerHTML;
        var oldAllow = parseFloat($R.cells[5].children[1].innerHTML.replace(/,/g, ''));
        var oldRowTot = parseFloat($R.cells[12].children[0].innerHTML.replace(/,/g, ''));
        var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[12].children[0];
        var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
        var $grndAllowTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[5].children[0];
        var oldGrndAllowTot = parseFloat($grndAllowTot.innerHTML.replace(/,/g, ''));
        // alert(oldRowTot +" :: "+oldTot);
        var str = document.getElementById("allowString1").value;
        var out = str.substring(str.indexOf(workType), str.length);
        var out1 = out.substring(out.indexOf("=") + 1, out.indexOf("$"));
        var allowArr = out1.split("@");
        var ex = 0, os = 0, hq;
        ex = allowArr[0];
        os = allowArr[1];
        hq = allowArr[2];
        if (allType == "EX") {
            $R.cells[5].children[0].innerHTML = ex;
            $R.cells[5].children[1].value = ex;

        }
        else if (allType == "OS" || allType == "OS-EX") {
            $R.cells[5].children[0].innerHTML = os;
            $R.cells[5].children[1].value = os;
        }
        else if (allType == "HQ") {
            $R.cells[5].children[0].innerHTML = hq;
            $R.cells[5].children[1].value = hq;
        }
        else {
            $R.cells[5].children[0].innerHTML = "0";
            $R.cells[5].children[1].value = "0";
        }
        var newAllow = parseFloat($R.cells[5].children[0].innerHTML.replace(/,/g, ''));
        //alert(oldAllow + " :: " + newAllow);
        if (isNaN(oldAllow)) oldAllow = 0;
        if (isNaN(newAllow)) newAllow = 0;
        if (isNaN(oldRowTot)) oldRowTot = 0;

        $R.cells[12].children[0].innerHTML = oldRowTot - oldAllow + newAllow;
        $R.cells[12].children[1].value = oldRowTot - oldAllow + newAllow;
        $tot.innerHTML = oldTot - oldAllow + newAllow;
        $grndAllowTot.innerHTML = oldGrndAllowTot - oldAllow + newAllow;

        $grndTot = document.getElementById("grandTotalName");
        var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
        if (isNaN(grndVal)) grndVal = 0;
        var newtotval = grndVal - oldAllow + newAllow;
        grandtotalcalc(newtotval, grndVal);
        grandtotalcalcTop(newtotval, grndVal);
        grandtotalcalc123(newtotval, grndVal);
    }

    function onFareChange($e) {
        $R = $e.parentNode.parentNode;
        var oldFare = parseFloat($R.cells[11].children[1].value.replace(/,/g, ''));
        var oldRowTot = parseFloat($R.cells[12].children[0].innerHTML.replace(/,/g, ''));
        var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[12].children[0];
        var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
        var $grndFareTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[11].children[0];
        var oldGrndFareTot = parseFloat($grndFareTot.innerHTML.replace(/,/g, ''));
        // alert(oldRowTot + " :: " + oldTot);
        var newFare = parseFloat($R.cells[11].children[0].value.replace(/,/g, ''));
        if (isNaN(oldFare)) oldFare = 0;
        if (isNaN(newFare)) newFare = 0;
        //alert(newFare + " :: " + oldFare);

        $R.cells[11].children[1].value = newFare;
        $R.cells[12].children[0].innerHTML = oldRowTot - oldFare + newFare;
        $R.cells[12].children[1].value = oldRowTot - oldFare + newFare;
        $tot.innerHTML = oldTot - oldFare + newFare;
        $grndFareTot.innerHTML = oldGrndFareTot - oldFare + newFare;

        $grndTot = document.getElementById("grandTotalName");
        var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
        if (isNaN(grndVal)) grndVal = 0;
        var newtotval = grndVal - oldFare + newFare;
        grandtotalcalc(newtotval, grndVal);
        grandtotalcalcTop(newtotval, grndVal);
        grandtotalcalc123(newtotval, grndVal);

    }
    function onAllowTypeComboChange($e, flag, type) {
        var $R;
        var allType = $e.value;
        //alert(flag+" :: "+$e);
        if (flag == 'false') {
            $R = $e.parentNode.parentNode;
        }
        else {
            $R = $e;
            allType = type;
        }
        $R.cells[4].children[1].value = allType;
        var oldAllow = parseFloat($R.cells[5].children[1].value.replace(/,/g, ''));
        var oldlc = parseFloat($R.cells[10].children[0].innerHTML.replace(/,/g, ''));
        var oldRowTot = parseFloat($R.cells[12].children[0].innerHTML.replace(/,/g, ''));
        var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[12].children[0];
        var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
        var $grndAllowTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[5].children[0];
        var oldGrndAllowTot = parseFloat($grndAllowTot.innerHTML.replace(/,/g, ''));
        // alert(oldRowTot +" :: "+oldTot);
        var sVal = document.getElementById("allowString").value;
        var allowArr = sVal.split('@');
        var ex = 0, os = 0, hq;
        ex = allowArr[0];
        os = allowArr[1];
        hq = allowArr[2];
        //alert(allType);
        if (allType == "EX")
        {
            $R.cells[5].children[0].innerHTML = ex;
            $R.cells[5].children[1].value = ex;
            $R.cells[5].children[2].value = ex;
            $R.cells[5].children[0].style = "display:block";
            $R.cells[5].children[1].readOnly = true;
            $R.cells[5].children[1].style = "display:none";
            $R.cells[10].children[0].innerHTML = "0";
            $R.cells[10].children[1].value = "0";
            $R.cells[8].children[0].style = "display:none";

        }
        else if (allType == "OS" || allType == "OS-EX") {
            $R.cells[5].children[0].innerHTML = os;
            $R.cells[5].children[1].value = os;
            $R.cells[5].children[2].value = os;
            $R.cells[5].children[0].style = "display:block";
            $R.cells[5].children[1].readOnly = true;
            $R.cells[5].children[1].style = "display:none";
            //onHotelTypeChange($R, 'true', '');
            $R.cells[8].children[0].style = "display:block";

        }
        else if (allType == "HQ") {
            $R.cells[5].children[0].innerHTML = hq;
            $R.cells[5].children[1].value = hq;
            $R.cells[5].children[2].value = hq;
            $R.cells[5].children[0].style = "display:block";
            $R.cells[5].children[1].readOnly = true;
            $R.cells[5].children[1].style = "display:none";
            $R.cells[10].children[0].innerHTML = "0";
            $R.cells[10].children[1].value = "0";
            $R.cells[8].children[0].style = "display:none";
        }
        else {
            $R.cells[5].children[0].innerHTML = "0";
            $R.cells[5].children[1].value = "0";
            $R.cells[5].children[2].value = "0";
            $R.cells[5].children[0].style = "display:block";
            $R.cells[5].children[1].readOnly = true;
            $R.cells[5].children[1].style = "display:none";
            $R.cells[10].children[0].innerHTML = "0";
            $R.cells[10].children[1].value = "0";
            $R.cells[8].children[0].style = "display:none";
        }
       // var $optionEle = $R.cells[8].children[0];
       // $optionEle.options[$optionEle.selectedIndex].text = "Without Bill";
        
        var newAllow = parseFloat($R.cells[5].children[0].innerHTML.replace(/,/g, ''));

        var newlc = parseFloat($R.cells[10].children[0].innerHTML.replace(/,/g, ''));

        if (isNaN(oldAllow)) oldAllow = 0;
        if (isNaN(newAllow)) newAllow = 0;
        if (isNaN(oldlc)) oldlc = 0;
        if (isNaN(newlc)) newlc = 0;
        if (isNaN(oldRowTot)) oldRowTot = 0;

        $R.cells[12].children[0].innerHTML = oldRowTot - oldAllow + newAllow - oldlc + newlc;
        $R.cells[12].children[1].value = oldRowTot - oldAllow + newAllow - oldlc + newlc;
        $tot.innerHTML = oldTot - oldAllow + newAllow - oldlc + newlc;
        $grndAllowTot.innerHTML = oldGrndAllowTot - oldAllow + newAllow - oldlc + newlc;

        $grndTot = document.getElementById("grandTotalName");
        var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
        if (isNaN(grndVal)) grndVal = 0;
        var newtotval = grndVal - oldAllow + newAllow;
        grandtotalcalc(newtotval, grndVal);
        grandtotalcalcTop(newtotval, grndVal);
        grandtotalcalc123(newtotval, grndVal);
    }
    function onHotelTypeChange($e, flag, type) {
        var $R;
        var allType = $e.value;
        if (flag == 'false') {
            $R = $e.parentNode.parentNode;
        }
        else {
            $R = $e;
            allType = type;
        }
        var workType = $R.cells[2].children[0].innerHTML;
        var oldAllow = parseFloat($R.cells[5].children[2].value.replace(/,/g, ''));
        var oldlc = parseFloat($R.cells[6].children[1].value.replace(/,/g, ''));
        var oldRowTot = parseFloat($R.cells[12].children[0].innerHTML.replace(/,/g, ''));
        var oldRowTotlc = parseFloat($R.cells[7].children[0].innerHTML.replace(/,/g, ''));
        var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[12].children[0];
        var $tot1 = $R.parentNode.children[$R.parentNode.children.length - 1].cells[7].children[0];
        var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
        var $grndAllowTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[5].children[0];
        var $grndconveyTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[6].children[0];
        //alert($grndAllowTot);
        var oldGrndAllowTot = parseFloat($grndAllowTot.innerHTML.replace(/,/g, ''));
        var oldgrandconvey = parseFloat($grndconveyTot.innerHTML.replace(/,/g, ''));
         //alert(oldgrandconvey);
        // alert(oldRowTot +" :: "+oldTot);
        //alert("h1i");
        var sVal = document.getElementById("allowString").value;
        var allowArr = sVal.split('@');
        //alert("h2i");
        var wb = 0, lc = 0;
        wb = allowArr[5];
        lc = allowArr[6];
        var str = document.getElementById("allowString1").value;
        
        var out = str.substring(str.indexOf(workType), str.length);
        
        var out1 = out.substring(out.indexOf("=") + 1, out.indexOf("$"));
        
        var allowArr1 = out1.split("@");
        
        var ex = 0, os = 0, hq;
        
        ex = allowArr1[0];
        
        os = allowArr[1];
        
        hq = allowArr1[2];
        
        
       // wob = allowArr[6];
        if (wb == null || wb == '')
            wb = 0;
        //if (wob == null || wob == '')
        //    wob = 0;

        if (allType == "With Bill") {
            
            //  $e.parentNode.parentNode.cells[7].children[1].value = 0;

            $R.cells[5].children[0].style = "display:none";
            $R.cells[5].children[1].readOnly = false;
            $R.cells[5].children[1].style = "display:block";
            $R.cells[5].children[1].style = "width:50px";
            //        $R.cells[7].children[0].innerHTML = wb;
            //        $R.cells[7].children[1].value = wb;
            $R.cells[6].children[0].innerHTML = lc;
            $R.cells[6].children[1].value = lc;
            $R.cells[5].children[0].innerHTML = "";
            $R.cells[5].children[0].value = "";
            $R.cells[5].children[1].value = "";
            $R.cells[5].children[2].value = "";


        }
        else if (allType == "Without Bill") {
            $R.cells[5].children[0].style = "display:block";
            $R.cells[5].children[1].readOnly = true;
            $R.cells[5].children[1].style = "display:none";
            $R.cells[6].children[0].innerHTML = "0";
            $R.cells[6].children[1].value = "0";
            $R.cells[5].children[0].innerHTML = os;
            $R.cells[5].children[0].value = os;
            $R.cells[5].children[1].value = os;
            $R.cells[5].children[2].value = os;
        }

        else {
            $R.cells[5].children[0].innerHTML = "0";
            $R.cells[5].children[1].value = "0";
            $R.cells[6].children[0].innerHTML = "0";
            $R.cells[6].children[1].value = "0";
        }
        var newAllow = parseFloat($R.cells[5].children[1].value.replace(/,/g, ''));
        var newlc = parseFloat($R.cells[6].children[0].innerHTML.replace(/,/g, ''));
        //alert(newlc);
        //alert(newlc);
        if (isNaN(oldAllow)) oldAllow = 0;
        if (isNaN(newAllow)) newAllow = 0;
        if (isNaN(oldlc)) oldlc = 0;
        if (isNaN(newlc)) newlc = 0;
        if (isNaN(oldRowTotlc)) oldRowTotlc = 0;
        if (isNaN(oldRowTot)) oldRowTot = 0;
        if (isNaN(oldGrndAllowTot)) oldGrndAllowTot = 0;
        //if (isNaN($grndconveyTot)) $grndconveyTot = 0;
        if (isNaN(oldgrandconvey)) oldgrandconvey = 0;
        $R.cells[12].children[0].innerHTML = oldRowTot - oldlc - oldAllow + newAllow + newlc;
        $R.cells[12].children[1].value = oldRowTot - oldAllow - oldlc + newlc + newAllow;

        $R.cells[7].children[0].innerHTML = oldRowTotlc - oldlc - oldAllow + newAllow + newlc;
        $R.cells[7].children[1].value = oldRowTotlc - oldAllow - oldlc + newlc + newAllow;
        $tot.innerHTML = oldTot - oldAllow + newAllow - oldlc + newlc;
        $grndAllowTot.innerHTML = oldGrndAllowTot - oldAllow + newAllow;
        //alert($grndAllowTot.innerHTML);
        //alert(oldlc);
        //alert(newlc);
        $grndconveyTot.innerHTML = oldgrandconvey - oldlc + newlc;
        //alert($grndconveyTot.innerHTML);
        $grndTot = document.getElementById("grandTotalName");
        var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
        if (isNaN(grndVal)) grndVal = 0;
        var newtotval = grndVal - oldAllow + newAllow - oldlc + newlc;
        grandtotalcalc(newtotval, grndVal);
        grandtotalcalcTop(newtotval, grndVal);
        grandtotalcalc123(newtotval, grndVal);
    }

    function onAllowChange($e) {
        $R = $e.parentNode.parentNode;
        var oldAllow = parseFloat($R.cells[5].children[2].value.replace(/,/g, ''));
        //var oldAllow2 = parseFloat($R.cells[5].children[0].value.replace(/,/g, ''));
        //var oldAllow3 = parseFloat($R.cells[5].children[2].value.replace(/,/g, ''));
        //alert(oldAllow);
        //alert(oldAllow2);
        //alert(oldAllow3);
        var oldRowTot = parseFloat($R.cells[12].children[0].innerHTML.replace(/,/g, ''));
        var oldRowTot1 = parseFloat($R.cells[7].children[0].innerHTML.replace(/,/g, ''));
        var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[12].children[0];
        var $tot1 = $R.parentNode.children[$R.parentNode.children.length - 1].cells[7].children[0];
        var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
        var oldTot1 = parseFloat($tot1.innerHTML.replace(/,/g, ''));
        var $grndAllowTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[5].children[0];
        var oldGrndAllowTot = parseFloat($grndAllowTot.innerHTML.replace(/,/g, ''));
        //alert(oldRowTot + " :: " + oldTot);
        var newAllow = parseFloat($R.cells[5].children[1].value.replace(/,/g, ''));
        var sVal = document.getElementById("allowString").value;
        var allowArr = sVal.split('@');
        //alert("h2i");
        var wb = 0, wob = 0;
        wb = allowArr[5];
        if (wb > 0 && newAllow > wb) {
            alert("Amount should be less than equal to " + wb);

            $e.parentNode.parentNode.cells[5].children[1].value = 0;
            newAllow = 0;
        }
        if (isNaN(oldAllow)) oldAllow = 0;
        if (isNaN(oldRowTot1)) oldRowTot1 = 0;
        if (isNaN($tot1)) $tot1 = 0;
        if (isNaN(newAllow)) newAllow = 0;
        $R.cells[5].children[2].value = newAllow;
        //$R.cells[5].children[0].value = newAllow;
        //alert(oldAllow + " :: " + newAllow);
        
        $R.cells[12].children[0].innerHTML = oldRowTot - oldAllow + newAllow;
        $R.cells[12].children[1].value = oldRowTot - oldAllow + newAllow;
        $R.cells[7].children[0].innerHTML = oldRowTot1 - oldAllow + newAllow;
        $R.cells[7].children[1].value = oldRowTot1 - oldAllow + newAllow;
        $tot.innerHTML = oldTot - oldAllow + newAllow;
        $tot1.innerHTML = oldTot1 - oldAllow + newAllow;
        $grndAllowTot.innerHTML = oldGrndAllowTot - oldAllow + newAllow;
        $grndTot = document.getElementById("grandTotalName");
        var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
        if (isNaN(grndVal)) grndVal = 0;
        var newtotval = grndVal - oldAllow + newAllow;
        grandtotalcalc(newtotval, grndVal);
        grandtotalcalcTop(newtotval, grndVal);
        grandtotalcalc123(newtotval, grndVal);
    }

</script>
</html>
