<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Core_Drs.aspx.cs" Inherits="MasterFiles_AnalysisReports_Core_Drs" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Core Drs</title>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
        //      var popUpObj;

        //      function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name, campcode, campname) {

        //          //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
        //          popUpObj = window.open("rpt_Campaign_View_FF.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + " &camp_code=" + campcode + " &campaign=" + campname,
        //"ModalPopUp",
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
        //);
        //          popUpObj.focus();
        //          $(popUpObj.document.body).ready(function () {

        //              //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

        //              //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

        //              var ImgSrc = "https://s8.postimg.cc/n2pneuc2t/Load_graph.jpg"

        //              // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


        //              $(popUpObj.document.body).append('<div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');

        //              // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
        //          });
        //          // LoadModalDiv();
        //      }
        function showModalPopUp_Core(sfcode, fmon, fyr, tyear, tmonth, sf_name) {

            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Core_Dr_View_FF.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name,
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
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {


            $('#btnSubmit').click(function () {


                 <%--var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "--Select--") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
                var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "--Select--") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }

                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                if (TYear == "--Select--") { alert("Select From Year."); $('#ddlToYear').focus(); return false; }
                var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "--Select--") { alert("Select From Month."); $('#ddlToMonth').focus(); return false; }--%>



                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                <%--var Frm_year = document.getElementById('<%=ddlFYear.ClientID%>').value;
                var Frm_Month = document.getElementById('<%=ddlFMonth.ClientID%>').value;
                var To_year = document.getElementById('<%=ddlTYear.ClientID%>').value;
                var To_Month = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>

                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var Frm_Month = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var Frm_year = frmMonYear[1];

                var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var To_Month = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var To_year = ToMonYear[1];

                var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (sf_name == "---Select Clear---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                showModalPopUp_Core(sf_Code, Frm_Month, Frm_year, To_year, To_Month, sf_name);

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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">


                                <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>


                                <%--<asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlFFType_SelectedIndexChanged" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                    </asp:DropDownList>--%>
                                <%-- <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        onselectedindexchanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                    </asp:DropDownList>--%>

                                <asp:TextBox ID="txtNew" runat="server" Width="330px" CssClass="nice-select" Visible="false" Style="float: right"
                                    ToolTip="Enter Text Here"></asp:TextBox><br />
                                <asp:LinkButton ID="linkcheck" runat="server" OnClick="linkcheck_Click">
                                <img src="../../Images/Selective_Mgr.png" />
                                </asp:LinkButton>
                                <br />
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="nice-select" Visible="false"></asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select"></asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%--          <div class="single-des clearfix">
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
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFYear" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                <asp:DropDownList ID="ddlFYear" runat="server" CssClass="nice-select"></asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
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
                            <div class="single-des clearfix">
                                <asp:Label ID="lblTYear" runat="server" CssClass="label" Text="To Year"></asp:Label>
                                <asp:DropDownList ID="ddlTYear" runat="server" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>--%><br />
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <center>
                                    <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />
                                </center>
                            </div>
                            <br />
                        </div>

                    </div>
                </div>
            </div>
        </div>
                 <!-- Bootstrap Datepicker -->
        <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
                <script type="text/javascript">
                    $(function () {
                        $('[id*=txtFromMonthYear]').datepicker({
                            changeMonth: true,
                            changeYear: true,
                            format: "M-yyyy",
                            viewMode: "months",
                            minViewMode: "months",
                            language: "tr"
                        });

                        $('[id*=txtToMonthYear]').datepicker({
                            changeMonth: true,
                            changeYear: true,
                            format: "M-yyyy",
                            viewMode: "months",
                            minViewMode: "months",
                            language: "tr"
                        });
                    });
        </script>
    </form>
</body>
</html>
