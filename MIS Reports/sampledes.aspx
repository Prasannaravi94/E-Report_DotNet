<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sampledes.aspx.cs" Inherits="MIS_Reports_sampledes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sample Despatch View</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
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
        var popUpObj;
        function showModalPopUp(sfcode, sf_name, cyear, cmonth, sCurrentDate) {
            popUpObj = window.open("sampledes1.aspx?sf_code=" + sfcode + "&sf_name=" + sf_name + "&Year=" + cyear + "&Month=" + cmonth + "&sCurrentDate=" + sCurrentDate,
     //"_blank",
    "ModalPopUp,"// +
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
    <style type="text/css">
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        .remove {
            text-decoration: none;
        }
        .display-table .table tr:nth-child(2) td:nth-child(n) {
    background-color: #f1f5f8;
    color: #636d73;
}
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td width="20%"></td>
                        <td width="80%" align="center">
                            <asp:Label ID="lblHead" Text="Sample Despatch View" CssClass="reportheader" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td style="padding-right: 30px">
                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" Visible="false" OnClick="btnPrint_Click">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                    </td>
                                    <td style="padding-right: 15px">
                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                    </td>
                                    <td style="padding-right: 50px">
                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" OnClick="btnClose_Click">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-center">
                    <div class="col-lg-12">
                        <table width="100%" align="center">
                            <tr>
                                <td width="2.5%"></td>
                                <td align="left">
                                    <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" Font-Size="14px"></asp:Label>
                                    <asp:Label ID="lblRegionName" runat="server" Font-Size="14px" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblIDMonth" Text="Month :" runat="server" Font-Size="14px"></asp:Label>
                                    <asp:Label ID="lblMonth" runat="server" Font-Size="14px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblIDYear" Text="Year :" runat="server" Font-Size="14px"></asp:Label>
                                    <asp:Label ID="lblYear" runat="server" Font-Size="14px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div class="designation-reactivation-table-area clearfix">
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                    <asp:Table ID="tbl" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                        GridLines="None" HorizontalAlign="Center" BorderWidth="0" Width="100%">
                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
