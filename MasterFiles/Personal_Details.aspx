<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Personal_Details.aspx.cs"
    Inherits="MasterFiles_Personal_Details" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Details ( Personal )</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />

    <link href="../assets/css/Calender_CheckBox.css" rel="stylesheet" />

    <style type="text/css">
        .div_fixed {
            position: fixed;
            top: 400px;
            right: 5px;
        }

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

        .chkboxLocation label {
            padding-left: 5px;
        }

        .height {
            height: 20px;
        }
    </style>
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

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.DOBDate').datepicker
            ({
                changeMonth: true,
                changeYear: true,
                yearRange: '1930:' + new Date().getFullYear().toString(),
                //                yearRange: "2010:2017",
                dateFormat: 'dd/mm/yy'
            });
        });

    </script>
    <style type="text/css">
        /*.label {
            width: 200px;
            display: inline-block;
            text-align: right;
            color: #f46b42;
        }*/

        .box1 {
            border: 2px solid #d1e2ea;
            border-radius: 8px;
            padding: 20px;
        }


        .label:hover {
            color: #D19BEC;
        }

        .textbox1 {
            width: 200px;
            display: inline-block;
            text-align: left;
        }

        .box {
            background: #F3F6ED;
            border: 3px solid #7E8D29;
            border-radius: 8px;
            border-style: dashed;
            marquee-direction: backwards;
            box-shadow: 10px 10px 5px #888888;
        }

        .textbox {
            background: white;
            border: 1px double #DDD;
            border-radius: 5px;
            box-shadow: 0 0 1px #333;
            color: #666;
            height: 20px;
            width: 275px;
        }

        .break {
            height: 10px;
        }

        .dashed {
            border-style: dashed;
        }
    </style>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <%--  <script type="text/javascript" src="../JsFiles/classie.js"></script>
    <script type="text/javascript" src="../JsFiles/modernizr.custom.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/component.css" />
    <link type="text/css" rel="Stylesheet" href="../css/default.css" />--%>

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

        function validate() {
            var perm_addr = document.getElementById('txtperm_addr');
            var pres_addr = document.getElementById('txtpres_addr');
            //            var DOB = document.getElementById('txtDOB');
            //            var DOW = document.getElementById('txtDOW');
            var email = document.getElementById('txtemail');
            var mob_no = document.getElementById('txtmob_no');
            var Resi_No = document.getElementById('txtResi_No');
            var emerg_contact = document.getElementById('txtemerg_contact');
            var pan_no = document.getElementById('txtpan_no');
            var aadhar = document.getElementById('txtaadhar');

            if (perm_addr.value == '') {
                alert('Enter Permanent Address');
                perm_addr.focus();
                return false;
            }

            if (pres_addr.value == '') {
                alert('Enter Present Address');
                pres_addr.focus();
                return false;
            }

            //            if (DOB.value == '') {
            //                alert('Enter DOB');
            //                DOB.focus();
            //                return false;
            //            }
            //            if (DOW.value == '') {
            //                alert('Enter DOW');
            //                DOW.focus();
            //                return false;
            //            }
            if (email.value == '') {
                alert('Enter Email ID');
                email.focus();
                return false;
            }
            if (mob_no.value == '') {
                alert('Enter Mobile No');
                mob_no.focus();
                return false;
            }
            //            if (Resi_No.value == '') {
            //                alert('Enter Residential No');
            //                Resi_No.focus();
            //                return false;
            //            }
            if (emerg_contact.value == '') {
                alert('Enter Emergency Contact No');
                emerg_contact.focus();
                return false;
            }
            //            if (pan_no.value == '') {
            //                alert('Enter PAN Card No');
            //                pan_no.focus();
            //                return false;
            //            }
            if (aadhar.value == '') {
                alert('Enter Aadhar No');
                aadhar.focus();
                return false;
            }


        }

    </script>
    <style type="text/css">
        .button1 {
            border-top: 1px solid #96d1f8;
            background: #BBD823;
            background: -webkit-gradient(linear, left top, left bottom, from(#3e779d), to(#BBD823));
            background: -webkit-linear-gradient(top, #3e779d, #BBD823);
            background: -moz-linear-gradient(top, #3e779d, #BBD823);
            background: -ms-linear-gradient(top, #3e779d, #BBD823);
            background: -o-linear-gradient(top, #3e779d, #BBD823);
            padding: 5px 10px;
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
            -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
            box-shadow: rgba(0,0,0,1) 0 1px 0;
            text-shadow: rgba(0,0,0,.4) 0 1px 0;
            color: white;
            font-size: 14px;
            font-family: Georgia, serif;
            text-decoration: none;
            vertical-align: middle;
        }

            .button1:hover {
                border-top-color: #f4ad42;
                background: #f4ad42;
                color: black;
            }

            .button1:active {
                border-top-color: #1b435e;
                background: #1b435e;
            }
    </style>

    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center" style="border-bottom: none">Employee Details ( Personal )</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-4">
                                        <asp:Label ID="lblname" runat="server" CssClass="label" Text="Select the Fieldforce Name :"></asp:Label>
                                        <%--  <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                            ToolTip="Enter Text Here"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" CssClass="custom-select2 nice-select"
                                            OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1" style="padding-top: 18px">
                                        <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Width="50px"
                                            Text="Go" OnClick="btnGo_Click" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <div class="row justify-content-center">
                                <div class="col-lg-6">
                                    <asp:Panel ID="pnl" runat="server" Visible="false">
                                        <div class="box1">
                                            <div class="designation-area clearfix">

                                                <div class="single-des clearfix">
                                                    <div class="row ">
                                                        <div class="col-lg-5">
                                                            <asp:Label ID="lblEmpl_name" runat="server" CssClass="label " Text="Employee Name " Font-Size="14px">
                                                            </asp:Label>
                                                        </div>
                                                        <div class="col-lg-1" style="font-size: 14px">
                                                            :
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="txtEmpl_name" CssClass="label " runat="server" ForeColor="Red" Font-Size="14px">  </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="single-des clearfix">
                                                    <div class="row ">
                                                        <div class="col-lg-5">
                                                            <asp:Label ID="lblDOJ" runat="server" CssClass="label " Text="DOJ" Font-Size="14px">
                                                            </asp:Label>
                                                        </div>
                                                        <div class="col-lg-1" style="font-size: 14px">
                                                            :
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="txtDOJ" runat="server" CssClass="label " ForeColor="Red" Font-Size="14px"> </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="single-des clearfix">
                                                    <div class="row ">
                                                        <div class="col-lg-5">
                                                            <asp:Label ID="lablHQ" runat="server" CssClass="label " Text="HQ" Font-Size="14px">
                                                            </asp:Label>
                                                        </div>
                                                        <div class="col-lg-1" style="font-size: 14px">
                                                            :
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="txtHq" runat="server" CssClass="label " ForeColor="Red" Font-Size="14px"> </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="single-des clearfix">
                                                    <div class="row ">
                                                        <div class="col-lg-5">
                                                            <asp:Label ID="lbldesig" runat="server" CssClass="label " Text="Designation" Font-Size="14px">
                                                            </asp:Label>
                                                        </div>
                                                        <div class="col-lg-1" style="font-size: 14px">
                                                            :
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="txtdesig" runat="server" CssClass="label " ForeColor="Red" Font-Size="14px"> </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="single-des clearfix">
                                                    <div class="row ">
                                                        <div class="col-lg-5">
                                                            <asp:Label ID="lblemp_code" runat="server" CssClass="label " Text="Employee Id" Font-Size="14px">
                                                            </asp:Label>
                                                        </div>
                                                        <div class="col-lg-1" style="font-size: 14px">
                                                            :
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="txtemp_code" runat="server" CssClass="label " ForeColor="Red" Font-Size="14px"> </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="single-des clearfix">
                                                    <div class="row ">
                                                        <div class="col-lg-5">
                                                            <asp:Label ID="lblstate" runat="server" CssClass="label " Text="State" Font-Size="14px">
                                                            </asp:Label>
                                                        </div>
                                                        <div class="col-lg-1" style="font-size: 14px">
                                                            :
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="txtstate" runat="server" CssClass="label " ForeColor="Red" Font-Size="14px"> </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="single-des clearfix">
                                                    <asp:Label ID="lblperm_addr" runat="server" CssClass="label " Text="Permanent Address :">
                                                    </asp:Label>
                                                    <asp:TextBox ID="txtperm_addr" runat="server" MaxLength="300" CssClass="input"
                                                        ToolTip="Enter Permanent Address" TabIndex="1" Placeholder="Type Here" Height="55px" Width="100%"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                                <div class="single-des clearfix">
                                                    <asp:Label ID="lblpres_addr" runat="server" CssClass="label " Text="Present Address :">
                                                    </asp:Label>
                                                    <asp:TextBox ID="txtpres_addr" runat="server" MaxLength="300" TabIndex="2" Placeholder="Type Here"
                                                        ToolTip="Enter Present Address" CssClass="input"
                                                        TextMode="MultiLine" Width="100%"
                                                        Height="55px"></asp:TextBox>
                                                </div>
                                                <div class="single-des clearfix">
                                                    <asp:Label ID="lblDOB" runat="server" CssClass="label " Text="DOB :"></asp:Label>
                                                    <asp:TextBox ID="txtDOB" runat="server" MaxLength="10" TabIndex="3"
                                                        ToolTip="Enter DOB" Width="100%"
                                                        CssClass="input " onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" CssClass="cal_Theme1"
                                                        TargetControlID="txtDOB" />
                                                </div>
                                                <%--  <div class="single-des clearfix">
                                                    <asp:Label ID="lblDOW" runat="server" CssClass="label" Text="DOW :"></asp:Label>
                                                    <asp:TextBox ID="txtDOW" runat="server" Height="22px" MaxLength="10" CssClass="DOBDate textbox1"
                                                        TabIndex="4" ToolTip="Enter DOW" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                        onblur="this.style.backgroundColor='White'" onkeypress="CheckNumeric(event);"
                                                        SkinID="TxtBxNumOnly"></asp:TextBox>
                                                </div>--%>
                                                <div class="single-des clearfix">
                                                    <asp:Label ID="lblemail" runat="server" CssClass="label " Text="Email ID :"></asp:Label>
                                                    <asp:TextBox ID="txtemail" Width="100%" CssClass="input" runat="server" TabIndex="5"
                                                        ToolTip="Enter Email ID"></asp:TextBox>
                                                </div>
                                                <div class="single-des clearfix">
                                                    <asp:Label ID="lblmob_no" runat="server" CssClass="label " Text="Mobile No :"></asp:Label>
                                                    <asp:TextBox ID="txtmob_no" Width="100%" CssClass="input" runat="server" TabIndex="6"
                                                        ToolTip="Enter Mobile No"></asp:TextBox>
                                                </div>
                                                <div class="single-des clearfix">
                                                    <asp:Label ID="lblResi_No" runat="server" CssClass="label " Text="Residential No :"></asp:Label>
                                                    <asp:TextBox ID="txtResi_No" Width="100%" CssClass="input" TabIndex="7" ToolTip="Enter Residential No"
                                                        runat="server"></asp:TextBox>

                                                </div>
                                                <div class="single-des clearfix">
                                                    <asp:Label ID="lblemerg_contact" runat="server" CssClass="label" Text="Emergency Contact No :"></asp:Label>
                                                    <asp:TextBox ID="txtemerg_contact" Width="100%" CssClass="input" runat="server" TabIndex="8" ToolTip="Enter Emergency Contact No"></asp:TextBox>
                                                </div>
                                                <%--  <div class="single-des clearfix">
                                                    <asp:Label ID="lblpan_no" runat="server" CssClass="label" Text="PAN Card No :"></asp:Label>
                                                    <asp:TextBox ID="txtpan_no" Width="254px" SkinID="MandTxtBox" TabIndex="9" ToolTip="Enter PAN Card No"
                                                        runat="server"></asp:TextBox>
                                                </div>--%>
                                                <%--   <div class="single-des clearfix">
                                                    <asp:Label ID="lblaadhar" runat="server" CssClass="label" Text="Aadhar No :"></asp:Label>
                                                    <asp:TextBox ID="txtaadhar" Width="154px" runat="server" SkinID="MandTxtBox" TabIndex="10"
                                                        ToolTip="Enter Aadhar No" CssClass="textbox1"></asp:TextBox>
                                                </div>--%>
                                            </div>
                                            <br />

                                            <center>
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" OnClick="btnSubmit_Click"
                                                    OnClientClick="return validate()" Text="Save" />
                                            </center>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>
