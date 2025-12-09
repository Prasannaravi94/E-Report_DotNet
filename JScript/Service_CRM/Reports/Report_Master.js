var Rep_SfCode = "";
var Rep_SfType = "";
var Rep_DivCode = "";
var arr = "";
var arrParam = "";

$(document).ready(function () {
    Rep_SfCode = $('#Rep_SfCode').val();
    Rep_SfType = $('#Rep_SfType').val();
    Rep_DivCode = $('#Rep_DivCode').val();
    $('.divRep').hide();
    $('.divParam').hide();
    $('.divSubParam').hide();
    $('#div1').hide();

    $('.cancel').click(function (e) {
        e.preventDefault();
        
        $("#txtReport_Name").val('');
        $("#hdnRep_ID").val('');
        $('.divRep').hide();
        $("#txtParameter").val('');
        $("#hdnParameterID").val('');
        $('.divParam').hide();
        $("#txtSubParam").val('');
        $("#hdnSubParamID").val('');
        $('.divSubParam').hide();

        $('#btnAddRep').prop('disabled', false);

        Bind_Report_Master();
    });

    $('#btnAddRep').click(function (e) {
        e.preventDefault();
        $('#btnAddRep').prop('disabled', true);
        $('.divRep').show();
        $("#txtReport_Name").val('');
        $("#hdnRep_ID").val('');
    });

    $('#btnSaveRep').click(function (e) {
        e.preventDefault();
        if ($("#txtReport_Name").val().trim() != "") {
            var rep_Name = $("#txtReport_Name").val();
            var rep_ID = $("#hdnRep_ID").val();
            if (rep_ID == "") {
                rep_ID = "-1";
            }
            var active = "";
            if ($("#chk_Rep").prop("checked") == true) {
                active = 0;
            } else {
                active = 1;
            }
            arr = rep_Name + "^" + rep_ID + "^" + active;
            Update_Report_Master(arr);
        }
        else {
            $.alert('Please enter Report Name!', 'Alert!');
        }
        $('#btnAddRep').prop('disabled', false);
        $('.divRep').hide();
    });

    $('#btnSaveParam').click(function (e) {
        e.preventDefault();
        if ($("#txtParameter").val().trim() != "") {
            var rep_ID = $("#hdnRep_ID").val();
            var Param_Name = $("#txtParameter").val();
            var Param_ID = $("#hdnParameterID").val();
            if (Param_ID == "") {
                Param_ID = "-1";
            }
            var active = "";
            if ($("#chkParameter").prop("checked") == true) {
                active = 0;
            } else {
                active = 1;
            }
            arrParam = Param_Name + "^" + rep_ID + "^" + Param_ID + "^" + active;
            Update_Param_Master(arrParam);
        }
        else {
            $.alert('Please enter Parameter Name!', 'Alert!');
        }
        $('#btnAddRep').prop('disabled', false);
        $('.divRep').hide();
    });

    $('#btnSaveSubParam').click(function (e) {
        e.preventDefault();
        if ($("#txtSubParam").val().trim() != "") {
            var rep_ID = $("#hdnRep_ID").val();
            var Param_ID = $("#hdnParameterID").val();
            var Sub_Param_Name = $("#txtSubParam").val();
            var Sub_Param_ID = $("#hdnSubParamID").val();
            if (Sub_Param_ID == "") {
                Sub_Param_ID = "-1";
            }
            var active = "";
            if ($("#chkSubParam").prop("checked") == true) {
                active = 0;
            } else {
                active = 1;
            }
            arrSubParam = Sub_Param_Name + "^" + rep_ID + "^" + Param_ID + "^" + Sub_Param_ID + "^" + active;
            Update_Sub_Param_Master(arrSubParam);
        }
        else {
            $.alert('Please enter Sub-Parameter Name!', 'Alert!');
        }
        $('#btnAddRep').prop('disabled', false);
        $('.divRep').hide();
    });

    Bind_Report_Master();
});


function Bind_Report_Master() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../MR/webservice/Report_Master_WebService.asmx/GetReport',
        data: '{}',
        dataType: 'json',
        success: function (response) {
            if ($.trim(response.d)) {
                $("#tblRep_Master").empty();
                var th = '<thead><th>#</th><th class="grdHeader">Rep_ID</th><th>Report Name</th><th>Status</th><th>Edit</th><th>Add/View Parameter</th></thead>';
                var tbody = '<tbody>';
                var tr = '<tr>';
                var sno = '';
                for (var i = 0; i < response.d.length; i++) {
                    sno = i + 1;
                    tr += "<td><span id='lblSNo_" + i + "'>" + sno + "</span></td>";
                    tr += "<td class='grdHeader'><span id='lblRepCode_" + i + "'>" + response.d[i].Rep_ID + "</span></td>";
                    tr += "<td><span id='lblRepName_" + i + "'>" + response.d[i].Rpt_Name + "</span></td>";
                    tr += "<td><span id='lblRepStatus_" + i + "'>" + response.d[i].Active + "</span></td>";
                    tr += "<td><a class='edit' id='btnEdit_" + i + "' title='Edit' href='#' data-toggle='tooltip'><i class=\"fa fa-pencil\"></i></a></td>";
                    tr += "<td><a class='add' id='btnAdd_" + i + "' title='Add Parameter' href='#' data-toggle='tooltip'><i class=\"fa fa-plus\"></i></a> / <a class='view' id='btnView_" + i + "' title='View Parameter' href='#' data-toggle='tooltip'><i class=\"fa fa-eye\"></i></a><a class='hide' id='btnHideP_" + i + "' title='Close Parameter' href='#' data-toggle='tooltip'><i class=\"fa fa-eye-slash\"></i></a></td>";
                    tr += '</tr>';
                }
                tbody += tr + "</tbody>";
                th += tbody;
                $("#tblRep_Master").append(th);

                $('.hide').hide();
            } else {
                $('#div1').show();
            }
        },
        error: function ajaxError(reponse) { }
    })
}

function Update_Report_Master(arr) {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../MR/webservice/Report_Master_WebService.asmx/Update_Rep_Master',
        data: '{obj_Rep:' + JSON['stringify'](arr) + '}',
        dataType: 'json',
        success: function (response) {
            if ($.trim(response.d)) {
                console.log(response.d);
                $.alert('Update Success!', 'Alert!');
                $("#txtReport_Name").val('');
                $("#hdnRep_ID").val('');
                Bind_Report_Master();
            } else {
                $('#div1').show();
            }
        },
        error: function ajaxError(reponse) { }
    })
}

function Update_Param_Master(arr) {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../MR/webservice/Report_Master_WebService.asmx/Update_Parameter',
        data: '{obj_Param:' + JSON['stringify'](arr) + '}',
        dataType: 'json',
        success: function (response) {
            if ($.trim(response.d)) {
                if (response.d > 0) {
                    $.alert('Update Success!', 'Alert!');
                    $("#txtParameter").val('');
                    $("#hdnParameterID").val('');
                    $("#txtReport_Name").val('');
                    $("#hdnRep_ID").val('');

                    $('.divParam').hide();
                }
            } else {
                
            }
        },
        error: function ajaxError(reponse) { }
    })
}

function Update_Sub_Param_Master(arr) {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../MR/webservice/Report_Master_WebService.asmx/Update_Sub_Parameter',
        data: '{obj_Param:' + JSON['stringify'](arr) + '}',
        dataType: 'json',
        success: function (response) {
            if ($.trim(response.d)) {
                if (response.d > 0) {
                    $.alert('Update Success!', 'Alert!');
                    $("#txtParameter").val('');
                    $("#hdnParameterID").val('');
                    $("#txtReport_Name").val('');
                    $("#hdnRep_ID").val('');
                    $("#txtSubParam").val('');
                    $("#hdnSubParamID").val('');

                    $('.divSubParam').hide();
                }
            } else {

            }
        },
        error: function ajaxError(reponse) { }
    })
}


$(document).on('click', '.edit', function (e) {
    e.preventDefault();

    var rep_ID, rep_Name, rep_Status = '';

    var row = $(this).closest("tr");
    rep_ID = row.find('span:eq(1)').text();
    rep_Name = row.find('span:eq(2)').text();
    rep_Status = row.find('span:eq(3)').text();
    
    $('#btnAddRep').prop('disabled', true);
    $('.divRep').show();
    $('.divParam').hide();

    $("#txtReport_Name").val(rep_Name);
    $("#hdnRep_ID").val(rep_ID);
    if (rep_Status == "Active") {
        $("#chk_Rep").prop("checked", true);
    } else {
        $("#chk_Rep").prop("checked", false);
    }
});

$(document).on('click', '.editP', function (e) {
    e.preventDefault();

    var rep_ID, param_ID, param_Name, param_Status = '';

    var row = $(this).closest("tr");
    rep_ID = row.find('span:eq(1)').text();
    param_ID = row.find('span:eq(2)').text();
    param_Name = row.find('span:eq(3)').text();
    rep_Status = row.find('span:eq(4)').text();

    $('#btnAddRep').prop('disabled', true);
    $('.divRep').hide();
    $('.divSubParam').hide();
    $('.divParam').show();

    $("#txtParameter").val(param_Name);
    $("#hdnParameterID").val(param_ID);
    $("#hdnRep_ID").val(rep_ID);
    if (rep_Status == "Active") {
        $("#chkParameter").prop("checked", true);
    } else {
        $("#chkParameter").prop("checked", false);
    }
});

$(document).on('click', '.add', function (e) {
    e.preventDefault();

    $("#txtParameter").val('');
    $("#hdnParameterID").val('');
    $("#txtReport_Name").val('');
    $("#hdnRep_ID").val('');

    var rep_ID, rep_Name, rep_Status = '';

    var row = $(this).closest("tr");
    rep_ID = row.find('span:eq(1)').text();

    $('#btnAddRep').prop('disabled', true);
    $('.divRep').hide();
    $('.divParam').show();

    $("#hdnRep_ID").val(rep_ID);
    
});

$(document).on('click', '.addP', function (e) {
    e.preventDefault();

    $("#txtParameter").val('');
    $("#hdnParameterID").val('');
    $("#txtReport_Name").val('');
    $("#hdnRep_ID").val('');

    var rep_ID, param_ID, param_Name, rep_Status = '';

    var row = $(this).closest("tr");
    rep_ID = row.find('span:eq(1)').text();
    param_ID = row.find('span:eq(2)').text();
    console.log(param_ID);
    $('#btnAddRep').prop('disabled', true);
    $('.divRep').hide();
    $('.divParam').hide();
    $('.divSubParam').show();

    $("#hdnRep_ID").val(rep_ID);
    $("#hdnParameterID").val(param_ID);
});

$(document).on('click', '.view', function (e) {
    e.preventDefault();    

    var rep_ID = '';

    var row = $(this).closest("tr");
    rep_ID = row.find('span:eq(1)').text();
    view_ID = row.find('a:eq(3)').attr("id");

    $(this).hide();
    $("#" + view_ID).show();

    Bind_Parameter(row, rep_ID);
});

$(document).on('click', '.hide', function (e) {
    e.preventDefault();

    var row = $(this).closest("tr");
    rep_ID = row.find('span:eq(1)').text();
    view_ID = row.find('a:eq(2)').attr("id");

    $(this).hide();
    $("#" + view_ID).show();

    $(this).closest('tr').next().remove();
});

$(document).on('click', '.viewSP', function (e) {
    e.preventDefault();

    var rep_ID, param_ID = '';

    var row = $(this).closest("tr");
    rep_ID = row.find('span:eq(1)').text();
    param_ID = row.find('span:eq(2)').text();
    view_ID = row.find('a:eq(3)').attr("id");

    $(this).hide();
    $("#" + view_ID).show();

    Bind_Sub_Parameter(row, rep_ID, param_ID);
});

$(document).on('click', '.hideSP', function (e) {
    e.preventDefault();

    var row = $(this).closest("tr");
    view_ID = row.find('a:eq(2)').attr("id");

    $(this).hide();
    $("#" + view_ID).show();

    $(this).closest('tr').next().remove();
});

function Bind_Parameter(row, rep_ID) {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../MR/webservice/Report_Master_WebService.asmx/GetParameter',
        data: '{objRep_ID:' + JSON['stringify'](rep_ID) + '}',
        dataType: 'json',
        success: function (response) {
            if ($.trim(response.d)) {
                var th = '<tr><td></td><td colspan="4"><table class="table"><thead><th>#</th><th class="grdHeader">Rep_ID</th><th class="grdHeader">Param_ID</th><th>Parameter Name</th><th>Status</th><th>Edit</th><th>Add/View Sub-Parameter</th></thead>';
                var tbody = '<tbody>';
                var tr = '<tr>';
                var sno = '';
                for (var i = 0; i < response.d.length; i++) {
                    sno = i + 1;
                    tr += "<td><span id='lblSNo_" + i + "'>" + sno + "</span></td>";
                    tr += "<td class='grdHeader'><span id='lblPRepCode_" + i + "'>" + response.d[i].Rep_ID + "</span></td>";
                    tr += "<td class='grdHeader'><span id='lblParameterCode_" + i + "'>" + response.d[i].Param_ID + "</span></td>";
                    tr += "<td><span id='lblParameterName_" + i + "'>" + response.d[i].Parameter_Name + "</span></td>";
                    tr += "<td><span id='lblParameterStatus_" + i + "'>" + response.d[i].Active + "</span></td>";
                    tr += "<td><a class='editP' id='btnEdit_" + i + "' title='Edit' href='#' data-toggle='tooltip'><i class=\"fa fa-pencil\"></i></a></td>";
                    tr += "<td><a class='addP' id='btnAdd_" + i + "' title='Add Sub-Parameter' href='#' data-toggle='tooltip'><i class=\"fa fa-plus\"></i></a> / <a class='viewSP' id='btnView_" + i + "' title='View Sub-Parameter' href='#' data-toggle='tooltip'><i class=\"fa fa-eye\"></i></a><a class='hideSP' id='btnHideSP_" + i + "' title='Close Sub-Parameter' href='#' data-toggle='tooltip'><i class=\"fa fa-eye-slash\"></i></a></td>";
                    tr += '</tr>';
                }
                tbody += tr + "</tbody>";
                th += tbody;
                th += '</table></td></tr>';

                $(th).insertAfter(row);

                $('.hideSP').hide();
            } else {
                $.alert('No Records Found!', 'Alert!');
            }
        },
        error: function ajaxError(reponse) { }
    })
}

function Bind_Sub_Parameter(row, rep_ID, param_ID) {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../MR/webservice/Report_Master_WebService.asmx/GetSubParameter',
        data: '{objRep_ID:' + JSON['stringify'](rep_ID) + ', objParamID: ' + JSON['stringify'](param_ID) + '}',
        dataType: 'json',
        success: function (response) {
            if ($.trim(response.d)) {
                var th = '<tr><td></td><td colspan="4"><table class="table"><thead><th>#</th><th class="grdHeader">Rep_ID</th><th class="grdHeader">Param_ID</th><th>Sub-Parameter Name</th><th>Status</th><th>Edit</th></thead>';
                var tbody = '<tbody>';
                var tr = '<tr>';
                var sno = '';
                for (var i = 0; i < response.d.length; i++) {
                    sno = i + 1;
                    tr += "<td><span id='lblSNo_" + i + "'>" + sno + "</span></td>";
                    tr += "<td class='grdHeader'><span id='lblPRepCode_" + i + "'>" + response.d[i].Rep_ID + "</span></td>";
                    tr += "<td class='grdHeader'><span id='lblParameterCode_" + i + "'>" + response.d[i].Param_ID + "</span></td>";
                    tr += "<td class='grdHeader'><span id='lblSubParameterCode_" + i + "'>" + response.d[i].Sub_Param_ID + "</span></td>";
                    tr += "<td><span id='lblSubParameterName_" + i + "'>" + response.d[i].Sub_Parameter_Name + "</span></td>";
                    tr += "<td><span id='lblSubParameterStatus_" + i + "'>" + response.d[i].Active + "</span></td>";
                    tr += "<td><a class='editSP' id='btnEdit_" + i + "' title='Edit' href='#' data-toggle='tooltip'><i class=\"fa fa-pencil\"></i></a></td>";
                    //tr += "<td><a class='addSP' id='btnAdd_" + i + "' title='Add Sub-Parameter' href='#' data-toggle='tooltip'><i class=\"fa fa-plus\"></i></a> / <a class='viewSP' id='btnView_" + i + "' title='View Sub-Parameter' href='#' data-toggle='tooltip'><i class=\"fa fa-eye\"></i></a><a class='hideSP' id='btnHideSP_" + i + "' title='Close Sub-Parameter' href='#' data-toggle='tooltip'><i class=\"fa fa-eye-slash\"></i></a></td>";
                    tr += '</tr>';
                }
                tbody += tr + "</tbody>";
                th += tbody;
                th += '</table></td></tr>';

                $(th).insertAfter(row);
            } else {
                $.alert('No Records Found!', 'Alert!');
            }
        },
        error: function ajaxError(reponse) { }
    })
}

$(document).on('click', '.editSP', function (e) {
    e.preventDefault();

    var rep_ID, param_ID, sub_Param_ID, sub_Param_Name, param_Status = '';

    var row = $(this).closest("tr");
    rep_ID = row.find('span:eq(1)').text();
    param_ID = row.find('span:eq(2)').text();
    sub_Param_ID = row.find('span:eq(3)').text();
    sub_Param_Name = row.find('span:eq(4)').text();
    sub_Param_Status = row.find('span:eq(5)').text();

    $('#btnAddRep').prop('disabled', true);
    $('.divRep').hide();
    $('.divParam').hide();
    $('.divSubParam').show();
    
    $("#hdnRep_ID").val(rep_ID);
    $("#hdnParameterID").val(param_ID);
    $("#hdnSubParamID").val(sub_Param_ID);
    $("#txtSubParam").val(sub_Param_Name);

    if (sub_Param_Status == "Active") {
        $("#chkSubParam").prop("checked", true);
    } else {
        $("#chkSubParam").prop("checked", false);
    }
});