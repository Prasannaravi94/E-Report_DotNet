<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActivityMasterList.aspx.cs" Inherits="MasterFiles_ActivityMasterList" %>

<!DOCTYPE html>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Activity - Master</title>

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

    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
                        <h2 class="text-center">Activity - Master</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-7">

                                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Text="Add" Width="60px"  OnClick="btnNew_Click" />
                                          <asp:Button ID="btnReactivate" runat="server" CssClass="resetbutton"
                                                    Text="Reactivation" OnClick="btnReactivate_Onclick" />
                                    </div>
                                </div>
                            </div>
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">

                                    <asp:GridView ID="grdActUpload" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowUpdating="grdActUpload_RowUpdating"
                                        OnRowEditing="grdActUpload_RowEditing" OnRowCommand="grdActUpload_RowCommand" OnPageIndexChanging="grdActUpload_PageIndexChanging"
                                        OnRowCreated="grdActUpload_RowCreated" OnRowCancelingEdit="grdActUpload_RowCancelingEdit"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                        AllowSorting="True" OnSorting="grdActUpload_Sorting">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdActUpload.PageIndex * grdActUpload.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Activity_ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivity_ID" runat="server" Text='<%#Eval("Activity_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Activity_S_Name"  HeaderText="Short Name" ItemStyle-HorizontalAlign="Left" >
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtShortName" CssClass="input" Width="100%" runat="server" MaxLength="10" Text='<%# Bind("Activity_S_Name") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Activity_S_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="subdivision_name" HeaderText=" Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtActivity_Name" CssClass="input" Width="100%" runat="server" MaxLength="100" onkeypress="CharactersOnly(event);"
                                                        Text='<%# Bind("Activity_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivity_Name" runat="server" Text='<%# Bind("Activity_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" ItemStyle-Width="140px" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True"></asp:CommandField>

                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="ActivityMasterCreation.aspx?Activity_ID={0}"
                                                DataNavigateUrlFields="Activity_ID"></asp:HyperLinkField>

                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="140px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Activity_ID") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Activity Master');">Deactivate
                                                    </asp:LinkButton>
                                           
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
