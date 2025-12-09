<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unique_DR_Tot_View.aspx.cs"
    Inherits="MasterFiles_Common_Doctors_Unique_DR_Tot_View" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listeddr Count View</title>
         <link type="text/css" rel="stylesheet" href="../../css/repstyle.css"/>
          <link type="text/css" rel="stylesheet" href="../../css/style.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
       <center>
       
        <h1 style="color:BlueViolet;font-weight:bold;text-decoration:underline;font-size:larger" >Existing DRs - at a Glance</h1>
        </center>
         <br />
       <center>
            <asp:Panel ID="Panel1" runat="server">
                <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                    AutoGenerateColumns="true" CssClass="mGrid" EmptyDataText="No Records Found"
                    GridLines="Both" HorizontalAlign="Center" BorderWidth="1" OnRowCreated="GrdFixation_RowCreated"
                    ShowHeader="False" Width="50%" Font-Names="calibri" OnRowDataBound="GrdFixation_RowDataBound"
                    Font-Size="Small">
                    <HeaderStyle Font-Bold="False" />
                    <PagerStyle CssClass="pgr" />
                    <SelectedRowStyle BackColor="BurlyWood" />
                    <AlternatingRowStyle CssClass="alt" />
                    <RowStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="Small" Font-Names="calibri" />
                    <Columns>
                    </Columns>
                    <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                        VerticalAlign="Middle" />
                </asp:GridView>
            </asp:Panel>
        </center>
         <br />
          <center>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Uploaded Unique Drs + New : " ForeColor="BlueViolet"
                        Font-Bold="true"></asp:Label>
                        
                </td>
                <td>
                    <asp:Label ID="lblUni" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label> +
                     <asp:Label ID="lblNew" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label> = 
                       <asp:Label ID="lbltot" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Selected Unique Drs : " ForeColor="BlueViolet"
                        Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSelect" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    
                </td>
            </tr>
           <%-- <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="New Unique Drs : " ForeColor="BlueViolet"
                        Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNew" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                </td>
            </tr>
             <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="New Pending Drs : " ForeColor="BlueViolet"
                        Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblpen" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                </td>
            </tr>--%>
        </table>
        </center>
        <center>
        <br />
        <h1 style="color:BlueViolet;font-weight:bold;text-decoration:underline;font-size:larger">New Unique DRs - at a Glance</h1>
        </center>
        <br />
        <center>
            <asp:Panel ID="Panel2" runat="server">
                <asp:GridView ID="GrdNewUnique" runat="server" AlternatingRowStyle-CssClass="alt"
                    AutoGenerateColumns="true" CssClass="mGrid" EmptyDataText="No Records Found"
                    GridLines="Both" HorizontalAlign="Center" BorderWidth="1" OnRowCreated="GrdNewUnique_RowCreated"
                    ShowHeader="False" Width="50%" Font-Names="calibri" OnRowDataBound="GrdNewUnique_RowDataBound"
                    Font-Size="Small">
                    <HeaderStyle Font-Bold="False" />
                    <PagerStyle CssClass="pgr" />
                    <SelectedRowStyle BackColor="BurlyWood" />
                    <AlternatingRowStyle CssClass="alt" />
                    <RowStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="Small" Font-Names="calibri" />
                    <Columns>
                    </Columns>
                    <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                        VerticalAlign="Middle" />
                </asp:GridView>
            </asp:Panel>
        </center>
    </div>
    </form>
</body>
</html>
