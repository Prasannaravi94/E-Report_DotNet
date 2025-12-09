<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDCRCountStateView.aspx.cs" Inherits="MasterFiles_Reports_DCRCount_frmDCRCountStateView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Count State Wise</title>
    <%--  <link type="text/css" rel="stylesheet" href="../../../css/style.css" />--%>
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <style type="text/css">
        .single-des [type="checkbox"]:not(:checked) + label, .single-des [type="checkbox"]:checked + label {
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div id="Divid" runat="server"></div>

        </div>
        <br />
        <br />
        <div class="container home-section-main-body position-relative clearfix">
            <br />
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <h2 class="text-center" id="heading" runat="server"></h2>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblDivision" runat="server" Text="Division Name" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                            <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                        </div>
                        <%--          <div class="single-des clearfix">
                            <asp:Label ID="lblMoth" runat="server" Text="Month" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="single-des clearfix">
                            <asp:Label ID="lblYear" runat="server" Text="To Year" CssClass="label"></asp:Label>
                            <span style="margin-left: 14px"></span>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>

                        <div class="single-des clearfix">
                            <asp:Label ID="lblWOVacant" Text="With Vacants" runat="server" CssClass="label"></asp:Label>
                            <asp:CheckBox ID="chkWOVacant" runat="server" Text="." />
                        </div>

                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="View"
                            CssClass="savebutton" OnClick="btnSubmit_Click" />
                    </div>

                    <%--  <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click"  />--%>

                    <br />
                    <br />
                    <div align="center">
                        <asp:Label ID="lblHead" runat="server" CssClass="reportheader">
                        </asp:Label>
                    </div>
                    <br />
                    <br />
                    <div class="display-table clearfix">
                        <div class="table-responsive">
                            <asp:GridView ID="GvDcrCount" runat="server" AutoGenerateColumns="false" Width="100%" ShowFooter="true"
                                CssClass="table" GridLines="None" OnRowDataBound="grdExpense_RowDataBound">

                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="State Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStateCode" ForeColor="Red" runat="server" Text='<%# Bind("state_code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Vacant" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVacant" ForeColor="Red" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="State Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStateName" runat="server" Text='<%# Bind("statename") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" Style="color: Red; font-weight: bold" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tour Month" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("IMonth") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tour Year" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="server" Text='<%# Bind("IYear") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DivCode" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiv_Code" runat="server" Text='<%# Bind("Div_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FieldForce Count"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFieldCount" runat="server" Text='<%# Eval("FieldForce_Count") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FlblFieldCount" Style="color: Red; font-weight: bold" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DCR Count"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hypDCRCount" Target="_blank" runat="server" Text='<%# Eval("DCR_Count") %>' NavigateUrl='<%# String.Format("rptDCRCountStateView.aspx?state_code={0}&Month={1}&Year={2}&Div_Code={3}&Vacant={4}&StateName={5}",Eval("state_code"), Eval("IMonth"), Eval("IYear"),Eval("Div_Code"),Eval("Vacant"),Eval("statename")) %>'>
                          
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCount" Style="color: Red; font-weight: bold" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
            <img src="../../../Images/loader.gif" alt="" />
        </div>
        <script type="text/javascript" src="../../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=txtMonthYear]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "M-yyyy",
                    viewMode: "months",
                    minViewMode: "months",
                    language: "tr"
                });
            });
        </script>
    </form>
</body>
</html>
