<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quiz_Category_List.aspx.cs"
    Inherits="MasterFiles_Options_Quiz_Category_List" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Online Quiz - Category List</title>
    <%--   <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <%--<link href="../../css/style.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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
    <script src="../../JsFiles/CommonValidation.js" type="text/javascript"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <%--<link href="../../css/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <br />
                        <h2 class="text-center" style="border-bottom: none !important;" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix pull-left">
                                <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Text="Add" OnClick="btnNew_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvCategoryList" runat="server" Width="100%" HorizontalAlign="Center"
                                EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowPaging="True"
                                PageSize="10" GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt"
                                OnRowEditing="gvCategoryList_RowEditing" OnRowUpdating="gvCategoryList_RowUpdating"
                                OnRowCancelingEdit="gvCategoryList_RowCancelingEdit" OnRowCreated="gvCategoryList_RowCreated"
                                OnRowCommand="gvCategoryList_RowCommand" ShowHeaderWhenEmpty="true" AllowSorting="True"
                                OnSorting="gvCategory_Sorting">
                                <%--<HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  (gvCategoryList.PageIndex * gvCategoryList.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryId" runat="server" Text='<%#Eval("Category_Id")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Category_ShortName" HeaderText="Short Name"
                                        ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <%-- <asp:TextBox ID="txtShortName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="20" onkeypress="CharactersOnly(event);"
                                            Text='<%# Bind("subdivision_sname") %>'></asp:TextBox>--%>
                                            <asp:TextBox ID="txtShortName" SkinID="TxtBxAllowSymb" CssClass="input" runat="server" MaxLength="10"
                                                Text='<%# Bind("Category_ShortName") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Category_ShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Category_Name" HeaderText="Category Name" 
                                        ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCategoryName" SkinID="TxtBxAllowSymb" runat="server" CssClass="input" MaxLength="100"
                                                onkeypress="CharactersOnly(event);" Text='<%# Bind("Category_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("Category_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowHeader="True" EditText="Inline Edit" 
                                        HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                    </asp:CommandField>
                                    <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center"
                                        DataNavigateUrlFormatString="Quiz_Category_Creation.aspx?Category_Id={0}" DataNavigateUrlFields="Category_Id">
                                    </asp:HyperLinkField>
                                    <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Category_Id") %>'
                                                CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the  Category');">Deactivate
                                            </asp:LinkButton>
                                            <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../../Images/deact1.png" alt="" width="55px" title="This Category Exists in Category" />
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" />
            </div>
        </div>
    </form>
</body>
</html>
