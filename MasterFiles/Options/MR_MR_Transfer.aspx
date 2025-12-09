<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MR_MR_Transfer.aspx.cs" Inherits="MasterFiles_Options_MR_MR_Transfer" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Master Details</title>
    <style type="text/css">
        table.gridtable {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }

            table.gridtable th {
                padding: 5px;
            }

            table.gridtable td {
                border-width: 1px;
                padding: 5px;
                border-style: solid;
                border-color: #666666;
            }
    </style>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;
                    }
                    else {

                        inputList[i].checked = false;

                    }
                }
            }
        }
    </script>
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
         .display-table #grdDoctor td,.display-table #GrdTransfer td {
            padding: 10px 5px;
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
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <br />
                    <h2 class="text-center" id="hHeading" runat="server"></h2>
                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:RadioButtonList ID="rdotransfer" BorderStyle="None" AutoPostBack="true" runat="server"
                                        RepeatDirection="Horizontal" OnSelectedIndexChanged="rdotransfer_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">Listed Doctor &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:ListItem>
                                        <asp:ListItem Text="Chemist" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <br />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Button ID="btnTran" runat="server" CssClass="savebutton" Text="Transfer" OnClick="btnTransfer_Click" />
                                    <asp:Button ID="btnClr" runat="server" CssClass="savebutton" Text="Clear All" OnClick="btnClear_Click" />
                                    <asp:Button ID="btnTransfer" runat="server" Text="Transfer" Visible="false" CssClass="savebutton" OnClick="btnTransfer_Click" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="savebutton" Visible="false" OnClick="btnClear_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblFF" runat="server" CssClass="label">Transfer From</asp:Label>
                                    <%--<asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" 
                                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" >
                                        </asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlFromFieldForce" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFromFieldForce_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblterrFrom" runat="server" CssClass="label">Transfer From Territory</asp:Label>
                                    <asp:DropDownList ID="ddlFromTerr" runat="server" AutoPostBack="true" CssClass="custom-select2 nice-select" Width="100%"
                                        OnSelectedIndexChanged="ddlFromTerr_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lbltoFF" runat="server" CssClass="label">Transfer To</asp:Label>
                                    <asp:DropDownList ID="ddlToFieldForce" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlToFieldForce_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblterrTo" runat="server" CssClass="label">Transfer To Territory</asp:Label>
                                    <asp:DropDownList ID="ddlToTerr" runat="server" AutoPostBack="true" CssClass="custom-select2 nice-select" Width="100%"
                                        OnSelectedIndexChanged="ddlToTerr_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-lg-3">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                                    <asp:Label ID="lblTo" CssClass="label" runat="server" ForeColor="Red" Visible="false">Please Select the Transfer To</asp:Label>
                                    <asp:Label ID="lblToTerr" CssClass="label" runat="server" ForeColor="Red" Visible="false">Select the Transfer To Territory</asp:Label>
                                    <asp:Label ID="lblSelect" CssClass="label" runat="server" ForeColor="Red" Visible="false">Please Select the Transfer From</asp:Label>
                                    <asp:Label ID="lblTerr1" CssClass="label" runat="server" ForeColor="Red" Visible="false">Please Select the Transfer From Territory</asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-table clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdDoctor" runat="server" HorizontalAlign="Center" EmptyDataText="No Listed Doctor's Found"
                                                    AutoGenerateColumns="false" GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                                    OnRowDataBound="grdDoctor_RowDataBound" AlternatingRowStyle-CssClass="alt">
                                                    <HeaderStyle Font-Bold="False" />
                                                    <PagerStyle CssClass="gridview1"></PagerStyle>
                                                    <SelectedRowStyle BackColor="BurlyWood" />
                                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="ListedDr_Name" HeaderText="Listed Doctor Name"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Doc_Cat_Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Doc_Special_Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Speciality">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField SortExpression="territory_Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Territory">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Transfer" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" AutoPostBack="true" Text="  Transfer" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkListedDR" runat="server" AutoPostBack="true" Text="&nbsp;" OnCheckedChanged="CheckBox_CheckChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                        VerticalAlign="Middle" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdChem" runat="server" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                                    AutoGenerateColumns="false" Visible="false" GridLines="None" CssClass="table"
                                                    OnRowDataBound="grdChem_RowDataBound" AlternatingRowStyle-CssClass="alt">
                                                    <HeaderStyle Font-Bold="False" />
                                                    <PagerStyle CssClass="gridview1"></PagerStyle>
                                                    <SelectedRowStyle BackColor="BurlyWood" />
                                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%# (grdChemist.PageIndex * grdChemist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chemists Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Chemists_Code" runat="server" Text='<%#Eval("Chemists_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Chemists_Name" HeaderText="Chemists Name"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChemName" runat="server" Text='<%#Eval("Chemists_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Chemists_Contact" HeaderText="Contact Person"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblContact" runat="server" Text='<%#Eval("Chemists_Contact")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblterr_code" runat="server" Text='<%# Bind("Territory_Code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="territory_Name" HeaderText="Territory"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblterr" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Transfer" ItemStyle-HorizontalAlign="Center">
                                                            <%--<HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" AutoPostBack="true" Text="  Transfer" />
                                    </HeaderTemplate>--%>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAllchem" runat="server" onclick="checkAll(this);" AutoPostBack="true" Text="  Transfer" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkChemist" runat="server" AutoPostBack="true" Text="&nbsp;" OnCheckedChanged="chkChemist_Changed" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                        VerticalAlign="Middle" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-table clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GrdTransfer" runat="server" HorizontalAlign="Center" EmptyDataText="No Listed Doctor's Found"
                                                    AutoGenerateColumns="false" OnRowDataBound="GrdTransfer_RowDataBound" GridLines="None"
                                                    CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">
                                                    <HeaderStyle Font-Bold="False" />
                                                    <PagerStyle CssClass="gridview1"></PagerStyle>
                                                    <SelectedRowStyle BackColor="BurlyWood" />
                                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="ListedDr_Name" HeaderText="Listed Doctor Name"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Doc_Cat_Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Doc_Special_Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Speciality">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblterr_code" runat="server" Text='<%# Bind("Territory_Code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="territory_Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Territory">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Color" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblColor" runat="server" Text='<%#Eval("color")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                        VerticalAlign="Middle" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdChemist" runat="server" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                                    AutoGenerateColumns="false" Visible="false" OnRowDataBound="grdChemist_RowDataBound"
                                                    GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt">
                                                    <HeaderStyle Font-Bold="False" />
                                                    <PagerStyle CssClass="gridview1"></PagerStyle>
                                                    <SelectedRowStyle BackColor="BurlyWood" />
                                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%# (grdChemist.PageIndex * grdChemist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chemists Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Chemists_Code" runat="server" Text='<%#Eval("Chemists_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Chemists_Name" HeaderText="Chemists Name"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChemName" runat="server" Text='<%#Eval("Chemists_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Chemists_Contact" HeaderText="Contact Person"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblContact" runat="server" Text='<%#Eval("Chemists_Contact")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="territory_Name" HeaderText="Territory"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblterr" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Color" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblColor" runat="server" Text='<%#Eval("color")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                        VerticalAlign="Middle" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
