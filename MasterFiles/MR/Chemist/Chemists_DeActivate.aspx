<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chemists_DeActivate.aspx.cs" Inherits="MasterFiles_MR_Chemist_Chemists_DeActivate" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chemist DeActivate</title>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" /> --%>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />--%>
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" type="text/css" />
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

        #grdChemists [type="checkbox"]:not(:checked) + label, #grdChemists [type="checkbox"]:checked + label {
            padding-left: 0.05em;
            color: white;
        }

        .display-table .table th:nth-child(2) {
            padding: 20px 20px;
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
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;
                    }
                    else {

                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server"></div>
            <%-- <ucl:Menu ID="menu1" runat="server" /> --%>

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <h2 class="text-center" style="border-style: none;">Chemist DeActivate</h2>
                        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" Style="text-align: center; font-size: 18px;">
                            <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                        </asp:Panel>
                        <table id="Table1" runat="server" width="90%">
                            <tr>
                                <td align="right" width="30%">
                                    <%--   <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server"
                                        OnClick="btnBack_Click" />
                                </td>
                            </tr>
                        </table>
                        <div class="row clearfix">
                            <div class="col-lg-3">
                                <asp:Label ID="lblType" runat="server" CssClass="label" Text="Search By"></asp:Label>
                                <asp:DropDownList ID="ddSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                    TabIndex="1" OnSelectedIndexChanged="ddSrch_OnSelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Chemists Name" Value="2"></asp:ListItem>
                                    <%--         <asp:ListItem Text ="Territory" Value ="3"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2" style="padding-top: 19px; padding-left: 0px;">
                                <div class="single-des clearfix">
                                    <asp:TextBox ID="txtsearch" runat="server" CssClass="input"
                                        Visible="false" MaxLength="50"></asp:TextBox>
                                </div>
                                <div style="margin-top: -20px">
                                    <asp:DropDownList ID="ddSrc2" runat="server" Visible="false"
                                        CssClass="nice-select" TabIndex="4"
                                        OnSelectedIndexChanged="ddSrc2_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-1" style="margin: 17px; margin-left: -25px;">
                                <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="40px" Height="25px"
                                    Text="Go" OnClick="Btnsrc_Click" Visible="false" />
                            </div>
                        </div>
                        <br />
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <table width="100%" align="center">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:GridView ID="grdChemists" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowDataBound="grdChemist_RowDataBound"
                                                    GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt"
                                                    AllowSorting="True" OnSorting="grdChemists_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" Text="." onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkChemists" runat="server" Text="." />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chemists Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChemCode" runat="server" Text='<%#Eval("Chemists_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Chemists_Name" HeaderText="Chemists Name" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChemName" runat="server" Text='<%#Eval("Chemists_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Chemists_Contact" HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChemContact" runat="server" Text='<%#Eval("Chemists_Contact")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- ---------------------------------------------%>
                                                        <asp:TemplateField SortExpression="Created_Date" HeaderText="Created Date" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChemcreated" runat="server" Text='<%#Eval("Created_Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  ---------------------------------------------%>
                                                        <asp:TemplateField SortExpression="territory_Name" HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="Button1" runat="server" CssClass="backbutton" Text="Back" OnClick="buttonBack_Click" />
                </div>
            </div>
            <br />
        </div>
        <div class="div_fixed">
            <asp:Button ID="btnSave" runat="server" Text="De-Activate" CssClass="savebutton" Visible="false"
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
