<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Analysis_Expand.aspx.cs" Inherits="MIS_Reports_DCR_Analysis_Expand" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--   <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

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

        .display-table .table th:first-child {
            width: 5px;
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
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row justify-content-center">
                    <div class="col-lg-9">
                        <center>
                            <div align="center">
                                <br />
                                <asp:Label ID="lblHead" runat="server" Text="Visit Details for the month of " CssClass="reportheader"></asp:Label>
                                <br />

                                <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="18px"></asp:Label>
                            </div>
                        </center>
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
                                                <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel()">
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
            <br />
            <br />

            <div class="container clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">


                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <left>
            <asp:Panel runat="server">
                <table width="100%" > 
                    <caption>
                        <asp:GridView ID="GrdJoint" runat="server" AutoGenerateColumns="true" 
                            BorderWidth="0" AllowSorting="true" 
                        EmptyDataText="No Records Found" GridLines="None" HorizontalAlign="Center" 
                            Width="100%" OnSorting="grdDoctor_Sorting" CssClass="table" 
                            onrowdatabound="GrdJoint_RowDataBound">
                           
                            <Columns>
                            </Columns>
                            <EmptyDataRowStyle CssClass="no-result-area" />
                        </asp:GridView>
                    </caption>
                </table>
                <table width="100%" > 
                    <caption>
                        <asp:GridView ID="GrdTtl" runat="server" AutoGenerateColumns="true" BorderWidth="0" AllowSorting="true" style="background-color:white;"
                        EmptyDataText="No Records Found" GridLines="None" HorizontalAlign="Center" Width="100%" OnSorting="grdDoctor_Sorting" CssClass="table">
                           
                            <Columns>
                            </Columns>
                            <EmptyDataRowStyle CssClass="no-result-area"/>
                        </asp:GridView>
                    </caption>
                </table>
       
                <table width="100%" > 
                    <caption>
                        <asp:GridView ID="GrdUnLst" runat="server" AutoGenerateColumns="true" BorderWidth="0" AllowSorting="true" style="background-color:white;" 
                        EmptyDataText="No Records Found" GridLines="None" HorizontalAlign="Center" Width="100%" OnSorting="grdDoctor_Sorting" CssClass="table">
                           
                            <Columns>
                            </Columns>
                            <EmptyDataRowStyle CssClass="no-result-area" />
                        </asp:GridView>
                    </caption>
                </table>
                <table width="100%" > 
                    <caption>
                        <asp:GridView ID="GrdChmst" runat="server" AutoGenerateColumns="true" BorderWidth="0" AllowSorting="true" style="background-color:white;"
                        EmptyDataText="No Records Found" GridLines="None" HorizontalAlign="Center" Width="100%" OnSorting="grdDoctor_Sorting" CssClass="table">
                            
                            <Columns>
                            </Columns>
                            <EmptyDataRowStyle CssClass="no-result-area"/>
                        </asp:GridView>
                    </caption>
                </table>
            </asp:Panel>
        </left>
                                </div>
                            </div>
                            <br />
                     
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
        <%--<div class="" id="divLoader" runat="server" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>--%>
<%--<script type="text/javascript">
    document.body.style.backgroundColor = '<%= Session["Div_color"].ToString() %>'
</script>--%>

    </form>
</body>
</html>
