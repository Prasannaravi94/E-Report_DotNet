<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="inputedit.aspx.cs" Inherits="MasterFiles_ActivityReports_inputedit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            -webkit-appearance: none;
            width: 230px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .dd {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            width: 70px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .ddl1 {
            border: 1px solid #1E90FF;
            border-radius: 5px;
            -webkit-appearance: none;
            width: 190px;
            height: 21px;
            font: bold;
            background-image: url('Images/arrow_sort_d.gif');
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow:;
        }

        #effect {
            width: 180px;
            height: 160px;
            padding: 0.4em;
            position: relative;
            overflow: auto;
        }

        .textbox {
            width: 185px;
            height: 14px;
        }

        body {
            font-size: 62.5%;
            background: none !important;
            background-color: #fafdff !important;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <ucl:Menu ID="menu" runat="server" />
    <script type="text/javascript" src="../../JsFiles/jquery.effects.core.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery.effects.blind.js"></script>
    <%--<script type="text/javascript" src="../../JsFiles/multiple-select.js"></script>--%>
    <script language="javascript" type="text/javascript">
        $(function () {
            $('.custom-select2').hide();
            $("#effect").hide();
            //run the currently selected effect
            function runEffect() {
                //get effect type from
                if (!($('#effect').is(":visible"))) {
                    //run the effect
                    $("#effect").show('blind', 200);
                }
                else {
                    $("#effect").hide('blind', 200);
                }
            };

            //set effect from select menu value
            $("#ddlArrow").click(function () {
                runEffect();
                return false;
            });


            $(document).click(function (e) { if (($('#effect').is(":visible"))) { $("#effect").hide('blind', 1000); } });

            $('#effect').click(function (e) {
                e.stopPropagation();
            });
        });

        function autoCompleteEx_ItemSelected(sender, args) {
            __doPostBack(sender.get_element().name, "");
        }

        function SubmitForm() {

            var     66666666666` = document.getElementById('<%=ddlYear.ClientID%>');
            var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>');

            if (ddlYear.selectedIndex == 0) {
                alert("Please select Year!!");
                ddlYear.focus();
                return false;
        }
        else if (ddlMonth.selectedIndex == 0) {
                alert("Please select Month!!");
                ddlMonth.focus();
                return false;
        }

            //document.getElementById('ctl00_MainContent_btnHdn').click();
            var clickButton = document.getElementById("<%= btnHdn.ClientID %>");

            var table = document.getElementById("<%= gvSampleProducts.ClientID %>");
            if (table != null) {
                var rowCount = table.rows.length;
                var prodVal = '';
                var prodName = '';
                for (var i = 1; i < rowCount; i++) {
                    var row = table.rows[i];

                    var label = row.getElementsByTagName('span');
            //alert(chkbox[0].value);
                    var txt = row.getElementsByTagName('input');
            //alert(txt[1].value);

                    if (txt[1].value == "") {
                        alert("Please Enter Quantity!!");
                        txt[1].focus();
                        return false;
        }

                    if (prodVal == "") {

                        prodVal = txt[0].value + '|' + label[0].innerHTML + '|' + txt[1].value;
                        prodName = label[0].innerHTML + '(' + txt[1].value + ')';
        }
        else {
                        prodVal = prodVal + "," + txt[0].value + '|' + label[0].innerHTML + '|' + txt[1].value;
                        prodName = prodName + "," + label[0].innerHTML + '(' + txt[1].value + ')';
        }
            //chkbox[0].value = 0;
            //txt[1].value = "";
        }

                document.getElementById("<%= hdnProdCode.ClientID %>").value = prodVal;
                document.getElementById("<%= hdnProdName.ClientID %>").value = prodName;
            //document.getElementById('lblProd').value = prodVal;

        }
        else {
                var table = document.getElementById("ctl00_MainContent_tbl");
                var rowCount = table.rows.length;

                for (var i = 0; i < rowCount; i++) {
                    var row = table.rows[i];
                    var chkbox = row.getElementsByTagName('select');
            //alert(chkbox[0].value);
                    var txt = row.getElementsByTagName('input');
                    var lbl = row.getElementsByTagName('span');
            //alert(chkbox);
                    if (txt[0].checked) {
                        if (txt[1].value == "") {
                            alert('Please Enter Quantity');
                            txt[1].focus();
                            return false;
        }
        }
        }
        }

            clickButton.click();
        }

        function Validate() {
            var lstFieldForce = document.getElementById('<%=ddlFieldForce.ClientID%>');
            var lstBaseLevel = document.getElementById('<%=ddlMR.ClientID%>');
            var ddlYear = document.getElementById('<%=ddlYear.ClientID%>');
            var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>');
            var rdoInput = document.getElementsByName('<%=rdoInput.ClientID%>');
            //var divInputs = document.getElementById('<%=divInputs.ClientID%>');
            //var btnSave = document.getElementById('<%=btnSave.ClientID%>');
            var rdoSelected = '';

            if (lstFieldForce.selectedIndex == -1) {
                alert("Please select FieldForce!!");
                lstFieldForce.focus();
                return false;
        }
        else if (lstBaseLevel.selectedIndex == -1) {
                alert("Please select BaseLevel!!");
                lstBaseLevel.focus();
                return false;
        }
        else if (ddlYear.selectedIndex == 0) {
                alert("Please select Year!!");
                ddlYear.focus();
                return false;
        }
        else if (ddlMonth.selectedIndex == 0) {
                alert("Please select Month!!");
                ddlMonth.focus();
                return false;
        }
        }

        function GetAllProducts() {
            var list = document.getElementById("<% =rdoInput.ClientID %>"); //Cleint ID of RadioButtonList
            var rdbtnLstValues = list.getElementsByTagName("input");

            var divInputs = document.getElementById('<%=divInputs.ClientID%>');

            //if (divProducts.length > 0) {
            divProducts.style.display = 'block';
            //}

            var Checkdvalue = '';
            for (var i = 0; i < rdbtnLstValues.length; i++) {
                if (rdbtnLstValues[i].checked) {
                    Checkdvalue = rdbtnLstValues[i].value;
                    break;
        }
        }
        }

        function ControlVisibility(control) {

            var chk = document.getElementById("ctl00_MainContent_chkNew" + control);
            var txt = document.getElementById("ctl00_MainContent_txtNew" + control);
            var objTextBox = document.getElementById("<%=txtInputs.ClientID%>");
            var hdnProdCode = document.getElementById("<%=hdnProdCode.ClientID%>");
            var hdnProdName = document.getElementById("<%=hdnGift.ClientID%>");

            if (chk.checked) {
                txt.style.display = "block";

                var table = document.getElementById("ctl00_MainContent_tbl");
                var rowCount = table.rows.length;
                objTextBox.value = "";
                for (var i = 0; i < rowCount; i++) {
                    var row = table.rows[i];
                    var chkbox = row.getElementsByTagName('select');
            //alert(chkbox[0].value);
                    var txt = row.getElementsByTagName('input');
                    var lbl = row.getElementsByTagName('span');

                    if (txt[0].checked) {
            // alert(txt[1].value);
                        if (objTextBox.value == "") {
                            objTextBox.value = lbl[0].innerHTML + "-" + txt[1].value;
                            hdnProdName.value = lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value;
                            hdnProdCode.value = txt[2].value + "-" + txt[1].value;
        }
        else {
                            objTextBox.value = objTextBox.value + "," + lbl[0].innerHTML + "-" + txt[1].value;
                            hdnProdName.value = hdnProdName.value + "," + lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value;
                            hdnProdCode.value = hdnProdCode.value + "," + txt[2].value + "-" + txt[1].value;
        }
        }
        else {
                        objTextBox.value = objTextBox.value.replace(lbl[0].innerHTML + "-" + txt[1].value, "");
                        hdnProdName.value = hdnProdName.value.replace(lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value, "");
                        hdnProdCode.value = hdnProdCode.value.replace(txt[2].value + "-" + txt[1].value, "");
        }
        }
            //this.SetValue();
        }
        else {
                txt.style.display = "none";
                var table = document.getElementById("ctl00_MainContent_tbl");
                var row = table.rows[control];
                var lbl = row.getElementsByTagName('span');
                var txt = row.getElementsByTagName('input');
                var txthdnProd = txt[2].value + "-" + txt[1].value;
                var txthdnProdName = lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value;
                var lblVal = lbl[0].innerHTML + "-" + txt[1].value;
                objTextBox.value = objTextBox.value.replace(lblVal + ",", "");
                objTextBox.value = objTextBox.value.replace("," + lblVal, "");
                objTextBox.value = objTextBox.value.replace(lblVal, "");
                objTextBox.value = objTextBox.value.replace(",,", ",");
                hdnProdCode.value = hdnProdCode.value.replace(txthdnProd + ",", "");
                hdnProdCode.value = hdnProdCode.value.replace("," + txthdnProd, "");
                hdnProdCode.value = hdnProdCode.value.replace(txthdnProd, "");
                hdnProdCode.value = hdnProdCode.value.replace(",,", ",");
                hdnProdName.value = hdnProdName.value.replace(txthdnProdName + ",", "");
                hdnProdName.value = hdnProdName.value.replace("," + txthdnProdName, "");
                hdnProdName.value = hdnProdName.value.replace(txthdnProdName, "");
                hdnProdName.value = hdnProdName.value.replace(",,", ",");
        }

            //alert(hdnProdName.value);
            this.addMultipleRows(hdnProdName.value);
        }

        function addMultipleRows(products) {
            var table = document.getElementById("<%= tblProductList.ClientID %>");
            var prodArray = products.split(',');

            var totalLength = table.rows.length - 1;
            this.deleteRow();
            //if (totalLength != prodArray.length) {
            for (var i = 0; i < prodArray.length; i++) {

                var prodVal = prodArray[i].split('-')
                var prodQty = '';
                var giftType = '';
                if (prodVal.length > 1) {
                    prodQty = prodVal[1];
        }

                if (prodVal.length > 2) {
                    giftType = prodVal[2];
        }
                addRow(prodVal[0], prodQty, giftType);
        }
            table.deleteRow(1);
            //}

        }

        function addRow(product, prodcount, newgiftType) {
            var table = document.getElementById("<%= tblProductList.ClientID %>");
            //alert(product);
            var rowCount = table.rows.length;
            var row = table.insertRow(rowCount);
            var colCount = table.rows[1].cells.length;
            for (var i = 0; i < colCount; i++) {
                var newcell = row.insertCell(i);

                newcell.innerHTML = table.rows[1].cells[i].innerHTML;
            //alert(newcell.childNodes);
                if (i == 0) {

            //newcell.childNodes[1].innerText = product;
                    newcell.childNodes[1].innerHTML = product;
            //alert("Test" + newcell.childNodes[1].innerText);
        }
        else if (i == 1) {
            //newcell.childNodes[1].innerText = prodcount;
                    newcell.childNodes[1].innerHTML = prodcount;
        }
        else if (i == 2) {
            //newcell.childNodes[1].innerText = newgiftType;
                    newcell.childNodes[1].innerHTML = newgiftType;
        }
        }
        }

        function deleteRow() {
            try {
                var table = document.getElementById("<%= tblProductList.ClientID %>");
                var rowCount = table.rows.length;
                for (var i = 2; i < rowCount; i++) {
                    var row = table.rows[i];

                    if (rowCount <= 1)
        { break; }
                    table.deleteRow(i);
                    rowCount--;
                    i--;

        }
        } catch (e) {
                alert(e);
        }
        }
    
    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
            $("[id*=ddlMR]").select2();            
        });
    </script>
    <div class="container home-section-main-body position-relative clearfix">
        <div class="row justify-content-center">
            <div class="col-lg-5">
                <center>
                    <table>
                        <tr>
                            <td align="center">
                                <h2 class="text-center">Input Despatch - Edit</h2>
                            </td>
                        </tr>
                    </table>
                </center>
                <div class="designation-area clearfix">
                    <div class="single-des clearfix">
                        <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged" Width="100%">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" Visible="False"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="single-des clearfix">
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </div>
                    <div class="single-des clearfix">
                        <asp:Label ID="Label1" runat="server" Text="Base Level" CssClass="label" Visible="False"></asp:Label>
                        <asp:DropDownList ID="ddlMR" runat="server" CssClass="custom-select2 nice-select" Width="100%" Visible="False">
                        </asp:DropDownList>
                    </div>
                    <div class="single-des clearfix">
                        <asp:Label ID="lblYear" runat="server" Text="Year" CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlrequired"
                            CssClass="dd" Font-Bold="True">
                            <asp:ListItem Value="0" Text="--Select----"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="single-des clearfix">
                        <asp:Label ID="lblMonth" runat="server" Text="Month" CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlrequired"
                            CssClass="dd" Font-Bold="True">
                            <asp:ListItem Value="0" Text="--Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="single-des clearfix">
                        <asp:Label ID="lnput" runat="server" Text="Input" CssClass="label"></asp:Label>
                        <asp:RadioButtonList ID="rdoInput" runat="server" RepeatDirection="Horizontal" CellSpacing="2" CellPadding="2" Width="230px">
                            <asp:ListItem Value="All" Text="All Input wise" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="w-100 designation-submit-button text-center clearfix">
                    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" OnClick="btnGo_Click" OnClientClick="return Validate();" />
                    <asp:Button ID="LinkButton1" runat="server" Text="Clear List" class="savebutton" OnClick="LinkButton1_Click" OnClientClick="Reset()" />
                </div>
            </div>
            <div class="col-lg-12">
                <table>
                    <tr>
                        <td colspan="4" align="center">
                            <div id="divInputs" runat="server">
                                <table id="tblProducts" runat="server" width="60%" border="1" visible="false">
                                    <tr>
                                        <td align="left">
                                            <div class="demo">
                                                <div class="toggler">
                                                    <span>
                                                        <asp:TextBox ID="txtInputs" runat="server" CssClass="dd" Width="180px"></asp:TextBox>
                                                        <img id="ddlArrow" src="../../Images/down_arrow.jpg" style="margin-left: -23px; margin-bottom: -4px" />
                                                    </span>
                                                    <div id="effect" class="ui-widget-content">
                                                        <asp:Panel ID="pnlList" runat="server" CssClass="dd" Width="180px" Style="white-space: nowrap;" />
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table id="tblProductList" runat="server" width="40%" border="1" class="mGrid">
                                                <tr>
                                                    <td>Input
                                                    </td>
                                                    <td>Despatch Quantity
                                                    </td>
                                                    <td>Gift Type
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span id="lblProduct"></span>&nbsp;
                                                    </td>
                                                    <td>
                                                        <span id="lblProductCount"></span>
                                                    </td>
                                                    <td>
                                                        <span id="lblGiftType"></span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <center>
                    <div class="display-reportMaintable clearfix">
                        <div class="table-responsive" style="padding-bottom: 20px;">
                            <table align="center">
                                <tr>
                                    <td style="padding-left: 90px;">
                                        <asp:DataList ID="gvSampleProducts" Width="100%" runat="server" RepeatDirection="Vertical">
                                            <HeaderTemplate>
                                                <div style="height: 50px; width: 100%">
                                                    <div style="height: 50px;">
                                                        <table style="width: 100%; background-color: #f1f5f8;">
                                                            <tr>
                                                                <td style="width: 75px; height: 50px; text-align: center;">
                                                                    <asp:Label ID="lblsln" runat="server" Font-Bold="true" Text="#"></asp:Label>
                                                                </td>
                                                                <td style="width: 250px;">
                                                                    <asp:Label ID="lblgft" Text="Gift Name" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 175px; text-align: center;">
                                                                    <asp:Label ID="qty" Text="Despatch Qty" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 175px; text-align: center;">
                                                                    <asp:Label ID="remarks" Text="Remarks" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemStyle BorderWidth="0px" />
                                            <AlternatingItemStyle />
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <div style="height: 30px; width: 100%">
                                                    <table>
                                                        <tr style="width: 100%; border-bottom: 1px solid #DCE2E8; border-right: 1px solid #DCE2E8; border-left: 1px solid #DCE2E8;">
                                                            <td style="width: 75px; text-align: center;height:30px">
                                                                <asp:Label ID="lblSLNO" runat="server" Width="65px" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 250px; border-left: none;">
                                                                <asp:Label ID="lblInput" runat="server" Text='<%# Eval("Gift_Name") %>' Width="225px"></asp:Label>
                                                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("productc") %>' />
                                                            </td>
                                                            <td style="width: 175px; text-align: center">
                                                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Despatch_Qty")%>' Width="100px" Height="25px" CssClass="ddl" MaxLength="4"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtQuantity">
                                                                </asp:FilteredTextBoxExtender>
                                                            </td>
                                                            <td style="width: 175px; text-align: center">
                                                                <asp:TextBox ID="txtremarks" runat="server" Text='<%#Eval("Remarks")%>' Width="100px" Height="25px" CssClass="ddl"></asp:TextBox>
                                                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"  TargetControlID="txtremarks">
                                                                </asp:FilteredTextBoxExtender>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </center>
            </div>
            <table>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="SubmitForm();return false;" OnClick="btnSave_Click"
                            CssClass="savebutton" Visible="false" />
                        <asp:Button ID="btnHdn" runat="server" Text="Hidden" Style="display: none;" OnClick="btnSave_Click" />
                        <asp:HiddenField ID="hdnProdCode" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdnProdName" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdnGift" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdnInput" runat="server"></asp:HiddenField>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $('.custom-select2').hide();
        });
    </script>
    <script language="javascript" type="text/javascript">
        function GetSelectedValue() {

        }

        function ValidateProducts() {

            var txtInputs = document.getElementById("<%=txtInputs.ClientID%>");

            if (txtInputs.value == "") {
                alert("Please select Gifts!!");
                txtInputs.focus();
                return false;
        }
        else {
                var table = document.getElementById("ctl00_MainContent_tbl");
                var rowCount = table.rows.length;

                for (var i = 0; i < rowCount; i++) {
                    var row = table.rows[i];
                    var chkbox = row.getElementsByTagName('select');
            //alert(chkbox[0].value);
                    var txt = row.getElementsByTagName('input');
                    var lbl = row.getElementsByTagName('span');
            //alert(chkbox);
                    if (txt[0].checked) {
                        if (txt[1].value == "") {
                            alert('Please Enter Quantity');
                            txt[1].focus();
                            return false;
        }
        }
        }
        }

            document.getElementById('ctl00_MainContent_btnHdnProd').click();
        }
    </script>
</asp:Content>
