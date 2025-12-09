<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Status_View.aspx.cs" Inherits="MIS_Reports_doctorbusview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Status View</title>
    <%--<link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript">
        var popUpObj5;
        function showModalPopUp5(sfcode, sf_name, sf_short, sf_hq) {
            popUpObj5 = window.open("/MIS Reports/Listeddr_Chemists_Map.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&sf_short=" + sf_short + "&sf_hq=" + sf_hq + "&MR=1",
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
            popUpObj5.focus();

            $(popUpObj5.document.body).ready(function () {
                var ImgSrc = "https://s4.postimg.org/j1heaetwt/loading16.gif"
                $(popUpObj5.document.body).append('<div><p style="color:blue;margin-top:10%;margin-left:40%;">Loading Please Wait....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style="width:310px;height:300px;position:fixed;top:20%;left:30%;"  alt="" /></div>');
            });
        }

        var popUpObj;
        function showModalPopUp(sfcode, sf_name, sf_short, sf_hq) {
            popUpObj = window.open("/MIS Reports/Doctor_Prod_Map_Details.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&sf_short=" + sf_short + "&sf_hq=" + sf_hq,
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
            //LoadModalDiv();
        }
        var popUpObj22;
        function showModalPopUp22(sfcode, sf_name, sf_short, sf_hq, Div_code) {
            popUpObj22 = window.open("/MIS Reports/Listeddr_Campaign_Map.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&sf_short=" + sf_short + "&Div_code=" + Div_code + "&sf_hq=" + sf_hq + "&MR=0",
    "ModalPopUp" //,
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
            popUpObj22.focus();
            //LoadModalDiv();
        }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
        function showLoader(loaderType) {
            if (loaderType == "Search1") {
                document.getElementById("loaderSearchddlSFCode").style.display = '';
            }
        }
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
                var mode = $('#<%=ddlmode.ClientID%> :selected').text();
                if (mode == "---Select---") { alert("Select Mode."); $('#ddlmode').focus(); return false; }
            });
        });
    </script>
    <style type="text/css">
        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .dd {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
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
            text-overflow:;
        }

        .tr_Sno {
            background: #414d55;
            color: white;
            font-weight: 400;
            border-radius: 8px 0 0 8px;
            font-size: 12px;
            border-bottom: 10px solid #fff;
            font-family: Roboto;
            border-left: 0px solid #F1F5F8;
        }

        .tr_th {
            padding: 20px 15px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 12px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            text-transform: uppercase;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server"></div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <center>
                        <table>
                            <tr>
                                <td align="center">
                                    <h2 class="text-center">Status - View</h2>
                                </td>
                            </tr>
                        </table>
                    </center>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblmode" runat="server" Text="Mode" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlmode" runat="server" CssClass="nice-select">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Doctor Business Valuewise - Status"></asp:ListItem>
                                <%--<asp:ListItem Value="7" Text="Hospital - Listed Doctor Map"></asp:ListItem>--%>
                                <asp:ListItem Value="1" Text="Listed Doctor - Product Map"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Listed Doctor - Chemist Map"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Listed Doctor - Campaign Map"></asp:ListItem>
                                <%--<asp:ListItem Value="3" Text="Manager - Core Drs Map"></asp:ListItem>
                                <asp:ListItem Value="4" Text="SFC - Updation"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Geo - Tagged drs - Status"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btnGo" runat="server" Text="View" CssClass="savebutton" OnClick="btnGo_Click" />
                    </div>
                </div>
            </div>
            <br />
            <div class="display-callAvgreporttable clearfix">
                <div class="table-responsive" style="max-height: 700px; scrollbar-width: thin;">
                    <asp:Table ID="tbl" runat="server" CssClass="table" Width="100%">
                    </asp:Table>
                    <asp:Label ID="lblNoRecord" runat="server" Visible="false">No Records Found</asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
