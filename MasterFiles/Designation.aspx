<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Designation.aspx.cs" Inherits="MasterFiles_Designation" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Designation</title>
  <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
             <br />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Designation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-7">


                                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Width="45px" Text="Add" OnClick="btnNew_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="resetbutton" Text="S.No Gen" Width="75px" OnClick="btnSlNo_Gen_Click" />&nbsp;
                                        <asp:Button ID="btnReactivate" runat="server" CssClass="resetbutton" Text="Reactivation" OnClick="btnReactivate_Click" />&nbsp;
                                        <asp:Button ID="btnDes_level" runat="server" CssClass="resetbutton" Width="175px"
                                            Text="Designation level - Mgr" OnClick="btnDes_level_Click" />
                                    </div>
                                    <div class="col-lg-5 ">
                                        <div class="d-inline-block division-name">Division Name</div>
                                        <div class="d-inline-block align-middle">
                                            <div class="single-des-option" style="min-width:250px;">
                                                <%-- <asp:Label ID="Lbldivi" runat="server" SkinID="lblMand">Division Name</asp:Label>--%>
                                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="nice-select"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdDesignation" runat="server"
                                        OnRowUpdating="grdDesignation_RowUpdating" OnRowEditing="grdDesignation_RowEditing"
                                        OnRowCommand="grdDesignation_RowCommand" 
                                        OnPageIndexChanging="grdDesignation_PageIndexChanging" OnRowCreated="grdDesignation_RowCreated"
                                        OnRowCancelingEdit="grdDesignation_RowCancelingEdit" AllowSorting="true" OnSorting="grdDesignation_Sorting"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignationCode" runat="server" Text='<%#Eval("Designation_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Designation_Short_Name" HeaderText="Short Name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtShortName" runat="server" CssClass="input"  MaxLength="4" onkeypress="CharactersOnly(event);" Text='<%# Bind("Designation_Short_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Designation_Name" HeaderText="Designation">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDesignationName" Width="160px" CssClass="input" runat="server" MaxLength="70" onkeypress="CharactersOnly(event);" Text='<%# Bind("Designation_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignationName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Level" >

                                                <ItemTemplate>
                                                    <asp:Label ID="lbllevel" runat="server" Text='<%# Bind("Level") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForce Count" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfCount" runat="server" Text='<%# Bind("sf_count") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Division_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldivi" runat="server" Text='<%# Bind("Division_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="Center" 
                                                ShowEditButton="True"></asp:CommandField>
                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="DesignationCreation.aspx?Designation_Code={0}&amp;Division_Code={1}"
                                                DataNavigateUrlFields="Designation_Code,Division_Code"></asp:HyperLinkField>

                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Designation_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Designation');">Deactivate
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../Images/deact2.png" alt="" width="75px" title="This Designation Exists in Field Force" />
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                      
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                  <div class="no-result-area" id="divid" runat="server" visible="false" >
                        No Records Found
                    </div>


            </div>
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
            <br />
        </div>
    </form>
</body>
</html>
