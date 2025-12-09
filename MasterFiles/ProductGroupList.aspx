<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductGroupList.aspx.cs"
    Inherits="MasterFiles_ProductGroupList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product-Group</title>

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

        .wrap {
            /* force the div to properly contain the floated images: */
            position: relative;
            float: left;
            clear: none;
            overflow: hidden;
        }

            .wrap img {
                position: relative;
                z-index: 1;
            }

            .wrap #lblimg {
                display: block;
                position: absolute;
                width: 100%;
                top: 30%;
                left: 0;
                z-index: 2;
                text-align: center;
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
        function popUp(Product_Grp_Code, Product_Grp_Name) {
            strOpen = "rptProduct_Grp.aspx?Product_Grp_Code=" + Product_Grp_Code + "&Product_Grp_Name=" + Product_Grp_Name
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
                        <h2 class="text-center">Product-Group</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-7">  
                                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Width="45px" Text="Add" OnClick="btnNew_Click" />
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

                                    <asp:GridView ID="grdProGrp" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowUpdating="grdProGrp_RowUpdating"
                                        OnRowEditing="grdProGrp_RowEditing" OnRowDeleting="grdProGrp_RowDeleting" EmptyDataText="No Records Found"
                                        OnPageIndexChanging="grdProGrp_PageIndexChanging" OnRowCreated="grdProGrp_RowCreated"
                                        OnRowCancelingEdit="grdProGrp_RowCancelingEdit" OnRowCommand="grdProGrp_RowCommand"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                        AllowSorting="True" OnSorting="grdProGrp_Sorting">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Group Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProGrpCode" runat="server" Text='<%#Eval("Product_Grp_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Product_Grp_SName" HeaderText="Short Name"
                                                ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProduct_Grp_SName" runat="server" CssClass="input" MaxLength="6"
                                                        Text='<%# Bind("Product_Grp_SName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct_Grp_SName" runat="server" Text='<%# Bind("Product_Grp_SName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Product_Grp_Name" HeaderText="Group Name"
                                                ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProGrpName" CssClass="input" runat="server" MaxLength="100"
                                                        Text='<%# Bind("Product_Grp_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProGrpName" runat="server" Text='<%# Bind("Product_Grp_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="No of Products" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='<%# Eval("Grp_count") %>'
                                                        OnClientClick='<%# "return popUp(\"" + Eval("Product_Grp_Code") + "\",\"" + Eval("Product_Grp_Name")  + "\");" %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="No of Slides" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                  <asp:Label ID="lblSlide" runat="server" Text='<%# Bind("slide_count") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True"></asp:CommandField>

                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-HorizontalAlign="Center"
                                                DataNavigateUrlFormatString="ProductGroup.aspx?Product_Grp_Code={0}" DataNavigateUrlFields="Product_Grp_Code"></asp:HyperLinkField>

                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Product_Grp_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Product Group');">Deactivate
                                                    </asp:LinkButton>

                                                    <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">        
                                                   <img src="../Images/deact2.png" alt="" width="75px" title="This Category Exists in Product" />
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--    <asp:TemplateField HeaderText="Delete">
                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                            </ControlStyle>
                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Product_Grp_Code") %>'
                                    CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Product Group');">Delete
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            --%>
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
