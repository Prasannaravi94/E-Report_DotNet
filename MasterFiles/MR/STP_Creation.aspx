<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STP_Creation.aspx.cs" Inherits="MasterFiles_MR_STP_Creation" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../../css/MR.css" />
    <link href="../../css/stylesheet.css" rel="stylesheet" type="text/css" />
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-2.2.3.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

    <style type="text/css">
        
        input[type="checkbox"]:checked {
  box-shadow: 0 0 0 1px hotpink;
    height: 18px;
  width: 18px;
  cursor: pointer;
  

}

input[type="checkbox"] 
{
    cursor: pointer;
}


        
          .GridStyle
{
    border: 3px solid rgb(137, 231, 155);
    background-color: White;
    font-family: arial;
    font-size: 11px;
    border-collapse: collapse;
    margin-bottom: 0px;
    height:20px;
}
.GridStyle tr
{
    border: 1px solid rgb(217, 231, 255);
    color: Black;
  
}
/* Your grid header column style */
.GridStyle th
{
    background-color: rgb(217, 231, 155);
    border: none;
    text-align: center;
    font-weight: bold;
    font-size: 15px;
    padding: 4px;
    color:Black;
}
/* Your grid header link style */
.GridStyle tr th a,.GridStyle tr th a:visited
{
        color:Black;
}
.GridStyle tr th, .GridStyle tr td table tr td
{
    border: none;
}

.GridStyle td
{
    border-bottom: 1px solid rgb(217, 231, 255);
    padding: 2px;
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
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}

@-webkit-keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}

@keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}
 .blink {
  animation: blink-animation 1s steps(5, start) infinite;
  -webkit-animation: blink-animation 1s steps(5, start) infinite;
}
@keyframes blink-animation {
  to {
    visibility: hidden;
  }
}
@-webkit-keyframes blink-animation {
  to {
    visibility: hidden;
  }
}

.div_fixed_Submit
        {
            position: fixed;
            top: 350px;
            right: 50px;
        }
        
         .div_fixed_List_Totss
        {
            position: fixed;
            top: 90px;
            right: 1px;
        }
    </style>
    <script type="text/javascript">

        function LoadFieldForce(button) {

            var cell = button.parentNode.parentNode.parentNode.parentNode;
            var row = button.parentNode.parentNode;
            var checkbox = button.getElementsByTagName("input");


           
            

            var objTextBox = "";
            arrayy = [];



            for (var i = 0; i < checkbox.length; i++) {

                if (checkbox[i].checked) {

                    var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');

                    arrayy.push(chkBoxText[0].innerText);



                }

            }

            GetChildControl(row, "txtterr").value = arrayy;



            function GetChildControl(element, id) {
                var child_elements = element.getElementsByTagName("*");
                for (var i = 0; i < child_elements.length; i++) {
                    if (child_elements[i].id.indexOf(id) != -1) {
                        return child_elements[i];

                    }
                }

            };


            firstload()



//             var grid = document.getElementById('<%= grdSTP.ClientID %>');

//                var row = <%=grdSTP.Rows.Count %>    

//            count_terr=[];

//             if (grid != null) {
//         

//                 var allcnt = grid.getElementsByTagName("tr");
//                 var ddlwrktype = '';
//                 var plan_name = '';
//                 var ter_name = '';
//                 var hdndr = '';
//                 var hdnchem='';

//                 var terrnam_count='';
//                 var doctor_count ='';
//                 var chemistt_count ='';

//                
//               
//                 for (i = 2; i <= row+1; i++) {

//                     if (i > 9) {

//                       
//                         plan_name = document.getElementById('grdSTP_ctl' + i + '_lblday_plan_name').innerHTML;
//                         ter_name = document.getElementById('grdSTP_ctl' + i + '_txtterr');
//                         hdndr = document.getElementById('grdSTP_ctl' + i + '_hdndr');
//                         hdnchem=document.getElementById('grdSTP_ctl' + i + '_hdnchem');
//                         

//                         terrnam_count =terrnam_count+','+ter_name.value +',';
//                         doctor_count =doctor_count +','+hdndr.value;
//                         chemistt_count=chemistt_count+','+hdnchem.value;
//                     }
//                     else {

//                       
//                         plan_name = document.getElementById('grdSTP_ctl0' + i + '_lblday_plan_name').innerHTML;
//                         ter_name = document.getElementById('grdSTP_ctl0' + i + '_txtterr');
//                         hdndr = document.getElementById('grdSTP_ctl0' + i + '_hdndr');
//                         hdnchem = document.getElementById('grdSTP_ctl0' + i + '_hdnchem');
//                       
//                         terrnam_count =terrnam_count+','+ter_name.value +',';
//                         doctor_count =doctor_count +','+hdndr.value;
//                         chemistt_count=chemistt_count+','+hdnchem.value;
//                     }
//                 }

//                   terrnam_count = Array.from(new Set(terrnam_count.split(','))).toString();
//              
//               terrnam_count= terrnam_count.replace(/^,|,$/g, '');
//               // alert(terrnam_count); 
//                var select_terr ='';                         
//                          
//                var elements = terrnam_count.split(',');
//                 if(terrnam_count !=''){

//                  select_terr =elements.length;
//           
//              //  alert(elements.length);
//                }
//                else 
//                {
//                select_terr='0';
//                 // alert(elements='0');
//                }
//         

//            doctor_count = Array.from(new Set(doctor_count.split(','))).toString();

//            
//              
//               doctor_count= doctor_count.replace(/^,|,$/g, '');
//               // alert(doctor_count);  
//               
//               var select_doctt ='';                        
//                          
//                var elements2 = doctor_count.split(',');
//                 if(doctor_count !=''){
//           
//           select_doctt=elements2.length;
//               // alert(elements2.length);
//                }
//                else 
//                {
//                select_doctt=0;
//                 // alert(elements2='0');
//                }

//                chemistt_count =Array.from(new Set(chemistt_count.split(','))).toString();
//                chemistt_count=chemistt_count.replace(/^,|,$/g, '');

//                var select_chemiss='';

//                var elements3=chemistt_count.split(',');
//                if(chemistt_count !='')
//                {
//                select_chemiss=elements3.length;
//                }
//                else 
//                {
//                select_chemiss='0';
//                }

//            

//                var totdoctorcode=$('#totdoctorcode').val();
//                var totterrcode =$('#totterrcode').val();
//                var totchemcode=$('#totchemcode').val();

//            

//                 document.getElementById('totterr').innerHTML=totterrcode;
//                 document.getElementById('terrselected').innerHTML=select_terr;
//                 document.getElementById('terr_notselect').innerHTML =totterrcode-select_terr;

//                 document.getElementById('totdoctorr').innerHTML =totdoctorcode;
//                 document.getElementById('doctselected').innerHTML=select_doctt;
//                 document.getElementById('doct_notseleceted').innerHTML=totdoctorcode-select_doctt;

//                 document.getElementById('totchemistt').innerHTML=totchemcode;
//                 document.getElementById('chemselected').innerHTML=select_chemiss;
//                 document.getElementById('chem_notselected').innerHTML=totchemcode-select_chemiss;

//            }
        }
        
    </script>
    <script type="text/javascript">
        function ShowModalPopup($x) {


      
            //        var hhh = val.id;
            //        var ggg = hhh.indexOf("_");

            //        alert(hhh);
            //        alert(ggg);


            var idString = $x.id;
            var $i1 = idString.indexOf("_");
            $i1 = $i1 + 4;
            var $i2 = idString.indexOf("_", $i1);
            var cnt = 0;
            var index = '';

            if ((idString.substring($i1, $i2) - 1) < 9) {

                var cIdprev = parseInt(idString.substring($i1, $i2));


                index = cnt.toString() + cIdprev.toString();




            }
            else {

                var cIdprev = parseInt(idString.substring($i1, $i2));
                index = cIdprev;


            }



               var grid = document.getElementById('<%= grdSTP.ClientID %>');

                var row = <%=grdSTP.Rows.Count %> ; 

            count_terr=[];

             if (grid != null) {
             
         

                 var allcnt = grid.getElementsByTagName("tr");
                 var ddlwrktype = '';
                 var plan_name = '';
                 var ter_name = '';
                 var hdndr = '';
                 var hdnchem='';

                 var terrnam_count='';
                 var doctor_count ='';
                 var chemistt_count ='';
                 var doctor_count_new=''

                
               
                 for (i = 2; i <= row+1; i++) {

                     if (i > 9) {

                       
                         plan_name = document.getElementById('grdSTP_ctl' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl' + i + '_hdndr');
                         hdnchem=document.getElementById('grdSTP_ctl' + i + '_hdnchem');
                         

                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;

                         doctor_count_new = doctor_count +','+hdndr.value;
                     }
                     else {

                       
                         plan_name = document.getElementById('grdSTP_ctl0' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl0' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl0' + i + '_hdndr');
                         hdnchem = document.getElementById('grdSTP_ctl0' + i + '_hdnchem');
                       
                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;

                          doctor_count_new = doctor_count +','+hdndr.value;
                     }
                 }
                 }


               

            var terrval = "";
            var day = "";


            terrval = document.getElementById("grdSTP_ctl" + index + "_txtterr").value;





            document.getElementById("grdSTP_ctl" + index + "_terr_name").innerHTML = terrval;

            if(terrval !='')
            {
            
            

            $.ajax({
                type: "POST",
                url: "STP_Creation.aspx/GetCustomersdr",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ "teritoryname": terrval }),
                dataType: "json",
                async: true,
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);

                },
                error: function (response) {
                    alert(response.d);

                }
            });

            function OnSuccess(response) {
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var customers = xml.find("Table");
                //  var repeatColumns = parseInt("grdTP_ctl" + index + "_CheckBoxList1.RepeatColumns == 0 ? 1 : grdTP_ctl" + index + "_CheckBoxList1.RepeatColumns");



                var row = $("[id*=grdSTP_ctl" + index + "_CheckBoxList1] tr:last-child").clone(true);

                $("[id*=grdSTP_ctl" + index + "_CheckBoxList1] tr").remove();

                var colCounter = 1;
                var colLimit = 8;



              
                

                $.each(customers, function () {


                    var customer = $(this);

                 var patchname= getpatchname(customer.find("ListedDrCode").text());

                 //alert(patchname);


                    $("input", row).val($(this).find("ListedDrCode").text());
                   var re = /\((.*)\)/;

                  var txt =customer.find("ListedDr_Name").text();

                   var visitt =txt.match(re)[1];

                  
                 
                    var counnt = 0;


                    str_arr4 = doctor_count_new.split(',');
                    for (i = 0; i < str_arr4.length; i++) {
                    if(str_arr4[i] !=''){
                        if ($.trim(str_arr4[i]) === customer.find("ListedDrCode").text()) {
                            counnt = counnt + 1;

                         
                            }
                        }
                    }

                    var bal =parseInt(visitt)-parseInt(counnt) ;


                     var savedvalue22 = document.getElementById("grdSTP_ctl" + index + "_hdndr").value;
                     var found2 = savedvalue22.split(",").indexOf(customer.find("ListedDrCode").text()) > -1;

                    
                
                
                 if(bal ==0)
                 {
                 
                        if (found2 == true) {
                       $("label", row).html($(this).find("ListedDr_Name").text() + ' - ' + counnt +' ('+patchname+')'+ ' - '+bal).css('text-decoration','none');
                          $("[id*=grdSTP_ctl" + index + "_CheckBoxList1] tbody").append(row);
                    row = $("[id*=grdSTP_ctl" + index + "_CheckBoxList1] tr:last-child").clone(true);

                   
                      
                       }
                       else 
                       {

                        $("label", row).html($(this).find("ListedDr_Name").text() + ' - ' + counnt +' ('+patchname+')'+ ' - '+bal).css('text-decoration','line-through');
                           $("[id*=grdSTP_ctl" + index + "_CheckBoxList1] tbody").append(row);
                    row = $("[id*=grdSTP_ctl" + index + "_CheckBoxList1] tr:last-child").clone(true);

                      //$('input[id*=grdSTP_ctl' + index + '_CheckBoxList1]:checkbox').remove();

             
                    //alert(customer.find("ListedDrCode").text());

                    //  $('input[id*=grdSTP_ctl' + index + '_CheckBoxList1]:checkbox').attr('disabled', 'disabled');
                    

                     // 
                       }
                     

                     
                 }
                 else 
                 {
    
                   
                  if (found2 == true) {
                 
                
                       $("label", row).html($(this).find("ListedDr_Name").text() + ' - ' + counnt +' ('+patchname+')'+ ' - '+bal).css('text-decoration','none');
                         $("[id*=grdSTP_ctl" + index + "_CheckBoxList1] tbody").append(row);
                    row = $("[id*=grdSTP_ctl" + index + "_CheckBoxList1] tr:last-child").clone(true);

                
                       }
                       else 
                       {
                     
                        
                        $("label", row).html($(this).find("ListedDr_Name").text() + ' - ' + counnt +' ('+patchname+')'+ ' - '+bal).css('text-decoration','none');
                          $("[id*=grdSTP_ctl" + index + "_CheckBoxList1] tbody").append(row);
                    row = $("[id*=grdSTP_ctl" + index + "_CheckBoxList1] tr:last-child").clone(true);

              
                       }
                   

                  }

                 


                    var checkboxx = document.getElementById("grdSTP_ctl" + index + "_CheckBoxList1");
                    var checkoptions = checkboxx.getElementsByTagName('input');
                    var listSelected = checkboxx.getElementsByTagName('label');

                    for (i = 0; i < checkoptions.length; i++) {

                        var savedvalue = document.getElementById("grdSTP_ctl" + index + "_hdndr").value
                        var found = savedvalue.split(",").indexOf(customer.find("ListedDrCode").text()) > -1;



                        if (found == true) {

                            if (customer.find("ListedDrCode").text() == checkoptions[i].value) {

                                checkoptions[i].checked = true;
                            
                         //  checkoptions[i].disabled =false;
                                                  
                               
                            }
                            else 
                            {
                            
                            }

                        }

                    }
                });

            }




            chem()



            function chem() {


                $.ajax({
                    type: "POST",
                    url: "STP_Creation.aspx/GetCustomerschem",
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ "teritoryname": terrval }),
                    dataType: "json",
                    async: true,
                    success: OnSuccess,
                    failure: function (response) {
                        alert(response.d);

                    },
                    error: function (response) {
                        alert(response.d);

                    }
                });

                function OnSuccess(response) {

                    var xmlDoc = $.parseXML(response.d);
                    var xml = $(xmlDoc);
                    var customers = xml.find("Table");

                    var row = $("[id*=grdSTP_ctl" + index + "_CheckBoxList2] tr:last-child").clone(true);

                    $("[id*=grdSTP_ctl" + index + "_CheckBoxList2] tr").remove();

                    $.each(customers, function () {
                        var customer = $(this);

                        $("input", row).val($(this).find("Chem_Code").text());
                        $("label", row).html($(this).find("Chem_Name").text());
                        $("[id*=grdSTP_ctl" + index + "_CheckBoxList2] tbody").append(row);
                        row = $("[id*=grdSTP_ctl" + index + "_CheckBoxList2] tr:last-child").clone(true);



                        var checkboxxx = document.getElementById("grdSTP_ctl" + index + "_CheckBoxList2");
                        var checkoptionss = checkboxxx.getElementsByTagName('input');
                        var listSelectedd = checkboxxx.getElementsByTagName('label');

                        for (i = 0; i < checkoptionss.length; i++) {

                            var savedvaluee = document.getElementById("grdSTP_ctl" + index + "_hdnchem").value
                            var foundd = savedvaluee.split(",").indexOf(customer.find("Chem_Code").text()) > -1;


                            if (foundd == true) {

                                if (customer.find("Chem_Code").text() == checkoptionss[i].value) {
                                    checkoptionss[i].checked = true;
                                }
                            }

                        }

                    });
                }
            }
            }





            $find("grdSTP_ctl" + index + "_mpe").show();
            return false;
        }


    </script>
    <script type="text/javascript">

        function GetSelectedRow(lnk) {
      


            var idString = lnk.id;

            var $i1 = idString.indexOf("_");

            $i1 = $i1 + 4;

            var $i2 = idString.indexOf("_", $i1);

            var cnt = 0;
            var index = '';


            if ((idString.substring($i1, $i2) - 1) < 9) {

                var cIdprev = parseInt(idString.substring($i1, $i2));


                index = cnt.toString() + cIdprev.toString();




            }
            else {

                var cIdprev = parseInt(idString.substring($i1, $i2));
                index = cIdprev;


            }

            arr = [];

            aarr_namee = [];

            var checkboxx = document.getElementById("grdSTP_ctl" + index + "_CheckBoxList1");
            var checkoptions = checkboxx.getElementsByTagName('input');
            var listSelected = checkboxx.getElementsByTagName('label');

            var last_val = '';
            for (i = 0; i < checkoptions.length; i++) {

                if (checkoptions[i].checked) {
                 

//                   var lastvalue= listSelected[i].innerHTML;

//                    var lastChar = lastvalue[lastvalue.length -1];

//                    if(lastChar !=0)
//                    {

                    arr.push(checkoptions[i].value);
                    aarr_namee.push(checkoptions[i].parentNode.getElementsByTagName('span')[0].innerHTML);
                   // }
                }

            }


       

            document.getElementById("grdSTP_ctl" + index + "_hdndr").value = arr;
            document.getElementById("grdSTP_ctl" + index + "_hdndr_name").value = aarr_namee;

                         var mystring = arr+',';                       
                  
                     var countt=0;

                     var strr = mystring;
                     var str_array = strr.split(',');

                    for(var i = 0; i < str_array.length; i++) {
                         // Trim the excess whitespace.

                        if(str_array[i] !=''){
                        str_array[i] = str_array[i].replace(/^\s*/, "").replace(/\s*$/, "");
                         countt = countt+1;
                            }
                         // Add additional code here, such as:
  
                            }


          





            arr2 = [];

            arrarr_nameee = [];

            var checkboxx2 = document.getElementById("grdSTP_ctl" + index + "_CheckBoxList2");
            var checkoptions2 = checkboxx2.getElementsByTagName('input');
            var listSelected2 = checkboxx2.getElementsByTagName('label');

            var last_val = '';
            for (i = 0; i < checkoptions2.length; i++) {

                if (checkoptions2[i].checked) {
                    //                last_val += checkoptions[i].value;
                    //           

                    arr2.push(checkoptions2[i].value);
                    arrarr_nameee.push(checkoptions2[i].parentNode.getElementsByTagName('span')[0].innerHTML);
                }

            }


            document.getElementById("grdSTP_ctl" + index + "_hdnchem").value = arr2;
            document.getElementById("grdSTP_ctl" + index + "_hdnchem_name").value = arrarr_nameee;


              var mystring22 = arr2+',';                       
                  
                     var countt2=0;

                     var strr2 = mystring22;
                     var str_array2 = strr2.split(',');

                    for(var i = 0; i < str_array2.length; i++) {
                         // Trim the excess whitespace.

                        if(str_array2[i] !=''){
                        str_array2[i] = str_array2[i].replace(/^\s*/, "").replace(/\s*$/, "");
                         countt2 = countt2+1;
                            }
                         // Add additional code here, such as:
  
                            }
                          


   document.getElementById("grdSTP_ctl" + index + "_lbldrchem").innerHTML=countt+' / '+ countt2;

            
             var grid = document.getElementById('<%= grdSTP.ClientID %>');

                var row = <%=grdSTP.Rows.Count %>  ;  

            count_terr=[];

             if (grid != null) {
             
         

                 var allcnt = grid.getElementsByTagName("tr");
                 var ddlwrktype = '';
                 var plan_name = '';
                 var ter_name = '';
                 var hdndr = '';
                 var hdnchem='';

                 var terrnam_count='';
                 var doctor_count ='';
                 var chemistt_count ='';
                 var doctor_count_new=''

                
               
                 for (i = 2; i <= row+1; i++) {

                     if (i > 9) {

                       
                         plan_name = document.getElementById('grdSTP_ctl' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl' + i + '_hdndr');
                         hdnchem=document.getElementById('grdSTP_ctl' + i + '_hdnchem');
                         

                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;

                         doctor_count_new = doctor_count +','+hdndr.value;
                     }
                     else {

                       
                         plan_name = document.getElementById('grdSTP_ctl0' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl0' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl0' + i + '_hdndr');
                         hdnchem = document.getElementById('grdSTP_ctl0' + i + '_hdnchem');
                       
                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;

                          doctor_count_new = doctor_count +','+hdndr.value;
                     }
                 }

                   terrnam_count = Array.from(new Set(terrnam_count.split(','))).toString();
              
               terrnam_count= terrnam_count.replace(/^,|,$/g, '');
               // alert(terrnam_count); 
                var select_terr ='';                         
                          
                var elements = terrnam_count.split(',');
                 if(terrnam_count !=''){

                  select_terr =elements.length;
           
              //  alert(elements.length);
                }
                else 
                {
                select_terr='0';
                 // alert(elements='0');
                }
         

            doctor_count = Array.from(new Set(doctor_count.split(','))).toString();

            
              
               doctor_count= doctor_count.replace(/^,|,$/g, '');
               // alert(doctor_count);  
               
               var select_doctt ='';                        
                          
                var elements2 = doctor_count.split(',');
                 if(doctor_count !=''){
           
           select_doctt=elements2.length;
               // alert(elements2.length);
                }
                else 
                {
                select_doctt=0;
                 // alert(elements2='0');
                }

                chemistt_count =Array.from(new Set(chemistt_count.split(','))).toString();
                chemistt_count=chemistt_count.replace(/^,|,$/g, '');

                var select_chemiss='';

                var elements3=chemistt_count.split(',');
                if(chemistt_count !='')
                {
                select_chemiss=elements3.length;
                }
                else 
                {
                select_chemiss='0';
                }

            

                var totdoctorcode=$('#totdoctorcode').val();
                var totterrcode =$('#totterrcode').val();
                var totchemcode=$('#totchemcode').val();

            

                 document.getElementById('totterr').innerHTML=totterrcode;
                 document.getElementById('terrselected').innerHTML=select_terr;
                 document.getElementById('terr_notselect').innerHTML =totterrcode-select_terr;

                 document.getElementById('totdoctorr').innerHTML =totdoctorcode;
                 document.getElementById('doctselected').innerHTML=select_doctt;
                 document.getElementById('doct_notseleceted').innerHTML=totdoctorcode-select_doctt;

                 document.getElementById('totchemistt').innerHTML=totchemcode;
                 document.getElementById('chemselected').innerHTML=select_chemiss;
                 document.getElementById('chem_notselected').innerHTML=totchemcode-select_chemiss;




            }




                     var grdcat = document.getElementById('<%= grdcat.ClientID %>');
                              var rowcat = <%=grdcat.Rows.Count %> ;

                              var hdndcatcode='';

                    
                             
                              for (k = 2; k <= rowcat+1; k++) {

                              
                                 if (k > 9) {

                                   hdndcatcode=  document.getElementById('grdcat_ctl' + k + '_hdndcatcode').value;

                                 }
                                 else 
                                 {
                                  hdndcatcode=  document.getElementById('grdcat_ctl0' + k + '_hdndcatcode').value;
                                 }

                               

            
                   var grdvst = document.getElementById('<%= grdvst.ClientID %>');
                              var rowvst = <%=grdvst.Rows.Count %> ;

                          



                              var drcodevst='';
                              var drnamevst ='';
                              var visit_cnt ='';
                              var cntt2=0;
                              var txtDoc_Cat_Code='';


                            


                              for (j = 2; j <= rowvst+1; j++) {

                                

                                   if (j > 9) {

                                 drcodevst=  document.getElementById('grdvst_ctl' + j + '_txtListedDrCode').value;
                                 visit_cnt=  document.getElementById('grdvst_ctl' + j + '_txtNo_of_Visit').value;
                                  drnamevst=  document.getElementById('grdvst_ctl' + j + '_txtListedDr_Name').value;
                                  txtDoc_Cat_Code =document.getElementById('grdvst_ctl' + j + '_txtDoc_Cat_Code').value;
                              
                                    }

                                    else 
                                    {
                                      drcodevst=  document.getElementById('grdvst_ctl0' + j + '_txtListedDrCode').value;
                                      visit_cnt=  document.getElementById('grdvst_ctl0' + j + '_txtNo_of_Visit').value;
                                      drnamevst=  document.getElementById('grdvst_ctl0' + j + '_txtListedDr_Name').value;
                                       txtDoc_Cat_Code =document.getElementById('grdvst_ctl0' + j + '_txtDoc_Cat_Code').value;
                                        
                                    }

                                   
                 if(hdndcatcode ==txtDoc_Cat_Code){

                                         
                        str_arr = doctor_count_new.split(',');
                       for (i = 0; i < str_arr.length; i++) { 
                         if($.trim(str_arr[i]) === drcodevst){
                             cntt2 =cntt2+1;
                         }
                      }

                      }

                    }

                   if (k > 9) {

                         document.getElementById('grdcat_ctl' + k + '_lbldrcntt').innerHTML =cntt2;

                                 }
                                 else 
                                 {
                               document.getElementById('grdcat_ctl0' + k + '_lbldrcntt').innerHTML = cntt2;
                                 }

                    }

  $find("grdSTP_ctl" + index + "_mpe").hide();
 
  
   return false;

        }
    </script>
</head>
<body oncontextmenu="return false;">
    <form id="form1" runat="server">
    <div>
        <ucl:Menu1 ID="menu" runat="server" />
        <center>
        <div>
        <asp:Label ID="lblHead" runat="server" Text="STP CREATION " Font-Underline="true" Font-Bold="true" Font-Size="Medium"
                        ForeColor="Green" Font-Names="Verdana"></asp:Label>
        </div>

            <div class="blink_me" style="float: right; width: 20%;text-decoration: underline">
        <%--   <a style="font-weight:bold; width:30px;"; href="#" onclick='openWindow("<%# Eval("Code") %>");'>STP View</a>--%>

           <asp:LinkButton ID="lnkcount2" runat="server" CausesValidation="False" Text="STP View" ForeColor="Magenta" 
                                            Font-Bold="true" OnClientClick="return popdcr()">
                                        </asp:LinkButton>
        </div>
    
            
            <div class="div_fixed_List_Totss" style="width: 100%">
            <div style="text-decoration: underline; font-weight: bold; color: Maroon; width: 150px;
               ">
                STP Selection Status
            </div>
                <table width="52%" >
                    <tr>
                        <td >
                       
                            <table width="100%" class="table" style="border: 1px solid black; box-shadow: 10px 10px 5px #888888;">
                                <tr style="border: 1px solid black;" class="success">
                                    <th colspan="2" style="border: 1px solid black; text-align: center; color: #D35400;
                                        font-weight: bold">
                                        Area Patch
                                    </th>
                                    <th colspan="2" style="border: 1px solid black; text-align: center; color: #1A5276;
                                        font-weight: bold">
                                        Listed Doctor
                                    </th>
                                    <th colspan="2" style="border: 1px solid black; text-align: center; color: #6C3483;
                                        font-weight: bold">
                                        Chemist
                                    </th>
                                </tr>
                                <tr class="active" style="border: 1px solid black;">
                                    <td align="left" style="border: 1px solid black;">
                                        Total
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        <asp:Label ID="totterr" runat="server" ForeColor="Magenta"></asp:Label>
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        Total
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        <asp:Label ID="totdoctorr" runat="server" ForeColor="Magenta"></asp:Label>
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        Total
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        <asp:Label ID="totchemistt" runat="server" ForeColor="Magenta"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="info">
                                    <td align="left" style="border: 1px solid black;">
                                        Selected
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        <asp:Label ID="terrselected" runat="server" ForeColor="#E56717"></asp:Label>
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        Selected
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        <asp:Label ID="doctselected" runat="server" ForeColor="#E56717"></asp:Label>
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        Selected
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        <asp:Label ID="chemselected" runat="server" ForeColor="#E56717"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="warning">
                                    <td align="left" style="border: 1px solid black;">
                                        Not Selected
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        <asp:Label ID="terr_notselect" runat="server" ForeColor="Purple" CssClass="blink_me"></asp:Label>
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        Not Selected
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        <asp:Label ID="doct_notseleceted" runat="server" ForeColor="Purple" CssClass="blink_me"></asp:Label>
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        Not Selected
                                    </td>
                                    <td align="left" style="border: 1px solid black;">
                                        <asp:Label ID="chem_notselected" runat="server" ForeColor="Purple" CssClass="blink_me"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td   >

                   
                        
                     
                        
                         <div style="border: 1px solid black; box-shadow: 10px 10px 5px #888888; height:100%">
                       
                            <asp:GridView ID="grdcat" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false" HeaderStyle-Wrap="false"
                                RowStyle-Wrap="false" GridLines="None" Width="100%" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Catg ( Vst )" HeaderStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:HiddenField ID="hdndcatcode" runat="server" Value='<%#  Eval("Doc_Cat_Code") %>' />
                                            <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_SName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tot Dr" HeaderStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldrcnt" runat="server" Height="30px" Text='<%#  Eval("drcnt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Vst Fr" HeaderStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvisitcnt" runat="server" Text='<%#  Eval("visitcnt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Selected" HeaderStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldrcntt" runat="server" ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                          </div> 
                       
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="totterrcode" runat="server" />
                <asp:HiddenField ID="totdoctorcode" runat="server" />
                <asp:HiddenField ID="totchemcode" runat="server" />
            </div>
           <%-- <div style="width: 520px; height: 350px; overflow: scroll; right: 50px;" onscroll="OnScrollDiv(this)"
                id="DivMainContent">--%>
                <br />
                <br />
                <br />
                <br />
                  <br />
                <br />
                <br />
                <br />
                <br />
                <br />
               <table width="52">
                <asp:GridView ID="grdSTP" runat="server" Width="53%" HorizontalAlign="Center" AutoGenerateColumns="false"
                    RowStyle-Wrap="false" GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt"
                    OnRowDataBound="grdSTP_RowDataBound">
                    <HeaderStyle Font-Bold="False" />
                    <PagerStyle CssClass="pgr"></PagerStyle>
                    <SelectedRowStyle BackColor="BurlyWood" />
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Day Plan" HeaderStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnplan_code" runat="server" Value='<%#  Eval("day_plan_code") %>' />
                                <asp:HiddenField ID="hdnplan_shortname" runat="server" Value='<%#  Eval("day_plan_short_name") %>' />
                                <asp:Label ID="lblday_plan_name" runat="server" Text='<%#  Eval("day_plan_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Area Patch" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" >
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <%--<asp:TextBox ID="txtFieldForce" SkinID="MandTxtBox" onkeypress="CheckNumeric(event);" 
                                            runat="server" Width="300px"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtterr" runat="server" SkinID="TxtBxNumOnly" MaxLength="1" ReadOnly="true"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                                                Width="300px" TabIndex="28" CssClass="form-control input-lg" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                            <asp:PopupControlExtender ID="PopupControlExtender3" runat="server" DynamicServicePath=""
                                                Enabled="True" ExtenderControlID="" TargetControlID="txtterr" PopupControlID="pnlFieldForce"
                                                OffsetY="22">
                                            </asp:PopupControlExtender>
                                            <asp:Panel ID="pnlFieldForce" runat="server" Height="116px" Width="300px" Direction="LeftToRight"
                                                ScrollBars="Auto" BackColor="#ceede3" Style="display: none; text-transform: capitalize">
                                                <div style="height: 15px; position: relative; background-color: #4682B4; overflow-y: scroll;
                                                    text-transform: capitalize; width: 100%; float: left" align="right">
                                                    
                                                </div>
                                                <br />
                                                <asp:CheckBoxList ID="chkterr" Font-Names="Verdana" Font-Size="8pt" runat="server"
                                                    DataSource="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code"
                                                    OnClick="LoadFieldForce(this);">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dr/Chem (Selection)" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button Text="Select" ID="lnkFake" CssClass="btn btn-info" runat="server" Height="28px"
                                    UseSubmitBehavior="false" OnClientClick="return ShowModalPopup(this)" />
                                <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake"
                                    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;
                                    height: 500px; width: 1100px; overflow: scroll;">
                                    <%--    <div class="header">
        Doctor
    </div>--%>
                                    <div class="body">
                                        <asp:Button ID="btnClose" runat="server" Text="Close"
                                            CssClass="btn-warning" Height="30px" Width="60px" />
                                               <asp:Button ID="btnsavee" runat="server" Text="Save" OnClientClick="return GetSelectedRow(this);" CssClass="btn-warning" Height="30px" Width="60px"/>
                                        <div align="right">
                                            <asp:Label ID="lblday_plan_name2" ForeColor="#680000" runat="server" Text='<%#  Eval("day_plan_name") %>'></asp:Label>
                                            <%--<asp:Label ID="popdate" runat="server" Text='<%#Eval("date")%>' + " " +'<% Eval("day_name")%>'  ></asp:Label>--%>
                                        </div>
                                        <%--  <br />--%>
                                        <asp:Label ID="lbldd" runat="server" Text="Select Alteast one Doctor and Chemist from selected Patch"
                                            ForeColor='Red' CssClass="blink_me"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblterr" Text="Patch :" runat="server"></asp:Label>
                                        <asp:Label ID="terr_name" runat="server" ForeColor="Violet"></asp:Label>
                                        <table width="100%">
                                            <tr>
                                                <td align="left" width="50%">
                                                    <asp:HiddenField ID="hdndr" runat="server" />
                                                    <asp:HiddenField ID="hdndr_name" runat="server" />
                                                    <div class="well">
                                                        <asp:Label ID="dr" Font-Bold="true" Text="LISTED DOCTOR" runat="server" Font-Size="16px"
                                                            Font-Underline="true" ForeColor="#728C00"></asp:Label>
                                                        <ul id="check-list-box" class="list-group checked-list-box">
                                                        <div >
                                                      <span style="color:#972D16;"> Doctor Name </span>-<span style="color:#169783 ;">Patch</span> - <span style="color:#166297;">Category(visit)</span>-<span style="color:#971693;">Allocated 
                                                      Visit(STP Plan Selected)</span>-<span style="color:#8A6E45;">Remaining Visit </span> 
                                                        </div>
                                                            <li class="list-group-item">
                                                                <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                                                                    <asp:ListItem Text="." Value="0"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </td>
                                                <td align="left" width="50%">
                                                    <asp:HiddenField ID="hdnchem" runat="server" />
                                                    <asp:HiddenField ID="hdnchem_name" runat="server" />
                                                    <div class="well">
                                                        <asp:Label ID="chem" Font-Bold="true" Text="CHEMIST" runat="server" Font-Underline="true"
                                                            ForeColor="#DE5D83" Font-Size="16px"></asp:Label>
                                                            <div >
                                                           
                                                      <asp:Label ID="ttt" ForeColor="White" Text="-----------------------------------------------------------------------------------------------------------------------" runat="server"></asp:Label> 
                                                        </div>
                                                        <ul id="Ul1" class="list-group checked-list-box">
                                                            <li class="list-group-item">
                                                                <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                                                                    <asp:ListItem Text="." Value="0"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="No.of Drs/Chem" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                               
                                <asp:Label ID="lbldrchem" Font-Bold="true" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

               </table>
           <%-- </div>--%>
            <%--   <div>
        
        </div>--%>
        </center>
        <center>
            <br />
             <div class="div_fixed_Submit">
            <asp:Button ID="btnSave" CssClass="btn-warning" runat="server" Width="85px" Height="26px"
                Text="Draft Save" OnClick="btnSave_Click" OnClientClick="return Draftsave()" />
            <asp:Button ID="btnSubmit" CssClass="btn-success" Width="90px" Height="27px" runat="server"
                Text="Final Submit" OnClick="btnSubmit_Click" OnClientClick="return ValidateEmptyValue()" />
                </div>
        </center>

        <div style="display:none">

         <asp:GridView ID="grdvst" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false" HeaderStyle-Wrap="false"
                                RowStyle-Wrap="false" GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo3" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ListedDrCode" HeaderStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                           <asp:TextBox ID="txtListedDrCode" runat="server" Text='<%#  Eval("ListedDrCode") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ListedDr_Name" HeaderStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox  ID="txtListedDr_Name" runat="server" Text='<%#  Eval("ListedDr_Name") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="No_of_Visit" HeaderStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:TextBox  ID="txtNo_of_Visit" runat="server" Text='<%#  Eval("No_of_Visit") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Doc_Cat_Code" HeaderStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:TextBox  ID="txtDoc_Cat_Code" runat="server" Text='<%#  Eval("Doc_Cat_Code") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       

                                </Columns>
                            </asp:GridView>

                            </div>
    </div>
    <script type="text/javascript">
        function Draftsave() {


            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Save as a Draft ?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
                return false;
            }

        }
    </script>
    <script type="text/javascript">
           function ValidateEmptyValue() {
               var grid = document.getElementById('<%= grdSTP.ClientID %>');

                var row = <%=grdSTP.Rows.Count %> ;

            count_terr=[];

          

             if (grid != null) {
         

                 var allcnt = grid.getElementsByTagName("tr");
                 var ddlwrktype = '';
                 var plan_name = '';
                 var ter_name = '';
                 var hdndr = '';
                 var hdnchem='';

                 var terrnam_count='';
                 var doctor_count ='';
                 var chemistt_count ='';

                
               
                 for (i = 2; i <= row+1; i++) {

                     if (i > 9) {

                       
                         plan_name = document.getElementById('grdSTP_ctl' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl' + i + '_hdndr');
                         hdnchem=document.getElementById('grdSTP_ctl' + i + '_hdnchem');
                         

                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;
                     }
                     else {

                       
                         plan_name = document.getElementById('grdSTP_ctl0' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl0' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl0' + i + '_hdndr');
                         hdnchem = document.getElementById('grdSTP_ctl0' + i + '_hdnchem');
                       
                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;
                     }
                   
                          
                         if (ter_name.value == '') {
                             alert('Select Area Patch of ' + plan_name);
                             ter_name.focus();
                             return false;
                         }

                         else if (hdndr.value == '') {
                             alert("Select Alteast one Doctor for the Day Plan of " + plan_name);

                             return false;
                           }

                         else if (hdnchem.value == '') {
                             alert("Select Alteast one Chemist for the Day Plan of " + plan_name);

                             return false;
                           }


                           }

                         


       


                             var grdvst = document.getElementById('<%= grdvst.ClientID %>');
                              var rowvst = <%=grdvst.Rows.Count %>; 

                          

                              var drcodevst='';
                              var drnamevst ='';
                              var visit_cnt ='';

                            


                              for (j = 2; j <= rowvst+1; j++) {

                                

                                   if (j > 9) {

                                 drcodevst=  document.getElementById('grdvst_ctl' + j + '_txtListedDrCode').value;
                                 visit_cnt=  document.getElementById('grdvst_ctl' + j + '_txtNo_of_Visit').value;
                                  drnamevst=  document.getElementById('grdvst_ctl' + j + '_txtListedDr_Name').value;
                              
                                    }

                                    else 
                                    {
                                      drcodevst=  document.getElementById('grdvst_ctl0' + j + '_txtListedDrCode').value;
                                      visit_cnt=  document.getElementById('grdvst_ctl0' + j + '_txtNo_of_Visit').value;
                                      drnamevst=  document.getElementById('grdvst_ctl0' + j + '_txtListedDr_Name').value;
                                        
                                    }


                                          var cntt=0;
                        str_arr = doctor_count.split(',');
                       for (i = 0; i < str_arr.length; i++) { 
                         if($.trim(str_arr[i]) === drcodevst){
                             cntt =cntt+1;
                         }
                      }

                   

                      if(visit_cnt > cntt)
                      {
                          alert("Select " +drnamevst +" Doctor for " +visit_cnt +" times");
                          return false;
                      }
                      else if(visit_cnt < cntt)
                      {
                          alert("Not select " +drnamevst +" Doctor more than " +visit_cnt +" times");
                          return false;
                      }
                                    

                    }
                             
           var totterrrr=  document.getElementById('totterr').innerHTML;   
           
           var terrselectedd =document.getElementById('terrselected').innerHTML; 

           var totdoctorrr =document.getElementById('totdoctorr').innerHTML;
           var doctselectedd =document.getElementById('doctselected').innerHTML;
           
           if(totterrrr == terrselectedd)
           {

           }
           else
           {
               alert("Select all Patch");
               return false;
           }
           
           if(totdoctorrr ==doctselectedd)
           {
           }
           else 
           {
               alert("Select all Doctor");
               return false;
           }    



               var confirm_value = document.createElement("INPUT");
               confirm_value.type = "hidden";
               confirm_value.name = "confirm_value";
               if (confirm("Do You want to Submit?")) {
                   confirm_value.value = "Yes";
               }
               else {
                   confirm_value.value = "No";
                   return false;
     }
     }
     }
     
           
    </script>

    <script type="text/javascript">


        $(document).ready(function () {
            firstload();
            pageload2();
        });


        function firstload() {
          

               var grid = document.getElementById('<%= grdSTP.ClientID %>');

                var row = <%=grdSTP.Rows.Count %>  ;

            count_terr=[];

             if (grid != null) {
         

                 var allcnt = grid.getElementsByTagName("tr");
                 var ddlwrktype = '';
                 var plan_name = '';
                 var ter_name = '';
                 var hdndr = '';
                 var hdnchem='';

                 var terrnam_count='';
                 var doctor_count ='';
                 var chemistt_count ='';

                
               
                 for (i = 2; i <= row+1; i++) {

                     if (i > 9) {

                       
                         plan_name = document.getElementById('grdSTP_ctl' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl' + i + '_hdndr');
                         hdnchem=document.getElementById('grdSTP_ctl' + i + '_hdnchem');
                         

                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;
                     }
                     else {

                       
                         plan_name = document.getElementById('grdSTP_ctl0' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl0' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl0' + i + '_hdndr');
                         hdnchem = document.getElementById('grdSTP_ctl0' + i + '_hdnchem');
                       
                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;
                     }
                 }

                   terrnam_count = Array.from(new Set(terrnam_count.split(','))).toString();

                  
              
               terrnam_count= terrnam_count.replace(/^,|,$/g, '');
               // alert(terrnam_count); 
                var select_terr ='';                         
                          
                var elements = terrnam_count.split(',');
                 if(terrnam_count !=''){

                  select_terr =elements.length;
           
              //  alert(elements.length);
                }
                else 
                {
                select_terr='0';
                 // alert(elements='0');
                }
         

            doctor_count = Array.from(new Set(doctor_count.split(','))).toString();

            
              
               doctor_count= doctor_count.replace(/^,|,$/g, '');
               // alert(doctor_count);  
               
               var select_doctt ='';                        
                          
                var elements2 = doctor_count.split(',');
                 if(doctor_count !=''){
           
           select_doctt=elements2.length;
               // alert(elements2.length);
                }
                else 
                {
                select_doctt=0;
                 // alert(elements2='0');
                }

                chemistt_count =Array.from(new Set(chemistt_count.split(','))).toString();
                chemistt_count=chemistt_count.replace(/^,|,$/g, '');

                var select_chemiss='';

                var elements3=chemistt_count.split(',');
                if(chemistt_count !='')
                {
                select_chemiss=elements3.length;
                }
                else 
                {
                select_chemiss='0';
                }

            

                var totdoctorcode=$('#totdoctorcode').val();
                var totterrcode =$('#totterrcode').val();
                var totchemcode=$('#totchemcode').val();

            

                 document.getElementById('totterr').innerHTML=totterrcode;
                 document.getElementById('terrselected').innerHTML=select_terr;
                 document.getElementById('terr_notselect').innerHTML =totterrcode-select_terr;

                 document.getElementById('totdoctorr').innerHTML =totdoctorcode;
                 document.getElementById('doctselected').innerHTML=select_doctt;
                 document.getElementById('doct_notseleceted').innerHTML=totdoctorcode-select_doctt;

                 document.getElementById('totchemistt').innerHTML=totchemcode;
                 document.getElementById('chemselected').innerHTML=select_chemiss;
                 document.getElementById('chem_notselected').innerHTML=totchemcode-select_chemiss;

            }
        }


        function pageload2() {


         var grid = document.getElementById('<%= grdSTP.ClientID %>');

                var row = <%=grdSTP.Rows.Count %> ; 

            count_terr=[];

             if (grid != null) {
             
         

                 var allcnt = grid.getElementsByTagName("tr");
                 var ddlwrktype = '';
                 var plan_name = '';
                 var ter_name = '';
                 var hdndr = '';
                 var hdnchem='';

                 var terrnam_count='';
                 var doctor_count ='';
                 var chemistt_count ='';
                 var doctor_count_new=''

                
               
                 for (i = 2; i <= row+1; i++) {

                     if (i > 9) {

                       
                         plan_name = document.getElementById('grdSTP_ctl' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl' + i + '_hdndr');
                         hdnchem=document.getElementById('grdSTP_ctl' + i + '_hdnchem');
                         

                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;

                         doctor_count_new = doctor_count +','+hdndr.value;
                     }
                     else {

                       
                         plan_name = document.getElementById('grdSTP_ctl0' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl0' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl0' + i + '_hdndr');
                         hdnchem = document.getElementById('grdSTP_ctl0' + i + '_hdnchem');
                       
                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;

                          doctor_count_new = doctor_count +','+hdndr.value;
                     }
                 }


            

                   terrnam_count = Array.from(new Set(terrnam_count.split(','))).toString();
              
               terrnam_count= terrnam_count.replace(/^,|,$/g, '');
               // alert(terrnam_count); 
                var select_terr ='';                         
                          
                var elements = terrnam_count.split(',');
                 if(terrnam_count !=''){

                  select_terr =elements.length;
           
              //  alert(elements.length);
                }
                else 
                {
                select_terr='0';
                 // alert(elements='0');
                }
         

            doctor_count = Array.from(new Set(doctor_count.split(','))).toString();

            
              
               doctor_count= doctor_count.replace(/^,|,$/g, '');
               // alert(doctor_count);  
               
               var select_doctt ='';                        
                          
                var elements2 = doctor_count.split(',');
                 if(doctor_count !=''){
           
           select_doctt=elements2.length;
               // alert(elements2.length);
                }
                else 
                {
                select_doctt=0;
                 // alert(elements2='0');
                }

                chemistt_count =Array.from(new Set(chemistt_count.split(','))).toString();
                chemistt_count=chemistt_count.replace(/^,|,$/g, '');

                var select_chemiss='';

                var elements3=chemistt_count.split(',');
                if(chemistt_count !='')
                {
                select_chemiss=elements3.length;
                }
                else 
                {
                select_chemiss='0';
                }

            

                var totdoctorcode=$('#totdoctorcode').val();
                var totterrcode =$('#totterrcode').val();
                var totchemcode=$('#totchemcode').val();

            

                 document.getElementById('totterr').innerHTML=totterrcode;
                 document.getElementById('terrselected').innerHTML=select_terr;
                 document.getElementById('terr_notselect').innerHTML =totterrcode-select_terr;

                 document.getElementById('totdoctorr').innerHTML =totdoctorcode;
                 document.getElementById('doctselected').innerHTML=select_doctt;
                 document.getElementById('doct_notseleceted').innerHTML=totdoctorcode-select_doctt;

                 document.getElementById('totchemistt').innerHTML=totchemcode;
                 document.getElementById('chemselected').innerHTML=select_chemiss;
                 document.getElementById('chem_notselected').innerHTML=totchemcode-select_chemiss;

            }




                     var grdcat = document.getElementById('<%= grdcat.ClientID %>');
                              var rowcat = <%=grdcat.Rows.Count %> ;

                              var hdndcatcode='';

                    
                             
                              for (k = 2; k <= rowcat+1; k++) {

                              
                                 if (k > 9) {

                                   hdndcatcode=  document.getElementById('grdcat_ctl' + k + '_hdndcatcode').value;

                                 }
                                 else 
                                 {
                                  hdndcatcode=  document.getElementById('grdcat_ctl0' + k + '_hdndcatcode').value;
                                 }

                               

            
                   var grdvst = document.getElementById('<%= grdvst.ClientID %>');
                              var rowvst = <%=grdvst.Rows.Count %> ;

                          



                              var drcodevst='';
                              var drnamevst ='';
                              var visit_cnt ='';
                              var cntt2=0;
                              var txtDoc_Cat_Code='';


                            


                              for (j = 2; j <= rowvst+1; j++) {

                                

                                   if (j > 9) {

                                 drcodevst=  document.getElementById('grdvst_ctl' + j + '_txtListedDrCode').value;
                                 visit_cnt=  document.getElementById('grdvst_ctl' + j + '_txtNo_of_Visit').value;
                                  drnamevst=  document.getElementById('grdvst_ctl' + j + '_txtListedDr_Name').value;
                                  txtDoc_Cat_Code =document.getElementById('grdvst_ctl' + j + '_txtDoc_Cat_Code').value;
                              
                                    }

                                    else 
                                    {
                                      drcodevst=  document.getElementById('grdvst_ctl0' + j + '_txtListedDrCode').value;
                                      visit_cnt=  document.getElementById('grdvst_ctl0' + j + '_txtNo_of_Visit').value;
                                      drnamevst=  document.getElementById('grdvst_ctl0' + j + '_txtListedDr_Name').value;
                                       txtDoc_Cat_Code =document.getElementById('grdvst_ctl0' + j + '_txtDoc_Cat_Code').value;
                                        
                                    }

                                   
                 if(hdndcatcode ==txtDoc_Cat_Code){

                                         
                        str_arr = doctor_count_new.split(',');
                       for (i = 0; i < str_arr.length; i++) { 
                         if($.trim(str_arr[i]) === drcodevst){
                             cntt2 =cntt2+1;
                         }
                      }

                      }

                    }

                   if (k > 9) {

                         document.getElementById('grdcat_ctl' + k + '_lbldrcntt').innerHTML =cntt2;

                                 }
                                 else 
                                 {
                               document.getElementById('grdcat_ctl0' + k + '_lbldrcntt').innerHTML = cntt2;
                                 }

                    }

               

        }

        function HidePopup() {


            var popup = $find('PopupControlExtender3');
            popup.hidePopup();
            return false;
        }
    </script>

    <script type="text/javascript">
        function Checkboxvalue(name) {
            $(document).ready(function () {

                $('#grdSTP_ctl02_CheckBoxList1 :checkbox').live('click', function () {
                    if ($(this).is(':checked')) {
                        //alert($(this).text());
                    }
                    //            document.getElementById('finalvalue').value 

                });
            });
        }
    </script>

    <script type="text/javascript">

        function getpatchname(doctor_code) {
      

             var grid = document.getElementById('<%= grdSTP.ClientID %>');

                var row = <%=grdSTP.Rows.Count %> ; 

            count_terr=[];

             if (grid != null) {
             
         

                 var allcnt = grid.getElementsByTagName("tr");
                 var ddlwrktype = '';
                 var plan_name = '';
                 var ter_name = '';
                 var hdndr = '';
                 var hdnchem='';

                 var terrnam_count='';
                 var doctor_count ='';
                 var chemistt_count ='';
                 var doctor_count_new=''
                 var patchname='';

                
               
                 for (i = 2; i <= row+1; i++) {

                     if (i > 9) {

                       
                         plan_name = document.getElementById('grdSTP_ctl' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl' + i + '_hdndr');
                         hdnchem=document.getElementById('grdSTP_ctl' + i + '_hdnchem');
                         

                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;

                         doctor_count_new = doctor_count +','+hdndr.value;
                     }
                     else {

                       
                         plan_name = document.getElementById('grdSTP_ctl0' + i + '_lblday_plan_name').innerHTML;
                         ter_name = document.getElementById('grdSTP_ctl0' + i + '_txtterr');
                         hdndr = document.getElementById('grdSTP_ctl0' + i + '_hdndr');
                         hdnchem = document.getElementById('grdSTP_ctl0' + i + '_hdnchem');
                       
                         terrnam_count =terrnam_count+','+ter_name.value +',';
                         doctor_count =doctor_count +','+hdndr.value;
                         chemistt_count=chemistt_count+','+hdnchem.value;

                          doctor_count_new = doctor_count +','+hdndr.value;
                     }

                  
                        var found = hdndr.value.split(",").indexOf(doctor_code) > -1;
                        
                        if(found ==true)
                        {
                       
                        patchname +=plan_name +',';
                        }

                 }
                 }

                 return patchname.replace(/,\s*$/, "");;

        }
    </script>

          <script language="javascript" type="text/javascript">
         function popdcr() {
            
             strOpen = "STP_View.aspx",
             window.open(strOpen, 'popWindow', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=800,height=600,left = 0,top = 0');
             return false;
         }
    </script>

    
    </form>
</body>
</html>
