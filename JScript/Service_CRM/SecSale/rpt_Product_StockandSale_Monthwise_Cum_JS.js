var ArrHQDet = {};
var dataTableSt;
var ColMonth = '';
var ArrSale = [];
var ArrTransit = [];
var ArrFree = [];
var ArrMonth = [];
var StockName = '';
$(document)['ready'](function () {
    $('.modal')['ajaxStart'](function () {
        $(this)['show']()
    })['ajaxStop'](function () {
        $(this)['hide']()
    });
    GetHQ_Det();
    var _0x3594x9 = $('#SS_DivCode')['val']();
    var _0x3594xa = GetParameterValues('sfcode');
    var _0x3594xb = GetParameterValues('Sf_Name');
    var _0x3594xc = GetParameterValues('FMonth');
    var _0x3594xd = GetParameterValues('FYear');
    var _0x3594xe = GetParameterValues('TMonth');
    var _0x3594xf = GetParameterValues('TYear');
    var _0x3594x10 = GetParameterValues('Param');

    function _0x3594x11() {
        var _0x3594x12 = window['location']['search'];
        var _0x3594x13 = /([^?&=]*)=([^&]*)/g;
        var _0x3594x14 = {};
        var _0x3594x15 = null;
        while (_0x3594x15 = _0x3594x13['exec'](_0x3594x12)) {
            _0x3594x14[_0x3594x15[1]] = decodeURIComponent(_0x3594x15[2])
        };
        return _0x3594x14
    }
    var _0x3594x14 = _0x3594x11();
    try {
        var _0x3594x16 = JSON['parse'](_0x3594x14.Param);
        ArrSale = _0x3594x16;
        var _0x3594x17 = JSON['parse'](_0x3594x14.StockName);
        StockName = _0x3594x17['replace']('_', ' ')
    } catch (err) { };
    var _0x3594x18 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0x3594x19 = parseInt(_0x3594xc - 1);
    var _0x3594x1a = parseInt(_0x3594xe - 1);
    var _0x3594x1b = _0x3594x18[_0x3594x19];
    var _0x3594x1c = _0x3594x18[_0x3594x1a];
    if (_0x3594xc != _0x3594xe) {
        ColMonth = _0x3594x1b + '-' + _0x3594x1c
    } else {
        ColMonth = _0x3594x1b + '-' + _0x3594x1c
    };
    var _0x3594x1d = 'Product - (Stock & Sale) Consolidate for ' + _0x3594x1b + ' ' + _0x3594xd + ' to ' + ' ' + _0x3594x1c + ' ' + ' ' + _0x3594xf;
    var _0x3594x1e = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0x3594xb + '</span>&nbsp - &nbsp;&nbsp;&nbsp;(<span style=\'color:#0077ff\'>Stockist Name :</span><span style=\'color:#c17111\'> ' + StockName + '</span>)';
    $('#lblStock')['html'](_0x3594x1d);
    $('#lblField')['html'](_0x3594x1e);
    var _0x3594x1f = (parseInt(_0x3594xf) - parseInt(_0x3594xd)) * 12 + parseInt(_0x3594xe) - parseInt(_0x3594xc);
    var _0x3594x20 = parseInt(_0x3594xc);
    var _0x3594x21 = parseInt(_0x3594xd);
    if (_0x3594x1f >= 0) {
        for (var _0x3594x22 = 1; _0x3594x22 <= _0x3594x1f + 1; _0x3594x22++) {
            var _0x3594x19 = parseInt(_0x3594x20 - 1);
            var _0x3594x1b = _0x3594x18[_0x3594x19];
            var _0x3594x23 = _0x3594x1b;
            ArrMonth['push'](_0x3594x23);
            _0x3594x20 = _0x3594x20 + 1;
            if (_0x3594x20 == 13) {
                _0x3594x20 = 1;
                _0x3594x21 = _0x3594x21 + 1
            }
        }
    };
    GetRecordData()
});

function GetParameterValues(_0x3594x25) {
    var _0x3594x26 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0x3594x15 = 0; _0x3594x15 < _0x3594x26['length']; _0x3594x15++) {
        var _0x3594x27 = _0x3594x26[_0x3594x15]['split']('=');
        if (_0x3594x27[0] == _0x3594x25) {
            return decodeURIComponent(_0x3594x27[1])
        }
    }
}

function GetRecordData() {
    var _0x3594x29 = '';
    var _0x3594x2a = '';
    var _0x3594x2b = '';
    for (var _0x3594x15 = 0; _0x3594x15 < ArrSale['length']; _0x3594x15++) {
        _0x3594x29 += ArrSale[_0x3594x15] + '#'
    };
    _0x3594x29 = _0x3594x29['slice'](0, -1);
    _0x3594x2a = _0x3594x2a['slice'](0, -1);
    _0x3594x2b = _0x3594x2b['slice'](0, -1);
    var _0x3594x2c = _0x3594x29;
    var _0x3594x2d = {};
    var _0x3594x2e = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/GetStock_and_Sale_Consolidate_Cum',
        data: '{Param:' + JSON['stringify'](_0x3594x2c) + '}',
        dataType: 'json',
        success: function (_0x3594x16) {
            dataTableSt = _0x3594x16['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                var _0x3594x2f = ['Prod_Name', 'Pack', 'Product_ERP_Code'];
                var _0x3594x30 = getPivotArray_Test(dataTableSt, _0x3594x2f, 'VST', 'Value');
                var _0x3594x31 = '<table id=tblSale class="table" style="min-width:95%;max-width:100%">';
                _0x3594x31 += '<tr>';
                _0x3594x31 += '<th rowspan="3" >#</th>';
                _0x3594x31 += '<th rowspan="3" >Product Name</th>';
                _0x3594x31 += '<th rowspan="3" >Product ERP Code</th>';
                _0x3594x31 += '<th rowspan="3" >Pack</th>';
                var _0x3594x32 = ArrSale['length'];
                var _0x3594x33 = parseInt(_0x3594x32) * 2;
                for (var _0x3594x34 = 0; _0x3594x34 < ArrMonth['length']; _0x3594x34++) {
                    _0x3594x31 += '<th colspan="' + _0x3594x33 + '" >' + ArrMonth[_0x3594x34] + '</th>'
                };
                _0x3594x31 += '<th colspan="' + _0x3594x33 + '" >Total</th>';
                _0x3594x31 += '</tr>';
                _0x3594x31 += '<tr>';
                for (var _0x3594x34 = 0; _0x3594x34 < ArrMonth['length']; _0x3594x34++) {
                    for (var _0x3594x35 = 0; _0x3594x35 < ArrHQDet['length']; _0x3594x35++) {
                        var _0x3594x36 = ArrHQDet[_0x3594x35]['Sec_Sale_Code'];
                        var _0x3594x37 = '';
                        for (var _0x3594x15 = 0; _0x3594x15 < ArrSale['length']; _0x3594x15++) {
                            var _0x3594x29 = ArrSale[_0x3594x15];
                            var _0x3594x10 = [];
                            _0x3594x10 = _0x3594x29['split']('^');
                            if ($['trim'](_0x3594x36) == $['trim'](_0x3594x10[0])) {
                                _0x3594x37 = '2';
                                _0x3594x31 += '<th   colspan="' + _0x3594x37 + '" >' + ArrHQDet[_0x3594x35]['Sec_Sale_Name'] + '</th>';
                                break
                            }
                        }
                    }
                };
                for (var _0x3594x35 = 0; _0x3594x35 < ArrHQDet['length']; _0x3594x35++) {
                    var _0x3594x36 = ArrHQDet[_0x3594x35]['Sec_Sale_Code'];
                    var _0x3594x37 = '';
                    for (var _0x3594x15 = 0; _0x3594x15 < ArrSale['length']; _0x3594x15++) {
                        var _0x3594x29 = ArrSale[_0x3594x15];
                        var _0x3594x10 = [];
                        _0x3594x10 = _0x3594x29['split']('^');
                        if ($['trim'](_0x3594x36) == $['trim'](_0x3594x10[0])) {
                            _0x3594x37 = '2';
                            _0x3594x31 += '<th   colspan="' + _0x3594x37 + '" >' + ArrHQDet[_0x3594x35]['Sec_Sale_Name'] + '</th>';
                            break
                        }
                    }
                };
                _0x3594x31 += '</tr>';
                _0x3594x31 += '<tr>';
                for (var _0x3594x34 = 0; _0x3594x34 < ArrMonth['length']; _0x3594x34++) {
                    for (var _0x3594x35 = 0; _0x3594x35 < ArrHQDet['length']; _0x3594x35++) {
                        var _0x3594x36 = ArrHQDet[_0x3594x35]['Sec_Sale_Code'];
                        var _0x3594x38 = false;
                        for (var _0x3594x15 = 0; _0x3594x15 < ArrSale['length'] && !_0x3594x38; _0x3594x15++) {
                            var _0x3594x29 = ArrSale[_0x3594x15];
                            var _0x3594x10 = [];
                            _0x3594x10 = _0x3594x29['split']('^');
                            if ($['trim'](_0x3594x10[0]) === $['trim'](_0x3594x36)) {
                                _0x3594x38 = true;
                                break
                            }
                        };
                        if (_0x3594x38 == true) {
                            _0x3594x31 += '<th >Qty </th>';
                            _0x3594x31 += '<th >Value</th>'
                        }
                    }
                };
                for (var _0x3594x35 = 0; _0x3594x35 < ArrHQDet['length']; _0x3594x35++) {
                    var _0x3594x36 = ArrHQDet[_0x3594x35]['Sec_Sale_Code'];
                    var _0x3594x38 = false;
                    for (var _0x3594x15 = 0; _0x3594x15 < ArrSale['length'] && !_0x3594x38; _0x3594x15++) {
                        var _0x3594x29 = ArrSale[_0x3594x15];
                        var _0x3594x10 = [];
                        _0x3594x10 = _0x3594x29['split']('^');
                        if ($['trim'](_0x3594x10[0]) === $['trim'](_0x3594x36)) {
                            _0x3594x38 = true;
                            break
                        }
                    };
                    if (_0x3594x38 == true) {
                        _0x3594x31 += '<th >Qty </th>';
                        _0x3594x31 += '<th >Value</th>'
                    }
                };
                _0x3594x31 += '</tr>';
                for (var _0x3594x15 = 2; _0x3594x15 < _0x3594x30['length']; _0x3594x15++) {
                    _0x3594x31 += '<tr>';
                    for (var _0x3594x22 = 0; _0x3594x22 < _0x3594x30[_0x3594x15]['length']; _0x3594x22++) {
                        var _0x3594x39 = '';
                        var _0x3594x3a = _0x3594x30[_0x3594x15]['length'] - parseInt(_0x3594x33);
                        if (_0x3594x3a <= _0x3594x22) {
                            _0x3594x39 = 'color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;'
                        } else {
                            _0x3594x39 = 'color:#636d73;'
                        };
                        _0x3594x31 += '<td style=' + _0x3594x39 + ';>' + _0x3594x30[_0x3594x15][_0x3594x22] + '</td>'
                    };
                    _0x3594x31 += '</tr>'
                };
                _0x3594x31 += '</table>';
                $('#divpnl')['append'](_0x3594x31);
                TotalBg()
            };
            $('.modal')['hide']()
        },
        error: function (_0x3594x3b) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    $('#tblSale tr:last td')['each'](function () {
        var _0x3594x3d = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#F1F5F8;')
    })
}

function getPivotArray_Test(_0x3594x3f, _0x3594x40, _0x3594x41, _0x3594x42) {
    var _0x3594x2d = {},
        _0x3594x43 = [];
    var _0x3594x44 = [];
    var _0x3594x45 = [];
    var _0x3594x46 = [];
    var _0x3594x47 = [];
    var _0x3594x48 = [];
    var _0x3594x49 = [];
    var _0x3594x4a = [];
    var _0x3594x4b = [];
    for (var _0x3594x15 = 0; _0x3594x15 < _0x3594x3f['length']; _0x3594x15++) {
        if (!_0x3594x2d[_0x3594x3f[_0x3594x15][_0x3594x40[0]]]) {
            _0x3594x2d[_0x3594x3f[_0x3594x15][_0x3594x40[0]]] = {};
            _0x3594x45['push'](_0x3594x3f[_0x3594x15][_0x3594x40[1]]);
            _0x3594x46['push'](_0x3594x3f[_0x3594x15][_0x3594x40[0]]);
            _0x3594x47['push'](_0x3594x3f[_0x3594x15][_0x3594x40[2]]);
            if (_0x3594x3f[_0x3594x15][_0x3594x40[2]] == null || _0x3594x3f[_0x3594x15][_0x3594x40[2]] == 0) {
                _0x3594x3f[_0x3594x15][_0x3594x40[2]] = '';
                _0x3594x48['push'](_0x3594x3f[_0x3594x15][_0x3594x40[2]])
            } else {
                _0x3594x48['push'](_0x3594x3f[_0x3594x15][_0x3594x40[2]])
            };
            if (_0x3594x3f[_0x3594x15][_0x3594x40[3]] == null || _0x3594x3f[_0x3594x15][_0x3594x40[3]] == 0) {
                _0x3594x3f[_0x3594x15][_0x3594x40[3]] = '';
                _0x3594x49['push'](_0x3594x3f[_0x3594x15][_0x3594x40[3]])
            } else {
                _0x3594x49['push'](_0x3594x3f[_0x3594x15][_0x3594x40[3]])
            };
            if (_0x3594x3f[_0x3594x15][_0x3594x40[4]] == null || _0x3594x3f[_0x3594x15][_0x3594x40[4]] == 0) {
                _0x3594x3f[_0x3594x15][_0x3594x40[4]] = '';
                _0x3594x4a['push'](_0x3594x3f[_0x3594x15][_0x3594x40[4]])
            } else {
                _0x3594x4a['push'](_0x3594x3f[_0x3594x15][_0x3594x40[4]])
            };
            if (_0x3594x3f[_0x3594x15][_0x3594x40[5]] == null || _0x3594x3f[_0x3594x15][_0x3594x40[5]] == 0) {
                _0x3594x3f[_0x3594x15][_0x3594x40[5]] = '';
                _0x3594x4b['push'](_0x3594x3f[_0x3594x15][_0x3594x40[5]])
            } else {
                _0x3594x4b['push'](_0x3594x3f[_0x3594x15][_0x3594x40[5]])
            }
        };
        _0x3594x2d[_0x3594x3f[_0x3594x15][_0x3594x40[0]]][_0x3594x3f[_0x3594x15][_0x3594x41]] = _0x3594x3f[_0x3594x15][_0x3594x42];
        if (_0x3594x44['indexOf'](_0x3594x3f[_0x3594x15][_0x3594x41]) == -1) {
            if (_0x3594x3f[_0x3594x15][_0x3594x41] != null) {
                _0x3594x44['push'](_0x3594x3f[_0x3594x15][_0x3594x41])
            }
        }
    };
    var _0x3594x4c = [];
    _0x3594x4c['push']('Sl_No');
    _0x3594x4c['push']('Item');
    _0x3594x4c['push']('ERP');
    _0x3594x4c['push']('Pack');
    _0x3594x4c['push']['apply'](_0x3594x4c, _0x3594x44);
    _0x3594x43['push'](_0x3594x4c);
    var _0x3594x4d = 0;
    var _0x3594x4e = 0;
    for (var _0x3594x4f in _0x3594x2d) {
        if (_0x3594x4d < 1) {
            _0x3594x4e = 0
        } else {
            _0x3594x4e += 1
        };
        _0x3594x4c = [];
        _0x3594x4c['push'](_0x3594x4e);
        _0x3594x4c['push'](_0x3594x4f);
        _0x3594x4c['push'](_0x3594x47[_0x3594x4d]);
        _0x3594x4c['push'](_0x3594x45[_0x3594x4d]);
        for (var _0x3594x15 = 0; _0x3594x15 < _0x3594x44['length']; _0x3594x15++) {
            _0x3594x4c['push'](_0x3594x2d[_0x3594x4f][_0x3594x44[_0x3594x15]] || ' ')
        };
        _0x3594x43['push'](_0x3594x4c);
        _0x3594x4d += 1
    };
    return _0x3594x43
}

function GetHQ_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_All_ParamField',
        data: '{}',
        dataType: 'json',
        success: function (_0x3594x16) {
            ArrHQDet = _0x3594x16['d']
        },
        error: function (_0x3594x3b) { }
    })
}