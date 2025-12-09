<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Common_Doctor_List_FDC.aspx.cs"
    Inherits="MasterFiles_Common_Doctor_List_FDC" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ADDITION LISTED DOCTOR LIST</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <link href="../../css/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../../JsFiles/jquery.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#FilUpImage").change(function () {
                $("#dvPreview").html("");
                var maxFileSize = 1048576
                var fileUpload = $('#FilUpImage');
                if (fileUpload[0].files[0].size < maxFileSize) {

                    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                    if (regex.test($(this).val().toLowerCase())) {
                        if ($.browser.msie && parseFloat(jQuery.browser.version) <= 9.0) {
                            $("#dvPreview").show();
                            $("#dvPreview")[0].filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = $(this).val();
                        }
                        else {
                            if (typeof (FileReader) != "undefined") {
                                $("#dvPreview").show();
                                $("#dvPreview").append("<img />");
                                var reader = new FileReader();
                                reader.onload = function (e) {
                                    $("#dvPreview img").attr("src", e.target.result);
                                }
                                reader.readAsDataURL($(this)[0].files[0]);
                            } else {
                                alert("This browser does not support FileReader.");
                            }
                        }
                    } else {
                        alert("Please upload a valid image file.");
                    }
                } else {
                    alert('File size should be less than 1 MB')
                    fileUpload.val('');
                    return false;
                }
            });
        });
    </script>
    <style type="text/css">
        .aclass
        {
            border: 1px solid lighgray;
        }
        .aclass
        {
            width: 50%;
        }
        .aclass tr td
        {
            background: White;
            font-weight: bold;
            color: Black;
            border: 1px solid black;
            border-collapse: collapse;
        }
        .aclass th
        {
            border: 1px solid black;
            border-collapse: collapse;
            background: LightBlue;
        }
        .lbl
        {
            color: Red;
        }
        
        
        .space
        {
            padding: 6px 6px;
        }
        .sp
        {
            padding-left: 11px;
        }
        
        .style6
        {
            padding: 3px 3px;
            height: 28px;
        }
        .marRight
        {
            margin-right: 35px;
        }
        
        .boxshadow
        {
            -moz-box-shadow: 3px 3px 5px #535353;
            -webkit-box-shadow: 3px 3px 5px #535353;
            box-shadow: 3px 3px 5px #535353;
        }
        .roundbox
        {
            -moz-border-radius: 6px 6px 6px 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px 6px 6px 6px;
        }
        .grd
        {
            border: 1;
            border-color: Black;
        }
        .roundbox-top
        {
            -moz-border-radius: 6px 6px 0 0;
            -webkit-border-radius: 6px 6px 0 0;
            border-radius: 6px 6px 0 0;
        }
        .roundbox-bottom
        {
            -moz-border-radius: 0 0 6px 6px;
            -webkit-border-radius: 0 0 6px 6px;
            border-radius: 0 0 6px 6px;
        }
        .gridheader, .gridheaderbig, .gridheaderleft, .gridheaderright
        {
            padding: 6px 6px 6px 6px;
            background: #003399 url(images/vertgradient.png) repeat-x;
            text-align: center;
            font-weight: bold;
            text-decoration: none;
            color: khaki;
        }
        
        .gridheaderleft
        {
            text-align: left;
        }
        .gridheaderright
        {
            text-align: right;
        }
        .gridheaderbig
        {
            font-size: 135%;
        }
        .gridview1
        {
            background-color: #336699;
            border-style: none;
            padding: 2px;
            margin: 2% auto;
        }
          .blink_me {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 1s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 1s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 1s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
        }
          @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        .gridview1 a
        {
            margin: auto 1%;
            border-style: none;
            border-radius: 50%;
            background-color: #444;
            padding: 5px 7px 5px 7px;
            color: #fff;
            text-decoration: none;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
        }
        .gridview1 a:hover
        {
            background-color: #1e8d12;
            color: #fff;
        }
        .gridview1 td
        {
            border-style: none;
        }
        .gridview1 span
        {
            background-color: #ae2676;
            color: #fff;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
            border-radius: 50%;
            padding: 5px 7px 5px 7px;
        }
        .mGridImg1
        {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }
        .mGridImg1 td
        {
            padding: 2px;
            border-color: Black;
            background: F2F1ED;
            font-size: small;
            font-family: Calibri;
        }
        
        .mGridImg1 th
        {
            padding: 4px 2px;
            color: white;
            background: #336699;
            border-color: Black;
            border-left: solid 1px Black;
            border-right: solid 1px Black;
            border-top: solid 1px Black;
            border-bottom: solid 1px Black;
            font-weight: normal;
            font-size: small;
            font-family: Calibri;
        }
        .mGridImg1 .pgr
        {
            background: #336699;
        }
        .mGridImg1 .pgr table
        {
            margin: 5px 0;
        }
        .mGridImg1 .pgr td
        {
            border-width: 0;
            text-align: left;
            padding: 0 6px;
            border-left: solid 1px #666;
            font-weight: bold;
            color: Red;
            line-height: 12px;
        }
        .mGridImg1 .pgr a
        {
            color: White;
            text-decoration: none;
        }
        .mGridImg1 .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
    </style>
    <style type="text/css">
        .WaterMarkedTextBox
        {
            height: 16px;
            line-height: 25px;
            width: 268px;
            padding: 2px 2 2 2px;
            border: 1px solid #BEBEBE;
            background-color: #F0F8FF;
            color: gray;
            font-size: 8pt;
            text-align: center;
        }
        .WaterMarkedTextBox1
        {
            height: 16px;
            width: 268px;
            padding: 2px 2 2 2px;
            border: 1px solid #BEBEBE;
            background-color: #F0F8FF;
            color: Red;
            font-size: 8pt;
            text-align: center;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function Focus(objname, waterMarkText) {
            obj = document.getElementById(objname);
            obj.value = "";
            obj.style.background = "#F0F8FF";
            obj.style.fontWeight = "normal";
            if (obj.value == waterMarkText) {
                obj.value = "";
                obj.className = "WaterMarkedTextBox";
                if (obj.value == "User Name" || obj.value == "" || obj.value == null) {
                    obj.style.color = "gray";
                }
            }
        }
        function Blur(objname, waterMarkText) {
            obj = document.getElementById(objname);
            if (obj.value == "") {
                obj.value = waterMarkText;
                obj.className = "WaterMarkedTextBox";
                obj.style.color = "gray";
                obj.style.size = "12px";
            }

        }
        //        function Minimum(obj, min) {
        //            if (obj.value.length < min) {
        //                alert('Please Enter Minimum 6 Characters');
        //                obj.value = "";
        //                obj.className = "WaterMarkedTextBox1";
        //            }
        //        }
    </script>
    <script type="text/javascript">
        function openWindow(C_Doctor_Code, type) {
            var mrcode = $("#hdnSfCode").val();
            var mode = $("#hdnMode").val();
            //   window.open('Common_Doctor_Updation_FDC.aspx?Code=' + code, 'open_window', ' width=640, height=480, left=0, top=0');
            var popUpObj;
            //  var randomnumber = Math.floor((Math.random() * 100) + 1);

            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("Common_Doctor_Updation_FDC.aspx?C_Doctor_Code=" + C_Doctor_Code + "&type=" + 1 + "&Mrcode=" + mrcode + "&Mode=" + mode,
            //"ModalPopUp" + randomnumber,
      "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=900," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            //
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                // var ImgSrc = "https://s3.postimg.org/x2mwp52dv/loading1.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
        }
       


    </script>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

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
                            $('#btnAdd').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnAdd').click(function () {
                var div_code = $("#hdndivCode").val();
                if ($("#txtName").val() == "") { alert("Enter Doctor Name."); $('#txtName').focus(); return false; }

                var qual = $('#<%=ddlQual.ClientID%> :selected').text();
                if (qual == "---Select---") { alert("Select Qualification."); $('#ddlQual').focus(); return false; }
                var spec = $('#<%=ddlSpec.ClientID%> :selected').text();
                if (spec == "---Select---") { alert("Select Speciality."); $('#ddlSpec').focus(); return false; }

                var cat = $('#<%=ddlCatg.ClientID%> :selected').text();
                if (cat == "---Select---") { alert("Select Category."); $('#ddlCatg').focus(); return false; }

                var cls = $('#<%=ddlCls.ClientID%> :selected').text();
                if (cls == "---Select---") { alert("Select Class."); $('#ddlCls').focus(); return false; }
                var Hq = $('#<%=ddlterr.ClientID%> :selected').text();
                if (Hq == "---Select---") { alert("Select Area Cluster Name."); $('#ddlterr').focus(); return false; }


                if ($("#txtHospital").val() == "") { alert("Enter Clinic Name."); $('#txtHospital').focus(); return false; }
                if ($("#txtHosAddress").val() == "") { alert("Enter Clinic Address."); $('#txtHosAddress').focus(); return false; }
                if ($("#txtMobile").val() == "") { alert("Enter Mobile No."); $('#txtMobile').focus(); return false; }
                if ($("#lblCity").val() == "") { alert("Enter City."); $('#lblCity').focus(); return false; }
                if ($("#txtPincode").val() == "") { alert("Enter Pincode."); $('#txtPincode').focus(); return false; }
                var st = $('#<%=ddlState.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select State."); $('#ddlState').focus(); return false; }

                if ($('#FilUpImage').val() == "") { alert("Please Select Visiting Card."); $('#FilUpImage').focus(); return false; }
                if (div_code == 3) {
                    var prod = $('#<%=ddlProd1.ClientID%> :selected').text();
                    if (prod == "---Select---") { alert("Select Product 1."); $('#ddlProd1').focus(); return false; }
                    var prod2 = $('#<%=ddlProd2.ClientID%> :selected').text();
                    if (prod2 == "---Select---") { alert("Select Product 2."); $('#ddlProd2').focus(); return false; }
                    var prod3 = $('#<%=ddlProd3.ClientID%> :selected').text();
                    if (prod3 == "---Select---") { alert("Select Product 3."); $('#ddlProd3').focus(); return false; }
                    var prod4 = $('#<%=ddlProd4.ClientID%> :selected').text();
                    if (prod4 == "---Select---") { alert("Select Product 4."); $('#ddlProd4').focus(); return false; }
                    var prod5 = $('#<%=ddlProd5.ClientID%> :selected').text();
                    if (prod5 == "---Select---") { alert("Select Product 5."); $('#ddlProd5').focus(); return false; }

                }
                if (div_code == 4) {
                    var prod = $('#<%=ddlProd1.ClientID%> :selected').text();
                    if (prod == "---Select---") { alert("Select Product 1."); $('#ddlProd1').focus(); return false; }
                    var prod2 = $('#<%=ddlProd2.ClientID%> :selected').text();
                    if (prod2 == "---Select---") { alert("Select Product 2."); $('#ddlProd2').focus(); return false; }
                    var prod3 = $('#<%=ddlProd3.ClientID%> :selected').text();
                    if (prod3 == "---Select---") { alert("Select Product 3."); $('#ddlProd3').focus(); return false; }
                    var prod4 = $('#<%=ddlProd4.ClientID%> :selected').text();
                    if (prod4 == "---Select---") { alert("Select Product 4."); $('#ddlProd4').focus(); return false; }
                    var prod5 = $('#<%=ddlProd5.ClientID%> :selected').text();
                    if (prod5 == "---Select---") { alert("Select Product 5."); $('#ddlProd5').focus(); return false; }
                }
                if (div_code == 5) {
                    var prod = $('#<%=ddlProd1.ClientID%> :selected').text();
                    if (prod == "---Select---") { alert("Select Product 1."); $('#ddlProd1').focus(); return false; }
                    var prod2 = $('#<%=ddlProd2.ClientID%> :selected').text();
                    if (prod2 == "---Select---") { alert("Select Product 2."); $('#ddlProd2').focus(); return false; }
                    var prod3 = $('#<%=ddlProd3.ClientID%> :selected').text();
                    if (prod3 == "---Select---") { alert("Select Product 3."); $('#ddlProd3').focus(); return false; }
                    var prod4 = $('#<%=ddlProd4.ClientID%> :selected').text();
                    if (prod4 == "---Select---") { alert("Select Product 4."); $('#ddlProd4').focus(); return false; }
                    var prod5 = $('#<%=ddlProd5.ClientID%> :selected').text();
                    if (prod5 == "---Select---") { alert("Select Product 5."); $('#ddlProd5').focus(); return false; }
                }
                if (div_code == 7 || div_code == 8 || div_code == 9 || div_code == 10) {
                    var prod = $('#<%=ddlProd1.ClientID%> :selected').text();
                    if (prod == "---Select---") { alert("Select Product 1."); $('#ddlProd1').focus(); return false; }
                    var prod2 = $('#<%=ddlProd2.ClientID%> :selected').text();
                    if (prod2 == "---Select---") { alert("Select Product 2."); $('#ddlProd2').focus(); return false; }
                    var prod3 = $('#<%=ddlProd3.ClientID%> :selected').text();
                    if (prod3 == "---Select---") { alert("Select Product 3."); $('#ddlProd3').focus(); return false; }

                }
                if (div_code == 6) {
                    var prod = $('#<%=ddlProd1.ClientID%> :selected').text();
                    if (prod == "---Select---") { alert("Select Product 1."); $('#ddlProd1').focus(); return false; }


                }

            });

        }); 
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSearch').click(function () {
                var txt = document.getElementById("txtDoctor");
                if ($("#txtDoctor").val() == "FirstName" && $("#txtLast").val() == "LastName" && $("#txtQual").val() == "Qualification" && $("#cboCountry").val() == "City" && $("#txtMob").val() == "Mobile" && $("#txtSt").val() == "State" && $("#txtReg").val() == "RegNo" && $("#txtPin").val() == "PinCode") {
                    alert("Please Enter atleast one Textbox"); $('#txtDoctor').focus(); return false;
                }
                if ($("#txtMob").val() == "Mobile" && $("#txtReg").val() == "RegNo") {
                    if ($("#txtDoctor").val() != "FirstName") {
                        if ($("#txtDoctor").val().length < 3) {

                            alert("Please Enter Minimum 3 Characters"); $('#txtDoctor').focus(); return false;
                        }
                        else if ($("#cboCountry").val() == "City" || $("#cboCountry").val() == null) {
                            alert("Please Enter City Name"); $('#cboCountry').focus(); return false;
                        }
                    }
                    if ($("#cboCountry").val() != "City") {
                        if ($("#txtDoctor").val() == "FirstName" && $("#txtQual").val() == "Qualification" && $("#txtPin").val() == "PinCode" && $("#txtSt").val() == "State" && $("#txtLast").val() == "LastName") {
                            alert("Please Enter Docotor Name"); $('#txtDoctor').focus(); return false;
                        }
                    }
                    if ($("#txtLast").val() != "LastName") {
                        if ($("#txtLast").val().length < 3) {

                            alert("Please Enter Minimum 3 Characters"); $('#txtLast').focus(); return false;
                        }
                        else if ($("#cboCountry").val() == "City" || $("#cboCountry").val() == null) {
                            alert("Please Enter City Name"); $('#cboCountry').focus(); return false;
                        }
                    }
                }
                if ($("#txtPin").val() != "PinCode") {
                    if ($("#cboCountry").val() == "City" || $("#cboCountry").val() == null) {
                        alert("Please Enter City Name"); $('#cboCountry').focus(); return false;
                    }
                }

                if ($("#txtQual").val() != "Qualification") {
                    if ($("#cboCountry").val() == "City" || $("#cboCountry").val() == null) {
                        alert("Please Enter City Name"); $('#cboCountry').focus(); return false;
                    }
                }
                if ($("#txtSt").val() != "State") {
                    if ($("#cboCountry").val() == "City" || $("#cboCountry").val() == null) {
                        alert("Please Enter City Name"); $('#cboCountry').focus(); return false;
                    }
                }
                if ($("#txtMob").val() != "Mobile") {
                    if ($("#txtMob").val().length < 6) {

                        alert("Mobile Number Enter Minimum 6 Characters"); $('#txtMob').focus(); return false;
                    }
                    else {

                        document.getElementById("txtDoctor").value = "FirstName";
                        document.getElementById("cboCountry").value = "City";
                        document.getElementById("txtLast").value = "LastName";
                        document.getElementById("txtQual").value = "Qualification";
                        document.getElementById("txtSt").value = "State";
                        document.getElementById("txtReg").value = "RegNo";
                        document.getElementById("txtPin").value = "PinCode";
                    }
                }
                if ($("#txtReg").val() != "RegNo") {
                    if ($("#txtReg").val().length < 4) {

                        alert("Reg No Enter Minimum 4 Characters"); $('#txtReg').focus(); return false;
                    }
                    else {

                        document.getElementById("txtDoctor").value = "FirstName";
                        document.getElementById("cboCountry").value = "City";
                        document.getElementById("txtLast").value = "LastName";
                        document.getElementById("txtQual").value = "Qualification";
                        document.getElementById("txtSt").value = "State";
                        document.getElementById("txtMob").value = "Mobile";
                        document.getElementById("txtPin").value = "PinCode";
                    }
                }
            });
            $('#btnRef').click(function () {
                if ($("#txtRef").val() == "") {
                    alert("Please Enter Ref No"); $('#txtRef').focus(); return false;
                }
            });
        }); 

    </script>
    <script type="text/javascript">
        function nospaces(t) {
            if (t.value.match(/\s/g)) {
                t.value = t.value.replace(/\s/g, '');
            }
        }
    </script>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("#btnRef").attr('disabled', 'disabled');
            $("#txtRef").keypress(function () {
                if ($("#txtRef").val().length > 0) {
                    $("#btnRef").removeAttr('disabled');

                }
            });

            
        });
</script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="Menu1" runat="server" />
        <br />
        <asp:HiddenField runat="server" ID="hdndivCode" />
        <%--<center>
            <table width="91%">
                <tr>
                    <td style="width: 3.2%" />
                    <td>
                        <asp:Button ID="btnAdd" runat="server" CssClass="savebutton" Text="Add Common Doctor"
                            BackColor="HotPink" ForeColor="White" Width="150px" OnClick="btnAdd_Click" />
                    </td>
                </tr>
            </table>
        </center>--%>
        <center>
            <asp:Panel runat="server" ID="pnl" Width="200px" BackColor="Yellow">
                <asp:RadioButton ID="rdoNew" runat="server" Text="New" Font-Size="14px" AutoPostBack="true"
                    Enabled="false" ForeColor="Red" Font-Bold="true" GroupName="Multi" OnCheckedChanged="rdoNew_CheckedChanged" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoEx" Font-Size="14px" Checked="true" AutoPostBack="true" ForeColor="Red"
                    runat="server" Text="Existing" Font-Bold="true" GroupName="Multi" OnCheckedChanged="rdoEx_CheckedChanged" />
            </asp:Panel>
        </center>
        <br />
        <asp:Panel ID="pnlexist" runat="server">
            <center>
                <%--<table>
                <tbody>
                    <tr>
                        <td align="left">
                            <asp:Label ID="SearchBy" Font-Bold="true" runat="server" Text="SearchBy" ForeColor="Purple">
                            </asp:Label>
                            &nbsp;
                            <asp:DropDownList ID="ddlFields" SkinID="ddlRequired" runat="server" CssClass="DropDownList">
                                <asp:ListItem Selected="true" Value="">---Select---</asp:ListItem>
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="C_Doctor_Name">Doctor Name</asp:ListItem>
                                <asp:ListItem Value="C_Doctor_HQ">HQ Name</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" Width="150px" onfocus="this.style.backgroundColor='#E0EE9D'"
                                CssClass="TEXTAREA"></asp:TextBox>
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Go" Width="30px"
                                Height="25px" CssClass="savebutton"></asp:Button>
                        </td>
                    </tr>
                </tbody>
            </table>--%>
                <div class="roundbox boxshadow" style="width: 60%; border: solid 2px steelblue;">
                    <div class="gridheaderleft" style="background: pink; color: Black">
                        Search By <span style="float: right">
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" BackColor="LightBlue"
                                Text="Search" Width="60px" Height="25px"></asp:Button>
                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" BackColor="LightGreen"
                                Text="Clear" Width="60px" Height="25px"></asp:Button>
                        </span>
                    </div>
                    <div class="boxcontenttext" style="background: White;">
                        <div id="pnlPreviewSurveyData">
                            <br />
                            <table>
                                <tr>
                                    <td align="left" class="stylespc">
                                        <asp:TextBox ID="txtDoctor" runat="server" BorderColor="BlueViolet" onfocus="Focus(this.id,'FirstName')"
                                            onblur="Blur(this.id,'FirstName');" Width="140px" Height="30px" Font-Names="verdana"
                                            Font-Size="12px" CssClass="WaterMarkedTextBox" Text="FirstName"></asp:TextBox>
                                    </td>
                                    <td align="left" class="stylespc">
                                        <asp:TextBox ID="txtLast" runat="server" BorderColor="BlueViolet" Font-Names="verdana"
                                            Font-Size="12px" onfocus="Focus(this.id,'LastName')" onblur="Blur(this.id,'LastName');"
                                            Width="140px" Height="30px" CssClass="WaterMarkedTextBox" Text="LastName"></asp:TextBox>
                                    </td>
                                    <td valign="bottom">
                                        <%--<asp:TextBox ID="txtCity" runat="server" BorderColor="BlueViolet" onfocus="Focus(this.id,'City')"
                                            onblur="Blur(this.id,'City')" Width="140px" Height="30px" Font-Names="verdana"
                                            Font-Size="12px" CssClass="WaterMarkedTextBox" Text="City"></asp:TextBox>--%>
                                        <%--   <asp:DropDownList ID="txtCity" runat="server" BorderColor="BlueViolet" onfocus="Focus(this.id,'City')"
                                            onblur="Blur(this.id,'City')" Width="140px" Height="30px" Font-Names="verdana"
                                            Font-Size="12px" CssClass="WaterMarkedTextBox"></asp:DropDownList>--%>
                                        <asp:DropDownList runat="server" ID="cboCountry" onblur="Blur(this.id,'City')" onfocus="Focus(this.id,'City')"
                                            class="chzn-select" Style="width: 140px; height: 50px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" class="stylespc">
                                        <asp:TextBox ID="txtReg" runat="server" BorderColor="BlueViolet" onfocus="Focus(this.id,'RegNo')"
                                            onblur="Blur(this.id,'RegNo')" Width="140px" Height="30px" Font-Names="verdana"
                                            Font-Size="12px" CssClass="WaterMarkedTextBox" Text="RegNo"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="stylespc">
                                        <asp:TextBox ID="txtPin" runat="server" BorderColor="BlueViolet" onkeypress="CheckNumeric(event);"
                                            onfocus="Focus(this.id,'PinCode')" onblur="Blur(this.id,'PinCode')" Width="140px"
                                            Height="30px" Font-Names="verdana" Font-Size="12px" CssClass="WaterMarkedTextBox"
                                            Text="PinCode"></asp:TextBox>
                                    </td>
                                    <td align="left" class="stylespc">
                                        <asp:TextBox ID="txtQual" runat="server" BorderColor="BlueViolet" onfocus="Focus(this.id,'Qualification')"
                                            onblur="Blur(this.id,'Qualification')" Width="140px" Height="30px" Font-Names="verdana"
                                            Font-Size="12px" CssClass="WaterMarkedTextBox" Text="Qualification"></asp:TextBox>
                                    </td>
                                    <td align="left" class="stylespc">
                                        <asp:TextBox ID="txtMob" runat="server" BorderColor="BlueViolet" onfocus="Focus(this.id,'Mobile')"
                                            onblur="Blur(this.id,'Mobile')" Width="140px" Height="30px" onkeypress="fnAllowNumeric(event);"
                                            CssClass="WaterMarkedTextBox" Font-Names="verdana" Font-Size="12px" MaxLength="10"
                                            Text="Mobile"></asp:TextBox>
                                    </td>
                                    <td align="left" class="stylespc">
                                        <%--<asp:TextBox ID="txtSt" runat="server" BorderColor="BlueViolet" onfocus="Focus(this.id,'State')"
                                            onblur="Blur(this.id,'State')" Width="140px" Height="30px" CssClass="WaterMarkedTextBox"
                                            Text="State" Font-Names="verdana" Font-Size="12px"></asp:TextBox>--%>
                                        <asp:DropDownList ID="txtSt" runat="server" BorderColor="BlueViolet" onfocus="Focus(this.id,'State')"
                                            onblur="Blur(this.id,'State')" Width="140px" Height="30px" CssClass="WaterMarkedTextBox"
                                            Font-Names="verdana" Font-Size="12px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%-- <tr>
                            <td colspan="5">
                            <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtDoctor" ID="RegularExpressionValidator2" ValidationExpression = "^[\s\S]{8,}$" runat="server" ErrorMessage="Doctor Name Minimum 8 Characters Required."></asp:RegularExpressionValidator>
                            </td>
                            </tr>--%>
                            </table>
                            <br />
                        </div>
                    </div>
                </div>
                <br />
                <asp:Label ID="lblRef" runat="server" Text="Ref No Search"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtRef" runat="server" SkinID="MandTxtBox" onkeypress="CheckNumeric(event);"></asp:TextBox>
                &nbsp;
                <asp:Button ID="btnRef" runat="server" Text="Go" Width="30px" Height="25px" BackColor="LightBlue"
                    OnClick="btnRef_Click" />
            </center>
            <br />
            <center>
                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:HiddenField runat="server" ID="hdnSfCode" />
                                <asp:HiddenField runat="server" ID="hdnMode" />
                                <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center"
                                    EmptyDataText="No Records Found" AutoGenerateColumns="false" GridLines="None"
                                    OnPageIndexChanging="grdDoctor_PageIndexChanging" AllowPaging="true" CssClass="mGridImg"
                                    AlternatingRowStyle-CssClass="alt" AllowSorting="True" PageSize="50">
                                    <PagerStyle CssClass="gridview1"></PagerStyle>
                                    <HeaderStyle Font-Bold="False" />
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcom" runat="server" Text='<%#Eval("C_Doctor_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Common Doctor Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("C_Doctor_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Qualification" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQua" runat="server" Text='<%# Bind("Qual_Short_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Speciality" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Speciality_Short_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Address" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddr" runat="server" Text='<%# Bind("C_Doctor_Hos_Addr") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Mobile" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMob" runat="server" Text='<%# Bind("C_Doctor_Mobile") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="City" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCity" runat="server" Text='<%# Bind("Drs_City") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="PinCode" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPin" runat="server" Text='<%# Bind("Pincode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="HQ" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHq" runat="server" Text='<%# Bind("C_Doctor_HQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--      <asp:HyperLinkField HeaderText="Click" Text="Click here to Add" DataNavigateUrlFormatString="Common_Doctor_Updation_FDC.aspx?type=1&C_Doctor_Code={0}"
                                        DataNavigateUrlFields="C_Doctor_Code" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    </asp:HyperLinkField>--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%--     <a href="#" onclick='openWindow("<%# Eval("C_Doctor_Code") %>");'>Click here to Add</a>--%>
                                                <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='Send to Approval'
                                                    OnClientClick='<%# "return openWindow(\"" + Eval("C_Doctor_Code") + "\",\"" +  Eval("C_Doctor_Code")  + "\");" %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </center>
        </asp:Panel>
        <center>
            <asp:Panel ID="pnlNew" runat="server" Width="100%">
                <center>
                    <span class="blink_me">
                        <asp:Label ID="lblFN" runat="server" Font-Bold="true" Font-Size="16px" ForeColor="Red">Important Note</asp:Label></span>
                    <br />
                    <br />
                    <asp:Label ID="lbl" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="BlueViolet">
                    Visiting Card Upload File Name Should by AlpharNumeric. Don't use Special Characters Like '," \ & +. <br />
                    Visiting Card Size should be less than 1 MB.
                    
                    </asp:Label>
                    <br />
                    <br />
                </center>
                <center>
                    <div class="roundbox boxshadow" style="width: 80%; border: solid 2px steelblue;">
                        <div class="gridheaderleft">
                            Doctor Details
                        </div>
                        <div class="boxcontenttext" style="background: White;">
                            <div id="Div1">
                                <br />
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <table>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblName" SkinID="lblMand" Width="180px" runat="server"><span style="Color:Red">*</span>Listed Doctor Name </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtName" runat="server" Width="220px" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                            onblur="this.style.backgroundColor='White'"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblQual" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Qualification </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlQual" runat="server" SkinID="ddlRequired">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label9" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Category </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlCatg" runat="server" SkinID="ddlRequired">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label14" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Class </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlCls" runat="server" SkinID="ddlRequired">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblAddress" runat="server" SkinID="lblMand">Residence Address </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtAddress1" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'" Width="211px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label5" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Clinic Name</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtHospital" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'" Width="180px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label1" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Clinic Address</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtHosAddress" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label6" runat="server" SkinID="lblMand">Landline No</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtland" runat="server" MaxLength="20" onkeypress="CheckNumeric(event);"
                                                            Width="100px" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblMobile" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Mobile No</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtMobile" onkeypress="CheckNumeric(event);" runat="server" SkinID="MandTxtBox"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'" onkeyup="nospaces(this)"
                                                            onblur="this.style.backgroundColor='White'" Width="140px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label7" runat="server" SkinID="lblMand">Mail ID</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtMail" runat="server" Width="120px" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Area"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="lblArea" runat="server" Width="130px" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'"></asp:TextBox>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblCa" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>City</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="lblCity" runat="server" Width="130px" SkinID="MandTxtBox" onkeypress="CharactersOnly(event);" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                            onblur="this.style.backgroundColor='White'">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label3" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>State Name</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlState" Width="126px" runat="server" SkinID="ddlRequired">
                                                            <asp:ListItem Value="-1">---Select---</asp:ListItem>
                                                            <asp:ListItem Value="0">ArunachalaPradesh</asp:ListItem>
                                                            <asp:ListItem Value="1">AndraPradesh</asp:ListItem>
                                                            <asp:ListItem Value="2">Assam</asp:ListItem>
                                                            <asp:ListItem Value="3">Bihar</asp:ListItem>
                                                            <asp:ListItem Value="4">Chattisgarh</asp:ListItem>
                                                            <asp:ListItem Value="5">Goa</asp:ListItem>
                                                            <asp:ListItem Value="6">Gujarat</asp:ListItem>
                                                            <asp:ListItem Value="7">Haryana</asp:ListItem>
                                                            <asp:ListItem Value="8">Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="9">Jammu and Kashmir</asp:ListItem>
                                                            <asp:ListItem Value="10">Jharkhand</asp:ListItem>
                                                            <asp:ListItem Value="11">Karnataka</asp:ListItem>
                                                            <asp:ListItem Value="12">Kerala</asp:ListItem>
                                                            <asp:ListItem Value="13">Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="14">Maharashtra</asp:ListItem>
                                                            <asp:ListItem Value="15">Manipur</asp:ListItem>
                                                            <asp:ListItem Value="16">Meghalaya</asp:ListItem>
                                                            <asp:ListItem Value="17">Mizoram</asp:ListItem>
                                                            <asp:ListItem Value="18">NagaLand</asp:ListItem>
                                                            <asp:ListItem Value="19">Odisha</asp:ListItem>
                                                            <asp:ListItem Value="20">Punjab</asp:ListItem>
                                                            <asp:ListItem Value="21">Rajasthan</asp:ListItem>
                                                            <asp:ListItem Value="22">Sikkim</asp:ListItem>
                                                            <asp:ListItem Value="23">Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem Value="24">Tripura</asp:ListItem>
                                                            <asp:ListItem Value="25">Uttaranchal</asp:ListItem>
                                                            <asp:ListItem Value="26">Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="27">West Bengal</asp:ListItem>
                                                            <asp:ListItem Value="28">Andaman and Niccobar Islands</asp:ListItem>
                                                            <asp:ListItem Value="29">Chandigarh</asp:ListItem>
                                                            <asp:ListItem Value="30">Dadra and Nagar Haveli</asp:ListItem>
                                                            <asp:ListItem Value="31">Daman and Diu</asp:ListItem>
                                                            <asp:ListItem Value="32">Delhi</asp:ListItem>
                                                            <asp:ListItem Value="33">Pondicherry</asp:ListItem>
                                                            <asp:ListItem Value="34">Mumbai</asp:ListItem>
                                                            <asp:ListItem Value="35">Telangana</asp:ListItem>
                                                            <asp:ListItem Value="36">Srinagar</asp:ListItem>
                                                            <asp:ListItem Value="37">Nepal</asp:ListItem>
                                                            <asp:ListItem Value="38">Buton</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label8" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>PinCode</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtPincode" MaxLength="10" runat="server" onkeypress="CheckNumeric(event);"
                                                            SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                                                            Width="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="Label4" runat="server" SkinID="lblMand">Reg. No</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:TextBox ID="txtRegNo" MaxLength="12" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblSpec" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Speciality </asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlSpec" runat="server" Width="120px" SkinID="ddlRequired">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblH" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Area Cluster Name</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlterr" Width="126px" runat="server" SkinID="ddlRequired">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblDOB" runat="server" SkinID="lblMand" Text="Date of Birth "></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlDobDate" runat="server" SkinID="ddlRequired" Width="50">
                                                            <asp:ListItem Value="01" Text="DD"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                                            <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlDobMonth" runat="server" SkinID="ddlRequired" Width="50">
                                                            <asp:ListItem Value="01" Text="MM"></asp:ListItem>
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
                                                        <asp:DropDownList ID="ddlDobYear" runat="server" SkinID="ddlRequired" Width="60">
                                                            <asp:ListItem Selected="True" Value="1900" Text="YYYY"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left">
                                                        <asp:Label ID="lblDOW" runat="server" SkinID="lblMand" Text="DOW "></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td class="space" align="left">
                                                        <asp:DropDownList ID="ddlDowDate" runat="server" SkinID="ddlRequired" Width="50">
                                                            <asp:ListItem Value="01" Text="DD"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                                            <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlDowMonth" runat="server" SkinID="ddlRequired" Width="50">
                                                            <asp:ListItem Value="01" Text="MM"></asp:ListItem>
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
                                                        <asp:DropDownList ID="ddlDowYear" runat="server" SkinID="ddlRequired" Width="60">
                                                            <asp:ListItem Selected="True" Value="1900" Text="YYYY"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="space" align="left" colspan="3">
                                                        <div class="roundbox boxshadow" style="width: 250px; border: solid 2px steelblue;">
                                                            <div class="gridheaderleft" style="background-color: LightPink; color: Blue">
                                                                Product Map
                                                            </div>
                                                            <div class="boxcontenttext" style="background: White;">
                                                                <div id="Div2">
                                                                    <table>
                                                                        <tr>
                                                                            <td class="space" align="left">
                                                                                <asp:Label ID="Label2" runat="server" SkinID="lblMand">Product 1</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td class="space" align="left">
                                                                                <asp:DropDownList ID="ddlProd1" runat="server" SkinID="ddlRequired">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="space" align="left">
                                                                                <asp:Label ID="Label11" runat="server" SkinID="lblMand">Product 2</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td class="space" align="left">
                                                                                <asp:DropDownList ID="ddlProd2" runat="server" SkinID="ddlRequired">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="space" align="left">
                                                                                <asp:Label ID="Label12" runat="server" SkinID="lblMand">Product 3</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td class="space" align="left">
                                                                                <asp:DropDownList ID="ddlProd3" runat="server" SkinID="ddlRequired">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="space" align="left">
                                                                                <asp:Label ID="Label13" runat="server" SkinID="lblMand">Product 4</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td class="space" align="left">
                                                                                <asp:DropDownList ID="ddlProd4" runat="server" SkinID="ddlRequired">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="space" align="left">
                                                                                <asp:Label ID="Label10" runat="server" SkinID="lblMand">Product 5</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td class="space" align="left">
                                                                                <asp:DropDownList ID="ddlProd5" runat="server" SkinID="ddlRequired">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <%--<td class="space" align="left">
                                                <asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Mode"></asp:Label>
                                            </td>
                                            <td>
                                            :  
                                            </td>
                                            <td class="space" align="left">
                                                <asp:Label ID="lblMode" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="12px" ForeColor="Red">
                                                </asp:Label>
                                            </td>--%>
                                                    <td align="left" class="stylespc">
                                                        <asp:Label ID="Label30" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Visiting Card Upload</asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="left" class="stylespc">
                                                        <asp:FileUpload ID="FilUpImage" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <center>
                                    <div id="dvPreview">
                                    </div>
                                    <asp:Label ID="lblVis" runat="server" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblVisFile" align="left" runat="server">
                                        <asp:Image ID="imgVisFile" runat="server" />
                                    </asp:Label>
                                    <asp:DataList ID="DataList1" runat="server" HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Image ID="imgHome" ImageUrl='<%# Eval("Visiting_Card") %>' Width="200px" ImageAlign="Top"
                                                    runat="server" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </center>
                                <br />
                                <span class="blink_me">
                                    <asp:Label ID="lblsu" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="BlueViolet">
                                      Mobile No is Mandatory. Without Mobile No, Doctor addition is not considered.
                                    </asp:Label>
                                </span>
                                <br />
                                <asp:Button ID="btnAdd" runat="server" Text="Submit" BackColor="LightBlue" Width="70px"
                                    OnClick="btnAdd_Click" />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </center>
            </asp:Panel>
        </center>
        <script src="../../JsFiles/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript">            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </div>
    </form>
</body>
</html>
