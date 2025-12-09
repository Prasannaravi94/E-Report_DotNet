window['onload'] = function () {
    if (window['location']['href'] == sessionStorage['getItem']('userObject')) {
        sessionStorage['clear']()
    }
};
var ArrHQDet = {};
var dataTableSt;
var ColMonth = '';
var ArrSale = {};
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
    var _0x5dfex9 = $('#SS_DivCode')['val']();
    var _0x5dfexa = GetParameterValues('sfcode');
    var _0x5dfexb = GetParameterValues('Sf_Name');
    var _0x5dfexc = GetParameterValues('FMonth');
    var _0x5dfexd = GetParameterValues('FYear');
    var _0x5dfexe = GetParameterValues('TMonth');
    var _0x5dfexf = GetParameterValues('TYear');
    DDl_Opt = '2';
    var _0x5dfex10 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0x5dfex11 = parseInt(_0x5dfexc - 1);
    var _0x5dfex12 = parseInt(_0x5dfexe - 1);
    var _0x5dfex13 = _0x5dfex10[_0x5dfex11];
    var _0x5dfex14 = _0x5dfex10[_0x5dfex12];
    if (_0x5dfexc != _0x5dfexe) {
        ColMonth = _0x5dfex13 + '-' + _0x5dfex14
    } else {
        ColMonth = _0x5dfex13 + '-' + _0x5dfex14
    };
    var _0x5dfex15 = 'SubDivision Wise Sale Report From ' + _0x5dfex13 + ' ' + _0x5dfexd + ' to ' + ' ' + _0x5dfex14 + ' ' + ' ' + _0x5dfexf;
    var _0x5dfex16 = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0x5dfexb + '</span>';
    $('#lblStock')['html'](_0x5dfex15);
    $('#lblField')['html'](_0x5dfex16);
    var _0x5dfex17 = (parseInt(_0x5dfexf) - parseInt(_0x5dfexd)) * 12 + parseInt(_0x5dfexe) - parseInt(_0x5dfexc);
    var _0x5dfex18 = parseInt(_0x5dfexc);
    var _0x5dfex19 = parseInt(_0x5dfexd);
    if (_0x5dfex17 >= 0) {
        for (var _0x5dfex1a = 1; _0x5dfex1a <= _0x5dfex17 + 1; _0x5dfex1a++) {
            var _0x5dfex11 = parseInt(_0x5dfex18 - 1);
            var _0x5dfex13 = _0x5dfex10[_0x5dfex11];
            var _0x5dfex1b = _0x5dfex13;
            ArrMonth['push'](_0x5dfex1b);
            _0x5dfex18 = _0x5dfex18 + 1;
            if (_0x5dfex18 == 13) {
                _0x5dfex18 = 1;
                _0x5dfex19 = _0x5dfex19 + 1
            }
        }
    }
});

function GetHQ_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Parameter',
        data: '{}',
        dataType: 'json',
        success: function (_0x5dfex1d) {
            ArrHQDet = _0x5dfex1d['d'];
            GetSaleclose()
        },
        error: function (_0x5dfex1e) { }
    })
}

function GetSaleclose() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/GetSaleClose_Field',
        data: '{}',
        dataType: 'json',
        success: function (_0x5dfex1d) {
            ArrSale = _0x5dfex1d['d'];
            GetRecord_Det()
        },
        error: function (_0x5dfex1e) { }
    })
}

function GetRecord_Det() {
    var _0x5dfex21 = {};
    var _0x5dfex22 = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_SubDivision_wise_SaleAndClosing',
        data: '{}',
        dataType: 'json',
        success: function (_0x5dfex1d) {
            dataTableSt = _0x5dfex1d['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                var _0x5dfex23 = '<table id="tblSecsale" class="table" style="min-width:95%;max-width:100%">';
                _0x5dfex23 += '<tr>';
                _0x5dfex23 += '<th rowspan="3" >#</th>';
                _0x5dfex23 += '<th rowspan="3" >SubDiv Name</th>';
                var _0x5dfex24 = ArrSale['length'];
                var _0x5dfex25 = parseInt(_0x5dfex24) * 2;
                for (var _0x5dfex26 = 0; _0x5dfex26 < ArrMonth['length']; _0x5dfex26++) {
                    _0x5dfex23 += '<th colspan="' + _0x5dfex25 + '" >' + ArrMonth[_0x5dfex26] + '</th>'
                };
                _0x5dfex23 += '<th colspan="' + _0x5dfex25 + '" >Total</th>';
                _0x5dfex23 += '</tr>';
                _0x5dfex23 += '<tr>';
                for (var _0x5dfex26 = 0; _0x5dfex26 < ArrMonth['length']; _0x5dfex26++) {
                    for (var _0x5dfex27 = 0; _0x5dfex27 < ArrHQDet['length']; _0x5dfex27++) {
                        var _0x5dfex28 = ArrHQDet[_0x5dfex27]['Sec_Sale_Code'];
                        var _0x5dfex29 = '';
                        for (var _0x5dfex2a = 0; _0x5dfex2a < ArrSale['length']; _0x5dfex2a++) {
                            var _0x5dfex2b = ArrSale[_0x5dfex2a]['Sec_Sale_Code'];
                            if ($['trim'](_0x5dfex28) == $['trim'](_0x5dfex2b)) {
                                _0x5dfex29 = '2';
                                _0x5dfex23 += '<th   colspan="' + _0x5dfex29 + '" >' + ArrHQDet[_0x5dfex27]['Sec_Sale_Name'] + '</th>';
                                break
                            }
                        }
                    }
                };
                for (var _0x5dfex27 = 0; _0x5dfex27 < ArrHQDet['length']; _0x5dfex27++) {
                    var _0x5dfex28 = ArrHQDet[_0x5dfex27]['Sec_Sale_Code'];
                    var _0x5dfex29 = '';
                    for (var _0x5dfex2a = 0; _0x5dfex2a < ArrSale['length']; _0x5dfex2a++) {
                        var _0x5dfex2b = ArrSale[_0x5dfex2a]['Sec_Sale_Code'];
                        if ($['trim'](_0x5dfex28) == $['trim'](_0x5dfex2b)) {
                            _0x5dfex29 = '2';
                            _0x5dfex23 += '<th   colspan="' + _0x5dfex29 + '" >' + ArrHQDet[_0x5dfex27]['Sec_Sale_Name'] + '</th>';
                            break
                        }
                    }
                };
                _0x5dfex23 += '</tr>';
                _0x5dfex23 += '<tr>';
                for (var _0x5dfex26 = 0; _0x5dfex26 < ArrMonth['length']; _0x5dfex26++) {
                    for (var _0x5dfex27 = 0; _0x5dfex27 < ArrHQDet['length']; _0x5dfex27++) {
                        var _0x5dfex28 = ArrHQDet[_0x5dfex27]['Sec_Sale_Code'];
                        var _0x5dfex2c = false;
                        for (var _0x5dfex2d = 0; _0x5dfex2d < ArrSale['length'] && !_0x5dfex2c; _0x5dfex2d++) {
                            if (ArrSale[_0x5dfex2d]['Sec_Sale_Code'] === _0x5dfex28) {
                                _0x5dfex2c = true;
                                break
                            }
                        };
                        if (_0x5dfex2c == true) {
                            _0x5dfex23 += '<th >Qty </th>';
                            _0x5dfex23 += '<th >Value</th>'
                        }
                    }
                };
                for (var _0x5dfex27 = 0; _0x5dfex27 < ArrHQDet['length']; _0x5dfex27++) {
                    var _0x5dfex28 = ArrHQDet[_0x5dfex27]['Sec_Sale_Code'];
                    var _0x5dfex2c = false;
                    for (var _0x5dfex2d = 0; _0x5dfex2d < ArrSale['length'] && !_0x5dfex2c; _0x5dfex2d++) {
                        if (ArrSale[_0x5dfex2d]['Sec_Sale_Code'] === _0x5dfex28) {
                            _0x5dfex2c = true;
                            break
                        }
                    };
                    if (_0x5dfex2c == true) {
                        _0x5dfex23 += '<th >Qty </th>';
                        _0x5dfex23 += '<th >Value</th>'
                    }
                };
                _0x5dfex23 += '</tr>';
                for (var _0x5dfex1a = 0; _0x5dfex1a < dataTableSt['length']; _0x5dfex1a++) {
                    var _0x5dfex2e = dataTableSt[_0x5dfex1a];
                    var _0x5dfex2f = 0;
                    _0x5dfex23 += '<tr>';
                    $['each'](_0x5dfex2e, function (_0x5dfex30, _0x5dfex31) {
                        var _0x5dfex32 = _0x5dfex30;
                        if (_0x5dfex31 == null || _0x5dfex31 == '0') {
                            _0x5dfex31 = ''
                        };
                        if (_0x5dfex32 == 'R_Id' || _0x5dfex32 == 'SubDiv_Name') {
                            _0x5dfex23 += '<td>' + _0x5dfex31 + '</td>'
                        } else {
                            if (_0x5dfex32['includes']('_ABT') || _0x5dfex32['includes']('_ACT')) {
                                var _0x5dfex33 = _0x5dfex32['split']('_');
                                var _0x5dfex34 = _0x5dfex33[1];
                                var _0x5dfex35 = _0x5dfex33[0];
                                if (_0x5dfex32['includes']('_ACT')) {
                                    if (_0x5dfex31 != '') {
                                        _0x5dfex31 = parseFloat(_0x5dfex31)['toFixed'](2)
                                    }
                                };
                                if (_0x5dfex1a == dataTableSt['length'] - 1) {
                                    if (_0x5dfex31 != '') {
                                        _0x5dfex31 = parseFloat(_0x5dfex31)['toFixed'](2)
                                    };
                                    _0x5dfex23 += '<td>' + _0x5dfex31 + '</td>'
                                } else {
                                    _0x5dfex23 += '<td>' + _0x5dfex31 + '</td>'
                                }
                            } else {
                                if (_0x5dfex32 == 'T_SaleQty' || _0x5dfex32 == 'T_SaleVal' || _0x5dfex32 == 'T_ClsQty' || _0x5dfex32 == 'T_ClsVal') {
                                    if (_0x5dfex32 == 'T_SaleVal' || _0x5dfex32 == 'T_ClsVal') {
                                        if (_0x5dfex31 != '') {
                                            _0x5dfex31 = parseFloat(_0x5dfex31)['toFixed'](2);
                                            if (_0x5dfex1a == dataTableSt['length'] - 1) {
                                                if (_0x5dfex32 == 'T_SaleVal') {
                                                    Active_Val['push'](_0x5dfex31)
                                                } else {
                                                    if (_0x5dfex32 == 'T_ClsVal') {
                                                        Active_Val['push'](_0x5dfex31)
                                                    }
                                                }
                                            }
                                        }
                                    };
                                    _0x5dfex23 += '<td style="color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;"; >' + _0x5dfex31 + '</td>'
                                }
                            }
                        }
                    });
                    _0x5dfex23 += '</tr>'
                };
                _0x5dfex23 += '</table>';
                $('#divpnl')['append'](_0x5dfex23);
                TotalBg();
                TotalDeActivate();
                mergeCells(1)
            };
            $('.modal')['hide']()
        },
        error: function (_0x5dfex1e) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    $('#tblSecsale tr:last td')['each'](function () {
        var _0x5dfex37 = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#F1F5F8;')
    })
}

function GetParameterValues(_0x5dfex39) {
    var _0x5dfex3a = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0x5dfex2d = 0; _0x5dfex2d < _0x5dfex3a['length']; _0x5dfex2d++) {
        var _0x5dfex3b = _0x5dfex3a[_0x5dfex2d]['split']('=');
        if (_0x5dfex3b[0] == _0x5dfex39) {
            return decodeURIComponent(_0x5dfex3b[1])
        }
    }
}

function mergeCells(_0x5dfex3d) {
    $('.modal')['hide']();
    var _0x5dfex3e = document['getElementsByTagName']('table')[0];
    var _0x5dfex3f = _0x5dfex3e['rows'][1];
    var _0x5dfex40 = _0x5dfex3f['nextSibling'];
    while (true) {
        if (_0x5dfex40['nodeType'] == 3) {
            break
        };
        if (_0x5dfex3f['cells'][_0x5dfex3d]['innerHTML'] == _0x5dfex40['cells'][_0x5dfex3d]['innerHTML']) {
            _0x5dfex3f['cells'][_0x5dfex3d]['rowSpan'] = 1 + parseInt(_0x5dfex3f['cells'][_0x5dfex3d]['rowSpan']);
            _0x5dfex40['cells'][_0x5dfex3d]['style']['display'] = 'none'
        } else {
            _0x5dfex3f = _0x5dfex40
        };
        _0x5dfex40 = _0x5dfex40['nextSibling']
    }
}

function TotalDeActivate() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0x5dfex1d) {
            dataTableSt = _0x5dfex1d['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0x5dfex1a = dataTableSt['length'] - 1;
                var _0x5dfex2e = dataTableSt[_0x5dfex1a];
                var _0x5dfex23 = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0x5dfex23 += '<tr>';
                _0x5dfex23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x5dfex23 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0x5dfex23 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Closing</th>';
                _0x5dfex23 += '</tr>';
                _0x5dfex23 += '<tr>';
                _0x5dfex23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x5dfex23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x5dfex23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x5dfex23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x5dfex23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x5dfex23 += '</tr>';
                _0x5dfex23 += '<tr>';
                _0x5dfex23 += '<td style=background-color:#d4d4d4;>';
                _0x5dfex23 += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0x5dfex23 += '</td>';
                var _0x5dfex42 = 0;
                var _0x5dfex43 = 0;
                var _0x5dfex44 = 0;
                $['each'](_0x5dfex2e, function (_0x5dfex30, _0x5dfex31) {
                    var _0x5dfex32 = _0x5dfex30;
                    if (_0x5dfex31 == null || _0x5dfex31 == '0') {
                        _0x5dfex31 = ''
                    };
                    if (_0x5dfex32['includes']('_ABT') || _0x5dfex32['includes']('_ACT')) {
                        var _0x5dfex45 = parseFloat(_0x5dfex31)['toFixed'](2);
                        DeAct_Val = _0x5dfex45;
                        if (_0x5dfex43 == 0) {
                            DeAct_Val = _0x5dfex45;
                            _0x5dfex42 = parseFloat(Active_Val[0]) + parseFloat(DeAct_Val);
                            _0x5dfex23 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[0])['toFixed'](2) + '</td>';
                            _0x5dfex23 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        } else {
                            DeAct_Val = _0x5dfex45;
                            _0x5dfex44 = parseFloat(Active_Val[1]) + parseFloat(DeAct_Val);
                            _0x5dfex23 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[1])['toFixed'](2) + '</td>';
                            _0x5dfex23 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        };
                        _0x5dfex43 += 1
                    }
                });
                _0x5dfex23 += '</tr>';
                _0x5dfex23 += '<tr>';
                _0x5dfex23 += '<td style=background-color:#d4d4d4;>';
                _0x5dfex23 += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0x5dfex23 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x5dfex42 + '</span></td>';
                _0x5dfex23 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x5dfex44 + '</span></td>';
                _0x5dfex23 += '</td>';
                _0x5dfex23 += '</tr>';
                _0x5dfex23 += '</table>';
                $('#div_DeAct')['append'](_0x5dfex23)
            }
        },
        error: function (_0x5dfex1e) {
            $('.modal')['hide']()
        }
    })
}

function storeUserDataInSession(_0x5dfex47) {
    sessionStorage['clear']();
    var _0x5dfex48 = JSON['stringify'](_0x5dfex47);
    window['sessionStorage']['setItem']('userObject', _0x5dfex48)
}