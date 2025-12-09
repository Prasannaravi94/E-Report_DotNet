<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotificationMsg_delete.aspx.cs" Inherits="MasterFiles_NotificationMsg_delete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listed Doctor Addition - Approval</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/Grid.css" />--%>
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
    <style type="text/css">
        body {
            font-size: 12pt;
        }

        .Grid th {
            color: #fff;
            background-color: green;
        }
        /* CSS to change the GridLines color */
        .Grid, .Grid th, .Grid td {
            font-size: small;
            font-family: Calibri;
            font-weight: bold;
            border: 1px solid;
        }

        .GridHeader {
            text-align: center !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
        <center>
           <U> <asp:Label ID="lblhead" runat="server" Text="Delete Of Notification Message" Font-Bold="true"></asp:Label></U>
            </center>
<br />
            <center>
                <table>

                    <tr>
                        <td align="center" width="500px" class="stylespc">
                            <asp:Label ID="lblmon" runat="server" Text="Select the Month"></asp:Label>&nbsp;
                   <asp:DropDownList ID="ddlMonth" runat="server" Width="80px" SkinID="ddlRequired">
                       <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
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
                            &nbsp;&nbsp;
<asp:Label ID="lblyr" runat="server" Text="Select the Year"></asp:Label>&nbsp;
                        <asp:DropDownList ID="ddlYear" runat="server" Width="80px" SkinID="ddlRequired">
                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;
                            <asp:Button ID="btnpgo" runat="server" Text="Go" OnClick="btnlink_goclick" />
                        </td>
                    </tr>

                </table>
            </center>
            <br />

            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdNotMsg" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                AutoGenerateColumns="false" GridLines="None" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
                                HeaderStyle-BackColor="#ededea"
                                AllowSorting="True">
                                <HeaderStyle Font-Bold="False" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" Text="  Select All" onclick="checkAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkmsg" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trans_Sl_No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrans_Sl_No" runat="server" Text='<%#Eval("Trans_Sl_No")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderText="From date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFrom_Date" runat="server" Text='<%#Eval("From_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="To date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTo_Date" runat="server" Text='<%#Eval("To_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notification Message">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotMSG" runat="server" Text='<%#Eval("Notification_Message")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
        <br />
        <center>
            <asp:Button ID="btnDelete" runat="server" CssClass="BUTTON" Text="delete" Width="170px" Visible="false" OnClick="btnDelete_Click" OnClientClick="return confirm('Do you want to delete notification message(s)?') && confirm('Are you sure want to delete permanently?');" />

        </center>

    </form>
</body>
</html>
