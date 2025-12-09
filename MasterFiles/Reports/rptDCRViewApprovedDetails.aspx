<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDCRViewApprovedDetails.aspx.cs"
    Inherits="Reports_rptDCRViewApprovedDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <style type="text/css">
        .tblHead {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            height: 25px;
            border: 1px solid;
            border-color: #999999;
        }

        .tblRow {
            font-size: 8pt;
            border: 1px solid;
            border-color: #999999;
            font-family: Verdana;
        }

        .tbldetail_main {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }

        .table {
            margin-top: 45px;
        }
    </style>

    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <br />
                <asp:Panel ID="pnlbutton" runat="server">
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
                                            <asp:Label ID="lblPrint" runat="server" Text="Print" Font-Size="14px"></asp:Label>

                                        </td>
                                        <td style="padding-right: 15px">
                                            <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClick="btnExcel_Click">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                            </asp:LinkButton>
                                            <asp:Label ID="lblExcel" runat="server" Text="Excel" Font-Size="14px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnPDF" ToolTip="PDF" runat="server" Visible="false">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                            </asp:LinkButton>
                                            <asp:Label ID="Label3" runat="server" Text="PDF" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>

                                        </td>
                                        <td style="padding-right: 50px">
                                            <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                            </asp:LinkButton>
                                            <asp:Label ID="lblClose" runat="server" Text="Close" Font-Size="14px"></asp:Label>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />

                <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <asp:Panel ID="pnlContents" runat="server" Width="100%">

                                <asp:Label ID="lblHead" runat="server" Text="Daily Call Report for " Visible="true"
                                    CssClass="reportheader"></asp:Label>
                                <br />
                                <br />
                                <br />
                                <div class="row" align="left">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblFieldForceName" runat="server" Text="Daily Call Report for " ForeColor="#696d6e" Font-Size="16px"
                                            Visible="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblSubArea" runat="server" Text="Daily Call Report for " Visible="true"
                                            ForeColor="#696d6e" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblHQ" runat="server" Text="Daily Call Report for " Visible="true"
                                            ForeColor="#696d6e" Font-Size="16px"></asp:Label>
                                    </div>
                                </div>

                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="grdLstDr" runat="server" Width="100%" HorizontalAlign="Center"
                                            CellPadding="2" EmptyDataText="" AutoGenerateColumns="false"
                                            OnRowDataBound="grdLstDr_RowDataBound" CssClass="table" GridLines="None">
                                            <%-- <HeaderStyle Font-Bold="False" Wrap="false" />--%>

                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-Width="50"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Session" ItemStyle-Width="70"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTourPlan" runat="server" Text='<%#Eval("Session")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Time" ItemStyle-Width="50"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-Width="20%"
                                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblListedDr_Name" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address" ItemStyle-Width="10%"
                                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoc_Address" runat="server" Text='<%# Bind("listeddr_address1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category" ItemStyle-Width="320"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoc_Category" runat="server" Text='<%# Bind("Doc_cat_shortname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Speciality" ItemStyle-Width="320"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoc_Speciality" runat="server" Text='<%# Bind("Doc_Spec_shortname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qual" ItemStyle-Width="260" Visible="false"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQual" runat="server" Text='<%# Bind("doc_qua_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Sub Area" ItemStyle-Width="10%"
                                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArea" runat="server" Text='<%# Bind("SDP_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Actual Place of Work" ItemStyle-Width="10%"
                                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProduct_Remainded" runat="server" Text='<%# Bind("GeoAddrs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               <%-- <asp:TemplateField HeaderText="Previous Visit" ItemStyle-Width="260"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLast_Visit" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Worked With" ItemStyle-Wrap="false" ItemStyle-Width="15%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWorked_With" runat="server" Text='<%# Bind("Worked_with_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Product Sampled" ItemStyle-Wrap="false" ItemStyle-Width="45%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProduct_Detail" runat="server" Text='<%# Bind("Product_Detail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Input" ItemStyle-Width="10%"
                                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGift_Name" runat="server" Text='<%# Bind("Gift_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Call Remark" ItemStyle-Wrap="false" ItemStyle-Width="35%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCallFeedback" runat="server" Text='<%# Bind("Rx") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ActivityRemark" Visible="false" ItemStyle-Wrap="false" ItemStyle-Width="35%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActivityRemark" runat="server" Text='<%# Bind("Activity_remarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lat & Long" ItemStyle-Wrap="false" ItemStyle-Width="35%"
                                                    ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLatlong" runat="server" Text='<%# Bind("latilong") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="GVChemist" runat="server" Width="100%" HorizontalAlign="Center" GridLines="None"
                                            CellPadding="2" OnRowDataBound="GVChemist_RowDataBound" EmptyDataText="" AutoGenerateColumns="false"
                                            CssClass="table">
                                            <%-- <HeaderStyle Font-Bold="False" Wrap="false" />--%>

                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-Width="50"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Session" ItemStyle-Width="70"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTourPlan" runat="server" Text='<%#Eval("Session")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Time" ItemStyle-Width="50"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chemists Name" ItemStyle-Wrap="false" ItemStyle-Width="10%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#Eval("Chemists_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address" ItemStyle-Wrap="false" ItemStyle-Width="10%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChemists_Address" runat="server" Text='<%#Eval("chemists_address1")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Booking Value" ItemStyle-Width="200"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOB" runat="server" Text='<%# Bind("POB") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Area" ItemStyle-Wrap="false" ItemStyle-Width="10%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubArea" runat="server" Text='<%#Eval("SDP_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual Place of Work" ItemStyle-Wrap="false" ItemStyle-Width="10%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActual" runat="server" Text='<%# Bind("GeoAddrs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Previous Visit" ItemStyle-Width="200"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPreVisit" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Worked With" ItemStyle-Wrap="false" ItemStyle-Width="10%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWorked_With" runat="server" Text='<%# Bind("Worked_with_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Sampled" ItemStyle-Wrap="false" ItemStyle-Width="10%"
                                                    HeaderStyle-Width="400px" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProduct_Detail" runat="server" Text='<%# Bind("Product_Detail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Input" ItemStyle-Wrap="false" ItemStyle-Width="10%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGift_Name" runat="server" Text='<%# Bind("additional_gift_dtl") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Call Remark" ItemStyle-Wrap="false" ItemStyle-Width="10%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCallFeedback" runat="server" Text='<%# Bind("Rx") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ActivityRemark" Visible="false" ItemStyle-Wrap="false" ItemStyle-Width="35%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActivityRemark" runat="server" Text='<%# Bind("Activity_remarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lat & Long" ItemStyle-Wrap="false" ItemStyle-Width="35%"
                                                    ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLatlong" runat="server" Text='<%# Bind("latilong") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="GVUnlstDr" runat="server" AutoGenerateColumns="false"
                                            CellPadding="2" CssClass="table" EmptyDataText="" GridLines="None"
                                            HorizontalAlign="Center" OnRowDataBound="GVUnlstDr_RowDataBound" Width="100%">
                                            <%--  <HeaderStyle Font-Bold="False" Wrap="false" />--%>

                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-Width="50"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Session" ItemStyle-Width="70"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTourPlan" runat="server" Text='<%#Eval("Session")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Time" ItemStyle-Width="50"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UnListed Doctor Name" ItemStyle-Width="20%"
                                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblListedDr_Name" runat="server" Text='<%# Bind("UnListedDr_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address" ItemStyle-Width="10%"
                                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoc_Address" runat="server" Text='<%# Bind("Unlisteddr_address1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category" ItemStyle-Width="320"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoc_Category" runat="server" Text='<%# Bind("Doc_cat_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Speciality" ItemStyle-Width="320"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoc_Speciality" runat="server" Text='<%# Bind("Doc_Special_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Sub Area" ItemStyle-Width="10%"
                                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArea" runat="server" Text='<%# Bind("SDP_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Actual Place of Work" ItemStyle-Width="10%"
                                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProduct_Remainded" runat="server" Text='<%# Bind("GeoAddrs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Previous Visit" ItemStyle-Width="260"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLast_Visit" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Worked With" ItemStyle-Wrap="false" ItemStyle-Width="15%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWorked_With" runat="server" Text='<%# Bind("Worked_with_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Product Sampled" ItemStyle-Wrap="false" ItemStyle-Width="45%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProduct_Detail" runat="server" Text='<%# Bind("Product_Detail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Input" ItemStyle-Width="10%"
                                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGift_Name" runat="server" Text='<%# Bind("Gift_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Call Remark" ItemStyle-Wrap="false" ItemStyle-Width="35%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCallFeedback" runat="server" Text='<%# Bind("Rx") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ActivityRemark" Visible="false" ItemStyle-Wrap="false" ItemStyle-Width="35%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActivityRemark" runat="server" Text='<%# Bind("Activity_remarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lat & Long" ItemStyle-Wrap="false" ItemStyle-Width="35%"
                                                    ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLatlong" runat="server" Text='<%# Bind("latilong") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="GVStockist" runat="server" AutoGenerateColumns="false"
                                            CellPadding="2" CssClass="table" EmptyDataText="" GridLines="None"
                                            HorizontalAlign="Center" Width="100%">

                                            <Columns>
                                                <asp:TemplateField
                                                    HeaderText="#" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="50">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server"
                                                            Text="<%#  ((GridViewRow)Container).RowIndex + 1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField
                                                    HeaderText="Stockist Name"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStockist_Name" runat="server"
                                                            Text='<%#Eval("Stockist_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField
                                                    HeaderText="Worked With"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWorked_with_Name" runat="server"
                                                            Text='<%# Bind("Worked_with_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField
                                                    HeaderText="POB" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="200">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOB" runat="server" Text='<%# Bind("POB") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="false" GridLines="None"
                                            CellPadding="2" CssClass="table" EmptyDataText="No Data Found"
                                            HorizontalAlign="Center" Width="100%">

                                            <Columns>
                                                <asp:TemplateField
                                                    HeaderText="#" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="50">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server"
                                                            Text="<%#  ((GridViewRow)Container).RowIndex + 1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField
                                                    HeaderText="Stockist Name"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStockist_Name" runat="server"
                                                            Text='<%#Eval("Stockist_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField
                                                    HeaderText="Worked With" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWorked_with_Name" runat="server"
                                                            Text='<%# Bind("Worked_with_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField
                                                    HeaderText="POB" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="200">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOB" runat="server" Text='<%# Bind("POB") %>'></asp:Label>
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
