<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDCRNotSubmit.aspx.cs"
    Inherits="Reports_rptDCRNotSubmit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Not Submit</title>
    <%-- <link id="Link1" type="text/css" runat="server" rel="stylesheet" href="../../css/Report.css" />--%>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {

                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=GrdDCRSubmit]').footable();
        });
    </script>

    <style type="text/css">
        /*Fixed Heading & Fixed Column-Begin*/
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .display-reportMaintable .table th:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 0;
        }

        .display-reportMaintable .table th:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 33px;
            /*background: inherit;*/
            z-index: 2;
            min-width: 158px;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            /*background-color: white;*/
            background: inherit;
            left: 33px;
        }

        .display-reportMaintable .table th:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 190px;
            /*background: inherit;*/
            z-index: 2;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            /*background-color: white;*/
            background: inherit;
            left: 190px;
        }
        /*Fixed Heading & Fixed Column-End*/
        .display-reportMaintable .table td {
            border-color: #DCE2E8;
            border-right: none;
        }
    </style>
</head>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <div>
            <br />
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <div class="row justify-content-center">
                        <div class="col-lg-9">
                        </div>
                        <div class="col-lg-3">
                            <table width="100%">
                                <tr>
                                    <td></td>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td style="padding-right: 30px">
                                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>

                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label3" runat="server" Text="PDF" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>

                                                </td>
                                                <td style="padding-right: 50px">
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
                        </div>
                    </div>
                </div>

                <div class="container clearfix" style="max-width: 1350px;">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">

                            <asp:Panel ID="pnlContents" runat="server">
                                <div align="center">
                                    <asp:Label ID="lblHead" runat="server" Text="Not Submitted DCR View for the month of "
                                        CssClass="reportheader"></asp:Label>
                                </div>
                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                        <center>
                                            <asp:Table ID="tblworktype" runat="server" Width="95%">
                                            </asp:Table>
                                            <asp:Label ID="lblNoRecord" runat="server"
                                                Visible="false" CssClass="no-result-area">No Records Found</asp:Label>

                                            <asp:GridView ID="GrdDCRSubmit" runat="server" Width="100%" HorizontalAlign="Center" Style="background-color: white"
                                                AutoGenerateColumns="false" PageSize="10" EmptyDataText="No Records Found" GridLines="Both" BorderColor="WhiteSmoke" BorderWidth="1"
                                                CssClass="table" AlternatingRowStyle-CssClass="alt" OnRowDataBound="GrdDoctor_OnRowDataBound">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <%--<ControlStyle Width="10%"></ControlStyle>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left"
                                                        Visible="false">
                                                        <ControlStyle Width="20%"></ControlStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSfCode" runat="server" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ControlStyle Width="180px"></ControlStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSf" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ControlStyle Width="110px"></ControlStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSf_HQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Design" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ControlStyle Width="50px"></ControlStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesig_color" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Joining_Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ControlStyle Width="70px"></ControlStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDOJ" runat="server" Text='<%# Bind("sf_joining_date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Resigned_Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ControlStyle Width="70px"></ControlStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblResigned_Date" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Desig_Color" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ControlStyle Width="40px"></ControlStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesigColor" runat="server" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Last DCR Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ControlStyle Width="80px"></ControlStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLDD" runat="server" Text='<%# Bind("Last_DCR_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reporting_Manager1" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ControlStyle Width="150px"></ControlStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReporting_Manager1" runat="server" Text='<%# Bind("Reporting_Manager1") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reporting_Manager2" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ControlStyle Width="158px"></ControlStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReporting_Manager2" runat="server" Text='<%# Bind("Reporting_Manager2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Not Submitted DCR Dates" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <%-- <ControlStyle Width="500px"></ControlStyle>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNSD" runat="server" Text='<%# Bind("DCR_Not_Submit_Days") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Not Submitted Count" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ControlStyle Width="40px"></ControlStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNSC" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </center>

                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
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
