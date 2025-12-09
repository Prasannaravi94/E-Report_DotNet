<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FVAddExpense_Parameter.aspx.cs"
    Inherits="MasterFiles_FVAddExpense_Parameter" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fixed / Variable Expense</title>
    <%-- <link type="text/css" rel="Stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link href="../assets/css/Calender_CheckBox.css" rel="stylesheet" />

    <style type="text/css">
      .single-des [type="checkbox"]:not(:checked) + label, .single-des [type="checkbox"]:checked + label {
            padding-left: 1.25em;
        }
    </style>


</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSave').click(function () {
                if ($("#txtParameter").val() == "") {
                    alert("Enter Parameter Name.");
                    $('#txtParameter').focus();
                    return false;
                }
                if ($("#txtFixedAmount").val() == "") {
                    alert("Enter Fixed amount.");
                    $('#txtFixedAmount').focus();
                    return false;
                }
                var multiple = $('#<%=ddlExpenseType.ClientID%> :selected').text();
                if (multiple == "--Select--") { alert("Select Expense Type."); $('#ddlExpenseType').focus(); return false; }
                var month = $('#<%=ddlLevel.ClientID%> :selected').text();
                if (month == "--Select--") { alert("Select Level."); $('#ddlLevel').focus(); return false; }

            });
        });
    </script>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center">Fixed / Variable Expense</h2>

                         <%--     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblParameter" runat="server" CssClass="label">Parameter<span style="Color:Red;padding-left:5px">*</span></asp:Label>
                                <asp:TextBox ID="txtParameter" runat="server" CssClass="input" Width="100%"
                                    TabIndex="1" MaxLength="50">
                                </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblExpenseType" runat="server" CssClass="label">Expense Type<span style="Color:Red;padding-left:5px">*</span></asp:Label>
                                <asp:DropDownList ID="ddlExpenseType" runat="server" AutoPostBack="true"
                                    CssClass="nice-select" OnSelectedIndexChanged="OnSelectedIndex_ddlExpenseType">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Fixed" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Variable" Value="V"></asp:ListItem>
                                    <asp:ListItem Text="Variable with Limit" Value="L"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblFixedAmount" runat="server" CssClass="label">Fixed Amount<span style="Color:Red;padding-left:5px">*</span></asp:Label>
                                <asp:TextBox ID="txtFixedAmount" runat="server" CssClass="input" Width="100%"
                                    TabIndex="1" MaxLength="50">
                                </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="labelbase" runat="server" CssClass="label">Base Level<span style="Color:Red;padding-left:5px">*</span></asp:Label>
                                <div class="row">
                                    <div class="col-lg-2" style="padding-top:10px">
                                        <asp:CheckBox ID="chkbase" runat="server" Text="." />
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtbse" runat="server" size="3" CssClass="input"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblmgr" runat="server" CssClass="label">Line Managers<span style="Color:Red;padding-left:5px">*</span></asp:Label>
                                <div class="row">
                                    <div class="col-lg-2" style="padding-top:10px">
                                        <asp:CheckBox ID="chkmgr" runat="server" Text="." />
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtmgr" runat="server" size="3" CssClass="input" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblLevel" Visible="false" runat="server" CssClass="label">Level<span style="Color:Red;padding-left:5px">*</span></asp:Label>
                                <asp:DropDownList ID="ddlLevel" Visible="false" runat="server" CssClass="nice-select">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Base Level" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Line Managers" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
   <%--      </ContentTemplate>
                        </asp:UpdatePanel>--%>

                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
                    </div>
                </div>
                <br />
                <br />
                <%--<div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>--%>
            </div>
    </form>
</body>
</html>
