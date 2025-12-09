<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product_Map_Sub_Divisionwise.aspx.cs"
    Inherits="MasterFiles_Product_Map_Sub_Divisionwise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SubDivision - Product Tag</title>

    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("[src*=plus]").live("click", function () {
                if ($(this).closest("tr").next().css('display') == "table-row") {
                    $(this).closest("tr").after("<tr class='camps'><td></td><td colspan = '1000'>" + $(this).next().html() + "</td></tr>")
                    $(this).next().remove();
                } else
                    $(this).closest("tr").next().css('display', 'table-row');
                $(this).attr("src", "../../../Images/minus.png");
            });
            $("[src*=minus]").live("click", function () {
                $(this).attr("src", "../../../Images/plus.png");
                $(this).closest("tr").next().css('display', 'none');
            });

        });
    </script>--%>
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

        function confirm_Save() {

            if (confirm('Do you want to Tag the Product?')) {
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
    </script>
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
        <%--    $('#btnGo').click(function () {

                var SubDiv = $('#<%=ddlSubDivision1.ClientID%> :selected').text();
                if (SubDiv == "---Select---") {

                    var txt = "Please Select SubDivision";
                    createCustomAlert(txt);

                    $('#ddlSubDivision1').focus();
                    return false;
                }
            });--%>
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=chkHeaderProduct]").click(function () {
                if ($(this).is(":checked")) {
                    $("[id*=DataList1] [id*=chkCatName]").prop("checked",true);
                }
                else {

                    var list = document.getElementById("<%=DataList1.ClientID%>");
                    var chklist = list.getElementsByTagName("input");
                    for (var i = 0; i < chklist.length; i++) {
                        if (chklist[i].type == "checkbox") {

                            var isChecked = $(chklist).prop("checked");

                            var text = $(chklist[i]).closest("td").find("label").html();

                            color = $(chklist[i]).closest("td").find("label").css('color');

                            if (color == "rgb(255, 0, 0)") {
                                if (isChecked) {
                                    chklist[i].checked = true;
                                }
                            }
                            else {
                                chklist[i].checked = false;
                            }

                        }

                    }


                }
            });
            $("[id*=DataList1] [id*=chkCatName]").click(function () {
                if ($("[id*=DataList1] [id*=chkCatName]").length == $("[id*=DataList1] [id*=chkCatName]:checked").length) {
                    $("[id*=chkHeaderProduct]").prop("checked", true);
                }
                else {
                    $("[id*=chkHeaderProduct]").prop("checked", false);
                }
            });
        });



    </script>
    <script type="text/javascript" language="javascript">
        function CheckBoxSelectionValidation() {
            var count = 0;
            var objgridview = document.getElementById('<%= DataList1.ClientID %>');

            for (var i = 0; i < objgridview.getElementsByTagName("input").length; i++) {

                var chknode = objgridview.getElementsByTagName("input")[i];

                if (chknode != null && chknode.type == "checkbox" && chknode.checked) {
                    count = count + 1;
                }
            }

            if (count == 0) {
                var txt = "Please select atleast one checkbox.";
                alert(txt);

                return false;
            }
            else {
                return true;
            }
        }

    </script>
    <link href="../JScript/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/ValidationAlertJs.js" type="text/javascript"></script>
    <link href="../JScript/css/buttons/buttons.css" rel="stylesheet" type="text/css" />
    <%-- <link href="../JScript/Service_CRM/Crm_Dr_Css_Ob/ProductMap_Single_SubDivCSS.css"
     rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />



    <script type="text/javascript">

        function Validation() {
            var SubDiv = $('#<%=ddlSubDivision1.ClientID%> :selected').text();
            if (SubDiv == "---Select---") {

                var txt = "Please Select SubDivision";
                alert(txt);

                $('#ddlSubDivision1').focus();
                return false;
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 100%">
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" style="border-bottom: none">SubDivision - Product Tag</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="single-des-option">

                                    <%--   <asp:Label ID="lblStateProduct" runat="server" Text="Select State" SkinID="lblMand"></asp:Label>
                   
                        <asp:DropDownList ID="ddlStateProduct" runat="server" SkinID="ddlRequired" Height="24px"
                            Width="170px">
                        </asp:DropDownList>--%>
                                    <asp:Label ID="lblSubDivision" runat="server" Text="Select SubDivision" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlSubDivision1" runat="server" CssClass="nice-select">
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnGo" Text="GO" CssClass="savebutton" runat="server"
                                OnClick="btnGo_Click" OnClientClick="return Validation();" />
                            <asp:Button ID="btnclr" runat="server" OnClick="btnclr_Click" Text="Clear" CssClass="resetbutton"></asp:Button>

                        </div>
                        <br />
                        <br />
                    </div>

                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
                </div>

                <div class="row justify-content-center">
                    <asp:Label ID="lblSelect" Text="Select the Product" runat="server"
                        Visible="false"></asp:Label>
                </div>
                <br />
                <div class="row justify-content-center">
                    <asp:CheckBox ID="chkHeaderProduct" runat="server" Text="Select All" />
                </div>
                <br />

                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="display-reportMaintable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin; padding-bottom: 20px;">
                                <table align="center" ">
                                    <tbody>
                                        <tr>
                                            <td align="center">
                                                <asp:DataList ID="DataList1" 
                                                    Width="100%" runat="server" RepeatDirection="Vertical" RepeatColumns="3">
                                                  
                                                        <HeaderTemplate>
                                                        <div style="height: 30px; width: 100%">
                                                            <div style="width: 33%; float: left; height: 50px;">
                                                                <table style="width: 100%; background-color: #f1f5f8;">
                                                                    <tr>
                                                                        <td style="min-width: 65px; max-width: 75px; height: 50px; text-align: center;">
                                                                            <asp:Label ID="lblsln" runat="server" Font-Bold="true" Text="#"></asp:Label>
                                                                        </td>
                                                                        <td style="min-width: 230px; max-width: 250px">
                                                                            <asp:Label ID="lblDocVisit" Text="Product Name" Font-Bold="true" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="min-width: 65px; max-width: 75px">
                                                                            <asp:Label ID="lblunit" Text="Pack" Font-Bold="true" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </div>
                                                            <div style="width: 33%; float: left; height: 30px;">

                                                                <table style="width: 100%; background-color: #f1f5f8">
                                                                    <tr>
                                                                        <td style="min-width: 65px; max-width: 75px; height: 50px; text-align: center">
                                                                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="#"></asp:Label>
                                                                        </td>
                                                                        <td style="min-width: 230px; max-width: 250px">
                                                                            <asp:Label ID="Label2" Text="Product Name" Font-Bold="true" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="min-width: 65px; max-width: 75px">
                                                                            <asp:Label ID="Label3" Text="Pack" Font-Bold="true" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div style="width: 33%; float: left; height: 30px;">

                                                                <table style="width: 100%; background-color: #f1f5f8">
                                                                    <tr>
                                                                        <td style="min-width: 65px; max-width: 75px; height: 50px; text-align: center">
                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="#"></asp:Label>
                                                                        </td>
                                                                        <td style="min-width: 230px; max-width: 250px">
                                                                            <asp:Label ID="Label5" Text="Product Name" Font-Bold="true" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="min-width: 75px; max-width: 75px">
                                                                            <asp:Label ID="Label6" Text="Pack" Font-Bold="true" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="0px" />
                                                    <AlternatingItemStyle />
                                                    <ItemStyle />
                                                    <ItemTemplate>
                                                        <b></b>
                                                        <div style="height: 30px; width: 100%">

                                                            <table>
                                                                 <tr style="width: 100%; border-bottom: 1px solid #DCE2E8; border-right: 1px solid #DCE2E8; border-left: 1px solid #DCE2E8;">
                                                                    <td style="min-width: 60px; max-width: 75px; text-align: center">
                                                                        <asp:Label ID="lblSLNO" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td style="border-left: none">
                                                                        <asp:Label ID="lblPrdCode" runat="server" Text='<%#Eval("Product_Code_SlNo")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                   <td style="min-width: 230px; max-width: 250px; text-align: left; border-left: none">
                                                                        <asp:CheckBox ID="chkCatName" Font-Names="Calibri" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product_Detail_Name")%>' />
                                                                       </td>
                                                                        <td style="min-width: 65px; max-width: 75px">
                                                                            <asp:Label ID="lblprd_sale" runat="server" Text='<%#Eval("Product_Sale_Unit")%>'></asp:Label>
                                                                        </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                    </div>

                </div>
                <br />
                <div class="row justify-content-center">
                     <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="savebutton"
                    Visible="false" OnClick="btnSubmit_Click" />

                </div>


            </div>
            <br />
            <br />

        </div>
    </form>
</body>
</html>
