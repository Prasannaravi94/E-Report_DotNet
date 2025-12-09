<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductBrandList.aspx.cs" Inherits="MasterFiles_ProductBrandList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Product-Brand</title>

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
    <script language="javascript" type="text/javascript">
        function popUp(Product_Brd_Code, Product_Brd_Name) {
            strOpen = "rptProduct_Brd.aspx?Product_Brd_Code=" + Product_Brd_Code + "&Product_Brd_Name=" + Product_Brd_Name
            window.open(strOpen, 'popWindow', '');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Product-Brand</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-7">
                                        <asp:Button ID="btnNew" runat="server" Width="45px" CssClass="savebutton" Text="Add" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnBulkEdit" runat="server" CssClass="resetbutton" Text="Bulk Edit" OnClick="btnBulkEdit_Click" />
                                        <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="resetbutton" Text="S.No Gen" OnClick="btnSlNo_Gen_Click" />
                                        <asp:Button ID="btnReactivate" runat="server" CssClass="resetbutton" Text="Reactivation" OnClick="btnReactivate_Click" />
                                    </div>
                                </div>
                            </div>
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">

                                    <asp:GridView ID="grdProBra" runat="server" Width="100%" HorizontalAlign="Center"
                                        OnRowDataBound="grdProBra_RowDataBound"
                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowUpdating="grdProBra_RowUpdating"
                                        OnRowEditing="grdProBra_RowEditing" OnRowDeleting="grdProBra_RowDeleting" EmptyDataText="No Records Found"
                                        OnPageIndexChanging="grdProBra_PageIndexChanging" OnRowCreated="grdProBra_RowCreated"
                                        OnRowCancelingEdit="grdProBra_RowCancelingEdit" OnRowCommand="grdProBra_RowCommand"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                        AllowSorting="True" OnSorting="grdProBra_Sorting">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdProBra.PageIndex * grdProBra.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Brand Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProBraCode" runat="server" Text='<%#Eval("Product_Brd_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Product_Brd_SName" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProduct_Bra_SName" runat="server" CssClass="input" Text='<%# Bind("Product_Brd_SName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct_Bra_SName" runat="server" Text='<%# Bind("Product_Brd_SName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Product_Brd_Name" HeaderText="Brand Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProBraName" runat="server" CssClass="input" MaxLength="100"
                                                        Text='<%# Bind("Product_Brd_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProBraName" runat="server" Text='<%# Bind("Product_Brd_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="No of Products" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='<%# Eval("brd_count") %>'
                                                        OnClientClick='<%# "return popUp(\"" + Eval("Product_Brd_Code") + "\",\"" + Eval("Product_Brd_Name")  + "\");" %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Slides" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                      <asp:Label ID="lblslide" runat="server" Text='<%# Bind("slide_count") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" ItemStyle-HorizontalAlign="Center"
                                                HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True"></asp:CommandField>

                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="ProductBrand.aspx?Product_Brd_Code={0}" ItemStyle-HorizontalAlign="Center"
                                                DataNavigateUrlFields="Product_Brd_Code"></asp:HyperLinkField>

                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Product_Brd_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Product Brand');">Deactivate
                                                    </asp:LinkButton>
                                                    <%--    <span style="background:url(../Images/cross.png) no repeat;">--%>
                                                    <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../Images/deact2.png" alt="" width="75px" title="This Brand Exists in Product" />
                                                    </asp:Label>
                                                    <%-- </span>--%>
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
