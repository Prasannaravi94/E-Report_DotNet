<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptJoin_LeftDetails.aspx.cs" Inherits="MIS_Reports_rptJoin_LeftDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <%--<link type="text/css" rel="Stylesheet" href="../../css/Report.css" />--%>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
        rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td></td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td style="padding-right: 30px">
                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" Visible="false">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                    </td>
                                    <td style="padding-right: 15px">
                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" Visible="false">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                    </td>
                                    <td style="padding-right: 40px">
                                        <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" Visible="false">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label1" runat="server" Text="Pdf" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                    </td>
                                    <td style="padding-right: 40px">
                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />

            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">
                            <div align="center">
                                <asp:Label ID="lblJoining" CssClass="reportheader" runat="server"></asp:Label>
                            </div>
                            <br />
                            <br />
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="grdJoining" runat="server" Width="100%" HorizontalAlign="Center" GridLines="None"
                                        CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                                        CssClass="table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-Width="20"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Code" ItemStyle-Width="40"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployee_Code" runat="server" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForceName" ItemStyle-Width="240px"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFieldForceName" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Design" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("sf_designation_short_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date of Join" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate_of_Join" runat="server" Text='<%# Bind("sf_joining_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DCR StartDate" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDCR_StartDate" runat="server" Text='<%# Bind("sf_tp_dcr_active_dt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID Created Date" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreated_ID" runat="server" Text='<%# Bind("Created_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Division" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDivision" runat="server" Text='<%# Bind("Division_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>

                            <br />
                            <br />
                            <div align="center">
                                <asp:Label ID="lblLeft" CssClass="reportheader" runat="server"></asp:Label>
                            </div>

                            <br />
                            <br />
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="grdLeft" runat="server" Width="100%" HorizontalAlign="Center" GridLines="None"
                                        CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                                        CssClass="table">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-Width="20"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Code" ItemStyle-Width="40"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployee_Code" runat="server" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForceName" ItemStyle-Width="240"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFieldForceName" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Design" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("sf_designation_short_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date of Left" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate_of_Join" runat="server" Text='<%# Bind("Left_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DCR Last Date" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDCR_StartDate" runat="server" Text='<%# Bind("DCR_End_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID Deactived Date" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreated_ID" runat="server" Text='<%# Bind("created_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Division" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDivision" runat="server" Text='<%# Bind("Division_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
		<script type="text/javascript">
        if ('<%= Session["Div_color"]!= null%>' == 'False') {
            document.body.style.backgroundColor = '#e8ebec';
        } else {
            document.body.style.backgroundColor = '<%= Session["Div_color"] %>'
        }
    </script>
    </form>
</body>
</html>
