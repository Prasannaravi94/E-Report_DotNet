<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDCR_Status_New_Detail.aspx.cs" Inherits="MasterFiles_Reports_rptDCR_Status_New_Detail" %>

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

    <%--  <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

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

        .cls {
            /*font-weight: bold;*/
            padding: 5px;
            border: 3px solid #DCE2E8;
        }

        .c1 {
            /*background: #4b8c74;*/
            background: #F1F5F8;
        }

        .c2 {
            /*background: #74c476;*/
        }

        .c3 {
            background: #a4e56d;
        }

        .c4 {
            background: #cffc83;
        }

        <style type="text/css" > .blink_me {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 1s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 1s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 1s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        .blink {
            animation: blink-animation 1s steps(5, start) infinite;
            -webkit-animation: blink-animation 1s steps(5, start) infinite;
        }

        @keyframes blink-animation {
            to {
                visibility: hidden;
            }
        }

        @-webkit-keyframes blink-animation {
            to {
                visibility: hidden;
            }
        }
    </style>

    <style type="text/css">
        .display-Approvaltable .table tr:nth-child(3) td:first-child, .display-Approvaltable .table tr:nth-child(3) td {
            border-left: 1px solid #DCE2E8;
            background-color: #F1F5F8;
            color: #636d73;
            border-bottom: 10px solid #fff;
        }

        /*.display-Approvaltable .table tr:first-child td {
            border-bottom: 10px solid #fff;
        }*/

        .display-Approvaltable .table tr:nth-child(4) td:first-child {
            background-color: #414d55;
            color: #ffffff;
        }


        .display-Approvaltable .table td {
            padding: 8px 6px;
        }

        .display-Approvaltable .table tr td:first-child {
            padding: 4px 6px;
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
            top: 34px;
            z-index: 0;
            background: inherit;
        }

        .stickyThirdRow {
            position: sticky;
            position: -webkit-sticky;
            top: 69px;
            z-index: 1;
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
            left: 180px;
            background: inherit;
            z-index: 2;
        }

        .display-Approvaltable .table tr:nth-child(n+4) td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 180px;
        }

        .display-Approvaltable .table tr:first-child td:nth-child(4) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 270px;
            background: inherit;
            z-index: 2;
        }

        .display-Approvaltable .table tr:nth-child(n+4) td:nth-child(4) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 280px;
        }

        .display-Approvaltable .table tr:first-child, .display-Approvaltable .table tr:nth-child(2) {
            padding: 20px 10px;
            border-bottom: 2px solid #dce2e8;
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
                                    <td align="center">
                                        <%-- <div runat="server" id="blinkText">
                                <asp:Label ID="lblHead" Text="Daily Call Status" ForeColor="Maroon" Font-Size="14px"
                                    Font-Bold="true" Font-Underline="true" runat="server"></asp:Label>
                            </div>--%>

                                    </td>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td style="padding-right: 30px">
                                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
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
                        </div>
        </asp:Panel>
        <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div runat="server" id="blinkText" align="center">
                            <asp:Label ID="lblHead" Text="Daily Call Status" CssClass="reportheader" runat="server"></asp:Label>
                        </div>
                        <div>
                            <div align="center">
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" CssClass="d-inline-block division-name"></asp:Label>
                                    <asp:Label ID="lblRegionName" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lblIDMonth" Text="Month :" runat="server" CssClass="d-inline-block division-name"></asp:Label>
                                    <asp:Label ID="lblMonth" runat="server"></asp:Label>

                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lblIDYear" Text="Year :" runat="server" CssClass="d-inline-block division-name"></asp:Label>
                                    <asp:Label ID="lblYear" runat="server"></asp:Label>

                                </div>
                            </div>
                            <br />
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="display-Approvaltable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                        <table align="center" width="100%">
                                            <tr>
                                                <td align="center" style="vertical-align: top;">
                                                    <asp:GridView ID="Grdprd" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="true"
                                                        CssClass="table" EmptyDataText="No Records Found" GridLines="Both" HorizontalAlign="Center" BorderColor="WhiteSmoke" BorderWidth="1"
                                                        OnRowCreated="Grdprd_RowCreated" OnRowDataBound="Grdprd_RowDataBound" Style="background-color: white"
                                                        ShowHeader="False">

                                                        <Columns>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <center>
                                <asp:Label ID="lblwrk" Text="Work Type" CssClass="reportheader"
                                    runat="server"></asp:Label>
                            </center>
                            <br />
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:DataList ID="DataList2" Font-Size="9pt" HeaderStyle-BackColor="#666699" Width="100%"
                                        runat="server" RepeatDirection="Vertical" RepeatColumns="3">
                                        <HeaderStyle ForeColor="White" />
                                        <ItemStyle BackColor="White" ForeColor="Black" />
                                        <AlternatingItemStyle />
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <b></b>
                                            <asp:Label ID="lblWType_SName" ForeColor="HotPink" Font-Bold="true" runat="server"
                                                Width="10%" Text='<%#Eval("WType_SName")%>'></asp:Label>&nbsp&nbsp
                                    <asp:Label ID="lblWorktype_Name_B" ForeColor="darkMagenta" Font-Bold="true" runat="server"
                                        Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                                <div class="col-lg-2">
                                </div>
                                <div class="col-lg-4">
                                    <table>
                                        <tr class="cls">
                                            <th class="c1 cls">Short Name
                                            </th>
                                            <th class="c2 cls">Full Name
                                            </th>

                                        </tr>
                                        <tr class="cls">
                                            <td class="c1 cls">TL
                                            </td>
                                            <td class="c2 cls">Total Leave Days
                                            </td>


                                        </tr>
                                        <tr>
                                            <td class="c1 cls">TH
                                            </td>
                                            <td class="c2 cls">Total Holiday Days
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="c1 cls">TW
                                            </td>
                                            <td class="c2 cls">Total WeekOff Days
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="c1 cls">TD
                                            </td>
                                            <td class="c2 cls">Total Delayed Days
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />
        </asp:Panel>
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

