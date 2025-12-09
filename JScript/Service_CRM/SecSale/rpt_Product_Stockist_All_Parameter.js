var ArrHQDet = {};
var dataTableSt;
var ColMonth = '';
var ArrSale = [];
var ArrTransit = [];
var ArrFree = [];
$(document)['ready'](function () {
    $('.modal')['ajaxStart'](function () {
        $(this)['show']()
    })['ajaxStop'](function () {
        $(this)['hide']()
    });
    GetHQ_Det();
    var _0xdc4fx7 = $('#SS_DivCode')['val']();
    var _0xdc4fx8 = GetParameterValues('sfcode');
    var _0xdc4fx9 = GetParameterValues('Sf_Name');
    var _0xdc4fxa = GetParameterValues('FMonth');
    var _0xdc4fxb = GetParameterValues('FYear');
    var _0xdc4fxc = GetParameterValues('TMonth');
    var _0xdc4fxd = GetParameterValues('TYear');
    var _0xdc4fxe = GetParameterValues('Param');

    function _0xdc4fxf() {
        var _0xdc4fx10 = window['location']['search'];
        var _0xdc4fx11 = /([^?&=]*)=([^&]*)/g;
        var _0xdc4fx12 = {};
        var _0xdc4fx13 = null;
        while (_0xdc4fx13 = _0xdc4fx11['exec'](_0xdc4fx10)) {
            _0xdc4fx12[_0xdc4fx13[1]] = decodeURIComponent(_0xdc4fx13[2])
        };
        return _0xdc4fx12
    }
    var _0xdc4fx12 = _0xdc4fxf();
    try {
        var _0xdc4fx14 = JSON['parse'](_0xdc4fx12.Param);
        ArrSale = _0xdc4fx14
    } catch (err) { };
    var _0xdc4fx15 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0xdc4fx16 = parseInt(_0xdc4fxa - 1);
    var _0xdc4fx17 = parseInt(_0xdc4fxc - 1);
    var _0xdc4fx18 = _0xdc4fx15[_0xdc4fx16];
    var _0xdc4fx19 = _0xdc4fx15[_0xdc4fx17];
    if (_0xdc4fxa != _0xdc4fxc) {
        ColMonth = _0xdc4fx18 + '-' + _0xdc4fx19
    } else {
        ColMonth = _0xdc4fx18 + '-' + _0xdc4fx19
    };
    var _0xdc4fx1a = 'Product - Stockist wise - (All) for ' + _0xdc4fx18 + ' ' + _0xdc4fxb + ' to ' + ' ' + _0xdc4fx19 + ' ' + ' ' + _0xdc4fxd;
    var _0xdc4fx1b = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0xdc4fx9 + '</span>';
    $('#lblStock')['html'](_0xdc4fx1a);
    $('#lblField')['html'](_0xdc4fx1b);
    var _0xdc4fx1c = [];
    var _0xdc4fx1d = (parseInt(_0xdc4fxd) - parseInt(_0xdc4fxb)) * 12 + parseInt(_0xdc4fxc) - parseInt(_0xdc4fxa);
    var _0xdc4fx1e = parseInt(_0xdc4fxa);
    var _0xdc4fx1f = parseInt(_0xdc4fxb);
    if (_0xdc4fx1d >= 0) {
        for (var _0xdc4fx20 = 1; _0xdc4fx20 <= _0xdc4fx1d + 1; _0xdc4fx20++) {
            var _0xdc4fx16 = parseInt(_0xdc4fx1e - 1);
            var _0xdc4fx18 = _0xdc4fx15[_0xdc4fx16];
            var _0xdc4fx21 = _0xdc4fx18;
            _0xdc4fx1c['push'](_0xdc4fx21);
            _0xdc4fx1e = _0xdc4fx1e + 1;
            if (_0xdc4fx1e == 13) {
                _0xdc4fx1e = 1;
                _0xdc4fx1f = _0xdc4fx1f + 1
            }
        };
        _0xdc4fx1c['push'](ColMonth)
    }
});

function GetHQ_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_All_Stockist_Det',
        data: '{}',
        dataType: 'json',
        success: function (_0xdc4fx14) {
            ArrHQDet = _0xdc4fx14['d'];
            GetRecordData()
        },
        error: function (_0xdc4fx23) { }
    })
}

function GetParameterValues(_0xdc4fx25) {
    var _0xdc4fx26 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0xdc4fx13 = 0; _0xdc4fx13 < _0xdc4fx26['length']; _0xdc4fx13++) {
        var _0xdc4fx27 = _0xdc4fx26[_0xdc4fx13]['split']('=');
        if (_0xdc4fx27[0] == _0xdc4fx25) {
            return decodeURIComponent(_0xdc4fx27[1])
        }
    }
}

function GetProduct_Detail() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/GetProduct_Detail',
        data: '{}',
        dataType: 'json',
        success: function (_0xdc4fx14) {
            dataTableSt = _0xdc4fx14['d']['dtProd']
        },
        error: function (_0xdc4fx23) { }
    })
}

function GetRecordData() {
    var _0xdc4fx2a = '';
    var _0xdc4fx2b = '';
    var _0xdc4fx2c = '';
    for (var _0xdc4fx13 = 0; _0xdc4fx13 < ArrSale['length']; _0xdc4fx13++) {
        _0xdc4fx2a += ArrSale[_0xdc4fx13] + '#'
    };
    _0xdc4fx2a = _0xdc4fx2a['slice'](0, -1);
    _0xdc4fx2b = _0xdc4fx2b['slice'](0, -1);
    _0xdc4fx2c = _0xdc4fx2c['slice'](0, -1);
    var _0xdc4fx2d = _0xdc4fx2a;
    var _0xdc4fx2e = {};
    var _0xdc4fx2f = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/GetStockist_Product_All_Parameter',
        data: '{objData:' + JSON['stringify'](_0xdc4fx2d) + '}',
        dataType: 'json',
        success: function (_0xdc4fx14) {
            dataTableSt = _0xdc4fx14['d']['dtStock'];
            var _0xdc4fx30 = ['Prod_Name', 'Pack', 'T_SaleQty', 'T_SaleVal', 'T_ClsQty', 'T_ClsVal', 'Product_ERP_Code'];
            var _0xdc4fx31 = getPivotArray_Test(dataTableSt, _0xdc4fx30, 'VST', 'Value');
            var _0xdc4fx2e = '<table id=tblHQSale class=\'table\' style=\'min-width:95%;max-width:100%\'>';
            _0xdc4fx2e += '<tr>';
            _0xdc4fx2e += '<th rowspan="3" >#</th>';
            _0xdc4fx2e += '<th rowspan="3" >Product Name</th>';
            _0xdc4fx2e += '<th rowspan="3" >Product ERP Code</th>';
            _0xdc4fx2e += '<th rowspan="3" >Pack</th>';
            var _0xdc4fx32 = 2;
            var _0xdc4fx33 = parseInt(2) * 2;
            for (var _0xdc4fx13 = 0; _0xdc4fx13 < ArrHQDet['length']; _0xdc4fx13++) {
                _0xdc4fx2e += '<th  colspan=' + _0xdc4fx33 + '>' + ArrHQDet[_0xdc4fx13]['HQ_Name'] + '</th>'
            };
            _0xdc4fx2e += '<th colspan="4" >Total</th>';
            _0xdc4fx2e += '</tr>';
            _0xdc4fx2e += '<tr>';
            for (var _0xdc4fx13 = 0; _0xdc4fx13 < ArrHQDet['length']; _0xdc4fx13++) {
                _0xdc4fx2e += '<th  colspan=' + _0xdc4fx32 + '>Secondary Sales</th>';
                _0xdc4fx2e += '<th  colspan=' + _0xdc4fx32 + '>Closing Including Transit</th>'
            };
            _0xdc4fx2e += '<th  colspan="2">Secondary Sales</th>';
            _0xdc4fx2e += '<th  colspan="2">Closing Including Transit</th>';
            _0xdc4fx2e += '</tr>';
            _0xdc4fx2e += '<tr>';
            for (var _0xdc4fx13 = 0; _0xdc4fx13 < ArrHQDet['length']; _0xdc4fx13++) {
                for (_0xdc4fx20 = 0; _0xdc4fx20 < 2; _0xdc4fx20++) {
                    _0xdc4fx2e += '<th >Qty</th>';
                    _0xdc4fx2e += '<th >Value</th>'
                }
            };
            for (k = 0; k < 2; k++) {
                _0xdc4fx2e += '<th >Qty</th>';
                _0xdc4fx2e += '<th >Value</th>'
            };
            _0xdc4fx2e += '</tr>';
            for (var _0xdc4fx13 = 2; _0xdc4fx13 < _0xdc4fx31['length']; _0xdc4fx13++) {
                _0xdc4fx2e += '<tr>';
                for (var _0xdc4fx20 = 0; _0xdc4fx20 < _0xdc4fx31[_0xdc4fx13]['length']; _0xdc4fx20++) {
                    var _0xdc4fx34 = '';
                    var _0xdc4fx35 = _0xdc4fx31[_0xdc4fx13]['length'] - 4;
                    if (_0xdc4fx35 <= _0xdc4fx20) {
                        _0xdc4fx34 = 'color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;'
                    } else {
                        _0xdc4fx34 = 'color:#636d73;'
                    };
                    _0xdc4fx2e += '<td style=' + _0xdc4fx34 + ';>' + _0xdc4fx31[_0xdc4fx13][_0xdc4fx20] + '</td>'
                };
                _0xdc4fx2e += '</tr>'
            };
            _0xdc4fx2e += '</table>';
            $('#divpnl')['append'](_0xdc4fx2e);
            TotalBg();
            $('.modal')['hide']()
        },
        error: function (_0xdc4fx23) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    $('#tblHQSale tr:last td')['each'](function () {
        var _0xdc4fx37 = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#F1F5F8;')
    })
}

function getPivotArray(_0xdc4fx39, _0xdc4fx3a, _0xdc4fx3b, _0xdc4fx3c) {
    var _0xdc4fx2e = {},
        _0xdc4fx3d = [];
    var _0xdc4fx3e = [];
    for (var _0xdc4fx13 = 0; _0xdc4fx13 < _0xdc4fx39['length']; _0xdc4fx13++) {
        if (!_0xdc4fx2e[_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a]]) {
            _0xdc4fx2e[_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a]] = {}
        };
        _0xdc4fx2e[_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a]][_0xdc4fx39[_0xdc4fx13][_0xdc4fx3b]] = _0xdc4fx39[_0xdc4fx13][_0xdc4fx3c];
        if (_0xdc4fx3e['indexOf'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3b]) == -1) {
            _0xdc4fx3e['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3b])
        }
    };
    var _0xdc4fx3f = [];
    _0xdc4fx3f['push']('Item');
    _0xdc4fx3f['push']['apply'](_0xdc4fx3f, _0xdc4fx3e);
    _0xdc4fx3d['push'](_0xdc4fx3f);
    for (var _0xdc4fx40 in _0xdc4fx2e) {
        _0xdc4fx3f = [];
        _0xdc4fx3f['push'](_0xdc4fx40);
        for (var _0xdc4fx13 = 0; _0xdc4fx13 < _0xdc4fx3e['length']; _0xdc4fx13++) {
            _0xdc4fx3f['push'](_0xdc4fx2e[_0xdc4fx40][_0xdc4fx3e[_0xdc4fx13]] || '-')
        };
        _0xdc4fx3d['push'](_0xdc4fx3f)
    };
    return _0xdc4fx3d
}

function arrayToHTMLTable(_0xdc4fx42) {
    var _0xdc4fx2e = '<table class=\'table table-bordered table-striped\' style=\'min-width:95%;max-width:100%\'>';
    for (var _0xdc4fx13 = 2; _0xdc4fx13 < _0xdc4fx42['length']; _0xdc4fx13++) {
        _0xdc4fx2e += '<tr>';
        for (var _0xdc4fx20 = 0; _0xdc4fx20 < _0xdc4fx42[_0xdc4fx13]['length']; _0xdc4fx20++) {
            _0xdc4fx2e += '<td>' + _0xdc4fx42[_0xdc4fx13][_0xdc4fx20] + '</td>'
        };
        _0xdc4fx2e += '</tr>'
    };
    _0xdc4fx2e += '</table>';
    return _0xdc4fx2e
}

function getPivotArray_Test(_0xdc4fx39, _0xdc4fx3a, _0xdc4fx3b, _0xdc4fx3c) {
    var _0xdc4fx2e = {},
        _0xdc4fx3d = [];
    var _0xdc4fx3e = [];
    var _0xdc4fx44 = [];
    var _0xdc4fx45 = [];
    var _0xdc4fx46 = [];
    var _0xdc4fx47 = [];
    var _0xdc4fx48 = [];
    var _0xdc4fx49 = [];
    var _0xdc4fx4a = [];
    for (var _0xdc4fx13 = 0; _0xdc4fx13 < _0xdc4fx39['length']; _0xdc4fx13++) {
        if (!_0xdc4fx2e[_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[0]]]) {
            _0xdc4fx2e[_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[0]]] = {};
            _0xdc4fx44['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[1]]);
            _0xdc4fx45['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[0]]);
            _0xdc4fx46['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[6]]);
            if (_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[2]] == null || _0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[2]] == 0) {
                _0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[2]] = '';
                _0xdc4fx47['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[2]])
            } else {
                _0xdc4fx47['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[2]])
            };
            if (_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[3]] == null || _0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[3]] == 0) {
                _0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[3]] = '';
                _0xdc4fx48['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[3]])
            } else {
                _0xdc4fx48['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[3]])
            };
            if (_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[4]] == null || _0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[4]] == 0) {
                _0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[4]] = '';
                _0xdc4fx49['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[4]])
            } else {
                _0xdc4fx49['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[4]])
            };
            if (_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[5]] == null || _0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[5]] == 0) {
                _0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[5]] = '';
                _0xdc4fx4a['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[5]])
            } else {
                _0xdc4fx4a['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[5]])
            }
        };
        _0xdc4fx2e[_0xdc4fx39[_0xdc4fx13][_0xdc4fx3a[0]]][_0xdc4fx39[_0xdc4fx13][_0xdc4fx3b]] = _0xdc4fx39[_0xdc4fx13][_0xdc4fx3c];
        if (_0xdc4fx3e['indexOf'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3b]) == -1) {
            if (_0xdc4fx39[_0xdc4fx13][_0xdc4fx3b] != null) {
                _0xdc4fx3e['push'](_0xdc4fx39[_0xdc4fx13][_0xdc4fx3b])
            }
        }
    };
    var _0xdc4fx3f = [];
    _0xdc4fx3f['push']('Sl_No');
    _0xdc4fx3f['push']('Item');
    _0xdc4fx3f['push']('ERP');
    _0xdc4fx3f['push']('Pack');
    _0xdc4fx3f['push']['apply'](_0xdc4fx3f, _0xdc4fx3e);
    _0xdc4fx3d['push'](_0xdc4fx3f);
    var _0xdc4fx4b = 0;
    var _0xdc4fx4c = 0;
    for (var _0xdc4fx40 in _0xdc4fx2e) {
        if (_0xdc4fx4b < 1) {
            _0xdc4fx4c = 0
        } else {
            _0xdc4fx4c += 1
        };
        _0xdc4fx3f = [];
        _0xdc4fx3f['push'](_0xdc4fx4c);
        _0xdc4fx3f['push'](_0xdc4fx40);
        _0xdc4fx3f['push'](_0xdc4fx46[_0xdc4fx4b]);
        _0xdc4fx3f['push'](_0xdc4fx44[_0xdc4fx4b]);
        for (var _0xdc4fx13 = 0; _0xdc4fx13 < _0xdc4fx3e['length']; _0xdc4fx13++) {
            _0xdc4fx3f['push'](_0xdc4fx2e[_0xdc4fx40][_0xdc4fx3e[_0xdc4fx13]] || ' ')
        };
        _0xdc4fx3f['push'](_0xdc4fx47[_0xdc4fx4b]);
        _0xdc4fx3f['push'](_0xdc4fx48[_0xdc4fx4b]);
        _0xdc4fx3f['push'](_0xdc4fx49[_0xdc4fx4b]);
        _0xdc4fx3f['push'](_0xdc4fx4a[_0xdc4fx4b]);
        _0xdc4fx3d['push'](_0xdc4fx3f);
        _0xdc4fx4b += 1
    };
    return _0xdc4fx3d
}