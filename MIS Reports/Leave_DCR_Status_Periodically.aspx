<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Leave_DCR_Status_Periodically.aspx.cs" Inherits="MIS_Reports_Leave_DCR_Status_Periodically" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Leave Status - Periodically</title>
    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript">

        var popUpObj;
        function showModalPopUp(sfcode, txtEffFrom, txtEffTo, sf_name, Detailed) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptLeave_DCR_Status_Periodically.aspx?sfcode=" + sfcode + "&txtEffFrom=" + txtEffFrom + "&txtEffTo=" + txtEffTo + "&sf_name=" + sf_name + "&Detailed=" + Detailed,
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

            if (Detailed == 1) {
                $(popUpObj.document.body).ready(function () {

                    //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                    //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                    var ImgSrc = "https://s22.postimg.org/ie640w5dd/tumblr_mvg7t4_Ey_T41qa5rnho1_500.gif"

                    //  var ImgSrc = "https://s9.postimg.org/95yy2iikf/triangle_square_animation_ook.gif"

                    // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                    $(popUpObj.document.body).append('<div><p style="color:red; width:180px; margin:0 auto;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:600px; height: 400px;position: fixed;top: 10%;left: 15%;"  alt="" /></div>');

                    // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
                });
            }
            else {
                $(popUpObj.document.body).ready(function () {

                    //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                    //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                    //var ImgSrc = "https://s22.postimg.org/ie640w5dd/tumblr_mvg7t4_Ey_T41qa5rnho1_500.gif"

                    var ImgSrc = "https://s9.postimg.org/95yy2iikf/triangle_square_animation_ook.gif"

                    // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                    $(popUpObj.document.body).append('<div><p style="color:red; width:180px; margin:0 auto;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:600px; height: 400px;position: fixed;top: 10%;left: 15%;"  alt="" /></div>');

                    // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
                });
            }

        }
    </script>

    <style type="text/css">
        .height {
            height: 15px;
        }

        .TEXTAREA {
        }
    </style>

    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <style type="text/css">
        .single-des [type="checkbox"]:not(:checked) + label, .single-des [type="checkbox"]:checked + label {
            padding-left: 1.15em;
        }
    </style>


</head>
<body>
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

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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

                if ($("#txtEffFrom").val() == "") { alert("Select From date."); $('#txtEffFrom').focus(); return false; }

                var txtEffFrom = document.getElementById('<%=txtEffFrom.ClientID%>').value;
                var txtEffTo = document.getElementById('<%=txtEffTo.ClientID%>').value;

                var chkDetail = document.getElementById("chkDetail");
                if (chkDetail.checked) {
                    var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;



                    showModalPopUp(sfcode, txtEffFrom, txtEffTo, SName, 1);

                }
                else {
                    var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;


                    showModalPopUp(sfcode, txtEffFrom, txtEffTo, SName, 0);

                }

            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[id$=txtEffFrom]').change(function () {
                if ($(this).val().length > 2) {
                    date = $(this).val();
                    var efftodate = '';
                    var emon = 0;
                    var edate = 0;
                    var eyear = 0;
                    var todate = $(this).val().split('/');
                    if (todate[0] == '01') {
                        if (todate[1] == '02')
                            edate = '28';
                        else
                            edate = '30';
                    }
                    else
                        edate = parseInt(todate[0]) - 1;

                    var evardate = edate.toString();

                    if (evardate.length == 1)
                        evardate = '0' + evardate;
                    if (todate[0] != '01') {
                        if (todate[1] != '12') {
                            emon = parseInt(todate[1]) + 1;

                            var evarmon = emon.toString();

                            if (evarmon.length == 1)
                                evarmon = '0' + evarmon;
                            efftodate = evardate + '/' + evarmon + '/' + todate[2];
                        }
                        else {
                            emon = 1;
                            var evarmon = emon.toString();

                            if (evarmon.length == 1)
                                evarmon = '0' + evarmon;
                            eyear = parseInt(todate[2]) + 1;
                            efftodate = evardate + '/' + evarmon + '/' + eyear;
                        }
                    }
                    else
                        efftodate = evardate + '/' + todate[1] + '/' + todate[2];
                    $('[id$=txtEffTo]').val(efftodate);

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


    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Leave Status - Periodically</h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <%--<asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>--%>
                                <%-- <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>--%>
                                <%--  <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                    ToolTip="Enter Text Here" Height="22px"></asp:TextBox>

                                <asp:LinkButton ID="linkcheck" runat="server"
                                    OnClick="linkcheck_Click">
                          <img src="../Images/Selective_Mgr.png" />
                                </asp:LinkButton>--%>

                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>

                                <%--  <div id="testImg">
                                    <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;" runat="server" /><span
                                        style="font-family: Verdana; color: Red; font-weight: bold;">Loading Please Wait...</span>
                                </div>--%>
                            </div>
                            <%--  <div class="single-des clearfix">
                                <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible="false">
                                </asp:DropDownList>
                            </div>--%>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblfrom" runat="server" CssClass="label" Text="From Date"></asp:Label>
                                <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input" Width="100%" onkeypress="Calendar_enter(event);"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom" CssClass="cal_Theme1"
                                    runat="server" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblto" runat="server" CssClass="label" Text="To Date"></asp:Label>
                                <asp:TextBox ID="txtEffTo" runat="server" CssClass="input" Width="100%" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="Detailed"></asp:Label>
                                <asp:CheckBox ID="chkDetail" runat="server" Text="." />
                            </div>
                        </div>

                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnGo" runat="server" Text="View"
                                CssClass="savebutton" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <%--  <center>
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="75%">
            </asp:Table>
            <asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue" Visible="false" Height="20px" BorderColor="Black"  BorderStyle="Solid" BorderWidth="2" Font-Bold="True" >No Records Found</asp:Label>
        </center>--%>

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
        </div>

    </form>
</body>
</html>

