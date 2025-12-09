<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetUp.aspx.cs" Inherits="SecondarySales_SetUp" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secondary Sale - Primary Setup</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" language="javascript">
        function showhidetext(e)
        {
            var cntrl_id = e.id;
            var qty = e.checked;
            cntrl_id = cntrl_id.substring(0, cntrl_id.indexOf("chkSub"));
            var txt_sub = cntrl_id + 'txtSub';
            var txtSub1 = cntrl_id + 'txtSub1';
            var txt_cntrl = document.getElementById(txt_sub);
            var txt_cntrl_1 = document.getElementById(txtSub1);
            if (txt_cntrl != null)
            {
                var txt_cntrl_id = txt_cntrl.id;
                if (qty) {
                    txt_cntrl.setAttribute("writeOnly", true);
                }
                else {
                    txt_cntrl.value = '';
                    //txt_cntrl.setAttribute("readOnly", true);
                    txt_cntrl.setAttribute("writeOnly", true);
                }
            }

            if (txt_cntrl_1 != null) {
                var txt_cntrl_id = txt_cntrl_1.id;
                if (qty) {
                    txt_cntrl_1.setAttribute("writeOnly", true);
                }
                else {
                    txt_cntrl_1.value = '';
                    // txt_cntrl_1.setAttribute("readOnly", true);
                    txt_cntrl_1.setAttribute("writeOnly", true);
                }
            }
        }

        function GetCheckedCheckBox() {
            var checkedCheckBox;
            var dataGrid = document.all['GridViewRDR1_Hidden'];
            var rows = dataGrid.rows;
            for (var index = 1; index < rows.length; index++) {
                var checkBox = rows[index].cells[0].childNodes[0];
                if (checkBox.Checked)
                    checkedCheckBox = checkBox;
            }
            return checkedCheckBox;
        }
    </script>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../JScript/jquery-1.4.3.min.js" type="text/javascript"></script>
    <style type="text/css">
        .NewBtn
        {
            background: #25A6E1;
            background: -moz-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -webkit-gradient(linear,left top,left bottom,color-stop(0%,#25A6E1),color-stop(100%,#188BC0));
            background: -webkit-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -o-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -ms-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            filter: progid: DXImageTransform.Microsoft.gradient( startColorstr='#25A6E1',endColorstr='#188BC0',GradientType=0);
            padding: 3px 8px;
            color: #fff;
            font-family: 'Helvetica Neue' ,sans-serif;
            font-size: 14px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border: 1px solid #1A87B9;
            cursor: pointer;
        }
        .NewBtn:hover
        {
            color: Black;
        }
        .panelbtn
        {
            /* float: right;
            margin-right: 100px;*/
        }
    </style>
    <link href="../JScript/Secondary_CSS.css" rel="stylesheet" type="text/css" />
    <link href="../JScript/BootStrap/dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/BootStrap/dist/js/bootstrap.js" type="text/javascript"></script>
    <script src="../JScript/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../JScript/js/codex-fly.js" type="text/javascript"></script>
    <style type="text/css">
        /* item box start */
        .col-lg-4
        {
            width: 24% !important;
        }
        .caption
        {
            height: 130px;
            overflow: hidden;
        }
        .caption h4
        {
            white-space: nowrap;
            font-size: 16px;
        }
        .thumbnail img
        {
            /*  width:30%;*/
            width: 6%;
        }
        .ratings
        {
            padding-right: 10px;
            padding-left: 10px;
            color: #d17581;
        }
        .thumbnail
        {
            padding: 0;
        }
        .thumbnail .caption-full
        {
            padding: 9px;
            color: #333;
        }
        .thumbnail .btn
        {
            margin: 0px 30% 10px 30%;
        }
        
        /* item box end */
        
        /* cart icon */
        /*.cart_anchor
        {
            float: right;
            vertical-align: top;
            background: url('../Images/cart.png') no-repeat center center / 100% auto;
            width: 50px;
            height: 50px;
            margin-bottom: 50px;
        }*/
        
        .cart_anchor
        {
            float: right;
            vertical-align: top;
            margin-right: 50%;
            background: url('../Images/cart.png') no-repeat center center / 100% auto;
            width: 30px;
            height: 30px;
            margin-bottom: 10px;
        }
        .TextWrap
        {
            white-space: nowrap;
        }
    </style>
    <style type="text/css">
        .btnReAct
        {
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
        }
        
        .btnReActivation
        {
            color: #fff;
            background-color: #158263;
            border-color: #158263;
        }
        .btnReActivation:hover
        {
            color: #fff;
            background-color: #2b9a7b;
            border-color: #2b9a7b;
        }
        .btnReActivation:focus, .btnReActivation.focus
        {
            color: #fff;
            background-color: #2b9a7b;
            border-color: #2b9a7b;
        }
        .btnReActivation:active, .btnReActivation.active
        {
            color: #fff;
            background-color: #158263;
            border-color: #158263;
            background-image: none;
        }
    </style>
    <style type="text/css">
        .web_dialog_overlay
        {
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
        .web_dialog
        {
            display: none;
            position: fixed;
            width: 380px;
            min-height: 250px;
            max-height: auto;
            top: 50%;
            left: 50%;
            margin-left: -190px;
            margin-top: -100px;
            background-color: #ffffff;
            border: 2px solid #336699;
            padding: 0px;
            z-index: 102;
            font-family: Verdana;
            font-size: 10pt;
        }
        .web_dialog_title
        {
            border-bottom: solid 2px #336699;
            background-color: #336699;
            padding: 4px;
            color: White;
            font-weight: bold;
        }
        .web_dialog_title a
        {
            color: White;
            text-decoration: none;
        }
        .align_right
        {
            text-align: right;
        }
        
        .Formatrbtn label
        {
            margin-right: 30px;
        }
        
        
        /* hover style just for information */
        label:hover:before
        {
            border: 1px solid #4778d9 !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('select').on('change', function () {
                // alert(this.value);

                var Option = this.value;

                if (Option == "+") {
                    $('.imgCSS').attr('src', '../Images/Tablet2.jpg');
                }
                else if (Option == "-") {
                    $('.imgCSS').attr('src', '../Images/Tablet3.jpg');
                }
                else if (Option == "C") {
                    $('.imgCSS').attr('src', '../Images/Tablet2.jpg');
                }
                else if (Option == "0") {
                    // $('.imgCSS').attr('src', '');

                    var $image = $('.imgCSS');
                    $image.removeAttr('src').replaceWith($image.clone());

                }

            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.add-to-cart').on('click', function () {

                //                //Scroll to top if cart icon is hidden on top
                //                $('html, body').animate({
                //                    'scrollTop': $(".cart_anchor").position().top
                //                });
                //                //Select item image and pass to the function
                //                var itemImg = $(this).parent().find('img').eq(0);
                //                flyToElement($(itemImg), $('.cart_anchor'));


                var Option = $("select option:selected").text();

                if (Option == "+ (Plus)") {

                    $('html, body').animate({
                        'scrollTop': $("#gvAddSecSale").position().top
                    });

                    var itemImg = $(this).parent().find('img').eq(0);
                    flyToElement($(itemImg), $('#gvAddSecSale'));

                }

                else if (Option == "- (Minus)") {

                    //$('html, body').animate({
                    //  'scrollTop': $("#gvMinusSecSale").position().top
                    //});

                    //  var itemImg = $(this).parent().find('img').eq(0);
                    //  flyToElement($(itemImg), $('#gvMinusSecSale'));

                    //.imgCSS  #gvMinusSecSale

                    $('html, body').animate({
                        'scrollDown': $("#gvMinusSecSale").position().down
                    });

                    var itemImg = $(this).parent().find('img').eq(0);
                    flyToElement_2($(itemImg), $('.imgCSS'));

                }

                else if (Option == "C (Closing)") {

                    $('html, body').animate({
                        'scrollTop': $("#gvOtherSecSale").position().top
                    });

                    var itemImg = $(this).parent().find('img').eq(0);
                    flyToElement($(itemImg), $('#gvOtherSecSale'));

                    $('html, body').animate({
                        'scrollDown': $("#gvMinusSecSale").position().down
                    });

                    var itemImg = $(this).parent().find('img').eq(0);
                    flyToElement_2($(itemImg), $('.imgCSS'));

                }

            });
        });
    </script>
    <script type="text/javascript">

        function Parameter_FieldCheck(SecCode, Operator) {

            $.ajax({
                type: "POST",
                url: "SetUp.aspx/GetParameter_Check",
                data: '{objParam:' + JSON.stringify(SecCode) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d.length > 0) {
                        var CalcF_Field = data.d[0].CalcF_Field;
                        var Sec_Operator = data.d[0].Sec_Operator;
                        var Sec_Sale_Code = data.d[0].Sec_Sale_Code;

                        if (Operator == "+") {

                            if ($.trim(Sec_Sale_Code) == $.trim(SecCode)) {

                                $("#ChkParamList_plus input[type=checkbox]").each(function (i) {

                                    var SS_Code = $(this).attr('id');
                                    var StrVal = $(this).parent().attr('hiddenValue');

                                    if (CalcF_Field == StrVal) {

                                        $("#" + SS_Code).prop('checked', true);
                                    }
                                    else {
                                        $("#" + SS_Code).prop('checked', false);
                                    }

                                });
                            }
                        }
                        else if (Operator == "-") {

                            if ($.trim(Sec_Sale_Code) == $.trim(SecCode)) {
                                $("#ChkParamList_minus input[type=checkbox]").each(function (i) {
                                    var SS_Code = $(this).attr('id');
                                    var StrVal = $(this).parent().attr('hiddenValue');

                                    //  alert(StrVal);

                                    if (CalcF_Field == StrVal) {

                                        $("#" + SS_Code).prop('checked', true);
                                    }
                                    else {
                                        $("#" + SS_Code).prop('checked', false);
                                    }

                                });
                            }
                        }
                        else if (Operator == "C") {

                            if ($.trim(Sec_Sale_Code) == $.trim(SecCode)) {

                                $("#ChkParamList_Other input[type=checkbox]").each(function (i) {

                                    var SS_Code = $(this).attr('id');
                                    var StrVal = $(this).parent().attr('hiddenValue');

                                    if (CalcF_Field == StrVal) {

                                        $("#" + SS_Code).prop('checked', true);
                                    }
                                    else {
                                        $("#" + SS_Code).prop('checked', false);
                                    }

                                });
                            }
                        }

                        else if (Operator == "D") {

                            if ($.trim(Sec_Sale_Code) == $.trim(SecCode)) {

                                $("#ChkParamList_formula input[type=checkbox]").each(function (i) {

                                    var SS_Code = $(this).attr('id');
                                    var StrVal = $(this).parent().attr('hiddenValue');

                                    if (CalcF_Field == StrVal) {

                                        $("#" + SS_Code).prop('checked', true);
                                    }
                                    else {
                                        $("#" + SS_Code).prop('checked', false);
                                    }

                                });
                            }
                        }


                    }

                },
                error: function (res) {
                    alert("Error");

                }
            });

        }

        /*--------------------------------------Primary Bill-----------------------------------------------------*/
        function Primary_Bill_Chk(SecCode, Operator) {

            $.ajax({
                type: "POST",
                url: "SetUp.aspx/Get_PrimaryBill",
                data: '{objBill:' + JSON.stringify(SecCode) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d.length > 0) {
                        var CalcF_Field = data.d[0].CalcF_Field;
                        var Sec_Operator = data.d[0].Sec_Operator;
                        var Sec_Sale_Code = data.d[0].Sec_Sale_Code;

                        var Data = [];
                        Data = CalcF_Field.split(',');

                     //  alert(Data.length);
                        //alert(CalcF_Field);

                        if (Operator == "+") {
                            if ($.trim(Sec_Sale_Code) == $.trim(SecCode)) {

                                $("#ChkParamList_Bill input[type=checkbox]").each(function (i) 
                                {
                                    var SS_Code = $(this).attr('id');

                                    var StrVal = $(this).next('label').text();

                                    // alert(StrVal);
                                    // var StrVal = $(this).parent().attr('hiddenValue');

                                  //  for (var i = 0; i < Data.length; i++) {

                                    if (CalcF_Field == StrVal && $.trim(Sec_Sale_Code) == $.trim(SecCode)) {
                                            $("#" + SS_Code).prop('checked', true);
                                        }
                                        else
                                        {
                                            $("#" + SS_Code).prop('checked', false);
                                        }
                                   // }
                                  

                                });
                            }
                        }
                        else if (Operator == "-") {

                            if ($.trim(Sec_Sale_Code) == $.trim(SecCode)) {
                                $("#ChkParamList_Bill input[type=checkbox]").each(function (i) {
                                    var SS_Code = $(this).attr('id');
                                    var StrVal = $(this).next('label').text();

                                    // alert(StrVal);
                                    // var StrVal = $(this).parent().attr('hiddenValue');

                                   // for (var i = 0; i < Data.length; i++) {

                                    if (CalcF_Field == StrVal && $.trim(Sec_Sale_Code) == $.trim(SecCode)) {
                                            $("#" + SS_Code).prop('checked', true);
                                        }
                                        else {
                                            $("#" + SS_Code).prop('checked', false);
                                        }
                                   // }

                                });
                            }
                        }
                        else if (Operator == "C") {

                            if ($.trim(Sec_Sale_Code) == $.trim(SecCode)) {

                                $("#ChkParamList_Bill input[type=checkbox]").each(function (i) {

                                    var SS_Code = $(this).attr('id');
                                    var StrVal = $(this).next('label').text();

                                    // alert(StrVal);
                                    // var StrVal = $(this).parent().attr('hiddenValue');

                                   // for (var i = 0; i < Data.length; i++) {

                                    if (CalcF_Field == StrVal && $.trim(Sec_Sale_Code) == $.trim(SecCode)) {
                                            $("#" + SS_Code).prop('checked', true);
                                        }
                                        else {
                                            $("#" + SS_Code).prop('checked', false);
                                        }
                                  //  }

                                });
                            }
                        }

                        else if (Operator == "D") {

                            if ($.trim(Sec_Sale_Code) == $.trim(SecCode)) {

                                $("#ChkParamList_Bill input[type=checkbox]").each(function (i) {

                                    var SS_Code = $(this).attr('id');
                                    var StrVal = $(this).next('label').text();

                                    // alert(StrVal);
                                    // var StrVal = $(this).parent().attr('hiddenValue');

                                    //for (var i = 0; i < Data.length; i++) {

                                    if (CalcF_Field == StrVal && $.trim(Sec_Sale_Code) == $.trim(SecCode)) {
                                            $("#" + SS_Code).prop('checked', true);
                                        }
                                        else {
                                            $("#" + SS_Code).prop('checked', false);
                                        }
                                   // }

                                });
                            }
                        }

                    }
                    else {
                       // alert("None");

                        $("#ChkParamList_Bill input[type=checkbox]").each(function (i) {

                            var SS_Code = $(this).attr('id');

                            $("#" + SS_Code).prop('checked', false);

                        });

                    }

                },
                error: function (res) {
                    alert("Error");

                }
            });

        }


    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#btn_Field_Bill").click(function ()
            {
                var Col = "";

                var SecSale_Code = $("#hidSecSaleCode_Bill").val();

                $("#ChkParamList_Bill input[type=checkbox]").each(function (i)
                {
                    var SS_Code = $(this).attr('id');
                    var StrVal = $(this).next('label').text();                                      

                    if ($("#" + SS_Code).prop('checked'))
                    {
                        // Col += StrVal + ",";

                        Col = StrVal;
                    }

                });

                $.ajax({
                    type: "POST",
                    url: "SetUp.aspx/BillField_Update",
                    data: '{objPrimary:' + JSON.stringify(Col) + ',objSecSale:' + JSON.stringify(SecSale_Code) + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d != 0)
                        {

                        }
                    },
                    error: function (res) {
                    }
                });

            });            
        });

    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            /*------------------------ Button Click Parameter Plus-------------------------------------  */
            $("#btnReActivate_Plus").click(function (e) {
                ShowDialog_Plus(false);
                e.preventDefault();
            });

            $("#btnClose_Plus").click(function (e) {
                HideDialog_Plus();
                e.preventDefault();
            });

            /*------------------------ Button Click Parameter Minus-------------------------------------  */
            $("#btnReActivate_Minus").click(function (e) {
                ShowDialog_Minus(false);
                e.preventDefault();
            });

            $("#btnClose_Minus").click(function (e) {
                HideDialog_Minus();
                e.preventDefault();
            });

            /*------------------------ Button Click Parameter Close-------------------------------------  */

            $("#btnReActivate_ParamClose").click(function (e) {
                ShowDialog_ParamClose(false);
                e.preventDefault();
            });

            $("#btnClose_ParamClose").click(function (e) {
                HideDialog_ParamClose();
                e.preventDefault();
            });


            /*------------------------ Button Click Parameter User Column-------------------------------------  */
            $("#btnReActive_UserColumn").click(function (e) {
                ShowDialog_Param_UserCol(false);
                e.preventDefault();
            });

            $("#btnClose_UserColumn").click(function (e) {
                HideDialog_Param_UserCol();
                e.preventDefault();
            });


            $("#btnActive_Plus").click(function (e) {
                //                var brand = $("#brands input:radio:checked").val();
                //                $("#output").html("<b>Your favorite mobile brand: </b>" + brand);
                //                HideDialog();
                //                e.preventDefault();
            });



            /*-----------------------------------------------------Field Popup Click (Parameter(+))--------------------------------*/

            $("[id*=grdSecSales] [id*=btnField_Link_Plus]").click(function (e) {


                var SecCode = $(this).closest('tr').find('td:eq(1)').text();

                var Operator = "+";

                $("#hidSecSaleCode_plus").val(SecCode);

                Parameter_FieldCheck(SecCode, Operator);

                ShowDialog_Field(false);
                e.preventDefault();
            });
            $("#btnClose_Field_plus").click(function (e) {
                HideDialog_Field();
                e.preventDefault();
            });


            $("[id*=grdSecSales] [id*=btnPrimaryField]").click(function (e) {


                var SecCode = $(this).closest('tr').find('td:eq(1)').text();

                $("#hidSecSaleCodePrime").val(SecCode);

                ShowDialog_Field_Prime(false);
                e.preventDefault();
            });
            $("#btn_Close_Prime").click(function (e) {
                HideDialog_Field_Prime();
                e.preventDefault();
            });


            $("[id*=grdSecSales] [id*=btnPrimaryBillField]").click(function (e) {

                var SecCode = $(this).closest('tr').find('td:eq(1)').text();

                $("#hidSecSaleCode_Bill").val(SecCode);
               
                var Operator = "+";

                Primary_Bill_Chk(SecCode, Operator);

                ShowDialog_Field_Bill(false);
                e.preventDefault();
            });
            $("#btnClose_Field_Bill").click(function (e) {
                HideDialog_Field_Bill();
                e.preventDefault();
            });



            /*-----------------------------------------------------Field Popup Click (Parameter(-))--------------------------------*/

            $("[id*=grdSecSalesMinus] [id*=btnField_Link_Minus]").click(function (e) {

                var SecCode = $(this).closest('tr').find('td:eq(1)').text();
                $("#hidSecSaleCode_minus").val(SecCode);

                var Operator = "-";
                Parameter_FieldCheck(SecCode, Operator);

                ShowDialog_Field_Minus(false);
                e.preventDefault();
            });
            $("#btnClose_Field_minus").click(function (e) {
                HideDialog_Field_Minus();
                e.preventDefault();
            });


            $("[id*=grdSecSalesMinus] [id*=btnPrimaryField]").click(function (e) {


                var SecCode = $(this).closest('tr').find('td:eq(1)').text();

                $("#hidSecSaleCodePrime").val(SecCode);

                ShowDialog_Field_Prime(false);
                e.preventDefault();
            });
            $("#btn_Close_Prime").click(function (e) {
                HideDialog_Field_Prime();
                e.preventDefault();
            });

            $("[id*=grdSecSalesMinus] [id*=btnPrimaryBillField]").click(function (e) {

                var SecCode = $(this).closest('tr').find('td:eq(1)').text();

                $("#hidSecSaleCode_Bill").val(SecCode);

                var Operator = "-";

                Primary_Bill_Chk(SecCode, Operator);

                ShowDialog_Field_Bill(false);
                e.preventDefault();
            });
            $("#btnClose_Field_Bill").click(function (e) {
                HideDialog_Field_Bill();
                e.preventDefault();
            });



            /*-----------------------------------------------------Field Popup Click (Parameter(C))--------------------------------*/

            $("[id*=grdSecSalesOthers] [id*=btnField_Link_Other]").click(function (e) {

                var SecCode = $(this).closest('tr').find('td:eq(1)').text();
                $("#hidSecSaleCode_Other").val(SecCode);

                var Operator = "C";
                Parameter_FieldCheck(SecCode, Operator);

                ShowDialog_Field_Other(false);
                e.preventDefault();
            });
            $("#btnClose_Field_Other").click(function (e) {
                HideDialog_Field_Other();
                e.preventDefault();
            });


            $("[id*=grdSecSalesOthers] [id*=btnPrimaryField]").click(function (e) {


                var SecCode = $(this).closest('tr').find('td:eq(1)').text();

                $("#hidSecSaleCodePrime").val(SecCode);

                ShowDialog_Field_Prime(false);
                e.preventDefault();
            });
            $("#btn_Close_Prime").click(function (e) {
                HideDialog_Field_Prime();
                e.preventDefault();
            });


            $("[id*=grdSecSalesOthers] [id*=btnPrimaryBillField]").click(function (e) {

                var SecCode = $(this).closest('tr').find('td:eq(1)').text();

                $("#hidSecSaleCode_Bill").val(SecCode);

                var Operator = "C";

                Primary_Bill_Chk(SecCode, Operator);

                ShowDialog_Field_Bill(false);
                e.preventDefault();
            });
            $("#btnClose_Field_Bill").click(function (e) {
                HideDialog_Field_Bill();
                e.preventDefault();
            });


            /*-----------------------------------------------------Field Popup Click (Parameter(Formula))--------------------------------*/

            $("[id*=grdCol] [id*=btnField_Link_Formula]").click(function (e) {

                var SecCode = $(this).closest('tr').find('td:eq(1)').text();
                $("#hidSecSaleCode_formula").val(SecCode);

                var Operator = "D";
                Parameter_FieldCheck(SecCode, Operator);

                ShowDialog_Field_Formula(false);
                e.preventDefault();
            });
            $("#btnClose_Field_formula").click(function (e) {
                HideDialog_Field_Formula();
                e.preventDefault();
            });


            $("[id*=grdCol] [id*=btnPrimaryField]").click(function (e) {

                var SecCode = $(this).closest('tr').find('td:eq(1)').text();

                $("#hidSecSaleCodePrime").val(SecCode);

                ShowDialog_Field_Prime(false);
                e.preventDefault();
            });
            $("#btn_Close_Prime").click(function (e) {
                HideDialog_Field_Prime();
                e.preventDefault();
            });


            $("[id*=grdCol] [id*=btnPrimaryBillField]").click(function (e) {

                var SecCode = $(this).closest('tr').find('td:eq(1)').text();

                $("#hidSecSaleCode_Bill").val(SecCode);

                var Operator = "D";

                Primary_Bill_Chk(SecCode, Operator);

                ShowDialog_Field_Bill(false);
                e.preventDefault();
            });
            $("#btnClose_Field_Bill").click(function (e) {
                HideDialog_Field_Bill();
                e.preventDefault();
            });


        });

        /*------------------------ Function Parameter Plus-------------------------------------  */
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


        /*------------------------ Function Parameter Minus-------------------------------------  */
        function ShowDialog_Minus(modal) {
            $("#overlay_Minus").show();
            $("#dialog_Minus").fadeIn(300);

            if (modal) {
                $("#overlay_Minus").unbind("click");
            }
            else {
                $("#overlay_Minus").click(function (e) {
                    HideDialog_Minus();
                });
            }
        }

        function HideDialog_Minus() {
            $("#overlay_Minus").hide();
            $("#dialog_Minus").fadeOut(300);
        }


        /*------------------------ Function Parameter Close-------------------------------------  */

        function ShowDialog_ParamClose(modal) {
            $("#overlay_ParamClose").show();
            $("#dialog_ParamClose").fadeIn(300);

            if (modal) {
                $("#overlay_ParamClose").unbind("click");
            }
            else {
                $("#overlay_ParamClose").click(function (e) {
                    HideDialog_ParamClose();
                });
            }
        }

        function HideDialog_ParamClose() {
            $("#overlay_ParamClose").hide();
            $("#dialog_ParamClose").fadeOut(300);
        }

        /*------------------------ Function Parameter User Column-------------------------------------  */


        function ShowDialog_Param_UserCol(modal) {
            $("#output_Param_UserColumn").show();
            $("#dialog_Param_UserColumn").fadeIn(300);

            if (modal) 
            {
                $("#output_Param_UserColumn").unbind("click");
            }
            else 
            {
                $("#output_Param_UserColumn").click(function (e) {
                    HideDialog_Param_UserCol();
                });
            }
        }

        function HideDialog_Param_UserCol() {
            $("#output_Param_UserColumn").hide();
            $("#dialog_Param_UserColumn").fadeOut(300);
        }


        /*------------------------ Function Field Parameters-------------------------------------  */

        /*----------------------------(Plus)---------------------------------*/
        function ShowDialog_Field(modal) {
            $("#overlay_Field_plus").show();
            $("#dialog_Field_plus").fadeIn(300);

            if (modal) {
                $("#overlay_Field_plus").unbind("click");
            }
            else {
                $("#overlay_Field_plus").click(function (e) {
                    HideDialog_Field();
                });
            }
        }

        function HideDialog_Field() {
            $("#overlay_Field_plus").hide();
            $("#dialog_Field_plus").fadeOut(300);
        }

        /*--------------------------------(Minus)---------------------------------------*/
        function ShowDialog_Field_Minus(modal) {
            $("#overlay_Field_minus").show();
            $("#dialog_Field_minus").fadeIn(300);

            if (modal) {
                $("#overlay_Field_minus").unbind("click");
            }
            else {
                $("#overlay_Field_minus").click(function (e) {
                    HideDialog_Field_minus();
                });
            }
        }

        function HideDialog_Field_Minus() {
            $("#overlay_Field_minus").hide();
            $("#dialog_Field_minus").fadeOut(300);
        }

        /*--------------------------------(Other)---------------------------------------*/
        function ShowDialog_Field_Other(modal) {
            $("#overlay_Field_Other").show();
            $("#dialog_Field_Other").fadeIn(300);

            if (modal) {
                $("#overlay_Field_Other").unbind("click");
            }
            else {
                $("#overlay_Field_Other").click(function (e) {
                    HideDialog_Field_Other();
                });
            }
        }

        function HideDialog_Field_Other() {
            $("#overlay_Field_Other").hide();
            $("#dialog_Field_Other").fadeOut(300);
        }


        /*--------------------------------(Formula)---------------------------------------*/
        function ShowDialog_Field_Formula(modal)
        {
            $("#overlay_Field_formula").show();
            $("#dialog_Field_formula").fadeIn(300);

            if (modal)
            {
                $("#overlay_Field_formula").unbind("click");
            }
            else
            {
                $("#overlay_Field_formula").click(function (e) {
                    HideDialog_Field_Formula();
                });
            }
        }

        function HideDialog_Field_Formula() {
            $("#overlay_Field_formula").hide();
            $("#dialog_Field_formula").fadeOut(300);
        }


        /*--------------------------------------------------------------------------------------*/

        function ShowDialog_Field_Prime(modal) {
            $("#overlay_Field_Prime").show();
            $("#dialog_Field_Prime").fadeIn(300);

            if (modal) {
                $("#overlay_Field_Prime").unbind("click");
            }
            else {
                $("#overlay_Field_Prime").click(function (e) {
                    HideDialog_Field_Prime();
                });
            }
        }

        function HideDialog_Field_Prime() {
            $("#overlay_Field_Prime").hide();
            $("#dialog_Field_Prime").fadeOut(300);
        }
        /*--------------------------Primary Bill Field ---------------------------------------------*/

        function ShowDialog_Field_Bill(modal) {
            $("#overlay_Field_Bill").show();
            $("#dialog_Field_Bill").fadeIn(300);

            if (modal) {
                $("#overlay_Field_Bill").unbind("click");
            }
            else {
                $("#overlay_Field_Bill").click(function (e) {
                    HideDialog_Field_Bill();
                });
            }
        }

        function HideDialog_Field_Bill() {
            $("#overlay_Field_Bill").hide();
            $("#dialog_Field_Bill").fadeOut(300);
        }


    </script>
    <%--<script src="../JScript/MultiChkbox/jquery.sumoselect.min.js" type="text/javascript"></script>
    <link href="../JScript/MultiChkbox/sumoselect.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
       var j = jQuery.noConflict();
        j(document).ready(function () 
        {
            j(<%=lstBoxTest.ClientID%>).SumoSelect();

        });
    </script>--%>
    <style type="text/css">
        .DDLBox
        {
            width: 200px;
            padding: 5px 8px;
            font-style: italic;
        }
        
        .ParamVisible
        {
            display: none;
        }

        body {
            overflow: auto !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0px;">
        <script type="text/javascript">
            $(document).ready(function () {
                $('#btnAdd_Param').click(function () {

                    if ($("#txtParamName").val() == "") {
                        createCustomAlert("Please Enter Parameter Name.");
                        $('#txtParamName').focus();
                        return false;
                    }

                    if ($("#txtShortName").val() == "") {
                        createCustomAlert("Please Enter Short Name.");
                        $('#txtShortName').focus();
                        return false;
                    }

                    var cat = $('#<%=ddlType.ClientID%> :selected').text();
                    if (cat == "---Select---") {
                        createCustomAlert("Please Select Type.");
                        $('#ddlDesType').focus();
                        return false;
                    }
                });

                $('#form1').find('link[href$="Calender_CheckBox.css"]').remove();
            });
            
        </script>
        <ucl:Menu ID="menu1" runat="server" />
    </div>
    <div>
       <%--<asp:Label ID="lbltest" runat="server"></asp:Label>--%>
        <center>
            <br />
            <table width="30%" align="center">
                <tbody>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblParamName" runat="server" SkinID="lblMand" Height="18px" Width="100px">
                            <span style="color:Red">*</span>Parameter Name</asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:TextBox ID="txtParamName" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                                onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                                MaxLength="120" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblShortName" runat="server" SkinID="lblMand" Height="19px" Width="100px">
                            <span style="color:Red">*</span>Short Name</asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:TextBox ID="txtShortName" SkinID="MandTxtBox" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                                onblur="this.style.backgroundColor='White'" runat="server" MaxLength="12" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblOpr" runat="server" SkinID="lblMand" Height="19px" Width="100px">
                            <span style="color:Red">*</span>Type</asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlType" runat="server" SkinID="ddlRequired">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <asp:ListItem Value="+" Text="+ (Plus)"></asp:ListItem>
                                <asp:ListItem Value="-" Text="- (Minus)"></asp:ListItem>
                                <asp:ListItem Value="C" Text="C (Closing)"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <div class="thumbnail">
                <img class="imgCSS" src="" alt="" />
                <asp:LinkButton ID="btnAdd_Param" runat="server" Text="Add to Param" OnClientClick="javascript:void(0);"
                    CssClass="btn btn-success add-to-cart" OnClick="btnAdd_Param_Click"></asp:LinkButton>
            </div>
            <br />
            <div style="width: 100%; height: 60px;">
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                    <center>
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <asp:Label ID="lblPlus" runat="server" Text="Parameter Details (+)" ForeColor="#c41cc5"
                                            Font-Size="Small" Font-Bold="true" Font-Underline="true"></asp:Label>
                                    </div>
                                    <div>
                                        <a class="cart_anchor" id="gvAddSecSale" runat="server"></a>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </center>
                </div>
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                    <div style="float: right; margin-top: 30px">
                        <div>
                            <asp:Button ID="btnReActivate_Plus" runat="server" Text="ReActivation (+)" CssClass="btnReAct btnReActivation" />
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
                                    <td class="web_dialog_title">
                                        ReActivation Parameters(+)
                                    </td>
                                    <td class="web_dialog_title align_right">
                                        <a href="#" id="btnClose_Plus">Close</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <b style="font-family: @Gulim; font-size: 14px; color: Maroon; font-weight: bold">Parameters
                                            (+) </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_Param_Plus" Style="margin-left: 35%; color: red" runat="server"
                                            Text="Parameters Not Found" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 15px;">
                                        <div>
                                            <asp:CheckBoxList ID="ChkParam_Plus" runat="server" Style="margin-left: 6px;" CssClass="Formatrbtn"
                                                RepeatDirection="Vertical" RepeatColumns="2">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:Button ID="btnActive_Plus" runat="server" Text="Activate" OnClick="btnActive_Plus_Click"
                                            CssClass="btn btnReActivation" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <%--<div runat="server" style="width: 900px; overflow: auto; height: auto;">--%>
                            <asp:GridView ID="grdSecSales" runat="server" Width="100%" HorizontalAlign="Center"
                                GridLines="None" OnRowDataBound="grdSecSales_RowDataBound" AutoGenerateColumns="false"
                                CssClass="mGridImg_1 TextWrap" AlternatingRowStyle-CssClass="alt" OnRowUpdating="grdSecSales_RowUpdating"
                                OnRowEditing="grdSecSales_RowEditing" OnRowCancelingEdit="grdSecSales_RowCancelingEdit"
                                OnRowCommand="grdSecSales_RowCommand">
                                <HeaderStyle Font-Bold="False" BackColor="#1a87b9" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Code" HeaderStyle-CssClass="ParamVisible" ItemStyle-CssClass="ParamVisible">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleCode" runat="server" Text='<%#Eval("Sec_Sale_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parameter Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSaleName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="50"
                                                Text='<%# Bind("Sec_Sale_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleName" runat="server" Text='<%# Bind("Sec_Sale_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtShortName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="50"
                                                Text='<%# Bind("Sec_Sale_Short_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Sec_Sale_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Default Field Value Select" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <a href="#" id="btnPrimaryField">Click</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Display Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDisplay" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkValue" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Free Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkFreeQty" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Carry Forward Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCarryFwd" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disable Mode" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDisable" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculation Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalc" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculation with Disable" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalcDis" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculated as Sale" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalcSale" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Carry Forward Field" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCarryFld" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Forward Field Select" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="#" id="btnField_Link_Plus">Click</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Primary Bill" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <a href="#" id="btnPrimaryBillField">Click</a>
                                            <asp:HiddenField ID="hdnPrimary" runat="server" Value='<%#Eval("Sec_Sale_Code")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Needed" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSub" runat="server" onclick="showhidetext(this);" />
                                            <asp:TextBox ID="txtSub" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                            <asp:TextBox ID="txtSub1" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Order By" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOrder" runat="server" Width="40" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-ForeColor="white"
                                        HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                            Font-Bold="True"></ItemStyle>
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandName="Deactivate" CommandArgument='<%# Eval("Sec_Sale_Code") %>'
                                                OnClientClick="return confirm('Do you want to Deactivate the  Category');">Deactivate
                                            </asp:LinkButton>
                                            <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                                <img src="../../Images/deact1.png" alt="" width="55px" title="This Category Exists in Category" />
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <%-- </div>--%>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <div style="width: 100%; height: 60px;">
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                    <center>
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <asp:Label ID="lblMinus" runat="server" Text="Parameter Details (-)" ForeColor="#d81b1b"
                                            Font-Size="Small" Font-Bold="true" Font-Underline="true"></asp:Label>
                                    </div>
                                    <div>
                                        <a class="cart_anchor" id="gvMinusSecSale" runat="server"></a>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </center>
                </div>
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                    <div style="float: right; margin-top: 30px">
                        <div>
                            <asp:Button ID="btnReActivate_Minus" runat="server" Text="ReActivation (-)" CssClass="btnReAct btnReActivation" />
                        </div>
                        <br />
                        <br />
                        <div id="output_Minus">
                        </div>
                        <div id="overlay_Minus" class="web_dialog_overlay">
                        </div>
                        <div id="dialog_Minus" class="web_dialog">
                            <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                                <tr>
                                    <td class="web_dialog_title">
                                        ReActivation Parameters(-)
                                    </td>
                                    <td class="web_dialog_title align_right">
                                        <a href="#" id="btnClose_Minus">Close</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <b style="font-family: @Gulim; font-size: 14px; color: Maroon; font-weight: bold">Parameters
                                            (-) </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_Param_Minus" Style="margin-left: 35%; color: red" runat="server"
                                            Text="Parameters Not Found" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 15px;">
                                        <div>
                                            <asp:CheckBoxList ID="ChkParam_Minus" runat="server" Style="margin-left: 6px;" CssClass="Formatrbtn"
                                                RepeatDirection="Vertical" RepeatColumns="2">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:Button ID="btnActive_Minus" runat="server" Text="Activate" OnClick="btnActive_Minus_Click"
                                            CssClass="btn btnReActivation" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <%--<div id="Div1" runat="server" style="width: 900px; overflow: auto; height: auto;">--%>
                            <asp:GridView ID="grdSecSalesMinus" runat="server" Width="100%" HorizontalAlign="Center"
                                GridLines="None" OnRowDataBound="grdSecSalesMinus_RowDataBound" AutoGenerateColumns="false"
                                CssClass="mGridImg_1 TextWrap" AlternatingRowStyle-CssClass="alt" OnRowUpdating="grdSecSalesMinus_RowUpdating"
                                OnRowEditing="grdSecSalesMinus_RowEditing" OnRowCommand="grdSecSalesMinus_RowCommand"
                                OnRowCancelingEdit="grdSecSalesMinus_RowCancelingEdit">
                                <HeaderStyle Font-Bold="False" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Code" HeaderStyle-CssClass="ParamVisible" ItemStyle-CssClass="ParamVisible">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleCode" runat="server" Text='<%#Eval("Sec_Sale_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parameter Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSaleName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="50"
                                                Text='<%# Bind("Sec_Sale_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleName" runat="server" Text='<%# Bind("Sec_Sale_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtShortName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="50"
                                                Text='<%# Bind("Sec_Sale_Short_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Sec_Sale_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Default Field Value Select" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <a href="#" id="btnPrimaryField">Click</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Display Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDisplay" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkValue" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Free Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkFreeQty" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Carry Forward Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCarryFwd" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disable Mode" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDisable" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculation Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalc" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculation with Disable" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalcDis" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculated as Sale" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalcSale" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Carry Forward Field" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCarryFld" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Forward Field Select" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="#" id="btnField_Link_Minus">Click</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Primary Bill" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <a href="#" id="btnPrimaryBillField">Click</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Needed" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSub" runat="server" onclick="showhidetext(this);" />
                                            <asp:TextBox ID="txtSub" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                            <asp:TextBox ID="txtSub1" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order By" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOrder" runat="server" Width="40" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-ForeColor="white"
                                        HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                            Font-Bold="True"></ItemStyle>
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Sec_Sale_Code") %>'
                                                CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the  Category');">Deactivate
                                            </asp:LinkButton>
                                            <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../../Images/deact1.png" alt="" width="55px" title="This Category Exists in Category" />
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <%--</div>--%>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <div style="width: 100%; height: 60px;">
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                    <center>
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <asp:Label ID="LblOth" runat="server" Text="Parameter Details (Others)" ForeColor="#04793f"
                                            Font-Size="Small" Font-Bold="true" Font-Underline="true"></asp:Label>
                                    </div>
                                    <div>
                                        <a class="cart_anchor" id="gvOtherSecSale" runat="server"></a>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </center>
                </div>
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                    <div style="float: right; margin-top: 30px">
                        <div>
                            <asp:Button ID="btnReActivate_ParamClose" runat="server" Text="ReActivation (Others)"
                                CssClass="btnReAct btnReActivation" />
                        </div>
                        <br />
                        <br />
                        <div id="output_ParamClose">
                        </div>
                        <div id="overlay_ParamClose" class="web_dialog_overlay">
                        </div>
                        <div id="dialog_ParamClose" class="web_dialog">
                            <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                                <tr>
                                    <td class="web_dialog_title">
                                        ReActivation Parameters(Others)
                                    </td>
                                    <td class="web_dialog_title align_right">
                                        <a href="#" id="btnClose_ParamClose">Close</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <b style="font-family: @Gulim; font-size: 14px; color: Maroon; font-weight: bold">Parameters
                                            (Others) </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_Param_Close" Style="margin-left: 35%; color: red" runat="server"
                                            Text="Parameters Not Found" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 15px;">
                                        <div>
                                            <asp:CheckBoxList ID="ChkParam_ParamClose" runat="server" Style="margin-left: 6px;"
                                                CssClass="Formatrbtn" RepeatDirection="Vertical" RepeatColumns="2">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:Button ID="btnActive_ParamClose" runat="server" Text="Activate" OnClick="btnActive_Close_Click"
                                            CssClass="btn btnReActivation" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <%--<div id="Div2" runat="server" style="width: 900px; overflow: auto; height: auto;">--%>
                            <asp:GridView ID="grdSecSalesOthers" runat="server" Width="100%" HorizontalAlign="Center"
                                GridLines="None" OnRowDataBound="grdSecSalesOthers_RowDataBound" AutoGenerateColumns="false"
                                CssClass="mGridImg_1 TextWrap" AlternatingRowStyle-CssClass="alt" OnRowUpdating="grdSecSalesOthers_RowUpdating"
                                OnRowEditing="grdSecSalesOthers_RowEditing" OnRowCommand="grdSecSalesOthers_RowCommand"
                                OnRowCancelingEdit="grdSecSalesOthers_RowCancelingEdit">
                                <HeaderStyle Font-Bold="False" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Code" HeaderStyle-CssClass="ParamVisible" ItemStyle-CssClass="ParamVisible">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleCode" runat="server" Text='<%#Eval("Sec_Sale_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parameter Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSaleName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="50"
                                                Text='<%# Bind("Sec_Sale_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleName" runat="server" Text='<%# Bind("Sec_Sale_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtShortName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="50"
                                                Text='<%# Bind("Sec_Sale_Short_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Sec_Sale_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Default Field Value Select" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <a href="#" id="btnPrimaryField">Click</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Display Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDisplay" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkValue" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Free Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkFreeQty" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Carry Forward Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCarryFwd" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disable Mode" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDisable" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculation Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalc" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculation with Disable" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalcDis" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculated as Sale" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalcSale" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Carry Forward Field" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCarryFld" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Forward Field Select" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="#" id="btnField_Link_Other">Click</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Primary Bill" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <a href="#" id="btnPrimaryBillField">Click</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Needed" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSub" runat="server" onclick="showhidetext(this);" />
                                            <asp:TextBox ID="txtSub" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                            <asp:TextBox ID="txtSub1" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order By" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOrder" runat="server" Width="40" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-ForeColor="white"
                                        HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                            Font-Bold="True"></ItemStyle>
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Sec_Sale_Code") %>'
                                                CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the  Category');">Deactivate
                                            </asp:LinkButton>
                                            <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../../Images/deact1.png" alt="" width="55px" title="This Category Exists in Category" />
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <%--</div>--%>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <div style="width: 100%; height: 60px;">
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                    <center>
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <asp:Label ID="lblCol" runat="server" Text="Parameter Details (User Defined Columns)"
                                            ForeColor="Navy" Font-Size="Small" Font-Bold="true" Font-Underline="true"></asp:Label>
                                    </div>
                                    <div>
                                        <%--<a class="cart_anchor" id="A1"></a>--%>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </center>
                </div>
                <div style="width: 20%; height: 60px; float: left">
                </div>
                <div style="width: 20%; height: 60px; float: left">
                    <div style="float: right; margin-top: 30px">
                        <div>
                            <asp:Button ID="btnReActive_UserColumn" runat="server" Text="ReActivation (D)" CssClass="btnReAct btnReActivation" />
                        </div>
                        <br />
                        <br />
                        <div id="output_Param_UserColumn">
                        </div>
                        <div id="overlay_Param_UserColumn" class="web_dialog_overlay">
                        </div>
                        <div id="dialog_Param_UserColumn" class="web_dialog">
                            <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                                <tr>
                                    <td class="web_dialog_title">
                                        ReActivation Parameters(User Defined Columns)
                                    </td>
                                    <td class="web_dialog_title align_right">
                                        <a href="#" id="btnClose_UserColumn">Close</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <b style="font-family: @Gulim; font-size: 14px; color: Maroon; font-weight: bold">Parameters
                                            (D) </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_Param_UserColumn" Style="margin-left: 35%; color: red" runat="server"
                                            Text="Parameters Not Found" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 15px;">
                                        <div>
                                            <asp:CheckBoxList ID="ChkParam_UserColumn" runat="server" Style="margin-left: 6px;"
                                                CssClass="Formatrbtn" RepeatDirection="Vertical" RepeatColumns="2">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:Button ID="btnActive_UserColumn" runat="server" Text="Activate" OnClick="btnActive_User_Column"
                                            CssClass="btn btnReActivation" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdCol" runat="server" Width="100%" HorizontalAlign="Center" GridLines="None"
                                OnRowDataBound="grdCol_RowDataBound" AutoGenerateColumns="false" CssClass="mGridImg_1 TextWrap"
                                AlternatingRowStyle-CssClass="alt" OnRowUpdating="grdCol_RowUpdating" OnRowEditing="grdCol_RowEditing"
                                OnRowCancelingEdit="grdCol_RowCancelingEdit" OnRowCommand="grdCol_RowCommand">
                                <HeaderStyle Font-Bold="False" />                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Code" HeaderStyle-CssClass="ParamVisible" ItemStyle-CssClass="ParamVisible">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleCode" runat="server" Text='<%#Eval("Sec_Sale_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Param Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSaleName" SkinID="TxtBxAllowSymb" Width="160px" runat="server"
                                                MaxLength="50" Text='<%# Bind("Sec_Sale_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleName" runat="server" Text='<%# Bind("Sec_Sale_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtShortName" SkinID="TxtBxAllowSymb" Width="160px" runat="server"
                                                MaxLength="50" Text='<%# Bind("Sec_Sale_Short_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Sec_Sale_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Default Field Value Select" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <a href="#" id="btnPrimaryField">Click</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Display Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDisplay" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkValue" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Free Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkFreeQty" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Carry Forward Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCarryFwd" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disable Mode" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDisable" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculation Needed" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalc" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculation with Disable" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalcDis" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculated as Sale" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCalcSale" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Carry Forward Field" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCarryFld" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Forward Field Select" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="#" id="btnField_Link_Formula">Click</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Primary Bill" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <a href="#" id="btnPrimaryBillField">Click</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Needed" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSub" runat="server" onclick="showhidetext(this);" />
                                            <asp:TextBox ID="txtSub" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                            <asp:TextBox ID="txtSub1" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order By" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOrder" runat="server" Width="40" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-ForeColor="white"
                                        HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                            Font-Bold="True"></ItemStyle>
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Sec_Sale_Code") %>'
                                                CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the  Category');">Deactivate
                                            </asp:LinkButton>
                                            <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../../Images/deact1.png" alt="" width="55px" title="This Category Exists in Category" />
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Save"
                CssClass="savebutton" OnClick="btnSubmit_Click" />
            &nbsp;
            <asp:Button ID="btnClear" runat="server" CssClass="savebutton" Width="60px" Height="25px"
                Text="Clear" OnClick="btnClear_Click" />
        </center>
    </div>
    <%-- Plus Field--%>
    <div>
        <div style="float: right; margin-top: 30px">
            <div id="output_Field_plus">
            </div>
            <div id="overlay_Field_plus" class="web_dialog_overlay">
            </div>
            <div id="dialog_Field_plus" class="web_dialog">
                <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="web_dialog_title">
                            Field Parameters
                        </td>
                        <td class="web_dialog_title align_right">
                            <a href="#" id="btnClose_Field_plus">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <b style="font-family: @Gulim; font-size: 14px; color: Maroon; font-weight: bold">Calculated
                                Field </b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="hidden" runat="server" id="hidSecSaleCode_plus" />
                        </td>
                    </tr>
                    <%-- <tr>
                            <td>
                                <asp:Label ID="lblParam_Field" Style="margin-left: 35%; color: red" runat="server" Text="Parameters Not Found"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>--%>
                    <tr>
                        <td colspan="2" style="padding-left: 15px;">
                            <div>
                                <asp:CheckBoxList ID="ChkParamList_plus" runat="server" Style="margin-left: 6px;"
                                    CssClass="Formatrbtn" RepeatDirection="Vertical" RepeatColumns="2">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btn_Field_plus" runat="server" Text="Save" OnClick="btnFieldAdd_plus_Click"
                                CssClass="btn btnReActivation" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <%-- Minus Field--%>
    <div>
        <div style="float: right; margin-top: 30px">
            <div id="output_Field_minus">
            </div>
            <div id="overlay_Field_minus" class="web_dialog_overlay">
            </div>
            <div id="dialog_Field_minus" class="web_dialog">
                <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="web_dialog_title">
                            Field Parameters
                        </td>
                        <td class="web_dialog_title align_right">
                            <a href="#" id="btnClose_Field_minus">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <b style="font-family: @Gulim; font-size: 14px; color: Maroon; font-weight: bold">Calculated
                                Field </b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="hidden" runat="server" id="hidSecSaleCode_minus" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 15px;">
                            <div>
                                <asp:CheckBoxList ID="ChkParamList_minus" runat="server" Style="margin-left: 6px;"
                                    CssClass="Formatrbtn" RepeatDirection="Vertical" RepeatColumns="2">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btn_Field_minus" runat="server" Text="Save" OnClick="btnFieldAdd_minus_Click"
                                CssClass="btn btnReActivation" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <%-- Other Field--%>
    <div>
        <div style="float: right; margin-top: 30px">
            <div id="output_Field_Other">
            </div>
            <div id="overlay_Field_Other" class="web_dialog_overlay">
            </div>
            <div id="dialog_Field_Other" class="web_dialog">
                <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="web_dialog_title">
                            Field Parameters
                        </td>
                        <td class="web_dialog_title align_right">
                            <a href="#" id="btnClose_Field_Other">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <b style="font-family: @Gulim; font-size: 14px; color: Maroon; font-weight: bold">Calculated
                                Field </b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="hidden" runat="server" id="hidSecSaleCode_Other" />
                        </td>
                    </tr>
                    <%-- <tr>
                            <td>
                                <asp:Label ID="lblParam_Field" Style="margin-left: 35%; color: red" runat="server" Text="Parameters Not Found"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>--%>
                    <tr>
                        <td colspan="2" style="padding-left: 15px;">
                            <div>
                                <asp:CheckBoxList ID="ChkParamList_Other" runat="server" Style="margin-left: 6px;"
                                    CssClass="Formatrbtn" RepeatDirection="Vertical" RepeatColumns="2">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btn_Field_Other" runat="server" Text="Save" OnClick="btnFieldAdd_Other_Click"
                                CssClass="btn btnReActivation" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <%-- Formula Field--%>
    <div>
        <div style="float: right; margin-top: 30px">
            <div id="output_Field_formula">
            </div>
            <div id="overlay_Field_formula" class="web_dialog_overlay">
            </div>
            <div id="dialog_Field_formula" class="web_dialog">
                <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="web_dialog_title">
                            Field Parameters
                        </td>
                        <td class="web_dialog_title align_right">
                            <a href="#" id="btnClose_Field_formula">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <b style="font-family: @Gulim; font-size: 14px; color: Maroon; font-weight: bold">Calculated
                                Field </b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="hidden" runat="server" id="hidSecSaleCode_formula" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 15px;">
                            <div>
                                <asp:CheckBoxList ID="ChkParamList_formula" runat="server" Style="margin-left: 6px;"
                                    CssClass="Formatrbtn" RepeatDirection="Vertical" RepeatColumns="2">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btn_Field_formula" runat="server" Text="Save" OnClick="btnFieldAdd_formula_Click"
                                CssClass="btn btnReActivation" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <%-- End--%>
    <div>
        <div style="float: right; margin-top: 30px">
            <div id="output_Field_Prime">
            </div>
            <div id="overlay_Field_Prime" class="web_dialog_overlay">
            </div>
            <div id="dialog_Field_Prime" class="web_dialog">
                <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="web_dialog_title">
                            Primary Field
                        </td>
                        <td class="web_dialog_title align_right">
                            <a href="#" id="btn_Close_Prime">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <b style="font-family: @Gulim; font-size: 14px; color: Maroon; font-weight: bold">Primary
                                Field </b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="hidden" runat="server" id="hidSecSaleCodePrime" />
                        </td>
                    </tr>
                    <%-- <tr>
                            <td>
                                <asp:Label ID="lblParam_Field" Style="margin-left: 35%; color: red" runat="server" Text="Parameters Not Found"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>--%>
                    <tr>
                        <td colspan="2" style="padding-left: 15px;">
                            <div>
                                <asp:RadioButtonList ID="ChkPrime" runat="server" Style="margin-left: 6px;" CssClass="Formatrbtn"
                                    RepeatDirection="Vertical" RepeatColumns="2">
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnPrime" runat="server" Text="Save" OnClick="btnPrimeAdd_Click"
                                CssClass="btn btnReActivation" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <%-- End PrimaryField  --%>

    <%-- Billwise Field  --%>   

    <div>
        <div style="float: right; margin-top: 30px">
            <div id="output_Field_Bill">
            </div>
            <div id="overlay_Field_Bill" class="web_dialog_overlay">
            </div>
            <div id="dialog_Field_Bill" class="web_dialog">
                <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="web_dialog_title">
                            Field Parameters
                        </td>
                        <td class="web_dialog_title align_right">
                            <a href="#" id="btnClose_Field_Bill">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <b style="font-family: @Gulim; font-size: 14px; color: Maroon; font-weight: bold">Primary
                                Field </b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="hidden" runat="server" id="hidSecSaleCode_Bill" />
                        </td>
                    </tr>
                    <%-- <tr>
                            <td>
                                <asp:Label ID="lblParam_Field" Style="margin-left: 35%; color: red" runat="server" Text="Parameters Not Found"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>--%>
                    <tr>
                        <td colspan="2" style="padding-left: 15px;">
                            <div>
                                <asp:CheckBoxList ID="ChkParamList_Bill" runat="server" Style="margin-left: 6px;"
                                    CssClass="Formatrbtn" RepeatDirection="Vertical" RepeatColumns="2">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btn_Field_Bill" runat="server" Text="Save" 
                                CssClass="btn btnReActivation" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>


   <%-- End  --%>
    </form>
</body>
</html>
