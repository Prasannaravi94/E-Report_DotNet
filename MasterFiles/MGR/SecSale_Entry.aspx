<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSale_Entry.aspx.cs" Inherits="MasterFiles_MGR_SecSales_SecSale_Entry" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secondary Sales Entry</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/MR.css" />
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <link href="../../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <link href="../../../JScript/Process_CSS.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .divrpt
        {
            margin: 20px;
            font-family: Verdana;
            color: Maroon;
            font-size: 20px;
        }
        .rpt
        {
            border: 1px solid #A6A6D2;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            -khtml-border-radius: 8px;
            color: #071019;
            padding: 0px;
        }
        .rpttr
        {
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            -khtml-border-radius: 8px;
            background-color: #F0F8FF;
            padding: 0px;
        }
        .rptSpan
        {
            color: Black;
        }
        .rptmar
        {
            margin: 8px;
            color: Maroon;
            font-family: Verdana;
        }
        .rpta
        {
            color: Maroon;
            width: 200px;
            height: auto;
            text-decoration: underline;
        }
        .rpta:hover
        {
            color: #b70b6e;
            text-decoration: underline;
        }
        .rpttdWidth
        {
            width: 100px;
        }
        .rptTr
        {
            border-bottom: dashed 1px maroon;
            background-color: #F5FAEA;
        }
    </style>
    <style type="text/css">
        .div_fixed
        {
            position: fixed;
            top: 400px;
            right: 5px;
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
            margin-top: 25px;
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
        
        
        #btnClose_Plus:focus
        {
            outline-offset: -2px;
        }
        #btnClose_Plus:hover, #btnClose_Plus:focus
        {
            color: #fff;
            text-decoration: underline;
        }
        #btnClose_Plus:hover, #btnClose_Plus:focus
        {
            color: #fff;
            text-decoration: underline;
        }
        #btnClose_Plus:active, #btnClose_Plus:hover
        {
            outline: 0px none currentColor;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" language="javascript">

        function disable_control() {
            //alert('ok');

            var hid_cl_opr = document.getElementById("<%=hidClBal.ClientID%>");
            var hid_prod_cnt = document.getElementById("<%=hidprdcnt.ClientID%>");

            var hid_plus_opr = document.getElementById("<%=hidPlus.ClientID%>");
            var hid_cust_opr = document.getElementById("<%=hidCust.ClientID%>");
            var hid_cust1_opr = document.getElementById("<%=hidCust1.ClientID%>");

            //rptProduct$ctl00$rptDetSecSale$ctl00$txtSecSale
            var control_id;
            var cls_control_id;

            var plus_control_id;
            var pls_control_id;

            var cust_sub_control_id;
            var col_sub_control_id;

            var cust_control_id;
            var col_control_id;

            var prod_cnt = hid_prod_cnt.value
            var ind;


            if (hid_cl_opr.value != '') {
                for (ind = 0; ind < parseInt(prod_cnt); ind++) {
                    if (parseInt(ind) > 9) {
                        control_id = "rptProduct_ctl" + ind + "_rptDetSecSale_ctl0" + hid_cl_opr.value + "_txtSecSale";
                    }
                    else {
                        control_id = "rptProduct_ctl0" + ind + "_rptDetSecSale_ctl0" + hid_cl_opr.value + "_txtSecSale";
                    }

                    cls_control_id = document.getElementById(control_id);
                    //cls_control_id.disabled = true;
                    //cls_control_id.readOnly = true; 
                    cls_control_id.setAttribute("readOnly", true);
                    //cls_control_id.setAttribute("BackColor", "Red");
                    cls_control_id.style.backgroundColor = "LightGreen";

                    if (parseInt(ind) > 9) {
                        plus_control_id = "rptProduct_ctl" + ind + "_rptDetSecSale_ctl0" + hid_plus_opr.value + "_txtSecSale";
                    }
                    else {
                        plus_control_id = "rptProduct_ctl0" + ind + "_rptDetSecSale_ctl0" + hid_plus_opr.value + "_txtSecSale";
                    }
                    pls_control_id = document.getElementById(plus_control_id);
                    pls_control_id.setAttribute("readOnly", true);
                    pls_control_id.style.backgroundColor = "LightGreen";

                    //Cust_Col
                    for (clbal_i = 0; clbal_i <= parseInt(hid_cl_opr.value); clbal_i++) {
                        if (parseInt(ind) > 9) {
                            cust_sub_control_id = "rptProduct_ctl" + ind + "_rptDetSecSale_ctl0" + clbal_i + "_hidSecSaleSub";
                        }
                        else {
                            cust_sub_control_id = "rptProduct_ctl0" + ind + "_rptDetSecSale_ctl0" + clbal_i + "_hidSecSaleSub";
                        }

                        col_sub_control_id = document.getElementById(cust_sub_control_id);

                        if (parseInt(ind) > 9) {
                            cust_control_id = "rptProduct_ctl" + ind + "_rptDetSecSale_ctl0" + clbal_i + "_txtSecSale";
                            //alert(cust_control_id);
                        }
                        else {
                            cust_control_id = "rptProduct_ctl0" + ind + "_rptDetSecSale_ctl0" + clbal_i + "_txtSecSale";
                        }

                        col_control_id = document.getElementById(cust_control_id);

                        if (col_sub_control_id != null) {
                            if (col_sub_control_id.value == 'cust_col') {
                                if (col_control_id != null) {
                                    col_control_id.setAttribute("readOnly", true);
                                    col_control_id.style.backgroundColor = "LightGreen";
                                }
                            }
                        }
                    }
                }
            }
        }

        function qtychange(e) {

            var cntrl_id = e.id;

            //alert("cntrl_id :"+ cntrl_id);

            var qty = e.value;
            var tot_amt = 0;
            cntrl_id = cntrl_id.substring(0, cntrl_id.indexOf("txtSecSale"));
            var hid_amt = cntrl_id.substring(0, cntrl_id.indexOf("rptDetSecSale"));

            var hid_cntrl;
            var hid_rate;
            var prate;

            var CalcRate = $("#hid_Calc_Rate").val();

            if (CalcRate == "R") {
                hid_cntrl = hid_amt + 'hidRate';
                hid_rate = document.getElementById(hid_cntrl);
                prate = hid_rate.value;
            }
            else if (CalcRate == "M") {

                hid_cntrl = hid_amt + 'hidMRPRate';
                hid_rate = document.getElementById(hid_cntrl);
                prate = hid_rate.value;
            }
            else if (CalcRate == "D") {

                hid_cntrl = hid_amt + 'hidDistRate';
                hid_rate = document.getElementById(hid_cntrl);
                prate = hid_rate.value;
            }
            else if (CalcRate == "N") {
                hid_cntrl = hid_amt + 'hidNSRRate';
                hid_rate = document.getElementById(hid_cntrl);
                prate = hid_rate.value;
            }
            else if (CalcRate == "T") {

                hid_cntrl = hid_amt + 'hidTargRate';
                hid_rate = document.getElementById(hid_cntrl);
                prate = hid_rate.value;
            }

            //if ((qty != '') && (prate != '')) {
            if ((qty > 0) && (prate > 0)) {
                tot_amt = parseFloat(qty) * parseFloat(prate);
            }

            var lbl_cntrl = cntrl_id + 'txtval';
            //var lbl_cntrl = cntrl_id + 'lblSecVal';
            var lbl = document.getElementById(lbl_cntrl);

            if (lbl != null) {
                var lbl_id = lbl.id;
                if (lbl_id != null) {
                    lbl.value = tot_amt;
                    lbl.value = parseFloat(lbl.value).toFixed(2);
                }
            }

            var cls_cntrl_id = e.id;
            cls_cntrl_id = cls_cntrl_id.substring(0, cls_cntrl_id.indexOf("txtSecSale"));
            cls_cntrl_id = cls_cntrl_id.substring(0, cls_cntrl_id.length - 2);



            //Total +

            var pls_cntrl_id = cls_cntrl_id;
            var hid_plusopr = document.getElementById("<%=hidPlus.ClientID%>");
            //alert(hid_plusopr.value);
            var plus_cntrl_id = pls_cntrl_id + hid_plusopr.value + "_txtSecSale";

            // alert("plus_cntrl_id : " + hid_plusopr.value);

            var plus_bal_pos = hid_plusopr.value;
            var plusbal = document.getElementById(plus_cntrl_id);
            var plus_bal_cntrl_id;
            var cur_plus_bal_cntrl_id;
            var hid_plus_bal_cntrl_id; // Calc needed
            var hid_cur_plus_bal_cntrl_id; // Calc needed
            var hid_opr_plus_cntrl_id;     // operator
            var hid_cur_opr_plus_cntrl_id; // opertor
            var val_plus_cntrl_id = pls_cntrl_id + hid_plusopr.value + "_txtval";

            var val_plusbal = document.getElementById(val_plus_cntrl_id);

            var pls_pos = hid_plusopr.value;

            //alert(prate);

            //var val_close_cntrl_id = cls_cntrl_id + hid_clopr.value + "_txtval";
            //var val_clsbal = document.getElementById(val_close_cntrl_id);


            var plusbalqty = 0;
            //alert(plusbal);
            if (plusbal != null) {
                //alert(plusbalqty);    
                for (i = 0; i < parseInt(plus_bal_pos); i++) {
                    plus_bal_cntrl_id = pls_cntrl_id + i + "_txtSecSale";

                    //  alert("plus_bal_cntrl_id : " + plus_bal_cntrl_id);

                    cur_plus_bal_cntrl_id = document.getElementById(plus_bal_cntrl_id);
                    hid_plus_opr_cntrl_id = pls_cntrl_id + i + "_hidSecSaleOpr";  // opertor      

                    //  alert("hid_plus_opr_cntrl_id : " + hid_plus_opr_cntrl_id);

                    hid_plus_cur_opr_cntrl_id = document.getElementById(hid_plus_opr_cntrl_id);   // opertor

                    //Calculating Total qty
                    if (parseInt(cur_plus_bal_cntrl_id.value) > 0) {
                        //If the field is mapped with '+' then add the closing balance with the current value
                        if (hid_plus_cur_opr_cntrl_id.value == '+') {
                            plusbalqty = parseInt(plusbalqty) + parseInt(cur_plus_bal_cntrl_id.value);

                            // alert("Calculating Total qty : " + plusbalqty);
                        }
                    }

                }

                plusbal.value = plusbalqty;
                //plusbal.value = parseFloat(plusbal.value).toFixed(2);

                if (val_plusbal != null) {
                    val_plusbal.value = plusbalqty * prate;
                    val_plusbal.value = parseFloat(val_plusbal.value).toFixed(2);
                    //  alert("val_plusbal : " + val_plusbal.value);
                }
            }  // End of Total Plus


            //Closing Balance
            var hid_clopr = document.getElementById("<%=hidClBal.ClientID%>");

            var close_cntrl_id = cls_cntrl_id + hid_clopr.value + "_txtSecSale";

            // alert("close_cntrl_id : " + close_cntrl_id);

            var cls_bal_pos = hid_clopr.value;

            var clsbal = document.getElementById(close_cntrl_id);

            // alert("clsbal :" + clsbal);

            var close_bal_cntrl_id;
            var cur_close_bal_cntrl_id;
            var hid_close_bal_cntrl_id; // Calc needed
            var hid_cur_close_bal_cntrl_id; // Calc needed
            var hid_opr_cntrl_id;     // operator
            var hid_cur_opr_cntrl_id; // opertor
            var hid_form_cntrl_id;     // formula
            var hid_cur_form_cntrl_id; // formula
            var hid_ss_code_cntrl_id;     // sec sale code
            var hid_cur_ss_code_cntrl_id; // sec sale code

            var hid_CF_Field_Other; //CF Field Other
            var hid_cur_CF_Field_Other; //CF Other Field

            var form_bal_cntrl_id;
            var cur_form_bal_cntrl_id;

            var val_close_cntrl_id = cls_cntrl_id + hid_clopr.value + "_txtval";
            var val_clsbal = document.getElementById(val_close_cntrl_id);

            var hid_Rep_Cnt_ID;
            var Hid_Rec_Cnl_Val;

            var arrForm;
            var form_val;
            var form_opr;
            var form_ss_cntrl_id;
            var cur_form_ss_cntrl_id;

            var CFval;

            if (clsbal != null) {
                var clsbalqty = 0;

                for (i = 0; i < parseInt(cls_bal_pos); i++) {
                    close_bal_cntrl_id = cls_cntrl_id + i + "_txtSecSale";
                    cur_close_bal_cntrl_id = document.getElementById(close_bal_cntrl_id);

                    hid_close_bal_cntrl_id = cls_cntrl_id + i + "_hidSecSalecalc";                  // Calc needed               
                    hid_cur_close_bal_cntrl_id = document.getElementById(hid_close_bal_cntrl_id);   // Calc needed  

                    hid_opr_cntrl_id = cls_cntrl_id + i + "_hidSecSaleOpr";                  // opertor               
                    hid_cur_opr_cntrl_id = document.getElementById(hid_opr_cntrl_id);   // opertor

                    hid_form_cntrl_id = cls_cntrl_id + i + "_hidSSFormula";                 // Formula   
                    hid_cur_form_cntrl_id = document.getElementById(hid_form_cntrl_id);   // Formula

                    // alert("Form Value "+  hid_cur_form_cntrl_id.value);

                    hid_ss_code_cntrl_id = cls_cntrl_id + i + "_hidSecSaleCode";    // sec sale code 
                    hid_cur_ss_code_cntrl_id = document.getElementById(hid_ss_code_cntrl_id);   // sec sale code


                    hid_CF_Field_Other = cls_cntrl_id + i + "_hidSS_CF_Other";    // CF Other Field
                    hid_cur_CF_Field_Other = document.getElementById(hid_CF_Field_Other);   // CF Other Field

                    if (hid_cur_CF_Field_Other.value != "") {
                        CF_Field = hid_cur_CF_Field_Other.value;

                        hid_Rep_Cnt_ID = cls_cntrl_id + i + "_hid_CF_Rep_Val";
                        Hid_Rec_Cnl_Val = document.getElementById(hid_Rep_Cnt_ID);

                        var CF_Val = parseInt(Hid_Rec_Cnl_Val.value);

                        // if (parseInt(cur_close_bal_cntrl_id.value) > 0) 
                        // {
                        if (hid_cur_form_cntrl_id.value != '') {
                            //1 + 2 - 5
                            form_val = 0;

                            //  alert('val:' + hid_cur_form_cntrl_id.value);

                            arrForm = hid_cur_form_cntrl_id.value.split(' ');
                            for (var j = 0; j < arrForm.length; j++) {
                                if (arrForm[j].length > 0) {
                                    if ((arrForm[j].trim() == '+') || (arrForm[j].trim() == '-')) {
                                        if (arrForm[j].trim() == '+') {
                                            form_opr = "+";
                                        }
                                        else if (arrForm[j].trim() == '-') {
                                            form_opr = "-";
                                        }

                                        else {
                                            if (arrForm[j].trim() == '+') {
                                                form_opr = "+";
                                            }
                                            else if (arrForm[j].trim() == '-') {
                                                form_opr = "-";
                                            }

                                        }

                                    }
                                    else {
                                        for (clbal_i = 0; clbal_i <= parseInt(cls_bal_pos); clbal_i++) {

                                            form_ss_cntrl_id = cls_cntrl_id + clbal_i + "_hidSecSaleCode";
                                            form_bal_cntrl_id = cls_cntrl_id + clbal_i + "_txtSecSale";
                                            cur_form_ss_cntrl_id = document.getElementById(form_ss_cntrl_id);
                                            cur_form_bal_cntrl_id = document.getElementById(form_bal_cntrl_id);

                                            if (cur_form_ss_cntrl_id.value.trim() == arrForm[j].trim()) {

                                                if (parseInt(cur_form_bal_cntrl_id.value) > 0) {
                                                    if (form_val != '') {
                                                        if (form_opr == '+') {
                                                            form_val = form_val + parseInt(cur_form_bal_cntrl_id.value);
                                                        }
                                                        else if (form_opr == '-') {
                                                            form_val = form_val - parseInt(cur_form_bal_cntrl_id.value);
                                                        }
                                                    }
                                                    else {
                                                        var Cur_CF = CF_Val;

                                                        if (Cur_CF > 0) {
                                                            form_val = parseInt(Cur_CF) + parseInt(cur_form_bal_cntrl_id.value);
                                                        }
                                                        else {
                                                            form_val = parseInt(cur_form_bal_cntrl_id.value);
                                                        }

                                                    }
                                                }
                                            }

                                        }

                                        cur_close_bal_cntrl_id.value = form_val;

                                    }
                                }
                            }


                        }



                        //}

                    }
                    else {

                        if (hid_cur_form_cntrl_id.value != '') {
                            //1 + 2 - 5
                            form_val = 0;

                            arrForm = hid_cur_form_cntrl_id.value.split(' ');
                            for (var j = 0; j < arrForm.length; j++) {
                                if (arrForm[j].length > 0) {

                                    if ((arrForm[j].trim() == '+') || (arrForm[j].trim() == '-')) {
                                        if (arrForm[j].trim() == '+') {
                                            form_opr = "+";
                                        }
                                        else if (arrForm[j].trim() == '-') {
                                            form_opr = "-";
                                        }
                                    }
                                    else {


                                        for (clbal_i = 0; clbal_i <= parseInt(cls_bal_pos); clbal_i++) {
                                            form_ss_cntrl_id = cls_cntrl_id + clbal_i + "_hidSecSaleCode";
                                            form_bal_cntrl_id = cls_cntrl_id + clbal_i + "_txtSecSale";
                                            cur_form_ss_cntrl_id = document.getElementById(form_ss_cntrl_id);
                                            cur_form_bal_cntrl_id = document.getElementById(form_bal_cntrl_id);


                                            if (cur_form_ss_cntrl_id.value.trim() == arrForm[j].trim()) {

                                                if (parseInt(cur_form_bal_cntrl_id.value) > 0) {
                                                    if (form_val != '') {
                                                        if (form_opr == '+') {
                                                            form_val = form_val + parseInt(cur_form_bal_cntrl_id.value);
                                                        }
                                                        else if (form_opr == '-') {
                                                            form_val = form_val - parseInt(cur_form_bal_cntrl_id.value);
                                                        }
                                                    }
                                                    else {
                                                        form_val = parseInt(cur_form_bal_cntrl_id.value);
                                                    }
                                                }
                                            }
                                        }

                                        cur_close_bal_cntrl_id.value = form_val;

                                        //alert("FormVal" + form_val);
                                    }
                                }
                            }

                        }

                    }


                    //Calculating Closing Balance
                    //alert('val: ' + cur_close_bal_cntrl_id.value);
                    if (parseInt(cur_close_bal_cntrl_id.value) > 0) {

                        // Below condition is to check the calculation needed for this field. This is fetched from setup

                        //alert('hid_cur_close_bal_cntrl_id.value : ' + hid_cur_close_bal_cntrl_id.value);
                        if (parseInt(hid_cur_close_bal_cntrl_id.value) == 1) {
                            //If the field is mapped with '+' then add the closing balance with the current value

                            if (hid_cur_opr_cntrl_id.value == '+') {
                                //alert('hid_cur_opr_cntrl_id.value : ' + hid_cur_opr_cntrl_id.value);
                                clsbalqty = parseInt(clsbalqty) + parseInt(cur_close_bal_cntrl_id.value);

                                // alert("clsbalqty (+): " + clsbalqty); 
                            }
                            //If the field is mapped with '-' then subtract the closing balance with the current value
                            else if (hid_cur_opr_cntrl_id.value == '-') {
                                clsbalqty = parseInt(clsbalqty) - parseInt(cur_close_bal_cntrl_id.value);

                                //alert("clsbalqty (-): " + clsbalqty); 
                            }
                        }
                    }
                }



                //alert('clbal : ' + clsbalqty);
                //alert(' tot+ : ' + plusbalqty);
                if (plusbalqty != null) {
                    clsbal.value = clsbalqty - plusbalqty;
                    //   alert("plusbalqty not null: " + clsbal.value); 
                }
                else {
                    clsbal.value = clsbalqty;
                    // alert("plusbalqty null: " + clsbal.value); 
                }

                if (plusbalqty != null) {
                    if (val_clsbal != null) {
                        val_clsbal.value = (clsbalqty - plusbalqty) * prate;
                        val_clsbal.value = parseFloat(val_clsbal.value).toFixed(2);

                        // alert("val_clsbal not null: " + val_clsbal.value); 
                    }
                }

                if (val_clsbal != null) {
                    if (val_clsbal.value < 0) {
                        alert('Negative Stock found');
                    }
                }
            }

            //Form Field Calculation


            //Formula Based Calculation


            var hid_Tot_Order_Cnt = document.getElementById("<%=hid_Order_Cnt.ClientID%>");

            var hid_Order = document.getElementById("<%=hid_Sec_Sale_Order.ClientID%>");

            var hid_tot = parseInt(hid_Tot_Order_Cnt.value) - 1;

            var custcol_cntrl_id = cls_cntrl_id + hid_tot + "_txtSecSale";

            // alert("hid_Tot_Order_Cnt :" + hid_tot);

            var Cur_Tot_Head_Pos = hid_Tot_Order_Cnt.value;

            var cust_col_pos = hid_Tot_Order_Cnt.value;

            var cusCol = document.getElementById(custcol_cntrl_id);

            // alert("cusCol :" + cusCol);

            var cus_col_cntrl_id;
            var cur_cus_col_cntrl_id;
            var hid_cus_col_cntrl_id; // Calc needed
            var hid_cur_cus_col_cntrl_id; // Calc needed
            var hid_opr_cntrl_id;     // operator
            var hid_cur_opr_cntrl_id; // opertor
            var hid_form_cntrl_id;     // formula
            var hid_cur_form_cntrl_id; // formula
            var hid_ss_code_cntrl_id;     // sec sale code
            var hid_cur_ss_code_cntrl_id; // sec sale code


            var form_bal_cntrl_id;
            var cur_form_bal_cntrl_id;

            var val_cus_col_cntrl_id = cls_cntrl_id + hid_Tot_Order_Cnt.value + "_txtval";
            var val_cus_col = document.getElementById(val_cus_col_cntrl_id);

            var hid_Rep_Cnt_ID;
            var Hid_Rec_Cnl_Val;

            var arrForm;
            var form_val;
            var form_opr;
            var form_ss_cntrl_id;
            var cur_form_ss_cntrl_id;

            if (parseInt(cust_col_pos) > parseInt(cls_bal_pos)) {
                //alert("CustCol:" + cust_col_pos);

                if (hid_Order.value != "") {
                    var Cus_Col_Val = 0;

                    for (i = 0; i < parseInt(cust_col_pos); i++) {
                        cus_col_cntrl_id = cls_cntrl_id + i + "_txtSecSale";
                        cur_cus_col_cntrl_id = document.getElementById(cus_col_cntrl_id);

                        hid_cus_col_cntrl_id = cls_cntrl_id + i + "_hidSecSalecalc";                  // Calc needed               
                        hid_cur_cus_col_cntrl_id = document.getElementById(hid_cus_col_cntrl_id);   // Calc needed  

                        hid_opr_cntrl_id = cls_cntrl_id + i + "_hidSecSaleOpr";                  // opertor               
                        hid_cur_opr_cntrl_id = document.getElementById(hid_opr_cntrl_id);   // opertor

                        hid_form_cntrl_id = cls_cntrl_id + i + "_hidSSFormula";                 // Formula   
                        hid_cur_form_cntrl_id = document.getElementById(hid_form_cntrl_id);   // Formula

                        //alert("Form Value "+  hid_cur_form_cntrl_id.value);

                        hid_ss_code_cntrl_id = cls_cntrl_id + i + "_hidSecSaleCode";    // sec sale code 
                        hid_cur_ss_code_cntrl_id = document.getElementById(hid_ss_code_cntrl_id);   // sec sale code

                        if (hid_cur_form_cntrl_id.value != '') {

                            //1 + 2 - 5
                            form_val = 0;

                            arrForm = hid_cur_form_cntrl_id.value.split(' ');
                            for (var j = 0; j < arrForm.length; j++) {

                                if (arrForm[j].length > 0) {

                                    if ((arrForm[j].trim() == '+') || (arrForm[j].trim() == '-')) {
                                        if (arrForm[j].trim() == '+') {
                                            form_opr = "+";
                                        }
                                        else if (arrForm[j].trim() == '-') {
                                            form_opr = "-";
                                        }
                                    }
                                    else {
                                        for (clb = 0; clb <= parseInt(hid_tot); clb++) {

                                            form_ss_cntrl_id = cls_cntrl_id + clb + "_hidSecSaleCode";
                                            form_bal_cntrl_id = cls_cntrl_id + clb + "_txtSecSale";
                                            cur_form_ss_cntrl_id = document.getElementById(form_ss_cntrl_id);
                                            cur_form_bal_cntrl_id = document.getElementById(form_bal_cntrl_id);

                                            if (cur_form_ss_cntrl_id.value.trim() == arrForm[j].trim()) {

                                                // alert("cur_form_bal_cntrl_id :" + cur_form_bal_cntrl_id.value);

                                                if (parseInt(cur_form_bal_cntrl_id.value) > 0) {
                                                    if (form_val != '') {
                                                        if (form_opr == '+') {
                                                            form_val = form_val + parseInt(cur_form_bal_cntrl_id.value);
                                                        }
                                                        else if (form_opr == '-') {
                                                            form_val = form_val - parseInt(cur_form_bal_cntrl_id.value);
                                                        }
                                                    }
                                                    else {
                                                        form_val = parseInt(cur_form_bal_cntrl_id.value);
                                                    }
                                                }
                                            }
                                        }

                                        //alert("form_val :" + form_val);

                                        cur_cus_col_cntrl_id.value = form_val;

                                    }

                                }
                            }
                        }


                        //                        //Calculating Closing Balance
                        //                        //alert('val: ' + cur_close_bal_cntrl_id.value);
                        //                        if (parseInt(cur_cus_col_cntrl_id.value) > 0) 
                        //                        {

                        //                            // Below condition is to check the calculation needed for this field. This is fetched from setup

                        //                            alert('hid_cur_cus_col_cntrl_id.value : ' + hid_cur_cus_col_cntrl_id.value);

                        //                            alert('Cus_Col_Val :' + Cus_Col_Val);

                        //                            if (parseInt(hid_cur_cus_col_cntrl_id.value) == 1) 
                        //                            {
                        //                                //If the field is mapped with '+' then add the closing balance with the current value

                        //                                if (hid_cur_opr_cntrl_id.value == '+') 
                        //                                {
                        //                                    //alert('hid_cur_opr_cntrl_id.value : ' + hid_cur_opr_cntrl_id.value);
                        //                                    Cus_Col_Val = parseInt(Cus_Col_Val) + parseInt(cur_cus_col_cntrl_id.value);

                        //                                    alert("Cus_Col_Val (+): " + clsbalqty); 
                        //                                }
                        //                                //If the field is mapped with '-' then subtract the closing balance with the current value
                        //                                else if (hid_cur_opr_cntrl_id.value == '-') 
                        //                                {
                        //                                    Cus_Col_Val = parseInt(Cus_Col_Val) - parseInt(cur_cus_col_cntrl_id.value);

                        //                                    alert("Cus_Col_Val (-): " + clsbalqty); 
                        //                                }
                        //                            }
                        //                        }

                    }

                    cusCol.value = form_val;
                }
                //  alert("cusCol :" + cusCol.value);
            }


            //Value
            var hid_prod_cnt = document.getElementById("<%=hidprdcnt.ClientID%>");
            var prod_cnt = hid_prod_cnt.value;

            //alert(prod_cnt); --49
            //alert(cls_bal_pos); -- 5

            var cur_val;
            var tot_val;
            var tot_cur_control_id;
            var tot_cum_control_id;
            var act_tot_control_id;
            var act_cum_control_id;

            for (clbal_i = 0; clbal_i <= parseInt(cls_bal_pos); clbal_i++) {
                cur_val = 0;
                for (cind = 0; cind < parseInt(prod_cnt) - 1; cind++) {
                    //tot_cur_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl00_txtSecSale";
                    // tot_cur_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtval";
                    // tot_cum_control_id = document.getElementById(tot_cur_control_id);
                    //alert(tot_cum_control_id);

                    if (parseInt(cind) > 9) {
                        tot_cur_control_id = "rptProduct_ctl" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtval";
                    }
                    else {
                        tot_cur_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtval";
                    }

                    tot_cum_control_id = document.getElementById(tot_cur_control_id);

                    if (tot_cum_control_id != null) {
                        if (tot_cum_control_id.value != '') {
                            //alert(tot_cum_control_id.value);
                            cur_val = parseFloat(cur_val) + parseFloat(tot_cum_control_id.value);

                            //alert("cur_val : " + cur_val); 
                        }
                    }
                }

                //alert(cur_val);
                //cind = cind;
                //act_tot_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl00_txtSecSale";
                if (parseInt(cind) > 9) {
                    act_tot_control_id = "rptProduct_ctl" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtval";
                }
                else {
                    act_tot_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtval";
                }
                //alert('1:' + act_tot_control_id);
                act_cum_control_id = document.getElementById(act_tot_control_id);
                //alert('2:' + act_cum_control_id);
                if (act_cum_control_id != null) {

                    act_cum_control_id.value = cur_val;
                    act_cum_control_id.value = parseFloat(act_cum_control_id.value).toFixed(2);
                    act_cum_control_id.setAttribute("readOnly", true);
                    //act_cum_control_id.style.backgroundColor = "Cyan";
                    //act_cum_control_id.style.Height = "300";
                    //act_cum_control_id.setAttribute("Height", "300");
                    //act_cum_control_id.style.ForeColor = "White";
                    //act_cum_control_id.setAttribute("ForeColor", "White");
                }
            }

        }

        function hide_catg_row() {

            var hid_prod_cnt = document.getElementById("<%=hidprdcnt.ClientID%>");
            var hid_clopr = document.getElementById("<%=hidClBal.ClientID%>");
            //var close_cntrl_id = cls_cntrl_id + hid_clopr.value + "_txtSecSale";
            var cls_bal_pos = hid_clopr.value;
            var prod_cnt = hid_prod_cnt.value;
            var tot_cur_control_id;
            var tot_cum_control_id;
            var act_tot_control_id;
            var act_cum_control_id;
            var cur_prod_id;
            var cur_prod_control_id;
            var cur_td_control_id;
            var act_td_control_id;
            var cur_tdval_control_id;
            var act_tdval_control_id;
            var cur_val_control_id;
            var act_val_control_id;

            for (cind = 0; cind < parseInt(prod_cnt) - 1; cind++) {
                cur_prod_id = "rptProduct_ctl0" + cind + "_hidPDesc";
                cur_prod_control_id = document.getElementById(cur_prod_id);
                for (clbal_i = 0; clbal_i <= parseInt(cls_bal_pos); clbal_i++) {

                    if (parseInt(cind) > 9) {
                        tot_cur_control_id = "rptProduct_ctl" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtSecSale";
                        cur_td_control_id = "rptProduct_ctl" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_tdSecQty";
                        cur_tdval_control_id = "rptProduct_ctl" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_tdSecVal";
                        cur_val_control_id = "rptProduct_ctl" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtval";
                    }
                    else {
                        tot_cur_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtSecSale";
                        cur_td_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_tdSecQty";
                        cur_tdval_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_tdSecVal";
                        cur_val_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtval";
                    }

                    tot_cum_control_id = document.getElementById(tot_cur_control_id);
                    act_td_control_id = document.getElementById(cur_td_control_id);
                    act_tdval_control_id = document.getElementById(cur_tdval_control_id);
                    act_val_control_id = document.getElementById(cur_val_control_id);

                    //alert(tot_cum_control_id.id);
                    if (tot_cum_control_id != null) {
                        if (cur_prod_control_id != null) {
                            if ((cur_prod_control_id.value == 'Catg_Code') || (cur_prod_control_id.value == 'Grp_Code')) {
                                tot_cum_control_id.style.display = "none";
                                //rptProduct_ctl00_rptDetSecSale_ctl00_tdSecQty.style.backgroundColor = "LightGreen";
                                act_td_control_id.style.backgroundColor = "LightGrey";
                                //act_td_control_id.style.border = "none";
                                act_td_control_id.style.borderLeftStyle = "none";
                                act_td_control_id.style.borderBottomStyle = "solid";
                                act_td_control_id.style.borderRightStyle = "none";
                                act_td_control_id.style.borderTopStyle = "solid";

                                //alert('cls_bal_pos :' + cls_bal_pos);
                                //alert('clbal_i : ' + clbal_i);

                                // if (clbal_i == parseInt(cls_bal_pos))
                                //                                act_td_control_id.style.borderRightStyle = "solid";

                                //alert(act_tdval_control_id);
                                if (act_tdval_control_id != null) {
                                    //alert(act_tdval_control_id.id);
                                    act_tdval_control_id.style.backgroundColor = "LightGrey";
                                    act_tdval_control_id.style.borderLeftStyle = "none";
                                    act_tdval_control_id.style.borderBottomStyle = "solid";
                                    act_tdval_control_id.style.borderRightStyle = "none";
                                    act_tdval_control_id.style.borderTopStyle = "solid";

                                    if (clbal_i == parseInt(cls_bal_pos)) {
                                        act_tdval_control_id.style.borderRightStyle = "solid";
                                        act_val_control_id.value = '';
                                    }
                                }
                            }
                            //                        else {
                            //                            tot_cum_control_id.style.display = "block";
                            //                        }  
                        }
                    }
                } // End of Inner For
            } // End of Main For
        }


        function hide_total_row() {

            var hid_prod_cnt = document.getElementById("<%=hidprdcnt.ClientID%>");
            var hid_clopr = document.getElementById("<%=hidClBal.ClientID%>");
            //var close_cntrl_id = cls_cntrl_id + hid_clopr.value + "_txtSecSale";
            var cls_bal_pos = hid_clopr.value;
            var prod_cnt = hid_prod_cnt.value;
            var tot_cur_control_id;
            var tot_cum_control_id;
            var act_tot_control_id;
            var act_cum_control_id;
            for (clbal_i = 0; clbal_i <= parseInt(cls_bal_pos); clbal_i++) {
                //cur_val = 0;
                for (cind = 0; cind < parseInt(prod_cnt) - 1; cind++) {
                    tot_cur_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl00_txtSecSale";

                    if (parseInt(cind) > 9) {
                        tot_cur_control_id = "rptProduct_ctl" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtSecSale";
                    }
                    else {
                        tot_cur_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtSecSale";
                    }
                    tot_cum_control_id = document.getElementById(tot_cur_control_id);
                    if (tot_cum_control_id.value != '') {
                        //cur_val = parseInt(cur_val) + parseInt(tot_cum_control_id.value);
                    }
                }

                //act_tot_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl00_txtSecSale";
                if (parseInt(cind) > 9) {
                    act_tot_control_id = "rptProduct_ctl" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtSecSale";
                }
                else {
                    act_tot_control_id = "rptProduct_ctl0" + cind + "_rptDetSecSale_ctl0" + clbal_i + "_txtSecSale";
                }
                act_cum_control_id = document.getElementById(act_tot_control_id);
                if (act_cum_control_id != null) {
                    //act_cum_control_id.value = cur_val;
                    //act_cum_control_id.setAttribute("readOnly", true);
                    //act_cum_control_id.style.backgroundColor = "LightGreen";
                    act_cum_control_id.style.display = "none";
                }
            }
        }

        function subchange(e) {

            var cntrl_id = e.id;
            var qty = e.value;
            var tot_amt;
            cntrl_id = cntrl_id.substring(0, cntrl_id.indexOf("txtSub"));

            //  alert("cntrl_id (Sub) : " + cntrl_id); 

            //            var hid_amt = cntrl_id.substring(0, cntrl_id.indexOf("rptDetSecSale"));
            //            var hid_cntrl = hid_amt + 'hidRate';
            //            var hid_rate = document.getElementById(hid_cntrl);
            //            var prate = hid_rate.value;

            //            if ((qty != '') && (prate != '')) {
            //                tot_amt = parseFloat(qty) * parseFloat(prate);
            //            }

            //            var lbl_cntrl = cntrl_id + 'txtval';

            //            var lbl = document.getElementById(lbl_cntrl);

            //            if (lbl != null) {
            //                var lbl_id = lbl.id;

            //                if (lbl_id != null) {
            //                    lbl.value = tot_amt;
            //                }
            //            }

            var cls_cntrl_id = e.id;
            cls_cntrl_id = cls_cntrl_id.substring(0, cls_cntrl_id.indexOf("txtSub"));
            cls_cntrl_id = cls_cntrl_id.substring(0, cls_cntrl_id.length - 2);

            //  alert("cls_cntrl_id (Sub) : " + cls_cntrl_id); 

            //Closing Balance
            var hid_clopr = document.getElementById("<%=hidPlus.ClientID%>");
            var close_cntrl_id = cls_cntrl_id + hid_clopr.value + "_txtSub";
            var cls_bal_pos = hid_clopr.value;

            //  alert("close_cntrl_id (Sub) : " + close_cntrl_id); 

            var clsbal = document.getElementById(close_cntrl_id);
            var close_bal_cntrl_id;
            var cur_close_bal_cntrl_id;
            var hid_close_bal_cntrl_id; // Calc needed
            var hid_cur_close_bal_cntrl_id; // Calc needed
            var hid_opr_cntrl_id;     // operator
            var hid_cur_opr_cntrl_id; // opertor

            if (clsbal != null) {
                var clsbalqty = 0;
                for (i = 0; i < parseInt(cls_bal_pos); i++) {
                    close_bal_cntrl_id = cls_cntrl_id + i + "_txtSub";
                    cur_close_bal_cntrl_id = document.getElementById(close_bal_cntrl_id);

                    // alert("close_bal_cntrl_id (Sub) : " + close_bal_cntrl_id);

                    //                    hid_close_bal_cntrl_id = cls_cntrl_id + i + "_hidSecSalecalc";                  // Calc needed               
                    //                    hid_cur_close_bal_cntrl_id = document.getElementById(hid_close_bal_cntrl_id);   // Calc needed               

                    hid_opr_cntrl_id = cls_cntrl_id + i + "_hidSecSaleOpr";                  // opertor               
                    hid_cur_opr_cntrl_id = document.getElementById(hid_opr_cntrl_id);   // opertor

                    //  alert("hid_opr_cntrl_id (Sub) : " + hid_opr_cntrl_id);

                    //Calculating Closing Balance
                    if (parseInt(cur_close_bal_cntrl_id.value) > 0) {
                        //If the field is mapped with '+' then add the closing balance with the current value
                        if (hid_cur_opr_cntrl_id.value == '+') {
                            clsbalqty = parseInt(clsbalqty) + parseInt(cur_close_bal_cntrl_id.value);

                            //alert("hid_cur_opr_cntrl_id (Sub +) : " + clsbalqty);
                        }
                        //If the field is mapped with '-' then subtract the closing balance with the current value
                        else if (hid_cur_opr_cntrl_id.value == '-') {
                            clsbalqty = parseInt(clsbalqty) - parseInt(cur_close_bal_cntrl_id.value);
                            //alert("hid_cur_opr_cntrl_id (Sub -) : " + clsbalqty);
                        }
                    }
                }
                clsbal.value = clsbalqty;
            }

        }

        function confirm_Submit() {
            if (confirm('Do you want to Submit?')) {
                return true;
            }
            else {
                return false;
            }
        }

        function confirm_Save() {
            if (confirm('Do you want to Save?')) {
                return true;
            }
            else {
                return false;
            }
        }
       
    </script>
    <script type="text/javascript">
        //        function ShowProgress() 
        //        {
        //            setTimeout(function () 
        //            {
        //                var modal = $('<div />');
        //                modal.addClass("modal");
        //                $('body').append(modal);
        //                var loading = $(".loading");
        //                loading.hide();
        //                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        //                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        //                loading.css({ top: top, left: left });
        //            }, 200);
        //        }

        //        $('form').live("submit", function () {
        //            ShowProgress();           
        //        });

    </script>
    <%--20/12/2016--%>
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
            min-height: 200px;
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
            margin-top: 25px;
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
        
        #btnClose_Plus:focus
        {
            outline-offset: -2px;
        }
        #btnClose_Plus:hover, #btnClose_Plus:focus
        {
            color: #fff;
            text-decoration: underline;
        }
        #btnClose_Plus:hover, #btnClose_Plus:focus
        {
            color: #fff;
            text-decoration: underline;
        }
        #btnClose_Plus:active, #btnClose_Plus:hover
        {
            outline: 0px none currentColor;
        }
    </style>
    <link href="../../../JScript/BootStrap/dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {
            var ProdArry = [];
            $.ajax({
                type: "GET",
                url: "../../../SS_XML_File/SS_Setup_ProductDetail_Xml.xml",
                dataType: "xml",
                success: function (xml) {
                    $(xml).find('Product').each(function () {
                        var ProdName = $(this).find('Product_Detail_Name').text();
                        ProdArry.push(ProdName);
                    });
                },
                error: function () {
                    alert("An error occurred while processing XML file.");
                }
            });


            $("#btn").click(function (e) {

                ShowDialog_Plus(false, ProdArry);

                //e.preventDefault();
                //$(".loading").hide();

            });



            $("#btnSubmit").click(function (e) {
                ShowDialog_Plus(false, ProdArry);

            });

            //            $("#btnClose_Plus").click(function (e) {

            //                HideDialog_Plus();
            //                e.preventDefault();

            //            });

        });

        function ShowDialog_Plus(modal, ProdArry) {

            var PrdLen = ProdArry.length;

            $("#overlay_Plus").show();
            $("#dialog_Plus").fadeIn(300);

            var counter = ProdArry.length - 1;
            var id;

            if (modal) {
                $("#overlay_Plus").unbind("click");
            }
            else {
                $("#overlay_Plus").click(function (e) {
                    HideDialog_Plus();
                });
            }

            //            id = setInterval(function () 
            //            {
            //                counter--;
            //                if (counter < 0) 
            //                {
            //                    clearInterval(id);
            //                    HideDialog_Plus();
            //                } 
            //                else 
            //                {
            //                    $("#countDown").text(ProdArry[counter]);
            //                }
            //            }, 1500);


            var i = -1;
            id = setInterval(function () {
                i++;
                if (i == counter) {
                    clearInterval(id);
                    HideDialog_Plus();

                    //__doPostBack('btn', 'OnClick');

                    i = -1;
                }
                else {
                    $("#countDown").text("Product Updating :" + ProdArry[i]);
                }

            }, 2000);

            // return true;
        }

        function HideDialog_Plus() {
            $("#overlay_Plus").hide();
            $("#dialog_Plus").fadeOut(300);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0px;">
        <ucl:Menu ID="menu1" runat="server" />
    </div>
    <div>
        <br />
        <center>
            <table width="80%" border="1">
                <tr>
                    <td align="right">
                        <asp:Label ID="lblReject" runat="server" Style="text-decoration: underline;" ForeColor="Red"
                            Font-Size="Small" Font-Names="Verdana" Text="Rejection Reason" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <%--  <td>
                    <asp:Label ID="lblStockiest" runat="server" Text="Stockiest" SkinID="lblMand"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStockiest" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>--%>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblStockiest" runat="server" SkinID="lblMand" Height="19px" Width="100px">
                            <span style="color:Red">*</span>Stockiest</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlStockiest" runat="server" SkinID="ddlRequired" Width="120px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMonth" runat="server" SkinID="lblMand" Height="19px" Width="100px">
                        <span style="color:Red">*</span>Month</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
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
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblYear" runat="server" SkinID="lblMand" Height="19px" Width="100px">
                        <span style="color:Red">*</span>Year</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <center>
                <asp:Button ID="btnGo" runat="server" Text="Go" Width="60px" CssClass="savebutton" OnClick="btnGo_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnClear" runat="server" Text="Clear" Width="60px" CssClass="savebutton"
                    OnClick="btnClear_Click" />
            </center>
            <br />
            <asp:Label ID="lblStatus" runat="server" Visible="false" Text="Edit Restricted as the entry for the month is already submitted. Please contact Admin"
                ForeColor="Red" Font-Bold="true" Font-Size="Medium">
            </asp:Label>
            <br />
            <asp:Panel ID="pnlSecSale" BorderStyle="Solid" runat="server" Visible="false" CssClass="padd">
                <table id="tblSecSale" border="1px solid" cellpadding="5" cellspacing="5" style="border-collapse: collapse;
                    border-width: 1; padding-right: 30px; width: 80%" class="tblSecSaleCss">
                    <tr class="rpttr">
                        <td id="tdSNo" runat="server" style="background-color: #1d8681; color: white; border: 1px solid black;
                            text-align: center;" width="45px">
                            S.No
                        </td>
                        <td id="tdPName" runat="server" style="background-color: #1d8681; color: White; border: 1px solid black;
                            text-align: center" width="150px">
                            Product Name
                        </td>
                        <td id="tdPack" runat="server" style="background-color: #1d8681; color: White; border: 1px solid black;
                            text-align: center" width="50px">
                            Pack
                        </td>
                        <td id="tdRate" runat="server" style="background-color: #1d8681; color: White; border: 1px solid black;
                            text-align: center" width="60px">
                            Rate
                        </td>
                        <asp:Repeater ID="rptSecSaleHeader" runat="server" OnItemDataBound="rptSecSaleHeader_ItemDataBound">
                            <ItemTemplate>
                                <td id="tdMainHdrSecVal" runat="server" align="center" style="background-color: #1d8681;
                                    color: White; width: 120px; border: 1px solid black">
                                    <asp:Literal ID="litSecSale" Text='<%#Eval("Sec_Sale_Name") %>' runat="server"></asp:Literal>
                                    <asp:HiddenField ID="hidMainHdrSecSaleCode" runat="server" Value='<%#Eval("Sec_Sale_Code") %>' />
                                    <asp:HiddenField ID="hidFormula" runat="server" Value='<%#Eval("Der_Formula") %>' />
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <asp:Repeater ID="rptSecSaleHdrVal" runat="server" OnItemDataBound="rptSecSaleHdrVal_ItemDataBound">
                            <ItemTemplate>
                                <td align="center" style="background-color: #1d8681; color: White; border: 1px solid black">
                                    <asp:Literal ID="litSecSaleQty" Text="Qty" runat="server"></asp:Literal>
                                </td>
                                <td id="tdHdrSecVal" runat="server" align="center" style="background-color: #1d8681;
                                    color: White; border: 1px solid black">
                                    <asp:Literal ID="litSecSaleVal" Text="Value" runat="server"></asp:Literal>
                                    <asp:HiddenField ID="hidHdrSecSaleVal" runat="server" Value='<%#Eval("value_needed") %>' />
                                    <asp:HiddenField ID="hidHdrSecSaleCode" runat="server" Value='<%#Eval("Sec_Sale_Code") %>' />
                                    <asp:HiddenField ID="hidHdrcalc" runat="server" Value='<%#Eval("calc_needed") %>' />
                                </td>
                                <td id="tdHdrSecSub" runat="server" align="center" style="background-color: #1d8681;
                                    color: White; border: 1px solid black">
                                    <asp:Literal ID="litSecSaleSub" Text='<%#Eval("Sub_Label") %>' runat="server"></asp:Literal>
                                    <asp:HiddenField ID="hidHdrSecSaleSub" runat="server" Value='<%#Eval("Sub_needed") %>' />
                                    <asp:HiddenField ID="hidHdrSecSaleCodeSub" runat="server" Value='<%#Eval("Sec_Sale_Code") %>' />
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <asp:Repeater ID="rptProduct" runat="server" OnItemDataBound="rptProduct_ItemDataBound">
                            <ItemTemplate>
                                <tr class="rpttr">
                                    <td id="tdpcode" runat="server" align="left" style="border: 1px solid black;">
                                        <div style="font-size: 12px; font-family: Calibri">
                                            <asp:Literal ID="litsno" runat="server"></asp:Literal>
                                            <asp:HiddenField ID="hidPCode" runat="server" Value='<%#Eval("Product_Detail_Code") %>' />
                                            <asp:HiddenField ID="hidPDesc" runat="server" Value='<%#Eval("Product_Description") %>' />
                                        </div>
                                    </td>
                                    <td id="tdpname" runat="server" align="left" style="border: 1px solid black;">
                                        <div style="font-size: 12px; font-family: Calibri">
                                            <asp:Literal ID="litpname" Text='<%#Eval("Product_Detail_Name") %>' runat="server"></asp:Literal>
                                        </div>
                                    </td>
                                    <td id="tdpunit" runat="server" align="left" style="border: 1px solid black;">
                                        <div style="font-size: 12px; font-family: Calibri;">
                                            <asp:Literal ID="litpack" Text='<%#Eval("Product_Sale_Unit") %>' runat="server"></asp:Literal>
                                        </div>
                                    </td>
                                    <td id="tdprate" runat="server" align="left" style="border: 1px solid black;">
                                        <div style="font-size: 12px; font-family: Calibri">
                                            <asp:Literal ID="litrate" Text='<%#Eval("Distributor_Price") %>' runat="server"></asp:Literal>
                                            <asp:HiddenField ID="hidRate" runat="server" Value='<%#Eval("Retailor_Price") %>' />
                                            <asp:HiddenField ID="hidMRPRate" runat="server" Value='<%#Eval("MRP_Price") %>' />
                                            <asp:HiddenField ID="hidDistRate" runat="server" Value='<%#Eval("Distributor_Price") %>' />
                                            <asp:HiddenField ID="hidNSRRate" runat="server" Value='<%#Eval("NSR_Price") %>' />
                                            <asp:HiddenField ID="hidTargRate" runat="server" Value='<%#Eval("Target_Price") %>' />
                                        </div>
                                    </td>
                                    <asp:Repeater ID="rptDetSecSale" runat="server" OnItemDataBound="rptDetSecSale_ItemDataBound">
                                        <ItemTemplate>
                                            <td id="tdSecQty" runat="server" style="border: 1px solid black; height: 30;" align="center"
                                                valign="middle" class="DetSecSale">
                                                <asp:HiddenField ID="hidSecSaleCode" runat="server" Value='<%#Eval("Sec_Sale_Code") %>' />
                                                <asp:HiddenField ID="hidSecSaleName" runat="server" Value='<%#Eval("Sec_Sale_Name") %>' />
                                                <asp:HiddenField ID="hidSecSaleVal" runat="server" Value='<%#Eval("value_needed") %>' />
                                                <asp:HiddenField ID="hidSecSalecalc" runat="server" Value='<%#Eval("calc_needed") %>' />
                                                <asp:HiddenField ID="hidSecSaleOpr" runat="server" Value='<%#Eval("Sel_Sale_Operator") %>' />
                                                <asp:HiddenField ID="hidSecSaleSub" runat="server" Value='<%#Eval("Sec_Sale_Sub_Name") %>' />
                                                <asp:HiddenField ID="hidSecSaleOB" runat="server" Value='<%#Eval("Carry_Fwd_Needed") %>' />
                                                <asp:HiddenField ID="hidSecSaleCB" runat="server" Value='<%#Eval("Carry_Fwd_Field") %>' />
                                                <asp:HiddenField ID="hidSSFormula" runat="server" Value='<%#Eval("Der_Formula") %>' />
                                                <asp:HiddenField ID="hidSS_CalculationMode" runat="server" Value='<%#Eval("CalculationMode") %>' />
                                                <asp:HiddenField ID="hidSS_CF_Other" runat="server" Value='<%#Eval("CalcF_Field") %>' />
                                                <asp:HiddenField ID="hidSecSale_OrderBy" runat="server" Value='<%#Eval("Order_by") %>' />
                                                <asp:TextBox ID="txtSecSale" runat="server" CssClass="rptmar" Width="50" SkinID="TxtBxNumOnlyWOColor"
                                                    onkeyup="qtychange(this)" onkeypress="CheckNumeric(event);" MaxLength="5"></asp:TextBox>
                                                <asp:HiddenField ID="hid_CF_Rep_Val" runat="server" />
                                            </td>
                                            <td id="tdSecVal" runat="server" style="border: 1px solid black;">
                                                <asp:Label ID="lblSecSale" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtval" runat="server" CssClass="rptmar" Width="80" BorderStyle="None"
                                                    BackColor="Transparent"></asp:TextBox>
                                            </td>
                                            <td id="tdSecSub" runat="server" style="border: 1px solid black;">
                                                <asp:Label ID="lblSecSaleSub" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtSub" runat="server" CssClass="rptmar" Width="40" SkinID="TxtBxNumOnly"
                                                    onchange="subchange(this)" onkeypress="CheckNumeric(event);"> </asp:TextBox>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
                <asp:HiddenField ID="hidClBal" runat="server" />
                <asp:HiddenField ID="hidprdcnt" runat="server" />
                <asp:HiddenField ID="hidCalc" runat="server" />
                <asp:HiddenField ID="hidPlus" runat="server" />
                <asp:HiddenField ID="hidMinus" runat="server" />
                <asp:HiddenField ID="hidCust" runat="server" />
                <asp:HiddenField ID="hidCust1" runat="server" />
                <asp:HiddenField ID="hid_Calc_Rate" runat="server" />
                <asp:HiddenField ID="hid_Calc_Field" runat="server" />
                <asp:HiddenField ID="hid_Sec_Sale_Order" runat="server" />
                <asp:HiddenField ID="hid_Order_Cnt" runat="server" />
            </asp:Panel>
            <br />
            <%--  <asp:Button ID="btn" runat="server" Text="Draft Save" 
                OnClick="btn_Click" Visible="false" CssClass="savebutton" Width="90px" Height="25px" />--%>
            <asp:LinkButton ID="btn" runat="server" Text="Draft Save" Style="text-decoration: none;
                text-align: center" OnClick="btn_Click" Visible="false" CssClass="savebutton" Width="90px"
                Height="25px"></asp:LinkButton>
            &nbsp; &nbsp;
            <%--      <asp:Button ID="btnSubmit" runat="server" Text="Send to Admin" OnClientClick="return confirm_Submit();"
                OnClick="btnSubmit_Click" Visible="false" CssClass="savebutton" Width="120px" Height="25px" />--%>
            <asp:LinkButton ID="btnSubmit" runat="server" Text="Send to Admin" OnClick="btnSubmit_Click"
                Visible="false" CssClass="savebutton" Style="text-decoration: none; text-align: center"
                Width="120px" Height="25px"></asp:LinkButton>
            <div>
                <asp:Button ID="btnApprove" CssClass="savebutton" runat="server" Visible="false" Text="Approve"
                    OnClick="btnApprove_Click" Width="100px" Height="25px" />
                &nbsp
                <asp:Button ID="btnReject" CssClass="savebutton" runat="server" Visible="false" Text="Reject"
                    OnClick="btnReject_Click" Width="100px" Height="25px" />
                &nbsp
                <asp:TextBox ID="txtReason" Width="400" Height="45" Visible="false" TextMode="MultiLine"
                    runat="server"></asp:TextBox>
                &nbsp
                <asp:Button ID="btnBack" CssClass="savebutton" runat="server" Visible="false" Text="Back to Field Force"
                    OnClick="btnBack_Click" />
            </div>
        </center>
        <div>
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
                            <td class="web_dialog_title">
                                Product Items
                            </td>
                            <%--<td class="web_dialog_title align_right">
                                <a href="#" id="btnClose_Plus">Close</a>
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <h1 id="countDown" style="color: Purple; text-align: center; font-size: 12px; font-weight: bold">
                                    </h1>
                                </div>
                                <br />
                                <div id="Shp_Img" style="margin-left: 50%; margin-top: 2%">
                                    <img src="../../../Images/loading/animated-shopping-cart-image-0013.gif" style="width: 100px;
                                        height: 100px; position: fixed;" alt="" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
