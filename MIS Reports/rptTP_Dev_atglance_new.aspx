<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTP_Dev_atglance_new.aspx.cs" Inherits="MIS_Reports_rptTP_Dev_atglance_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP - Deviation</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, sf_name, fyear, fmonth) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptTP_Dev_atglance_new2.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&FYear=" + fyear + "&FMonth=" + fmonth,
       "_blank",
        "ModalPopUp" //+
        //"toolbar=no," +
        //"scrollbars=yes," +
        //"location=no," +
        //"statusbar=no," +
        //"menubar=no," +
        //"addressbar=no," +
        //"resizable=yes," +
        //"width=700," +
        //"height=500," +
        //"left = 0," +
        //"top=0"
        );
            popUpObj.focus();
            // LoadModalDiv();
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
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        .remove {
            text-decoration: none;
        }

        .display-reportMaintable .table tr:first-child td:first-child {
            border-radius: 8px 0 0 8px;
            background-color: #414d55;
            color: #ffffff;
            font-size: 14px;
            font-weight: 400;
            border-left: 0px solid #F1F5F8;
        }

        .display-reportMaintable .table tr:first-child td {
            padding: 20px 10px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            background-color: #F1F5F8;
        }
        /*Fixed Heading & Fixed Column-Begin*/
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .display-reportMaintable .table tr:first-child td:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
            width: 2%;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 0;
        }

        .display-reportMaintable .table tr:first-child td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 33px;
            /*background: inherit;*/
            z-index: 2;
            min-width: 158px;
            width: 20%;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            /*background-color: white;*/
            background: inherit;
            left: 33px;
            max-width: 150px;
        }

        .display-reportMaintable .table tr:first-child td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 190px;
            /*background: inherit;*/
            z-index: 2;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            /*background-color: white;*/
            background: inherit;
            left: 190px;
        }
        /*Fixed Heading & Fixed Column-End*/
        .display-reportMaintable .table td {
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
                                        <%-- <td width="80%" align="center">
                            <asp:Label ID="lblHead" Text="TP - Deviation" SkinID="lblMand" Font-Bold="true" Font-Underline="true"
                                runat="server"></asp:Label>
                        </td>--%>
                                        <td align="right">
                                            <table>
                                                <tr>
                                                    <td style="padding-right: 30px">
                                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="25px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>
                                                    <td style="padding-right: 15px">
                                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="25px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>

                                                    <td style="padding-right: 40px">
                                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" OnClick="btnClose_Click">
                                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="25px" Style="border-width: 0px;" />
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
            </asp:Panel>
            <br />

            <div class="container clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server" Width="100%">
                            <div>
                                <div align="center">
                                    <%--<asp:Label ID="lblHead" Text="Product Exposure Analysis" SkinID="lblMand" Font-Underline="true"
                runat="server"></asp:Label>--%>
                                    <asp:Label ID="lblHead" Text="TP - Deviation" CssClass="reportheader"
                                        runat="server"></asp:Label>
                                </div>

                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                        <asp:Label ID="lblRegionName" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                    </div>
                                    <%-- <div class="col-lg-3">
                                        <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand"></asp:Label>
                                        <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand"></asp:Label>
                                        <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                                    </div>--%>
                                </div>
                                <br />

                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                        <table width="100%" align="center">
                                            <tbody>
                                                <tr>
                                                    <td align="center">
                                                        <%--<asp:GridView ID="grdTP" runat="server" Width="65%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdTP_RowDataBound" 
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField  HeaderText="Territory_Code" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_Code" runat="server" Width="120px" Text='<%#Eval("Territory_Code1")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField  HeaderText="ASper_Dcr" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblASper_Dcr" runat="server" Width="120px" Text='<%#Eval("ASper_Dcr")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField  HeaderText="Date" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" runat="server" Width="80px" Text='<%#Eval("Activity_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField  HeaderText="Day" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate_Name" runat="server" Width="80px" Text='<%#Eval("Date_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="As Per TP" ItemStyle-HorizontalAlign="Left" 
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblAsper_Tp" runat="server" Text='<%# Bind("Asper_Tp") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="As Per DCR" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblASper_Dcr_match" runat="server" Text='<%# Bind("ASper_Dcr_match") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField  HeaderText="WorkType_Name" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblworkDCR" runat="server" Width="120px" Text='<%#Eval("WorkType_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField  HeaderText="Worktype_Name_B" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblworkTP" runat="server" Width="120px" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField  HeaderText="trans_slno" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lbltrans_sl" runat="server" Width="120px" Text='<%#Eval("trans_slno")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                                                         
                            </Columns>
                               <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>--%>
                                                        <asp:GridView ID="GrdPrdExp" runat="server" AlternatingRowStyle-CssClass="alt" Style="background-color: white"
                                                            AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                                            GridLines="Both" HorizontalAlign="Center" OnRowCreated="GrdPrdExp_RowCreated" BorderColor="WhiteSmoke" BorderWidth="1"
                                                            OnRowDataBound="GrdPrdExp_RowDataBound" ShowHeader="False" Width="100%" ShowFooter="true">

                                                            <Columns>
                                                            </Columns>
                                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
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


