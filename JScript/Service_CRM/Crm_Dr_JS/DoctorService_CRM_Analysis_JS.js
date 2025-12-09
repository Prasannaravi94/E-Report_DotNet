$(document)['ready'](function () {   
    $('#lblProduct')['show']();
    $('#ddlProduct')['show']();
    document.getElementById("ProductDD").style.display = "block";
    $('#lblDoctors')['hide']();
    $('#ddlDoctors')['hide']();
    document.getElementById("DoctorDD").style.display = "none";
    $('#lblChemist')['hide']();
    $('#ddlChemist')['hide']();
    document.getElementById("ChemistDD").style.display = "none";
    $('#lblType')['show']();
    $('#ddlPrdModeType')['show']();
    document.getElementById("PrdModeTypeDD").style.display = "block";
    BindField_Force_DDL();
    BindYear_DDL();
    $('#divLoad')['css']('display', 'none');
    var _0xf9bex1 = new Date();
    n = _0xf9bex1['getMonth']() + 1;
    $('#ddlMonth option:eq(' + n + ')')['prop']('selected', true);
    $('#ddlTMonth option:eq(' + n + ')')['prop']('selected', true);
    $('#ddlMode')['change'](function () {
        var _0xf9bex2 = $('#ddlMode')['val']();
        if (_0xf9bex2 == '2') {
            $('#lblProduct')['show']();
            $('#ddlProduct')['show']();
            document.getElementById("ProductDD").style.display = "block";
            $('#lblDoctors')['hide']();
            $('#ddlDoctors')['hide']();
            document.getElementById("DoctorDD").style.display = "none";
            $('#lblChemist')['hide']();
            $('#ddlChemist')['hide']();
            document.getElementById("ChemistDD").style.display = "none";
            $('#lblType')['show']();
            $('#ddlPrdModeType')['show']();
            document.getElementById("PrdModeTypeDD").style.display = "block";
            if ($('#ddlFieldForce')['val']() != '') {
                $('#ddlFieldForce')['trigger']('change')
            }
        } else {
            if (_0xf9bex2 == '3') {
                $('#lblDoctors')['show']();
                $('#ddlDoctors')['show']();
                document.getElementById("DoctorDD").style.display = "block";
                $('#lblProduct')['hide']();
                $('#ddlProduct')['hide']();
                document.getElementById("ProductDD").style.display = "none";
                $('#lblChemist')['hide']();
                $('#ddlChemist')['hide']();
                document.getElementById("ChemistDD").style.display = "none";
                $('#lblType')['hide']();
                $('#ddlPrdModeType')['hide']();
                document.getElementById("PrdModeTypeDD").style.display = "none";
                if ($('#ddlFieldForce')['val']() != '') {
                    $('#ddlFieldForce')['trigger']('change')
                }
            } else {
                if (_0xf9bex2 == '4') {
                    $('#lblDoctors')['hide']();
                    $('#ddlDoctors')['hide']();
                    document.getElementById("DoctorDD").style.display = "none";
                    $('#lblProduct')['hide']();
                    $('#ddlProduct')['hide']();
                    document.getElementById("ProductDD").style.display = "none";
                    $('#lblChemist')['show']();
                    $('#ddlChemist')['show']();
                    document.getElementById("ChemistDD").style.display = "block";
                    $('#lblType')['hide']();
                    $('#ddlPrdModeType')['hide']();
                    document.getElementById("PrdModeTypeDD").style.display = "none";
                    if ($('#ddlFieldForce')['val']() != '') {
                        $('#ddlFieldForce')['trigger']('change')
                    }
                }
            }
        }
    });
    $('#ddlFieldForce')['change'](function () {
        var _0xf9bex3 = $('#ddlMode option:selected')['text']();
        var _0xf9bex4 = $('#ddlFieldForce')['val']();
        var _0xf9bex2 = $('#ddlMode')['val']();
        if (_0xf9bex4 == 'admin') {
            if (_0xf9bex2 == '2') {
                $('#ddlProduct')['empty']();
                $('#ddlProduct')['append']('<option value=\'0\'>--All--</option>')
            } else {
                if (_0xf9bex2 == '3') {
                    $('#ddlDoctors')['empty']();
                    $('#ddlDoctors')['append']('<option value=\'0\'>--All--</option>')
                } else {
                    if (_0xf9bex2 == '4') {
                        $('#ddlChemist')['empty']();
                        $('#ddlChemist')['append']('<option value=\'0\'>--All--</option>')
                    }
                }
            }
        } else {
            if (_0xf9bex2 == '2') {
                BindProduct(_0xf9bex4)
            } else {
                if (_0xf9bex2 == '3') {
                    BindDoctorDetail(_0xf9bex4)
                } else {
                    if (_0xf9bex2 == '4') {
                        BindChemistDetail(_0xf9bex4)
                    }
                }
            }
        }
    });
    $('#btnGo')['click'](function () { })
});

function BindField_Force_DDL() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/DrService_Analysis_WebService.asmx/Get_FieldForce_Name',
        data: '{}',
        dataType: 'json',
        success: function (_0xf9bex6) {
            $('#ddlFieldForce')['empty']();
            $['each'](_0xf9bex6['d'], function (_0xf9bex7, _0xf9bex8) {
                $('#ddlFieldForce')['append']($('<option></option>')['val'](_0xf9bex8.Field_Sf_Code)['html'](_0xf9bex8.Field_Sf_Name))
            });
            $('#ddlFieldForce')['trigger']('change')
        },
        error: function _0xf9bex9(_0xf9bex6) {
            alert('Error')
        }
    })
}

function BindHospital(_0xf9bex4) {
    var _0xf9bexb = _0xf9bex4;
    $('#divLoad')['css']('display', 'block');
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/DrService_Analysis_WebService.asmx/GetHospital',
        data: '{objSfCode:' + JSON['stringify'](_0xf9bexb) + '}',
        dataType: 'json',
        success: function (_0xf9bex6) {
            $('#ddlHospital')['empty']();
            $('#ddlHospital')['append']('<option value=\'0\'>--All--</option>');
            $['each'](_0xf9bex6['d'], function (_0xf9bex7, _0xf9bex8) {
                $('#ddlHospital')['append']($('<option></option>')['val'](_0xf9bex8.Hospital_Code)['html'](_0xf9bex8.Hospital_Name))
            });
            $('#divLoad')['css']('display', 'none')
        },
        error: function _0xf9bex9(_0xf9bex6) {
            //createCustomAlert('Error');
            alert('Error');
            $('#divLoad')['css']('display', 'none')
        }
    })
}

function BindProduct(_0xf9bex4) {
    var _0xf9bexb = _0xf9bex4;
    $('#divLoad')['css']('display', 'block');
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/DrService_Analysis_WebService.asmx/GetProductDel',
        data: '{objPrdSf:' + JSON['stringify'](_0xf9bexb) + '}',
        dataType: 'json',
        success: function (_0xf9bex6) {
            $('#ddlProduct')['empty']();
            $('#ddlProduct')['append']('<option value=\'0\'>--All--</option>');
            $['each'](_0xf9bex6['d'], function (_0xf9bex7, _0xf9bex8) {
                $('#ddlProduct')['append']($('<option></option>')['val'](_0xf9bex8.Product_Code)['html'](_0xf9bex8.Product_Name))
            });
            $('#divLoad')['css']('display', 'none')
        },
        error: function _0xf9bex9(_0xf9bex6) {
            alert('Error');
            $('#divLoad')['css']('display', 'none')
        }
    })
}

function BindDoctorDetail(_0xf9bex4) {
    var _0xf9bexb = _0xf9bex4;
    $('#divLoad')['css']('display', 'block');
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/DrService_Analysis_WebService.asmx/GetDoctorList',
        data: '{objDrLst:' + JSON['stringify'](_0xf9bexb) + '}',
        dataType: 'json',
        success: function (_0xf9bex6) {
            $('#ddlDoctors')['empty']();
            $('#ddlDoctors')['append']('<option value=\'0\'>--All--</option>');
            $['each'](_0xf9bex6['d'], function (_0xf9bex7, _0xf9bex8) {
                $('#ddlDoctors')['append']($('<option></option>')['val'](_0xf9bex8.ListedDrCode)['html'](_0xf9bex8.ListedDr_Name))
            });
            $('#divLoad')['css']('display', 'none')
        },
        error: function _0xf9bex9(_0xf9bex6) {
            alert('Error');
            $('#divLoad')['css']('display', 'none')
        }
    })
}

function BindChemistDetail(_0xf9bex4) {
    var _0xf9bexb = _0xf9bex4;
    $('#divLoad')['css']('display', 'block');
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/DrService_Analysis_WebService.asmx/GetChemist_DDL',
        data: '{objSfCode:' + JSON['stringify'](_0xf9bexb) + '}',
        dataType: 'json',
        success: function (_0xf9bex6) {
            $('#ddlChemist')['empty']();
            $('#ddlChemist')['append']('<option value=\'0\'>--All--</option>');
            $['each'](_0xf9bex6['d'], function (_0xf9bex7, _0xf9bex8) {
                $('#ddlChemist')['append']($('<option></option>')['val'](_0xf9bex8.Chemists_Code)['html'](_0xf9bex8.Chemists_Name))
            });
            $('#divLoad')['css']('display', 'none')
        },
        error: function _0xf9bex9(_0xf9bex6) {
            alert('Error');
            $('#divLoad')['css']('display', 'none')
        }
    })
}

function BindYear_DDL() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/DrService_Analysis_WebService.asmx/FillYear',
        data: '{}',
        dataType: 'json',
        success: function (_0xf9bex10) {
            $('#ddlYear')['empty']();
            var _0xf9bex11 = _0xf9bex10['d'][0]['Year'];
            var _0xf9bex12 = new Date()['getFullYear']();
            for (var _0xf9bex13 = parseInt(_0xf9bex11) ; _0xf9bex13 <= _0xf9bex12; _0xf9bex13++) {
                $('#ddlYear')['append']($('<option></option>')['val'](_0xf9bex13)['html'](_0xf9bex13));
                $('#ddlTYear')['append']($('<option></option>')['val'](_0xf9bex13)['html'](_0xf9bex13))
            };
            $('#ddlYear option:contains(\'' + _0xf9bex12 + '\')')['attr']('selected', 'selected');
            $('#ddlTYear option:contains(\'' + _0xf9bex12 + '\')')['attr']('selected', 'selected')
        },
        error: function _0xf9bex9(_0xf9bex6) {
            alert('Error')
        }
    })
}