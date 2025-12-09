<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="DoctorProductBusinessEntry.aspx.cs" EnableViewState="true" Inherits="DoctorBusinessEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <link rel="shortcut icon" type="image/png" href="../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />
    <link rel="stylesheet" href="../assets/css/responsive.css" />
    <script type="text/javascript">
        function confirm_Save() {
            if (confirm('Do you want to delete the Doctor?')) {
                if (confirm('Are you sure?')) {
                    ShowProgress();
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Tab
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }

        var sum = 0;
        function FetchData(button) {
            var cell = button.parentNode;
            var row = button.parentNode.parentNode;
            var Roow = cell.getAttribute("rel");
            var Roow1 = button.getAttribute("rel");
            var label = GetChildControl(row, "lblPrice").innerHTML;
            var label1 = GetChildControl(row, "txtQty").value;
            var label2 = GetChildControl(row, "txtValue").value;

            if (label2 == "") {
                var Multi = parseFloat(label) * parseFloat(label1);

                GetChildControl(row, "txtValue").value = Multi;

                if (!isNaN(Multi) && Multi.length != 0) {
                    sum += parseFloat(Multi);

                }
            }
            else {

                if (!isNaN(label2) && label2.length != 0) {
                    sum -= parseFloat(label2);
                }
                var Multi = parseFloat(label) * parseFloat(label1);

                GetChildControl(row, "txtValue").value = Multi;
                if (!isNaN(Multi) && Multi.length != 0) {
                    sum += parseFloat(Multi);
                }
            }

            var col1;
            var totalcol1 = 0;
            var grid = document.getElementById('<%=gvProduct.ClientID %>');

            for (i = 0; i < grid.rows.length; i++) {
                col1 = grid.rows[i].cells[5];

                for (j = 0; j < col1.childNodes.length; j++) {
                    if (col1.childNodes[j].type == "text") {
                        if (!isNaN(col1.childNodes[j].value) && col1.childNodes[j].value != "") {
                            totalcol1 += parseInt(col1.childNodes[j].value)
                        }
                    }
                }
            }
            document.getElementById('<%= txtTotalVal.ClientID %>').value = totalcol1.toString();

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

        function multiplication() {

        }

    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            try {
                $(".divgrid").each(function () {
                    var grid = $(this).find("table")[0];
                    var ScrollHeight = $(this).height();
                    var gridWidth = $(this).width() - 20;
                    var headerCellWidths = new Array();
                    for (var i = 0; i < grid.getElementsByTagName('TH').length; i++) {
                        headerCellWidths[i] = grid.getElementsByTagName('TH')[i].offsetWidth;
                    }
                    grid.parentNode.appendChild(document.createElement('div'));
                    var parentDiv = grid.parentNode; var table = document.createElement('table');
                    for (i = 0; i < grid.attributes.length; i++) {
                        if (grid.attributes[i].specified && grid.attributes[i].name != 'id') {
                            table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                        }
                    }
                    table.style.cssText = grid.style.cssText;
                    table.style.width = gridWidth + 'px';
                    table.appendChild(document.createElement('tbody'));
                    table.getElementsByTagName('tbody')[0].appendChild(grid.getElementsByTagName('TR')[0]);
                    var cells = table.getElementsByTagName('TH');
                    var gridRow = grid.getElementsByTagName('TR')[0];
                    for (var i = 0; i < cells.length; i++) {
                        var width; if (headerCellWidths[i] > gridRow.getElementsByTagName('TD')[i].offsetWidth) {
                            width = headerCellWidths[i];
                        } else {
                            width = gridRow.getElementsByTagName('TD')[i].offsetWidth;
                        } cells[i].style.width = parseInt(width - 3) + 'px';
                        gridRow.getElementsByTagName('TD')[i].style.width = parseInt(width - 3) + 'px';
                    }
                    var gridHeight = grid.offsetHeight;
                    if (gridHeight < ScrollHeight)
                        ScrollHeight = gridHeight;
                    parentDiv.removeChild(grid);
                    var dummyHeader = document.createElement('div');
                    dummyHeader.appendChild(table); parentDiv.appendChild(dummyHeader);
                    var scrollableDiv = document.createElement('div');
                    if (parseInt(gridHeight) > ScrollHeight) {
                        gridWidth = parseInt(gridWidth) + 17;
                    }
                    scrollableDiv.style.cssText = 'overflow:auto;height:' + ScrollHeight + 'px;width:' + gridWidth + 'px';
                    scrollableDiv.appendChild(grid);
                    parentDiv.appendChild(scrollableDiv);

                    //fixed footer
                    var dummyFooter = document.createElement('div');
                    dummyFooter.innerHTML = dummyHeader.innerHTML;
                    var footertr = grid.rows[grid.rows.length - 1];
                    grid.deleteRow(grid.rows.length - 1);
                    gridHeight = grid.offsetHeight;
                    if (gridHeight < ScrollHeight)
                        ScrollHeight = gridHeight;
                    scrollableDiv.style.height = ScrollHeight + 'px';
                    dummyFooter.getElementsByTagName('Table')[0].deleteRow(0);
                    dummyFooter.getElementsByTagName('Table')[0].appendChild(footertr);
                    gridRow = dummyHeader.getElementsByTagName('Table')[0].getElementsByTagName('TR')[0];
                    for (var i = 0; i < footertr.cells.length; i++) {
                        var width;
                        if (headerCellWidths[i] > gridRow.getElementsByTagName('TH')[i].offsetWidth) {
                            width = headerCellWidths[i];
                        }
                        else {
                            width = gridRow.getElementsByTagName('TH')[i].offsetWidth;
                        }
                        footertr.cells[i].style.width = parseInt(width - 3) + 'px';
                    }
                    parentDiv.appendChild(dummyFooter);
                });
            }
            catch (err) { }
        }
    );
    </script>
    <style type="text/css">
        .divgrid {
            height: 315px;
            width: 700px;
        }

            .divgrid table {
                width: 350px;
            }

                .divgrid table th {
                    background-color: #666699;
                    color: #fff;
                }

        .dtxt {
            border: 1px solid #ffffFF;
            border-radius: 4px;
            margin: 2px;
            width: 70px;
            text-align: center;
            background-image: url('css/download%20(2).png');
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

        .tbListedDr {
            width: 100%;
            background-color: white;
        }

        /*.tbListedDr th span {
                color: white !important;
            }*/

        body {
            font-size: 62.5%;
            background: none !important;
            background-color: #fafdff !important;
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
            border-radius: 8px !important;
            border: 1px solid #d1e2ea !important;
            background-color: #f4f8fa !important;
            color: #90a1ac !important;
            font-size: 14px !important;
            padding-right: 10px !important;
            height: 30px !important;
            text-align: right !important;
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

        .display-table .table tr:nth-child(2) td:first-child {
            color: #636d73 !important;
        }
    </style>
    <style type="text/css">
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        .remove {
            text-decoration: none;
        }

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


        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .footer td {
            border: none;
        }

        .footer th {
            border: none;
        }

        .tbListedDr {
            width: 100%;
            background-color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="Divid" runat="server"></div>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });

        $(function () {
            var $txt = $('input[id$=txtNew1]');
            var $ddl = $('select[id$=ddlListedDr]');
            var $items = $('select[id$=ddlListedDr] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>
    <script type="text/javascript" src="http://malsup.github.io/jquery.blockUI.js"></script>
    <script type="text/javascript">
        function showLoader(loaderType) {
            if (loaderType == "Search1") {
                document.getElementById("loaderSearchddlSFCode").style.display = '';
            }
            if (loaderType == "Search2") {
                $('#dvTbListedDr').block({
                    message: '<h1>Please Wait...</h1>',
                    css: {
                        border: '3px solid #a00',
                        padding: '10px',
                        fontWeight: 'bold'
                    }
                });
            }
        }
        $(document).ready(function () {
            $('#<%=btnGo.ClientID%>').click(function () {
                var st1 = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (st1 == "---Select---") { alert("Select Field Force"); $('ddlFieldForce').focus(); return false; }
                <%--var st = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (st == "--Select--") { alert("Select Month"); $('ddlMonth').focus(); return false; }--%>
            });

            $('#<%=lblbtnProduct.ClientID%>').click(function () {
                var st2 = $('#<%=ddlListedDr.ClientID%> :selected').text();
                if (st2 == "---Select---") { alert("Select ListedDr"); $('ddlListedDr').focus(); return false; }
                if (st2 == "") { alert("Select ListedDr"); $('ddlListedDr').focus(); return false; }

                $('#dvGridProduct').block({
                    message: '<h1>Please Wait...</h1>',
                    css: {
                        border: '3px solid #a00',
                        padding: '10px',
                        fontWeight: 'bold'
                    }
                });
            });

            <%--$("#<%=testImg.ClientID%>").hide();
            $('#<%=linkcheck.ClientID%>').click(function () {
                window.setTimeout(function () {
                    $("#<%=testImg.ClientID%>").show();
                }, 500);
            });--%>
        });

        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1 || (keyCode == 46) && ($(this).indexOf('.') != -1));
            return ret;
        }
    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
        });
    </script>
    <div class="container home-section-main-body position-relative clearfix">
        <div class="row justify-content-center">
            <div class="col-lg-5">
                <center>
                    <table>
                        <tr>
                            <td align="center">
                                <h2 class="text-center">Doctor Business Productwise - Entry</h2>
                            </td>
                        </tr>
                    </table>
                </center>
                <div class="designation-area clearfix">
                    <div class="single-des clearfix">
                        <asp:HiddenField ID="hdnBasedOn" runat="server" />
                        <asp:Label ID="lblFieldForceName" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%"
                            OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="single-des clearfix">
                        <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                        <input id="txtMonthYear" type="text" runat="server" class="nice-select" readonly="true"></input>
                        <%--  <asp:Label ID="lblMonth" runat="server" Text="Month " CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                        </asp:DropDownList>--%>
                    </div>
                    <%--     <div class="single-des clearfix">
                        <asp:Label ID="lblYear" runat="server" Text="Year " CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </div>--%>
                </div>
                <div class="w-100 designation-submit-button text-center clearfix">
                    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" OnClick="btnGo_Click" />
                    <asp:Button ID="btnClear" CssClass="savebutton" runat="server" Text="Clear" OnClick="btnClear_Click" />
                </div>
            </div>
            <div class="col-lg-12">
                <div class="designation-reactivation-table-area clearfix">
                    <br />
                    <div class="display-table clearfix">
                        <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                            <asp:GridView ID="gvListedDr" runat="server" AlternatingRowStyle-CssClass="alt"
                                AutoGenerateColumns="false" CssClass="table" GridLines="None" HorizontalAlign="Center" BorderWidth="0"
                                Width="100%" ShowFooter="true" FooterStyle-HorizontalAlign="Center" Visible="false" OnRowDeleting="gvListedDr_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDrName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnListedDr_Code" runat="server" Value='<%#Eval("ListedDrCode")%>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specialty" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpec" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCat" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Sub Area" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblterr" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            <asp:HiddenField ID="hdnTrtyCode" runat="server" Value='<%# Bind("Territory_Code") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDrValue" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblbtnEdit" runat="server" ToolTip="Edit"
                                                OnClick="lblbtnEdit_Click" CommandName="Add"
                                                OnClientClick="return ValidateEmptyValue()" SkinID="lblMand"><i class="fa fa-pencil"></i></asp:LinkButton>
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
                <br />
            </div>
            <div class="col-lg-12">
                <center>
                    <div id="dvTbListedDr" style="width: 100%">
                        <table id="tbListedDr" class="tbListedDr" runat="server" visible="false">
                            <thead>
                                <tr style="border-bottom: 10px solid #fff;">
                                    <th align="center" class="tbRCPAth" style="width: 30%;">
                                        <asp:Label ID="lblListedDr" CssClass="chklabel" runat="server" Text="Listed Doctor Name"></asp:Label>
                                    </th>
                                    <th align="center" class="tbRCPAth">
                                        <asp:Label ID="lblSpecl" CssClass="chklabel" runat="server" Text="Specialty"></asp:Label>
                                    </th>
                                    <th align="center" class="tbRCPAth">
                                        <asp:Label ID="lblCatgry" CssClass="chklabel" runat="server" Text="Category"></asp:Label>
                                    </th>
                                    <th align="center" class="tbRCPAth">
                                        <asp:Label ID="lblterritory" CssClass="chklabel" runat="server" Text="Sub Area"></asp:Label>
                                    </th>
                                    <th align="center" class="tbRCPAth">
                                        <asp:Label ID="lblProduct" CssClass="chklabel" runat="server" Text="Product"></asp:Label>
                                    </th>
                                </tr>
                            </thead>
                            <tr>
                                <td align="center" class="stylespc">
                                    <asp:DropDownList ID="ddlListedDr" runat="server" SkinID="ddlRequired"
                                        OnSelectedIndexChanged="ddlListedDr_SelectedIndexChanged" AutoPostBack="true"
                                        Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" class="stylespc">
                                    <asp:Label ID="lblSpecName" runat="server" Text="-" CssClass="chklabel"></asp:Label>
                                </td>
                                <td align="center" class="stylespc">
                                    <asp:Label ID="lblCatName" runat="server" Text="-" CssClass="chklabel"></asp:Label>
                                </td>
                                <td align="center" class="stylespc">
                                    <asp:Label ID="lblTrName" runat="server" Text="-" CssClass="chklabel"></asp:Label>
                                    <asp:HiddenField ID="hdnTrCode" runat="server" />
                                </td>
                                <td align="center" class="stylespc">
                                    <asp:LinkButton ID="lblbtnProduct" runat="server" CssClass="label" ForeColor="Blue" Font-Size="16px" Text="Fill Product" OnClick="lblbtnProduct_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </center>
                <div class="designation-reactivation-table-area clearfix">
                    <div class="display-table clearfix" runat="server" id="dvProduct" visible="false">
                        <br />
                        <center>
                            <table align="right" style="margin-right: 0px">
                                <tr>
                                    <td style="padding-right: 10px !important;">
                                        <asp:Label ID="lblTotal" runat="server" CssClass="label" Font-Bold="true" Font-Size="14px" Text="Grand Total : " />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalVal" runat="server" Font-Bold="true" Font-Size="Large" ClientIDMode="Static"
                                            CssClass="textbox" Width="110px" Enabled="false"
                                            Text="0" ForeColor="Red" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                            <br />
                            <center>
                                <div style="max-height:500px;overflow-y:scroll;width:100%;overflow-x: hidden;">
                                <asp:GridView ID="gvProduct" runat="server" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="false" CssClass="table"
                                    GridLines="None" HorizontalAlign="Center" BorderWidth="0" Height="50%" 
                                    Width="100%" ShowFooter="true" FooterStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSlNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("Product_Detail_Name") %>' />
                                                <asp:HiddenField ID="hdnProductCode" runat="server" Value='<%# Eval("Product_Detail_Code") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pack" HeaderStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprd_sale" runat="server" Text='<%#Eval("Product_Sale_Unit")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQty" runat="server" CssClass="textbox" Width="75px" MaxLength="5" onpaste="return false"
                                                    onkeyup="FetchData(this)" rel="<%# Container.DataItemIndex  %>"
                                                    onkeypress="return IsNumeric(event);" ondrop="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" Width="75" CssClass="label" runat="server" Text='<%# Eval("Retailor_Price") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value" HeaderStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" Width="75" AutoPostBack="true"
                                                    onkeyup="FetchData(this)" rel="<%# Container.DataItemIndex %>"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
                                      </div>
                            </center>
                          
                        </div>
                        <center>
                            <asp:Button ID="btnSave" CssClass="savebutton" runat="server" CommandName="Save" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidateEmptyValue()" />
                            <asp:Button ID="btnCancel" CssClass="savebutton" runat="server" Text="Clear" OnClick="btnCancel_Click" />
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

                <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=txtMonthYear]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "M-yyyy",
                    viewMode: "months",
                    minViewMode: "months",
                    language: "tr"
                });
            });
        </script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $('.custom-select2').hide();
        });
    </script>
</asp:Content>
