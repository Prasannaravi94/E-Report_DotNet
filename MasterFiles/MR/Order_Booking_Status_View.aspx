<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Order_Booking_Status_View.aspx.cs" Inherits="MasterFiles_MR_Order_Booking_Status_View" %>

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

    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <style type="text/css">
        .gvHeader th {
            padding: 3px;
            background-color: #DDEECC;
            color: maroon;
            border: 1px solid #bbb;
        }

        .gvRow td {
            padding: 3px;
            background-color: #ffffff;
            border: 1px solid #bbb;
        }

        .gvAltRow td {
            padding: 3px;
            background-color: #f1f1f1;
            border: 1px solid #bbb;
        }

        .gvHeader th:first-child {
            display: none;
        }

        .gvRow td:first-child {
            display: none;
        }

        .gvAltRow td:first-child {
            display: none;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $("form1").live("submit", function () {
            ShowProgress();
        });
    </script>
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
            </div>

            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">

                            <center>
                                <asp:Label ID="lblViewreq" Text="Order Booking View " runat="server" CssClass="reportheader" Font-Underline="true" ForeColor="BlueViolet"></asp:Label>

                            </center>

                            <br />
                            <asp:Table ID="Table7" BorderStyle="Solid" BackColor="White" BorderWidth="0" Width="70%"
                                CellSpacing="5" CellPadding="5" runat="server">
                                <%--<asp:TableRow HorizontalAlign="Left">
                            <asp:TableCell BorderWidth="0" CssClass="padding" BackColor="White">
                                <asp:Label ID="lblvisit" runat="server" Text="Order Booking View :" Font-Bold="true" Font-Size="16px" ForeColor="BlueViolet"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>--%>
                                <asp:TableRow HorizontalAlign="center">
                                    <asp:TableCell BorderWidth="0" CssClass="padding" ColumnSpan="2" VerticalAlign="Middle" HorizontalAlign="Left">
                                        <asp:Table ID="Table9" BorderStyle="Solid" BackColor="White" BorderWidth="0" Width="100%"
                                            CellSpacing="0" CellPadding="0" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableCell BorderWidth="0" CssClass="padding">
                                                    <asp:Label ID="lblNo" Text="P O No " runat="server" Font-Bold="false" Width="170px" Font-Size="16px"></asp:Label>
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="false" Font-Size="16px"> &nbsp;: &nbsp; &nbsp;</asp:Label>
                                                    <asp:Label ID="txtNo" runat="server" Font-Bold="false" Font-Size="16px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableCell BorderWidth="0" CssClass="padding">
                                                    <asp:Label ID="lblDate" Text="Date " runat="server" Font-Bold="false" Width="170px" Font-Size="16px"></asp:Label>
                                                    <asp:Label ID="Label43" runat="server" Font-Bold="false" Font-Size="16px"> &nbsp;: &nbsp; &nbsp;</asp:Label>
                                                    <asp:Label ID="txtDate" runat="server" Font-Bold="false" Font-Size="16px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableCell BorderWidth="0" CssClass="padding">
                                                    <asp:Label ID="lblCus" Text="Customer Name " runat="server" Font-Bold="false" Width="170px" Font-Size="16px"></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="false" Font-Size="16px"> &nbsp;: &nbsp; &nbsp;</asp:Label>
                                                    <asp:Label ID="txtCus" runat="server" Font-Bold="false" Font-Size="16px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableCell BorderWidth="0" CssClass="padding">
                                                    <asp:Label ID="lblAdres" Text="Customer Address " runat="server" Font-Bold="false" Width="170px" Font-Size="16px"></asp:Label>
                                                    <asp:Label ID="Label5" runat="server" Font-Bold="false" Font-Size="16px"> &nbsp;: &nbsp; &nbsp;</asp:Label>
                                                    <asp:Label ID="txtAddres" runat="server" Font-Bold="false" Font-Size="16px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableCell BorderWidth="0" CssClass="padding">
                                                    <asp:Label ID="lblMbl" Text="Customer Mobile No " runat="server" Font-Bold="false" Width="170px" Font-Size="16px"></asp:Label>
                                                    <asp:Label ID="Label8" runat="server" Font-Bold="false" Font-Size="16px"> &nbsp;: &nbsp; &nbsp;</asp:Label>
                                                    <asp:Label ID="txtMbl" runat="server" Font-Bold="false" Font-Size="16px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableCell BorderWidth="0" CssClass="padding">
                                                    <asp:Label ID="lblTown" Text="Customer Town " runat="server" Font-Bold="false" Width="170px" Font-Size="16px"></asp:Label>
                                                    <asp:Label ID="Label11" runat="server" Font-Bold="false" Font-Size="16px"> &nbsp;: &nbsp; &nbsp;</asp:Label>
                                                    <asp:Label ID="txtTown" runat="server" Font-Bold="false" Font-Size="16px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableCell BorderWidth="0" CssClass="padding">
                                                    <asp:Label ID="lblSupp" Text="Supplied Via " runat="server" Font-Bold="false" Width="170px" Font-Size="16px"></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="false" Font-Size="16px"> &nbsp;: &nbsp; &nbsp;</asp:Label>
                                                    <asp:Label ID="txtSupp" runat="server" Font-Bold="false" Font-Size="16px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>


                            <br />
                           <%-- <div class="display-Approvaltable clearfix">--%>
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;max-height:700px;">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table width="100%">
                                            <caption>
                                                <asp:GridView ID="GrdFixation" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                                    GridLines="None"
                                                    OnRowCreated="GrdFixation_RowCreated" OnRowDataBound="GrdFixation_RowDataBound"
                                                    AllowSorting="True" ShowHeader="False">
                                                    
                                                    <Columns>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </caption>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                            <br />
                            <asp:Table ID="Table1" BorderStyle="Solid" BackColor="White" BorderWidth="0" Width="70%"
                                CellSpacing="0" CellPadding="0" runat="server" Visible="false">
                                <%--<asp:TableRow HorizontalAlign="Left">
                            <asp:TableCell BorderWidth="0" CssClass="padding" BackColor="White">
                                <asp:Label ID="lblvisit" runat="server" Text="Order Booking View :" Font-Bold="true" Font-Size="16px" ForeColor="BlueViolet"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>--%>
                                <asp:TableRow HorizontalAlign="center">
                                    <asp:TableCell BorderWidth="0" CssClass="padding" ColumnSpan="2" VerticalAlign="Middle" HorizontalAlign="Left">
                                        <asp:Table ID="Table2" BorderStyle="Solid" BackColor="White" BorderWidth="0" Width="100%"
                                            CellSpacing="0" CellPadding="0" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableCell BorderWidth="0" CssClass="padding">
                                                    <asp:Label ID="lblPrepared" Text="Order Prepared by " runat="server" Font-Bold="true" Width="180px" Font-Size="18px"></asp:Label>
                                                    <asp:Label ID="Label4" runat="server" Font-Size="18px"> &nbsp;: &nbsp; &nbsp;</asp:Label>
                                                    <asp:Label ID="Label6" runat="server" Font-Size="18px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableCell BorderWidth="0" CssClass="padding">
                                                    <asp:Label ID="lblRmrk" Text="Remarks " runat="server" Font-Bold="true" Width="180px" Font-Size="18px"></asp:Label>
                                                    <asp:Label ID="Label9" runat="server" Font-Size="18px"> &nbsp;: &nbsp; &nbsp;</asp:Label>
                                                    <asp:Label ID="Label10" runat="server" Font-Size="18px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>


                            <br />
                            <center>
                                <asp:Table ID="tblChk" BorderStyle="Solid" BackColor="White" BorderWidth="0" Width="70%"
                                    CellSpacing="0" CellPadding="0" runat="server">
                                    <asp:TableRow HorizontalAlign="center">
                                        <asp:TableCell BorderWidth="0" CssClass="padding" ColumnSpan="2" VerticalAlign="Middle" HorizontalAlign="Left">
                                            <asp:Table ID="Table4" BorderStyle="Solid" BackColor="White" BorderWidth="0" Width="100%"
                                                CellSpacing="0" CellPadding="0" runat="server" HorizontalAlign="Center">
                                                <asp:TableRow HorizontalAlign="Left">
                                                    <asp:TableCell BorderWidth="0" CssClass="padding">
                                                        <asp:CheckBox ID="chkinvoiced" runat="server" ForeColor="#696D6E"/>
                                                         <asp:Label ID="lblchkinv" runat="server" ForeColor="#696D6E" Text=" &nbsp;Invoiced"></asp:Label>
                                                         <asp:Image ID="imgCross1" runat="server" ImageUrl="../../../Images/PinkCross.png" Visible="false" />
                                                         <asp:Label ID="lblInv" runat="server" ForeColor="#696D6E" Text=" &nbsp;Invoiced" Visible="false"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow HorizontalAlign="Left">
                                                    <asp:TableCell BorderWidth="0" CssClass="padding">
                                                        <asp:CheckBox ID="chkdespatched"  runat="server" ForeColor="#696D6E"/>
                                                        <asp:Label ID="lblchkDesp" runat="server" ForeColor="#696D6E" Text=" &nbsp;Despatched" ></asp:Label>
                                                         <asp:Image ID="imgCross3" runat="server" ImageUrl="~/Images/Orange.png" Visible="false" />
                                                         <asp:Label ID="lblDesp" runat="server" ForeColor="#696D6E" Text=" &nbsp;Despatched" Visible="false"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow HorizontalAlign="Left">
                                                    <asp:TableCell BorderWidth="0" CssClass="padding">
                                                        <asp:CheckBox ID="chkdelivered"  runat="server" ForeColor="#696D6E"/>
                                                         <asp:Label ID="lblchkdeli" runat="server" ForeColor="#696D6E" Text=" &nbsp;Delivered" ></asp:Label>
                                                          <asp:Image ID="imgCross" runat="server" ImageUrl="../../../Images/Campcross.png" Visible="false" />
                                                         <asp:Label ID="lbldeli" runat="server" ForeColor="#696D6E" Text=" &nbsp;Delivered" Visible="false"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </center>
                        </asp:Panel>
                    </div>

                    <br />

                    <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Text="Process" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>

    </form>
</body>
</html>
