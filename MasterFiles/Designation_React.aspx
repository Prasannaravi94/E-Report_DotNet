<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Designation_React.aspx.cs" Inherits="MasterFiles_Designation_React" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Designation Reactivation</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
                <div class="row justify-content-center">
                    <div class="col-lg-8">
                        <br />
                        <h2 class="text-center">Designation Reactivation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading text-center clearfix">
                                <div class="d-inline-block division-name">Division Name</div>
                                <div class="d-inline-block align-middle">
                                    <div class="single-des-option">
                                        <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="nice-select"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">

                                    <asp:GridView ID="grdDesignation" runat="server"
                                        AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                        OnRowCommand="grdDesignation_RowCommand" GridLines="None" CssClass="table"
                                        AllowSorting="true">

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

                                            <asp:TemplateField HeaderText="Short Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignationName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText ="FieldForce Count" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign ="Left" >
                           <ItemTemplate >
                              <asp:Label ID ="lblSfCount" runat ="server" Text='<%# Bind("sf_count") %>'></asp:Label>
                           </ItemTemplate>
                        
                        </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Reactivate" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Designation_Code") %>'
                                                        CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Designation');">Reactivate 
                                                    </asp:LinkButton>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>




                        </div>
                    </div>
                     
                </div>
                <asp:Button ID="btnback" runat="server" Text="Back" CssClass="backbutton" OnClick="btnback_Click" />
              
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
