<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEditProd_Group.cs"
    Inherits="MasterFiles_BulkEditProd_Group" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bulk Edit - Product Group</title>
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

            var f = document.getElementById("grdProGrp");
            if (f != null) {
                var TargetChildPrdCode = "txtProduct_Grp_SName";
                var TargetChildPrdName = "txtProGrpName";

                var Inputs = f.getElementsByTagName("input");
                for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                    if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildPrdCode, 0) >= 0) {
                        if (Inputs[i].value == "") {
                            alert("Enter Short Name");
                            f.getElementsByTagName("input").item(i).focus();
                            return false;
                        }
                    }

                    if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildPrdName, 0) >= 0) {
                        if (Inputs[i].value == "") {
                            alert("Enter Group Name");
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
                        <h2 class="text-center">Bulk Edit - Product Group</h2>
                        <div class="display-table clearfix">
                            <div class="table-responsive">

                                <asp:GridView ID="grdProGrp" runat="server" Width="60%" HorizontalAlign="Center"
                                    AutoGenerateColumns="false" GridLines="None" EmptyDataText="No Records Found"
                                    CssClass="table" >
                                  
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">  
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProGrpCode" runat="server" Text='<%#Eval("Product_Grp_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtProduct_Grp_SName" MaxLength="6" Width="60px" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" CssClass="input" Text='<%# Bind("Product_Grp_SName") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group Name" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtProGrpName" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" MaxLength="100" Text='<%# Bind("Product_Grp_Name") %>'></asp:TextBox>
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
