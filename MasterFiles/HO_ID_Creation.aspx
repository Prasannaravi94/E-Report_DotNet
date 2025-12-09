<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HO_ID_Creation.aspx.cs" Inherits="MasterFiles_HO_ID_Creation" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HO ID Creation</title>
    <style type="text/css">
        .test tr input {
            margin-right: 10px;
            padding-right: 10px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
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
    <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
</head>
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
        $('#btnSubmit').click(function () {


            if ($("#txtName").val() == "") { alert("Please Enter Name."); $('#txtName').focus(); return false; }
            if ($("#txtUserName").val() == "") { alert("Please Enter User Name."); $('#txtUserName').focus(); return false; }
            if ($("#txtPassword").val() == "") { alert("Please Enter Password."); $('#txtPassword').focus(); return false; }
            if ($('#chkDivision input:checked').length > 0) { return true; } else { alert('Please Select Division'); return false; }

        });
    });
</script>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
             <br />
            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Change Password</h2>

                        <div class="designation-area clearfix">

                            <div class="single-des clearfix">
                                <asp:Label ID="lblName" runat="server" Text="Name" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtName" runat="server" TabIndex="1" CssClass="input" Width="100%"></asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblUserName" runat="server" Text="User Name" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtUserName" runat="server" TabIndex="2" CssClass="input" Width="100%"></asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtPassword" runat="server" TabIndex="3" CssClass="input" MaxLength="15" TextMode="Password" Width="100%"></asp:TextBox>
                            </div>

                            <%--  <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblMenu" runat="server" Text="Menu Type" SkinID="lblMand"></asp:Label>
                                    <asp:DropDownList ID="ddlMenu" runat="server" TabIndex="4" SkinID="ddlRequired" Width="100">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Menu Type 1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Menu Type 2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Menu Type 3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
                            <%--  <asp:Label ID="lblReproting" runat="server" TabIndex="6" Text="Reporting To" SkinID="lblMand"></asp:Label>
                             <asp:DropDownList ID="ddlReporting" runat="server" DataTextField="ho_name" DataValueField="ho_id" Width="200" SkinID="ddlRequired">
                    </asp:DropDownList>--%>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDivision" runat="server" Text="Division Name" CssClass="label"></asp:Label>

                                <asp:CheckBoxList ID="chkDivision" TabIndex="5" runat="server" DataTextField="division_name"
                                    DataValueField="division_code" RepeatColumns="2" Font-Bold="false"
                                    RepeatDirection="vertical" CssClass="test">
                                </asp:CheckBoxList>
                            </div>

                        </div>
                        <p>
                            <br />
                           
                        </p>
                        <div class="w-100 designation-submit-button text-center clearfix">                          

                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" TabIndex="7"  Text="Submit"
                                OnClick="btnSubmit_Click" />
                          
                             <asp:Button ID="btnReset" runat="server" TabIndex="8"   CssClass="resetbutton" Text="Reset"
                            OnClick="btnReset_Click" />
                        </div>
                        

                    </div>
                </div>
                  <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click"/>
              
            </div>

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
            <br />
            <br />
          
        </div>
    </form>
</body>
</html>
