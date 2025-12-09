<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HelpVideo.aspx.cs" Inherits="HelpVideo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            overflow: hidden;
            height: 100%;
            max-height: 100%;
            font-family: Sans-serif;
            line-height: 1.5em;
        }
        
        main
        {
            position: fixed;
            top: 95px; /* Set this to the height of the header */
            bottom: 50px; /* Set this to the height of the footer */
            left: 250px;
            right: 300px;
            overflow: auto;
            background: #fff;
        }
        
        #header
        {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 95px;
            overflow: hidden; /* Disables scrollbars on the header frame. To enable scrollbars, change "hidden" to "scroll" */
            background: #BCCE98;
        }
        
        #footer
        {
            position: absolute;
            left: 0;
            bottom: 0;
            width: 100%;
            height: 50px;
            overflow: hidden; /* Disables scrollbars on the footer frame. To enable scrollbars, change "hidden" to "scroll" */
            background: #BCCE98;
        }
        
        #left
        {
            position: absolute;
            top: 95px; /* Set this to the height of the header */
            bottom: 50px; /* Set this to the height of the footer */
            left: 0;
            width: 250px;
            overflow: auto; /* Scrollbars will appear on this frame only when there's enough content to require scrolling. To disable scrollbars, change to "hidden", or use "scroll" to enable permanent scrollbars */
            background: #DAE9BC;
        }
        
        #right
        {
            position: absolute;
            top: 95px; /* Set this to the height of the header */
            bottom: 50px; /* Set this to the height of the footer */
            right: 0;
            width: 300px;
            overflow: auto; /* Scrollbars will appear on this frame only when there's enough content to require scrolling. To disable scrollbars, change to "hidden", or use "scroll" to enable permanent scrollbars */
            background: #DAE9BC;
        }
        
        .innertube
        {
            margin: 15px; /* Provides padding for the content */
        }
        
        p
        {
            color: #555;
        }
        
        nav ul
        {
            list-style-type: none;
            margin: 0;
            padding: 0;
        }
        
        nav ul a
        {
            color: darkgreen;
            text-decoration: none;
        }
        
        /*IE6 fix*/
        * html body
        {
            padding: 50px 200px 50px 200px; /* Set the first value to the height of the header, the second value to the width of the right column, third value to the height of the footer, and last value to the width of the left column */
        }
        
        * html main
        {
            height: 100%;
            width: 100%;
        }
        h1
        {
            text-align: center;
            color: #8B0000;
            font-size: 40px; /*   text-shadow: 0 1px 0 #cccccc, 0 2px 0 #c9c9c9, 0 3px 0 #bbbbbb, 0 4px 0 #b9b9b9, 0 5px 0 #aaaaaa, 0 6px 1px rgba(0, 0, 0, 0.1), 0 0 5px rgba(0, 0, 0, 0.1), 0 1px 3px rgba(0, 0, 0, 0.3), 0 3px 5px rgba(0, 0, 0, 0.2), 0 5px 10px rgba(0, 0, 0, 0.25), 0 10px 10px rgba(0, 0, 0, 0.2), 0 20px 20px rgba(0, 0, 0, 0.15);*/
        }
        #search-form_3
        {
            background: #e1e1e1; /* Fallback color for non-css3 browsers */
            width: 365px; /* Gradients */
            background: -webkit-gradient( linear,left top, left bottom, color-stop(0, rgb(243,243,243)), color-stop(1, rgb(225,225,225)));
            background: -moz-linear-gradient( center top, rgb(243,243,243) 0%, rgb(225,225,225) 100%); /* Rounded Corners */
            border-radius: 17px;
            -webkit-border-radius: 17px;
            -moz-border-radius: 17px; /* Shadows */
            box-shadow: 1px 1px 2px rgba(0,0,0,.3), 0 0 2px rgba(0,0,0,.3);
            -webkit-box-shadow: 1px 1px 2px rgba(0,0,0,.3), 0 0 2px rgba(0,0,0,.3);
            -moz-box-shadow: 1px 1px 2px rgba(0,0,0,.3), 0 0 2px rgba(0,0,0,.3);
        }
        
        /*** TEXT BOX ***/
        .search_3
        {
            background: #fafafa; /* Fallback color for non-css3 browsers */ /* Gradients */
            background: -webkit-gradient( linear, left bottom, left top, color-stop(0, rgb(250,250,250)), color-stop(1, rgb(230,230,230)));
            background: -moz-linear-gradient( center top, rgb(250,250,250) 0%, rgb(230,230,230) 100%);
            border: 0;
            border-bottom: 1px solid #fff;
            border-right: 1px solid rgba(255,255,255,.8);
            font-size: 16px;
            margin: 4px;
            padding: 5px;
            width: 250px; /* Rounded Corners */
            border-radius: 17px;
            -webkit-border-radius: 17px;
            -moz-border-radius: 17px; /* Shadows */
            box-shadow: -1px -1px 2px rgba(0,0,0,.3), 0 0 1px rgba(0,0,0,.2);
            -webkit-box-shadow: -1px -1px 2px rgba(0,0,0,.3), 0 0 1px rgba(0,0,0,.2);
            -moz-box-shadow: -1px -1px 2px rgba(0,0,0,.3), 0 0 1px rgba(0,0,0,.2);
        }
        
        /*** USER IS FOCUSED ON TEXT BOX ***/
        .search_3:focus
        {
            outline: none;
            background: #fff; /* Fallback color for non-css3 browsers */ /* Gradients */
            background: -webkit-gradient( linear, left bottom, left top, color-stop(0, rgb(255,255,255)), color-stop(1, rgb(235,235,235)));
            background: -moz-linear-gradient( center top, rgb(255,255,255) 0%, rgb(235,235,235) 100%);
        }
        
        /*** SEARCH BUTTON ***/
        .submit_3
        {
            background: #44921f; /* Fallback color for non-css3 browsers */ /* Gradients */
            background: -webkit-gradient( linear, left top, left bottom, color-stop(0, rgb(79,188,32)), color-stop(0.15, rgb(73,157,34)), color-stop(0.88, rgb(62,135,28)), color-stop(1, rgb(49,114,21)));
            background: -moz-linear-gradient( center top, rgb(79,188,32) 0%, rgb(73,157,34) 15%, rgb(62,135,28) 88%, rgb(49,114,21) 100%);
            border: 0;
            color: #eee;
            cursor: pointer;
            float: right;
            font: 16px 'Raleway' , sans-serif;
            font-weight: bold;
            height: 30px;
            margin: 4px 4px 0;
            text-shadow: 0 -1px 0 rgba(0,0,0,.3);
            width: 84px;
            outline: none; /* Rounded Corners */
            border-radius: 30px;
            -webkit-border-radius: 30px;
            -moz-border-radius: 30px; /* Shadows */
            box-shadow: -1px -1px 1px rgba(255,255,255,.5), 1px 1px 0 rgba(0,0,0,.4);
            -moz-box-shadow: -1px -1px 1px rgba(255,255,255,.5), 1px 1px 0 rgba(0,0,0,.2);
            -webkit-box-shadow: -1px -1px 1px rgba(255,255,255,.5), 1px 1px 0 rgba(0,0,0,.4);
        }
        /*** SEARCH BUTTON HOVER ***/
        .submit_3:hover
        {
            background: #4ea923; /* Fallback color for non-css3 browsers */ /* Gradients */
            background: -webkit-gradient( linear, left top, left bottom, color-stop(0, rgb(89,222,27)), color-stop(0.15, rgb(83,179,38)), color-stop(0.8, rgb(66,143,27)), color-stop(1, rgb(54,120,22)));
            background: -moz-linear-gradient( center top, rgb(89,222,27) 0%, rgb(83,179,38) 15%, rgb(66,143,27) 80%, rgb(54,120,22) 100%);
        }
        .submit_3:active
        {
            background: #4ea923; /* Fallback color for non-css3 browsers */ /* Gradients */
            background: -webkit-gradient( linear, left bottom, left top, color-stop(0, rgb(89,222,27)), color-stop(0.15, rgb(83,179,38)), color-stop(0.8, rgb(66,143,27)), color-stop(1, rgb(54,120,22)));
            background: -moz-linear-gradient( center bottom, rgb(89,222,27) 0%, rgb(83,179,38) 15%, rgb(66,143,27) 80%, rgb(54,120,22) 100%);
        }
        .cross
        {
            padding-right: 2px;
            float: left;
            margin-bottom: 10px;
        }
        
        span
        {
            margin-left: 5px;
            display: inline-block;
            width: calc(100% - 40px);
            vertical-align: top;
        }
        .gradient-button
        {
            margin: 10px;
            font-family: "Arial Black" , Gadget, sans-serif;
            font-size: 20px;
            padding: 15px;
            text-align: center;
            text-transform: uppercase;
            transition: 0.5s;
            background-size: 200% auto;
            color: #FFF;
            box-shadow: 0 0 20px #eee;
            border-radius: 10px;
            width: 120px;
            box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24);
            transition: all 0.3s cubic-bezier(.25,.8,.25,1);
            cursor: pointer;
            display: inline-block;
            border-radius: 25px;
        }
        .gradient-button:hover
        {
            box-shadow: 0 10px 20px rgba(0,0,0,0.19), 0 6px 6px rgba(0,0,0,0.23);
            margin: 8px 10px 12px;
        }
        .gradient-button-1
        {
            background-image: linear-gradient(to right, #DD5E89 0%, #F7BB97 51%, #DD5E89 100%);
        }
        .gradient-button-1:hover
        {
            background-position: right center;
        }
        .square_btn
        {
            display: inline-block;
            padding: 7px 20px;
            border-radius: 25px;
            text-decoration: none;
            color: #FFF;
            background-image: -webkit-linear-gradient(45deg, #FFC107 0%, #ff8b5f 100%);
            background-image: linear-gradient(45deg, #FFC107 0%, #ff8b5f 100%);
            transition: .4s;
        }
        
        .square_btn:hover
        {
            background-image: -webkit-linear-gradient(45deg, #FFC107 0%, #f76a35 100%);
            background-image: linear-gradient(45deg, #FFC107 0%, #f76a35 100%);
        }
        .button
        {
            display: inline-block;
            padding: 7px 12px;
            font-size: 15px;
            font-weight: bold;
            cursor: pointer;
            text-align: center;
            text-decoration: none;
            outline: none;
            color: #fff;
            background-color: #FF69B4;
            border: none;
            border-radius: 15px;
            box-shadow: 0 9px #999;
        }
        
        .button:hover
        {
            background-color: #FFB6C1;
        }
        
        .button:active
        {
            background-color: #FF69B4;
            box-shadow: 0 5px #666;
            transform: translateY(4px);
        }
        .button2
        {
            display: inline-block;
            padding: 7px 12px;
            font-size: 15px;
            font-weight: bold;
            cursor: pointer;
            text-align: center;
            text-decoration: none;
            outline: none;
            color: #fff;
            background-color: #8B4513;
            border: none;
            border-radius: 15px;
            box-shadow: 0 9px #999;
        }
        
        .button2:hover
        {
            background-color: #CD853F;
        }
        
        .button2:active
        {
            background-color: #8B4513;
            box-shadow: 0 5px #666;
            transform: translateY(4px);
        }
        #fc_frame, #fc_frame.fc-widget-normal {
            top: 15px !important;
        }
        #LiveChatCap {
                font-size: 16px;
                /* background-color: #333fff; */
                color: #333fff;
                padding: 2px;
                border-radius: 30px;
                font-weight: 600;
        }
    </style>
    <script type="text/javascript">
       
    </script>
    <script type="text/javascript" src="JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="http://fonts.googleapis.com/css?family=Lobster" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.js"></script>
    <%--<script type="text/javascript">
        $(document).ready(function () {


            $("#btnlive").click(function () {

                window.$zopim || (function (d, s) {

                    var z = $zopim = function (c) {
                      
                        z._.push(c)
                    }, $ = z.s =
d.createElement(s), e = d.getElementsByTagName(s)[0]; z.set = function (o) {
    z.set.
_.push(o)
}; z._ = []; z.set._ = []; $.async = !0; $.setAttribute('charset', 'utf-8');
                    $.src = 'https://v2.zopim.com/?5DURYZFDFeE3izx6HWO5i5IteKcQeGaU'; z.t = +new Date; $.
type = 'text/javascript'; e.parentNode.insertBefore($, e)
                })(document, 'script');
               
            });

        });
    </script>--%>
    <script type="text/javascript">

        function initFreshChat() {
            window.fcWidget.init({
                token: "65d3efbd-8e34-4db8-bd0b-dbcd2d844694",
                host: "https://wchat.freshchat.com"
            });
        }
        function initialize(i, t) { var e; i.getElementById(t) ? initFreshChat() : ((e = i.createElement("script")).id = t, e.async = !0, e.src = "https://wchat.freshchat.com/js/widget.js", e.onload = initFreshChat, i.head.appendChild(e)) } function initiateCall() { initialize(document, "freshchat-js-sdk") } window.addEventListener ? window.addEventListener("load", initiateCall, !1) : window.attachEvent("load", initiateCall, !1);
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <header id="header">
			<div class="innertube">
            <table width="100%" border="0">
            <tr>
            <td  align="left" style="width: 35%">
          <img src="Images/logo.png" alt="" height="50px" /> 
          </td>
          <td  align="center" style="width: 30%">
			<h1>Support Portal</h1>

			
            </td>
            <td align="right"  > 
            <%--<a href="" class="gradient-button gradient-button-1" onclick="openOnImageClick()">Live Chat</a>--%>
            <%--<asp:Label ID="lbl" runat="server"  Font-Bold="true" Font-Italic="true">For Additional Info:</asp:Label>
         
            <img src="Images/hand.gif"  alt="" />&nbsp;&nbsp;
 <input type="button" id="btnlive" class="button" value="Live Chat" style="display:none;"/>
 <input type="button" id="btnticket" class="button2" value="Ticketing System" />--%>
                <label id="LiveChatCap"> Live Chat</label>
                <img src="Images/hand.gif"  alt="" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</td>
           </tr>
           </table>
           </div>
		</header>
        <main>
			<div class="innertube">
   
           <div id="search-form_3" style="margin-left:200px">
<input type="text" class="search_3" id="txtName" name="Name" value=""/>
<%--<input type="submit" class="submit_3" value="Search"  />--%>

<input type="submit" name="btnSubmit" id="btnSubmit" value="Search" runat="server" class="submit_3" onserverclick="btnLogin_Click"/>
</div>


</div>
<br />

			&nbsp;&nbsp;	<asp:Label ID="lblhead" runat="server" ForeColor="Blue" Font-Bold="true" Font-Size="20px"></asp:Label>
               <asp:Panel ID="Panel1" runat="Server" CssClass="myPanelCSS" Style="overflow-y: auto; overflow-x: hidden">
			 <asp:GridView ID="grdResult" runat="server" Width="100%" Height="100%" HorizontalAlign="Center" 
                                        AutoGenerateColumns="false"
                                        RowStyle-Height="20px" GridLines="None"  >
                                        <HeaderStyle Font-Bold="False" />
                                        <SelectedRowStyle BackColor="BurlyWood"/>
                                        <RowStyle Font-Size="14px" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>                
                                            <asp:TemplateField HeaderText="Topic_ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbld" runat="server" Text='<%#Eval("Topic_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-Width="300" HeaderText="" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    &nbsp;
                                                      <img src="Images/arrow.gif" alt="" />
                                                    <asp:Label ID="lbldis" runat="server" Text='<%#Eval("Topic_Display_Name")%>' Font-Names="verdana" ></asp:Label>
                                                    <br /> &nbsp;&nbsp;&nbsp;&nbsp;
                                                  <%--  <asp:LinkButton ID="lnkEnglish" runat="server" Font-Underline="false" Font-Bold="true" Font-Size="12px" ForeColor="Blue" Font-Names="verdana" Text="English" targe0
                                                     CommandArgument='<%# Eval("Topic_Display_AName_One")%>' OnClientClick="aspnetForm.target ='_blank';"
                                                    ></asp:LinkButton>--%>
                                                    <a href='<%# Eval("Topic_Display_AName_One") %>' style="text-decoration:none;font-weight:bold;color:Blue;font-family:Verdana;font-size:12px" target="_blank">English</a>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                      <a href='<%# Eval("Topic_Display_AName_Two") %>' style="text-decoration:none;font-weight:bold;color:Blue;font-family:Verdana;font-size:12px" target="_blank">Hindi</a>
                                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <a href='<%# "SupportPortal_PDF/"+Eval("Topic_Display_AName_Three")+"" %>' style="text-decoration:none;font-weight:bold;color:Blue;font-family:Verdana;font-size:12px" target="_blank">Pdf</a>

                                                     &nbsp;&nbsp;&nbsp;
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </asp:Panel>
    </div>
    </main>
    <nav id="left">
			<div class="innertube">
				 <asp:GridView ID="grdTopic" runat="server" Width="100%" Height="100%" HorizontalAlign="Center" 
                                        AutoGenerateColumns="false"  OnRowCommand="grdTopic_RowCommand"
                                        RowStyle-Height="35px" GridLines="None" >
                                        <HeaderStyle Font-Bold="true" Font-Size="16px"  />
                                               <RowStyle Font-Bold="False"  />
                                        <SelectedRowStyle BackColor="BurlyWood"/>
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>                
                                            <asp:TemplateField HeaderText="Topic_ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Topic_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Topics" HeaderStyle-BackColor="#333FFF" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="16px" HeaderStyle-Font-Bold="true" HeaderStyle-Width="200px">
                                                <ControlStyle ForeColor="DarkBlue" Font-Size="14px" Font-Names="verdana" Font-Bold="True">
                                                </ControlStyle>
                                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                                <ItemTemplate>
                                                    &nbsp;

                                                    <asp:LinkButton ID="lnkbutTop" runat="server" CommandArgument='<%# Eval("Topic_ID") + "," + Eval("Topic_Name")%>'
                                                        CommandName="Topic" Text ='<%#Eval("Topic_Name") %>' >
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
		</nav>
    <div id="right">
        <div class="innertube">
            <asp:GridView ID="grdFreq" runat="server" Width="100%" Height="100%" HorizontalAlign="Center"
                AutoGenerateColumns="false" RowStyle-Height="20px" GridLines="None">
                <HeaderStyle Font-Bold="False" />
                <SelectedRowStyle BackColor="BurlyWood" />
                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="Topic_ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Frequent_Q_ID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="300" HeaderStyle-BackColor="#333FFF" HeaderStyle-Width="200px"
                        HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="16px" HeaderStyle-Font-Bold="true"
                        HeaderText="Frequently asked Questions" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            &nbsp;
                            <img src="Images/arrow.gif" alt="" />
                            <a href='<%# Eval("Url") %>' target="_blank">
                            <asp:Label ID="lbldis" runat="server" Text='<%#Eval("Topic_Display_Name")%>' Font-Size="12px"
                                Font-Names="verdana"></asp:Label></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:Panel ID="footer" runat="Server" CssClass="myPanelCSS">
        <marquee onmouseover="this.setAttribute('scrollamount', 0, 0);" onmouseout="this.setAttribute('scrollamount', 6, 0);">
         <%--<asp:Label ID="lblFlash" runat="Server"  style="margin-top:10px;font-weight:bold" Width="100%" ForeColor="Brown" Font-Size="16px" Font-Names="Tahoma"  Text="Technical Support No :- +91-96000 01166 /// +91-8929 222 444 /// Whatsapp Support No:- +91-98416 18881 /// Admin Support Time:- Monday to Friday 10.00 a.m to 5.30 p.m" /></marquee>--%>
            <asp:Label ID="lblFlash" runat="Server"  style="margin-top:10px;font-weight:bold" Width="100%" ForeColor="Brown" Font-Size="16px" Font-Names="Tahoma"  Text="Technical Support No :- +91-70882 22444 /// Whatsapp Support No:- +91-98416 18881 /// +91-97890 48264 /// Admin Support Time:- Monday to Saturday 9.30 a.m to 6.00 p.m" /></marquee>
    </asp:Panel>
                <script type="text/javascript">
            //function initFreshChat() {
            //    window.fcWidget.init({
            //        token: "65d3efbd-8e34-4db8-bd0b-dbcd2d844694",
            //        host: "https://wchat.freshchat.com"
            //    });
            //}
            //function initialize(i, t) { var e; i.getElementById(t) ? initFreshChat() : ((e = i.createElement("script")).id = t, e.async = !0, e.src = "https://wchat.freshchat.com/js/widget.js", e.onload = initFreshChat, i.head.appendChild(e)) } function initiateCall() { initialize(document, "freshchat-js-sdk") } window.addEventListener ? window.addEventListener("load", initiateCall, !1) : window.attachEvent("load", initiateCall, !1);
            //  $(document).ready(function () {


            //        window.$zopim || (function (d, s) {
            //            var z = $zopim = function (c) {
            //                z._.push(c)
            //            }, $ = z.s =
            //    d.createElement(s), e = d.getElementsByTagName(s)[0]; z.set = function (o) {
            //        z.set.
            //    _.push(o)
            //    }; z._ = []; z.set._ = []; $.async = !0; $.setAttribute('charset', 'utf-8');
            //            $.src = 'https://v2.zopim.com/?5DURYZFDFeE3izx6HWO5i5IteKcQeGaU'; z.t = +new Date; $.
            //type = 'text/javascript'; e.parentNode.insertBefore($, e)
            //        })(document, 'script');

            //  });
</script>
    </form>
</body>
</html>
