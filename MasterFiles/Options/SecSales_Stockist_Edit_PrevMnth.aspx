<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSales_Stockist_Edit_PrevMnth.aspx.cs" Inherits="MasterFiles_Options_SecSales_SecSales_Stockist_Edit_PrevMnth" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secondary Sales Edit</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>

     
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>

      <script type="text/javascript">
        function checkapp() {
            var grid = document.getElementById('<%= grdSecSales.ClientID %>');

            if (grid != null) {

                var inputList = grid.getElementsByTagName("input");
                 
                var cnt = 0;
                var index = '';
                var Count = 0;
                var CountVisi = 0;

                for (i = 2; i < inputList.length; i++) {

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {

                        index = i.toString();
                    }


                    var imgCross = document.getElementById('grdSecSales_ctl' + index + '_imgCross');
                    var chkSaleEntry = document.getElementById('grdSecSales_ctl' + index + '_chkSaleEntry');

                    document.getElementById('grdSecSales_ctl' + index + '_imgCross').style.visibility = "visible";
  
                }
            }
        }
    </script>
     
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                     
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblSF" runat="server" CssClass="label">HQ Name </asp:Label>
                                  
                               <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                    CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                                
                            </div>
                            <div class="single-des clearfix">
                                  <asp:Label ID="lblStk" runat="server" CssClass="label">Stockist Name </asp:Label>
                                <asp:DropDownList ID="ddlStk" runat="server" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                  <asp:Label ID="lblFinanYear" runat="server" CssClass="label">Financial Year </asp:Label>
                                <asp:DropDownList ID="ddlFinancial" runat="server" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                            </div>
                             
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" CssClass="savebutton" />
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-lg-8">
                        <div class="designation-reactivation-table-area clearfix">
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSecSales" runat="server" HorizontalAlign="Center"
                                        GridLines="None" EmptyDataText="No Records Found" AutoGenerateColumns="false"
                                        CssClass="table" AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdSecSales_RowDataBound" >
                                        <HeaderStyle Font-Bold="False" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sf Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSf_Code" runat="server" Text='<%#Eval("SF_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="SS_Head_Sl_No" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhead" runat="server" Text='<%#Eval("SS_Head_Sl_No")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rowcnt" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowcnt" runat="server" Text='<%#Eval("Rowcnt")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_name" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                             <asp:TemplateField HeaderText="Month Num" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMonth_num" runat="server" Text='<%# Bind("Month") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Month" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("Mnth") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblYear" runat="server" Text='<%# Bind("Year") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSaleEntry" Text="." runat="server"  Visible="false"/>
                                                     <asp:Image ID="imgCross" runat="server" ImageUrl="../../../Images/cross.png"  />    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="Edit" CssClass="savebutton" Visible="false" OnClientClick="return confirm('Do you want to allow Sec Sales Edit for the selected stockiest(s)');"
                                OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                    <center>
                        <br />
                        <div style="float: right">
                            <span style="border-style: none; font-family: Verdana; font-size: 14px; border-color: #E0E0E0; color: #8A2EE6">
                                <a href="../Secondary_Sale_Price_Update.aspx" title="***"
                                    style="text-decoration: none; color: white">***</a>
                            </span>
                        </div>
                        <%--<br />

                        <div style="float: right">
                            <span style="border-style: none; font-family: Verdana; font-size: 14px; border-color: #E0E0E0; color: #8A2EE6">
                                <a href="../MR/SecSales/SecSale_Delete.aspx" title="***"
                                    style="text-decoration: none; color: white">***</a>
                            </span>
                        </div>--%>
                    </center>
                </div>
            </div>
        </div>
           <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

       <%-- <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>--%>
       <%-- <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=txtMonthYear]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "M-yyyy",
                    viewMode: "months",
                    minViewMode: "months",
                    language: "tr"
                });
            });
        </script>--%>
    </form>
</body>
</html>
