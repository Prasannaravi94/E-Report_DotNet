<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TerritoryWiseDrList.aspx.cs" Inherits="MasterFiles_Report_TerritoryWiseDrList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--  <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />


    <title></title>
    
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();" Visible="false">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px" Visible="false" ></asp:Label>
                                    <%--<asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" Visible="false" BorderWidth="1" Height="25px" Width="60px"
                                     />--%>
                                </td>
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" Visible="false" >
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                    <%-- <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" Visible="false" BorderWidth="1" Height="25px" Width="60px"
                                     />--%>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnPDF" ToolTip="PDF" runat="server" Visible="false" >
                                        <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label3" runat="server" Text="PDF" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                    <%--<asp:Button ID="btnPDF" runat="server" Text="PDF" Visible="false" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid"  BorderWidth="1" Height="25px" Width="60px"
                                     />--%>
                                </td>
                                <td style="padding-right: 50px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" Visible="false">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px" Visible="false" ></asp:Label>
                                    <%--<asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" Visible="false" BorderWidth="1" Height="25px" Width="60px"
                                    OnClientClick="RefreshParent()" />--%>
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
                <div class="col-lg-11">
                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <center>
                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="TP-Drs View for"
                                    Font-Underline="True" class="reportheader"></asp:Label>
                            </div>
                            <br />
                            <br />
                            <div align="left" style="font-size: 13px">

                                <div class="row">
                                    <div class="col-lg-5">
                                        <asp:Label ID="lblFieldForce" runat="server" Text="FieldForce Name"></asp:Label>
                                        <asp:Label ID="lblFieldForcename" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblhq" runat="server" Text="HQ"></asp:Label>
                                        <asp:Label ID="lblhq1" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label ID="ldldesig" runat="server" Text="Designation"></asp:Label>
                                        <asp:Label ID="lbldesignation" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-5">
                                        <asp:Label ID="lblterr" runat="server" Text="Territory Name"></asp:Label>
                                        <asp:Label ID="lblterritoryname" runat="server" Text=""></asp:Label>

                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lbltype" runat="server" Text="Type"></asp:Label>
                                        <asp:Label ID="lbltypename" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label ID="lbldte" runat="server" Text="Date"></asp:Label>
                                        <asp:Label ID="lbldate" runat="server" Text=""></asp:Label>
                                    </div>

                                </div>

                            </div>
                            <br />
                          
                            <%--<div><asp:Label ID="lblFieldForce" runat="server" Font-Underline="True" Font-Size="9pt" Font-Bold="True"></asp:Label></div>--%>
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">

                                    <asp:GridView ID="grdTP" runat="server" Width="100%" HorizontalAlign="Center" GridLines="None"
                                        CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                                        CssClass="table">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-Width="50"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Doctor Name" ItemStyle-Width="200"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLstDoc" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qualification" ItemStyle-Width="100"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQualification" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Category Name" ItemStyle-Width="100"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCate" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Speciality Name" ItemStyle-Width="200"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpeciality" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </center>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <br />
        <br />
    </form>
</body>
</html>
