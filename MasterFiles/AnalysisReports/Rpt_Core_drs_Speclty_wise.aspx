<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Core_drs_Speclty_wise.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Rpt_Core_drs_Speclty_wise" %>

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


    .table .input {
        border-radius: 8px;
        /*border: 1px solid #d1e2ea;*/
        border: 1px solid #fff000;
        background-color: #f4f8fa;
        color: #90a1ac;
        font-size: 12px;
        /*width: 70%;*/
        padding-left: 10px;
        height: 25px;
        padding-right: 10px;
        /*margin-top:5px;*/
    }

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
        top: 30px;
        z-index: 0;
        background: inherit;
    }

    .stickyThirdRow {
        position: sticky;
        position: -webkit-sticky;
        top: 0;
        z-index: 2;
        background: inherit;
    }

    #page {
        display: none;
    }

    .display-Approvaltable .table tr:first-child, .display-Approvaltable .table tr:nth-child(2) {
        padding: 20px 10px;
        border-bottom: 2px solid #dce2e8;
    }

    .display-Approvaltable .table td {
        border-color: #DCE2E8;
        border-right: none;
    }
</style>



<script type="text/javascript" language="Javascript">
    function RefreshParent() {
        window.opener.document.getElementById('form1').click();
        window.close();
    }
</script>
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


<body>

    <form id="form1" runat="server">

        <div>

            <div class="container clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <br />
                        <div class="row justify-content-center">
                            <div class="col-lg-9">
                            </div>
                            <div class="col-lg-3">
                                <table width="100%">
                                    <tr>
                                        <td></td>
                                        <td align="right">
                                            <table>
                                                <tr>
                                                    <td style="padding-right: 30px">
                                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" Width="20px" OnClick="btnPrint_Click">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>

                                                    </td>
                                                    <td style="padding-right: 15px">
                                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" Width="20px" runat="server">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="btnPDF" ToolTip="Excel" Width="20px" runat="server" OnClick="btnPDF_Click" Visible="false">
                                                            <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label1" runat="server" Text="Pdf" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>

                                                    </td>
                                                    <td style="padding-right: 50px">
                                                        <asp:LinkButton ID="btnClose" ToolTip="Close" Width="20px" runat="server" OnClientClick="RefreshParent();">
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
                        <asp:Panel ID="pnlContents" runat="server">
                            <div class="display-Approvaltable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                    <%--<asp:Table ID="tblhq" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                    Width="95%">
                                </asp:Table>--%>
                                    <div align="center">
                                        <asp:Label ID="lblHead" runat="server" Text="Manager - HQ - Coverage from " CssClass="reportheader"></asp:Label>
                                        <br />
                                        <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" Font-Size="18px" ForeColor="#696d6e"></asp:Label>
                                    </div>
                                    <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found" BorderColor="WhiteSmoke"
                                        GridLines="Both" HorizontalAlign="Center" BorderWidth="1" OnRowCreated="GrdFixation_RowCreated"
                                        ShowHeader="False" Width="100%" OnRowDataBound="GrdFixation_RowDataBound">

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
