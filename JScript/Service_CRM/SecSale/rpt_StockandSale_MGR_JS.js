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
var FMonth = '';
var FYear = '';
var TMonth = '';
var TYear = '';
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
    var _0x87efxd = $('#SS_DivCode')['val']();
    var _0x87efxe = GetParameterValues('sfcode');
    var _0x87efxf = GetParameterValues('Sf_Name');
    FMonth = GetParameterValues('FMonth');
    FYear = GetParameterValues('FYear');
    TMonth = GetParameterValues('TMonth');
    TYear = GetParameterValues('TYear');
    DDl_Opt = '2';
    var _0x87efx10 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0x87efx11 = parseInt(FMonth - 1);
    var _0x87efx12 = parseInt(TMonth - 1);
    var _0x87efx13 = _0x87efx10[_0x87efx11];
    var _0x87efx14 = _0x87efx10[_0x87efx12];
    if (FMonth != TMonth) {
        ColMonth = _0x87efx13 + '-' + _0x87efx14
    } else {
        ColMonth = _0x87efx13 + '-' + _0x87efx14
    };
    var _0x87efx15 = 'Manager wise (Stock & Sale) Statement for ' + _0x87efx13 + ' ' + FYear + ' to ' + ' ' + _0x87efx14 + ' ' + ' ' + TYear;
    var _0x87efx16 = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0x87efxf + '</span>';
    $('#lblStock')['html'](_0x87efx15);
    $('#lblField')['html'](_0x87efx16);
    var _0x87efx17 = (parseInt(TYear) - parseInt(FYear)) * 12 + parseInt(TMonth) - parseInt(FMonth);
    var _0x87efx18 = parseInt(FMonth);
    var _0x87efx19 = parseInt(FYear);
    if (_0x87efx17 >= 0) {
        for (var _0x87efx1a = 1; _0x87efx1a <= _0x87efx17 + 1; _0x87efx1a++) {
            var _0x87efx11 = parseInt(_0x87efx18 - 1);
            var _0x87efx13 = _0x87efx10[_0x87efx11];
            var _0x87efx1b = _0x87efx13;
            ArrMonth['push'](_0x87efx1b);
            _0x87efx18 = _0x87efx18 + 1;
            if (_0x87efx18 == 13) {
                _0x87efx18 = 1;
                _0x87efx19 = _0x87efx19 + 1
            }
        }
    }
});

function GetRecords() {
    var _0x87efx1d = {};
    var _0x87efx1e = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Mgr_StockandSale',
        data: '{}',
        dataType: 'json',
        success: function (_0x87efx1f) {
            dataTableSt = _0x87efx1f['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                var _0x87efx20 = '<table id="tblMGR" class="table" style="min-width:95%;max-width:100%">';
                _0x87efx20 += '<tr>';
                _0x87efx20 += '<th rowspan="3">#</th>';
                _0x87efx20 += '<th rowspan="3" >MGR Name</th>';
                _0x87efx20 += '<th rowspan="3" >HQ</th>';
                _0x87efx20 += '<th rowspan="3" >Designation</th>';
                var _0x87efx21 = ArrHQDet['length'];
                var _0x87efx22 = parseInt(_0x87efx21) + 2;
                for (var _0x87efx23 = 0; _0x87efx23 < ArrMonth['length']; _0x87efx23++) {
                    _0x87efx20 += '<th colspan="' + _0x87efx22 + '" >' + ArrMonth[_0x87efx23] + '</th>'
                };
                _0x87efx20 += '</tr>';
                _0x87efx20 += '<tr>';
                for (var _0x87efx23 = 0; _0x87efx23 < ArrMonth['length']; _0x87efx23++) {
                    for (var _0x87efx24 = 0; _0x87efx24 < ArrHQDet['length']; _0x87efx24++) {
                        var _0x87efx25 = ArrHQDet[_0x87efx24]['Sec_Sale_Code'];
                        var _0x87efx26 = '';
                        for (var _0x87efx27 = 0; _0x87efx27 < ArrSale['length']; _0x87efx27++) {
                            var _0x87efx28 = ArrSale[_0x87efx27]['Sec_Sale_Code'];
                            if ($['trim'](_0x87efx25) == $['trim'](_0x87efx28)) {
                                _0x87efx26 = '2';
                                break
                            } else {
                                _0x87efx26 = '1'
                            }
                        };
                        _0x87efx20 += '<th   colspan="' + _0x87efx26 + '">' + ArrHQDet[_0x87efx24]['Sec_Sale_Name'] + '</th>'
                    }
                };
                _0x87efx20 += '</tr>';
                _0x87efx20 += '<tr>';
                for (var _0x87efx23 = 0; _0x87efx23 < ArrMonth['length']; _0x87efx23++) {
                    for (var _0x87efx24 = 0; _0x87efx24 < ArrHQDet['length']; _0x87efx24++) {
                        var _0x87efx25 = ArrHQDet[_0x87efx24]['Sec_Sale_Code'];
                        var _0x87efx29 = false;
                        for (var _0x87efx2a = 0; _0x87efx2a < ArrSale['length'] && !_0x87efx29; _0x87efx2a++) {
                            if (ArrSale[_0x87efx2a]['Sec_Sale_Code'] === _0x87efx25) {
                                _0x87efx29 = true;
                                break
                            }
                        };
                        if (_0x87efx29 == true) {
                            _0x87efx20 += '<th >Qty Val</th>';
                            _0x87efx20 += '<th >Free Val</th>'
                        } else {
                            _0x87efx20 += '<th >Qty Val</th>'
                        }
                    }
                };
                _0x87efx20 += '</tr>';
                for (var _0x87efx1a = 0; _0x87efx1a < dataTableSt['length']; _0x87efx1a++) {
                    var _0x87efx2b = dataTableSt[_0x87efx1a];
                    var _0x87efx2c = 0;
                    var _0x87efx2d = '';
                    var _0x87efx2e = '';
                    var _0x87efx2f = '';
                    _0x87efx2e = _0x87efx2b['desg'];
                    _0x87efx2e = _0x87efx2e['replace'](/ /g, '_');
                    _0x87efx2f = _0x87efx2b['hq'];
                    _0x87efx2f = _0x87efx2f['replace'](/ /g, '_');
                    _0x87efx20 += '<tr>';
                    $['each'](_0x87efx2b, function (_0x87efx30, _0x87efx31) {
                        var _0x87efx32 = _0x87efx30;
                        if (_0x87efx31 == null || _0x87efx31 == '0') {
                            _0x87efx31 = ''
                        };
                        if (_0x87efx32 == 'sf_code') {
                            _0x87efx2d = _0x87efx31
                        };
                        if (_0x87efx32 == 'INX' || _0x87efx32 == 'sf_name' || _0x87efx32 == 'desg' || _0x87efx32 == 'hq') {
                            if (_0x87efx32 == 'sf_name') {
                                var _0x87efx33 = _0x87efx31['replace'](/ /g, '_');
                                if (_0x87efx31 != 'Grand Total') {
                                    var _0x87efx34 = 'rpt_SecSale_StockSale_MR_wise_Report.aspx?sfcode=' + _0x87efx2d + '&Sf_Name=' + _0x87efx33 + '&FMonth=' + FMonth + '&FYear=' + FYear + '&TMonth=' + TMonth + '&TYear=' + TYear + '&Design=' + _0x87efx2e + '&HQ=' + _0x87efx2f + '';
                                    _0x87efx20 += '<td><div class="tooltip"><a class="mrlnk" href="#" onclick=javascript:window.open("' + _0x87efx34 + '",null,"") >' + _0x87efx31 + '</a><span class="tooltiptext">Click Hear to View </br> MR wise Stockists</span></div></td>'
                                } else {
                                    _0x87efx20 += '<td>' + _0x87efx31 + '</td>'
                                }
                            } else {
                                _0x87efx20 += '<td>' + _0x87efx31 + '</td>'
                            }
                        } else {
                            if (_0x87efx32['includes']('_ABT') || _0x87efx32['includes']('_ACT')) {
                                var _0x87efx35 = _0x87efx32['split']('_');
                                var _0x87efx36 = _0x87efx35[1];
                                var _0x87efx37 = _0x87efx35[0];
                                if (_0x87efx32['includes']('_ABT') || _0x87efx32['includes']('_ACT')) {
                                    if (_0x87efx31 != '') {
                                        _0x87efx31 = parseFloat(_0x87efx31)['toFixed'](2)
                                    }
                                };
                                if (_0x87efx1a == dataTableSt['length'] - 1) {
                                    if (_0x87efx31 != '') {
                                        _0x87efx31 = parseFloat(_0x87efx31)['toFixed'](2)
                                    };
                                    _0x87efx20 += '<td>' + _0x87efx31 + '</td>'
                                } else {
                                    _0x87efx20 += '<td>' + _0x87efx31 + '</td>'
                                }
                            } else {
                                if (_0x87efx32 == 'T_SaleVal' || _0x87efx32 == 'T_ClsVal') {
                                    if (_0x87efx31 != '') {
                                        _0x87efx31 = parseFloat(_0x87efx31)['toFixed'](2);
                                        if (_0x87efx1a == dataTableSt['length'] - 1) {
                                            if (_0x87efx32 == 'T_SaleVal') {
                                                Active_Val['push'](_0x87efx31)
                                            } else {
                                                if (_0x87efx32 == 'T_ClsVal') {
                                                    Active_Val['push'](_0x87efx31)
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    });
                    _0x87efx20 += '</tr>'
                };
                _0x87efx20 += '</table>';
                $('#divpnl')['append'](_0x87efx20);
                TotalBg();
                TotalDeActivate()
            };
            $('.modal')['hide']()
        },
        error: function (_0x87efx38) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    $('#tblMGR tr:last td')['each'](function () {
        var _0x87efx3a = $(this)['html']();
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
        success: function (_0x87efx1f) {
            ArrHQDet = _0x87efx1f['d'];
            GetSaleclose()
        },
        error: function (_0x87efx38) { }
    })
}

function GetSaleclose() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/GetSaleClose_Field',
        data: '{}',
        dataType: 'json',
        success: function (_0x87efx1f) {
            ArrSale = _0x87efx1f['d'];
            GetRecords()
        },
        error: function (_0x87efx38) { }
    })
}

function GetParameterValues(_0x87efx3e) {
    var _0x87efx3f = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0x87efx2a = 0; _0x87efx2a < _0x87efx3f['length']; _0x87efx2a++) {
        var _0x87efx40 = _0x87efx3f[_0x87efx2a]['split']('=');
        if (_0x87efx40[0] == _0x87efx3e) {
            return decodeURIComponent(_0x87efx40[1])
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
        success: function (_0x87efx1f) {
            dataTableSt = _0x87efx1f['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0x87efx1a = dataTableSt['length'] - 1;
                var _0x87efx2b = dataTableSt[_0x87efx1a];
                var _0x87efx20 = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0x87efx20 += '<tr>';
                _0x87efx20 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x87efx20 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0x87efx20 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Closing</th>';
                _0x87efx20 += '</tr>';
                _0x87efx20 += '<tr>';
                _0x87efx20 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x87efx20 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x87efx20 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x87efx20 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x87efx20 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x87efx20 += '</tr>';
                _0x87efx20 += '<tr>';
                _0x87efx20 += '<td style=background-color:#d4d4d4;>';
                _0x87efx20 += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0x87efx20 += '</td>';
                var _0x87efx42 = 0;
                var _0x87efx43 = 0;
                var _0x87efx44 = 0;
                $['each'](_0x87efx2b, function (_0x87efx30, _0x87efx31) {
                    var _0x87efx32 = _0x87efx30;
                    if (_0x87efx31 == null || _0x87efx31 == '0') {
                        _0x87efx31 = ''
                    };
                    if (_0x87efx32['includes']('_ABT') || _0x87efx32['includes']('_ACT')) {
                        var _0x87efx45 = parseFloat(_0x87efx31)['toFixed'](2);
                        DeAct_Val = _0x87efx45;
                        if (_0x87efx43 == 0) {
                            DeAct_Val = _0x87efx45;
                            _0x87efx42 = parseFloat(Active_Val[0]) + parseFloat(DeAct_Val);
                            _0x87efx20 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[0]) + '</td>';
                            _0x87efx20 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        } else {
                            DeAct_Val = _0x87efx45;
                            _0x87efx44 = parseFloat(Active_Val[1]) + parseFloat(DeAct_Val);
                            _0x87efx20 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[1]) + '</td>';
                            _0x87efx20 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        };
                        _0x87efx43 += 1
                    }
                });
                _0x87efx20 += '</tr>';
                _0x87efx20 += '<tr>';
                _0x87efx20 += '<td style=background-color:#d4d4d4;>';
                _0x87efx20 += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0x87efx20 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x87efx42 + '</span></td>';
                _0x87efx20 += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x87efx44 + '</span></td>';
                _0x87efx20 += '</td>';
                _0x87efx20 += '</tr>';
                _0x87efx20 += '</table>';
                $('#div_DeAct')['append'](_0x87efx20)
            }
        },
        error: function (_0x87efx38) {
            $('.modal')['hide']()
        }
    })
}

function storeUserDataInSession(_0x87efx47) {
    sessionStorage['clear']();
    var _0x87efx48 = JSON['stringify'](_0x87efx47);
    window['sessionStorage']['setItem']('userObject', _0x87efx48)
}