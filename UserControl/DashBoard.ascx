<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashBoard.ascx.cs" Inherits="UserControl_DashBoard" %>
     <style type="text/css">
        .accordionContent
        {
            background-color: #D3DEEF;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            width: 20%;
        }
        
        .accordionHeaderSelected
        {
            background-color: #5078B3;
            border: 1px solid #2F4F4F;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
        }
        
        .accordionHeader
        {
            background-color: #2E4D7B;
            border: 1px solid #2F4F4F;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
        }
        
        .href
        {
            color: White;
            font-weight: bold;
            text-decoration: none;
        }
        
        .gridtable
        {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }
        .gridtable th
        {
            border-width: 1px;
            border-style: solid;
            border-color: #666666;
            background-color: #A6A6D2;
        }
        .gridtable td
        {
            border-color: #666666;
            background-color: #ffffff;
        }
        
        .fancy-green .ajax__tab_header
        {
            background: url(green_bg_Tab.gif) repeat-x;
            cursor: pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
            background: url(green_left_Tab.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
            background: url(green_right_Tab.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
            font-size: 13px;
            font-weight: bold;
            color: #000;
            font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
            height: 46px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
            height: 46px;
            margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
            margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
            color: #fff;
        }
        .fancy .ajax__tab_body
        {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }
        .button
        {
            font-family: Helvetica, Arial, sans-serif;
            font-size: 18px;
            font-weight: bold;
            color: #FFFFFF;
            padding: 10px 45px;
            margin: 0 20px;
            text-decoration: none;
        }
        .shape-1
        {
            -webkit-border-radius: 5px 50px 5px 50px;
            border-radius: 5px 50px 5px 50px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 50px;
            -moz-border-radius-bottomleft: 50px;
            -moz-border-radius-bottomright: 5px;
        }
        
        
        .shape-2
        {
            -webkit-border-radius: 50px 5px 50px 5px;
            border-radius: 50px 5px 50px 5px;
            -moz-border-radius-topleft: 50px;
            -moz-border-radius-topright: 5px;
            -moz-border-radius-bottomleft: 5px;
            -moz-border-radius-bottomright: 50px;
        }
        
        .effect-4
        {
            transition: border-radius 1s;
            -webkit-transition: border-radius 1s;
            -moz-transition: border-radius 1s;
            -o-transition: border-radius 1s;
            -ms-transition: border-radius 1s;
        }
        
        
        
        .effect-4:hover
        {
            border-radius: 50px 5px 50px 5px;
            -webkit-border-radius: 50px 5px 50px 5px;
            -moz-border-radius-topleft: 50px;
            -moz-border-radius-topright: 5px;
            -moz-border-radius-bottomleft: 5px;
            -moz-border-radius-bottomright: 50px;
        }
        
        .effect-5
        {
            transition: border-radius 1s;
            -webkit-transition: border-radius 1s;
            -moz-transition: border-radius 1s;
            -o-transition: border-radius 1s;
            -ms-transition: border-radius 1s;
        }
        
        
        
        .effect-5:hover
        {
            border-radius: 5px 50px 5px 50px;
            -webkit-border-radius: 5px 50px 5px 50px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 50px;
            -moz-border-radius-bottomleft: 50px;
            -moz-border-radius-bottomright: 5px;
        }
        .green
        {
            border: solid 1px #3b7200;
            background-color: #88c72a;
            background: -moz-linear-gradient(top, #88c72a 0%, #709e0e 100%);
            background: -webkit-linear-gradient(top, #88c72a 0%, #709e0e 100%);
            background: -o-linear-gradient(top, #88c72a 0%, #709e0e 100%);
            background: -ms-linear-gradient(top, #88c72a 0% ,#709e0e 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#709e0e', endColorstr='#709e0e',GradientType=0 );
            background: linear-gradient(top, #88c72a 0% ,#709e0e 100%);
            -webkit-box-shadow: 0px 0px 1px #66FF00, inset 0px 0px 1px #FFFFFF;
            -moz-box-shadow: 0px 0px 1px #66FF00, inset 0px 0px 1px #FFFFFF;
            box-shadow: 0px 0px 1px #66FF00, inset 0px 0px 1px #FFFFFF;
        }
        
        
        
        .red
        {
            border: solid 1px #720000;
            background-color: #c72a2a;
            background: -moz-linear-gradient(top, #c72a2a 0%, #9e0e0e 100%);
            background: -webkit-linear-gradient(top, #c72a2a 0%, #9e0e0e 100%);
            background: -o-linear-gradient(top, #c72a2a 0%, #9e0e0e 100%);
            background: -ms-linear-gradient(top, #c72a2a 0% ,#9e0e0e 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#9e0e0e', endColorstr='#9e0e0e',GradientType=0 );
            background: linear-gradient(top, #c72a2a 0% ,#9e0e0e 100%);
            -webkit-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            -moz-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
        }
        .orange
        {
            border: solid 1px #720000;
            background-color: #FF9900;
            background: -moz-linear-gradient(top, #FF9900 0%, #FF9900 100%);
            background: -webkit-linear-gradient(top, #FF9900 0%, #FF9900 100%);
            background: -o-linear-gradient(top, #FF9900 0%, #FF9900 100%);
            background: -ms-linear-gradient(top, #FF9900 0% ,#FF9900 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#9e0e0e', endColorstr='#9e0e0e',GradientType=0 );
            background: linear-gradient(top, #FF9900 0% ,#9e0e0e 100%);
            -webkit-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            -moz-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
        }
        
        .blue
        {
            border: solid 1px #720000;
            background-color: #9900FF;
            background: -moz-linear-gradient(top, #9900FF 0%, #9900FF 100%);
            background: -webkit-linear-gradient(top, #9900FF 0%, #9900FF 100%);
            background: -o-linear-gradient(top, #9900FF 0%, #9900FF 100%);
            background: -ms-linear-gradient(top, #9900FF 0% ,#9900FF 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#9e0e0e', endColorstr='#9e0e0e',GradientType=0 );
            background: linear-gradient(top, #9900FF 0% ,#9e0e0e 100%);
            -webkit-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            -moz-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
        }
        
        .pink
        {
            border: solid 1px Black;
            background-color: #CC3399;
            background: -moz-linear-gradient(top, #CC3399 0%, #CC3399 100%);
            background: -webkit-linear-gradient(top, #CC3399 0%, #CC3399 100%);
            background: -o-linear-gradient(top, #CC3399 0%, #CC3399 100%);
            background: -ms-linear-gradient(top, #CC3399 0% ,#CC3399 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#9e0e0e', endColorstr='#9e0e0e',GradientType=0 );
            background: linear-gradient(top, #CC3399 0% ,#9e0e0e 100%);
            -webkit-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            -moz-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
        }
        .myListBox
        {
            border-style: none;
            border-width: 0px;
            border: none;
            font-size: 12px;
            font-family: Verdana;
            height: 300px;
            width: 300px;
        }
    </style>
      <div>
      <center>
            <img src="../Images/dash.jpg" />
        </center>
  <table id="Table1" runat="server" width="95%">
            <tr>
                <td align="right" width="30%">
                 <%--   <asp:Label ID="lblTerrritory" runat="server" SkinID="lblMand" Font-Size="12px" Font-Names="Verdana"
                        Visible="true"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" PostBackUrl="~/BasicMaster.aspx"
                runat="server" />
                </td>
            </tr>
        </table>
    
        <center>
            <%--<a href="#" class="button shape-1 green effect-4">SFE KPI</a>  --%>
            <asp:Button ID="btnmaster" CssClass="button shape-2 red effect-5" Text="Masters KPI"
                runat="server"  onclick="btnmaster_Click"  />
                <asp:Button ID="btnsfe" CssClass="button shape-2 green effect-5" Text="SFE KPI" runat="server"
            onclick="btnSfe_Click"
               />
            <asp:Button ID="btnmar" class="button shape-2 orange effect-5" Text="Marketing KPI"  Visible="false"
                runat="server" onclick="btnmar_Click"  />
                 <asp:Button ID="btnsale" CssClass="button shape-2 blue effect-5" Text="Sales KPI"
                runat="server" onclick="btnsale_Click"  />
            <asp:Button ID="btntrain" class="button shape-2 pink effect-5" Text="Training KPI" Visible="false"
                runat="server" />
        </center>
        <hr style="width:100%; border-style:dashed; border-color:Green"/>
     
    </div>