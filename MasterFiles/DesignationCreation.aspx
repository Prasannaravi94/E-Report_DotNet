<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignationCreation.aspx.cs"
    Inherits="MasterFiles_DesignationCreation" %>

<%@ Register Assembly="obout_ColorPicker" Namespace="OboutInc.ColorPicker" TagPrefix="obout" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Designation</title>
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
    </style>
   <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
    <form id="form1" runat="server" method="post">
        <div>
            <script type="text/javascript">
                $(document).ready(function () {
                    $('input:text:first').focus();
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
                        if ($("#txtShortName").val() == "") { alert("Enter Short Name."); $('#txtShortName').focus(); return false; }
                        if ($("#txtDesignation").val() == "") { alert("Enter Designation."); $('#txtDesignation').focus(); return false; }
                        var cat = $('#<%=ddlDesType.ClientID%> :selected').text();
                        if (cat == "--Select--") { alert("Enter Type."); $('#ddlDesType').focus(); return false; }

                        if ($("#txtColor").val() == "") { alert("Select Color."); $('#txtColor').focus(); return false; }
                    });
                });

            </script>
            <script type="text/javascript">
                function onBGColorPicked(sender) {

                    var hiddenField = document.getElementById('<%=bgColor.ClientID %>');
                    hiddenField.value = sender.getColor();
                    var color = hiddenField.value.replace('#', '');

                    document.getElementById('txtColor').value = color;


                }


            </script>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
              <br />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center">Designation</h2>
                 
                            <div class="designation-area clearfix">
                            
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblDesName" runat="server" CssClass="label">Short Name  <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                    <asp:TextBox ID="txtShortName" runat="server" CssClass="input" Width="100%" TabIndex="1" MaxLength="10" onkeypress="CharactersOnly(event);">                                      
                                    </asp:TextBox>
                                </div>

                                <div class="single-des clearfix">
                                    <asp:Label ID="lblDesignation" runat="server" CssClass="label">Designation  <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                    <asp:TextBox ID="txtDesignation" TabIndex="2" CssClass="input" runat="server" Width="100%" MaxLength="120" onkeypress="CharactersOnly(event);">                                                                           
                                    </asp:TextBox>
                                </div>

                                 <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblDesType" runat="server" CssClass="label" >Type  <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlDesType" runat="server" CssClass="nice-select"
                                        TabIndex="2">
                                        <asp:ListItem Selected="True" Text="--Select--" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Base Level" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Manager" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                </div>

                                 <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="Lbldivi" runat="server" CssClass="label">Division Name  <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" AutoPostBack="false" CssClass="nice-select"></asp:DropDownList>

                                </div>
                                      </div>

                                <div class="single-des clearfix">
                                    <asp:HiddenField runat="server" ID="bgColor" />
                                    <asp:Label ID="lblColor" runat="server" CssClass="label">BackColor  <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtColor"   onkeypress="Calendar_enter(event);" AutoCompleteType="None" runat="server" CssClass="input" width="93%"/>

                                    <obout:ColorPicker ID="cpBGColor" runat="server"  
                                        PickButton="False" TargetProperty="style.backgroundColor" OnClientPicked="onBGColorPicked">
                                    </obout:ColorPicker>
                                </div>
                                  <%-- <asp:TextBox ID="txtColor" SkinID="MandTxtBox" AutoCompleteType="None" runat="server" />
                        &nbsp;<asp:ImageButton ID="Imgpick" ImageUrl="~/Images/color.png" runat="server" />
                    
                        
                        <asp:ColorPickerExtender ID="ColorPickerExtender1" TargetControlID="txtColor" PopupButtonID="Imgpick"
                            PopupPosition="TopRight" SampleControlID="PreviewColor" Enabled="True" runat="server">
                        </asp:ColorPickerExtender>--%>

                                  <%--     <div id="PreviewColor" style="width: 20px; height: 20px; border: 1px solid Gray">
                        </div>--%>

                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Save" 
                     OnClick="btnSubmit_Click" />
                                
                            </div>
                           
                   
                    </div>
                       <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" 
                    OnClick="btnBack_Click" />
                   
                </div>
              
              
              
            </div>

            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
            <br />
        </div>
    </form>
</body>
</html>
