<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEdit_Chemists.aspx.cs" Inherits="MasterFiles_MR_Chemist_BulkEdit_Chemists" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit - Chemist</title>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" language="javascript">
        function checkEmail() {
            var email = document.getElementById('Chemists_EMail');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(email.value)) {
                alert('Please provide a valid email address');
                email.focus;
                return false;
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

        [type="checkbox"]:disabled:checked + label:after {
            top: 0em;
            left: .2em;
        }
        /*[type="checkbox"]:disabled:checked + label:before
     {
         left: .3em;
         top: .4em;
     }*/
        /*.marRight
        {
            margin-right:35px;
        }*/
		.display-table .table td {
    padding: 5px 5px !important;
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
</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>
        <div>
            <%--  <ucl:Menu ID="menu1" runat="server" />--%>
            <div class="home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <h2 class="text-center" style="border-style: none;">Bulk Edit - Chemist</h2>
                        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" Style="text-align: center; font-size: 18px;">
                            <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                        </asp:Panel>
                        <br />
                        <div class="row justify-content-center">
                            <%--   <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                            <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server"
                                OnClick="btnBack_Click" />
                        </div>
                        <div class="row justify-content-center" style="text-align: center;">
                            <asp:Label ID="lblTitle" runat="server" ForeColor="#696D6E" Text="Select the Fields to Edit"
                                TabIndex="6">
                            </asp:Label>
                        </div>

                        <div class="row justify-content-center" style="overflow-x: auto;">
                              <div class="col-lg-6">
                            <table border="0" cellpadding="3" cellspacing="3" id="tblLocationDtls" align="center" width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:CheckBoxList ID="CblChemistsCode" CssClass="Checkbox" runat="server" Style="width: 600px; margin-top: 25px;"
                                            RepeatColumns="4" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Chemists_Name">&nbsp;Chemists Name</asp:ListItem>
                                            <asp:ListItem Value="Chemists_Contact">&nbsp;Contact Person</asp:ListItem>
                                            <asp:ListItem Value="Chemists_Address1">&nbsp;Address</asp:ListItem>
                                            <asp:ListItem Value="Chemists_Phone">&nbsp;Phone</asp:ListItem>
                                            <asp:ListItem Value="Chemists_Fax">&nbsp;Fax</asp:ListItem>
                                            <asp:ListItem Value="Chemists_EMail">&nbsp;EMail ID</asp:ListItem>
                                            <asp:ListItem Value="Chemists_Mobile">&nbsp;Mobile No</asp:ListItem>
                                            <asp:ListItem Value="Chemist_ERP_Code">&nbsp;Chemist ERP Code</asp:ListItem>
                                            <asp:ListItem Value="Cat_Code">&nbsp;Category</asp:ListItem>
                                            <%--  <asp:ListItem Value="Territory_Code">&nbsp;Territory</asp:ListItem>--%>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnOk" runat="server" CssClass="savebutton" Text="Ok"
                                            OnClick="btnOk_Click" />
                                        <asp:Button ID="btnClr" CssClass="resetbutton" runat="server" Text="Clear"
                                            OnClick="btnClr_Click" />
                                    </td>
                                </tr>
                            </table>
                                  </div>
                        </div>
                        <br />
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                    <table runat="server" id="tblDoctor" visible="false" width="100%" align="center">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdChemists" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowDataBound="grdChemist_RowDataBound"
                                                    GridLines="None" CssClass="table" PagerStyle-CssClass="GridView1" style="background-color:white"
                                                    AlternatingRowStyle-CssClass="alt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chemists_Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Chemists_Code" runat="server" Text='<%#Eval("Chemists_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chemists Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="160px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Chemists_Name" runat="server" Height="40px" CssClass="label" Text='<%# Bind("Chemists_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chemists Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Chemists_Name" Width="150px" Height="40px" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" CssClass="input" MaxLength="150" Text='<%# Bind("Chemists_Name") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Chemists_Contact" runat="server" Height="40px" Width="150px" onkeypress="AlphaNumeric_NoSpecialChars(event);" CssClass="input" MaxLength="150" Text='<%# Bind("Chemists_Contact") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="150px" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Chemists_Address1" runat="server" onkeypress="AlphaNumeric(event);" CssClass="input" Height="40px" Width="150px" MaxLength="150" Text='<%# Bind("Chemists_Address1") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Phone" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Chemists_Phone" runat="server" onkeypress="CheckNumeric(event);" Height="40px" CssClass="input" MaxLength="150" Text='<%# Bind("Chemists_Phone") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fax" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Chemists_Fax" runat="server" Height="40px" CssClass="input" MaxLength="150" Text='<%# Bind("Chemists_Fax") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EMail ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Chemists_EMail" runat="server" Height="40px" CssClass="input" MaxLength="150" Text='<%# Bind("Chemists_EMail") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Chemists_Mobile" onkeypress="CheckNumeric(event);" runat="server" Height="40px" CssClass="input" MaxLength="150" Text='<%# Bind("Chemists_Mobile") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="Territory_Code" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chemist ERP Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Chemist_ERP_Code" runat="server" Height="40px" CssClass="input" MaxLength="150" Text='<%# Bind("Chemist_ERP_Code") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="Cat_Code" runat="server" SkinID="ddlRequired" DataSource="<%# Fillchemcat() %>" DataTextField="Chem_Cat_SName" DataValueField="Cat_Code">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnUpdate" CssClass="savebutton" runat="server" Width="70px" Text="Update" Visible="false"
                                                    OnClick="btnUpdate_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="Button1" runat="server" CssClass="backbutton" Text="Back" OnClick="buttonBack_Click" />
                </div>
            </div>
        </div>
        <div class="div_fixed">
            <asp:Button ID="btnSave" runat="server" Text="Update" Width="70px" CssClass="savebutton" Visible="false"
                OnClick="btnSave_Click" />
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
