<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_CRM_Approve_MGR.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_Approve_MGR" %>

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
        
    <link href="../../../JScript/Service_CRM/Crm_Dr_Css_Ob/Dr_Service_CRM_MgrApproveCSS.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../JScript/Service_CRM/Crm_Dr_JS/DoctorServiceCRM_MGR_Approve.js"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <br />       

        <input id="Session_SfCode" type="hidden" value='<%= Session["sf_code"] %>' />
        <input id="Session_SfType" type="hidden" value='<%= Session["sf_type"] %>' />
        <input id="SS_DivCode" type="hidden" value='<%= Session["div_code"] %>' />
        <center>
        <div>
        <table>
                <tr>
                    <td>
                        <span id="lblDivCode" style="width: 100px; height: 19px; padding: 15px;"><span style="color: Red">
                            *</span>Division Name </span>
                    </td>
                    <td>
                        <select id="ddlDivision" style="width:200px">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span id="lblMode" style="width: 100px; height: 19px; padding: 15px;"><span style="color: Red">
                            *</span>Mode </span>
                    </td>
                    <td>
                        <select id="ddlMode" style="width:150px">                            
                            <option value="1" selected="selected">Doctor</option>
                            <option value="2">Chemist/Pharmacy</option>                          
                        </select>
                    </td>
                   <%-- <td>
                       <input type="button" id="btnGo" value="Go" class="btn" style="width:40px"/>
                    </td>--%>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <table id="tblReceived" class="table table-bordered table-striped" style="width:90%">
            </table>
        </div>
        </center>
           <div class="div_fixed">
            <input type="button" id="btnConfirm" value="Confirmed" class="btn" />
        </div>
    </div>
     <%-- <div id="shader" class="shader">
        <div id="loading" class="bar">
            <p>
                loading</p>
        </div>
    </div>--%>
     <div class="modal">       
          <img src="../../../Images/ICP/loading_9_k.gif"  style=" width:150px; height:150px;position: fixed;top:45%;left:45%;"  alt="" />
    </div>
    </form>
</body>
</html>
