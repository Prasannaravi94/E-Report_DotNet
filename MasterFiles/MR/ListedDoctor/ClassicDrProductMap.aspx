<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassicDrProductMap.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_ClassicDrProductMap" %>

<!DOCTYPE html>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor - Product Map</title>

    <link type="text/css" rel="stylesheet" href="//cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />

    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script src="/JScript/jquery-1.10.2.js" type="text/javascript"></script>
    <link type="text/css" href="/css/Report.css" rel="Stylesheet" />
    <link type="text/css" href="/css/multiple-select.css" rel="Stylesheet" />
    <script type="text/javascript" src="/JsFiles/jquery.effects.core.js"></script>
    <script type="text/javascript" src="/JsFiles/jquery.effects.blind.js"></script>
    <script type="text/javascript" src="/JsFiles/multiple-select.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <script language="javascript" type="text/javascript">

        function autoCompleteEx_ItemSelected(sender, args) {
            __doPostBack(sender.get_element().name, "");
        }
        function GetAllProducts() {
            //Cleint ID of RadioButtonList
            var rdbtnLstValues = list.getElementsByTagName("input");


            //}

            var Checkdvalue = '';
            for (var i = 0; i < rdbtnLstValues.length; i++) {
                if (rdbtnLstValues[i].checked) {
                    Checkdvalue = rdbtnLstValues[i].value;
                    break;
                }
            }
        }

    </script>

    <script type="text/javascript">

        function checkAllRej() {
            var grid = document.getElementById('<%= grdDCR.ClientID %>');
            if (grid != null) {
                var inputList = grid.getElementsByTagName("input");
                var cnt = 0;
                var index = '';
                var chkrejall = document.getElementById('grdDCR_ctl01_chkRejAll');
                for (i = 2; i < inputList.length + 1; i++) {
                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {
                        index = i.toString();
                    }
                    var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');
                    if (chkrejall.checked) {
                        chkrej.checked = true;
                        document.getElementById('grdDCR_ctl' + index + '_pnlList').classList.remove("effectPnl");
                    }
                    else {
                        chkrej.checked = false;
                        document.getElementById('grdDCR_ctl' + index + '_pnlList').classList.add("effectPnl");
                    }
                }
            }
        }

        function checkrej(element) {
            var grid = document.getElementById('<%= grdDCR.ClientID %>');

            if (grid != null) {

                var inputList = grid.getElementsByTagName("input");
                var chkrejall = document.getElementById('grdDCR_ctl01_chkRejAll');
                var cnt = 0;
                var index = '';
                var Count = 0;
                var CountVisi = 0;

                for (i = 2; i < inputList.length + 1; i++) {
                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {
                        index = i.toString();
                    }

                    var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');
                    if (chkrej.id == element.id) {
                        if (chkrej.checked) {
                            document.getElementById("btnSave").style.visibility = "visible";
                            document.getElementById('grdDCR_ctl' + index + '_pnlList').classList.remove("effectPnl");
                            $("#grdDCR > tbody > tr:nth-child(" + index + ")").css("background-color", "#fff");
                            CountVisi = CountVisi + 1;
                        }
                        else {
                            document.getElementById('grdDCR_ctl' + index + '_pnlList').classList.add("effectPnl");
                            var CampignTag = document.getElementById('grdDCR_ctl' + index + '_hdnCampignTag');
                            var txtProducts = document.getElementById('grdDCR_ctl' + index + '_txtProducts');
                            if (CampignTag.value == "1" || txtProducts.value != "") {
                                $("#grdDCR > tbody > tr:nth-child(" + index + ")").css("background-color", "#edd1d1");
                            }
                        }
                    }
                    else {
                        Count = Count + 1;
                        chkrej.checked = false;
                        document.getElementById('grdDCR_ctl' + index + '_pnlList').classList.add("effectPnl");
                        var CampignTag = document.getElementById('grdDCR_ctl' + index + '_hdnCampignTag');
                        var txtProducts = document.getElementById('grdDCR_ctl' + index + '_txtProducts');
                        if (CampignTag.value == "1" || txtProducts.value != "") {
                            $("#grdDCR > tbody > tr:nth-child(" + index + ")").css("background-color", "#edd1d1");
                        }
                    }

                    if (CountVisi == 0) {
                        document.getElementById("btnSave").style.visibility = "hidden";
                    }
                }
            }
        }
    </script>

    <style>
        #grdDCR > tbody > tr:nth-child(n) > td:nth-child(5) > div > div.nice-select.selectpicker {
            display: none !important;
        }

        #grdDCR > tbody > tr:nth-child(n) > td:nth-child(5) > div.nice-select.selectpicker {
            display: none !important;
        }

        [type="checkbox"]:not(:checked), [type="checkbox"]:checked {
            position: revert !important;
            left: 0px !important;
        }

            [type="checkbox"]:not(:checked) + label, [type="checkbox"]:checked + label {
                position: revert !important;
            }

            [type="checkbox"]:not(:checked) + label, [type="checkbox"]:checked + label {
                display: none !important;
            }

        .effectPnl {
            display: none !important;
        }

        #grdDCR tr td {
            border: 1px solid black;
        }
    </style>

</head>
<body style="overflow-y: scroll">
    <form id="form1" runat="server">


        <div id="Divid" runat="server">
        </div>
        <asp:HiddenField ID="hdnValidPrio" runat="server" />
        <asp:HiddenField ID="hdnPriYesNo" runat="server" />
        <asp:HiddenField ID="hdnPriCnt" runat="server" />
        <div>
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
                <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
            </asp:Panel>
            <div class="display-reportMaintable clearfix">
                <div class="table-responsive" style="overflow: inherit">
                    <h4 class="text-center" style="border-bottom: none">classic Listed Doctor - Product Tag</h4>
                    <asp:GridView ID="grdDCR" runat="server" Width="100%" EmptyDataText="No Records Found"
                        AutoGenerateColumns="false" GridLines="Both" CssClass="borderalignment" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdDCR_RowDataBound">
                        <HeaderStyle Font-Bold="False" BackColor="#117db7" ForeColor="white" />
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkRejAll" runat="server" Text="  Select All" Visible="false" onclick="checkAllRej(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRjtDCR" runat="server" Text="  ." onclick="checkrej(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDCR.PageIndex * grdDCR.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dr_Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDrcode" runat="server" Text='<%#Eval("listedDrcode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Listed DrName">
                                <ItemTemplate>
                                    <asp:Label ID="lblDrname" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Class">
                                <ItemTemplate>
                                    <asp:Label ID="lblClass" runat="server" Text='<%#Eval("Doc_Cat_ShortName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Speciality">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpcl" runat="server" Text='<%#Eval("Doc_Spec_ShortName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Territory Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblArea" runat="server" Text='<%#Eval("Territory_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Tagged">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtProducts" runat="server" Enabled="false" BorderColor="Black" BorderStyle="Solid" TextMode="MultiLine" CssClass="textbox"></asp:TextBox>
                                    <div id="effect " class="ui-widget-content" style="border-color: Black; border-style: solid; border-width: 1px">
                                        <asp:Panel ID="pnlList" runat="server" CssClass="effectPnl" Style="height: 100px; overflow-y: scroll;" />
                                    </div>
                                    <asp:HiddenField ID="hdnProdCodeSelected" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hdnCampignTag" runat="server"></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                    <br />

                </div>
            </div>
            <center>
                 <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <%--OnClientClick="ValidPriority()" --%>
            </center>
        </div>
    </form>
        <script>
        $(function () {
            $("[id*=btnSave]").click(function () {

                var hdnPriYesNo = document.getElementById("hdnPriYesNo");
                if (hdnPriYesNo.value != 0) {

                    //function ValidPriority() {
                    var hdnPriCnt = document.getElementById("hdnPriCnt");
                    var gridViewRowCount = document.getElementById("<%=grdDCR.ClientID%>");
                    if (gridViewRowCount.rows.length > 0) {
                        debugger
                        var cnt = 0, index = '';
                        for (var k = 2; k < gridViewRowCount.rows.length; k++) {
                            if (k.toString().length == 1) {
                                index = cnt.toString() + k.toString();
                            }
                            else {
                                index = k.toString();
                            }
                            var table = document.getElementById("grdDCR_ctl" + index + "_tbl");
                            var rowCount = table.rows.length;
                            if (rowCount > 0) {
                                let arrPriority = new Array();
                                let arrPriorityMat = new Array();

                                for (var m = 0; m < rowCount; m++) {
                                    var chkCnt = document.getElementById("grdDCR_ctl" + index + "_chkNew" + m.toString());
                                    var TxtNumVal = Number($('#grdDCR_ctl' + index + '_txtNew' + m.toString())[0].value);
                                    if (chkCnt.checked) {
                                        arrPriority.push(TxtNumVal);
                                    }
                                }

                                arrPriority.sort();

                                for (var n = 0; n < rowCount; n++) {
                                    arrPriorityMat.push(n + 1)
                                }
                                var txt = document.getElementById("grdDCR_ctl" + index + "_txtNew" + (k - 2));
                                for (var t = 0; t < arrPriority.length; t++) {
                                    if (arrPriority[t] != arrPriorityMat[t] || arrPriority[t] == 0) {
                                        var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');
                                        chkrej.checked = true;
                                        document.getElementById('grdDCR_ctl' + index + '_pnlList').classList.remove("effectPnl");
                                        $("#grdDCR > tbody > tr:nth-child(" + index + ")").css("background-color", "#fff");
                                        alert("Same Priority is Not Allowed for Different Product and Zero value is not allowed and Enter from 1st priority");
                                        txt.focus();
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            });
        });
    </script>
    <script>
        function ValidPriority() {
            var hdnPriCnt = document.getElementById("hdnPriCnt");
            var gridViewRowCount = document.getElementById("<%=grdDCR.ClientID%>");
            if (gridViewRowCount.rows.length > 0) {
                debugger
                var cnt = 0, index = '';
                for (var k = 2; k < gridViewRowCount.rows.length; k++) {
                    if (k.toString().length == 1) {
                        index = cnt.toString() + k.toString();
                    }
                    else {
                        index = k.toString();
                    }
                    var table = document.getElementById("grdDCR_ctl" + index + "_tbl");
                    var rowCount = table.rows.length;
                    if (rowCount >0)
                    {
                        let arrPriority = new Array();
                        let arrPriorityMat = new Array();

                        for (var m = 0; m < rowCount; m++) {
                            var chkCnt = document.getElementById("grdDCR_ctl" + index + "_chkNew" + m.toString());
                            var TxtNumVal = Number($('#grdDCR_ctl' + index + '_txtNew' + m.toString())[0].value);
                            if (chkCnt.checked) {
                                if (TxtNumVal != 0)
                                { arrPriority.push(TxtNumVal); }
                            }
                        }
                        arrPriority.sort();

                        for (var n = 0; n < rowCount; n++) {
                            arrPriorityMat.push(n + 1)
                        }
                        var txt = document.getElementById("grdDCR_ctl" + index + "_txtNew" + (k-2));
                        //var chk = document.getElementById("grdDCR_ctl" + index + "_chkNew" + control);
                        for (var t = 0; t < arrPriority.length; t++) {
                            if (arrPriority[t] != arrPriorityMat[t]) {
                                alert("Same Priority is Not Allowed for Different Product");
                                //chk.checked = false;
                                txt.focus();
                                hdnValidPrio.value = "1";
                                //txt.value = "";
                                //txt.style.display = "none";
                                return false;
                            }
                            else { hdnValidPrio.value = "0";}
                        }
                    }
                }
            }
           
        }
        function ControlVisibility(control, rowIndex) {
            var cnt = 0, index = '';
        
            if (rowIndex.toString().length == 1) {
                index = cnt.toString() + rowIndex.toString();
            }
            else {
                index = rowIndex.toString();
            }

            var hdnPriYesNo = document.getElementById("hdnPriYesNo");
            var objTextBox = document.getElementById("grdDCR_ctl" + index + "_txtProducts");

            if (hdnPriYesNo.value == "0") {
                var chk = document.getElementById("grdDCR_ctl" + index + "_chkNew" + control);
                var hdnProdCodeSelected = document.getElementById("grdDCR_ctl" + index + "_hdnProdCodeSelected");

                if (chk.checked) {
                    var table = document.getElementById("grdDCR_ctl" + index + "_tbl");
                    var rowCount = table.rows.length;

                    for (var i = 0; i < rowCount; i++) {

                        var row = table.rows[i];

                        var chkbox = row.getElementsByTagName('select');
                        var txt = row.getElementsByTagName('input');
                        var lbl = row.getElementsByTagName('span');
                        if (txt[0].checked) {
                            if (hdnProdCodeSelected.value == "") {
                                hdnProdCodeSelected.value = txt[1].value + "~~0#";
                                objTextBox.value = lbl[0].innerHTML + ", ";
                            }
                            else {
                                hdnProdCodeSelected.value = hdnProdCodeSelected.value.replace(txt[1].value + "~~0#", "");
                                hdnProdCodeSelected.value = hdnProdCodeSelected.value + txt[1].value + "~~0#";
                                objTextBox.value = objTextBox.value.replace(lbl[0].innerHTML + ", ", "");
                                objTextBox.value = objTextBox.value + lbl[0].innerHTML + ", ";
                            }
                        }
                        else {
                            hdnProdCodeSelected.value = hdnProdCodeSelected.value.replace(txt[1].value + "~~0#", "");
                            objTextBox.value = objTextBox.value.replace(lbl[0].innerHTML + ", ", "");
                        }
                    }
                }
                else {
                    //txt.style.display = "none";
                    var table = document.getElementById("grdDCR_ctl" + index + "_tbl");
                    var row = table.rows[control];
                    var lbl = row.getElementsByTagName('span');
                    var txt = row.getElementsByTagName('input');
                    var txthdnProd = txt[1].value + "~~0#";

                    objTextBox.value = objTextBox.value.replace(lbl[0].innerHTML + ", ", "");

                    hdnProdCodeSelected.value = hdnProdCodeSelected.value.replace(txthdnProd, "");
                }
            }
            else {
                var chk = document.getElementById("grdDCR_ctl" + index + "_chkNew" + control);
                var txt = document.getElementById("grdDCR_ctl" + index + "_txtNew" + control);
                var hdnPriCnt = document.getElementById("hdnPriCnt");
                var hdnProdCodeSelected = document.getElementById("grdDCR_ctl" + index + "_hdnProdCodeSelected");

                if (chk.checked) {
                    var table = document.getElementById("grdDCR_ctl" + index + "_tbl");
                    var rowCount = table.rows.length;

                    txt.style.display = "block";

                    txt.value = Number(hdnPriCnt.value) + 1 <= Number(txt.value) ? "0" : txt.value;

                    for (var i = 0; i < rowCount; i++) {

                        var row = table.rows[i];

                        var chkbox = row.getElementsByTagName('select');
                        var txt = row.getElementsByTagName('input');
                        var lbl = row.getElementsByTagName('span');
                        if (txt[0].checked) {

                            if (hdnProdCodeSelected.value == "") {
                                hdnProdCodeSelected.value = txt[2].value + "~~" + txt[1].value + "#";
                                objTextBox.value = lbl[0].innerHTML + ", ";
                            }
                            else {
                                hdnProdCodeSelected.value = hdnProdCodeSelected.value.replace(txt[2].value + "~~" + txt[1].value + "#", "");
                                hdnProdCodeSelected.value = hdnProdCodeSelected.value + txt[2].value + "~~" + txt[1].value + "#";
                                objTextBox.value = objTextBox.value.replace(lbl[0].innerHTML + ", ", "");
                                objTextBox.value = objTextBox.value + lbl[0].innerHTML + ", ";
                            }
                        }
                        else {
                            hdnProdCodeSelected.value = hdnProdCodeSelected.value.replace(txt[2].value + "~~" + txt[1].value + "#", "");
                            objTextBox.value = objTextBox.value.replace(lbl[0].innerHTML + ", ", "");
                        }
                    }
                }
                else {
                    txt.style.display = "none";
                    var table = document.getElementById("grdDCR_ctl" + index + "_tbl");
                    var row = table.rows[control];
                    var lbl = row.getElementsByTagName('span');
                    var txt = row.getElementsByTagName('input');
                    var txthdnProd = txt[2].value + "~~" + txt[1].value + "#";
                    objTextBox.value = objTextBox.value.replace(lbl[0].innerHTML + ", ", "");
                    hdnProdCodeSelected.value = hdnProdCodeSelected.value.replace(txthdnProd, "");
                }
            }
        }
    </script>

    <script type="text/javascript" src="//cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/js/bootstrap-select.min.js"></script>
</body>
</html>
