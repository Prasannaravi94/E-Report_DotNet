<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoolName_List.aspx.cs" Inherits="MasterFiles_PoolName_List" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stockist - HQ List</title>
    <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
        .alignment
        {
            min-width:130px;
        }
    </style>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
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

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center" style="border-bottom: none">Stockist - HQ List</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-12">
                                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Width="60px"
                                            Text="Add" OnClick="btnNew_Click" />
                                    </div>
                                </div>
                                <br />

                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <%-- 
                                           Search :
                                            <asp:TextBox ID="txtSearch" runat="server" Font-Size="12px" 
                                            onkeyup="Search_Gridview(this, 'grdStockist')"></asp:TextBox><br />
                                        --%>
                                        <asp:Label ID="lbl" runat="server" Text="Search By" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <%--  <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>    --%>
                                            <asp:ListItem Text="State Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="HQ Name" Value="3" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                        <img src="../Images/ajaxRoundLoader.gif" style="display: none;" id="loaderSearch" />
                                        <%-- <img src="../Images/ajax_loadinf_3.gif"  style="display: none;" id="loaderSearch" />--%>
                                    </div>

                                    <div class="col-lg-3">
                                        <div class="single-des clearfix" style="padding-top: 19px;">
                                            <asp:TextBox ID="TxtSrch" runat="server" CssClass="input" Width="100%"
                                                Visible="false"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px;">
                                            <asp:DropDownList ID="ddlSt" runat="server" CssClass="nice-select" TabIndex="4" Visible="false"
                                                onfocus="this.style.backgroundColor='#E0EE9D'">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">

                                        <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="50px"
                                            Text="Go" Visible="false" OnClick="Btnsrc_Click" />
                                    </div>


                                </div>

                            </div>

                            <br />
                            <br />

                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="grdPoolName" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowCancelingEdit="grdPoolName_RowCancelingEdit"
                                        OnRowEditing="grdPoolName_RowEditing" GridLines="None" CssClass="table" 
                                        AllowSorting="True" OnRowUpdating="grdPoolName_RowUpdating" OnPageIndexChanging="grdPoolName_PageIndexChanging"
                                        PagerStyle-CssClass="gridview1" AllowPaging="True" PageSize="10" OnRowCommand="grdPoolName_RowCommand"
                                        OnRowDataBound="grdPoolName_RowDataBound">
                                      
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdPoolName.PageIndex * grdPoolName.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pool Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPoolCode" runat="server" Text='<%#Eval("Pool_Id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Short Name"  ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <%-- <asp:TextBox ID="txtShortName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="20" onkeypress="CharactersOnly(event);"
                                            Text='<%# Bind("subdivision_sname") %>'></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtShortName" CssClass="input" runat="server" MaxLength="10" Height="38px"
                                                        Text='<%# Bind("Pool_SName") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Pool_SName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPoolName" CssClass="input" runat="server" MaxLength="100" Height="38px"
                                                        onkeypress="CharactersOnly(event);" Text='<%# Bind("Pool_Name") %>'></asp:TextBox>

                                                    <asp:HiddenField ID="hdnPoolName" runat="server" Value='<%# Bind("Pool_Name") %>' />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPoolName" runat="server" Text='<%# Bind("Pool_Name") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State">
                                                <EditItemTemplate>
                                                    <%-- <asp:TextBox ID="txtState" runat="server" SkinID="TxtBxNumOnly" MaxLength="10" Text='<%# Bind("State") %>'></asp:TextBox>--%>
                                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>' Visible="false" />
                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="nice-select">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="revDdlState" runat="server" ControlToValidate="ddlState" Display="Dynamic"
                                                        InitialValue="0" ErrorMessage="Please Select State"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Count" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Pool_Count") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-CssClass="alignment"
                                                HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True" ItemStyle-HorizontalAlign="Center">                                              
                                            </asp:CommandField>
                                            <%--   <asp:HyperLinkField HeaderText="Edit" Text="Edit">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>--%>
                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-Width="120px" DataNavigateUrlFormatString="PoolName_Creation.aspx?Pool_Id={0}"
                                                DataNavigateUrlFields="Pool_Id" ItemStyle-HorizontalAlign="Center">                                              
                                            </asp:HyperLinkField>
                                            <%-- <asp:TemplateField HeaderText="Deactivate">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Pool_Id") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Pool Name');">Deactivate
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">                                              
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Pool_Id") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the HQ Name');">Deactivate
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../Images/deact2.png" alt="" width="75px" title="This Category Exists in Doctor" />
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
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
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
