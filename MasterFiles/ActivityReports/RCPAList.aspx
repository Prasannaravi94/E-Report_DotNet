<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="RCPAList.aspx.cs"
    Inherits="MasterFiles_RCPAList" EnableEventValidation="false" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" href="../../css/Report.css" rel="Stylesheet" />
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <link rel="shortcut icon" type="image/png" href="../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />
    <link rel="stylesheet" href="../assets/css/responsive.css" />

    <style type="text/css">
        .mGrid1 {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
            margin-right: 18px;
        }

            .mGrid1 td {
                padding: 2px;
                border: solid 1px Black;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                font-size: small;
                font-family: Calibri;
            }

            .mGrid1 th {
                padding: 4px 2px;
                color: white;
                background: #666699;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                border-bottom: solid 1px Black;
                font-weight: normal;
                font-size: small;
                font-family: Calibri;
            }


        .mGrid {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }

            .mGrid td {
                padding: 2px;
                border: solid 1px Black;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                font-size: small;
                font-family: Calibri;
            }

            .mGrid th {
                padding: 4px 2px;
                color: white;
                background: #A6A6D2;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                border-bottom: solid 1px Black;
                font-weight: normal;
                font-size: small;
                font-family: Calibri;
            }

            .mGrid .pgr {
                background: #A6A6D2;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    text-align: left;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: White;
                    line-height: 12px;
                }

                .mGrid .pgr th {
                    background: #A6A6D2;
                }

                .mGrid .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        .footer td {
            border: none;
        }

        .footer th {
            border: none;
        }

        .tbRCPA {
            width: 100%;
            background-color: white;
        }

        body {
            font-size: 62.5%;
            background: none !important;
            background-color: #fafdff !important;
        }


        /*.tbRCPA th span {
                color: white !important;
            }*/

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none !important;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        .chkChoice {
            margin-top: 5px;
            margin-left: 5px;
        }

            .chkChoice input[type="checkbox"] {
                margin-right: 5px;
            }

            .chkChoice td {
                padding-left: 5px;
            }

                .chkChoice td > label {
                    font-weight: unset;
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

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .chklabel {
            color: #636d73;
            font-size: 12px;
            font-weight: 401;
            text-transform: uppercase;
            text-align: center;
            padding-left: 10px;
        }

        .textbox {
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            color: #90a1ac;
            font-size: 14px;
            width: 100%;
            padding-right: 10px;
            height: 40px;
            text-align: right;
        }

        .tbRCPAth {
            background-color: #f1f5f8;
            height: 50px;
            width: 20%;
            text-align: center;
        }

        .display-table .table th:first-child {
            border-radius: 0px 0 0 0px !important;
            background-color: #f1f5f8 !important;
            color: #636d73 !important;
            font-size: 12px !important;
            font-weight: 401 !important;
            border-left: 0px solid #F1F5F8 !important;
        }

        .display-table .table th {
            font-size: 12px !important;
            font-weight: 401 !important;
        }

        .display-table .table tr:nth-child(2) td:first-child {
            background-color: #fff !important;
            color: #ffffff !important;
            border: 1px solid #dee2e6 !important;
        }

        .display-table .table tr td:first-child {
            background-color: #fff !important;
            border: 1px solid #dee2e6 !important;
        }

        .display-table .table td {
            border: 1px solid #DCE2E8 !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });

            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });

            $('#<%=btnGo.ClientID%>').click(function () {
                var st = $('#<%=ddlSFCode.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Field Force Name."); $('ddlSFCode').focus(); return false; }
                var st = $('#<%=ddlDocName.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Doctor Name."); $('ddlDocName').focus(); return false; }
                var group = $('#<%=cblChemistList.ClientID%> input:checked');
                var hasChecked = false;
                for (var i = 0; i < group.length; i++) {
                    if (group[i].checked)
                        hasChecked = true;
                    break;
                }
                if (hasChecked == false) {
                    alert("Select Chemist Name"); $('#<%=cblChemistList.ClientID%>').focus(); return false;
                }
            });

            $('#<%=lnkbtnCompMap.ClientID%>').click(function () {
                var st2 = $('#<%=ddlProduct.ClientID%> :selected').text();
                if (st2 == "---Select---") { alert("Select Product"); $('ddlProduct').focus(); return false; }
                if (st2 == "") { alert("Select Product"); $('ddlProduct').focus(); return false; }

                var st2 = $('#<%=txtQtyperMonth.ClientID%>').val();
                if (st2 == "") { alert("Enter Qty per Month"); $('txtQtyperMonth').focus(); return false; }

                var st2 = $('#<%=txtTotal.ClientID%>').val();
                if (st2 == "") { alert("Re-Enter Qty Manually"); $('txtTotal').focus(); return false; }
                else if (st2 == "NaN") { alert("Re-Enter Qty Manually"); $('txtTotal').focus(); return false; }

                $('#dvGridProduct').block({
                    message: '<h1>Please Wait...</h1>',
                    css: {
                        border: '3px solid #a00',
                        padding: '10px',
                        fontWeight: 'bold'
                    }
                });
            });
        });

        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1 || (keyCode == 46) && ($(this).indexOf('.') != -1));
            return ret;
        }

        function min(value, min) {
            if (parseInt(value) < min || isNaN(parseInt(value)))
                return 1;
            else return value;
        }

        var sum = 0;
        function FetchData(button) {
            var cell = button.parentNode;
            var row = button.parentNode.parentNode;
            var Roow = cell.getAttribute("rel");
            var Roow1 = button.getAttribute("rel");
            var label = GetChildControl(row, "txtOurVal").value;
            var label1 = GetChildControl(row, "txtQtyperMonth").value;
            var label2 = GetChildControl(row, "txtTotal").value;

            if (label2 == "") {
                var Multi = parseFloat(label) * parseFloat(label1);

                GetChildControl(row, "txtTotal").value = Multi;

                if (!isNaN(Multi) && Multi.length != 0) {
                    sum += parseFloat(Multi);
                }
            }
            else {
                if (!isNaN(label2) && label2.length != 0) {
                    sum -= parseFloat(label2);
                }
                var Multi = parseFloat(label) * parseFloat(label1);

                GetChildControl(row, "txtTotal").value = Multi;
                if (!isNaN(Multi) && Multi.length != 0) {
                    sum += parseFloat(Multi);
                }
            }
            return false;
        };

        function GetChildControl(element, id) {
            var child_elements = element.getElementsByTagName("*");
            for (var i = 0; i < child_elements.length; i++) {
                if (child_elements[i].id.indexOf(id) != -1) {
                    return child_elements[i];
                }
            }
        };

        var sum1 = 0;
        function FetchData1(button) {
            var cell = button.parentNode;
            var row1 = button.parentNode.parentNode;
            var Roow = cell.getAttribute("rel");
            var Roow1 = button.getAttribute("rel");
            var label = GetChildControl1(row1, "txtCPRate").value;
            var label1 = GetChildControl1(row1, "txtCPQty").value;
            var label2 = GetChildControl1(row1, "txtCPValue").value;

            if (label2 == "") {
                var Multi = parseFloat(label) * parseFloat(label1);

                GetChildControl1(row1, "txtCPValue").value = Multi;

                if (!isNaN(Multi) && Multi.length != 0) {
                    sum += parseFloat(Multi);
                }
            }
            else {
                if (!isNaN(label2) && label2.length != 0) {
                    sum -= parseFloat(label2);
                }
                var Multi = parseFloat(label) * parseFloat(label1);

                GetChildControl1(row1, "txtCPValue").value = Multi;
                if (!isNaN(Multi) && Multi.length != 0) {
                    sum += parseFloat(Multi);
                }
            }
            return false;
        };

        function GetChildControl1(element, id) {
            var child_elements = element.getElementsByTagName("*");
            for (var i = 0; i < child_elements.length; i++) {
                if (child_elements[i].id.indexOf(id) != -1) {
                    return child_elements[i];
                }
            }
        };
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../../Images/minus.png");
        });

        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../../Images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlSFCode]").select2();
        });
    </script>
    <div id="Divid" runat="server">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upRCPA" runat="server">
        <ContentTemplate>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <center>
                            <table>
                                <tr>
                                    <td align="center">
                                        <h2 class="text-center">RCPA - Entry</h2>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblSalesforce" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <asp:DropDownList ID="ddlSFCode" runat="server" CssClass="custom-select2 nice-select" Width="100%" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSFCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDocName" runat="server" CssClass="label" Text="Doctor Name"></asp:Label>
                                <asp:DropDownList ID="ddlDocName" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlDocName_SelectedIndexChanged" CssClass="selectpicker" data-live-search="true">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblChemistName" runat="server" CssClass="label" Text="Support Chemist"></asp:Label>
                                <div style="height: 150px; background-color: #f4f8fa; border: 1px solid Silver; overflow: auto; color: #90a1ac; font-size: 14px; border-radius: 8px; border: 1px solid #d1e2ea; background-color: #f4f8fa; margin-top: 5px;">
                                    <asp:CheckBoxList ID="cblChemistList" Font-Size="8pt" runat="server"
                                        CssClass="chkChoice">
                                    </asp:CheckBoxList><br />
                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" OnClick="btnGo_Click" />
                            <asp:Button ID="btnClear" CssClass="savebutton" runat="server" Text="Clear" OnClick="btnClear_Click" />
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <br />
                        <center>
                            <asp:Label ID="lblSelect" runat="server" Font-Size="Small" ForeColor="Red" Visible="false"
                                Text="Select the FieldForce Name, Doctor Name & Support Chemist and Press the 'Go' Button"></asp:Label>
                        </center>
                        <div class="designation-reactivation-table-area clearfix">
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvProductVal" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="false" CssClass="table"
                                        GridLines="None" HorizontalAlign="Center" BorderWidth="0"
                                        Width="100%" ShowFooter="true" FooterStyle-HorizontalAlign="Center" OnRowDataBound="gvProductVal_RowDataBound"
                                        Visible="false" OnRowDeleting="gvProductVal_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <img alt="" style="cursor: pointer" src="../../Images/plus.png" />
                                                    <asp:Panel ID="pnlComp" runat="server" Style="display: none">
                                                        <asp:GridView ID="gvComp" runat="server" AutoGenerateColumns="false" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Competitor Name" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCompName" runat="server" Text='<%# Eval("CompName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Brand Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCompPName" runat="server" Text='<%#Eval("CompPName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Competitor Rate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCPRate" runat="server" Text='<%#Eval("CPRate")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty per Month">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCPQty" runat="server" Text='<%#Eval("CPQty")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Value">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCPValue" runat="server" Text='<%#Eval("CPValue")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOPName" runat="server" Text='<%#Eval("OPName")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnOPCode" runat="server" Value='<%#Eval("OPCode")%>'></asp:HiddenField>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty Per Month" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOPQty" runat="server" Text='<%# Bind("OPQty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Our Rate" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOPRate" runat="server" Text='<%# Bind("OPRate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOPValue" runat="server" Text='<%# Bind("OPValue") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblbtnEdit" runat="server" ToolTip="Edit"
                                                        OnClick="lblbtnEdit_Click" CommandName="Add" SkinID="lblMand"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblbtnDel" runat="server" ToolTip="Delete"
                                                        CommandName="Delete" OnClientClick="return confirm_Save();" SkinID="lblMand"><i class="fa fa-times"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <center>
                            <div id="dvTbRCPA" style="width: 70%">
                                <table id="tbRCPA" class="tbRCPA" runat="server" visible="false">
                                    <thead>
                                        <tr style="border-bottom: 10px solid #fff;">
                                            <th align="center" class="tbRCPAth">
                                                <asp:Label ID="lblHProduct" runat="server" Text="Product Name" CssClass="chklabel"></asp:Label>
                                            </th>
                                            <th align="center" class="tbRCPAth">
                                                <asp:Label ID="lblHOurQty" runat="server" Text="Qty per Month" CssClass="chklabel"></asp:Label>
                                            </th>
                                            <th align="center" class="tbRCPAth">
                                                <asp:Label ID="lblHOurVal" runat="server" Text="Our Rate" CssClass="chklabel"></asp:Label>
                                            </th>
                                            <th align="center" class="tbRCPAth">
                                                <asp:Label ID="lblHTotal" runat="server" Text="Total" CssClass="chklabel"></asp:Label>
                                            </th>
                                            <th align="center" class="tbRCPAth">
                                                <asp:Label ID="lblHCompMap" runat="server" Text="Competitor" CssClass="chklabel"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td align="center" class="stylespc">
                                                <asp:DropDownList ID="ddlProduct" runat="server" SkinID="ddlRequired" data-live-search="true"
                                                    OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" class="stylespc">
                                                <asp:TextBox ID="txtQtyperMonth" runat="server" CssClass="textbox" MaxLength="5" Width="100" onpaste="return false"
                                                    onkeypress="return IsNumeric(event);" ondrop="return false;" onkeyup="FetchData(this)" rel=""></asp:TextBox>
                                            </td>
                                            <td align="center" class="stylespc">
                                                <asp:TextBox ID="txtOurVal" ForeColor="Black" runat="server" Enabled="false" Width="100" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td align="center" class="stylespc">
                                                <asp:TextBox ID="txtTotal" ForeColor="Black" runat="server" Enabled="false" Width="100" CssClass="textbox"
                                                    onkeyup="FetchData(this)" rel=""></asp:TextBox>
                                            </td>
                                            <td align="center" class="stylespc">
                                                <asp:LinkButton ID="lnkbtnCompMap" runat="server" CssClass="label" ForeColor="Blue" Font-Size="16px" Text="Map Competitor" OnClick="lnkbtnCompMap_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </center>
                        <center>
                            <div class="designation-reactivation-table-area clearfix" id="dvGridProduct">
                                <br />
                                <div class="display-table clearfix" id="dvProduct" runat="server" visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvCompetitor" runat="server" OnRowDataBound="gvCompetitor_RowDataBound" AlternatingRowStyle-CssClass="alt"
                                            AutoGenerateColumns="false" CssClass="table"
                                            GridLines="None" HorizontalAlign="Center" BorderWidth="0"
                                            Width="70%" ShowFooter="true" FooterStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Competitor Name" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCompName" runat="server" BackColor="#E3FDF8" Width="100"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCompName_SelectedIndexChanged" CssClass="selectpicker" data-live-search="true" />
                                                        <asp:HiddenField ID="hdnCompName" runat="server" Value='<%# Eval("CompCode") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Brand Name">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCompPName" runat="server" Width="100" BackColor="#E3FDF8" CssClass="selectpicker" data-live-search="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty per Month" HeaderStyle-Width="100">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCPQty" runat="server" CssClass="textbox" MaxLength="5" Width="75" onpaste="return false"
                                                            onkeyup="FetchData1(this)" rel="<%# Container.DataItemIndex  %>"
                                                            onkeypress="return IsNumeric(event);" ondrop="return false;"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Competitor Rate" HeaderStyle-Width="100">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCPRate" runat="server" CssClass="textbox" Width="75" onpaste="return false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Value" HeaderStyle-Width="100">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCPValue" runat="server" CssClass="textbox" Width="75" onpaste="return false"
                                                            onkeyup="FetchData1(this)" rel="<%# Container.DataItemIndex  %>" Enabled="false"
                                                            onkeypress="return IsNumeric(event);" ondrop="return false;"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:Button ID="btnSave" CssClass="savebutton" runat="server" CommandName="Save" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidateEmptyValue()" />
                                    <asp:Button ID="btnCancel" CssClass="savebutton" runat="server" Text="Clear" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </center>
                    </div>
                </div>
            </div>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlSFCode" />
            <asp:PostBackTrigger ControlID="ddlDocName" />
            <asp:PostBackTrigger ControlID="btnGo" />
            <asp:PostBackTrigger ControlID="ddlProduct" />
            <asp:PostBackTrigger ControlID="gvProductVal" />
            <asp:PostBackTrigger ControlID="lnkbtnCompMap" />
            <asp:PostBackTrigger ControlID="gvCompetitor" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
