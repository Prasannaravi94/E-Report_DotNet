<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product_Exp_Detail.aspx.cs" Inherits="MIS_Reports_Product_Exp_Detail" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Exposure Analysis</title>
    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, ddlprod, prd_name, SName, div_code, modee) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptProduct_Exp_Detail.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&Prod=" + ddlprod + "&Prod_Name=" + prd_name + "&sf_name=" + SName + "&div_code=" + div_code+ "&modee="+modee,
    "ModalPopUp"//,
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
            //LoadModalDiv();
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
                var prd_name = $('#<%=ddlprod.ClientID%> :selected').text();
                if (prd_name == "---Select the Product---") { alert("Select the Product."); $('#ddlprod').focus(); return false; }
                <%--var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
                var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }
                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }--%>
                var mode = $('#<%=ddlmode.ClientID%> :selected').text();
                if (mode == "---Select---") { alert("Select Mode."); $('#ddlmode').focus(); return false; }

                //                var TYear = $('#<%=ddlMR.ClientID%> :selected').text();
                //                if (TYear == "---Select---") { alert("Select Baselevel."); $('#ddlMR').focus(); return false; }

                // var SName = $('#<%=ddlMR.ClientID%> :selected').text();
                //if (SName == "---Select---") { alert("Select Base Level."); $('#ddlMR').focus(); return false; }

                var ddlprod = document.getElementById('<%=ddlprod.ClientID%>').value;
                var modee = document.getElementById('<%=ddlmode.ClientID%>').value;
                var div_code = "";

                var fromMon = "", fromYear = "", ToMon = "", ToYear = "";

                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                Month1 = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                Year1 = frmMonYear[1];

                var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                Year2 = ToMonYear[1];

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
                if (ddlprod != -1) {

                    var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                    <%--var Year1 = document.getElementById('<%=ddlFYear.ClientID%>').value;
                    var Month1 = document.getElementById('<%=ddlFMonth.ClientID%>').value;
                    var Year2 = document.getElementById('<%=ddlTYear.ClientID%>').value;
                    var Month2 = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>

                    if (Month1 > Month2 && Year1 == Year2) {
                        alert("To Month must be greater than From Month"); return false;
                    }
                    else if (Year1 > Year2) {
                        alert("To Year must be greater than From Year"); return false;
                    }
                    else {
                        if (Year1 <= Year2) {
                            showModalPopUp(sfcode, Month1, Year1, Month2, Year2, ddlprod, prd_name, SName, div_code, modee);

                        }
                    }
                }
                else {
                    var ddlMR = document.getElementById('<%=ddlMR.ClientID%>').value;


                    if (ddlMR != 0) {

                        var ddlMR_name = $('#<%=ddlMR.ClientID%> :selected').text();

                        var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                        <%--var Year1 = document.getElementById('<%=ddlFYear.ClientID%>').value;
                        var Month1 = document.getElementById('<%=ddlFMonth.ClientID%>').value;
                        var Year2 = document.getElementById('<%=ddlTYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>

                        if (Month1 > Month2 && Year1 == Year2) {
                            alert("To Month must be greater than From Month"); return false;
                        }
                        else if (Year1 > Year2) {
                            alert("To Year must be greater than From Year"); return false;
                        }
                        else {
                            if (Year1 <= Year2) {
                                showModalPopUp(ddlMR, Month1, Year1, Month2, Year2, ddlprod, prd_name, ddlMR_name, div_code, modee);

                            }
                        }
                    }
                    else {
                        var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                        <%--var Year1 = document.getElementById('<%=ddlFYear.ClientID%>').value;
                        var Month1 = document.getElementById('<%=ddlFMonth.ClientID%>').value;
                        var Year2 = document.getElementById('<%=ddlTYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>

                        if (Month1 > Month2 && Year1 == Year2) {
                            alert("To Month must be greater than From Month"); return false;
                        }
                        else if (Year1 > Year2) {
                            alert("To Year must be greater than From Year"); return false;
                        }
                        else {
                            if (Year1 <= Year2) {
                                showModalPopUp(sfcode, Month1, Year1, Month2, Year2, ddlprod, prd_name, SName, div_code, modee);

                            }
                        }
                    }
                }
            });
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
            <div id="Divid" runat="server">
            </div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Product Exposure Analysis</h2>

                        <asp:Label ID="Lblmain" runat="server" SkinID="lblMand" Text="Product Exposure Analysis"></asp:Label>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <%--<asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>--%>
                                <%--    <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>--%>
                                <%--  <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                    ToolTip="Enter Text Here"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblmode" runat="server" CssClass="label" Text="Mode"></asp:Label>
                                <asp:DropDownList ID="ddlmode" runat="server" AutoPostBack="true" CssClass="nice-select" 
                                    OnSelectedIndexChanged="ddlmode_SelectedIndexChanged" >
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True" Text="Product Exposure"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Campaign Exposure"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Brand Exposure"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblprod" runat="server" CssClass="label" Text="Product Name"></asp:Label>
                                <asp:DropDownList ID="ddlprod" runat="server" AutoPostBack="true" CssClass="nice-select"
                                    OnSelectedIndexChanged="ddlprod_SelectedIndexChanged">
                                    <%--<asp:ListItem Value="0" Text ="All Product"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMR" runat="server" Text="Base Level" CssClass="label" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlMR" runat="server" CssClass="custom-select2 nice-select" Visible="false" Width="100%">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div style="float: right; width: 45%;">
                                    <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                </div>
                                <%--            <div style="float: left; width: 45%;">
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
                                <div style="float: right; width: 45%;">
                                    <asp:Label ID="lblFYear" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                    <asp:DropDownList ID="ddlFYear" runat="server" CssClass="nice-select"
                                        Width="60">
                                    </asp:DropDownList>
                                </div>--%>
                            </div>
                            <%--                    <div class="single-des clearfix">
                                <div style="float: left; width: 45%;">
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
                                <div style="float: right; width: 45%;">
                                    <asp:Label ID="lblTYear" runat="server" CssClass="label" Text="To Year"></asp:Label>
                                    <asp:DropDownList ID="ddlTYear" runat="server" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnGo" runat="server" Text="View" CssClass="savebutton" OnClick="btnGo_Click1" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
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
