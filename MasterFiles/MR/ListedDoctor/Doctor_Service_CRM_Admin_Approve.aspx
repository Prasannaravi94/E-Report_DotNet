<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_CRM_Admin_Approve.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_Admin_Approve" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="TitleCRM">Received Service CRM </title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <link href="../../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>    
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

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
    
    <link href="../../../JScript/Service_CRM/Crm_Dr_Css_Ob/Dr_Service_CRM_AdminApproveCSS.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../JScript/Service_CRM/Crm_Dr_JS/DoctorService_CRM_Admin_ApproveJS.js"
        type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>

        <input id="sessionInput" type="hidden" value='<%= Session["sf_code"] %>' />

        <br />
        <center>
        <div>
            <table>
                <tr>
                <td>
                        <span id="lblType" style="width: 100px; height: 19px; padding: 15px;"><span style="color: Red">
                            *</span>Type</span>
                    </td>
                    <td>
                        <select id="ddlType" style="width:150px">                            
                            <option value="1" selected="selected">Doctor</option>
                            <option value="2">Chemist/Pharmacy</option>                          
                        </select>
                    </td>
                    <td>
                        <span id="lblMode" style="width: 100px; height: 19px; padding: 15px;"><span style="color: Red">
                            *</span>Mode </span>
                    </td>
                    <td>
                        <select id="ddlMode">
                            <option value="0">--Select--</option>
                            <option value="1" selected="selected">Pending</option>
                            <option value="2">Completed</option>
                            <option value="3">Reject</option>
                        </select>
                    </td>
                  
                    <td>
                        <span id="lblMonth" style="width: 100px; height: 19px; padding: 15px;"><span style="color: Red">
                            *</span>Month </span>
                    </td>
                    <td>
                        <select id="ddlMonth">
                            <option value="0">--All--</option>
                            <option value="1">Jan</option>
                            <option value="2">Feb</option>
                            <option value="3">Mar</option>
                            <option value="4">Apr</option>
                            <option value="5">May</option>
                            <option value="6">Jun</option>
                            <option value="7">Jul</option>
                            <option value="8">Aug</option>
                            <option value="9">Sep</option>
                            <option value="10">Oct</option>
                            <option value="11">Nov</option>
                            <option value="12">Dec</option>
                        </select>
                    </td>
                    <td>
                        <span id="lblYear" style="width: 100px; height: 19px; padding: 15px;"><span style="color: Red">
                            *</span>Year </span>
                    </td>
                    <td>
                        <select id="ddlYear">
                            <option value="0">--All--</option>
                        </select>
                    </td>
                    <td>
                       <input type="button" id="btnGo" value="Go" class="btn" style="width:50px"/>
                    </td>
                </tr>
            </table>
        </div>
        </center>
        <br />
        <center>
        <div>
            <table id="tblReceived" class="table table-bordered table-striped" style="min-width:95%;max-width:100%">
            </table>
        </div>
        </center>
        <div class="div_fixed">
            <input type="button" id="btnConfirm" value="Confirmed" class="btn" />
        </div>
    </div>
    <div class="modal">
        <img src="https://s2.postimg.org/l99kqyrk9/loading_9_k.gif" style="width: 150px;
            height: 150px; position: fixed; top: 45%; left: 45%;" alt="" />
    </div>
    <%--  <div id="shader" class="shader">
        <div id="loading" class="bar">
            <p>
                loading</p>
        </div>
    </div>--%>
    </form>
</body>
</html>
