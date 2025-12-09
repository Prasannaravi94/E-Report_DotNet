<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_TP_View.aspx.cs" Inherits="MasterFiles_Report_rpt_TP_View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%-- <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../../assets/css/style.css" />

    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
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
    <script language="javascript" type="text/javascript">
        function popUp(Terr, Sf_Code, Date, Div_Code, mon, yr, type) {
            strOpen = "TerritoryWiseDrList.aspx?Terr=" + Terr + "&Sf_Code=" + Sf_Code + "&Date=" + Date + "&Div_Code=" + Div_Code + "&mon=" + mon + "&yr=" + yr + "&type=" + type
            window.open(strOpen, 'popWindow', '');
        }
    </script>
    <style type="text/css">
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .display-table .table td , .display-table .table th {
            border-color: #DCE2E8;
            border-right: none;
        }
    </style>
</head>
<body>
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
                                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="btnPDF" ToolTip="PDF" runat="server" Visible="false" OnClick="btnPDF_Click">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label3" runat="server" Text="PDF" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                                    <%--<asp:Button ID="btnPDF" runat="server" Text="PDF" Visible="false" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnPDF_Click" />--%>
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
                </div>
        </asp:Panel>
        <br />


        <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <div align="center">
                            <asp:Label ID="lblHead" runat="server" Text="TP View" class="reportheader" Font-Bold="True"></asp:Label>
                            <asp:Label ID="lblHq" runat="server" class="reportheader" Font-Size="9pt" Font-Bold="True"></asp:Label>

                        </div>
                        <center>

                        <div id="tblStatus" style="padding-left: 50px" runat="server" width="90%" class="common"
                            align="left">

                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="lblFieldForce" runat="server" Text="FieldForce Name" ForeColor="black"
                                        CssClass="label"></asp:Label>
                                    <asp:Label ID="lblFieldForceValue" runat="server" CssClass="label" ForeColor="Maroon"></asp:Label>
                                    <asp:Label ID="lbltpfieldforcenames" runat="server" CssClass="label" ForeColor="Maroon"></asp:Label>

                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lblValHQ" Text="HQ" runat="server" CssClass="label" ForeColor="black"></asp:Label>
                                    <asp:Label ID="lblHQValue" runat="server" CssClass="label" ForeColor="Maroon"></asp:Label>

                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lblDesgn" Text="Designation" runat="server" CssClass="label" ForeColor="black"></asp:Label>
                                    <asp:Label ID="lblDesgnValue" runat="server" CssClass="label" ForeColor="Maroon"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="lblstatus" runat="server" Text="Status" CssClass="label" ForeColor="black"></asp:Label>
                                    :<asp:Label ID="lblstatusdetail" runat="server" Text="" CssClass="label"></asp:Label>

                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lblCompleted" runat="server" Text="Completed Date/Time" CssClass="label" ForeColor="black"></asp:Label>
                                    <asp:Label ID="lblCompletedValue" runat="server" CssClass="label" ForeColor="Magenta"></asp:Label>

                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lblConfirmed" runat="server" Text="Confirmed Date/Time" CssClass="label" ForeColor="black"></asp:Label>
                                    <asp:Label ID="lblConfirmedValue" runat="server" Font-Size="10pt" Width="140px" CssClass="TPFontSize" ForeColor="DarkGreen"></asp:Label>

                                </div>

                            </div>


                            <%-- <table align="center" width="100%">
                    <tr>
                        <td width="10%">
                            <asp:Label ID="lblFieldForce" runat="server" Font-Size="9pt" Text="FieldForce Name"
                                CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="30%">
                            <asp:Label ID="lblFieldForceValue" Font-Size="9pt" runat="server" CssClass="TPFontSize"></asp:Label>
                            <asp:Label ID="lbltpfieldforcenames" Font-Size="9pt" runat="server" CssClass="TPFontSize"></asp:Label>
                        </td>
                        
                       
                        <td width="10%">
                            <asp:Label ID="lblValHQ" Text="HQ" Font-Size="9pt" runat="server" CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="15%">
                            <asp:Label ID="lblHQValue" runat="server" Font-Size="9pt" CssClass="TPFontSize"></asp:Label>
                        </td>
                        
                        <td width="10%">
                        
                        </td>
                        <td width="10%">
                            <asp:Label ID="lblDesgn" Text="Designation" Font-Size="9pt"  runat="server" CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="10%">
                            <asp:Label ID="lblDesgnValue" runat="server" Font-Size="9pt" CssClass="TPFontSize"></asp:Label>
                        </td>
                    </tr>
                </table>--%>
                        </div>
                        <%--<div style="padding-left: 50px" runat="server" width="90%" class="common" align="left">
                <table align="center" width="90%">
                    <tr>
                       
                        <td width="15%">
                            <asp:Label ID="lblCompleted" runat="server" Font-Size="9pt" Text="Completed Date/Time"
                                CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="15%">
                            <asp:Label ID="lblCompletedValue" runat="server" Font-Size="9pt" CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="10%">
                        </td>
                        
                        <td width="15%">
                            <asp:Label ID="lblConfirmed" runat="server" Font-Size="9pt" Text="Confirmed Date/Time"
                                CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="15%">
                            <asp:Label ID="lblConfirmedValue" runat="server" Font-Size="9pt" Width="140px" CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="10%">
                        </td>
                        <td width="15%">
                        </td>
                        <td width="15%">
                        </td>
                    </tr>
                </table>
            </div>--%>
                        <div>
                            <asp:Label ID="summ" runat="server" Text="Consolidated Summary"
                                class="reportheader"></asp:Label>
                        </div>
                        <br />

                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:GridView ID="grdSummary" runat="server" Width="50%" AutoGenerateColumns="false"
                                    EmptyDataText="No Data found for View" CssClass="table" GridLines="Both" BorderColor="WhiteSmoke" BorderWidth="1">

                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="45px">
                                            <ItemTemplate>
                                                <asp:Label ID="lbno" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particular">
                                            <ItemTemplate>
                                                <asp:Label ID="lblparticular" runat="server" Text='<%#Eval("type_nam") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No.of Days">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldays" runat="server" Text='<%#Eval("days") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <br />

                        <%--<table width="90%">
                <tr>
                    <td style="width: 45%;" align="left">
                        
                    </td>
                    <td style="width: 30%;" align="left">
                       
                    </td>
                    <td style="width: 35%;" align="left">
                    </td>
                </tr>
            </table>--%>

                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                <div align="center" width="100%">
                                    <asp:GridView ID="grdTP" runat="server" Width="100%" HorizontalAlign="Center"
                                        CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                                        OnRowDataBound="grdTP_RowDataBound" CssClass="table" GridLines="Both" BorderColor="WhiteSmoke" BorderWidth="1">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-Width="50" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" ItemStyle-Width="70" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTourPlan" runat="server" Text='<%#Eval("tour_date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Work Type" ItemStyle-Width="100" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorkType" runat="server" Text='<%# Bind("Worktype_Name_B") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Type" ItemStyle-Width="200" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbltype" runat="server" Font-Size="9pt" Text='<%# Bind("type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Joint Work (WorkedWith)" ItemStyle-Width="200" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorkWithSFName" runat="server" Text='<%# Bind("Worked_With_SF_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='<%# Eval("Tour_Schedule1") %>'
                                                        OnClientClick='<%# "return popUp(\"" + Eval("Tour_Schedule1") + "\",\"" + Eval("sf_code")  + "\",\"" + Eval("tour_date")  + "\",\"" + Eval("division_code")  + "\",\"" + Eval("mon")  + "\",\"" + Eval("yr")  + "\",\"" + Eval("type")  + "\");" %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Work Type" ItemStyle-Width="100" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorkType2" runat="server" Text='<%# Bind("Worktype_Name_B1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Territory Planned2" ItemStyle-Width="200" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblterr2" runat="server" Text='<%# Bind("Tour_Schedule2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Type" ItemStyle-Width="100" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorkType3" runat="server" Text='<%# Bind("Worktype_Name_B2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Territory Planned3" ItemStyle-Width="200" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblterr3" runat="server" Text='<%# Bind("Tour_Schedule3") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" ItemStyle-Width="200" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltype" runat="server" Text='<%# Bind("type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Objective" ItemStyle-Width="300" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblObjective" runat="server" Text='<%#Eval("objective")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Manager_JFW" ItemStyle-Width="100" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblManager_JFW" runat="server" Text='<%# Bind("Manager_JFW") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="mon" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="mon" runat="server" Text='<%#Eval("mon")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="yr" ItemStyle-Width="70"
                                                ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="yr" runat="server" Text='<%#Eval("yr")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                    <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                        Width="80%" align="center">
                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                        </center>
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
