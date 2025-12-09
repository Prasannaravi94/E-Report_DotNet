<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActivityUploadReact.aspx.cs" Inherits="MasterFiles_ActivityUploadReact" %>

<!DOCTYPE html>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor Category Reactivation</title>
         <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
     <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
        <style type="text/css">
            .modal
    {
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
    .loading
    {
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
            <div class="row justify-content-center">
                <div class="col-lg-11">
                    <br />
                    <h2 class="text-center">Activity Reactivation</h2>
                    <div class="display-table clearfix">
                        <div class="table-responsive">
                            <asp:GridView ID="grdActUpload" runat="server" Width="70%" HorizontalAlign="Center" 
                                AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"                     
                                onpageindexchanging="grdActUpload_PageIndexChanging" onrowcommand="grdActUpload_RowCommand"                 
                                GridLines="None" CssClass="table" PagerStyle-CssClass="pgr" 
                                AlternatingRowStyle-CssClass="alt" AllowSorting="True" >
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activity_ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivity_ID" runat="server" Text='<%#Eval("Activity_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtActivity_S_Name" runat="server" CssClass="input" Text='<%# Bind("Activity_S_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivity_S_Name" runat="server" Text='<%# Bind("Activity_S_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtActivity_Name" CssClass="input" runat="server" MaxLength="100" Text='<%# Bind("Activity_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivity_Name" runat="server" Text='<%# Bind("Activity_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                      
                                    <asp:TemplateField HeaderText="Reactivate" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Activity_ID") %>'
                                                CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Activity');">Reactivate
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="no-result-area"/>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
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
