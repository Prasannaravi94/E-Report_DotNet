<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptProduct_Exp_specat.aspx.cs" Inherits="MIS_Reports_rptProduct_Exp_specat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Exposure Analysis</title>

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
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, sf_name, cyear, cmonth, Prod_Name, Prod, sCurrentDate, Doc_Special_Code, Type) {
            popUpObj = window.open("rptProduct_Exp_specat1.aspx?sf_code=" + sfcode + "&sf_name=" + sf_name + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "&Doc_Special_Code=" + Doc_Special_Code + "&Type=" + Type,
     "_blank",
    "ModalPopUp"// +
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=700," +
    //"height=500," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
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

    <script type="text/javascript" language="Javascript">
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

    <style type="text/css">
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        .remove {
            text-decoration: none;
        }

        .table tr:first-child td:first-child {
            width: 5%;
        }

        .display-table3rowspan .table tr:nth-child(2) td {
            min-width: 190px;
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
            top: 28px;
            z-index: 0;
            background: inherit;
        }

        .stickyThirdRow {
            position: sticky;
            position: -webkit-sticky;
            top: 57px;
            z-index: 1;
            background: inherit;
        }

        .display-table3rowspan .table tr:first-child td:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-table3rowspan .table tr:nth-child(n+4) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 0;
        }

        .display-table3rowspan .table tr:first-child td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 33px;
            background: inherit;
            z-index: 2;
            min-width: 158px;
        }

        .display-table3rowspan .table tr:nth-child(n+4) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 33px;
        }
        /*Fixed Heading & Fixed Column-End*/
         .display-table3rowspan .table td {
            border-color: #DCE2E8;
            border-right: none;
        }
    </style>

</head>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <br />
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
                                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" OnClick="btnClose_Click">
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
      

        <div class="container clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <div>
                            <div align="center">
                                <asp:Label ID="lblHead" Text="Product Exposure Analysis" CssClass="reportheader" runat="server"></asp:Label>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-5">
                                    <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                    <asp:Label ID="lblRegionName" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label ID="lblIDMonth" Text="Month :" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                    <asp:Label ID="lblMonth" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label ID="lblIDYear" Text="Year :" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                    <asp:Label ID="lblYear" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lblprd_name" Text="Product Name :" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                    <asp:Label ID="lblname" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                </div>
                            </div>
                          

                            <div class="display-table3rowspan clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table width="100%">
                                            <caption>
                                                <asp:GridView ID="GrdPrdExp" runat="server" AlternatingRowStyle-CssClass="alt"
                                                    AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found" BorderColor="WhiteSmoke" BorderWidth="1"
                                                    GridLines="Both" HorizontalAlign="Center" OnRowCreated="GrdPrdExp_RowCreated" Style="background-color: white"
                                                    OnRowDataBound="GrdPrdExp_RowDataBound" ShowHeader="False" Width="100%">
                                                    <Columns>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </caption>
                                        </table>
                                    </asp:Panel>
                                    <%--<asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="95%">
                            </asp:Table>--%>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>

        <br />
        <br />
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
