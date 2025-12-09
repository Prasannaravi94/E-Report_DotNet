<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_View.aspx.cs" Inherits="MasterFiles_MR_RptAutoExpense" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Statement</title>
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <style type="text/css">
        table {
            border-collapse: collapse;
        }

        .mainDiv {
            background-color: White;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .gridheight {
            overflow-y: auto !important;
            height: 500px !important;
        }
    </style>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td width="80%"></td>
                <td align="right">
                    <table>
                        <tr>
                            <td style="padding-right: 50px">
                                <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                </asp:LinkButton>
                                <p>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-6">
                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <center>
                            <font size="3" face="Verdana, Arial, Helvetica, sans-serif"><b><u>Territory View For The HQ : <span id="hqId" style="color:red;" runat="server"></span></u></b></font>
                        </center>
                        <br />
                    </asp:Panel>
                    <br />
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-table clearfix">
                            <div class="table-responsive">
                                <center>
                                    <asp:GridView ID="grdExpMain" runat="server" Width="85%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                        CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdExpMain.PageIndex * grdExpMain.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TerritoryName" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ADate" runat="server" Text='<%# Bind("territory_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDayName" runat="server" Text='<%# Bind("Town_Cat") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
