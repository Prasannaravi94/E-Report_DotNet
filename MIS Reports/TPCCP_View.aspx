<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TPCCP_View.aspx.cs" Inherits="MIS_Reports_TPCCP_View" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>CCP - View</title>
     <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />

     <script type="text/javascript">
         var popUpObj;
         function showModalPopUp(sfcode, fyr, sf_name) {
             //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
             popUpObj = window.open("rptTPCCP_View.aspx?sfcode=" + sfcode + "&FYear=" + fyr + "&sf_name=" + sf_name,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=800," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
             popUpObj.focus();
             // LoadModalDiv();
             $(popUpObj.document.body).ready(function () {

                 //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                 //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                 var ImgSrc = "https://s9.postimg.org/95yy2iikf/triangle_square_animation_ook.gif"

                 // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                 $(popUpObj.document.body).append('<div><p style="color:red; width:180px; margin:0 auto;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:600px; height: 400px;position: fixed;top: 10%;left: 15%;"  alt="" /></div>');

                 // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
             });
         }
    </script>
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var FYear = $('#<%=ddlYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }

                var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;

                var sf_name = document.getElementById('<%=ddlFieldForce.ClientID%>').text;

                showModalPopUp(sfcode, Year1, SName);


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
           $(document).ready(function () {
               $("#testImg").hide();
               $('#linkcheck').click(function () {
                   window.setTimeout(function () {
                       $("#testImg").show();
                   }, 500);
               })
           });              
</script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <div id="Divid" runat="server">
        </div>
    <br />
    
       <center>
           <br />
           <table >
               <tr>
                   <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Fieldforce Name"></asp:Label>
                    </td>
                    <td align="left">
                       <%-- <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>--%>
                      <%--  <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>--%>
                        <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
            ToolTip="Enter Text Here"></asp:TextBox>

            <asp:LinkButton ID="linkcheck" runat="server" 
                            onclick="linkcheck_Click">
                          <img src="../Images/Selective_Mgr.png" />
                            </asp:LinkButton>

                        <asp:DropDownList ID="ddlFieldForce" runat="server" Width="300px" Visible="false" SkinID="ddlRequired" >
                            
                        </asp:DropDownList>
                        
                        <%--<asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>--%>
                    </td>
                      <td>                   
                      <div id="testImg">
                        <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height:20px;" runat="server" /><span
                            style="font-family: Verdana; color: Red; font-weight: bold; ">Loading Please Wait...</span>
                     </div>
                    </td>
               </tr>
            
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblYear" runat="server" SkinID="lblMand" Text="Year"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlYear" runat="server" Width="80px" SkinID="ddlRequired">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                   
            <%--    </asp:Panel>--%>
                                      
             </table>
             <br />
              <asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="View" CssClass="savebutton" Enabled="false"
                 />

         </center>
          <br />
         <%--<br />
          <center>
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="90%">
            </asp:Table>
            <asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue" Visible="false" Height="20px" BorderColor="Black"  BorderStyle="Solid" BorderWidth="2" Font-Bold="True" >No Records Found</asp:Label>
        </center>
        <br />--%>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>

    </div>
    </form>
</body>
</html>
