<%@ Page Language="C#" AutoEventWireup="true" CodeFile="App_CallRemarks.aspx.cs" Inherits="MasterFiles_App_CallRemarks" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Call Remarks Creation</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .alignment {
            min-width: 100%;
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //  $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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
                            $('#btnaddnew').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnaddnew').click(function () {
                if ($("#txtaddnew").val() == "") { alert("Enter Remarks."); $('#txtaddnew').focus(); return false; }

            });
        });
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-8">

                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:GridView ID="grdRemarks" runat="server" Width="100%" HorizontalAlign="Center" OnRowCreated="grdRemarks_RowCreated"
                                    AutoGenerateColumns="false" GridLines="None" OnPageIndexChanging="grdRemarks_PageIndexChanging" ShowFooter="true"
                                    OnRowCommand="grdRemarks_RowCommand" CssClass="table" PagerStyle-CssClass="gridview1" OnSelectedIndexChanging="grdRemarks_SelectedIndexChanging">

                                    <Columns>

                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%# (grdRemarks.PageIndex * grdRemarks.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="More" HeaderStyle-Width="40px">
                                            <FooterTemplate>
                                                <asp:Button ID="btnadd" runat="server" CssClass="savebutton" Width="110px" CausesValidation="true" CommandName="Select" Text="Add Remarks" OnClientClick="GetGridFooterRowvalues()" />
                                                <%--<asp:LinkButton ID="LkB1" runat="server" CommandName="Select">Add Folder</asp:LinkButton>--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks_Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremarks_id" runat="server" Text='<%#Eval("Remarks_Id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="50%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtremarks_name" onkeypress="AlphaNumeric_NoSpecialCharshq(event);" runat="server" CssClass="input alignment" Height="35px" Text='<%# Bind("Remarks_Content") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_Name" runat="server" CssClass="input alignment" onkeypress="AlphaNumeric_NoSpecialCharshq(event);" Height="35px" align="left"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete"  HeaderStyle-Width="120px">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Remarks_Id") %>'
                                                    CommandName="Deactivate" OnClientClick="return confirm('Do you want to Delete Call Remarks');">Delete
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                        <center>
                            <asp:Button ID="btnUpdate" runat="server" CssClass="savebutton" Width="70px" Height="25px" Text="Save" OnClick="btnUpdate_Click" />
                        </center>

                        <div class="row justify-content-center">
                            <div class="col-lg-3" align="center">
                                <asp:Button ID="btnaddnew" runat="server" CssClass="savebutton" Width="110px" Text="Add Remarks" OnClick="btnaddnew_Click" />
                            </div>
                            <div class="col-lg-6" align="center">
                                <div class="single-des clearfix">
                                    <asp:TextBox ID="txtaddnew" runat="server" CssClass="input" Width="100%" onkeypress="AlphaNumeric_NoSpecialCharshq(event);"></asp:TextBox>
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
