<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Not_At_All_Visit_Drs.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_rpt_Not_At_All_Visit_Drs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SAN eReport</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <style type="text/css">
        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
            border-style: solid;
        }

        #div1 {
            background-color: #cfc;
        }

        .display-reportMaintable .table tr:first-child td:first-child {
            border-radius: 8px 0 0 8px;
            background-color: #414d55;
            color: #ffffff;
            font-size: 14px;
            font-weight: 400;
            border-left: 0px solid #F1F5F8;
        }

        .display-reportMaintable .table tr:first-child td {
            padding: 8px 10px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            background-color: #F1F5F8;
        }

        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
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
        var popUpObj;
        debugger
        function showMissedDR(sSf_code, div_code, mode) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Not_At_All_Visit_Drs_Zoom.aspx?sSf_code=" + sSf_code + "&div_code=" + div_code + "&mode=" + mode,
            "_blank",
    "ModalPopUp," +
    "0" //+
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=800," +
    //"height=600," +
    //"left = 0," +
    //"top = 0"
    );
            popUpObj.focus();
            //  LoadModalDiv();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row justify-content-center">
                    <div class="col-lg-9">
                        <div align="center">
                            <br />
                            <asp:Label ID="lblHead" runat="server" Text="Not at all Visit Drs from " CssClass="reportheader"></asp:Label>
                            <br />

                            <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" Font-Size="18px" ForeColor="#696d6e"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <br />
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
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">

                            <center>
                                <table style="border: 1px; border-color: Black; border-width: thick; font-size: 14px;">
                                    <tr align="center">
                                        <th colspan="8" style="padding-bottom: 15px;">Listeddr Range & Colour
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="background-color: #00FF00; padding: 10px; border: 1px solid #DCE2E8;">
                                            </div>
                                        </td>
                                        <td>0
                                        </td>
                                        <td>
                                            <div style="background-color: #D4FF00; padding: 10px; border: 1px solid #DCE2E8;">
                                            </div>
                                        </td>
                                        <td>50 - 150
                                        </td>
                                        <td>
                                            <div style="background-color: #FFAF00; padding: 10px; border: 1px solid #DCE2E8;">
                                            </div>
                                        </td>
                                        <td>200 - 500
                                        </td>
                                        <td>
                                            <div style="background-color: #FF4600; padding: 10px; border: 1px solid #DCE2E8;">
                                            </div>
                                        </td>
                                        <td>1000 - 2000
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="background-color: #9FFF00; padding: 10px; border: 1px solid #DCE2E8;">
                                            </div>
                                        </td>
                                        <td>0 - 50
                                        </td>
                                        <td>
                                            <div style="background-color: #FFE400; padding: 10px; border: 1px solid #DCE2E8;">
                                            </div>
                                        </td>
                                        <td>150 - 200
                                        </td>
                                        <td>
                                            <div style="background-color: #FF7B00; padding: 10px; border: 1px solid #DCE2E8;">
                                            </div>
                                        </td>
                                        <td>500 - 1000
                                        </td>
                                        <td>
                                            <div style="background-color: #FF0000; padding: 10px; border: 1px solid #DCE2E8;">
                                            </div>
                                        </td>
                                        <td>> 2000
                                        </td>
                                    </tr>
                                </table>
                            </center>

                            <br />
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow-x: inherit;">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table width="100%">
                                            <caption>
                                                <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                                    AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                                    GridLines="None" HorizontalAlign="Center" OnRowCreated="GrdFixation_RowCreated"
                                                    ShowHeader="False" Width="100%" OnRowDataBound="GrdFixation_RowDataBound">
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
