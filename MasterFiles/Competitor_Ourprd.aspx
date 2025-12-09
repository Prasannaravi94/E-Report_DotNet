<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Competitor_Ourprd.aspx.cs" Inherits="MasterFiles_Competitor_Ourprd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Our Product - Competitor Product Map</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
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
        
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
  <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
   
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //  $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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
                if ($("#txtprd").val() == "") { alert("Select Competitor Product."); $('#txtprd').focus(); return false; }

            });
        });
    </script>

    <script type="text/javascript">
        function Checkedvalues() {
            var checkbox = document.getElementById('<%=Chkprd.ClientID%>');
            var options = checkbox.getElementsByTagName('input');
            var listspan = checkbox.getElementsByTagName('span');

            var checkednames = "";

            for (var i = 0; i < options.length; i++) {

                if (options[i].checked) {
                    var text = options[i].parentNode.getElementsByTagName("LABEL")[0].innerHTML;
                    checkednames += text+',';
                    
                    //                    alert(listspan[i].attributes["JSvalue"].value);
                }
            }



            document.getElementById('txtprd').value = checkednames.replace(/,\s*$/, "");
        }
    </script>

     <script type="text/javascript">

         function HidePopupFF() {
             var popup = $find('txtprd_PopupControlExtender');
             popup.hidePopup();
             return false;
           
            
         }
    </script>

      <script type="text/javascript">
          $(function () {
              var $txt = $('input[id$=txtNew]');
              var $ddl = $('select[id$=ddlour_prd]');
              var $items = $('select[id$=ddlour_prd] option');

              $txt.keyup(function () {
                  searchDdl($txt.val());
              });

              function searchDdl(item) {
                  $ddl.empty();
                  var exp = new RegExp(item, "i");
                  var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                  if (arr.length > 0) {
                      countItemsFound(arr.length);
                      $.each(arr, function () {
                          $ddl.append(this);
                          $ddl.get(0).selectedIndex = 0;
                      }
                    );
                  }
                  else {
                      countItemsFound(arr.length);
                      $ddl.append("<option>No Items Found</option>");
                  }
              }

              function countItemsFound(num) {
                  $("#para").empty();
                  if ($txt.val().length) {
                      $("#para").html(num + " items found");
                  }

              }
          });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <ucl:Menu ID="menu1" runat="server" />

   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

    <br />

    <center>
    

    <table >
      <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblourprd" runat="server" SkinID="lblMand" Height="18px">Our Product</asp:Label>
                    </td>
                    <td class="stylespc" align="left">
                        <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="200px" CssClass="TEXTAREA" AutoPostBack="true"
            ToolTip="Enter Text Here"></asp:TextBox>
                        <asp:DropDownList ID="ddlour_prd" runat="server" onblur="this.style.backgroundColor='White'" Width="250px"
                            onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired"
                             TabIndex="7" AutoPostBack="true" onselectedindexchanged="ddlour_prd_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </td>
                </tr>

                     <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblprd" runat="server" SkinID="lblMand" Height="18px">Competitor Product</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
               <%--         <asp:UpdatePanel ID="updatepanel1" runat="server">
                            <ContentTemplate>--%>
                                <asp:TextBox ID="txtprd" ReadOnly="true" SkinID="MandTxtBox" 
                                 Width="300px" runat="server" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    ></asp:TextBox>
                                <asp:PopupControlExtender ID="txtprd_PopupControlExtender" runat="server" DynamicServicePath=""
                                    Enabled="True" ExtenderControlID="" TargetControlID="txtprd" PopupControlID="Panel1"
                                    OffsetY="22">
                                </asp:PopupControlExtender>
                                <asp:Panel ID="Panel1" runat="server" Height="300px" Width="305px" BorderStyle="Solid"
                                    BorderWidth="2px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                    Style="display: none">

                                    <div style="height: 15px; position: relative; background-color: #4682B4; overflow-y: scroll;
                                                    text-transform: capitalize; width: 100%; float: left" align="right">
                                                    <asp:Button ID="Button2" BackColor="Yellow" Style="font-family: Verdana; height: 15px;
                                                        font-size: 5pt; width: 20px; color: Black; margin-top: -1px;" Text="X" runat="server"
                                                         OnClientClick="return HidePopupFF();" />
                                                </div>
                                    <asp:CheckBoxList ID="Chkprd" runat="server" OnClick="return Checkedvalues()" >
                                    </asp:CheckBoxList>
                                </asp:Panel>
                      <%--      </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </td>
                </tr>
    </table>
    <br />

    <asp:Button ID="btnSubmit"  runat="server" Width="60px" Height="25px"
                            CssClass="BUTTON" Text="Save" OnClick="btnSubmit_Click" />

    </center>
    
    </div>
    </form>
</body>
</html>
