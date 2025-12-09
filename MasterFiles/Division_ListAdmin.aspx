<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Division_ListAdmin.aspx.cs" Inherits="MasterFiles_Division_ListAdmin" %>

<%@ Register Src="~/UserControl/AdminMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Division List</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <%--    <script type="text/javascript">

  window.onload=function(){
      document.getElementById('btnNew').focus();
  }

    </script>--%>
    <style type="text/css">
        #tblDivisionDtls
        {
            margin-left: 300px;
        }
        #tblLocationDtls
        {
            margin-left: 300px;
        }
        .style2
        {
            width: 92px;
            height: 25px;
        }
        .style3
        {
            height: 25px;
        }
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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
        <asp:Panel ID="pnldivi" runat="server">
            <table width="80%">
                <tr>
                    <td style="width: 9.2%" />
                    <td class="style3">
                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Text="Add" Width="60px"
                            Height="25px" OnClick="btnNew_Click" />&nbsp;
                        <asp:Button ID="btnSlNo" runat="server" CssClass="savebutton" Text="S.No Gen" Width="90px"
                            Height="25px" OnClick="btnSlNo_Click" />&nbsp;
                        <asp:Button ID="btnReactivate" runat="server" CssClass="savebutton" Width="110px" Height="25px"
                            Text="Reactivation" OnClick="btnReactivate_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdDivision" runat="server" Width="85%" HorizontalAlign="Center"
                                    AutoGenerateColumns="false"  EmptyDataText="No Records Found"
                                    OnRowUpdating="grdDivision_RowUpdating" OnRowEditing="grdDivision_RowEditing"
                                    OnPageIndexChanging="grdDivision_PageIndexChanging" OnRowCreated="grdDivision_RowCreated"
                                    OnRowCancelingEdit="grdDivision_RowCancelingEdit" OnRowCommand="grdDivision_RowCommand"
                                    GridLines="None" CssClass="mGrid" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt"
                                    AllowSorting="True" OnSorting="grdDivision_Sorting">
                                    <HeaderStyle Font-Bold="False" />
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="20px" HeaderStyle-ForeColor="white"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Div_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDivCode" runat="server" Text='<%#Eval("Division_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Division_Name" HeaderStyle-Width="280px" HeaderStyle-ForeColor="white"
                                            HeaderText="Division Name" ItemStyle-HorizontalAlign="Left">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDiv" runat="server" Width="280px" SkinID="TxtBxAllowSymb" MaxLength="100"
                                                    Text='<%# Bind("Division_Name") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiv" runat="server" Text='<%# Bind("Division_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="New S.No" HeaderStyle-ForeColor="White" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                    <asp:Label ID="lblNewSNo" runat="server" Text='<%#Eval("Div_Sl_No")%>'></asp:Label>
                                </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNewSlNo" runat="server" MaxLength="3" Width="50%" SkinID="MandTxtBox" Text='<%#Eval("Div_Sl_No")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>--%>
                                        <%--<asp:TemplateField SortExpression="Division_SName" HeaderStyle-ForeColor="white" HeaderText="Short Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSName"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="3" Text='<%# Bind("Division_SName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSName" runat="server" Text='<%# Bind("Division_SName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                        <asp:TemplateField SortExpression="Alias_Name" HeaderStyle-ForeColor="white" HeaderStyle-Width="100px"
                                            HeaderText="Alias Name" ItemStyle-HorizontalAlign="Left">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAlName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="8"
                                                    Text='<%# Bind("Alias_Name") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAlName" runat="server" Text='<%# Bind("Alias_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Division_City" HeaderText="City" HeaderStyle-Width="160px"
                                            HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCity" runat="server" SkinID="TxtBxChrOnly" MaxLength="20" Text='<%# Bind("Division_City") %>'
                                                    onkeypress="CharactersOnly(event);"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCity" runat="server" Text='<%# Bind("Division_City") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-Width="120px"
                                            HeaderStyle-ForeColor="White" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER"
                                            ShowEditButton="True">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                                Font-Bold="True"></ItemStyle>
                                        </asp:CommandField>
                                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" HeaderStyle-Width="100px" DataNavigateUrlFormatString="DivisionCreation_admin.aspx?Div_Code={0}"
                                            DataNavigateUrlFields="Division_code" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Center">
                                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                            </ControlStyle>
                                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                        </asp:HyperLinkField>
                                        <asp:TemplateField HeaderText="Deactivate" HeaderStyle-ForeColor="White" HeaderStyle-Width="120px">
                                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                            </ControlStyle>
                                            <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Division_Code") %>'
                                                    CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the division');">Deactivate
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stand By" HeaderStyle-ForeColor="White" HeaderStyle-Width="120px">
                                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                            </ControlStyle>
                                            <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbutStand" runat="server" CommandArgument='<%# Eval("Division_Code") %>'
                                                    CommandName="Standby" OnClientClick="return confirm('Do you want to Stand by the division');">Stand by
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    
    </div>
    </form>
</body>
</html>
