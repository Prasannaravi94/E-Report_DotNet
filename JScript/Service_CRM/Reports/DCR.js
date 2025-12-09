var DCR_SfCode = "";
var DCR_SfType = "";
var DCR_DivCode = "";
var arr = "";
var rep = "DCR_Analysis";
var rep_IDs = [];
var rep_ID = "";
var usr_Setting = [];
var param = [];
var sub_param = [];

$(document).ready(function () {
    DCR_SfCode = $('#DCR_SfCode').val();
    DCR_SfType = $('#DCR_SfType').val();
    DCR_DivCode = $('#DCR_DivCode').val();

    $(document).ajaxStart(function () {
        $('#loader').css("display", "block")
    })['ajaxStop'](function () {
        $('#loader').css("display", "none")
    });

    BindField_Force_DDL();

    $(".selectpicker").selectpicker();

    $('#btnGO').click(function (e) {
        e.preventDefault();

        if ($("#ddlFieldForce").val() != 0) {
            var sf_Code = $("#ddlFieldForce").val();
            var date = $("#date").text();
            arr = sf_Code + "^" + date;
            GetReportData(arr);
        }
        else {
            $.alert('Select FieldForce!', 'Alert!');
        }
    });

    $('#btnSave').click(function (e) {
        e.preventDefault();

        if ($('#ddlSettings').val() == "-1") {
            if ($("#txtSetting").val() == "") {
                $.alert('Enter Setting Name!', 'Alert!');
            } else {
                UpdateUserSetting();
            }
        } else if ($('#ddlSettings').val() == "0") {

        } else {
            UpdateUserSetting();
        }
    });
});

function BindField_Force_DDL() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../MR/webservice/DCR_WebService.asmx/GetFieldForceName',
        data: '{}',
        dataType: 'json',
        success: function (response) {
            $('#ddlFieldForce').empty();
            $('#ddlFieldForce').append('<option value=\'0\'>---Select---</option>');
            $.each(response.d, function () {
                if (this.Field_Sf_Name != "---Select Clear---") {
                    $("#ddlFieldForce").append($("<option/>").val(this.Field_Sf_Code).text(this.Field_Sf_Name));
                }
            });
            $("#ddlFieldForce").prop('selectedIndex', 2);
            $("#ddlFieldForce").selectpicker("refresh");
        },
        error: function ajaxError(reponse) { }
    })

    FillDate();
}

function FillDate() {
    //var start = moment().subtract(29, 'days');
    var start = moment();
    var end = moment();

    function cb(start, end) {
        $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
    }

    $('#reportrange').daterangepicker({
        startDate: start,
        endDate: end,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        }
        //maxSpan: {
        //    'days': 30
        //},
    }, cb);

    cb(start, end);
}

function GetUserSetting() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../MR/webservice/DCR_WebService.asmx/GetUserSetting',
        data: '{}',
        data: '{obj_Rep:' + JSON['stringify'](rep) + '}',
        dataType: 'json',
        success: function (response) {
            FillUserSettings(response);
        },
        error: function ajaxError(reponse) { }
    })
}

function GetReportData(arr) {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../MR/webservice/DCR_WebService.asmx/GetReportData',
        data: '{}',
        data: '{obj_Rep:' + JSON['stringify'](arr) + '}',
        dataType: 'json',
        success: function (response) {
            if ($.trim(response.d)) {
                var rep_Data = $.parseJSON(response.d);

                var col = [];
                for (var i = 0; i < rep_Data.length; i++) {
                    for (var key in rep_Data[i]) {
                        if (col.indexOf(key) === -1) {
                            col.push(key);
                        }
                    }
                }

                var table = document.createElement("table");
                $(table).addClass('display compact nowrap row-border cell-border order-column');
                $(table).attr('id', 'tblDCR')
                $(table).attr("cellspacing", 0);
                $(table).attr("cellpadding", 0);

                var thead = document.createElement("thead");
                var tbody = document.createElement("tbody");
                var trth = thead.insertRow(-1);
                var tr;

                for (var i = 0; i < col.length; i++) {
                    var th = document.createElement("th");
                    th.innerHTML = col[i];
                    trth.appendChild(th);
                }



                for (var i = 0; i < rep_Data.length; i++) {

                    tr = tbody.insertRow(-1);

                    for (var j = 0; j < col.length; j++) {
                        var tabCell = tr.insertCell(-1);
                        tabCell.innerHTML = rep_Data[i][col[j]];
                    }
                }

                table.appendChild(thead);
                table.appendChild(tbody);

                var divContainer = document.getElementById("tblDCR1");
                divContainer.innerHTML = "";
                divContainer.appendChild(table);

                $('#tblDCR').DataTable().destroy();

                var table = $('#tblDCR').DataTable({
                    dom: 'Bfrtip',
                    destroy: true,
                    ordering: true,
                    info: false,
                    searching: false,
                    deferRender: true,
                    scrollY: '500',
                    scrollX: true,
                    paging: false,
                    fixedColumns: {
                        leftColumns: 2
                    }
                });

                //$(".sorting_asc").remove();
            }
        },
        error: function ajaxError(reponse) { }
    })
}

function FillUserSettings(setting) {

    rep_IDs = $.map(setting.d, function (element) {
        return { Rep_ID: element.Rep_ID };
    });

    rep_ID = rep_IDs[0].Rep_ID;

    usr_Setting = $.map(setting.d, function (element) {
        return { Setting_ID: element.Setting_ID, Setting_Name: element.Setting_Name, Default_Flag: element.Default_Flag };
    });

    param = $.map(setting.d, function (element) {
        return { Setting_ID: element.Setting_ID, Param_ID: element.Param_ID, Parameter_Name: element.Parameter_Name, Parameter_Order: element.Parameter_Order, Parameter_Flag: element.Parameter_Flag, Sub_Parameter_Flag: element.Sub_Parameter_Flag };
    });

    sub_param = $.map(setting.d, function (element) {
        return { Setting_ID: element.Setting_ID, Param_ID: element.Param_ID, Sub_Param_ID: element.Sub_Param_ID, Sub_Parameter_Name: element.Sub_Parameter_Name, Sub_Parameter_Order: element.Sub_Parameter_Order, Sub_Flag: element.Sub_Flag };
    });

    param = GetUnique(param);

    Bind_ddlSettings();
}

function Bind_ddlSettings() {

    var setting_Flag = $.grep(usr_Setting, function (v) {
        return v.Default_Flag === "1" && v.Setting_Name != '';
    });

    $('#ddlSettings').empty();
    $('#ddlSettings').append('<option value=\'0\'>Default</option>');
    $.each(usr_Setting, function () {
        if (this.Setting_Name !== "") {
            $("#ddlSettings").append($("<option/>").val(this.Setting_ID).text(this.Setting_Name));
        }
    });
    if (setting_Flag.length != 0) {
        $('#ddlSettings option[value=' + setting_Flag[0].Setting_ID + ']').attr('selected', 'selected');
        $("#chkDefault").prop("checked", true);
        BindUserSetting();
    } else {
        Bind_DefaultSetting();
    }
    $('#ddlSettings').append('<option value=\'-1\'>--Create New--</option>');
}

$("#ddlSettings").change(function () {
    if ($('#ddlSettings').val() == "-1") {
        $(".chkDefault").show();
        $(".newSetting").empty();
        var input = $('<input/>', { class: "form-control", type: 'text', id: "txtSetting", placeholder: "Enter Setting Name" });
        $(".newSetting").append(input);
    } else if ($('#ddlSettings').val() == "0") {
        $(".chkDefault").hide();
        $(".newSetting").empty();
    }
    else {
        $(".newSetting").empty();
    }
});

function Bind_DefaultSetting() {
    $(".chkDefault").hide();
    $(".Parameters").empty();
    param.sort(SortByName)

    var root_ul = $('<ul/>', { class: 'list-group sortable' });

    $.each(param, function (index) {
        var param_idx = index;
        var param_li = $('<li/>', { class: 'list-group-item param_' + param_idx });
        var param_i = $('<i/>', { class: 'fa fa-arrows-alt pull-left paramI_' + param_idx });
        var param_div = $('<div/>', { class: 'custom-control custom-checkbox icon-left paramDiv_' + param_idx });
        var param_a = $('<a/>', { class: 'collapsed', "data-toggle": 'collapse', href: '#paramA_' + param_idx, role: 'button', "aria-expanded": "true", "aria-controls": "paramA_" + param_idx });
        var param_hdn = $('<input/>', { type: 'hidden', value: this.Param_ID, id: 'hdnParam_' + param_idx });

        var param_chk = $('<input/>', { type: 'checkbox', class: 'custom-control-input', id: 'chkParam_' + param_idx });
        var param_lbl = $('<label/>', { class: "custom-control-label", for: 'chkParam_' + param_idx, text: this.Parameter_Name });

        if (this.Parameter_Flag == "1") {
            $(param_chk).prop("checked", true);
        } else {
            $(param_chk).prop("checked", false);
        }

        param_div.append(param_chk);
        param_div.append(param_lbl);

        param_li.append(param_i);
        param_li.append(param_div);
        param_li.append(param_hdn);

        if (this.Sub_Parameter_Flag == "1") {
            var param_Span = $('<span/>', { class: 'mr-3' });
            param_a.append(param_Span);

            param_li.append(param_a);

            var sub_div1 = $('<div/>', { class: "collapse", id: "paramA_" + param_idx });
            var sub_div2 = $('<div/>', { class: "box mt-2" });
            var sub_ul = $('<ul/>', { class: 'list-group sub-sort' });

            $.each(sub_param, function (index) {
                if (this.Sub_Param_ID != "" && this.Sub_Parameter_Name != "") {
                    var sub_li = $('<li/>', { class: 'list-group-item sub_' + param_idx + '_' + index });
                    var sub_i = $('<i/>', { class: 'fa fa-arrows-alt pull-left subI_' + param_idx + '_' + index });
                    var sub_div = $('<div/>', { class: 'custom-control custom-checkbox icon-left subDiv_' + param_idx + '_' + index });
                    var sub_hdn = $('<input/>', { type: 'hidden', value: this.Sub_Param_ID, id: 'hdnSubParam_' + param_idx + '_' + index });

                    var sub_chk = $('<input/>', { type: 'checkbox', class: 'custom-control-input', id: 'chkSub_' + param_idx + '_' + index });
                    var sub_lbl = $('<label/>', { class: "custom-control-label", for: 'chkSub_' + param_idx + '_' + index, text: this.Sub_Parameter_Name });

                    if (this.Sub_Flag == "1") {
                        $(sub_chk).prop("checked", true);
                    } else {
                        $(sub_chk).prop("checked", false);
                    }

                    sub_div.append(sub_chk);
                    sub_div.append(sub_lbl);

                    sub_li.append(sub_i);
                    sub_li.append(sub_div);
                    sub_li.append(sub_hdn);

                    sub_ul.append(sub_li);
                    sub_div2.append(sub_ul);
                    sub_div1.append(sub_div2);

                    param_li.append(sub_div1);
                }
            });
        }
        root_ul.append(param_li);
    });
    $(".Parameters").append(root_ul);

    $(".sortable").sortable({
        update: function (event, ui) { }
    });

    $(".sub-sort").sortable({
        update: function (event, ui) { }
    });
}

$(".sortable").on("sortupdate", function (event, ui) {

});

$(".sub-sort").on("sortupdate", function (event, ui) {

});

function UpdateUserSetting() {
    if ($('#ddlSettings').val() == "-1") {
        $("#txtSetting").val();

    } else if ($('#ddlSettings').val() == "0") {

    } else {
        UpdateUserSetting();
    }
}

function SortByName(a, b) {
    var aName = a.Parameter_Order.toLowerCase();
    var bName = b.Parameter_Order.toLowerCase();
    return ((aName < bName) ? -1 : ((aName > bName) ? 1 : 0));
}

function GetUnique(inputArray) {
    var outputArray = inputArray.reduce(function (memo, e1) {
        var matches = memo.filter(function (e2) {
            return e1.Parameter_Name == e2.Parameter_Name && e1.Param_ID == e2.Param_ID
        })
        if (matches.length == 0)
            memo.push(e1)
        return memo;
    }, [])

    return outputArray;
}

$('#modelSettings').on('show.bs.modal', function (event) {
    $(".newSetting").empty();
    GetUserSetting();
});