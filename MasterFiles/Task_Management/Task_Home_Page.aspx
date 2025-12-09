<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task_Home_Page.aspx.cs" Inherits="MasterFiles_Task_Management_Task_Home_Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
     #header, #mainContent, #footer{
    /*defaut all margin 0,  The browser calculates a margin*/
    margin:0 auto;
    width:100%;
    }
#header { 
    height:100px; 
    background:#8A0651; 
    border:2px;
    border-style:solid;
    /* header and main content distance 5 px */
  
    border-collapse:collapse;
    }
#mainContent { 
    position:relative; 
    height:600px;
    border:2px;
    border-style:solid;
    border-collapse:collapse;
     width:100%;
    }
#sidebar { 
    position:absolute;
    top:0;
    left:0; 
    width:23%; 
    height:600px; 
    background:#E9E9E9;
      border-left:2px;
      
   
    border-collapse:collapse;
    }
#content { 
    margin-left:23%;    
    width:77%;
    height:600px; 
    background:#F5F4EB;
   
   
    }
#footer { 
    height:60px; 
    background:#9c6;
    }
    input {
  display: none;
}


 @import url('https://fonts.googleapis.com/css?family=Source+Code+Pro');

body {
  background-image: url('');
  background-size : cover;
}
.nav {
  width : 240px;
  float : left;
  -webkit-transition : 0.3s cubic-bezier(0.175, 0.885, 0.32, 1.275) all;
  -moz-transition : 0.3s cubic-bezier(0.175, 0.885, 0.32, 1.275) all;
}



   @import url('https://fonts.googleapis.com/css?family=Source+Code+Pro');

body {
  background-image: url('');
  background-size : cover;
}
.nav {
  width : 240px;
  float : left;
  -webkit-transition : 0.3s cubic-bezier(0.175, 0.885, 0.32, 1.275) all;
  -moz-transition : 0.3s cubic-bezier(0.175, 0.885, 0.32, 1.275) all;
}

input {
  display: none;
}


li {
  list-style-type : none;
  border-top    : 1px solid #FFF;
  border-bottom : 1px solid #ddd;
  background-color: #f4f5f6;
  background-size : 200% 100%;
  background-position : 90% 0px;
  -webkit-transition : 0.3s cubic-bezier(0.175, 0.885, 0.32, 1.275) all;
  -moz-transition : 0.3s cubic-bezier(0.175, 0.885, 0.32, 1.275) all;
}

a {
  display    : block;
  padding    : 25px 0;
  color      : #454545;
  text-align : center;
  font-family: 'Source Code Pro', sans-serif;
  text-decoration: none;
  -webkit-transition : color .1s;
  -moz-transition : color .1s;
}

a:hover {
  color : #fff;
  -webkit-transition : color .1s;
  -moz-transition : color .1s;
}

li:nth-child(1) {
  border-top : none;
  background-image : -webkit-linear-gradient(left, #78cdce 0%, #78cdce 50%, #f4f5f6 0%);
  background-image : -moz-linear-gradient(left, #78cdce 0%, #78cdce 50%, #f4f5f6 0%);
}

li:nth-child(2) {
  background-image : -webkit-linear-gradient(left, #f16767 0%, #f16767 50%, #f4f5f6 0%);
  background-image : -moz-linear-gradient(left, #f16767 0%, #f16767 50%, #f4f5f6 0%);
}



li:nth-child(3) {
  background-image : -webkit-linear-gradient(left, #97cc69 0%, #97cc69 50%, #f4f5f6 0%);
  background-image : -moz-linear-gradient(left, #97cc69 0%, #97cc69 50%, #f4f5f6 0%);
}

li:nth-child(4) {
  background-image : -webkit-linear-gradient(left, #507abd 0%, #507abd 50%, #f4f5f6 0%);
  background-image : -moz-linear-gradient(left, #507abd 0%, #507abd 50%, #f4f5f6 0%);
}

li:nth-child(5) {
  background-image : -webkit-linear-gradient(left, #6b408e 0%, #6b408e 50%, #f4f5f6 0%);
  background-image : -moz-linear-gradient(left, #6b408e 0%, #6b408e 50%, #f4f5f6 0%);
  border-bottom : none;
}

li:nth-child(6) {
  background-image : -webkit-linear-gradient(left, #f5ee32 0%, #f5ee32 50%, #f4f5f6 0%);
  background-image : -moz-linear-gradient(left, #f5ee32 0%, #f5ee32 50%, #f4f5f6 0%);
}


li:hover {
  -webkit-transition : 0.3s cubic-bezier(0.175, 0.285, 0.32,1.0) all;
  -moz-transition : 0.3s cubic-bezier(0.175, 0.285, 0.32,1.0) all;
  background-position : 0% 0%;
}

#toggle:checked ~ nav {
  margin-left:  -240px;
  -webkit-transition : 0.3s cubic-bezier(0.175, 0.885, 0.32, 1.275) all;
  -moz-transition : 0.3s cubic-bezier(0.175, 0.885, 0.32, 1.275) all;
}
p {
    text-transform: uppercase;
    letter-spacing: .5em;
    display: inline-block;
    color:White;
    padding: .5em 0em;
    position: absolute;
    top: 1%;
    left: 50%;
    width: 70em;
    margin: 0 0 0 -20em;
}
  span {

    font: 700 4em/1 "Oswald", sans-serif;
    letter-spacing: 0;
    padding: .25em 0 .325em;
      display: block;
      margin: 0 auto;
    text-shadow: 0 0 80px rgba(255,255,255,.5);

/* Clip Background Image */

  

/* Animate Background Image

      -webkit-text-fill-color: transparent;
      -webkit-animation: aitf 80s linear infinite;

/* Activate hardware acceleration for smoother animations

      -webkit-transform: translate3d(0,0,0);
      -webkit-backface-visibility: hidden;
       */
  }




@-webkit-keyframes aitf {
    0% { background-position: 0% 50%; }
    100% { background-position: 100% 50%; }
}
 td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
    .mydropdownlist
{
    

font-size: 20px;
padding: 5px 10px;
border-radius: 5px;
/*
color: #fff;
background-color: #cc2a41;*/
font-weight: bold;
}
    </style>
       <script type="text/javascript" language="javascript">
           var today = new Date();
           var lastDate = new Date(today.getFullYear(), today.getMonth(0) - 1, 31);
           var year = today.getFullYear() - 1;


           var dd = today.getDate();
           var mm = today.getMonth() + 01; //January is 0!
           var yyyy = today.getFullYear();

           var j = jQuery.noConflict();
           j(document).ready(function () {
               j('.DOBDate').datepicker
            ({
                minDate: dd + '/' + mm + '/' + yyyy,
                //                minDate: new Date(year, 11, 1),
                //                maxDate: new Date(year, 11, 31),

                dateFormat: 'dd/mm/yy'
                //                yearRange: "2010:2017",
            });

               j('.FROMDate').datepicker
            ({
                //                minDate: new Date(year, 11, 1),
                //                maxDate: new Date(year, 11, 31),

                dateFormat: 'dd/mm/yy'
                //                yearRange: "2010:2017",
            });
           });              
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="header">
            <p>
                <span>Task Management
                
                </span>
               
            </p>
        </div>
        <div id="mainContent">
            <div id="sidebar">
      <ul class="nav">
                    <li class="active" ><a href="Default.aspx" target="">Home</a></li>
                    <li style=" font-size:20px;font-weight:bold;color:Red"><a href="Task_Home_Page.aspx" target="">Assign</a></li>
                    <li ><a href="" target="">Status Updation</a></li>
                    <li ><a href="" target="">Status Tracking</a></li>
                    <li><a href="" target="">Back</a></li>
                    <%--<li style="width: 16.6666666666667%"><a href="6.html" target="">6</a></li>--%>
                </ul>
            </div>
            <div id="content">
                <%--   <span style="margin-left:40%">
   
  <asp:Label ID="lbl1" runat="server" Font-Bold="true" Font-Size="26px"  BackColor="Green" ForeColor="#f16767" >TASK ASSIGNMENT</asp:Label>
  </span>--%>
                <span style="margin-left: 30%">
                    <asp:Label ID="lblsf" runat="server" Font-Bold="true" Font-Size="22px" ForeColor="BlueViolet"></asp:Label>
                </span>
                <table>
                    <tr>
                        <td align="left" style="padding-bottom: 10px; padding-right: 10px;">
                            <asp:Label ID="lblFilter" runat="server" Text="Task Assign To" Font-Bold="true" Font-Size="16px" Font-Names="verdana" ForeColor="BlueViolet"></asp:Label>
                        </td>
                        <td align="left" style="padding-bottom: 10px; padding-right: 10px;">
                        
                          
                            <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="mydropdownlist">
                            </asp:DropDownList>
                         
                          
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                            </asp:DropDownList>
                          
                        </td>
                      
                    </tr>
                    <tr>
                      <td align="left" class="stylespc">
                         <asp:Label ID="Label1" runat="server" Text="Priority" Font-Bold="true" Font-Size="16px" Font-Names="verdana" ForeColor="BlueViolet"></asp:Label>
                        </td>
                          <td align="left" class="stylespc">
                             <asp:DropDownList ID="ddlPri" runat="server" CssClass="mydropdownlist">
                             <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                             <asp:ListItem Value="H" Text="High"></asp:ListItem>
                             <asp:ListItem Value="M" Text="Medium"></asp:ListItem>
                             <asp:ListItem Value="L" Text="Low"></asp:ListItem>
                            </asp:DropDownList>
                         
                        </td>
                    </tr>
                    <tr>
                      <td align="left" class="stylespc">
                         <asp:Label ID="Label2" runat="server" Text="Mode of Task" Font-Bold="true" Font-Size="16px" Font-Names="verdana" ForeColor="BlueViolet"></asp:Label>
                        </td>
                          <td align="left" class="stylespc">
                             <asp:DropDownList ID="ddlmode" runat="server" CssClass="mydropdownlist">
                             <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                             <asp:ListItem Value="H" Text="High"></asp:ListItem>
                             <asp:ListItem Value="M" Text="Medium"></asp:ListItem>
                             <asp:ListItem Value="L" Text="Low"></asp:ListItem>
                            </asp:DropDownList>
                         
                        </td>
                    </tr>
                     <tr>
                      <td align="left" class="stylespc">
                         <asp:Label ID="Label3" runat="server" Text="Dead Line From" Font-Bold="true" Font-Size="16px" Font-Names="verdana" ForeColor="BlueViolet"></asp:Label>
                        </td>
                          <td align="left" class="stylespc">
                                  <asp:TextBox ID="txtFrom" runat="server" SkinID="MandTxtBox" CssClass="FROMDate"
                        onkeypress="Calendar_enter(event);" onfocus="this.style.backgroundColor='#E0EE9D'"
                        onblur="this.style.backgroundColor='White'" TabIndex="6"></asp:TextBox>
                         
                        </td>
                    </tr>
                     <tr>
                      <td align="left" class="stylespc">
                         <asp:Label ID="Label4" runat="server" Text="Task Description" Font-Bold="true" Font-Size="16px" Font-Names="verdana" ForeColor="BlueViolet"></asp:Label>
                        </td>
                          <td align="left" class="stylespc">
                            <asp:TextBox ID="txtdes" runat="server" TextMode="MultiLine" Width="600px" Height="200px"></asp:TextBox>
                         
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
         
                
    </form>
</body>
</html>
