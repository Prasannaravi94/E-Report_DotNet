<%@ Page Language="C#" AutoEventWireup="true" CodeFile="password2.aspx.cs" Inherits="MasterFiles_password2" %>



<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
    <title>SFC/Fare Updation</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

        <script type="text/javascript" >

        //    $(window).on("load", function () {
              
        //        $("#block").show();
        //    $("#access").hide();
        //});

        //    $(document).ready(function () {                
        //        $("#block").show();
        //        $("#access").hide();
        //    $("#testImg").hide();
        //    $(".custom-select2").select2();
        //    $('#linkcheck').click(function () {
        //        window.setTimeout(function () {
        //            $("#testImg").show();
        //        }, 500);
        //    })
        //});

   
            function Validate_Auth() {
                var Auth = "til@san";
                if ($("#password").val() == Auth) {
                   
                    window.location.replace("http://www.torssfa.info/MasterFiles/AllowanceFixation.aspx");
                    //$("#block").hide();
                    //$("#access").show();
                } else {
                 
                    //$("#block").show();
                    //$("#access").hide();
                    if (Auth == "") { alert("Enter Password"); } else { alert("Enter Correct Password") }
                    
                }

            }
         

   
    
  </script>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
  
    <link href="../assets/css/select2.min.css" rel="stylesheet" />
    <style type="text/css">
        td, th {
            padding: 10px;
            text-align: center;
        }

        .current {
            display: block;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            width: 250px;
        }
          #tableId select
        {
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            color: #90a1ac;
            font-size: 14px;
            width: 100%;
            padding-left: 20px;
            height: 43px;
        }
        /*table tr td {
        border:2px solid black;
        }*/
    </style>
</head>

    <body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server"></div>

      <div id="block" >

                <h3 class="text-center">Authorized Personals Only</h3>
            <br />  <br /> 
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="designation-area clearfix">-
                            <div class="single-des clearfix">
                                
                                <div class="row clearfix">
                                    <div class="col-lg-5"></div>
                                    <div class="col-lg-2">
                                        <asp:textbox ID="password" TextMode="Password" placeholder="Enter Password" runat="server" Visible="True" Width="100%" ></asp:textbox>
                                           
                                    
                                    </div>

                                    <div class="col-lg-4" style="margin-left: 20px;">
                                        <asp:Button ID="Button1" runat="server" Width="65px" Text="Go" CssClass="savebutton" Visible="true" OnClientClick="Validate_Auth(); return false;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>


        </div></form></body>