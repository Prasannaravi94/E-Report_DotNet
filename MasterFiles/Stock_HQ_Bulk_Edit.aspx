<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stock_HQ_Bulk_Edit.aspx.cs"
    Inherits="MasterFiles_Stock_HQ_Bulk_Edit" EnableEventValidation="true" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stockist - HQ - Bulk Updation</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <style type="text/css">
        .modal {
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

        .loading {
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

        img.likeordisklike {
            height: 24px;
            width: 24px;
            margin-right: 4px;
        }

        h4.liketext {
            color: #F00;
            display: inline;
        }
    </style>
    <style type="text/css">
        #btnSubmit {
            /*margin-right: 330px;*/
        }

        .div_fixed {
            position: fixed;
            top: 400px;
            right: 5px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../JScript/jquery-1.10.2.js" type="text/javascript"></script>
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

        function check() {
            var gv = document.getElementById("gvStockist_HQ");
            var items = gv.getElementsByTagName('select');

            for (i = 0; i < items.length; i++) {
                // alert(items[i].type);

                if (items[i].type == "select-one") {
                    var index = items[i].selectedIndex;
                    //  alert(items[i].options[index].value);
                    if (items[i].options[index].value == 0) {
                        alert("Please select DropDown Value");
                        return false;
                    }
                }
            }

            return true;
        }


    </script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <link href="../JScript/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript">

        function Validate_1() {
            var gridView = $("table[id*=gvStockist_HQ]");

            var GvLength = $("table[id*=gvStockist_HQ] tr:not(:first)").length;


            Textbox = $("table[id*=gvStockist_HQ] tr td input:text");

            TxtLen = $("table[id*=gvStockist_HQ] tr td input:text").length;

            if (GvLength > 0) {



                for (var j = 0; j < TxtLen; j++) {

                    var Lst_Dr_Name = $(Textbox[j]).closest("td").find("[id$='txthq']").val();

                    if (Lst_Dr_Name == "" || Lst_Dr_Name == "0") {
                        alert("Please Enter HQ");
                        $(this).focus();
                        return false;
                    }

                    else {
                    }
                }

                if (Lst_Dr_Name == "" || Lst_Dr_Name == "0") {

                    alert("Please Enter HQ");
                    $(this).focus();
                    return false;

                }

                else {
                }

            }

            for (var j = 0; j < TxtLen; j++) {

                var Lst_Dr_Name = $(Textbox[j]).closest("td").find("[id$='txthq']").val();


                if (Lst_Dr_Name == "" || Lst_Dr_Name == "0") {
                    alert("Please Enter HQ");
                    $(this).focus();
                    return false;
                }

                else {
                }
            }

        }


    </script>
    <style type="text/css">
        .web_dialog_overlay {
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

        .web_dialog {
            display: none;
            position: fixed;
            width: 380px;
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

        .web_dialog_title {
            border-bottom: solid 2px #336699;
            background-color: #336699;
            padding: 4px;
            color: White;
            font-weight: bold;
        }

            .web_dialog_title a {
                color: White;
                text-decoration: none;
            }

        .align_right {
            text-align: right;
        }

        .Formatrbtn label {
            margin-right: 30px;
        }


        /* hover style just for information */
        label:hover:before {
            border: 1px solid #4778d9 !important;
        }


        .btnReAct {
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

        .btnReActivation {
            color: #fff;
            background-color: #158263;
            border-color: #158263;
        }

            .btnReActivation:hover {
                color: #fff;
                background-color: #2b9a7b;
                border-color: #2b9a7b;
            }

            .btnReActivation:focus, .btnReActivation.focus {
                color: #fff;
                background-color: #2b9a7b;
                border-color: #2b9a7b;
            }

            .btnReActivation:active, .btnReActivation.active {
                color: #fff;
                background-color: #158263;
                border-color: #158263;
                background-image: none;
            }

        #btnClose_Plus:focus {
            outline-offset: -2px;
        }

        #btnClose_Plus:hover, #btnClose_Plus:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btnClose_Plus:hover, #btnClose_Plus:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btnClose_Plus:active, #btnClose_Plus:hover {
            outline: 0px none currentColor;
        }
    </style>
    <%--<link href="../JScript/BootStrap/dist/css/bootstrap.css" rel="stylesheet" type="text/css" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <br />
                        <h2 class="text-center" style="border-bottom: none;">Stockist - HQ - Bulk Updation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-5">
                                        <asp:Label ID="lblstate" runat="server" CssClass="label">State</asp:Label>
                                        <asp:DropDownList ID="ddlSt" runat="server" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1" style="padding-top: 18px">
                                        <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" Width="60px"
                                            OnClick="btnGo_Click" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <div class="display-table clearfix ">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="gvStockist_HQ" runat="server" Width="100%" HorizontalAlign="Center"
                                        EmptyDataText="No Records Found" OnRowDataBound="gvStockist_HQ_RowDataBound"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                        AlternatingRowStyle-CssClass="alt">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stockist_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Code" runat="server" Text='<%#Bind("Stockist_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stockist Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Name" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ERP Code">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtERPCode" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="38px"
                                                        runat="server" MaxLength="100" Text='<%# Bind("Stockist_Designation") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQName" runat="server" Text='<%# Bind("Territory") %>' Visible="false" />
                                                    <asp:TextBox ID="txthq" runat="server" Text='<%# Bind("Territory") %>' CssClass="input" Height="38px"> 
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>' Visible="false" />
                                                    <asp:DropDownList ID="ddlState" runat="server" SkinID="nice-select">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>

                            <br />
                            <center>
                                <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="savebutton"
                                    Visible="false" OnClick="btnSubmit_Click" OnClientClick="return Validate_1()" />
                            </center>
                        </div>                       
                    </div>
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />                   
                </div>
                  
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
