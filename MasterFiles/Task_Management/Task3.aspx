<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task3.aspx.cs" Inherits="MasterFiles_Task_Management_Task3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    * {
  box-sizing: border-box;
}

html, body {
  margin: 0;
  padding: 0;
  width: 100%;
  width: 100%;
}

#wrapper {
  display: block;
  position: fixed;
  width: 100%;
  height: 100%;
}

#leftWrapper {
  display: inline-block;
  position: absolute;
  left: 0;
  margin: 0;
  padding: 0;
  width: 15em;
  height: 100%;
  
  background-color: #1e1e1e;
}

#listView {
  display: block;
  position: relative;
}

#listView li {
  display: block;
  list-style: none;
}

#listView li a {
  display: block;
  padding: 20px;
  
  color: #fff;
  font-family: sans-serif;
  font-size: 1.1em;
  text-decoration: none;
  
  border-top: 1px solid rgba(0,0,0,0.5);
  border-bottom:1px solid rgba(255,255,255,0.04);
  
  -webkit-transition: all .20s ease;
  -moz-transition: all .20s ease;
  -o-transition: all .20s ease;
  transition: all .20s ease;
}

#rightWrapper {
  display: inline-block;
  position: absolute;
  left: 15em;
  margin: 0;
  padding: 0px 40px;
  width: calc(100% - 15em); 
  height: 100%;
  overflow-y: scroll;
  
  background-color: #efefef;
  
  -webkit-transition: all .20s ease;
  -moz-transition: all .20s ease;
  -o-transition: all .20s ease;
  transition: all .20s ease;
}

#header {
  display: block;
  position: fixed;
  left: 15em;
  width: 100%;
  z-index: 999;
  
  background-color: #efefef;
  border-bottom:2px solid rgba(0,0,0,0.2);
  
  -webkit-transition: all .20s ease;
  -moz-transition: all .20s ease;
  -o-transition: all .20s ease;
  transition: all .20s ease;
}

#fullPage {
  display: inline-block;
  margin-left: 20px;
  padding: 10px;
  
  color: #1e1e1e;
  font-family: sans-serif;
  font-size: 2em;
  font-weight: bold;
  text-decoration: none;
  
  -webkit-transform: rotate(90deg);
  -moz-transform: rotate(90deg);
  -o-transform: rotate(90deg);
  transform: rotate(90deg);
}

#contentWrapper {
  display: block;
  position: relative;
  margin-top: 100px;
  width: 100%;
  height: 100%;
}

article {
  margin-bottom: 20px;
  
  color: #1e1e1e;
  font-family: sans-serif;
  font-size: 1em;
  text-decoration: none;
  
  background-color: #fff;
  border: 1px solid rgba(0,0,0,0.1);
  border-bottom: 2px solid rgba(0,0,0,0.1);
}

section {
  padding: 10px;
}

.article-header {
  padding: 10px;
  
  color: #fff;
  font-family: sans-serif;
  font-size: 1.5em;
  text-align: left;
  
  background-color: #2DCC70; 
}

#showCase, #showCase section, #showCase section a {
  display: inline-block;
  padding: 10px;
  vertical-align: top;
  
  color: #1e1e1e;
  font-family: sans-serif;
  font-size: 1em;
  text-align: center;
  text-decoration: none;
  
  -webkit-transition: all .20s ease;
  -moz-transition: all .20s ease;
  -o-transition: all .20s ease;
  transition: all .20s ease;
}

#showCase {
  padding: 0;
}

#showCase section a {
  box-shadow: inset 0px -2px #2DCC70;
}

#showCase section a:hover {
  color: #fff;
  box-shadow: inset 0px -50px #2DCC70;
}

#social {
  text-align: center;
}

#social a {
  display: inline-block;
  width: 60px;
  height: 60px;
  box-shadow: inset 0px 0px;
}

#social a:hover {
  box-shadow: inset 0px 0px;
}

#social a img {
  max-width: 100%;
}


.full-page {
  left: 0 !important;
  width: 100% !important;
}

.list-item-active:after {
  content: "";
  position: absolute;
  margin-top: -65px; 
  margin-left: 13.45em;
  
  width: 0; 
  height: 0; 
  border-top: 33px solid transparent;
  border-bottom: 33px solid transparent;
  border-right: 25px solid #2DCC70;
  
  -webkit-transition: all .20s ease;
  -moz-transition: all .20s ease;
  -o-transition: all .20s ease;
  transition: all .20s ease;
}
    </style>
  
</head>
<script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
<script src="../../DashBoard/JS/jquery-1.7.2.min.js" type="text/javascript"></script>
 <script type="text/javascript">
     $(function () {
         $("#fullPage").click(function () {
             $("#rightWrapper").toggleClass("full-page");
             $("#header").toggleClass("full-page");
         });
     })

     $(function () {
         $("#listView li").click(function () {
             if ($("#listView li").hasClass("list-item-active")) {
                 $("#listView li").removeClass("list-item-active");
             }
             $(this).addClass("list-item-active");
         });
     });

    
    </script>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="wrapper">
    
  <div id="leftWrapper">
 
      <div id="listView" class="list">
       
        <li class="list-item-active" id="lihome" onclick="lihome"><a href="#">Home</a></li>
        <li><a href="#">Assign</a></li>
        <li><a href="#">Status Updation</a></li>
        <li><a href="#">Status Tracking</a></li>
        <li><a href="#">Back</a></li>
      
      </div>
    </div>

    <div id="rightWrapper">
      <div id="header"><a id="fullPage" href="#">|||</a>
  <span style="font-size:50px;color:Blue;font-weight:bold">Task Management System</span>
      </div>
       
      <div id="contentWrapper">
        <article id="showCase">
        <%--  <div class="article-header">Home</div>--%>
         
        </article>
        
        
          
      </div>
    </div>
</div>
 
    </div>
    </form>
</body>
</html>
