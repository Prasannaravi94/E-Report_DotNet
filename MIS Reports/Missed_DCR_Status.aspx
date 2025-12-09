<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Missed_DCR_Status.aspx.cs" Inherits="MIS_Reports_Missed_DCR_Status" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Missed Status</title>
<link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
<%--    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />--%>
<%--    <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(cmon, cyear, SF_code) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptMissed_DCR_Status.aspx?cmon=" + cmon + "&cyear=" + cyear + "&SF_code=" + SF_code,
    "ModalPopUp",
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
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s3.postimg.org/x2mwp52dv/loading1.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
            //LoadModalDiv();
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
                var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }

                var cmon = document.getElementById('<%=ddlMonth.ClientID%>').value;
                var cyear = document.getElementById('<%=ddlYear.ClientID%>').value;
                var SF_code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                showModalPopUp(cmon, cyear, SF_code);

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
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Missed Status</h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <%--<asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>--%>
                                <%--  <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>--%>

                                <%--   <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                    ToolTip="Enter Text Here"></asp:TextBox>
                                <asp:LinkButton ID="linkcheck" runat="server"
                                    OnClick="linkcheck_Click">
                                 <img src="../Images/Selective_Mgr.png" />
                                </asp:LinkButton>--%>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>

                                <%--<asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
            ToolTip="Enter Text Here"></asp:TextBox>

            <asp:LinkButton ID="linkcheck" runat="server" 
                            onclick="linkcheck_Click">
                          <img src="../Images/Selective_Mgr.png" />
                            </asp:LinkButton>--%>

                                <%--                                <div id="testImg">
                                    <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;" runat="server" /><span
                                        style="font-family: Verdana; color: Red; font-weight: bold;">Loading Please Wait...</span>
                                </div>--%>
                            </div>

                            <%-- <div class="single-des clearfix">
                                <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible="false">
                                </asp:DropDownList>
                            </div>--%>
                         <%--   <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                            </div>--%>
                                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Month"></asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select">
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
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Year"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnGo" runat="server" Text="View" CssClass="savebutton" />
                        </div>
                    </div>
                </div>
            </div>
            <center>
                <%--<asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="75%">
            </asp:Table>--%>
                <%--<asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue" Visible="false" Height="20px" BorderColor="Black"  BorderStyle="Solid" BorderWidth="2" Font-Bold="True" >No Records Found</asp:Label>--%>
            </center>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>

        </div>
    </form>
</body>
</html>



