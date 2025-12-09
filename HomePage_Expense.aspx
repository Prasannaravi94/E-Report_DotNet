<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage_Expense.aspx.cs" Inherits="HomePage_Expense" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <link type="text/css" rel="stylesheet" href="css/Grid.css" />
     <script type="text/javascript" src="JsFiles/jquery-1.10.1.js"></script>
     
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

   <style type="text/css">
       
        .blink_me {
    -webkit-animation-name: blinker;
    -webkit-animation-duration: 1s;
    -webkit-animation-timing-function: linear;
    -webkit-animation-iteration-count: infinite;
    
    -moz-animation-name: blinker;
    -moz-animation-duration: 1s;
    -moz-animation-timing-function: linear;
    -moz-animation-iteration-count: infinite;
    
    animation-name: blinker;
    animation-duration: 1s;
    animation-timing-function: linear;
    animation-iteration-count: infinite;
}

@-moz-keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}

@-webkit-keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}

@keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}
 .blink {
  animation: blink-animation 1s steps(5, start) infinite;
  -webkit-animation: blink-animation 1s steps(5, start) infinite;
}
@keyframes blink-animation {
  to {
    visibility: hidden;
  }
}
@-webkit-keyframes blink-animation {
  to {
    visibility: hidden;
  }
}

/* Loading Progress Bar */  
        
        .loadingContainer {
        width:20%;
        height: 20%;
        overflow: auto;
        margin: auto;
        position: absolute;
        top: 0;
        left: 0;
        bottom: 0;
        right: 0;
       }
        .shader {
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.65);
            top: 0;
            bottom: 0;
            right: 0;
            left: 0;
            position: fixed;
            display: none;
        }
        #divLoading3 {
            border-radius: 40px;
            margin: auto;
            overflow: hidden;
            width: 100%;
            height: 100%;
            background-color: white;
        }
    
       
         @keyframes anim {
         0% { background-position: 0 0; }
         100% { background-position: 50px 50px; }
        }    

         .bar {
          position: absolute;
         /* margin: auto;*/
        
          margin-left:45%;
          margin-top:20%;
          top: 0; bottom: 0; left: 0; right: 0;
          width:250px;
          height: 30px;
          overflow: hidden;
          background-size:100px 100px;
          background-image: linear-gradient(-45deg,
             #33c9ff 25%, #00b2f2 25%, 
             #00b2f2 50%, #33c9ff 50%,
             #33c9ff 75%, #00b2f2 75%);
          animation: anim 1s linear infinite;
          
        }
       
        .bar p {
          width: 100px;
          margin: 6px auto;
          text-align: center;
          color: white;
        }
        
    </style>

      <style type="text/css">
        .boxshadow
        {
            -moz-box-shadow: 3px 3px 5px #535353;
            -webkit-box-shadow: 3px 3px 5px #535353;
            box-shadow: 3px 3px 5px #535353;
        }
        .roundbox
        {
            -moz-border-radius: 6px 6px 6px 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px 6px 6px 6px;
        }
        .grd
        {
            border: 1;
            border-color: Black;
        }
        .roundbox-top
        {
            -moz-border-radius: 6px 6px 0 0;
            -webkit-border-radius: 6px 6px 0 0;
            border-radius: 6px 6px 0 0;
        }
        .roundbox-bottom
        {
            -moz-border-radius: 0 0 6px 6px;
            -webkit-border-radius: 0 0 6px 6px;
            border-radius: 0 0 6px 6px;
        }
        .gridheader, .gridheaderbig, .gridheaderleft, .gridheaderright
        {
            padding: 6px 6px 6px 6px;
            background: #003399 url(images/vertgradient.png) repeat-x;
            text-align: center;
            font-weight: bold;
            text-decoration: none;
            color: khaki;
        }
        .gridheaderleft
        {
            text-align: left;
        }
        .gridheaderright
        {
            text-align: right;
        }
        .gridheaderbig
        {
            font-size: 135%;
        }
        
        
        .Space label
        {
            margin-left: 5px;
            margin-right: 10px;
        }
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>


</head>
<body background="Images/bg5.jpg" style="background-repeat:no-repeat">
    <form id="form1" runat="server">
    <div>
    <table width="100%">
     <tr>
         
            
                <td align="left">
                    <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                        font-size: 22px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                </td>
                 <td align="right">
                    <asp:Label ID="lbldiv_name" runat="server" Text="Division Name" Style="text-transform: capitalize;
                        font-size: 22px;" ForeColor="OrangeRed" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                </td>
            </tr>
    <tr>

    

    </tr>
    
    </table>
      <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
           
            <tr>
                <td align="right">
                  <asp:Button ID="btnNext" runat="server" Width="150px" Height="30px" Text="Direct to Home Page" Visible="false"
                        OnClick="btnHome_Click" BackColor="LightPink" ForeColor="Black" CssClass="roundCorner" />
                    &nbsp;&nbsp;
                   
                    <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" CssClass="roundCorner"
                        Text="Logout" OnClick="btnLogout_Click" BackColor="Red" ForeColor="White" />
                </td>
            </tr>
        </table>
        <center>
        <br />
        <br />
        <br />
     
        <table align="center">
        <tr>
              <td>
                <asp:Label ID="lblblink" class="blink_me" runat="server" Visible="false" Font-Bold="true"  ForeColor="Purple" Font-Size="18px" ><span style="Color:Red">***</span>Today is the <span style="color:ButtonText"> Last day</span> for enter your Last Month <span style="color:ButtonText">Expense</span><span style="Color:Red">***</span></asp:Label>
              </td>
              
            </tr>
            <tr>
            <td align="center">
              <asp:Label ID="lblalert" runat="server" Text ="( If not Prepared ,your DCR will be Blocked)" Font-Size="10" Font-Bold="true" Visible="false" ></asp:Label>
            </td>
            </tr>
        </table>
        <br />
     <%--    <asp:Panel ID="pnlmr" runat="server" >
           
                <div class="roundbox boxshadow" style="width: 800px; border: solid 2px steelblue;">
                    <div class="gridheaderleft" style="font-size:14PX">
                        Important Information
                    </div>
                    <div class="boxcontenttext" style="background: khaki;">
                        <div id="pnlPreviewSurveyData">
                     
                         
                            <br />
                          
<asp:Label ID="lblpol" runat="server" Font-Bold="true" Font-Names="Verdana">
1.All Fieldforce Submit their Expense before 5th of Every Month.</asp:Label><br />
<asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Names="Verdana">
2. RM has to Approve of their Reporting Subordinate Expense before 8th . <br /> </asp:Label>
                         
                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Names="Verdana">
3. ZM has to do Second Approval of Expense before 12th .<br /><br />   </asp:Label>
                          
                                                    
                        </div>
                    </div>
                </div>
            </asp:Panel>
            --%>
            <br />
            <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

            <table align="center">
            
                <tr>
                    <td>                

                    <asp:LinkButton ID="clicktp" Text ="Click here to Prepare Your Expense for the Month of" Font-Names="verdana" OnClientClick="Load();" 
                            ForeColor ="Brown" Font-Bold="true" Font-Size="27px" runat="server" 
                            onclick="clicktp_Click" ></asp:LinkButton>                          
                   
                    </td>
                </tr>
                
                <tr>
                 <td>

                 </td>
                </tr>
            </table>
        </center>
       

          <div id="shader" class="shader">
            <div id="loading" class="bar">
                <p>
                    loading</p>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#loading").hide();
            $("#shader").fadeOut();
        });

        function Load() {
            $("#loading").show();
            $("#shader").fadeIn();
        }
    </script>
</body>
</html>
