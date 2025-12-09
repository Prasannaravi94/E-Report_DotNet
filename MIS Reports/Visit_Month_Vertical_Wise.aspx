<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visit_Month_Vertical_Wise.aspx.cs" Inherits="MIS_Reports_Visit_Details_Basedonfield" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Visit Analysis</title>
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>

    <script type="text/javascript">
        var popUpObj;
        var randomnumber = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, sf_name, cmode, cLvl, div_code) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("Visit_Month_Vertical_Wise_Report.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&sf_name=" + sf_name + "&cMode=" + cmode + "&cLvl=" + cLvl + "&div_code=" + div_code,
        "ModalPopUp" + randomnumber//,
        //"toolbar=no," +
        //"scrollbars=yes," +
        //"location=no," +
        //"statusbar=no," +
        //"menubar=no," +
        //"addressbar=no," +
        //"resizable=yes," +
        //"width=900," +
        //"height=600," +
        //"left = 0," +
        //"top=0"
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
                <%--var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "Select") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
                var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }
                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }--%>
                var type = $('#<%=ddlType.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Type."); $('#ddlType').focus(); return false; }
                var mode = $('#<%=ddlMode.ClientID%> :selected').text();
                if (mode == "---Select---") { alert("Select Mode."); $('#ddlMode').focus(); return false; }

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                <%--var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                var FMonth = document.getElementById('<%=ddlFMonth.ClientID%>').value;
                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                var TMonth = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>
                var ddMdINDEX = $('#ddlType').find(":selected").index();
                var cLvl = document.getElementById('<%=ddlLvl.ClientID%>').value;

                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var fromMon = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var fromYear = frmMonYear[1];

                var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var ToMon = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var ToYear = ToMonYear[1];
                    var div_code = "";
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
                if (ddMdINDEX != 0 && ddMdINDEX != 4) {
                    <%--var frmYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                    var frmMonth = $('#ddlFMonth').find(":selected").index();
                    var toYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                    var toMonth = $('#ddlTMonth').find(":selected").index();--%>
                    var mnth = fromMon, yr = parseInt(fromYear), validate = '', tmp = '';
                    if ((fromMon <= ToMon && parseInt(fromYear) === parseInt(ToYear)) || (parseInt(fromYear) < parseInt(ToYear) && (fromMon <= ToMon || fromMon >= ToMon))) {
                        showModalPopUp(sf_Code, fromMon, fromYear, ToMon, ToYear, SName, ddMdINDEX, cLvl, div_code);
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
        <div>
            <div id="Divid" runat="server"></div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Visit Analysis</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Field Force Name"></asp:Label>
                                <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                                    CssClass="nice-select" Visible="false">
                                    <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                    OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" CssClass="nice-select">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="false" Width="100%">
                                    
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblMR" runat="server" Text="Base Level" CssClass="label" Visible="false"></asp:Label>
                                <asp:Label ID="lblLvl" runat="server" Text="Level" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlMR" runat="server" CssClass="nice-select" Visible="false"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlMR_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlLvl" runat="server" CssClass="nice-select">
                                    <asp:ListItem Text="MR" Value="1" />
                                    <asp:ListItem Text="Manager" Value="2" />
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblFMonth" runat="server" CssClass="label" Text="From Month-Year"></asp:Label>
                                        <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <%--         <div class="col-lg-6">
                                        <asp:Label ID="lblFMonth" runat="server" CssClass="label" Text="From Month"></asp:Label>
                                        <asp:DropDownList ID="ddlFMonth" runat="server" CssClass="nice-select">
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
                                    </div>--%>
                                     <%--<div class="col-lg-6">
                                       <asp:Label ID="lblFYear" runat="server" CssClass="label" Text="From Year"></asp:Label>

                                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired">
                                           <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                           <asp:ListItem Value="2011" Text="2011"></asp:ListItem>
                                          <asp:ListItem Value="2012" Text="2012"></asp:ListItem>
                                           <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                                           <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                                           <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                                           </asp:DropDownList>
                                        <asp:DropDownList ID="ddlFYear" runat="server" AutoPostBack="true" CssClass="nice-select">
                                        </asp:DropDownList>--%>
                                    </div>
                                </div>
                            </div>

                            <%--<div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblTMonth" runat="server" CssClass="label" Text="To Month"></asp:Label>
                                        <asp:DropDownList ID="ddlTMonth" runat="server" CssClass="nice-select">
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
                                    <div class="col-lg-6">

                                        <asp:Label ID="lblTYear" runat="server" CssClass="label" Text="To Year" Width="55"></asp:Label>
                                        <asp:DropDownList ID="ddlTYear" runat="server" AutoPostBack="true" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>

                            <div class="single-des clearfix">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Type"></asp:Label>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="nice-select">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Category"></asp:ListItem>
                                    <%-- <asp:ListItem Value="2" Text="Speciality"></asp:ListItem>
                                      <asp:ListItem Value="3" Text="Class"></asp:ListItem>
                                         <asp:ListItem Value="4" Text="Campaign"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblMode" runat="server" CssClass="label" Text="Mode" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlMode" runat="server" CssClass="nice-select" Visible="false">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Self"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Team"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
        <!-- Bootstrap -->
        <script type="text/javascript" src="../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap-datepicker.js"></script>
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
