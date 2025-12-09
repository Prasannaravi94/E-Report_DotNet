<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChemistList.aspx.cs" Inherits="MasterFiles_MR_Chemist_ChemistList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chemist Details</title>
    <%-- <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
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

        .align {
            min-width: 150px;
        }

        #dlAlpha tr td {
            padding-right: 1px;
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
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#Btnsrc').click(function () {

                var divi = $('#<%=ddSrch.ClientID%> :selected').text();
                var divi1 = $('#<%=ddSrc2.ClientID%> :selected').text();
                if (divi1 == "---Select---") { alert("Select " + divi); $('#ddSrc2').focus(); return false; }
                if ($("#txtsearch").val() == "") { alert("Chemists  Name."); $('#txtsearch').focus(); return false; }


            });
        });
    </script>
    <%--<link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet"/>--%>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Chemist Details</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <div class="row clearfix">
                                        <table width="90%">
                                            <tr>
                                                <td align="right" colspan="3">
                                                    <%--     <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server" 
                                                 onclick="btnBack_Click" />--%>
                                                    <div style="margin-left: 90%">
                                                        <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 8.2%"></td>
                                                <%-- <td>
                                        <asp:Panel ID="pnlAdmin" runat="server">
                                            <asp:Label ID="lblSalesforce" runat="server" Text="Field Force Name"></asp:Label>
                                              <asp:DropDownList ID="Alpha" runat="server" AutoPostBack="true" SkinID="ddlRequired" OnSelectedIndexChanged="Alpha_SelectedIndexChanged">     
                                         <asp:ListItem Selected="true">---ALL---</asp:ListItem>
                                            <asp:ListItem >A</asp:ListItem>
                                            <asp:ListItem >B</asp:ListItem>
                                            <asp:ListItem >C</asp:ListItem>    
                                            <asp:ListItem >D</asp:ListItem>
                                            <asp:ListItem >E</asp:ListItem>
                                            <asp:ListItem >F</asp:ListItem>  
                                            <asp:ListItem >G</asp:ListItem>
                                            <asp:ListItem >H</asp:ListItem>
                                            <asp:ListItem >I</asp:ListItem>    
                                            <asp:ListItem >J</asp:ListItem>
                                            <asp:ListItem >K</asp:ListItem>
                                            <asp:ListItem >L</asp:ListItem> 
                                            <asp:ListItem >M</asp:ListItem>
                                            <asp:ListItem >N</asp:ListItem>
                                            <asp:ListItem >O</asp:ListItem>    
                                            <asp:ListItem >P</asp:ListItem>
                                            <asp:ListItem >Q</asp:ListItem>
                                            <asp:ListItem >R</asp:ListItem>  
                                            <asp:ListItem >S</asp:ListItem>
                                            <asp:ListItem >T</asp:ListItem>
                                            <asp:ListItem >U</asp:ListItem>    
                                            <asp:ListItem >V</asp:ListItem>
                                            <asp:ListItem >W</asp:ListItem>
                                            <asp:ListItem >X</asp:ListItem>     
                                            <asp:ListItem >Y</asp:ListItem>
                                            <asp:ListItem >Z</asp:ListItem>   
                                        </asp:DropDownList>
                                            <asp:DropDownList ID="ddlSFCode" runat="server" SkinID="ddlRequired">
                                            </asp:DropDownList>--%>


                                                <%--  </asp:Panel>
                                        </td>
                                        <td align="right" width="30%">
                                            <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>
                                        </td>--%>
                                            </tr>
                                        </table>
                                    </div>
                                    <asp:Panel ID="pnlAdmin" runat="server">
                                        <div class="row clearfix">
                                            <div class="col-lg-2" style="margin-right: -52px;">
                                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="ddlSFCode" data-live-search="true"
                                                    class="selectpicker" runat="server" CssClass="custom-select2 nice-select">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-1" style="margin-left: -25px;">
                                                <asp:Button ID="Button1" runat="server" Width="40px" Text="Go" CssClass="savebutton" OnClick="btnSubmit_Click" />
                                                <span id="SPl"
                                                    style="font-family: Verdana; color: Red; font-weight: bold; display: none; width: 200px">Please Wait....</span>
                                                <asp:HiddenField ID="hdnBasedOn" runat="server" />
                                                <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name" Visible="false" CssClass="label"></asp:Label>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <br />
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnQAdd" runat="server" ForeColor="White" CssClass="savebutton" Text="Add Chemist" Width="100px"
                                            OnClick="btnQAdd_Click" />
                                        <asp:Button ID="btnEdit" runat="server" CssClass="resetbutton" Text="Edit All Chemist" Width="120px"
                                            OnClick="btnEdit_Click" />
                                        <asp:Button ID="btnDeAc" runat="server" CssClass="resetbutton" Width="140px"
                                            Text="Deactivate Chemist" OnClick="btnDeAc_Click" />
                                        <asp:Button ID="btnReAc" runat="server" CssClass="resetbutton" Width="140px"
                                            Text="Reactivate Chemist" OnClick="btnReAc_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblType" runat="server" CssClass="label" Text="Search By"></asp:Label>
                                        <asp:DropDownList ID="ddSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddSrch_OnSelectedIndexChanged">
                                            <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Chemists Name" Value="2"></asp:ListItem>
                                            <%--<asp:ListItem Text ="Territory" Value ="3"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2" style="padding-top: 19px; padding-left: 0px;">
                                        <div class="single-des clearfix">
                                            <asp:TextBox ID="txtsearch" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px">
                                            <asp:DropDownList ID="ddSrc2" runat="server" Visible="false"
                                                CssClass="nice-select" TabIndex="4"
                                                OnSelectedIndexChanged="ddSrc2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="margin: 17px; margin-left: -25px;">
                                        <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="40px"
                                            Text="Go" OnClick="Btnsrc_Click" Visible="false" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row" style="scrollbar-width: thin; overflow-x: auto">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <%-- <td style="width: 50%" />--%>
                                            <td colspan="2" align="center">
                                                <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand" runat="server">
                                                    <SeparatorTemplate></SeparatorTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnAlpha" Font-Size="15px" runat="server"
                                                            CommandArgument='<%#bind("Chemists_Name") %>' Text='<%#bind("Chemists_Name") %>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <table width="100%" align="center">
                                        <tbody>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:GridView ID="grdChemist" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10"
                                                        OnRowUpdating="grdChemist_RowUpdating" OnRowEditing="grdChemist_RowEditing"
                                                        OnRowCancelingEdit="grdChemist_RowCancelingEdit" OnRowDataBound="grdChemist_RowDataBound"
                                                        OnRowCommand="grdChemist_RowCommand" OnPageIndexChanging="grdChemist_PageIndexChanging"
                                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                                        AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdChemist_Sorting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdChemist.PageIndex * grdChemist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Chemists Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Chemists_Code" runat="server" Text='<%#Eval("Chemists_Code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="Chemists_Name" HeaderText="Chemists Name" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblChemName" runat="server" Text='<%#Eval("Chemists_Name")%>'></asp:Label>

                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtChemName" runat="server" Width="200px" CssClass="input" Text='<%#Eval("Chemists_Name")%>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvChem" runat="server" ControlToValidate="txtChemName" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="Chemists_Contact" HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContact" runat="server" Text='<%#Eval("Chemists_Contact")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtContact" runat="server" Width="160px" CssClass="input"
                                                                        Text='<%#Eval("Chemists_Contact")%>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="territory_Name" HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblterr" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlterr" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                                    </asp:DropDownList>

                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlterr" ID="RequiredFieldValidator6"
                                                                        ErrorMessage="*Required" InitialValue="0" runat="server"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>

                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-CssClass="align" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="90px"
                                                                ShowEditButton="True"></asp:CommandField>
                                                            <asp:HyperLinkField HeaderText="View" Text="View" DataNavigateUrlFormatString="Chemists_View.aspx?Chemists_Code={0}"
                                                                DataNavigateUrlFields="Chemists_Code" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
                                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Chemists_Code") %>'
                                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Chemists');">Deactivate
                                                                    </asp:LinkButton>
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
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
        <%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>--%>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
        <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/js/bootstrap-select.min.js"></script>
    </form>
</body>
</html>
