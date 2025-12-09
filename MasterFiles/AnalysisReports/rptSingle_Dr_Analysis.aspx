<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptSingle_Dr_Analysis.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_rptSingle_Dr_Analysis" %>

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
    <style type="text/css">
        .clp {
            border-collapse: collapse;
        }

            .clp tr td {
                border: none;
                /*border-color: #DCE2E8;*/
                font-size: 14px;
                background-color: #f7f9fa;
                padding: 10px;
            }

                .clp tr td:nth-child(2n+1), .display-table .table tr:first-child td {
                    background-color: #414d55;
                    color: white;
                }

                .clp tr td:nth-child(2n+2) {
                    color: #0077FF;
                }


        .clp1 tr td, .clp1 tr {
            border-color: #DCE2E8;
            font-size: 10pt;
        }

            .clp1 tr:first-child td {
                border-style: solid;
                border-width: 1px;
            }


        .display-table .table tr:nth-child(2) td, .display-table .table tr:nth-child(2) td:first-child {
            background-color: #bad4f5;
            color: #292a34;
        }

        .display-table .table tr td, .display-table .table tr:nth-child(4) td:first-child {
            background-color: white;
            border-bottom: 1px solid #DCE2E8;
        }

            .display-table .table tr td:first-child {
                border: 1px solid #DCE2E8;
                border-left: none;
            }
        /*.display-table .table tr:first-child td:first-child 
        {
            border-radius: 8px 0 0 0px;
        }
        .display-table .table tr:first-child td:last-child
        {
             border-radius: 0px 8px 0 0px;
        }*/

        /*.display-Approvaltable .table tr:first-child td:first-child {
            background-color: #F1F5F8;
            color: #636d73;
            font-size: 14px;
            border-top: none;
        }

        .display-Approvaltable .table tr:nth-child(2) td:first-child {
            background-color: #F1F5F8;
            border-top: none;
        }

        .display-Approvaltable .table tr td:first-child {
            background-color: white;
            border-top: 1px solid #dee2e6;
        }*/
    </style>
    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row justify-content-center">
                    <div class="col-lg-9">
                        <center>
                            <div align="center">
                                <br />
                                <asp:Label ID="lblHead" runat="server" Text="Coverage Analysis for the month of "
                                    CssClass="reportheader"></asp:Label>
                                <br />
                                <asp:Label ID="LblForceName" runat="server" Font-Bold="True" CssClass="reportheader" Font-Size="18px" ForeColor="#696d6e"></asp:Label>
                            </div>
                        </center>
                    </div>
                    <div class="col-lg-3">
                        <br />
                        <asp:Panel ID="pnlbutton" runat="server">
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
                                                    <asp:Label ID="Label7" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label8" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                                <td style="padding-right: 50px">
                                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label9" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>

            </div>
            <br />
            <br />

            <div class="container clearfix" style="max-width: 1350px; background-color: white; margin-left: 35px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">

                            <br />
                            <center>
                                <div style="overflow-x: auto; scrollbar-width: thin;">
                                    <asp:Panel ID="pnlCount" runat="server">
                                        <asp:Table ID="Table1" runat="server" Width="100%"
                                            CssClass="clp" CellSpacing="3" CellPadding="3" BackColor="White">
                                            <%-- <asp:TableHeaderRow>
                            <asp:TableHeaderCell ColumnSpan="4">
                                <asp:Label ID="lbltrans" runat="server" Text="Listed Doctor Details" Font-Names="Verdana"
                                    Font-Bold="true" Font-Size="12px" ForeColor="DarkGreen"></asp:Label>
                            </asp:TableHeaderCell>
                        </asp:TableHeaderRow>--%>
                                            <asp:TableRow>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblListed" runat="server" Text="Doctor Name:"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblName" runat="server" ForeColor="Green"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblAddr" runat="server" Text="Address:"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblmobile" runat="server" Text="Mobile:"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblmob" runat="server"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblemail" runat="server" Text="Email ID:"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblEm" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblHos" runat="server" Text="Hospital Address:"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblHospital" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblCate" runat="server" Text="Category:"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblCat" runat="server"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblSpec" runat="server" Text="Speciality:"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblSpc" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="Label1" runat="server" Text="Class:"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblcls" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="Label3" runat="server" Text="Qualification:"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblqua" runat="server"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left">
                                                    <asp:Label ID="Label6" runat="server" Text="Campaign Name:" Font-Bold="true"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100px" ColumnSpan="5" Height="35px" HorizontalAlign="Left">
                                                    <asp:Label ID="lblcampaign" runat="server" Font-Bold="true"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:Panel>
                                </div>
                                <br />
                                <hr style="width: 100%;" />
                                <br />
                                <center>
                                    <asp:Label ID="lblvisit" runat="server" Text="Visit Details - Datewise" CssClass="reportheader"></asp:Label>
                                </center>
                                <br />
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="display-table clearfix">
                                        <div class="table-responsive" style="scrollbar-width: thin;">
                                            <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                                AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                                GridLines="None" HorizontalAlign="Center" BorderWidth="0" ShowHeader="False"
                                                Width="100%" OnRowDataBound="GrdFixation_RowDataBound"
                                                OnRowCreated="GrdFixation_RowCreated">

                                                <Columns>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <br />

                                <hr style="width: 100%;" />
                                <br />

                                <center>
                                    <asp:Label ID="Label2" runat="server" Text="Product Detailed and Sampled" CssClass="reportheader"></asp:Label>
                                </center>
                                <br />
                                <div style="scrollbar-width: thin; overflow-x: auto">
                                    <asp:Table ID="tblProduct" runat="server" GridLines="None" CssClass="clp1"
                                        Width="100%">
                                    </asp:Table>
                                </div>
                                <br />

                                <hr style="width: 100%;" />
                                <br />
                                <center>
                                    <asp:Label ID="Label5" runat="server" Text="Input Given" CssClass="reportheader"></asp:Label>
                                </center>
                                <br />
                                <div style="scrollbar-width: thin; overflow-x: auto">
                                    <asp:Table ID="tblInput" runat="server" GridLines="None" CssClass="clp1"
                                        Width="100%">
                                    </asp:Table>
                                </div>
                                <br />

                                <hr style="width: 100%" />
                                <br />
                                <center>
                                    <asp:Label ID="Label4" runat="server" Text="Listed drwise Remarks / Call Feedback"
                                        CssClass="reportheader"></asp:Label>
                                </center>
                                <br />
                                <div style="scrollbar-width: thin; overflow-x: auto">
                                    <asp:Table ID="tblFeed" runat="server" GridLines="None" CssClass="clp1"
                                        Width="100%">
                                    </asp:Table>
                                </div>
                                <br />

                                <hr style="width: 100%" />
                                <br />
                                <center>
                                    <asp:Label ID="lblLst" runat="server" Text="Listed Drs Visit - Productwise" CssClass="reportheader"></asp:Label>
                                </center>
                                <br />
                                <div style="scrollbar-width: thin; overflow-x: auto">
                                    <asp:Table ID="tblLstProd" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="none" CssClass="clp1"
                                        Width="100%">
                                    </asp:Table>
                                </div>
                                <br />

                                <hr style="width: 100%" />
                                <br />
                                <center>
                                    <asp:Label ID="lblChemist" runat="server" Text="Supportive Chemist" CssClass="reportheader"></asp:Label>
                                </center>
                                <br />
                                <div style="scrollbar-width: thin; overflow-x: auto">
                                    <asp:Table ID="tblChemist" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="None" CssClass="clp1"
                                        Width="100%">
                                    </asp:Table>
                                </div>

                                <br />
                                <hr style="width: 100%;" />
                                <br />
                                <center>
                                    <asp:Label ID="lblRcpa" runat="server" Text="RCPA Details" CssClass="reportheader"></asp:Label>
                                </center>
                                <br />
                                <div style="scrollbar-width: thin; overflow-x: auto">
                                    <asp:Table ID="tblRCPA" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="None" CssClass="clp1"
                                        Width="100%">
                                    </asp:Table>
                                </div>

                                <br />
                                <hr style="width: 100%;" />
                                <br />
                                <center>
                                    <asp:Label ID="lblcrm" runat="server" Text="CRM Details" CssClass="reportheader"></asp:Label>
                                </center>
                                <br />
                                <div style="scrollbar-width: thin; overflow-x: auto">
                                    <asp:Table ID="tblcrm" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="None" CssClass="clp1"
                                        Width="100%">
                                    </asp:Table>
                                </div>
                                <br />
                                <hr style="width: 100%;" />
                                <br />
                                <center>
                                    <asp:Label ID="lblbus" runat="server" Text="Business Details" CssClass="reportheader"></asp:Label>
                                </center>
                                <br />
                                <div style="scrollbar-width: thin; overflow-x: auto">
                                    <asp:Table ID="tblBus" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="None" CssClass="clp1"
                                        Width="100%">
                                    </asp:Table>
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
