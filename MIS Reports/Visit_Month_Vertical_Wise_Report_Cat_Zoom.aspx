<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx.cs"
    Inherits="MIS_Reports_Visit_Month_Vertical_Wise_Report_Cat_Zoom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Visit Details Field Report</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/repstyle.css" />--%>
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
    <script type="text/javascript">
        $(function () {
            $("[id*=GrdFixationMode] td").bind("click", function () {
                var row = $(this).parent();
                $("[id*=GrdFixationMode] tr").each(function () {
                    if ($(this)[0] != row[0]) {
                        $("td", this).removeClass("selected_row");
                    }
                });
                $("td", row).each(function () {
                    if (!$(this).hasClass("selected_row")) {
                        $(this).addClass("selected_row");
                    } else {
                        $(this).removeClass("selected_row");
                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, cmode, Type) {
            popUpObj = window.open("Visit_Details_Basedonfield_Level2.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&cMode=" + cmode + "&Type=" + Type,
            "_blank",
        "ModalPopUp_Level1," +
         "0," +
        "toolbar=no," +
        "scrollbars=yes," +
        "location=no," +
        "statusbar=no," +
        "menubar=no," +
        "addressbar=no," +
        "resizable=yes," +
        "width=800," +
        "height=600," +
        "left = 0," +
        "top=0"
        );
            popUpObj.focus();
            //LoadModalDiv();
        }

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

    <%--<script language="javascript" type="text/javascript">
        function callServerButtonEvent(a, b) {
            document.getElementById('<%=lblMonthExc.ClientID%>').value = a;
            document.getElementById("LinkBtn").click();
        }
    </script>--%>

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

        td {
            cursor: pointer;
        }

        .selected_row {
            background-color: Black;
            color: White;
        }

        .display-table3rowspan .table td {
            padding: 5px 20px;
        }

        .display-table3rowspan .table tr td:first-child {
            padding: 5px 10px;
        }
    </style>
</head>
<body style="overflow-x: scroll;">
    <form id="form1" runat="server">
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <br />
                <div class="row justify-content-center">
                   <%-- <div class="col-lg-2">
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="lblMonthExc" runat="server" ForeColor="White" Width="1px" Height="1px"
                                        BackColor="White"></asp:TextBox>
                                    <asp:Button ID="LinkBtn" runat="server" Font-Names="Verdana" Font-Size="10px" BorderColor="Black"
                                        BorderStyle="Solid" Width="1px" Height="1px" OnClick="LinkBtn_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <div class="col-lg-7">
                        <center>
                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="Visit Details for the month of " CssClass="reportheader"></asp:Label>
                                <br />

                            <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" Font-Size="18px" ForeColor="#696d6e"></asp:Label>
                            <asp:Label ID="lblBrndName" runat="server" CssClass="reportheader" Font-Size="18px" ForeColor="#696d6e"></asp:Label>
                            </div>
                        </center>
                    </div>

                    <div class="col-lg-3">
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
                                                    <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                                <td style="padding-right: 50px">
                                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
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

            <div class="container clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">

                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="lblFFName" runat="server" SkinID="lblMand" Text="Field Force Name"
                                        CssClass="reportheader" Font-Size="16px" ForeColor="#696d6e" Visible="false"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lblHQ1" runat="server" SkinID="lblMand" Text="HQ"
                                        CssClass="reportheader" Font-Size="16px" ForeColor="#696d6e" Visible="false"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lblDesignation1" runat="server" SkinID="lblMand" Text="Designation"
                                        CssClass="reportheader" Font-Size="16px" ForeColor="#696d6e" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <br />

                            <div class="display-table3rowspan clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow-x: inherit; background-color: white">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <%--  <table width="100%">
                                                <caption>--%>
                                        <asp:GridView ID="GrdFixationMode" runat="server" AlternatingRowStyle-CssClass="alt"
                                            AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                            GridLines="None" HorizontalAlign="Center" BorderWidth="0" Style="background-color: white"
                                            OnRowCreated="GrdFixationMode_RowCreated" OnRowDataBound="GrdFixationMode_RowDataBound"
                                            ShowHeader="False" Width="100%">

                                            <Columns>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                        <%--     </caption>
                                            </table>--%>

                                        <br />
                                        <br />
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
