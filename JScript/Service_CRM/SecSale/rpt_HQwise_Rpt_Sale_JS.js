//window['onload'] = function () {
//    if (window['location']['href'] == sessionStorage['getItem']('userObject')) {
//        sessionStorage['clear']()
//    }
//};
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
var ArrDataStock = [];
var Sf_Code = '';
var Sf_Name = '';
var FMonth = '';
var FYear = '';
var TMonth = '';
var TYear = '';
var sURL = '';
$(document)['ready'](function () {
    $('.modal')['ajaxStart'](function () {
        $(this)['show']()
    })['ajaxStop'](function () {
        $(this)['hide']()
    });
    GetHQ_Det();
    var _0x3c8ax14 = $('#SS_DivCode')['val']();
    Sf_Code = GetParameterValues('sfcode');
    Sf_Name = GetParameterValues('Sf_Name');
    FMonth = GetParameterValues('FMonth');
    FYear = GetParameterValues('FYear');
    TMonth = GetParameterValues('TMonth');
    TYear = GetParameterValues('TYear');
    DDl_Opt = '1';

    function _0x3c8ax15() {
        var _0x3c8ax16 = window['location']['search'];
        var _0x3c8ax17 = /([^?&=]*)=([^&]*)/g;
        var _0x3c8ax18 = {};
        var _0x3c8ax19 = null;
        while (_0x3c8ax19 = _0x3c8ax17['exec'](_0x3c8ax16)) {
            _0x3c8ax18[_0x3c8ax19[1]] = decodeURIComponent(_0x3c8ax19[2])
        };
        return _0x3c8ax18
    }
    var _0x3c8ax18 = _0x3c8ax15();
    try {
        var _0x3c8ax1a = JSON['parse'](_0x3c8ax18.ValField);
        if ($['trim'](_0x3c8ax1a) == 'chkQty') {
            ModeType = 'Q'
        } else {
            if ($['trim'](_0x3c8ax1a) == 'chkValue') {
                ModeType = 'V'
            }
        }
    } catch (err) { };
    var _0x3c8ax1b = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0x3c8ax1c = parseInt(FMonth - 1);
    var _0x3c8ax1d = parseInt(TMonth - 1);
    var _0x3c8ax1e = _0x3c8ax1b[_0x3c8ax1c];
    var _0x3c8ax1f = _0x3c8ax1b[_0x3c8ax1d];
    if (FMonth != TMonth) {
        ColMonth = _0x3c8ax1e + '-' + _0x3c8ax1f
    } else {
        ColMonth = _0x3c8ax1e + '-' + _0x3c8ax1f
    };
    var _0x3c8ax20 = 'Product - HQ wise (Sale) From ' + _0x3c8ax1e + ' ' + FYear + ' to ' + ' ' + _0x3c8ax1f + ' ' + ' ' + TYear;
    var _0x3c8ax21 = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + Sf_Name + '</span>';
    $('#lblStock')['html'](_0x3c8ax20);
    $('#lblField')['html'](_0x3c8ax21);
    var _0x3c8ax22 = (parseInt(TYear) - parseInt(FYear)) * 12 + parseInt(TMonth) - parseInt(FMonth);
    var _0x3c8ax23 = parseInt(FMonth);
    var _0x3c8ax24 = parseInt(FYear);
    if (_0x3c8ax22 >= 0) {
        for (var _0x3c8ax25 = 1; _0x3c8ax25 <= _0x3c8ax22 + 1; _0x3c8ax25++) {
            var _0x3c8ax1c = parseInt(_0x3c8ax23 - 1);
            var _0x3c8ax1e = _0x3c8ax1b[_0x3c8ax1c];
            var _0x3c8ax26 = _0x3c8ax1e;
            ArrMonth['push'](_0x3c8ax26);
            _0x3c8ax23 = _0x3c8ax23 + 1;
            if (_0x3c8ax23 == 13) {
                _0x3c8ax23 = 1;
                _0x3c8ax24 = _0x3c8ax24 + 1
            }
        };
        ArrMonth['push'](ColMonth)
    }
});

function GetParameterValues(_0x3c8ax28) {
    var _0x3c8ax29 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0x3c8ax19 = 0; _0x3c8ax19 < _0x3c8ax29['length']; _0x3c8ax19++) {
        var _0x3c8ax2a = _0x3c8ax29[_0x3c8ax19]['split']('=');
        if (_0x3c8ax2a[0] == _0x3c8ax28) {
            return decodeURIComponent(_0x3c8ax2a[1])
        }
    }
}

function GetRecordData() {
    var _0x3c8ax2c = '';
    var _0x3c8ax2d = '';
    var _0x3c8ax2e = '';
    for (var _0x3c8ax19 = 0; _0x3c8ax19 < ArrSale['length']; _0x3c8ax19++) {
        _0x3c8ax2c += ArrSale[_0x3c8ax19] + '#'
    };
    _0x3c8ax2c = _0x3c8ax2c['slice'](0, -1);
    _0x3c8ax2d = _0x3c8ax2d['slice'](0, -1);
    _0x3c8ax2e = _0x3c8ax2e['slice'](0, -1);
    var _0x3c8ax2f = _0x3c8ax2c;
    var _0x3c8ax30 = {};
    var _0x3c8ax31 = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_SS_HQwise_Sale_Det',
        data: '{ModeType:' + JSON['stringify'](ModeType) + '}',
        dataType: 'json',
        success: function (_0x3c8ax1a) {
            dataTableSt = _0x3c8ax1a['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                var _0x3c8ax32 = ['Prod_Name', 'Pack', 'T_SaleQty', 'T_SaleVal', 'Product_ERP_Code'];
                var _0x3c8ax33 = getPivotArray_Test(dataTableSt, _0x3c8ax32, 'VST', 'Value');
                if (_0x3c8ax33['length'] - 2 > 0) {
                    var _0x3c8ax34 = '<table id="tblHQSale" class="table" style="min-width:95%;max-width:100%">';
                    _0x3c8ax34 += '<tr>';
                    _0x3c8ax34 += '<th rowspan="2" >#</th>';
                    _0x3c8ax34 += '<th rowspan="2" >Product Name</th>';
                    _0x3c8ax34 += '<th rowspan="2">Product ERP Code</th>';
                    _0x3c8ax34 += '<th rowspan="2">Pack</th>';
                    var _0x3c8ax35 = ArrMonth['length'];
                    var _0x3c8ax36 = 4 * parseInt(ArrHQDet['length']);
                    for (var _0x3c8ax19 = 0; _0x3c8ax19 < ArrHQDet['length']; _0x3c8ax19++) {
                        _0x3c8ax34 += '<th  colspan="' + _0x3c8ax35 + '">' + ArrHQDet[_0x3c8ax19]['HQ_Name'] + '</th>'
                    };
                    _0x3c8ax34 += '<th colspan="2" >Total</th>';
                    _0x3c8ax34 += '</tr>';
                    _0x3c8ax34 += '<tr>';
                    for (var _0x3c8ax19 = 0; _0x3c8ax19 < ArrHQDet['length']; _0x3c8ax19++) {
                        for (var _0x3c8ax37 = 0; _0x3c8ax37 < ArrMonth['length']; _0x3c8ax37++) {
                            _0x3c8ax34 += '<th>' + ArrMonth[_0x3c8ax37] + '</th>'
                        }
                    };
                    _0x3c8ax34 += '<th >Sale Qty</th>';
                    _0x3c8ax34 += '<th >Sale Value</th>';
                    _0x3c8ax34 += '</tr>';
                    for (var _0x3c8ax19 = 2; _0x3c8ax19 < _0x3c8ax33['length']; _0x3c8ax19++) {
                        _0x3c8ax34 += '<tr>';
                        for (var _0x3c8ax25 = 0; _0x3c8ax25 < _0x3c8ax33[_0x3c8ax19]['length']; _0x3c8ax25++) {
                            var _0x3c8ax38 = '';
                            var _0x3c8ax39 = _0x3c8ax33[_0x3c8ax19]['length'] - 2;
                            if (_0x3c8ax39 <= _0x3c8ax25) {
                                _0x3c8ax38 = 'color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;'
                            } else {
                                _0x3c8ax38 = 'color:#6c757d;'
                            };
                            _0x3c8ax34 += '<td style=' + _0x3c8ax38 + ';>' + _0x3c8ax33[_0x3c8ax19][_0x3c8ax25] + '</td>';
                            if (_0x3c8ax25 == _0x3c8ax33[_0x3c8ax19]['length'] - 1) {
                                Active_Val = _0x3c8ax33[_0x3c8ax19][_0x3c8ax25]
                            }
                        };
                        _0x3c8ax34 += '</tr>'
                    };
                    _0x3c8ax34 += '</table>';
                    $('#divpnl')['append'](_0x3c8ax34);
                    TotalBg();
                    TotalDeActivate();
                    $('#btn_AllProduct')['show']()
                }
            };
            $('.modal')['hide']()
        },
        error: function (_0x3c8ax3a) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    $('#tblHQSale tr:last td')['each'](function () {
        var _0x3c8ax3c = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#f1f5f8;')
    })
}

function TotalDeActivate() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0x3c8ax1a) {
            dataTableSt = _0x3c8ax1a['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0x3c8ax25 = dataTableSt['length'] - 1;
                var _0x3c8ax3e = dataTableSt[_0x3c8ax25];
                var _0x3c8ax34 = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0x3c8ax34 += '<tr>';
                _0x3c8ax34 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x3c8ax34 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0x3c8ax34 += '</tr>';
                _0x3c8ax34 += '<tr>';
                _0x3c8ax34 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x3c8ax34 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x3c8ax34 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x3c8ax34 += '</tr>';
                _0x3c8ax34 += '<tr>';
                _0x3c8ax34 += '<td style=background-color:#d4d4d4;>';
                _0x3c8ax34 += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0x3c8ax34 += '</td>';
                var _0x3c8ax3f = 0;
                $['each'](_0x3c8ax3e, function (_0x3c8ax40, _0x3c8ax41) {
                    var _0x3c8ax42 = _0x3c8ax40;
                    if (_0x3c8ax41 == null || _0x3c8ax41 == '0') {
                        _0x3c8ax41 = ''
                    };
                    if (_0x3c8ax42['includes']('_ABT') || _0x3c8ax42['includes']('_ACT')) {
                        var _0x3c8ax43 = parseFloat(_0x3c8ax41)['toFixed'](2);
                        DeAct_Val = _0x3c8ax43;
                        _0x3c8ax3f = parseFloat(Active_Val) + parseFloat(DeAct_Val)
                    }
                });
                var _0x3c8ax44 = Sf_Name['replace'](/ /g, '_');
                var _0x3c8ax45 = DDl_Opt;
                _0x3c8ax34 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val) + '</td>';
                _0x3c8ax34 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>';
                _0x3c8ax34 += '</tr>';
                _0x3c8ax34 += '<tr>';
                _0x3c8ax34 += '<td style=background-color:#d4d4d4;>';
                _0x3c8ax34 += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0x3c8ax34 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x3c8ax3f + '</span></td>';
                _0x3c8ax34 += '</td>';
                _0x3c8ax34 += '</tr>';
                _0x3c8ax34 += '</table>';
                $('#div_DeAct')['append'](_0x3c8ax34)
            }
        },
        error: function (_0x3c8ax3a) {
            $('.modal')['hide']()
        }
    })
}

function getPivotArray_Test(_0x3c8ax47, _0x3c8ax48, _0x3c8ax49, _0x3c8ax4a) {
    var _0x3c8ax30 = {},
        _0x3c8ax4b = [];
    var _0x3c8ax4c = [];
    var _0x3c8ax4d = [];
    var _0x3c8ax4e = [];
    var _0x3c8ax4f = [];
    var _0x3c8ax50 = [];
    var _0x3c8ax51 = [];
    var _0x3c8ax52 = [];
    var _0x3c8ax53 = [];
    for (var _0x3c8ax19 = 0; _0x3c8ax19 < _0x3c8ax47['length']; _0x3c8ax19++) {
        if (!_0x3c8ax30[_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[0]]]) {
            _0x3c8ax30[_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[0]]] = {};
            _0x3c8ax4d['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[1]]);
            _0x3c8ax4e['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[0]]);
            _0x3c8ax4f['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[4]]);
            if (_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[2]] == null || _0x3c8ax47[_0x3c8ax19][_0x3c8ax48[2]] == 0) {
                _0x3c8ax47[_0x3c8ax19][_0x3c8ax48[2]] = '';
                _0x3c8ax50['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[2]])
            } else {
                _0x3c8ax50['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[2]])
            };
            if (_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[3]] == null || _0x3c8ax47[_0x3c8ax19][_0x3c8ax48[3]] == 0) {
                _0x3c8ax47[_0x3c8ax19][_0x3c8ax48[3]] = '';
                _0x3c8ax51['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[3]])
            } else {
                _0x3c8ax51['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[3]])
            };
            if (_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[4]] == null || _0x3c8ax47[_0x3c8ax19][_0x3c8ax48[4]] == 0) {
                _0x3c8ax47[_0x3c8ax19][_0x3c8ax48[4]] = '';
                _0x3c8ax52['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[4]])
            } else {
                _0x3c8ax52['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[4]])
            };
            if (_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[5]] == null || _0x3c8ax47[_0x3c8ax19][_0x3c8ax48[5]] == 0) {
                _0x3c8ax47[_0x3c8ax19][_0x3c8ax48[5]] = '';
                _0x3c8ax53['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[5]])
            } else {
                _0x3c8ax53['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[5]])
            }
        };
        _0x3c8ax30[_0x3c8ax47[_0x3c8ax19][_0x3c8ax48[0]]][_0x3c8ax47[_0x3c8ax19][_0x3c8ax49]] = _0x3c8ax47[_0x3c8ax19][_0x3c8ax4a];
        if (_0x3c8ax4c['indexOf'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax49]) == -1) {
            if (_0x3c8ax47[_0x3c8ax19][_0x3c8ax49] != null) {
                _0x3c8ax4c['push'](_0x3c8ax47[_0x3c8ax19][_0x3c8ax49])
            }
        }
    };
    var _0x3c8ax54 = [];
    _0x3c8ax54['push']('Sl_No');
    _0x3c8ax54['push']('Item');
    _0x3c8ax54['push']('ERP');
    _0x3c8ax54['push']('Pack');
    _0x3c8ax54['push']['apply'](_0x3c8ax54, _0x3c8ax4c);
    _0x3c8ax54['push']('SaleQty');
    _0x3c8ax54['push']('SaleVal');
    _0x3c8ax4b['push'](_0x3c8ax54);
    var _0x3c8ax55 = 0;
    var _0x3c8ax56 = 0;
    for (var _0x3c8ax40 in _0x3c8ax30) {
        if (_0x3c8ax55 < 1) {
            _0x3c8ax56 = 0
        } else {
            _0x3c8ax56 += 1
        };
        _0x3c8ax54 = [];
        _0x3c8ax54['push'](_0x3c8ax56);
        _0x3c8ax54['push'](_0x3c8ax40);
        _0x3c8ax54['push'](_0x3c8ax4f[_0x3c8ax55]);
        _0x3c8ax54['push'](_0x3c8ax4d[_0x3c8ax55]);
        for (var _0x3c8ax19 = 0; _0x3c8ax19 < _0x3c8ax4c['length']; _0x3c8ax19++) {
            _0x3c8ax54['push'](_0x3c8ax30[_0x3c8ax40][_0x3c8ax4c[_0x3c8ax19]] || ' ')
        };
        _0x3c8ax54['push'](_0x3c8ax50[_0x3c8ax55]);
        _0x3c8ax54['push'](_0x3c8ax51[_0x3c8ax55]);
        _0x3c8ax4b['push'](_0x3c8ax54);
        _0x3c8ax55 += 1
    };
    return _0x3c8ax4b
}

function GetHQ_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_All_HQ_Det',
        data: '{}',
        dataType: 'json',
        success: function (_0x3c8ax1a) {
            ArrHQDet = _0x3c8ax1a['d'];
            GetRecordData()
        },
        error: function (_0x3c8ax3a) { }
    })
}

function storeUserDataInSession(_0x3c8ax59) {
    sessionStorage['removeItem']('userObject');
    var _0x3c8ax5a = JSON['stringify'](_0x3c8ax59);
    window['sessionStorage']['setItem']('userObject', _0x3c8ax5a)
}