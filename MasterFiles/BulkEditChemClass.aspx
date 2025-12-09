<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEditChemClass.aspx.cs" Inherits="MasterFiles_BulkEditChemCat" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit - Chemists Class</title>
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
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <h2 class="text-center" id="heading" runat="server"></h2>
                        <br />
                        <div class="display-table clearfix">
                            <div class="table-responsive">
                                <asp:GridView ID="grdChemClass" runat="server" Width="50%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="false" GridLines="None" CssClass="table" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt">
                                  
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChemClassCode" runat="server" Text='<%#Eval("Class_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtChem_Class_SName" MaxLength="12" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" CssClass="input" Text='<%# Bind("Chem_Class_SName") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class Name" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtChemClassName" CssClass="input" Width="200px" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" MaxLength="100" Text='<%# Bind("Chem_Class_Name") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                        <center>
                            <asp:Button ID="btnSubmit" runat="server"  Text="Update" CssClass="savebutton"
                                OnClick="btnSubmit_Click" />
                        </center>

                    </div>
                </div>
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />
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
