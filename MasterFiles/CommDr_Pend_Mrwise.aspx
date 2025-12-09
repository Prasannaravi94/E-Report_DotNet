<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommDr_Pend_Mrwise.aspx.cs"
    Inherits="MasterFiles_CommDr_Pend_Mrwise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Dr Approval - Pending Count</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <%--<link type="text/css" rel="Stylesheet" href="../../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />--%>

    <%--<link type="text/css" rel="stylesheet" href="../css/style.css"/>--%>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#Panel1').html())
                location.href = url
                return false
            })
        })
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <h2 class="text-center">Listed Dr Approval - Pending Count </h2>
                        <div class="row ">
                            <div class="col-lg-12">
                                <asp:Panel ID="pnlexcel" runat="server" CssClass="panelmarright">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="30px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                </asp:Panel>
                            </div>
                        </div>



                    </div>

                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:GridView ID="grdPenDr" runat="server" Width="100%" ShowFooter="true"
                                    HorizontalAlign="Center" EmptyDataText="No Records Found" OnRowDataBound="grdPenDr_Rowdatabound"
                                    AutoGenerateColumns="false" GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt"
                                    AllowSorting="True">
                                    <HeaderStyle Font-Bold="False" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reporting Manager 1" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblreportmg1" runat="server" Text='<%#Eval("Reporting_To_Manager1")%>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:Label ID="lblTot" Text="Total" Font-Bold="true" ForeColor="Red" runat="server" />

                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Reporting Manager 2" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblreportmg2" runat="server" Text='<%#Eval("Reporting_To_Manager2")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_name" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desigantion" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                            ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("Designation_Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                            ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhq" runat="server" Text='<%#Eval("sf_hq")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="State" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                            ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstate" runat="server" Text='<%#Eval("StateName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total <br> Dr Count" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                            ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbtot_count" runat="server" Text='<%#Eval("Tot_ListDr")%>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:Label ID="lblTot_dr" Font-Bold="true" ForeColor="Red" runat="server" />

                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Exisiting Dr <br> Approval Count" ItemStyle-HorizontalAlign="center"
                                            HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpend_exist" runat="server" Text='<%#Eval("Exisiting_Dr_Approval_Count")%>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:Label ID="lblExisiting_Dr" Font-Bold="true" ForeColor="Red" runat="server" />

                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="New Dr <br> Approval Count" ItemStyle-HorizontalAlign="center"
                                            HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpend_new" runat="server" Text='<%#Eval("New_Dr_Approval_Count")%>'></asp:Label>
                                            </ItemTemplate>


                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:Label ID="lblNew_Dr" Font-Bold="true" ForeColor="Red" runat="server" />

                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total <br> Approval Count" ItemStyle-HorizontalAlign="center"
                                            HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpend_tot" runat="server" Text='<%#Eval("total_appr_pen_count")%>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:Label ID="lbltot_appcnt" Font-Bold="true" ForeColor="Red" runat="server" />

                                                </div>
                                            </FooterTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="sf_code" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                            Visible="false" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_code" runat="server" Text='<%#Eval("sf_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
