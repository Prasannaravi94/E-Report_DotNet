<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptWrkTypeViewStatus.aspx.cs"
    Inherits="Reports_rptWrkTypeViewStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link type="text/css" rel="Stylesheet" href="../../css/rptMissCall.css" />

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Work Type Status View</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" type="image/png" href="../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../../assets/css/responsive.css" />
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->

    <script src="../../assets/js/jQuery.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../assets/js/main.js"></script>


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

        .tr_Sno {
            background: #414d55;
            color: white;
            font-weight: 400;
            border-radius: 8px 0 0 8px;
            font-size: 12px;
            border-bottom: 10px solid #fff;
            font-family: Roboto;
            border-left: 0px solid #F1F5F8;
        }

        .tr_th {
            padding: 15px 5px;
            /*border-bottom: 10px solid #fff;*/
            border-top: 0px;
            font-size: 12px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            text-transform: uppercase;
        }

        .display-callAvgreporttable .table tr:nth-child(2) td:first-child {
            background-color: #F1F5F8;
            color: #636d73;
            border-left: 1px solid #DCE2E8;
        }

        .display-callAvgreporttable .table tr:nth-child(3) td:first-child {
            background-color: #414D55;
            color: white;
        }
        /*Fixed Heading & Fixed Column-Begin*/
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .stickySecondRow {
            position: sticky;
            position: -webkit-sticky;
            top: 48px;
            z-index: 0;
            background: inherit;
        }

        .display-callAvgreporttable .table tr:first-child td:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-callAvgreporttable .table tr:nth-child(n+3) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 0;
        }

        .display-callAvgreporttable .table tr:first-child td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 30px;
            background: inherit;
            z-index: 2;
            min-width: 158px;
        }

        .display-callAvgreporttable .table tr:nth-child(n+3) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 30px;
        }

        .display-callAvgreporttable .table tr:first-child td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 180px;
            background: inherit;
            z-index: 2;
        }

        .display-callAvgreporttable .table tr:nth-child(n+3) td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 180px;
        }

        .display-Approvaltable .table tr:first-child, .display-Approvaltable .table tr:nth-child(2) {
            padding: 20px 10px;
            border-bottom: 2px solid #dce2e8;
        }
        /*Fixed Heading & Fixed Column-End*/
    </style>




    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script language="Javascript">
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
</head>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <div>
            <br />
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <div class="row justify-content-center">
                        <div class="col-lg-9">
                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="Visit Doctor Field for the month of " CssClass="reportheader"></asp:Label>
                                <br />
                                <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" ForeColor="#696D6E"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <table width="100%" style="padding-right: 100px;">
                                <tr>
                                    <td></td>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td style="padding-right: 50px">

                                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>

                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>

                                                </td>
                                                <td style="padding-right: 100px">
                                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
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

                                <div class="display-callAvgreporttable clearfix">
                                    <div class="table-responsive" style="max-height: 700px; scrollbar-width: thin;overflow:inherit;background-color:white;">
                                        <table width="100%" align="center">
                                            <tr>
                                                <td>
                                                    <asp:Table ID="tbl" runat="server" GridLines="None" CssClass="table" Width="100%" style="background-color:white;">
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                        </table>
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
