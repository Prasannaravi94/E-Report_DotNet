<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Common_Doctor_Updation_FDC.aspx.cs"
    Inherits="MasterFiles_Common_Doctor_Updation_FDC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Common Doctor Updation</title>
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
            padding: 6px 6px;
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
    </style>
     <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
                            $('#btnsubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnsubmit').click(function () {
                var div_code = $("#hdndivCode").val();

                var qual = $('#<%=ddlQual.ClientID%> :selected').text();
                if (qual == "---Select---") { alert("Select Qualification."); $('#ddlQual').focus(); return false; }
                var spec = $('#<%=ddlSpec.ClientID%> :selected').text();
                if (spec == "---Select---") { alert("Select Speciality."); $('#ddlSpec').focus(); return false; }

                var cat = $('#<%=ddlCatg.ClientID%> :selected').text();
                if (cat == "---Select---") { alert("Select Category."); $('#ddlCatg').focus(); return false; }
                var cls = $('#<%=ddlCls.ClientID%> :selected').text();
                if (cls == "---Select---") { alert("Select Class."); $('#ddlCls').focus(); return false; }

                var terr = $('#<%=ddlterr.ClientID%> :selected').text();
                if (terr == "---Select---") { alert("Select Area Cluster Name."); $('#ddlterr').focus(); return false; }


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
                if (div_code == 7 || div_code == 8 || div_code == 9 || div_code == 10) {
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
<body style="background-color: White" >
    <form id="form1" runat="server">
    <div>
    <asp:HiddenField runat="server" ID="hdndivCode" />
        <br />
        <center>
            <div align="center">
                <asp:Label ID="lblHead" runat="server" Text="Listed Dr - Addition" Font-Underline="True"
                    Font-Bold="True" Font-Names="Verdana" Font-Size="11pt"></asp:Label>
                <br />
                <br />
                <asp:Label ID="LblForceName" runat="server" Font-Bold="True" ForeColor="BlueViolet"
                    Font-Names="Verdana" Font-Size="9pt"></asp:Label>
            </div>
            <br />
        </center>
        <center>
            <div class="roundbox boxshadow" style="width: 1000px; border: solid 2px steelblue;">
                <div class="gridheaderleft">
                    Doctor Details
                </div>
                <div class="boxcontenttext" style="background: White;">
                    <div id="pnlPreviewSurveyData">
                        <br />
                        <table>
                            <tr>
                                <td valign="top">
                                    <table >
                                        <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblName" SkinID="lblMand" Width="180px" runat="server">Listed Doctor Name </asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                                <asp:Label ID="lbldr" runat="server" Width="200px" Font-Bold="true" Font-Names="Verdana"
                                                    Font-Size="12px" ForeColor="Red"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblQual" runat="server" SkinID="lblMand">Qualification </asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                                <%--<asp:Label ID="lblQua" runat="server"  Font-Bold="true" Font-Names="Verdana" Font-Size="12px" ForeColor="Red" >
                                                </asp:Label>--%>
                                                <asp:DropDownList ID="ddlQual" runat="server" SkinID="ddlRequired">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblqum" runat="server" SkinID="lblMand">Qualification (Based on UM) </asp:Label>
                                            </td>
                                            <td>
                                                 <asp:Label ID="lblcolQ" runat="server">:</asp:Label>
                                            </td>
                                            <td class="space" align="left">
                                               <asp:Label ID="lblQua" runat="server"  Font-Bold="true" Font-Names="Verdana" Font-Size="12px" ForeColor="Red" >
                                                </asp:Label>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="Label9" runat="server" SkinID="lblMand">Category </asp:Label>
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
                                                <asp:Label ID="Label2" runat="server" SkinID="lblMand">Class </asp:Label>
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
                                                <asp:Label ID="Label5" runat="server" SkinID="lblMand">Clinic Name</asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblHosp" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px"
                                                    ForeColor="Red">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Clinic Address"></asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblCliAddr" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px"
                                                    ForeColor="Red"></asp:Label>
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
                                                <asp:Label ID="lblRAddr" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px"
                                                    ForeColor="Red"></asp:Label>
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
                                                <asp:Label ID="lblland" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px"
                                                    ForeColor="Red"></asp:Label>
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
                                                <asp:Label ID="lblMail" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px"
                                                    ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                      
                                         <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblCa" runat="server" SkinID="lblMand">City</asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblCity" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px"
                                                    ForeColor="Red">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td class="space" align="left">
                                                <asp:Label ID="lblst" runat="server" SkinID="lblMand" Text="State"  ></asp:Label>
                                            </td>
                                           <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                            <asp:Label ID="lblstate" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px"
                                                    ForeColor="Red"></asp:Label>
                                                <%--<asp:DropDownList ID="ddlSt" runat="server" SkinID="ddlRequired" Visible="false">
                                                    <asp:ListItem Value="-1">---Select---</asp:ListItem>
                                                    <asp:ListItem Value="0">ArunachalaPradesh</asp:ListItem>
                                                    <asp:ListItem Value="1">AndraPradesh</asp:ListItem>
                                                    <asp:ListItem Value="2">Assam</asp:ListItem>
                                                    <asp:ListItem Value="3">Bihar</asp:ListItem>
                                                    <asp:ListItem Value="4">Chattisgarh</asp:ListItem>
                                                    <asp:ListItem Value="5">Goa</asp:ListItem>
                                                    <asp:ListItem Value="6">Gujarat</asp:ListItem>
                                                    <asp:ListItem Value="7">Haryana</asp:ListItem>
                                                    <asp:ListItem Value="8">Himachal Pradesh</asp:ListItem>
                                                    <asp:ListItem Value="9">Jammu and Kashmir</asp:ListItem>
                                                    <asp:ListItem Value="10">Jharkhand</asp:ListItem>
                                                    <asp:ListItem Value="11">Karnataka</asp:ListItem>
                                                    <asp:ListItem Value="12">Kerala</asp:ListItem>
                                                    <asp:ListItem Value="13">Madhya Pradesh</asp:ListItem>
                                                    <asp:ListItem Value="14">Maharashtra</asp:ListItem>
                                                    <asp:ListItem Value="15">Manipur</asp:ListItem>
                                                    <asp:ListItem Value="16">Meghalaya</asp:ListItem>
                                                    <asp:ListItem Value="17">Mizoram</asp:ListItem>
                                                    <asp:ListItem Value="18">NagaLand</asp:ListItem>
                                                    <asp:ListItem Value="19">Odisha</asp:ListItem>
                                                    <asp:ListItem Value="20">Punjab</asp:ListItem>
                                                    <asp:ListItem Value="21">Rajasthan</asp:ListItem>
                                                    <asp:ListItem Value="22">Sikkim</asp:ListItem>
                                                    <asp:ListItem Value="23">Tamil Nadu</asp:ListItem>
                                                    <asp:ListItem Value="24">Tripura</asp:ListItem>
                                                    <asp:ListItem Value="25">Uttaranchal</asp:ListItem>
                                                    <asp:ListItem Value="26">Uttar Pradesh</asp:ListItem>
                                                    <asp:ListItem Value="27">West Bengal</asp:ListItem>
                                                    <asp:ListItem Value="28">Andaman and Niccobar Islands</asp:ListItem>
                                                    <asp:ListItem Value="29">Chandigarh</asp:ListItem>
                                                    <asp:ListItem Value="30">Dadra and Nagar Haveli</asp:ListItem>
                                                    <asp:ListItem Value="31">Daman and Diu</asp:ListItem>
                                                    <asp:ListItem Value="32">Delhi</asp:ListItem>
                                                    <asp:ListItem Value="33">Pondicherry</asp:ListItem>
                                                    <asp:ListItem Value="34">Mumbai</asp:ListItem>
                                                    <asp:ListItem Value="35">Telangana</asp:ListItem>
                                                    <asp:ListItem Value="36">Srinagar</asp:ListItem>
                                                    <asp:ListItem Value="37">Nepal</asp:ListItem>
                                                    <asp:ListItem Value="38">Buton</asp:ListItem>
                                                </asp:DropDownList>
--%>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="Label8" runat="server" SkinID="lblMand">PinCode</asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblPin" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px"
                                                    ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                       <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblv" runat="server" SkinID="lblMand" Text="Visiting Card"></asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblVis" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px"
                                                    ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                         <tr>
                                       <td ></td>
                                       <td>
                                       </td>
                                       <td>
                                           <asp:Panel runat="server" ID="pnlVis">

                                                        <asp:DataList ID="dtvis" runat="server" HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Image ID="imgHome" ImageUrl='<%# Eval("Visiting_Card") %>' Width="350px" ImageAlign="Top"
                                                                        runat="server" />
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:DataList>

                                                    </asp:Panel>
                                       </td>
                                       </tr>
                                    </table>
                                </td>
                             <td valign="top">
                                    <table>
                                        <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="Label4" runat="server" Width="180px"  SkinID="lblMand" Text="Reg. NO"></asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblReg" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px"
                                                    ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblSpec" runat="server" SkinID="lblMand">Speciality </asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                                <%--   <asp:Label ID="lblSpe" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px" ForeColor="Red">
                                                    </asp:Label>--%>
                                                <asp:DropDownList ID="ddlSpec" runat="server" Width="120px" SkinID="ddlRequired">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblS" runat="server" SkinID="lblMand">Speciality (Based on UM)</asp:Label>
                                            </td>
                                            <td>
                                               <asp:Label ID="lblcol" runat="server"> :</asp:Label>
                                            </td>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblSpe" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px" ForeColor="Red">
                                                    </asp:Label>
                                              
                                            </td>
                                        </tr>
                                       <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="Label14" runat="server" SkinID="lblMand">Area Cluster Name </asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                                <%--   <asp:Label ID="lblSpe" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px" ForeColor="Red">
                                                    </asp:Label>--%>
                                                <asp:DropDownList ID="ddlterr" runat="server" SkinID="ddlRequired">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblMobile" runat="server" SkinID="lblMand" Text="Mobile No "></asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblMob" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px"
                                                    ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td class="space" align="left" colspan="4">
                                                <div class="roundbox boxshadow" style="width: 280px; border: solid 2px steelblue;">
                                                    <div class="gridheaderleft" style="background-color:LightPink; color:Blue">
                                                        Product Map
                                                    </div>
                                                    <div class="boxcontenttext" style="background: White;">
                                                        <div id="Div1">
                                                            <table>
                                                                <tr>
                                                                    <td class="space" align="left">
                                                                        <asp:Label ID="Label3" runat="server" SkinID="lblMand">Product 1</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td class="space" align="left">
                                                                        <asp:DropDownList ID="ddlProd1" Width="120px" runat="server" SkinID="ddlRequired">
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
                                                                        <asp:DropDownList ID="ddlProd2"  Width="120px" runat="server" SkinID="ddlRequired">
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
                                                                        <asp:DropDownList ID="ddlProd3"  Width="120px" runat="server" SkinID="ddlRequired">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="space" align="left">
                                                                        <asp:Label ID="Label13" runat="server"  SkinID="lblMand">Product 4</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td class="space" align="left">
                                                                        <asp:DropDownList ID="ddlProd4"  Width="120px" runat="server" SkinID="ddlRequired">
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
                                                                        <asp:DropDownList ID="ddlProd5"  Width="120px" runat="server" SkinID="ddlRequired">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </center>
        <br />
        <asp:Panel ID="pnl" runat="server" align="Center" Visible="false">
        </asp:Panel>
        <br />
        <center>
            <asp:Button ID="btnsubmit" runat="server" BackColor="LightBlue" Text="Send to Approve"
                Width="130px" OnClick="btnsubmit_Click"  />
        </center>
    </div>
    </form>
</body>
</html>
