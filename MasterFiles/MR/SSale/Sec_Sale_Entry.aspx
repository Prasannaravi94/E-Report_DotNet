<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sec_Sale_Entry.aspx.cs" Inherits="MasterFiles_MR_SSale_Sec_Sale_Entry" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />

    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <%-- <script language="javascript" type="text/javascript">
        function OnKeyUp_TextBox() {
            //    var oGridDIV = document.getElementById("grdProd");
            var grid = document.getElementById("<%= grdProd.ClientID%>");
         
                    var txtAmountReceive = $("input[id*=txtCB]")

                    alert(txtAmountReceive.value);
                   
        }
    
    
    </script>--%>
    <%--    <script type="text/javascript">
        function get(txt1, txt2) {
            if (isNaN(txt2)) {
                txt1.value = parseInt(txt1.value) - parseInt(txt2.value);
            }

        }
</script>--%>
 <script type="text/javascript">
     function validate() {
         var txtreject = document.getElementById('<%=txtreject.ClientID %>').value;
         if (txtreject == "") {
             alert("Please Enter the Reason");
             document.getElementById('<%=txtreject.ClientID %>').focus();
             return false;
         }

         if (confirm('Do you want to Reject?')) {
             return true;
         }
         else {
             return false;
         }
     }
    </script>



    <script type="text/javascript">
      
            function CalculateTotals() {
                var gv = document.getElementById("<%= grdProd.ClientID %>");
                var lb = gv.getElementsByTagName("span");
                var rowCount = gv.rows.length;
                var total = 0;
                var CB = 0;
                var rate = '';
                var obval = 0;
                var salval = 0;
                var Status = $("#hdnStatus").val();
                var Prev = $("#hdnPrev").val();
                for (var i = 1; i < rowCount - 1; i++) {

                    var row = gv.rows[i];
                    var txt = row.getElementsByTagName('input');
                    var label = row.getElementsByTagName('span');
                    var OldCB = parseInt(txt[8].value);
                    var NewCB = parseInt(txt[7].value);
                    var OldOB = parseInt(txt[1].value);
                    var NewOB = parseInt(txt[0].value);

                    var Prim = parseInt(txt[2].value);
                    var Rec = parseInt(txt[3].value);
                    var Sal = parseInt(txt[5].value);
                    var SRet = parseInt(txt[6].value);
                    var Trans = parseInt(txt[9].value);

                    var Sal2 = parseInt(txt[4].value);
                    rate = label[3].innerHTML;
                    var sale = 0;

                    if (isNaN(OldCB)) OldCB = 0;
                    if (isNaN(NewCB)) NewCB = 0;
                    if (isNaN(OldOB)) OldOB = 0;
                    if (isNaN(NewOB)) NewOB = 0;

                    if (isNaN(Prim)) Prim = 0;
                    if (isNaN(Rec)) Rec = 0;
                    if (isNaN(Sal)) Sal = 0;
                    if (isNaN(SRet)) SRet = 0;
                    if (isNaN(Trans)) Trans = 0;
                    if (isNaN(Sal2)) Sal2 = 0;
                 
                    if (txt[5].value != undefined) {
                        if (Status == 2) {
                            //                        if (parseInt(txt[5].value) != "0" || parseInt(txt[5].value) != "") {
                            sale = (parseInt(txt[5].value) - NewCB) + OldCB
                            txt[0].Enabled = false;
                            // if ((Prim != 0) && (Rec != 0) && (Sal != 0) || (SRet != 0) || (Trans != 0) || (NewCB != 0)) {                                
                            //                                if (sale < 0) {                                    
                            //                                    alert('Negative Found')
                            //                                    txt[7].focus();
                            //                                    txt[7].value = OldCB;
                            //                                    txt[4].value = parseInt(txt[5].value)
                            //                                    
                            //                                    return false;
                            //                                }
                            // }
                            //                        }
                            //                        else {
                            //                         //   txt[0].value = NewCB;
                            //                        }
                        }
                        else {

                            if (Prev == 1) {
                                //  if (parseInt(txt[5].value) != "0" || parseInt(txt[5].value) != "") {
                                sale = (parseInt(txt[5].value) - NewCB) + OldCB
                                
                                txt[0].Enabled = false;
                                // if (((Prim != 0) && (Rec != 0) && (Sal != 0)) || (SRet != 0) || (Trans != 0)) {
                                //                                    if (sale < 0) {
                                //                                        alert('Negative Found')
                                //                                        txt[7].focus();
                                //                                        txt[7].value = OldCB;
                                //                                        txt[4].value = parseInt(txt[5].value)
                                //                                        return false;
                                //                                    }
                                  
                                //}
                            
                            }
                            else if (Prev == 2) {
                                sale = (parseInt(txt[5].value) - NewCB) + OldCB
                                sale = sale + NewOB
                                //Math.abs(sale)
                                txt[0].Enabled = true;
                                //if ((Prim != 0) || (Rec != 0) || (Sal != 0) || (SRet != 0) || (Trans != 0) || (NewCB != 0)) {      
                                //                                if (sale < 0) {
                                //                                    alert('Negative Found')
                                //                                    txt[7].focus();
                                //                                    txt[7].value = '';
                                //                                    txt[4].value = parseInt(txt[5].value) + NewOB
                                //                                    return false;
                                //                                }
                                // }

                            }
                            else if (Prev == 3) {
                                sale = (parseInt(txt[5].value) - NewCB) + OldCB
                                sale = (sale + NewOB) - OldOB
                                //Math.abs(sale)
                                txt[0].Enabled = true;
                           

                            }
                            else if (prev == undefined) {
                                txt[0].Enabled = false;
                                sale = (parseInt(txt[5].value) - NewCB) + OldCB

                            }
                        }
                        //  if (sale != undefined && sale.type == "text") //check only textbox, ignore empty one
                        //      if (!isNaN(sale.value) && sale.value != "") //check for valid number
                        if (isNaN(sale)) {
                            txt[4].value = '';
                       
                            CB += parseInt(NewCB) * rate;
                            obval = parseInt(NewCB) * rate;
                            label[5].innerHTML = obval.toFixed(2);
                        }
                        else {
                            txt[4].value = sale
                            total += parseInt(sale) * rate;
                            obval = parseInt(NewCB) * rate;
                            salval = parseInt(sale) * rate;
                            label[4].innerHTML = salval.toFixed(2);
                            label[5].innerHTML = obval.toFixed(2);
                            CB += parseInt(NewCB) * rate;
                        }
                        //                    if ((Prim == 0) && (Rec == 0) && (Sal == 0) && (SRet == 0) && (Trans == 0)) { 
                        //                        if (NewCB == 0) {
                        //                            txt[0].value = '';
                        //                            txt[4].value = '';
                        //                        }
                        //                        else {
                        //                            txt[0].value = NewCB;
                        //                           txt[4].value = '';

                        //                        }
                        //                    }
                    }
                }
          
                lb[lb.length - 8].innerHTML = total.toFixed(2);
                lb[lb.length - 6].innerHTML = total.toFixed(2);
                lb[lb.length - 4].innerHTML = CB.toFixed(2);
                lb[lb.length - 3].innerHTML = CB.toFixed(2);

            }
       
    </script>

    <script type="text/javascript">
     
            function CalculateOB() {
                var gv = document.getElementById("<%= grdProd.ClientID %>");
                var lb = gv.getElementsByTagName("span");
                var rowCount = gv.rows.length;
                var total = 0;
                var OB = 0;
                var rate = '';
                var ob_qty = 0;
                var salval = 0;
                for (var i = 1; i < rowCount - 1; i++) {

                    var row = gv.rows[i];
                    var txt = row.getElementsByTagName('input');
                    var label = row.getElementsByTagName('span');

                    rate = label[3].innerHTML;

                    var sale2 = 0;
                    ob_qty = parseInt(txt[0].value);
                    var OldOB = parseInt(txt[1].value);
                    var NewOB = parseInt(txt[0].value);
                    var OldCB = parseInt(txt[8].value);
                    var NewCB = parseInt(txt[7].value);

                    var Prim = parseInt(txt[2].value);
                    var Rec = parseInt(txt[3].value);
                    var Sal = parseInt(txt[5].value);
                    var SRet = parseInt(txt[6].value);
                    var Trans = parseInt(txt[9].value);

                    if (isNaN(OldCB)) OldCB = 0;
                    if (isNaN(NewCB)) NewCB = 0;
                    if (isNaN(OldOB)) OldOB = 0;
                    if (isNaN(NewOB)) NewOB = 0;

                    if (isNaN(Prim)) Prim = 0;
                    if (isNaN(Rec)) Rec = 0;
                    if (isNaN(Sal)) Sal = 0;
                    if (isNaN(SRet)) SRet = 0;
                    if (isNaN(Trans)) Trans = 0;
                    if (txt[5].value != undefined) {
                        sale2 = ((parseInt(txt[5].value) + NewOB) - OldOB)
                        //  if (sale != undefined && sale.type == "text") //check only textbox, ignore empty one
                        //      if (!isNaN(sale.value) && sale.value != "") //check for valid number
                        sale2 = (sale2 - NewCB) + OldCB
                        // if ((Prim != 0) && (Rec != 0) && (Sal != 0) || (SRet != 0) || (Trans != 0) || (NewCB != 0)) {
                        //                        if (sale2 < 0) {
                        //                            alert('Negative Found')
                        //                            txt[7].focus();
                        //                            txt[7].value = OldCB;
                        //                            txt[4].value = parseInt(txt[5].value)

                        //                            return false;
                        //                        }
                        // }
                        if (isNaN(sale2)) {
                            txt[4].value = '';

                            OB += parseInt(NewOB) * rate;
                        }
                        else {
                            txt[4].value = sale2
                            total += parseInt(sale2) * rate;
                            salval = parseInt(sale2) * rate;
                            label[4].innerHTML = salval.toFixed(2);
                            OB += parseInt(NewOB) * rate;
                        }
                        //                    if ((Prim == 0) && (Rec == 0) && (Sal == 0) && (SRet == 0) && (Trans == 0)) { 
                        //                        if (isNaN(ob_qty)) {
                        //                            txt[7].value = '';
                        //                            txt[4].value = '';
                        //                        }
                        //                        else {
                        //                            txt[7].value = ob_qty;
                        //                            txt[4].value = '';
                        //                         

                        //                        }
                        //                    }
                    }
                }



                lb[lb.length - 8].innerHTML = total.toFixed(2);
                lb[lb.length - 6].innerHTML = total.toFixed(2);
                lb[lb.length - 11].innerHTML = OB.toFixed(2);
            }
        
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            
                var gv = document.getElementById("<%= grdProd.ClientID %>");
                var lb = gv.getElementsByTagName("span");
                var rowCount = gv.rows.length;
                var total = 0;
                var CB = 0;
                var rate = '';
                var obval = 0;
                var salval = 0;
                var Status = $("#hdnStatus").val();
                var Prev = $("#hdnPrev").val();
                for (var i = 1; i < rowCount - 1; i++) {

                    var row = gv.rows[i];
                    var txt = row.getElementsByTagName('input');
                    var label = row.getElementsByTagName('span');
                    var OldCB = parseInt(txt[8].value);
                    var NewCB = parseInt(txt[7].value);
                    var OldOB = parseInt(txt[1].value);
                    var NewOB = parseInt(txt[0].value);

                    var Prim = parseInt(txt[2].value);
                    var Rec = parseInt(txt[3].value);
                    var Sal = parseInt(txt[5].value);
                    var SRet = parseInt(txt[6].value);
                    var Trans = parseInt(txt[9].value);

                    var Sal2 = parseInt(txt[4].value);
                    rate = label[3].innerHTML;
                    var sale = 0;

                    if (isNaN(OldCB)) OldCB = 0;
                    if (isNaN(NewCB)) NewCB = 0;
                    if (isNaN(OldOB)) OldOB = 0;
                    if (isNaN(NewOB)) NewOB = 0;

                    if (isNaN(Prim)) Prim = 0;
                    if (isNaN(Rec)) Rec = 0;
                    if (isNaN(Sal)) Sal = 0;
                    if (isNaN(SRet)) SRet = 0;
                    if (isNaN(Trans)) Trans = 0;
                    if (isNaN(Sal2)) Sal2 = 0;
               
                    if (txt[5].value != undefined) {
                        if (Status == 2) {
                            //                        if (parseInt(txt[5].value) != "0" || parseInt(txt[5].value) != "") {
                            sale = (parseInt(txt[5].value) - NewCB) + OldCB
                            txt[0].Enabled = false;
                            // if ((Prim != 0) && (Rec != 0) && (Sal != 0) || (SRet != 0) || (Trans != 0) || (NewCB != 0)) {                                
                            //                                if (sale < 0) {                                    
                            //                                    alert('Negative Found')
                            //                                    txt[7].focus();
                            //                                    txt[7].value = OldCB;
                            //                                    txt[4].value = parseInt(txt[5].value)
                            //                                    
                            //                                    return false;
                            //                                }
                            // }
                            //                        }
                            //                        else {
                            //                         //   txt[0].value = NewCB;
                            //                        }
                        }
                        else {

                            if (Prev == 1) {
                                //  if (parseInt(txt[5].value) != "0" || parseInt(txt[5].value) != "") {
                                sale = (parseInt(txt[5].value) - NewCB) + OldCB
                                
                                txt[0].Enabled = false;
                                // if (((Prim != 0) && (Rec != 0) && (Sal != 0)) || (SRet != 0) || (Trans != 0)) {
                                //                                    if (sale < 0) {
                                //                                        alert('Negative Found')
                                //                                        txt[7].focus();
                                //                                        txt[7].value = OldCB;
                                //                                        txt[4].value = parseInt(txt[5].value)
                                //                                        return false;
                                //                                    }
                                  
                                //}
                            
                            }
                            else if (Prev == 2) {
                                sale = (parseInt(txt[5].value) - NewCB) + OldCB
                                sale = sale + NewOB
                                //Math.abs(sale)
                                txt[0].Enabled = true;
                                //if ((Prim != 0) || (Rec != 0) || (Sal != 0) || (SRet != 0) || (Trans != 0) || (NewCB != 0)) {      
                                //                                if (sale < 0) {
                                //                                    alert('Negative Found')
                                //                                    txt[7].focus();
                                //                                    txt[7].value = '';
                                //                                    txt[4].value = parseInt(txt[5].value) + NewOB
                                //                                    return false;
                                //                                }
                                // }

                            }
                            else if (Prev == 3) {
                                sale = (parseInt(txt[5].value) - NewCB) + OldCB
                                sale = (sale + NewOB) - OldOB
                                //Math.abs(sale)
                                txt[0].Enabled = true;
                           

                            }
                            else if (prev == undefined) {
                                txt[0].Enabled = false;
                                sale = (parseInt(txt[5].value) - NewCB) + OldCB

                            }
                        }
                        //  if (sale != undefined && sale.type == "text") //check only textbox, ignore empty one
                        //      if (!isNaN(sale.value) && sale.value != "") //check for valid number
                        if (isNaN(sale)) {
                            txt[4].value = '';
                       
                            CB += parseInt(NewCB) * rate;
                            obval = parseInt(NewCB) * rate;
                            label[5].innerHTML = obval.toFixed(2);
                        }
                        else {
                            txt[4].value = sale
                            total += parseInt(sale) * rate;
                            obval = parseInt(NewCB) * rate;
                            salval = parseInt(sale) * rate;
                            label[4].innerHTML = salval.toFixed(2);
                            label[5].innerHTML = obval.toFixed(2);
                            CB += parseInt(NewCB) * rate;
                        }
                        //                    if ((Prim == 0) && (Rec == 0) && (Sal == 0) && (SRet == 0) && (Trans == 0)) { 
                        //                        if (NewCB == 0) {
                        //                            txt[0].value = '';
                        //                            txt[4].value = '';
                        //                        }
                        //                        else {
                        //                            txt[0].value = NewCB;
                        //                           txt[4].value = '';

                        //                        }
                        //                    }
                    }
                }
          
                lb[lb.length - 8].innerHTML = total.toFixed(2);
                lb[lb.length - 6].innerHTML = total.toFixed(2);
                lb[lb.length - 4].innerHTML = CB.toFixed(2);
                lb[lb.length - 3].innerHTML = CB.toFixed(2);

           
        });

    </script>
        <script type="text/javascript">

            $(document).ready(function () {
             
                var gv = document.getElementById("<%= grdProd.ClientID %>");
                var lb = gv.getElementsByTagName("span");
                var rowCount = gv.rows.length;
                var total = 0;
                var OB = 0;
                var rate = '';
                var ob_qty = 0;
                var salval = 0;
                for (var i = 1; i < rowCount - 1; i++) {

                    var row = gv.rows[i];
                    var txt = row.getElementsByTagName('input');
                    var label = row.getElementsByTagName('span');

                    rate = label[3].innerHTML;

                    var sale2 = 0;
                    ob_qty = parseInt(txt[0].value);
                    var OldOB = parseInt(txt[1].value);
                    var NewOB = parseInt(txt[0].value);
                    var OldCB = parseInt(txt[8].value);
                    var NewCB = parseInt(txt[7].value);

                    var Prim = parseInt(txt[2].value);
                    var Rec = parseInt(txt[3].value);
                    var Sal = parseInt(txt[5].value);
                    var SRet = parseInt(txt[6].value);
                    var Trans = parseInt(txt[9].value);

                    if (isNaN(OldCB)) OldCB = 0;
                    if (isNaN(NewCB)) NewCB = 0;
                    if (isNaN(OldOB)) OldOB = 0;
                    if (isNaN(NewOB)) NewOB = 0;

                    if (isNaN(Prim)) Prim = 0;
                    if (isNaN(Rec)) Rec = 0;
                    if (isNaN(Sal)) Sal = 0;
                    if (isNaN(SRet)) SRet = 0;
                    if (isNaN(Trans)) Trans = 0;
                    if (txt[5].value != undefined) {
                        sale2 = ((parseInt(txt[5].value) + NewOB) - OldOB)
                        //  if (sale != undefined && sale.type == "text") //check only textbox, ignore empty one
                        //      if (!isNaN(sale.value) && sale.value != "") //check for valid number
                        sale2 = (sale2 - NewCB) + OldCB
                        // if ((Prim != 0) && (Rec != 0) && (Sal != 0) || (SRet != 0) || (Trans != 0) || (NewCB != 0)) {
                        //                        if (sale2 < 0) {
                        //                            alert('Negative Found')
                        //                            txt[7].focus();
                        //                            txt[7].value = OldCB;
                        //                            txt[4].value = parseInt(txt[5].value)

                        //                            return false;
                        //                        }
                        // }
                        if (isNaN(sale2)) {
                            txt[4].value = '';

                            OB += parseInt(NewOB) * rate;
                        }
                        else {
                            txt[4].value = sale2
                            total += parseInt(sale2) * rate;
                            salval = parseInt(sale2) * rate;
                            label[4].innerHTML = salval.toFixed(2);
                            OB += parseInt(NewOB) * rate;
                        }
                        //                    if ((Prim == 0) && (Rec == 0) && (Sal == 0) && (SRet == 0) && (Trans == 0)) { 
                        //                        if (isNaN(ob_qty)) {
                        //                            txt[7].value = '';
                        //                            txt[4].value = '';
                        //                        }
                        //                        else {
                        //                            txt[7].value = ob_qty;
                        //                            txt[4].value = '';
                        //                         

                        //                        }
                        //                    }
                    }
                }



                lb[lb.length - 8].innerHTML = total.toFixed(2);
                lb[lb.length - 6].innerHTML = total.toFixed(2);
                lb[lb.length - 11].innerHTML = OB.toFixed(2);
          

            });
            </script>
     <script language="javascript" type="text/javascript">
         $(document).ready(function () {

            //For navigating using left and right arrow of the keyboard
            $("input[type='text'], select").keydown(
  function (event) {
      if ((event.keyCode == 39) || (event.keyCode == 9 && event.shiftKey == false)) {
          var inputs = $(this).parents("form").eq(0).find("input:not(:disabled)[type='text'], select");
          var idx = inputs.index(this);
          if (idx == inputs.length - 1) {
              inputs[0].select()
          } else {
              $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                  $(this).attr("style", "BACKGROUND-COLOR: white; ");
              });

              inputs[idx + 1].focus();
          }
          return false;
      }
      if ((event.keyCode == 37) || (event.keyCode == 9 && event.shiftKey == true)) {
          var inputs = $(this).parents("form").eq(0).find("input:not(:disabled)[type='text'], select");
          var idx = inputs.index(this);
          if (idx > 0) {
              $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                  $(this).attr("style", "BACKGROUND-COLOR: white; ");
              });


              inputs[idx - 1].focus();
          }
          return false;
      }
  });
            //For navigating using up and down arrow of the keyboard
            $("input[type='text'], select").keydown(
  function (event) {
      if ((event.keyCode == 40)) {
          if ($(this).parents("tr").next() != null) {
              var nextTr = $(this).parents("tr").next();
              var inputs = $(this).parents("tr").eq(0).find("input[type='text'], select");
              var idx = inputs.index(this);
              nextTrinputs = nextTr.find("input[type='text'], select");
              if (nextTrinputs[idx] != null) {
                  $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                      $(this).attr("style", "BACKGROUND-COLOR: white; ");
                  });

                  nextTrinputs[idx].focus();
              }
          }
          else {
              $(this).focus();
          }
      }
      if ((event.keyCode == 38)) {
          if ($(this).parents("tr").next() != null) {
              var nextTr = $(this).parents("tr").prev();
              var inputs = $(this).parents("tr").eq(0).find("input[type='text'], select");
              var idx = inputs.index(this);
              nextTrinputs = nextTr.find("input[type='text'], select");
              if (nextTrinputs[idx] != null) {
                  $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                      $(this).attr("style", "BACKGROUND-COLOR: white;");
                  });

                  nextTrinputs[idx].focus();
              }
              return false;
          }
          else {
              $(this).focus();
          }
      }
  });
        });    </script>
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
        .hideGridColumn {
            display: none;
        }

        .GVFixedHeader {
            font-weight: bold;
            background-color: Green;
            position: relative;
            top: expression(this.parentNode.parentNode.parentNode.scrollTop-1);
        }

        .GVFixedFooter {
            font-weight: bold;
            background-color: Green;
            position: relative;
            bottom: expression(getScrollBottom(this.parentNode.parentNode.parentNode.parentNode));
        }
        .gridtable {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }

            .gridtable th {
                border-width: 1px;
                border-style: solid;
                border-color: #666666;
                font-size: large;
                color: Red;
            }

            .gridtable td {
                border-color: #666666;
                background-color: #ffffff;
            }
            .savebutton {
    width: 100px;
    height: 32px;
    border-radius: 8px;
    background-image: linear-gradient(to top, #0077ff 0%, #28b5e0 100%);
    cursor: pointer;
    border: 0px;
    color: #ffffff;
    font-size: 14px;
    font-weight: 600;
    margin: 0 3px;
    padding: 0px;
    margin-top: 5px;
    margin-bottom: 5px;
}
            .savebtn {
    width: 100px;
    height: 32px;
    border-radius: 8px;
    background-image: linear-gradient(to top, #0077ff 0%, #28b5e0 100%);
    cursor: pointer;
    border: 0px;
    color: #ffffff;
    font-size: 14px;
    font-weight: 600;
    margin: 0 3px;
    padding: 0px;
    margin-top: 5px;
    margin-bottom: 5px;
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
    
    <script language="javascript" type="text/javascript">
        function getScrollBottom(p_oElem) {
            return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
        }
    </script>
</head>
<body style="background:white">
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>
        <br />
        <div>
            <center>
                <asp:Label ID="lblHead" runat="server" Font-Underline="True" Font-Bold="True" Font-Names="Verdana"
                    Font-Size="11pt"></asp:Label>
            </center>
            <br />
            <asp:HiddenField ID="hdnStatus" runat="server" />
            <asp:HiddenField ID="hdnPrev" runat="server" />
            <asp:Panel ID="pnl" runat="server" Visible="false">
            <table width="80%">
                <tr>
                    <td align="right">
                        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Bold="true" ForeColor="Red"
                            Text="Stockist Name:" Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblStkName" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Bold="true" ForeColor="Red"
                            Text="HQ:" Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblStkHq" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Bold="true" ForeColor="Red"
                            Text="State:" Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblState" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblsub" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblSF" runat="server" Font-Names="Verdana" Font-Bold="true" ForeColor="Red"
                            Text="FieldForce Name:" Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label33" runat="server" Font-Names="Verdana" Font-Bold="true" ForeColor="Red"
                            Text="HQ:" Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblHQ" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label34" runat="server" Font-Names="Verdana" Font-Bold="true" ForeColor="Red"
                            Text="Desig:" Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblDesig" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblSt" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
                </asp:Panel>
            <center>
             <asp:Label ID="lblstk" runat="server" Font-Bold="True" ForeColor="BlueViolet" Font-Names="Verdana" Font-Size="12pt"></asp:Label>
            </center>
            <br />
            <center>
                <%--     <asp:Panel runat="server" ID="pnlContainer" ScrollBars="Auto" Height="350px" Width="500">--%>
                <%--    <div style="background-color: Green; height: 30px; width: 1200px; margin: 0; padding: 0">
                <table cellspacing="0" cellpadding="0" rules="all" border="1" id="Table1" style="font-family: Arial;
                    font-size: 10pt; width: 1200px; border-collapse: collapse; color: White; height: 100%;
                    border-style: solid; border-width: 1px; border-color: Black">
                    <tr>
                        <td style="width: 100px; text-align: center; background-color: #336699;">
                           S.No
                        </td>
                        <td style="width: 300px; text-align: center; background-color: #336699;">
                           Product Name
                        </td>
                        <td style="width: 100px; text-align: center; background-color: #336699;">
                          Pack
                        </td>
                        <td style="width: 100px; text-align: center; background-color: #336699;">
                            Rate
                        </td>
                        <td style="width: 100px; text-align: center; background-color: #336699;">
                            OB
                        </td>
                    <td style="width: 100px; text-align: center; background-color: #336699;">
                           Primary
                        </td>
                         <td style="width: 100px; text-align: center; background-color: #336699;">
                           Receipt
                        </td>
                            <td style="width: 100px; text-align: center; background-color: #336699;">
                           Sale
                        </td>
                         <td style="width: 100px; text-align: center; background-color: #336699;">
                          Sale Ret
                        </td>
                         <td style="width: 100px; text-align: center; background-color: #336699;">
                         CB
                        </td>
                         <td style="width: 100px; text-align: center; background-color: #336699;">
                          Transit
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 500px; width: 1200px; overflow: auto;">--%>
                <asp:GridView ID="grdProd" runat="server" Width="900px" Height="100%" HorizontalAlign="Center"
                    AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" OnRowDataBound="grdProd_RowDataBound"
                    PagerStyle-CssClass="pgr" ShowFooter="true" AlternatingRowStyle-CssClass="alt" EmptyDataText="Rate Not Updated">
                    <RowStyle Wrap="false" />
                    <PagerStyle CssClass="pgr"></PagerStyle>
                    <SelectedRowStyle BackColor="BurlyWood" />
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="100px">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#   Bind("SlNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prod_Code" Visible="false">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblProd_Code" runat="server" Text='<%#   Bind("Product_Detail_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="300px" HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblProdName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                            <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text="Total Value"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pack" ItemStyle-Width="100px">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSaleUnit" runat="server" Text='<%#Bind("Product_Sale_Unit")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate" ItemStyle-Width="100px">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblRate" runat="server" Text='<%#Bind("Distributor_Price")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OB" ItemStyle-Width="100px">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtOB" Width="40px" Style="text-align: right;" MaxLength="6" CssClass="TEXTAREA"
                                    Text='<%#(Eval("1_AAT"))%>' runat="server" onkeypress="CheckNumeric(event);" onkeyup="CalculateOB();"></asp:TextBox>
                                <asp:HiddenField ID="hdnOb" runat="server" Value='<%# Bind("1_AAT") %>' />
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                            <FooterTemplate>
                                <asp:Label ID="lblOBqty" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Primary" ItemStyle-Width="100px">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtPrimary" Enabled="false" Width="40px" Style="text-align: right;"
                                    MaxLength="6" CssClass="TEXTAREA" Text='<%#(Eval("1_ABT"))%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                            <FooterTemplate>
                                <asp:Label ID="lblPrimqty" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Receipt" ItemStyle-Width="100px">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtRec" Enabled="false" Width="60px" Style="text-align: right;"
                                    MaxLength="6" CssClass="TEXTAREA" Text='<%#(Eval("1_ACT"))%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                            <FooterTemplate>
                                <asp:Label ID="lblRecqty" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sale" ItemStyle-Width="100px">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtSale" Enabled="false" Width="60px" Style="text-align: right;"
                                    MaxLength="6" CssClass="TEXTAREA" Text='<%#(Eval("1_ADT"))%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                            <FooterTemplate>
                                <asp:Label ID="lblSaleqty" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sale" ItemStyle-Width="100px" HeaderStyle-CssClass="hideGridColumn"
                            ItemStyle-CssClass="hideGridColumn">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtSale2" Enabled="false" Width="60px" Style="text-align: right;"
                                    MaxLength="6" CssClass="TEXTAREA" Text='<%#(Eval("1_ADDT"))%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" CssClass="hideGridColumn" />
                            <FooterTemplate>
                                <asp:Label ID="lblSaleqty2" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sale Value" ItemStyle-Width="100px" 
                            >
                            <ItemStyle BorderStyle="Solid" BackColor="LightYellow" ForeColor="BlueViolet" Font-Bold="true" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblsal"  runat="server"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                            <FooterTemplate>
                                <asp:Label ID="lblSaleval" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sale Ret" ItemStyle-Width="100px">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"  HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtSRet" Enabled="false" Width="60px" Style="text-align: right;"
                                    MaxLength="6" CssClass="TEXTAREA" Text='<%#(Eval("1_AET"))%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                            <FooterTemplate>
                                <asp:Label ID="lblSalRetqty" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CB" ItemStyle-Width="100px">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtCB" Width="60px" Style="text-align: right;" MaxLength="6" CssClass="TEXTAREA"
                                    Text='<%#(Eval("1_AFT"))%>' runat="server" onkeypress="CheckNumeric(event);" onkeyup="CalculateTotals();"></asp:TextBox>
                                <asp:HiddenField ID="hdnCb" runat="server" Value='<%# Bind("1_AFT") %>' />
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                            <FooterTemplate>
                                <asp:Label ID="lblCBqty" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="CB Value" ItemStyle-Width="100px" 
                            >
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" ForeColor="BlueViolet" Font-Bold="true" BackColor="LightYellow" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblCb"  runat="server"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                            <FooterTemplate>
                                <asp:Label ID="lblCbVal" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transit" ItemStyle-Width="100px">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtTran" Enabled="false" Width="60px" Style="text-align: right;"
                                    MaxLength="6" CssClass="TEXTAREA" Text='<%#(Eval("1_AGT"))%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                            <FooterTemplate>
                                <asp:Label ID="lblTransqty" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transit2" ItemStyle-Width="100px" HeaderStyle-CssClass="hideGridColumn"
                            ItemStyle-CssClass="hideGridColumn">
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txttrans2" Enabled="false" Width="60px" Style="text-align: right;"
                                    MaxLength="6" CssClass="TEXTAREA" Text='<%#(Eval("1_AHT"))%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" CssClass="hideGridColumn" />
                            <FooterTemplate>
                                <asp:Label ID="lblTransqty2" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                       <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                </asp:GridView>
                <%--   </div>--%>
            </center>
            <br />
            <center>
                <asp:Button ID="btnBackBill" runat="server" Text="Back to Bill Selection" Width="180px" CssClass="savebutton" OnClick="btnBackBill_Click" />
                &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDraft" runat="server" Text="Draft Save" Width="90px" Height="25px"
                Visible="false" BackColor="LightBlue" />
                &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnApprove" runat="server" Text="Submit" Width="120px" CssClass="savebtn"  
                OnClick="btnApprove_Click"   />
                &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="80px" CssClass="savebutton" Visible="false" OnClick="btnSubmit_Click" />
                &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReject" runat="server" Text="Reject" Width="80px" Height="25px" Visible="false"
                BackColor="LightBlue" OnClick="btnReject_Click" />

                 <asp:Label ID="lblRejectReason" Text="Reject Reason : " Visible="false" SkinID="lblMand" runat="server"></asp:Label>
            &nbsp;
        <asp:TextBox ID="txtreject" runat="server" TextMode="MultiLine" BorderStyle="Solid" BorderColor="Gray" Visible="false"  Height="70px" Width="350px"></asp:TextBox>
          &nbsp
            <asp:Button ID="btnCReject" CssClass="BUTTON" Width="140px" runat="server" 
                    Visible="false" OnClientClick="return validate();"
                Text="Confirm Reject" onclick="btnCReject_Click"   />
            </center>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
