<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptVacantBlockId.aspx.cs" Inherits="MasterFiles_Reports_rptVacantBlockId" %>

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


    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <%--    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Report.css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
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
     <style type="text/css">
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlbutton" runat="server">
            <br />
            <table width="100%">
                <tr>
                    <td></td>
                    <td></td>
                    <%--  <td width="80%" align="center">
                        <asp:Label ID="lbldiv" runat="server" Font-Underline="true" Text="Divison Name" Font-Size="18px" ForeColor="#794044" Font-Bold="true"></asp:Label>
                    </td>--%>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>

                                </td>
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

                                </td>

                                <td style="padding-right: 50px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" OnClick="btnClose_Click">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        </asp:Panel>
        <div>

             <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server" Width="100%">
                            <div align="center">
                                <asp:Label ID="lbldiv" runat="server" Text="Divison Name" CssClass="reportheader"></asp:Label>
                                <br />
                                <br />
                            </div>
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <div align="center">
                                        <asp:Label ID="lblvac" runat="server" Font-Size="14px" Text="Hold ID's " CssClass="reportheader"></asp:Label>
                                    </div>
                                    <br />
                                    <asp:GridView ID="grdvac" runat="server" HorizontalAlign="Center" Width="100%" EmptyDataText="No Records Found" OnRowDataBound="grdvac_Rowdatabound"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt"
                                        AllowSorting="True">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="sf_Block_ID" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_Block_ID" runat="server" Text='<%#Eval("sf_Block_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="sf_code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfCode" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Id" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempid" runat="server" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblforce" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesig" runat="server" Text='<%# Bind("sf_designation_short_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhq" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reason for Hold" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblblog" runat="server" Text='<%# Bind("sf_block_reason") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Hold Start Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstartdate" runat="server" Text='<%# Bind("sf_block_start_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Hold End Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblenddate" runat="server" Text='<%# Bind("sf_block_end_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reporting Manager" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblreport" runat="server" Text='<%# Bind("Reporting_ManagerName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="sf_block_flag" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblflag" runat="server" Text='<%# Bind("sf_block_flag") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <br />
                                    <div align="center">
                                        <asp:Label ID="lblblock" runat="server" Font-Size="14px" Text="Blocked ID's " CssClass="reportheader"></asp:Label>
                                    </div>
                                    <br />
                                    <asp:GridView ID="grdblock" runat="server" HorizontalAlign="Center" Width="100%" EmptyDataText="No Records Found" OnRowDataBound="grdblock_Rowdatabound"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt"
                                        AllowSorting="True">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo2" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="sf_Block_ID" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_Block_ID2" runat="server" Text='<%#Eval("sf_Block_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="sf_code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfCode2" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Id" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempid2" runat="server" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblforce2" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesig2" runat="server" Text='<%# Bind("sf_designation_short_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhq2" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reason for Block" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblblog2" runat="server" Text='<%# Bind("sf_block_reason") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Block Start Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstartdate2" runat="server" Text='<%# Bind("sf_block_start_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Block End Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblenddate2" runat="server" Text='<%# Bind("sf_block_end_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reporting Manager" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblreport2" runat="server" Text='<%# Bind("Reporting_ManagerName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus2" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="sf_block_flag" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblflag2" runat="server" Text='<%# Bind("sf_block_flag") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <br />
                                    <div align="center">
                                        <asp:Label ID="Label1" runat="server" Font-Size="14px" Text="Vacant ID's " CssClass="reportheader"></asp:Label>
                                    </div>
                                    <br />
                                    <asp:GridView ID="grdvacant" runat="server" HorizontalAlign="Center" Width="100%" EmptyDataText="No Records Found" OnRowDataBound="grdvacant_Rowdatabound"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt"
                                        AllowSorting="True">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNov" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Id" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemp_id" runat="server" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblforcev" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesigv" runat="server" Text='<%# Bind("sf_designation_short_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhqv" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Resigned Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblresigned_date" runat="server" Text='<%#Eval("Vacant_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reporting Manager" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblreportv" runat="server" Text='<%# Bind("Reporting_To_SF_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Active Flag" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblflagv" runat="server" Text='<%# Bind("Sf_TP_Active_Flag") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatusv" runat="server"></asp:Label>
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

