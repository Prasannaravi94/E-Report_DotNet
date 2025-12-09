<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptLstDr_Search.aspx.cs"
    Inherits="MasterFiles_rptLstDr_Search" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listed Doctor Details</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>

    <%--    <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />

    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .marright {
            margin-left: 85%;
        }
    </style>

    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
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
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Panel ID="pnlbutton" runat="server">
                <br />
                <table width="100%">
                    <tr>
                        <td></td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td style="padding-right: 30px">
                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                    </td>
                                    <td style="padding-right: 15px">
                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="../assets/images/Pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="lblPdf" runat="server" Text="Pdf" CssClass="label" Font-Size="14px"></asp:Label>

                                    </td>
                                    <td style="padding-right: 50px">
                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            </asp:Panel>

            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server" Width="100%">
                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="Listed Doctor Details" CssClass="reportheader"></asp:Label>
                            </div>
                            <br />
                            <br />

                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="lblFFName" runat="server" Text="Field Force Name"
                                        Font-Size="14px"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lblHQ1" runat="server" Text="HQ"
                                        Font-Size="14px"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lblDesignation1" runat="server" Text="Designation"
                                        Font-Size="14px"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center"
                                        EmptyDataText="No Records Found" AutoGenerateColumns="false"
                                        GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt">
                                        <HeaderStyle Font-Bold="False" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Listed Dr Name"
                                                ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblLisdr_Name" runat="server"
                                                        Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Address"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladdress" runat="server"
                                                        Text='<%# Bind("ListedDr_Address1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hospital Name"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHosp_Name" runat="server"
                                                        Text='<%# Bind("ListedDr_Hospital") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hospital Address"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHosp_Addr" runat="server"
                                                        Text='<%# Bind("Hospital_Address") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Territory"
                                                ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerr" runat="server"
                                                        Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qualification"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQual" runat="server"
                                                        Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Speciality"
                                                ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpec" runat="server"
                                                        Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category"
                                                ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblCat" runat="server"
                                                        Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClass" runat="server"
                                                        Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile No"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMbl" runat="server"
                                                        Text='<%# Bind("ListedDr_Mobile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DOB"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDOB" runat="server"
                                                        Text='<%# Bind("ListedDr_DOB") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DOW"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDOW" runat="server"
                                                        Text='<%# Bind("ListedDr_DOW") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp Id"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpid" runat="server"
                                                        Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No.of Visit"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisit" runat="server"
                                                        Text='<%# Bind("No_of_Visit") %>'></asp:Label>
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
