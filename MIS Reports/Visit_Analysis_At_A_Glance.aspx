<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visit_Analysis_At_A_Glance.aspx.cs" Inherits="MIS_Reports_Visit_Details_Basedonfield" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu1" TagPrefix="ucl1" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu2" TagPrefix="ucl2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Visit Analysis</title>
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />

<script type = "text/javascript">
    var popUpObj;
    var randomnumber = Math.floor((Math.random() * 100) + 1);
    function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, sf_name) {
        //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
        popUpObj = window.open("Visit_Analysis_At_A_Glance_Report.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&sf_name=" + sf_name,
    "ModalPopUp"+randomnumber,
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=900," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
        popUpObj.focus();
        //
        $(popUpObj.document.body).ready(function () {
            //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
            var ImgSrc = "https://s3.postimg.org/x2mwp52dv/loading1.gif"
            $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
        });        
    }

    function showMissedDR(sfcode, fmon, fyr, cmode) {
        //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
        popUpObj = window.open("VisitDetList.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&cMode=" + cmode,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=1100," +
    "height=1000," +
    "left = 100," +
    "top=100"
    );
        popUpObj.focus();
        //LoadModalDiv();
    }

</script>

   <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
                  var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                  if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
                  var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                  if (FYear == "Select") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
                  var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                  if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }
                  var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                  if (TYear == "---Select---") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }

                  var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                  var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                  var FMonth = document.getElementById('<%=ddlFMonth.ClientID%>').value;
                  var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                  var TMonth = document.getElementById('<%=ddlTMonth.ClientID%>').value;
                  //var ddMdINDEX = $('#ddlType').find(":selected").index();
                  var ddMdINDEX = 1;

                  if (ddMdINDEX != 0 && ddMdINDEX != 4) {
                      var frmYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                      var frmMonth = $('#ddlFMonth').find(":selected").index();
                      var toYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                      var toMonth = $('#ddlTMonth').find(":selected").index();
                      var mnth = frmMonth, yr = parseInt(frmYear), validate = '', tmp = '';
                      if ((frmMonth <= toMonth && parseInt(frmYear) === parseInt(toYear)) || (parseInt(frmYear) < parseInt(toYear) && (frmMonth <= toMonth || frmMonth >= toMonth))) {
                          showModalPopUp(sf_Code, FMonth, FYear, TMonth, TYear, SName);
                      }
                      else {
                          alert("Select Valid Month & Year...");
                          $('#ddlFMonth').focus(); return false;
                      }
                      //showModalPopUp(sf_Code, Month1, Year1, Name, ddMdINDEX);
                  }
              });
          }); 
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

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server"></div>
        <center>    
            <br />
            <table>
                <tr>
                    <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Field Force Name"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired" Visible="false">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" AutoPostBack="false"> <%--<onselectedindexchanged="ddlFieldForce_SelectedIndexChanged">--%>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr style="display:none;">
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible ="false" ></asp:Label>
                        <asp:Label ID="lblLvl" runat="server" Visible="false" Text="Level" SkinID="lblMand" ></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible ="false" 
                        AutoPostBack="true" onselectedindexchanged="ddlMR_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList ID="ddlLvl" runat="server" Visible="false" SkinID="ddlRequired">
                            <asp:ListItem Text="MR" Value="1" />
                            <asp:ListItem Text="Manager" Value="2" />
                        </asp:DropDownList>
                    </td>   
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="From Month"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>
                
                        <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="From Year"></asp:Label>
                  
                        <%--<asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="2011" Text="2011"></asp:ListItem>
                            <asp:ListItem Value="2012" Text="2012"></asp:ListItem>
                            <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                            <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                            <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                        </asp:DropDownList>--%>
                          <asp:DropDownList ID="ddlFYear" runat="server" AutoPostBack="true" SkinID="ddlRequired" Width="60">
               </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblTMonth" runat="server" SkinID="lblMand" Text="To Month"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired" >
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>
                 
                        <asp:Label ID="lblTYear" runat="server" SkinID="lblMand" Text="To Year" Width="55"></asp:Label>
                         
                        <%--<asp:DropDownList ID="ddlTYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="2011" Text="2011"></asp:ListItem>
                            <asp:ListItem Value="2012" Text="2012"></asp:ListItem>
                            <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                            <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                            <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                        </asp:DropDownList>--%>
                         <asp:DropDownList ID="ddlTYear" runat="server" AutoPostBack="true" SkinID="ddlRequired" Width="60">
                         </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" Visible="false" SkinID="lblMand" Text="Type"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList Visible="false" ID="ddlType" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Category"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Speciality"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Class"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Campaign"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMode" runat="server" SkinID="lblMand" Text="Mode" Visible="false"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlMode" runat="server" SkinID="ddlRequired" Visible="false">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Self"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Team"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="savebutton" 
                />
                 <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
        </center>         
    
    </div>
    </form>
</body>
</html>
