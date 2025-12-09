<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_Entry_Terr.aspx.cs" Inherits="MasterFiles_MR_TP_Entry_Terr" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
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
    <style type="text/css">
        .Color
        {
            background: yellow;
        }
        
        
        .modalPopupNew
        {
            background-color: #FFFFFF;
            width: 350px;
            border: 3px solid #0DA9D0;
            padding: 0;
        }
        
        .modalBackgroundNew
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        
        .modalBackground
        {
            background-color: Black;
            width: 200px;
            height: 500px;
        }
        
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 200px;
            height: 500px;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }
        .ques {width:100%;}
        
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
    </style>

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

             var grid = document.getElementById('<%= grdTP.ClientID %>');
             if (grid != null) {

                 var allcnt = grid.getElementsByTagName("input");
                 var ddlwrktype = '';
                 var datee = '';
                 var ter_name = '';
                 var hdndr = '';
               
                 for (i = 2; i < allcnt.length; i++) {

                     if (i > 9) {

                         ddlwrktype = document.getElementById('grdTP_ctl' + i + '_ddlwrktype');
                         datee = document.getElementById('grdTP_ctl' + i + '_lblDate').innerHTML;
                         ter_name = document.getElementById('grdTP_ctl' + i + '_txtterr');
                         hdndr = document.getElementById('grdTP_ctl' + i + '_hdndr');
                     }
                     else {

                         ddlwrktype = document.getElementById('grdTP_ctl0' + i + '_ddlwrktype');
                         datee = document.getElementById('grdTP_ctl0' + i + '_lblDate').innerHTML;
                         ter_name = document.getElementById('grdTP_ctl0' + i + '_txtterr');
                         hdndr = document.getElementById('grdTP_ctl0' + i + '_hdndr');
                     }

                     if (ddlwrktype.value == '0') {
                         alert('Select Work Type for the date of ' + datee);
                         ddlwrktype.focus();
                         return false;

                     }
                     else

                 

                     var hdnwrktype = $('#hdnwrktype').val();

                     var ddlwrktxt = ddlwrktype.options[ddlwrktype.selectedIndex].text;

                     var found = hdnwrktype.split(",").indexOf(ddlwrktxt) > -1;

                     if (found == true) {

                        
                         if (ter_name.value == '') {
                             alert('Select Territory for the date of ' + datee);
                             ter_name.focus();
                             return false;
                         }

                         else if (hdndr.value == '') {
                             alert("Select Alteast one Doctor for the date of " + datee);
                            // document.getElementById('grdTP_ctl' + i + '_lnkFake').focus();
//                             $find("grdTP_ctl" + i + "_mpe").show();
                             return false;

                         }


                     }
                     else {


                     }
                 }
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
       
              
</script>


  <script type="text/javascript">
      function ValidateEmptyValue_appr() {

          var grid = document.getElementById('<%= grdTP.ClientID %>');
          if (grid != null) {

              var allcnt = grid.getElementsByTagName("input");
              var ddlwrktype = '';
              var datee = '';
              var ter_name = '';

              for (i = 2; i < allcnt.length; i++) {

                  if (i > 9) {

                      ddlwrktype = document.getElementById('grdTP_ctl' + i + '_ddlwrktype');
                      datee = document.getElementById('grdTP_ctl' + i + '_lblDate').innerHTML;
                      ter_name = document.getElementById('grdTP_ctl' + i + '_txtterr');
                  }
                  else {

                      ddlwrktype = document.getElementById('grdTP_ctl0' + i + '_ddlwrktype');
                      datee = document.getElementById('grdTP_ctl0' + i + '_lblDate').innerHTML;
                      ter_name = document.getElementById('grdTP_ctl0' + i + '_txtterr');
                  }

                  if (ddlwrktype.value == '0') {
                      alert('Select Work Type for the date of ' + datee);
                      ddlwrktype.focus();
                      return false;

                  }
                  else

                      var hdnwrktype = $('#hdnwrktype').val();

                  var ddlwrktxt = ddlwrktype.options[ddlwrktype.selectedIndex].text;

                  var found = hdnwrktype.split(",").indexOf(ddlwrktxt) > -1;

                  if (found == true) {


                      if (ter_name.value == '') {
                          alert('Select Territory for the date of ' + datee);
                          ter_name.focus();
                          return false;
                      }


                  }
                  else {


                  }
              }
          }


          var confirm_value = document.createElement("INPUT");
          confirm_value.type = "hidden";
          confirm_value.name = "confirm_value";
          if (confirm("Do You want to Approve?")) {
              confirm_value.value = "Yes";
          }
          else {
              confirm_value.value = "No";
              return false;
          }
      }
       
              
</script>
    <script type="text/javascript">

        function disable(row) {


            var idString = row.id;
            var $i1 = idString.indexOf("_");
            $i1 = $i1 + 4;
            var $i2 = idString.indexOf("_", $i1);
            var cnt = 0;
            var index = '';

            if ((idString.substring($i1, $i2) - 1) < 10) {

                var cIdprev = parseInt(idString.substring($i1, $i2));


                index = cnt.toString() + cIdprev.toString();




            }
            else {

                var cIdprev = parseInt(idString.substring($i1, $i2));
                index = cIdprev;


            }





                   var chkterr = document.getElementById('grdTP_ctl' + index + '_chkterr');
                     var checkBoxArray = chkterr.getElementsByTagName('input');
                    for (var i = 0; i < checkBoxArray.length; i++) {
                        if (checkBoxArray[i].checked) {
                            checkBoxArray[i].checked = false;
                          
                        }
                    }

                    document.getElementById('grdTP_ctl' + index + '_txtterr').value = '';
                    document.getElementById('grdTP_ctl' + index + '_hdndr').value = '';
                    document.getElementById('grdTP_ctl' + index + '_hdndr_name').value = '';
                    document.getElementById('grdTP_ctl' + index + '_hdnchem').value = '';
                    document.getElementById('grdTP_ctl' + index + '_hdnchem_name').value = '';

  



            var hdnwrktype = $('#hdnwrktype').val();


            var ddlwrk = document.getElementById('grdTP_ctl' + index + '_ddlwrktype');

            var ddlwrktxt = ddlwrk.options[ddlwrk.selectedIndex].text;

            var found = hdnwrktype.split(",").indexOf(ddlwrktxt) > -1;

            if (found == true) {


                $('input[id*=grdTP_ctl' + index + '_txtterr]').removeAttr("disabled");

            }
            else {
                $('input[id*=grdTP_ctl' + index + '_txtterr]').attr("disabled", "disabled");

            }





            //              if (ddlwrktxt == "Field Work") {

            //                  alert(ddlwrktxt);

            //                  $('input[id*=grdTP_ctl' + index + '_txtterr]').removeAttr("disabled");
            //                  $('input[id*=grdTP_ctl' + index + '_lnkFake]').removeAttr("disabled");

            //              }


        } 
    </script>
</head>
<body oncontextmenu="return false;">
    <script type="text/javascript">

        function HidePopupFF() {
            var popup = $find('PopupControlExtender3');
            popup.hidePopup();
            alert("hii");
            return true;
        }
    </script>
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



                    //                    if (objTextBox == "") {

                    //                        objTextBox = chkBoxText[0].innerText;

                    //                        GetChildControl(row, "txtterr").value = objTextBox;

                    //                    }
                    //                    else {
                    //                        objTextBox = objTextBox + ", " + chkBoxText[0].innerText;

                    //                        GetChildControl(row, "txtterr").value = objTextBox;

                    //                    }
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
        }
    </script>
    <script type="text/javascript">
        function getselectedTerritory() {

            $(document).ready(function () {
                $('input[id*=lnkFake]').click(function () {
                    alert("test");
                    var teritoryname = "terr";

                    alert(teritoryname);
                    $.ajax({
                        url: 'TP_Entry_Terr.aspx/teritory',
                        dataType: 'json',
                        type: 'post',
                        contentType: 'application/json',
                        data: JSON.stringify({ "teritoryname": teritoryname }),
                        processData: false,
                        success: function (data) {
                            alert("test");

                            $.each(data.d, function (key, value) {
                                alert("tebhjgbdfbgj");

                                $('#chkdr').append($("<option></option>").val(value.ListedDrCode).html(value.ListedDrCode));

                            });
                            // $("#chkterr").append(data);
                            // to do
                            //$('#response pre').html(JSON.stringify(data));
                        },
                        error: function (jqXhr, textStatus, errorThrown) {
                            console.log(errorThrown);
                        }
                    });
                });
            });
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

            if ((idString.substring($i1, $i2) - 1) < 10) {

                var cIdprev = parseInt(idString.substring($i1, $i2));


                index = cnt.toString() + cIdprev.toString();




            }
            else {

                var cIdprev = parseInt(idString.substring($i1, $i2));
                index = cIdprev;


            }


            //        var chk = document.getElementById('chkdr');
            //        var checkBoxArray = chk.getElementsByTagName('input');
            //        for (var i = 0; i < checkBoxArray.length; i++) {
            //            if (checkBoxArray[i].checked) {
            //                checkBoxArray[i].checked = false;
            //                alert("test");
            //            }
            //        }



         


            var terrval = "";
            var day = "";

            terrval = document.getElementById("grdTP_ctl" + index + "_txtterr").value;

            day = document.getElementById("grdTP_ctl" + index + "_lblDay_name").innerHTML;


            document.getElementById("grdTP_ctl" + index + "_terr_name").innerHTML = terrval;

            $.ajax({
                type: "POST",
                url: "TP_Entry_Terr.aspx/GetCustomersdr",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ "teritoryname": terrval,"day": day }),
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

               

                var row = $("[id*=grdTP_ctl" + index + "_CheckBoxList1] tr:last-child").clone(true);

                $("[id*=grdTP_ctl" + index + "_CheckBoxList1] tr").remove();

                var colCounter = 1;
                var colLimit = 8;

                $.each(customers, function () {

                    //                    var roww;

                    //                    if (i % repeatColumns == 0) {
                    //                        roww = $("<tr />");
                    //                        $("[id*=grdTP_ctl" + index + "_CheckBoxList1] tbody").append(roww);
                    //                    } else {
                    //                        row = $("[id*=grdTP_ctl" + index + "_CheckBoxList1] tr:last-child");
                    //                    }



                    var customer = $(this);



                    $("input", row).val($(this).find("ListedDrCode").text());
                    $("label", row).html($(this).find("ListedDr_Name").text());
                    $("[id*=grdTP_ctl" + index + "_CheckBoxList1] tbody").append(row);
                    row = $("[id*=grdTP_ctl" + index + "_CheckBoxList1] tr:last-child").clone(true);




                    var checkboxx = document.getElementById("grdTP_ctl" + index + "_CheckBoxList1");
                    var checkoptions = checkboxx.getElementsByTagName('input');
                    var listSelected = checkboxx.getElementsByTagName('label');

                    for (i = 0; i < checkoptions.length; i++) {

                        var savedvalue = document.getElementById("grdTP_ctl" + index + "_hdndr").value
                        var found = savedvalue.split(",").indexOf(customer.find("ListedDrCode").text()) > -1;

                        if (found == true) {

                            if (customer.find("ListedDrCode").text() == checkoptions[i].value) {


                                checkoptions[i].checked = true;
                            }

                        }
                    }
                });
            }




            chem()



            function chem() {


                $.ajax({
                    type: "POST",
                    url: "TP_Entry_Terr.aspx/GetCustomerschem",
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

                    var row = $("[id*=grdTP_ctl" + index + "_CheckBoxList2] tr:last-child").clone(true);

                    $("[id*=grdTP_ctl" + index + "_CheckBoxList2] tr").remove();

                    $.each(customers, function () {
                        var customer = $(this);

                        $("input", row).val($(this).find("Chem_Code").text());
                        $("label", row).html($(this).find("Chem_Name").text());
                        $("[id*=grdTP_ctl" + index + "_CheckBoxList2] tbody").append(row);
                        row = $("[id*=grdTP_ctl" + index + "_CheckBoxList2] tr:last-child").clone(true);



                        var checkboxxx = document.getElementById("grdTP_ctl" + index + "_CheckBoxList2");
                        var checkoptionss = checkboxxx.getElementsByTagName('input');
                        var listSelectedd = checkboxxx.getElementsByTagName('label');

                        for (i = 0; i < checkoptionss.length; i++) {

                            var savedvaluee = document.getElementById("grdTP_ctl" + index + "_hdnchem").value
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





            $find("grdTP_ctl" + index + "_mpe").show();
            return false;
        }


    </script>
    <%-- <script type="text/javascript">

      $(document).ready(function () {
        $('input[id*=lnkFake]').click(function () {
              alert("jquery");

              var checkboxx = document.getElementById("grdTP_ctl02_CheckBoxList1");
              var checkoptions = checkboxx.getElementsByTagName('input');
              var listSelected = checkboxx.getElementsByTagName('label');

              var last_val = '';
              for (i = 0; i < checkoptions.length; i++) {

                  //var found = savedvalue.split(",").indexOf(checkoptions[i].value) > -1;

                  //                    if (found == true) {
                  //                        alert("bxdhfknb");
                 //  alert(checkoptions.length);
                  //  alert(checkoptions[i].value);

                  // checkoptions[0].checked = true;
                  //  alert(checkoptions[i].value);
                  //  $("[id*=grdTP_ctl" + index + "_CheckBoxList1] input").attr("checked", "checked");

                  //                last_val += checkoptions[i].value;
                  //           

                  //arr.push(checkoptions[i].value);
                  // }

                  //  alert(i);
                  //                    if (i == 1) {
                  //                        alert("bxdhfknb");
                  //                        checkoptions[0].checked = true;
                  //                    }
              }
              alert(checkoptions.length);


         // });
      });

            </script>--%>
    <%--<script type="text/javascript">
    function getselectedTerritory() {

        $(document).ready(function () {
            $('input[id*=lnkFake]').click(function () {

    --%>
    <script type="text/javascript">

        function GetSelectedRow(lnk) {


            var idString = lnk.id;

            var $i1 = idString.indexOf("_");

            $i1 = $i1 + 4;

            var $i2 = idString.indexOf("_", $i1);

            var cnt = 0;
            var index = '';


            if ((idString.substring($i1, $i2) - 1) < 10) {

                var cIdprev = parseInt(idString.substring($i1, $i2));


                index = cnt.toString() + cIdprev.toString();




            }
            else {

                var cIdprev = parseInt(idString.substring($i1, $i2));
                index = cIdprev;


            }

            arr = [];

            aarr_namee = [];

            var checkboxx = document.getElementById("grdTP_ctl" + index + "_CheckBoxList1");
            var checkoptions = checkboxx.getElementsByTagName('input');
            var listSelected = checkboxx.getElementsByTagName('label');

            var last_val = '';
            for (i = 0; i < checkoptions.length; i++) {

                if (checkoptions[i].checked) {
                    //                last_val += checkoptions[i].value;
                    //
                  //  alert(checkoptions[i].parentNode.getElementsByTagName('span')[0].innerHTML);
                   // var chkBoxText = checkoptions[i].parentNode.getElementsByTagName('label');

                    arr.push(checkoptions[i].value);
                    aarr_namee.push(checkoptions[i].parentNode.getElementsByTagName('span')[0].innerHTML);
                }

            }


 

            document.getElementById("grdTP_ctl" + index + "_hdndr").value = arr;
            document.getElementById("grdTP_ctl" + index + "_hdndr_name").value = aarr_namee;

           



            arr2 = [];

            arrarr_nameee = [];

            var checkboxx2 = document.getElementById("grdTP_ctl" + index + "_CheckBoxList2");
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


            document.getElementById("grdTP_ctl" + index + "_hdnchem").value = arr2;
            document.getElementById("grdTP_ctl" + index + "_hdnchem_name").value = arrarr_nameee;

           


        }
    </script>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu1 ID="menu" runat="server" />
    </div>
    <div align="center">
        <table id="tblMargin" runat="server" align="center">
            <tr>
                <td>
                    <asp:Label ID="lblHead" runat="server" Text="Tour Plan for the Month of " Font-Size="Medium"
                        ForeColor="Green" Font-Names="Verdana"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLink" runat="server" Font-Size="Small" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                    <asp:LinkButton ID="hylEdit" runat="server" OnClick="hylEdit_Onclick" Font-Size="Small"
                        Font-Names="Verdana" ForeColor="Blue"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <center>
            <table width="65%" align="center" runat="server" id="tabsf">
                <tr>
                    <%-- <td width="2.5%"></td>--%>
                    <td align="left">
                        <asp:Label ID="lblname" Text="Filed Force Name :" runat="server" SkinID="lblMand"></asp:Label>
                        <asp:Label ID="lblsf_name" runat="server" Font-Names="Verdana" ForeColor="#A0522D"
                            Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left">
                    </td>
                    <td align="left">
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblemp" Text="Employee Id :" runat="server" SkinID="lblMand"></asp:Label>
                        <asp:Label ID="lblempid" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lbldoj" Text="DOJ :" runat="server" SkinID="lblMand"></asp:Label>
                        <asp:Label ID="lbldoj2" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblreport" Text="Reporting To :" runat="server" SkinID="lblMand"></asp:Label>
                        <asp:Label ID="lblreportname" runat="server" Font-Names="Verdana" ForeColor="#A0522D"
                            Font-Bold="true"></asp:Label>
                        <asp:HiddenField ID="hdnwrktype" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<asp:CheckBoxList ID="CheckBoxList1" runat="server">
    <asp:ListItem Text="test" Value="0"></asp:ListItem>
</asp:CheckBoxList>--%>
                    </td>
                </tr>
                <%-- <tr>
                    <td>
                    <asp:CheckBoxList ID="chkFruits" runat="server" DataSourceID="SqlDataSource1"
    DataTextField="ListedDr_Name" DataValueField="ListedDrCode">
</asp:CheckBoxList>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
    SelectCommand="SELECT ListedDrCode, ListedDr_Name FROM mas_listeddr where sf_code='MR0002'"></asp:SqlDataSource>
<br />
                    </td>
                    </tr>--%>
            </table>
        </center>
        <section style="display: block">
   <ol id="ExampleList">
   
    <li><a href="#NCP_Lightbox" onclick="getInternetExplorerVersion()"><img src="../../Images/help_animated.gif" alt="" /></a></li>
  
    </ol>
<div class="ncp-popup ncp-popup-overlay" id="NCP_Lightbox">
	<div class="ncp-popup-spacer">
		<a href="#" id="LnkClose" class="ncp-popup-close">X</a>
		<div class="ncp-popup-container">
			
			<div class="ncp-popup-content">
				 <h2 style="color: Red; font-weight: bold;font-family: Arial, Helvetica, sans-serif;">
                    TP - Entry / Edit</h2>
                <p class="p">
                    1. Fill Your "TP" for all days and Press "Send to Manager Approval" Button for Manager
                    Approval.</p>
                <p>
                    2. After Approval From Your Manager, then next Month "TP" will open.</p>
                <p>
                    3. After Selecting the "Field Work" , the Area will appear for Selection for the
                    Particular Day. Whichever having the Doctors, those area only will appear.
                </p>
                <p>
                    4. "Without Doctor Areas" - will not reflect in your TP- Entry.
                </p>
                <p>
                    5. For Other Worktypes, not Possible to Select the Areas. The "Selection box" will
                    be in "Disable" Mode.</p>
                <p>
                    6. Before Approval from your Manager, You can Edit your TP for the Particular Month.</p>
                <p>
                    7. After Approval from your Manager, the Fieldforce cannot Edit their TP. Get the
                    Permission from "Admin", then the Fiedlforce can Edit their "TP" for the required
                    month.</p>
			</div>
		</div>
	</div>
        </div>
             <asp:Panel ID="Panel1" runat="server" Style="text-align: center;">
                    <asp:Label ID="lblReason" runat="server" Style="text-align: center" Font-Size="Small"
                        Font-Names="Verdana" Visible="false"></asp:Label>
                </asp:Panel>


        <center>


      
        <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>--%>
            <table align="center">
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdTP" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false" 
                            GridLines="None" CssClass="mGridImg" OnRowDataBound="grdTP_RowDataBound" AlternatingRowStyle-CssClass="alt">
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
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                      
                                        <asp:DropDownList ID="ddlwrktype" runat="server" SkinID="ddlRequired" Width="150px" CssClass="form-control"
                                            DataSource="<%# FillWorkType() %>"  DataTextField="WorkType_Name_B"
                                            DataValueField="WorkType_Code_B" onchange="disable(this)">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Patch Name" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                           <asp:DropDownList ID="ddlTerr" Width="230" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                            DataTextField="Territory_Name" DataValueField="Territory_Code">
                                        </asp:DropDownList>
                                    
                                 
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Territory Name" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>

                               
                                <table>
                                    <tr>
                                        <td>
                                            <%--<asp:TextBox ID="txtFieldForce" SkinID="MandTxtBox" onkeypress="CheckNumeric(event);" 
                                            runat="server" Width="300px"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtterr" runat="server" SkinID="TxtBxNumOnly" MaxLength="1" Enabled="false" ReadOnly="true"
                                                onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                                                Width="300px" TabIndex="28" CssClass="form-control input-lg" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                            <asp:PopupControlExtender ID="PopupControlExtender3" runat="server" DynamicServicePath=""
                                                Enabled="True" ExtenderControlID="" TargetControlID="txtterr" PopupControlID="pnlFieldForce"
                                                OffsetY="22">
                                            </asp:PopupControlExtender>
                                            <asp:Panel ID="pnlFieldForce" runat="server" Height="116px" Width="300px" 
                                                 Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                                Style="display: none; text-transform: capitalize">
                                                <div style="height: 15px; position: relative; background-color: #4682B4; overflow-y: scroll;
                                                    text-transform: capitalize; width: 100%; float: left" align="right">
                                                  <%--  <asp:Button ID="Button2" BackColor="Yellow" Style="font-family: Verdana; height: 15px;
                                                        font-size: 5pt; width: 20px; color: Black; margin-top: -1px;" Text="X" runat="server"
                                                         OnClientClick="return HidePopupFF();" />--%>
                                                </div>
                                                <br />
                                                <asp:CheckBoxList ID="chkterr" Font-Names="Verdana" Font-Size="8pt" runat="server"  DataSource="<%# FillTerritory() %>"
                                            DataTextField="Territory_Name" DataValueField="Territory_Code"
                                                     OnClick="LoadFieldForce(this);" >
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                       
                                    </tr>
                                </table>
                      

                                          </ItemTemplate>
                                </asp:TemplateField>


       <asp:TemplateField HeaderText="Dr/Chem" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>

                                     <asp:Button Text="Select" ID = "lnkFake" CssClass="btn btn-info" runat="server" Height="28px" UseSubmitBehavior="false" OnClientClick="return ShowModalPopup(this)"   />
<%--                                    <asp:Button Text="Select" ID = "lnkFake"  runat="server" UseSubmitBehavior="false" OnClientClick="return getselectedTerritory();"   />--%>
                                    <%--<asp:Button Text="Select" ID = "lnkFake"  runat="server" UseSubmitBehavior="false"   />--%>
                                <%--     <asp:HiddenField ID="hdndrchem" runat="server" />--%>

                                     
<asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake"
CancelControlID="btnClose" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;height: 500px; width:900px; overflow: scroll;">
<%--    <div class="header">
        Doctor
    </div>--%>
    <div class="body">
    <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="return GetSelectedRow(this);" CssClass="btn-warning" Height="30px" Width="60px"/>
    <div align="right">
    <asp:Label ID="popdate" runat="server" ForeColor="#680000" Text='<%#Eval("date")+ " - " + Eval("day_name")%>' ></asp:Label>
       <%--<asp:Label ID="popdate" runat="server" Text='<%#Eval("date")%>' + " " +'<% Eval("day_name")%>'  ></asp:Label>--%>
    </div>
   
  <%--  <br />--%>
     <asp:Label ID="lbldd" runat="server" Text="Select Alteast one Doctor and Chemist from selected Territory" ForeColor='Red' CssClass="blink_me"></asp:Label>
    <br />

    <asp:Label ID="lblterr" Text="Territory :" runat="server"></asp:Label>
     <asp:Label ID="terr_name"  runat="server" ForeColor="Violet" Text="terrname"></asp:Label>
    
    <table width="100%">

      <tr>
       <td align="left" width="50%">
        <asp:HiddenField ID="hdndr" runat="server" />
         <asp:HiddenField ID="hdndr_name" runat="server" />

        <div class="well" >
        <asp:Label ID="dr" Font-Bold="true" Text="LISTED DOCTOR" runat="server" Font-Size="16px" Font-Underline="true" ForeColor="#728C00"></asp:Label>
        <ul id="check-list-box" class="list-group checked-list-box">
			<li class="list-group-item">
             

      <asp:CheckBoxList ID="CheckBoxList1" runat="server"  >
    <asp:ListItem Text="." Value ="0"></asp:ListItem>
</asp:CheckBoxList>
</li>
		</ul>
</div>
       
       
      
       </td>
       <td align="left"  width="50%">
             <asp:HiddenField ID="hdnchem" runat="server" />
              <asp:HiddenField ID="hdnchem_name" runat="server" />
                          <div class="well" >
                          <asp:Label ID="chem"  Font-Bold="true" Text="CHEMIST" runat="server" Font-Underline="true" ForeColor="#DE5D83" Font-Size="16px" ></asp:Label>                                                       
        <ul id="Ul1" class="list-group checked-list-box">
			<li class="list-group-item">  
              
                                      
      <asp:CheckBoxList ID="CheckBoxList2" runat="server" >
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
         


                                <asp:TemplateField HeaderText="Objective" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                      <asp:HiddenField ID="hdnholiday" runat="server" Value='<%#  Eval("Holiday") %>' />
                                        <asp:HiddenField ID="hdnweekoff" runat="server" Value='<%#  Eval("weekoff") %>' />
                                        <asp:HiddenField ID="hdnField_Work" runat="server" Value='<%#  Eval("Field_Work") %>' />
                                        <asp:HiddenField ID="hdnterr_code" runat="server" Value='<%#  Eval("territory_code") %>' />
                                         
                                            <asp:HiddenField ID="hdnwrktype_code" runat="server" Value='<%#  Eval("WorkType_Code_B") %>' />
                                        <asp:TextBox ID="txtObjective" runat="server" CssClass="form-control input-lg" SkinID="MandTxtBox" Text='<%#  Eval("Objective") %>'
                                            Width="250">                                           
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>

                 
            <br />
           <asp:Button ID="btnSave" CssClass="savebutton" runat="server" Width="85px" Height="26px" Text="Draft Save" 
                        OnClick="btnSave_Click" OnClientClick="return Draftsave()" />     &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" CssClass="savebutton" Width="175px" Height="26px" runat="server"
                Text="Send to Manager Approval" OnClick="btnSubmit_Click" OnClientClick="return ValidateEmptyValue()"/>

            <%--    <div style="margin-left: 30%">--%>
            <asp:Button ID="btnApprove" CssClass="savebutton" runat="server" Visible="false" Height="26px" Width="90px" Text="Approve TP" OnClick="btnApprove_Click" 
                OnClientClick="return ValidateEmptyValue_appr()" />
            &nbsp
            
            <asp:Button ID="btnReject" CssClass="savebutton" runat="server" Visible="false" Text="Reject TP" Height="26px" Width="90px"  OnClick="btnReject_Click"/>
              
            &nbsp
            <asp:Label ID="lblRejectReason" Text="Reject Reason : " Visible="false" SkinID="lblMand" runat="server"></asp:Label>
            &nbsp
            <asp:TextBox ID="txtReason" Width="400" Height="45" Visible="false" TextMode="MultiLine"
                runat="server"></asp:TextBox>
            &nbsp
            <asp:Button ID="btnSendBack" CssClass="savebutton" Height="26px" Width="140px" runat="server" Visible="false" 
                Text="Send for ReEntry" OnClick="btnSendBack_Click" />

                  
        </center>
    </div>
    </form>
</body>
</html>
