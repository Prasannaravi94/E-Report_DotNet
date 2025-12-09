<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChemistCampaign.aspx.cs" Inherits="MasterFiles_Chemist_Campaign_ChemistCampaign" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chemist Campaign</title>
<%--    <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //     $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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
            $('#btnSubmit').click(function () {
                if ($("#txtChm_SubCat_SName").val() == "") { alert("Enter Short Name."); $('#txtChm_SubCat_SName').focus(); return false; }
                if ($("#txtChmSubCatName").val() == "") { alert("Enter Chemist Campaign Name."); $('#txtChmSubCatName').focus(); return false; }
                if ($("#txtEffFrom").val() == "") { alert("Enter Effective From."); $('#txtEffFrom').focus(); return false; }
                if ($("#txtEffTo").val() == "") { alert("Enter Effective To."); $('#txtEffTo').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
    <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center" id="heading" runat="server"></h2>

                        <div class="designation-area clearfix">

                            <div class="single-des clearfix">

                                 <asp:Label ID="lblChm_SubCat_SName" runat="server" CssClass="label">Short Name<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                <asp:TextBox ID="txtChm_SubCat_SName" CssClass="input" Width="100%" TabIndex="1"
                                    runat="server" MaxLength="12" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                </asp:TextBox>
                       
                    </div>
              <div class="single-des clearfix">
                       
                   
                       
                   <asp:Label ID="lblChmSubCatName" runat="server" CssClass="label">Chemist Campaign Name<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                <asp:TextBox ID="txtChmSubCatName" CssClass="input" Width="100%"
                                    TabIndex="2" runat="server"
                                    MaxLength="120" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                </asp:TextBox>


                    </div>
              <div class="single-des clearfix">
                    
                        
                   
                     
                  <asp:Label ID="lblEffFrom" runat="server" CssClass="label">Effective From<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                <asp:TextBox ID="txtEffFrom" runat="server" onkeypress="Calendar_enter(event);" CssClass="input" Width="100%"
                                    TabIndex="6"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtEffFrom" CssClass="cal_Theme1"
                                    runat="server" />
                   
                  </div>
               <div class="single-des clearfix">
                        

                      <asp:Label ID="lblEffTo" runat="server" CssClass="label">Effective To<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                                <asp:TextBox ID="txtEffTo" runat="server" onkeypress="Calendar_enter(event);"
                                    TabIndex="7" CssClass="input" Width="100%" />
                                <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEffTo" CssClass="cal_Theme1"
                                    runat="server" />
                    </div>
            <%--    <tr>
                 <td class="stylespc" align="left">
                  <asp:CheckBox ID="chkall_drs" Checked="true" Text ="Show all chemist for campaign tagging" Width="250px" runat="server" />
                 </td>
                </tr>--%>
            </div>

                        </div>
                      
            <br />
                     <div class="w-100 designation-submit-button text-center clearfix">
            <asp:Button ID="btnSubmit" runat="server" Width="60px" Height="30px" CssClass="BUTTON" Text="Save" OnClick="btnSubmit_Click"/>
       </div>
        </div>
        </div>
        </div>
   
    </form>
</body>
</html>
