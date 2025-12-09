<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDr_Approval_Pending.aspx.cs" Inherits="MasterFiles_MR_ListedDr_Approval_Pending" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor's - Pending Approval View</title>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/MR.css" />
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
            .bgClr{        
        background-color:Green;
    }
    </style>--%>
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
        $(function () {
            $(".btn").click(function () {
                $('.btn').removeClass('bgClr');
                $(this).addClass('bgClr');
            });
        });
    </script>
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
                        <h2 class="text-center">
                            <asp:Panel ID="pnlList" runat="server" HorizontalAlign="Center">
                                <asp:Label ID="lblADD" runat="server" Text="Listed Dr Additional Approval Pending List" Visible="false"></asp:Label>
                                <asp:Label ID="lblDeAct" runat="server" Text="Listed Dr Deactivation Approval Pending List" Visible="false"></asp:Label>
                                <asp:Label ID="lblReject" runat="server" Text="Approval - Rejection List" Visible="false"></asp:Label>
                            </asp:Panel>
                        </h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="row justify-content-center">
                                    <div class="col-lg-4">
                                        <asp:Button ID="btnAdd_Pending" runat="server" Width="100%"
                                            Text="Listed Dr Addition - Approval Pending"
                                            OnClick="btnAdd_Pending_Click" CssClass="savebutton" />
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Button ID="btnDeact_Pending" runat="server" Width="100%"
                                            Text="Listed Dr Deactivation - Approval Pending"
                                            OnClick="btnDeact_Pending_Click" CssClass="savebutton" />
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Button ID="btnReject" runat="server" Text="Approval - Rejection List"
                                            OnClick="btnReject_Click" Width="100%" CssClass="savebutton" />
                                    </div>
                                </div>
                            </div>
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                        AutoGenerateColumns="false" OnRowDataBound="grdDoctor_RowDataBound"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt"
                                        AllowSorting="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDocName" SkinID="MandTxtBox" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDocCat" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Category() %>" DataTextField="Doc_Cat_Name" DataValueField="Doc_Cat_Code">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDocSpec" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Speciality() %>" DataTextField="Doc_Special_Name" DataValueField="Doc_Special_Code">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQl" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDocQua" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Qualification() %>" DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDocClass" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Class() %>" DataTextField="Doc_ClsName" DataValueField="Doc_ClsCode">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlterr" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Territory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="loading" align="center">
                    Loading. Please wait.<br />
                    <br />
                    <img src="../../Images/loader.gif" alt="" />
                </div>
            </div>
    </form>
</body>
</html>
