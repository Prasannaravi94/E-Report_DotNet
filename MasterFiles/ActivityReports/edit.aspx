<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="MasterFiles_AnalysisReports_edit" %>

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
            padding-bottom: 20px;
            padding-right: 10px;
        }

        .style1 {
            width: 195px;
        }

        .style2 {
            width: 232px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <ucl:Menu ID="menu" runat="server" />
    <script type="text/javascript" src="../../JsFiles/jquery.effects.core.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery.effects.blind.js"></script>
    <%--<script type="text/javascript" src="../../JsFiles/multiple-select.js"></script>--%>
    <%--<script type="text/javascript">
        function valit() {
            //            $("#lstFieldForce").css('display', 'none');
            //document.getElementById("lstFieldForce").options.length = 0;
            var select = document.getElementById("lstFieldForce");
                        var length = select.options.length;
                        for (i = 0; i < length; i++) {
                            select.options[i] = null;
                        }
        }
    </script>--%>
    <script type="text/javascript">
        function Reset() {
            var dropDown = document.getElementById("ddlFieldForce");
            dropDown.selectedIndex = 0;

        }
    </script>
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
            //          
            var ddlYear = document.getElementById('<%=ddlYear.ClientID%>');
            var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>');
            var rdoProduct = document.getElementsByName('<%=rdoProduct.ClientID%>');
            //var divProducts = document.getElementById('<%=divProducts.ClientID%>');
            //var btnSave = document.getElementById('<%=btnSave.ClientID%>');
            var rdoSelected = '';

            //            if (lstFieldForce.selectedIndex == -1) {
            //                alert("select the FieldForce!!");
            //                lstFieldForce.focus();
            //                return false;
            //            }
            //            else if (lstBaseLevel.selectedIndex == -1) {
            //                alert("select the BaseLevel!!");
            //                lstBaseLevel.focus();
            //                return false;
            //            }
            if (ddlYear.selectedIndex == 0) {
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
                                <h2 class="text-center">Sample Despatch - Edit</h2>
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
                        <asp:Label ID="lblProd" runat="server" Text="Product" CssClass="label"></asp:Label>
                        <asp:RadioButtonList ID="rdoProduct" runat="server" RepeatDirection="Horizontal" CellSpacing="2" CellPadding="2" Width="230px">
                            <asp:ListItem Value="All" Text="All Product wise" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="w-100 designation-submit-button text-center clearfix">
                    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton"
                        OnClientClick="return Validate();" OnClick="btnGo_Click1" />
                    <asp:Button ID="LinkButton1" runat="server" Text="Clear List" class="savebutton"
                        OnClick="LinkButton1_Click1" OnClientClick="Reset()" />
                </div>
            </div>
            <div class="col-lg-12">
                <table>
                    <tr>
                        <td colspan="4" align="center" style="padding-top: 20px;">
                            <div id="divProducts" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <%--<asp:GridView ID="gvSampleProducts" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                        Width="100%" Visible="false">
                                        <Columns>
                                          <asp:TemplateField HeaderText="#" HeaderStyle-Width="20px" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProducts" runat="server" Text='<%# Eval("Product_Detail_Name") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnProducts" runat="server" Value='<%# Eval("Product_Detail_Code") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Product_Sale_Unit" HeaderText="Pack" HeaderStyle-ForeColor="White"
                                                HeaderStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="Despatch Qty" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" Width="50px"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtQuantity">
                                                    </asp:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>--%>                                    
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
                                        <asp:DataList ID="data" Width="100%" runat="server" RepeatDirection="Vertical" RepeatColumns="2">
                                            <HeaderTemplate>
                                                <div style="height: 50px; width: 100%">
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
                                                                  <td style="width: 175px; text-align: center;">
                                                                    <asp:Label ID="rmrks" Text="Remarks" Font-Bold="true" runat="server"></asp:Label>
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
                                                                  <td style="width: 175px; text-align: center;">
                                                                    <asp:Label ID="rmrks1" Text="Remarks" Font-Bold="true" runat="server"></asp:Label>
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
                                                            <td style="width: 65px; text-align: center;">
                                                                <asp:Label ID="lblSLNO" runat="server" Width="65px" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 150px; border-left: none;">
                                                                <asp:Label ID="lblProducts" runat="server" Text='<%# Eval("Product_Code") %>' Width="150px"></asp:Label>
                                                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("productc") %>' />
                                                            </td>
                                                            <td style="width: 65px; border-left: none">
                                                                <asp:Label ID="lblprd_sale" runat="server" Text='<%#Eval("Product_Sale_Unit")%>' Width="65px"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px; text-align: center">
                                                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Despatch_Qty")%>' Width="100px" Height="25px" CssClass="ddl" MaxLength="4"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtQuantity">
                                                                </asp:FilteredTextBoxExtender>
                                                            </td>
                                                             <td style="width: 100px; text-align: center">
                                                                <asp:TextBox ID="txtremarks" runat="server" Text='<%#Eval("Remarks")%>' Width="100px" Height="25px" CssClass="ddl"></asp:TextBox>
                                                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtQuantity">
                                                                </asp:FilteredTextBoxExtender>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="20px"></asp:Label>
                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </table>
                        </div>
                    </div>
                    <table>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click2" CssClass="savebutton" Visible="false" />
                                <asp:Button ID="btnHdn" runat="server" Text="Hidden" Style="display: none;" />
                                <asp:HiddenField ID="hdnProdCode" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdnProdName" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdnProducts" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdnProductPack" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
        </div>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</asp:Content>

