<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_BulkEdit.aspx.cs" Inherits="MasterFiles_MR_Territory_BulkEdit" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Territory - Bulk Edit</title>

    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>

    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
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
        /*.marRight
        {
            margin-right:35px;
        }*/
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
    <script language="javascript" type="text/javascript">
        function gvValidate() {

            var f = document.getElementById("grdTerritory");
            if (f != null) {
                var TargetChildTerrname = "txtTerritory_Name";



                var Inputs = f.getElementsByTagName("input");
                for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                    if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildTerrname, 0) >= 0) {
                        if (Inputs[i].value == "") {
                            alert("Enter Territory Name");
                            f.getElementsByTagName("input").item(i).focus();
                            return false;
                        }
                    }

                }

            }

        }


    </script>
    <script type="text/javascript">
    // Write the validation script.
    function isNumber(evt) {
        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
            return false;

        return true;
    }
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div></div>
        <div id="Divid" runat="server">
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <br />
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">Territory - Bulk Edit</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                <asp:Panel ID="pnlsf" runat="server" Style="text-align: center; font-size: 18px;" HorizontalAlign="Right">
                                    <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                                </asp:Panel>
                                <br />
                                <br />
                                <table align="center" width="100%">
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:GridView ID="grdTerritory" runat="server" Width="80%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                                AutoGenerateColumns="false" GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                                OnRowDataBound="grdTerritory_RowDataBound"
                                                AlternatingRowStyle-CssClass="alt">
                                                <HeaderStyle Font-Bold="False" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                        <%--<ControlStyle Width="90%"></ControlStyle>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Territory Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTerritory_Sname" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="TxtBxAllowSymb" Text='<%# Bind("Territory_Sname") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTerritory_Name" CssClass="input" Width="300px" Height="36px" onkeypress="AlphaNumeric_NoSpecialChars_New(event)" runat="server" MaxLength="80" Text='<%# Bind("Territory_Name") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="City Name" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAlias_Name" CssClass="input" Width="300px" Height="36px" onkeypress="AlphaNumeric_NoSpecialChars_New(event)" runat="server" MaxLength="80" Text='<%# Bind("Alias_Name") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Town/City" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>

                                                            <asp:DropDownList ID="City_Code" runat="server" CssClass="nice-select" DataSource="<%# FillCity() %>" DataTextField="Town_Name" DataValueField="Sl_No">
                                                            </asp:DropDownList>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Territory_Type" runat="server" CssClass="nice-select">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="HQ" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="EX" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="OS" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="OS-EX" Value="4"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Territory Visit" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Territory_Visit" runat="server" CssClass="nice-select">
                                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Territory Allowance" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTerritory_Allowance" CssClass="input" Width="300px" Height="36px" onkeypress='javascript: return isNumber(event)'  runat="server" MaxLength="8" Text='<%# Bind("Territory_Allowance") %>'>

                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <center>
                                    <asp:Button ID="btnSubmit" runat="server" Width="70px" Text="Update" CssClass="savebutton"
                                        OnClick="btnSubmit_Click" OnClientClick="return gvValidate()" />
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
        <%--<table width="90%" >
         
            <tr>
               <td align="right" width="30%">--%>
        <%-- <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
        <%-- </td>
                </tr>
                <tr>
                <td align="right">
                    <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server" 
                    onclick="btnBack_Click" />
                    </td>                    
            </tr> 
            </table> --%>
        <br />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
