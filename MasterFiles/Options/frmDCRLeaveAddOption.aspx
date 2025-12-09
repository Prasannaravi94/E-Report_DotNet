<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDCRLeaveAddOption.aspx.cs" Inherits="MasterFiles_Options_frmDCRLeaveAddOption" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Delete</title>
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"  rel="stylesheet" type="text/css" />

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
              $('#btnGo').click(function () {

                

              });
          });
    </script>
     <script type="text/javascript">
         $(function () {
             var $txt = $('input[id$=txtNew]');
             var $ddl = $('select[id$=ddlFieldForce]');
             var $items = $('select[id$=ddlFieldForce] option');

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
    <script type="text/javascript" language="javascript">
        function Altert() {
            if (confirm('Do you want to Delete?')) {
            }
            else {
                return false;
            }
        }

    </script>
     <script type="text/javascript">

         var j = jQuery.noConflict();
         j(document).ready(function () {
             j('.DOBDate').datepicker
         ({
             changeMonth: true,
             changeYear: true,
             yearRange: '1930:' + new Date().getFullYear().toString(),
             //                yearRange: "2010:2017",
             dateFormat: 'dd/mm/yy'
         });
         });

    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSub').click(function () {
                if ($("#txtFromdte").val() != "") {
                    if ($("#txtTodte").val() != "") {
                        return true;
                    }
                    else {
                        alert('Enter To Date')
                        return false;
                    }
                }
                else {
                    alert('Enter From Date')
                    return false;
                }

            })
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br /> 
        <center>
        <table >
            <tr>
                <td align="left" class="stylespc" width="120px">
                    <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>     
                    </td>
                    <td align="left" class="stylespc">  
                    
                      <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
            ToolTip="Enter Text Here"></asp:TextBox>          
                    <asp:DropDownList ID="ddlFieldForce" runat="server" Width="300px" SkinID="ddlRequired">
                    <asp:ListItem Selected="True" Value="-1" Text="---Select---"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
              
          
          
          <tr>
                      <td align="left" class="stylespc">
                         <asp:Label ID="lblfrom" runat="server" SkinID="lblMand" Text="From Date" ></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                       <asp:TextBox ID="txtFromdte" runat="server"  Height="22px" MaxLength="10" 
                        CssClass="DOBDate" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                         onkeypress="CheckNumeric(event);" SkinID="TxtBxNumOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc"> 
                        <asp:Label ID="lblto" runat="server" SkinID="lblMand" Text="To Date"></asp:Label>
                    </td>
                   <td align="left" class="stylespc">
                        <asp:TextBox ID="txtTodte" runat="server" SkinID="TxtBxNumOnly" Height="22px" MaxLength="10" 
                         CssClass="DOBDate" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                        onkeypress="CheckNumeric(event);" ></asp:TextBox>
                       
                    </td>
                </tr>
          
          </table>

          
       
          <br />
            <center>
                <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Text="Delete" OnClientClick="return Altert()" OnClick="btnSub_Click" />
            </center>
        <br />
       
    <br />
   
    </center>
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
