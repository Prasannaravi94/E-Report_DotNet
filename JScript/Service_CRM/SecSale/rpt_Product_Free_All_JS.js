window['onload'] = function () {
    if (window['location']['href'] == sessionStorage['getItem']('userObject')) {
        sessionStorage['clear']()
    }
};
var ArrHQDet = {};
var dataTableSt;
var ColMonth = '';
var ArrMonth = [];
var DDl_Opt = '';
var Active_Val = [];
var DeAct_Val = 0;
var ArrSale = {};
$(document)['ready'](function () {
    $('.modal')['ajaxStart'](function () {
        $(this)['show']()
    })['ajaxStop'](function () {
        $(this)['hide']()
    });
    GetHQ_Det();
    var _0x3336x9 = $('#SS_DivCode')['val']();
    var _0x3336xa = GetParameterValues('sfcode');
    var _0x3336xb = GetParameterValues('Sf_Name');
    var _0x3336xc = GetParameterValues('FMonth');
    var _0x3336xd = GetParameterValues('FYear');
    var _0x3336xe = GetParameterValues('TMonth');
    var _0x3336xf = GetParameterValues('TYear');
    DDl_Opt = '2';
    var _0x3336x10 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0x3336x11 = parseInt(_0x3336xc - 1);
    var _0x3336x12 = parseInt(_0x3336xe - 1);
    var _0x3336x13 = _0x3336x10[_0x3336x11];
    var _0x3336x14 = _0x3336x10[_0x3336x12];
    if (_0x3336xc != _0x3336xe) {
        ColMonth = _0x3336x13 + '-' + _0x3336x14
    } else {
        ColMonth = _0x3336x13 + '-' + _0x3336x14
    };
    var _0x3336x15 = 'Stock & Sale Statement for ' + _0x3336x13 + ' ' + _0x3336xd + ' to ' + ' ' + _0x3336x14 + ' ' + ' ' + _0x3336xf;
    var _0x3336x16 = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0x3336xb + '</span>';
    $('#lblStock')['html'](_0x3336x15);
    $('#lblField')['html'](_0x3336x16);
    var _0x3336x17 = (parseInt(_0x3336xf) - parseInt(_0x3336xd)) * 12 + parseInt(_0x3336xe) - parseInt(_0x3336xc);
    var _0x3336x18 = parseInt(_0x3336xc);
    var _0x3336x19 = parseInt(_0x3336xd);
    if (_0x3336x17 >= 0) {
        for (var _0x3336x1a = 1; _0x3336x1a <= _0x3336x17 + 1; _0x3336x1a++) {
            var _0x3336x11 = parseInt(_0x3336x18 - 1);
            var _0x3336x13 = _0x3336x10[_0x3336x11];
            var _0x3336x1b = _0x3336x13;
            ArrMonth['push'](_0x3336x1b);
            _0x3336x18 = _0x3336x18 + 1;
            if (_0x3336x18 == 13) {
                _0x3336x18 = 1;
                _0x3336x19 = _0x3336x19 + 1
            }
        }
    }
});

function TotalBg() {
    $('#tblFreeAll tr:last td')['each'](function () {
        var _0x3336x1d = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#F1F5F8;')
    })
}

function GetHQ_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Parameter',
        data: '{}',
        dataType: 'json',
        success: function (_0x3336x1f) {
            ArrHQDet = _0x3336x1f['d'];
            GetReport()
        },
        error: function (_0x3336x20) { }
    })
}

function GetReport() {
    var _0x3336x22 = {};
    var _0x3336x23 = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_SecSale_Free_AllMode',
        data: '{}',
        dataType: 'json',
        success: function (_0x3336x1f) {
            dataTableSt = _0x3336x1f['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                var _0x3336x24 = '<table id="tblFreeAll" class="table" style="min-width:95%;max-width:100%">';
                _0x3336x24 += '<tr>';
                _0x3336x24 += '<th rowspan="3" >#</th>';
                _0x3336x24 += '<th rowspan="3" >Product Name</th>';
                _0x3336x24 += '<th rowspan="3" >Pack</th>';
                _0x3336x24 += '<th rowspan="3" >Product ERP Code</th>';
                var _0x3336x25 = ArrHQDet['length'];
                var _0x3336x26 = parseInt(_0x3336x25) * 4;
                for (var _0x3336x27 = 0; _0x3336x27 < ArrMonth['length']; _0x3336x27++) {
                    _0x3336x24 += '<th colspan="' + _0x3336x26 + '" >' + ArrMonth[_0x3336x27] + '</th>'
                };
                _0x3336x24 += '</tr>';
                _0x3336x24 += '<tr>';
                for (var _0x3336x27 = 0; _0x3336x27 < ArrMonth['length']; _0x3336x27++) {
                    for (var _0x3336x28 = 0; _0x3336x28 < ArrHQDet['length']; _0x3336x28++) {
                        _0x3336x24 += '<th colspan="4" >' + ArrHQDet[_0x3336x28]['Sec_Sale_Name'] + '</th>'
                    }
                };
                _0x3336x24 += '</tr>';
                _0x3336x24 += '<tr>';
                for (var _0x3336x27 = 0; _0x3336x27 < ArrMonth['length']; _0x3336x27++) {
                    for (var _0x3336x28 = 0; _0x3336x28 < ArrHQDet['length']; _0x3336x28++) {
                        _0x3336x24 += '<th >Qty</th>';
                        _0x3336x24 += '<th >Value</th>';
                        _0x3336x24 += '<th >Free</th>';
                        _0x3336x24 += '<th >Value</th>'
                    }
                };
                _0x3336x24 += '</tr>';
                for (var _0x3336x1a = 0; _0x3336x1a < dataTableSt['length']; _0x3336x1a++) {
                    var _0x3336x29 = dataTableSt[_0x3336x1a];
                    var _0x3336x2a = 0;
                    _0x3336x24 += '<tr>';
                    $['each'](_0x3336x29, function (_0x3336x2b, _0x3336x2c) {
                        var _0x3336x2d = _0x3336x2b;
                        if (_0x3336x2c == null || _0x3336x2c == '0') {
                            _0x3336x2c = ''
                        };
                        if (_0x3336x2d == 'Sl_No' || _0x3336x2d == 'Prod_Name' || _0x3336x2d == 'Sale_ERP_Code' || _0x3336x2d == 'Pack') {
                            _0x3336x24 += '<td>' + _0x3336x2c + '</td>'
                        } else {
                            if (_0x3336x2d['includes']('_ABT') || _0x3336x2d['includes']('_ACT') || _0x3336x2d['includes']('_ADT') || _0x3336x2d['includes']('_AET')) {
                                var _0x3336x2e = _0x3336x2d['split']('_');
                                var _0x3336x2f = _0x3336x2e[1];
                                var _0x3336x30 = _0x3336x2e[0];
                                if (_0x3336x2d['includes']('_ACT') || _0x3336x2d['includes']('_AET')) {
                                    if (_0x3336x2c != '') {
                                        _0x3336x2c = parseFloat(_0x3336x2c)['toFixed'](2)
                                    }
                                };
                                if (_0x3336x1a == dataTableSt['length'] - 1) {
                                    if (_0x3336x2c != '') {
                                        _0x3336x2c = parseFloat(_0x3336x2c)['toFixed'](2)
                                    };
                                    _0x3336x24 += '<td>' + _0x3336x2c + '</td>'
                                } else {
                                    _0x3336x24 += '<td>' + _0x3336x2c + '</td>'
                                }
                            } else {
                                if (_0x3336x2d == 'T_SaleVal' || _0x3336x2d == 'T_ClsVal') {
                                    if (_0x3336x2c != '') {
                                        _0x3336x2c = parseFloat(_0x3336x2c)['toFixed'](2);
                                        if (_0x3336x1a == dataTableSt['length'] - 1) {
                                            if (_0x3336x2d == 'T_SaleVal') {
                                                Active_Val['push'](_0x3336x2c)
                                            } else {
                                                if (_0x3336x2d == 'T_ClsVal') {
                                                    Active_Val['push'](_0x3336x2c)
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    });
                    _0x3336x24 += '</tr>'
                };
                _0x3336x24 += '</table>';
                $('#divpnl')['append'](_0x3336x24);
                TotalBg();
                TotalDeActivate()
            };
            $('.modal')['hide']()
        },
        error: function (_0x3336x20) {
            $('.modal')['hide']()
        }
    })
}

function GetParameterValues(_0x3336x32) {
    var _0x3336x33 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0x3336x34 = 0; _0x3336x34 < _0x3336x33['length']; _0x3336x34++) {
        var _0x3336x35 = _0x3336x33[_0x3336x34]['split']('=');
        if (_0x3336x35[0] == _0x3336x32) {
            return decodeURIComponent(_0x3336x35[1])
        }
    }
}

function TotalDeActivate() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0x3336x1f) {
            dataTableSt = _0x3336x1f['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0x3336x1a = dataTableSt['length'] - 1;
                var _0x3336x29 = dataTableSt[_0x3336x1a];
                var _0x3336x24 = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0x3336x24 += '<tr>';
                _0x3336x24 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x3336x24 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0x3336x24 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Closing</th>';
                _0x3336x24 += '</tr>';
                _0x3336x24 += '<tr>';
                _0x3336x24 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x3336x24 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x3336x24 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x3336x24 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x3336x24 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x3336x24 += '</tr>';
                _0x3336x24 += '<tr>';
                _0x3336x24 += '<td style=background-color:#d4d4d4;>';
                _0x3336x24 += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0x3336x24 += '</td>';
                var _0x3336x37 = 0;
                var _0x3336x38 = 0;
                var _0x3336x39 = 0;
                $['each'](_0x3336x29, function (_0x3336x2b, _0x3336x2c) {
                    var _0x3336x2d = _0x3336x2b;
                    if (_0x3336x2c == null || _0x3336x2c == '0') {
                        _0x3336x2c = ''
                    };
                    if (_0x3336x2d['includes']('_ABT') || _0x3336x2d['includes']('_ACT')) {
                        var _0x3336x3a = parseFloat(_0x3336x2c)['toFixed'](2);
                        DeAct_Val = _0x3336x3a;
                        if (_0x3336x38 == 0) {
                            DeAct_Val = _0x3336x3a;
                            _0x3336x37 = parseFloat(Active_Val[0]) + parseFloat(DeAct_Val);
                            _0x3336x24 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[0]) + '</td>';
                            _0x3336x24 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        } else {
                            DeAct_Val = _0x3336x3a;
                            _0x3336x39 = parseFloat(Active_Val[1]) + parseFloat(DeAct_Val);
                            _0x3336x24 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[1]) + '</td>';
                            _0x3336x24 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        };
                        _0x3336x38 += 1
                    }
                });
                _0x3336x24 += '</tr>';
                _0x3336x24 += '<tr>';
                _0x3336x24 += '<td style=background-color:#d4d4d4;>';
                _0x3336x24 += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0x3336x24 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x3336x37 + '</span></td>';
                _0x3336x24 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x3336x39 + '</span></td>';
                _0x3336x24 += '</td>';
                _0x3336x24 += '</tr>';
                _0x3336x24 += '</table>';
                $('#div_DeAct')['append'](_0x3336x24)
            }
        },
        error: function (_0x3336x20) {
            $('.modal')['hide']()
        }
    })
}

function storeUserDataInSession(_0x3336x3c) {
    sessionStorage['clear']();
    var _0x3336x3d = JSON['stringify'](_0x3336x3c);
    window['sessionStorage']['setItem']('userObject', _0x3336x3d)
}