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
var Active_Val = [];
var DeAct_Val = 0;
var Sf_Code = '';
var Sf_Name = '';
var FMonth = '';
var FYear = '';
var TMonth = '';
var TYear = '';
$(document)['ready'](function () {
    $('.modal')['ajaxStart'](function () {
        $(this)['show']()
    })['ajaxStop'](function () {
        $(this)['hide']()
    });
    GetHQ_Det();
    var _0x3ca2x12 = $('#SS_DivCode')['val']();
    Sf_Code = GetParameterValues('sfcode');
    Sf_Name = GetParameterValues('Sf_Name');
    FMonth = GetParameterValues('FMonth');
    FYear = GetParameterValues('FYear');
    TMonth = GetParameterValues('TMonth');
    TYear = GetParameterValues('TYear');
    DDl_Opt = '2';

    function _0x3ca2x13() {
        var _0x3ca2x14 = window['location']['search'];
        var _0x3ca2x15 = /([^?&=]*)=([^&]*)/g;
        var _0x3ca2x16 = {};
        var _0x3ca2x17 = null;
        while (_0x3ca2x17 = _0x3ca2x15['exec'](_0x3ca2x14)) {
            _0x3ca2x16[_0x3ca2x17[1]] = decodeURIComponent(_0x3ca2x17[2])
        };
        return _0x3ca2x16
    }
    var _0x3ca2x16 = _0x3ca2x13();
    try {
        var _0x3ca2x18 = JSON['parse'](_0x3ca2x16.ValField);
        if ($['trim'](_0x3ca2x18) == 'chkQty') {
            ModeType = 'Q'
        } else {
            if ($['trim'](_0x3ca2x18) == 'chkValue') {
                ModeType = 'V'
            }
        }
    } catch (err) { };
    var _0x3ca2x19 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0x3ca2x1a = parseInt(FMonth - 1);
    var _0x3ca2x1b = parseInt(TMonth - 1);
    var _0x3ca2x1c = _0x3ca2x19[_0x3ca2x1a];
    var _0x3ca2x1d = _0x3ca2x19[_0x3ca2x1b];
    if (FMonth != TMonth) {
        ColMonth = _0x3ca2x1c + '-' + _0x3ca2x1d
    } else {
        ColMonth = _0x3ca2x1c + '-' + _0x3ca2x1d
    };
    var _0x3ca2x1e = 'Product - HQ Wise (Sale & CB) From ' + _0x3ca2x1c + ' ' + FYear + ' to ' + ' ' + _0x3ca2x1d + ' ' + ' ' + TYear;
    var _0x3ca2x1f = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + Sf_Name + '</span>';
    $('#lblStock')['html'](_0x3ca2x1e);
    $('#lblField')['html'](_0x3ca2x1f);
    var _0x3ca2x20 = (parseInt(TYear) - parseInt(FYear)) * 12 + parseInt(TMonth) - parseInt(FMonth);
    var _0x3ca2x21 = parseInt(FMonth);
    var _0x3ca2x22 = parseInt(FYear);
    if (_0x3ca2x20 >= 0) {
        for (var _0x3ca2x23 = 1; _0x3ca2x23 <= _0x3ca2x20 + 1; _0x3ca2x23++) {
            var _0x3ca2x1a = parseInt(_0x3ca2x21 - 1);
            var _0x3ca2x1c = _0x3ca2x19[_0x3ca2x1a];
            var _0x3ca2x24 = _0x3ca2x1c;
            ArrMonth['push'](_0x3ca2x24);
            _0x3ca2x21 = _0x3ca2x21 + 1;
            if (_0x3ca2x21 == 13) {
                _0x3ca2x21 = 1;
                _0x3ca2x22 = _0x3ca2x22 + 1
            }
        };
        ArrMonth['push'](ColMonth)
    }
});

function GetParameterValues(_0x3ca2x26) {
    var _0x3ca2x27 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0x3ca2x17 = 0; _0x3ca2x17 < _0x3ca2x27['length']; _0x3ca2x17++) {
        var _0x3ca2x28 = _0x3ca2x27[_0x3ca2x17]['split']('=');
        if (_0x3ca2x28[0] == _0x3ca2x26) {
            return decodeURIComponent(_0x3ca2x28[1])
        }
    }
}

function GetRecordData() {
    var _0x3ca2x2a = '';
    var _0x3ca2x2b = '';
    var _0x3ca2x2c = '';
    for (var _0x3ca2x17 = 0; _0x3ca2x17 < ArrSale['length']; _0x3ca2x17++) {
        _0x3ca2x2a += ArrSale[_0x3ca2x17] + '#'
    };
    _0x3ca2x2a = _0x3ca2x2a['slice'](0, -1);
    _0x3ca2x2b = _0x3ca2x2b['slice'](0, -1);
    _0x3ca2x2c = _0x3ca2x2c['slice'](0, -1);
    var _0x3ca2x2d = _0x3ca2x2a;
    var _0x3ca2x2e = {};
    var _0x3ca2x2f = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_SSPrd_SaleandClosing',
        data: '{ModeType:' + JSON['stringify'](ModeType) + '}',
        dataType: 'json',
        success: function (_0x3ca2x18) {
            dataTableSt = _0x3ca2x18['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                var _0x3ca2x30 = ['Prod_Name', 'Pack', 'T_SaleQty', 'T_SaleVal', 'T_ClsQty', 'T_ClsVal', 'Product_ERP_Code'];
                var _0x3ca2x31 = getPivotArray_Test(dataTableSt, _0x3ca2x30, 'VST', 'Value');
                var _0x3ca2x32 = '<table id="tblHQSaleCB" class="table " style="min-width:95%;max-width:100%">';
                _0x3ca2x32 += '<tr>';
                _0x3ca2x32 += '<th rowspan="3" >#</th>';
                _0x3ca2x32 += '<th rowspan="3" >Product Name</th>';
                _0x3ca2x32 += '<th rowspan="3" >Product ERP Code</th>';
                _0x3ca2x32 += '<th rowspan="3" >Pack</th>';
                var _0x3ca2x33 = ArrMonth['length'];
                var _0x3ca2x34 = parseInt(_0x3ca2x33) * 2;
                for (var _0x3ca2x17 = 0; _0x3ca2x17 < ArrHQDet['length']; _0x3ca2x17++) {
                    _0x3ca2x32 += '<th  colspan=' + _0x3ca2x34 + '>' + ArrHQDet[_0x3ca2x17]['HQ_Name'] + '</th>'
                };
                _0x3ca2x32 += '<th colspan="4" >Total</th>';
                _0x3ca2x32 += '</tr>';
                _0x3ca2x32 += '<tr>';
                for (var _0x3ca2x17 = 0; _0x3ca2x17 < ArrHQDet['length']; _0x3ca2x17++) {
                    _0x3ca2x32 += '<th colspan=' + _0x3ca2x33 + '>Sale</th>';
                    _0x3ca2x32 += '<th colspan=' + _0x3ca2x33 + '>Closing</th>'
                };
                _0x3ca2x32 += '<th  colspan="2">Sale</th>';
                _0x3ca2x32 += '<th  colspan="2">Closing</th>';
                _0x3ca2x32 += '</tr>';
                _0x3ca2x32 += '<tr>';
                for (var _0x3ca2x17 = 0; _0x3ca2x17 < ArrHQDet['length']; _0x3ca2x17++) {
                    for (_0x3ca2x23 = 0; _0x3ca2x23 < 2; _0x3ca2x23++) {
                        for (var _0x3ca2x35 = 0; _0x3ca2x35 < ArrMonth['length']; _0x3ca2x35++) {
                            _0x3ca2x32 += '<th >' + ArrMonth[_0x3ca2x35] + '</th>'
                        }
                    }
                };
                for (k = 0; k < 2; k++) {
                    _0x3ca2x32 += '<th >Qty</th>';
                    _0x3ca2x32 += '<th >Value</th>'
                };
                _0x3ca2x32 += '</tr>';
                for (var _0x3ca2x17 = 2; _0x3ca2x17 < _0x3ca2x31['length']; _0x3ca2x17++) {
                    _0x3ca2x32 += '<tr>';
                    for (var _0x3ca2x23 = 0; _0x3ca2x23 < _0x3ca2x31[_0x3ca2x17]['length']; _0x3ca2x23++) {
                        var _0x3ca2x36 = '';
                        var _0x3ca2x37 = _0x3ca2x31[_0x3ca2x17]['length'] - 4;
                        if (_0x3ca2x37 <= _0x3ca2x23) {
                            _0x3ca2x36 = 'color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;'
                        } else {
                            _0x3ca2x36 = 'color:#636d73;'
                        };
                        _0x3ca2x32 += '<td style=' + _0x3ca2x36 + ';>' + _0x3ca2x31[_0x3ca2x17][_0x3ca2x23] + '</td>'
                    };
                    _0x3ca2x32 += '</tr>'
                };
                _0x3ca2x32 += '</table>';
                $('#divpnl')['append'](_0x3ca2x32);
                TotalBg();
                TotalDeActivate();
                $('#btn_AllProduct')['show']()
            };
            $('.modal')['hide']()
        },
        error: function (_0x3ca2x38) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    $('#tblHQSaleCB tr:last td')['each'](function () {
        var _0x3ca2x3a = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#F1F5F8;')
    })
}

function getPivotArray_Test(_0x3ca2x3c, _0x3ca2x3d, _0x3ca2x3e, _0x3ca2x3f) {
    var _0x3ca2x2e = {},
        _0x3ca2x40 = [];
    var _0x3ca2x41 = [];
    var _0x3ca2x42 = [];
    var _0x3ca2x43 = [];
    var _0x3ca2x44 = [];
    var _0x3ca2x45 = [];
    var _0x3ca2x46 = [];
    var _0x3ca2x47 = [];
    var _0x3ca2x48 = [];
    for (var _0x3ca2x17 = 0; _0x3ca2x17 < _0x3ca2x3c['length']; _0x3ca2x17++) {
        if (!_0x3ca2x2e[_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[0]]]) {
            _0x3ca2x2e[_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[0]]] = {};
            _0x3ca2x42['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[1]]);
            _0x3ca2x43['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[0]]);
            _0x3ca2x44['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[6]]);
            if (_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[2]] == null || _0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[2]] == 0) {
                _0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[2]] = '';
                _0x3ca2x45['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[2]])
            } else {
                _0x3ca2x45['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[2]])
            };
            if (_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[3]] == null || _0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[3]] == 0) {
                _0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[3]] = '';
                _0x3ca2x46['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[3]])
            } else {
                _0x3ca2x46['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[3]])
            };
            if (_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[4]] == null || _0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[4]] == 0) {
                _0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[4]] = '';
                _0x3ca2x47['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[4]])
            } else {
                _0x3ca2x47['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[4]])
            };
            if (_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[5]] == null || _0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[5]] == 0) {
                _0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[5]] = '';
                _0x3ca2x48['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[5]])
            } else {
                _0x3ca2x48['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[5]])
            }
        };
        _0x3ca2x2e[_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3d[0]]][_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3e]] = _0x3ca2x3c[_0x3ca2x17][_0x3ca2x3f];
        if (_0x3ca2x41['indexOf'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3e]) == -1) {
            if (_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3e] != null) {
                _0x3ca2x41['push'](_0x3ca2x3c[_0x3ca2x17][_0x3ca2x3e])
            }
        }
    };
    var _0x3ca2x49 = [];
    _0x3ca2x49['push']('Sl_No');
    _0x3ca2x49['push']('Item');
    _0x3ca2x49['push']('ERP');
    _0x3ca2x49['push']('Pack');
    _0x3ca2x49['push']['apply'](_0x3ca2x49, _0x3ca2x41);
    _0x3ca2x40['push'](_0x3ca2x49);
    var _0x3ca2x4a = 0;
    var _0x3ca2x4b = 0;
    for (var _0x3ca2x4c in _0x3ca2x2e) {
        if (_0x3ca2x4a < 1) {
            _0x3ca2x4b = 0
        } else {
            _0x3ca2x4b += 1
        };
        _0x3ca2x49 = [];
        _0x3ca2x49['push'](_0x3ca2x4b);
        _0x3ca2x49['push'](_0x3ca2x4c);
        _0x3ca2x49['push'](_0x3ca2x44[_0x3ca2x4a]);
        _0x3ca2x49['push'](_0x3ca2x42[_0x3ca2x4a]);
        for (var _0x3ca2x17 = 0; _0x3ca2x17 < _0x3ca2x41['length']; _0x3ca2x17++) {
            _0x3ca2x49['push'](_0x3ca2x2e[_0x3ca2x4c][_0x3ca2x41[_0x3ca2x17]] || ' ')
        };
        _0x3ca2x49['push'](_0x3ca2x45[_0x3ca2x4a]);
        _0x3ca2x49['push'](_0x3ca2x46[_0x3ca2x4a]);
        _0x3ca2x49['push'](_0x3ca2x47[_0x3ca2x4a]);
        _0x3ca2x49['push'](_0x3ca2x48[_0x3ca2x4a]);
        _0x3ca2x40['push'](_0x3ca2x49);
        _0x3ca2x4a += 1
    };
    var _0x3ca2x4d = _0x3ca2x46['length'] - 1;
    var _0x3ca2x4e = _0x3ca2x48['length'] - 1;
    Active_Val['push'](_0x3ca2x46[_0x3ca2x4d]);
    Active_Val['push'](_0x3ca2x48[_0x3ca2x4e]);
    return _0x3ca2x40
}

function GetHQ_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_All_HQ_Det',
        data: '{}',
        dataType: 'json',
        success: function (_0x3ca2x18) {
            ArrHQDet = _0x3ca2x18['d'];
            GetRecordData()
        },
        error: function (_0x3ca2x38) { }
    })
}

function GetAll_Sale_CB() {
    var _0x3ca2x2a = '';
    var _0x3ca2x2b = '';
    var _0x3ca2x2c = '';
    for (var _0x3ca2x17 = 0; _0x3ca2x17 < ArrSale['length']; _0x3ca2x17++) {
        _0x3ca2x2a += ArrSale[_0x3ca2x17] + '#'
    };
    _0x3ca2x2a = _0x3ca2x2a['slice'](0, -1);
    _0x3ca2x2b = _0x3ca2x2b['slice'](0, -1);
    _0x3ca2x2c = _0x3ca2x2c['slice'](0, -1);
    var _0x3ca2x2d = _0x3ca2x2a;
    var _0x3ca2x2e = {};
    var _0x3ca2x2f = '';
    $('#divAll')['html']('');
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0x3ca2x18) {
            dataTableSt = _0x3ca2x18['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                var _0x3ca2x32 = '<table id="tblSaleCB" class="table table-bordered table-striped" style="min-width:50%;max-width:50%">';
                _0x3ca2x32 += '<tr>';
                _0x3ca2x32 += '<th colspan="2" style="background-color:#0097AC">Sale</th>';
                _0x3ca2x32 += '<th colspan="2" style="background-color:#0097AC">Closing</th>';
                _0x3ca2x32 += '</tr>';
                _0x3ca2x32 += '<tr>';
                _0x3ca2x32 += '<th style="background-color:#0097AC">Qty</th>';
                _0x3ca2x32 += '<th style="background-color:#0097AC">Value</th>';
                _0x3ca2x32 += '<th style="background-color:#0097AC">Qty</th>';
                _0x3ca2x32 += '<th style="background-color:#0097AC">Value</th>';
                _0x3ca2x32 += '</tr>';
                for (var _0x3ca2x23 = 0; _0x3ca2x23 < dataTableSt['length']; _0x3ca2x23++) {
                    var _0x3ca2x51 = dataTableSt[_0x3ca2x23];
                    var _0x3ca2x52 = 0;
                    _0x3ca2x32 += '<tr>';
                    $['each'](_0x3ca2x51, function (_0x3ca2x4c, _0x3ca2x53) {
                        var _0x3ca2x54 = _0x3ca2x4c;
                        if (_0x3ca2x53 == null || _0x3ca2x53 == '0') {
                            _0x3ca2x53 = ''
                        };
                        if (_0x3ca2x54['includes']('_ABT') || _0x3ca2x54['includes']('_ACT')) {
                            _0x3ca2x32 += '<td style=color:#000;font-size:12px;font-weight:bold;font-family:Cambria;background-color:#cecece;>' + parseFloat(_0x3ca2x53)['toFixed'](2) + '</td>'
                        }
                    })
                };
                _0x3ca2x32 += '</table>';
                $('#divAll')['append'](_0x3ca2x32)
            };
            $('.modal')['hide']()
        },
        error: function (_0x3ca2x38) {
            $('.modal')['hide']()
        }
    })
}

function TotalDeActivate() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0x3ca2x18) {
            dataTableSt = _0x3ca2x18['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0x3ca2x23 = dataTableSt['length'] - 1;
                var _0x3ca2x51 = dataTableSt[_0x3ca2x23];
                var _0x3ca2x32 = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0x3ca2x32 += '<tr>';
                _0x3ca2x32 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x3ca2x32 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0x3ca2x32 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Closing</th>';
                _0x3ca2x32 += '</tr>';
                _0x3ca2x32 += '<tr>';
                _0x3ca2x32 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x3ca2x32 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x3ca2x32 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x3ca2x32 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x3ca2x32 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x3ca2x32 += '</tr>';
                _0x3ca2x32 += '<tr>';
                _0x3ca2x32 += '<td style=background-color:#d4d4d4;>';
                _0x3ca2x32 += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0x3ca2x32 += '</td>';
                var _0x3ca2x56 = 0;
                var _0x3ca2x4a = 0;
                var _0x3ca2x57 = 0;
                $['each'](_0x3ca2x51, function (_0x3ca2x4c, _0x3ca2x53) {
                    var _0x3ca2x54 = _0x3ca2x4c;
                    if (_0x3ca2x53 == null || _0x3ca2x53 == '0') {
                        _0x3ca2x53 = ''
                    };
                    if (_0x3ca2x54['includes']('_ABT') || _0x3ca2x54['includes']('_ACT')) {
                        var _0x3ca2x58 = parseFloat(_0x3ca2x53)['toFixed'](2);
                        DeAct_Val = _0x3ca2x58;
                        if (_0x3ca2x4a == 0) {
                            DeAct_Val = _0x3ca2x58;
                            _0x3ca2x56 = parseFloat(Active_Val[0]) + parseFloat(DeAct_Val);
                            _0x3ca2x32 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[0]) + '</td>';
                            _0x3ca2x32 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        } else {
                            DeAct_Val = _0x3ca2x58;
                            _0x3ca2x57 = parseFloat(Active_Val[1]) + parseFloat(DeAct_Val);
                            _0x3ca2x32 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[1]) + '</td>';
                            _0x3ca2x32 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        };
                        _0x3ca2x4a += 1
                    }
                });
                _0x3ca2x32 += '</tr>';
                _0x3ca2x32 += '<tr>';
                _0x3ca2x32 += '<td style=background-color:#d4d4d4;>';
                _0x3ca2x32 += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0x3ca2x32 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x3ca2x56 + '</span></td>';
                _0x3ca2x32 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x3ca2x57 + '</span></td>';
                _0x3ca2x32 += '</td>';
                _0x3ca2x32 += '</tr>';
                _0x3ca2x32 += '</table>';
                $('#div_DeAct')['append'](_0x3ca2x32)
            }
        },
        error: function (_0x3ca2x38) {
            $('.modal')['hide']()
        }
    })
}

function storeUserDataInSession(_0x3ca2x5a) {
    sessionStorage['clear']();
    var _0x3ca2x5b = JSON['stringify'](_0x3ca2x5a);
    window['sessionStorage']['setItem']('userObject', _0x3ca2x5b)
}