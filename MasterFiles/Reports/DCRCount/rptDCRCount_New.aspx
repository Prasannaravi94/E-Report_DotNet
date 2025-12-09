<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDCRCount_New.aspx.cs"
    Inherits="Reports_DCRCount_rptDCRCount_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DCR Count</title>
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
            font-size: 9pt;
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
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var myBlob = new Blob([pnlContents.innerHTML], { type: 'application/vnd.ms-excel' });
                    var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                    var a = document.createElement("a");
                    document.body.appendChild(a);
                    a.href = url;
                    a.download = "export.xls";
                    a.click();
                    setTimeout(function () { window.URL.revokeObjectURL(url); }, 0);
                    return false;

                //var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                //location.href = url
                //return false
            })
        })
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
<body>
    <form id="form1" runat="server">
        <div>
            <center>

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
                                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="20px" Height="20px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click">
                                                            <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/Pdf.png" ToolTip="Pdf" Width="20px" Height="20px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="lblPdf" runat="server" Text="Pdf" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>
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


                <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
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
                                    <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                            Width="99%">
                                        </asp:Table>
                                        <asp:GridView ID="GvDcrCount" runat="server" AutoGenerateColumns="false" CssClass="table" GridLines="None" AlternatingRowStyle-Height="35px"
                                            OnRowDataBound="GvDcrCount_RowDataBound" EmptyDataText="No Records Found" HeaderStyle-Height="60px">
                                            <RowStyle Height="35px" />
                                            <AlternatingRowStyle Height="35px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Month" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                    HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                    <ControlStyle Width="90%"></ControlStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblMonth" ForeColor="Red" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSfcode" runat="server" Visible="false" Text='<%# Bind("sf_Code") %>'  ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Field Force Name" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblSf_Name" Text="Field Force Name" runat="server" height="30px" /><br />
                                                        <asp:TextBox ID="txtSf_Name" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" height="18px"/>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSf_Name" runat="server" Text='<%# Bind("sf_Name") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSf_Hq" runat="server" Text='<%# Bind("Sf_HQ") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Desig" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesg" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DOJ" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDOJ" runat="server" Text='<%# Bind("Sf_Joining_Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Emp<br>Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="20">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSf_Employee" runat="server" Text='<%# Bind("sf_Emp_Id") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last DCR Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLast_DCR_Date" runat="server" Text='<%# Bind("activity_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User<br>Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUser" runat="server" Text='<%# Bind("Sf_UserName") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bank Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBank" runat="server" Text='<%# Bind("Bank_Name") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DCR Count" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDCR_Count" runat="server" Text='<%# Bind("Count_value") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approval Pending Dates" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="550" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApproval_Pending_Dates" runat="server" Text='<%# Bind("Pendind_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="desig_color" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" Visible="false"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesig_color" Visible="false" Text='<%# Bind("desig_color") %>' 
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reporting To" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblReporting" Text="Reporting To" runat="server" height="30px" /><br />
                                                        <asp:TextBox ID="txtReporting" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="80px" height="18px" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReporting" Text='<%# Bind("Reporting_To_SF") %>' runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No.of FWD" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFW" runat="server" Text='<%# Bind("No_of_FW") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dr Calls" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDrsn" runat="server" Text='<%# Bind("Dr_Seen") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Call Avg" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAvrg" runat="server" Text='<%# Bind("Call_Avg") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Days" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLVDays" runat="server" Text='<%# Bind("Leave") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLVDate" runat="server" Text='<%# Bind("Leave_Days") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delayed Days" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDLDays" runat="server" Text='<%# Bind("DCR_Dalayed_Dates_Count") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delayed Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDLDate" runat="server" Text='<%# Bind("DCR_Dalayed_Dates") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chem Calls" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChemSn" runat="server" Text='<%# Bind("Chem_Seen") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stockist Calls" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStckSn" runat="server" Text='<%# Bind("Stck_Seen") %>' height="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-Width="40">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblStatus" Text="Status" runat="server" height="30px" /><br />
                                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="60px" height="18px" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" Text='<%# Bind("sf_Tp_Active_flag") %>' runat="server" ></asp:Label>
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
            </center>
        </div>
    </form>
</body>
</html>
