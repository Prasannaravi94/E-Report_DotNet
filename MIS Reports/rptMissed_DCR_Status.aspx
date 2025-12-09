<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptMissed_DCR_Status.aspx.cs"
    Inherits="MIS_Reports_rptMissed_DCR_Status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Missed Status</title>
<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <%--<link id="Link1" type="text/css" runat="server" rel="stylesheet" href="../../css/Report.css" />--%>
    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <%--    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
        rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>




    <script type="text/javascript">
        $(function () {
            $('[id*=GrdDCRDelayed]').footable();
        });
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
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .alingment {
            word-break: break-all;
        }

        .display-reportMaintable .table td {
            border-color: #DCE2E8;
            border-right: none;
        }
    </style>
</head>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
    <div>
                    <asp:Panel ID="pnlbutton" runat="server">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="row justify-content-center">
                            <div class="col-lg-9">
                            </div>
                            <div class="col-lg-3">
                                <table width="100%">
                                    <tr>
                                        <td></td>
                                          <%--<td align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Delayed Status for the month of " ForeColor="#794044"
                                Font-Underline="True" Font-Size="14px" Font-Bold="True"></asp:Label>
                        </td>--%>
                                        <td align="right">
                                            <table>
                                                <tr>
                                                    <td style="padding-right: 30px">
                                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="25px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>
                                                    <td style="padding-right: 15px">
                                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="25px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>
                                                    <td style="padding-right: 40px">
                                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="25px" Style="border-width: 0px;" />
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
            </asp:Panel>
        <br />
        <div class="container clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">
                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="Delayed Status for the month of " CssClass="reportheader"></asp:Label>
                            </div>
            <%-- <center>
                <table border="0" width="90%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Delayed Status for the month of "
                                Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>--%>
            <%--<asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" Style="border-collapse: collapse;
                border: solid 1px Black;" GridLines="Both" Width="95%">
            </asp:Table>--%>
            <center>
                <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                <asp:Table ID="tblworktype" runat="server" Width="95%">
                </asp:Table>
                <asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue"
                    Visible="false" Height="20px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2"
                    Font-Bold="True">No Records Found</asp:Label>
                <asp:GridView ID="GrdDCRDelayed" runat="server" Width="100%" HorizontalAlign="Center" Style="background-color: white"
                                        AutoGenerateColumns="false" PageSize="10" EmptyDataText="No Records Found" GridLines="Both" BorderColor="WhiteSmoke" BorderWidth="1"
                                        CssClass="table" AlternatingRowStyle-CssClass="alt" OnRowDataBound="GrdDoctor_OnRowDataBound" HeaderStyle-CssClass="stickyFirstRow">
                    <Columns>
                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#336277"
                            HeaderStyle-Font-Size="12px" HeaderStyle-Height="30px" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="10%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Font-Size="10px" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#336277"
                            HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="White" Visible="false">
                            <ControlStyle Width="20%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSfCode" runat="server" Font-Size="10px" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Size="12px"
                            HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="80px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblEmp_Code" runat="server" Font-Size="10px" Text='<%# Bind("UsrDfd_UserName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-Font-Size="12px" HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="180px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSf" runat="server" Font-Size="10px" Text='<%# Bind("sf_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Size="12px"
                            HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="110px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSf_HQ" runat="server" Font-Size="10px" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="12px"
                            HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="50px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDesig_color" runat="server" Font-Size="10px" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Joining_Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="12px"
                            HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="70px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" runat="server" Font-Size="10px" Text='<%# Bind("sf_joining_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resigned_Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Size="12px"
                            HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="70px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblResigned_Date" Font-Size="10px" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Desig_Color" ItemStyle-HorizontalAlign="Center" Visible="false"
                            HeaderStyle-Font-Size="12px" HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="40px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDesigColor" runat="server" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last DCR Date" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Font-Size="12px" HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="80px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblLDD" runat="server" Font-Size="10px" Text='<%# Bind("Last_DCR_Date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reporting_Manager1" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-Font-Size="12px" HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="150px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblReporting_Manager1" runat="server" Font-Size="10px" Text='<%# Bind("Reporting_Manager1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reporting_Manager2" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-Font-Size="12px" HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="158px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblReporting_Manager2" runat="server" Font-Size="10px" Text='<%# Bind("Reporting_Manager2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Missed Not Released Dates" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-Font-Size="12px" HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="380px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNSD" runat="server" Font-Size="10px" Text='<%# Bind("DCR_Missed_Dates") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Missed Release Dates" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-Font-Size="12px" HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="370px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNSD_Rel" runat="server" Font-Size="10px" Text='<%# Bind("Missed_Dates_Release") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Submitted Dates" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-Font-Size="12px" HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="370px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNSD_Re_entry" runat="server" Font-Size="10px" Text='<%# Bind("Missed_Dates_Release_entry") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle CssClass="no-result-area" />
                </asp:GridView>
                                      </div>
                            </div>
            </center>
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
