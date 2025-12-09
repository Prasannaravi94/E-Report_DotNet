<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivisionList.aspx.cs" Inherits="MasterFiles_DivisionList" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Welcome Corporate – HQ</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/png" href="../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="../assets/css/nice-select.css">
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/css/style.css">
    <link rel="stylesheet" href="../assets/css/responsive.css">
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>

    <style type="text/css">
        .modal {
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
            <asp:Panel ID="pnldivi" runat="server">
                <table width="80%">
                    <tr>
                        <td style="width: 9.2%" />
                        <td class="style3">
                            <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Text="Add" Width="60px" Visible="true"
                                Height="25px" OnClick="btnNew_Click" />&nbsp;
                        <asp:Button ID="btnSlNo" runat="server" CssClass="savebutton" Text="S.No Gen" Width="90px" Visible="true"
                            Height="25px" OnClick="btnSlNo_Click" />&nbsp;
                        <asp:Button ID="btnReactivate" runat="server" CssClass="savebutton" Width="110px" Height="25px" Visible="true"
                            Text="Reactivation" OnClick="btnReactivate_Click" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <div class="designation-reactivation-table-area clearfix">
                            <h2 class="text-center">Division List</h2>
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdDivision" runat="server" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" GridLines="None" AllowPaging="True" PageSize="10"
                                        OnRowUpdating="grdDivision_RowUpdating" OnRowEditing="grdDivision_RowEditing"
                                        OnPageIndexChanging="grdDivision_PageIndexChanging" OnRowCreated="grdDivision_RowCreated"
                                        OnRowCancelingEdit="grdDivision_RowCancelingEdit" OnRowCommand="grdDivision_RowCommand"
                                        CssClass="table"
                                        AllowSorting="True" OnSorting="grdDivision_Sorting">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Div_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDivCode" runat="server" Text='<%#Eval("Division_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Division_Name"
                                                HeaderText="Division Name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDiv" runat="server" Width="230px" CssClass="input" MaxLength="100"
                                                        Text='<%# Bind("Division_Name") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiv" runat="server" Text='<%# Bind("Division_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Alias_Name"
                                                HeaderText="Alias Name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtAlName" runat="server" MaxLength="8" CssClass="input" Width="100px"
                                                        Text='<%# Bind("Alias_Name") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAlName" runat="server" Text='<%# Bind("Alias_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Division_City" HeaderText="City" ItemStyle-HorizontalAlign="center">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="input" MaxLength="20" Width="130px" Text='<%# Bind("Division_City") %>'
                                                        onkeypress="CharactersOnly(event);"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCity" runat="server" Text='<%# Bind("Division_City") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="center"
                                                ShowEditButton="True"></asp:CommandField>
                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="DivisionCreation.aspx?Div_Code={0}"
                                                DataNavigateUrlFields="Division_code">

                                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center"></ItemStyle>
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="center">

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Division_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the division');">Deactivate
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stand By">


                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutStand" runat="server" CommandArgument='<%# Eval("Division_Code") %>'
                                                        CommandName="Standby" OnClientClick="return confirm('Do you want to Stand by the division');">Stand by
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="loading" align="center">
                                Loading. Please wait.<br />
                                <br />
                                <img src="../Images/loader.gif" alt="" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>


    </form>
    <script src="../assets/js/jQuery.min.js"></script>
    <script src="../assets/js/popper.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/jquery.nice-select.min.js"></script>
    <script src="../assets/js/main.js"></script>
</body>
</html>
