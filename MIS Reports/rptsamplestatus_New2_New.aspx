<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptsamplestatus_New2_New.aspx.cs" Inherits="MIS_Reports_rptsamplestatus_New2_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td width="20%"></td>
                    <td></td>
                    <td width="80%" align="center">
                        <asp:Label ID="lblProd" runat="server" Text="Sample Details" CssClass="reportheader"></asp:Label>
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" Visible="false" OnClick="btnPrint_Click">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                </td>
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClick="btnExcel_Click">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                                <td style="padding-right: 50px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" OnClick="btnClose_Click">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <table width="100%" align="center">
                            <tbody>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblfieldname" runat="server" Font-Size="14px" Text="Fieldforce Name:"></asp:Label>
                                        <asp:Label ID="lblname" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="Magenta"></asp:Label>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                        <div class="designation-reactivation-table-area clearfix">
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                    <asp:GridView ID="grdprd" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                        GridLines="None" HorizontalAlign="Center" BorderWidth="0" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmnth" runat="server" Width="120px" Text='<%#Eval("Month_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyear" runat="server" Width="120px" Text='<%#Eval("Fyr")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblforce" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Emp Code" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempcode" runat="server" Width="220px" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhq" runat="server" Text='<%#Eval("Sf_Hq")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="First Level Reporting" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpting1" runat="server" Width="150px" Text='<%#Eval("Reporting_To_leve1")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Second Level Reporting" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpting2" runat="server" Width="150px" Text='<%#Eval("Reporting_To_leve2")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Third Level Reporting" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpting3" runat="server" Width="150px" Text='<%#Eval("Reporting_To_leve3")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Sample Code" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprd_code" runat="server" Text='<%#Eval("Product_Code_SlNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sample ERP Code" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblErp_prd_code" runat="server" Text='<%#Eval("Sample_Erp_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprd_name" runat="server" Text='<%#Eval("Product_Detail_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening Bal" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblopening" runat="server" Font-Size="10px" Text='<%# Bind("opening") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Akcnowledged Qty" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsamqty" runat="server" Font-Size="10px" Text='<%# Bind("Despatch_Qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doctor Issued Qty" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblissued" runat="server" Font-Size="10px" Text='<%# Bind("issedd2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Chemist Issued Qty" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblissued_Chem" runat="server" Width="150px" Text='<%# Bind("issed_Chem") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Closing Qty" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblclosing" runat="server" Font-Size="10px" Text='<%# Bind("closing") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
