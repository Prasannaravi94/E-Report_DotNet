<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEditProdBrd.aspx.cs" Inherits="MasterFiles_BulkEditProdBrd" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit - Product Brand</title>

    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>

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

        .display-table .table .radio1 tr td {
            border: none;        
        }

        .display-table .table tr td .radio1 tr:first-child td:first-child {
            background-color: white;
            padding: 0px;
        }

        .display-table .table tr td .radio1 tr td {
            padding: 0px;
        }
          .display-table .table tr td:nth-child(4)
              {
              min-width:230px;
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
    <script language="javascript" type="text/javascript">
        function gvValidate() {

            var f = document.getElementById("grdProdBrd");
            if (f != null) {
                var TargetChildPrdCode = "txtProductBrdSName";
                var TargetChildPrdName = "txtProductBrdName";


                var Inputs = f.getElementsByTagName("input");
                for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                    if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildPrdCode, 0) >= 0) {
                        if (Inputs[i].value == "") {
                            alert("Enter the ShortName");
                            f.getElementsByTagName("input").item(i).focus();
                            return false;
                        }
                    }

                    if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildPrdName, 0) >= 0) {
                        if (Inputs[i].value == "") {
                            alert("Enter the Brand Name");
                            f.getElementsByTagName("input").item(i).focus();
                            return false;
                        }
                    }
                }

            }

        }


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
                        <h2 class="text-center">Bulk Edit - Product Brand</h2>
                        <div class="display-table clearfix">
                            <div class="table-responsive">

                                <asp:GridView ID="grdProdBrd" runat="server" Width="65%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="false" GridLines="None" CssClass="table" OnRowDataBound="grdProdBrd_RowDataBound">

                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductBrdCode" runat="server" Text='<%#Eval("Product_Brd_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtProductBrdSName" Width="80px" MaxLength="12" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" CssClass="input" Text='<%# Bind("Product_Brd_SName") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtProductBrdName" Width="120px" MaxLength="120" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" Text='<%# Bind("Product_Brd_Name") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" >
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="rdotype" runat="server" RepeatDirection="Horizontal"  CssClass="radio1">
                                                    <asp:ListItem Value="S">Sample &nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="R">Rx &nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="N" Selected="True">None</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />

                                </asp:GridView>

                            </div>
                            <br />
                            <center>
                                <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="savebutton" Visible="false"
                                    OnClick="btnSubmit_Click" OnClientClick="return gvValidate()" />
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
