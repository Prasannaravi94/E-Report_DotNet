<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChemistClassList.aspx.cs" Inherits="MasterFiles_ChemistCategoryList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chemists-Class</title>
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
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center" id="heading" runat="server"></h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-12">
                                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Width="45px" Text="Add" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnBulkEdit" runat="server" CssClass="resetbutton" Text="Bulk Edit" OnClick="btnBulkEdit_Click" />
                                        <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="resetbutton" Text="S.No Gen" OnClick="btnSlNo_Gen_Click" />
                                        <asp:Button ID="btnTransfer_Class" runat="server" CssClass="resetbutton" Width="120px" Text="Transfer Class" OnClick="btnTransfer_Class_Click" />
                                        <asp:Button ID="btnReactivate" runat="server" CssClass="resetbutton" Text="Reactivation" OnClick="btnReactivate_Onclick" />
                                    </div>

                                </div>
                            </div>
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdChemClass" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                        OnRowUpdating="grdChemClass_RowUpdating" OnRowEditing="grdChemClass_RowEditing"
                                        OnRowDeleting="grdChemClass_RowDeleting" EmptyDataText="No Records Found"
                                        OnPageIndexChanging="grdChemClass_PageIndexChanging" OnRowCreated="grdChemClass_RowCreated"
                                        OnRowCancelingEdit="grdChemClass_RowCancelingEdit" OnRowCommand="grdChemClass_RowCommand"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                        AllowSorting="True" OnSorting="grdChemClass_Sorting">
                                       
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Chemist Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChemClassCode" runat="server" Text='<%#Eval("Class_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Chem_Class_SName" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtChem_Class_SName" runat="server" CssClass="input" MaxLength="12" Text='<%# Bind("Chem_Class_SName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChem_Class_SName" runat="server" Text='<%# Bind("Chem_Class_SName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Chem_Class_Name"   HeaderText="Class Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtChemClassName" CssClass="input" runat="server"  MaxLength="100" Text='<%# Bind("Chem_Class_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChemClassName" runat="server" Text='<%# Bind("Chem_Class_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="No of Chemists"  ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Class_Count") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="CENTER"
                                                ShowEditButton="True">
                                               
                                            </asp:CommandField>
                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="ChemistClass.aspx?Class_Code={0}"
                                                DataNavigateUrlFields="Class_Code">
                                              
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                               
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Class_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Chemists Class');">Deactivate
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../Images/deact1.png" alt="" width="55px" title="This Class Exists in Chemists" />
                                                    </asp:Label>
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



















            <table width="80%">
                <tr>
                    <td style="width: 9.2%" />
                    <td></td>
                    <td></td>
                </tr>
            </table>
            <br />
            <table align="center" style="width: 100%">
                <tbody>
                    <tr>
                        <td colspan="2" align="center"></td>
                    </tr>
                </tbody>
            </table>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
