<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
   
    <form id="form1" runat="server">
         <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.js"></script>
<script>
  function initFreshChat() {
    window.fcWidget.init({
      token: "65d3efbd-8e34-4db8-bd0b-dbcd2d844694",
      host: "https://wchat.freshchat.com"
    });
  }
  function initialize(i,t){var e;i.getElementById(t)?initFreshChat():((e=i.createElement("script")).id=t,e.async=!0,e.src="https://wchat.freshchat.com/js/widget.js",e.onload=initFreshChat,i.head.appendChild(e))}function initiateCall(){initialize(document,"freshchat-js-sdk")}window.addEventListener?window.addEventListener("load",initiateCall,!1):window.attachEvent("load",initiateCall,!1);
</script>
    <div>
    
    </div>
    </form>
</body>
</html>
