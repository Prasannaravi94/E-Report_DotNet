<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductCategoryList.aspx.cs"
    Inherits="MasterFiles_ProductCategoryList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product-Category</title>

  
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
        function popUp(Product_Cat_Code, Product_Cat_Name) {
            strOpen = "rptProduct_Cat.aspx?Product_Cat_Code=" + Product_Cat_Code + "&Product_Cat_Name=" + Product_Cat_Name
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
                        <h2 class="text-center">Product-Category</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-7" >
                                        <asp:Button ID="btnNew" runat="server" Width="45px" CssClass="savebutton" Text="Add" OnClick="btnNew_Click"  />
                                        <asp:Button ID="btnBulkEdit" runat="server" CssClass="resetbutton" Text="Bulk Edit" OnClick="btnBulkEdit_Click"  />
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

                                    <asp:GridView ID="grdProCat" runat="server" Width="100%" HorizontalAlign="Center"
                                        OnRowDataBound="grdProCat_RowDataBound"
                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowUpdating="grdProCat_RowUpdating"
                                        OnRowEditing="grdProCat_RowEditing" OnRowDeleting="grdProCat_RowDeleting" EmptyDataText="No Records Found"
                                        OnPageIndexChanging="grdProCat_PageIndexChanging" OnRowCreated="grdProCat_RowCreated"
                                        OnRowCancelingEdit="grdProCat_RowCancelingEdit" OnRowCommand="grdProCat_RowCommand"
                                        GridLines="None" CssClass="table"  AllowSorting="True" OnSorting="grdProCat_Sorting"  PagerStyle-CssClass="gridview1">
                                       
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdProCat.PageIndex * grdProCat.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Category Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProCatCode" runat="server" Text='<%#Eval("Product_Cat_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Product_Cat_SName"  HeaderText="Short Name"
                                               ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProduct_Cat_SName" runat="server" CssClass="input" Text='<%# Bind("Product_Cat_SName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct_Cat_SName" runat="server" Text='<%# Bind("Product_Cat_SName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Product_Cat_Name"    HeaderText="Category Name"    ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProCatName" CssClass="input" runat="server" MaxLength="100"
                                                        Text='<%# Bind("Product_Cat_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProCatName" runat="server" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--    <asp:TemplateField  HeaderText="Product Count" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="center">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblCatCount" runat="server" Text='<%# Bind("cat_count") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                            <%--<asp:HyperLinkField DataTextField="cat_count" Target="_blank" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="cat_count" 
                     DataNavigateUrlFormatString="rptProduct_Cat.aspx?Product_Cat_Code={0}" HeaderText="Product Count" />--%>
                                            <asp:TemplateField HeaderText="No of Products" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='<%# Eval("cat_count") %>'
                                                        OnClientClick='<%# "return popUp(\"" + Eval("Product_Cat_Code") + "\",\"" + Eval("Product_Cat_Name")  + "\");" %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit"  ItemStyle-HorizontalAlign="Center"
                                                HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                            </asp:CommandField>
                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit"  ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="ProductCategory.aspx?Product_Cat_Code={0}"
                                                DataNavigateUrlFields="Product_Cat_Code">                                               
                                            </asp:HyperLinkField>

                                            <asp:TemplateField HeaderText="Deactivate"  ItemStyle-HorizontalAlign="Center">                                              
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Product_Cat_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Product Category');">Deactivate
                                                    </asp:LinkButton>
                                                    <%--    <span style="background:url(../Images/cross.png) no repeat;">--%>
                                                    <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../Images/deact2.png" alt="" width="75px" title="This Category Exists in Product" />
                                                    </asp:Label>
                                                    <%-- </span>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--    <asp:TemplateField HeaderText="Delete">
                                         <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                      </ControlStyle>
                                      <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                       <ItemTemplate>
                                         <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Product_Cat_Code") %>'
                                    CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Product Category');">Delete
                                   </asp:LinkButton>
                                   </ItemTemplate>
                                   </asp:TemplateField>--%>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center"   VerticalAlign="Middle" CssClass="no-result-area" />
 
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
