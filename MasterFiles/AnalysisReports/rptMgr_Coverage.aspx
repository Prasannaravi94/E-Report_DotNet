<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptMgr_Coverage.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_rptMgr_Coverage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SAN eReport</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />


    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>

</head>

<style type="text/css">
    .tblCellFont {
        font-size: 9pt;
        font-family: Calibri;
    }

    #loading {
        display: block;
        position: absolute;
        top: 0;
        left: 0;
        z-index: 100;
        width: 100vw;
        height: 100vh;
        background-color: rgba(192, 192, 192, 0.5);
        background-image: url("../../Images/loader.gif");
        background-repeat: no-repeat;
        background-position: center;
    }

    #page {
        display: none;
    }

    .display-reporttable .table tr:first-child td {
        background-color: #F1F5F8;
        padding: 20px 10px;
        border-bottom: 10px solid #fff;
        border-top: 0px;
        font-size: 14px;
        font-weight: 400;
        text-align: center;
        border-left: 1px solid #DCE2E8;
        vertical-align: inherit;
    }

        .display-reporttable .table tr:first-child td:first-child, .display-Approvaltable .table tr:first-child td:first-child {
            border-top: 0px;
            border-left: 0px;
            background-color: #F1F5F8;
        }

    .display-reporttable .table tr td:first-child, .display-Approvaltable .table tr td:first-child {
        border-top: 1px solid #dee2e6;
        /*background-color: white;*/
    }

    .display-reporttable .table tr:nth-child(2) td:first-child {
        color: #636d73;
    }

    .display-Approvaltable .table tr:first-child td:first-child, .display-Approvaltable .table tr:nth-child(3) td, .display-Approvaltable .table tr:nth-child(2) td {
        background-color: #F1F5F8;
        color: #636d73;
        border-bottom: 1px solid #fff;
        font-size: 14px;
    }

        .display-Approvaltable .table tr:nth-child(3) td:first-child {
            background-color: #F1F5F8;
            color: #636d73;
            border-left: 1px solid #DCE2E8;
             font-size:12px;

        }

        .display-Approvaltable .table tr:nth-child(2) td:first-child
   {
       text-align:left;
   }


 .display-reporttable .table tr td:first-child 
   {
        padding: 2px 6px;
       font-size:10px;
   }
 .display-Approvaltable .table tr:nth-child(3) td {
            text-align:left;
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
        top: 38px;
        z-index: 0;
        background: inherit;
    }

    .stickyThirdRow {
        position: sticky;
        position: -webkit-sticky;
        top: 80px;
        z-index: 1;
        background: inherit;
    }

    .display-Approvaltable .table tr:first-child td:first-child {
        position: sticky;
        position: -webkit-sticky;
        top: 0;
        left: 0;
        z-index: 2;
         text-align:center;
    }

    .display-Approvaltable .table tr:nth-child(n+3) td:first-child {
        position: -webkit-sticky;
        position: sticky;
        left: 0;
        z-index: 0;
         font-size:11px;
        text-align:left;
    }

    .display-Approvaltable .table tr:first-child, .display-Approvaltable .table tr:nth-child(2), .display-Approvaltable .table tr:nth-child(3) {
        padding: 20px 10px;
        border-bottom: 2px solid #dce2e8;
    }

    .display-Approvaltable .table tr td:first-child {
        padding: 3px 3px;
    }

    .display-Approvaltable .table td {
        padding: 10px 8px;
    }
    .display-reporttable .table td, .display-Approvaltable .table td {

    border-color: #DCE2E8;
    border-right: none;
     padding : 1px 2px;
}
   
   
    /*.display-Approvaltable .table tr:nth-child(2) td {
            border-bottom: 1px solid #fff;
        }
         .display-Approvaltable .table tr:nth-child(3) td {
            border-bottom: 1px solid #fff;
        }
    .display-Approvaltable .table tr td:first-child {
        padding: 19px 10px;
    }*/
    /*Fixed Heading & Fixed Column-End*/
</style>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>

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
            $(this).css("font-size", "15px");
        });
        $(".btnDrMt").mouseout(function () {
            $(this).css("color", "Fuchsia");
            $(this).css("font-weight", "normal");
            $(this).css("font-size", "13px");
        });
        //
        $(".btnDrSn").mouseover(function () {
            $(this).css("color", "red");
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
    function showModalPopUp(sfcode, FMonth, FYear, Tmon, Tyr, mode, sf_name, SfMGR) {
        popUpObj = window.open("rptMgr_Coverage_Zoom.aspx?sfcode=" + sfcode + "&FMnth=" + FMonth + "&FYear=" + FYear + "&TMonth=" + Tmon + "&TYear=" + Tyr + "&mode=" + mode + "&sf_name=" + sf_name + "&SfMGR=" + SfMGR,
        "_blank",
    "ModalPopUp_Level1," +
     "0" //+
    //"toolbar=no," +
    //"scrollbars=1," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"status=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=650," +
    //"height=450," +
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
<body style="overflow-x: scroll;">

    <form id="form1" runat="server">
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row justify-content-center">
                    <div class="col-lg-9">
                        <%-- <div align="center">
                            <br />
                            <asp:Label ID="lblHead" runat="server" Text="Manager - HQ - Coverage from " CssClass="reportheader"></asp:Label>
                            <br />
                            <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="18px"></asp:Label>
                        </div>--%>
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
                                                <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                </asp:LinkButton>
                                                <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>

                                            </td>
                                            <td style="padding-right: 15px">
                                                <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" >
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                </asp:LinkButton>
                                                <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                            </td>
                                            <td style="padding-right: 15px">
                                                <asp:LinkButton ID="btnPDF" ToolTip="PDF" runat="server" Visible="false" OnClick="btnPDF_Click">
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                </asp:LinkButton>
                                                <asp:Label ID="Label3" runat="server" Text="PDF" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>

                                            </td>
                                            <td style="padding-right: 50px">
                                                <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
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
                <div>

                    <div class="container clearfix" style="max-width: 1350px;">
                        <div class="row justify-content-center">
                            <div class="col-lg-12">
                                <asp:Panel ID="pnlContents" runat="server" Width="100%">

                                    <div class="row justify-content-center">
                                        <div class="col-lg-12">
                                            <div align="center">
                                                <br />
                                                <asp:Label ID="lblHead" runat="server" Text="Manager - HQ - Coverage from " CssClass="reportheader"></asp:Label>
                                                <br />
                                                <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="18px"></asp:Label>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="display-reporttable clearfix">
                                        <div class="table-responsive" style="scrollbar-width: thin; overflow-x: inherit;">
                                            <asp:Table ID="tbl" runat="server" BorderStyle="None" BorderWidth="1" GridLines="Both" CssClass="table" BorderColor="WhiteSmoke"
                                                Width="100%" Style="background-color: white">
                                            </asp:Table>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="display-Approvaltable clearfix">

                                        <div class="table-responsive" style="overflow-x: inherit">
                                            <asp:GridView ID="grdMgr" runat="server" AlternatingRowStyle-CssClass="alt"
                                                AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found" BorderColor="WhiteSmoke"
                                                GridLines="Both" HorizontalAlign="Center" BorderWidth="1" OnRowCreated="grdMgr_RowCreated"
                                                ShowHeader="False" Width="99%" OnRowDataBound="grdMgr_RowDataBound" Style="background-color: white;">

                                                <Columns>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </div>



                                        <div class="table-responsive" style="overflow-x: inherit">
                                            <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                                AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found" BorderColor="WhiteSmoke"
                                                GridLines="Both" HorizontalAlign="Center" BorderWidth="1" OnRowCreated="GrdFixation_RowCreated"
                                                ShowHeader="False" Width="99%" OnRowDataBound="GrdFixation_RowDataBound" Style="background-color: white;">

                                                <Columns>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <br />
                    <br />

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
