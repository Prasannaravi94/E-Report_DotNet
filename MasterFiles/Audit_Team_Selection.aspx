<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Audit_Team_Selection.aspx.cs" Inherits="MasterFiles_Audit_Team_Selection" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Audit Team Selection</title>
  
    <link href="../assets/css/Calender_CheckBox.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
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
    <style type="text/css">
        .div_fixed {
            position: fixed;
            top: 400px;
            right: 5px;
        }
        .display-table .table tr td:first-child
        {  color: #636d73;}
        [type="checkbox"]:not(:checked) + label, [type="checkbox"]:checked + label
         {
  padding-left: 0em;
         }
    </style>


</head>
<body>
    <form id="form2" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <br />
                    <div class="col-lg-11">
                        <div class="designation-reactivation-table-area clearfix">
                            <h2 class="text-center"><asp:Label ID="lblTitle" runat="server" Text="Label" ></asp:Label></h2>
                            <div class="display-table clearfix">
                                <div class="table-responsive">

                                    <asp:UpdatePanel ID="Upl" runat="server">
                                        <ContentTemplate>

                                            <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                                AutoGenerateColumns="false" GridLines="None"
                                                OnRowDataBound="grdSalesForce_RowDataBound" CssClass="table">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                        <asp:CheckBox ID="chksf" runat="server" AutoPostBack="true" OnCheckedChanged="chksf_CheckedChanged" Text="."/>                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sf Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsfCode" runat="server" Text='<%#Eval("sf_Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Field Force">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSfName" runat="server" Text='<%#Eval("sf_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HQ">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("sf_hq")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reporting Manager">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("Reporting_To_SF") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Color" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBackColor" runat="server" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSFType" runat="server" Text='<%# Bind("sf_Type") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="div_fixed">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Update" Visible="false" OnClick="btnSubmit_Click" />
                    </div>
                </div>
                <asp:Button ID="btnback" runat="server" Text="Back" CssClass="backbutton" OnClick="btnback_Click" />

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
