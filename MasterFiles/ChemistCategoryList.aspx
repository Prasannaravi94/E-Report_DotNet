<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChemistCategoryList.aspx.cs" Inherits="MasterFiles_ChemistCategoryList" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chemists-Category</title>

      <style type="text/css">
            .modal
    {
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
    .loading
    {
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
          .resetbutton
          {}
          .savebutton
          {}
          .align
          {
              min-width:150px;
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
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">Chemists-Category</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-name-heading clearfix">
                            <div class="row clearfix">
                                <div class="col-lg-7">
                                    <asp:Button ID="btnNew" runat="server" CssClass="savebutton"  Width="61px" Text="Add" onClick="btnNew_Click"/>
                                    <asp:Button ID="btnBulkEdit" runat="server" CssClass="resetbutton"  Width="77px" Text="Bulk Edit" onClick="btnBulkEdit_Click"/>
                                    <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="resetbutton"  Width="79px" Text="S.No Gen" onClick="btnSlNo_Gen_Click"/>
                                    <asp:Button ID="btnTransfer_Cat" runat="server" CssClass="resetbutton"  Width="130px" Text="Transfer Category" onClick="btnTransfer_Cat_Click" />
                                    <asp:Button ID="btnReactivate" runat="server" CssClass="resetbutton"  Width="95px" Text="Reactivation" OnClick="btnReactivate_Onclick"/>
                                </div>
                            </div>
                        </div>
                        <p>
                            <br />
                        </p>
                        <div class="display-table clearfix">
                            <div class="table-responsive">
                                <asp:GridView ID="grdChemCat" runat="server" Width="100%" HorizontalAlign="Center"
                                 AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                 onrowupdating="grdChemCat_RowUpdating" onrowediting="grdChemCat_RowEditing" 
                                 onrowdeleting="grdChemCat_RowDeleting" EmptyDataText="No Records Found"
                                 onpageindexchanging="grdChemCat_PageIndexChanging" OnRowCreated="grdChemCat_RowCreated"
                                 onrowcancelingedit="grdChemCat_RowCancelingEdit" onrowcommand="grdChemCat_RowCommand" 
                                 GridLines="None" CssClass="table" PagerStyle-CssClass="pgr"
                                 AllowSorting="True" OnSorting="grdChemCat_Sorting"  >
                                 <PagerStyle CssClass="GridView1"></PagerStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chemist Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChemCatCode" runat="server" Text='<%#Eval("Cat_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField SortExpression="Chem_Cat_SName" HeaderStyle-Width="120px" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtChem_Cat_SName" runat="server" CssClass="input" MaxLength="12" Text='<%# Bind("Chem_Cat_SName") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblChem_Cat_SName" runat="server" Text='<%# Bind("Chem_Cat_SName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField SortExpression="Chem_Cat_Name" HeaderStyle-Width="260px" HeaderText="Category Name" ItemStyle-HorizontalAlign="Left">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtChemCatName" CssClass="input" runat="server" Width="200px" MaxLength="100" Text='<%# Bind("Chem_Cat_Name") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblChemCatName" runat="server" Text='<%# Bind("Chem_Cat_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="No of Chemists" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Cat_Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  

                                        <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="Center"
                                            ShowEditButton="True" ItemStyle-CssClass="align">
                                            <%--<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle ForeColor="DarkBlue" 
                                    HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ItemStyle>--%>
                                        </asp:CommandField>
                                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="ChemistCategory.aspx?Cat_Code={0}"
                                            DataNavigateUrlFields="Cat_Code">
                                            <%--<ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                        </asp:HyperLinkField>
                                        <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                            <%--<ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Cat_Code") %>'
                                                    CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Chemists Category');">Deactivate
                                                </asp:LinkButton>
                                                <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                                <img src="../Images/deact2.png" alt="" width="75px" title="This Category Exists in Chemists" />
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area"/>
                                 </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

           <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="../Images/loader.gif" alt="" />
</div>
    </div>
    </form>
</body>
</html>
