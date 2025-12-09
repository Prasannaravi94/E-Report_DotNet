<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mob_App_Setting2.aspx.cs" Inherits="MasterFiles_Options_Mob_App_Setting2" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>App Setting</title>
     <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
     <style type="text/css">
        .spc
        {
            padding-left: 5%;
        }
        .spc1
        {
            padding-left: 10%;
        }
        
        .box
        {
            background: #FFFFFF;
            border: 5px solid #427BD6;
            border-radius: 8px;
        }

       
        </style>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <br />

     <table  border="1" width="60%" align="center" style="margin-left: 20%"  class="box" >
         <tr>
             <td align="center">
                 <asp:Label ID="lblhead" runat="server" Text="#" Font-Bold="true" Font-Italic="true"></asp:Label>
             </td>
              <td align="center">
                 <asp:Label ID="lblhead2" runat="server" Text="OBJECTIVE" Font-Bold="true" Font-Italic="true"></asp:Label>
             </td>
               <td align="center">
                 <asp:label ID="lblhead3" runat="server" Text="YES/NO" Font-Bold="true" Font-Italic="true"></asp:label>
             </td>
         </tr>
        
          <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno" runat="server" Text="1" Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl1" runat="server" Text="RCPA Entry Option as Mandatory" ></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
          <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         
          <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno1" runat="server" Text="2"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl2" runat="server" Text="'Missed Date' Entry Option Needed in Mobile App"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio2" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
         <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno2" runat="server" Text="3"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_3" runat="server" Text="Doctor Categorywise Visist Control Needed"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio3" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
          <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno3" runat="server" Text="4"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_4" runat="server" Text="Internal Communication Option Needed"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio4" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
         <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno4" runat="server" Text="5"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_5" runat="server" Text="Circular Option Needed"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio5" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
          <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno5" runat="server" Text="6"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_6" runat="server" Text="Dr-Call Feed Back Entry as Mandatory"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio6" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
         <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno6" runat="server" Text="7"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_7" runat="server" Text="Dr-Call Product Entry as Mandatory"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio7" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
         <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno7" runat="server" Text="8"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_8" runat="server" Text="Dr-Call Input Entry as Mandatory"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio8" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
         <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno8" runat="server" Text="9"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_9" runat="server" Text="Dr-Call Sample Quantity Entry as Mandatory"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio9" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
          <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno9" runat="server" Text="10"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_10" runat="server" Text="Dr-Call-Rx Quantity Entry Needed"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio10" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
          <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
          <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno17" runat="server" Text="11"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_17" runat="server" Text="Dr-Call-Rx Entry as Needed/Not"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio17" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
          <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno10" runat="server" Text="12"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_11" runat="server" Text="Dr-Call-Rx Quantity Caption"></asp:Label>
          
      </td>
              <td align="left">
                 <asp:TextBox ID="txtrxqty" runat="server" MaxLength="20" Text="Rx Qty"></asp:TextBox>
             </td>
         </tr>
          <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno11" runat="server" Text="13"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_12" runat="server" Text="Dr-Call-Sample Quantity Caption"></asp:Label>
          
      </td>
              <td align="left">
                 <asp:TextBox ID="txtsampqty" runat="server" MaxLength="20" Text="Sample Qty"></asp:TextBox>
             </td>
         </tr>
         <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
        <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno12" runat="server" Text="14"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="Label2" runat="server" Text="Dr-POB Entry Option as Mandatory"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio11" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
         <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno13" runat="server" Text="15"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_14" runat="server" Text="'Start' & 'Stop' (for Attendance)button display"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio14" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
        <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno14" runat="server" Text="16"  Height="30px"></asp:Label>
             </td>
      <td align="left">
          <asp:Label ID="lbl_15" runat="server" Text="MCL Details updation Needed"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio15" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
         </tr>
        <tr><td>
       <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
       <td> <table style="width:100%;background-color:gray;"><tr><td></td></tr></table></td>
        </tr>
         <tr>
             <td align="left">
                  <asp:Label ID="lbl_sno15" runat="server" Text="17"  Height="30px"></asp:Label>
             </td>



      <td align="left">
          <asp:Label ID="llb_16" runat="server" Text="Chemist-POB Entry Option as Mandatory"></asp:Label>
      </td>
              <td align="left">
                 <asp:RadioButtonList ID="Radio16" runat="server" Font-Names="Verdana" RepeatDirection="Horizontal">
                   <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                   <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
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
