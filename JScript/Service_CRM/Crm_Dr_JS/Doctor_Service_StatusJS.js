$(document)['ready'](function () {
    $('#lblDivCode')['hide']();
    document.getElementById("DDdisplay").style.display = "none";
    $('#ddlDivision')['hide']();
    var _0xc353x1 = $('#Session_SfCode')['val']();
    var _0xc353x2 = $('#Session_SfType')['val']();
    var _0xc353x3 = $('#SS_DivCode')['val']();
    if (_0xc353x2 == '2') {
        BindDivision_DDL(_0xc353x1, _0xc353x2)
    };
    if (_0xc353x2 == '3') {
        BindField_Force_DDL(_0xc353x3)
    };
    if (_0xc353x2 == '1') {
        BindField_Force_DDL(_0xc353x3)
    };
    $('#ddlDivision')['change'](function () {
        var _0xc353x4 = $('#ddlDivision')['val']();
        BindField_Force_DDL(_0xc353x4)
    });
    BindYear_DDL();
    var _0xc353x5 = new Date();
    n = _0xc353x5['getMonth']() + 1;
    $('#ddlMonth option:eq(' + n + ')')['prop']('selected', true);
    $('#ddlTMonth option:eq(' + n + ')')['prop']('selected', true)
  
});

function BindDivision_DDL(_0xc353x1, _0xc353x2) {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/DrService_Analysis_WebService.asmx/GetDivision_DDL',
        data: '{SS_SCode:' + JSON['stringify'](_0xc353x1) + ',SS_Stype:' + JSON['stringify'](_0xc353x2) + '}',
        dataType: 'json',
        success: function (_0xc353x7) {
            if (_0xc353x7['d']['length'] > 0) {
                $('#lblDivCode')['show']();
                document.getElementById("DDdisplay").style.display = "block";
                $('#ddlDivision')['show']();
                $('#ddlDivision')['empty']();
                $['each'](_0xc353x7['d'], function (_0xc353x8, _0xc353x9) {
                    $('#ddlDivision')['append']($('<option></option>')['val'](_0xc353x9.Division_Code)['html'](_0xc353x9.Division_Name))
                });
                $('#ddlDivision option:eq(1)')['prop']('selected', true);
                BindField_Force_DDL($('#ddlDivision')['val']())
            } else {
                $('#lblDivCode')['hide']();
                document.getElementById("DDdisplay").style.display = "none";
                $('#ddlDivision')['hide']();
                BindField_Force_DDL($('#SS_DivCode')['val']())
            }
        },
        error: function _0xc353xa(_0xc353x7) { }
    })
}

function BindField_Force_DDL(_0xc353x4) {
    $('#divLoad')['css']('display', 'block');
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/DrService_Analysis_WebService.asmx/GetFieldForceName',
        data: '{SS_DivCode:' + JSON['stringify'](_0xc353x4) + '}',
        dataType: 'json',
        success: function (_0xc353x7) {
            $('#ddlFieldForce')['empty']();
            $['each'](_0xc353x7['d'], function (_0xc353x8, _0xc353x9) {
                $('#ddlFieldForce')['append']($('<option></option>')['val'](_0xc353x9.Field_Sf_Code)['html'](_0xc353x9.Field_Sf_Name))
            });
            $('#ddlFieldForce')['trigger']('change');
            $('#divLoad')['css']('display', 'none')
        },
        error: function _0xc353xa(_0xc353x7) {
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
        success: function (_0xc353xd) {
            $('#ddlYear')['empty']();
            var _0xc353xe = _0xc353xd['d'][0]['Year'];
            var _0xc353xf = new Date()['getFullYear']();
            for (var _0xc353x10 = parseInt(_0xc353xe) ; _0xc353x10 <= _0xc353xf; _0xc353x10++) {
                $('#ddlYear')['append']($('<option></option>')['val'](_0xc353x10)['html'](_0xc353x10));
                $('#ddlTYear')['append']($('<option></option>')['val'](_0xc353x10)['html'](_0xc353x10))
            };
            $('#ddlYear option:contains(\'' + _0xc353xf + '\')')['attr']('selected', 'selected');
            $('#ddlTYear option:contains(\'' + _0xc353xf + '\')')['attr']('selected', 'selected')
        },
        error: function _0xc353xa(_0xc353x7) {
            alert('Error')
        }
    })
}