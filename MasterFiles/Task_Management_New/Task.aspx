<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task.aspx.cs" Inherits="MasterFiles_Task_Management_Task" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    body   
{
    background: #b6b7bc;
    font-size: .80em;
    font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
    margin: 0px;
    padding: 0px;
    color: #696969;
}

a:link, a:visited
{
    color: #034af3;
}

a:hover
{
    color: #1d60ff;
    text-decoration: none;
}

a:active
{
    color: #034af3;
}

p
{
    margin-bottom: 10px;
    line-height: 1.6em;
}


/* HEADINGS   
----------------------------------------------------------*/

h1, h2, h3, h4, h5, h6
{
    font-size: 1.5em;
    color: #666666;
    font-variant: small-caps;
    text-transform: none;
    font-weight: 200;
    margin-bottom: 0px;
}

h1
{
    font-size: 1.6em;
    padding-bottom: 0px;
    margin-bottom: 0px;
}

h2
{
    font-size: 1.5em;
    font-weight: 600;
}

h3
{
    font-size: 1.2em;
}

h4
{
    font-size: 1.1em;
}

h5, h6
{
    font-size: 1em;
}

/* this rule styles <h1> and <h2> tags that are the 
first child of the left and right table columns */
.rightColumn > h1, .rightColumn > h2, .leftColumn > h1, .leftColumn > h2
{
    margin-top: 0px;
}


/* PRIMARY LAYOUT ELEMENTS   
----------------------------------------------------------*/

.page
{
    width: 98%;
    background-color: #fff;
    margin: 20px auto 0px auto;
    border: 1px solid #496077;
}

.header
{
    position: relative;
    margin: 0px;
    padding: 0px;
    background: #4b6c9e;
    width: 100%;
}

.header h1
{
    font-weight: 700;
    margin: 0px;
    padding: 0px 0px 0px 20px;
    color: #f9f9f9;
    border: none;
    line-height: 2em;
    font-size: 2em;
}

.main
{
    padding: 0px 12px;
    margin: 12px 8px 8px 8px;
    min-height: 600px;
}

.leftCol
{
    padding: 6px 0px;
    margin: 12px 8px 8px 8px;
    width: 200px;
    min-height: 200px;
}

.footer
{
    color: #4e5766;
    padding: 8px 0px 0px 0px;
    margin: 0px auto;
    text-align: center;
    line-height: normal;
}


/* TAB MENU   
----------------------------------------------------------*/

div.hideSkiplink
{
    background-color:#3a4f63;
    width:100%;
}

div.menu
{
    padding: 4px 0px 4px 8px;
}

div.menu ul
{
    list-style: none;
    margin: 0px;
    padding: 0px;
    width: auto;
}

div.menu ul li a, div.menu ul li a:visited
{
    background-color: #465c71;
    border: 1px #4e667d solid;
    color: #dde4ec;
    display: block;
    line-height: 1.35em;
    padding: 4px 20px;
    text-decoration: none;
    white-space: nowrap;
}

div.menu ul li a:hover
{
    background-color: #bfcbd6;
    color: #465c71;
    text-decoration: none;
}

div.menu ul li a:active
{
    background-color: #465c71;
    color: #cfdbe6;
    text-decoration: none;
}

/* FORM ELEMENTS   
----------------------------------------------------------*/

fieldset
{
    margin: 1em 0px;
    padding: 1em;
    border: 1px solid #ccc;
}

fieldset p 
{
    margin: 2px 12px 10px 10px;
}

fieldset.login label, fieldset.register label, fieldset.changePassword label
{
    display: block;
}

fieldset label.inline 
{
    display: inline;
}

legend 
{
    font-size: 1.1em;
    font-weight: 600;
    padding: 2px 4px 8px 4px;
}

input.textEntry 
{
    width: 320px;
    border: 1px solid #ccc;
}

input.passwordEntry 
{
    width: 320px;
    border: 1px solid #ccc;
}

div.accountInfo
{
    width: 42%;
}

/* MISC  
----------------------------------------------------------*/

.clear
{
    clear: both;
}

.title
{
    display: block;
    float: left;
    text-align: left;
    width: auto;
}

.loginDisplay
{
    font-size: 1.1em;
    display: block;
    text-align: right;
    padding: 10px;
    color: White;
}

.loginDisplay a:link
{
    color: white;
}

.loginDisplay a:visited
{
    color: white;
}

.loginDisplay a:hover
{
    color: white;
}

.failureNotification
{
    font-size: 1.2em;
    color: Red;
}

.bold
{
    font-weight: bold;
}

.submitButton
{
    text-align: right;
    padding-right: 10px;
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
  border-top: 1px solid rgba(0, 0, 0, 0.5);
  border-bottom: 1px solid rgba(255, 255, 255, 0.04);
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
  border-bottom: 2px solid rgba(0, 0, 0, 0.2);
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
  border: 1px solid rgba(0, 0, 0, 0.1);
  border-bottom: 2px solid rgba(0, 0, 0, 0.1);
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <div class="page">
        <div class="header">
            <div class="title">
                <h1>
                   Task Management System
                </h1>
            </div>
            <div class="loginDisplay">
                <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                    <AnonymousTemplate>
                        <a href="" ID="HeadLoginStatus" runat="server"></a> 
                    </AnonymousTemplate>
                   <%-- <LoggedInTemplate>
                        Welcome <span class="bold"><asp:LoginName ID="HeadLoginName" runat="server" /></span>!
                        [ <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Log Out" LogoutPageUrl="~/"/> ]
                    </LoggedInTemplate>--%>
                </asp:LoginView>
            </div>
            <div class="clear hideSkiplink">
               <%-- <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" 
                    EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                        <asp:MenuItem NavigateUrl="" Text="Create Task"/>                        
                        <asp:MenuItem NavigateUrl="" Text="View Task"/>                       
                    </Items>
                </asp:Menu>--%>
            </div>
        </div>


        <div class="main">
          
        </div>
        <div class="clear">
        </div>
    </div>
    
    <div class="footer">
        
    </div>
   
    </form>
</body>
</html>
