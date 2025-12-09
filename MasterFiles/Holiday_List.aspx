<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Holiday_List.aspx.cs" Inherits="MasterFiles_Holiday_List" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday List</title>
   <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
            <br />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <h2 class="text-center">Holiday List</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-7">

                                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Width="55px" Text="Add" OnClick="btnNew_Click" />
                                        &nbsp;
                                         <asp:Button ID="btnSlNO" runat="server" CssClass="resetbutton" Text="Sl.No Gen" OnClick="btnSlNO_Click" />

                                        <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand" runat="server" Width="70%" HorizontalAlign="Center">
                                            <SeparatorTemplate></SeparatorTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnAlpha" runat="server" Font-Names="Calibri" Font-Size="14px" ForeColor="#336277" CommandArgument='<%#Bind("Holiday_Name") %>' Text='<%#Bind("Holiday_Name") %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:DataList>

                                    </div>
                                    <div class="col-lg-5 text-right">
                                        <div class="d-inline-block division-name">Division Name</div>
                                        <div class="d-inline-block align-middle">
                                            <div class="single-des-option">
                                               <%-- <asp:Label ID="Lbldivi" runat="server" SkinID="lblMand">Division Name</asp:Label>--%>
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    
                                         <asp:GridView ID="grdHoliday" runat="server" 
                            AllowPaging="true"
                            OnRowUpdating="grdHoliday_RowUpdating" OnRowEditing="grdHoliday_RowEditing"
                            OnRowCommand="grdHoliday_RowCommand" PageSize="40" 
                            OnPageIndexChanging="grdHoliday_PageIndexChanging" OnRowCreated="grdHoliday_RowCreated"
                            OnRowCancelingEdit="grdHoliday_RowCancelingEdit" AllowSorting="True"
                            AutoGenerateColumns="false" GridLines="None" CssClass="table"
                            PagerStyle-CssClass="gridview1" OnRowDataBound="grdHoliday_RowDataBound">
                                           
                            <Columns>
                                <asp:TemplateField HeaderText="#" >                                 
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdHoliday.PageIndex * grdHoliday.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Holiday_Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHoliday_Id" runat="server" Text='<%#Bind("Holiday_Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Holiday Name" >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtHolidayName" CssClass="input"  Width="300px" runat="server" MaxLength="70" onkeypress="CharactersOnly(event);" Text='<%# Bind("Holiday_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblHolidayName" runat="server" Text='<%# Bind("Holiday_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month" >
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                        <asp:HiddenField ID="lblMnthId" runat="server" Value='<%# Bind("Month") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--   <asp:TemplateField HeaderText="Fixed Date" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("Fixed_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit"  HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ItemStyle-HorizontalAlign="Center"
                                    ShowEditButton="True">
                                   <%-- <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue"
                                        HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ItemStyle>--%>
                                </asp:CommandField>
                                <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="HolidayCreation.aspx?Holiday_Id={0}" ItemStyle-HorizontalAlign="Center"
                                    DataNavigateUrlFields="Holiday_Id">
                                    <%--<ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Bold="False"></ItemStyle>--%>
                                </asp:HyperLinkField>

                                <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                   <%-- <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Bold="False"></ItemStyle>--%>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Holiday_Id") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Holiday');">Deactivate
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

                   <div class="no-result-area" id="divid" runat="server" visible="false" >
                        No Records Found
                    </div>

            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
