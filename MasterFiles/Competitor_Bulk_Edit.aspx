<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Competitor_Bulk_Edit.aspx.cs" Inherits="MasterFiles_Competitor_Bulk_Edit" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Competitor Bulk - Edit</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
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
  <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <ucl:Menu ID="menu1" runat="server" />
        <br />
    <br />
    <center>

     <table align="center">
                <tr>
                    <td align="center" class="stylespc">
                        <asp:Label ID="lblname" runat="server" SkinID="lblMand"  Height="18px">Competitor Name</asp:Label>
                    </td>
                    <td class="stylespc" align="center">
                        <asp:DropDownList ID="ddltype" runat="server" onblur="this.style.backgroundColor='White'"
                         onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired"
                            AutoPostBack="true" TabIndex="7" OnSelectedIndexChanged="ddlcompe_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Competitor</asp:ListItem>
                            <asp:ListItem Value="2">Competitor Product</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                
            </table>
            </center>
            <br />

             <table align="center" style="width: 100%">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdCampet" runat="server" Width="65%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" 
                        EmptyDataText="No Records Found" 
                       GridLines="None" CssClass="mGrid"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10px">
                                    <ItemTemplate>
                                 
                                         <asp:Label ID="lblSNo" runat="server" Text='<%# (grdCampet.PageIndex * grdCampet.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Campetitor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComp_Sl_No" runat="server" Text='<%#Eval("Comp_Sl_No")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="120px" HeaderStyle-ForeColor="white" HeaderText="Competitor Name" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                       <asp:TextBox ID="txtComp_Name" Width="90%" runat="server" Text='<%# Bind("Comp_Name") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>

                      </tbody>
        </table>
        <br />
        <center>
<asp:Button ID="btnSubmit" runat="server" Width="60px" Height="25px"
                            CssClass="savebutton" Text="Update" OnClick="btnSubmit_Click" />
                            </center>
    
    </div>
    </form>
</body>
</html>
