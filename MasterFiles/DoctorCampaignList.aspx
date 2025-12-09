<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorCampaignList.aspx.cs" Inherits="MasterFiles_DoctorCampaignList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor - Campaign</title>

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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center" id="Heading" runat="server"></h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-12">
                                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Width="60px" Text="Add" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnBulkEdit" runat="server" CssClass="resetbutton" Text="Bulk Edit" OnClick="btnBulkEdit_Click" />
                                        <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="resetbutton" Text="S.No Gen" OnClick="btnSlNo_Gen_Click" />
                                        <asp:Button ID="btnReact" runat="server" CssClass="resetbutton" Text="Reactivation" OnClick="btnReact_Click" />

                                    </div>

                                </div>
                            </div>
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdDocSubCat" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                                        OnRowUpdating="grdDocSubCat_RowUpdating" OnRowEditing="grdDocSubCat_RowEditing"
                                        OnRowDeleting="grdDocSubCat_RowDeleting"
                                        OnPageIndexChanging="grdDocSubCat_PageIndexChanging" OnRowCreated="grdDocSubCat_RowCreated"
                                        OnRowCancelingEdit="grdDocSubCat_RowCancelingEdit" OnRowCommand="grdDocSubCat_RowCommand"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                        AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdDocSubCat_Sorting">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Campaign Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocSubCatCode" runat="server" Text='<%#Eval("Doc_SubCatCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_SubCatSName" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDoc_SubCat_SName" runat="server" CssClass="input" MaxLength="12" Text='<%# Bind("Doc_SubCatSName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDoc_SubCat_SName" runat="server" Text='<%# Bind("Doc_SubCatSName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_SubCatName"  HeaderText="Campaign Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDocSubCatName" CssClass="input" runat="server" MaxLength="100" Text='<%# Bind("Doc_SubCatName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocSubCatName" runat="server" Text='<%# Bind("Doc_SubCatName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit"  HeaderText="Inline Edit" ItemStyle-HorizontalAlign="CENTER"
                                                ShowEditButton="True"></asp:CommandField>
                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit"  ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="DoctorCampaign.aspx?Doc_SubCatCode={0}"
                                                DataNavigateUrlFields="Doc_SubCatCode"></asp:HyperLinkField>

                                            <asp:TemplateField HeaderText="Deactivate"  ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Doc_SubCatCode") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Doctor Sub-Category');">Deactivate
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Delete">
                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                            </ControlStyle>
                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Doc_SubCatCode") %>'
                                    CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Doctor Sub-Category');">Delete
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
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
