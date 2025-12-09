var DD_SfCode = "";
var DD_SfType = "";
var DD_DivCode = "";

$(document).ready(function () {
    $(document).ajaxStart(function () {
        $('#loader').css("display", "block")
    })['ajaxStop'](function () {
        $('#loader').css("display", "none")
    });

    DD_SfCode = $('#DD_SfCode').val();
    DD_SfType = $('#DD_SfType').val();
    DD_DivCode = $('#DD_DivCode').val();

    $('#eData').hide();
});

$(document).on("click", "#upload", function () {

    //Reference the FileUpload element.
    var fileUpload = $("#fileUpload")[0];

    //Validate whether File is valid Excel file.
    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$/;
    if (regex.test(fileUpload.value.toLowerCase())) {
        if (typeof (FileReader) != "undefined") {
            var reader = new FileReader();

            //For Browsers other than IE.
            if (reader.readAsBinaryString) {
                reader.onload = function (e) {
                    ProcessExcel(e.target.result);
                };
                reader.readAsBinaryString(fileUpload.files[0]);
            } else {
                //For IE Browser.
                reader.onload = function (e) {
                    var data = "";
                    var bytes = new Uint8Array(e.target.result);
                    for (var i = 0; i < bytes.byteLength; i++) {
                        data += String.fromCharCode(bytes[i]);
                    }
                    ProcessExcel(data);
                };
                reader.readAsArrayBuffer(fileUpload.files[0]);
            }
        } else {
            alert("This browser does not support HTML5.");
        }
    } else {
        alert("Please upload a valid Excel file.");
    }


    ////Reference the FileUpload element.
    //var fileUpload = $("#fileUpload")[0];

    //Validate whether File is valid Excel file.
    //var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$/;
    //if (regex.test(fileUpload.value.toLowerCase())) {
    //    if (typeof (FileReader) != "undefined") {
    //        $.ajax({
    //            type: "POST",
    //            contentType: "application/json; charset=utf-8",
    //            url: "../MR/webservice/SecSale_Upload_WebService.asmx/GetExcelData",
    //            data: "{}",
    //            dataType: "json",
    //            success: function (result) {
    //                ProcessExcel(result);
    //            }
    //        });
    //    } else {
    //        alert("This browser does not support HTML5.");
    //    }
    //} else {
    //    alert("Please upload a valid Excel file.");
    //}

    ////Reference the FileUpload element.
    //var fileUpload = $("#fileUpload")[0];

    ////Validate whether File is valid Excel file.
    //var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$/;
    //if (regex.test(fileUpload.value.toLowerCase())) {
    //    if (typeof (FileReader) != "undefined") {
    //        var reader = new FileReader();

    //        //For Browsers other than IE.
    //        if (reader.readAsBinaryString) {
    //            reader.onload = function (e) {
    //                ProcessExcel(e.target.result);
    //            };
    //            reader.readAsBinaryString(fileUpload.files[0]);
    //        } else {
    //            //For IE Browser.
    //            reader.onload = function (e) {
    //                var data = "";
    //                var bytes = new Uint8Array(e.target.result);
    //                for (var i = 0; i < bytes.byteLength; i++) {
    //                    data += String.fromCharCode(bytes[i]);
    //                }
    //                ProcessExcel(data);
    //            };
    //            reader.readAsArrayBuffer(fileUpload.files[0]);
    //        }
    //    } else {
    //        alert("This browser does not support HTML5.");
    //    }
    //} else {
    //    alert("Please upload a valid Excel file.");
    //}
});

function ProcessExcel(data) {

    //Read the Excel File data.
    var workbook = XLSX.read(data, {
        type: 'binary'
    });

    //Fetch the name of First Sheet.
    var firstSheet = workbook.SheetNames[0];

    //Read all rows from First Sheet into an JSON array.
    //var excelRows = {};
    var excelRows = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[firstSheet], {
        header: 0,
        defval: ''
    });

    var xml = '';
    xml += "<xml>";
    for (var i = 0; i < excelRows.length; i++) {
        xml += "<HolidayUpload>";
        $.each(excelRows[i], function (key, value) {
            xml += "<" + key.replace(/ /g, '').replace(/\//g, '') + ">" + value + "</" + key.replace(/ /g, '').replace(/\//g, '') + ">";
        });
        xml += "</HolidayUpload>";
    }
    xml += "</xml>";

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/Holiday_Upload_WebService.asmx/Holiday_Upload_Process",
        data: "{objHolidayUpload:" + JSON.stringify(xml) + "}",
        dataType: "json",
        success: function (response) {

            var fData = $.grep(response.d, function (v) {
                return v.Holiday_ID === "0" && v.Division_Code === "";
            });

            if (fData.length > 0) {

                $('#tblUploadE').empty();
                $('#tblUploadE').append("<thead><tr style='background-color:White;'><th scope='col'>S.No</th><th class='grdHeader' scope='col'>Holiday_Id</th><th class='grdHeader' scope='col'>Division_Code</th><th scope='col'>Holiday Name</th><th scope='col'>Holiday Date</th><th scope='col'>State Code</th><tbody>");
                for (var i = 0; i < fData.length; i++) {
                    var sno = i + 1;
                    $('#tblUploadE').append("<tr><td><span id='lblSNo_" + i + "'>" + sno + "</span></td>"
                        + " <td class='grdHeader'><span id='lblHoliday_Id" + i + "'>" + fData[i].Holiday_ID + "</span></td> "
                        + " <td class='grdHeader'><span id='lblDivision_Code" + i + "'>" + fData[i].Division_Code + "</span></td> "
                        + " <td><span id='lblHoliday_Name" + i + "'>" + fData[i].Holiday_Name + "</span></td> "
                        + " <td><span id='lblHoliday_Date" + i + "'>" + fData[i].Holiday_Date + "</span></td> "
                        + " <td><span id='lblState_Code" + i + "'>" + fData[i].State_Code + "</span></td> "
                        + "</tr>");
                };

                $('#tblUploadE').append("</tbody>");

                $('#tblUploadE').DataTable({
                    "destroy": true,
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "searching": false
                });

                $("#eData").show();
            } else {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../MR/webservice/Holiday_Upload_WebService.asmx/Holiday_Upload",
                    data: "{objHolidayUpload:" + JSON.stringify(xml) + "}",
                    dataType: "json",
                    success: function (response) {
                        $('#fileUpload').val('');
                        var result = $.alert('Upload Success!', 'Alert!');
                    },
                    error: function ajaxError(reponse) { }
                })
            }
        },
        error: function ajaxError(reponse) { }
    });


    ////Create a HTML Table element.

    //$('#tblUpload').empty();
    //var table = $('#tblUpload');
    //var thead = $("<thead></thead>");
    //var tr = $("<tr></tr>");
    //var tbody = $("<tbody></tbody>");
    //thead.append(tr);

    ////Add the header row.
    ////var row = $(table[0].insertRow(-1));

    //////Add the header cells.
    ////var headerCell = $("<th />");
    ////headerCell.html("Id");
    ////row.append(headerCell);

    ////var headerCell = $("<th />");
    ////headerCell.html("Name");
    ////row.append(headerCell);

    ////var headerCell = $("<th />");
    ////headerCell.html("Country");
    ////row.append(headerCell);


    //var header = [];
    //$.each(excelRows[0], function (key, value) {
    //    //Add the header cells.

    //    var headerCell = $("<th />");
    //    headerCell.html(key);
    //    tr.append(headerCell);
    //});

    ////Add the data rows from Excel file.
    //for (var i = 0; i < excelRows.length; i++) {


    //    //Add the data row.
    //    var row = $("<tr />");

    //    //Add the data cells.
    //    var cell = $("<td />");
    //    cell.html(excelRows[i]['S.No']);
    //    row.append(cell);

    //    var cell = $("<td />");
    //    cell.html(excelRows[i]['Statement Date']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Month']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Year']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Division Name']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Company Name']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Stockist Code']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Stockist Name']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Product Code']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Product Name']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Opening Qty']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Opening Value']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Receipt Qty']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Receipt Free Qty']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Receipt Value']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Sales Return Qty']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Sales Return Free Qty']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Sales Return Value']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Sales Qty']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Sales Free Qty']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Sales Qty Value']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Free Qty']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Free QtyValue']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Purchase Return Qty']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Purchase Return Qty Value']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['ADJ/Ret']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Closing Qty']);
    //    row.append(cell);

    //    cell = $("<td />");
    //    cell.html(excelRows[i]['Value']);
    //    row.append(cell);

    //    tbody.append(row);
    //    ////Add the data row.
    //    //var row = $(table[0].insertRow(-1));

    //    ////Add the data cells.
    //    //var cell = $("<td />");
    //    //cell.html(excelRows[i].S.No);
    //    //row.append(cell);

    //    //cell = $("<td />");
    //    //cell.html(excelRows[i].Country);
    //    //row.append(cell);

    //}

    //$('#tblUpload').append(thead);
    //$('#tblUpload').append(tbody);
    //$('#tblUpload').DataTable({
    //    "destroy": true,
    //    "paging": false,
    //    "ordering": false,
    //    "info": false,
    //    "searching": false,
    //    "scrollX": true,
    //    "scrollY": "500px",
    //    "scrollCollapse": true
    //});

    //var dvExcel = $("#dvExcel");
    //dvExcel.html("");
    //dvExcel.append(table);
};


//function ProcessExcel(data) {

//    //Read all rows from First Sheet into an JSON array.
//    var excelRows = data;

//    //Create a HTML Table element.
//    var table = $("<table />");
//    table[0].border = "1";

//    //Add the header row.
//    var row = $(table[0].insertRow(-1));

//    ////Add the header cells.
//    //var headerCell = $("<th />");
//    //headerCell.html("Id");
//    //row.append(headerCell);

//    //var headerCell = $("<th />");
//    //headerCell.html("Name");
//    //row.append(headerCell);

//    //var headerCell = $("<th />");
//    //headerCell.html("Country");
//    //row.append(headerCell);

//    var header = [];
//    $.each(excelRows[0], function (key, value) {
//        //Add the header cells.
//        var headerCell = $("<th />");
//        headerCell.html(key);
//        row.append(headerCell);
//    });

//    console.log(table.innerHTML);

//    //Add the data rows from Excel file.
//    for (var i = 0; i < excelRows.length; i++) {


//        $.each(excelRows[i], function (key, value) {
//            //Add the data row.
//            var row = $(table[0].insertRow(-1));

//            //Add the data cells.
//            var cell = $("<td />");
//            cell.html(excelRows[i].Statment_Date);
//            row.append(cell);

//            cell = $("<td />");
//            cell.html(excelRows[i].Trans_Month);
//            row.append(cell);

//            cell = $("<td />");
//            cell.html(excelRows[i].Trans_Year);
//            row.append(cell);

//            cell = $("<td />");
//            cell.html(excelRows[i].Division_Name);
//            row.append(cell);

//            cell = $("<td />");
//            cell.html(excelRows[i].Stockist_Code);
//            row.append(cell);

//            cell = $("<td />");
//            cell.html(excelRows[i].Stockist_Name);
//            row.append(cell);
//        });

//        ////Add the data row.
//        //var row = $(table[0].insertRow(-1));

//        ////Add the data cells.
//        //var cell = $("<td />");
//        //cell.html(excelRows[i].S.No);
//        //row.append(cell);

//        //cell = $("<td />");
//        //cell.html(excelRows[i].Country);
//        //row.append(cell);

//    }

//    var dvExcel = $("#dvExcel");
//    dvExcel.html("");
//    dvExcel.append(table);
//};
