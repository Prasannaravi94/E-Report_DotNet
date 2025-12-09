<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDCRCount.aspx.cs" Inherits="Reports_DCRCount_rptDCRCount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link type="text/css" rel="Stylesheet" href="../../../css/rptMissCall.css" />


    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>DCR Count</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" type="image/png" href="../../../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../../assets/css/style.css" />
    <link rel="stylesheet" href="../../../assets/css/responsive.css" />
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->

    <script src="../../../assets/js/jQuery.min.js"></script>
    <script src="../../../assets/js/popper.min.js"></script>
    <script src="../../../assets/js/bootstrap.min.js"></script>
    <script src="../../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../../assets/js/main.js"></script>


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
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>

    <script type="text/javascript">
        function Filter() {
            var grid = document.getElementById("<%=GvDcrCount.ClientID %>");
            var row = grid.rows.length;
            var FForce = document.getElementById("GvDcrCount_ctl01_txtSf_Name");
            var Reporting = document.getElementById("GvDcrCount_ctl01_txtReporting");

            var FForceValue = FForce.value.toLowerCase();
            var ReportingValue = Reporting.value.toLowerCase();

            var FForceSplitter = FForceValue.split();
            var ReportingSplitter = ReportingValue.split();

            var display = '';

            var FForceRowValue = '';
            var ReportingRowValue = '';

            for (var i = 1; i < row; i++) {
                display = 'none';

                FForceRowValue = grid.rows[i].cells[2].innerText;
                ReportingRowValue = grid.rows[i].cells[6].innerText;

                for (var m = 0; m < FForceSplitter.length; m++) {
                    for (var n = 0; n < ReportingSplitter.length; n++) {

                        if (FForceRowValue.toLowerCase().indexOf(FForceSplitter[m]) >= 0 && ReportingRowValue.toLowerCase().indexOf(ReportingSplitter[n]) >= 0) {
                            display = '';
                        }

                        else {
                            display = 'none';
                            break;

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
            <%--  <center>--%>
            <br />
            <table width="100%" style="padding-right: 100px;">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 50px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
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
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>

                                </td>
                                <td style="padding-right: 100px">
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
            <br />

            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">
                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="DCR Count Report for the Period " CssClass="reportheader"></asp:Label>
                                <br />
                                <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" ForeColor="#696D6E"></asp:Label>
                                <br />
                                <asp:Label ID="lblDcrCount" Style="color: red" runat="server" CssClass="reportheader"></asp:Label>
                            </div>
                            <br />

                            <div class="display-callAvgreporttable clearfix">
                                <div class="table-responsive" style="max-height: 700px; scrollbar-width: thin;">

                                    <asp:Table ID="tbl" runat="server" GridLines="None" CssClass="table"
                                        Width="95%">
                                    </asp:Table>
                                    <asp:GridView ID="GvDcrCount" runat="server" AutoGenerateColumns="false" CssClass="table" GridLines="None"
                                        OnRowDataBound="GvDcrCount_RowDataBound">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40">
                                                <ControlStyle Width="90%"></ControlStyle>
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
                                            <asp:TemplateField HeaderText="Month" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNextMonth" ForeColor="Red" runat="server" Text='<%# Bind("Date2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40"
                                                Visible="false">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfcode" runat="server" Visible="false" Text='<%# Bind("sf_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Field Force Name" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="40">
                                                <ControlStyle Width="220px"></ControlStyle>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSf_Name" Text="Field Force Name" runat="server" /><br />
                                                    <asp:TextBox ID="txtSf_Name" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSf_Name" runat="server" Text='<%# Bind("sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Last DCR Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLast_DCR_Date" runat="server" Text='<%# Bind("activity_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DCR Count" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDCR_Count" runat="server" Text='<%# Bind("Count_value") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Approval Pending Dates" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="550">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApproval_Pending_Dates" runat="server" Text='<%# Bind("Pendind_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="desig_color" ItemStyle-HorizontalAlign="Left" Visible="false"
                                                ItemStyle-Width="40">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesig_color" Visible="false" Text='<%# Bind("desig_color") %>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reporting To" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="40">
                                                <ControlStyle Width="220px"></ControlStyle>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblReporting" Text="Reporting To" runat="server" /><br />
                                                    <asp:TextBox ID="txtReporting" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReporting" Text='<%# Bind("Reporting_To_SF") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </asp:Panel>
                        <%--  </center>--%>
                        <table width="100%" style="margin-left: 26px">
                            <tr style="margin-left: 120px">
                                <td width="100%">
                                    <asp:Label ID="lblExplain" Text="(LP) - Leave Apporoval Pending  ,   (DAP) - DCR Approval Pending "
                                        Font-Underline="True" Font-Bold="True" Font-Size="Small" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
