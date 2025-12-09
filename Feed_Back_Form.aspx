<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feed_Back_Form.aspx.cs" Inherits="Feed_Back_Form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,400italic">
    <link rel='stylesheet prefetch' href='https://cdn.gitcdn.xyz/cdn/angular/bower-material/v1.0.0-rc3/angular-material.css'>
    <link rel='stylesheet prefetch' href='http://localhost:8080/docs.css'>
    <style type="text/css">
        .form-submit-button {
            background: #016ABC;
            color: #fff;
            border: 1px solid #eee;
            border-radius: 20px;
            box-shadow: 5px 5px 5px #eee;
            text-shadow: none;
            width: 200px;
        }

        .modal {
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

        .loading {
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

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        fieldset {
            border: 1px solid green;
        }

        legend {
            padding: 0.2em 0.5em;
            border: 1px solid green;
            color: White;
            font-size: 90%;
            text-align: right;
            background-color: green;
        }

        body {
        }

        #SignupContent {
            max-width: 800px;
            backgroud-color: white;
            border-radius: 4px;
        }

        @media screen and (max-width: 800px) {
            #SignupContent {
                height: 100%;
            }

            #materialToolbar {
                padding: 10px;
            }
        }

        .blink_me {
            animation: blinker 1s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }
    </style>
    <script type="text/javascript" src="JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="JsFiles/jquery-1.10.1.js"></script>
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSubmit').click(function () {

                var grp = $('#<%=ddlarea.ClientID%> :selected').text();
                if ($('#rdoproduct input:checked').length == 0)
                { alert('Select Product Rating'); return false; }
                else if ($('#rdoService input:checked').length == 0) {
                    alert('Select Service Rating'); return false;
                }
                else if (grp == "--Select--") {
                    alert("Select Area of Problem."); $('#ddlarea').focus(); return false;
                }
                else if (grp != "Nil") {
                    if ($("#txtCon").val() == "") {
                        alert("Enter Contact No."); $('#txtCon').focus(); return false;
                    }
                    else if ($("#txtcom").val() == "") {
                        alert("Enter Comments."); $('#txtcom').focus(); return false;
                    }
                }

                else {
                    return true;
                }




            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <center>
        <h2 class="blink_me" style="color: Blue">
             Welcome to Our Customer Feedback Form<a href="Default.aspx" style="text-decoration:none; color:white" >.</a></h2>
    </center>
    <center>
        <div ><span  style="font-size:22px;font-weight:bold;color:green;font-family:Verdana" align="center">Customer Feedback Form</span> <span flex=""></span> </div>
				
				<div layout-padding="">
						<div>
                           <p style="margin-left:30px; color:Red; font-style:italic">
        You are the Valueable Customer for us. <br />Are you enjoying the Good Support & service from SANEFORCE.... Is it?
          

        </p>
        <%--   <fieldset>
  <legend float="left">Product Rating</legend>

        
        <asp:RadioButtonList ID="rdoproduct" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Value="Excellent" Text="Excellent" ></asp:ListItem>
        <asp:ListItem Value="Good" Text="Good" ></asp:ListItem>
        <asp:ListItem Value="Fair" Text="Fair" ></asp:ListItem>
        <asp:ListItem Value="Poor" Text="Poor" ></asp:ListItem>
        </asp:RadioButtonList>
         </fieldset>
         <br />
                <fieldset>
  <legend>Service Rating</legend>
           <asp:RadioButtonList ID="rdoService" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Value="Excellent" Text="Excellent" ></asp:ListItem>
        <asp:ListItem Value="Good" Text="Good" ></asp:ListItem>
        <asp:ListItem Value="Fair" Text="Fair" ></asp:ListItem>
        <asp:ListItem Value="Poor" Text="Poor" ></asp:ListItem>
        </asp:RadioButtonList>
       </fieldset>
       <br />--%>
     <br />
       <table style="border:3px solid;border-color:green;border-radius:8px;border-spacing:10px;">
           <tr>
                                    <td align="left" class="stylespc">
                                           <asp:Label ID="Label1" runat="server" Text="Product Rating" Font-Size="14px" ForeColor="Black" Font-Bold="true" Font-Names="Verdana"></asp:Label>

                                    </td>
                                     <td align="left" class="stylespc">
                                           <asp:RadioButtonList ID="rdoproduct" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="Excellent" Text="Excellent"></asp:ListItem>
                                    <asp:ListItem Value="Good" Text="Good"></asp:ListItem>
                                    <asp:ListItem Value="Fair" Text="Fair"></asp:ListItem>
                                    <asp:ListItem Value="Poor" Text="Poor"></asp:ListItem>
                                </asp:RadioButtonList>
                                     </td>
                                </tr>
                                <tr>
                                    <td align="left" class="stylespc">  <asp:Label ID="Label2" runat="server" Text="Service Rating" Font-Size="14px" ForeColor="Black" Font-Bold="true" Font-Names="Verdana"></asp:Label>
</td>

                                      <td align="left" class="stylespc">
                                            <asp:RadioButtonList ID="rdoService" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="Excellent" Text="Excellent"></asp:ListItem>
                                    <asp:ListItem Value="Good" Text="Good"></asp:ListItem>
                                    <asp:ListItem Value="Fair" Text="Fair"></asp:ListItem>
                                    <asp:ListItem Value="Poor" Text="Poor"></asp:ListItem>
                                </asp:RadioButtonList>

                                      </td>
                                </tr>
       <tr>
       <td align="left" class="stylespc">
       <asp:Label ID="lblarea" runat="server" Text="Area of Problem" Font-Size="14px" ForeColor="Black" Font-Bold="true" Font-Names="Verdana"></asp:Label>
   </td>
   <td align="left" class="stylespc">
     <asp:DropDownList ID="ddlarea" SkinID="ddlRequired" runat="server">
     <asp:ListItem Value="-1" Text="--Select--" ></asp:ListItem>
      <asp:ListItem Value="0" Text="Nil" ></asp:ListItem>
     <asp:ListItem Value="1" Text="Support at Field Level" ></asp:ListItem>
     <asp:ListItem Value="2" Text="Support at Admin Level" ></asp:ListItem>
     <asp:ListItem Value="3" Text="New Module Implementation" ></asp:ListItem>
     <asp:ListItem Value="4" Text="Existing Module Errors - Web" ></asp:ListItem>
     <asp:ListItem Value="5" Text="Existing Module Errors - App" ></asp:ListItem>
     <asp:ListItem Value="6" Text="Existing Module Errors - Detailing" ></asp:ListItem>
     <asp:ListItem Value="7" Text="Additional Report Requirement" ></asp:ListItem>
     <asp:ListItem Value="8" Text="Training Needed through Web" ></asp:ListItem>
     <asp:ListItem Value="9" Text="IVR Related" ></asp:ListItem>
     <asp:ListItem Value="10" Text="Mobile APP - Related" ></asp:ListItem>
     <asp:ListItem Value="11" Text="Detailing through Tab - Related" ></asp:ListItem>
     <asp:ListItem Value="12" Text="Repeated Errors" ></asp:ListItem>
     <asp:ListItem Value="13" Text="Others" ></asp:ListItem>
     </asp:DropDownList>
    </td>
    </tr>
     <tr>
     <td align="left" class="stylespc">
      <asp:Label ID="lblCon" runat="server" Text="Contact No" Font-Size="14px" ForeColor="Black" Font-Bold="true" Font-Names="Verdana"></asp:Label>
      </td>
      <td align="left" class="stylespc">
       <asp:TextBox ID="txtCon" runat="server" SkinID="MandTxtBox"></asp:TextBox>
       </td>
       </tr>
       <tr>
       <td align="left" class="stylespc">
          <asp:Label ID="lblCom" runat="server" Font-Size="14px" ForeColor="Black" Font-Bold="true" Font-Names="Verdana"> Comments for the Problem
              
            </asp:Label>
            </td>
            <td align="left" class="stylespc">
             <asp:TextBox ID="txtcom" runat="server" Width="400px" Height="100px" TextMode="MultiLine"></asp:TextBox>
             </td>
             </tr>
           <tr>
          
          <td colspan="2" align="center">
            <asp:Label ID="lblInd" runat="server" Font-Size="14px" ForeColor="Black" Font-Bold="true" Font-Names="Verdana">
                Would you Recommended our Product/Service to Colleagues or Contacts within your Industry?</asp:Label>
              </td>
         </tr>
         <tr>
              <td  colspan="2" align="center">
              <asp:RadioButton ID="rdoYes" runat="server" Text="Yes"  Font-Bold="true" GroupName="Multi" />
                               <asp:RadioButton ID="rdoNo"  runat="server" Text="No" Font-Bold="true" GroupName="Multi" />
                </td>
                   </tr>
                             <tr>
       <td align="left" class="stylespc">   
                           <asp:Label ID="lblRem" runat="server" Font-Size="14px" ForeColor="Black" Font-Bold="true" Font-Names="Verdana">Remarks</asp:Label>
                           </td>
                           <td align="left" class="stylespc">
            <asp:TextBox ID="txtRem" runat="server"  Width="400px" TextMode="MultiLine"></asp:TextBox>
            </td>
            </tr>
           <tr>
               <td colspan="2" align="center">
                   	<asp:Button ID="btnSubmit" runat="server" CssClass="form-submit-button" Height="40px" Width="100px" Font-Bold="true" 
                            Text="Submit" onclick="btnsubmit_Click" />
               </td>

           </tr>
            </table>
                        </div>
						
							
								
					
				</div>
		
        </div>
      
    
    <br />
    <center>
        <asp:Image ID="imgthank" runat="server" ImageUrl="Images/thanks.gif" />
    </center>
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="Images/loader.gif" alt="" />
        </div>
     </center>  
    </form>
</body>
</html>
