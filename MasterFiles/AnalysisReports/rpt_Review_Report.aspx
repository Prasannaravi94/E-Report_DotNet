<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Review_Report.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_rpt_Review_Report" %>

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

    <style type="text/css">
        .single-block-area {
            border-collapse: collapse;
            width: 300px;
        }

            .single-block-area th {
                text-align: left;
                color: #fff;
                text-align: left;
                padding: 15px;
                border-radius: 10px 10px 0px 0px;
            }

            .single-block-area td {
                padding: 15px;
            }

            .single-block-area tr td {
                border-bottom: 1px solid #DCE2E8;
            }
    </style>

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
    <script type="text/javascript">
        $(document).live('load', function () {
            $('#GrdDoctor tr').each(function () {
                var vara = $(this).find('td').html();
                if (vara != null)
                    alert($(this).find('td').html());
            });
        });
    </script>
    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                 <br />
                <div class="row justify-content-center">
                    <br />
                    <div class="col-lg-12">
                        <div class="row justify-content-center">
                          
                            <div class="col-lg-9">
                                <div align="center">
                                    <asp:Label ID="lblHead" runat="server" Text="Rep Vs Manager - Comparison" CssClass="reportheader"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-3">
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
                                                        <asp:Label ID="Label3" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>
                                                    <td style="padding-right: 15px">
                                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label8" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>
                                                    <td style="padding-right: 40px">
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

                <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <asp:Panel ID="pnlContents" runat="server">

                                <div class="row" align="left">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblSF" runat="server" Font-Bold="true" ForeColor="Red"
                                            Text="FieldForce Name:" Font-Size="10pt"></asp:Label>
                                        <asp:Label ID="LblForceName" runat="server" Font-Bold="True"
                                            Font-Size="10pt"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Red"
                                            Text="HQ:" Font-Size="10pt"></asp:Label>
                                        <asp:Label ID="lblHQ" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" ForeColor="Red"
                                            Text="Desig:" Font-Size="10pt"></asp:Label>
                                        <asp:Label ID="lblDesig" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                    </div>
                                </div>
                                <div class="row" align="left">
                                    <div class="col-lg-6">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" ForeColor="Red"
                                            Text="Employee Code:" Font-Size="10pt"></asp:Label>
                                        <asp:Label ID="lblEmp" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="true" ForeColor="Red"
                                            Text="State:" Font-Size="10pt"></asp:Label>
                                        <asp:Label ID="lblSt" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" ForeColor="Red"
                                            Text="Division:" Font-Size="10pt"></asp:Label>
                                        <asp:Label ID="lblDiv" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row" align="center">
                                    <div class="col-lg-4">

                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #4b64e3">WORKING INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Total Working Days
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblWork" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Holiday & Sunday
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblHol" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Leave
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblLeave" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Fw Days
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblFW" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Non Field Days
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNFW" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>
                                    <div class="col-lg-4">

                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #83e68e">MASTER INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Listed drs in List
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldrs" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Chemists in the List
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblChem" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Unlisted drs in the List
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUnlst" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Stockist List
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstk" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Hospital List
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblHosp" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #0aa8a3">LISTED DR INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Listed drs Met
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblmet" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Listed drs Seen
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSeen" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Coverage
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCov" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Call Average
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCallAvg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Missed Calls
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblmiss" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <br />
                                <div class="row" align="center">
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #2ab8f5">CHEMIST INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Chemist Met
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCMet" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Chemist Seen
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCSeen" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Call Average
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCall" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Missed Calls
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCMiss" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #f257d1">UNLISTED DR INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Unlisteddr Met
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUMet" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Unlisteddr Seen
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUseen" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Call Average
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUCall" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Missed Calls
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUmiss" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #eb7442">JOINT WORK INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Joint Working Days
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblJWDays" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Joint Calls Met
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblJWMet" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Joint Calls Seen
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblJWSeen" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Joint Call Avg
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblJWCall" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <br />
                                <div class="row" align="center">
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th style="background-color: #4f5f8f">DR CATEGORY INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Table ID="tbl" runat="server" GridLines="None"
                                                            Width="100%">
                                                        </asp:Table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #f7343a">SAMPLE INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Sample Given Qty
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSamQty" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Sample Given drs
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSamDrs" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Sample Given Products
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSamProd" runat="server"></asp:Label>
                                                    </td>
                                                </tr>




                                                <tr>
                                                    <td>Rx Qty
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRxQty" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Rx Drs
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRxDrs" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Rx Products
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRxProd" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Rx Value
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRxValue" runat="server"></asp:Label>
                                                    </td>
                                                </tr>



                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #2ea6d1">INPUT INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Input Given Qty
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblInQty" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Input Given drs
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblInDrs" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Input Given Products
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblInProd" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <br />
                                <div class="row" align="center">
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #e6c929">PRODUCT INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>No of Detailing drs
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblProdDet" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>No of Rx drs
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPres" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" style="color: Red; font-weight: bold">Top 5 Products
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Table ID="tblProddet" runat="server" GridLines="None"
                                                            Width="100%">
                                                        </asp:Table>
                                                        <asp:Label ID="lblNO" runat="server" Width="100%" CssClass="no-result-area"
                                                            Visible="false">  No Records Found</asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th style="background-color: #6aab8f">DR CATEGORY VISIT INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Table ID="tblcat" runat="server" GridLines="None"
                                                            Width="100%">
                                                        </asp:Table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th style="background-color: #872dcc">DRS CATEGORY CALL ADHERENCE
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Table ID="tblcatVisit" runat="server" GridLines="None"
                                                            Width="100%">
                                                        </asp:Table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <br />
                                <div class="row" align="center">
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #b86a49">DRS VISIT
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>1 Visit Drs
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl1Vis" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>2 Visit Drs
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl2Vis" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>3 Visit Drs
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl3Vis" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>More than 3 Visit Drs
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblmore" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #733b8a">TARGET & SALE INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>TARGET
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>SECONDARY SALE
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbltotsale" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>PRIMARY SALE
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPrimSale" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ACHIEVEMENT(%)
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #d13864">OTHER INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Total Sample Spent (in Rs/-)
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total Input Spent (in Rs/-)
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label12" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Dr Service Spent (in Rs/-)
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDrservice" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Expense Spent (in Rs/-)
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblexpSpent" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <br />
                                <div class="row" align="center">
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th style="background-color: #76a10a">SECONDARY SALE INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td align="center" style="color: Red; font-weight: bold">Top 5 Products Sale
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Table ID="tblSec" runat="server" GridLines="None"
                                                            Width="100%">
                                                        </asp:Table>
                                                        <asp:Label ID="lblNoRecord" runat="server"
                                                            Visible="false" CssClass="no-result-area" Width="100%">  No Records Found</asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #7ea1cf">TP INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>No. of HQ Plan
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTPHq" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>No. of HQ Worked
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDcrHq" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>No. of EX Plan
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTPEx" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>No. of EX Worked
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDcrEx" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>No. of OS Plan
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTPOs" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>No. of OS Worked
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDcrOs" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                        <td>
                                          No. of TP Deviation Days
                                        </td>
                                        <td>
                                            <asp:Label ID="Label21" runat="server"></asp:Label>
                                        </td>
                                    </tr>--%>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-lg-4">
                                        <table class="single-block-area">
                                            <thead>
                                                <tr>
                                                    <th colspan="2" style="background-color: #8f813b">EXPENSE INFO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>HQ (<asp:Label ID="lblHdays" runat="server" ForeColor="Red"></asp:Label>)
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblExpHQ" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>EX (<asp:Label ID="lblEdays" runat="server" ForeColor="Red"></asp:Label>)
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblExpEx" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>OS (<asp:Label ID="lblOdays" runat="server" ForeColor="Red"></asp:Label>)
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblExpOs" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Miscellaneous
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblmiscel" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbltotalexp" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
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
