<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LicenceKey_View.aspx.cs" Inherits="MasterFiles_LicenceKey_View" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>License Key View</title>
    <style>
        
        .header {
            text-align: center; 
            margin-top: 100px;   
            font-size: 20px;    
            font-weight: bold;   
            color: #333;
            text-decoration: underline;
        }

        .mGrid {
            border: solid 1px black;
            border-collapse: collapse;
        }

        .mGrid th, .mGrid td {
            padding: 10px;
            border: solid 1px black;
            text-align: center;
        }

        .mGrid th {
            background-color: #A6A6D2;
            color: white;
            font-weight: normal;
            font-size: small;
            font-family: Calibri;
        }

        .pgr {
            font-size: 12px;
            text-align: center;
            color: #000;
        }

        .alt {
            background-color: #f2f2f2;
        }

        .grid-header {
            color: white;
            background-color: #333;
            font-weight: bold;
            text-align: center;
        }

        .grid-link {
            color: darkblue;
            font-size: xx-small;
            font-family: Verdana, sans-serif;
            font-weight: bold;
        }

        .grid-link:hover {
            color: darkred;
        }

        .grid-row {
            color: #333;
            text-align: center;
        }

        .grid-empty {
            color: black;
            background-color: AliceBlue;
            height: 5px;
            border-color: black;
            border-width: 2px;
            text-align: center;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <div class="header">
        License Key View
    </div>

    <form id="form1" runat="server">
        <div style="width: 100%; padding-left: 20px; padding-right: 20px;">
            <table align="center" style="width: 100%; border-collapse: collapse; margin-top: 30px;">
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdlicense" runat="server" 
                            Width="60%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" EmptyDataText="No Records Found"
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            style="margin-left: 20px; margin-right: 20px;">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Division Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Name" runat="server" Text='<%# Bind("Division_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="License Key" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Key" runat="server" Text='<%# Bind("Division_Add2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
