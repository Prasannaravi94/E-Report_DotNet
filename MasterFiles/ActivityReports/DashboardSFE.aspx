<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashboardSFE.aspx.cs" Inherits="MasterFiles_ActivityReports_DashboardSFE" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SFE (Dashboard)</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <link rel="shortcut icon" type="image/png" href="../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />
    <link rel="stylesheet" href="../assets/css/responsive.css" />
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, sf_name, mode) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptDashboardSFE.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&sf_name=" + sf_name + "&mode=" + mode,
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
                var FMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
                var FYear = $('#<%=ddlYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }



                var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;
                var sf_name = document.getElementById('<%=ddlFieldForce.ClientID%>').text;

                var mode = document.getElementById("<%=ddlmode.ClientID %>").value;

                showModalPopUp(sfcode, Month1, Year1, SName, mode);


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

    <style type="text/css">
        .button1 {
            border-top: 1px solid #96d1f8;
            background: #FF69B4;
            background: -webkit-gradient(linear, left top, left bottom, from(#ADFF2F), to(#FF69B4));
            background: -webkit-linear-gradient(top, #ADFF2F, #FF69B4);
            background: -moz-linear-gradient(top, #ADFF2F, #FF69B4);
            background: -ms-linear-gradient(top, #ADFF2F, #FF69B4);
            background: -o-linear-gradient(top, #ADFF2F, #FF69B4);
            padding: 5px 10px;
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
            -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
            box-shadow: rgba(0,0,0,1) 0 1px 0;
            text-shadow: rgba(0,0,0,.4) 0 1px 0;
            color: white;
            font-size: 14px;
            font-family: Georgia, serif;
            text-decoration: none;
            vertical-align: middle;
        }

            .button1:hover {
                border-top-color: #f4ad42;
                background: #f4ad42;
                color: black;
            }

            .button1:active {
                border-top-color: #1b435e;
                background: #1b435e;
            }

        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }

        .dd {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }

        .ddl1 {
            border: 1px solid #1E90FF;
            border-radius: 5px;
            width: 190px;
            height: 21px;
            font: bold;
            background-image: url('Images/arrow_sort_d.gif');
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }
		  #grdAppdashboard > tbody > tr:nth-child(1) > td:nth-child(1) {
            background-color: Lavender;
            position: sticky;
            left: 0px;
            z-index: 1;
            min-width: 50px;
        }

        #grdAppdashboard > tbody > tr:nth-child(1) > td:nth-child(2) {
            background-color: Lavender;
            position: sticky;
            left: 55px;
            z-index: 1;
        }

        #grdAppdashboard > tbody > tr:nth-child(1) > td:nth-child(3) {
            background-color: Lavender;
            position: sticky;
            left: 403px;
            z-index: 1;
        }
        #grdAppdashboard > tbody > tr:nth-child(1) > td:nth-child(4) {
            background-color: Lavender;
            position: sticky;
            left: 521px;
            z-index: 1;
        }
        #grdAppdashboard > tbody > tr:nth-child(n+3) > td:nth-child(1) {
            position: sticky;
            left: 0px;
            z-index: 1;
        }

        #grdAppdashboard > tbody > tr:nth-child(n+3) > td:nth-child(2) {
            background-color: white;
            position: sticky;
            left: 55px;
            z-index: 1;
        }

        #grdAppdashboard > tbody > tr:nth-child(n+3) > td:nth-child(3) {
            background-color: white;
            position: sticky;
            left: 403px;
            z-index: 1;
        }
     #grdAppdashboard > tbody > tr:nth-child(n+3) > td:nth-child(4) {
            background-color: white;
            position: sticky;
            left: 521px;
            z-index: 1;
        }
    </style>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
            $("[id*=ddlmode]").select2();
        });
    </script>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <center>
                        <table>
                            <tr>
                                <td align="center">
                                    <h2 class="text-center">SFE Dasboard</h2>
                                </td>
                            </tr>
                        </table>
                    </center>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false" CssClass="ddl">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <div style="float: left; width: 45%;">
                                <asp:Label ID="lblMonth" runat="server" CssClass="label" Text="Month"></asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired" CssClass="ddl">
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
                            <div style="float: right; width: 45%;">
                                <asp:Label ID="lblYear" runat="server" CssClass="label" Text=" Year"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" CssClass="ddl">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblmode" runat="server" Text="Mode" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlmode" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                <asp:ListItem Value="1" Text="Coverage"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Call Average"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Missed Call"></asp:ListItem>
                                <asp:ListItem Value="4" Text="No.of Days in Field"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Drs Visit - Categorywise"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Drs Missed - Categorywise"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Drs Visit - Classwise"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Drs Visit - Frequencywise"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Drs Missed - Frequencywise"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Consolidated - SFE"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btnGo" runat="server" Text="View" CssClass="savebutton" Enabled="false" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>
