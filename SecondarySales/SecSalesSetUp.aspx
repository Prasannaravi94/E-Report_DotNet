<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSalesSetUp.aspx.cs" Inherits="SecondarySales_SecSalesSetUp" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Sale - Additional Setup</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <style type="text/css">
        input[type="radio"]
        {
            width: 15px;
            height: 15px;
            
            vertical-align: middle;
            position: relative;
            top: -1px;
            *overflow: hidden;
        }
        
        .Formatrbtn label
        {
          margin-right:30px;
        }
        
        input[type="radio"]:checked:before
        {
            content: "";
            display: block;
            position: relative;
            top: 4px;
            left: 4px;
            width: 6px;
            height: 6px;
            border-radius: 50%;
            background: #ad094d;
        }
        
        /* Base for label styling */
        
        [type="checkbox"]:not(:checked), [type="checkbox"]:checked
        {
            position: absolute;
            left: -9999px;
        }
        [type="checkbox"]:not(:checked) + label, [type="checkbox"]:checked + label
        {
            position: relative;
            padding-left: 20px;
            cursor: pointer;
        }
        
        /* checkbox aspect */
        [type="checkbox"]:not(:checked) + label:before, [type="checkbox"]:checked + label:before
        {
            content: '';
            position: absolute;
            left: 0;
            top: 2px;
            width: 13px;
            height: 13px;
            border: 1px solid #aaa;
            background: #f8f8f8;
            border-radius: 3px;
            box-shadow: inset 0 1px 3px rgba(0,0,0,.3);
        }
        /* checked mark aspect */
        [type="checkbox"]:not(:checked) + label:after, [type="checkbox"]:checked + label:after
        {
            content: '✔';
            position: absolute;
            top: 2px;
            left: 3px;
            font-size: 14px;
            line-height: 0.8;
            color: #09ad7e;
            transition: all .2s;
        }
        /* checked mark aspect changes */
        [type="checkbox"]:not(:checked) + label:after
        {
            opacity: 0;
            transform: scale(0);
        }
        [type="checkbox"]:checked + label:after
        {
            opacity: 1;
            transform: scale(1);
        }
        /* disabled checkbox */
        [type="checkbox"]:disabled:not(:checked) + label:before, [type="checkbox"]:disabled:checked + label:before
        {
            box-shadow: none;
            border-color: #bbb;
            background-color: #ddd;
        }
        [type="checkbox"]:disabled:checked + label:after
        {
            color: #999;
        }
        [type="checkbox"]:disabled + label
        {
            color: #aaa;
        }
        /* accessibility */
        [type="checkbox"]:checked:focus + label:before, [type="checkbox"]:not(:checked):focus + label:before
        {
            border: 1px dotted blue;
        }
        
        /* hover style just for information */
        label:hover:before
        {
            border: 1px solid #4778d9 !important;
        }
        
        #tblSec td
        {
            padding:3px;
        }
        
    </style>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript">

        function Validation() {

            var rbt_Total = $("#rdoTotalNeeded :radio:checked").length;
            var rbt_Value = $("#rdoValueNeeded :radio:checked").length;
            var rbt_Approval = $("#rdoApproval :radio:checked").length;
            var rbt_SaleRate = $("#rdoSale :radio:checked").length;
            var chk_ReportingField = $("#chklstSecSale input:checked").length;
            var rbt_Product = $("#rdoProd :radio:checked").length;

            if (rbt_Total == 0) {

                createCustomAlert("Please Select Total Needed between + and -");
                return false;
            }
            else if (rbt_Value == 0) {

                createCustomAlert("Please Select Value Needed ");
                return false;
            }
            else if (rbt_Approval == 0) {
                createCustomAlert("Please Select Approval System ");
                return false;
            }
            else if (rbt_SaleRate == 0) {
                createCustomAlert("Please Select Sale Calculated Rate ");
                return false;
            }
            else if (chk_ReportingField == 0) {

                createCustomAlert("Please Select Reporting Field ");
                return false;

            }
            else if (rbt_Product == 0) {

                createCustomAlert("Please Select Product Grouping ");
                return false;

            }
            else {
                return true;
            }

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <center>
            <br />
            <div style="width: 50%; border: 1px solid black; background-color: White; border-radius: 5px">
                <table id="tblSec" align="center" border="1">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Is Total Needed in between + and -  "
                                    SkinID="lblMand"></asp:Label>
                                <span style="margin-left: 17px;"><b>:</b></span>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoTotalNeeded" runat="server" RepeatLayout="TABLE" RepeatDirection="Vertical"
                                    Font-Names="Verdana" Font-Size="8">
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblValue" runat="server" Text="Is Value Needed for Total" SkinID="lblMand"></asp:Label>
                                <span style="margin-left: 71px;"><b>:</b></span>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoValueNeeded" runat="server" RepeatDirection="Vertical"
                                    Font-Names="Verdana" Font-Size="8">
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblApproval" runat="server" Text="Approval System Needed?" SkinID="lblMand"></asp:Label>
                                <span style="margin-left: 68px;"><b>:</b></span>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoApproval" runat="server" RepeatDirection="Vertical" Font-Names="Verdana"
                                    Font-Size="8">
                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSale" runat="server" Text="Sale calculated rate based on " SkinID="lblMand"></asp:Label>
                                <span style="margin-left: 49px;"><b>:</b></span>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoSale" runat="server" RepeatDirection="Vertical" RepeatColumns="2"
                                    Font-Names="Verdana" Font-Size="8" CssClass="Formatrbtn">
                                    <asp:ListItem Value="M" Text="MRP Price"></asp:ListItem>
                                    <asp:ListItem Value="R" Text="Retailor Price"></asp:ListItem>
                                    <asp:ListItem Value="D" Text="Distributor Price"></asp:ListItem>
                                    <asp:ListItem Value="T" Text="Target Price"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="NSR Price"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSecSale" runat="server" Text="Reporting Field " SkinID="lblMand"></asp:Label>
                                <span style="margin-left: 125px;"><b>:</b></span>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chklstSecSale" Style="margin-left: 6px;" runat="server" CssClass="Formatrbtn"
                                    RepeatDirection="Vertical" RepeatColumns="2" Font-Names="Verdana" Font-Size="8">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProd" runat="server" Text="Product Grouping" SkinID="lblMand"></asp:Label>
                                <span style="margin-left: 110px;"><b>:</b></span>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoProd" runat="server" RepeatColumns="2" RepeatDirection="Vertical"
                                    Font-Names="Verdana" CssClass="Formatrbtn" Font-Size="8">
                                    <asp:ListItem Value="0" Text="Not Required"></asp:ListItem>
                                    <asp:ListItem Value="C" Text="Category"></asp:ListItem>
                                    <asp:ListItem Value="G" Text="Group"></asp:ListItem>
                                    <asp:ListItem Value="B" Text="Brand"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblSaleField" runat="server" Text="Which one is Sale?" SkinID="lblMand"></asp:Label>
                                <span style="margin-left:102px;"><b>:</b></span>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkSaleField" Style="margin-left: 6px;" runat="server" CssClass="Formatrbtn"
                                    RepeatDirection="Vertical" RepeatColumns="2" Font-Names="Verdana" Font-Size="8">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblClosingField" runat="server" Text="Which one is Closing?" SkinID="lblMand"></asp:Label>
                                <span style="margin-left:85px;"><b>:</b></span>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkClosingField" Style="margin-left: 6px;" runat="server" CssClass="Formatrbtn"
                                    RepeatDirection="Vertical" RepeatColumns="2" Font-Names="Verdana" Font-Size="8">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Save"
                CssClass="savebutton" OnClick="btnSubmit_Click"  OnClientClick="if(!Validation()) return false;" />
            &nbsp;
            <asp:Button ID="btnClear" runat="server" CssClass="savebutton" Width="60px" Height="25px"
                Text="Clear" OnClick="btnClear_Click" />
        </center>
    </div>
    </form>
</body>
</html>
