<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Standby.aspx.cs" Inherits="Standby" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <br />
    <center>
    <asp:Image runat="server" ID="img" ImageUrl="~/Images/Standby.jpg" />
    </center>
    </div>
    </form>

     <% if (Session["sf_type"].ToString() == "3")
        { %>
    <script src="https://sanebilling.info/JScript/sanbilling.js?v=<%= DateTime.Now.Ticks.ToString() %>" data-san-billing-divisions="<%= Session["division_code"].ToString() %>" data-san-billing-tenant-id="torssfa"></script>
    <% } %>

</body>
</html>
