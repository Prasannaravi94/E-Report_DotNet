<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEditDocCat.aspx.cs" Inherits="MasterFiles_BulkEditDocCat" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit - Doctor Category</title>
       <style type="text/css">
            .modal
    {
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
    .loading
    {
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
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <br />
                        <h2 class="text-center">Bulk Edit - Doctor Category</h2>
                        <div class="display-table clearfix">
                            <div class="table-responsive">
                                <table width="100%" align="center">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" align="center">

                                                <asp:GridView ID="grdDocCat" runat="server" Width="60%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                                                AutoGenerateColumns="false" GridLines="None" CssClass="table" OnRowDataBound="grdDocCat_RowDataBound">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocCatCode" runat="server" Text='<%#Eval("Doc_Cat_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDoc_Cat_SName" CssClass="input" Width="80px" MaxLength="12" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDocCatName" Width="120px" MaxLength="120" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No of visit" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                            <asp:TextBox ID="txtDocVisit"  CssClass="input" Width="80px" onkeypress="CheckNumeric(event);"  runat="server" MaxLength="3" Text='<%# Bind("No_of_visit") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAnalysisNd" runat="server" Text='<%#Eval("Analysis_needed")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Analysis Needed" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkAnalysisNd" runat="server" Text="." />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <br />
                            <center>
                                <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="savebutton"
                                    OnClick="btnSubmit_Click"/>
                            </center>
                        </div>
                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
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
