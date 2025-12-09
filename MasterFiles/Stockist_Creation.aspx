<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_Creation.aspx.cs"
    Inherits="MasterFiles_Stockist_Creation" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stockist Creation</title>
    <link href="../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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

        .height {
            height: 20px;
        }
    </style>
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
            $('#btnSubmit').click(function () {

                if ($("#txtStockist_Name").val() == "") {
                   alert("Please Enter Stockist Name.");
                    $('#txtStockist_Name').focus();
                    return false;
                }
                if ($("#ddlState").val() == "0") {
                   alert("Please Select State");
                    $('#ddlState').focus();
                    return false;
                }
                if ($("#ddlPoolName").val() == "--Select--") {

                   alert("Please Select HQ Name");
                    $('#ddlPoolName').focus();
                    return false;

                }
                if (!CheckBoxSelectionValidation()) {
                    return false;
                }

            });


            $('#btnSave').click(function () {

                if ($("#txtStockist_Name").val() == "") {
                   alert("Please Enter Stockist Name.");
                    $('#txtStockist_Name').focus();
                    return false;
                }
                if ($("#ddlState").val() == "0") {
                   alert("Please Select State");
                    $('#ddlState').focus();
                    return false;
                }
                if ($("#ddlPoolName").val() == "--Select--") {
                  alert("Please Select HQ Name");
                    $('#ddlPoolName').focus();
                    return false;
                }

                if (!CheckBoxSelectionValidation()) {
                    return false;
                }

            });

        });
    </script>
    <style type="text/css">
        .div_fixed {
            position: fixed;
            top: 400px;
            right: 5px;
        }
    </style>
    <style type="text/css">
        .web_dialog_overlay {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            background: #000000;
            opacity: .15;
            filter: alpha(opacity=15);
            -moz-opacity: .15;
            z-index: 101;
            display: none;
        }

        .web_dialog {
            display: none;
            position: fixed;
            width: 380px;
            min-height: 180px;
            max-height: auto;
            top: 50%;
            left: 50%;
            margin-left: -190px;
            margin-top: -100px;
            background-color: #ffffff;
            border: 2px solid #336699;
            padding: 0px;
            z-index: 102;
            /*font-family: Verdana;*/
            font-size: 10pt;
        }

        .web_dialog_title {
            border-bottom: solid 2px #336699;
            background-color: #336699;
            padding: 4px;
            color: White;
            font-weight: bold;
        }

            .web_dialog_title a {
                color: White;
                text-decoration: none;
            }

        .align_right {
            text-align: right;
        }

        .Formatrbtn label {
            margin-right: 30px;
        }


        /* hover style just for information */
        label:hover:before {
            border: 1px solid #4778d9 !important;
        }


        .btnReAct {
            display: inline-block;
            padding: 3px 9px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            margin-top: 25px;
        }

        .btnReActivation {
            color: #fff;
            background-color: #158263;
            border-color: #158263;
        }

            .btnReActivation:hover {
                color: #fff;
                background-color: #2b9a7b;
                border-color: #2b9a7b;
            }

            .btnReActivation:focus, .btnReActivation.focus {
                color: #fff;
                background-color: #2b9a7b;
                border-color: #2b9a7b;
            }

            .btnReActivation:active, .btnReActivation.active {
                color: #fff;
                background-color: #158263;
                border-color: #158263;
                background-image: none;
            }


        #btnClose_Plus:focus {
            outline-offset: -2px;
        }

        #btnClose_Plus:hover, #btnClose_Plus:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btnClose_Plus:hover, #btnClose_Plus:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btnClose_Plus:active, #btnClose_Plus:hover {
            outline: 0px none currentColor;
        }


        [type="checkbox"]:checked + label {
        color: #ff008f;
    }

        /* checked mark aspect */
        [type="checkbox"]:not(:checked) + label:after, [type="checkbox"]:checked + label:after { 

           content: '';
            position: absolute;
            left: 0;
            top: 0;
            width: 1.4em;
            height: 1.4em;
            border: 4px solid #ccc;
             background: #ff008f;
            /*background: #0077ff;*/
            border-radius: 4px;
            box-shadow: inset 0 1px 3px rgba(0,0,0,.1);
            margin-left: 3px;

                      /*content: '\25FC';
                      position: absolute;
                      top: .4em;
                      left: .3em;                   
                      font-size: 1.3em;
                      line-height: 0.3; 
                      color: #0077ff;
                      transition: all .2s;*/
                      /* color: #09ad7e;*/
        }
    </style>

    <%--  <link href="../JScript/BootStrap/dist/css/bootstrap.css" rel="stylesheet" type="text/css" />--%>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript">


        function HQ_Validation() {

            var ShortName = $("#txtPool_Sname").val();
            var Name = $("#txtPool_Name").val();

            if (ShortName == "") {
                var txt = "Please Enter Short Name";
               alert(txt);
                return false;
            }
            else if (Name == "") {
                var txt = "Please Enter Name";
               alert(txt);
                return false;
                //createCustomAlert(txt);
            }
            else {
                return true;
            }
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            function blinker() {
                $('.blink_me').fadeOut(500);
                $('.blink_me').fadeIn(500);
            }

            setInterval(blinker, 1000);
        });
    </script>
    <style type="text/css">
        #tblStockistDetails {
            margin-left: 330px;
        }

        #tblSalesforceDtls {
            margin-left: 330px;
        }

        /*#btnSubmit {
            margin-right: 330px;
        }*/

        .div_fixed {
            position: fixed;
            top: 400px;
            right: 5px;
        }
        
 
    </style>
    <script type="text/javascript" language="javascript">
        function CheckBoxSelectionValidation() {
            var count = 0;
            var objgridview = document.getElementById('<%= DataList1.ClientID %>');

            for (var i = 0; i < objgridview.getElementsByTagName("input").length; i++) {

                var chknode = objgridview.getElementsByTagName("input")[i];

                if (chknode != null && chknode.type == "checkbox" && chknode.checked) {
                    count = count + 1;
                }
            }

            if (count == 0) {
                var txt = "Please select atleast one Fieldforce Name.";
                alert(txt);

                return false;
            }

            else {
                return true;
            }
        }

    </script>
    <style type="text/css">
        .Tbl_td {
            border-right: 1px solid #d1e2ea;
            padding-left: 7%;
        }
    </style>
    <script type="text/javascript">


        $(document).ready(function () {

            $("#btnReActivate_Plus").click(function (e) {
                ShowDialog_Plus(false);
                e.preventDefault();
            });

            $("#btnClose_Plus").click(function (e) {
                HideDialog_Plus();
                e.preventDefault();
            });
        });

        function ShowDialog_Plus(modal) {
            $("#overlay_Plus").show();
            $("#dialog_Plus").fadeIn(300);

            if (modal) {
                $("#overlay_Plus").unbind("click");
            }
            else {
                $("#overlay_Plus").click(function (e) {
                    HideDialog_Plus();
                });
            }
        }

        function HideDialog_Plus() {
            $("#overlay_Plus").hide();
            $("#dialog_Plus").fadeOut(300);
        }


    </script>
    <script type="text/javascript">

        function GetHQDDL() {

            $("#hdnPoolName").val($("#ddlPoolName option:selected").text());

            $("#ddlPoolName option:selected").val($("#hdnPoolName").val());
            return true;
        }

    </script>

    <script src="../JScript/Service_CRM/Stockist_JS/Stockist_Add_Detail_JS.js" type="text/javascript"></script>

      <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />

    

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />


            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <h2 class="text-center" style="border-bottom: 0px">Stockist Creation</h2>

                        <div class="row ">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-5">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblStockistName" runat="server" CssClass="label"> Stockist Name<span style="Color:Red; padding-left: 5px;">*</span></asp:Label>
                                        <asp:TextBox ID="txtStockist_Name" runat="server" TabIndex="1" CssClass="input" Width="100%"
                                            MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>

                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblStockist_ContactPerson" runat="server" Text="Contact Person"
                                            CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtStockist_ContactPerson" runat="server" Width="100%" TabIndex="3"
                                            MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);" CssClass="input"></asp:TextBox>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblStockist_Mobile" runat="server" Text="Mobile No"
                                            CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtStockist_Mobile" runat="server" TabIndex="5" Width="100%" CssClass="input"
                                            MaxLength="100" onkeypress="CheckNumeric(event);">
                                        </asp:TextBox>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblTerritory" runat="server" CssClass="label">
                                         HQ Name<span style="Color:Red; padding-left: 5px;">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlPoolName" runat="server" CssClass="nice-select" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnPoolName" runat="server" />
                                    </div>

                                    <div class="single-des clearfix">
                                        <a href="#" id="btnReActivate_Plus" class="blink_me" style="color: red; font-weight: bold; font-family: @NSimSun; font-size: 14px"
                                            shape="circle">Add HQ</a>
                                    </div>

                                </div>

                            </div>

                            <div class="col-lg-5" style="padding-top: 10px">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblStockist_Address" runat="server" Text="Address"
                                            CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtStockist_Address" runat="server" Width="100%" CssClass="input"
                                            TabIndex="2"
                                            MaxLength="150" onkeypress="AlphaNumeric(event);"> 
                                        </asp:TextBox>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblStockist_Designation" runat="server" Text="ERP Code"
                                            CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtStockist_Desingation" runat="server" Width="100%" CssClass="input"
                                            TabIndex="4"
                                            MaxLength="15" onkeypress="AlphaNumeric_NoSpecialCharshq(event);">
                                        </asp:TextBox>
                                    </div>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblState" runat="server" CssClass="label">State<span style="Color:Red; padding-left: 5px;">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="single-des clearfix">
                                    </div>

                                </div>
                            </div>


                            <div>
                                <div style="width: 100px">
                                </div>
                                <br />
                                <br />
                                <div id="output_Plus">
                                </div>
                                <div id="overlay_Plus" class="web_dialog_overlay">
                                </div>
                                <div id="dialog_Plus" class="web_dialog">
                                    <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                                        <tr>
                                            <td class="web_dialog_title">Stockist - HQ Creation
                                            </td>
                                            <td class="web_dialog_title align_right">
                                                <a href="#" id="btnClose_Plus">Close</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <center>
                                                    <table border="0" cellpadding="3" cellspacing="3" id="tblDivisionDtls" align="center" style="margin-right:0px">
                                                        <tr>
                                                           
                                                            <td >
                                                                <div class="designation-area clearfix">
                                                                    <div class="single-des clearfix">
                                                                        <asp:Label ID="lblShortName" runat="server" CssClass="label">Short Name<span style="Color:Red; padding-left: 5px;">*</span></asp:Label>
                                                                        <asp:TextBox ID="txtPool_Sname" CssClass="input" Width="100%"
                                                                            TabIndex="1" runat="server" MaxLength="10"
                                                                            onkeypress="CharactersOnly(event);">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <div class="designation-area clearfix">
                                                                    <div class="single-des clearfix">
                                                                        <asp:Label ID="lblPoolName" runat="server" CssClass="label">Name<span style="Color:Red; padding-left: 5px;">*</span></asp:Label>

                                                                        <asp:TextBox ID="txtPool_Name"
                                                                            TabIndex="2" runat="server" CssClass="input" Width="100%"
                                                                            MaxLength="120" onkeypress="CharactersOnly(event);">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </center>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <asp:Button ID="btnHq" runat="server" Text="Save" CssClass="savebutton" Width="60px"
                                                    OnClick="btnHq_Click" OnClientClick="if(!HQ_Validation()) return false;" />
                                                <%--<asp:Button ID="btnActive_Plus" runat="server" Text="Activate" OnClick="btnActive_Plus_Click"
                                            CssClass="btn btnReActivation" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>


                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <div class="text-center" style="border-bottom: 0px">
                                    <asp:Label ID="lblTitle_SalesforceDtls" runat="server" Text="Select the Fieldforce Name"
                                        TabIndex="6"></asp:Label>
                                </div>
                                <br />
                                <div class="row justify-content-center">
                                    <div class="col-lg-10">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblFilter" runat="server" Font-Bold="true" Text="Filter By" CssClass="label"></asp:Label>
                                                <asp:DropDownList ID="ddlFilter" runat="server" CssClass="custom-select2 nice-select" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-2" style="padding-top: 20px">
                                        <asp:Button ID="btnGo" runat="server" Width="50px" Text="Go" CssClass="savebutton"
                                            OnClick="btnGo_Click" />
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />

                <div class="row justify-content-center" style="overflow-x: auto;">
                    <div class="col-lg-11">
                        <table width="100%" cellpadding="3" cellspacing="3" align="center">
                            <tr>
                                <td rowspan="1" align="left" style="width: 100%; height: 10px">
                                    <br />
                                    <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="2"
                                        RepeatLayout="Table" Width="100%" BorderStyle="Solid" BorderWidth="1px" BorderColor="#d1e2ea"
                                        GridLines="none">
                                        <ItemStyle CssClass="Tbl_td" />
                                        <ItemTemplate>
                                            <h3>
                                                <asp:Label ID="lblsf_Name" ForeColor="#3333cc" runat="server" Font-Size="14px" Font-Bold="true"
                                                    Text='<%# Eval("rep_hq") %>' /></h3>
                                            <asp:CheckBox ID="chkCategoryNameLabel" runat="server" Text='<%# Eval("sf_Name") %>'></asp:CheckBox>
                                            <asp:HiddenField ID="cbTestID" runat="server" Value='<%# Eval("SF_Code") %>' />
                                            <asp:Label ID="lbltest" runat="server"></asp:Label>
                                            <asp:HiddenField ID="HidStockistCode" runat="server" />
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                                <td>
                                    <asp:Label ID="lbltest" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:CheckBoxList ID="chkboxSalesforce" runat="server" DataTextField="sf_Name" DataValueField="sf_code"
                            RepeatColumns="2" RepeatDirection="Vertical" TabIndex="7">
                        </asp:CheckBoxList>
                        <asp:HiddenField ID="HidStockistCode" runat="server" />

                    </div>

                </div>
                <br />
                <div class="text-center">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton"
                        Text="Save" OnClick="btnSubmit_Click" OnClientClick="return GetHQDDL()" />
                </div>
                <div class="div_fixed">
                    <asp:Button ID="btnSave" runat="server" CssClass="savebutton"
                        Text="Save" OnClick="btnSave_Click" OnClientClick="return GetHQDDL()" />
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
          <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>
