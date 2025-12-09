<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptDrs_Add_Del_at_a_glance.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_RptDrs_Add_Del_at_a_glance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    </style>
        <link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
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
                $(this).css("color", "black");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "15px");
            });
            $(".btnLstDr").mouseout(function () {
                $(this).css("color", "brown");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            //
            $(".btnDrMt").mouseover(function () {
                $(this).css("color", "darkgreen");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "15px");
            });
            $(".btnDrMt").mouseout(function () {
                $(this).css("color", "red");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "13px");
            });
            //
            $(".btnDrSn").mouseover(function () {
                $(this).css("color", "Fuchsia");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "15px");
            });
            $(".btnDrSn").mouseout(function () {
                $(this).css("color", "blue");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "13px");
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
        var popUpObj;
        function showModalPopUp(sfcode, FMonth, FYear, Tmon, Tyr, mode, Sf_Name, SfMGR) {
            popUpObj = window.open("RptDrs_Add_Del_at_a_glance_Zoom.aspx?sfcode=" + sfcode + "&FMnth=" + FMonth + "&FYear=" + FYear + "&TMonth=" + Tmon + "&TYear=" + Tyr + "&mode=" + mode + "&Sf_Name=" + Sf_Name + "&SfMGR=" + SfMGR,
            "_blank",
        "ModalPopUp_Level1," +
         "0," +
        "toolbar=no," +
        "scrollbars=1," +
        "location=no," +
        "statusbar=no," +
        "menubar=no," +
        "status=no," +
        "addressbar=no," +
        "resizable=yes," +
        "width=650," +
        "height=450," +
        "left = 0," +
        "top=0"
        );
            popUpObj.focus();
            //LoadModalDiv();
        }
    </script>
    <script language="javascript" type="text/javascript">
        function callServerButtonEvent(a, b, c) {
            document.getElementById('<%=lblMonthExc.ClientID%>').value = a;
            document.getElementById('<%=lblYearExc.ClientID%>').value = b;
            document.getElementById('<%=lblMode.ClientID%>').value = c;
            document.getElementById("btnExcelGrid").click();
        }   
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="lblMonthExc" runat="server" ForeColor="White" Width="0.1px" Height="0.1px"
                        BackColor="White" BorderColor="White"></asp:TextBox>
                    <asp:TextBox ID="lblYearExc" runat="server" ForeColor="White" Width="0.1px" Height="0.1px"
                        BackColor="White" BorderColor="White"></asp:TextBox>
                    <asp:TextBox ID="lblMode" runat="server" ForeColor="White" Width="0.1px" Height="0.1px"
                        BackColor="White" BorderColor="White"></asp:TextBox>
                    <asp:Button ID="btnExcelGrid" runat="server" Font-Names="Verdana" Font-Size="1px"
                        BackColor="White" BorderColor="White" Width="0.1px" Height="0.1px" OnClick="btnExcelGrid_Click" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td width="80%">
                </td>
                <td style="float: right;">
                    <table>
                        <tr style="float: right; margin-right: 10px;">
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnPrint_Click" />
                            </td>
                            &nbsp;&nbsp;&nbsp;
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />
                            </td>
                            &nbsp;&nbsp;&nbsp;
                            <td>
                                <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnPDF_Click" Visible="false" />
                            </td>
                            &nbsp;&nbsp;&nbsp;
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClientClick="RefreshParent()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlContents" runat="server">
            <center>
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="POB Wise Report for the month of " Font-Underline="True"
                        Font-Bold="True" Font-Names="Verdana" Font-Size="11pt"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                        Font-Size="9pt"></asp:Label>
                </div>
        </center>
            <br />
            <center>
              <asp:Panel ID="Panel1" runat="server">
                <table width="95%"> 
                    <caption>
                        <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt" 
                            AutoGenerateColumns="true" CssClass="mGrids" EmptyDataText="No Records Found" 
                            GridLines="Both" HorizontalAlign="Center"  BorderWidth="1"
                            OnRowCreated="GrdFixation_RowCreated" onrowdatabound="GrdFixation_RowDataBound" 
                            ShowHeader="False" Width="90%" Font-Names="calibri" Font-Size="Small">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt" />
                            <RowStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="Small" Font-Names="calibri" />
                            <Columns>
                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" ForeColor="Black" 
                                Height="5px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </caption>
                </table>
                <br />
            </asp:Panel>
       </center>
        </asp:Panel>
    </div>
    
    </div>
    </form>
</body>
</html>
