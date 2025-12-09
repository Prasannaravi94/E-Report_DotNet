<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTerrTypeWise_DrVisit.aspx.cs" Inherits="MIS_Reports_rptTerrTypeWise_DrVisit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Territory TypeWise Doctor Visit</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <%--<link type="text/css" rel="stylesheet" href="../css/Report.css" />--%>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

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

    <script language="javascript" type="text/javascript">
        function popUp(sfcode, sf_name, Terr_Name, Terr_Code, Fmonth, Fyear) {
            strOpen = "Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + sfcode + "&FMnth=" + Fmonth + "&FYear=" + Fyear + "&TMonth=" + Terr_Name + "&TYear=" + Terr_Code + "&mode=" + 6 + "&sf_name=" + sf_name
            window.open(strOpen, 'popWindow', '');
        }
    </script>

    <script type="text/javascript" language="Javascript">
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

        .SubTotalRowStyle {
            border: solid 1px White;
            background-color: #81BEF7;
            font-weight: bold;
        }

        .alingment {
            max-width: 100px;
            word-wrap: break-word;
        }
        /*Fixed Heading & Fixed Column-Begin*/
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 2; /*1*/
            background: inherit;
        }

        .display-reportMaintable .table tr th:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 1;
        }

        .display-reportMaintable .table tr th:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 38px;
            /*background: inherit;*/
            z-index: 2;
            min-width: 158px;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 38px;
        }

        .display-reportMaintable .table tr th:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 198px;
            /*background: inherit;*/
            z-index: 2;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 198px;
        }

        .display-reportMaintable .table tr th:nth-child(4) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 298px;
            /*background: inherit;*/
            z-index: 2;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:nth-child(4) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 298px;
        }
        /*Fixed Heading & Fixed Column-End*/
        .display-table .table td {
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
                                    <%-- <td width="80%" align="center">
                            <asp:Label ID="lblHead" Text="Territory TypeWise Doctor Visit" Font-Bold="true" ForeColor="#794044" Font-Underline="true"
                                runat="server" Font-Size="16px"></asp:Label>
                        </td>--%>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td style="padding-right: 30px">
                                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
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
                                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
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
     
        <div class="container clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <div>
                            <div align="center">
                                <%--<asp:Label ID="lblHead" Text="Product Exposure Analysis" SkinID="lblMand" Font-Underline="true"
                                    runat="server"></asp:Label>--%>
                                <asp:Label ID="lblHead" Text="Territory TypeWise Doctor Visit" CssClass="reportheader" runat="server"></asp:Label>
                            </div>
                          
                            <div class="row">
                                <div class="col-lg-8">
                                    <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                    <asp:Label ID="lblRegionName" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                </div>
                                <%-- <div class="col-lg-6">
                                    <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                                </div>--%>
                            </div>
                            <br />
                            <div class="display-table clearfix">
                                <%--       <div class="display-reportMaintable clearfix">--%>
                                <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                    <table width="100%" align="center">
                                        <tbody>
                                            <tr>
                                                <td align="center">
                                                    <asp:GridView ID="grdTerr" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found" OnRowCreated="grdTerr_onrowCreated"
                                                        AutoGenerateColumns="false" GridLines="Both" CssClass="table" AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdTerr_RowDataBound"
                                                        AllowSorting="True" HeaderStyle-CssClass="stickyFirstRow" Style="background-color: white" BorderColor="WhiteSmoke" BorderWidth="1">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="id" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="id" runat="server" Text='<%#Eval("id")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsf_name" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("sf_designation_short_name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsf_hq" runat="server" Text='<%#Eval("sf_hq")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Territory Name" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblterritory_Code" runat="server" Text='<%#Eval("Territory_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="No.of Drs Available" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotal_Dr" runat="server" Text='<%#Eval("Total_Dr")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="No.of A Drs" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCore_dr" runat="server" Text='<%#Eval("Core_dr")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="No.of APlus Drs" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblnon_coredr" runat="server" Text='<%#Eval("non_coredr")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="No.of V Drs" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsuper_coredr" runat="server" Text='<%#Eval("super_coredr")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%-- ABP--%>
                                                            <asp:TemplateField HeaderText="No of Dr Visited" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='<%# Eval("Dr_Visited") %>'
                                                                        OnClientClick='<%# "return popUp(\"" + Eval("sf_code") + "\",\"" + Eval("sf_name")  + "\",\"" + Eval("Territory_Name")  + "\",\"" + Eval("territory_code")  + "\",\"" + Eval("Dr_Month")  + "\",\"" + Eval("Dr_Year")  + "\");" %>'>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- ABP--%>
                                                            <asp:TemplateField HeaderText="A Dr Visited" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDr_Vst_core" runat="server" Text='<%# Bind("Dr_Vst_core") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="APlus Dr Visited" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDr_Vst_noncore" runat="server" Text='<%# Bind("Dr_Vst_noncore") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="V Dr Visited" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDr_Vst_supercore" runat="server" Text='<%# Bind("Dr_Vst_supercore") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Date of Visit" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVisited_Days" runat="server" Text='<%# Bind("Visited_Days") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Missed Dr" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMissed_Dr" runat="server" Text='<%# Bind("Missed_Dr") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Missed A Dr" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMissed_Dr_core" runat="server" Text='<%# Bind("Missed_Dr_core") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Missed APlus Dr" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMissed_Dr_noncore" runat="server" Text='<%# Bind("Missed_Dr_noncore") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Missed A Dr" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMissed_Dr_supercore" runat="server" Text='<%# Bind("Missed_Dr_supercore") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:GridView ID="grdspec" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found" Visible="false"
                                                        AutoGenerateColumns="false" GridLines="Both" CssClass="table" AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdTerr_RowDataBound"
                                                        AllowSorting="True" HeaderStyle-CssClass="stickyFirstRow" BorderColor="WhiteSmoke" BorderWidth="1">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsf_name" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("sf_designation_short_name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsf_hq" runat="server" Text='<%#Eval("sf_hq")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblspe" runat="server" Width="120px" Text='<%#Eval("Doc_Special_SName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="No.of Drs Available" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotal_Dr" runat="server" Text='<%#Eval("Total_Dr")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="No of Dr Visited" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDr_Visited" runat="server" Text='<%# Bind("Dr_Visited") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Date of Visit" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="alingment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVisited_Days" runat="server" Text='<%# Bind("Visited_Days") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Missed Dr" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMissed_Dr" runat="server" Text='<%# Bind("Missed_Dr") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
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


