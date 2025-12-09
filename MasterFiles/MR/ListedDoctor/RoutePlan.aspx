<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoutePlan.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_RoutePlan" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Route Plan</title>

    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
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

        #pnlGiftUnlst .display-table .table th:last-child {
            border-radius: 0px 0px 0px 0px;
        }

        #pnlGiftUnlst .display-table .table th:first-child {
            background-color: #F1F5F8;
            color: #636d73;
            border-radius: 0px 0 0 0px;
        }

        #pnlGiftUnlst .display-table .table tr:nth-child(2) td:first-child {
            background-color: #fff;
        }

        #pnlGiftUnlst .display-table .table tr td:first-child {
            border-top: 1px solid #dee2e6;
            text-align: left;
            background-color: #fff;
        }

        .container {
            max-width: 1325px !important;
        }

        #pnlMove .display-table .table th:last-child {
            border-radius: 0px 0px 0px 0px;
        }

        #pnlMove .display-table .table th:first-child {
            background-color: #F1F5F8;
            color: #636d73;
            border-radius: 0px 0 0 0px;
        }

        #pnlMove .display-table .table tr:nth-child(2) td:first-child {
            background-color: #fff;
        }

        #pnlMove .display-table .table tr td:first-child {
            border-top: 1px solid #dee2e6;
            text-align: center;
            background-color: #fff;
        }

        [type="checkbox"]:not(:checked), [type="checkbox"]:checked {
            position: static !important;
            left: -9999px;
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
        <ucl:Menu ID="menu1" runat="server" />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <center>
                        <table>
                            <tr>
                                <td align="center">
                                    <h2 class="text-center">Territory Normal</h2>
                                </td>
                            </tr>
                        </table>
                    </center>
                    <asp:Panel ID="pnlMain" runat="server" Width="100%">
                        <table width="15%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblmon" runat="server" Visible="false" CssClass="label"></asp:Label>
                                    <asp:Label ID="lblHead" runat="server" Text="  Missed Doctor(s)  in Plan" Visible="false" CssClass="label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 20%;">
                                    <asp:Panel ID="pnlGiftUnlst" runat="server" Width="100%">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="designation-reactivation-table-area clearfix">
                                                        <div class="display-table clearfix">
                                                            <div class="table-responsive" style="scrollbar-width: thin; max-height: 500px;">
                                                                <asp:GridView ID="grdTerritory" runat="server" AlternatingRowStyle-CssClass="alt"
                                                                    AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                                    GridLines="None" HorizontalAlign="Center" OnRowCommand="grdTerritory_RowCommand"
                                                                    OnRowDataBound="grdTerritory_RowDataBound" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Territory_Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Route Plan List" HeaderStyle-Width="200px">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkbutTerr" runat="server" CommandArgument='<%# Eval("Territory_Code") %>' CommandName="Territory" Text='<%#Eval("Territory_Name") %>' CssClass="label" ForeColor="#0d5cb0" Font-Bold="true">
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 60%;" valign="top" align="center">
                                    <asp:Label ID="lblContent" runat="server" Style="left: 500px; top: 230px; width: 32%; position: absolute;" Text="Please select any Route Plan from the Route Plan List...." CssClass="label" BackColor="#FFFF99"></asp:Label>
                                    <asp:Panel ID="pnlDoctor" runat="server" Visible="false" Style="left: 220px; width: 50%; position: absolute;">
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblrt" Text="Route Plan Add/Delete/View" Font-Bold="true" Font-Size="14px" ForeColor="Navy" CssClass="label" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <div class="designation-reactivation-table-area clearfix">
                                                        <div class="display-table clearfix">
                                                            <div class="table-responsive" style="scrollbar-width: thin; max-height: 460px;">
                                                                <asp:GridView ID="grdDoctor" runat="server" AlternatingRowStyle-CssClass="alt"
                                                                    AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                                    GridLines="None" HorizontalAlign="Center" OnRowDataBound="grdDoctor_RowDataBound" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remove" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkRemove" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="300" HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="200" HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTerritoryName" runat="server" Text='<%#Eval("Territory_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="200" HeaderText="Plan Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTerritoryCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="200" HeaderText="Plan Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPlanNo" runat="server" Text='<%#Eval("SLVNo")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="200" HeaderText="Color" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblColor" runat="server" Text='<%#Eval("color")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="savebutton" OnClick="btnSubmit_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <%--<td style="width: 15%;">
                                    <asp:Panel ID="pnlImgCopyMove" runat="server" BorderStyle="Solid" BorderWidth="1" Visible="false" Style="left: 850px; top: 150px; position: absolute;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="imgCopyMove" runat="server" ImageAlign="Middle"
                                                        ImageUrl="~/Images/arrowIcon.jpg" Width="25" OnClick="imgCopyMove_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>--%>
                                <td style="width: 20%;">
                                    <asp:Panel ID="pnlMove" runat="server" Visible="false" Style="left: 885px; top: 59px; position: absolute; width: 430px;">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <div class="designation-area clearfix">
                                                        <div class="single-des clearfix">
                                                            <asp:Label ID="lblTerr" runat="server" Text="Route Plan" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlTerritory" runat="server" AutoPostBack="true" CssClass="nice-select"
                                                                OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged" Width="100%">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="designation-reactivation-table-area clearfix">
                                                        <div class="display-table clearfix">
                                                            <div class="table-responsive" style="scrollbar-width: thin; max-height: 415px;">
                                                                <asp:GridView ID="GrdCopyMove" runat="server" AlternatingRowStyle-CssClass="alt"
                                                                    AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                                    GridLines="None" HorizontalAlign="Center" OnRowDataBound="GrdCopyMove_RowDataBound" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkCopyMove" runat="server" AutoPostBack="true" OnCheckedChanged="chkCopyMove_CheckedChanged" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDocCode_CopyMove" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="300" HeaderText="Listed Doctor Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDocName_CopyMove" runat="server" Text='<%#Eval("ListedDr_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="200" HeaderText="Plan Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTerritoryName_CopyMove" runat="server" Text='<%#Eval("Territory_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="200" HeaderText="Plan Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPlanNo_CopyMove" runat="server" Text='<%#Eval("SLVNo")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
