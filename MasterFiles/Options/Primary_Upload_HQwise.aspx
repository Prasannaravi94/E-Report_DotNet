<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Primary_Upload_HQwise.aspx.cs"
    Inherits="MasterFiles_Options_Primary_Upload_HQwise" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Primary Bill HQ Wise Upload</title>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMoth" runat="server" CssClass="label">Month</asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblToYear" runat="server" CssClass="label">Year</asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" Width="80px">
                                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblExcel" runat="server" CssClass="label">Excel file</asp:Label>
                                <asp:FileUpload ID="FlUploadcsv" runat="server" CssClass="input" />
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="Button1" runat="server" Text="Upload" CssClass="savebutton" OnClick="btnUpload_Click" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblIns" runat="server" CssClass="label" ForeColor="Red">Note:</asp:Label>
                                <asp:Label ID="Label1" runat="server" CssClass="label">1) Sheet Name Must be 'UPL_Primary_Bill_HQwise'</asp:Label>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblExc" runat="server" CssClass="label">Excel Format File</asp:Label>
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download Here" OnClick="lnkDownload_Click"> 
                                </asp:LinkButton>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" Text="Download Not Uploaded List" CssClass="label"  ></asp:Label>
                                <asp:LinkButton ID="lnlnot" runat="server"
                                    Text="Download Here" OnClick="lnlnot_Click"  > 
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:Panel ID="pnlprimary" runat="server" Visible="false">
                                        <center>
                                            <img src="../../Images/arrowdown1.gif" height="80px" alt="" />
                                            <h2 style="color: Red; font-weight: bold; font-size: x-large">Not Uploaded List</h2>
                                            <asp:GridView ID="grdPrimary" runat="server" AutoGenerateColumns="false" Width="100%"
                                                GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" ShowHeaderWhenEmpty="true" AlternatingRowStyle-CssClass="alt">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsl" runat="server" Text='<%#Eval("Sl_No")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="HQ code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSErp" runat="server" Text='<%#Eval("HQ_code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stockist ERP Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSErp" runat="server" Text='<%#Eval("Stockist_ERP_Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stockist Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Stockist_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product ERP Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProdErp" runat="server" Text='<%#Eval("Product_ERP_Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProd" runat="server" Text='<%#Eval("Product_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </center>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
