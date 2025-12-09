<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDCRCountStateView.aspx.cs" Inherits="MasterFiles_Reports_DCRCount_rptDCRCountStateView" %>

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
    <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../../assets/css/style.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', '');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>

            <br />
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <div class="row justify-content-center">
                        <div class="col-lg-9">
                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="DCR Count View - State Wise Report for the Period " CssClass="reportheader"></asp:Label>
                                <br />
                                <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" Font-Size="18px" ForeColor="#696d6e"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <table width="100%">
                                <tr>
                                    <td></td>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td style="padding-right: 30px">
                                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>

                                                <td style="padding-right: 50px">
                                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <br />

                <div class="container clearfix" style="max-width: 1350px;">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <asp:Panel ID="pnlContents" runat="server">

                                <br />

                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                            Width="95%">
                                        </asp:Table>
                                        <asp:GridView ID="GvDcrCount" runat="server" AutoGenerateColumns="false" CssClass="table"
                                            OnRowDataBound="GvDcrCount_RowDataBound" GridLines="None" style="background-color:white">

                                            <Columns>

                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DCR Count" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDCR_Count" runat="server" Text='<%# Bind("DCR_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40"
                                                    Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSfcode" runat="server" Visible="false" Text='<%# Bind("sf_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Field Force Name" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="40">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSf_Name" runat="server" Text='<%# Bind("sf_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="40">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSf_Hq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Design" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="40">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesig" runat="server" Text='<%# Bind("sf_designation_short_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Employee Code" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="40">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesig" runat="server" Text='<%# Bind("Employee_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last DCR Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLast_DCR_Date" runat="server" Text='<%# Bind("last_dcr_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="gvMissingName" runat="server" AutoGenerateColumns="false" CssClass="table" GridLines="None"
                                            OnRowDataBound="OnDataBinding_gvMissingName" style="background-color:white">

                                            <Columns>

                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSfcode" runat="server" Visible="false" Text='<%# Bind("sf_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Field Force Name" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSf_Name" runat="server" Text='<%# Bind("sf_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSf_Hq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesig" runat="server" Text='<%# Bind("sf_designation_short_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Employee Code" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesig1" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last DCR Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLast_DCR_Date" runat="server" Text='<%# Bind("last_dcr_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Flag" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFlag" runat="server" Text='<%# Bind("sf_tp_active_flag") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
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
    </form>
</body>
</html>
