<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Consolidated_Report_Zoom.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_rpt_Consolidated_Report_Zoom" %>

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

    #page {
        display: none;
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
<script language="javascript" type="text/javascript">
    function callServerButtonEvent(a, b) {
        document.getElementById('<%=lblMonthExc.ClientID%>').value = a;
        document.getElementById("LinkBtn").click();
    }
</script>
<body>
    <form id="form1" runat="server">
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row justify-content-center">
                    <div class="col-lg-2">
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
                    </div>
                    <div class="col-lg-7">
                        <div align="center">
                            <br />
                            <asp:Label ID="lblHead" runat="server" Text="Manager - HQ - Coverage from " CssClass="reportheader"></asp:Label>
                            <br />
                            <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" Font-Size="18px" ForeColor="#696d6e"></asp:Label>
                        </div>
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
                                            <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
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
                                        <%--<td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPDF_Click" Visible="false" />
                                </td>--%>
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
        </div>
        <br />
        <div class="container clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <asp:Panel ID="pnlContents" runat="server">

                        <br />
                        <div class="display-Approvaltable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <%--<asp:Table ID="tblhq" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                    Width="95%">
                                </asp:Table>--%>
                                <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found" style="background-color:white"
                                    GridLines="None" HorizontalAlign="Center" BorderWidth="0" OnRowCreated="GrdFixation_RowCreated"
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
