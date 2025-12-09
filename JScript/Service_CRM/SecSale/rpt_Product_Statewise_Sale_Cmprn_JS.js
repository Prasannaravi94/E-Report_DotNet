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
var Arr_Tot = [];
var ModeType = '';
var Type = '';
var lblMsg = '';
var Mnth = '';
var DDl_Opt = '';
var Active_Val = 0;
var DeAct_Val = 0;
$(document)['ready'](function () {
    $('.modal')['ajaxStart'](function () {
        $(this)['show']()
    })['ajaxStop'](function () {
        $(this)['hide']()
    });
    var _0xad76x10 = $('#SS_DivCode')['val']();
    var _0xad76x11 = GetParameterValues('sfcode');
    var _0xad76x12 = GetParameterValues('Sf_Name');
    var _0xad76x13 = GetParameterValues('FMonth');
    var _0xad76x14 = GetParameterValues('FYear');
    var _0xad76x15 = GetParameterValues('TMonth');
    var _0xad76x16 = GetParameterValues('TYear');
    Type = GetParameterValues('Type');
    DDl_Opt = '1';

    function _0xad76x17() {
        var _0xad76x18 = window['location']['search'];
        var _0xad76x19 = /([^?&=]*)=([^&]*)/g;
        var _0xad76x1a = {};
        var _0xad76x1b = null;
        while (_0xad76x1b = _0xad76x19['exec'](_0xad76x18)) {
            _0xad76x1a[_0xad76x1b[1]] = decodeURIComponent(_0xad76x1b[2])
        };
        return _0xad76x1a
    }
    var _0xad76x1a = _0xad76x17();
    try {
        var _0xad76x1c = JSON['parse'](_0xad76x1a.DataMn_Yr);
        Mnth = _0xad76x1c
    } catch (err) { };
    if (Type == '1') {
        GetState_Det();
        lblMsg = 'State wise'
    } else {
        if (Type == '2') {
            GetHQ_Det();
            lblMsg = 'HQ wise'
        }
    };
    var _0xad76x1d = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0xad76x1e = parseInt(_0xad76x13 - 1);
    var _0xad76x1f = parseInt(_0xad76x15 - 1);
    var _0xad76x20 = _0xad76x1d[_0xad76x1e];
    var _0xad76x21 = _0xad76x1d[_0xad76x1f];
    if (_0xad76x13 != _0xad76x15) {
        ColMonth = _0xad76x20 + '-' + _0xad76x21
    } else {
        ColMonth = _0xad76x20 + '-' + _0xad76x21
    };
    var _0xad76x22 = 'Product - ' + lblMsg + ' (Sales) Comparison for ' + _0xad76x20 + ' ' + _0xad76x14 + ' to ' + ' ' + _0xad76x21 + ' ' + ' ' + _0xad76x16;
    var _0xad76x23 = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0xad76x12 + '</span>';
    $('#lblStock')['html'](_0xad76x22);
    $('#lblField')['html'](_0xad76x23);
    var _0xad76x24 = [];
    _0xad76x24 = Mnth['split']('^');
    var _0xad76x25 = [];
    _0xad76x25 = _0xad76x24[1]['split'](',');
    var _0xad76x26 = [];
    _0xad76x26 = _0xad76x24[0]['split'](',');
    var _0xad76x27 = '';
    var _0xad76x28 = '';
    var _0xad76x29 = '';
    var _0xad76x2a = '';
    var _0xad76x2b = '';
    var _0xad76x2c = '';
    for (var _0xad76x1b = 0; _0xad76x1b < _0xad76x25['length']; _0xad76x1b++) {
        var _0xad76x2d = _0xad76x25[_0xad76x1b];
        for (var _0xad76x2e = 0; _0xad76x2e < _0xad76x26['length']; _0xad76x2e++) {
            var _0xad76x2f = _0xad76x26[_0xad76x2e];
            var _0xad76x20 = _0xad76x1d[_0xad76x2f - 1];
            if (_0xad76x2e == 0) {
                _0xad76x29 = _0xad76x20
            };
            ArrMonth['push'](_0xad76x20);
            if (_0xad76x2e == _0xad76x26['length'] - 1) {
                _0xad76x2a = _0xad76x20
            }
        };
        var _0xad76x30 = _0xad76x29 + '-' + _0xad76x2a + ' ' + _0xad76x2d;
        ArrMonth['push'](_0xad76x30);
        Arr_Tot['push'](_0xad76x30);
        if (_0xad76x1b == _0xad76x25['length'] - 1) {
            ArrMonth['push']('Surplus/Deficit (+/-)');
            Arr_Tot['push']('Surplus/Deficit (+/-)')
        }
    }
});

function GetState_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_State_Detail',
        data: '{}',
        dataType: 'json',
        success: function (_0xad76x1c) {
            ArrHQDet = _0xad76x1c['d'];
            GetRecordData()
        },
        error: function (_0xad76x32) { }
    })
}

function GetHQ_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_All_HQ_Det',
        data: '{}',
        dataType: 'json',
        success: function (_0xad76x1c) {
            ArrHQDet = _0xad76x1c['d'];
            GetRecordData()
        },
        error: function (_0xad76x32) { }
    })
}

function GetParameterValues(_0xad76x35) {
    var _0xad76x36 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0xad76x1b = 0; _0xad76x1b < _0xad76x36['length']; _0xad76x1b++) {
        var _0xad76x37 = _0xad76x36[_0xad76x1b]['split']('=');
        if (_0xad76x37[0] == _0xad76x35) {
            return decodeURIComponent(_0xad76x37[1])
        }
    }
}

function getPivotArray_Test(_0xad76x39, _0xad76x3a, _0xad76x3b, _0xad76x3c) {
    var _0xad76x3d = {},
        _0xad76x3e = [];
    var _0xad76x3f = [];
    var _0xad76x40 = [];
    var _0xad76x41 = [];
    var _0xad76x42 = [];
    var _0xad76x43 = [];
    var _0xad76x44 = [];
    var _0xad76x45 = [];
    var _0xad76x46 = [];
    var _0xad76x47 = [];
    var _0xad76x48 = [];
    for (var _0xad76x1b = 0; _0xad76x1b < _0xad76x39['length']; _0xad76x1b++) {
        if (!_0xad76x3d[_0xad76x39[_0xad76x1b][_0xad76x3a[0]]]) {
            _0xad76x3d[_0xad76x39[_0xad76x1b][_0xad76x3a[0]]] = {};
            _0xad76x40['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[1]]);
            _0xad76x41['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[0]]);
            _0xad76x42['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[8]]);
            if (_0xad76x39[_0xad76x1b][_0xad76x3a[2]] == null || _0xad76x39[_0xad76x1b][_0xad76x3a[2]] == 0) {
                _0xad76x39[_0xad76x1b][_0xad76x3a[2]] = '';
                _0xad76x43['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[2]])
            } else {
                _0xad76x43['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[2]])
            };
            if (_0xad76x39[_0xad76x1b][_0xad76x3a[3]] == null || _0xad76x39[_0xad76x1b][_0xad76x3a[3]] == 0) {
                _0xad76x39[_0xad76x1b][_0xad76x3a[3]] = '';
                _0xad76x44['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[3]])
            } else {
                _0xad76x44['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[3]])
            };
            if (_0xad76x39[_0xad76x1b][_0xad76x3a[4]] == null || _0xad76x39[_0xad76x1b][_0xad76x3a[4]] == 0) {
                _0xad76x39[_0xad76x1b][_0xad76x3a[4]] = '';
                _0xad76x45['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[4]])
            } else {
                _0xad76x45['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[4]])
            };
            if (_0xad76x39[_0xad76x1b][_0xad76x3a[5]] == null || _0xad76x39[_0xad76x1b][_0xad76x3a[5]] == 0) {
                _0xad76x39[_0xad76x1b][_0xad76x3a[5]] = '';
                _0xad76x46['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[5]])
            } else {
                _0xad76x46['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[5]])
            };
            if (_0xad76x39[_0xad76x1b][_0xad76x3a[6]] == null || _0xad76x39[_0xad76x1b][_0xad76x3a[6]] == 0) {
                _0xad76x39[_0xad76x1b][_0xad76x3a[6]] = '';
                _0xad76x47['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[6]])
            } else {
                _0xad76x47['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[6]])
            };
            if (_0xad76x39[_0xad76x1b][_0xad76x3a[7]] == null || _0xad76x39[_0xad76x1b][_0xad76x3a[7]] == 0) {
                _0xad76x39[_0xad76x1b][_0xad76x3a[7]] = '';
                _0xad76x48['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[7]])
            } else {
                _0xad76x48['push'](_0xad76x39[_0xad76x1b][_0xad76x3a[7]])
            }
        };
        _0xad76x3d[_0xad76x39[_0xad76x1b][_0xad76x3a[0]]][_0xad76x39[_0xad76x1b][_0xad76x3b]] = _0xad76x39[_0xad76x1b][_0xad76x3c];
        if (_0xad76x3f['indexOf'](_0xad76x39[_0xad76x1b][_0xad76x3b]) == -1) {
            if (_0xad76x39[_0xad76x1b][_0xad76x3b] != null) {
                _0xad76x3f['push'](_0xad76x39[_0xad76x1b][_0xad76x3b])
            }
        }
    };
    var _0xad76x49 = [];
    _0xad76x49['push']('Sl_No');
    _0xad76x49['push']('Item');
    _0xad76x49['push']('ERP');
    _0xad76x49['push']('Pack');
    _0xad76x49['push']['apply'](_0xad76x49, _0xad76x3f);
    _0xad76x49['push']('Y1_Qty');
    _0xad76x49['push']('Y1_Val');
    _0xad76x49['push']('Y2_Qty');
    _0xad76x49['push']('Y2_Val');
    _0xad76x49['push']('Sub_Qty');
    _0xad76x49['push']('Sub_Val');
    _0xad76x3e['push'](_0xad76x49);
    var _0xad76x4a = 0;
    var _0xad76x4b = 0;
    for (var _0xad76x4c in _0xad76x3d) {
        if (_0xad76x4a < 1) {
            _0xad76x4b = 0
        } else {
            _0xad76x4b += 1
        };
        _0xad76x49 = [];
        _0xad76x49['push'](_0xad76x4b);
        _0xad76x49['push'](_0xad76x4c);
        _0xad76x49['push'](_0xad76x42[_0xad76x4a]);
        _0xad76x49['push'](_0xad76x40[_0xad76x4a]);
        for (var _0xad76x1b = 0; _0xad76x1b < _0xad76x3f['length']; _0xad76x1b++) {
            _0xad76x49['push'](_0xad76x3d[_0xad76x4c][_0xad76x3f[_0xad76x1b]] || ' ')
        };
        _0xad76x49['push'](_0xad76x43[_0xad76x4a]);
        _0xad76x49['push'](_0xad76x44[_0xad76x4a]);
        _0xad76x49['push'](_0xad76x45[_0xad76x4a]);
        _0xad76x49['push'](_0xad76x46[_0xad76x4a]);
        _0xad76x49['push'](_0xad76x47[_0xad76x4a]);
        _0xad76x49['push'](_0xad76x48[_0xad76x4a]);
        _0xad76x3e['push'](_0xad76x49);
        _0xad76x4a += 1
    };
    return _0xad76x3e
}

function GetRecordData() {
    var _0xad76x3d = {};
    var _0xad76x4e = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Sales_Comparison',
        data: '{objType:' + JSON['stringify'](Type) + ',objMonth:' + JSON['stringify'](Mnth) + '}',
        dataType: 'json',
        success: function (_0xad76x1c) {
            dataTableSt = _0xad76x1c['d']['dtStock'];
            var _0xad76x4f = ['Prod_Name', 'Pack', 'Y1_Qty', 'Y1_Value', 'Y2_Qty', 'Y2_Val', 'Sub_Qty', 'Sub_Val', 'Product_ERP_Code'];
            var _0xad76x50 = getPivotArray_Test(dataTableSt, _0xad76x4f, 'VST', 'Value');
            if (dataTableSt['length'] > 0) {
                var _0xad76x51 = '<table id=tblHQSale class="table" style="min-width:95%;max-width:100%">';
                _0xad76x51 += '<tr>';
                _0xad76x51 += '<th rowspan="3" >#</th>';
                _0xad76x51 += '<th rowspan="3" >Product Name</th>';
                _0xad76x51 += '<th rowspan="3" >Product ERP Code</th>';
                _0xad76x51 += '<th rowspan="3" >Pack</th>';
                var _0xad76x52 = ArrMonth['length'];
                var _0xad76x53 = _0xad76x52 * 2;
                var _0xad76x54 = Arr_Tot['length'];
                var _0xad76x55 = _0xad76x54 * 2;
                for (var _0xad76x1b = 0; _0xad76x1b < ArrHQDet['length']; _0xad76x1b++) {
                    if (Type == '1') {
                        _0xad76x51 += '<th  colspan="' + _0xad76x53 + '">' + ArrHQDet[_0xad76x1b]['State_Name'] + '</th>'
                    } else {
                        if (Type == '2') {
                            _0xad76x51 += '<th  colspan="' + _0xad76x53 + '">' + ArrHQDet[_0xad76x1b]['HQ_Name'] + '</th>'
                        }
                    }
                };
                _0xad76x51 += '<th colspan="' + _0xad76x55 + '" >Total</th>';
                _0xad76x51 += '</tr>';
                _0xad76x51 += '<tr>';
                for (var _0xad76x1b = 0; _0xad76x1b < ArrHQDet['length']; _0xad76x1b++) {
                    for (var _0xad76x56 = 0; _0xad76x56 < ArrMonth['length']; _0xad76x56++) {
                        _0xad76x51 += '<th  colspan="2" >' + ArrMonth[_0xad76x56] + '</th>'
                    }
                };
                for (var _0xad76x56 = 0; _0xad76x56 < Arr_Tot['length']; _0xad76x56++) {
                    _0xad76x51 += '<th  colspan="2" >' + Arr_Tot[_0xad76x56] + '</th>'
                };
                _0xad76x51 += '</tr>';
                _0xad76x51 += '<tr>';
                for (var _0xad76x1b = 0; _0xad76x1b < ArrHQDet['length']; _0xad76x1b++) {
                    for (var _0xad76x56 = 0; _0xad76x56 < ArrMonth['length']; _0xad76x56++) {
                        _0xad76x51 += '<th >Qty</th>';
                        _0xad76x51 += '<th >Value</th>'
                    }
                };
                for (var _0xad76x56 = 0; _0xad76x56 < Arr_Tot['length']; _0xad76x56++) {
                    _0xad76x51 += '<th >Qty</th>';
                    _0xad76x51 += '<th >Value</th>'
                };
                _0xad76x51 += '</tr>';
                for (var _0xad76x1b = 2; _0xad76x1b < _0xad76x50['length']; _0xad76x1b++) {
                    _0xad76x51 += '<tr>';
                    for (var _0xad76x2e = 0; _0xad76x2e < _0xad76x50[_0xad76x1b]['length']; _0xad76x2e++) {
                        var _0xad76x57 = '';
                        var _0xad76x58 = _0xad76x50[_0xad76x1b]['length'] - parseInt(_0xad76x55);
                        if (_0xad76x58 <= _0xad76x2e) {
                            _0xad76x57 = 'color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;'
                        } else {
                            _0xad76x57 = 'color:#636d73;'
                        };
                        _0xad76x51 += '<td style=' + _0xad76x57 + ';>' + _0xad76x50[_0xad76x1b][_0xad76x2e] + '</td>';
                        if (_0xad76x2e == _0xad76x50[_0xad76x1b]['length'] - 1) {
                            Active_Val = _0xad76x50[_0xad76x1b][_0xad76x2e]
                        }
                    };
                    _0xad76x51 += '</tr>'
                };
                _0xad76x51 += '</table>';
                $('#divpnl')['append'](_0xad76x51);
                TotalBg();
                TotalDeActivate()
            };
            $('.modal')['hide']()
        },
        error: function (_0xad76x32) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    $('#tblHQSale tr:last td')['each'](function () {
        var _0xad76x5a = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#F1F5F8;')
    })
}

function TotalDeActivate() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0xad76x1c) {
            dataTableSt = _0xad76x1c['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0xad76x2e = dataTableSt['length'] - 1;
                var _0xad76x5c = dataTableSt[_0xad76x2e];
                var _0xad76x51 = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0xad76x51 += '<tr>';
                _0xad76x51 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0xad76x51 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0xad76x51 += '</tr>';
                _0xad76x51 += '<tr>';
                _0xad76x51 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0xad76x51 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0xad76x51 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0xad76x51 += '</tr>';
                _0xad76x51 += '<tr>';
                _0xad76x51 += '<td style=background-color:#d4d4d4;>';
                _0xad76x51 += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0xad76x51 += '</td>';
                var _0xad76x5d = 0;
                $['each'](_0xad76x5c, function (_0xad76x4c, _0xad76x5e) {
                    var _0xad76x5f = _0xad76x4c;
                    if (_0xad76x5e == null || _0xad76x5e == '0') {
                        _0xad76x5e = ''
                    };
                    if (_0xad76x5f['includes']('_ABT') || _0xad76x5f['includes']('_ACT')) {
                        var _0xad76x60 = parseFloat(_0xad76x5e)['toFixed'](2);
                        DeAct_Val = _0xad76x60;
                        _0xad76x5d = parseFloat(Active_Val) + parseFloat(DeAct_Val)
                    }
                });
                _0xad76x51 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val) + '</td>';
                _0xad76x51 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>';
                _0xad76x51 += '</tr>';
                _0xad76x51 += '<tr>';
                _0xad76x51 += '<td style=background-color:#d4d4d4;>';
                _0xad76x51 += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0xad76x51 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0xad76x5d + '</span></td>';
                _0xad76x51 += '</td>';
                _0xad76x51 += '</tr>';
                _0xad76x51 += '</table>';
                $('#div_DeAct')['append'](_0xad76x51)
            }
        },
        error: function (_0xad76x32) {
            $('.modal')['hide']()
        }
    })
}

function storeUserDataInSession(_0xad76x62) {
    sessionStorage['removeItem']('userObject');
    var _0xad76x63 = JSON['stringify'](_0xad76x62);
    window['sessionStorage']['setItem']('userObject', _0xad76x63)
}