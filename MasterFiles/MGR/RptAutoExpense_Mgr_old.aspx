<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptAutoExpense_Mgr_old.aspx.cs" Inherits="MasterFiles_MGR_RptAutoExpense_Mgr" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Expense Statement</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
     <link type="text/css" rel="stylesheet" href="../../css/MR.css" />
   
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
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>

    <script type="" language="javascript">
        function TerritoryView(sfCode, divCode) {
            window.open("Territory_View.aspx?sfCode=" + sfCode + "&divCode=" + divCode, 'TerritoryView', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
        }
        function sfcView(sfCode, divCode) {
            // alert("sss");
            window.open("/Dotnet_Expense/Reports/Distance_fixation_view.aspx?sfCode=" + sfCode + "&divCode=" + divCode, 'sfcView', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
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
            $grndTot.innerHTML = grndVal - oldvalue + newvalue;
        }

        function validate() {
            if (document.getElementById("monthId").value == "0") {
                alert('"Select the Month');
                document.getElementById("monthId").focus();
                return false;
            }
            else if (document.getElementById("yearID").value == "0") {
                alert('"Select the Year');
                document.getElementById("yearID").focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
                              <tr>
                        <td>
                            <u><font color="#800000" size="4">Expense Statement</font></u><br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="monthId" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="yearID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Go" CssClass="savebutton" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
</center>
  <br /><br />
              <center>
                <table align="center">
                    <tr>
                        <td>
                            <font color="red" size="3"><b>Kindly Submit Your Expense First...</b></font><br />
                            </td>
                    </tr></table></center>
  </div>
  <br />
  <div id="heading" runat="server" visible="false">
  <asp:HiddenField ID="sfCode" runat="server" Value="" /><asp:HiddenField ID="divCode" runat="server" Value="" />
   <table align="center" width="100%">
<tr>
<td align="left" width="25%"><asp:label Font-Bold="true" ForeColor="red" runat="server" Visible="false" id="messageId" Text=""></asp:label></td>
<td align="center" style="font-weight:bold" width="50%" ><font size="3"  face="Verdana, Arial, Helvetica, sans-serif" color="maroon"><b><u>Expense Statement For The month of <span style="background-color:yellow;color:red" id="mnthtxtId" runat="server"></span>-<span style="background-color:yellow;color:red" id="yrtxtId" runat="server"></span></u></b></font></td>
<td align="right" width="25%">&nbsp;
<asp:Button ID="btnBack" runat="server" CssClass="savebutton" Height="25px" Width="60px" Text="Back" onClick="btnBack_Click" /></td>
</tr>
</table>
<br/>
<div id="rejectedDiv" runat="server" visible="false">
 <table align="center" width="100%">
<tr>
<td align="left"  style="font-weight:bold;font-size:small;" id="rejectedBy" runat="server"></td>
<td align="center" style="font-weight:bold;font-size:small;" id="rejectedReason" runat="server"></td>

</tr>
</table>
</div>

 <table align="center" width="100%">
<tr>
<td align="left"  style="font-weight:bold;font-size:small;" id="fieldforceId" runat="server"></td>
<td align="center" style="font-weight:bold;font-size:small;" id="hqId" runat="server"></td>
<td align="right" style="font-weight:bold;font-size:small;" id="empId" runat="server"></td>
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
                                    <asp:TemplateField HeaderText="#" HeaderStyle-Width="4%">
                                        <ItemStyle BorderStyle="Solid" Width="0px" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left">
                                        </ItemStyle>
                                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>

                                            <asp:Label ID="lblSNo" SkinID="lblMand" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" >
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" Width="0px" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTerrName" runat="server" Text=''></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
                                     <asp:TemplateField HeaderText="Allowance Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:Label ID="lblCat" runat="server" Text=''></asp:Label>
                                        <asp:DropDownList  onchange="onAllowTypeChange(this,'false','')" ID="AllowType" Visible="false" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                                                <asp:ListItem Selected="false" Text="---Select---" Value=""></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="HQ" Value="HQ"></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="EX" Value="EX"></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="OS" Value="OS"></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="Hill" Value="Hill"></asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:DropDownList  onchange="onAllowTypeChangeother(this,'false','')" ID="AllowTypeother" Visible="false" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                                                <asp:ListItem Selected="false" Text="---Select---" Value=""></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="HQ" Value="HQ"></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="EX" Value="EX"></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="OS" Value="OS"></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="Hill" Value="Hill"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="allowTypeHidden" runat="server" Value='<%# Bind("Type_Code") %>' />
                                            <asp:HiddenField ID="allowTypeHidden1" runat="server" Value='<%# Bind("Type_Code") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Type"  Visible="false" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text=''></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Applied DA" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BackColor="LightBlue" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        
                                        <ItemTemplate><asp:TextBox ID="txtAllow" onkeyup="onAllowChange(this);" onkeypress="CheckNumeric(event);" MaxLength=5 visible="true"  ReadOnly="true" runat="server" Text='<%# Bind("Allowance") %>'></asp:TextBox><asp:HiddenField ID="allowHidden" runat="server" Value='<%# Bind("Fare") %>' /></ItemTemplate>
                                        
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Place of work" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" >
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                          <ItemTemplate><asp:Label ID="lblTerritoryName" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Distance Travelled</br>(in Kms)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:TextBox ID="lblDistance" onkeypress="CheckNumeric(event);" onKeyup="distchange(this);"  MaxLength=5 Visible="true" runat="server" Text='<%# Bind("Distance") %>'/>
                                        <asp:HiddenField ID="distHidden" runat="server" Value='<%# Bind("Distance") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Applied TA" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:TextBox ID="txtFare" onkeyup="onFareChange(this);" onkeypress="CheckNumeric(event);" MaxLength=5 visible="true" runat="server" Text='<%# Bind("Fare") %>'></asp:TextBox><asp:Label ID="lblFare" runat="server" Visible="false" Text='<%# Bind("Fare") %>'></asp:Label><asp:HiddenField ID="fareHidden" runat="server" Value='<%# Bind("Fare") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Applied Remarks" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:TextBox ID="txtRemarks" visible="true" onkeypress="AlphaNumeric_NoSpecialChars(event);" width="160" runat="server" Text='<%# Bind("exp_remarks") %>'>
                                        </asp:TextBox><asp:HiddenField ID="remarksHidden" runat="server" Value='<%# Bind("exp_remarks") %>' />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Total Amount" ItemStyle-HorizontalAlign="Left">
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
            <table width="100%" align="right">
            <tr><td colspan="10">&nbsp;</td></tr>
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
<td width="40%" valign="top">
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

                                <asp:GridView ID="otherExpGrid" runat="server" Width="40%" Font-Names="Verdana"  Font-Size="8pt" 
            HeaderStyle-BackColor="#FFEFD5"
            HeaderStyle-ForeColor="black" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Exepense Name" ItemStyle-HorizontalAlign="Left">
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
</td></tr>
</table>
<div id="misExp" visible="false" runat="server">
<br />
<br />
<br />
<table align="right" border="0">

<tr>
<th class="tblHead" colspan="4" align="center" style="padding-top:10px;font-weight:bold;"><b>Miscellaneous Expenses</b></th></tr>
<tr>
<td class="b" align="right" style="width: 50px;padding:0px;" id="td1">


 <table border="0" id="tableId" runat="server" clientidmode="Static">
<tr style="font-weight:bold">
<td class="tblHead" align="center"><b>Type</b></td>
<td class="tblHead" align="center"><b>Amount</b></td>
<td class="tblHead" align="center"><b>Remarks</b></td>
<td class="tblHead" colspan="2" align="center"><b>Add/Del</b></td></tr>
<tr>
 <td>
  <asp:DropDownList  class="otherExp" ID="otherExp" runat="server" AutoPostBack="false" SkinID="ddlRequired">
  <asp:ListItem Selected="false" Text="--Select--" Value="0"></asp:ListItem>
 </asp:DropDownList>
 </td>
<td><input type='text' class='textbox' name="OthExpVal" id="OthExpVal"  value="" size='6' maxlength=6 onkeyup="OthExpCalc(this)"/></td>
<td><input type='text' class='textbox' name="OthExpRmk" id="OthExpRmk" value="" size='50' onkeyup="rmksval(this)"/></td>
 <td>
<input type="button" id="btnadd" value=" + " class='btnSave' onclick="_AdRowByCurrElem(this)" />&nbsp
</td>
 <td>
<input type="button" value=" - " class='btnSave' onclick="DRForOthExp(this,this.parentNode.parentNode,1)"/>
</td>
 </tr>
            </table></td>
            
            </tr>
            </table>
<table width="100%"  border="0">
 <tr>
 <td  style="padding-top:10px;text-align:right;color:red; font-family:Times New Roman;
     font-weight:bold;font-size:30px">Grand Total : </td>
 <td style="padding-top:10px;color:red;text-align:right;font-family:Times New Roman;
     font-weight:bold;font-size:30px" runat="server" id="grandTotalName">0</td>
  </tr>
</table> 
<asp:HiddenField ID="otherExpValues" runat="server" Value="" /><asp:HiddenField ID="Othtotal" runat="server" Value="0" />
<asp:HiddenField ID="distString" runat="server" Value="" /><asp:HiddenField ID="allowString" runat="server" Value="" />
<br />
<br />
               <center>
        <asp:Button ID="btnDrftSave" runat="server" Visible="False" CssClass="savebutton" Width="80px" Height="25px"
            Text="Draft Save" OnClientClick="saveOtherExp()" OnClick="btnSaveDraft_Click" /><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                    <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Width="110px" Height="25px"
            Text="Send To HO" Visible="true" OnClientClick="saveOtherExp()" OnClick="btnSave_Click" />
    </center>
    </div>
    </div>

    </form>

</body>
<script>
    function saveOtherExp() {
        //alert(document.getElementById("otherExp"));
        var otherExpRmk = document.getElementsByName("OthExpRmk");
        //alert(otherExpRmk.length);
        var otherExpVal = document.getElementsByName("OthExpVal");
        var otherExp = document.getElementsByClassName("otherExp");
        var exp = 0, val = 0;
        var remarks = "";
        for (var i = 0; i < otherExpRmk.length; i++) {
            var value = otherExp[i].options[otherExp[i].selectedIndex].value;
            var text = otherExp[i].options[otherExp[i].selectedIndex].text;

            //alert(otherExpRmk[i].value + "::" + otherExpVal[i].value + "::" + otherExp[i].value);
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
        //alert(remarks + "~" + val + "~" + exp);
        document.getElementById("otherExpValues").value = remarks + "~" + val + "~" + exp;

    }

    function onAllowTypeChange($e, flag, type) {
        var $R;
        var allType = $e.value;
        if (flag == 'false') {
            $R = $e.parentNode.parentNode;
        }
        else {
            $R = $e;
            allType = type;
        }
        $R.cells[7].children[0].readOnly = false;
        $R.cells[7].children[1].readOnly = false;

        //alert(flag + " :: " + $e +" :: "+allType);
        $R.cells[4].children[1].value = allType;
        $R.cells[4].children[2].value = allType;
        $R.cells[4].children[3].value = allType;
        var oldAllow = parseFloat($R.cells[5].children[0].value.replace(/,/g, ''));

        var oldRowTot = parseFloat($R.cells[10].children[0].innerHTML.replace(/,/g, ''));
        var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[10].children[0];
        var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
        var $grndAllowTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[5].children[0];
        var oldGrndAllowTot = parseFloat($grndAllowTot.innerHTML.replace(/,/g, ''));
        var oldFare = parseFloat($R.cells[8].children[1].value.replace(/,/g, ''));
        // alert(oldRowTot +" :: "+oldTot);
        var sVal = document.getElementById("allowString").value;

        var allowArr = sVal.split('@');
        var ex = 0, os = 0, hq = 0, ht = 0;
        ex = allowArr[0];
        os = allowArr[1];
        hq = allowArr[2];
        ht = allowArr[3];
        if (isNaN(ex)) ex = 0;
        if (isNaN(os)) os = 0;
        if (isNaN(hq)) hq = 0;
        if (isNaN(ht)) ht = 0;
        if (isNaN(oldFare)) oldFare = 0;
        $R.cells[7].children[0].value = 0;
        $R.cells[7].children[1].value = 0;

        $R.cells[8].children[0].value = 0;
        $R.cells[8].children[1].value = 0;

        //alert(ex + hq + os);
        if (allType != "HQ") {
            $R.cells[7].children[0].style = "display:block";
            $R.cells[8].children[0].style = "display:block";

        }

        if (allType == "OS") {

            $R.cells[5].children[0].readOnly = false;
        }

        if (allType == "EX") {

            $R.cells[5].children[0].readOnly = true;
            $R.cells[5].children[0].value = ex;
            $R.cells[5].children[1].value = ex;


        }
        else if (allType == "OS" || allType == "OS-EX") {
            $R.cells[5].children[0].readOnly = false;
            $R.cells[5].children[0].value = os;
            $R.cells[5].children[1].value = os;


        }
        else if (allType == "HQ" || allType == '') {
            $R.cells[5].children[0].readOnly = true;
            $R.cells[7].children[0].style = "display:none";
            $R.cells[8].children[0].style = "display:none";

            $R.cells[5].children[0].value = hq;
            $R.cells[5].children[1].value = hq;

        }
        else if (allType == "Hill") {
            $R.cells[5].children[0].readOnly = true;
            $R.cells[5].children[0].value = ht;
            $R.cells[5].children[1].value = ht;

        }
        else {
            $R.cells[5].children[0].readOnly = true;
            $R.cells[5].children[0].value = 0;
            $R.cells[5].children[0].value = 0;

        }
        var newAllow = parseFloat($R.cells[5].children[0].value.replace(/,/g, ''));
        //alert(oldAllow + " :: " + newAllow);
        if (isNaN(oldAllow)) oldAllow = 0;
        if (isNaN(newAllow)) newAllow = 0;
        if (isNaN(oldRowTot)) oldRowTot = 0;

        oldAllow = oldAllow + oldFare;

        $R.cells[10].children[0].innerHTML = newAllow;
        $R.cells[10].children[1].value = newAllow;
        //$R.cells[10].children[0].innerHTML = oldRowTot - oldAllow + newAllow;
        // $R.cells[10].children[1].value = oldRowTot - oldAllow + newAllow;
        $tot.innerHTML = oldTot - oldAllow + newAllow;
        $grndAllowTot.innerHTML = oldGrndAllowTot - oldAllow + newAllow;

        $grndTot = document.getElementById("grandTotalName");
        var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
        if (isNaN(grndVal)) grndVal = 0;
        var newtotval = grndVal - oldAllow + newAllow;
        grandtotalcalc(newtotval, grndVal);
    }
    function onAllowTypeChangeother($e, flag, type) {
        var $R;
        var allType = $e.value;
        if (flag == 'false') {
            $R = $e.parentNode.parentNode;
        }
        else {
            $R = $e;
            allType = type;
        }
        $R.cells[7].children[0].readOnly = false;
        $R.cells[7].children[1].readOnly = false;
        
        //alert(flag + " :: " + $e +" :: "+allType);
        $R.cells[4].children[1].value = allType;
        $R.cells[4].children[2].value = allType;
        $R.cells[4].children[3].value = allType;
        var oldAllow = parseFloat($R.cells[5].children[0].value.replace(/,/g, ''));

        var oldRowTot = parseFloat($R.cells[10].children[0].innerHTML.replace(/,/g, ''));
        var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[10].children[0];
        var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
        var $grndAllowTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[5].children[0];
        var oldGrndAllowTot = parseFloat($grndAllowTot.innerHTML.replace(/,/g, ''));
        var oldFare = parseFloat($R.cells[8].children[1].value.replace(/,/g, ''));
        // alert(oldRowTot +" :: "+oldTot);
        var sVal = document.getElementById("allowString").value;

        var allowArr = sVal.split('@');
        var ex = 0, os = 0, hq = 0, ht = 0;
        ex = allowArr[0];
        os = allowArr[1];
        hq = allowArr[2];
        ht = allowArr[3];
        if (isNaN(ex)) ex = 0;
        if (isNaN(os)) os = 0;
        if (isNaN(hq)) hq = 0;
        if (isNaN(ht)) ht = 0;
        if (isNaN(oldFare)) oldFare = 0;
        $R.cells[7].children[0].value = 0;
        $R.cells[7].children[1].value = 0;

        $R.cells[8].children[0].value = 0;
        $R.cells[8].children[1].value = 0;

        //alert(ex + hq + os);
        if (allType != "HQ") {
            $R.cells[7].children[0].style = "display:block";
            $R.cells[8].children[0].style = "display:block";

        }

        if (allType == "OS") {

            $R.cells[5].children[0].readOnly = true;
        }

        if (allType == "EX") {

            $R.cells[5].children[0].readOnly = true;
            $R.cells[5].children[0].value = ex;
            $R.cells[5].children[1].value = ex;


        }
        else if (allType == "OS" || allType == "OS-EX") {
            $R.cells[5].children[0].readOnly = true;
            $R.cells[5].children[0].value = os;
            $R.cells[5].children[1].value = os;


        }
        else if (allType == "HQ" || allType == '') {
            $R.cells[5].children[0].readOnly = true;
            $R.cells[7].children[0].style = "display:none";
            $R.cells[8].children[0].style = "display:none";

            $R.cells[5].children[0].value = hq;
            $R.cells[5].children[1].value = hq;

        }
        else if (allType == "Hill") {
            $R.cells[5].children[0].readOnly = true;
            $R.cells[5].children[0].value = ht;
            $R.cells[5].children[1].value = ht;

        }
        else {
            $R.cells[5].children[0].readOnly = true;
            $R.cells[5].children[0].value = 0;
            $R.cells[5].children[0].value = 0;

        }
        var newAllow = parseFloat($R.cells[5].children[0].value.replace(/,/g, ''));
        //alert(oldAllow + " :: " + newAllow);
        if (isNaN(oldAllow)) oldAllow = 0;
        if (isNaN(newAllow)) newAllow = 0;
        if (isNaN(oldRowTot)) oldRowTot = 0;

        oldAllow = oldAllow + oldFare;

        $R.cells[10].children[0].innerHTML = newAllow;
        $R.cells[10].children[1].value = newAllow;
        //$R.cells[10].children[0].innerHTML = oldRowTot - oldAllow + newAllow;
        // $R.cells[10].children[1].value = oldRowTot - oldAllow + newAllow;
        $tot.innerHTML = oldTot - oldAllow + newAllow;
        $grndAllowTot.innerHTML = oldGrndAllowTot - oldAllow + newAllow;

        $grndTot = document.getElementById("grandTotalName");
        var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
        if (isNaN(grndVal)) grndVal = 0;
        var newtotval = grndVal - oldAllow + newAllow;
        grandtotalcalc(newtotval, grndVal);
    }
    function onFareChange($e) {
        $R = $e.parentNode.parentNode;
        var oldFare = parseFloat($R.cells[8].children[1].value.replace(/,/g, ''));

        var oldRowTot = parseFloat($R.cells[10].children[0].innerHTML.replace(/,/g, ''));

        var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[10].children[0];
        var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
        var $grndFareTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[8].children[0];
        var oldGrndFareTot = parseFloat($grndFareTot.innerHTML.replace(/,/g, ''));
        // alert(oldRowTot + " :: " + oldTot);
        var newFare = parseFloat($R.cells[8].children[0].value.replace(/,/g, ''));
        //var newFare = parseFloat($R.cells[8].children[1].value.replace(/,/g, ''));
        if (isNaN(oldFare)) oldFare = 0;
        if (isNaN(newFare)) newFare = 0;
        if (isNaN(oldRowTot)) oldRowTot = 0;
        //alert(newFare + " :: " + oldFare);

        $R.cells[8].children[1].value = newFare;
        $R.cells[8].children[0].value = newFare;
        $R.cells[10].children[0].innerHTML = oldRowTot - oldFare + newFare;
        $R.cells[10].children[1].value = oldRowTot - oldFare + newFare;
        $tot.innerHTML = oldTot - oldFare + newFare;
        $grndFareTot.innerHTML = oldGrndFareTot - oldFare + newFare;

        $grndTot = document.getElementById("grandTotalName");
        var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
        if (isNaN(grndVal)) grndVal = 0;
        var newtotval = grndVal - oldFare + newFare;
        grandtotalcalc(newtotval, grndVal);





    }
    function distchange($e) {
        var sVal = document.getElementById("allowString").value;
        var allowArr = sVal.split('@');
        var fare = 0;
        var fareAmt = 0;
        fare = allowArr[4];
        // alert(fare);
        var dist = $e.value;
        // alert(dist);

        fareAmt = dist * fare;


        //var amt = parseFloat(dist);
        $R = $e.parentNode.parentNode;
        var altyp = $R.cells[4].children[1].value;
        var amt = parseFloat($R.cells[7].children[0].value);
        //alert(amt);
        if (isNaN(amt)) amt = 0;
        var newFare = 0;
        if (altyp == "Hill") {
            newFare = 0;
            $R.cells[8].children[0].readOnly = false;

        }
        else {
            //if (amt > 0 && amt < 21) {
            //    newFare = 0;

            //    $R.cells[8].children[0].readOnly = true;
            //}
            //else if (amt > 0 && amt > 20 && amt < 121) {
            //    newFare = fareAmt;
            //    $R.cells[8].children[0].readOnly = true;

            //}
            //else {
                if (amt > 0)
                    {
                newFare = 0;
                $R.cells[8].children[0].readOnly = false;
                }
            //}
        }

        var oldFare = parseFloat($R.cells[8].children[1].value.replace(/,/g, ''));
        var oldFare = parseFloat($R.cells[8].children[0].value.replace(/,/g, ''));
        var oldRowTot = parseFloat($R.cells[10].children[0].innerHTML.replace(/,/g, ''));
        var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[10].children[0];
        var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
        var $grndFareTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[8].children[0];
        var oldGrndFareTot = parseFloat($grndFareTot.innerHTML.replace(/,/g, ''));

        if (isNaN(oldFare)) oldFare = 0;
        if (isNaN(newFare)) newFare = 0;
        //alert(newFare + " :: " + oldFare);

        $R.cells[8].children[0].value = newFare;
        // $R.cells[8].children[1].value = newFare;
        $R.cells[10].children[0].innerHTML = oldRowTot - oldFare + newFare;
        $R.cells[10].children[1].value = oldRowTot - oldFare + newFare;
        $tot.innerHTML = oldTot - oldFare + newFare;

        $grndFareTot.innerHTML = oldGrndFareTot - oldFare + newFare;

        $grndTot = document.getElementById("grandTotalName");
        var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
        if (isNaN(grndVal)) grndVal = 0;
        var newtotval = grndVal - oldFare + newFare;
        grandtotalcalc(newtotval, grndVal);

    }

    function onAllowChange($e) {

        $R = $e.parentNode.parentNode;

        var oldFare = parseFloat($R.cells[5].children[1].value.replace(/,/g, ''));
        var oldRowTot = parseFloat($R.cells[10].children[0].innerHTML.replace(/,/g, ''));
        var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[10].children[0];
        var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
        var $grndFareTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[5].children[0];
        var oldGrndFareTot = parseFloat($grndFareTot.innerHTML.replace(/,/g, ''));
        var newFare = parseFloat($R.cells[5].children[0].value.replace(/,/g, ''));
        var $maxLimit = 1500;
        if ($maxLimit > 0 && newFare > $maxLimit) {
            alert("Amount should be less than equal to " + $maxLimit);
            // parseFloat($R.cells[5].children[0].value.replace(/,/g, '')) = 0;
            $e.parentNode.parentNode.cells[5].children[0].value = 0;
            newFare = 0;
        }
        if (isNaN(oldFare)) oldFare = 0;
        if (isNaN(newFare)) newFare = 0;
        if (isNaN(oldRowTot)) oldRowTot = 0;


        $R.cells[5].children[1].value = newFare;
        $R.cells[5].children[0].value = newFare;
        $R.cells[10].children[0].innerHTML = oldRowTot - oldFare + newFare;
        $R.cells[10].children[1].value = oldRowTot - oldFare + newFare;
        $tot.innerHTML = oldTot - oldFare + newFare;
        $grndFareTot.innerHTML = oldGrndFareTot - oldFare + newFare;

        $grndTot = document.getElementById("grandTotalName");
        var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
        if (isNaN(grndVal)) grndVal = 0;
        var newtotval = grndVal - oldFare + newFare;
        grandtotalcalc(newtotval, grndVal);


    }
</script>
</html>
