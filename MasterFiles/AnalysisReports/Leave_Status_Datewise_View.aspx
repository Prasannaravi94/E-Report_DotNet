<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Leave_Status_Datewise_View.aspx.cs" EnableEventValidation="false" Inherits="MasterFiles_AnalysisReports_Leave_Status_Datewise_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Attendance Status</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../../assets/css/style.css" />
    <%--    <link type="text/css" rel="Stylesheet" href="../../../css/rptMissCall.css" />--%>
    <style>
        .tr_det_head {
            font-family: Verdana;
            color: White;
            /*font-size: 9pt;*/
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }

        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .display-reportMaintable .table td {
            border-color: #DCE2E8;
            border-right: none;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
   <%-- <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>--%>
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
        function Filter() {
            var grid = document.getElementById("<%=GvDcrCount.ClientID %>");
            var row = grid.rows.length;
            var FForce = document.getElementById("GvDcrCount_ctl01_txtSf_Name");
            var Reporting = document.getElementById("GvDcrCount_ctl01_txtReporting");
            var Status = document.getElementById("GvDcrCount_ctl01_txtStatus");

            var FForceValue = FForce.value.toLowerCase();
            var ReportingValue = Reporting.value.toLowerCase();
            var ActiveStatus = Status.value.toLowerCase();

            var FForceSplitter = FForceValue.split();
            var ReportingSplitter = ReportingValue.split();
            var ActiveStatusSplitter = ActiveStatus.split();

            var display = '';

            var FForceRowValue = '';
            var ReportingRowValue = '';
            var StatusRowValue = '';

            for (var i = 1; i < row; i++) {
                display = 'none';

                FForceRowValue = grid.rows[i].cells[1].innerText;
                ReportingRowValue = grid.rows[i].cells[10].innerText;
                StatusRowValue = grid.rows[i].cells[20].innerText;

                for (var m = 0; m < FForceSplitter.length; m++) {
                    for (var n = 0; n < ReportingSplitter.length; n++) {
                        for (var p = 0; p < ActiveStatusSplitter.length; p++) {

                            if (FForceRowValue.toLowerCase().indexOf(FForceSplitter[m]) >= 0 && ReportingRowValue.toLowerCase().indexOf(ReportingSplitter[n]) >= 0 && StatusRowValue.toLowerCase().indexOf(ActiveStatusSplitter[p]) >= 0) {
                                display = '';
                            }

                            else {
                                display = 'none';
                                break;
                            }
                        }
                    }
                }
                grid.rows[i].style.display = display;
            }
        }
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function tog(v) { return v ? 'addClass' : 'removeClass'; }
        $(document).on('input', '.clearable', function () {
            $(this)[tog(this.value)]('x');
        }).on('mousemove', '.x', function (e) {
            $(this)[tog(this.offsetWidth - 18 < e.clientX - this.getBoundingClientRect().left)]('onX');
        }).on('touchstart click', '.onX', function (ev) {
            ev.preventDefault();
            $(this).removeClass('x onX').val('').change();
            Filter();
        });
    </script>
</head>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <div>
            <br />
            <div class="container clearfix" style="max-width: 1350px;">
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
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="20px" Height="20px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>
                                                    <td style="padding-right: 15px">
                                                     <%-- <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" OnClick="btnExcel_Click"
                                                         />--%>
                                                         <asp:LinkButton ID="Button1" runat="server" OnClick="btnExcel_Click">
                                                             <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="20px" Height="20px" Style="border-width: 0px;" />
                                                         </asp:LinkButton>
                                                       <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                               <%-- style="padding-right: 15px">--%>
                                                        <%--<asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClick="btnExcel_Click">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="20px" Height="20px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>--%>
                                                    </td>
                                                    <%--<td>
                                                        <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click">
                                                            <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/Pdf.png" ToolTip="Pdf" Width="20px" Height="20px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="lblPdf" runat="server" Text="Pdf" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>--%>
                                                    <td style="padding-right: 40px">
                                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="20px" Height="20px" Style="border-width: 0px;" />
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
                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="DCR Count -  View for the Period "
                                    CssClass="reportheader"></asp:Label>
                                <br />
                                <asp:Label ID="LblForceName" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblDcrCount" Style="color: red" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                            </div>
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                    <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                        Width="99%">
                                    </asp:Table>
                                    <asp:GridView ID="GvDcrCount" runat="server" AutoGenerateColumns="false" CssClass="table" GridLines="Both"  BorderWidth="1" BorderColor="WhiteSmoke"
                                        OnRowDataBound="GvDcrCount_RowDataBound" EmptyDataText="No Records Found"> <%--AlternatingRowStyle-Height="10px"--%>
                                      <%--  <RowStyle Height="10px" />
                                        <AlternatingRowStyle Height="10px" />--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp<br>Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="20">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSf_Employee" runat="server" Text='<%# Bind("sf_Emp_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfcode" runat="server" Visible="false" Text='<%# Bind("sf_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Field Force Name" ItemStyle-HorizontalAlign="Left"  HeaderStyle-CssClass="stickyFirstRow" ItemStyle-Width="40">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSf_Name" Text="Field Force Name" runat="server" /><br />
                                                    <asp:TextBox ID="txtSf_Name" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSf_Name" runat="server" Text='<%# Bind("sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSf_Hq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Desig" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesg" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DOJ" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDOJ" runat="server" Text='<%# Bind("Sf_Joining_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("State_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="desig_color" ItemStyle-HorizontalAlign="Left" Visible="false"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesig_color" Visible="false" Text='<%# Bind("desig_color") %>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reporting To" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblReporting" Text="Reporting To" runat="server" /><br />
                                                    <asp:TextBox ID="txtReporting" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="80px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReporting" Text='<%# Bind("Reporting_To_SF") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFlag" runat="server" Text='<%# Bind("sf_Tp_Active_flag") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PD" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFW" runat="server" Text='<%# Bind("No_of_FW") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="W/H" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWH" runat="server" Text='<%# Bind("Holiday_WOf") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CL" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCL" runat="server" Text='<%# Bind("CL") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PL" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPL" runat="server" Text='<%# Bind("PL") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSL" runat="server" Text='<%# Bind("SL") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Leave Availed Dates" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLVDays" runat="server" Text='<%# Bind("Leave_Avail_Days") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="No Of Delayed Dates" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeleycnt" runat="server" Text='<%# Bind("Delayed_Days")  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total No of Delayed Days" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDLDays" runat="server" Text='<%# Bind("Delayed_Cnt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total No Of Days Not At All Reported" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNtalAt" runat="server" Text='<%# Bind("Not_At_All") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Not At All Reported Dates" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNtalAtDate" runat="server" Text='<%# Bind("Not_At_All_Days") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IT" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIT" runat="server" Text='<%# Bind("IT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TAdv" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTAdv" runat="server" Text='<%# Bind("TAdv") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SOC" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSOC" runat="server" Text='<%# Bind("SOC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FAdv" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFAdv" runat="server" Text='<%# Bind("FAdv") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PTax" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPTax" runat="server" Text='<%# Bind("PTax") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HP" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHP" runat="server" Text='<%# Bind("HP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MMAdv" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMMAdv" runat="server" Text='<%# Bind("MMAdv") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EDU ADV" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEDU_ADV" runat="server" Text='<%# Bind("EDU_ADV") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WWF" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWWF" runat="server" Text='<%# Bind("WWF") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LWF" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLWF" runat="server" Text='<%# Bind("LWF") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LIC" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLIC" runat="server" Text='<%# Bind("LIC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TP Deviation Not Reported Dates" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTp_Devn_NR_Dates" runat="server" Text='<%# Bind("Tp_Devn_NR_Dates") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow"
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLeave_Status" runat="server" Text='<%# Bind("Leave_Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

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
    </form>
</body>
</html>
