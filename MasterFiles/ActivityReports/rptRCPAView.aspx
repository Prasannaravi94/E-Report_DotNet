<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptRCPAView.aspx.cs" Inherits="MasterFiles_rptRCPAView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RCPA_View</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <style type="text/css">
        .mGrid {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }

            .mGrid td {
                padding: 2px;
                border: solid 1px Black;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                font-size: small;
                font-family: Calibri;
            }

            .mGrid th {
                padding: 4px 2px;
                color: white;
                background: #A6A6D2;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                border-bottom: solid 1px Black;
                font-weight: normal;
                font-size: small;
                font-family: Calibri;
            }

            .mGrid .pgr {
                background: #A6A6D2;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    text-align: left;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: White;
                    line-height: 12px;
                }

                .mGrid .pgr th {
                    background: #A6A6D2;
                }

                .mGrid .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('btnClear').click();
            //window.opener.location.reload();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <center>
                <table width="85%">
                    <tr>
                        <td width="85%"></td>
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
                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClick="btnExcel_Click">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

                                    </td>
                                    <td style="padding-right: 15px">
                                        <asp:LinkButton ID="btnPDF" ToolTip="Excel" runat="server" OnClick="btnPDF_Click">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/Pdf.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label1" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

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
            </center>
            <center>
                <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <asp:Panel ID="pnlContents" runat="server">
                                <center>
                                    <div align="center">
                                        <asp:Label ID="lblHead" runat="server" Text="RCPA Details" CssClass="reportheader" Font-Underline="true"></asp:Label>
                                        <br />
                                    </div>
                                    <br />
                                    <div align="center">
                                        <asp:Label ID="lblFieldForce" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="18px"></asp:Label>
                                    </div>
                                </center>
                                <center>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <br />
                                        <div class="display-Approvaltable clearfix">
                                            <asp:GridView ID="gvRCPAList" runat="server" AlternatingRowStyle-CssClass="alt"
                                                AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                GridLines="None" HorizontalAlign="Center" BorderWidth="0" FooterStyle-HorizontalAlign="Center"
                                                OnRowDataBound="gvRCPAList_RowDataBound" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("DrName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Our Product Name" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOurProduct" runat="server" Text='<%# Bind("ourBrndNm") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity Per Month" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOurQty" runat="server" Text='<%# Bind("OurQty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Our Value" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOurValue" runat="server" Text='<%# Bind("CmptrPOB") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Competitor Name" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCmpName" runat="server" Text='<%# Bind("CmptrName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Brand Name" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCmptrBrnd" runat="server" Text='<%# Bind("CmptrBrnd") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity Per Month" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCmptrQty" runat="server" Text='<%# Bind("CmptrQty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Competitor Value" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCmptrPriz" runat="server" Text='<%# Bind("CmptrPriz") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </center>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </center>
        </div>
    </form>
</body>
</html>
