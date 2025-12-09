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
    var _0xd2ffx9 = $('#SS_DivCode')['val']();
    var _0xd2ffxa = GetParameterValues('sfcode');
    var _0xd2ffxb = GetParameterValues('Sf_Name');
    var _0xd2ffxc = GetParameterValues('FMonth');
    var _0xd2ffxd = GetParameterValues('FYear');
    var _0xd2ffxe = GetParameterValues('TMonth');
    var _0xd2ffxf = GetParameterValues('TYear');
    DDl_Opt = '2';
    var _0xd2ffx10 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0xd2ffx11 = parseInt(_0xd2ffxc - 1);
    var _0xd2ffx12 = parseInt(_0xd2ffxe - 1);
    var _0xd2ffx13 = _0xd2ffx10[_0xd2ffx11];
    var _0xd2ffx14 = _0xd2ffx10[_0xd2ffx12];
    if (_0xd2ffxc != _0xd2ffxe) {
        ColMonth = _0xd2ffx13 + '-' + _0xd2ffx14
    } else {
        ColMonth = _0xd2ffx13 + '-' + _0xd2ffx14
    };
    var _0xd2ffx15 = 'HQ - Stockist wise (Sale & CB) From ' + _0xd2ffx13 + ' ' + _0xd2ffxd + ' to ' + ' ' + _0xd2ffx14 + ' ' + ' ' + _0xd2ffxf;
    var _0xd2ffx16 = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0xd2ffxb + '</span>';
    $('#lblStock')['html'](_0xd2ffx15);
    $('#lblField')['html'](_0xd2ffx16);
    var _0xd2ffx17 = (parseInt(_0xd2ffxf) - parseInt(_0xd2ffxd)) * 12 + parseInt(_0xd2ffxe) - parseInt(_0xd2ffxc);
    var _0xd2ffx18 = parseInt(_0xd2ffxc);
    var _0xd2ffx19 = parseInt(_0xd2ffxd);
    if (_0xd2ffx17 >= 0) {
        for (var _0xd2ffx1a = 1; _0xd2ffx1a <= _0xd2ffx17 + 1; _0xd2ffx1a++) {
            var _0xd2ffx11 = parseInt(_0xd2ffx18 - 1);
            var _0xd2ffx13 = _0xd2ffx10[_0xd2ffx11];
            var _0xd2ffx1b = _0xd2ffx13;
            ArrMonth['push'](_0xd2ffx1b);
            _0xd2ffx18 = _0xd2ffx18 + 1;
            if (_0xd2ffx18 == 13) {
                _0xd2ffx18 = 1;
                _0xd2ffx19 = _0xd2ffx19 + 1
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
        success: function (_0xd2ffx1d) {
            ArrHQDet = _0xd2ffx1d['d'];
            GetSaleclose()
        },
        error: function (_0xd2ffx1e) { }
    })
}

function GetSaleclose() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/GetSaleClose_Field',
        data: '{}',
        dataType: 'json',
        success: function (_0xd2ffx1d) {
            ArrSale = _0xd2ffx1d['d'];
            GetRecord_Det()
        },
        error: function (_0xd2ffx1e) { }
    })
}

function GetRecord_Det() {
    var _0xd2ffx21 = {};
    var _0xd2ffx22 = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_HQwise_Stockist_SaleAndClosing',
        data: '{}',
        dataType: 'json',
        success: function (_0xd2ffx1d) {
            dataTableSt = _0xd2ffx1d['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                var _0xd2ffx23 = '<table id="tblSecsale" class="table" style="min-width:95%;max-width:100%">';
                _0xd2ffx23 += '<tr>';
                _0xd2ffx23 += '<th rowspan="3" >#</th>';
                _0xd2ffx23 += '<th rowspan="3" >HQ</th>';
                _0xd2ffx23 += '<th rowspan="3" >Stockist Name</th>';
                var _0xd2ffx24 = ArrSale['length'];
                var _0xd2ffx25 = parseInt(_0xd2ffx24) * 2;
                for (var _0xd2ffx26 = 0; _0xd2ffx26 < ArrMonth['length']; _0xd2ffx26++) {
                    _0xd2ffx23 += '<th colspan="' + _0xd2ffx25 + '" >' + ArrMonth[_0xd2ffx26] + '</th>'
                };
                _0xd2ffx23 += '<th colspan="' + _0xd2ffx25 + '" >Total</th>';
                _0xd2ffx23 += '</tr>';
                _0xd2ffx23 += '<tr>';
                for (var _0xd2ffx26 = 0; _0xd2ffx26 < ArrMonth['length']; _0xd2ffx26++) {
                    for (var _0xd2ffx27 = 0; _0xd2ffx27 < ArrHQDet['length']; _0xd2ffx27++) {
                        var _0xd2ffx28 = ArrHQDet[_0xd2ffx27]['Sec_Sale_Code'];
                        var _0xd2ffx29 = '';
                        for (var _0xd2ffx2a = 0; _0xd2ffx2a < ArrSale['length']; _0xd2ffx2a++) {
                            var _0xd2ffx2b = ArrSale[_0xd2ffx2a]['Sec_Sale_Code'];
                            if ($['trim'](_0xd2ffx28) == $['trim'](_0xd2ffx2b)) {
                                _0xd2ffx29 = '2';
                                _0xd2ffx23 += '<th   colspan="' + _0xd2ffx29 + '" >' + ArrHQDet[_0xd2ffx27]['Sec_Sale_Name'] + '</th>';
                                break
                            }
                        }
                    }
                };
                for (var _0xd2ffx27 = 0; _0xd2ffx27 < ArrHQDet['length']; _0xd2ffx27++) {
                    var _0xd2ffx28 = ArrHQDet[_0xd2ffx27]['Sec_Sale_Code'];
                    var _0xd2ffx29 = '';
                    for (var _0xd2ffx2a = 0; _0xd2ffx2a < ArrSale['length']; _0xd2ffx2a++) {
                        var _0xd2ffx2b = ArrSale[_0xd2ffx2a]['Sec_Sale_Code'];
                        if ($['trim'](_0xd2ffx28) == $['trim'](_0xd2ffx2b)) {
                            _0xd2ffx29 = '2';
                            _0xd2ffx23 += '<th   colspan="' + _0xd2ffx29 + '" >' + ArrHQDet[_0xd2ffx27]['Sec_Sale_Name'] + '</th>';
                            break
                        }
                    }
                };
                _0xd2ffx23 += '</tr>';
                _0xd2ffx23 += '<tr>';
                for (var _0xd2ffx26 = 0; _0xd2ffx26 < ArrMonth['length']; _0xd2ffx26++) {
                    for (var _0xd2ffx27 = 0; _0xd2ffx27 < ArrHQDet['length']; _0xd2ffx27++) {
                        var _0xd2ffx28 = ArrHQDet[_0xd2ffx27]['Sec_Sale_Code'];
                        var _0xd2ffx2c = false;
                        for (var _0xd2ffx2d = 0; _0xd2ffx2d < ArrSale['length'] && !_0xd2ffx2c; _0xd2ffx2d++) {
                            if (ArrSale[_0xd2ffx2d]['Sec_Sale_Code'] === _0xd2ffx28) {
                                _0xd2ffx2c = true;
                                break
                            }
                        };
                        if (_0xd2ffx2c == true) {
                            _0xd2ffx23 += '<th >Qty </th>';
                            _0xd2ffx23 += '<th >Value</th>'
                        }
                    }
                };
                for (var _0xd2ffx27 = 0; _0xd2ffx27 < ArrHQDet['length']; _0xd2ffx27++) {
                    var _0xd2ffx28 = ArrHQDet[_0xd2ffx27]['Sec_Sale_Code'];
                    var _0xd2ffx2c = false;
                    for (var _0xd2ffx2d = 0; _0xd2ffx2d < ArrSale['length'] && !_0xd2ffx2c; _0xd2ffx2d++) {
                        if (ArrSale[_0xd2ffx2d]['Sec_Sale_Code'] === _0xd2ffx28) {
                            _0xd2ffx2c = true;
                            break
                        }
                    };
                    if (_0xd2ffx2c == true) {
                        _0xd2ffx23 += '<th >Qty </th>';
                        _0xd2ffx23 += '<th >Value</th>'
                    }
                };
                _0xd2ffx23 += '</tr>';
                for (var _0xd2ffx1a = 0; _0xd2ffx1a < dataTableSt['length']; _0xd2ffx1a++) {
                    var _0xd2ffx2e = dataTableSt[_0xd2ffx1a];
                    var _0xd2ffx2f = 0;
                    var _0xd2ffx30 = 0;
                    _0xd2ffx23 += '<tr>';
                    $['each'](_0xd2ffx2e, function (_0xd2ffx31, _0xd2ffx32) {
                        var _0xd2ffx33 = _0xd2ffx31;
                        if (_0xd2ffx32 == null || _0xd2ffx32 == '0') {
                            _0xd2ffx32 = ''
                        };
                        if (_0xd2ffx33 == 'R_Id' || _0xd2ffx33 == 'HQ' || _0xd2ffx33 == 'Stockist_Name') {
                            if (_0xd2ffx33 == 'Stockist_Name') {
                                if (_0xd2ffx32 != '' && _0xd2ffx32 == 'Total') {
                                    _0xd2ffx32 = '';
                                    _0xd2ffx23 += '<td style=color:#000;font-size:12px;font-weight:bold;background-color:#f1f5f8;>' + _0xd2ffx32 + '</td>'
                                } else {
                                    _0xd2ffx23 += '<td>' + _0xd2ffx32 + '</td>'
                                }
                            } else {
                                if (_0xd2ffx33 == 'HQ') {
                                    if (_0xd2ffx32 != '' && _0xd2ffx32['includes']('_Subtotal')) {
                                        _0xd2ffx32 = 'Total';
                                        _0xd2ffx23 += '<td style=color:#000;font-size:12px;font-weight:bold;background-color:#f1f5f8;>Total</td>';
                                        _0xd2ffx30 = 1
                                    } else {
                                        if (_0xd2ffx32 == 'Grand Total') {
                                            _0xd2ffx23 += '<td style="color:Black;font-weight:bold">' + _0xd2ffx32 + '</td>'
                                        } else {
                                            if (_0xd2ffx30 == 1) {
                                                _0xd2ffx23 += '<td style=color:#000;font-size:12px;font-weight:bold;background-color:#f1f5f8;>' + _0xd2ffx32 + '</td>'
                                            } else {
                                                _0xd2ffx23 += '<td>' + _0xd2ffx32 + '</td>'
                                            }
                                        }
                                    }
                                } else {
                                    if (_0xd2ffx33 == 'R_Id') {
                                        _0xd2ffx23 += '<td>' + _0xd2ffx32 + '</td>'
                                    }
                                }
                            }
                        } else {
                            if (_0xd2ffx33['includes']('_ABT') || _0xd2ffx33['includes']('_ACT')) {
                                var _0xd2ffx34 = _0xd2ffx33['split']('_');
                                var _0xd2ffx35 = _0xd2ffx34[1];
                                var _0xd2ffx36 = _0xd2ffx34[0];
                                if (_0xd2ffx33['includes']('_ACT')) {
                                    if (_0xd2ffx32 != '') {
                                        _0xd2ffx32 = parseFloat(_0xd2ffx32)['toFixed'](2)
                                    }
                                };
                                if (_0xd2ffx1a == dataTableSt['length'] - 1) {
                                    if (_0xd2ffx32 != '') {
                                        _0xd2ffx32 = parseFloat(_0xd2ffx32)['toFixed'](2)
                                    };
                                    if (_0xd2ffx30 == 1) {
                                        _0xd2ffx23 += '<td style=color:#000;font-size:12px;font-weight:bold;background-color:#f1f5f8;>' + _0xd2ffx32 + '</td>'
                                    } else {
                                        _0xd2ffx23 += '<td>' + _0xd2ffx32 + '</td>'
                                    }
                                } else {
                                    if (_0xd2ffx30 == 1) {
                                        _0xd2ffx23 += '<td style=color:#000;font-size:12px;font-weight:bold;background-color:#f1f5f8;>' + _0xd2ffx32 + '</td>'
                                    } else {
                                        _0xd2ffx23 += '<td>' + _0xd2ffx32 + '</td>'
                                    }
                                }
                            } else {
                                if (_0xd2ffx33 == 'T_SaleQty' || _0xd2ffx33 == 'T_SaleVal' || _0xd2ffx33 == 'T_ClsQty' || _0xd2ffx33 == 'T_ClsVal') {
                                    if (_0xd2ffx33 == 'T_SaleVal' || _0xd2ffx33 == 'T_ClsVal') {
                                        if (_0xd2ffx32 != '') {
                                            _0xd2ffx32 = parseFloat(_0xd2ffx32)['toFixed'](2);
                                            if (_0xd2ffx1a == dataTableSt['length'] - 1) {
                                                if (_0xd2ffx33 == 'T_SaleVal') {
                                                    Active_Val['push'](_0xd2ffx32)
                                                } else {
                                                    if (_0xd2ffx33 == 'T_ClsVal') {
                                                        Active_Val['push'](_0xd2ffx32)
                                                    }
                                                }
                                            }
                                        }
                                    };
                                    if (_0xd2ffx30 == 1) {
                                        _0xd2ffx23 += '<td style=color:#000;font-size:12px;font-weight:bold;background-color:#f1f5f8;>' + _0xd2ffx32 + '</td>'
                                    } else {
                                        _0xd2ffx23 += '<td style="color:#DC143C;font-size:12px;font-weight:bold;background-color:#f1f5f8;"; >' + _0xd2ffx32 + '</td>'
                                    }
                                }
                            }
                        }
                    });
                    _0xd2ffx23 += '</tr>'
                };
                _0xd2ffx23 += '</table>';
                $('#divpnl')['append'](_0xd2ffx23);
                TotalBg();
                TotalDeActivate_Both();
                mergeCells(1)
            };
            $('.modal')['hide']()
        },
        error: function (_0xd2ffx1e) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    $('#tblSecsale tr:last td')['each'](function () {
        var _0xd2ffx38 = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#f1f5f8;')
    })
}

function GetParameterValues(_0xd2ffx3a) {
    var _0xd2ffx3b = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0xd2ffx2d = 0; _0xd2ffx2d < _0xd2ffx3b['length']; _0xd2ffx2d++) {
        var _0xd2ffx3c = _0xd2ffx3b[_0xd2ffx2d]['split']('=');
        if (_0xd2ffx3c[0] == _0xd2ffx3a) {
            return decodeURIComponent(_0xd2ffx3c[1])
        }
    }
}

function mergeCells(_0xd2ffx3e) {
    $('.modal')['hide']();
    var _0xd2ffx3f = document['getElementsByTagName']('table')[0];
    var _0xd2ffx40 = _0xd2ffx3f['rows'][1];
    var _0xd2ffx41 = _0xd2ffx40['nextSibling'];
    while (true) {
        if (_0xd2ffx41['nodeType'] == 3) {
            break
        };
        if (_0xd2ffx40['cells'][_0xd2ffx3e]['innerHTML'] == _0xd2ffx41['cells'][_0xd2ffx3e]['innerHTML']) {
            _0xd2ffx40['cells'][_0xd2ffx3e]['rowSpan'] = 1 + parseInt(_0xd2ffx40['cells'][_0xd2ffx3e]['rowSpan']);
            _0xd2ffx41['cells'][_0xd2ffx3e]['style']['display'] = 'none'
        } else {
            _0xd2ffx40 = _0xd2ffx41
        };
        _0xd2ffx41 = _0xd2ffx41['nextSibling']
    }
}

function storeUserDataInSession(_0xd2ffx43) {
    sessionStorage['removeItem']('userObject');
    var _0xd2ffx44 = JSON['stringify'](_0xd2ffx43);
    window['sessionStorage']['setItem']('userObject', _0xd2ffx44)
}

function TotalDeActivate_Both() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0xd2ffx1d) {
            dataTableSt = _0xd2ffx1d['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0xd2ffx1a = dataTableSt['length'] - 1;
                var _0xd2ffx2e = dataTableSt[_0xd2ffx1a];
                var _0xd2ffx23 = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0xd2ffx23 += '<tr>';
                _0xd2ffx23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0xd2ffx23 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0xd2ffx23 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Closing</th>';
                _0xd2ffx23 += '</tr>';
                _0xd2ffx23 += '<tr>';
                _0xd2ffx23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0xd2ffx23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0xd2ffx23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0xd2ffx23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0xd2ffx23 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0xd2ffx23 += '</tr>';
                _0xd2ffx23 += '<tr>';
                _0xd2ffx23 += '<td style=background-color:#d4d4d4;>';
                _0xd2ffx23 += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0xd2ffx23 += '</td>';
                var _0xd2ffx46 = 0;
                var _0xd2ffx30 = 0;
                var _0xd2ffx47 = 0;
                $['each'](_0xd2ffx2e, function (_0xd2ffx31, _0xd2ffx32) {
                    var _0xd2ffx33 = _0xd2ffx31;
                    if (_0xd2ffx32 == null || _0xd2ffx32 == '0') {
                        _0xd2ffx32 = ''
                    };
                    if (_0xd2ffx33['includes']('_ABT') || _0xd2ffx33['includes']('_ACT')) {
                        var _0xd2ffx48 = parseFloat(_0xd2ffx32)['toFixed'](2);
                        DeAct_Val = _0xd2ffx48;
                        if (_0xd2ffx30 == 0) {
                            DeAct_Val = _0xd2ffx48;
                            _0xd2ffx46 = parseFloat(Active_Val[0]) + parseFloat(DeAct_Val);
                            _0xd2ffx23 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[0]) + '</td>';
                            _0xd2ffx23 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        } else {
                            DeAct_Val = _0xd2ffx48;
                            _0xd2ffx47 = parseFloat(Active_Val[1]) + parseFloat(DeAct_Val);
                            _0xd2ffx23 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[1]) + '</td>';
                            _0xd2ffx23 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        };
                        _0xd2ffx30 += 1
                    }
                });
                _0xd2ffx23 += '</tr>';
                _0xd2ffx23 += '<tr>';
                _0xd2ffx23 += '<td style=background-color:#d4d4d4;>';
                _0xd2ffx23 += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0xd2ffx23 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0xd2ffx46 + '</span></td>';
                _0xd2ffx23 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0xd2ffx47 + '</span></td>';
                _0xd2ffx23 += '</td>';
                _0xd2ffx23 += '</tr>';
                _0xd2ffx23 += '</table>';
                $('#div_DeAct')['append'](_0xd2ffx23)
            }
        },
        error: function (_0xd2ffx1e) {
            $('.modal')['hide']()
        }
    })
}