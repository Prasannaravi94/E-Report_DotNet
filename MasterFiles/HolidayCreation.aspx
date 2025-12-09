<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidayCreation.aspx.cs"
    Inherits="MasterFiles_HolidayCreation" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday Creation</title>
      <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <style type="text/css">
        .modal {
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

        .loading {
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

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
            // $('input:text:first').focus();
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

            //            $('input:text').keyup(function () {
            //                str = $(this).val()
            //                str = str.replace(/\s/g, '')
            //                $(this).val(str)
            //            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });

        });

    </script>
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($("#txtHolidayName").val() == "") { alert("Enter Holiday Name."); $('#txtHolidayName').focus(); return false; }
                var multiple = $('#<%=ddlMulti.ClientID%> :selected').text();
                if (multiple == "--Select--") { alert("Select Multiple Dates."); $('#ddlMulti').focus(); return false; }
                var month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (month == "--Select--") { alert("Select month."); $('#ddlMonth').focus(); return false; }

            });
        });
    </script>
    <script type="text/javascript">
        var myDropDown = document.getElementById("ddlMulti"); var length = myDropDown.options.length; //open dropdown myDropDown.size = length; //close dropdown myDropDown.size = 0;
    </script>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <br />



            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Holiday Creation</h2> 
                        <div align="right"> </div>
                       
                       

                        <div class="designation-area clearfix">

                            <div class="single-des clearfix">
                                <asp:HiddenField ID="hdnHolidayID" runat="server" />
                                <asp:Label ID="lblHolidayName" runat="server" CssClass="label">Holiday Name <span style="Color:Red">*</span></asp:Label>

                                <asp:TextBox ID="txtHolidayName" CssClass="input"  Width="100%"
                                   TabIndex="1" runat="server"
                                    MaxLength="50" onkeypress="CharactersOnly(event);"> </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">

                                    <asp:Label ID="lblMulti" runat="server" CssClass="label">Multiple Dates <span style="Color:Red">*</span></asp:Label>

                                    <asp:DropDownList ID="ddlMulti" TabIndex="2" runat="server" CssClass="nice-select">
                                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                                        <asp:ListItem Value="0">Yes</asp:ListItem>
                                        <asp:ListItem Value="1">No</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblFix_Date" runat="server" CssClass="label" Text="Fixed Date" Width="100px"></asp:Label>
                                <asp:TextBox ID="txtFix_Date" runat="server" onkeypress="Calendar_enter(event);" CssClass="input"  Width="100%"
                                   TabIndex="3"></asp:TextBox>
                                
                                  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFix_Date" CssClass= " cal_Theme1" />
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblMonth" runat="server" CssClass="label" >Month <span style="Color:Red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlMonth" TabIndex="4" runat="server" CssClass="nice-select" >
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                     <asp:Label ID="Lbldivi" runat="server" CssClass="label" >Division Name <span style="color:Red">*</span></asp:Label>
                                     <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select"
                                AutoPostBack="true">
                            </asp:DropDownList>

                                </div>
                            </div>

                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server"  CssClass="savebutton" Text="Save"
                    OnClick="btnSubmit_Click" />
                         
                        </div>
                      
                    </div>

                     <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click"  />

                </div>
                 
                <br />
                <br />
               
            </div>
          
            <div class="loading" align="center">
                Loading. Please wait.
                <br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
