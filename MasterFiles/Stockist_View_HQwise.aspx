<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_View_HQwise.aspx.cs" Inherits="MasterFiles_Stockist_View_HQwise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HQwise - Contribution View</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link href="../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link href="../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet"
        type="text/css" />
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="../../../JScript/BootStrap/dist/css/CRMService.css" rel="stylesheet"
        type="text/css" />
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
    <script type="text/javascript" language="javascript">
        function disp_confirm() {
            if (confirm("Do you want to Create the ID ?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
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


        });
    </script>
    <style type="text/css">
        .TextUnder {
            margin-top: 2px;
            text-decoration: underline;
        }

        .Service {
            font-size: 15px;
            font-family: Verdana;
            -webkit-text-stroke: 1px #d10091;
            -webkit-text-fill-color: #fff;
            -webkit-animation: fillser 0.5s infinite alternate;
        }

        @-webkit-keyframes fillser {
            from {
                -webkit-text-fill-color: #d10091;
            }

            to {
                -webkit-text-fill-color: #FFFFFF;
            }
        }

        .btn {
            background: #3498db;
            background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
            background-image: -moz-linear-gradient(top, #3498db, #2980b9);
            background-image: -ms-linear-gradient(top, #3498db, #2980b9);
            background-image: -o-linear-gradient(top, #3498db, #2980b9);
            background-image: linear-gradient(to bottom, #3498db, #2980b9);
            font-family: Verdana;
            color: #ffffff;
            font-size: 15px;
            padding: 3px 6px 3px 6px;
            text-decoration: none;
        }

            .btn:hover {
                background: #3cb0fd; /* background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
            background-image: linear-gradient(to bottom, #3cb0fd, #3498db);*/
                text-decoration: none;
                color: Black;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var keyDown = false, ctrl = 17, vKey = 86, Vkey = 118;

            $(document).keydown(function (e) {
                if (e.keyCode == ctrl) keyDown = true;
            }).keyup(function (e) {
                if (e.keyCode == ctrl) keyDown = false;
            });

            $('.txtSerCss').on('keypress', function (e) {
                if (!e) var e = window.event;
                if (e.keyCode > 0 && e.which == 0) return true;
                if (e.keyCode) code = e.keyCode;
                else if (e.which) code = e.which;
                var character = String.fromCharCode(code);
                if (character == '\b' || character == ' ' || character == '\t') return true;
                if (keyDown && (code == vKey || code == Vkey)) return (character);
                else return (/[0-9]$/.test(character));
            }).on('focusout', function (e) {
                var $this = $(this);
                $this.val($this.val().replace(/[^0-9]/g, ''));
            }).on('paste', function (e) {
                var $this = $(this);
                setTimeout(function () {
                    $this.val($this.val().replace(/[^0-9]/g, ''));
                }, 5);
            });
        });

    </script>
    <script type="text/javascript">
        function preventMultipleSubmissions() {
            $('#btnSave').prop('disabled', true);
        }
        window.onbeforeunload = preventMultipleSubmissions;
    </script>
    <script src="../JScript/CRM/StockHQwise_Cont_JS.js"></script>
    <style type="text/css">
        #tblHQCont {
            font-size: 10px;
        }

            #tblHQCont > thead > tr > th, #tblHQCont > tbody > tr > th, #tblHQCont > tfoot > tr > th, #tblHQCont > thead > tr > td, #tblHQCont > tbody > tr > td, #tblHQCont > tfoot > tr > td {
                padding: 2px;
                font-size: 10px;
                line-height: 2.0em;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <center>
            <div>
                <table>
                    <%--<tr>
                        <td>
                            <span id="lblHQWise" style="font-size: 12px">HQ Wise<span style="color: Red">*</span> </span>&nbsp;
                        </td>
                        <td>
                            <input type="text"  id="txtHQwise" class="textClass" style="width:100px" />&nbsp;                         
                        </td>
                        <td>
                            <select id="ddlHQwise" class="input-sm" style="font-size: 11px">
                                <option>--Select--</option>                           
                            </select>&nbsp;&nbsp; 
                        </td>
                        <td>
                            <input type="button" id="btnGo" value="Go" class="btn"/>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <span id="lblSearch" style="font-size: 12px">Search by<span style="color: Red">*</span> </span>&nbsp;
                        </td>
                        <td>
                             <select id="ddlSearch" class="input-sm" style="font-size: 11px">
                                <option value="1">State Wise</option> 
                                <option value="2" selected="selected">Stockist Wise</option>                           
                            </select>&nbsp;
                        </td>
                        <td>
                            <input type="text"  id="txtHQwise" class="textClass" style="width:100px;height:26px" />&nbsp;                         
                        </td>
                        <td>
                            <select id="ddlState" class="input-sm" style="font-size: 11px">
                                <option>--Select--</option>                           
                            </select>&nbsp;&nbsp; 
                        </td>
                        <td>
                            <input type="text"  id="txtStockist" class="textClass" style="width:250px;height:26px" />&nbsp;    
                        </td>
                        <td>
                            <input type="button" id="btnGo" value="Go" class="btn"/>
                        </td>
                    </tr>
                </table>
            </div>
                <br />
                 <table id="tblHQCont" class="table table-bordered table-striped"  style="width:90%">
                 </table>
            </center>
        </div>
        <div class="modal">
            <img src="https://s2.postimg.org/l99kqyrk9/loading_9_k.gif" style="width: 150px; height: 150px; position: fixed; top: 45%; left: 45%;"
                alt="" />
        </div>
    </form>
</body>
</html>
