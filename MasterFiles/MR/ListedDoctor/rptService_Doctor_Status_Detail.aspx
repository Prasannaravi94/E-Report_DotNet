<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptService_Doctor_Status_Detail.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_rptService_Doctor_Status_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>CRM Status</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../../assets/css/style.css" />
    <link rel="stylesheet" href="../../../assets/css/responsive.css" />
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->


    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', '');//height=400,width=800
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
    <script type="text/javascript">
        $(document).ready(function () {
            //$('#GrdFixation tr:last-child').each(function () {
            //    //tr:last-child { color:red; }
            //    $(this).children('td').css('color', 'red');
            //    $(this).children('td').css('border-color', 'black');
            //    $(this).children('td').css('font-size', '16px');
            //});
        });
    </script>
    <style type="text/css">
        .display-reportMaintable .table tr:first-child td:first-child {
            border-radius: 8px 0 0 8px;
            background-color: #414d55;
            color: #ffffff;
            font-size: 18px;
            font-weight: 400;
            border-left: 0px solid #F1F5F8;
        }

        .display-reportMaintable .table tr:first-child td {
            background-color: #F1F5F8;
            padding: 20px 10px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
        }

        .display-reportMaintable .table tr.no-result-area td:first-child {
            border: solid 1px #d1e2ea;
            text-align: center;
            padding: 10px;
            color: #696d6e;
            font-size: 18px;
            font-weight: 500;
            text-transform: none;
            background-color: transparent;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <div>
                <center>
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
                                            <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>

                                        </td>
                                        <td style="padding-right: 15px">
                                            <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                            </asp:LinkButton>
                                            <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

                                        </td>

                                        <td style="padding-right: 100px">
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
                    <br />
                    <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                        <div class="row justify-content-center">
                            <div class="col-lg-12">
                                <asp:Panel ID="pnlContents" runat="server">
                                    <div align="center">
                                        <asp:Label ID="lblHead" runat="server" Text="Doctor List"
                                            CssClass="reportheader"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" ForeColor="#696d6e" ></asp:Label>
                                        <br />

                                        <asp:Label ID="lblstock" runat="server" CssClass="reportheader" ForeColor="#696d6e" ></asp:Label>
                                    </div>
                                    <br />

                                    <div class="display-reportMaintable clearfix">
                                        <div class="table-responsive" style="scrollbar-width: thin;">

                                            <asp:Panel ID="Panel1" runat="server">
                                                <table width="95%">
                                                    <caption>
                                                        <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                                            AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                                            GridLines="None" HorizontalAlign="Center" OnRowCreated="GrdFixation_RowCreated"
                                                            ShowHeader="False" Width="100%" OnRowDataBound="GrdFixation_RowDataBound">

                                                            <PagerStyle CssClass="gridview1" />

                                                            <Columns>
                                                            </Columns>
                                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                                        </asp:GridView>
                                                    </caption>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                </center>
            </div>
        </div>
    </form>
</body>
</html>
