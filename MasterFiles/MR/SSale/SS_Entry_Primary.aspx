<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SS_Entry_Primary.aspx.cs"
    Inherits="MasterFiles_MR_SSale_SS_Entry_Primary" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Sale Entry</title>

    <link type="text/css" href="../../../css/Report.css" rel="Stylesheet" />
    <%--<link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />--%>

    <style type="text/css">
        .padding {
            padding: 3px;
        }

        .chkboxLocation label {
            padding-left: 5px;
        }

        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
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
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .ddl1 {
            border: 1px solid #1E90FF;
            border-radius: 5px;
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

        .divlabel {
            width: 45%;
            float: right;
            position: relative;
            padding-left: 2.15em;
            cursor: pointer;
            vertical-align: top;
            line-height: 20px;
            margin: 2px 0;
            display: block;
            font-size: 14px;
        }

        .divpadding {
            padding-right: 10px;
        }

        .calendarcontrol {
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            color: #90a1ac;
            font-size: 14px;
            padding-left: 10px;
            height: 35px;
        }

        #grdSale [type="checkbox"]:not(:checked), #grdSale [type="checkbox"]:checked {
            position: inherit;
        }

        #pblNObills [type="checkbox"]:not(:checked), #pblNObills [type="checkbox"]:checked {
            position: inherit;
        }
    </style>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .aclass {
            border: 1px solid lighgray;
        }

        .aclass {
            width: 50%;
        }

            .aclass tr td {
                background: White;
                font-weight: bold;
                color: Black;
                border: 1px solid black;
                border-collapse: collapse;
            }

            .aclass th {
                border: 1px solid black;
                border-collapse: collapse;
                background: LightBlue;
            }

        .lbl {
            color: Red;
        }


        .space {
            padding: 3px 3px;
        }

        .sp {
            padding-left: 11px;
        }

        .style6 {
            padding: 3px 3px;
            height: 28px;
        }

        .marRight {
            margin-right: 35px;
        }

        .boxshadow {
            -moz-box-shadow: 3px 3px 5px #535353;
            -webkit-box-shadow: 3px 3px 5px #535353;
            box-shadow: 3px 3px 5px #535353;
        }

        .roundbox {
            -moz-border-radius: 6px 6px 6px 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px 6px 6px 6px;
        }

        .grd {
            border: 1;
            border-color: Black;
        }

        .roundbox-top {
            -moz-border-radius: 6px 6px 0 0;
            -webkit-border-radius: 6px 6px 0 0;
            border-radius: 6px 6px 0 0;
        }

        .roundbox-bottom {
            -moz-border-radius: 0 0 6px 6px;
            -webkit-border-radius: 0 0 6px 6px;
            border-radius: 0 0 6px 6px;
        }

        .gridheader, .gridheaderbig, .gridheaderleft, .gridheaderright {
            padding: 6px 6px 6px 6px;
            background: #003399 url(images/vertgradient.png) repeat-x;
            text-align: center;
            font-weight: bold;
            text-decoration: none;
            color: khaki;
        }

        .gridheaderleft {
            text-align: left;
        }

        .gridheaderright {
            text-align: right;
        }

        .gridheaderbig {
            font-size: 135%;
        }

        .gridtable {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }

            .gridtable th {
                border-width: 1px;
                border-style: solid;
                border-color: #666666;
                font-size: large;
                color: Red;
            }

            .gridtable td {
                border-color: #666666;
                background-color: #ffffff;
            }
    </style>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
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
            $('#btnGo').click(function () {
                var HQ = $('#<%=ddlHQ.ClientID%> :selected').text();
                if (HQ == "---Select---") { alert("Select HQ Name."); $('#ddlHQ').focus(); return false; }
                var StokName = $('#<%=ddlStockiest.ClientID%> :selected').text();
                if (StokName == "---Select---") { alert("Select Stockist Name."); $('#ddlStockiest').focus(); return false; }
                var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                if (Year == "--Select--") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "--Select--") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
            });
            $('#btnSubmit').click(function () {
                if (!CheckBoxSelectionValidation()) {
                    return false;
                }
            });
        });
    </script>
    <%-- <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {

                var sfcode = $("#hdnSfcode").val();
                var stk_code = document.getElementById('<%=ddlStockiest.ClientID%>').value;
                var FYear = document.getElementById('<%=ddlYear.ClientID%>').value;
                var FMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;
                //   window.location = 'Sec_Sale_Entry?username=' + stk_code
                //  window.location = "Sec_Sale_Entry.aspx?sfcode=" + sfcode + "&stk_code=" + stk_code + "&FMonth=" + FYear + "&Fyear=" + FMonth;
                window.location.replace("Sec_Sale_Entry.aspx?sfcode=" + sfcode + "&stk_code=" + stk_code + "&FMonth=" + FMonth + "&Fyear=" + FYear);
                
               
            });
        });
    </script>--%>
    <script type="text/javascript">
        function checkRec() {
            var grid = document.getElementById('<%= grdSale.ClientID %>');

            if (grid != null) {

                var inputList = grid.getElementsByTagName("input");

                var cnt = 0;
                var index = '';

                for (i = 2; i <= inputList.length; i++) {

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {

                        index = i.toString();
                    }

                    var chkR = document.getElementById('grdSale_ctl' + index + '_chkReceived');
                    var chkT = document.getElementById('grdSale_ctl' + index + '_chkTransit');

                    if (chkR.checked) {
                        chkT.checked = false;

                    }

                }
            }
        }
        function checkTran() {
            var grid = document.getElementById('<%= grdSale.ClientID %>');

            if (grid != null) {


                var inputList = grid.getElementsByTagName("input");

                var cnt = 0;
                var index = '';

                for (i = 2; i <= inputList.length; i++) {

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {

                        index = i.toString();
                    }

                    var chkR = document.getElementById('grdSale_ctl' + index + '_chkReceived');
                    var chkT = document.getElementById('grdSale_ctl' + index + '_chkTransit');
                    if (chkT.checked) {
                        chkR.checked = false;
                    }

                }
            }
        }
    </script>
    <script type="text/javascript">
        function CheckBoxSelectionValidation() {
            <%--var gridView = document.getElementById("<%= grdSale.ClientID %>");

            for (var i = 1; i < gridView.rows.length; i++) {
                var count = 0;
                var chkConfirm = gridView.rows[i].cells[3].getElementsByTagName('input')[0];
             
                var chkConfirm2 = gridView.rows[i].cells[4].getElementsByTagName('input')[0];

                if (chkConfirm.checked || chkConfirm2.checked) {
                    count = 1;
                }
                if (count == 0) {
                    alert("Check at least One CheckBox from each Bill No");
                    return false;
                }
                else {
                    return true;
                }
            }--%>

            var grid = document.getElementById('<%= grdSale.ClientID %>');

            if (grid != null) {


                var inputList = grid.getElementsByTagName("input");

                var cnt = 0;
                var index = '';

                for (i = 2; i <= inputList.length; i++) {
                    var count = 0;
                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {

                        index = i.toString();
                    }

                    var chkR = document.getElementById('grdSale_ctl' + index + '_chkReceived');
                    var chkT = document.getElementById('grdSale_ctl' + index + '_chkTransit');

                    if (chkR.checked || chkT.checked) {
                        count = 1;

                    }
                    if (count == 0) {
                        alert("Check at least One CheckBox from each Bill No");
                        return false;
                    }


                }
            }
            return true;
        }
    </script>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>

        <br />
        <div>
            <asp:HiddenField ID="hdnSfcode" runat="server" />
            <ucl:Menu ID="Menu2" runat="server" />
            <link href="../../../assets/css/style.css" rel="stylesheet" />
            <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <center>
                            <table>
                                <tr>
                                    <td align="center">
                                        <h2 class="text-center">Secondary Sale Entry</h2>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="Label3" runat="server" CssClass="label">
                            HQ</asp:Label>

                                <asp:DropDownList ID="ddlHQ" runat="server" CssClass="custom-select2 nice-select" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlHQ_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <div style="float: left; width: 80%">
                                    <asp:Label ID="lblStockiest" runat="server" CssClass="label">
                            Stockiest</asp:Label>

                                    <asp:DropDownList ID="ddlStockiest" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: right; width: 15%; margin-top: 20px">
                                    <asp:Button ID="btnGo2" runat="server" Text="Go" CssClass="savebutton" Width="50px" OnClick="btnGo2_Click" />
                                </div>

                            </div>
                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblMonth" runat="server" CssClass="label">
                      Month</asp:Label>

                                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired" Enabled="false">
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
                                    </asp:DropDownList>
                                </div>
                                <div style="float: right; width: 45%;">
                                    <asp:Label ID="lblYear" runat="server" CssClass="label">
                       Year</asp:Label>

                                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" Enabled="false">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnGo" runat="server" Text="Go" Width="50px" CssClass="savebutton" OnClick="btnGo_Click" />
                                &nbsp;&nbsp;
                  <asp:Button ID="btnClear" runat="server" Text="Clear" Width="50px"
                      CssClass="savebutton" OnClick="btnClear_Click" />
                                &nbsp;&nbsp;
                                 <br />
                                <center>
                                    <asp:Panel ID="pblNObills" runat="server" Visible="false">

                                        <asp:CheckBox ID="chkbill" runat="server" Text="" AutoPostBack="true"
                                            OnCheckedChanged="chkbill_CheckedChanged" />
                                        &nbsp;&nbsp;
              <asp:Label ID="Label2" runat="server" Style="color: Red; font-size: 22px; font-weight: bold">No Bills</asp:Label>
                                    </asp:Panel>
                                </center>
                            </div>
                        </div>
                        <br />
                        <center>
                            <asp:Panel ID="pnlApp" runat="server" Visible="false">

                                <asp:Label ID="lblApp" runat="server" Style="color: Red; font-size: 22px; font-weight: bold"></asp:Label>

                            </asp:Panel>
                        </center>
                    </div>
                    <br />
                    <br />
                    <center>
                        <asp:Panel ID="pnlReject" runat="server" Visible="false">
                            <asp:Label ID="lblrej" runat="server" Style="color: blueviolet; font-size: 15px; font-weight: bold"></asp:Label>
                        </asp:Panel>
                    </center>
                    <br />
                    <center>
                        <asp:Panel ID="pnlprimary" runat="server" Visible="false">
                            <div class="roundbox boxshadow" style="width: 100%; border: solid 2px steelblue;">
                                <div class="gridheaderleft" style="text-align: center">
                                    Primary Bill Details
                                </div>
                                <div class="boxcontenttext" style="background: White;">
                                    <div id="pnlPreviewSurveyData">
                                        <br />
                                        <table id="gridtable" border="1" class="gridtable" cellspacing="0" cellpadding="8"
                                            width="80%">
                                            <tr>
                                                <th style="color: Red">Sale
                                                </th>
                                                <th style="color: Red">Return
                                                </th>
                                            </tr>
                                            <tr>
                                                <td style="border-style: solid; border-width: 1px" align="center" width="45%">
                                                    <table width="45%" align="center" style="border-width: 1px;">
                                                        <tr>
                                                            <td>
                                                                <br />
                                                                <div style="background-color: Green; height: 30px; width: 500px; margin: 0; padding: 0">
                                                                    <table cellspacing="0" cellpadding="0" rules="all" border="1" id="tblHeader" style="font-family: Arial; font-size: 10pt; width: 500px; border-collapse: collapse; color: White; height: 100%; border-style: solid; border-width: 1px; border-color: Black">
                                                                        <tr>
                                                                            <%--  <td style="width: 100px; text-align: center; background-color: #336699;">
                                                                    Bill No
                                                                </td>--%>
                                                                            <td style="width: 100px; text-align: center; background-color: #336699;">Bill Date
                                                                            </td>
                                                                            <td style="width: 100px; text-align: center; background-color: #336699;">Inv No
                                                                            </td>
                                                                            <td style="width: 100px; text-align: center; background-color: #336699;">Value
                                                                            </td>
                                                                            <td style="width: 100px; text-align: center; background-color: #336699;">Received
                                                                            </td>
                                                                            <td style="width: 100px; text-align: center; background-color: #336699;">Transit
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div style="height: 150px; width: 500px; overflow: auto;">
                                                                    <asp:GridView ID="grdSale" runat="server" AutoGenerateColumns="false" CssClass="mGridImg"
                                                                        Width="100%" EmptyDataText="No Records Found" GridLines="None" Height="100%"
                                                                        HorizontalAlign="Center" ShowHeader="false" RowStyle-Height="20px">
                                                                        <HeaderStyle Font-Bold="False" />
                                                                        <RowStyle Wrap="false" />
                                                                        <SelectedRowStyle BackColor="BurlyWood" />
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                        <Columns>
                                                                            <%-- <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Bill No" ItemStyle-Width="100px"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBill" runat="server" ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Bill Date" ItemStyle-Width="100px"
                                                                                ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldate" runat="server" Text='<%#Eval("Inv_Date")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Invoice No" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
                                                                                ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblinvoice" runat="server" Text='<%#Eval("Inv_No")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Value" ItemStyle-Width="100px"
                                                                                ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblvalue" runat="server" Text='<%#Eval("Svalue")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Received" ItemStyle-Width="100px"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkReceived" onclick="checkRec(this); " runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Transit" ItemStyle-Width="100px"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkTransit" runat="server" onclick="checkTran(this);" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                                            BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                                            VerticalAlign="Middle" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="border-style: solid; border-width: 1px" align="center" width="45%">
                                                    <table width="35%" align="center" style="border-width: 1px;">
                                                        <tr>
                                                            <td>
                                                                <br />
                                                                <div style="background-color: Green; height: 30px; width: 400px; margin: 0; padding: 0">
                                                                    <table cellspacing="0" cellpadding="0" rules="all" border="1" id="Table1" style="font-family: Arial; font-size: 10pt; width: 400px; border-collapse: collapse; color: White; height: 100%; border-style: solid; border-width: 1px; border-color: Black">
                                                                        <tr>
                                                                            <%--  <td style="width: 100px; text-align: center; background-color: #336699;">
                                                                    Bill No
                                                                </td>--%>
                                                                            <td style="width: 100px; text-align: center; background-color: #336699;">Bill Date
                                                                            </td>
                                                                            <td style="width: 100px; text-align: center; background-color: #336699;">Value
                                                                            </td>
                                                                            <td style="width: 100px; text-align: center; background-color: #336699;">Received
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div style="height: 150px; width: 400px; overflow: auto;">
                                                                    <asp:GridView ID="grdRet" runat="server" Height="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                                        EmptyDataText="No Records Found" RowStyle-Height="20px" GridLines="None" CssClass="mGridImg"
                                                                        ShowHeader="false" Width="100%">
                                                                        <HeaderStyle Font-Bold="False" />
                                                                        <RowStyle Wrap="false" />
                                                                        <SelectedRowStyle BackColor="BurlyWood" />
                                                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <%-- <asp:TemplateField HeaderText="Bill No" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
                                                                    ItemStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRBill" runat="server" ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="Bill Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
                                                                                ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRdate" runat="server" Text='<%#Eval("Inv_Date")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
                                                                                ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRvalue" runat="server" Text='<%#Eval("Rvalue")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Received" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White"
                                                                                ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkRReceived" Checked="true" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                                            VerticalAlign="Middle" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <div class="w-100 designation-submit-button text-center clearfix">
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Go to Enter SS Entry" Width="200px" OnClick="btnSubmit_Click" />
                                        </div>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </center>
                    <br />
                    <br />





                    <center>

                        <asp:Panel ID="pnlTrans" runat="server" Visible="false">
                            <br />
                            <br />
                            <asp:Label ID="Label1" runat="server" Style="color: Red; font-size: 15px; font-weight: bold">Previous Month Transit Bill Details</asp:Label>
                            <br />
                            <br />
                            <asp:GridView ID="grdPrev" runat="server" Height="100%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" RowStyle-Height="20px"
                                GridLines="None" CssClass="mGrid" Width="50%">
                                <HeaderStyle Font-Bold="False" />
                                <RowStyle Wrap="false" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Bill No" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
                                        ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <%--       <asp:Label ID="lblPrevBill" runat="server" Text='<%#Eval("Transit_Bill_No_Date")%>'></asp:Label>--%>
                                            <asp:Literal runat="server" ID="Values" Text='<%# string.Join("<br />", Eval("Transit_Bill_No_Date").ToString().Split(new []{"~"},StringSplitOptions.None)) %>'>
                                            </asp:Literal>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
                                        ItemStyle-Width="200px">
                                        <ItemTemplate>

                                            <asp:Literal runat="server" ID="Literal1" Text='<%# string.Join("<br />", Eval("Transit_bill_Dt").ToString().Split(new []{"~"},StringSplitOptions.None)) %>'>
                                            </asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Value" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
                                        ItemStyle-Width="200px">
                                        <ItemTemplate>

                                            <asp:Literal runat="server" ID="Literal1" Text='<%# string.Join("<br />", Eval("Transit_bill_val").ToString().Split(new []{"~"},StringSplitOptions.None)) %>'>
                                            </asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </center>
                </div>
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>

</body>
</html>
