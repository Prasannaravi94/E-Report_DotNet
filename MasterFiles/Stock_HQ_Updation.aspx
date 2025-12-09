<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stock_HQ_Updation.aspx.cs"
    Inherits="MasterFiles_Stock_HQ_Updation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stockist - HQ - Updation</title>
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
                        createCustomAlert("Please select DropDown Value");
                        return false;
                    }
                }
            }

            return true;
        }

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
            width: 340px;
            min-height: 200px;
            max-height: auto;
            top: 50%;
            left: 50%;
            margin-left: -170px;
            margin-top: -100px;
            background-color: #ffffff;
            border: 2px solid #28b5e0;
            padding: 0px;
            z-index: 102;
            /*font-family: Verdana;*/
            font-size: 10pt;
        }

        .web_dialog_title {
            border-bottom: solid 2px #28b5e0;
            background-color: #28b5e0;
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

    <%--  <link href="../JScript/BootStrap/dist/css/bootstrap.css" rel="stylesheet" type="text/css" />--%>
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
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });
    </script>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center" id="heading" runat="server" style="border-bottom: none"></h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-12">
                                        <asp:Button ID="btnEdit" runat="server" Width="80px" CssClass="savebutton"
                                            Text="Bulk Edit" OnClick="btnEdit_Click" />
                                    </div>
                                </div>
                                <br />


                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-5">
                                        <div class="single-des clearfix">
                                            <div style="float: left; width: 90%">
                                                <asp:Label ID="Lbldivi" runat="server" CssClass="label">Field Force</asp:Label>
                                                <%-- <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                            ToolTip="Enter Text Here"></asp:TextBox>
                                        <asp:LinkButton ID="linkcheck" runat="server"
                                            OnClick="linkcheck_Click">
                                        <img src="../Images/Selective_Mgr.png" />
                                        </asp:LinkButton>--%>
                                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                                </asp:DropDownList>

                                                <%-- <div id="testImg">
                                            <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;" runat="server" /><span
                                                style="font-family: Verdana; color: Red; font-weight: bold;">Loading Please Wait...</span>
                                        </div>--%>
                                            </div>
                                            <div style="float: left; width: 8%; padding-right: 5px; padding-top: 22px">
                                                <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" Width="60px"
                                                    OnClick="btnGo_Click" />
                                            </div>
                                        </div>

                                        <div class="single-des clearfix">
                                            <div style="margin-top: 2px; float: right; margin-right: 32%">
                                                <div style="width: 20%; height: 60px; float: left">
                                                    <div>
                                                        <div style="width: 100px">
                                                            <a href="#" id="btnAdd_Hq" class="blink_me" style="color: red; font-weight: bold; font-family: @NSimSun; font-size: 14px"
                                                                shape="circle" runat="server">Add HQ</a>
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
                                                                    <td class="web_dialog_title">Stockist - HQ Creation
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
                                                                                    <td align="left" class="stylespc" style="padding-bottom: 10px;">
                                                                                        <asp:Label ID="lblShortName" runat="server" CssClass="label">Short Name<span style="color:Red;padding-left:2px;">*</span></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" class="stylespc" style="padding-bottom: 10px;">
                                                                                        <asp:TextBox ID="txtPool_Sname" CssClass="input" Height="33px"
                                                                                            TabIndex="1" runat="server" MaxLength="10"
                                                                                            onkeypress="CharactersOnly(event);">
                                                                                        </asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="stylespc">
                                                                                        <asp:Label ID="lblPoolName" runat="server" CssClass="label">Name<span style="color:Red;padding-left:2px;">*</span></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" class="stylespc">
                                                                                        <asp:TextBox ID="txtPool_Name" CssClass="input" Height="33px"
                                                                                            TabIndex="2" runat="server"
                                                                                            MaxLength="120" onkeypress="CharactersOnly(event);">
                                                                                        </asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </center>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="text-align: center; padding-top: 20px;">
                                                                        <asp:Button ID="btnHq" runat="server" Text="Save" CssClass="savebutton"
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
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                <asp:GridView ID="gvStockist_HQ" runat="server" Width="100%" HorizontalAlign="Center"
                                    EmptyDataText="No Records Found" Visible="false" OnRowDataBound="gvStockist_HQ_RowDataBound"
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
                                                <asp:TextBox ID="txtERPCode" CssClass="input" Height="42px" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                                    runat="server" MaxLength="100" Text='<%# Bind("Stockist_Designation") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHQName" runat="server" Text='<%# Bind("Territory") %>' Visible="false" />
                                                <asp:DropDownList ID="ddlHQ" runat="server" CssClass="nice-select">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>' Visible="false" />
                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="nice-select">
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
                                Visible="false" OnClick="btnSubmit_Click" />
                        </center>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
        </div>
    </form>
</body>
</html>
