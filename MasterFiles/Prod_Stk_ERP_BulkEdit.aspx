<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prod_Stk_ERP_BulkEdit.aspx.cs" Inherits="MasterFiles_Prod_Stk_ERP_BulkEdit" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stockist/Product ERP Code Bulk Edit</title>
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

        .spacing {
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
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />


            <div class="home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <br />
                        <h2 class="text-center">Stockist/Product - ERP Code Bulk Edit</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <div class="row justify-content-center">
                                    <asp:Label ID="lblTitle" runat="server" Text="Select the Fields to Edit" ForeColor="#696d6e" Font-Bold="true"
                                        TabIndex="6">   </asp:Label>
                                </div>
                                <br />
                                <div class="row justify-content-center" style="overflow-x: auto; padding-bottom: 20px; margin-left: -35px;">

                                    <asp:RadioButtonList ID="rdolstrpt" runat="server" RepeatDirection="Horizontal" Width="580px" Font-Bold="true" RepeatColumns="3" AutoPostBack="true" OnSelectedIndexChanged="rdolstrpt_SelectedIndexChanged">
                                        <asp:ListItem Value="Stk_Erp_Code" Text="Stockist ERP Code&nbsp;&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="Sample_Erp_Code" Text="Product Sample ERP Code&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                        <asp:ListItem Value="SF_Cat_Code" Text="FF HQ Code &nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                    </asp:RadioButtonList>

                                    <%--<asp:CheckBoxList ID="CblProdCode" runat="server"
                                        RepeatColumns="4" RepeatDirection="Horizontal" Width="580px" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="CblProdCode_SelectedIndexChanged">
                                        <asp:ListItem Value="Stk_Erp_Code" Selected="True">Stockist ERP Code</asp:ListItem>
                                        <asp:ListItem Value="Sample_Erp_Code">Product Sample ERP Code</asp:ListItem>
                                    </asp:CheckBoxList>--%>
                                </div>
                                <br />
                                <div class="row justify-content-center">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblGiftType" runat="server" Text="Search By" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlstk" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Stockist Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="State" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="HQ Name" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="ERP Code" Value="8"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged" Visible="false">
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
                                <div class="table-responsive " style="scrollbar-width: thin; overflow: inherit;">

                                    <div runat="server" id="tblProduct" visible="false" align="center">
                                        <%--style="width: max-content;"--%>

                                        <asp:GridView ID="grdProduct" runat="server" Width="80%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                            AutoGenerateColumns="false" OnPageIndexChanging="grdProduct_PageIndexChanging" OnRowCreated="grdProduct_RowCreated" OnRowDataBound="grdProduct_RowDataBound" Style="background-color: white"
                                            GridLines="None" CssClass="table ">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProdCode" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="TxtBxAllowSymb" MaxLength="5" Text='<%#Bind("Product_Detail_Code")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Product_Detail_Name" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Description" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Product_Description" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Product Category" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Product_Cat_Name" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Product Brand" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Product_Brd_Code" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("Product_Brd_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Product Group" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Product_Group" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("Product_Grp_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Sample ERP Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSamp_Erp" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="38px" CssClass="input" runat="server" MaxLength="15" Text='<%# Bind("Sample_Erp_Code") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>


                                        <br />

                                        <asp:GridView ID="grdStk" runat="server" Width="80%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                            AutoGenerateColumns="false" OnPageIndexChanging="grdStk_PageIndexChanging" OnRowCreated="grdStk_RowCreated" OnRowDataBound="grdStk_RowDataBound" Style="background-color: white"
                                            GridLines="None" CssClass="table ">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stockist Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStockistCode" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="TxtBxAllowSymb" MaxLength="5" Text='<%#Bind("Stockist_Code")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stockist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Stockist_Name" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_State" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HQ Name" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_HQ_Name" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("Approved_by") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HQ Code" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_HQ_Code" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("SF_Cat_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ERP Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtstk_Erp" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="38px" CssClass="input" runat="server" MaxLength="15" Text='<%# Bind("Stockist_Designation") %>'></asp:TextBox>
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
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />
                </div>
            </div>

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
