<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RCPA_View.aspx.cs" Inherits="MasterFiles_RCPA_View" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RCPA View</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp() {
            popUpObj = window.open("rptRCPAView.aspx?sf_Code=" + sf_Code + "&sf_Code_Name=" + sf_Code_Name + "&selValues=" + selValues,
                                    "ModalPopUp"//,
                                    //"directories = 0," +
                                    //"titlebar = 0," +
                                    //"toolbar=no," +
                                    //"scrollbars=yes," +
                                    //"location=no," +
                                    //"statusbar=no," +
                                    //"menubar=no," +
                                    //"addressbar=no," +
                                    //"resizable=yes," +
                                    //"width=800," +
                                    //"height=600," +
                                    //"left = 0," +
                                    //"top=0"
                                    );
            popUpObj.focus();
        }
    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            });
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
            $('#btnSubmit').click(function () {
                sf_Code = document.getElementById("ddlFieldForce").value;
                sf_Code_Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                var st = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Field Force Name."); $('ddlFieldForce').focus(); return false; }
                var group = $('#<%=cblProductList.ClientID%> input:checked');
                var hasChecked = false;
                for (var i = 0; i < group.length; i++) {
                    if (group[i].checked)
                        hasChecked = true;
                    break;
                }
                if (hasChecked == false) {
                    alert("Select Product Name"); $('cblProductList').focus(); return false;
                }

                selValues = "";
                var ChkboxList1Ctl = document.getElementById("cblProductList");
                var itemarr = document.getElementById("cblProductList").getElementsByTagName("span");
                var ChkboxList1Arr = document.getElementById("cblProductList").getElementsByTagName("input");

                if (ChkboxList1Arr != null) {
                    for (var i = 0; i < ChkboxList1Arr.length; i++) {
                        if (ChkboxList1Arr[i].checked)
                            selValues += "'" + itemarr[i].getAttribute("dvalue") + "'" + ",";
                    }
                    if (selValues.length > 0)
                        selValues = selValues.substr(0, selValues.length - 1);
                }
                if (sf_Code != '' && sf_Code != 0 && sf_Code_Name != '' && selValues != '') {
                    showModalPopUp(sf_Code, sf_Code_Name, selValues);
                }
            });
        });
    </script>
    <link href="../JScript/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/ValidationAlertJs.js" type="text/javascript"></script>
    <link href="../JScript/css/buttons/buttons.css" rel="stylesheet" type="text/css" />
    <%--   <link href="../JScript/Service_CRM/Crm_Dr_Css_Ob/ProductMap_Single_SubDivCSS.css"
        rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <script type="text/javascript">
        function showLoader(loaderType) {

            if (loaderType == "Search1") {
                document.getElementById("loaderSearchddlSFCode").style.display = '';
            }
        }

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
    </script>
    <style type="text/css">
        .chkChoice input[type="checkbox"] {
            margin-right: 5px;
            margin-top: 10px;
        }

        .chkChoice tr {
            padding: 5px;
            border-bottom: 1px solid #DCE2E8;
            border-right: 1px solid #DCE2E8;
            border-left: 1px solid #DCE2E8;
        }

        .chkChoice td {
            padding-left: 5px;
            width: 25%;
            height: 30px;
            border-right: 1px solid #DCE2E8;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .gridheight {
            overflow-y: auto !important;
            height: 500px !important;
        }

        .chklabel {
            color: #636d73;
            font-size: 12px;
            font-weight: 401;
            text-transform: uppercase;
            text-align: center;
            padding-left: 10px;
        }
    </style>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
        });
    </script>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">RCPA - View</h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" CssClass="custom-select2 nice-select" Width="100%"
                                    OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <img src="../../Images/ajaxRoundLoader.gif" style="display: none;" id="loaderSearchddlSFCode" />
                        </div>
                        <br />
                    </div>
                </div>
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <center>
                            <div id="Product" runat="server" visible="false">
                                <div class="row justify-content-center">
                                    <asp:Label ID="lblSelect" Text="Select Product Name" runat="server"></asp:Label>
                                </div>
                                <br />
                                <table width="85%">
                                    <tr>
                                        <td style="background-color: #f1f5f8; height: 50px; width: 25%;">
                                            <asp:Label ID="Label1" CssClass="chklabel" Text="Product Name" runat="server"></asp:Label>
                                        </td>
                                        <td style="background-color: #f1f5f8; height: 50px; width: 25%;">
                                            <asp:Label ID="Label2" CssClass="chklabel" Text="Product Name" runat="server"></asp:Label>
                                        </td>
                                        <td style="background-color: #f1f5f8; height: 50px; width: 25%;">
                                            <asp:Label ID="Label3" CssClass="chklabel" Text="Product Name" runat="server"></asp:Label>
                                        </td>
                                        <td style="background-color: #f1f5f8; height: 50px; width: 25%;">
                                            <asp:Label ID="Label4" CssClass="chklabel" Text="Product Name" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <asp:CheckBoxList ID="cblProductList" Width="85%" Font-Size="8pt" runat="server"
                                            RepeatColumns="4" DataTextField="Ch_Name" DataValueField="Ch_Code" CssClass="chkChoice">
                                        </asp:CheckBoxList>
                                    </tr>
                                </table>
                                <br />
                                <table width="85%">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="savebutton" OnClick="btnClear_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>
