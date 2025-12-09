<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_Reject_Report.aspx.cs" Inherits="MasterFiles_Reports_TP_Reject_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>DCR View Report</title>
    <%--    <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />--%>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>

    <style>
        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }

        .tbldetail_main {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }

        .tbldetail_Data {
            height: 18px;
        }

        .Holiday {
            color: Red;
            font-size: 9pt;
            font-family: Calibri;
        }

        .NoRecord {
            font-size: 10pt;
            font-weight: bold;
            color: Red;
            background-color: AliceBlue;
        }
    </style>
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
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
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=pnlContents.ClientID %>');
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', '');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
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
                                <asp:Label ID="lblHead" runat="server"
                                    Text="TP Reject View from"
                                    CssClass="reportheader"></asp:Label>
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
                                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="PrintGridData();">
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
                <br />

                <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                       
                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvApproveReject" runat="server" Width="100%" HorizontalAlign="Center"
                                                        CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false" GridLines="None"
                                                        CssClass="table">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" ItemStyle-Width="30"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-Width="200"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblffNAme" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HQ" ItemStyle-Width="100"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation" ItemStyle-Width="70"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mode" ItemStyle-Width="60"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMode" runat="server" Style="text-align: center" Text='<%#Eval("Mode")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Reject/Approve DCR Date" ItemStyle-Width="90" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDCRDate" runat="server" Font-Size="9pt" Text='<%#Eval("Activity_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  --%>
                                                            <asp:TemplateField HeaderText="Reject/Approve/Deviate Release Reason" ItemStyle-Width="200"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Rejection_Reason")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reject/Approve/Deviate Release Date" ItemStyle-Width="90"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRjctAppDate" runat="server" Text='<%#Eval("Rejected_Date")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
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
