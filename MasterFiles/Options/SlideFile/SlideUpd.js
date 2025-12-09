


var expanded = false;



function showCheckboxes() {
    var checkboxes = document.getElementById("checkboxes");
    if (!expanded) {
        checkboxes.style.display = "block";
        expanded = true;
    } else {
        checkboxes.style.display = "none";
        expanded = false;
    }
}

function showSpec() {
         var checkboxes = document.getElementById("DivSpec");
         if (!expanded) {
             checkboxes.style.display = "block";
             expanded = true;
         } else {
             checkboxes.style.display = "none";
             expanded = false;
         }
     }

function showThera() {
    var checkboxes = document.getElementById("divThero");
    if (!expanded) {
        checkboxes.style.display = "block";
        expanded = true;
    } else {
        checkboxes.style.display = "none";
        expanded = false;
    }
}

function BrandWisePrd() {
   var BrandText = $('#ddlBrand :selected').val();
   var Sub_Div = $('#ddlSubdivision :selected').val();
   var obj = { BrandText: BrandText, Sub_Div: Sub_Div };

    $.ajax({
        type: "POST",
        url: "frmBrandWiseSlidesUpd.aspx/FillProductGroup",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(obj),
        dataType: "json",          
        success: function (data) {
            var checkboxes = document.getElementById("checkboxes");
            $(checkboxes).find("label").remove();
            for (var i = 0; i < data.d.length; i++) {

                var node = document.createElement('div');
                $("#checkboxes").append('<label for="' + data.d[i].id_ + '"><input name="chkPrd" attPrd="' + data.d[i].name_ + '"  type="checkbox"  id="' + data.d[i].id_ + '"/>' + data.d[i].name_ + '</label>');


                        }

           


        },
        error: function (msg) {

           // alert("error" + msg);
        }

    });



    $.ajax({
        type: "POST",
        url: "frmBrandWiseSlidesUpd.aspx/FillSpecGroup",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var checkboxes = document.getElementById("DivSpec");
            $(checkboxes).find("label").remove();
            for (var i = 0; i < data.d.length; i++) {

                var node = document.createElement('div');
                $("#DivSpec").append('<label for="' + data.d[i].id_ + '"><input name="chkSpec" attPrd="' + data.d[i].name_ + '"  type="checkbox"  id="' + data.d[i].id_ + '"/>' + data.d[i].name_ + '</label>');


            }

        },
        error: function (msg) {

            //alert("error" + msg);
        }

    });



    $.ajax({
        type: "POST",
        url: "frmBrandWiseSlidesUpd.aspx/FillTheroGroup",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var checkboxes = document.getElementById("divThero");
            $(checkboxes).find("label").remove();
            for (var i = 0; i < data.d.length; i++) {

                var node = document.createElement('div');
                $("#divThero").append('<label for="' + data.d[i].id_ + '"><input name="chkThera" attPrd="' + data.d[i].name_ + '"  type="checkbox"  id="' + data.d[i].id_ + '"/>' + data.d[i].name_ + '</label>');


            }

        },
        error: function (msg) {

            //alert("error" + msg);
        }

    });
}

   
    function makeAjaxCall(e) {
        var PrdValue;
        var PrdName;
        var VPrdData;
        var VSpecData;
        var VTheraData;
        var vFileDate="";
        var filename;
        var Div_Code;
        var Sub_Div;
        var BrandValue;
        var BrandText;

        var ArPrddataValue = [];
        var ArPrddataText = [];

        var ArSpcdataValue = [];
        var ArSpcdataText = [];

        var ArTheradataValue = [];
        var ArTheradataText = [];

        var ArFileName = [];
        var data = [];

        Div_Code = $('#ddlDivision :selected').val();
        Sub_Div = $('#ddlSubdivision :selected').val();
        BrandValue = $('#ddlBrand :selected').text();
        BrandText = $('#ddlBrand :selected').val();

        var ddlval = $('select#ddlBrand option:selected').val();

        if (ddlval == 0) {
            alert("Select the Brand");
            e.preventDefault();
        }     

        $.each($('#fileUpload').prop("files"), function (k, v) {
           
            filename += v['name']+',';
            vFileDate = filename.replace("undefined", '');
        });

        if (vFileDate == "") {
            alert("Please Select any one Slide");
            k.preventDefault();
        }   
        
        ArFileName.push(vFileDate);
            
        //$('#fileUpload').on('change', function () {
            //for (var i = 0; i < this.files.length; i++) {
            //    alert(this.files[i].name);
            //    alert(this.files.item(i).name); // alternatively
            //}
      
        var VPrdValue, VPrdText;
        PrdValue = "";
        PrdName = "";
        $('input[name=chkPrd]:checked').each(function (i) {
            var value = $(this).val();
            if (this.checked) {
                PrdValue += $(this).attr("id") + ',';
                PrdName += $(this).attr("attPrd")+',';
               
                VPrdValue = PrdValue.replace("undefined", '')
                VPrdText= PrdName.replace("undefined", '');
            }
        });

        if (PrdValue == "") {
            alert("Select the Product");
            e.preventDefault();
        }

        ArPrddataValue.push(VPrdValue);
        ArPrddataText.push(VPrdText);

        var VSpecValue, VSpecText;
        PrdValue = "";
        PrdName = "";
        $('input[name=chkSpec]:checked').each(function (i) {
            var value = $(this).val();
            if (this.checked) {
                PrdValue += $(this).attr("id") + ',';
                PrdName += $(this).attr("attPrd") + ',';

                VSpecValue = PrdValue.replace("undefined", '');
                VSpecText =  PrdName.replace("undefined", '');
            }
        });

        if (PrdValue == "") {
            alert("Select the Speciality");
            e.preventDefault();
        }

        ArSpcdataValue.push(VSpecValue);
        ArSpcdataText.push(VSpecText);

        var VTheraValue, VTheraText;
        PrdValue = "";
        PrdName = "";
        $('input[name=chkThera]:checked').each(function (i) {
            var value = $(this).val();
            if (this.checked) {
                PrdValue += $(this).attr("id") + ',';
                PrdName += $(this).attr("attPrd") + ',';

                VTheraValue = PrdValue.replace("undefined", '');
                VTheraText= PrdName.replace("undefined", '');
            }
        });

        if (PrdValue == "") {
            alert("Select the Herapathite");
            e.preventDefault();
        }

        ArTheradataValue.push(VTheraValue);
        ArTheradataText.push(VTheraText);

        PageMethods.ImgUpload(ArPrddataValue, ArPrddataText, ArSpcdataValue, ArSpcdataText, ArTheradataValue, ArTheradataText, ArFileName, Div_Code, Sub_Div, BrandValue, BrandText);

        setTimeout(function () {
            FileMovetoFolder();
        }, 10);
       
      
        
        //alert('Slide Upload Successfully');

    }

    $(document).ready(function () {
        BrandWisePrd();
    });

    function FileMovetoFolder() {
        // Image File Move To Folder 
        var fileUpload = $("#fileUpload").get(0);
        var files = fileUpload.files;

        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        /// <reference path="FileUploadHandler.ashx" />
        $.ajax({
            type: "POST",
            url: "FileUploadHandler.ashx",
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                alert(result);
                

            },
            error: function (err) {
                alert("Please again Resubmit")
            }
        });
    }

    function GetspecHera() {
       
        var ddlSelect = $('#ddlMode :selected').text();
        $('#ddlFiler').empty()
        if (ddlSelect == "Speciality Wise") {
            $('#ddlFiler').css("display", "block");
            $('#lblFiler').css("display", "block");
            $.ajax({
                type: "POST",
                url: "frmSlidePriority.aspx/FillSpecGroup",
                dataType: "json",
                contentType: "application/json",
                success: function (res) {
                    $.each(res.d, function (data, value) {
                        $("#ddlFiler").append($("<option></option>").val(value.id_).html(value.name_));
                    })
                }

            });
        }
        else if (ddlSelect == "Herapathite") {
            $('#ddlFiler').css("display", "block");
            $('#lblFiler').css("display", "block");
            $.ajax({
                type: "POST",
                url: "frmSlidePriority.aspx/FillTheroGroup",
                dataType: "json",
                contentType: "application/json",
                success: function (res) {
                    $.each(res.d, function (data, value) {

                        $("#ddlFiler").append($("<option></option>").val(value.id_).html(value.name_));
                    })
                }

            });

        }

        else if (ddlSelect == "Brand Wise") {
            $('#ddlFiler').css("display", "none");
            $('#lblFiler').css("display", "none");
            $.ajax({
                type: "POST",
                url: "frmSlidePriority.aspx/GetSelect",
                dataType: "json",
                contentType: "application/json",
                success: function (res) {
                    $.each(res.d, function (data, value) {

                        $("#ddlFiler").append($("<option></option>").val(value.id_).html(value.name_));
                    })
                }

            });

        }

    }

    

    function SetPriority() {
        var TableData = '';
        //TableData = $.toJSON(TableData);

        var TableData = new Array();
        var table = $("#DCVisit table");

        table.find('tr').each(function (i, tr) {
            var $tds = $(this).find('td');
            TableData[i] = {
                "Brand_Name": $tds.eq(0).text()
               , "Url": $tds.eq(1).text()
                , "Img_Name": $tds.eq(2).text()
               , "Priority": $tds.eq(3).find("option:selected").text()
            }
            // do something with productId, product, Quantity
        });       

        checkValue(TableData)
        PageMethods.Save_Priority(JSON.stringify(TableData));
        alert('Slide Priority Updated Successfully.....');
    }

    function checkValue(TableData) {
        var status = 'Not exist';
        var count = 0;
//        for (var i = 0; i < TableData.length; i++) {

//            var CheckFirst = TableData[i];
//            if (CheckFirst.Brand_Name == '') {
//                var first = CheckFirst.Priority;
//                for (var j = i + 1; j < TableData.length; j++) {
//                    var CheckSecond = TableData[j];
//                    if (CheckSecond.Brand_Name == '') {
//                        var Second = CheckSecond.Priority;
//                        // var CheckFirst.Priority = CheckSecond.Priority;
//                        if ($.trim(first) == $.trim(Second)) {
//                            status = 'Exist';
//                            count = 1;
//                            alert('Already Exit Priority Order ! ....');
//                            e.preventDefault();
//                        }
//                    }
//                    else {
//                        break;
//                    }
//                }
//            }
        //        }

        $(".tblCall").each(function () { //get all rows in table
            var result = [];
            var tblid = $(this).attr("id");
            $("#" + tblid + " tr td.myClass").each(function () {
                var this_row = $(this);
                var ID = $.trim(this_row.find("option:selected").text());

                if (ID != '') {
                  
                    if (result.indexOf(ID) == -1) {
                        result.push(ID);
                    }
                    else {
                        alert('Priority Order No Already Exit');                        
                        $.trim(this_row.find("select").focus())
                       
                    }
                }

            });
        });

    }
    // ---------------------- DropDown Click Selected Value ------------------------

    $(function () {

        $("#checkboxes").click(function () {
            var VPrdValue = "";
            var VPrdText = "";
            PrdValue = "";
            PrdName = "";
            $("#selected-people").empty();
            $('input[name=chkPrd]:checked').each(function (i) {
                var value = $(this).val();
                if (this.checked) {
                    PrdValue += $(this).attr("id") + ',';
                    PrdName += $(this).attr("attPrd") + ',';

                    VPrdValue = PrdValue.replace("undefined", '')
                    VPrdText = PrdName.replace("undefined", '');
                }
            });

            var val = VPrdText;
            if (val != "") {
                $("#SelPrd option[value='0']").text(VPrdText);
            }
            else {
                $("#SelPrd option[value='0']").text("Select an option");
            }
        });


    });

    $(function () {

        $("#DivSpec").click(function () {
            var VPrdValue = "";
            var VPrdText = "";
            PrdValue = "";
            PrdName = "";
            $("#selected-people").empty();
            $('input[name=chkSpec]:checked').each(function (i) {
                var value = $(this).val();
                if (this.checked) {
                    PrdValue += $(this).attr("id") + ',';
                    PrdName += $(this).attr("attPrd") + ',';

                    VPrdValue = PrdValue.replace("undefined", '')
                    VPrdText = PrdName.replace("undefined", '');
                }
            });

            var val = VPrdText;
            if (val != "") {
                $("#SelSpec option[value='0']").text(VPrdText);
            }
            else {
                $("#SelSpec option[value='0']").text("Select an option");
            }
            //$("#Sel").append('<option>' + VPrdText + '</option>');
        });


    });


    $(function () {

        $("#divThero").click(function () {
            var VPrdValue = "";
            var VPrdText = "";
            PrdValue = "";
            PrdName = "";
            $("#selected-people").empty();
            $('input[name=chkThera]:checked').each(function (i) {
                var value = $(this).val();
                if (this.checked) {
                    PrdValue += $(this).attr("id") + ',';
                    PrdName += $(this).attr("attPrd") + ',';

                    VPrdValue = PrdValue.replace("undefined", '')
                    VPrdText = PrdName.replace("undefined", '');
                }
            });

            var val = VPrdText;
            if (val != "") {
                $("#SelTher option[value='0']").text(VPrdText);
            }
            else {
                $("#SelTher option[value='0']").text("Select an option");
            }
            //$("#Sel").append('<option>' + VPrdText + '</option>');
        });


    });   
