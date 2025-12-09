<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MR_DCR_Approval.aspx.cs" Inherits="MasterFiles_MR_DCR_MR_DCR_Approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>MR - DCR Approval</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
</head>
<body style="background:white">
    <form id="form1" runat="server">
         <div>
         <center>
               <%--  <table id="Heading" runat="server" style="width: 95%;  font-family:Bookman Old Style; font-size:Small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
             
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                       <td align="center" >
                           <asp:Label ID="Label1" runat="server" Font-Size="Medium" Font-Names="Bookman Old Style"   Font-Underline ="true"  Font-Bold = "true" text="DCR Approval "></asp:Label>
                          
                        </td>
                         <td align="right"><asp:Button ID="btnBack" Text="Back" runat="server" 
                            onclick="btnBack_Click" /></td>
                    </tr>                        
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
             
                    </table>--%>
                     <br />
                    <center>
                      <asp:Label ID="Label2" runat="server" Font-Size="Medium" Font-Names="Bookman Old Style"   Font-Underline ="true"  Font-Bold = "true" text="DCR Approval "></asp:Label>
                    </center>
                         <asp:Panel ID="pnlback" runat="server" HorizontalAlign="Right" Width="97%">
                         <asp:Button ID="btnBack" Text="Back" Width="60px" Height="25px" BackColor="LightBlue" runat="server" 
                            onclick="btnBack_Click" />
                         </asp:Panel>
                    <br />
                 <table id="tblPreview_LstDoc" runat="server" style="width: 80%;  font-family:Bookman Old Style; font-size:x-small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
             
                    <tr>
                        <td align="center">
                           <asp:Label ID="lblText" runat="server" Font-Size="Medium" Font-Names="Bookman Old Style"  ForeColor ="Black" 
                                text="Daily Calls Entry For  "></asp:Label>
                           <asp:Label ID="lblHeader" runat="server" Font-Size="Medium" Font-Names="Bookman Old Style" ForeColor ="Black" ></asp:Label>
                        </td>
                    </tr>                        
                    <tr>
                        
                        <td>&nbsp;</td>
                       </tr>
                        <tr>
                        <td align = "left" >
                           <asp:Label ID="lblworktype" runat="server" Font-Size="Small" Font-Names="Bookman Old Style"  Font-Bold ="true" 
                                text="Work Type : "></asp:Label>
                           <asp:Label ID="lblwt" runat="server" ForeColor="Red" BackColor="Yellow" Font-Size="Small" Font-Names="Bookman Old Style"  Font-Bold ="true" ></asp:Label>
                        </td>
                    </tr>
             
                    </table>
                <table id="Table1" runat="server" style="width: 95%;  font-family:Bookman Old Style; font-size:Small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblld" runat="server" Text="Listed Doctor Details" 
                                Font-Size="Small" Font-Names="Bookman Old Style"  Font-Underline="True"></asp:Label>
                            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%" >
                            </asp:Table>
                        </td>
                    </tr>
                </table>

                <br />
                 <table id="tblPreview_Chem" runat="server" style="width: 95%;  font-family:Bookman Old Style; font-size:Small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td align="center">
                         <asp:Label ID="lblch" runat="server" Text="Chemists Details" Font-Size="Small" Font-Names="Bookman Old Style"  Font-Underline="true"></asp:Label>
                            <asp:Table ID="tblChem" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%">
                            </asp:Table>
                        </td>
                    </tr>

                </table>
                <br />
                 <table id="tblPreview_Stock" runat="server" style="width: 95%;  font-family:Tahoma; font-size:Small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td align="center">
                         <asp:Label ID="lblst" runat="server" Text="Stockiests Details" Font-Size="Small" Font-Names="Verdana"  Font-Underline="true"></asp:Label>
                            <asp:Table ID="tblstk" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                 <table id="tblPreview_UnLstDoc" runat="server" style="width: 95%;  font-family:Bookman Old Style; font-size:Small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td align="center">
                         <asp:Label ID="lblunls" runat="server" Text="UnListed Doctor Details" Font-Size="Small" Font-Names="Bookman Old Style"  Font-Underline="true"></asp:Label>
                            <asp:Table ID="tblunlst" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <br />
               <br />
                 <table id="tblPreview_Hos" runat="server" style="width: 95%;  font-family:Bookman Old Style; font-size:Small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td align="center">
                           <asp:Label ID="lblhos" runat="server" Text="Hospital Details" Font-Size="Small" Font-Names="Bookman Old Style"  Font-Underline="true" ></asp:Label>
                            <asp:Table ID="tblhos" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <br />
              
                 <br />
                 <table id="tblPreview_Remarks" runat="server" style="width: 95%;  font-family:Bookman Old Style; font-size:Small;" cellspacing="0" cellpadding= "0">
                  
                    <tr>
                         <td align="center">
                         <asp:Label ID="lblRemarks" runat="server" Text="Remarks" Font-Size="Small" Font-Names="Bookman Old Style" Visible ="false"   Font-Underline="true" ></asp:Label>
                        </td>
                        </tr>
                          <tr>
                       <td align="center">
                            <asp:TextBox ID="RevPreview" runat="server" Width = "1180" BorderStyle="Groove"  ></asp:TextBox >
                                              </td>
                    </tr>
                </table>
                 
                   <br />  
                   
                   </center>          
                <div style="margin-left:40%">
                    <asp:Button ID="btnSave" CssClass="savebutton" runat="server" Visible="false" Text="Approve DCR" OnClick="btnSave_Click" />
                    &nbsp
                    <asp:Button ID="btnReject" CssClass="savebutton" runat="server" Visible="false" Text="Reject DCR" OnClick="btnReject_Click" />
                    &nbsp                               
                    <asp:TextBox ID="txtReason" Width="400" Height="45" Visible="false" TextMode="MultiLine" runat="server"></asp:TextBox>
                    &nbsp
                     <asp:Button ID="btnSubmit" CssClass="savebutton" runat="server" Visible="false" Text="Back to Field Force" OnClick="btnSubmit_Click" />
                </div>
    </div>
    </form>
</body>
</html>
