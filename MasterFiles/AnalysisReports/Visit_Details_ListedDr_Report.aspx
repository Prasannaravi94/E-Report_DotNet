<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visit_Details_ListedDr_Report.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Coverage_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

        #loading {
            width: 100%;
            height: 100%;
            top: 0px;
            left: 0px;
            position: fixed;
            display: block;
            opacity: 0.7; /*background-color: #fff;*/
            z-index: 99;
            text-align: center;
        }

        #loadingimage {
            position: absolute;
            top: 100px;
            left: 240px;
            z-index: 100;
        }


        .ball {
            background-color: rgba(0,0,0,0);
            border: 5px solid rgba(0,183,229,0.9);
            opacity: 0.9;
            border-top: 5px solid rgba(0,0,0,0);
            border-left: 5px solid rgba(0,0,0,0);
            border-radius: 50px;
            box-shadow: 0 0 35px #2187e7;
            width: 50px;
            height: 50px;
            margin: 0 auto;
            -moz-animation: spin .5s infinite linear;
            -webkit-animation: spin .5s infinite linear;
        }

        .ball1 {
            background-color: rgba(0,0,0,0);
            border: 5px solid rgba(0,183,229,0.9);
            opacity: 0.9;
            border-top: 5px solid rgba(0,0,0,0);
            border-left: 5px solid rgba(0,0,0,0);
            border-radius: 50px;
            box-shadow: 0 0 15px #2187e7;
            width: 30px;
            height: 30px;
            margin: 0 auto;
            position: relative;
            top: -50px;
            -moz-animation: spinoff .5s infinite linear;
            -webkit-animation: spinoff .5s infinite linear;
        }

        .divProgress {
            margin-top: 40%;
        }

        @-moz-keyframes spin {
            0% {
                -moz-transform: rotate(0deg);
            }

            100% {
                -moz-transform: rotate(360deg);
            }

            ;
        }

        @-moz-keyframes spinoff {
            0% {
                -moz-transform: rotate(0deg);
            }

            100% {
                -moz-transform: rotate(-360deg);
            }

            ;
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }

            ;
        }

        @-webkit-keyframes spinoff {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(-360deg);
            }

            ;
        }

        @-o-keyframes spin {
            0% {
                -o-transform: rotate(0deg);
            }

            100% {
                -o-transform: rotate(360deg);
            }

            ;
        }

        @-o-keyframes spinoff {
            0% {
                -o-transform: rotate(0deg);
            }

            100% {
                -o-transform: rotate(-360deg);
            }

            ;
        }


        @-ms-keyframes spin {
            0% {
                -moz-transform: rotate(0deg);
            }

            100% {
                -moz-transform: rotate(360deg);
            }

            ;
        }

        @-ms-keyframes spinoff {
            0% {
                -ms-transform: rotate(0deg);
            }

            100% {
                -ms-transform: rotate(-360deg);
            }

            ;
        }

        td {
            cursor: pointer;
        }

        .selected_row {
            background-color: Black;
            color: White;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
        function loading() {
            debugger;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                beforesend: $("#ShowProgressDiv").show(),
                url: "ViewDetails_ListedDr_Report.aspx/BindGrid",
                data: "{name: 'loading'}",
                dataType: "json",
                success: function (data) {
                    //alert("");
                },
                error: function (result) {
                    if (result === 1) {
                        $("#ShowProgressDiv").hide();
                    }
                },
                complete: function (data) {
                    if (data === 1) {
                        $("#ShowProgressDiv").hide();
                    }
                }
            });
        }

        /*function ShowProgress() {
            setTimeout(function () {
                var modal = $('divLoader');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        
        $(document).live("load",function () {
            ShowProgress();
        });*/
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
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
            top: 50px;
            z-index: 0;
            background: inherit;
        }

        .display-Approvaltable .table tr:first-child td:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
            width: 2%;
        }

        .display-Approvaltable .table tr:nth-child(n+3) td:first-child {
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
            width: 10%;
        }

        .display-Approvaltable .table tr:nth-child(n+3) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            /*background-color: white;*/
            background: inherit;
            left: 33px;
        }

        .display-Approvaltable .table tr:first-child td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 190px;
            background: inherit;
            z-index: 2;
            width: 10%;
        }

        .display-Approvaltable .table tr:nth-child(n+3) td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            /*background-color: white;*/
            background: inherit;
            left: 190px;
        }

        .display-Approvaltable .table tr:first-child td:nth-child(4) {
            width: 10%;
        }
        /*Fixed Heading & Fixed Column-End*/
    </style>
</head>
<body>
    <%--<div id="divLoader" visible="true" runat="server" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>--%>
    <form id="form1" runat="server">
        <div>
            <br />
            <table width="100%">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel()">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                    <%--<asp:ImageButton ID="btnPrint" ImageUrl="~/Images/printer.png" runat="server" Width="35px" Height="30px" OnClientClick="return PrintPanel()" />--%>
                                </td>

                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                    <%--<asp:ImageButton ID="btnExcel" ImageUrl="~/Images/Excels.png" runat="server" Height="30px" Width="35px" />--%>
                                </td>

                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" Visible="false">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label1" runat="server" Text="Pdf" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                    <%--<asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        Visible="false" />--%>
                                </td>

                                <td style="padding-right: 40px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                    <%--<asp:ImageButton ID="btnClose" ImageUrl="~/Images/closebtn.png" runat="server" Height="30px" Width="35px" OnClientClick="RefreshParent()" />--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />

            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">

                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="Visit Details for the month of " CssClass="reportheader"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="LblForceName" runat="server" CssClass="label"
                                    Font-Size="18px"></asp:Label>
                            </div>

                            <br />
                            <br />

                            <div class="display-Approvaltable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                    <asp:Panel runat="server">
                                        <table width="100%">
                                            <caption>
                                                <asp:GridView ID="GrdDoctor" runat="server" AlternatingRowStyle-CssClass="alt"
                                                    AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                                    GridLines="None" HorizontalAlign="Center"
                                                    OnRowCreated="GVMissedCall_RowCreated" OnRowDataBound="GrdDoctor_RowDataBound"
                                                    ShowHeader="false" Width="100%">

                                                    <Columns>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </caption>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div>
                                <div class="divProgress" id="ShowProgressDiv" runat="server" style="display: none">
                                    <div class="ball" id="ball" runat="server">
                                    </div>
                                    <div class="ball1" id="ball1" runat="server">
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
