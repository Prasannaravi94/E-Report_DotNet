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
var DDl_Opt = '';
var Active_Val = [];
var DeAct_Val = 0;
$(document)['ready'](function () {
    $('.modal')['ajaxStart'](function () {
        $(this)['show']()
    })['ajaxStop'](function () {
        $(this)['hide']()
    });
    GetHQ_Det();
    var _0x1e4dxb = $('#SS_DivCode')['val']();
    var _0x1e4dxc = GetParameterValues('sfcode');
    var _0x1e4dxd = GetParameterValues('Sf_Name');
    var _0x1e4dxe = GetParameterValues('FMonth');
    var _0x1e4dxf = GetParameterValues('FYear');
    var _0x1e4dx10 = GetParameterValues('TMonth');
    var _0x1e4dx11 = GetParameterValues('TYear');
    var _0x1e4dx12 = GetParameterValues('Param');
    DDl_Opt = '2';
    var _0x1e4dx13 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0x1e4dx14 = parseInt(_0x1e4dxe - 1);
    var _0x1e4dx15 = parseInt(_0x1e4dx10 - 1);
    var _0x1e4dx16 = _0x1e4dx13[_0x1e4dx14];
    var _0x1e4dx17 = _0x1e4dx13[_0x1e4dx15];
    if (_0x1e4dxe != _0x1e4dx10) {
        ColMonth = _0x1e4dx16 + '-' + _0x1e4dx17
    } else {
        ColMonth = _0x1e4dx16 + '-' + _0x1e4dx17
    };
    var _0x1e4dx18 = 'Product - Stockist wise - (Sale & CB) for ' + _0x1e4dx16 + ' ' + _0x1e4dxf + ' to ' + ' ' + _0x1e4dx17 + ' ' + ' ' + _0x1e4dx11;
    var _0x1e4dx19 = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0x1e4dxd + '</span>';
    $('#lblStock')['html'](_0x1e4dx18);
    $('#lblField')['html'](_0x1e4dx19);
    var _0x1e4dx1a = (parseInt(_0x1e4dx11) - parseInt(_0x1e4dxf)) * 12 + parseInt(_0x1e4dx10) - parseInt(_0x1e4dxe);
    var _0x1e4dx1b = parseInt(_0x1e4dxe);
    var _0x1e4dx1c = parseInt(_0x1e4dxf);
    if (_0x1e4dx1a >= 0) {
        for (var _0x1e4dx1d = 1; _0x1e4dx1d <= _0x1e4dx1a + 1; _0x1e4dx1d++) {
            var _0x1e4dx14 = parseInt(_0x1e4dx1b - 1);
            var _0x1e4dx16 = _0x1e4dx13[_0x1e4dx14];
            var _0x1e4dx1e = _0x1e4dx16;
            ArrMonth['push'](_0x1e4dx1e);
            _0x1e4dx1b = _0x1e4dx1b + 1;
            if (_0x1e4dx1b == 13) {
                _0x1e4dx1b = 1;
                _0x1e4dx1c = _0x1e4dx1c + 1
            }
        };
        ArrMonth['push'](ColMonth)
    }
});

function GetHQ_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_All_Stockist_Det',
        data: '{}',
        dataType: 'json',
        success: function (_0x1e4dx20) {
            ArrHQDet = _0x1e4dx20['d'];
            GetRecordData()
        },
        error: function (_0x1e4dx21) { }
    })
}

function GetParameterValues(_0x1e4dx23) {
    var _0x1e4dx24 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0x1e4dx25 = 0; _0x1e4dx25 < _0x1e4dx24['length']; _0x1e4dx25++) {
        var _0x1e4dx26 = _0x1e4dx24[_0x1e4dx25]['split']('=');
        if (_0x1e4dx26[0] == _0x1e4dx23) {
            return decodeURIComponent(_0x1e4dx26[1])
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
        success: function (_0x1e4dx20) {
            dataTableSt = _0x1e4dx20['d']['dtProd']
        },
        error: function (_0x1e4dx21) { }
    })
}

function GetRecordData() {
    var _0x1e4dx29 = {};
    var _0x1e4dx2a = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Stockistwise_Prd_SaleAndClosing',
        data: '{}',
        dataType: 'json',
        success: function (_0x1e4dx20) {
            dataTableSt = _0x1e4dx20['d']['dtStock'];
            var _0x1e4dx2b = ['Prod_Name', 'Pack', 'T_SaleQty', 'T_SaleVal', 'T_ClsQty', 'T_ClsVal', 'Product_ERP_Code'];
            var _0x1e4dx2c = getPivotArray_Test(dataTableSt, _0x1e4dx2b, 'VST', 'Value');
            var _0x1e4dx2d = '<table id=tblHQSale class="table " style="min-width:95%;max-width:100%">';
            _0x1e4dx2d += '<tr>';
            _0x1e4dx2d += '<th rowspan="3" >#</th>';
            _0x1e4dx2d += '<th rowspan="3" >Product Name</th>';
            _0x1e4dx2d += '<th rowspan="3" >Product ERP Code</th>';
            _0x1e4dx2d += '<th rowspan="3" >Pack</th>';
            var _0x1e4dx2e = ArrMonth['length'];
            var _0x1e4dx2f = parseInt(_0x1e4dx2e) * 2;
            for (var _0x1e4dx25 = 0; _0x1e4dx25 < ArrHQDet['length']; _0x1e4dx25++) {
                _0x1e4dx2d += '<th  colspan=' + _0x1e4dx2f + '>' + ArrHQDet[_0x1e4dx25]['HQ_Name'] + '</th>'
            };
            _0x1e4dx2d += '<th colspan="4" >Total</th>';
            _0x1e4dx2d += '</tr>';
            _0x1e4dx2d += '<tr>';
            for (var _0x1e4dx25 = 0; _0x1e4dx25 < ArrHQDet['length']; _0x1e4dx25++) {
                _0x1e4dx2d += '<th  colspan=' + _0x1e4dx2e + '>Sale</th>';
                _0x1e4dx2d += '<th  colspan=' + _0x1e4dx2e + '>Closing</th>'
            };
            _0x1e4dx2d += '<th  colspan="2">Sale</th>';
            _0x1e4dx2d += '<th  colspan="2">Closing</th>';
            _0x1e4dx2d += '</tr>';
            _0x1e4dx2d += '<tr>';
            for (var _0x1e4dx25 = 0; _0x1e4dx25 < ArrHQDet['length']; _0x1e4dx25++) {
                for (_0x1e4dx1d = 0; _0x1e4dx1d < 2; _0x1e4dx1d++) {
                    for (var _0x1e4dx30 = 0; _0x1e4dx30 < ArrMonth['length']; _0x1e4dx30++) {
                        _0x1e4dx2d += '<th >' + ArrMonth[_0x1e4dx30] + '</th>'
                    }
                }
            };
            for (k = 0; k < 2; k++) {
                _0x1e4dx2d += '<th >Qty</th>';
                _0x1e4dx2d += '<th >Value</th>'
            };
            _0x1e4dx2d += '</tr>';
            for (var _0x1e4dx25 = 2; _0x1e4dx25 < _0x1e4dx2c['length']; _0x1e4dx25++) {
                _0x1e4dx2d += '<tr>';
                for (var _0x1e4dx1d = 0; _0x1e4dx1d < _0x1e4dx2c[_0x1e4dx25]['length']; _0x1e4dx1d++) {
                    var _0x1e4dx31 = '';
                    var _0x1e4dx32 = _0x1e4dx2c[_0x1e4dx25]['length'] - 4;
                    if (_0x1e4dx32 <= _0x1e4dx1d) {
                        _0x1e4dx31 = 'color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;'
                    } else {
                        _0x1e4dx31 = 'color:#636d73;'
                    };
                    _0x1e4dx2d += '<td style=' + _0x1e4dx31 + ';>' + _0x1e4dx2c[_0x1e4dx25][_0x1e4dx1d] + '</td>'
                };
                _0x1e4dx2d += '</tr>'
            };
            _0x1e4dx2d += '</table>';
            $('#divpnl')['append'](_0x1e4dx2d);
            TotalBg();
            TotalDeActivate();
            $('.modal')['hide']()
        },
        error: function (_0x1e4dx21) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    $('#tblHQSale tr:last td')['each'](function () {
        var _0x1e4dx34 = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#F1F5F8;')
    })
}

function getPivotArray(_0x1e4dx36, _0x1e4dx37, _0x1e4dx38, _0x1e4dx39) {
    var _0x1e4dx29 = {},
        _0x1e4dx3a = [];
    var _0x1e4dx3b = [];
    for (var _0x1e4dx25 = 0; _0x1e4dx25 < _0x1e4dx36['length']; _0x1e4dx25++) {
        if (!_0x1e4dx29[_0x1e4dx36[_0x1e4dx25][_0x1e4dx37]]) {
            _0x1e4dx29[_0x1e4dx36[_0x1e4dx25][_0x1e4dx37]] = {}
        };
        _0x1e4dx29[_0x1e4dx36[_0x1e4dx25][_0x1e4dx37]][_0x1e4dx36[_0x1e4dx25][_0x1e4dx38]] = _0x1e4dx36[_0x1e4dx25][_0x1e4dx39];
        if (_0x1e4dx3b['indexOf'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx38]) == -1) {
            _0x1e4dx3b['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx38])
        }
    };
    var _0x1e4dx3c = [];
    _0x1e4dx3c['push']('Item');
    _0x1e4dx3c['push']['apply'](_0x1e4dx3c, _0x1e4dx3b);
    _0x1e4dx3a['push'](_0x1e4dx3c);
    for (var _0x1e4dx3d in _0x1e4dx29) {
        _0x1e4dx3c = [];
        _0x1e4dx3c['push'](_0x1e4dx3d);
        for (var _0x1e4dx25 = 0; _0x1e4dx25 < _0x1e4dx3b['length']; _0x1e4dx25++) {
            _0x1e4dx3c['push'](_0x1e4dx29[_0x1e4dx3d][_0x1e4dx3b[_0x1e4dx25]] || '-')
        };
        _0x1e4dx3a['push'](_0x1e4dx3c)
    };
    return _0x1e4dx3a
}

function arrayToHTMLTable(_0x1e4dx3f) {
    var _0x1e4dx29 = '<table class=\'table table-bordered table-striped\' style=\'min-width:95%;max-width:100%\'>';
    for (var _0x1e4dx25 = 2; _0x1e4dx25 < _0x1e4dx3f['length']; _0x1e4dx25++) {
        _0x1e4dx29 += '<tr>';
        for (var _0x1e4dx1d = 0; _0x1e4dx1d < _0x1e4dx3f[_0x1e4dx25]['length']; _0x1e4dx1d++) {
            _0x1e4dx29 += '<td>' + _0x1e4dx3f[_0x1e4dx25][_0x1e4dx1d] + '</td>'
        };
        _0x1e4dx29 += '</tr>'
    };
    _0x1e4dx29 += '</table>';
    return _0x1e4dx29
}

function getPivotArray_Test(_0x1e4dx36, _0x1e4dx37, _0x1e4dx38, _0x1e4dx39) {
    var _0x1e4dx29 = {},
        _0x1e4dx3a = [];
    var _0x1e4dx3b = [];
    var _0x1e4dx41 = [];
    var _0x1e4dx42 = [];
    var _0x1e4dx43 = [];
    var _0x1e4dx44 = [];
    var _0x1e4dx45 = [];
    var _0x1e4dx46 = [];
    var _0x1e4dx47 = [];
    for (var _0x1e4dx25 = 0; _0x1e4dx25 < _0x1e4dx36['length']; _0x1e4dx25++) {
        if (!_0x1e4dx29[_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[0]]]) {
            _0x1e4dx29[_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[0]]] = {};
            _0x1e4dx41['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[1]]);
            _0x1e4dx42['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[0]]);
            _0x1e4dx43['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[6]]);
            if (_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[2]] == null || _0x1e4dx36[_0x1e4dx25][_0x1e4dx37[2]] == 0) {
                _0x1e4dx36[_0x1e4dx25][_0x1e4dx37[2]] = '';
                _0x1e4dx44['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[2]])
            } else {
                _0x1e4dx44['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[2]])
            };
            if (_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[3]] == null || _0x1e4dx36[_0x1e4dx25][_0x1e4dx37[3]] == 0) {
                _0x1e4dx36[_0x1e4dx25][_0x1e4dx37[3]] = '';
                _0x1e4dx45['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[3]])
            } else {
                _0x1e4dx45['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[3]])
            };
            if (_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[4]] == null || _0x1e4dx36[_0x1e4dx25][_0x1e4dx37[4]] == 0) {
                _0x1e4dx36[_0x1e4dx25][_0x1e4dx37[4]] = '';
                _0x1e4dx46['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[4]])
            } else {
                _0x1e4dx46['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[4]])
            };
            if (_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[5]] == null || _0x1e4dx36[_0x1e4dx25][_0x1e4dx37[5]] == 0) {
                _0x1e4dx36[_0x1e4dx25][_0x1e4dx37[5]] = '';
                _0x1e4dx47['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[5]])
            } else {
                _0x1e4dx47['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[5]])
            }
        };
        _0x1e4dx29[_0x1e4dx36[_0x1e4dx25][_0x1e4dx37[0]]][_0x1e4dx36[_0x1e4dx25][_0x1e4dx38]] = _0x1e4dx36[_0x1e4dx25][_0x1e4dx39];
        if (_0x1e4dx3b['indexOf'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx38]) == -1) {
            if (_0x1e4dx36[_0x1e4dx25][_0x1e4dx38] != null) {
                _0x1e4dx3b['push'](_0x1e4dx36[_0x1e4dx25][_0x1e4dx38])
            }
        }
    };
    var _0x1e4dx3c = [];
    _0x1e4dx3c['push']('Sl_No');
    _0x1e4dx3c['push']('Item');
    _0x1e4dx3c['push']('ERP');
    _0x1e4dx3c['push']('Pack');
    _0x1e4dx3c['push']['apply'](_0x1e4dx3c, _0x1e4dx3b);
    _0x1e4dx3a['push'](_0x1e4dx3c);
    var _0x1e4dx48 = 0;
    var _0x1e4dx49 = 0;
    for (var _0x1e4dx3d in _0x1e4dx29) {
        if (_0x1e4dx48 < 1) {
            _0x1e4dx49 = 0
        } else {
            _0x1e4dx49 += 1
        };
        _0x1e4dx3c = [];
        _0x1e4dx3c['push'](_0x1e4dx49);
        _0x1e4dx3c['push'](_0x1e4dx3d);
        _0x1e4dx3c['push'](_0x1e4dx43[_0x1e4dx48]);
        _0x1e4dx3c['push'](_0x1e4dx41[_0x1e4dx48]);
        for (var _0x1e4dx25 = 0; _0x1e4dx25 < _0x1e4dx3b['length']; _0x1e4dx25++) {
            _0x1e4dx3c['push'](_0x1e4dx29[_0x1e4dx3d][_0x1e4dx3b[_0x1e4dx25]] || ' ')
        };
        _0x1e4dx3c['push'](_0x1e4dx44[_0x1e4dx48]);
        _0x1e4dx3c['push'](_0x1e4dx45[_0x1e4dx48]);
        _0x1e4dx3c['push'](_0x1e4dx46[_0x1e4dx48]);
        _0x1e4dx3c['push'](_0x1e4dx47[_0x1e4dx48]);
        _0x1e4dx3a['push'](_0x1e4dx3c);
        _0x1e4dx48 += 1
    };
    var _0x1e4dx4a = _0x1e4dx45['length'] - 1;
    var _0x1e4dx4b = _0x1e4dx47['length'] - 1;
    Active_Val['push'](_0x1e4dx45[_0x1e4dx4a]);
    Active_Val['push'](_0x1e4dx47[_0x1e4dx4b]);
    return _0x1e4dx3a
}

function TotalDeActivate() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0x1e4dx20) {
            dataTableSt = _0x1e4dx20['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0x1e4dx1d = dataTableSt['length'] - 1;
                var _0x1e4dx4d = dataTableSt[_0x1e4dx1d];
                var _0x1e4dx2d = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0x1e4dx2d += '<tr>';
                _0x1e4dx2d += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x1e4dx2d += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0x1e4dx2d += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Closing</th>';
                _0x1e4dx2d += '</tr>';
                _0x1e4dx2d += '<tr>';
                _0x1e4dx2d += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x1e4dx2d += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x1e4dx2d += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x1e4dx2d += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x1e4dx2d += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x1e4dx2d += '</tr>';
                _0x1e4dx2d += '<tr>';
                _0x1e4dx2d += '<td style=background-color:#d4d4d4;>';
                _0x1e4dx2d += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0x1e4dx2d += '</td>';
                var _0x1e4dx4e = 0;
                var _0x1e4dx48 = 0;
                var _0x1e4dx4f = 0;
                $['each'](_0x1e4dx4d, function (_0x1e4dx3d, _0x1e4dx50) {
                    var _0x1e4dx51 = _0x1e4dx3d;
                    if (_0x1e4dx50 == null || _0x1e4dx50 == '0') {
                        _0x1e4dx50 = ''
                    };
                    if (_0x1e4dx51['includes']('_ABT') || _0x1e4dx51['includes']('_ACT')) {
                        var _0x1e4dx52 = parseFloat(_0x1e4dx50)['toFixed'](2);
                        DeAct_Val = _0x1e4dx52;
                        if (_0x1e4dx48 == 0) {
                            DeAct_Val = _0x1e4dx52;
                            _0x1e4dx4e = parseFloat(Active_Val[0]) + parseFloat(DeAct_Val);
                            _0x1e4dx2d += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[0]) + '</td>';
                            _0x1e4dx2d += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        } else {
                            DeAct_Val = _0x1e4dx52;
                            _0x1e4dx4f = parseFloat(Active_Val[1]) + parseFloat(DeAct_Val);
                            _0x1e4dx2d += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[1]) + '</td>';
                            _0x1e4dx2d += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        };
                        _0x1e4dx48 += 1
                    }
                });
                _0x1e4dx2d += '</tr>';
                _0x1e4dx2d += '<tr>';
                _0x1e4dx2d += '<td style=background-color:#d4d4d4;>';
                _0x1e4dx2d += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0x1e4dx2d += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x1e4dx4e + '</span></td>';
                _0x1e4dx2d += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x1e4dx4f + '</span></td>';
                _0x1e4dx2d += '</td>';
                _0x1e4dx2d += '</tr>';
                _0x1e4dx2d += '</table>';
                $('#div_DeAct')['append'](_0x1e4dx2d)
            }
        },
        error: function (_0x1e4dx21) {
            $('.modal')['hide']()
        }
    })
}

function storeUserDataInSession(_0x1e4dx54) {
    sessionStorage['clear']();
    var _0x1e4dx55 = JSON['stringify'](_0x1e4dx54);
    window['sessionStorage']['setItem']('userObject', _0x1e4dx55)
}