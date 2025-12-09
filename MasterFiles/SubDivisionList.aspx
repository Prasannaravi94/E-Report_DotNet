<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubDivisionList.aspx.cs"
    Inherits="MasterFiles_SubDivisionList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sub-Division</title>

    <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
                        <h2 class="text-center">Sub-Division</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-7">

                                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Text="Add" Width="60px"  OnClick="btnNew_Click" />

                                    </div>
                                </div>
                            </div>
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">

                                    <asp:GridView ID="grdSubDiv" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowUpdating="grdSubDiv_RowUpdating"
                                        OnRowEditing="grdSubDiv_RowEditing" OnRowCommand="grdSubDiv_RowCommand" OnPageIndexChanging="grdSubDiv_PageIndexChanging"
                                        OnRowCreated="grdSubDiv_RowCreated" OnRowCancelingEdit="grdSubDiv_RowCancelingEdit"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                        AllowSorting="True" OnSorting="grdSubDiv_Sorting">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdSubDiv.PageIndex * grdSubDiv.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sub Division Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsubdivCode" runat="server" Text='<%#Eval("subdivision_code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="subdivision_sname"  HeaderText="Short Name" ItemStyle-HorizontalAlign="Left" >
                                                <EditItemTemplate>
                                                    <%-- <asp:TextBox ID="txtShortName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="20" onkeypress="CharactersOnly(event);"
                                            Text='<%# Bind("subdivision_sname") %>'></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtShortName" CssClass="input" Width="100%" runat="server" MaxLength="10" Text='<%# Bind("subdivision_sname") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("subdivision_sname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="subdivision_name" HeaderText="Sub Division Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSubDivName" CssClass="input" Width="100%" runat="server" MaxLength="100" onkeypress="CharactersOnly(event);"
                                                        Text='<%# Bind("subdivision_name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubDivName" runat="server" Text='<%# Bind("subdivision_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Productwise Count" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubDiv_count" runat="server" Text='<%# Bind("Sub_Count") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fieldforcewise Count" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubfield_count" runat="server" Text='<%# Bind("SubField_Count") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" ItemStyle-Width="140px" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True"></asp:CommandField>

                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="SubDivisionCreation.aspx?Subdivision_Code={0}"
                                                DataNavigateUrlFields="Subdivision_Code"></asp:HyperLinkField>

                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="140px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Subdivision_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the sub division');">Deactivate
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../Images/deact2.png" alt=""width="75px" title="This Subdivision Name Exists in Product or Fieldforce" />
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="no-result-area" VerticalAlign="Middle" />

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
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
