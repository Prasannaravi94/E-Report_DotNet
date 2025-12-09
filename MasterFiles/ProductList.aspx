<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductList.aspx.cs" Inherits="MasterFiles_ProductList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Detail</title>

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
            $('#Btnsrc').click(function () {

                var divi = $('#<%=ddlSrch.ClientID%> :selected').text();
                var divi1 = $('#<%=ddlProCatGrp.ClientID%> :selected').text();
                if (divi1 == "---Select---") { alert(divi + "."); $('#ddlProCatGrp').focus(); return false; }

                if ($("#TxtSrch").val() == "") { alert("Product Name."); $('#TxtSrch').focus(); return false; }


            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Product Detail</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-12">

                                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Text="Add Product" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnCreate" runat="server" CssClass="resetbutton" Text="Quick Add" OnClick="btnCreate_Click" />
                                        <asp:Button ID="btnBulk" runat="server" CssClass="resetbutton" Width="130px" Text="Edit All Products" OnClick="btnBulk_Click" />
                                        <asp:Button ID="btnSno" runat="server" CssClass="resetbutton" Text="S.No Gen" OnClick="btnSno_Click" />
                                        <asp:Button ID="btnView" runat="server" CssClass="resetbutton" Text="Product List" OnClick="btnView_Click" />
                                        <asp:Button ID="btnStateWiseProduct" runat="server" CssClass="resetbutton" Width="155px" Text="State - Product Tag" OnClick="btnStateWiseProduct_Click" />
                                        <asp:Button ID="btnSubDivProductMap" runat="server" CssClass="resetbutton" Width="195px" Text="SubDivision - Product Tag" OnClick="btnSubDivProductMap_Click" />
                                        <asp:Button ID="btnReactivate" runat="server" CssClass="resetbutton" Text="Reactivation" OnClick="btnReactivate_Click" />
                                        <asp:Button ID="btnProdCodeChg" runat="server" CssClass="resetbutton" Text="Code Change" OnClick="btnProdCodeChg_Click" Visible="false" />
                                        <%--<asp:Button ID="btnCatMap" runat="server" CssClass="savebutton" Text="Product Cat Map" onclick="btnCatMap_Click" />--%>
                                    </div>
                                </div>
                                <br />
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblGiftType" runat="server" Text="Search By" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Product Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Product Category" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Product Group" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Product Brand" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Sub Division" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="State" Value="7"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-3">
                                        <div class="single-des clearfix" style="padding-top: 19px">
                                            <asp:TextBox ID="TxtSrch" runat="server" CssClass="input" Visible="false" Width="100%"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px">
                                            <asp:DropDownList ID="ddlProCatGrp" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlProCatGrp_SelectedIndexChanged"
                                                CssClass="nice-select" TabIndex="4" Visible="false">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-lg-2" style="padding-top: 19px;">
                                        <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="50px"
                                            Text="Go" OnClick="Btnsrc_Click" Visible="false" />
                                    </div>
                                </div>

                            </div>
                            <p>
                                <br />
                            </p>

                            <table width="70%">
                                <tr>
                                    <td style="width: 50%" />
                                    <td colspan="2" align="center">
                                        <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand" runat="server" Width="70%" HorizontalAlign="center">
                                            <SeparatorTemplate></SeparatorTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnAlpha" Font-Size="14px" runat="server" CommandArgument='<%#Bind("Product_Detail_Name") %>' Text='<%#Bind("Product_Detail_Name") %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>

                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">

                                    <asp:GridView ID="grdProduct" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                                        OnRowUpdating="grdProduct_RowUpdating" OnRowEditing="grdProduct_RowEditing"
                                        OnPageIndexChanging="grdProduct_PageIndexChanging" OnRowCreated="grdProduct_RowCreated"
                                        OnRowCancelingEdit="grdProduct_RowCancelingEdit" OnRowCommand="grdProduct_RowCommand"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                        AllowSorting="True" OnSorting="grdProduct_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdProduct.PageIndex * grdProduct.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Product Code" SortExpression="Product_Detail_Code" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProdCode" runat="server" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Product_Detail_Name" HeaderStyle-Width="200px" HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProName" runat="server" CssClass="input" MaxLength="150" Text='<%# Bind("Product_Detail_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProDesc" runat="server" Width="230px" CssClass="input" MaxLength="250" Text='<%# Bind("Product_Description") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProDesc" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sale Unit" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSaleUn" CssClass="input" Width="80px" runat="server" MaxLength="15" Text='<%# Bind("Product_Sale_Unit") %>' onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSaleUn" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Slides" ItemStyle-HorizontalAlign="Left">
                                                  <ItemTemplate>
                                                    <asp:Label ID="lblslide" runat="server" Text='<%# Bind("slide_count") %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ItemStyle-HorizontalAlign="Center"
                                                ShowEditButton="True"></asp:CommandField>

                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="ProductDetail.aspx?Product_Detail_Code={0}"
                                                DataNavigateUrlFields="Product_Detail_Code"></asp:HyperLinkField>

                                            <asp:TemplateField HeaderText="Deactivate" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Product_Detail_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Product');">Deactivate
                                                    </asp:LinkButton>
                                                     <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">        
                                                   <img src="../Images/deact2.png" alt="" width="75px" title="This Product Exists in Slide" />
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                </div>
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
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
