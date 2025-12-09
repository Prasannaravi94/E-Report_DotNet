<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisitDetail_Datewise.aspx.cs" Inherits="MIS_Reports_VisitDetail_Datewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Visit Detail - DateWise</title>
    <%--    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, sf_name, checkmatrix, ddlmatrix, mode,div_code) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptVisitDetail_Datewise.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&sf_name=" + sf_name + "&checkmatrix=" + checkmatrix + "&ddlmatrix=" + ddlmatrix + "&mode=" + mode+ "&div_code=" + div_code,
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
                <%--var FMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
                var FYear = $('#<%=ddlYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }--%>
                var ddlmatrix = $('#<%=ddlmatrix.ClientID%> :selected').text();
                if (ddlmatrix == "--Select--") { alert("Select matrix."); $('#ddlmatrix').focus(); return false; }

                var ddlmode = $('#<%=ddlMode.ClientID%> :selected').text();
                if (ddlmode == "--Select--") { alert("Select Mode."); $('#ddlmode').focus(); return false; }

                var mode = document.getElementById('<%=ddlMode.ClientID%>').value;


                var ToMonYear = document.getElementById('<%=txtMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = ToMonYear[1];

                var checkmatrix = document.getElementById("checkmatrix");
                  var div_code;
                    var sf_type = '<%=Session["sf_type"] %>';

                    if (sf_type == 1 || sf_type == 2) {
                        div_code = '<%=Session["div_code"] %>';
                    }
                    else {
                        div_code = '<%=Session["div_code"] %>';
                        if (div_code.includes(",")) {
                            div_code = div_code.substring(0, div_code.length - 1)
                        }
                    }
                if (checkmatrix.checked) {

                    var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                    <%--var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                    var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;--%>

                    showModalPopUp(sfcode, Month1, Year1, SName, 1, ddlmatrix, 0, div_code);
                }
                else {

                    var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                    <%--var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                    var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;--%>

                    showModalPopUp(sfcode, Month1, Year1, SName, 0, ddlmatrix, mode,div_code);
                }

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
        input[type="checkbox"] + label, input[type="checkbox"]:checked + label {
            color: white;
        }
    </style>

    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <script type="text/javascript">
        function DisableCheckBox(opts) {
            if (opts.value == '2') {
                document.getElementById("lblmatrix").style.display = 'none';
                $("#checkmatrix").css('display', 'none');
                $("label[for='checkmatrix']").css('display', 'none');

            }
            else {
                document.getElementById("lblmatrix").style.display = 'block';
                $("#checkmatrix").css('display', 'block');
                $("label[for='checkmatrix']").css('display', 'block');
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%"></asp:DropDownList>
                            </div>
                            <%--                 <div class="single-des clearfix">
                                <asp:Label ID="lblMonth" runat="server" Text="Month" CssClass="label"></asp:Label>
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
                                <asp:Label ID="lblYear" runat="server" CssClass="label" Text="Year"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>--%>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Mode"></asp:Label>
                                <asp:DropDownList ID="ddlMode" runat="server" CssClass="nice-select" onchange="DisableCheckBox(this)">
                                    <asp:ListItem Selected="True" Value="0" Text="--Select--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Visit datewise(all Drs)"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Visit datewise(Campaign Drs)"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblmatrix" Text="Visit Matrix" CssClass="label" runat="server"></asp:Label>
                                <asp:CheckBox ID="checkmatrix" runat="server" Text="." AutoPostBack="true"
                                    OnCheckedChanged="checkmatrix_CheckedChanged" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:DropDownList ID="ddlmatrix" runat="server" Visible="false" CssClass="nice-select">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    <%--    <asp:ListItem Value="2" Text ="8 - 14"></asp:ListItem>
                                 <asp:ListItem Value="3" Text ="15 - 21"></asp:ListItem>
                                 <asp:ListItem Value="4" Text ="22 - 28"></asp:ListItem>
                                 <asp:ListItem Value="5" Text ="29 -30"></asp:ListItem>
                                 <asp:ListItem Value="6" Text ="29 -31"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnGo" runat="server" Text="View" CssClass="savebutton" Enabled="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>

        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

        <script type="text/javascript" src="../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=txtMonthYear]').datepicker({
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

