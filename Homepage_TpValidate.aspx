<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Homepage_TpValidate.aspx.cs" Inherits="Bus_EReport_Homepage_TpValidate" %>

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


</head>
<body>
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
                  <asp:Button ID="btnNext" runat="server" Width="150px" Height="30px" Text="Next to Home Page" Visible="false"
                        OnClick="btnHome_Click" BackColor="LightPink" ForeColor="Black" CssClass="roundCorner" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" CssClass="roundCorner" Visible="false"
                        Text="Direct to Home Page" OnClick="btnHomepage_Click" BackColor="Green" ForeColor="White" />
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
                <asp:Label ID="lblblink" class="blink_me" runat="server" Visible="false" Font-Bold="true"  ForeColor="Purple" Font-Size="18px" ><span style="Color:Red">***</span>Today is the <span style="color:ButtonText"> Last day</span> for enter your Next Month <span style="color:ButtonText">Tour Plan</span><span style="Color:Red">***</span></asp:Label>
              </td>
              
            </tr>
            <tr>
            <td align="center">
              <asp:Label ID="lblalert" runat="server" Text ="( If not Prepared ,your DCR will be Blocked)" Font-Size="10" Font-Bold="true" Visible="false" ></asp:Label>
            </td>
            </tr>
        </table>
        <br />
        <br />
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

                    <asp:LinkButton ID="clicktp" Text ="Click here to Prepare Your Tour Plan for the Month of" Font-Names="verdana" OnClientClick="Load();" 
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
