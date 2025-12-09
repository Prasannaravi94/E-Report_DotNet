<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mob_App_Setting3.aspx.cs" Inherits="MasterFiles_Options_Mob_App_Setting3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>App Setting</title>
     <script src="JsFiles/CommonValidation.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>

    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    
</head>

<body>
    <form id="form1" runat="server">
    <div>
         <ucl:Menu ID="menu1" runat="server" />
         
         <br />
        <br />
         <table id="table2" runat="server" align="center" style="background-color: #808080;margin-left: 25%">
                    <tr>
                       
                        <td align="center">

                            <table id="tbl" runat="server" align="center">
                                <tr>
                                    <td>
                                        <table id="tbl3" runat="server" align="center">
                                            <tr style="background-color: white">
                                                
                                                <td style="width: 60px" align="center">
                                                    <asp:Label runat="server" ID="lblh1" Text="#" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 450px" align="center">
                                                    <asp:Label runat="server" ID="lblh2" Text="Objective" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="center">
                                                    <asp:Label runat="server" ID="lblh3" Text="Yes/No" Font-Bold="true"></asp:Label>
                                                </td>
                                              
                                            </tr>

                                            <tr style="background-color: white">
                                                <td>  <asp:Label runat="server" ID="Label1" Text="1" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="lbl_tpdcr" Text="TP Based DCR  - Deviation"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                             <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label2" Text="2" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label3" Text="TP Entry mandatory"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio1" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                            <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label4" Text="3" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label5" Text="TP Start date Range"></asp:Label> </td>
                                                <td align="left">
                                              <asp:TextBox ID="txt_srtdate" runat="server" SkinID="TxtBxNumOnly" Width="60px" Height="22px" MaxLength="10"
                                            onkeypress="CheckNumeric(event);"></asp:TextBox>
                                               </td>
                                            </tr>

                                             <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label6" Text="4" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label7" Text="TP End date Range"></asp:Label> </td>
                                                <td align="left">
                                               <asp:TextBox ID="txt_enddate" runat="server" SkinID="TxtBxNumOnly" Width="60px" Height="22px" MaxLength="10"
                                               onkeypress="CheckNumeric(event);"></asp:TextBox>
                                               </td>
                                            </tr>

                                             <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label8" Text="5" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label9" Text="TP based my day plan"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio2" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                            <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label10" Text="6" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label11" Text="Dr next visit needed /Not Needed"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio3" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                            <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label12" Text="7" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label13" Text="Dr next visit Entry Mandatory"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio4" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                             <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label14" Text="8" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label15" Text="All Approval  Mandatory"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio5" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                            <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label16" Text="9" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label17" Text="RCPA Qty Needed"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio6" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                             <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label18" Text="10" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label19" Text="Multiple  Doctor selection"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio7" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                            <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label20" Text="11" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label21" Text="Cluster name Changes"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio8" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                              <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label22" Text="12" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label23" Text="All Product display"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio9" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                             <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label24" Text="13" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label25" Text="Dr speciality wise product details"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio10" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                             <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label26" Text="14" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label27" Text="Focused Product - Dr wise product mapping"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio11" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>
                                             <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label28" Text="15" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label29" Text="Product - screen Stockist needed/not needed"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio12" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>


                                             <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label30" Text="16" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label31" Text="Other Text box needed in DCR entry page"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio13" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                             <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label32" Text="17" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label33" Text="Missed Date Having > 48hrs(2days) the Current Date DCR has beed Locaked"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio14" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>

                                             <tr style="background-color: white">
                                                <td><asp:Label runat="server" ID="Label34" Text="18" Font-Bold="true"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="Label35" Text="Separate RCPA entry Needed /Not"></asp:Label> </td>
                                                <td align="left">
                                               <asp:RadioButtonList ID="Radio15" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                              <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                               </asp:RadioButtonList>
                                               </td>
                                            </tr>
                                        </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
             </table>
       
       <center>
            <table>
                <tr><td><asp:Button ID="btn_save" runat="server" Text="Save" BackColor="LightBlue" OnClick="btn_save_click"/>
                </td></tr>
                </table>
            </center>
    
    </div>
    </form>
</body>
</html>
