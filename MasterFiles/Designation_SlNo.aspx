<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Designation_SlNo.aspx.cs"
    Inherits="MasterFiles_Designation_SlNo" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Designation Serial No Generation</title>
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
                  
                    <div class="col-lg-9">
                          <br />
                  
                        <h2 class="text-center">Designation Serial No Generation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading text-center clearfix">
                                <div class="d-inline-block division-name">Division Name</div>
                                <div class="d-inline-block align-middle">
                                    <div class="single-des-option">
                                        <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="nice-select"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <p>
                                <br />
                            </p>
                            <h2 class="text-center">Base Level</h2>
                            <div class="display-table clearfix" align="center">
                                <div class="table-responsive overflow-x-none" align="center">

                                    <asp:GridView ID="grdBaselevel" runat="server" OnRowDataBound="grdBaselevel_RowDataBound"
                                        AutoGenerateColumns="false" AllowSorting="True"
                                        GridLines="None" CssClass="table">
                                        <%-- <HeaderStyle Font-Bold="False" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>--%>
                                        <Columns>
                                            <%-- <asp:BoundField DataField="Doc_ClsCode" ShowHeader="true" HeaderText="Class Code"  ItemStyle-Width="7%"  Visible="false"/>--%>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesign_code" runat="server" Text='<%#Bind("Designation_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Designation_Short_Name"
                                                ShowHeader="true" HeaderText="Short Name" />

                                            <asp:BoundField DataField="Designation_Name" ShowHeader="true"
                                                HeaderText="Designation Name" />

                                            <asp:TemplateField HeaderText="Existing S.No" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New S.No" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBaseSlNo" onkeypress="CheckNumeric(event);" runat="server" MaxLength="2"
                                                        CssClass="input"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Analysis Needed" ItemStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                   <asp:hiddenfield ID="HdnTag_Needed"  runat="server" Value='<%# Bind("Tag_Needed") %>'></asp:hiddenfield>
                                               <asp:CheckBox ID="ChkTagged" runat="server" Width="100px" Text="."/>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        </Columns>


                                    </asp:GridView>
                                    <br />
                                    <div class="no-result-area" id="divid" runat="server" visible="false">
                                        No Records Found
                                    </div>


                                </div>
                                <asp:Button ID="btnbase" runat="server" Text="Update"
                                    OnClick="btnbase_Click" CssClass="savebutton" />
                            </div>
                            <p>
                                <br />
                            </p>
                            <h2 class="text-center">Managers</h2>
                            <div class="display-table clearfix" align="center">
                                <div class="table-responsive overflow-x-none">

                                    <asp:GridView ID="grdmanager" runat="server" OnRowDataBound="grdmanager_RowDataBound"
                                        AutoGenerateColumns="false" AllowSorting="True"
                                        GridLines="None" CssClass="table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="Doc_ClsCode" ShowHeader="true" HeaderText="Class Code"  ItemStyle-Width="7%"  Visible="false"/>--%>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Des_Code" runat="server" Text='<%#Bind("Designation_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Designation_Short_Name"
                                                ShowHeader="true" HeaderText="Short Name" />
                                            <asp:BoundField DataField="Designation_Name"
                                                ShowHeader="true" HeaderText="Designation Name" />
                                            <asp:TemplateField HeaderText="Existing S.No" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New S.No" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtManSlNo" onkeypress="CheckNumeric(event);" runat="server" MaxLength="2"
                                                        CssClass="input"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Analysis Needed" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                   <asp:hiddenfield ID="HdnTag_Needed"  runat="server" Value='<%# Bind("Tag_Needed") %>'></asp:hiddenfield>
                                               <asp:CheckBox ID="ChkTagged" runat="server" Width="100px" Text="."/>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        </Columns>
                                        <EmptyDataRowStyle />
                                    </asp:GridView>
                                    <center>
                                        <br />
                                        <div class="no-result-area" id="divid1" runat="server" visible="false">
                                            No Records Found
                                        </div>

                                    </center>
                                </div>
                                <asp:Button ID="btnManager" runat="server" Text="Update"
                                    OnClick="btnManager_Click" CssClass="savebutton" />
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnback" runat="server" CssClass="backbutton" Text="Back"
                        OnClick="btnback_Click" />
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
