<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DOB_DOW_ListedDr.aspx.cs"
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
                    runat="server">Listed Doctor(s) Date of Birth and Wedding Anniversary Details</asp:Label>
            </center>
            <br />
            <br />
            <center>
                <table width="100%" align="center">
                    <tbody>
                        <th>Date of Birth View</th>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="false" OnRowDataBound="grdDoctor_RowDataBound"
                                    GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    AllowSorting="True">
                                    <HeaderStyle Font-Bold="False" />
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSF" runat="server" Text='<%#Eval("sf_name")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhq" runat="server" Text='<%#Eval("sf_hq")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocName" ForeColor="Blue" runat="server" Text='<%#Eval("ListedDr_Name")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="ListedDR_Address1" onkeypress="AlphaNumeric(event);" runat="server" SkinID="lblMand" MaxLength="250"
                                                    Text='<%#Eval("ListedDR_Address1")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOB" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOB" ForeColor="Blue" Width="70px" runat="server" Text='<%# Bind("ListedDr_DOB") %>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone/Mobile" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob" runat="server" Text='<%# Bind("ListedDr_Mobile") %>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="100%" align="center">
                    <tbody>
                        <th>Date of Wedding View</th>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="grdDoctor_Dow" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="false" OnRowDataBound="grdDoctor_Dow_RowDataBound"
                                    GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    AllowSorting="True">
                                    <HeaderStyle Font-Bold="False" />
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSF" runat="server" Text='<%#Eval("sf_name")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDG" runat="server" Text='<%#Eval("Sf_Designation_Short_Name")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhq" runat="server" Text='<%#Eval("sf_hq")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocName" ForeColor="Blue" runat="server" Text='<%#Eval("ListedDr_Name")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="ListedDR_Address1" onkeypress="AlphaNumeric(event);" runat="server" SkinID="lblMand" MaxLength="250"
                                                    Text='<%#Eval("ListedDR_Address1")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblterr2" runat="server" Text='<%# Bind("territory_Name") %>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOW" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOW" ForeColor="BlueViolet" Width="70px" runat="server" Text='<%# Bind("ListedDr_DOW") %>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone/Mobile" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob" runat="server" Text='<%# Bind("ListedDr_Mobile") %>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="100%" align="center">
                    <tbody>
                        <th>Date of Birth/Date of Wedding View</th>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="grdDobDow" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="false" OnRowDataBound="grdDobDow_RowDataBound"
                                    GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    AllowSorting="True">
                                    <HeaderStyle Font-Bold="False" />
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSF" runat="server" Text='<%#Eval("sf_name")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhq" runat="server" Text='<%#Eval("sf_hq")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="ListedDR_Address1" onkeypress="AlphaNumeric(event);" runat="server" SkinID="lblMand" MaxLength="250"
                                                    Text='<%#Eval("ListedDR_Address1")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblterr1" runat="server" Text='<%# Bind("territory_Name") %>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOB" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOB1" runat="server" Width="70px" Text='<%# Bind("ListedDr_DOB") %>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOW" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOW1" runat="server" Width="70px" Text='<%# Bind("ListedDr_DOW") %>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone/Mobile" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblterr" runat="server" Text='<%# Bind("ListedDr_Mobile") %>' SkinID="lblMand"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
