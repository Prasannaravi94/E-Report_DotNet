<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cover_Page.aspx.cs" Inherits="Cover_Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .CursorPointer
        {
            cursor: default;
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
            color: White;
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

    </style>
         <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
</script>
<script language="javascript" type="text/javascript">
    function fireServerButtonEvent() {
        document.getElementById("btnSubmit").click();
    } 
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlnot" runat="server">
            <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
                <tr>
                    <td>
                        <asp:Label ID="lnk" runat="server" Style="text-decoration: none; text-transform: capitalize;
                            font-size: 14px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"
                            Text="Welcome " ></asp:Label>
                        <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                            font-size: 14px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="lbldiv" runat="server" Text="User" Style="text-transform: capitalize;
                            font-size: 14px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                            <asp:LinkButton ID="lnkNew" runat="server" Style="text-decoration: none; " OnClick="OnClick_ShCut">.</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                         
                    </td>
                </tr>
            </table>
        </asp:Panel>
     <center>
            <asp:Panel runat="server" ID="pnlfinal" Visible="false">
            <asp:Label ID="lblatt" runat="server" Font-Bold="true" ForeColor="DarkGreen" Font-Size="20px">[Final Attempt]
          
            </asp:Label>
            </asp:Panel>
                <asp:Panel runat="server" ID="pnlfirst" Visible="false">
            <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="DarkGreen" Font-Size="20px">[First Attempt]
            </asp:Label>
            </asp:Panel>
            </center>
    <br />
    <center>
    <asp:Label ID="lblacc" runat="server" Font-Bold="true" BackColor="Yellow" ForeColor="Red" Font-Names="Verdana" Font-Size="14px" Text="For Accessing the 'Home Page', Kindly attend the  'Online Quiz' for 2 attempts. Best of Luck...  "></asp:Label>
    </center>
        <%--<asp:Panel ID="pnlback" runat="server" HorizontalAlign="Right" Width="97%">
            <asp:ImageButton ID="btnimg" runat="server" ImageUrl="~/Images/click1.gif" Width="200px"
                OnClick="btnimg_Click" />
        </asp:Panel>--%>
        <br />
           <center>
           <div class="roundbox boxshadow" style="width: 800px;height:350px; border: solid 2px steelblue;">
            <div class="gridheaderleft">
            <center>
                  <img src="Images/blue.png" alt="" />
                  </center>
                  </div>
                  
            <div class="boxcontenttext" style="background: white;">
                <div id="pnlPreviewSurveyData">
        <asp:Panel ID="pnlblue" runat="server" >
        <center>
    <br />
       <asp:Label ID="lbldown" runat="server" Font-Size="22px" Font-Bold="true" ForeColor="Red" Text="Step 1 :- Kindly Download the below Content for Online quiz" ></asp:Label>
        <br />
         <br />
       
             <%--<asp:Label ID="lblimg" runat="server"><img src="Images/hand.gif" alt=""  /></asp:Label>--%>
        
        <%--  <asp:LinkButton ID="lnkDownload" runat="server" Font-Size="14px" Font-Names="Verdana" 
                                    Text="Download Here" ForeColor="Red" Font-Bold="true"  OnClick="lnkDownload_Click"> 
                                    </asp:LinkButton>--%>
                                    <%--<asp:LinkButton ID="imgbtn" runat="server" 
                onclick="imgbtn_Click" > <img src="Images/download-button.gif" alt="" /></asp:LinkButton>--%>
                                                        
                 <asp:Button id="btnSubmit" runat="server" text="Submit" onclick="lnkDownload_Click" style="display:none" />
                 
                                       <a id="A1" onclick="fireServerButtonEvent()"  target="_blank" runat="server">
                                       <img src="Images/download-button.gif" alt="" />
                                       </a>
                                       <%--  <a id="A2" href="~/MasterFiles/Options/Files/EXTACEF 100 & 200 DT E-Learning Content.pdf" onclick="fireServerButtonEvent()"   target="_blank" runat="server">
                                       <img src="Images/download-button.gif" alt="" />
                                       </a>--%>
                                  <br />
                                  <br />
                                       <asp:Label ID="Label2" runat="server" Font-Size="22px" Font-Bold="true" ForeColor="Red" Text="Step 2 :- After Reading the Contents, attend the Quiz..." ></asp:Label>
                                       <br />
            <asp:ImageButton ID="btnimg" runat="server" ImageUrl="~/Images/click1.gif" Width="200px" 
                OnClick="btnimg_Click" />
       
       
        </center>
        </asp:Panel>
       </div>
                           
          </div>                       <%--<a href="MasterFiles/Options/Files/MEFTAL-SPAS -June BC.pdf" download="newfilename">Download the pdf</a>--%>
        </div>
        </center>
          <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="Images/loader.gif" alt="" />
</div> 
    </div>
    </form>
</body>
</html>
