<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unique_Dr_View_admin.aspx.cs"
    Inherits="MasterFiles_Common_Doctors_Unique_Dr_View_admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .aclass
        {
            border: 1px solid lighgray;
        }
        .aclass
        {
            width: 50%;
        }
        .aclass tr td
        {
            background: White;
            font-weight: bold;
            color: Black;
            border: 1px solid black;
            border-collapse: collapse;
        }
        .aclass th
        {
            border: 1px solid black;
            border-collapse: collapse;
            background: LightBlue;
        }
        .lbl
        {
            color: Red;
        }
        
        
        .space
        {
            padding: 3px 3px;
        }
        .sp
        {
            padding-left: 11px;
        }
        
        .style6
        {
            padding: 3px 3px;
            height: 28px;
        }
        .marRight
        {
            margin-right: 35px;
        }
        
        .boxshadow
        {
            -moz-box-shadow: 3px 3px 5px #535353;
            -webkit-box-shadow: 3px 3px 5px #535353;
            box-shadow: 3px 3px 5px #535353;
        }
        .roundbox
        {
            -moz-border-radius: 6px 6px 6px 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px 6px 6px 6px;
        }
        .grd
        {
            border: 1;
            border-color: Black;
        }
        .roundbox-top
        {
            -moz-border-radius: 6px 6px 0 0;
            -webkit-border-radius: 6px 6px 0 0;
            border-radius: 6px 6px 0 0;
        }
        .roundbox-bottom
        {
            -moz-border-radius: 0 0 6px 6px;
            -webkit-border-radius: 0 0 6px 6px;
            border-radius: 0 0 6px 6px;
        }
        .gridheader, .gridheaderbig, .gridheaderleft, .gridheaderright
        {
            padding: 6px 6px 6px 6px;
            background: #003399 url(images/vertgradient.png) repeat-x;
            text-align: center;
            font-weight: bold;
            text-decoration: none;
            color: khaki;
        }
        
        .gridheaderleft
        {
            text-align: left;
        }
        .gridheaderright
        {
            text-align: right;
        }
        .gridheaderbig
        {
            font-size: 135%;
        }
        .gridview1
        {
            background-color: #336699;
            border-style: none;
            padding: 2px;
            margin: 2% auto;
        }
        
        .gridview1 a
        {
            margin: auto 1%;
            border-style: none;
            border-radius: 50%;
            background-color: #444;
            padding: 5px 7px 5px 7px;
            color: #fff;
            text-decoration: none;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
        }
        .gridview1 a:hover
        {
            background-color: #1e8d12;
            color: #fff;
        }
        .gridview1 td
        {
            border-style: none;
        }
        .gridview1 span
        {
            background-color: #ae2676;
            color: #fff;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
            border-radius: 50%;
            padding: 5px 7px 5px 7px;
        }
        .mGridImg1
        {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }
        .mGridImg1 td
        {
            padding: 2px;
            border-color: Black;
            background: F2F1ED;
            font-size: small;
            font-family: Calibri;
        }
        
        .mGridImg1 th
        {
            padding: 4px 2px;
            color: white;
            background: #336699;
            border-color: Black;
            border-left: solid 1px Black;
            border-right: solid 1px Black;
            border-top: solid 1px Black;
            border-bottom: solid 1px Black;
            font-weight: normal;
            font-size: small;
            font-family: Calibri;
        }
        .mGridImg1 .pgr
        {
            background: #336699;
        }
        .mGridImg1 .pgr table
        {
            margin: 5px 0;
        }
        .mGridImg1 .pgr td
        {
            border-width: 0;
            text-align: left;
            padding: 0 6px;
            border-left: solid 1px #666;
            font-weight: bold;
            color: Red;
            line-height: 12px;
        }
        .mGridImg1 .pgr a
        {
            color: White;
            text-decoration: none;
        }
        .mGridImg1 .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
    </style>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
     <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        function validate() {
            var txtreject = document.getElementById('<%=txtreject.ClientID %>').value;
            if (txtreject == "") {
                alert("Please Enter the Reason");
                document.getElementById('<%=txtreject.ClientID %>').focus();
                return false;
            }

            if (confirm('Do you want to Submit?')) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
     <script src="../../JsFiles/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="../../JsFiles/jquery.tooltip.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".gridImages").tooltip({
                track: true,
                delay: 0,
                showURL: false,
                fade: 100,
                bodyHandler: function () {
                    return $($(this).next().html());
                },
                showURL: false
            });
        })
    </script>
    
    <script type="text/javascript">
        $(document).ready(function () {

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
                            $('#btnAdd').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnApprove').click(function () {
                var div_code = $("#hdndivCode").val();
                if ($("#txtName").val() == "") { alert("Enter Doctor Name."); $('#txtName').focus(); return false; }

                var qual = $('#<%=ddlQual.ClientID%> :selected').text();
                if (qual == "---Select---") { alert("Select Qualification."); $('#ddlQual').focus(); return false; }
                var spec = $('#<%=ddlSpec.ClientID%> :selected').text();
                if (spec == "---Select---") { alert("Select Speciality."); $('#ddlSpec').focus(); return false; }

                var cat = $('#<%=ddlCatg.ClientID%> :selected').text();
                if (cat == "---Select---") { alert("Select Category."); $('#ddlCatg').focus(); return false; }

                var Hq = $('#<%=ddlterr.ClientID%> :selected').text();
                if (Hq == "---Select---") { alert("Select Area Cluster Name."); $('#ddlterr').focus(); return false; }


                if ($("#txtHospital").val() == "") { alert("Enter Clinic Name."); $('#txtHospital').focus(); return false; }
                if ($("#txtHosAddress").val() == "") { alert("Enter Clinic Address."); $('#txtHosAddress').focus(); return false; }
                if ($("#txtMobile").val() == "") { alert("Enter Mobile No."); $('#txtMobile').focus(); return false; }
                if ($("#lblCity").val() == "") { alert("Enter City."); $('#lblCity').focus(); return false; }
                if ($("#txtPincode").val() == "") { alert("Enter Pincode."); $('#txtPincode').focus(); return false; }


                // if ($('#FilUpImage').val() == "") { alert("Please Select Visiting Card."); $('#FilUpImage').focus(); return false; }
                  if (div_code == 3) {
                    var prod = $('#<%=ddlProd1.ClientID%> :selected').text();
                    if (prod == "---Select---") { alert("Select Product 1."); $('#ddlProd1').focus(); return false; }
                    var prod2 = $('#<%=ddlProd2.ClientID%> :selected').text();
                    if (prod2 == "---Select---") { alert("Select Product 2."); $('#ddlProd2').focus(); return false; }
                    var prod3 = $('#<%=ddlProd3.ClientID%> :selected').text();
                    if (prod3 == "---Select---") { alert("Select Product 3."); $('#ddlProd3').focus(); return false; }
                    var prod4 = $('#<%=ddlProd4.ClientID%> :selected').text();
                    if (prod4 == "---Select---") { alert("Select Product 4."); $('#ddlProd4').focus(); return false; }
                    var prod5 = $('#<%=ddlProd5.ClientID%> :selected').text();
                    if (prod5 == "---Select---") { alert("Select Product 5."); $('#ddlProd5').focus(); return false; }

                }
                if (div_code == 4) {
                    var prod = $('#<%=ddlProd1.ClientID%> :selected').text();
                    if (prod == "---Select---") { alert("Select Product 1."); $('#ddlProd1').focus(); return false; }
                    var prod2 = $('#<%=ddlProd2.ClientID%> :selected').text();
                    if (prod2 == "---Select---") { alert("Select Product 2."); $('#ddlProd2').focus(); return false; }
                    var prod3 = $('#<%=ddlProd3.ClientID%> :selected').text();
                    if (prod3 == "---Select---") { alert("Select Product 3."); $('#ddlProd3').focus(); return false; }
                    var prod4 = $('#<%=ddlProd4.ClientID%> :selected').text();
                    if (prod4 == "---Select---") { alert("Select Product 4."); $('#ddlProd4').focus(); return false; }
                    var prod5 = $('#<%=ddlProd5.ClientID%> :selected').text();
                    if (prod5 == "---Select---") { alert("Select Product 5."); $('#ddlProd5').focus(); return false; }
                }
                if (div_code == 5) {
                    var prod = $('#<%=ddlProd1.ClientID%> :selected').text();
                    if (prod == "---Select---") { alert("Select Product 1."); $('#ddlProd1').focus(); return false; }
                    var prod2 = $('#<%=ddlProd2.ClientID%> :selected').text();
                    if (prod2 == "---Select---") { alert("Select Product 2."); $('#ddlProd2').focus(); return false; }
                    var prod3 = $('#<%=ddlProd3.ClientID%> :selected').text();
                    if (prod3 == "---Select---") { alert("Select Product 3."); $('#ddlProd3').focus(); return false; }
                    var prod4 = $('#<%=ddlProd4.ClientID%> :selected').text();
                    if (prod4 == "---Select---") { alert("Select Product 4."); $('#ddlProd4').focus(); return false; }
                    var prod5 = $('#<%=ddlProd5.ClientID%> :selected').text();
                    if (prod5 == "---Select---") { alert("Select Product 5."); $('#ddlProd5').focus(); return false; }
                }
                if (div_code == 7) {
                    var prod = $('#<%=ddlProd1.ClientID%> :selected').text();
                    if (prod == "---Select---") { alert("Select Product 1."); $('#ddlProd1').focus(); return false; }
                    var prod2 = $('#<%=ddlProd2.ClientID%> :selected').text();
                    if (prod2 == "---Select---") { alert("Select Product 2."); $('#ddlProd2').focus(); return false; }
                    var prod3 = $('#<%=ddlProd3.ClientID%> :selected').text();
                    if (prod3 == "---Select---") { alert("Select Product 3."); $('#ddlProd3').focus(); return false; }

                }
                if (div_code == 6) {
                    var prod = $('#<%=ddlProd1.ClientID%> :selected').text();
                    if (prod == "---Select---") { alert("Select Product 1."); $('#ddlProd1').focus(); return false; }


                }

            

            });

        }); 
    </script>
</head>
<body onunload="window.opener.location=window.opener.location;">
    <form id="form1" runat="server">
    <br />
       <asp:HiddenField runat="server" ID="hdndivCode" />
    <div>
        <center>
            <h2 style="color: Blue; font-weight: bold; text-decoration: underline">
                New Unique Dr Approval - View</h2>
            <br />
            <asp:Panel ID="pnlNew" runat="server" Width="100%">
                <center>
                    <div class="roundbox boxshadow" style="width: 85%; border: solid 2px steelblue;">
                        <div class="gridheaderleft">
                            Doctor Details
                        </div>
                        <div class="boxcontenttext" style="background: White;">
                            <div id="Div1">
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblName" SkinID="lblMand" Width="180px" runat="server"><span style="Color:Red">*</span>Listed Doctor Name </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtName" runat="server" Width="220px" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                            onkeypress="AlphaNumeric_NoSpecialChars(event)" onblur="this.style.backgroundColor='White'"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblQual" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Qualification </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlQual" runat="server" SkinID="ddlRequired">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label9" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Category </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlCatg" runat="server" SkinID="ddlRequired">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label14" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Class </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlCls" runat="server" SkinID="ddlRequired">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblAddress" runat="server" SkinID="lblMand">Residence Address </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtAddress1" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'" Width="220px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label5" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Clinic Name</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtHospital" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label1" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Clinic Address</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtHosAddress" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'" Width="220px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label6" runat="server" SkinID="lblMand">Landline No</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtland" runat="server" onkeypress="CheckNumeric(event);" Width="100px"
                                                            SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblMobile" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Mobile No</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtMobile" onkeypress="CheckNumeric(event);" runat="server" SkinID="MandTxtBox"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                                                            Width="140px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label7" runat="server" SkinID="lblMand">Mail ID</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtMail" runat="server" Width="120px" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblCa" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>City</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="lblCity" runat="server" Width="130px" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label3" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>State Name</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <%-- <asp:DropDownList ID="ddlState" Width="126px" runat="server" SkinID="ddlRequired">
                                                        </asp:DropDownList>--%>
                                                        <asp:Label ID="lblState" runat="server" SkinID="lblMand"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label8" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>PinCode</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtpin" runat="server" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                                                            Width="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label4" runat="server" SkinID="lblMand">Reg. No</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtRegNo" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblSpec" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Speciality </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlSpec" runat="server" SkinID="ddlRequired">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblH" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Area Cluster Name</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlterr" Width="126px" runat="server" SkinID="ddlRequired">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblDOB" runat="server" SkinID="lblMand" Text="Date of Birth "></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlDobDate" runat="server" SkinID="ddlRequired" Width="50">
                                                            <asp:ListItem Value="01" Text="DD"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                                            <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlDobMonth" runat="server" SkinID="ddlRequired" Width="50">
                                                            <asp:ListItem Value="01" Text="MM"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlDobYear" runat="server" SkinID="ddlRequired" Width="60">
                                                            <asp:ListItem Selected="True" Value="1900" Text="YYYY"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblDOW" runat="server" SkinID="lblMand" Text="DOW "></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlDowDate" runat="server" SkinID="ddlRequired" Width="50">
                                                            <asp:ListItem Value="01" Text="DD"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                                            <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlDowMonth" runat="server" SkinID="ddlRequired" Width="50">
                                                            <asp:ListItem Value="01" Text="MM"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlDowYear" runat="server" SkinID="ddlRequired" Width="60">
                                                            <asp:ListItem Selected="True" Value="1900" Text="YYYY"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <%--  <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Area"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="lblArea" runat="server" Width="130px" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'"></asp:TextBox>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td class="space" align="left" colspan="3">
                                                        <div class="roundbox boxshadow" style="width: 250px; border: solid 2px steelblue;">
                                                            <div class="gridheaderleft" style="background-color: LightPink; color: Blue">
                                                                Product Map
                                                            </div>
                                                            <div class="boxcontenttext" style="background: White;">
                                                                <div id="Div3">
                                                                    <table>
                                                                        <tr>
                                                                            <td class="space" align="left">
                                                                                <asp:Label ID="Label2" runat="server" SkinID="lblMand">Product 1</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td class="space" align="left">
                                                                                <asp:DropDownList ID="ddlProd1" runat="server" SkinID="ddlRequired">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="space" align="left">
                                                                                <asp:Label ID="Label11" runat="server" SkinID="lblMand">Product 2</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td class="space" align="left">
                                                                                <asp:DropDownList ID="ddlProd2" runat="server" SkinID="ddlRequired">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="space" align="left">
                                                                                <asp:Label ID="Label12" runat="server" SkinID="lblMand">Product 3</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td class="space" align="left">
                                                                                <asp:DropDownList ID="ddlProd3" runat="server" SkinID="ddlRequired">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="space" align="left">
                                                                                <asp:Label ID="Label13" runat="server" SkinID="lblMand">Product 4</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td class="space" align="left">
                                                                                <asp:DropDownList ID="ddlProd4" runat="server" SkinID="ddlRequired">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="space" align="left">
                                                                                <asp:Label ID="Label10" runat="server" SkinID="lblMand">Product 5</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td class="space" align="left">
                                                                                <asp:DropDownList ID="ddlProd5" runat="server" SkinID="ddlRequired">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <%--<td class="space" align="left">
                                                <asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Mode"></asp:Label>
                                            </td>
                                            <td>
                                            :  
                                            </td>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblMode" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px" ForeColor="Red">
                                                </asp:Label>
                                            </td>--%>
                                                    <td align="left" class="stylespc">
                                                        <asp:Label ID="Label30" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Visiting Card Upload</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="left" class="stylespc">
                                                        <asp:FileUpload ID="FilUpImage" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <center>
                                    <div id="dvPreview">
                                    </div>
                                    <asp:Label ID="lblVis" runat="server" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblVisFile" align="left" runat="server">
                                        <asp:Image ID="imgVisFile" runat="server" />
                                    </asp:Label>
                                    <asp:DataList ID="DataList1" runat="server" HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Image ID="imgHome" ImageUrl='<%# Eval("Visiting_Card") %>' Width="200px" ImageAlign="Top"
                                                    runat="server" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </center>
                                <br />
                                <br />
                                <%--   <asp:Button ID="btnAdd" runat="server" Text="Submit" BackColor="LightBlue" Width="70px"
                                    OnClick="btnAdd_Click" />--%>
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </center>
            </asp:Panel>
        </center>
        <br />
        <center>
            <asp:Button ID="btnApprove" runat="server" CssClass="savebutton" Text="Approve - New UniqueDR"
                Width="170px" BackColor="LightBlue" OnClick="btnApprove_Click" />&nbsp;
            <asp:Button ID="btnReject" runat="server" CssClass="savebutton" Text="Reject" Width="70px"
                BackColor="LightBlue" OnClick="btnReject_Click" />
            <asp:Label ID="lblRejectReason" Text="Reject Reason : " Visible="false" SkinID="lblMand"
                runat="server"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtreject" runat="server" TextMode="MultiLine" BorderStyle="Solid"
                BorderColor="Gray" Visible="false" Height="70px" Width="350px"></asp:TextBox>
            &nbsp
            <asp:Button ID="btnSubmit" CssClass="savebutton" Width="140px" BackColor="LightBlue"
                runat="server" Visible="false" OnClientClick="return validate();" Text="Confirm Reject"
                OnClick="btnSubmit_Click" />
        </center>
    </div>
    <br />
    <asp:Panel ID="Panel1" runat="server" Width="100%">
        <center>
            <div class="roundbox boxshadow" style="width: 80%; border: solid 2px steelblue;">
                <div class="gridheaderleft">
                    Search Result
                </div>
                <div class="boxcontenttext" style="background: White;">
                    <div id="Div2">
                        <center>
                            <table width="100%" align="center">
                                <tbody>
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center"
                                                EmptyDataText="No Records Found" AutoGenerateColumns="false" GridLines="None"
                                                CssClass="mGridImg" AlternatingRowStyle-CssClass="alt">
                                                <PagerStyle CssClass="gridview1"></PagerStyle>
                                                <HeaderStyle Font-Bold="False" />
                                                <SelectedRowStyle BackColor="BurlyWood" />
                                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ref No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcom" runat="server" Text='<%#Eval("C_Doctor_Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Common Doctor Name" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("C_Doctor_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Qualification" HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQua" runat="server" Text='<%# Bind("Qual_Short_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Speciality" HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Speciality_Short_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Clinic Name" HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClinic" runat="server" Text='<%# Bind("C_Doc_Hospital") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Clinic Address" HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddr" runat="server" Text='<%# Bind("C_Doctor_Hos_Addr") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Mobile" HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMob" runat="server" Text='<%# Bind("C_Doctor_Mobile") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="City" HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCity" runat="server" Text='<%# Bind("Drs_City") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="PinCode" HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPin" runat="server" Text='<%# Bind("Pincode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="HQ" Visible="false"
                                                        HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHq" runat="server" Text='<%# Bind("C_Doctor_HQ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Visiting Card" >
                                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                         
                                                            <asp:Image ID="Image1" Width="25px" Height="25px" runat="server" class="gridImages"
                                                                ImageUrl='<%#Eval("Visiting_Card") %>' />
                                                            <div id="tooltip" style="display: none;">
                                                                <table>
                                                                    <tr>
                                                                       
                                                                        <td>
                                                                            <asp:Image ID="imgUserName" Width="250px" Height="120px" ImageUrl='<%#Eval("Visiting_Card") %>'
                                                                                runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                    VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </center>
                    </div>
                </div>
            </div>
        </center>
    </asp:Panel>
    </form>
</body>
</html>
