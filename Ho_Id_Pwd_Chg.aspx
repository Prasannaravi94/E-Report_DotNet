<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ho_Id_Pwd_Chg.aspx.cs" Inherits="Ho_Id_Pwd_Chg" %>

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
         <br />
                        <br />
                        <br />
        <center>
            <div class="roundbox boxshadow" style="width: 60%; border: solid 2px steelblue;">
                <div class="gridheaderleft">
                    Password - Change
                </div>
                <div class="boxcontenttext" style="background: White;">
                    <div id="pnlPreviewSurveyData">
                       <br />
                        <table>
                            <tr style="height: 25px;">
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label1" runat="server" Text="Old Password" Font-Size="14px" Font-Bold="true"  ForeColor="BlueViolet"></asp:Label>
                                </td>
                                <td align="left" class="stylespc">
                                    <asp:TextBox ID="txtOldPwd" runat="server" SkinID="MandTxtBox" MaxLength="15" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px;" class="stylespc">
                                <td align="left">
                                    <asp:Label ID="Label2" runat="server" Text="New Password" Font-Size="14px" Font-Bold="true"  ForeColor="BlueViolet"></asp:Label>
                                </td>
                                <td align="left" class="stylespc">
                                    <asp:TextBox ID="txtNewPwd" runat="server" SkinID="MandTxtBox" MaxLength="15" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px;" class="stylespc">
                                <td align="left">
                                    <asp:Label ID="Label3" runat="server" Text="Confirm Password" Font-Size="14px" Font-Bold="true"  ForeColor="BlueViolet"></asp:Label>
                                </td>
                                <td align="left" class="stylespc">
                                    <asp:TextBox ID="txtConfirmPwd" runat="server" SkinID="MandTxtBox" MaxLength="15"
                                        TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />

                          <asp:Button ID="btnGo" runat="server" Width="70px" Height="25px" Text="Save" BackColor="Lightblue" onclick="btnGo_Click" />

                          <br />
                          <br />

                          <asp:Label ID="lblpol" Font-Bold="true" ForeColor="Red" Font-Size="16px" runat="server" >(For Security Policy, Change your Password every 45 days)</asp:Label>
                    </div>
                </div>
            </div>
        </center>
        <br />
        <br />
        <br />
        <center>
        <img src="Images/LoginIcon.jpg" alt="" />
        
        </center>
    </div>
    </form>
</body>
</html>
