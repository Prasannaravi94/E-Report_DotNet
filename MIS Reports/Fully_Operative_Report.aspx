<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fully_Operative_Report.aspx.cs"
    Inherits="MIS_Reports_Fully_Operative_Report" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fully Operative Report </title>
    <%--<link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="../../css/Font-Awesome-4.7.0/css/font-awesome.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, sfname, cmode, Vacant) {
            var randomnumber = Math.floor((Math.random() * 100) + 1);
            popUpObj = window.open("rpt_Fully_Operative_Report.aspx?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&TMonth=" + tmon + "&Tyear=" + tyr + "&sf_name=" + sfname + "&cMode=" + cmode + " &Vacant=" + Vacant,
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
                var ImgSrc = "https://s4.postimg.org/l4xbni8jx/loading13.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;"></p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:650px; height: 300px;position: fixed;top: 20%;left: 20%;"  alt="" /></div>');
            });
        }

        function showVisitDR_type(sfcode, fmon, fyr, cmode, modeval, novst) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("VisitDocList_noofvisit.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&cMode=" + cmode + "&vMode=" + modeval + "&novst=" + novst,
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
            //LoadModalDiv();
        }

        function showModalPopUp_HQ(sfcode, fmon, fyr, tmon, tyr, sfname, cmode, Vacant) {
            var randomnumber = Math.floor((Math.random() * 100) + 1);
            popUpObj = window.open("rpt_Fully_Operative_Report_HQ.aspx?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&TMonth=" + tmon + "&Tyear=" + tyr + "&sf_name=" + sfname + "&cMode=" + cmode + " &Vacant=" + Vacant,
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
                var ImgSrc = "https://s4.postimg.org/l4xbni8jx/loading13.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;"></p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:650px; height: 300px;position: fixed;top: 20%;left: 20%;"  alt="" /></div>');
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
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
                var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }
                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }--%>
                var type = $('#<%=ddlType.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Type."); $('#ddlType').focus(); return false; }

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                <%--var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                var FMonth = document.getElementById('<%=ddlFMonth.ClientID%>').value;
                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                var TMonth = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>
                var ddMdINDEX = $('#ddlType').find(":selected").index();

                //if (ddMdINDEX != 0 && ddMdINDEX != 4) {
                if (ddMdINDEX != 0) {
                    <%--var frmYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                    var frmMonth = $('#ddlFMonth').find(":selected").index();
                    var toYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                    var toMonth = $('#ddlTMonth').find(":selected").index();--%>

                    var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                    var frmMonth = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                    var frmYear = frmMonYear[1];

                    var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                    var ToMon = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var ToYear = ToMonYear[1];

                    var mnth = frmMonth, yr = parseInt(frmYear), validate = '', tmp = '';
                    //if ((frmMonth <= toMonth && parseInt(frmYear) === parseInt(toYear)) || (parseInt(frmYear) < parseInt(toYear) && (frmMonth <= toMonth || frmMonth >= toMonth))) {
                    //    showModalPopUp(sf_Code, FMonth, FYear, TMonth, TYear, SName, ddMdINDEX);
                    //}
                    //else {
                    //    alert("Select Valid Month & Year...");
                    //    $('#ddlFMonth').focus(); return false;
                    //}
                    //showModalPopUp(sf_Code, Month1, Year1, Name, ddMdINDEX);
                    if (ddMdINDEX == 1) {
                        showModalPopUp(sf_Code, frmMonth, frmYear, ToMon, ToYear, SName, ddMdINDEX, 0);
                    }
                    else if (ddMdINDEX == 2) {
                        showModalPopUp_HQ(sf_Code, frmMonth, frmYear, ToMon, ToYear, SName, ddMdINDEX, 0);
                    }

                }
            });
        });
    </script>

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
        .tblStyl {
            margin-left: 25px;
            margin-right: 20px;
        }

        .mGrids {
            width: 95%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
            margin-left: 15px;
            margin-right: 10px;
        }

            .mGrids td {
                padding: 2px;
                border: solid 1px Black;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                font-size: small;
                font-family: Calibri;
            }


            .mGrids th {
                padding: 4px 2px;
                color: white;
                background: #A6A6D2;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                border-bottom: solid 1px Black;
                font-weight: normal;
                font-size: small;
                font-family: Calibri;
            }

            .mGrids .pgr {
                background: #A6A6D2;
            }

                .mGrids .pgr table {
                    margin: 5px 0;
                }

                .mGrids .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    text-align: left;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: White;
                    line-height: 12px;
                }

                .mGrids .pgr th {
                    background: #A6A6D2;
                }

                .mGrids .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrids .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>
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
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Field Force Name"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="false" Width="100%">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="custom-select2 nice-select" Visible="false" Width="100%">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMR" runat="server" Text="Base Level" CssClass="label" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlMR" runat="server" CssClass="custom-select2 nice-select" Visible="false" Width="100%"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlMR_SelectedIndexChanged">
                                </asp:DropDownList>
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
                                    <%--<div class="col-lg-6">
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
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblFYear" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                        <asp:DropDownList ID="ddlFYear" runat="server" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>--%>
                                </div>
                            </div>
                            <%--                            <div class="single-des clearfix">
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
                                        <asp:Label ID="lblTYear" runat="server" CssClass="label" Text="To Year"></asp:Label>
                                        <asp:DropDownList ID="ddlTYear" runat="server" CssClass="nice-select"
                                            Width="60">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Type"></asp:Label>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="nice-select">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="FieldForcewise"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Line Manager HQ wise" ></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMode" runat="server" CssClass="label" Text="Mode" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlMode" runat="server" CssClass="nice-select" Visible="false">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Self" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Team"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <%-- <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="With Vacant"></asp:Label>
                                <asp:CheckBox ID="chkDetail" runat="server" Text="." />
                            </div>--%>
                            <div class="single-des clearfix">
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" Enabled="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="Panel1" runat="server">
                <table width="95%">
                    <caption>
                  <%--      <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                            AutoGenerateColumns="true" CssClass="mGrids" EmptyDataText="No Records Found"
                            GridLines="Both" HorizontalAlign="Center" BorderWidth="1"
                            OnRowCreated="GrdFixation_RowCreated" OnRowDataBound="GrdFixation_RowDataBound"
                            ShowHeader="False" Width="90%">--%>
                            <asp:GridView ID="GridView1" runat="server" AlternatingRowStyle-CssClass="alt"
                            AutoGenerateColumns="true" CssClass="mGrids" EmptyDataText="No Records Found"
                            GridLines="Both" HorizontalAlign="Center" BorderWidth="1" OnRowDataBound="GrdFixation_RowDataBound"
                            ShowHeader="False" Width="90%">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt" />
                            <RowStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            <Columns>
                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" ForeColor="Black"
                                Height="5px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </caption>
                </table>
            </asp:Panel>
            </left>
            <br />
            <br />
            <asp:Table ID="tbl" runat="server" CssClass="tblStyl" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="95%">
            </asp:Table>
            </center>
            <center>
                <asp:Label ID="norecords" Text="No Records Found" Visible="False" BorderColor="Black"
                    BorderStyle="Groove" BorderWidth="1px" Font-Bold="True" Font-Size="Small" BackColor="White"
                    runat="server" Width="752px"></asp:Label>
                <div class="loading" align="center">
                    Loading. Please wait.<br />
                    <br />
                    <img src="../Images/loader.gif" alt="" />
                </div>
            </center>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

        <!-- Bootstrap Datepicker -->
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
