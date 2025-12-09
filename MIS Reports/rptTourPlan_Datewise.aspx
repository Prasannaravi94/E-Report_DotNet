<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTourPlan_Datewise.aspx.cs" Inherits="MIS_Reports_rptTourPlan_Datewise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%-- <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, sf_name, cyear, cmonth, sCurrentDate, mode) {
            popUpObj = window.open("rptsampleproduct_details1.aspx?sf_code=" + sfcode + "&sf_name=" + sf_name + "&Year=" + cyear + "&Month=" + cmonth + "&sCurrentDate=" + sCurrentDate + "&mode=" + mode,
     "_blank",
    "ModalPopUp," +
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=700," +
    "height=500," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();

            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s10.postimg.org/4i4mt6p3t/loading_23_ook.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red; width:180px; margin:0 auto;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:400px; height: 300px;position: fixed;top: 10%;left: 15%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
        }

        function showModal(sfcode, sf_name, cyear, cmonth, Prod, sCurrentDate, mode) {
            popUpObj = window.open("rptsampleproduct_details2.aspx?sf_code=" + sfcode + "&sf_name=" + sf_name + "&Year=" + cyear + "&Month=" + cmonth + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "&totalinput=" + mode + "&mode=" + mode,
     "_blank",
    "ModalPopUp," +
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=700," +
    "height=500," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();

            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s10.postimg.org/4i4mt6p3t/loading_23_ook.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red; width:180px; margin:0 auto;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:400px; height: 300px;position: fixed;top: 10%;left: 15%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
        }



    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
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
    <style type="text/css">
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        .remove {
            text-decoration: none;
        }

        .display-Approvaltable .table tr:nth-child(3) td, .table tr:first-child td, .table tr:first-child td:first-child {
            padding: 20px 10px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
        }

            .display-Approvaltable .table tr:nth-child(3) td:first-child, .table tr:nth-child(3) td {
                background-color: #f1f5f8;
                color: #6c757d;
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
            top: 65px;
            z-index: 0;
            background: inherit;
        }

        .display-Approvaltable .table tr:first-child td:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-Approvaltable .table tr:nth-child(n+4) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 0;
        }

        .display-Approvaltable .table tr:first-child td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 33px;
            background: inherit;
            z-index: 2;
            min-width: 158px;
        }

        .display-Approvaltable .table tr:nth-child(n+4) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 33px;
        }

        .display-Approvaltable .table tr:first-child td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 190px;
            background: inherit;
            z-index: 2;
        }

        .display-Approvaltable .table tr:nth-child(n+4) td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 190px;
        }

        .display-Approvaltable .table tr:first-child td:nth-child(4) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 310px;
            background: inherit;
            z-index: 2;
        }

        .display-Approvaltable .table tr:nth-child(n+4) td:nth-child(4) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 310px;
        }
        /*Fixed Heading & Fixed Column-End*/
        .display-Approvaltable .table td {
            border-color: #DCE2E8;
            border-right: none;
        }
    </style>

</head>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <div>
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
                                        <%--   <td width="80%" align="center" >
                    <asp:Label ID="lblHead" Text="Field Activity Summary"  ForeColor="	#ff7256" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
                    </td>--%>
                                        <td align="right">
                                            <table>
                                                <tr>
                                                    <td style="padding-right: 30px">
                                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                        <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="return PrintPanel();" />--%>
                                                    </td>
                                                    <td style="padding-right: 15px">
                                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                        <%-- <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />--%>
                                                    </td>

                                                    <td style="padding-right: 50px">
                                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" OnClick="btnClose_Click">
                                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                                        <%-- <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="RefreshParent();" OnClick="btnClose_Click" />--%>
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
            <br />
        </div>

        <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <div>
                            <div align="center">
                                <%--<asp:Label ID="lblHead" Text="Product Exposure Analysis" SkinID="lblMand" Font-Underline="true"
                runat="server"></asp:Label>--%>
                            </div>
                            <center>
                                <table width="100%">
                                    <tr align="center">
                                        <td width="80%" align="center">
                                            <asp:Label ID="lblHead" Text="TourPlan Datewise" CssClass="reportheader"
                                                runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                            <div>

                              
                                <table width="100%" align="center">
                                    <tr>
                                        <td width="2.5%"></td>
                                        <td align="left">
                                            <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" CssClass="reportheader" ForeColor="#696D6E"></asp:Label>
                                            <asp:Label ID="lblRegionName" runat="server" CssClass="reportheader" ForeColor="#696D6E"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblIDMonth" Text="Month :" runat="server" CssClass="reportheader" ForeColor="#696D6E"></asp:Label>
                                            <asp:Label ID="lblMonth" runat="server" CssClass="reportheader" ForeColor="#696D6E"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblIDYear" Text="Year :" runat="server" CssClass="reportheader" ForeColor="#696D6E"></asp:Label>
                                            <asp:Label ID="lblYear" runat="server" CssClass="reportheader" ForeColor="#696D6E"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />

                            <div class="display-Approvaltable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table width="100%">
                                            <caption>
                                                <asp:GridView ID="Grdprd" runat="server"
                                                    AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                                    GridLines="Both" HorizontalAlign="Center" OnRowCreated="Grdprd_RowCreated" BorderColor="WhiteSmoke" BorderWidth="1"
                                                    OnRowDataBound="Grdprd_RowDataBound" ShowHeader="False" Width="100%" Style="background-color: white">
                                                    <Columns>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </caption>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                            <%-- <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="95%">
                            </asp:Table>--%>
                        </div>
                    </asp:Panel>
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
