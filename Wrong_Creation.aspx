<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Wrong_Creation.aspx.cs"
    Inherits="FlashNews_Design" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .roundCorner
        {
            border-radius: 25px;
            background-color: #4F81BD;
            text-align: center;
            font-family: arial, helvetica, sans-serif;
            padding: 5px 10px 10px 10px;
            font-weight: bold;
            width: 150px;
            height: 30px;
        }

        .mGrid
        {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }

            .mGrid td
            {
                padding: 2px;
                border: solid 1px Black;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                font-size: small;
                font-family: Calibri;
            }

            .mGrid th
            {
                padding: 4px 2px;
                color: white;
                background: #A6A6D2;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                border-bottom: solid 1px Black;
                font-weight: normal;
                font-size: small;
                font-family: Calibri;
            }

            .mGrid .pgr
            {
                background: #A6A6D2;
            }

                .mGrid .pgr table
                {
                    margin: 5px 0;
                }

                .mGrid .pgr td
                {
                    border-width: 0;
                    padding: 0 6px;
                    text-align: left;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: White;
                    line-height: 12px;
                }

                .mGrid .pgr th
                {
                    background: #A6A6D2;
                }

                .mGrid .pgr a
                {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover
                    {
                        color: #000;
                        text-decoration: none;
                    }

        td.stylespc
        {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {

                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;
                    }
                    else {

                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
                <tr>
                    <td>
                        <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize; font-size: 14px;"
                            ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="lbldiv" runat="server" Text="User" Style="text-transform: capitalize; font-size: 14px;"
                            ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnNext" runat="server" Width="150px" Height="30px" Text="Next to Home Page"
                            OnClick="btnHome_Click" BackColor="LightPink" ForeColor="Black" CssClass="roundCorner" />
                        &nbsp;&nbsp;
                    <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" Text="Goto Home Page" CssClass="roundCorner"
                        OnClick="btnHomepage_Click" BackColor="Green" ForeColor="White" />
                        &nbsp;&nbsp;
                    <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" Text="Logout" CssClass="roundCorner"
                        OnClick="btnLogout_Click" BackColor="Red" ForeColor="White" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
            </table>
            <br />
            <center>
                <asp:Label ID="lblHome" Font-Size="X-Large" ForeColor="Blue" Font-Bold="true" Font-Names="Verdana"
                    runat="server">Listed Doctor(s) Wrong Creation Details</asp:Label>
            </center>
            <br />
            <br />
            <center>
                <table width="100%" align="center">
                    <tbody>
                        <th>Listed Doctor Wrong Creation View</th>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="gvDoctor" runat="server" Width="70%" HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    EmptyDataText="No Records Found" AutoGenerateColumns="false" 
                                    GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" RowStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="False" />
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" Text="  Select All" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkListedDR" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="#" ItemStyle-Width="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Doctor Name"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Doctor Created Date"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCrtDate" runat="server" Text='<%#Eval("ListedDr_Created_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activity Date"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActDate" runat="server" Text='<%#Eval("Activity_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
                                <br /><br />
                                <asp:Button ID="btnProcess" CssClass="savebutton" runat="server" Width="60px" CommandName="Process"
                                    Height="25px" Text="Process" OnClick="btnProcess_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
