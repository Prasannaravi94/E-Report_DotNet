<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_MR_wise_BulkDeActivation.aspx.cs" Inherits="MasterFiles_Stockist_MR_wise_BulkDeActivation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="TitleCRM">Stockist Deactivation</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <link href="../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link href="../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet"
        type="text/css" />
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
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

        });
    </script>

    <style type="text/css">
        #tblStockist > thead > tr > th,
        #tblStockist > tbody > tr > th,
        #tblStockist > tfoot > tr > th,
        #tblStockist > thead > tr > td,
        #tblStockist > tbody > tr > td,
        #tblStockist > tfoot > tr > td {
             padding: 2px;
            font-size: 10px;
            line-height:2.0em;
        }
    </style>
     
    <script src="../JScript/Service_CRM/Stockist_JS/Stockist_MR_Bulk_DeActivate.js" type="text/javascript"></script>
    <link href="../JScript/Service_CRM/Crm_Dr_Css_Ob/Dr_Service_CRM_MgrApproveCSS.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <input id="hdnSfCode" type="hidden" value='<%= Session["sf_code"] %>' />
            <input id="hdnSfType" type="hidden" value='<%= Session["sf_type"] %>' />
            <br />
            <center>
        <div>
            <table>
                <tr>
                    <td>
                        <span id="lblField" style="width: 100px; height: 19px; padding: 15px;"><span style="color: Red">
                            *</span>Field Force Name </span>
                    </td>
                    <td>
                        <select id="ddlField" style="width:auto;font-size:11px;">
                            <option value="0">--All--</option>
                        </select>
                    </td>
                    <td>
                       &nbsp;&nbsp<input type="button" id="btnGo" value="Go" class="btn" style="width:40px;font-size:11px"/>
                    </td>
                </tr>
            </table>
        </div>
        </center>
            <br />
        <center>
        <div>
            <table id="tblStockist" class="table table-bordered table-striped" style="width:80%;font-size:10px">
            </table>
        </div>
        </center>
        <center>
        <div>
            <input type="button" id="btnConfirm" value="DeActivate" class="btn" style="font-size: 11px;" />
        </div>
        </center>
        </div>
        <div class="modal">
            <img src="https://s2.postimg.org/l99kqyrk9/loading_9_k.gif" style="width: 150px; height: 150px; position: fixed; top: 45%; left: 45%;"
                alt="" />
        </div>

    </form>
</body>
</html>
