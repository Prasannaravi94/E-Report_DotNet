<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage_ImageUpload_Common.aspx.cs" Inherits="MasterFiles_Options_HomePage_ImageUpload_Common" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page Image Upload</title>
    <script type="text/javascript" src="../../JsFiles/common.js"></script>
    <link href="../../assets/css/style.css" rel="stylesheet" />
    <link href="../../assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/nice-select.css" rel="stylesheet" />
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

      <style type="text/css">
        [type="radio"]:checked + label {
            color: #6a6a6a;
        }
        #pnlpopup label {
            font-weight:normal !important;
        }
        body {
            color: #6a6a6a;
        }

        .btn-xs, .btn-group-xs > .btn {
            font-size: 14px !important;
            line-height: 1.5;
        }

        .mceLayout {
            width: 100% !important;
        }

        /*td.chkList input[type="checkbox"] + label, input[type="radio"] + label {
            color: #6a6a6a;
        }*/

        /*input[type="checkbox"] + label {
            color: white;
        }*/

        .dropdown-menu li a:hover {
            color: #6a6a6a !important;
        }

        .img1 {
            margin: 0px 0px 0px 0px;
            background: url("../../images/sendicon.gif") left center no-repeat;
            padding: 0em 1.2em;
            font: 8pt "tahoma";
            color: #336699;
            text-decoration: none;
            font-weight: normal;
            letter-spacing: 0px;
        }

        .gvBorder {
            border: 1px;
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

        .pgr1 {
            z-index: 1;
            left: 140px;
            top: 525px;
            position: absolute;
            width: 89%;
        }

        .CursorPointer {
            cursor: default;
        }

        /*.mGrid .pgr1 {
            background: #A6A6D2;
        }

            .mGrid .pgr1 table {
                margin: 5px 0;
            }

            .mGrid .pgr1 td {
                border-width: 0;
                padding: 0 6px;
                text-align: left;
                border-left: solid 0px #666;
                font-weight: bold;
                color: Red;
                line-height: 12px;
            }

            .mGrid .pgr1 th {
                background: #A6A6D2;
            }

            .mGrid .pgr1 a {
                color: Blue;
                text-decoration: none;
            }

                .mGrid .pgr1 a:hover {
                    color: White;
                    text-decoration: none;
                }*/

        .imgBtnSrch {
            margin-bottom: -2%;
        }

        .deactive {
            visibility: hidden;
        }
        /************************/
        .GridPager {
            margin-top: 150px;
        }

            .GridPager a, .GridPager span {
                display: block;
                height: auto;
                min-width: 20px;
                width: auto;
                margin-top: 50px;
                margin-right: 2px;
                font-weight: bold;
                text-align: center;
                text-decoration: none;
            }

            .GridPager a {
                background-color: #f5f5f5;
                color: #969696;
                border: 1px solid #969696;
            }

            .GridPager span {
                background-color: #A1DCF2;
                color: #000;
                border: 1px solid #3AC0F2;
            }
        /********************************/
        .loader {
            background-color: rgba(0,0,0,0.95);
            height: 100%;
            width: 100%;
            position: fixed;
            z-index: 999;
            margin-top: 0px;
            top: 0px;
        }

        .loader-centered {
            position: absolute;
            left: 50%;
            top: 50%;
            height: 200px;
            width: 200px;
            margin-top: -100px;
            margin-left: -100px;
        }

        .object {
            width: 50px;
            height: 50px;
            background-color: rgba(255,255,255,0);
            margin-right: auto;
            margin-left: auto;
            border: 4px solid #FFF;
            left: 73px;
            top: 73px;
            position: absolute;
        }

        .square-one {
            -webkit-animation: first_object_animate 1s infinite ease-in-out;
            animation: first_object_animate 1s infinite ease-in-out;
        }

        .square-two {
            -webkit-animation: second_object 1s forwards, second_object_animate 1s infinite ease-in-out;
            animation: second_object 1s forwards, second_object_animate 1s infinite ease-in-out;
        }

        .square-three {
            -webkit-animation: third_object 1s forwards, third_object_animate 1s infinite ease-in-out;
            animation: third_object 1s forwards, third_object_animate 1s infinite ease-in-out;
        }
        /*
        @-webkit-keyframes second_object {
	        100% { width: 100px; height:100px; left: 48px; top: 48px; }
        }		
        @keyframes second_object {
	        100% { width: 100px; height:100px; left: 48px; top: 48px; }
        }
        @-webkit-keyframes third_object {
	        100% { width: 150px; height:150px; left: 23px; top: 23px;}
        }		
        @keyframes third_object {
	        100% { width: 150px; height:150px; left: 23px; top: 23px;}
        }

        @-webkit-keyframes first_object_animate {
          0% { -webkit-transform: perspective(100px); }
          50% { -webkit-transform: perspective(100px) rotateY(-180deg); }
          100% { -webkit-transform: perspective(100px) rotateY(-180deg) rotateX(-180deg); }
        }

        @keyframes first_object_animate {
	        0% { 
		        transform: perspective(100px) rotateX(0deg) rotateY(0deg);
		        -webkit-transform: perspective(100px) rotateX(0deg) rotateY(0deg); 
	        } 50% { 
		        transform: perspective(100px) rotateX(-180deg) rotateY(0deg);
		        -webkit-transform: perspective(100px) rotateX(-180deg) rotateY(0deg) ;
	        } 100% { 
		        transform: perspective(100px) rotateX(-180deg) rotateY(-180deg);
		        -webkit-transform: perspective(100px) rotateX(-180deg) rotateY(-180deg);
	        }
        }

        @-webkit-keyframes second_object_animate {
	        0% { -webkit-transform: perspective(200px); }
	        50% { -webkit-transform: perspective(200px) rotateY(180deg); }
	        100% { -webkit-transform: perspective(200px) rotateY(180deg) rotateX(180deg); }
        }	


        @keyframes second_object_animate {
	        0% { 
		        transform: perspective(200px) rotateX(0deg) rotateY(0deg);
		        -webkit-transform: perspective(200px) rotateX(0deg) rotateY(0deg); 
	        } 50% { 
		        transform: perspective(200px) rotateX(180deg) rotateY(0deg);
		        -webkit-transform: perspective(200px) rotateX(180deg) rotateY(0deg) ;
	        } 100% { 
		        transform: perspective(200px) rotateX(180deg) rotateY(180deg);
		        -webkit-transform: perspective(200px) rotateX(180deg) rotateY(180deg);
	        }
        }

        @-webkit-keyframes third_object_animate {
	        0% { -webkit-transform: perspective(300px); }
	        50% { -webkit-transform: perspective(300px) rotateY(-180deg); }
	        100% { -webkit-transform: perspective(300px) rotateY(-180deg) rotateX(-180deg); }
        }

        @keyframes third_object_animate {
	        0% { 
		        transform: perspective(300px) rotateX(0deg) rotateY(0deg);
		        -webkit-transform: perspective(300px) rotateX(0deg) rotateY(0deg); 
	        } 50% { 
		        transform: perspective(300px) rotateX(-180deg) rotateY(0deg);
		        -webkit-transform: perspective(300px) rotateX(-180deg) rotateY(0deg) ;
	        } 100% { 
		        transform: perspective(300px) rotateX(-180deg) rotateY(-180deg);
		        -webkit-transform: perspective(300px) rotateX(-180deg) rotateY(-180deg);
	        }
        }*/
        .mail-box {
            border-collapse: collapse;
            border-spacing: 0;
            display: table;
            table-layout: fixed;
            width: 100%;
        }

            .mail-box aside {
                display: table-cell;
                float: none;
                height: 100%;
                padding: 0;
                vertical-align: top;
            }

            .mail-box .sm-side {
                background: none repeat scroll 0 0 #e5e8ef;
                border-radius: 4px 0 0 4px;
                width: 15%;
            }

            .mail-box .lg-side {
                background: none repeat scroll 0 0 #fff;
                border-radius: 0 4px 4px 0;
                width: 75%;
            }

            .mail-box .sm-side .user-head {
                background: none repeat scroll 0 0 #00a8b3;
                border-radius: 4px 0 0;
                color: #fff;
                min-height: 80px;
                padding: 10px;
            }

        .user-head .inbox-avatar {
            float: left;
            width: 65px;
        }

            .user-head .inbox-avatar img {
                border-radius: 4px;
            }

        .user-head .user-name {
            display: inline-block;
            margin: 0 0 0 10px;
        }

            .user-head .user-name h5 {
                font-size: 14px;
                font-weight: 300;
                margin-bottom: 0;
                margin-top: 15px;
            }

                .user-head .user-name h5 a {
                    color: #fff;
                }

            .user-head .user-name span a {
                color: #87e2e7;
                font-size: 12px;
            }

        a.mail-dropdown {
            background: none repeat scroll 0 0 #80d3d9;
            border-radius: 2px;
            color: #01a7b3;
            font-size: 10px;
            margin-top: 20px;
            padding: 3px 5px;
        }

        .inbox-body {
            padding: 20px;
        }

        .btn-compose {
            background: none repeat scroll 0 0 #ff6c60;
            color: #fff;
            padding: 12px 0;
            text-align: center;
            width: 100%;
        }

            .btn-compose:hover {
                background: none repeat scroll 0 0 #f5675c;
                color: #fff;
            }

        ul.inbox-nav {
            display: inline-block;
            margin: 0;
            padding: 0;
            width: 100%;
        }

        .inbox-divider {
            border-bottom: 1px solid #d5d8df;
        }

        ul.inbox-nav li {
            display: inline-block;
            line-height: 45px;
            width: 100%;
        }

            ul.inbox-nav li a {
                color: #6a6a6a;
                display: inline-block;
                line-height: 45px;
                padding: 0 20px;
                width: 100%;
            }

                ul.inbox-nav li a:hover, ul.inbox-nav li.active a, ul.inbox-nav li a:focus {
                    background: none repeat scroll 0 0 #d5d7de;
                    color: #6a6a6a;
                }

                ul.inbox-nav li a i {
                    color: #6a6a6a;
                    font-size: 16px;
                    padding-right: 10px;
                }

                ul.inbox-nav li a span.label {
                    margin-top: 13px;
                }

        ul.labels-info li h4 {
            color: #5c5c5e;
            font-size: 13px;
            padding-left: 15px;
            padding-right: 15px;
            padding-top: 5px;
            text-transform: uppercase;
        }

        ul.labels-info li {
            margin: 0;
        }

            ul.labels-info li a {
                border-radius: 0;
                color: #6a6a6a;
            }

                ul.labels-info li a:hover, ul.labels-info li a:focus {
                    background: none repeat scroll 0 0 #d5d7de;
                    color: #6a6a6a;
                }

                ul.labels-info li a i {
                    padding-right: 10px;
                }

        .nav.nav-pills.nav-stacked.labels-info p {
            color: #9d9f9e;
            font-size: 11px;
            margin-bottom: 0;
            padding: 0 22px;
        }

        .inbox-head {
            background: none repeat scroll 0 0 #41cac0;
            border-radius: 0 4px 0 0;
            color: #fff;
            min-height: 80px;
            padding: 20px;
        }

            .inbox-head h3 {
                display: inline-block;
                font-weight: 300;
                margin: 0;
                padding-top: 6px;
            }

            .inbox-head .sr-input {
                border: medium none;
                border-radius: 4px 0 0 4px;
                box-shadow: none;
                color: #8a8a8a;
                float: left;
                height: 40px;
                padding: 0 10px;
            }

            .inbox-head .sr-btn {
                background: none repeat scroll 0 0 #00a6b2;
                border: medium none;
                border-radius: 0 4px 4px 0;
                color: #fff;
                height: 40px;
                padding: 0 20px;
            }

        .table-inbox {
            border: 1px solid #d3d3d3;
            margin-bottom: 0;
        }

            .table-inbox tr td {
                padding: 12px !important;
            }

                .table-inbox tr td:hover {
                    cursor: pointer;
                }

                .table-inbox tr td .fa-star.inbox-started, .table-inbox tr td .fa-star:hover {
                    color: #f78a09;
                }

                .table-inbox tr td .fa-star {
                    color: #d5d5d5;
                }

            .table-inbox tr.unread td {
                background: none repeat scroll 0 0 #f7f7f7;
                font-weight: 600;
            }

        ul.inbox-pagination {
            float: right;
        }

            ul.inbox-pagination li {
                float: left;
            }

        .mail-option {
            display: inline-block;
            margin-bottom: 10px;
            width: 100%;
        }

            .mail-option .chk-all, .mail-option .btn-group {
                margin-right: 5px;
            }

                .mail-option .chk-all, .mail-option .btn-group a.btn {
                    background: none repeat scroll 0 0 #fcfcfc;
                    border: 1px solid #e7e7e7;
                    border-radius: 3px !important;
                    color: #afafaf;
                    display: inline-block;
                    padding: 5px 10px;
                }

        .inbox-pagination a.np-btn {
            background: none repeat scroll 0 0 #fcfcfc;
            border: 1px solid #e7e7e7;
            border-radius: 3px !important;
            color: #afafaf;
            display: inline-block;
            padding: 5px 15px;
        }

        .mail-option .chk-all input[type="checkbox"] {
            margin-top: 0;
        }

        .mail-option .btn-group a.all {
            border: medium none;
            padding: 0;
        }

        .inbox-pagination a.np-btn {
            margin-left: 5px;
        }

        .inbox-pagination li span {
            display: inline-block;
            margin-right: 5px;
            margin-top: 7px;
        }

        .fileinput-button {
            background: none repeat scroll 0 0 #eeeeee;
            border: 1px solid #e6e6e6;
        }

        .inbox-body .modal .modal-body input, .inbox-body .modal .modal-body textarea {
            border: 1px solid #e6e6e6;
            box-shadow: none;
        }

        .btn-send, .btn-send:hover {
            background: none repeat scroll 0 0 #00a8b3;
            color: #fff;
        }

            .btn-send:hover {
                background: none repeat scroll 0 0 #009da7;
            }

        .modal-header h4.modal-title {
            font-family: "Open Sans",sans-serif;
            font-weight: 300;
        }

        .modal-body label {
            font-family: "Open Sans",sans-serif;
            font-weight: 400;
        }

        .heading-inbox h4 {
            border-bottom: 1px solid #ddd;
            color: #444;
            font-size: 18px;
            margin-top: 20px;
            padding-bottom: 10px;
        }

        .sender-info {
            margin-bottom: 20px;
        }

            .sender-info img {
                height: 30px;
                width: 30px;
            }

        .sender-dropdown {
            background: none repeat scroll 0 0 #eaeaea;
            color: #777;
            font-size: 10px;
            padding: 0 3px;
        }

        .view-mail a {
            color: #ff6c60;
        }

        .attachment-mail {
            margin-top: 30px;
        }

            .attachment-mail ul {
                display: inline-block;
                margin-bottom: 30px;
                width: 100%;
            }

                .attachment-mail ul li {
                    float: left;
                    margin-bottom: 10px;
                    margin-right: 10px;
                    width: 150px;
                }

                    .attachment-mail ul li img {
                        width: 100%;
                    }

                    .attachment-mail ul li span {
                        float: right;
                    }

            .attachment-mail .file-name {
                float: left;
            }

            .attachment-mail .links {
                display: inline-block;
                width: 100%;
            }

        .fileinput-button {
            float: left;
            margin-right: 4px;
            overflow: hidden;
            position: relative;
        }

            .fileinput-button input {
                cursor: pointer;
                direction: ltr;
                font-size: 23px;
                margin: 0;
                opacity: 0;
                position: absolute;
                right: 0;
                top: 0;
                transform: translate(-300px, 0px) scale(4);
            }

        .fileupload-buttonbar .btn, .fileupload-buttonbar .toggle {
            margin-bottom: 5px;
        }

        .files .progress {
            width: 200px;
        }

        .fileupload-processing .fileupload-loading {
            display: block;
        }

        * html .fileinput-button {
            line-height: 24px;
            margin: 1px -3px 0 0;
        }

        * + html .fileinput-button {
            margin: 1px 0 0;
            padding: 2px 15px;
        }

        @media (max-width: 767px) {
            .files .btn span {
                display: none;
            }

            .files .preview * {
                width: 40px;
            }

            .files .name * {
                display: inline-block;
                width: 80px;
                word-wrap: break-word;
            }

            .files .progress {
                width: 20px;
            }

            .files .delete {
                width: 60px;
            }
        }

          ul {
              list-style-type: none;
              padding: 0px;
              margin: 0px;
          }
          .list{
              font-family: tahoma;
    font-size: 10px;
    font-weight: 500;
    color: #414d55;
          }
        
    </style>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
     <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
       <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>
    <script type="text/javascript">

     $(function () {

         $("[id*=ChkAll]").bind("click", function () {

             if ($(this).is(":checked")) {

                 $("[id*=chkDesig] input").attr("checked", "checked");

             } else {

                 $("[id*=chkDesig] input").removeAttr("checked");

             }

         });

         $("[id*=chkDesig] input").bind("click", function () {

             if ($("[id*=chkDesig] input:checked").length == $("[id*=chkDesig] input").length) {

                 $("[id*=ChkAll]").attr("checked", "checked");

             } else {

                 $("[id*=ChkAll]").removeAttr("checked");

             }

         });

     });

    </script>
    <script type="text/javascript" language="javascript">
        function ShowProgress1() {
            $('.loading').hide();
            $('#pan_fld').hide();
        }
        </script>
       <script type="text/javascript" language="javascript">
            $(document).ready(function () {
                if ($("#chkView").is(':checked')) {
                    $("#lblEffFrom").css("display", "none");
                    $("#txtEffFrom").css("display", "none");
                    $("#lblEffTo").css("display", "none");
                    $("#txtEffTo").css("display", "none");

                    $("#lblDate").css("display", "block");
                    $("#dvDate").css("display", "block");
                }
                else {
                    $("#lblEffFrom").css("display", "block");
                    $("#txtEffFrom").css("display", "block");
                    $("#lblEffTo").css("display", "block");
                    $("#txtEffTo").css("display", "block");

                    $("#lblDate").css("display", "none");
                    $("#dvDate").css("display", "none");
                }
            });
            $(document).on('change', '#chkView', function () {
                //debugger;
                if ($(this).prop('checked')) {
                    $("#lblEffFrom").css("display", "none");
                    $("#txtEffFrom").css("display", "none");
                    $("#lblEffTo").css("display", "none");
                    $("#txtEffTo").css("display", "none");

                    $("#lblDate").css("display", "block");
                    $("#dvDate").css("display", "block");
                } else {
                    $("#lblEffFrom").css("display", "block");
                    $("#txtEffFrom").css("display", "block");
                    $("#lblEffTo").css("display", "block");
                    $("#txtEffTo").css("display", "block");

                    $("#lblDate").css("display", "none");
                    $("#dvDate").css("display", "none");
                }
            });

        

            $("#ddlsubdiv").change(function () {
                var ddlVal = $("#ddlsubdiv").val();

                if ($("#chkView").is(':checked')) {
                    $("#lblEffFrom").css("display", "none");
                    $("#txtEffFrom").css("display", "none");
                    $("#lblEffTo").css("display", "none");
                    $("#txtEffTo").css("display", "none");

                    $("#lblDate").css("display", "block");
                    $("#dvDate").css("display", "block");
                }
                else {
                    $("#lblEffFrom").css("display", "block");
                    $("#txtEffFrom").css("display", "block");
                    $("#lblEffTo").css("display", "block");
                    $("#txtEffTo").css("display", "block");

                    $("#lblDate").css("display", "none");
                    $("#dvDate").css("display", "none");
                }
            });
    </script>
    

    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        var today = new Date();
        var lastDate = new Date(today.getFullYear(), today.getMonth(0) - 1, 31);
        var year = today.getFullYear() - 1;


        var dd = today.getDate();
        var mm = today.getMonth() + 01; //January is 0!
        var yyyy = today.getFullYear();

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.DOBDate').datepicker
            ({
                minDate: dd + '/' + mm + '/' + yyyy,
                //                minDate: new Date(year, 11, 1),
                //                maxDate: new Date(year, 11, 31),

                dateFormat: 'dd/mm/yy'
                //                yearRange: "2010:2017",
            });

            j('.DOBfROMDate').datepicker
            ({
                //                                minDate: new Date(year, 11, 1),
                //                                maxDate: new Date(year, 11, 31),

                dateFormat: 'dd/mm/yy'
                //                yearRange: "2010:2017",
            });
        });
    </script>
    
       <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
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
                            $('#btnUpload').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnUpload').click(function () {

               
                if ($("#txtSub").val() == "") { alert("Enter the Subject."); $('#txtSub').focus(); return false; }
             
              
                if ($("#txtEffFrom").val() == "") { alert("Select Effective From Date"); $('#txtEffFrom').focus(); return false; }
                if ($("#txtEffTo").val() == "") { alert("Select Effective To Date"); $('#txtEffTo').focus(); return false; }
               

           
            });
        });
    </script>  
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
        
            <div class="container home-section-main-body position-relative clearfix">         
      <asp:Panel ID="pnlCompose" runat="server" Width="100%" Visible="false">

      
         

          
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                      
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                              <div class="single-des clearfix">
                                <asp:Label ID="lblHome" CssClass="label" runat="server">Login Page Image<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                           
                            </div>
                                <asp:UpdatePanel ID="updPanelCompose" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                       
          
                                    <div class="DcrDispHPadFix">
                                        <table width="100%" align="center">
                                            <tr>
                                                <td width="10%">
                                                    <asp:Label ID="lblToMail" runat="server" Text="To:" visible="false"></asp:Label>
                                                    <td width="70%">
                                                        <asp:TextBox ID="txtAddr" runat="server" CssClass="form-control" ReadOnly="true" Width="85%" Visible="false"></asp:TextBox>
                                                           <asp:Panel ID ="pan_fld" runat="server" Visible="false" align="right"><span style="Color:Red;padding-left:5px;">Please Select FieldForce</span></asp:Panel> 
                                                    </td>
                                                    <td width="10%">
                                                        <asp:Panel ID="lblAddr" runat="server">
                                                            <span class="itemImage1">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="imgAddressBook" runat="server" OnClick="imgAddressBook_Click" UseSubmitBehavior="false"><i class=" fa fa-address-book"></i></asp:LinkButton>
                                                                                </span></td>
                                                                            <td style="width: 100%; margin-right: 40px; margin-top: 0px">
                                                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                                                    <ProgressTemplate>
                                                                                        <img id="Img22" alt="" src="~/Images/loading/loading19.gif" style="height: 20px; margin-top: 0px" runat="server" />
                                                                                        <span style="font-family: Verdana; color: Green; font-weight: bold;">Please Wait....</span>
                                                                                    </ProgressTemplate>
                                                                                </asp:UpdateProgress>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                              
                                   
                                                                     <Triggers>   <asp:PostBackTrigger ControlID="btnUpload" /> </Triggers>
                              
                                                            </asp:UpdatePanel>
                                                            </span>
                                                        </asp:Panel>
                                                    </td>
                                                    </td>
                                                </tr>
                                            </table>
                                                 
                         
                                                     </ContentTemplate>
                                                     </asp:UpdatePanel>
                             <div class="single-des clearfix">
                            <asp:FileUpload ID="fileUpload1" runat="server" CssClass="input" Width="100%" /></div>
                           
                             <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Subject<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtSub" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                       
                                    </div>

                            
                                  <div class="single-des clearfix">
                                <asp:Label ID="lblEffFrom" runat="server" CssClass="label">Effective From<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                <div id="dvEffc_Frm" class="row-fluid">
                                    <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input" 
                                        onkeypress="Calendar_enter(event);" Width="100%"
                                        onblur="this.style.backgroundColor='White'" TabIndex="6"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffFrom" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
                             
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblEffTo" runat="server" CssClass="label">Effective To<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                <div id="dvEffc_To" class="row-fluid">
                                    <asp:TextBox ID="txtEffTo" runat="server" CssClass="input"  
                                        onkeypress="Calendar_enter(event);" Width="100%"
                                        TabIndex="7" onblur="this.style.backgroundColor='White'" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEffTo" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
                                
                                </div>
                            </div>
                            </div>
                            
               <div class="w-100 designation-submit-button text-center clearfix">
                              
                                   
                                    <asp:Button ID="btnUpload" runat="server" CssClass="savebutton" OnClick="btnUpload_Click" Text="Upload" />
                             
                            </div>  
                        </div>
                        </div>

                           
                        </asp:Panel>
                              <asp:UpdatePanel ID="updPnlAddress" runat="server">
            <ContentTemplate>
              

                <asp:Panel ID="pnlpopup" runat="server" BackColor="AliceBlue" Width="700px"  background-color="AliceBlue" height="550px"
   
    

    
                    Style="left: 26%; top: 10%; position: fixed;z-index:9999; overflow-y: scroll;" BorderStyle="Solid" BorderWidth="1" >
                    <div>
                        <table border='0' style='border-collapse: collapse; width: 100%; height: 100%'>
                            <tr>
                               <td class='print Header' style='width: 100%'>&nbsp;<span class='itemImage1'>
                                    <i class=" fa fa-address-book"></i></span>&nbsp;&nbsp; </td>
                                <td>
                                    <asp:LinkButton ID="btnaddressclose" runat="server" ForeColor="Black" UseSubmitBehavior="false" OnClick="imgAddressClose_Click" OnClientClick="ShowProgress1()"><i class="fa fa-window-close"></i></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <table style="margin-left:10px">
                        <tr>
                            <td >
                                <asp:Label ID="Label1" runat="server">Search By</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="rdoadr" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rdoadr_SelectedIndexChanged" OnClick="ShowProgress();">
                                    <asp:ListItem Value="0" Text="" Selected="True">Designation</asp:ListItem>
                                    <asp:ListItem Value="1" Text="Field Force"></asp:ListItem>
                                    
                                  <asp:ListItem Value="2" Text="SubDivision "></asp:ListItem>
                                    <asp:ListItem Value="3" Text="State "></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table  style="margin-left:10px">
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server">Designation</asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td class="chkList">
                                <asp:CheckBox ID="chkLevelAll" runat="server" Text="All" AutoPostBack="true"
                                    OnCheckedChanged="chkLevelAll_CheckedChanged" OnChange="ShowProgress();" />
                            </td>
                        </tr>
                        <tr>
                            <td class="chkList">
                                <asp:CheckBox ID="chkMR" AutoPostBack="true" Visible="false" Text="MR" OnCheckedChanged="chkMR_OnCheckChanged"
                                    runat="server" CellPadding="1" CellSpacing="1" RepeatDirection="Horizontal" OnChange="ShowProgress();" ></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>                        
                            <td class="chkList">
                                <asp:CheckBoxList ID="chkDesgn" AutoPostBack="true" OnSelectedIndexChanged="chkDesgn_OnSelectedIndexChanged"
                                    runat="server" CellPadding="1" RepeatDirection="Vertical" RepeatColumns="7" OnChange="ShowProgress();"
                            TabIndex="7">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                    <table  style="margin-left:10px">
                        <tr>
                            <td width="1%">
                                <asp:Label ID="lblFF" Visible="false" runat="server" Text="Field Force"></asp:Label>
                            </td>
                            <td  width="10%">
                                <asp:DropDownList ID="ddlFFType" Visible="false" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="---Select---" Enabled="false"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Team" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Sub Division" Enabled="false"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td  width="1%">
                                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"  CssClass="form-control"
                                    OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" Width="80%"  >
                                </asp:DropDownList>
                            </td>
                            <td  width="15%">
                                <asp:DropDownList ID="ddlFieldForce" Visible="false" runat="server"  CssClass="form-control" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                    AutoPostBack="true" Width="80%" >
                                </asp:DropDownList>
                            </td>
                            <td  width="10%">
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false"  CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <caption>
                            <br />
                            <tr style="height: 10px">
                                <td width="10%"></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSelectedCount" runat="server" Text="Selected Value"></asp:Label>
                                </td>
                            </tr>
                        </caption>
                    </table>
                    <br />
                    <table  style="margin-left:10px">
                        <tr>
                            <td width="10%">
                       <asp:Label ID="lblsubname" Visible="false" runat="server" Text="SubDivision Name" CssClass="Hprint"
                           ForeColor="Navy" Font-Bold="true"></asp:Label>&nbsp; 
                        
                      <asp:DropDownList ID="ddlsub" Visible="false" runat="server" OnSelectedIndexChanged="ddlSub_SelectedIndexChanged" 
                                AutoPostBack="true" Width="400px"   CssClass="form-control"  >
                            </asp:DropDownList>
                                 </td>
                           
                            </tr>
                        <tr>
                            <td><asp:Label ID="lblstate" Visible="false" runat="server" Text="State" CssClass="Hprint"
                                ForeColor="Navy" Font-Bold="true"></asp:Label></td>
                          
                               <tr>
                            <td class="chkList">
                                <asp:CheckBox ID="ChkAllState" runat="server" Text="All" AutoPostBack="true" Visible="false"
                                    OnCheckedChanged="ChkAllState_CheckedChanged" OnChange="ShowProgress();" ForeColor="Black"  />
                            </td>
                                 
                        </tr>
                        <tr>
                            <td class="chkList">
                                <asp:CheckBox ID="chkMR_State" AutoPostBack="true" Visible="false"   OnCheckedChanged="chkMR_State_OnCheckChanged"
                                    runat="server" CellPadding="1" CellSpacing="1"  TabIndex="7" RepeatColumns="5"  RepeatDirection="Horizontal" ForeColor="Black" OnChange="ShowProgress();" ></asp:CheckBox>
                            </td>
                            
                        </tr>



                             <tr>
                                 <td>
                                     <asp:CheckBoxList ID="chkstate" runat="server" ForeColor="Black" AutoPostBack="true" CellPadding="1" CellSpacing="1" OnChange="ShowProgress();"  OnSelectedIndexChanged="chkstate_OnSelectedIndexChanged" RepeatColumns="7" RepeatDirection="vertical" Visible="false">
                                     </asp:CheckBoxList >
                                 </td>
                                
                            </tr>
                            </tr>
                        </table>
                    <table  style="margin-left:10px">
                        <tr>
                            <td width="10%">
                                <asp:Label ID="lblStateName" Visible="false" runat="server" Text="State Name" CssClass="Hprint" ForeColor="Navy" Font-Bold="true"></asp:Label>
                            

                            </td>
                            <td  width="10%">

                                <asp:DropDownList ID="ddlState" Visible="false" runat="server" Width="50%" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                    AutoPostBack="true" CssClass="form-control" >
                                </asp:DropDownList>
                                     
<%--                                             <asp:ListBox ID="lstst" runat="server" CssClass="selectpicker" Visible="false" 
                                            data-live-search="true" multiple data-actions-box="true" Font-Bold="True">
                                            <asp:ListItem Value="0" Text="-----Select the State-----"></asp:ListItem>
                                        </asp:ListBox>--%>


                             
                            </td>
                        </tr>
                       
                    </table>

                       <div class="loading" align="center">
                Loading. Please wait.            <br />
                <img src="../../images/loader.gif" alt="" />
            </div>
       
                    <table width="100%" style="width: 100%; height: 350px; margin-top: 5px" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <td style="border-top-style: solid; border-width: thin" valign="top">
                                <div style="height: 270px; overflow: auto;">
                                    <asp:CheckBoxList ID="chkFF" runat="server">
                                    </asp:CheckBoxList>
                                    <asp:GridView ID="gvFF" runat="server" AutoGenerateColumns="False"
                                        GridLines="None" HeaderStyle-CssClass="Hprint"
                                        HeaderStyle-HorizontalAlign="Left" OnRowDataBound="grdFF_RowDataBound" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSf" CssClass="chkSf" runat="server" Text="&nbsp;" OnCheckedChanged="gvFF_OnCheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="230px" HeaderText="FieldForce Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSf_Name" runat="server" Text='<% #Eval("sf_name")%>' Width="230px"  class="list" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation_Short_Name" runat="server" class="list" Text='<% #Eval("Designation_Short_Name")%>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_mail" runat="server" Text='<% #Eval("sf_mail")%>' Visible="false" class="list"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="HQ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSf_HQ" runat="server" class="list" Text='<% #Eval("Sf_HQ")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBackcolor" runat="server" class="list" Text='<% #Eval("des_color")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_color" runat="server" class="list" Text='<% #Eval("sf_color")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation_Code" class="list" runat="server" Text='<% #Eval("Designation_Code")%>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_Code" Visible="false" class="list" runat="server" Text='<% #Eval("sf_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_Type" runat="server" class="list" Text='<% #Eval("sf_Type")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" runat="server" class="list" Text='<% #Eval("State_Code")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsubdivision_code" runat="server"  class="list" Text='<% #Eval("subdivision_code")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Red" />

                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

                           

                         <%--   <div class="single-des clearfix">
                                <asp:Label ID="lblDate" runat="server" CssClass="label" Text="Select Date"></asp:Label>
                                <div id="dvDate" class="row-fluid">
                                    <asp:DropDownList ID="ddlDate" CssClass="nice-select" runat="server" SkinID="ddlRequired" Width="100%"  >
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:CheckBox ID="chkView" Text="Edit" Checked="false" runat="server" visible="true"/>
                            </div>             
                            </div>--%>
                           
                    <div class="col-lg-11">
                        <div class="designation-reactivation-table-area clearfix">
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" OnRowCommand="gvDetails_RowCommand"
                                        OnRowDeleting="gvDetails_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         
                                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Left" />
                                            
                                            <asp:BoundField DataField="Subject" HeaderText="Subject" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="FileName" HeaderText="FileName" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="FilePath" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandArgument='<%# Eval("Id") %>' CommandName="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SF_NAME" HeaderText="Send To FieldForce" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Upload_Date" HeaderText="Upload Date and Time" ItemStyle-HorizontalAlign="Left" />
                                                  <asp:BoundField DataField="Effective_From" HeaderText="From Date" ItemStyle-HorizontalAlign="Left" />
                                                  <asp:BoundField DataField="Effective_To" HeaderText="To Date" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                        CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Image');">Delete
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    
           
             </div>  
                        </div>
               
           <%-- <div class="loading" align="center">
                Loading. Please wait.            <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>--%>
       
         </div>  
        
    </form>
   


        <%--<script type="text/javascript">
            $(function () {

                $('[id*=lstst]').multiselect({
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: 250,
                    includeSelectAllOption: true,
                    buttonWidth: '270px'
                });
            });
                 </script>--%>


     <script src="../../assets/js/jQuery.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../assets/js/main.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    <script>
        $(document).on("click", ".chkSf", function (e) {
            var cnt = 0;
            $('#gvFF tbody tr').each(function () {
                var chk = $(this).find("input:checkbox");
                if ($(chk).prop("checked")) {
                    cnt++;
                }
            });
            $("#lblSelectedCount").text("No.of Filed Force Selected : " + cnt);
        });
       
    </script>
</body>
</html>
