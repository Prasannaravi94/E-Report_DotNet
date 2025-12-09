<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Security.aspx.cs" Inherits="Security" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <style type="text/css">
        *
        {
            margin:0px;
        }
        .hw
        {
            height:100%;
            width:100%;
        }
</style>
<script type="text/javascript">
    function noBack() {
        window.history.forward()
    }
    noBack();
    window.onload = noBack;
    window.onpageshow = function (evt) { if (evt.persisted) noBack() }
    window.onunload = function () { void (0) }
    </script>
</head>
<body>
  <form id="form1" runat="server">
   <div>
   <%--  <iframe id="frhome" src="https://torssfa.info//Index.aspx" style="border:none;overflow: hidden; height: 100%; width: 100%; position: absolute;" height="100%" width="100%" frameborder="0"></iframe>--%>
<br />
 
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<center>
 <asp:Label ID="lbl" runat="server" Font-Size="Large" Text="Today(07-11-2017) Site is Under maintenance from 2.00 PM to 3.15 PM" ForeColor="Red" Font-Bold="true"></asp:Label> 
</center>
<center>
 <asp:Label ID="lbl1" runat="server" Font-Size="Large" Text="Network Updation is going on....." ForeColor="BlueViolet" Font-Bold="true"></asp:Label> 
</center>

    </div>
    </form>
</body>
</html>
