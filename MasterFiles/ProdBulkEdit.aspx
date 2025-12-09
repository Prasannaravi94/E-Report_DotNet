<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProdBulkEdit.aspx.cs" Inherits="MasterFiles_ProdBulkEdit" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit - Product Detail</title>
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>

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

        .collp {
            border-collapse: collapse;
        }

        .normal {
            background-color: white;
        }

        .highlight_clr {
            background-color: lightblue;
        }

        .closeLoginPanel {
            font-family: Verdana, Helvetica, Arial, sans-serif;
            height: 14px;
            font-size: 11px;
            font-weight: bold;
            position: absolute;
            top: -2px;
            right: 1px;
        }

            .closeLoginPanel a {
                /*background-color: Yellow;*/
                cursor: pointer;
                color: Black;
                text-align: center;
                text-decoration: none;
                padding: 3px;
            }
            .spacing{
                margin-bottom: 5px;
            }
        .display-table .table th {
            font-size: 12px !important;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript">
        function gvValidate() {

            var f = document.getElementById("grdProduct");
            if (f != null) {
                var TargetChildPrdName = "txtProduct_Detail_Name";
                var TargetChildPrdDescr = "txtProduct_Description";
                var TargetChildPrdSale = "txtProduct_Sale_Unit";
                var TargetChildState = "txtState";

                var Inputs = f.getElementsByTagName("input");
                for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                    if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildPrdName, 0) >= 0) {
                        if (Inputs[i].value == "") {
                            alert("Enter Product Name");
                            f.getElementsByTagName("input").item(i).focus();
                            return false;
                        }
                    }

                    if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildPrdDescr, 0) >= 0) {
                        if (Inputs[i].value == "") {
                            alert("Enter Description");
                            f.getElementsByTagName("input").item(i).focus();
                            return false;
                        }
                    }

                    if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildPrdSale, 0) >= 0) {
                        if (Inputs[i].value == "") {
                            alert("Enter Sales Unit");
                            f.getElementsByTagName("input").item(i).focus();
                            return false;
                        }
                    }

                    if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildState, 0) >= 0) {
                        if (Inputs[i].value == "---- Select ----") {
                            alert("Enter State");
                            f.getElementsByTagName("input").item(i).focus();
                            return false;
                        }
                    }

                }

            }

        }


    </script>
    <script type="text/javascript">
        function HidePopup() {
            var mpu = $find('txtstate_PopupControlExtender');
            mpu.hide();
        }
    </script>
    <script type="text/javascript">
        function HidePopup() {

            var popup = $find('TextBox1_PopupControlExtender');
            popup.hide();
        }
    </script>

      <script type="text/javascript">
        function HidePopup() {

            var popup = $find('txtDivision_PopupControlExtender');
            popup.hide();
        }
    </script>
   



</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />


            <div class="home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <br />
                        <h2 class="text-center">Bulk Edit - Product Detail</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <div class="row justify-content-center">
                                    <asp:Label ID="lblTitle" runat="server" Text="Select the Fields to Edit" ForeColor="#696d6e" Font-Bold="true"
                                        TabIndex="6">   </asp:Label>
                                </div>
                                <br />
                                <div class="row justify-content-center" style="overflow-x: auto; padding-bottom: 20px; margin-left: -35px;">
                                    <asp:CheckBoxList ID="CblProdCode" runat="server"
                                        RepeatColumns="4" RepeatDirection="Horizontal" Width="580px" Font-Bold="true">
                                        <asp:ListItem Value="Product_Detail_Code" Enabled="false">Product Code</asp:ListItem>
                                        <asp:ListItem Value="Product_Detail_Name">Product Name</asp:ListItem>
                                        <asp:ListItem Value="Product_Description">Product Description</asp:ListItem>
                                        <asp:ListItem Value="Product_Cat_Code">Product Category</asp:ListItem>
                                        <asp:ListItem Value="Product_Type_Code">Product Type</asp:ListItem>
                                        <asp:ListItem Value="Product_Sale_Unit">Sale Unit</asp:ListItem>
                                        <asp:ListItem Value="Product_Sample_Unit_One">Sample Unit</asp:ListItem>
                                        <%--<asp:ListItem Value="Product_Sample_Unit_Two">&nbsp;Sample Unit2</asp:ListItem>
                        <asp:ListItem Value="Product_Sample_Unit_Three">&nbsp;Sample Unit3</asp:ListItem>--%>
                                        <asp:ListItem Value="State_Code">State</asp:ListItem>
                                        <asp:ListItem Value="subdivision_code">Sub Division</asp:ListItem>
                                        <asp:ListItem Value="Sample_Erp_Code">Sample ERP Code</asp:ListItem>
                                        <asp:ListItem Value="Sale_Erp_Code">Sale ERP Code</asp:ListItem>
                                        <asp:ListItem Value="Product_Brd_Code">Product Brand</asp:ListItem>
                                          <asp:ListItem Value="Product_Grp_Code">Product Group</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                                <br />
                                <div class="row justify-content-center">
                                    <div class="col-lg-3">

                                        <asp:Label ID="lblGiftType" runat="server" Text="Search By" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Product Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Product Category" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Product Group" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Product Brand" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Sub Division" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="State" Value="7"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-4">
                                        <div class="single-des clearfix" style="padding-top: 19px">
                                            <asp:TextBox ID="TxtSrch" runat="server" CssClass="input" Width="100%" Visible="false"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px">
                                            <asp:DropDownList ID="ddlProCatGrp" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlProCatGrp_SelectedIndexChanged"
                                                CssClass="nice-select" TabIndex="4" Visible="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                </div>
                                <br />

                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton"
                                        Text="Go" OnClick="btnOk_Click" />
                                </div>
                                <br />
                            </div>

                            <div class="display-table clearfix">
                                <div class="table-responsive " style="scrollbar-width: thin;overflow:inherit;">

                                    <div runat="server" id="tblProduct" visible="false" align="center" > <%--style="width: max-content;"--%>
                                       
                                                <asp:GridView ID="grdProduct" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                                    AutoGenerateColumns="false" OnPageIndexChanging="grdProduct_PageIndexChanging" OnRowCreated="grdProduct_RowCreated" OnRowDataBound="grdProduct_RowDataBound" style="background-color:white"
                                                    GridLines="None" CssClass="table ">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="20px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProdCode" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="TxtBxAllowSymb" Width="60px" MaxLength="5" Text='<%#Bind("Product_Detail_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Product_Detail_Name" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="TxtBxAllowSymb" Width="110px" MaxLength="150" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Description" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Product_Description" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb" runat="server" Width="90px" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                                            </ItemTemplate>
                                                           
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProduct_Detail_Name" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" CssClass="input" Height="38px" Width="110px" MaxLength="150" Text='<%# Bind("Product_Detail_Name") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Description" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProduct_Description" Width="200px" onkeypress="AlphaNumeric_NoSpecialChars(event);" CssClass="input" Height="38px" runat="server" Text='<%# Bind("Product_Description") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                    
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Category" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlProduct_Cat_Code" runat="server" CssClass="nice-select"  MaxLength="110" DataSource="<%# FillCategory() %>" DataTextField="Product_Cat_Name" DataValueField="Product_Cat_Code">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product Brand" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlProduct_Brd_Code" runat="server" CssClass="nice-select" MaxLength="110" DataSource="<%# FillBrand() %>" DataTextField="Product_Brd_Name" DataValueField="Product_Brd_Code">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Type" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlProduct_Type_Code" runat="server" CssClass="nice-select">
                                                                    <asp:ListItem Value="0" Selected="True">---Select Type---</asp:ListItem>
                                                                    <asp:ListItem Value="R">Regular Product</asp:ListItem>
                                                                    <asp:ListItem Value="N">New Product</asp:ListItem>
                                                                    <asp:ListItem Value="O">Others</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sale Unit" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProduct_Sale_Unit" Width="80px" onkeypress="AlphaNumeric_NoSpecialChars(event);"  Height="38px"  CssClass="input" runat="server" MaxLength="15" Text='<%# Bind("Product_Sale_Unit") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sample Unit" HeaderStyle-HorizontalAlign="Center">
                                                          
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProduct_Sample_Unit_One" Width="70px" Height="38px"  onkeypress="AlphaNumeric_NoSpecialChars(event);" CssClass="input spacing" runat="server" MaxLength="15" Text='<%# Bind("Product_Sample_Unit_One") %>'></asp:TextBox>&nbsp

                                                                <asp:TextBox ID="txtProduct_Sample_Unit_Two" Width="70px" Height="38px"  onkeypress="AlphaNumeric_NoSpecialChars(event);" CssClass="input spacing" runat="server" MaxLength="15" Text='<%# Bind("Product_Sample_Unit_Two") %>'></asp:TextBox>&nbsp

                                                                <asp:TextBox ID="txtProduct_Sample_Unit_Three" Width="70px" Height="38px"  onkeypress="AlphaNumeric_NoSpecialChars(event);" CssClass="input" runat="server" MaxLength="15" Text='<%# Bind("Product_Sample_Unit_Three") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Center">
                                                           
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="updatepanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtState" runat="server" Width="200px"  Height="38px"  CssClass="input"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnStateId" runat="server"></asp:HiddenField>
                                                                        <asp:PopupControlExtender ID="txtState_PopupControlExtender" runat="server" Enabled="True"
                                                                            ExtenderControlID="" TargetControlID="txtState" PopupControlID="Panel2" OffsetY="2" Position="Bottom">
                                                                        </asp:PopupControlExtender>
                                                                        <asp:Panel ID="Panel2" runat="server" BorderWidth="1px" BorderColor="#d1e2ea" Direction="LeftToRight" BackColor="#f4f8fa" Style="display: none; border-radius: 8px; overflow-x: auto; height: 250px; scrollbar-width: thin">
                                                                            <%--   <div style="height:15px; position:relative; background-color: #4682B4; 
                                        text-transform: capitalize; width:100%; float: left" align="right">
                                        <asp:Button ID="Button2" Style="font-family: Verdana; font-size: 7pt; font-weight:bold; width: 25px; background-color:Yellow; 
                                            color: Black; margin-top: -1px;" Text="X" runat="server" OnClick="btnClose_Click"  OnClientClick="HidePopup();" />
                                        
                                            </div>--%>
                                                                            <div style="height: 17px; position: relative; text-transform: capitalize; width: 100%; float: left"
                                                                                align="right">
                                                                                <div class="closeLoginPanel">
                                                                                    <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                                                        title="Close">X</a>
                                                                                </div>
                                                                            </div>
                                                                            <asp:CheckBoxList ID="chkstate" runat="server" Width="200px" CssClass="gridcheckbox"
                                                                                DataTextField="StateName" DataValueField="State_Code" AutoPostBack="True"
                                                                                OnSelectedIndexChanged="chkstate_SelectedIndexChanged">
                                                                            </asp:CheckBoxList>
                                                                            <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
                                                        SelectCommand="SELECT [State_Code],[StateName] FROM [Mas_State]"></asp:SqlDataSource>--%>
                                                                        </asp:Panel>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Sub Division" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="110px"  ></ItemStyle>
                                                            <ItemTemplate>
                                                            
                                                                        <asp:TextBox ID="txtDivision" runat="server" Width="200px"  Height="38px" CssClass="input"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnDivision" runat="server"></asp:HiddenField>
                                                                        <asp:PopupControlExtender ID="txtDivision_PopupControlExtender" runat="server" Enabled="True"
                                                                            ExtenderControlID="" TargetControlID="txtDivision" PopupControlID="Panel3" OffsetY="2" Position="Bottom">
                                                                        </asp:PopupControlExtender>
                                                                        <asp:Panel ID="Panel3" runat="server" BorderWidth="1px" BorderColor="#d1e2ea" Direction="LeftToRight" BackColor="#f4f8fa" Style="display: none; border-radius: 8px; overflow-x: auto; height: 130px; scrollbar-width: thin">
                                                                            
                                                                            <div style="height: 17px; position: relative; text-transform: capitalize; width: 100%; float: left"
                                                                                align="right">
                                                                                <div class="closeLoginPanel">
                                                                                    <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                                                        title="Close">X</a>
                                                                                </div>
                                                                            </div>
                                                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" Width="200px" CssClass="gridcheckbox"
                                                                                DataTextField="subdivision_name" DataValueField="subdivision_code" AutoPostBack="True"
                                                                                OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
                                                                            </asp:CheckBoxList>
                                                                         
                                                                        </asp:Panel>
                                                                 
                                                                <%--  <asp:ListBox ID="CheckBoxList1" runat="server" SelectionMode="Multiple"  Width="150px" Height="200px"></asp:ListBox>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="Sample ERP Code" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSamp_Erp" Width="80px" onkeypress="AlphaNumeric_NoSpecialChars(event);"  Height="38px" CssClass="input" runat="server" MaxLength="15" Text='<%# Bind("Sample_Erp_Code") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sale ERP Code" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSale_Erp" Width="80px" onkeypress="AlphaNumeric_NoSpecialChars(event);"  Height="38px" CssClass="input" runat="server" MaxLength="15" Text='<%# Bind("Sale_Erp_Code") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>

                                    </div>
                                </div>
                            </div>

                            <br />
                              <div class="row justify-content-center">
                                   <asp:Button ID="btnUpdate" CssClass="savebutton" runat="server" Text="Update" Visible="false"
                                                    OnClick="btnUpdate_Click" OnClientClick="return gvValidate()" />
                                  </div>

                        </div>

                    </div>
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click"  />
                </div>
            </div>



            <%--  <br />
        <table >
            
            <tr>
                <td align="center">
                    <asp:Button ID="btnOk" runat="server" CssClass="savebutton" Width="30px" Height="25px" Text="Go"  
                        onclick="btnOk_Click" />
                   
                    <asp:Button ID="btnClr" CssClass="savebutton" runat="server" Width="60px" Height="25px" Text="Clear" 
                         onclick="btnClr_Click" />
                </td>
            </tr>
        </table>
        <br />--%>


            <div class="loading" align="center">
                Loading. Please wait.
           
                <img src="../Images/loader.gif" alt="" />
            </div>
         <%--   <link type="text/css" href="../css/multiple-select.css" rel="Stylesheet" />

            <script type="text/javascript" src="../JsFiles/multiple-select_2.js"></script>
            <script type="text/javascript">

                $('[id*=CheckBoxList1]').multipleSelect();
            </script>--%>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
