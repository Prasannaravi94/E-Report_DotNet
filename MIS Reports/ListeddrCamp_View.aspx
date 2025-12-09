<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListeddrCamp_View.aspx.cs" Inherits="MIS_Reports_ListeddrCamp_View" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Campaign Drs / Core Drs - View</title>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name, campcode, campname) {

            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Campaign_View_FF.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + " &camp_code=" + campcode + " &campaign=" + campname,
  "ModalPopUp"//,
  //"toolbar=no," +
  //"scrollbars=yes," +
  //"location=no," +
  //"statusbar=no," +
  //"menubar=no," +
  //"addressbar=no," +
  //"resizable=yes," +
  //"width=800," +
  //"height=600," +
  //"left = 0," +
  //"top=0"
  );
            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s8.postimg.cc/n2pneuc2t/Load_graph.jpg"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
            // LoadModalDiv();
        }
        function showModalPopUp_Core(sfcode, fmon, fyr, tyear, tmonth, sf_name) {

            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Core_Dr_View_FF.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name,
  "ModalPopUp"//,
  //"toolbar=no," +
  //"scrollbars=yes," +
  //"location=no," +
  //"statusbar=no," +
  //"menubar=no," +
  //"addressbar=no," +
  //"resizable=yes," +
  //"width=800," +
  //"height=600," +
  //"left = 0," +
  //"top=0"
  );
            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s8.postimg.cc/u5xiubrr9/loading-red-spot.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
            // LoadModalDiv();
        }
    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {


            $('#btnSubmit').click(function () {


                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "--Select--") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
                var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "--Select--") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }

                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                if (TYear == "--Select--") { alert("Select From Year."); $('#ddlToYear').focus(); return false; }
                var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "--Select--") { alert("Select From Month."); $('#ddlToMonth').focus(); return false; }



                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Frm_year = document.getElementById('<%=ddlFYear.ClientID%>').value;
                var Frm_Month = document.getElementById('<%=ddlFMonth.ClientID%>').value;
                var To_year = document.getElementById('<%=ddlTYear.ClientID%>').value;
                var To_Month = document.getElementById('<%=ddlTMonth.ClientID%>').value;

                var sType = document.getElementById('<%=ddlmode.ClientID%>').value;
                if (sType == "0") {
                    var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                    if (sf_name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                    var campaign = $('#<%=ddlCampaign.ClientID%> :selected').text();

                    var camp_code = document.getElementById('<%=ddlCampaign.ClientID%>').value;
                    if (campaign == "---Select---") { alert("Select Campaign."); $('#ddlCampaign').focus(); return false; }

                    showModalPopUp(sf_Code, Frm_Month, Frm_year, To_year, To_Month, sf_name, camp_code, campaign);
                }
                else if (sType == "1") {
                    var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                        if (sf_name == "---Select Clear---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                        showModalPopUp_Core(sf_Code, Frm_Month, Frm_year, To_year, To_Month, sf_name);
                    }
            });
        });
    </script>
    <%--  <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });
               
</script> --%>



    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.on('keyup', function () {
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
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <h2 class="text-center" id="dheading" runat="server"></h2>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%"  Visible="true"></asp:DropDownList> <%--Enabled="false"--%>
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select"></asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <div style="width: 100%">
                                <div style="width: 45%; float: left;">
                                    <asp:Label ID="lblFMonth" runat="server" CssClass="label" Text="From Month"></asp:Label>
                                    <asp:DropDownList ID="ddlFMonth" runat="server" CssClass="nice-select">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                                <div style="width: 45%; float: right;">
                                    <asp:Label ID="lblFYear" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                    <asp:DropDownList ID="ddlFYear" runat="server" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="single-des clearfix">
                            <div style="width: 100%">
                                <div style="width: 45%; float: left;">
                                    <asp:Label ID="lblTMonth" runat="server" CssClass="label" Text="To Month"></asp:Label>
                                    <asp:DropDownList ID="ddlTMonth" runat="server" CssClass="nice-select">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                                <div style="width: 45%; float: right;">
                                    <asp:Label ID="lblTYear" runat="server" CssClass="label" Text="To Year"></asp:Label>
                                    <asp:DropDownList ID="ddlTYear" runat="server" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblmode" runat="server" CssClass="label" Text="Mode"></asp:Label>
                            <asp:DropDownList ID="ddlmode" runat="server" CssClass="nice-select">
                                <asp:ListItem Value="0">Campaign View</asp:ListItem>
                                <asp:ListItem Value="1">Core Dr View</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblCamp" runat="server" Text="Campaign" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlCampaign" runat="server" CssClass="nice-select"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />
                    </div>
                </div>
            </div>
        </div>       
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
