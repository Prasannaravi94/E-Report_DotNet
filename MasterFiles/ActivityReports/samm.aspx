<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="samm.aspx.cs" Inherits="MasterFiles_AnalysisReports_samm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" href="../../css/Report.css" rel="Stylesheet" />
    <%--<link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />--%>
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <style type="text/css">
        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            font-size: 11px;
            font-family: Calibri;
            -webkit-appearance: none;
            width: 300px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
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
        }

        .ddl1 {
            border: 1px solid #1E90FF;
            border-radius: 5px;
            -webkit-appearance: none;
            width: 190px;
            height: 21px;
            font-weight: bold;
            background-image: url('Images/arrow_sort_d.gif');
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
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
            padding-bottom: 20px;
            padding-right: 10px;
        }

        .style1 {
            width: 195px;
        }

        .style2 {
            width: 232px;
        }

        /*.list {
            display: none;
        }*/
        .selectpicker > ul {
            display: none;
        }

        .selectpicker > span {
            display: none;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .bootstrap-select:not([class*="col-"]):not([class*="form-control"]):not(.input-group-btn) {
            width: 295px !important;
            color: #90a1ac;
            font-size: 14px !important;
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            padding-left: 3px;
            padding-right: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="Divid" runat="server">
    </div>
    <script type="text/javascript" src="../../JsFiles/jquery.effects.core.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery.effects.blind.js"></script>
    <%--<script type="text/javascript" src="../../JsFiles/multiple-select.js"></script>--%>

    <script language="javascript" type="text/javascript">
        function validateTextBox() {
            // Get The base and Child controls
            var TargetBaseControl = document.getElementById('<%=this.data.ClientID%>');
            var TargetChildControl1 = "txtQuantity";
            // Get the all the control of the type Input in the basse contrl
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            // loop thorught the all textboxes
            for (var n = 0; n < Inputs.length; ++n) {
                if (Inputs[n].type == 'text' && Inputs[n].id.indexOf(TargetChildControl1, 0) >= 0) {
                    // Validate for input
                    //                    if (Inputs[n].value != "")

                    //                        return true; alert('Enter Product Quantity!');
                    //                    $("#txtQuantity").focus();
                    //                    return false;

                }
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $('.selectpicker').removeClass('nice-select');
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

            var ddlYear = document.getElementById('<%=ddlYear.ClientID%>');
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








            var table = document.getElementById("<%= data.ClientID %>");
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

                    var TargetBaseControl = document.getElementById('<%=this.data.ClientID%>');
                    var TargetChildControl1 = "txtQuantity";
                    // Get the all the control of the type Input in the basse contrl
                    var Inputs = TargetBaseControl.getElementsByTagName("input");
                    // loop thorught the all textboxes
                    for (var n = 0; n < Inputs.length; ++n) {
                        if (Inputs[n].type == 'text' && Inputs[n].id.indexOf(TargetChildControl1, 0) >= 0) {
                            // Validate for input
                            if (Inputs[n].value != "")

                                return true; alert('Enter Product !');
                            return false;

                        }
                    }
                    //                    if (txt[1].value == "") {
                    //                        alert("Ente Product Quantity!!");
                    //                        txt[1].focus();
                    //                        return false;
                    //                    }

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

                //                document.getElementById("<%= hdnProdCode.ClientID %>").value = prodVal;
                //                document.getElementById("<%= hdnProdName.ClientID %>").value = prodName;
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
                    //                    if (txt[0].checked) {
                    //                        if (txt[1].value == "") {
                    //                            alert('Enter Product Quantity');
                    //                            txt[1].focus();
                    //                            return false;
                    //                        }
                    //                    }
                }
            }

            clickButton.click();
        }

        function Validate() {
            var lstFieldForce = document.getElementById('<%=lstFieldForce.ClientID%>');
            var lstBaseLevel = document.getElementById('<%=lstBaseLevel.ClientID%>');
            var ddlYear = document.getElementById('<%=ddlYear.ClientID%>');
            var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>');
            var rdoProduct = document.getElementsByName('<%=rdoProduct.ClientID%>');
            //var divProducts = document.getElementById('<%=divProducts.ClientID%>');
            //var btnSave = document.getElementById('<%=btnSave.ClientID%>');
            var rdoSelected = '';

            if (lstFieldForce.selectedIndex == -1) {
                alert("select the FieldForce!!");
                lstFieldForce.focus();
                return false;
            }
            else if (lstBaseLevel.selectedIndex == -1) {
                alert("select the BaseLevel!!");
                lstBaseLevel.focus();
                return false;
            }
            else if (ddlYear.selectedIndex == 0) {
                alert("select Year!!");
                ddlYear.focus();
                return false;
            }
            else if (ddlMonth.selectedIndex == 0) {
                alert("select Month!!");
                ddlMonth.focus();
                return false;
            }

            //if (divProducts.length > 0) {
            //divProducts.style.display = 'block';
            //}

            //btnSave.style.display = 'block';
            //this.GetAllProducts();
            //return false;
        }

        function GetAllProducts() {
            var list = document.getElementById("<% =rdoProduct.ClientID %>"); //Cleint ID of RadioButtonList
            var rdbtnLstValues = list.getElementsByTagName("input");

            var divProducts = document.getElementById('<%=divProducts.ClientID%>');

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

            var rowCount = table.rows.length;
            var row = table.insertRow(rowCount);
            var colCount = table.rows[1].cells.length;
            for (var i = 0; i < colCount; i++) {
                var newcell = row.insertCell(i);
                newcell.innerHTML = table.rows[1].cells[i].innerHTML;
                //alert(newcell.childNodes);
                if (i == 0) {
                    newcell.childNodes[1].innerHTML = product;
                }
                else if (i == 1) {
                    newcell.childNodes[1].innerHTML = newgiftType;
                }
                else if (i == 2) {
                    newcell.childNodes[1].innerHTML = prodcount;
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
    <div class="container home-section-main-body position-relative clearfix">
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <center>
                    <table>
                        <tr>
                            <td align="center">
                                <h2 class="text-center">Sample Despatch from HQ</h2>
                            </td>
                        </tr>
                    </table>
                </center>
                <center>
                    <table width="30%">
                        <tr>
                            <td align="left">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblsub" runat="server" Text="SubDivision" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlsub" runat="server" SkinID="ddlrequired" CssClass="dd" AutoPostBack="true"
                                            Font-Bold="True" OnSelectedIndexChanged="ddlsub_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div class="designation-area clearfix">
                                    <div class="clearfix" style="margin-bottom: 5px;">
                                        <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name"
                                            CssClass="label"></asp:Label>
                                    </div>
                                    <div class="clearfix" style="margin-bottom: 10px;">
                                        <div style="float: left; width: 90%;">
                                            <select id="lstFieldForce" class="selectpicker" runat="server" placeholder="Select FieldForce"
                                                data-live-search="true" multiple data-actions-box="true"
                                                style="font-weight: bold; padding-top: 3px; width: 100%;">
                                            </select>
                                        </div>
                                        <div style="float: right; width: 9%; margin-top: 2px; margin-right: 3px;">
                                            <asp:Button ID="btnFieldForce" Text="Go" runat="server" OnClick="Submit" CssClass="savebutton" Width="40" Height="28" />
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div class="designation-area clearfix">
                                    <div class="clearfix" style="margin-bottom: 5px;">
                                        <asp:Label ID="Label1" runat="server" Text="Base Level" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="clearfix" style="margin-bottom: 10px;">
                                        <asp:ListBox ID="lstBaseLevel" runat="server" CssClass="selectpicker"
                                            data-live-search="true" multiple data-actions-box="true" Font-Bold="True">
                                            <asp:ListItem Value="0" Text="-----Select the Baselevel-----"></asp:ListItem>
                                        </asp:ListBox>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblYear" runat="server" Text="Year" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlrequired"
                                            CssClass="dd" Font-Bold="True">
                                            <asp:ListItem Value="0" Text="--Select----"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div class="designation-area clearfix">
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
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblProd" runat="server" Text="Product" CssClass="label"></asp:Label>
                                        <asp:RadioButtonList ID="rdoProduct" runat="server"
                                            RepeatDirection="Horizontal" CellSpacing="2" CellPadding="2" Width="230px">
                                            <asp:ListItem Value="All" Text="All Product wise" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style2" align="left" style="padding-bottom: 6px;">
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="btnGo" runat="server" Text="Go" class="savebutton"
                                        OnClick="btnGo_Click" OnClientClick="return Validate();" />
                                    <asp:Button ID="LinkButton1" runat="server" Text="Clear" class="savebutton"
                                        OnClick="LinkButton1_Click" OnClientClick="valit()" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </center>
                <center>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-table clearfix">
                            <div class="table-responsive">
                                <asp:GridView ID="gvSampleDespatch" runat="server" AutoGenerateColumns="false" OnRowCommand="gvSampleDespatch_RowCommand"
                                    OnRowDataBound="gvSampleDespatch_RowDataBound" OnRowDeleting="gvSampleDespatch_RowDeleting"
                                    CssClass="table" Width="65%" AlternatingRowStyle-BorderColor="#666699">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="20px" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Products" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlProducts" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Despatch Qty" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtProducts" runat="server" Text='<%# Eval("DespatchQty") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure to delete?');"
                                                    CommandName="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </center>
                <center>
                    <table>
                        <tr>
                            <td colspan="4" align="center" style="padding-top: 20px;">
                                <div id="divProducts" runat="server">
                                    <table id="tblProducts" runat="server" width="60%" border="1" visible="false">
                                        <tr>
                                            <td align="left" class="style1">
                                                <div class="demo">
                                                    <div class="toggler">

                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtProducts" runat="server" CssClass="ddl1"></asp:TextBox></td>
                                                                <td>
                                                                    <img id="ddlArrow" src="../../Images/down_arrow.jpg" style="margin-left: -23px; margin-bottom: -4px" /></td>
                                                            </tr>
                                                        </table>


                                                        <div id="effect" class="ui-widget-content">
                                                            <asp:Panel ID="pnlList" runat="server" CssClass="ddl" Style="white-space: nowrap;" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="style1">
                                                <table id="tblProductList" runat="server" width="60%" border="1" class="mGrid"
                                                    style="border-width: 2px; border-color: #1E90FF;">
                                                    <tr>
                                                        <td align="center" style="font-weight: bold; font-family: Andalus">Products
                                                        </td>
                                                        <td align="center" style="font-weight: bold; font-family: Andalus">Pack
                                                        </td>
                                                        <td align="center" style="font-weight: bold; font-family: Andalus">Despatch Qty
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="lblProduct"></span>&nbsp;
                                                        </td>
                                                        <td>
                                                            <span id="lblPack"></span>
                                                        </td>
                                                        <td>
                                                            <span id="lblProductCount"></span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </table>

                                </div>
                            </td>
                        </tr>
                    </table>
                    <div class="display-reportMaintable clearfix">
                        <div class="table-responsive" style="padding-bottom: 20px;">
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:DataList ID="data" Width="100%" runat="server" RepeatDirection="Vertical" RepeatColumns="2">
                                            <HeaderTemplate>
                                                <div style="height: 30px; width: 100%">
                                                    <div style="width: 50%; float: left; height: 50px;">
                                                        <table style="width: 100%; background-color: #f1f5f8;">
                                                            <tr>
                                                                <td style="width: 75px; height: 50px; text-align: center;">
                                                                    <asp:Label ID="lblsln" runat="server" Font-Bold="true" Text="#"></asp:Label>
                                                                </td>
                                                                <td style="width: 250px;">
                                                                    <asp:Label ID="lblDocVisit" Text="Product Name" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 100px; text-align: center;">
                                                                    <asp:Label ID="lblunit" Text="Pack" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 175px; text-align: center;">
                                                                    <asp:Label ID="qty" Text="Despatch Qty" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div style="width: 50%; float: right; height: 50px;">
                                                        <table style="width: 100%; background-color: #f1f5f8;">
                                                            <tr>
                                                                <td style="width: 75px; height: 50px; text-align: center;">
                                                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="#"></asp:Label>
                                                                </td>
                                                                <td style="width: 250px;">
                                                                    <asp:Label ID="lbl2" Text="Product Name" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 100px; text-align: center;">
                                                                    <asp:Label ID="lbl3" Text="Pack" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 175px; text-align: center;">
                                                                    <asp:Label ID="qty1" Text="Despatch Qty" Font-Bold="true" runat="server"></asp:Label>
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
                                                            <td style="width: 75px; text-align: center;">
                                                                <asp:Label ID="lblSLNO" runat="server" Width="65px" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 250px; border-left: none;">
                                                                <asp:Label ID="lblProducts" runat="server" Text='<%# Eval("Product_Detail_Name") %>' Width="250px"></asp:Label>
                                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Product_Detail_Code") %>' />
                                                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("Product_Code_SlNo") %>' />
                                                                <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Eval("Product_Sample_Unit_One") %>' />
                                                            </td>
                                                            <td style="width: 100px; border-left: none">
                                                                <asp:Label ID="lblprd_sale" runat="server" Text='<%#Eval("Product_Sale_Unit")%>' Width="100px"></asp:Label>
                                                            </td>
                                                            <td style="width: 175px">
                                                                <asp:TextBox ID="txtQuantity" runat="server" Width="75px" Height="25px" CssClass="ddl" MaxLength="4"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtQuantity">
                                                                </asp:FilteredTextBoxExtender>
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
                <table width="100%;">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Save"
                                Visible="false" ImageUrl="~/css/save.png" OnClick="btnSave_Click2" OnClientClick="javascript:return validateTextBox();"
                                CssClass="savebutton" />

                            <asp:Button ID="btnHdn" runat="server" Text="Hidden" Style="display: none;" OnClick="btnSave_Click" />
                            <asp:HiddenField ID="hdnProdCode" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hdnProdName" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hdnProducts" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hdnProductPack" runat="server"></asp:HiddenField>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <%--<link href="../../css/multiple-select.css" rel="stylesheet" type="text/css" />--%>
    <%--<script src="../../JsFiles/multiple-select.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/js/bootstrap-select.min.js"></script>


    <script type="text/javascript">
        <%--$('[id*=lstFieldForce]').multipleSelect({
            onClose: function (event) {
                document.getElementById("<%= btnFieldForce.ClientID %>").click();

            }
        });

            $('[id*=lstBaseLevel]').multipleSelect();--%>

    </script>
    <script language="javascript" type="text/javascript">
        function GetSelectedValue() {

        }

        function ControlVisibility(control) {

            var chk = document.getElementById("ctl00_MainContent_chkNew" + control);
            var txt = document.getElementById("ctl00_MainContent_txtNew" + control);
            var objTextBox = document.getElementById("<%=txtProducts.ClientID%>");
            var hdnProdCode = document.getElementById("<%=hdnProdCode.ClientID%>");
            var hdnProductPack = document.getElementById("<%=hdnProductPack.ClientID%>");

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
                    //alert(chkbox);
                    if (txt[0].checked) {
                        if (objTextBox.value == "") {
                            objTextBox.value = lbl[0].innerHTML + "-" + txt[1].value;
                            hdnProductPack.value = lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value;
                            hdnProdCode.value = txt[2].value + "-" + txt[1].value;
                        }
                        else {
                            objTextBox.value = objTextBox.value + "," + lbl[0].innerHTML + "-" + txt[1].value;
                            hdnProductPack.value = hdnProductPack.value + "," + lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value;
                            hdnProdCode.value = hdnProdCode.value + "," + txt[2].value + "-" + txt[1].value;
                        }
                    }
                    else {
                        objTextBox.value = objTextBox.value.replace(lbl[0].innerHTML + "-" + txt[1].value, "");
                        hdnProductPack.value = hdnProductPack.value.replace(lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value, "");
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
                var txthdnProdPack = lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value;
                var lblVal = lbl[0].innerHTML + "-" + txt[1].value;
                objTextBox.value = objTextBox.value.replace(lblVal + ",", "");
                objTextBox.value = objTextBox.value.replace("," + lblVal, "");
                objTextBox.value = objTextBox.value.replace(lblVal, "");
                objTextBox.value = objTextBox.value.replace(",,", ",");
                hdnProdCode.value = hdnProdCode.value.replace(txthdnProd + ",", "");
                hdnProdCode.value = hdnProdCode.value.replace("," + txthdnProd, "");
                hdnProdCode.value = hdnProdCode.value.replace(txthdnProd, "");
                hdnProdCode.value = hdnProdCode.value.replace(",,", ",");
                hdnProductPack.value = hdnProductPack.value.replace(txthdnProdPack + ",", "");
                hdnProductPack.value = hdnProductPack.value.replace("," + txthdnProdPack, "");
                hdnProductPack.value = hdnProductPack.value.replace(txthdnProdPack, "");
                hdnProductPack.value = hdnProductPack.value.replace(",,", ",");
            }

            this.addMultipleRows(hdnProductPack.value);
        }

        function ValidateProducts() {

            var txtProducts = document.getElementById("<%=txtProducts.ClientID%>");

            if (txtProducts.value == "") {
                alert("Please select Products!!");
                txtProducts.focus();
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
                            alert('Please Enter Product Quantity');
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


