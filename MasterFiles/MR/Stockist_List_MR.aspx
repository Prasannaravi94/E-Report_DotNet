<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_List_MR.aspx.cs"
    Inherits="MasterFiles_MR_Stockist_List_MR" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stockist List</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
    
    <style type="text/css">
        .web_dialog_overlay
        {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            background: #000000;
            opacity: .15;
            filter: alpha(opacity=15);
            -moz-opacity: .15;
            z-index: 101;
            display: none;
        }
        .web_dialog
        {
            display: none;
            position: fixed;
            width: 400px;
            min-height: 180px;
            max-height: auto;
            top: 50%;
            left: 50%;
            margin-left: -190px;
            margin-top: -100px;
            background-color: #ffffff;
            border: 2px solid #336699;
            padding: 0px;
            z-index: 102;
            font-family: Verdana;
            font-size: 10pt;
        }
        .web_dialog_title
        {
            border-bottom: solid 2px #336699;
            background-color: #336699;
            padding: 4px;
            color: White;
            font-weight: bold;
        }
        .web_dialog_title a
        {
            color: White;
            text-decoration: none;
        }
        .align_right
        {
            text-align: right;
        }
        
        .Formatrbtn label
        {
            margin-right: 30px;
        }
        
        
        /* hover style just for information */
        label:hover:before
        {
            border: 1px solid #4778d9 !important;
        }
        
        
        .btnReAct
        {
            display: inline-block;
            padding: 3px 9px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            margin-top: 25px;
        }
        
        .btnReActivation
        {
            color: #fff;
            background-color: #158263;
            border-color: #158263;
        }
        .btnReActivation:hover
        {
            color: #fff;
            background-color: #2b9a7b;
            border-color: #2b9a7b;
        }
        .btnReActivation:focus, .btnReActivation.focus
        {
            color: #fff;
            background-color: #2b9a7b;
            border-color: #2b9a7b;
        }
        .btnReActivation:active, .btnReActivation.active
        {
            color: #fff;
            background-color: #158263;
            border-color: #158263;
            background-image: none;
        }
        
        #btnClose_Plus:focus
        {
            outline-offset: -2px;
        }
        #btnClose_Plus:hover, #btnClose_Plus:focus
        {
            color: #fff;
            text-decoration: underline;
        }
        #btnClose_Plus:hover, #btnClose_Plus:focus
        {
            color: #fff;
            text-decoration: underline;
        }
        #btnClose_Plus:active, #btnClose_Plus:hover
        {
            outline: 0px none currentColor;
        }
    </style>
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
    <script type="text/javascript">

        $(document).ready(function () {
            $('#foobar #foo').change(function () {
                $('#foobar1 img.loading').show();

                $('#foobar1 #foo1').load('foo.html', function () {
                    $('#foobar1 img.loading').hide();

                });

            });
        });
    </script>
    <script type="text/javascript">

        function Search_Gridview(strKey, strGV) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById(strGV);
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

    </script>
    <script type="text/javascript">


        function showLoader(loaderType) {
            $("#TxtSrch").hide();
            $("#Btnsrc").hide();
            $("#ddlStockist").hide();

            if (loaderType == "Search") {
                document.getElementById("loaderSearch").style.display = '';
            }

        }
 
    </script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Btnsrc').click(function () {

                if ($("#TxtSrch").val() == "") {
                    createCustomAlert("Please Enter Stockist Name."); $('#TxtSrch').focus(); return false;
                }

                var divi = $('#<%=ddlSrch.ClientID%> :selected').text();
                var divi1 = $('#<%=ddlStockist.ClientID%> :selected').text();
                // if (divi1 == "---Select---") { createCustomAlert("Please Select " + divi + "."); $('#ddlStockist').focus(); return false; }

                if (divi == "State Name") {
                    if (divi1 == "---Select---") {
                        createCustomAlert("Please Select " + divi + ".");
                        $('#ddlStockist').focus();
                        return false;
                    }
                }
                if (divi == "HQ Name") {
                    if (divi1 == "---Select---") {
                        createCustomAlert("Please Select " + divi + ".");
                        $('#ddlStockist').focus();
                        return false;
                    }
                }
                else {
                    return true;
                }


            });
        });
    </script>
    <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>
    <script type="text/javascript">

        function Validate_1() {
            var gridView = $("table[id*=grdStockist]");
            var dropdownList = $("table[id*=grdStockist] select");

            var GvLength = $("table[id*=grdStockist] tr:not(:first)").length;

            selected = $("table[id*=grdStockist]  tr td select");


            if (GvLength > 0) {

                for (var i = 0; i < Len; i++) {

                    var ddlState = $(selected[i]).closest("td").find("select[id$='ddlState'] option:selected").text();
                    var ddlHQName = $(selected[i]).closest("td").find("select[id$='ddlHQ'] option:selected").text();


                    if (ddlState == "---Select---") {
                        createCustomAlert("Please Select State");
                        $(this).focus();
                        return false;
                    }
                    else if (ddlHQName == "--Select--") {
                        createCustomAlert("Please Select HQ Name");
                        $(this).focus();
                        return false;
                    }

                    else {

                    }

                }

            }

        }

    </script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript">


        function HQ_Validation() {

            var ShortName = $("#txtPool_Sname").val();
            var Name = $("#txtPool_Name").val();

            if (ShortName == "") {
                var txt = "Please Enter Short Name";
                createCustomAlert(txt);
                return false;
            }
            else if (Name == "") {
                var txt = "Please Enter Name";
                createCustomAlert(txt);
                return false;
                //createCustomAlert(txt);
            }
            else {
                return true;
            }
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            function blinker() {
                $('.blink_me').fadeOut(500);
                $('.blink_me').fadeIn(500);
            }

            setInterval(blinker, 1000);
        });
    </script>

    <script type="text/javascript">


        $(document).ready(function () {

            $("#btnAdd_Hq").click(function (e) {
                ShowDialog_Plus(false);
                e.preventDefault();
            });

            $("#btnClose_Plus").click(function (e) {
                HideDialog_Plus();
                e.preventDefault();
            });
        });

        function ShowDialog_Plus(modal) {
            $("#overlay_Plus").show();
            $("#dialog_Plus").fadeIn(300);

            if (modal) {
                $("#overlay_Plus").unbind("click");
            }
            else {
                $("#overlay_Plus").click(function (e) {
                    HideDialog_Plus();
                });
            }
        }

        function HideDialog_Plus() {
            $("#overlay_Plus").hide();
            $("#dialog_Plus").fadeOut(300);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<div id="Divid" runat="server">
        </div>--%>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <br />
        <table width="100%">
            <tr>
                <td style="width: 7.4%" />
                <%-- <td>
                    <asp:Button ID="btnNew" runat="server" Width="60px" Height="25px" CssClass="savebutton"
                        Text="Add" OnClick="btnNew_Click" />
                </td>--%>
                <td>
                    <div style="float: right">
                        <asp:Button ID="btnBack" runat="server" Width="60px" Height="25px" CssClass="savebutton"
                            Text="Back" OnClick="btnBack_Click" Visible="false" />
                    </div>
                </td>
            </tr>
        </table>
        <table width="50%">
            <tr>
                <td style="width: 15%">
                </td>
                <td>
                    <asp:Label ID="lblGiftType" runat="server" Text="Search By"></asp:Label>
                    <asp:DropDownList ID="ddlSrch" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                        TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged" onchange="showLoader('Search')">
                        <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Stockist Name" Value="2"></asp:ListItem>
                        <asp:ListItem Text="State Name" Value="3"></asp:ListItem>
                        <asp:ListItem Text="HQ Name" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                    <img src="../../Images/ajaxRoundLoader.gif" style="display: none;" id="loaderSearch" />
                    <%-- <img src="../Images/ajax_loadinf_3.gif"  style="display: none;" id="loaderSearch" />--%>
                    <asp:TextBox ID="TxtSrch" runat="server" SkinID="MandTxtBox" Width="150px" CssClass="TEXTAREA"
                        Visible="false" onfocus="this.style.backgroundColor='#E0EE9D'"></asp:TextBox>
                    <asp:DropDownList ID="ddlStockist" runat="server" SkinID="ddlRequired" TabIndex="4"
                        Visible="false" onfocus="this.style.backgroundColor='#E0EE9D'">
                    </asp:DropDownList>
                    <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="30px" Height="25px"
                        Text="Go" Visible="false" OnClick="Btnsrc_Click" />
                </td>
            </tr>
        </table>
        <div>
            <div style="margin-top: 2px; float: right; margin-right: 22%">
                <div style="width: 20%; height: 60px; float: left">
                    <div>
                        <div style="width: 100px">
                            <a href="#" id="btnAdd_Hq" class="blink_me" style="color: red; font-weight: bold;
                                font-family: @NSimSun; font-size: 14px" shape="circle" runat="server">Add HQ</a>
                        </div>
                        <br />
                        <br />
                        <div id="output_Plus">
                        </div>
                        <div id="overlay_Plus" class="web_dialog_overlay">
                        </div>
                        <div id="dialog_Plus" class="web_dialog">
                            <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                                <tr>
                                    <td class="web_dialog_title">
                                        Stockist - HQ Creation
                                    </td>
                                    <td class="web_dialog_title align_right">
                                        <a href="#" id="btnClose_Plus">Close</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        <center>
                                            <table border="0" cellpadding="3" cellspacing="3" id="tblDivisionDtls" align="center">
                                                <tr>
                                                    <td align="left" class="stylespc">
                                                        <asp:Label ID="lblShortName" runat="server" SkinID="lblMand" Height="19px" Width="120px">
                                                                <span style="color:Red">*</span>Short Name</asp:Label>
                                                    </td>
                                                    <td align="left" class="stylespc">
                                                        <asp:TextBox ID="txtPool_Sname" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                            onblur="this.style.backgroundColor='White'" TabIndex="1" runat="server" MaxLength="10"
                                                            onkeypress="CharactersOnly(event);">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="stylespc">
                                                        <asp:Label ID="lblPoolName" runat="server" SkinID="lblMand" Height="19px" Width="120px">
                                                                <span style="color:Red">*</span>Name</asp:Label>
                                                    </td>
                                                    <td align="left" class="stylespc">
                                                        <asp:TextBox ID="txtPool_Name" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                                                            MaxLength="120" onkeypress="CharactersOnly(event);">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </center>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:Button ID="btnHq" runat="server" Text="Save" CssClass="btnReAct btnReActivation"
                                            OnClick="btnHq_Click" OnClientClick="if(!HQ_Validation()) return false;" />
                                        <%--<asp:Button ID="btnActive_Plus" runat="server" Text="Activate" OnClick="btnActive_Plus_Click"
                                            CssClass="btn btnReActivation" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" style="width: 50%" align="center">
                        <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                            runat="server" HorizontalAlign="Center">
                            <SeparatorTemplate>
                            </SeparatorTemplate>
                            <ItemTemplate>
                                &nbsp
                                <asp:LinkButton ID="lnkbtnAlpha" runat="server" Font-Names="Calibri" Font-Size="14px"
                                    ForeColor="#8A2EE6" CommandArgument='<%#bind("stockist_name") %>' Text='<%#bind("stockist_name") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdStockist" runat="server" Width="85%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" OnRowUpdating="grdStockist_RowUpdating" OnRowDataBound="grdStockist_RowDataBound"
                            OnRowEditing="grdStockist_RowEditing" OnRowCreated="grdStockist_RowCreated" OnRowCancelingEdit="grdStockist_RowCancelingEdit"
                            OnRowCommand="grdStockist_RowCommand" OnPageIndexChanging="grdStockist_PageIndexChanging"
                            GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowPaging="True" PageSize="10" AllowSorting="True" OnSorting="grdStockist_Sorting"
                            ShowFooter="true">
                            <HeaderStyle Font-Bold="false" />
                            <PagerStyle CssClass="gridview1"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdStockist.PageIndex * grdStockist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stockist_Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStockist_Code" runat="server" Text='<%# Eval("Stockist_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Stockist_Name" HeaderText="Stockist Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-ForeColor="white">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtStockist_Name" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150"
                                            Text='<%# Bind("Stockist_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStockist_Name" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtStockist_Name_Foot" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <EditItemTemplate>
                                        <%-- <asp:TextBox ID="txtState" runat="server" SkinID="TxtBxNumOnly" MaxLength="10" Text='<%# Bind("State") %>'></asp:TextBox>--%>
                                        <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>' Visible="false" />
                                        <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="revDdlState" runat="server" ControlToValidate="ddlState"
                                            InitialValue="0" ErrorMessage="Please Select State"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlState_Foot" runat="server" SkinID="ddlRequired">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HQ Name" Visible="true">
                                    <EditItemTemplate>
                                        <%--  <asp:TextBox ID="txtTerritory" runat="server" SkinID="TxtBxNumOnly" MaxLength="10"
                                            Text='<%# Bind("Territory") %>'></asp:TextBox>--%>
                                        <asp:Label ID="lblHQName" runat="server" Text='<%# Bind("Territory") %>' Visible="false" />
                                        <asp:DropDownList ID="ddlHQ" runat="server" SkinID="ddlRequired">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="revDdlMatch" runat="server" ControlToValidate="ddlHQ"
                                            InitialValue="0" ErrorMessage="Please Select HQ Name"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlHQ_Foot" runat="server" SkinID="ddlRequired">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtStockist_ContactPerson" runat="server" SkinID="TxtBxAllowSymb"
                                            MaxLength="150" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStockist_ContactPerson" runat="server" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtStockist_ContactPerson_Foot" runat="server" SkinID="TxtBxAllowSymb"
                                            MaxLength="150"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtStockist_Mobile" runat="server" SkinID="TxtBxNumOnly" MaxLength="10"
                                            Text='<%# Bind("Stockist_Mobile") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStockist_Mobile" runat="server" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtStockist_Mobile_Foot" runat="server" SkinID="TxtBxNumOnly" MaxLength="10"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                    HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:CommandField>
                                <%--   <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="Stockist_MR_Creation.aspx?stockist_code={0}"
                                    DataNavigateUrlFields="stockist_code">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>--%>
                                <asp:TemplateField HeaderText="Deactivate">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Stockist_Code") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Stockist');">Deactivate
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ADD">
                                    <FooterTemplate>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" CommandName="Insert" Width="60px"
                                            Height="25px" CssClass="savebutton" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <%--  <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />--%>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
