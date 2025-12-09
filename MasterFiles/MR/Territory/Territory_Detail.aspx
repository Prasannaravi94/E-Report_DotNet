<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_Detail.aspx.cs" Inherits="MasterFiles_Territory_Detail" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Add</title>
    <%--      <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  
       <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />--%>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
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
            $('#btnSave').click(function () {
                if ($("#txtAlName").val() == "") { alert("Enter City."); $('#txtAlName').focus(); return false; }

                var Res = document.getElementById("rdoYes");
                if (Res.checked) {

                    if ($("#txtFare").val() == "") { alert("Enter Fare."); $('#txtFare').focus(); return false; }
                }
                var metro = $('#<%=ddlMetro.ClientID%> :selected').text();
                if (metro == "--Select--") { alert("Select Expense Category."); $('#ddlMetro').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="heading" runat="server"></h2>
                        <div class="row justify-content-center">
                            <div class="col-lg-6">
                                <asp:Label ID="lblN" runat="server" CssClass="label" Font-Size="14px" Text="Name:"></asp:Label>
                                <asp:Label ID="lblName" runat="server" CssClass="label" Font-Size="14px" ForeColor="Red"  Text='<%#Eval("Territory_Name")%>'></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:Label ID="lblty" runat="server" CssClass="label" Font-Size="14px" Text="Type:"></asp:Label>
                                <asp:Label ID="lblType" runat="server" CssClass="label" Font-Size="14px" ForeColor="Red"  Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                            </div>
                        </div>
                        <br />

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblSName" runat="server" Text="Short Name" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtSName" runat="server" MaxLength="6" CssClass="input" Width="100%" onkeypress="CharactersOnly(event);" Text='<%#Eval("Territory_SName")%>'></asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblAlName" runat="server" CssClass="label" Text="City Name"></asp:Label>
                                <asp:TextBox ID="txtAlName" runat="server" MaxLength="50" CssClass="input" Width="100%"  Text='<%#Eval("Alias_Name")%>'></asp:TextBox>
                            </div>
                            <div class="single-des clearfix" runat="server" visible ="false">
                                <asp:Label ID="lblHill" runat="server" CssClass="label" Text="Hill Station"></asp:Label>
                                <asp:RadioButton ID="rdoYes" runat="server" Font-Bold="true" Text="Yes" GroupName="FF" />
                                <asp:RadioButton ID="rdoNo" runat="server" Font-Bold="true" Text="No" GroupName="FF" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Fare/Km"></asp:Label>
                                <asp:TextBox ID="txtFare" runat="server" MaxLength="6" onkeypress="CheckNumeric(event);" CssClass="input" Width="100%"  Text='<%#Eval("Fare")%>'></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Expense Allowance Category"></asp:Label>
                                <asp:DropDownList ID="ddlMetro" runat="server" CssClass="nice-select">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Metro" Value="MM"></asp:ListItem>
                                    <asp:ListItem Text="Semi Metro" Value="SM"></asp:ListItem>
                                    <asp:ListItem Text="Non Metro" Value="NM"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Additional Allowance (if Any)"></asp:Label>
                                <asp:TextBox ID="txtAdditional" runat="server" MaxLength="4" onkeypress="CheckNumeric(event);" CssClass="input" Width="100%"  Text='<%#Eval("Additional_Allowance")%>'></asp:TextBox>
                            </div>

                           <%-- <div class="single-des clearfix">
                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="Territory Visit"></asp:Label>
                                <asp:DropDownList ID="ddlTerr_Visit" runat="server" CssClass="nice-select">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>

                                </asp:DropDownList>
                            </div>--%>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSave" CssClass="savebutton" runat="server"  Text="Save" OnClick="btnSave_Click" />
                        </div>

                    </div>
                         <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back"  OnClick="btnBack_Click" />
                </div>
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
