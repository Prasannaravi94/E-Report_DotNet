<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductRate.aspx.cs" Inherits="MasterFiles_MR_ProductRate" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Statewise - Product Rate View</title>
    <%-- <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
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

        .marright {
            margin-left: 90%;
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
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
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
                var divi = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (divi == "--Select--") { alert("Select Division Name."); $('#ddlDivision').focus(); return false; }
            });
        });
    </script>
    <script type="text/javascript">
        function PrintGridData() {

            var prtGrid = document.getElementById('<%=grdProduct.ClientID %>');

            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', '');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Product Rate</h2>
                        <div class="designation-area clearfix">
                            <asp:Panel ID="pnldivision" runat="server" Visible="false">

                                <div class="single-des clearfix">
                                    <div class="single-des-option">

                                        <center>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" CssClass="savebutton" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="lblSelect" Text="Please Select the Division Name" runat="server" ForeColor="DarkGreen"
                                                Font-Size="Large" Visible="false"></asp:Label>
                                        </center>
                                    </div>
                                </div>
                            </asp:Panel>


                            <asp:Panel ID="pnlstate" runat="server" Visible="false">

                                <div class="single-des clearfix">
                                    <div class="single-des-option">
                                        <asp:Label ID="lblState" runat="server" CssClass="label">State Name</asp:Label>

                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />

                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="btnstate" runat="server" CssClass="savebutton" Text="Go"
                                        OnClick="btnstate_Click" />
                                </div>

                            </asp:Panel>
                        </div>
                        <br />
                    </div>
                </div>

            


            <div class="row ">
                <div class="col-lg-12">
                    <asp:Panel ID="pnlprint" runat="server" CssClass="panelmarright">
                        <asp:LinkButton ID="lnkPrint" ToolTip="Print" runat="server" OnClientClick="PrintGridData()">
                            <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="30px" Style="border-width: 0px;" />
                        </asp:LinkButton>
                    </asp:Panel>
                </div>
            </div>


            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <div class="display-table clearfix">
                        <div class="table-responsive" style="scrollbar-width: thin;">
                            <asp:GridView ID="grdProduct" runat="server" Width="100%" HorizontalAlign="Center"
                                AutoGenerateColumns="False" AllowSorting="true"
                                OnSorting="grdProduct_Sorting" EmptyDataText="No Records Found" OnPageIndexChanging="grdProduct_PageIndexChanging"
                                OnRowDataBound="grdProduct_RowDataBound" GridLines="None" CssClass="table">

                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProdCode" runat="server" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Description" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProDesc" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Packing" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleUn" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRP Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsam1" runat="server" Text='<%# Bind("MRP_Price")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsam2" runat="server" Text='<%# Bind("Retailor_Price")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distributor Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsam3" runat="server" Text='<%# Bind("Distributor_Price") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsam4" runat="server" Text='<%# Bind("Target_Price") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSample" runat="server" Text='<%# Bind("Sample_Price") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="no-result-area" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>

            </div>
            </div>
         <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
    </form>
</body>
</html>
