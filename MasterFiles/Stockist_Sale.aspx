<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_Sale.aspx.cs" Inherits="MasterFiles_Stockist_Sale"
    EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fieldforce Stockist Entry Map</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        #btnSubmit
        {
            margin-right: 30px;
        }
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
        
        .Tbl_td
        {
            border-right: 1px solid #ab3131;
            padding-left: 7%;
        }
        
        .TxtboxCss
        {
            width: 224px;
            font-size: 8pt;
            color: gray;
            border-top-style: groove;
            border-right-style: groove;
            border-left-style: groove;
            height: 22px;
            padding-left: 4px;
            background-color: white;
            border-bottom-style: groove;
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
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript">
        var previousCheckId;

        function toggle(chkBox) {
            if (chkBox.checked) {
                if (previousCheckId) {
                    document.getElementById(previousCheckId).checked = false;
                }
                previousCheckId = chkBox.getAttribute('id');
            }
        }

        function CheckBox() {

            var Chk_Length = $("#DataList1 tr td input[type='checkbox']:checked").length;
            //alert(Chk_Length);
            if (Chk_Length > 1) {
                createCustomAlert("Please Select Only One Checkbox");
                return false;
            }
            else if (Chk_Length == 0) {
                createCustomAlert("Please Select Atleast One Checkbox");
                return false;
            }
            else {
                return true;
            }
        }

    </script>
    <script type="text/javascript">
        $(function () {

            var $txt = $('input[id$=txtNew]');
            $txt.keyup(function () {

                searchDdl($txt.val());
            });

            function searchDdl(item) {

                var $txt = $('input[id$=txtNew]');
                var $ddl = $('select[id$=ddlStockist]');
                var $items = $('select[id$=ddlStockist] option');

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
                    });
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
    <script type="text/javascript">

        $(document).ready(function () {

            if ($("#hdnStockist").val() == "") {
                $('#ddlStockist').append($("<option> --- Select the Stockist --- </option>"));
            }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Stockist_Sale.aspx/GetStateDetail",
                data: "{}",
                dataType: "json",
                success: function (data) {

                    if (data.d.length > 0) {

                        $.each(data.d, function (key, value) {
                            $('#ddlState').append($("<option></option>").val(value.State_Code).html(value.StateName));

                        });

                        var ValOpt = data.d.length + 1;
                        $('#ddlState').append("<option value=" + ValOpt + ">--All--</option>");

                        if ($("#hdnState").val() != "") {

                            $("#ddlState option:contains(" + $("#hdnState").val() + ")").attr('selected', 'selected');

                            var StateName = $("#hdnState").val();

                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: "Stockist_Sale.aspx/GetStockistDet",
                                data: '{objStock:' + JSON.stringify(StateName) + '}',
                                dataType: "json",
                                success: function (data) {

                                    $('#ddlStockist').empty();
                                    $.each(data.d, function (key, value) {
                                        $('#ddlStockist').append($("<option></option>").val(value.stockist_code).html(value.Stockist_Name));

                                    });

                                    if ($("#hdnStockist").val() != "") {
                                        $("#ddlStockist option[value=" + $("#hdnStockist").val() + "]").attr('selected', 'selected');
                                    }
                                },
                                error: function (res) {
                                }
                            });

                        }
                    }
                },
                error: function (res) {
                }
            });


            $("#ddlState").change(function () {

                var StateName = $("#ddlState option:selected").text();
                $("#txtNew").val("");

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Stockist_Sale.aspx/GetStockistDet",
                    data: '{objStock:' + JSON.stringify(StateName) + '}',
                    dataType: "json",
                    success: function (data) {

                        $('#ddlStockist').empty();
                        $.each(data.d, function (key, value) {
                            $('#ddlStockist').append($("<option></option>").val(value.stockist_code).html(value.Stockist_Name));
                        });

                        if ($("#hdnStockist").val() != "") {
                            $("#ddlStockist option:contains(" + $("#hdnStockist").val() + ")").attr('selected', 'selected');
                        }
                    },
                    error: function (res) {
                    }
                });
            });

            //            $("#btnStock_Go").click(function () {

            //                $(".StockDDL").prop("disabled", true)
            //            });

        });
    </script>
    <script type="text/javascript">
        function ProcessData() {
            $("#hdnStockist").val($("#ddlStockist").val());
            $("#hdnState").val($("#ddlState option:selected").text());
            //$("#ddlSrc option:selected").val($("#hdnProduct").val());

            //                $("#ddlState").attr("disabled", true);
            //                $("#txtNew").attr("disabled", true);
            //                $("#ddlStockist").attr("disabled", true);


            return true;
        }
    </script>
    <style type="text/css">
    
    .blink {
  animation: blink-animation 1s steps(5, start) infinite;
  -webkit-animation: blink-animation 1s steps(5, start) infinite;
}
@keyframes blink-animation {
  to {
    visibility: hidden;
  }
}
@-webkit-keyframes blink-animation {
  to {
    visibility: hidden;
  }
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <%--<table width="50%">  
         <tr>
         <td style="width:15%" />
               <td colspan="2" align="center">  
               <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand" runat="server" Width="70%" HorizontalAlign="left">
                        <SeparatorTemplate></SeparatorTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnAlpha" runat="server" CommandArgument = '<%#bind("stockist_name") %>' text = '<%#bind("stockist_name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                   
                </td>
            </tr>   
            </table> --%>
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="tblddlDetails">
                <tr style="height: 30px">
                    <td align="left">
                        <asp:Label ID="lblStockistName" runat="server" SkinID="lblMand" Font-Bold="true"
                            Text="State Name "></asp:Label>
                    </td>

                    <td align="left">
                        <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired" CssClass="StockDDL">
                        </asp:DropDownList>

                         <asp:HiddenField ID="hdnState" runat="server" />

                    </td>

                    <%-- <td>
                <asp:DropDownList ID="dlAlpha" runat="server" AutoPostBack="true" Width="160px" DataNavigateUrlFormatString="Stockist_Sale.aspx?stockist_code={0}" 
                       DataNavigateUrlFields="stockist_code" onselectedindexchanged="ddlStockist_SelectedIndexChanged">                    
                    </asp:DropDownList>
                </td>--%>
                    <td align="left">
                        <%--<asp:DropDownList ID="dlAlpha" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                            OnSelectedIndexChanged="dlAlpha_SelectedIndexChanged">
                            <asp:ListItem Selected="true">---ALL---</asp:ListItem>
                            <asp:ListItem>A</asp:ListItem>
                            <asp:ListItem>B</asp:ListItem>
                            <asp:ListItem>C</asp:ListItem>
                            <asp:ListItem>D</asp:ListItem>
                            <asp:ListItem>E</asp:ListItem>
                            <asp:ListItem>F</asp:ListItem>
                            <asp:ListItem>G</asp:ListItem>
                            <asp:ListItem>H</asp:ListItem>
                            <asp:ListItem>I</asp:ListItem>
                            <asp:ListItem>J</asp:ListItem>
                            <asp:ListItem>K</asp:ListItem>
                            <asp:ListItem>L</asp:ListItem>
                            <asp:ListItem>M</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                            <asp:ListItem>O</asp:ListItem>
                            <asp:ListItem>P</asp:ListItem>
                            <asp:ListItem>Q</asp:ListItem>
                            <asp:ListItem>R</asp:ListItem>
                            <asp:ListItem>S</asp:ListItem>
                            <asp:ListItem>T</asp:ListItem>
                            <asp:ListItem>U</asp:ListItem>
                            <asp:ListItem>V</asp:ListItem>
                            <asp:ListItem>W</asp:ListItem>
                            <asp:ListItem>X</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>Z</asp:ListItem>
                        </asp:DropDownList>--%>
                        <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA StockDDL"
                            ToolTip="Enter Text Here"></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlStockist" runat="server" DataNavigateUrlFormatString="Stockist_Sale.aspx?stockist_code={0}"
                            DataNavigateUrlFields="stockist_code" SkinID="ddlRequired" Height="24px" CssClass="StockDDL">
                        </asp:DropDownList>
                         <asp:HiddenField ID="hdnStockist" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="btnStock_Go" runat="server" Width="40px" Height="25px" Text="Go"
                            CssClass="savebutton" OnClick="btnStock_Go_Click"  OnClientClick="return ProcessData()" />
                    </td>
                    <td>
                        <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear"
                            CssClass="savebutton" onclick="btnClear_Click"  />
                    </td>
                </tr>
            </table>
            <br />
        </center>
        <br />
        <center>
         <span class="blink" style="color:Red;font-family:Andalus;font-size:18px;font-weight:bold">Select Stockist Name</span>
        </center>
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="Table1" align="center">
                <tr>
                    <td style="width: 92px" align="left">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Stockist Name" Width="86px"
                            Visible="False"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStockist_Name" runat="server" Width="224px" TabIndex="1" MaxLength="100"
                            onkeypress="AlphaNumeric_NoSpecialChars(event);" Visible="False" CssClass="TxtboxCss"></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblStockist_Address" runat="server" SkinID="lblMand" Text="Address"
                            Width="99px" Visible="False"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStockist_Address" runat="server" Width="297px" TabIndex="2" CssClass="TxtboxCss"
                            MaxLength="150" onkeypress="AlphaNumeric(event);" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 92px" align="left">
                        <asp:Label ID="lblStockist_ContactPerson" runat="server" SkinID="lblMand" Text="Contact Person"
                            Width="91px" Visible="False"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStockist_ContactPerson" runat="server" Width="224px" TabIndex="3"
                            CssClass="TxtboxCss" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            Visible="False"></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblStockist_Designation" runat="server" SkinID="lblMand" Text="ERP Code"
                            Width="91px" Visible="False"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStockist_Desingation" runat="server" Width="150px" TabIndex="4"
                            CssClass="TxtboxCss" MaxLength="15" onkeypress="AlphaNumeric_NoSpecialCharshq(event);"
                            Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 92px" align="left">
                        <asp:Label ID="lblStockist_Mobile" runat="server" SkinID="lblMand" Text="Mobile No"
                            Width="87px" Visible="False"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStockist_Mobile" runat="server" Width="224px" TabIndex="5" CssClass="TxtboxCss"
                            MaxLength="100" onkeypress="CheckNumeric(event);" Visible="False"></asp:TextBox>
                    </td>
                    <td align="left" visible="false">
                        <asp:Label ID="lblTerritory" runat="server" SkinID="lblMand" Text="Territory" Width="91px"
                            Visible="False"></asp:Label>
                    </td>
                    <td align="left" visible="false">
                        <asp:TextBox ID="txtTerritory" runat="server" Width="210px" TabIndex="6" CssClass="TxtboxCss"
                            MaxLength="150" onkeypress="AlphaNumeric_NoSpecialChars(event);" Visible="False">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" cellpadding="3" cellspacing="3" id="tblSalesforceDtls" align="center">
                <tr>
                    <td rowspan="">
                        <asp:Label ID="lblTitle_SalesforceDtls" runat="server" Width="755px" Text="Select the Fieldforce Name"
                            TabIndex="6" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="small" ForeColor="#8B0000" Visible="False"></asp:Label>
                    </td>
                </tr>
                <%-- <table width="80%" align="center" border="0"> --%>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Label ID="lblFilter" runat="server" SkinID="lblMand" Text="Filter By" Font-Bold="true"
                            Visible="False"></asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="ddlFilter" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"
                            Visible="False">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" OnClick="btnGo_Click"
                            CssClass="savebutton" Visible="False" />
                    </td>
                </tr>
            </table>
            <table border="1" width="100%" cellpadding="3" cellspacing="3" align="center">
                <tr>
                    <td rowspan="1" align="left" style="width: 100%; height: 10px">
                        <br />
                        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="2"
                            RepeatLayout="table" CssClass="DataList" BorderStyle="Solid" BorderWidth="1px"
                            Width="100%" BorderColor="#ab3131" GridLines="Both">
                            <ItemStyle CssClass="Tbl_td" />
                            <ItemTemplate>
                                <h3>
                                    <asp:Label ID="lblsf_Name" ForeColor="#3333cc" runat="server" Font-Size="14px" Font-Bold="true"
                                        Text='<%# Eval("HQ") %>' /></h3>
                                <asp:CheckBox ID="chkCategoryNameLabel" runat="server" Font-Size="10px" Text='<%# Eval("sf_Name") %>'>
                                </asp:CheckBox>
                                <asp:HiddenField ID="cbTestID" runat="server" Value='<%# Eval("SF_Code") %>' />
                                <asp:Label ID="lbltest" runat="server"></asp:Label>
                                <asp:HiddenField ID="HidStockistCode" runat="server" />
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Width="70px" Height="25px"
                Text="Update" OnClick="btnSubmit_Click" Visible="False" OnClientClick="if(!CheckBox()) return false;" />
        </center>
    </div>
    <div class="div_fixed">
        <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="savebutton" Visible="false"
            OnClick="btnSave_Click" OnClientClick="if(!CheckBox()) return false;" />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
