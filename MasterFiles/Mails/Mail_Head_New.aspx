<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="Mail_Head_New.aspx.cs" Inherits="MasterFiles_Mails_Mail_Head"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mail</title>

    <%--<link href="../../css/bootstrap_new.min.css" rel="stylesheet" type="text/css" />--%>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>--%>
    <%--<script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>

    <script type="text/javascript" src="../../JsFiles/common.js"></script>
    <link href="../../assets/css/style.css" rel="stylesheet" />
    <link href="../../assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/nice-select.css" rel="stylesheet" />
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link href="../../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <%--<link type="text/css" rel="stylesheet" href="../../css/sfm_style.css" />--%>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" rel="stylesheet">
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>

    <script type="text/javascript">
        function PrintGridData() {

            var prtGrid = document.getElementById('<%=pnlViewMailInbox.ClientID %>');
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <script type="text/javascript" src="../../tinymce/jscripts/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            //$(".loader").delay(5000).fadeOut("slow");                      
        });
    </script>
    <script type="text/javascript">
        tinyMCE.init({
            mode: "textareas",
            theme: "advanced",
            plugins: "spellchecker,table,style,save,advhr,advimage,emotions,iespell,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,visualchars,nonbreaking,xhtmlxtras,template,imagemanager,filemanager",
            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,fontselect,fontsizeselect,|,cut,copy,paste,pastetext,pasteword,|",
            theme_advanced_buttons2: "bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,image,|,insertdate,inserttime,preview,|,forecolor,backcolor,|,hr,sub,sup,charmap,emotions,|,table",
            theme_advanced_buttons3: "",
            theme_advanced_buttons4: "",
            //theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
            //theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
            //theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
            //theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: false,
            template_external_list_url: "js/template_list.js",
            external_link_list_url: "js/link_list.js",
            external_image_list_url: "js/image_list.js",
            media_external_list_url: "js/media_list.js"
        });
        function valid() {
            with (frmAddr) {
                if (txtAddr.value == '') { alert('Select the Fieldforce to whom to send'); txtAddr.focus(); return false; }
                if (txtSub.value == '') { alert('Enter Subject'); txtSub.focus(); return false; }
                if (document.getElementById("txtMsg").value.length == 0) {

                    //confrm = confirm("Send this subject without text in the body?");
                    if (confrm == true) {
                        return true;
                    }
                    else {
                        txtMsg.focus();
                        return false;
                    }
                }
            }
        }
    </script>
    <%--<script type="text/javascript">
        function CCFunc(cc) {
            if (cc.id == "idCC") {
                //clearAddr(1);
                if (cc.childNodes[1].innerText == 'Remove Cc') {
                    cc.childNodes[1].innerText = 'Add Cc';
                    TrCC.style.display = 'none';
                    cc.childNodes[1].title = 'Add to CC';
                    return true;
                }
                if (cc.childNodes[1].innerText == 'Add Cc') {
                    cc.childNodes[1].innerText = 'Remove Cc';
                    cc.childNodes[1].title = 'Remove From CC';
                    TrCC.style.display = '';
                    return true;
                }
            }
            if (cc.id == "idBCC") {
                //clearAddr(2);
                if (cc.childNodes[1].innerText == 'Remove Bcc') {
                    cc.childNodes[1].innerText = 'Add Bcc';
                    TrBCC.style.display = 'none';
                    cc.childNodes[1].title = 'Add From BCC';
                    return true;
                }
                if (cc.childNodes[1].innerText == 'Add Bcc') {
                    cc.childNodes[1].innerText = 'Remove Bcc';
                    TrBCC.style.display = '';
                    cc.childNodes[1].title = 'Remove From BCC';
                    return true;
                }
            }
        }
    </script>--%>
    <%--<script type="text/javascript">
        function LimtCharacters(txtMsg, CharLength, indicator) {
            chars = txtMsg.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (chars > CharLength) {
                txtMsg.value = txtMsg.value.substring(0, CharLength);
            }
        }
    </script>--%>
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

        td.chkList input[type="checkbox"] + label, input[type="radio"] + label {
            color: #6a6a6a;
        }

        input[type="checkbox"] + label {
            color: white;
        }

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
    </style>
</head>
<body id="bdy" runat="server">
    <form id="frmAddr" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div class="container-fluid">
            <link rel='stylesheet prefetch' href='http://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css'>
            <div class="mail-box">
                <aside class="sm-side" style="height: 1000px">
                      <div class="user-head">
                          <div id="divBtnHome" runat="server">
                                    <span>
                                        <asp:Button ID="btnHome" CssClass="savebutton" Text="Go to Home" runat="server" OnClick="btnHome_Click">
                                        </asp:Button></span>
                                </div>
                      </div>
                      <div class="inbox-body">
                          <a href="#myModal" data-toggle="modal"  title="rejectbutton">
                              <asp:LinkButton ID="LinkButton5" Text="Compose" CssClass="btn btn-compose" runat="server" Font-Underline="false" OnClick="btnCompose_Onclick">
                            </asp:LinkButton>
                          </a>
                          <!-- Modal -->
                          <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="Div6" class="modal fade" style="display: none;">
                              <div class="modal-dialog">
                                  <div class="modal-content">
                                      <div class="modal-header">
                                          <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                          <h4 class="modal-title">Compose</h4>
                                      </div>
                                      <div class="modal-body">
                                          <form role="form" class="form-horizontal">
                                              <div class="form-group">
                                                  <label class="col-lg-2 control-label">To</label>
                                                  <div class="col-lg-10">
                                                      <input type="text" placeholder="" id="Text1" class="form-control">
                                                  </div>
                                              </div>
                                              <div class="form-group">
                                                  <label class="col-lg-2 control-label">Cc / Bcc</label>
                                                  <div class="col-lg-10">
                                                      <input type="text" placeholder="" id="Text2" class="form-control">
                                                  </div>
                                              </div>
                                              <div class="form-group">
                                                  <label class="col-lg-2 control-label">Subject</label>
                                                  <div class="col-lg-10">
                                                      <input type="text" placeholder="" id="Text3" class="form-control">
                                                  </div>
                                              </div>
                                              <div class="form-group">
                                                  <label class="col-lg-2 control-label">Message</label>
                                                  <div class="col-lg-10">
                                                      <textarea rows="10" cols="30" class="form-control" id="Textarea1" name=""></textarea>
                                                  </div>
                                              </div>

                                              <div class="form-group">
                                                  <div class="col-lg-offset-2 col-lg-10">
                                                      <span class="btn green fileinput-button">
                                                        <i class="fa fa-plus fa fa-white"></i>
                                                        <span>Attachment</span>
                                                        <input type="file" name="files[]" multiple="">
                                                      </span>
                                                      <button class="btn btn-send" type="submit">Send</button>
                                                  </div>
                                              </div>
                                          </form>
                                      </div>
                                  </div><!-- /.modal-content -->
                              </div><!-- /.modal-dialog -->
                          </div><!-- /.modal -->
                      </div>
                      <ul class="inbox-nav inbox-divider">
                          <li>
                              <asp:LinkButton ID="btnInbox" runat="server" OnClick="btnInbox_Click" Font-Underline="false">
                                  <i class="fa fa-inbox"></i>
                                  <asp:Label ID="lblInboxCnt" runat="server">Inbox</asp:Label>
                            </asp:LinkButton>
                          </li>
                          <li>
                            <asp:LinkButton ID="btnSentItem" runat="server" 
                                Font-Underline="false" OnClick="btnSentItem_Click">
                                <i class="fa fa-envelope-o"></i>
                                <asp:Label ID="lblSent" runat="server">Sent Mails</asp:Label>
                            </asp:LinkButton>
                          </li>
                          <li>
                            <asp:LinkButton ID="btnView" Text="" runat="server"
                                Font-Underline="false" OnClick="btnView_Click">
                                <i class="fa fa-bookmark-o"></i>
                                <asp:Label ID="lblViewed" runat="server">Viewed Mails</asp:Label>
                            </asp:LinkButton>
                          </li>
                          <li>
                            <asp:LinkButton ID="btnTrash" Visible="false" Text="" runat="server" Font-Underline="false"><i class=" fa fa-trash-o"></i>
                                <asp:Label ID="Label7" Text="" runat="server">Trash Mails</asp:Label>
                            </asp:LinkButton>
                          </li>
                      </ul>
                      <ul class="nav nav-pills nav-stacked labels-info inbox-divider">
                          <li> <h4>Folders<asp:LinkButton ID="lnk" OnClick="btnHome_Click" runat="server" Text=" " Style="text-decoration: none;"></asp:LinkButton>List
</h4> </li>
                          <asp:Panel ID="pnlMoveFolder" runat="server">
                            <asp:GridView ID="grdClickFolder" runat="server" AutoGenerateColumns="False" OnRowCommand="grdClickFolder_RowCommand"
                                OnRowDataBound="grdClickFolder_RowDataBound" Width="100%"
                                AllowPaging="True" GridLines="None">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <i class="fa fa-folder-open"></i>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnFolder" CommandName="Folder" runat="server" ForeColor="#6a6a6a"
                                                Text='<% #Eval("Move_MailFolder_Name")%>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblslNo" runat="server" Visible="false" ForeColor="#6a6a6a" Text='<% #Eval("Move_MailFolder_Name")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                      </ul>
                  </aside>
                <aside class="lg-side">
                      <div class="inbox-head">
                          <h3></h3>
                          <div class="btn-group img-rounded" style="float: right; margin-right: 7px; background: none repeat scroll 0 0 #00a8b3;">
                                <asp:LinkButton ID="LinkButton2" runat="server" class="btn dropdown-toggle" ForeColor="White" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <span>
                                        <img src="~/Images/User-Profile.png" runat="server" id="imgSF" class="img-circle" alt=" " />
                                        <asp:Label ID="lblSfName" runat="server" />
                                    </span>&nbsp; <span class="caret"></span>
                                </asp:LinkButton>
                              <ul class="dropdown-menu">
                                    <li><a style="background-color: transparent;"><%--<span class="glyphicon glyphicon-picture"></span>--%>
                                        <asp:Button Text="Edit Photo" ID="btnProfilePhoto" OnClick="btnProfilePhoto_Click" BackColor="Transparent" BorderColor="Transparent" runat="server" /></a><%--<a href="../../index.aspx" style="background-color: transparent;"><span class="glyphicon glyphicon-picture">
                                    </span>&nbsp;Change Photo</a>--%></li>
                                    <li><a href="../../index.aspx" style="background-color: transparent;"><%--<span class="glyphicon glyphicon-log-out">
                                    </span>--%>&nbsp;Logout</a></li>
                                </ul>
                            </div>
                      </div>
                      <div class="inbox-body">
                         <div class="mail-option">
                             <table width="100%">
                                    <tr>
                                        <td width="4%" title='Selected Mail(s) Move To Folder'>
                                            <asp:Label ID="Lab7" runat="server"><i class="fa fa-folder-open"></i>&nbsp;Move To</asp:Label>
                                            </td>
                                        <td width="10%">
                                        <asp:DropDownList ID="ddlMoved" runat="server" Enabled="false"  CssClass="form-control" OnSelectedIndexChanged="ddlMoved_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </td>
                                        <td width="2%">
                                    <%--</div>
                                    <div class="single-des clearfix">--%>
                                        <asp:Label ID="lblSubjectSearch" runat="server">&nbsp;&nbsp;Search</asp:Label>
                                            </td>
                                        <td width="10%">
                                        <asp:TextBox ID="txtboxSearch" runat="server" Enabled="false"  CssClass="form-control"></asp:TextBox>
                                            </td>
                                        <td width="2%">
                                        <asp:ImageButton ID="imgSearch" CssClass="imgBtnSrch" runat="server" Width="20" Height="20" OnClick="imgSearch_Click" ImageUrl="../../images/Search.png" />
                                    </td>
                                        <td width="2%">
                                        <asp:Label ID="Label2" runat="server">Filter</asp:Label>
                                            </td>
                                        <td width="10%">
                                            <%--</div>
                                    <div class="single-des clearfix">--%>
                                        <asp:DropDownList ID="ddlMon" OnSelectedIndexChanged="ddlMon_OnSelectedIndex" CssClass="form-control" AutoPostBack="true" runat="server">
                                                    <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        <td width="1%">&nbsp;</td>
                                        <td width="10%">
                                                <asp:DropDownList ID="ddlYr" AutoPostBack="true"  CssClass="form-control" OnSelectedIndexChanged="ddlYr_OnSelectedIndex" runat="server">
                                                </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                             <br />
                             <asp:UpdatePanel ID="updPnlAddress" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlpopup" runat="server" BackColor="AliceBlue" MinWidth="700px" Width="70%"
                    Style="left: 19%; top: 10%; position: absolute;" BorderStyle="Solid" BorderWidth="1">
                    <div>
                        <table border='0' style='border-collapse: collapse; width: 100%; height: 100%'>
                            <tr>
                                <td class='print Header' style='width: 100%'>&nbsp;<span class='itemImage1'>
                                    <i class=" fa fa-address-book"></i></span>&nbsp;&nbsp;Address
                                Book </td>
                                <td>
                                    <asp:LinkButton ID="btnaddressclose" runat="server" ForeColor="Black" UseSubmitBehavior="false" OnClick="imgAddressClose_Click"><i class="fa fa-window-close"></i></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <table style="margin-left:10px">
                        <tr>
                            <td >
                                <asp:Label ID="lblSearch" runat="server">Search By</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="rdoadr" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rdoadr_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="" Selected="True">Designation</asp:ListItem>
                                    <asp:ListItem Value="1" Text="Field Force"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="State Name"></asp:ListItem>
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
                                    OnCheckedChanged="chkLevelAll_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="chkList">
                                <asp:CheckBox ID="chkMR" AutoPostBack="true" Visible="false" Text="MR" OnCheckedChanged="chkMR_OnCheckChanged"
                                    runat="server" CellPadding="1" CellSpacing="1" RepeatDirection="Horizontal"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>                        
                            <td class="chkList">
                                <asp:CheckBoxList ID="chkDesgn" AutoPostBack="true" OnSelectedIndexChanged="chkDesgn_OnSelectedIndexChanged"
                                    runat="server" CellPadding="1" CellSpacing="1" RepeatDirection="Horizontal">
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
                                    <asp:ListItem Value="3" Text="Sub Division" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td  width="1%">
                                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"  CssClass="form-control"
                                    OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" Width="90%">
                                </asp:DropDownList>
                            </td>
                            <td  width="15%">
                                <asp:DropDownList ID="ddlFieldForce" Visible="false" runat="server"  CssClass="form-control" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                    AutoPostBack="true" Width="90%">
                                </asp:DropDownList>
                            </td>
                            <td  width="10%">
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false"  CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                        </tr><br />
                        <tr style="height: 10px">
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSelectedCount" runat="server" Text="Selected Value"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                    <table  style="margin-left:10px">
                        <tr>
                            <td width="3%">
                                <asp:Label ID="lblStateName" Visible="false" runat="server" Text="State Name"></asp:Label>
                            </td>
                            <td  width="10%">
                                <asp:DropDownList ID="ddlState" Visible="false" runat="server" Width="50%" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                    AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height: 10px">
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="SelectedValue"></asp:Label></td>
                        </tr>
                    </table>
                    <table width="100%" style="width: 100%; height: 350px; margin-top: 5px" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <td style="border-top-style: solid; border-width: thin" valign="top">
                                <div style="height: 350px; overflow: auto;">
                                    <asp:CheckBoxList ID="chkFF" runat="server">
                                    </asp:CheckBoxList>
                                    <asp:GridView ID="gvFF" runat="server" AutoGenerateColumns="False"
                                        GridLines="None" HeaderStyle-BackColor="#ededea" HeaderStyle-CssClass="Hprint"
                                        HeaderStyle-HorizontalAlign="Left" OnRowDataBound="grdFF_RowDataBound" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSf" runat="server" Text="." AutoPostBack="true" OnCheckedChanged="gvFF_OnCheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="230px" HeaderText="FieldForce Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSf_Name" runat="server" Text='<% #Eval("sf_name")%>' Width="230px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation_Short_Name" runat="server" Text='<% #Eval("Designation_Short_Name")%>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_mail" runat="server" Text='<% #Eval("sf_mail")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="HQ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSf_HQ" runat="server" Text='<% #Eval("Sf_HQ")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBackcolor" runat="server" Text='<% #Eval("des_color")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_color" runat="server" Text='<% #Eval("sf_color")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation_Code" runat="server" Text='<% #Eval("Designation_Code")%>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_Code" Visible="false" runat="server" Text='<% #Eval("sf_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_Type" runat="server" Text='<% #Eval("sf_Type")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" runat="server" Text='<% #Eval("State_Code")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsubdivision_code" runat="server" Text='<% #Eval("subdivision_code")%>' Visible="false"></asp:Label>
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
        <asp:Panel runat="server" Visible="false" ID="pnlProfile_Img" Height="100px" Width="400px" >
            <div style="border-collapse: collapse; width: 100%; margin: 5%; text-align: center;">
                <asp:FileUpload ID="upldProfile_Img" runat="server" CssClass="form-control" />
                <br />
                <asp:Button Text="Update" ID="btnPhoto_Upld" CssClass="btn btn-sm btn-primary" runat="server" OnClick="btnPhoto_Upld_Click" />
                <asp:Button ID="btnPhoto_Cncl" Text="Cancel" CssClass="btn btn-sm btn-primary" runat="server" OnClick="btnPhoto_Cncl_Click" />
            </div>
        </asp:Panel>
                        <asp:Panel ID="pnlInbox" runat="server" Width="100%">
                            <div class="DcrDispHPadFix">
                                <table width="100%" id="tmp">
                                    <tr>
                                        <td class='print' id='tdDel' width="10%" title='Delete the Selected Mail(s)'>
                                            <div runat="server" id="divBtnDelete" visible="false">
                                                <span>
                                                    <asp:LinkButton ID="btnDelete" CssClass="btn btn-default btn-sm" Text="" Font-Underline="false"
                                                        Font-Bold="true" ForeColor="black" runat="server" OnClick="btnDelete_Onclick">
                                                        <i class=" fa fa-trash-o"></i>
                                                        <asp:Label ID="Label4" Text="Delete" runat="server" />
                                                    </asp:LinkButton></span>
                                            </div>
                                        </td>
                                        <td class='print' id='tdrply' title='Reply to a Current Read Mail' width="10%" onclick="val='rly';RlyFun();">
                                            <div runat="server" id="divBtnReply" visible="false">
                                                <span>
                                                    <asp:LinkButton ID="btnReply" CssClass="btn btn-default btn-sm" Text="" Font-Underline="false"
                                                        Font-Bold="true" ForeColor="black" runat="server" OnClick="btnReply_Onclick">
                                                        <i class=" fa fa-reply"></i>
                                                        &nbsp;<asp:Label ID="Label5" Text="Reply" runat="server" />
                                                    </asp:LinkButton></span>
                                            </div>
                                        </td>
                                        <td class='print' id='tdrplyAll' title='Forward  To a Current Read Mail' onclick="val='rlyAll';RlyFun();">
                                            <div runat="server" id="divBtnForward" visible="false">
                                                <span>
                                                    <asp:LinkButton ID="btnForward" CssClass="btn btn-default btn-sm" Text="" Font-Underline="false"
                                                        Font-Bold="true" ForeColor="black" runat="server" OnClick="btnForward_Onclick">
                                                        <i class=" fa fa-arrow-right"></i>
                                                        <asp:Label ID="Label6" Text="Forward" runat="server" />
                                                    </asp:LinkButton></span>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                             <asp:Panel ID="pnlViewInbox" runat="server" Width="100%">
            <div style="border-style:solid;border-width:1px; border-color:#41cac0">
                <table border='0' style='border-collapse: collapse; width: 100%; height: 100%'>
                    <tr>
                        <td class='print Header' style='width: 100%'>&nbsp;<span class='itemImage1'>
                            <i class="fa fa-address-book"></i></span>&nbsp;&nbsp;View Mail Details </td>
                        <td class='print Header' title='Close The Current Window'>
                            <asp:LinkButton ID="btnimgViewMail" runat="server" Height="20px" Width="20px" ForeColor="Black"
                                UseSubmitBehavior="false" OnClick="imgViewMail_Click"><i class="fa fa-window-close"></i></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="DcrDispHPadFix" style="border-style:solid;border-width:1px; border-color:#41cac0">
                <table width="100%" id="Table3">
                    <tr>
                        <td class='print' id='td2' width="9%" title='Delete the Selected Mail(s)'>
                            <div id="divDelete" onclick='return MoveFlder(0)'>
                                <asp:LinkButton ID="imgbtnDeleteViewMail" ForeColor="Black" runat="server" OnClick="imgbtnDeleteViewMail_Click">
                                    &nbsp;&nbsp;<i class="fa fa-trash"></i>&nbsp;&nbsp; Delete</asp:LinkButton>
                            </div>
                        </td>
                        <td class='print' id='td3' width="8%" title='Reply to a Current Read Mail'>
                            <asp:LinkButton ID="imgbtnReplyViewMail" ForeColor="Black" runat="server" OnClick="imgbtnReplyViewMail_Click">
                                <i class="fa fa-reply"></i>&nbsp;&nbsp; Reply</asp:LinkButton>
                        </td>
                        <td id='td5' class='print' width="9%" title='Forward  To a Current Read Mail'>
                            <asp:LinkButton ID="imgbtnFwdViewMail" ForeColor="Black" runat="server" OnClick="imgbtnFwdViewMail_Click">
                            <i class="fa fa-arrow-right"></i>&nbsp;&nbsp;Forward</asp:LinkButton>
                        </td>
                        <td class='print' width="7%" onclick='tkPrn()' id='Td8' title='Print Mail'>
                            <span class='itemImage'>
                                <asp:LinkButton ID="LinkButton1" ForeColor="Black" runat="server" OnClientClick="PrintGridData()">
                                <i class="fa fa-print">&nbsp;&nbsp; Print</asp:LinkButton></span></td>
                        <td class='print' id="td10" width="5%" runat="server" nowrap title='Close The Current Window'>
                            <asp:LinkButton ID="lnkBtnMailClose" ForeColor="Black" runat="server" OnClick="imgViewMail_Click">
                            <i class="fa fa-close"></i>&nbsp;&nbsp; Close</asp:LinkButton></td>
                        <td class="print" style="width: 80%; text-align: left">
                            <div id="Div2">
                                &nbsp;&nbsp;
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="pnlViewMailInbox" BorderStyle="Solid" BorderWidth="1" BorderColor="#41cac0" runat="server" Style="background: none repeat scroll 0 0 #41cac0;">
                <table width="100%" style="margin-left: 1%;">
                    <tr style="width: 50px">
                        <td style="width: 75px">
                            <asp:Label ID="lblFrom" runat="server" Text="" Font-Size="X-Small">&nbsp;From</asp:Label></td>
                        <td width="10px">: </td>
                        <td>
                            <asp:Label ID="lblViewFrom" runat="server" Font-Size="X-Small"></asp:Label>
                        </td>
                    </tr>
                    <tr style="width: 50px">
                        <td style="width: 25px">
                            <asp:Label ID="lblTo" runat="server" Text="" Font-Size="X-Small">&nbsp;To</asp:Label></td>
                        <td width="10px">: </td>
                        <td>
                            <asp:Label ID="lblViewTo" runat="server" Font-Size="X-Small"></asp:Label></td>
                    </tr>
                    <tr style="width: 50px">
                        <td style="width: 25px">
                            <asp:Label ID="lblCc" runat="server" Text="" Font-Size="X-Small">&nbsp;CC</asp:Label></td>
                        <td width="10px">: </td>
                        <td>
                            <asp:Label ID="lblViewCC" runat="server" Font-Size="X-Small"></asp:Label></td>
                    </tr>
                    <tr style="width: 50px">
                        <td style="width: 25px">
                            <asp:Label ID="lblSubject" runat="server" Text="" Font-Size="X-Small">&nbsp;Subject</asp:Label></td>
                        <td width="10px">: </td>
                        <td>
                            <asp:Label ID="lblViewSub" runat="server" Font-Size="X-Small"></asp:Label></td>
                    </tr>
                    <tr style="width: 50px">
                        <td style="width: 25px">
                            <asp:Label ID="lblSentDate" runat="server" Text="" Font-Size="X-Small">&nbsp;Sent Date</asp:Label></td>
                        <td width="10px">: </td>
                        <td>
                            <asp:Label ID="lblViewSent" runat="server" Font-Size="X-Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Image ID="imgViewAttach" runat="server" Visible="false" ImageUrl="~/Images/Attachment.gif" />
                        </td>
                        <td>
                            <asp:PlaceHolder ID="plcHldr_Attachments" runat="server" />
                        </td>
                    </tr>
                </table>
                <div style="background-color: White; padding: 1% 2% 3% 2%; min-height: 350px; height: auto; max-height: 95%;">
                    <span style="white-space: pre-line;">
                        <asp:Label Text="" ID="lblMailBody" runat="server" />
                        <%--<asp:TextBox  SkinID="txtArea" onDrag="return false;" onDrop="return false;"
                        name="txtMsg" TextMode="MultiLine" Enabled="false" onpaste="return MaxLenOnPaste(5000)" MaxLength="5000"
                        Height="400px" Width="100%" runat="server"></asp:TextBox>--%>
                    </span>
                </div>
            </asp:Panel>
        </asp:Panel>
                         </div>
                        <div id="divMain" runat="server">

                        <table id="divInboxList" runat="server">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvInbox" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInbox_RowDataBound"
                                        Width="100%" HeaderStyle-CssClass="Hprint" 
                                        GridLines="None" OnRowCommand="gvInbox_RowCommand" AllowPaging="True" PageSize="15"
                                        HeaderStyle-BackColor="#ededea" CssClass="table table-inbox table-hover" HeaderStyle-HorizontalAlign="Left"
                                        OnPageIndexChanging="gvInbox_OnPageIndexChanging" PagerStyle-CssClass="gridview1" EmptyDataText="No Mail(s)...">
                                        <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" LastPageText="Last" FirstPageText="First" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="cbSelectAll" runat="server"  Text="." AutoPostBack="true" OnCheckedChanged="cbSelectAll_OnCheckedChanged"
                                                         />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkId" runat="server"  Text="." AutoPostBack="true" OnCheckedChanged="chkId_OnCheckedChanged"
                                                         />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="From" SortExpression="From_Name"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMail_From" runat="server" Text='<% #Eval("From_Name")%>'
                                                        Visible="false" />
                                                    <asp:LinkButton ID="lnk_MailFrom" ForeColor="Black" Font-Underline="false" runat="server" Text='<% #Eval("From_Name")%>'
                                                        CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Subject" SortExpression="Mail_Subject"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMail_subject" runat="server" Text='<% #Eval("Mail_subject")%>'
                                                        Visible="false" />
                                                    <asp:LinkButton ID="lnk_MailSub" runat="server" Text='<% #Eval("Mail_subject")%>'
                                                        CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Date" SortExpression="Mail_Sent_Time"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMail_Date" runat="server" Text='<% #Eval("Mail_Sent_Time")%>'
                                                        Visible="false" />
                                                    <asp:LinkButton ID="lnk_MailDate" ForeColor="Black" Font-Underline="false" runat="server" Text='<% #Eval("Mail_Sent_Time")%>'
                                                        CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderImageUrl="~/Images/Attachment.gif">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgAttach" runat="server" ImageUrl="~/Images/Attachment.gif" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblView" Visible="false" runat="server" Text='<% #Eval("Mail_Attachement")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Trans_sl_No" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblslNo" runat="server" Visible="false" Text='<% #Eval("Trans_sl_No")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnslNo" runat="server" Value='<% #Eval("Trans_sl_No")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                        <FooterStyle BackColor="#DDEEFF" />
                                        <EmptyDataRowStyle Width="100%" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlViewMail" runat="server">
                            <table id="Table1" runat="server">
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdView_RowDataBound"
                                            HeaderStyle-CssClass="Hprint" AllowPaging="True" Width="100%"
                                            OnRowCommand="grdView_RowCommand" PageSize="15" HeaderStyle-BackColor="#ededea"
                                            CssClass="table table-inbox table-hover" OnPageIndexChanging="grdView_PageIndexChanging" HeaderStyle-HorizontalAlign="Left"
                                            PagerStyle-CssClass="gridview1" EmptyDataText="No Mail(s)..." GridLines="None">
                                            
                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" LastPageText="Last" FirstPageText="First" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="cbSelectAll" runat="server"  Text="."  AutoPostBack="true" OnCheckedChanged="grdViewcbSelectAll_OnCheckedChanged"
                                                             />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" runat="server"  Text="."  AutoPostBack="true" OnCheckedChanged="grdViewchkId_OnCheckedChanged"
                                                             />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="From" SortExpression="From_Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMail_From" runat="server" Text='<% #Eval("From_Name")%>'
                                                            Visible="false" />
                                                        <asp:LinkButton ID="lnk_ViewMailFrom" Font-Underline="false" ForeColor="Black" runat="server" Text='<% #Eval("From_Name")%>'
                                                            CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Subject" SortExpression="Mail_Subject" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMail_subject" runat="server" Text='<% #Eval("Mail_subject")%>' Visible="false" />
                                                        <asp:LinkButton ID="lnk_MailSub" runat="server" Text='<% #Eval("Mail_subject")%>' CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Date" SortExpression="Mail_Sent_Time" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMail_Date" runat="server" Text='<% #Eval("Mail_Sent_Time")%>' Visible="false" />
                                                        <asp:LinkButton ID="lnk_ViewMailDate" ForeColor="Black" Font-Underline="false" runat="server" Text='<% #Eval("Mail_Sent_Time")%>'
                                                            CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderImageUrl="~/Images/Attachment.gif" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgAttach" runat="server" ImageUrl="~/Images/Attachment.gif" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Trans_sl_No" ItemStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblslNo" runat="server" Visible="false" Text='<% #Eval("Trans_sl_No")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnslNo" runat="server" Value='<% #Eval("Trans_sl_No")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                            <FooterStyle BackColor="#DDEEFF" />
                                            <EmptyDataRowStyle Wrap="false" Width="100%" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlSent" runat="server">
                            <table id="Table2">
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdSent" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdSent_RowDataBound"
                                            HeaderStyle-CssClass="Hprint" AllowPaging="True"
                                            Width="100%" OnRowCommand="grdSent_RowCommand" PageSize="15" CssClass="table table-inbox table-hover"
                                            HeaderStyle-BackColor=" #ededea" HeaderStyle-HorizontalAlign="Left" EmptyDataText="No Mail(s)..."
                                            PagerStyle-CssClass="gridview1" HeaderStyle-Font-Size="7pt" HeaderStyle-Font-Names="Verdana"
                                            OnPageIndexChanging="grdSent_PageIndexChanging" GridLines="None">
                                            
                                            <PagerStyle CssClass="pgr" Font-Size="6pt" />
                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" LastPageText="Last" FirstPageText="First" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width='1%' ItemStyle-HorizontalAlign="center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="cbSelectAll" runat="server"  Text="."  AutoPostBack="true" OnCheckedChanged="grdcbSelectAll_OnCheckedChanged"
                                                             />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" runat="server"  Text="."  AutoPostBack="true" OnCheckedChanged="grdchkId_OnCheckedChanged"
                                                             />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="To" HeaderStyle-Width="35%" ItemStyle-Width="35%" ItemStyle-CssClass="print"
                                                    SortExpression="Mail_Sf_Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%# Eval("Mail_Sf_Name") %>'
                                                            Visible="false" />
                                                        <asp:LinkButton ID="lnk_SentMailTo" Font-Underline="false" ForeColor="Black" runat="server" Text='<% #Eval("To_SFName")%>'
                                                            CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="Subject" HeaderStyle-Width="45%" ItemStyle-Width="45%" ItemStyle-CssClass="print"
                                                    SortExpression="Mail_Subject" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMail_subject" runat="server" Text='<% #Eval("Mail_subject")%>'
                                                            Visible="false" />
                                                        <asp:LinkButton ID="lnk_MailSub" runat="server" Text='<% #Eval("Mail_subject")%>'
                                                            CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="Date" HeaderStyle-Width="17%" ItemStyle-Width="17%" ItemStyle-CssClass="print"
                                                    SortExpression="Mail_Sent_Time" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMail_Date" runat="server" Text='<% #Eval("Mail_Sent_Time")%>'
                                                            Visible="false" />
                                                        <asp:LinkButton ID="lnk_SentMailDate" Font-Underline="false" ForeColor="Black" runat="server" Text='<% #Eval("Mail_Sent_Time")%>'
                                                            CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderImageUrl="~/Images/Attachment.gif" HeaderStyle-Width="3%"
                                                    ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgAttach" runat="server" ImageUrl="~/Images/Attachment.gif" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblslNo" runat="server" Visible="false" Text='<% #Eval("Trans_sl_No")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Trans_sl_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%"
                                                    Visible="false" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnslNo" runat="server" Value='<% #Eval("Trans_sl_No")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                            <FooterStyle BackColor="#DDEEFF" />
                                            <EmptyDataRowStyle Width="100%" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlFolder" runat="server" Width="100%">
                            <table id="Table4">
                                <tr>
                                    <td height="100%">
                                        <asp:GridView ID="grdFolder" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdFolder_RowDataBound"
                                            Width="100%" Height="100%" HeaderStyle-CssClass="Hprint"
                                            OnRowCommand="grdFolder_RowCommand" AllowPaging="True" PageSize="20" HeaderStyle-BackColor="#ededea"
                                            OnPageIndexChanging="grdFolder_PageIndexChanging" HeaderStyle-HorizontalAlign="Left"
                                            EmptyDataText="No Mail(s)..." GridLines="None">
                                            
                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" LastPageText="Last" FirstPageText="First" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width='1%' HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="cbSelectAll" runat="server" Text="." AutoPostBack="true" OnCheckedChanged="grdFoldercbSelected_OnCheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" AutoPostBack="true" Text="." runat="server" OnCheckedChanged="grdFoldercbChkId_OnCheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="From" HeaderStyle-Width="35%" ItemStyle-Width="35%" ItemStyle-CssClass="print"
                                                    SortExpression="From_Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%# Eval("From_Name") %>'
                                                            Visible="false" />
                                                        <asp:LinkButton ID="lnk_SentMailTo" Font-Underline="false" ForeColor="Black" runat="server" Text='<% #Eval("From_Name")%>'
                                                            CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Subject"
                                                    SortExpression="Mail_Subject" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMail_subject" runat="server" Text='<% #Eval("Mail_subject")%>'
                                                            Visible="false" />
                                                        <asp:LinkButton ID="lnk_MailSub" runat="server" Text='<% #Eval("Mail_subject")%>'
                                                            CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="Date" HeaderStyle-Width="17%" ItemStyle-Width="17%" ItemStyle-CssClass="print"
                                                    SortExpression="Mail_Sent_Time" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMail_Date" runat="server" Text='<% #Eval("Mail_Sent_Time")%>'
                                                            Visible="false" />
                                                        <asp:LinkButton ID="lnk_SentMailDate" Font-Underline="false" ForeColor="Black" runat="server" Text='<% #Eval("Mail_Sent_Time")%>'
                                                            CommandArgument='<%# Eval("Trans_sl_No") %>' CommandName="ViewMail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="3%" HeaderImageUrl="~/Images/Attachment.gif">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgAttach" runat="server" ImageUrl="~/Images/Attachment.gif" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Trans_sl_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width='30%'
                                                    Visible="false" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblslNo" runat="server" Visible="false" Text='<% #Eval("Trans_sl_No")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnslNo" runat="server" Value='<% #Eval("Trans_sl_No")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                            <%--<PagerStyle Font-Names="Verdana" ForeColor="Black" VerticalAlign="Bottom" BackColor="AliceBlue" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1" Font-Size="Large" CssClass="GridPager" />--%>
                                            <FooterStyle BackColor="#DDEEFF" />
                                            <EmptyDataRowStyle Font-Size="9pt" ForeColor="Red" Font-Names="Verdana" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlCompose" runat="server" Width="100%">
                            <div class="DcrDispHPadFix">
                                <table>
                                    <tr>
                                        <td title='Clear a New Mail' width="10%">
                                            <div runat="server" id="div1" visible="true">
                                                <span>
                                                    <asp:LinkButton ID="lnkClear" Width="100%" CssClass="btn btn-primary btn-xs" Text="" Font-Underline="false" runat="server" OnClick="lnkClear_Click">
                                                    <i class=" fa fa-eraser"></i>
                                                    <asp:Label ID="Label10" ForeColor="White" Text="Clear All" runat="server" />
                                                    </asp:LinkButton></span>
                                            </div>
                                        </td>
                                        <td width="1%"></td>
                                        <td title='Send a Composed Mail' width="10%">
                                            <div runat="server" id="divBtnSend" visible="true">
                                                <span>
                                                    <asp:LinkButton ID="ImgBtnSend" Width="100%" CssClass="btn btn-primary btn-xs" Text="" Font-Underline="false" runat="server" OnClientClick="return valid()" OnClick="lnkBtnSend_Click">
                                                    <i class=" fa fa-paper-plane"></i>                                                        
                                                        <asp:Label ID="Label9" Text="Send" runat="server" />
                                                    </asp:LinkButton></span>
                                            </div>
                                        </td>
                                        <td width="1%"></td>
                                        <td title='Add/Remove Cc' width="10%">
                                            <div runat="server" id="div3" visible="true">
                                                <span>
                                                    <asp:LinkButton ID="LinkButton3" Width="100%" CssClass="btn btn-primary btn-xs" Text="" Font-Underline="false" runat="server" OnClick="lnkRemoveCC_Click">
                                                        <i class=" fa fa-user-plus"></i>
                                                        <asp:Label ID="lnkRemoveCC" ForeColor="White" Text="Remove CC" runat="server" />
                                                    </asp:LinkButton></span>
                                            </div>
                                        </td>
                                        <td width="1%"></td>
                                        <td title='Add/Remove Bcc' width="10%">
                                            <div runat="server" id="div4" visible="true">
                                                <span>
                                                    <asp:LinkButton ID="LinkButton4" Width="100%" CssClass="btn btn-primary btn-xs" Text="" Font-Underline="false" runat="server" OnClick="imgRemoveBCC_Click">
                                                        <i class=" fa fa-user-plus"></i> 
                                                        &nbsp;<asp:Label ID="imgRemoveBCC" ForeColor="White" Text="Remove BCC" runat="server" />
                                                    </asp:LinkButton></span>
                                            </div>
                                        </td>
                                        <td width="1%"></td>
                                        <td width="10%">
                                            <div runat="server" id="div5" visible="true">
                                                <span>
                                                    <asp:LinkButton ID="btnMsgDiscard" Width="100%" CssClass="btn btn-primary btn-xs" Text="" Font-Underline="false" runat="server" OnClick="btnMsgDiscard_Click">
                                                        <i class=" fa fa-close"></i>
                                                        <asp:Label ID="Label11" ForeColor="White" Text="Discard Message" runat="server" />
                                                    </asp:LinkButton></span>
                                            </div>
                                        </td>
                                    </tr>
                                    </table>
                                <table>
                                    <tr>
                                                    <td width="1%">
                                                        <i class=" fa fa-paperclip"></i>
                                                    </td>
                                                    <td width="2%">
                                                        &nbsp;<asp:Label ID="Label8" Text="Attachment(s) :" runat="server" />
                                                    </td>
                                                    <td width="50%">
                                                        <asp:GridView ID="grdAttchmentFiles" runat="server" ShowFooter="true" AutoGenerateColumns="false"
                                                            GridLines="None" OnRowCreated="grdAttchmentFiles_RowCreated">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="RowNumber" HeaderText="#" />--%>
                                                                <%--<asp:TemplateField  HeaderText="Header 3">  
                                                            <ItemTemplate>  
                                                                <asp:Label ID="lblUpldFiles" runat="server" />
                                                            </ItemTemplate>  
                                                            </asp:TemplateField>--%>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:FileUpload ID="upldFiles" CssClass="form-control" runat="server" />
                                                                        <asp:Label ID="lblFileAttach" Text='<%# Eval("Original_Name") %>' Visible="false"
                                                                            runat="server" />
                                                                        <asp:HiddenField ID="hdnAttachFile" Value='<%# Eval("New_File_Name") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <br />
                                                                        <asp:Button ID="btnNewFile" CssClass="btn btn-primary btn-xs" runat="server" Text="More Files" OnClick="btnNewFile_Click" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkRemoveAttach" runat="server" OnClick="lnkRemoveAttach_Click">Remove
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                </table>
                            </div><br />
                            <asp:UpdatePanel ID="updPanelCompose" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="DcrDispHPadFix">
                                        <table width="100%" align="center">
                                            <tr>
                                                <td width="10%">
                                                    <asp:Label ID="lblToMail" runat="server">To:</asp:Label>
                                                    </td>
                                                <td width="70%">
                                                    <asp:TextBox ID="txtAddr" runat="server" Width="95%" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td width="10%">
                                                    <asp:Panel ID="lblAddr" runat="server">
                                                        <span class='itemImage1'>
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton  ID="imgAddressBook" runat="server" UseSubmitBehavior="false" 
                                                                                    OnClick="imgAddressBook_Click"><i class=" fa fa-address-book"></i></asp:LinkButton>
                                                                                </span>
                                                                            </td>
                                                                            <td style="width: 100%; margin-right: 40px; margin-top: 0px">
                                                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                                                    <ProgressTemplate>
                                                                                        <img id="Img22" alt="" src="~/Images/loading/loading19.gif" style="height: 20px; margin-top: 0px" runat="server" /><span
                                                                                            style="font-family: Verdana; color: Green; font-weight: bold;">Please Wait....</span>
                                                                                    </ProgressTemplate>
                                                                                </asp:UpdateProgress>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr><td>&nbsp;</td></tr>
                                            <tr id="TrCC" runat="server">
                                                <td width="10%">
                                                    <asp:Label ID="Label13" runat="server">CC:</asp:Label>
                                                    </td>
                                                <td width="70%">
                                                    <asp:TextBox ID="txtAddr1" ReadOnly="true" runat="server" Width="95%" CssClass="form-control"></asp:TextBox></td>
                                                <td width="10%">
                                                    <asp:Panel ID="lblAddr2" runat="server">
                                                        <span class='itemImage1'>
                                                            <asp:LinkButton ID="imgComposeCC" runat="server" UseSubmitBehavior="false"
                                                                OnClick="imgComposeCC_Click"><i class=" fa fa-address-book"></i></asp:LinkButton>
                                                        </span>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr><td>&nbsp;</td></tr>
                                            <tr id="TrBCC" runat="server">
                                                <td width="10%">
                                                    <asp:Label ID="Label14" runat="server">BCC:</asp:Label>
                                                    </td>
                                                <td width="70%">
                                                    <asp:TextBox ID="txtAddr2" ReadOnly="true" runat="server" Width="95%" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:Panel ID="lblAddr3" runat="server" Width="150px">
                                                        <span class='itemImage1'>
                                                            <asp:LinkButton ID="imgComposeBCC" runat="server" UseSubmitBehavior="false" 
                                                                OnClick="imgComposeBCC_Click"><i class=" fa fa-address-book"></i></asp:LinkButton>
                                                        </span>
                                                    </asp:Panel>
                                                </td>
                                                <td width="50%"></td>
                                            </tr>
                                            <tr><td>&nbsp;</td></tr>
                                            <tr>
                                                <td width="10%">
                                                    <asp:Label ID="Label15" runat="server">Subject:</asp:Label>
                                                    </td>
                                                <td width="70%">
                                                    <asp:TextBox ID="txtSub" runat="server" MaxLength="1000" Width="95%" CssClass="form-control"></asp:TextBox></td>
                                                <td colspan="2">
                                                    <div id="divAttach" style="display: none;">
                                                        <%--<asp:Image ID="imgAtt" ImageUrl="~/images/Attachment.gif" runat="server" />--%>
                                                        <i class=" fa fa-paperclip"></i>
                                                        <asp:Label ID="lblFileName" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="lblAttachment" runat="server"></asp:Label>
                                                        <asp:LinkButton CssClass="print" ID="lbFileDel" runat="server" Text="Remove"></asp:LinkButton>
                                                    </div>
                                                    <asp:HiddenField ID="hidAttPath" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnaddressclose" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" style="background-color: White;">
                                        <div align="center" style="margin-top: 10px;">
                                            <asp:TextBox ID="txtMsg" Style="border: 1px solid;" SkinID="txtArea" onDrag="return true;"
                                                onDrop="return true;" name="excel_data" TextMode="MultiLine" BorderWidth="1"
                                                onpaste="return LimtCharacters(this,5000,'lblCount')" MaxLength="5000" Height="330px"
                                                Width="95%" runat="server" onKeyDown="return LimtCharacters(this,5000,'lblCount')">
                                            </asp:TextBox>
                                        </div>
                                        <br />
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="col-xs-2">
                                                    <asp:Label ID="Label1" Text="Insert Image to Above" runat="server" />
                                                </div>
                                                <div class="col-xs-4">
                                                    <asp:FileUpload ID="fileUpld" runat="server" Width="100%" CssClass="form-control" />
                                                </div>
                                                <div class="col-xs-3">
                                                    <asp:Button Text="Add Image" ID="btnAddImg" CssClass="btn btn-primary btn-xs" runat="server" OnClick="btnAddImg_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                             
        <%--<asp:Panel ID="PnlAttachment" Visible="false" runat="server" BackColor="AliceBlue" Height="200px"
        Width="600px" Style="left: 21%; top: 45%; position: absolute;" BorderStyle="Solid"
        BorderWidth="1">
        <div>
            <table border='0' style='border-collapse: collapse; width: 100%; height: 100%'>
                <tr>
                    <td class='print Header' style='width: 100%'>
                        &nbsp;<span class='itemImage1'><i class=" fa fa-address-book"></i></span>Address
                        Book
                    </td>
                    <td class='print Header'>
                        <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="../../images/close.gif"
                            OnClick="ImgAttachment_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="border-collapse: collapse; width: 100%; margin-top: 20px">
            <table border='1' style='border-collapse: collapse; width: 100%; height: 100%'>
                <tr>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
                    </td>
                </tr>
                <tr align="right">
                    <td>
                        <asp:Button ID="btnUpload" Text="Attachment" runat="server" OnClick="btn_Go" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>--%>
        
                        </div>
                    </div>
            </aside>
            </div>
        </div>
    </form>
    <%--<div class="loader" runat="server" id="divLoading">
	    <div class="loader-centered">
		    <div class="object square-one"></div>
		    <div class="object square-two"></div>
		    <div class="object square-three"></div>
	    </div>
    </div>--%>.
    
    <script src="../../assets/js/jQuery.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../assets/js/main.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
</body>
</html>
