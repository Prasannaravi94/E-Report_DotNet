window['onload'] = function () {
    if (window['location']['href'] == sessionStorage['getItem']('userObject')) {
        sessionStorage['clear']()
    }
};
var ArrHQDet = {};
var dataTableSt;
var ColMonth = '';
var ArrSale = [];
var ArrTransit = [];
var ArrFree = [];
var ArrMonth = [];
var ModeType = '';
var DDl_Opt = '';
var Active_Val = 0;
var DeAct_Val = 0;
$(document)['ready'](function () {
    $('.modal')['ajaxStart'](function () {
        $(this)['show']()
    })['ajaxStop'](function () {
        $(this)['hide']()
    });
    GetState_Det();
    var _0x1783xc = $('#SS_DivCode')['val']();
    var _0x1783xd = GetParameterValues('sfcode');
    var _0x1783xe = GetParameterValues('Sf_Name');
    var _0x1783xf = GetParameterValues('FMonth');
    var _0x1783x10 = GetParameterValues('FYear');
    var _0x1783x11 = GetParameterValues('TMonth');
    var _0x1783x12 = GetParameterValues('TYear');
    DDl_Opt = '1';

    function _0x1783x13() {
        var _0x1783x14 = window['location']['search'];
        var _0x1783x15 = /([^?&=]*)=([^&]*)/g;
        var _0x1783x16 = {};
        var _0x1783x17 = null;
        while (_0x1783x17 = _0x1783x15['exec'](_0x1783x14)) {
            _0x1783x16[_0x1783x17[1]] = decodeURIComponent(_0x1783x17[2])
        };
        return _0x1783x16
    }
    var _0x1783x16 = _0x1783x13();
    try {
        var _0x1783x18 = JSON['parse'](_0x1783x16.ValField);
        if ($['trim'](_0x1783x18) == 'chkQty') {
            ModeType = 'Q'
        } else {
            if ($['trim'](_0x1783x18) == 'chkValue') {
                ModeType = 'V'
            }
        }
    } catch (err) { };
    var _0x1783x19 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0x1783x1a = parseInt(_0x1783xf - 1);
    var _0x1783x1b = parseInt(_0x1783x11 - 1);
    var _0x1783x1c = _0x1783x19[_0x1783x1a];
    var _0x1783x1d = _0x1783x19[_0x1783x1b];
    if (_0x1783xf != _0x1783x11) {
        ColMonth = _0x1783x1c + '-' + _0x1783x1d
    } else {
        ColMonth = _0x1783x1c + '-' + _0x1783x1d
    };
    var _0x1783x1e = 'Product - State wise (Sale) From ' + _0x1783x1c + ' ' + _0x1783x10 + ' to ' + ' ' + _0x1783x1d + ' ' + ' ' + _0x1783x12;
    var _0x1783x1f = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0x1783xe + '</span>';
    $('#lblStock')['html'](_0x1783x1e);
    $('#lblField')['html'](_0x1783x1f);
    var _0x1783x20 = (parseInt(_0x1783x12) - parseInt(_0x1783x10)) * 12 + parseInt(_0x1783x11) - parseInt(_0x1783xf);
    var _0x1783x21 = parseInt(_0x1783xf);
    var _0x1783x22 = parseInt(_0x1783x10);
    if (_0x1783x20 >= 0) {
        for (var _0x1783x23 = 1; _0x1783x23 <= _0x1783x20 + 1; _0x1783x23++) {
            var _0x1783x1a = parseInt(_0x1783x21 - 1);
            var _0x1783x1c = _0x1783x19[_0x1783x1a];
            var _0x1783x24 = _0x1783x1c;
            ArrMonth['push'](_0x1783x24);
            _0x1783x21 = _0x1783x21 + 1;
            if (_0x1783x21 == 13) {
                _0x1783x21 = 1;
                _0x1783x22 = _0x1783x22 + 1
            }
        };
        ArrMonth['push'](ColMonth)
    }
});

function GetParameterValues(_0x1783x26) {
    var _0x1783x27 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0x1783x17 = 0; _0x1783x17 < _0x1783x27['length']; _0x1783x17++) {
        var _0x1783x28 = _0x1783x27[_0x1783x17]['split']('=');
        if (_0x1783x28[0] == _0x1783x26) {
            return decodeURIComponent(_0x1783x28[1])
        }
    }
}

function GetRecordData() {
    var _0x1783x2a = '';
    var _0x1783x2b = '';
    var _0x1783x2c = '';
    for (var _0x1783x17 = 0; _0x1783x17 < ArrSale['length']; _0x1783x17++) {
        _0x1783x2a += ArrSale[_0x1783x17] + '#'
    };
    _0x1783x2a = _0x1783x2a['slice'](0, -1);
    _0x1783x2b = _0x1783x2b['slice'](0, -1);
    _0x1783x2c = _0x1783x2c['slice'](0, -1);
    var _0x1783x2d = _0x1783x2a;
    var _0x1783x2e = {};
    var _0x1783x2f = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_SS_Statewise_Sale_Det',
        data: '{ModeType:' + JSON['stringify'](ModeType) + '}',
        dataType: 'json',
        success: function (_0x1783x18) {
            dataTableSt = _0x1783x18['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                var _0x1783x30 = ['Prod_Name', 'Pack', 'T_SaleQty', 'T_SaleVal', 'Product_ERP_Code'];
                var _0x1783x31 = getPivotArray_Test(dataTableSt, _0x1783x30, 'VST', 'Value');
                if (_0x1783x31['length'] - 2 > 0) {
                    var _0x1783x32 = '<table id="tblHQSale" class="table" style="min-width:95%;max-width:100%">';
                    _0x1783x32 += '<tr>';
                    _0x1783x32 += '<th rowspan="2" >#</th>';
                    _0x1783x32 += '<th rowspan="2" >Product Name</th>';
                    _0x1783x32 += '<th rowspan="2" >Product ERP Code</th>';
                    _0x1783x32 += '<th rowspan="2" >Pack</th>';
                    var _0x1783x33 = ArrMonth['length'];
                    for (var _0x1783x17 = 0; _0x1783x17 < ArrHQDet['length']; _0x1783x17++) {
                        _0x1783x32 += '<th colspan="' + _0x1783x33 + '">' + ArrHQDet[_0x1783x17]['State_Name'] + '</th>'
                    };
                    _0x1783x32 += '<th colspan="2" >Total</th>';
                    _0x1783x32 += '</tr>';
                    _0x1783x32 += '<tr>';
                    for (var _0x1783x17 = 0; _0x1783x17 < ArrHQDet['length']; _0x1783x17++) {
                        for (var _0x1783x34 = 0; _0x1783x34 < ArrMonth['length']; _0x1783x34++) {
                            _0x1783x32 += '<th >' + ArrMonth[_0x1783x34] + '</th>'
                        }
                    };
                    _0x1783x32 += '<th >Sale Qty</th>';
                    _0x1783x32 += '<th >Sale Value</th>';
                    _0x1783x32 += '</tr>';
                    for (var _0x1783x17 = 2; _0x1783x17 < _0x1783x31['length']; _0x1783x17++) {
                        _0x1783x32 += '<tr>';
                        for (var _0x1783x23 = 0; _0x1783x23 < _0x1783x31[_0x1783x17]['length']; _0x1783x23++) {
                            var _0x1783x35 = '';
                            var _0x1783x36 = _0x1783x31[_0x1783x17]['length'] - 2;
                            if (_0x1783x36 <= _0x1783x23) {
                                _0x1783x35 = 'color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;'
                            } else {
                                _0x1783x35 = 'color:#636d73;'
                            };
                            _0x1783x32 += '<td style=' + _0x1783x35 + ';>' + _0x1783x31[_0x1783x17][_0x1783x23] + '</td>';
                            if (_0x1783x23 == _0x1783x31[_0x1783x17]['length'] - 1) {
                                Active_Val = _0x1783x31[_0x1783x17][_0x1783x23]
                            }
                        };
                        _0x1783x32 += '</tr>'
                    };
                    _0x1783x32 += '</table>';
                    $('#divpnl')['append'](_0x1783x32);
                    TotalBg();
                    TotalDeActivate()
                }
            };
            $('.modal')['hide']()
        },
        error: function (_0x1783x37) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    $('#tblHQSale tr:last td')['each'](function () {
        var _0x1783x39 = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#F1F5F8;')
    })
}

function getPivotArray_Test(_0x1783x3b, _0x1783x3c, _0x1783x3d, _0x1783x3e) {
    var _0x1783x2e = {},
        _0x1783x3f = [];
    var _0x1783x40 = [];
    var _0x1783x41 = [];
    var _0x1783x42 = [];
    var _0x1783x43 = [];
    var _0x1783x44 = [];
    var _0x1783x45 = [];
    var _0x1783x46 = [];
    var _0x1783x47 = [];
    for (var _0x1783x17 = 0; _0x1783x17 < _0x1783x3b['length']; _0x1783x17++) {
        if (!_0x1783x2e[_0x1783x3b[_0x1783x17][_0x1783x3c[0]]]) {
            _0x1783x2e[_0x1783x3b[_0x1783x17][_0x1783x3c[0]]] = {};
            _0x1783x41['push'](_0x1783x3b[_0x1783x17][_0x1783x3c[1]]);
            _0x1783x42['push'](_0x1783x3b[_0x1783x17][_0x1783x3c[0]]);
            _0x1783x43['push'](_0x1783x3b[_0x1783x17][_0x1783x3c[4]]);
            if (_0x1783x3b[_0x1783x17][_0x1783x3c[2]] == null || _0x1783x3b[_0x1783x17][_0x1783x3c[2]] == 0) {
                _0x1783x3b[_0x1783x17][_0x1783x3c[2]] = '';
                _0x1783x44['push'](_0x1783x3b[_0x1783x17][_0x1783x3c[2]])
            } else {
                _0x1783x44['push'](_0x1783x3b[_0x1783x17][_0x1783x3c[2]])
            };
            if (_0x1783x3b[_0x1783x17][_0x1783x3c[3]] == null || _0x1783x3b[_0x1783x17][_0x1783x3c[3]] == 0) {
                _0x1783x3b[_0x1783x17][_0x1783x3c[3]] = '';
                _0x1783x45['push'](_0x1783x3b[_0x1783x17][_0x1783x3c[3]])
            } else {
                _0x1783x45['push'](_0x1783x3b[_0x1783x17][_0x1783x3c[3]])
            };
            if (_0x1783x3b[_0x1783x17][_0x1783x3c[4]] == null || _0x1783x3b[_0x1783x17][_0x1783x3c[4]] == 0) {
                _0x1783x3b[_0x1783x17][_0x1783x3c[4]] = '';
                _0x1783x46['push'](_0x1783x3b[_0x1783x17][_0x1783x3c[4]])
            } else {
                _0x1783x46['push'](_0x1783x3b[_0x1783x17][_0x1783x3c[4]])
            };
            if (_0x1783x3b[_0x1783x17][_0x1783x3c[5]] == null || _0x1783x3b[_0x1783x17][_0x1783x3c[5]] == 0) {
                _0x1783x3b[_0x1783x17][_0x1783x3c[5]] = '';
                _0x1783x47['push'](_0x1783x3b[_0x1783x17][_0x1783x3c[5]])
            } else {
                _0x1783x47['push'](_0x1783x3b[_0x1783x17][_0x1783x3c[5]])
            }
        };
        _0x1783x2e[_0x1783x3b[_0x1783x17][_0x1783x3c[0]]][_0x1783x3b[_0x1783x17][_0x1783x3d]] = _0x1783x3b[_0x1783x17][_0x1783x3e];
        if (_0x1783x40['indexOf'](_0x1783x3b[_0x1783x17][_0x1783x3d]) == -1) {
            if (_0x1783x3b[_0x1783x17][_0x1783x3d] != null) {
                _0x1783x40['push'](_0x1783x3b[_0x1783x17][_0x1783x3d])
            }
        }
    };
    var _0x1783x48 = [];
    _0x1783x48['push']('Sl_No');
    _0x1783x48['push']('Item');
    _0x1783x48['push']('ERP');
    _0x1783x48['push']('Pack');
    _0x1783x48['push']['apply'](_0x1783x48, _0x1783x40);
    _0x1783x48['push']('SaleQty');
    _0x1783x48['push']('SaleVal');
    _0x1783x3f['push'](_0x1783x48);
    var _0x1783x49 = 0;
    var _0x1783x4a = 0;
    for (var _0x1783x4b in _0x1783x2e) {
        if (_0x1783x49 < 1) {
            _0x1783x4a = 0
        } else {
            _0x1783x4a += 1
        };
        _0x1783x48 = [];
        _0x1783x48['push'](_0x1783x4a);
        _0x1783x48['push'](_0x1783x4b);
        _0x1783x48['push'](_0x1783x43[_0x1783x49]);
        _0x1783x48['push'](_0x1783x41[_0x1783x49]);
        for (var _0x1783x17 = 0; _0x1783x17 < _0x1783x40['length']; _0x1783x17++) {
            _0x1783x48['push'](_0x1783x2e[_0x1783x4b][_0x1783x40[_0x1783x17]] || ' ')
        };
        _0x1783x48['push'](_0x1783x44[_0x1783x49]);
        _0x1783x48['push'](_0x1783x45[_0x1783x49]);
        _0x1783x3f['push'](_0x1783x48);
        _0x1783x49 += 1
    };
    return _0x1783x3f
}

function GetState_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_State_Detail',
        data: '{}',
        dataType: 'json',
        success: function (_0x1783x18) {
            ArrHQDet = _0x1783x18['d'];
            GetRecordData()
        },
        error: function (_0x1783x37) { }
    })
}

function TotalDeActivate() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0x1783x18) {
            dataTableSt = _0x1783x18['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0x1783x23 = dataTableSt['length'] - 1;
                var _0x1783x4e = dataTableSt[_0x1783x23];
                var _0x1783x32 = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0x1783x32 += '<tr>';
                _0x1783x32 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x1783x32 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0x1783x32 += '</tr>';
                _0x1783x32 += '<tr>';
                _0x1783x32 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x1783x32 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x1783x32 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x1783x32 += '</tr>';
                _0x1783x32 += '<tr>';
                _0x1783x32 += '<td style=background-color:#d4d4d4;>';
                _0x1783x32 += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0x1783x32 += '</td>';
                var _0x1783x4f = 0;
                $['each'](_0x1783x4e, function (_0x1783x4b, _0x1783x50) {
                    var _0x1783x51 = _0x1783x4b;
                    if (_0x1783x50 == null || _0x1783x50 == '0') {
                        _0x1783x50 = ''
                    };
                    if (_0x1783x51['includes']('_ABT') || _0x1783x51['includes']('_ACT')) {
                        var _0x1783x52 = parseFloat(_0x1783x50)['toFixed'](2);
                        DeAct_Val = _0x1783x52;
                        _0x1783x4f = parseFloat(Active_Val) + parseFloat(DeAct_Val)
                    }
                });
                _0x1783x32 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val) + '</td>';
                _0x1783x32 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>';
                _0x1783x32 += '</tr>';
                _0x1783x32 += '<tr>';
                _0x1783x32 += '<td style=background-color:#d4d4d4;>';
                _0x1783x32 += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0x1783x32 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x1783x4f + '</span></td>';
                _0x1783x32 += '</td>';
                _0x1783x32 += '</tr>';
                _0x1783x32 += '</table>';
                $('#div_DeAct')['append'](_0x1783x32)
            }
        },
        error: function (_0x1783x37) {
            $('.modal')['hide']()
        }
    })
}

function storeUserDataInSession(_0x1783x54) {
    sessionStorage['removeItem']('userObject');
    var _0x1783x55 = JSON['stringify'](_0x1783x54);
    window['sessionStorage']['setItem']('userObject', _0x1783x55)
}