<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mail_Status_Zoom.aspx.cs"
    Inherits="Reports_Mail_Status_Zoom" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title runat="server">Listed Doctor Details</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <style type="text/css">
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
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .bar {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
                                    <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="lblPdf" runat="server" Text="Pdf" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                                <td style="padding-right: 40px">
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
                        <asp:Panel ID="pnlContents" runat="server" Width="100%">
                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="Mail Status" CssClass="reportheader"></asp:Label>
                                <br />
                                <br />
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="LblForceName" runat="server" CssClass="label"
                                        Font-Size="16px"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label ID="Lblsubject" runat="server" CssClass="label"
                                        Font-Size="16px"></asp:Label>
                                </div>

                            </div>
                            <br />
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                    <asp:GridView ID="grdMail" runat="server" Width="100%" HorizontalAlign="Center" GridLines="None"
                                        AutoGenerateColumns="false"
                                        CssClass="table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                                                <%--<ControlStyle Width="50%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfName" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesig" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Read Date & Time" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("mail_read_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
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
