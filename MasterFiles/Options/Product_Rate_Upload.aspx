<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product_Rate_Upload.aspx.cs"
    Inherits="MasterFiles_Options_Product_Rate_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Rate</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                             <br />
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                        <asp:Label ID="lblState" runat="server" SkinID="lblMand" CssClass="label">State Name</asp:Label>
                        <asp:DropDownList ID="ddlState" runat="server"  Width="60px">
                        </asp:DropDownList>
                      </div>
            
                <div class="single-des clearfix">
           <asp:LinkButton ID="lnkcount" runat="server"  Text='Download Link'
                            OnClick="lnkcount_Click">  </asp:LinkButton>
                    </div>
        <br />
        <div class="single-des clearfix">
     <center>
            <asp:FileUpload ID="FlUploadcsv" runat="server" />
            </center>
               </div>
            <br />
            <div class="w-100 designation-submit-button text-center clearfix">
           
            <asp:Button ID="btnUPload" runat="server" CssClass="savebutton"
                Text="Upload" OnClick="btnUpload_Click" />
       
          </div>
</div>
     </div>
            </div>
         
        <br />
         <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive">
            <asp:Panel ID="pnlContents" runat="server">
                <asp:GridView ID="grdRate" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                    GridLines="None" Visible="false"  CssClass="table" PagerStyle-CssClass="gridview1" ShowHeaderWhenEmpty="true" 
                    AllowSorting="True">
                   
                    <Columns>
                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="40px">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                            </ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prod_Code">
                            <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                            </ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblProd_Code" runat="server" Text='<%#   Bind("Product_Detail_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="240px" HeaderText="Product Name" HeaderStyle-ForeColor="Black"
                            ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtProdName" SkinID="MandTxtBox" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                            </ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblProdName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sale Unit" HeaderStyle-Width="90px">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                            </ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSaleUnit" runat="server" Text='<%#Bind("Product_Sale_Unit")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State Code" HeaderStyle-Width="90px">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                            </ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblState" runat="server" Text='<%#Bind("state_code")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MRP" HeaderStyle-Width="140px">
                            <ControlStyle Width="60%"></ControlStyle>
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                            </ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtMRP" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                    Text='<%#(Eval("MRP_Price"))%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Retailer Price" HeaderStyle-Width="120px">
                            <ControlStyle Width="60%"></ControlStyle>
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                            </ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtRP" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                    Text='<%#(Eval("Retailor_Price"))%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Distributor Price" HeaderStyle-Width="120px">
                            <ControlStyle Width="60%"></ControlStyle>
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                            </ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtDP" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                    Text='<%#(Eval("Distributor_Price"))%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NSR Price" HeaderStyle-Width="120px">
                            <ControlStyle Width="60%"></ControlStyle>
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                            </ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtNSR" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                    Text='<%#(Eval("NSR_Price"))%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target Price" HeaderStyle-Width="120px">
                            <ControlStyle Width="60%"></ControlStyle>
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                            </ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtTarg" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                    Text='<%#(Eval("Target_Price"))%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <br />

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
