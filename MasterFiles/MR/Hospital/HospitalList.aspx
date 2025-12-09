<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HospitalList.aspx.cs" Inherits="MasterFiles_MR_Hospital_HospitalList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hospital Details</title>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" /> --%>
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
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
            min-width: 120px;
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
                if (divi1 == "---Select---") { alert(divi + " Name."); $('#ddSrc2').focus(); return false; }
                if ($("#txtsearch").val() == "") { alert("Hospital  Name."); $('#txtsearch').focus(); return false; }


            });
        });
    </script>
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
                        <h2 class="text-center">Hospital Details</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <div class="row clearfix">
                                        <div class="col-lg-6">
                                            <asp:Panel ID="pnlAdmin" runat="server">

                                                <%--     <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server" 
                                                   onclick="btnBack_Click" />--%>
                                                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />
                                                <div style="float: left; width: 90%">
                                                    <asp:Label ID="lblSalesforce" runat="server" CssClass="label" Text="Field Force Name"></asp:Label>
                                                    <asp:DropDownList ID="ddlSFCode" runat="server" CssClass="custom-select2 nice-select" Width="100%"></asp:DropDownList>
                                                </div>
                                                <div style="float: right; width: 10%;padding-top:22px">
                                                    <asp:Button ID="btnGo" runat="server" Width="45px" Text="Go" CssClass="savebutton" OnClick="btnSubmit_Click" />
                                                </div>
                                            </asp:Panel>
                                        </div>
                                       
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-10">
                                        <asp:Button ID="btnQAdd" runat="server" CssClass="savebutton" Text="Add Hospital" OnClick="btnQAdd_Click" Width="100px" />
                                        <asp:Button ID="btnEdit" runat="server" CssClass="resetbutton" Text="Edit All Hospital" OnClick="btnEdit_Click" Width="120px" />
                                        <asp:Button ID="btnDeAc" runat="server" CssClass="resetbutton" Text="Deactivate Hospital" Width="140px"
                                            OnClick="btnDeAc_Click" />
                                        <asp:Button ID="btnReAc" runat="server" CssClass="resetbutton" Text="Reactivate Hospital" Width="140px" OnClick="btnReAc_Click" />
                                        <asp:Button ID="btnHosListedDrMap" runat="server" CssClass="resetbutton" Text="Hospital-ListedDr Map" Width="160px"
                                            OnClick="btnHosListedDrMap_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblType" runat="server" CssClass="label" Text="Search By"></asp:Label>
                                        <asp:DropDownList ID="ddSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddSrch_OnSelectedIndexChanged">
                                            <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Hospital Name" Value="2"></asp:ListItem>
                                            <%--<asp:ListItem Text ="Territory" Value ="3"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2" style="padding-top: 19px; padding-left: 0px;">
                                        <div class="single-des clearfix">
                                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input"
                                                Visible="false" MaxLength="50" Width="100%"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px">
                                            <asp:DropDownList ID="ddSrc2" runat="server" Width="20%" Visible="false"
                                                CssClass="nice-select" TabIndex="4" OnSelectedIndexChanged="ddSrc2_OnSelectedIndexChanged">
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
                                            <%--<td style="width: 30%" />--%>
                                            <td colspan="2" align="center">
                                                <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                                    runat="server" HorizontalAlign="Center">
                                                    <SeparatorTemplate>
                                                    </SeparatorTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnAlpha" Font-Size="15px" runat="server" CommandArgument='<%#bind("Hospital_Name") %>'
                                                            Text='<%#bind("Hospital_Name") %>'>
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
                                                    <asp:GridView ID="grdHospital" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnPageIndexChanging="grdHospital_PageIndexChanging"
                                                        OnRowCreated="grdHospital_RowCreated"
                                                        OnRowUpdating="grdHospital_RowUpdating" OnRowEditing="grdHospital_RowEditing"
                                                        OnRowCancelingEdit="grdHospital_RowCancelingEdit" OnRowDataBound="grdHospital_RowDataBound"
                                                        OnRowCommand="grdHospital_RowCommand" GridLines="None" CssClass="table" PagerStyle-CssClass="GridView1"
                                                        AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdHospital_Sorting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdHospital.PageIndex * grdHospital.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Hospital Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Hospital_Code" runat="server" Text='<%#Eval("Hospital_Code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <%--  <EditItemTemplate>
                                        <asp:TextBox ID="txtHospitalCode" runat="server" Text='<%#Eval("Hospital_Code")%>'></asp:TextBox>
                                    </EditItemTemplate>--%>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="Hospital_Name" HeaderText="Hospital Name" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHospitalName" runat="server" Text='<%#Eval("Hospital_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtHospitalName" runat="server" Width="200px" CssClass="input" Text='<%#Eval("Hospital_Name")%>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvHos" runat="server" ControlToValidate="txtHospitalName" ErrorMessage="*Required"></asp:RequiredFieldValidator>

                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="Hospital_Contact" HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContact" runat="server" Text='<%#Eval("Hospital_Contact")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtContact" runat="server" Width="170px" CssClass="input" Text='<%#Eval("Hospital_Contact")%>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="territory_Name" HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlterr" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ControlToValidate="ddlterr" ID="RequiredFieldValidator6"
                                                                        ErrorMessage="Select Territory" InitialValue="0" runat="server"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="90px"
                                                                ShowEditButton="True" ItemStyle-CssClass="align"></asp:CommandField>
                                                            <asp:HyperLinkField HeaderText="View" Text="View" DataNavigateUrlFormatString="Hospital_View.aspx?Hospital_Code={0}"
                                                                DataNavigateUrlFields="Hospital_Code" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
                                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Hospital_Code") %>'
                                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Hospital');">Deactivate
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
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    </form>
</body>
</html>
