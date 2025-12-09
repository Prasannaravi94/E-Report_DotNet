<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sub_HO_ID_View.aspx.cs" Inherits="MasterFiles_Sub_HO_ID_View" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HO-ID-View</title>
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
    <%--  --%>
   <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
                        <h2 class="text-center">HO-ID-View</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-7">

                                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Width="60px"  Text="Add" OnClick="btnNew_Click" />&nbsp;  
                                      
                                    </div>

                                </div>
                            </div>
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">

                                    <asp:GridView ID="grdSubHoID" runat="server" 
                                        AutoGenerateColumns="false"  OnRowCommand="grdSubHoID_RowCommand"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt">
                                        <HeaderStyle Font-Bold="false" />
                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                       
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HO ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHOID" runat="server" Text='<%#Eval("HO_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="User Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_UsrName" runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Password" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Pwd" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField   HeaderText="Employee Id" >
                            <ItemTemplate>
                                <asp:Label ID="lbl_Emp" runat="server" Text='<%# Bind("Emp_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit"  ItemStyle-HorizontalAlign="center" DataNavigateUrlFormatString="Sub_HO_ID_Creation.aspx?HO_ID={0} & division_code={0}"
                                                DataNavigateUrlFields="HO_ID">
                                              <%--  <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="Deactivate"  ItemStyle-HorizontalAlign="center">
                                               <%-- <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("HO_ID") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate');">Deactivate
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

                <div class="no-result-area" id="divid" runat="server" visible="false">
                    No Records Found
                </div>
            </div>

              <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
            <br />
            <br />

       </div>
    </form>
</body>
</html>
