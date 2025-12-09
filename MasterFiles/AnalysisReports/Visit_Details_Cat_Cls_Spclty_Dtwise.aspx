<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visit_Details_Cat_Cls_Spclty_Dtwise.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Visit_Details_Cat_Cls_Spclty_Dtwise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <style type="text/css">
        .gvHeader th
        {
            padding: 3px;
            background-color: #DDEECC;
            color: maroon;
            border: 1px solid #bbb;
        }
        .gvRow td
        {
            padding: 3px;
            background-color: #ffffff;
            border: 1px solid #bbb;
        }
        .gvAltRow td
        {
            padding: 3px;
            background-color: #f1f1f1;
            border: 1px solid #bbb;
        }
        .gvHeader th:first-child
        {
            display: none;
        }
        .gvRow td:first-child
        {
            display: none;
        }
        .gvAltRow td:first-child
        {
            display: none;
        }
        .loading
        {
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
        td
        {
            cursor: pointer;
        }
        .selected_row
        {
            background-color: Black;
            color: White;
        }
    </style>
       <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<script type="text/javascript">
        $(function () {
            $("[id*=GrdDoctor] td").bind("click", function () {
                var row = $(this).parent();
                $("[id*=GrdDoctor] tr").each(function () {
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
    </script>--%>
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
    <script type="text/javascript">
        $(document).ready(function () {
            //
            $(".btnLstDr").mouseover(function () {
                $(this).css("color", "Fuchsia");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            $(".btnLstDr").mouseout(function () {
                $(this).css("color", "black");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "11px");
            });
            //
            $(".btnDrMt").mouseover(function () {
                $(this).css("color", "darkgreen");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            $(".btnDrMt").mouseout(function () {
                $(this).css("color", "black");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "11px");
            });
            //
            $(".btnDrSn").mouseover(function () {
                $(this).css("color", "blue");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            $(".btnDrSn").mouseout(function () {
                $(this).css("color", "black");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "11px");
            });
            //
            $(".btnDrMsd").mouseover(function () {
                $(this).css("color", "red");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            $(".btnDrMsd").mouseout(function () {
                $(this).css("color", "black");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "11px");
            });
            //
        });
    </script>
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
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td width="80%">
                </td>
                <td style="float: right;">
                    <table>
                        <tr style="float: right; margin-right: 10px;">
                            <td>
                                <asp:ImageButton ID="btnPrint" ImageUrl="~/Images/printer.png" runat="server" Width="35px"
                                    Height="30px" OnClientClick="return PrintPanel();" />
                            </td>
                            &nbsp;&nbsp;&nbsp;
                            <td>
                                <asp:ImageButton ID="btnExcel" ImageUrl="~/Images/Excels.png" runat="server" Height="30px"
                                    Width="35px" />
                            </td>
                            &nbsp;&nbsp;&nbsp;
                            <td>
                                <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    Visible="false" />
                            </td>
                            &nbsp;&nbsp;&nbsp;
                            <td>
                                <asp:ImageButton ID="btnClose" ImageUrl="~/Images/closebtn.png" runat="server" Height="30px"
                                    Width="35px" OnClientClick="RefreshParent()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlContents" runat="server">
            <center>
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="Visit Details for the month of " Font-Underline="True"
                        Font-Bold="True" Font-Names="Verdana" Font-Size="11pt"></asp:Label>
                    <br />
                    <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                        Font-Size="9pt"></asp:Label>
                </div>
            </center>
            <br />
            <center>
                <asp:GridView ID="GrdDoctor" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="true"
                    CssClass="mGrid" EmptyDataText="No Records Found" GridLines="Both" HorizontalAlign="Center"
                    BorderWidth="1" OnRowCreated="GVMissedCall_RowCreated" OnRowDataBound="GrdDoctor_RowDataBound"
                    PageSize="10" ShowHeader="false" Width="90%">
                    <HeaderStyle Font-Bold="False" />
                    <PagerStyle CssClass="pgr" />
                    <SelectedRowStyle BackColor="BurlyWood" />
                    <AlternatingRowStyle CssClass="alt" />
                    <RowStyle HorizontalAlign="left" />
                    <Columns>
                    </Columns>
                    <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                        VerticalAlign="Middle" />
                </asp:GridView>
            </center>
        </asp:Panel>
    </div>
    <%--<div class="" id="divLoader" runat="server" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>--%>
    </form>
</body>
</html>
