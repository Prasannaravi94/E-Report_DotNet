<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="MasterFiles_ProductDetail" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Detail</title>

    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />

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

        .chkboxLocation label {
            padding-left: 5px;
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
            //   $('input:text:first').focus();
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
                //   if ($("#txtProdDetailCode").val() == "") { alert("Enter Product Code."); $('#txtProdDetailCode').focus(); return false; }
                if ($("#txtProdDetailName").val() == "") { alert("Enter Product Name."); $('#txtProdDetailName').focus(); return false; }
                if ($("#txtProdSUnit").val() == "") { alert("Enter Sale Unit."); $('#txtProdSUnit').focus(); return false; }

                var cat = $('#<%=ddlCat.ClientID%> :selected').text();
                if (cat == "---Select---") { alert("Select Category."); $('#ddlCat').focus(); return false; }
                var grp = $('#<%=ddlGroup.ClientID%> :selected').text();
                if (grp == "---Select---") { alert("Select Group."); $('#ddlGroup').focus(); return false; }
                var grp = $('#<%=ddlBrand.ClientID%> :selected').text();
                if (grp == "---Select---") { alert("Select Brand."); $('#ddlBrand').focus(); return false; }
                if ($("#txtProdDesc").val() == "") { alert("Enter Description."); $('#txtProdDesc').focus(); return false; }
                if ($("#txtsample").val() == "") { alert("Enter Sample Erp Code."); $('#txtsample').focus(); return false; }
                if ($("#txtsale").val() == "") { alert("Enter Sale Erp Code."); $('#txtsale').focus(); return false; }
                if ($('#chkSubdiv input:checked').length > 0) { return true; } else { alert('Select Subdivision'); return false; }
            });
        });
    </script>
    <script type="text/javascript">
        function ValidateCheckBoxList() {

            var listItems = document.getElementById("chkboxLocation").getElementsByTagName("input");
            var itemcount = listItems.length;
            var iCount = 0;
            var isItemSelected = false;
            for (iCount = 0; iCount < itemcount; iCount++) {
                if (listItems[iCount].checked) {
                    isItemSelected = true;
                    break;
                }
            }
            if (!isItemSelected) {
                alert("Please select State");
            }
            else {
                return true;
            }
            return false;
        }
    </script>
    <script type="text/javascript">

        function checkAll(obj1) {
            var checkboxCollection = document.getElementById('<%=chkboxLocation.ClientID %>').getElementsByTagName('input');
            for (var i = 0; i < checkboxCollection.length; i++) {
                if (checkboxCollection[i].type.toString().toLowerCase() == "checkbox") {

                    checkboxCollection[i].checked = obj1.checked;

                }
            }
        }

    </script>
    <script type="text/javascript">

        $(function () {

            $("[id*=ChkAll]").bind("click", function () {

                if ($(this).is(":checked")) {

                    $("[id*=chkboxLocation] input").attr("checked", "checked");

                } else {

                    $("[id*=chkboxLocation] input").removeAttr("checked");

                }

            });

            $("[id*=chkboxLocation] input").bind("click", function () {

                if ($("[id*=chkboxLocation] input:checked").length == $("[id*=chkboxLocation] input").length) {

                    $("[id*=ChkAll]").attr("checked", "checked");

                } else {

                    $("[id*=ChkAll]").removeAttr("checked");

                }

            });

        });

    </script>
    <%--  <script type="text/javascript">
    function check(){
        var chkNil = document.getElementById("chkNil");
        var chkSubdiv = document.getElementById("chkSubdiv");
    if (chkNil.checked == true)
    {
        chkSubdiv.checked == false;
    } 
    else if(chkSubdiv.checked == true)
    {
        chkNil.checked == false;
    }
}
</script>--%>
    <%--   <script type="text/javascript">

        function checkNIL(obj1) {
            var checkSubCollection = document.getElementById('<%=chkSubdiv.ClientID %>').getElementsByTagName('input');
            
            var checkNilCollection = document.getElementById('<%=chkNil.ClientID %>').getElementsByTagName('input2');

            for (var i = 0; i < checkSubCollection.length; i++) {
                if (obj1.checked) {
                    checkSubCollection[i].checked = false;
                }
                else {
                    checkSubCollection[i].checked = true;
                }             



            }


        }  
      
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center">Product Detail</h2>
                        <div class="designation-area clearfix">

                            <div class="single-des clearfix">
                                <asp:Label ID="lblDetailCode" runat="server" Visible="false" CssClass="label">Product Code<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtProdDetailCode" runat="server" Visible="false" MaxLength="20"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1" CssClass="input" Width="100%"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Product Name<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtProdDetailName" runat="server" TabIndex="2"
                                    AutoCompleteType="Search" MaxLength="50" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                    CssClass="input" Width="100%"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label4" runat="server" CssClass="label">Sale Unit<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtProdSUnit" runat="server" TabIndex="3" MaxLength="15"
                                    onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="100%" CssClass="input"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label6" runat="server" Text="Sample Units" CssClass="label"></asp:Label>
                                <div class="row">
                                    <div class="col-lg-4" style="padding-bottom:10px">
                                        <asp:TextBox ID="txtSamp1" runat="server" Width="100%" CssClass="input" TabIndex="4" 
                                            MaxLength="15"
                                            onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4" style="padding-bottom:10px">
                                        <asp:TextBox ID="txtSamp2" runat="server" Width="100%" CssClass="input" TabIndex="4"
                                            MaxLength="15"
                                            onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4" style="padding-bottom:10px">
                                        <asp:TextBox ID="txtSamp3" runat="server" Width="100%" CssClass="input" TabIndex="4"
                                            MaxLength="15"
                                            onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="Label3" runat="server" CssClass="label">Category<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlCat" runat="server" EnableViewState="true" CssClass="nice-select" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="Label1" runat="server" CssClass="label">Group<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlGroup" runat="server" EnableViewState="true" CssClass="nice-select"
                                        TabIndex="5" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblbrd" runat="server" CssClass="label">Brand<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlBrand" runat="server" EnableViewState="true" CssClass="nice-select"
                                        TabIndex="5" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix" style="min-width:280px">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Type<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                <asp:RadioButtonList ID="RblType" runat="server" RepeatColumns="3">
                                    <asp:ListItem Value="R" Selected="True">Regular Product </asp:ListItem>
                                    <asp:ListItem Value="N">New Product </asp:ListItem>
                                    <asp:ListItem Value="O">Others</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="Label8" runat="server" Text="Mode of Product" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlmode" runat="server" CssClass="nice-select">
                                        <asp:ListItem Text="Sale" Value="Sale"></asp:ListItem>
                                        <asp:ListItem Text="Sample" Value="Sample"></asp:ListItem>
                                        <asp:ListItem Text="Sale/Sample" Value="Sale/Sample"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Description<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtProdDesc" runat="server" CssClass="input" TabIndex="6" MaxLength="120"
                                    onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="100%"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-6" style="padding-bottom:10px">
                                        <asp:Label ID="Label9" runat="server" Text="Sample ERP Code" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtsample" runat="server" CssClass="input" MaxLength="15" Width="100%"
                                            onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6" style="padding-bottom:10px">
                                        <asp:Label ID="Label10" runat="server" Width="150px" Text="Sale ERP Code" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtsale" runat="server" CssClass="input" MaxLength="15"
                                            onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                      
                    </div>
                      <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back"
                        OnClick="btnBack_Click" />
                </div>
                <div class="row justify-content-center">
                    <asp:Label ID="lblTitle_LocationDtls" runat="server" Text="Select the State/Location"
                        TabIndex="6">  
                    </asp:Label>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-7">
                        <asp:CheckBox ID="ChkAll" runat="server" Text=" All" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    </div>
                </div>
                <div class="row justify-content-center" style="overflow-x: auto;">
                    <div class="col-lg-7">
                        <asp:CheckBoxList ID="chkboxLocation" runat="server"  DataTextField="State_Name" DataValueField="State_Code"
                            RepeatDirection="Vertical" RepeatColumns="4" Width="650px" TabIndex="7" >
                        </asp:CheckBoxList>
                          <br />
                    </div>
                </div>
              
                <div class="row justify-content-center">
                    <asp:Label ID="lbldivision" runat="server" Text="Sub Division" TabIndex="6"> </asp:Label>
                </div>
                <br />
                <div class="row justify-content-center" style="overflow-x: auto;">
                    <div class="col-lg-7">
                        <%-- <asp:CheckBox ID="chkNil" runat="server" Checked="true" Text="NIL" onclick="checkNIL(this);"
                            OnClientClick="return check()" OnCheckedChanged="chkNil_CheckedChanged" />--%>
                        <asp:CheckBoxList ID="chkSubdiv" runat="server" DataTextField="subdivision_name"
                            DataValueField="subdivision_code" RepeatDirection="Vertical" RepeatColumns="4"
                            TabIndex="7">
                        </asp:CheckBoxList>
                          <br />
                    </div>
                </div>
              
                 <div class="row justify-content-center">
                <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton"  Text="Save" OnClick="Submit_Click"
                    OnClientClick="return ValidateCheckBoxList()" />
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
