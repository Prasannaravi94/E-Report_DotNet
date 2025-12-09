var ArrHQDet = {};
var dataTableSt;
var ColMonth = '';
var ArrMonth = [];
var ArrSale = {};
var StockName = '';
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
    var _0x86cexa = $('#SS_DivCode')['val']();
    var _0x86cexb = GetParameterValues('sfcode');
    var _0x86cexc = GetParameterValues('Sf_Name');
    var _0x86cexd = GetParameterValues('FMonth');
    var _0x86cexe = GetParameterValues('FYear');
    var _0x86cexf = GetParameterValues('TMonth');
    var _0x86cex10 = GetParameterValues('TYear');
    DDl_Opt = '2';

    function _0x86cex11() {
        var _0x86cex12 = window['location']['search'];
        var _0x86cex13 = /([^?&=]*)=([^&]*)/g;
        var _0x86cex14 = {};
        var _0x86cex15 = null;
        while (_0x86cex15 = _0x86cex13['exec'](_0x86cex12)) {
            _0x86cex14[_0x86cex15[1]] = decodeURIComponent(_0x86cex15[2])
        };
        return _0x86cex14
    }
    var _0x86cex14 = _0x86cex11();
    try {
        var _0x86cex16 = JSON['parse'](_0x86cex14.StockName);
        StockName = _0x86cex16;
        StockName = StockName['replace']('_', ' ')
    } catch (err) { };
    var _0x86cex17 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0x86cex18 = parseInt(_0x86cexd - 1);
    var _0x86cex19 = parseInt(_0x86cexf - 1);
    var _0x86cex1a = _0x86cex17[_0x86cex18];
    var _0x86cex1b = _0x86cex17[_0x86cex19];
    if (_0x86cexd != _0x86cexf) {
        ColMonth = _0x86cex1a + '-' + _0x86cex1b
    } else {
        ColMonth = _0x86cex1a + '-' + _0x86cex1b
    };
    var _0x86cex1c = 'Product wise - Stock (All Sale & CB) for ' + _0x86cex1a + ' ' + _0x86cexe + ' to ' + ' ' + _0x86cex1b + ' ' + ' ' + _0x86cex10;
    var _0x86cex1d = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0x86cexc + '</span>&nbsp - &nbsp;&nbsp;&nbsp;(<span style=\'color:#0077ff\'>Stockist Name :</span><span style=\'color:#c17111\'> ' + StockName + '</span>)';
    $('#lblStock')['html'](_0x86cex1c);
    $('#lblField')['html'](_0x86cex1d);
    var _0x86cex1e = (parseInt(_0x86cex10) - parseInt(_0x86cexe)) * 12 + parseInt(_0x86cexf) - parseInt(_0x86cexd);
    var _0x86cex1f = parseInt(_0x86cexd);
    var _0x86cex20 = parseInt(_0x86cexe);
    if (_0x86cex1e >= 0) {
        for (var _0x86cex21 = 1; _0x86cex21 <= _0x86cex1e + 1; _0x86cex21++) {
            var _0x86cex18 = parseInt(_0x86cex1f - 1);
            var _0x86cex1a = _0x86cex17[_0x86cex18];
            var _0x86cex22 = _0x86cex1a;
            ArrMonth['push'](_0x86cex22);
            _0x86cex1f = _0x86cex1f + 1;
            if (_0x86cex1f == 13) {
                _0x86cex1f = 1;
                _0x86cex20 = _0x86cex20 + 1
            }
        }
    }
});

function GetParameterValues(_0x86cex24) {
    var _0x86cex25 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0x86cex15 = 0; _0x86cex15 < _0x86cex25['length']; _0x86cex15++) {
        var _0x86cex26 = _0x86cex25[_0x86cex15]['split']('=');
        if (_0x86cex26[0] == _0x86cex24) {
            return decodeURIComponent(_0x86cex26[1])
        }
    }
}

function GetRecordData() {
    var _0x86cex28 = {};
    var _0x86cex29 = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/GetStockist_Product_Stockist_Month_Cum',
        data: '{}',
        dataType: 'json',
        success: function (_0x86cex16) {
            dataTableSt = _0x86cex16['d']['dtStock'];
            var _0x86cex2a = ['Prod_Name', 'Pack', 'Product_ERP_Code'];
            var _0x86cex2b = getPivotArray_Test(dataTableSt, _0x86cex2a, 'VST', 'Qty');
            if (dataTableSt['length'] > 0) {
                var _0x86cex2c = '<table id="tblSale" class="table" style="min-width:95%;max-width:100%">';
                _0x86cex2c += '<tr>';
                _0x86cex2c += '<th rowspan="3" >#</th>';
                _0x86cex2c += '<th rowspan="3" >Product Name</th>';
                _0x86cex2c += '<th rowspan="3" >Product ERP Code</th>';
                _0x86cex2c += '<th rowspan="3" >Pack</th>';
                var _0x86cex2d = ArrSale['length'];
                var _0x86cex2e = parseInt(_0x86cex2d) * 2;
                for (var _0x86cex2f = 0; _0x86cex2f < ArrMonth['length']; _0x86cex2f++) {
                    _0x86cex2c += '<th colspan="' + _0x86cex2e + '" >' + ArrMonth[_0x86cex2f] + '</th>'
                };
                _0x86cex2c += '<th colspan="' + _0x86cex2e + '" >Total</th>';
                _0x86cex2c += '</tr>';
                _0x86cex2c += '<tr>';
                for (var _0x86cex2f = 0; _0x86cex2f < ArrMonth['length']; _0x86cex2f++) {
                    for (var _0x86cex30 = 0; _0x86cex30 < ArrHQDet['length']; _0x86cex30++) {
                        var _0x86cex31 = ArrHQDet[_0x86cex30]['Sec_Sale_Code'];
                        var _0x86cex32 = '';
                        for (var _0x86cex33 = 0; _0x86cex33 < ArrSale['length']; _0x86cex33++) {
                            var _0x86cex34 = ArrSale[_0x86cex33]['Sec_Sale_Code'];
                            if ($['trim'](_0x86cex31) == $['trim'](_0x86cex34)) {
                                _0x86cex32 = '2';
                                _0x86cex2c += '<th   colspan="' + _0x86cex32 + '" >' + ArrHQDet[_0x86cex30]['Sec_Sale_Name'] + '</th>';
                                break
                            }
                        }
                    }
                };
                for (var _0x86cex30 = 0; _0x86cex30 < ArrHQDet['length']; _0x86cex30++) {
                    var _0x86cex31 = ArrHQDet[_0x86cex30]['Sec_Sale_Code'];
                    var _0x86cex32 = '';
                    for (var _0x86cex33 = 0; _0x86cex33 < ArrSale['length']; _0x86cex33++) {
                        var _0x86cex34 = ArrSale[_0x86cex33]['Sec_Sale_Code'];
                        if ($['trim'](_0x86cex31) == $['trim'](_0x86cex34)) {
                            _0x86cex32 = '2';
                            _0x86cex2c += '<th   colspan="' + _0x86cex32 + '" >' + ArrHQDet[_0x86cex30]['Sec_Sale_Name'] + '</th>';
                            break
                        }
                    }
                };
                _0x86cex2c += '</tr>';
                _0x86cex2c += '<tr>';
                for (var _0x86cex2f = 0; _0x86cex2f < ArrMonth['length']; _0x86cex2f++) {
                    for (var _0x86cex30 = 0; _0x86cex30 < ArrHQDet['length']; _0x86cex30++) {
                        var _0x86cex31 = ArrHQDet[_0x86cex30]['Sec_Sale_Code'];
                        var _0x86cex35 = false;
                        for (var _0x86cex15 = 0; _0x86cex15 < ArrSale['length'] && !_0x86cex35; _0x86cex15++) {
                            if (ArrSale[_0x86cex15]['Sec_Sale_Code'] === _0x86cex31) {
                                _0x86cex35 = true;
                                break
                            }
                        };
                        if (_0x86cex35 == true) {
                            _0x86cex2c += '<th >Qty </th>';
                            _0x86cex2c += '<th >Value</th>'
                        }
                    }
                };
                for (var _0x86cex30 = 0; _0x86cex30 < ArrHQDet['length']; _0x86cex30++) {
                    var _0x86cex31 = ArrHQDet[_0x86cex30]['Sec_Sale_Code'];
                    var _0x86cex35 = false;
                    for (var _0x86cex15 = 0; _0x86cex15 < ArrSale['length'] && !_0x86cex35; _0x86cex15++) {
                        if (ArrSale[_0x86cex15]['Sec_Sale_Code'] === _0x86cex31) {
                            _0x86cex35 = true;
                            break
                        }
                    };
                    if (_0x86cex35 == true) {
                        _0x86cex2c += '<th >Qty </th>';
                        _0x86cex2c += '<th >Value</th>'
                    }
                };
                _0x86cex2c += '</tr>';
                for (var _0x86cex15 = 2; _0x86cex15 < _0x86cex2b['length']; _0x86cex15++) {
                    _0x86cex2c += '<tr>';
                    for (var _0x86cex21 = 0; _0x86cex21 < _0x86cex2b[_0x86cex15]['length']; _0x86cex21++) {
                        var _0x86cex36 = '';
                        var _0x86cex37 = _0x86cex2b[_0x86cex15]['length'] - parseInt(_0x86cex2e);
                        if (_0x86cex37 <= _0x86cex21) {
                            _0x86cex36 = 'color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;'
                        } else {
                            _0x86cex36 = 'color:#636d73;'
                        };
                        _0x86cex2c += '<td style=' + _0x86cex36 + ';>' + _0x86cex2b[_0x86cex15][_0x86cex21] + '</td>'
                    };
                    _0x86cex2c += '</tr>'
                };
                _0x86cex2c += '</table>';
                $('#divpnl')['append'](_0x86cex2c);
                TotalBg();
                TotalDeActivate()
            };
            $('.modal')['hide']()
        },
        error: function (_0x86cex38) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    var _0x86cex3a = $('#tblSale tr:last td')['length'] - 4;
    $('#tblSale tr:last td')['each'](function (_0x86cex15) {
        var _0x86cex3b = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#F1F5F8;');
        if (_0x86cex15 >= _0x86cex3a) {
            Active_Val['push'](_0x86cex3b)
        }
    })
}

function getPivotArray(_0x86cex3d, _0x86cex3e, _0x86cex3f, _0x86cex40) {
    var _0x86cex28 = {},
        _0x86cex41 = [];
    var _0x86cex42 = [];
    for (var _0x86cex15 = 0; _0x86cex15 < _0x86cex3d['length']; _0x86cex15++) {
        if (!_0x86cex28[_0x86cex3d[_0x86cex15][_0x86cex3e]]) {
            _0x86cex28[_0x86cex3d[_0x86cex15][_0x86cex3e]] = {}
        };
        _0x86cex28[_0x86cex3d[_0x86cex15][_0x86cex3e]][_0x86cex3d[_0x86cex15][_0x86cex3f]] = _0x86cex3d[_0x86cex15][_0x86cex40];
        if (_0x86cex42['indexOf'](_0x86cex3d[_0x86cex15][_0x86cex3f]) == -1) {
            _0x86cex42['push'](_0x86cex3d[_0x86cex15][_0x86cex3f])
        }
    };
    var _0x86cex43 = [];
    _0x86cex43['push']('Item');
    _0x86cex43['push']['apply'](_0x86cex43, _0x86cex42);
    _0x86cex41['push'](_0x86cex43);
    for (var _0x86cex44 in _0x86cex28) {
        _0x86cex43 = [];
        _0x86cex43['push'](_0x86cex44);
        for (var _0x86cex15 = 0; _0x86cex15 < _0x86cex42['length']; _0x86cex15++) {
            _0x86cex43['push'](_0x86cex28[_0x86cex44][_0x86cex42[_0x86cex15]] || '-')
        };
        _0x86cex41['push'](_0x86cex43)
    };
    return _0x86cex41
}

function arrayToHTMLTable(_0x86cex46) {
    var _0x86cex28 = '<table class=\'table table-bordered table-striped\' style=\'min-width:95%;max-width:100%\'>';
    for (var _0x86cex15 = 2; _0x86cex15 < _0x86cex46['length']; _0x86cex15++) {
        _0x86cex28 += '<tr>';
        for (var _0x86cex21 = 0; _0x86cex21 < _0x86cex46[_0x86cex15]['length']; _0x86cex21++) {
            _0x86cex28 += '<td>' + _0x86cex46[_0x86cex15][_0x86cex21] + '</td>'
        };
        _0x86cex28 += '</tr>'
    };
    _0x86cex28 += '</table>';
    return _0x86cex28
}

function getPivotArray_Test(_0x86cex3d, _0x86cex3e, _0x86cex3f, _0x86cex40) {
    var _0x86cex28 = {},
        _0x86cex41 = [];
    var _0x86cex42 = [];
    var _0x86cex48 = [];
    var _0x86cex49 = [];
    var _0x86cex4a = [];
    var _0x86cex4b = [];
    var _0x86cex4c = [];
    var _0x86cex4d = [];
    var _0x86cex4e = [];
    for (var _0x86cex15 = 0; _0x86cex15 < _0x86cex3d['length']; _0x86cex15++) {
        if (!_0x86cex28[_0x86cex3d[_0x86cex15][_0x86cex3e[0]]]) {
            _0x86cex28[_0x86cex3d[_0x86cex15][_0x86cex3e[0]]] = {};
            _0x86cex48['push'](_0x86cex3d[_0x86cex15][_0x86cex3e[1]]);
            _0x86cex49['push'](_0x86cex3d[_0x86cex15][_0x86cex3e[0]]);
            _0x86cex4a['push'](_0x86cex3d[_0x86cex15][_0x86cex3e[2]])
        };
        _0x86cex28[_0x86cex3d[_0x86cex15][_0x86cex3e[0]]][_0x86cex3d[_0x86cex15][_0x86cex3f]] = _0x86cex3d[_0x86cex15][_0x86cex40];
        if (_0x86cex42['indexOf'](_0x86cex3d[_0x86cex15][_0x86cex3f]) == -1) {
            if (_0x86cex3d[_0x86cex15][_0x86cex3f] != null) {
                _0x86cex42['push'](_0x86cex3d[_0x86cex15][_0x86cex3f])
            }
        }
    };
    var _0x86cex43 = [];
    _0x86cex43['push']('Sl_No');
    _0x86cex43['push']('Item');
    _0x86cex43['push']('ERP');
    _0x86cex43['push']('Pack');
    _0x86cex43['push']['apply'](_0x86cex43, _0x86cex42);
    _0x86cex41['push'](_0x86cex43);
    var _0x86cex4f = 0;
    var _0x86cex50 = 0;
    for (var _0x86cex44 in _0x86cex28) {
        if (_0x86cex4f < 1) {
            _0x86cex50 = 0
        } else {
            _0x86cex50 += 1
        };
        _0x86cex43 = [];
        _0x86cex43['push'](_0x86cex50);
        _0x86cex43['push'](_0x86cex44);
        _0x86cex43['push'](_0x86cex4a[_0x86cex4f]);
        _0x86cex43['push'](_0x86cex48[_0x86cex4f]);
        for (var _0x86cex15 = 0; _0x86cex15 < _0x86cex42['length']; _0x86cex15++) {
            _0x86cex43['push'](_0x86cex28[_0x86cex44][_0x86cex42[_0x86cex15]] || ' ')
        };
        _0x86cex41['push'](_0x86cex43);
        _0x86cex4f += 1
    };
    return _0x86cex41
}

function GetHQ_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Parameter',
        data: '{}',
        dataType: 'json',
        success: function (_0x86cex16) {
            ArrHQDet = _0x86cex16['d'];
            GetSaleclose()
        },
        error: function (_0x86cex38) { }
    })
}

function GetSaleclose() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/GetSaleClose_Field',
        data: '{}',
        dataType: 'json',
        success: function (_0x86cex16) {
            ArrSale = _0x86cex16['d'];
            GetRecordData()
        },
        error: function (_0x86cex38) { }
    })
}

function TotalDeActivate() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0x86cex16) {
            dataTableSt = _0x86cex16['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0x86cex21 = dataTableSt['length'] - 1;
                var _0x86cex54 = dataTableSt[_0x86cex21];
                var _0x86cex2c = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0x86cex2c += '<tr>';
                _0x86cex2c += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x86cex2c += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0x86cex2c += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Closing</th>';
                _0x86cex2c += '</tr>';
                _0x86cex2c += '<tr>';
                _0x86cex2c += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x86cex2c += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x86cex2c += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x86cex2c += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x86cex2c += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x86cex2c += '</tr>';
                _0x86cex2c += '<tr>';
                _0x86cex2c += '<td style=background-color:#d4d4d4;>';
                _0x86cex2c += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0x86cex2c += '</td>';
                var _0x86cex55 = 0;
                var _0x86cex4f = 0;
                var _0x86cex56 = 0;
                $['each'](_0x86cex54, function (_0x86cex44, _0x86cex57) {
                    var _0x86cex58 = _0x86cex44;
                    if (_0x86cex57 == null || _0x86cex57 == '0') {
                        _0x86cex57 = ''
                    };
                    if (_0x86cex58['includes']('_ABT') || _0x86cex58['includes']('_ACT')) {
                        var _0x86cex59 = parseFloat(_0x86cex57)['toFixed'](2);
                        DeAct_Val = _0x86cex59;
                        if (_0x86cex4f == 0) {
                            DeAct_Val = _0x86cex59;
                            _0x86cex55 = parseFloat(Active_Val[1]) + parseFloat(DeAct_Val);
                            _0x86cex2c += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[1]) + '</td>';
                            _0x86cex2c += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        } else {
                            DeAct_Val = _0x86cex59;
                            _0x86cex56 = parseFloat(Active_Val[3]) + parseFloat(DeAct_Val);
                            _0x86cex2c += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[3]) + '</td>';
                            _0x86cex2c += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        };
                        _0x86cex4f += 1
                    }
                });
                _0x86cex2c += '</tr>';
                _0x86cex2c += '<tr>';
                _0x86cex2c += '<td style=background-color:#d4d4d4;>';
                _0x86cex2c += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0x86cex2c += '<td colspan=2  style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x86cex55 + '</span></td>';
                _0x86cex2c += '<td colspan=2  style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x86cex56 + '</span></td>';
                _0x86cex2c += '</td>';
                _0x86cex2c += '</tr>';
                _0x86cex2c += '</table>';
                $('#div_DeAct')['append'](_0x86cex2c)
            };
            $('.modal')['hide']()
        },
        error: function (_0x86cex38) {
            $('.modal')['hide']()
        }
    })
}

function storeUserDataInSession(_0x86cex5b) {
    sessionStorage['clear']();
    var _0x86cex5c = JSON['stringify'](_0x86cex5b);
    window['sessionStorage']['setItem']('userObject', _0x86cex5c)
}